using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using System.Data;
using EPolicy.XmlCooker;
using Baldrich.DBRequest;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Drawing;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EPolicy
{
    public partial class HomeOwners : System.Web.UI.Page
    {

        private string ClientIDPPS = "";

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            //Setup top Banner
            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            //Setup top Banner
            //Control BannerLIST = new Control();
            //BannerLIST = LoadControl(@"TODOLIST.ascx");
            //this.PlaceHolder1.Controls.Add(BannerLIST);

            //Control leftMenu = new Control();
            //leftMenu = LoadControl(@"LeftMenu.ascx");
            //this.phTopBanner1.Controls.Add(leftMenu);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            //Control Banner = new Control();
            //Banner = LoadControl(@"TopBannerNew.ascx");
            //this.phTopBanner.Controls.Add(Banner);

            ////Setup top Banner
            //Control BannerLIST = new Control();
            //BannerLIST = LoadControl(@"TODOLIST.ascx");
            //this.PlaceHolder1.Controls.Add(BannerLIST);

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "SetWaitCursor();", true);

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            if (cp == null)
            {
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                if (!cp.IsInRole("HOME OWNERS") && !cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("AUTO VI ADMINISTRATOR")) // Cambiar
                {
                    Response.Redirect("Default.aspx?001");
                }
            }

            if (cp.UserID == 1)
                btnSentToPPS.Visible = true;
            else
                btnSentToPPS.Visible = false;

            if (Page.IsPostBack)
            {
                string targetId = Page.Request.Params.Get("__EVENTTARGET");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "focusthis", "document.getElementById('" + targetId + "').focus()", true);

                if (Session["TaskControl"] == null)
                {
                    Response.Redirect("Default.aspx?007");
                }
            }
            else
            {
                if (Session["LookUpTables"] == null)
                {
                    FillLookupTables();
                }
            }

            if (Session["AutoPostBack"] == null)
            {
                if (!IsPostBack)
                {
                    if (Session["TaskControl"] != null)
                    {
                        EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                        AccordionClient.SelectedIndex = -1;
                        AccordionAppPolicy.SelectedIndex = -1;
                        AccordionDrivers.SelectedIndex = -1;
                        Accordion6.SelectedIndex = -1;
                        AccordionAdditionalInsured.SelectedIndex = -1;

                        switch (taskControl.Mode)
                        {
                            case 1: //ADD

                                if (Session["Customer"] != null)
                                {
                                    taskControl.Customer = (EPolicy.Customer.Customer)Session["Customer"];
                                }
                                EnableControls();
                                FillTextControl();
                                if (1 == 2)
                                {
                                    if (Session["FromConvert"] != null)
                                    {
                                        txtEffectiveDate.Text = "";
                                        BtnPremiumFinance.Visible = false;
                                        Session["FromConvert"] = null;
                                        CheckBank.Checked = true;
                                        chkBank_CheckedChange(String.Empty, EventArgs.Empty);
                                    }
                                }
                                break;

                            case 2: //UPDATE
                                EnableControls();
                                FillTextControl();

                                break;

                            default:
                                DisableControls();
                                FillTextControl();

                                break;
                        }

                        if (!taskControl.isQuote)
                        {
                            if (BtnEdit.Visible)
                            {
                                btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;
                                btnQuote.Visible = false; btnQuote2.Visible = false;
                            }
                            else
                            {
                                //Victor-IssuePolicy
                                btnSavePolicy.Visible = true; btnSavePolicy2.Visible = true;
                                //btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;

                                btnQuote.Visible = false; btnQuote2.Visible = false;
                                imgCalendarEff.Visible = true;
                            }

                        }
                    }
                }
                else
                {
                    if (Session["TaskControl"] != null)
                    {
                        EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                        if (taskControl.Mode == 4)
                        {

                            DisableControls();
                            //FillTextControl();.

                        }
                    }
                }
            }
            else
            {
                EnableControls();
                FillTextControl();

                Session.Remove("AutoPostBack");
            }

            if ((!Page.IsPostBack && (Request.QueryString["PolicyIDPPS"] != "" && Request.QueryString["PolicyIDPPS"] != null)) || (!Page.IsPostBack && (Request.QueryString["PolicyIDePPS"] != "" && Request.QueryString["PolicyIDePPS"] != null))) 
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                DivRenew1.Visible = true;
                DivRenew2.Visible = true;
                DivRenew3.Visible = true;
                string policyID;
                if (Request.QueryString["PolicyIDPPS"] != "" && Request.QueryString["PolicyIDPPS"] != null)
                {
                    policyID = Request.QueryString["PolicyIDPPS"];
                }
                else
                {
                    policyID = Request.QueryString["PolicyIDePPS"];
                }
                String[] arrayPolicytoRenew;
                arrayPolicytoRenew = policyID.Trim().Split('-');
                txtPolicyNoToRenew.Text = arrayPolicytoRenew[0];
                if (txtPolicyNoToRenew.Text.Contains("HOM"))
                {
                    txtPolicyNoToRenew.Text = txtPolicyNoToRenew.Text.Replace("HOM", "");
                    txtPolicyToRenewType.Text = "HOM";
                    txtPreviousPolicyType.Text = "HOM";
                    txtPolicyType.Text = "HOM";
                }
                else
                {
                    txtPolicyNoToRenew.Text = txtPolicyNoToRenew.Text.Replace("INC", "");
                    txtPolicyToRenewType.Text = "INC";
                    txtPreviousPolicyType.Text = "INC";
                    txtPolicyType.Text = "INC";
                }
                txtPolicyNoToRenewSuffix.Text = arrayPolicytoRenew[1];
                if (Request.QueryString["PolicyIDPPS"] != "" && Request.QueryString["PolicyIDPPS"] != null)
                {
                    CheckHomeOwnersExistPPS();
                }
                else
                {
                    FillRenHOEPPS(GetTaskControlByPolicyTypeAndPolicyNoAndSuffix(txtPolicyToRenewType.Text, txtPolicyNoToRenew.Text, txtPolicyNoToRenewSuffix.Text));
                }               
            }
            if (!IsPostBack)
            {
                //lblBank.ForeColor = System.Drawing.Color.Gray;
                //lblLoanNo.ForeColor = System.Drawing.Color.Gray;
                //lblTypeOfInterest.ForeColor = System.Drawing.Color.Gray;
                //btnBankList.Enabled = false;
                //txtBank.Enabled = false;
                //txtLoanNo.Enabled = false;
                //txtBank2.Enabled = false;
                //txtLoanNo2.Enabled = false;
                //ddlTypeOfInterest.Enabled = false;
            }
            else
            {
                chkBank_CheckedChange(String.Empty, EventArgs.Empty);
            }
        }

        protected void FillLookupTables()
        {
            System.Data.DataTable dtAgent = GetAgency();
            System.Data.DataTable dtLocation = EPolicy.LookupTables.LookupTables.GetTable("Location");
            System.Data.DataTable dtVehicleTerritory = EPolicy.LookupTables.LookupTables.GetTable("VI_VehicleTerritory");
            System.Data.DataTable dtBankList = EPolicy.LookupTables.LookupTables.GetTable("Bank_VI");
            System.Data.DataTable dtDiscountsHomeOwners = EPolicy.LookupTables.LookupTables.GetTable("DiscountsHomeOwners");

            System.Data.DataTable dtTypeOfInsured = EPolicy.LookupTables.LookupTables.GetTable("TypeOfInsured");
            //VehicleTerritory
            ddlIsland.DataSource = dtVehicleTerritory;
            ddlIsland.DataTextField = "VehicleTerritoryDesc";
            ddlIsland.DataValueField = "VehicleTerritoryID";
            ddlIsland.DataBind();
            ddlIsland.SelectedIndex = -1;
            ddlIsland.Items.Insert(0, "");

            //Location
            ddlOffice.DataSource = dtLocation;
            ddlOffice.DataTextField = "locationDesc";
            ddlOffice.DataValueField = "locationID";
            ddlOffice.DataBind();
            ddlOffice.SelectedIndex = -1;
            ddlOffice.Items.Insert(0, "");

            //TypeOfInsured
            ddlTypeOfInsured.DataSource = dtTypeOfInsured;
            ddlTypeOfInsured.DataTextField = "TypeOfInsuredDesc";
            ddlTypeOfInsured.DataValueField = "TypeOfInsuredID";
            ddlTypeOfInsured.DataBind();
            ddlTypeOfInsured.SelectedIndex = -1;
            ddlTypeOfInsured.Items.Insert(0, "");
            

            //for (int i = ddlOffice.Items.Count - 1; i >= 0; i--)
            //{
            //    if (!ddlOffice.Items[i].Text.ToString().Contains("GUARDIAN") && ddlOffice.Items[i].Text.ToString() != "")
            //    {
            //        ddlOffice.Items.Remove(ddlOffice.Items[i]);
            //    }
            //}

            //Agent VI
            ddlAgency.DataSource = dtAgent;
            ddlAgency.DataTextField = "AgentDesc";
            ddlAgency.DataValueField = "AgentID";
            ddlAgency.DataBind();
            ddlAgency.SelectedIndex = -1;
            ddlAgency.Items.Insert(0, "");

            foreach (ListItem listItem in this.ddlAgency.Items)
            {
                listItem.Attributes.Add("title", listItem.Text);
            }
            ddlAgency.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");


            //BANK_VI
            ddlBankList.DataSource = dtBankList;
            ddlBankList.DataTextField = "BankDesc";
            ddlBankList.DataValueField = "PPSID";
            ddlBankList.DataBind();
            ddlBankList.SelectedIndex = -1;
            ddlBankList.Items.Insert(0, "");

            //BANK_VI 2
            ddlBankList2.DataSource = dtBankList;
            ddlBankList2.DataTextField = "BankDesc";
            ddlBankList2.DataValueField = "PPSID";
            ddlBankList2.DataBind();
            ddlBankList2.SelectedIndex = -1;
            ddlBankList2.Items.Insert(0, "");


            

                 //BANK_VI 2
            ddlDiscount.DataSource = dtDiscountsHomeOwners;
            ddlDiscount.DataTextField = "DiscountsHomeOwnersDesc";
            ddlDiscount.DataValueField = "DiscountsHomeOwnersID";
            ddlDiscount.DataBind();
            ddlDiscount.SelectedIndex = ddlDiscount.Items.IndexOf(ddlDiscount.Items.FindByText("0"));
            //ddlDiscount.SelectedIndex = -1;
            //ddlDiscount.Items.Insert(0, "");


            Session.Add("LookUpTables", "In");
        }

        private void RemoveSessionLookUp()
        {
            Session.Remove("LookUpTables");
        }


        private void EnableControls()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            if (taskControl.isQuote)
            {
                if (cp.IsInRole("HOME OWNERS REQUEST"))// && !taskControl.approved
                {
                    //btnSubmit.Visible = false; btnSubmit2.Visible = false;
                    btnQuote.Visible = true; btnQuote2.Visible = true;
                }
                else
                {
                    // btnSubmit.Visible = false; btnSubmit2.Visible = false;
                    btnQuote.Visible = true; btnQuote2.Visible = true;
                }

                btnSubmit.Visible = true; btnSubmit2.Visible = true;

                if (taskControl.approved)
                {
                    BtnConvert.Visible = true; BtnConvert2.Visible = true;
                }
                else
                {
                    BtnConvert.Visible = false; BtnConvert2.Visible = false;
                }

                btnAdjuntar.Visible = false;
                BtnEdit.Visible = false; BtnEdit2.Visible = false;
                btnSavePolicy.Enabled = false; btnSavePolicy2.Enabled = false;
                btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;
                btnPrintQuote.Visible = false; btnPrintQuote2.Visible = false;
                btnPrintApplication.Visible = true;//false

                btnCloneQuote.Visible = false;
                //BtnConvert.Visible = false; BtnConvert2.Visible = false;

                //txtAutoPolicyNo.Visible = false;
                //lblAutoPolicyNo.Visible = false;



                //if (txtPolicyType.Text.Trim() == "INC")
                //{
                //    ddlLiaLimit.Items[0].Text = "NO COVERAGE";
                //    ddlLiaLimit.SelectedIndex = 0;
                //    txtLiaMedicalPayments.Text = "NO COVERAGE";
                //    txtliaPremium.Text = "$0.00";
                //    ddlLiaLimit.Enabled = false;
                //}
                //else 
                //{
                //    ddlLiaLimit.Enabled = true;
                //}
            }
            else
            {
                //En el modo Convert Solo debe poder cambiarse Effective y Expiration Date
                DisableControls();

                btnAdjuntar.Visible = false;
                btnSubmit.Visible = false; btnSubmit2.Visible = false;
                BtnEdit.Visible = false; BtnEdit2.Visible = false;
                //Victor-IssuePolicy
                btnSavePolicy.Enabled = true; btnSavePolicy2.Enabled = true;
                btnSavePolicy.Visible = true; btnSavePolicy2.Visible = true;
                //btnSavePolicy.Enabled = false; btnSavePolicy2.Enabled = false;
                //btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;				

                BtnConvert.Visible = false; BtnConvert2.Visible = false;
                btnQuote.Visible = false; btnQuote2.Visible = false;
                BtnPrintPolicy.Visible = false; BtnPrintPolicy2.Visible = false;
                btnPrintDec.Visible = false;
                btnPrintInvoice.Visible = false;

                btnCloneQuote.Visible = false;

                txtEffectiveDate.Enabled = true;
                txtExpirationDate.Enabled = true;

                CheckBank.Checked = true;
                chkBank_CheckedChange(String.Empty, EventArgs.Empty);

                btnBankList.Enabled = true;
                txtBank.Enabled = true;
                txtBank2.Enabled = true;
                txtLoanNo.Enabled = true;
                txtLoanNo2.Enabled = true;
                ddlTypeOfInterest.Enabled = true;
                return;
            }

            if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
            {
                ddlAgency.Enabled = false;
                ddlOffice.Enabled = false;
                lblOffice.Visible = false;
                ddlOffice.Visible = false;
                txtEffectiveDate.Enabled = false;
                txtExpirationDate.Enabled = false;
            }
            else
            {
                ddlAgency.Enabled = true;
                ddlOffice.Enabled = true;
                lblOffice.Visible = true;
                ddlOffice.Visible = true;
                txtEffectiveDate.Enabled = true;
                txtExpirationDate.Enabled = true;
            }

            if (cp.IsInRole("AUTO VI AGENCY"))
            {
                ddlAgency.Enabled = true;
            }

            btnStatus.Visible = false;

            btnCalculate.Visible = true; btnCalculate2.Visible = true;
            ////BtnSave.Visible = false;
            BtnExit.Visible = false; BtnExit2.Visible = false;
            BtnCancel.Visible = true; BtnCancel2.Visible = true;
            BtnPrintPolicy.Visible = false; BtnPrintPolicy2.Visible = false;
            btnPrintDec.Visible = false;
            btnPrintInvoice.Visible = false;
            btnPrintQuote.Visible = false; btnPrintQuote2.Visible = false;
            btnPrintApplication.Visible = true;//false
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtMailingAddress.Enabled = true;
            txtMailingAddress2.Enabled = true;
            txtPhysicalAddress1.Enabled = true;
            txtPhysicalAddress1.Enabled = true;
            txtCity.Enabled = true;
            txtCity2.Enabled = true;
            txtState.Enabled = true;
            txtState2.Enabled = true;
            txtZipCode.Enabled = true;
            txtZipCode2.Enabled = true;
            btnBankList.Enabled = true;
            txtBank.Enabled = true;
            txtBank2.Enabled = true;
            txtLoanNo.Enabled = true;
            txtLoanNo2.Enabled = true;
            ddlTypeOfInterest.Enabled = true;
            rdbNo.Enabled = true;
            rdbYes.Enabled = true;
            txtIfYes.Enabled = false;
            ddlMortgageeBilled.Enabled = true;
            txtCatastropheCoverage.Enabled = false;
            ddlCatastropheDeduc.Enabled = true;
            txtWindstormDeductible.Enabled = false;
            txtAllOtherPerilsDeductible.Enabled = false;
            ddlConstructionType.Enabled = true;
            txtConstructionYear.Enabled = true;
            ddlNumberOfStories.Enabled = true;
            ddlNumOfFamilies.Enabled = true;
            txtIfYes.Enabled = true;
            txtLivingArea.Enabled = true;
            txtPorches.Enabled = true;
            ddlRoofDwelling.Enabled = true;
            txtEarthQuakeDeductible.Enabled = false;
            ddlResidence.Enabled = true;
            ddlPropertyType.Enabled = true;
            txtPropertyForm.Enabled = false;
            txtPolicyType.Enabled = true;
            ddlLosses3Years.Enabled = true;
            txtOtherStruct.Enabled = false;
            ddlPropertyShuttered.Enabled = true;
            ddlRoofOverhang.Enabled = true;
            ddlAutoPolicyWitGuardian.Enabled = true;
            txtAutoPolicyNo.Enabled = false;
            txtLimit1.Enabled = true;
            txtLimit2.Enabled = true;
            txtLimit3.Enabled = true;
            txtLimit4.Enabled = true;
            txtTotalLimit.Enabled = false;
            txtAOPDed1.Enabled = false;
            txtAOPDed2.Enabled = false;
            txtAOPDed3.Enabled = false;
            txtAOPDed4.Enabled = false;
            txtWindstormDed1.Enabled = false;
            txtWindstormDed2.Enabled = false;
            txtWindstormDed3.Enabled = false;
            txtWindstormDed4.Enabled = false;
            txtWindstormDedPer1.Enabled = false;
            txtWindstormDedPer2.Enabled = false;
            txtWindstormDedPer3.Enabled = false;
            txtWindstormDedPer4.Enabled = false;
            txtEarthquakeDed1.Enabled = false;
            txtEarthquakeDed2.Enabled = false;
            txtEarthquakeDed3.Enabled = false;
            txtEarthquakeDed4.Enabled = false;
            txtEarthQuakeDedPer1.Enabled = false;
            txtEarthQuakeDedPer2.Enabled = false;
            txtEarthQuakeDedPer3.Enabled = false;
            txtEarthQuakeDedPer4.Enabled = false;
            txtCoinsurance1.Enabled = false;
            txtCoinsurance2.Enabled = false;
            txtCoinsurance3.Enabled = false;
            txtCoinsurance4.Enabled = false;
            txtPremium1.Enabled = false;
            txtPremium2.Enabled = false;
            txtPremium3.Enabled = false;
            txtPremium4.Enabled = false;
            txtTotalLimit.Enabled = false;
            txtTotalWind.Enabled = false;
            txtTotalEarth.Enabled = false;
            //txtTotalPremium.Enabled = true;
            txtLiaPropertyType.Enabled = false;
            txtLiaPolicyType.Enabled = false;
            txtLiaNumOfFamilies.Enabled = false;
            ddlLiaLimit.Enabled = true;
            txtLiaMedicalPayments.Enabled = false;
            txtLiaPremium.Enabled = false;
            txtPremium.Enabled = false;
            txtPolicyType.Enabled = false;
            //txtEffectiveDate.Enabled = false;
            //txtExpirationDate.Enabled = false;
            txtTotalPremium.Enabled = false;
            txtOccupation.Enabled = true;
            txtBusinessPhone.Enabled = true;
            txtMobilePhone.Enabled = true;
            txtEmail.Enabled = true;
            rdbNoStruct.Enabled = true;
            rdbYesStruct.Enabled = true;
            txtComments.Enabled = true;
            CheckBank.Enabled = false;
            chkMailSame.Enabled = true;
            txtComment.Enabled = true;
            ddlIsland.Enabled = true;
            //txtTotalPremium.Enabled = true;

            txtInspectionDate.Enabled = true;
            txtInspector.Enabled = true;
            ddlTypeOfInsured.Enabled = true;

            txtAdditionalInsuredName.Enabled = true;
            txtAdditionalInsuredLastName.Enabled = true;
            BtnAddInsured.Text = "ADD INSURED";
            BtnAddInsured.Enabled = true;


            if (cp.IsInRole("HOMEOWNERS DISCOUNT"))
            {
                ddlDiscount.Enabled = true;
          //      ddlDiscount.Visible = true;
          //      lblDiscount.Visible = true;
          //  }
          // else
          //  {
          //      ddlDiscount.Enabled = false;
          //      ddlDiscount.Visible = false;
          //      lblDiscount.Visible = false;
          //  }

          //  if (txtPolicyType.Text.Trim() == "INC")
          //  {
          //      ddlLiaLimit.Items[0].Text = "NO COVERAGE";
          //      ddlLiaLimit.SelectedIndex = 0;
          //      txtLiaMedicalPayments.Text = "NO COVERAGE";
          //      txtliaPremium.Text = "$0.00";
          //      ddlLiaLimit.Enabled = false;
          //  }
          //  else
          //  {
          //      ddlLiaLimit.Enabled = true;
            }
            

        }

        private void DisableControls()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            System.Data.DataTable dt = null;
            dt = GetVerifyPolicyExist(taskControl.TaskControlID);

            imgCalendarEff.Visible = false;

            if (cp.IsInRole("HOME OWNERS REQUEST"))
            {
                if (cp.IsInRole("HOME OWNERS REQUEST") && cp.IsInRole("HOME OWNERS APPROVE"))
                    btnStatus.Visible = true;
                else
                    btnStatus.Visible = false;
            }
            else
                btnStatus.Visible = true;

            if (taskControl.isQuote)
            {
                if (dt != null)
                {
                    //If this quote has been converted to policy (Read Only)
                    if (dt.Rows.Count > 0)
                    {
                        if (cp.IsInRole("HOME OWNERS REQUEST"))
                        {
                            ReadOnly();
                            return;
                        }
                    }
                    else if (taskControl.submitted && taskControl.approved)
                    {
                        //When the Quote has been approved by a Admin the user can convert to policy but not edit this quote
                        ReadOnly();
                        BtnConvert.Visible = true; BtnConvert2.Visible = true;//true when policy
                        return;
                    }
                }

                if (cp.IsInRole("HOME OWNERS REQUEST"))
                {
                    BtnEdit.Visible = true; BtnEdit2.Visible = true;
                    btnPrintQuote.Visible = true; btnPrintQuote2.Visible = true;
                    btnPrintApplication.Visible = true;
                    //btnSubmit.Visible = true; btnSubmit2.Visible = true;
                    //BtnConvert.Visible = false; BtnConvert2.Visible = false;
                }
                else
                {
                    BtnEdit.Visible = true; BtnEdit2.Visible = true;
                    btnPrintQuote.Visible = true; btnPrintQuote2.Visible = true;
                    btnPrintApplication.Visible = true;
                    //btnSubmit.Visible = false; btnSubmit2.Visible = false;
                    //BtnConvert.Visible = true; BtnConvert2.Visible = true;
                }

                btnSubmit.Visible = true; btnSubmit2.Visible = true;
                if (taskControl.approved)
                {
                    BtnConvert.Visible = true; BtnConvert2.Visible = true;
                }
                else
                {
                    BtnConvert.Visible = false; BtnConvert2.Visible = false;
                }

                btnCloneQuote.Visible = true;

                btnAdjuntar.Visible = true;
                btnQuote.Visible = false; btnQuote2.Visible = false;
                btnSavePolicy.Enabled = false; btnSavePolicy2.Enabled = false;
                btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;
                BtnPrintPolicy.Visible = false; BtnPrintPolicy2.Visible = false;
                btnPrintDec.Visible = false;
                btnPrintInvoice.Visible = false;

                if (1 == 1)
                {
                    BtnPremiumFinance.Visible = true;
                }
                btnFM2.Enabled = false;
                btnFM5.Enabled = false;
                btnFM8.Enabled = false;
            }
            else
            {
                btnAdjuntar.Visible = true;
                btnSubmit.Visible = false; btnSubmit2.Visible = false;
                BtnEdit.Visible = false; BtnEdit2.Visible = false;
                BtnPrintPolicy.Visible = true; BtnPrintPolicy2.Visible = true;
                btnPrintDec.Visible = true;
                btnPrintInvoice.Visible = true;
                btnQuote.Visible = false; btnQuote2.Visible = false;
                btnSavePolicy.Enabled = false; btnSavePolicy2.Enabled = false;
                btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;
                btnPrintQuote.Visible = false; btnPrintQuote2.Visible = false;
                btnPrintApplication.Visible = false;
                btnStatus.Visible = false;

                btnCloneQuote.Visible = false;

                if (1 == 1)
                    BtnPremiumFinance.Visible = true;
                btnFM2.Enabled = true;
                btnFM5.Enabled = true;
                btnFM8.Enabled = true;
            }

            if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
            {
                ddlOffice.Enabled = false;
                lblOffice.Visible = false;
                ddlOffice.Visible = false;
            }
            else
            {
                ddlOffice.Enabled = false;
            }

            ddlAgency.Enabled = false;
            BtnExit.Visible = true; BtnExit2.Visible = true;
            BtnCancel.Visible = false; BtnCancel2.Visible = false;
            btnCalculate.Visible = false; btnCalculate2.Visible = false;

            if (cp.UserID == 1)
                btnCalculate.Visible = true; btnCalculate2.Visible = true;

            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtPhysicalAddress1.Enabled = false;
            txtPhysicalAddress1.Enabled = false;
            txtMailingAddress.Enabled = false;
            txtMailingAddress2.Enabled = false;
            txtCity.Enabled = false;
            txtCity2.Enabled = false;
            txtState.Enabled = false;
            txtState2.Enabled = false;
            txtZipCode.Enabled = false;
            txtZipCode2.Enabled = false;
            btnBankList.Enabled = false;
            txtBank.Enabled = false;
            txtBank2.Enabled = false;
            txtLoanNo.Enabled = false;
            txtLoanNo2.Enabled = false;
            ddlTypeOfInterest.Enabled = false;
            ddlMortgageeBilled.Enabled = false;
            txtEffectiveDate.Enabled = false;
            txtExpirationDate.Enabled = false;
            txtCatastropheCoverage.Enabled = false;
            ddlCatastropheDeduc.Enabled = false;
            txtWindstormDeductible.Enabled = false;
            txtAllOtherPerilsDeductible.Enabled = false;
            ddlConstructionType.Enabled = false;
            txtConstructionYear.Enabled = false;
            ddlNumberOfStories.Enabled = false;
            ddlNumOfFamilies.Enabled = false;
            txtIfYes.Enabled = false;
            txtLivingArea.Enabled = false;
            txtPorches.Enabled = false;
            ddlRoofDwelling.Enabled = false;
            rdbNo.Enabled = false;
            rdbYes.Enabled = false;
            txtEarthQuakeDeductible.Enabled = false;
            ddlResidence.Enabled = false;
            ddlPropertyType.Enabled = false;
            txtPropertyForm.Enabled = false;
            txtPolicyType.Enabled = false;
            ddlLosses3Years.Enabled = false;
            txtOtherStruct.Enabled = false;
            ddlPropertyShuttered.Enabled = false;
            ddlRoofOverhang.Enabled = false;
            ddlAutoPolicyWitGuardian.Enabled = false;
            txtAutoPolicyNo.Enabled = false;
            txtLimit1.Enabled = false;
            txtLimit2.Enabled = false;
            txtLimit3.Enabled = false;
            txtLimit4.Enabled = false;
            txtAOPDed1.Enabled = false;
            txtAOPDed2.Enabled = false;
            txtAOPDed3.Enabled = false;
            txtAOPDed4.Enabled = false;
            txtWindstormDed1.Enabled = false;
            txtWindstormDed2.Enabled = false;
            txtWindstormDed3.Enabled = false;
            txtWindstormDed4.Enabled = false;
            txtWindstormDedPer1.Enabled = false;
            txtWindstormDedPer2.Enabled = false;
            txtWindstormDedPer3.Enabled = false;
            txtWindstormDedPer4.Enabled = false;
            txtEarthquakeDed1.Enabled = false;
            txtEarthquakeDed2.Enabled = false;
            txtEarthquakeDed3.Enabled = false;
            txtEarthquakeDed4.Enabled = false;
            txtEarthQuakeDedPer1.Enabled = false;
            txtEarthQuakeDedPer2.Enabled = false;
            txtEarthQuakeDedPer3.Enabled = false;
            txtEarthQuakeDedPer4.Enabled = false;
            txtCoinsurance1.Enabled = false;
            txtCoinsurance2.Enabled = false;
            txtCoinsurance3.Enabled = false;
            txtCoinsurance4.Enabled = false;
            txtPremium1.Enabled = false;
            txtPremium2.Enabled = false;
            txtPremium3.Enabled = false;
            txtPremium4.Enabled = false;
            txtTotalLimit.Enabled = false;
            txtTotalWind.Enabled = false;
            txtTotalEarth.Enabled = false;
            //txtTotalPremium.Enabled = false;
            txtLiaPropertyType.Enabled = false;
            txtLiaPolicyType.Enabled = false;
            txtLiaNumOfFamilies.Enabled = false;
            ddlLiaLimit.Enabled = false;
            txtLiaMedicalPayments.Enabled = false;
            txtLiaPremium.Enabled = false;
            txtPremium.Enabled = false;
            rdbNoStruct.Enabled = false;
            rdbYesStruct.Enabled = false;
            txtOtherStruct.Enabled = false;
            txtTotalPremium.Enabled = false;
            chkMailSame.Enabled = false;
            txtOccupation.Enabled = false;
            txtBusinessPhone.Enabled = false;
            txtMobilePhone.Enabled = false;
            txtEmail.Enabled = false;
            CheckBank.Enabled = false;
            txtComments.Enabled = false;
            txtComment.Enabled = false;
            ddlIsland.Enabled = false;

            txtInspectionDate.Enabled = false;
            txtInspector.Enabled = false;
            ddlDiscount.Enabled = false;
            ddlTypeOfInsured.Enabled = false;

            txtAdditionalInsuredName.Enabled = false;
            BtnAddInsured.Enabled = false;
            GridInsured.Enabled = false;
        }

        public void rdbYes_CheckedChanged(object sender, EventArgs e)
        {
            //My Code
            txtIfYes.Enabled = true;
            lblIfYes.ForeColor = Color.Red;
        }

        public void rdbYesStruct_CheckedChanged(object sender, EventArgs e)
        {
            //My Code
            txtOtherStruct.Enabled = true;
            lblOtherSruct.ForeColor = Color.Red;
        }


        public void rdbNoStruct_CheckedChanged(object sender, EventArgs e)
        {
            //My Code
            txtOtherStruct.Text = "";
            txtOtherStruct.Enabled = false;
            lblOtherSruct.ForeColor = Color.Gray;
        }

        public void rdbNo_CheckedChanged(object sender, EventArgs e)
        {
            //My Code
            txtIfYes.Text = "";
            txtIfYes.Enabled = false;
            lblIfYes.ForeColor = Color.Gray;
        }

        public void chkMailSame_CheckedChange(object sender, EventArgs e)
        {
            //My Code
            if (chkMailSame.Checked)
            {
                txtPhysicalAddress1.Text = txtMailingAddress.Text;
                txtPhysicalAddress2.Text = txtMailingAddress2.Text;
                txtCity2.Text = txtCity.Text;
                txtState2.Text = txtState.Text;
                txtZipCode2.Text = txtZipCode.Text;
            }
            else
            {
                txtPhysicalAddress1.Text = "";
                txtPhysicalAddress2.Text = "";
                txtCity2.Text = "";
                txtState2.Text = "";
                txtZipCode2.Text = "";
            }
        }

        protected void FillProperties()
        {
            try
            {
                //EPolicy.TaskControl.Dwelling taskControl = (EPolicy.TaskControl.Dwelling)Session["TaskControl"];
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                //if (!cp.IsInRole("HOME OWNERS REQUEST"))
                //    taskControl.approved = false;//APPROVE WHEN NOT REQUEST
                //else
                //    taskControl.approved = true;

                taskControl.Customer.FirstName = txtFirstName.Text.ToString().Trim().ToUpper();
                taskControl.Customer.LastName1 = txtLastName.Text.ToString().Trim().ToUpper();

                taskControl.Customer.AddressPhysical1 = txtPhysicalAddress1.Text.ToString().Trim().ToUpper();
                taskControl.Customer.AddressPhysical2 = txtPhysicalAddress2.Text.ToString().Trim().ToUpper();
                taskControl.Customer.CityPhysical = txtCity2.Text.ToString().Trim().ToUpper();
                taskControl.Customer.StatePhysical = txtState2.Text.ToString().Trim().ToUpper();
                taskControl.Customer.ZipPhysical = txtZipCode2.Text.ToString().Trim().ToUpper();
                taskControl.Customer.Address1 = txtMailingAddress.Text.ToString().Trim().ToUpper();
                taskControl.Customer.Address2 = txtMailingAddress2.Text.ToString().Trim().ToUpper();
                taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
                taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
                taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();
                taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
                taskControl.Customer.JobPhone = txtBusinessPhone.Text.ToString().Trim().ToUpper();
                taskControl.Customer.HomePhone = txtMobilePhone.Text.ToString().Trim().ToUpper();
                taskControl.Customer.LocationID = int.Parse(ddlOffice.SelectedItem.Value);//STT ONLY

                if (ClientIDPPS != "")
                {
                    taskControl.Customer.Description = ClientIDPPS;
                }

                if (rdbYes.Checked)
                {
                    taskControl.isUpgraded = true;
                    txtIfYes.Enabled = true;
                }
                else
                {
                    taskControl.isUpgraded = false;
                    txtIfYes.Enabled = false;
                }

                if (txtPolicyNoToRenew.Text.Trim() != "")
                {
                    taskControl.PreviousPolicy = txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim();
                }

                taskControl.TaskControlID = int.Parse(LblControlNo.Text.Trim());
                taskControl.occupation = txtOccupation.Text.ToString().Trim().ToUpper();

                taskControl.TaskStatusID = 31;

                taskControl.Suffix = txtSuffix.Text.ToString().Trim();
                taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();
                taskControl.bank = ddlBankList.SelectedItem.Text.Trim();//txtBank.Text.ToString().Trim();
                taskControl.bank2 = ddlBankList2.SelectedItem.Text.Trim();//txtBank2.Text.ToString().Trim();
                taskControl.BankPPSID = ddlBankList.SelectedItem.Value.Trim();
                taskControl.BankPPSID2 = ddlBankList2.SelectedItem.Value.Trim();
                taskControl.loanNo = txtLoanNo.Text.ToString().Trim();
                taskControl.loanNo2 = txtLoanNo2.Text.ToString().Trim();
                taskControl.typeOfInterest = ddlTypeOfInterest.SelectedItem.Value;

                taskControl.Inspector = txtInspector.Text.ToString().Trim();
                taskControl.InspectionDate = txtInspectionDate.Text.Trim();

                if (ddlMortgageeBilled.SelectedItem.Value == "Yes")
                {
                    taskControl.mortgageeBilled = true;

                }
                else
                {
                    taskControl.mortgageeBilled = false;
                }

                if (!taskControl.isQuote)
                {
                    taskControl.EffectiveDate = txtEffectiveDate.Text.ToString().Trim();
                    taskControl.ExpirationDate = txtExpirationDate.Text.ToString().Trim();
                }
                else if (taskControl.isQuote && taskControl.isRenew)
                {
                    taskControl.EffectiveDate = txtEffectiveDate.Text.ToString().Trim();
                    taskControl.ExpirationDate = txtExpirationDate.Text.ToString().Trim();
                }

                if (taskControl.isQuote)
                {
                    taskControl.EntryDate = DateTime.Now;
                    //Pedido Por Ramon:
                    //Se supone que si yo llamo una cotización con fecha previa al día de hoy, cuando le de save, me traiga la fecha de hoy, como fecha de cotización. 

                }
                
                taskControl.catastropheCov = txtCatastropheCoverage.Text.ToString().Trim();
                taskControl.catastropheDeduc = ddlCatastropheDeduc.SelectedItem.Value;
                taskControl.windstormDeduc = txtWindstormDeductible.Text.ToString().Trim();
                taskControl.allOtherPerDeduc = txtAllOtherPerilsDeductible.Text.ToString().Trim();
                taskControl.constructionType = ddlConstructionType.SelectedItem.Value;
                taskControl.constructionYear = txtConstructionYear.Text.ToString().Trim();
                taskControl.numOfStories = ddlNumberOfStories.SelectedItem.Value;
                taskControl.numOfFamilies = ddlNumOfFamilies.SelectedItem.Value;
                taskControl.ifYes = txtIfYes.Text.ToString().Trim();
                taskControl.livingArea = txtLivingArea.Text.ToString().Trim();
                taskControl.porshcesDeck = txtPorches.Text.ToString().Trim();
                taskControl.roofDwelling = ddlRoofDwelling.SelectedItem.Value;
                taskControl.earthquakeDeduc = txtEarthQuakeDeductible.Text.ToString().Trim();
                taskControl.residence = ddlResidence.SelectedItem.Value;
                taskControl.propertyType = ddlPropertyType.SelectedItem.Value;
                taskControl.propertyForm = txtPropertyForm.Text.ToString().Trim();
                taskControl.PolicyType = txtPolicyType.Text.ToString().Trim();

                if (rdbNoStruct.Checked)
                {
                    taskControl.additionalStructure = false;
                }
                else
                {
                    taskControl.additionalStructure = true;
                }

                taskControl.comments = txtComments.Text.ToString().Trim();

                if (ddlLosses3Years.SelectedItem.Value == "Yes")
                {
                    taskControl.losses3Year = true;

                }
                else
                {
                    taskControl.losses3Year = false;
                }


                taskControl.otherStructuresType = txtOtherStruct.Text.ToString().Trim();

                if (ddlPropertyShuttered.SelectedItem.Value == "Yes")
                {
                    taskControl.isPropShuttered = true;
                }
                else
                {
                    taskControl.isPropShuttered = false;
                }

                taskControl.roofOverhang = ddlRoofOverhang.SelectedItem.Value;

                if (ddlAutoPolicyWitGuardian.SelectedItem.Value == "Yes")
                {
                    taskControl.autoPolicy = true;
                }
                else
                {
                    taskControl.autoPolicy = false;
                }

                //taskControl.autoPolicyNo = txtAutoPolicyNo.Text.ToString().Trim();

                taskControl.limit1 = double.Parse(txtLimit1.Text.ToString().Replace("$", ""));
                taskControl.limit2 = double.Parse(txtLimit2.Text.ToString().Replace("$", ""));
                taskControl.limit3 = double.Parse(txtLimit3.Text.ToString().Replace("$", ""));
                taskControl.limit4 = double.Parse(txtLimit4.Text.ToString().Replace("$", ""));

                taskControl.aopDed1 = double.Parse(txtAOPDed1.Text.ToString().Replace("$", ""));
                taskControl.aopDed2 = double.Parse(txtAOPDed2.Text.ToString().Replace("$", ""));
                taskControl.aopDed3 = double.Parse(txtAOPDed3.Text.ToString().Replace("$", ""));
                taskControl.aopDed4 = double.Parse(txtAOPDed4.Text.ToString().Replace("$", ""));

                if (txtWindstormDed1.Text.ToString().Trim() == "")
                {
                    taskControl.windstormDed1 = 0.0;
                }
                else
                {
                    taskControl.windstormDed1 = double.Parse(txtWindstormDed1.Text.ToString().Replace("$", ""));
                }

                if (txtWindstormDed2.Text.ToString().Trim() == "")
                {
                    taskControl.windstormDed2 = 0.0;
                }
                else
                {
                    taskControl.windstormDed2 = double.Parse(txtWindstormDed2.Text.ToString().Replace("$", ""));
                }

                if (txtWindstormDed3.Text.ToString().Trim() == "")
                {
                    taskControl.windstormDed3 = 0.0;
                }
                else
                {
                    taskControl.windstormDed3 = double.Parse(txtWindstormDed3.Text.ToString().Replace("$", ""));
                }

                if (txtWindstormDed4.Text.ToString().Trim() == "")
                {
                    taskControl.windstormDed4 = 0.0;
                }
                else
                {
                    taskControl.windstormDed4 = double.Parse(txtWindstormDed4.Text.ToString().Replace("$", ""));
                }



                if ((txtEarthquakeDed1.Text.ToString().Trim() == ""))
                {
                    taskControl.earthquakeDed1 = 0.0;
                }
                else
                {
                    taskControl.earthquakeDed1 = double.Parse(txtEarthquakeDed1.Text.ToString().Replace("$", ""));
                }

                if ((txtEarthquakeDed2.Text.ToString().Trim() == ""))
                {
                    taskControl.earthquakeDed2 = 0.0;
                }
                else
                {
                    taskControl.earthquakeDed2 = double.Parse(txtEarthquakeDed2.Text.ToString().Replace("$", ""));
                }

                if ((txtEarthquakeDed3.Text.ToString().Trim() == ""))
                {
                    taskControl.earthquakeDed3 = 0.0;
                }
                else
                {
                    taskControl.earthquakeDed3 = double.Parse(txtEarthquakeDed3.Text.ToString().Replace("$", ""));
                }

                if ((txtEarthquakeDed4.Text.ToString().Trim() == ""))
                {
                    taskControl.earthquakeDed4 = 0.0;
                }
                else
                {
                    taskControl.earthquakeDed4 = double.Parse(txtEarthquakeDed4.Text.ToString().Replace("$", ""));
                }


                if ((cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR")) || cp.IsInRole("AUTO VI AGENCY"))
                {
                    if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
                    {
                        taskControl.Agent = ddlAgency.SelectedItem.Value;
                        taskControl.AgentDesc = ddlAgency.SelectedItem.Text.Trim();
                    }
                    else
                        taskControl.Agent = "000";
                }
                else if (cp.IsInRole("AUTO VI AGENCY"))
                {
                    System.Data.DataTable dtAgent = null;

                    System.Data.DataTable DtAgentVI = null;

                    DtAgentVI = Login.Login.GetGroupAgentVIByUserID(cp.UserID);

                    if (DtAgentVI.Rows.Count > 0)
                    {
                        ddlAgency.DataSource = DtAgentVI;
                        ddlAgency.DataTextField = "AgentDesc";
                        ddlAgency.DataValueField = "AgentID";
                        ddlAgency.DataBind();
                        ddlAgency.SelectedIndex = -1;
                        ddlAgency.Items.Insert(0, "");

                        if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
                        {
                            ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                                ddlAgency.Items.FindByValue(taskControl.Agent.Trim()));
                        }
                        else
                        {
                            ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                                ddlAgency.Items.FindByValue("000"));
                        }
                    }
                    else
                    {
                        //Agent
                        if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
                            taskControl.Agent = ddlAgency.SelectedItem.Value;
                        else
                            taskControl.Agent = "000";
                    }
                }
                else
                {

                    System.Data.DataTable dtAgentByUserID = GetAgentByUserID(cp.UserID.ToString());
                    string Agent = "000";

                    if (dtAgentByUserID.Rows.Count > 0)
                    {
                        Agent = dtAgentByUserID.Rows[0]["Agent"].ToString();

                        taskControl.Agent = Agent.Trim();
                    }
                    else
                    {
                        //Agent
                        if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
                        {
                            taskControl.Agent = ddlAgency.SelectedItem.Value;
                            taskControl.AgentDesc = ddlAgency.SelectedItem.Text.Trim();
                        }
                        else
                            taskControl.Agent = "000";
                    }
                }

                if (ddlOffice.SelectedIndex > 0)
                {
                    taskControl.OriginatedAt = int.Parse(ddlOffice.SelectedItem.Value.ToString());
                    taskControl.office = ddlOffice.SelectedItem.Text.Trim();
                }
                else
                {
                    taskControl.office = "";
                }

                taskControl.windstormDedPer1 = txtWindstormDedPer1.Text.ToString().Trim();
                taskControl.windstormDedPer2 = txtWindstormDedPer2.Text.ToString().Trim();
                taskControl.windstormDedPer3 = txtWindstormDedPer3.Text.ToString().Trim();
                taskControl.windstormDedPer4 = txtWindstormDedPer4.Text.ToString().Trim();

                //if((txtEarthquakeDed1.Text.ToString().Trim() == ""))
                //{
                //    taskControl.earthquakeDed1 = 0.0;
                //}
                //else
                //{
                //    taskControl.earthquakeDed1 = double.Parse(txtEarthquakeDed1.Text.ToString().Replace("$", ""));
                //}

                //if ((txtEarthquakeDed2.Text.ToString().Trim() == ""))
                //{
                //    taskControl.earthquakeDed2 = 0.0;
                //}
                //else
                //{
                //    taskControl.earthquakeDed2 = double.Parse(txtEarthquakeDed2.Text.ToString().Replace("$", ""));
                //}

                //if ((txtEarthquakeDed3.Text.ToString().Trim() == ""))
                //{
                //    taskControl.earthquakeDed3 = 0.0;
                //}
                //else
                //{
                //    taskControl.earthquakeDed3 = double.Parse(txtEarthquakeDed3.Text.ToString().Replace("$", ""));
                //}

                //if ((txtEarthquakeDed4.Text.ToString().Trim() == ""))
                //{
                //    taskControl.earthquakeDed4 = 0.0;
                //}
                //else
                //{
                //    taskControl.earthquakeDed4 = double.Parse(txtEarthquakeDed4.Text.ToString().Replace("$", ""));
                //}

                taskControl.earthquakeDedper1 = txtEarthQuakeDedPer1.Text.ToString().Trim();
                taskControl.earthquakeDedper2 = txtEarthQuakeDedPer2.Text.ToString().Trim();
                taskControl.earthquakeDedper3 = txtEarthQuakeDedPer3.Text.ToString().Trim();
                taskControl.earthquakeDedper4 = txtEarthQuakeDedPer4.Text.ToString().Trim();

                taskControl.coinsurance1 = txtCoinsurance1.Text.ToString().Trim();
                taskControl.coinsurance2 = txtCoinsurance2.Text.ToString().Trim();
                taskControl.coinsurance3 = txtCoinsurance3.Text.ToString().Trim();
                taskControl.coinsurance4 = txtCoinsurance4.Text.ToString().Trim();

                taskControl.premium1 = double.Parse(txtPremium1.Text.ToString().Replace("$", ""));
                taskControl.premium2 = double.Parse(txtPremium2.Text.ToString().Replace("$", ""));
                taskControl.premium3 = double.Parse(txtPremium3.Text.ToString().Replace("$", ""));
                taskControl.premium4 = double.Parse(txtPremium4.Text.ToString().Replace("$", ""));

                taskControl.totalLimit = double.Parse(txtTotalLimit.Text.ToString().Replace("$", ""));

                if (txtTotalWind.Text.ToString() == "NO COVERAGE")
                {
                    taskControl.totalWindstorm = 0.0;
                }
                else
                {
                    taskControl.totalWindstorm = double.Parse(txtTotalWind.Text.ToString().Replace("$", ""));
                }

                if (txtTotalEarth.Text.ToString() == "NO COVERAGE")
                {
                    taskControl.totalEarthquake = 0.0;
                }
                else
                {
                    taskControl.totalEarthquake = double.Parse(txtTotalEarth.Text.ToString().Replace("$", ""));
                }


                //taskControl.totalPremium = double.Parse(txtTotalPremium.Text.ToString().Replace("$", ""));

                taskControl.liaPropertyType = txtLiaPropertyType.Text.ToString().Trim();
                taskControl.liaPolicyType = txtLiaPolicyType.Text.ToString().Trim();
                taskControl.liaNumOfFamilies = txtLiaNumOfFamilies.Text.ToString().Trim();

                if (ddlLiaLimit.SelectedItem.Value.ToLower().Contains('$'))
                {
                    taskControl.liaLimit = double.Parse(ddlLiaLimit.SelectedItem.Value.Replace("$", ""));
                }
                else
                {
                    taskControl.liaLimit = 0;
                }

                if (txtLiaMedicalPayments.Text.ToLower().Contains('$'))
                {
                    taskControl.liaMedicalPayments = double.Parse(txtLiaMedicalPayments.Text.ToString().Replace("$", ""));
                }
                else
                {
                    taskControl.liaMedicalPayments = 0;
                }

                if (txtPremium.Text.ToString().Replace("$", "") != "NO COVERAGE")
                {
                    taskControl.premium = double.Parse(txtPremium.Text.ToString().Replace("$", ""));
                }
                else
                {
                    taskControl.premium = 0;
                }

                if (txtTotalPremium.Text.ToString().Replace("$", "") != "NO COVERAGE")
                {
                    taskControl.totalPremium = double.Parse(txtTotalPremium.Text.ToString().Replace("$", ""));
                }
                else
                {
                    taskControl.totalPremium = 0;
                }

                if (txtLiaPremium.Text.ToLower().Contains('$'))
                {
                    taskControl.liaPremium = double.Parse(txtLiaPremium.Text.ToString().Replace("$", ""));
                }
                else
                {
                    taskControl.liaPremium = 0;
                }

                double GrossTax = 0;

                if (txtGrossTax.Text.ToLower().Contains('$'))
                {
                    taskControl.GrossTax = double.Parse(txtGrossTax.Text.Trim(), System.Globalization.NumberStyles.Currency);
                    GrossTax = double.Parse(txtGrossTax.Text.Trim(), System.Globalization.NumberStyles.Currency);
                }
                else
                {
                    taskControl.GrossTax = 0;
                }

                if (txtLiaTotalPremium.Text.ToLower().Contains('$'))
                {
                    taskControl.liaTotalPremium = double.Parse(txtLiaTotalPremium.Text.ToString().Replace("$", "").Replace(",", "").Replace(" ", "")) - GrossTax;
                }
                else
                {
                    taskControl.liaTotalPremium = 0;
                }

                taskControl.comment = txtComment.Text;

                taskControl.Island = ddlIsland.SelectedItem.Text.Trim();

                taskControl.DiscountsHomeOwners = double.Parse(ddlDiscount.SelectedItem.Text.Trim()) / 100;

                taskControl.TypeOfInsuredID = int.Parse(ddlTypeOfInsured.SelectedItem.Value);

                

                //taskControl.GrossTax = double.Parse(txtGrossTax.Text.ToString(), System.Globalization.NumberStyles.Currency);

                if (Session["TaskControlIDOLD"] != null)
                {
                    if (Session["TaskControlIDOLD"].ToString() != "")
                        taskControl.TCIDQuotes = int.Parse(Session["TaskControlIDOLD"].ToString());
                }

            }
            catch (Exception exp)
            {
                throw new Exception("Could not fill properties.", exp);
            }
        }

        protected void FillTextControl()
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                LblControlNo.Text = taskControl.TaskControlID.ToString().Trim();

                lbCustomerNo.Text = "Customer #:" + taskControl.CustomerNo.ToString().Trim();

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                Login.Login login = new Login.Login();

                if (taskControl.isRenew == false)
                {
                    if (taskControl.isQuote == true && taskControl.approved==false && taskControl.submitted==false)
                    {
                        txtConfirmEmailAddress.Visible = true;
                        LabelConfirmEmailAddress.Visible = true;
                    }
                }

                //ddl.SelectedIndex = ddlOffice.Items.IndexOf(
                // ddlOffice.Items.FindByValue(taskControl.OriginatedAt.ToString()));
                txtFirstName.Text = taskControl.Customer.FirstName;
                txtLastName.Text = taskControl.Customer.LastName1;
                txtMailingAddress.Text = taskControl.Customer.Address1;
                txtMailingAddress2.Text = taskControl.Customer.Address2;
                txtCity.Text = taskControl.Customer.City;
                txtState.Text = taskControl.Customer.State;
                txtZipCode.Text = taskControl.Customer.ZipCode;
                txtPhysicalAddress1.Text = taskControl.Customer.AddressPhysical1;
                txtPhysicalAddress2.Text = taskControl.Customer.AddressPhysical2;
                txtCity2.Text = taskControl.Customer.CityPhysical;
                txtState2.Text = taskControl.Customer.StatePhysical;
                txtZipCode2.Text = taskControl.Customer.ZipPhysical;
                txtMobilePhone.Text = taskControl.Customer.HomePhone;
                txtBusinessPhone.Text = taskControl.Customer.JobPhone;
                taskControl.Customer.LocationID = taskControl.Customer.LocationID;//STT ONLY
                txtOccupation.Text = taskControl.occupation;
                txtEmail.Text = taskControl.Customer.Email;

                txtInspector.Text = taskControl.Inspector;
                txtInspectionDate.Text = taskControl.InspectionDate;

                ddlTypeOfInsured.SelectedIndex = ddlTypeOfInsured.Items.IndexOf(ddlTypeOfInsured.Items.FindByText(taskControl.TypeOfInsuredID.ToString()));

               

                if (taskControl.CustomerNo != "")
                {
                    System.Data.DataTable dt = null;

                    dt = GetCustomerCommentsByCustomerNo(int.Parse(taskControl.CustomerNo));

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["Comments"].ToString().Contains(taskControl.TaskControlID.ToString()) && dt.Rows[i]["Comments"].ToString().Contains(" - COMMENT: "))
                            {
                                txtAlertComment.Text = ((dt.Rows[i]["Comments"].ToString().Substring(dt.Rows[i]["Comments"].ToString().LastIndexOf(":"))).Replace(":","")).ToUpper();
                            }
                        }

                    }
                }
                if (taskControl.SubmittedDate != "")
                {
                    txtAlertEntryDate.Text = taskControl.SubmittedDate;
                }
                else
                {
                    txtAlertEntryDate.Text = DateTime.Now.ToShortDateString();
                }

                


                ddlTypeOfInterest.SelectedIndex = ddlTypeOfInterest.Items.IndexOf(
                       ddlTypeOfInterest.Items.FindByValue(taskControl.typeOfInterest));
                
                

                if (taskControl.Customer.Description.ToString().Trim() != "")
                {
                    ClientIDPPS = taskControl.Customer.Description.ToString().Trim();
                    //taskControl.CustomerNo = CustomerNumberPPS;
                }

                txtBank.Text = taskControl.bank;
                txtLoanNo.Text = taskControl.loanNo;
                if (taskControl.bank != "" || taskControl.bank2 != "") //&& taskControl.loanNo != "")
                {
                    CheckBank.Checked = true;

                    if (taskControl.bank != "")
                    {
                        ddlBankList.SelectedIndex = ddlBankList.Items.IndexOf(ddlBankList.Items.FindByText(taskControl.bank));
                        ddlBankList_SelectedIndexChanged(String.Empty, EventArgs.Empty);
                    }
                    if (taskControl.bank2 != "")
                    {
                        ddlBankList2.SelectedIndex = ddlBankList2.Items.IndexOf(ddlBankList2.Items.FindByText(taskControl.bank2));
                        ddlBankList2_SelectedIndexChanged(String.Empty, EventArgs.Empty);
                    }
                    chkBank_CheckedChange(String.Empty, EventArgs.Empty);
                }
                else
                {
                    CheckBank.Checked = true;//false;
                    chkBank_CheckedChange(String.Empty, EventArgs.Empty);
                }
                txtBank2.Text = taskControl.bank2;
                txtLoanNo2.Text = taskControl.loanNo2;
                txtComments.Text = taskControl.comments;

                txtIssueDate.Text = taskControl.EntryDate.ToString("MM/dd/yyyy");

                ddlTypeOfInsured.SelectedIndex = ddlTypeOfInsured.Items.IndexOf(
                       ddlTypeOfInsured.Items.FindByValue(taskControl.TypeOfInsuredID.ToString()));

              

               

                if (taskControl.mortgageeBilled)
                {
                    ddlMortgageeBilled.SelectedIndex = 1;
                }
                else
                {
                    ddlMortgageeBilled.SelectedIndex = 0;
                }

                if (taskControl.renewalNo.ToString() != "")
                {
                    txtSuffix.Text = DateTime.Parse(taskControl.EffectiveDate).Year.ToString().Substring(2);
                }
                else
                {
                    txtSuffix.Text = taskControl.Suffix.ToString().Trim();
                }


                if ((cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR")) || cp.IsInRole("AUTO VI AGENCY"))
                {
                    if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
                    {
                        ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                            ddlAgency.Items.FindByValue(taskControl.Agent.Trim()));
                    }
                    else
                    {
                        ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                            ddlAgency.Items.FindByValue("000"));
                    }
                }
                else
                {

                    System.Data.DataTable dtAgentByUserID = GetAgentByUserID(cp.UserID.ToString());
                    string Agent = "000";

                    if (dtAgentByUserID.Rows.Count > 0)
                    {
                        Agent = dtAgentByUserID.Rows[0]["Agent"].ToString();

                        taskControl.Agent = Agent.Trim();

                        ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                            ddlAgency.Items.FindByValue(taskControl.Agent.Trim()));

                        taskControl.AgentDesc = ddlAgency.SelectedItem.Text.Trim();
                    }
                    else
                    {
                        //Agent
                        if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
                        {
                            taskControl.Agent = ddlAgency.SelectedItem.Value;
                            taskControl.AgentDesc = ddlAgency.SelectedItem.Text.Trim();
                        }
                        else
                            taskControl.Agent = "000";
                    }
                }

                //if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
                //{
                //    ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                //        ddlAgency.Items.FindByValue(taskControl.Agent.Trim()));
                //}
                //else
                //{
                //    ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                //        ddlAgency.Items.FindByValue("000"));
                //}

                taskControl.OriginatedAt = EPolicy.Login.Login.GetLocationByUserID(userID);

                if (taskControl.office.Trim() != "" || taskControl.OriginatedAt != 0)
                {
                    ddlOffice.SelectedIndex = ddlOffice.Items.IndexOf(
                        ddlOffice.Items.FindByValue(taskControl.OriginatedAt.ToString()));
                }
                else
                {
                    ddlOffice.SelectedIndex = 0;
                }

                if (!taskControl.isQuote)
                {
                    lblIssueDate.Text = "Issue Date";
                    txtEffectiveDate.Text = taskControl.EffectiveDate;
                    txtExpirationDate.Text = taskControl.ExpirationDate;
                }
                else if (taskControl.isQuote && !taskControl.isRenew)
                {
                    lblIssueDate.Text = "Quote Date";
                    txtEffectiveDate.Text = "Not available for quote";
                    txtExpirationDate.Text = "Not available for quote";
                }
                else if (taskControl.isQuote && taskControl.isRenew)
                {
                    System.Data.DataTable PolicyDt = new System.Data.DataTable();
                    if (taskControl.PreviousPolicy != "")
                    {
                        //PolicyDt = TaskControl.Policy.GetPolicyByPolicyNo(taskControl.PreviousPolicy.Substring(0, 3), taskControl.renewalNo, "", taskControl.PreviousPolicy.Substring(taskControl.PreviousPolicy.Length - 2, 2));
                        PolicyDt = GetPolicyQuoteByTaskControlID(taskControl.TaskControlID);
                        lblIssueDate.Text = "Quote Date";
                        if (PolicyDt.Rows.Count > 0)
                        {
                            //TaskControl.Policy PreviousPolicy = new TaskControl.Policy();
                            //PreviousPolicy = TaskControl.Policy.GetPolicyQuoteByTaskControlID(int.Parse(PolicyDt.Rows[0]["TaskcontroID"].ToString()), PreviousPolicy);
                            if (txtEffectiveDate.Text.Trim() == "")
                            {
                                txtEffectiveDate.Text = DateTime.Parse(PolicyDt.Rows[0]["EffectiveDate"].ToString()).ToShortDateString(); ;
                            }
                            if (txtExpirationDate.Text.Trim() == "")
                            {
                                txtExpirationDate.Text = DateTime.Parse(PolicyDt.Rows[0]["ExpirationDate"].ToString()).ToShortDateString(); ;
                            }
                            //if (DateTime.Parse(DateTime.Now.ToShortDateString()) <= DateTime.Parse(PolicyDt.Rows[0]["Expire"].ToString())) //ExpirationDate
                            //{
                            //    if (txtEffectiveDate.Text.Trim() == "")
                            //    {txtEffectiveDate.Text = DateTime.Parse(PolicyDt.Rows[0]["Expire"].ToString()).ToShortDateString();}
                            //    if (txtExpirationDate.Text.Trim() == "")
                            //    {txtExpirationDate.Text = DateTime.Parse(PolicyDt.Rows[0]["Expire"].ToString()).AddYears(1).ToShortDateString();}
                            //}
                            //else
                            //{
                            //    if (txtEffectiveDate.Text.Trim() == "")
                            //    {txtEffectiveDate.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString();}
                            //    if (txtExpirationDate.Text.Trim() == "")
                            //    {txtExpirationDate.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).AddYears(1).ToShortDateString();}
                            //}
                        }
                    }
                   
                }

                txtCatastropheCoverage.Text = taskControl.catastropheCov;

                ddlCatastropheDeduc.SelectedIndex = ddlCatastropheDeduc.Items.IndexOf(
                     ddlCatastropheDeduc.Items.FindByValue(taskControl.catastropheDeduc));

                txtWindstormDeductible.Text = taskControl.windstormDeduc;
                txtAllOtherPerilsDeductible.Text = taskControl.allOtherPerDeduc;

                ddlConstructionType.SelectedIndex = ddlConstructionType.Items.IndexOf(
                    ddlConstructionType.Items.FindByValue(taskControl.constructionType));

                txtConstructionYear.Text = taskControl.constructionYear;

                ddlNumberOfStories.SelectedIndex = ddlNumberOfStories.Items.IndexOf(
                   ddlNumberOfStories.Items.FindByValue(taskControl.numOfStories));

                ddlNumOfFamilies.SelectedIndex = ddlNumOfFamilies.Items.IndexOf(
                   ddlNumOfFamilies.Items.FindByValue(taskControl.numOfFamilies));

                txtIfYes.Text = taskControl.ifYes;

                if (taskControl.isUpgraded)
                {
                    rdbYes.Checked = true;
                    lblIfYes.ForeColor = Color.Red;
                }
                else
                {
                    rdbNo.Checked = true;
                    lblIfYes.ForeColor = Color.Gray;
                }

                if (taskControl.additionalStructure)
                {
                    rdbYesStruct.Checked = true;
                    lblStructures.ForeColor = Color.Red;
                }
                else
                {
                    rdbNoStruct.Checked = true;
                    lblStructures.ForeColor = Color.Gray;
                }

                txtLivingArea.Text = taskControl.livingArea;
                txtPorches.Text = taskControl.porshcesDeck;

                ddlRoofDwelling.SelectedIndex = ddlRoofDwelling.Items.IndexOf(
                   ddlRoofDwelling.Items.FindByValue(taskControl.roofDwelling));

                txtEarthQuakeDeductible.Text = taskControl.earthquakeDeduc;

                ddlResidence.SelectedIndex = ddlResidence.Items.IndexOf(
                   ddlResidence.Items.FindByValue(taskControl.residence));

                ddlPropertyType.SelectedIndex = ddlPropertyType.Items.IndexOf(
                   ddlPropertyType.Items.FindByValue(taskControl.propertyType));


                txtPropertyForm.Text = taskControl.propertyForm;

                txtPolicyType.Text = taskControl.PolicyType;

                if (taskControl.losses3Year)
                {
                    ddlLosses3Years.SelectedIndex = 1;
                }
                else
                {
                    ddlLosses3Years.SelectedIndex = 0;
                }

                txtOtherStruct.Text = taskControl.otherStructuresType;

                if (taskControl.isPropShuttered)
                {
                    ddlPropertyShuttered.SelectedIndex = 1;
                }
                else
                {
                    ddlPropertyShuttered.SelectedIndex = 0;
                }

                ddlRoofOverhang.SelectedIndex = ddlRoofOverhang.Items.IndexOf(
                   ddlRoofOverhang.Items.FindByValue(taskControl.roofOverhang));

                if (taskControl.autoPolicy)
                {
                    ddlAutoPolicyWitGuardian.SelectedIndex = 1;
                }
                else
                {
                    ddlAutoPolicyWitGuardian.SelectedIndex = 0;
                }

                txtAutoPolicyNo.Text = taskControl.PolicyNo;


                if (taskControl.limit1.ToString() != "")
                {
                    if (taskControl.limit1.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.limit1.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtLimit1.Text = var.ToString("$##,###,###.00");
                        else
                            txtLimit1.Text = "$0.00";
                    }
                    else
                    {
                        txtLimit1.Text = "$0.00";
                    }

                }
                else
                {
                    txtLimit1.Text = "$0.00";
                }

                if (taskControl.limit2.ToString() != "")
                {
                    if (taskControl.limit2.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.limit2.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtLimit2.Text = var.ToString("$##,###,###.00");
                        else
                            txtLimit2.Text = "$0.00";
                    }
                    else
                    {
                        txtLimit2.Text = "$0.00";
                    }

                }
                else
                {
                    txtLimit2.Text = "$0.00";
                }



                if (taskControl.limit3.ToString() != "")
                {
                    if (taskControl.limit3.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.limit3.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtLimit3.Text = var.ToString("$##,###,###.00");
                        else
                        {
                            txtLimit3.Text = "$0.00";
                        }
                    }
                    else
                    {
                        txtLimit3.Text = "$0.00";
                    }

                }
                else
                {
                    txtLimit3.Text = "$0.00";
                }


                if (taskControl.limit4.ToString() != "")
                {

                    if (taskControl.limit4.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.limit4.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtLimit4.Text = var.ToString("$##,###,###.00");
                        else
                            txtLimit4.Text = "$0.00";

                    }
                    else
                    {
                        txtLimit4.Text = "$0.00";
                    }

                }
                else
                {
                    txtLimit4.Text = "$0.00";
                }

                if (taskControl.aopDed1.ToString() != "")
                {
                    if (taskControl.aopDed1.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.aopDed1.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtAOPDed1.Text = var.ToString("$##,###,###.00");
                        else
                            txtAOPDed1.Text = "$0.00";
                    }
                    else
                    {
                        txtAOPDed1.Text = "$0.00";
                    }

                }
                else
                {
                    txtAOPDed1.Text = "$0.00";
                }

                if (taskControl.aopDed2.ToString() != "")
                {
                    if (taskControl.aopDed2.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.aopDed2.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtAOPDed2.Text = var.ToString("$##,###,###.00");
                        else
                            txtAOPDed2.Text = "$0.00";
                    }
                    else
                    {
                        txtAOPDed2.Text = "$0.00";
                    }

                }
                else
                {
                    txtAOPDed2.Text = "$0.00";
                }


                if (taskControl.aopDed3.ToString() != "")
                {
                    if (taskControl.aopDed3.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.aopDed3.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtAOPDed3.Text = var.ToString("$##,###,###.00");
                        else
                            txtAOPDed3.Text = "$0.00";
                    }
                    else
                    {
                        txtAOPDed3.Text = "$0.00";
                    }

                }
                else
                {
                    txtAOPDed3.Text = "$0.00";
                }

                if (taskControl.aopDed4.ToString() != "")
                {
                    if (taskControl.aopDed4.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.aopDed4.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtAOPDed4.Text = var.ToString("$##,###,###.00");
                        else
                            txtAOPDed4.Text = "$0.00";
                    }
                    else
                    {
                        txtAOPDed4.Text = "$0.00";
                    }


                }
                else
                {
                    txtAOPDed4.Text = "$0.00";
                }

                if (taskControl.windstormDed1.ToString() != "")
                {
                    if (taskControl.windstormDed1.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.windstormDed1.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtWindstormDed1.Text = var.ToString("$##,###,###.00");
                        else
                            txtWindstormDed1.Text = "$0.00";
                    }
                    else
                    {
                        txtWindstormDed1.Text = "$0.00";
                    }


                }
                else
                {
                    txtWindstormDed1.Text = "$0.00";
                }

                if (taskControl.windstormDed2.ToString() != "")
                {
                    if (taskControl.windstormDed2.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.windstormDed2.ToString().Replace("$", "").Replace(",", ""));
                        if (var > 0)
                            txtWindstormDed1.Text = var.ToString("$##,###,###.00");
                        else
                            txtWindstormDed2.Text = "$0.00";
                    }
                    else
                    {
                        txtWindstormDed2.Text = "$0.00";
                    }

                }
                else
                {
                    txtWindstormDed2.Text = "$0.00";
                }

                if (taskControl.windstormDed3.ToString() != "")
                {
                    if (taskControl.windstormDed3.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.windstormDed3.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtWindstormDed3.Text = var.ToString("$##,###,###.00");
                        else
                            txtWindstormDed3.Text = "$0.00";
                    }
                    else
                    {
                        txtWindstormDed3.Text = "$0.00";
                    }

                }
                else
                {
                    txtWindstormDed3.Text = "$0.00";
                }

                if (taskControl.windstormDed4.ToString() != "")
                {
                    if (taskControl.windstormDed4.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.windstormDed4.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtWindstormDed4.Text = var.ToString("$##,###,###.00");
                        else
                            txtWindstormDed4.Text = "$0.00";
                    }
                    else
                    {
                        txtWindstormDed4.Text = "$0.00";
                    }

                }
                else
                {
                    txtWindstormDed4.Text = "$0.00";
                }

                if (taskControl.earthquakeDed1.ToString() != "")
                {
                    if (taskControl.earthquakeDed1.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.earthquakeDed1.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtEarthquakeDed1.Text = var.ToString("$##,###,###.00");
                        else
                            txtEarthquakeDed1.Text = "$0.00";
                    }
                    else
                    {
                        txtEarthquakeDed1.Text = "$0.00";
                    }

                }
                else
                {
                    txtEarthquakeDed1.Text = "$0.00";
                }

                if (taskControl.earthquakeDed2.ToString() != "")
                {
                    if (taskControl.earthquakeDed2.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.earthquakeDed2.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtEarthquakeDed2.Text = var.ToString("$##,###,###.00");
                        else
                            txtEarthquakeDed2.Text = "$0.00";
                    }
                    else
                    {
                        txtEarthquakeDed2.Text = "$0.00";
                    }


                }
                else
                {
                    txtEarthquakeDed2.Text = "$0.00";
                }

                if (taskControl.earthquakeDed3.ToString() != "")
                {
                    if (taskControl.earthquakeDed3.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.earthquakeDed3.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtEarthquakeDed3.Text = var.ToString("$##,###,###.00");
                        else
                            txtEarthquakeDed3.Text = "$0.00";
                    }
                    else
                    {
                        txtEarthquakeDed3.Text = "$0.00";
                    }

                }
                else
                {
                    txtEarthquakeDed3.Text = "$0.00";
                }

                if (taskControl.earthquakeDed4.ToString() != "")
                {
                    if (taskControl.earthquakeDed4.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.earthquakeDed4.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtEarthquakeDed4.Text = var.ToString("$##,###,###.00");
                        else
                            txtEarthquakeDed4.Text = "$0.00";
                    }
                    else
                    {
                        txtEarthquakeDed4.Text = "$0.00";
                    }

                }
                else
                {
                    txtEarthquakeDed4.Text = "$0.00";
                }

                if (taskControl.premium1.ToString() != "")
                {
                    if (taskControl.premium1.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.premium1.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtPremium1.Text = var.ToString("$##,###,###.00");
                        else
                            txtPremium1.Text = "$0.00";
                    }
                    else
                    {
                        txtPremium1.Text = "$0.00";
                    }

                }
                else
                {
                    txtPremium1.Text = "$0.00";
                }

                if (taskControl.premium2.ToString() != "")
                {
                    if (taskControl.premium2.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.premium2.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtPremium2.Text = var.ToString("$##,###,###.00");
                        else
                            txtPremium2.Text = "$0.00";
                    }
                    else
                    {
                        txtPremium2.Text = "$0.00";
                    }

                }
                else
                {
                    txtPremium2.Text = "$0.00";
                }

                if (taskControl.premium3.ToString() != "")
                {
                    if (taskControl.premium3.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.premium3.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtPremium3.Text = var.ToString("$##,###,###.00");
                        else
                            txtPremium3.Text = "$0.00";
                    }
                    else
                    {
                        txtPremium3.Text = "$0.00";
                    }

                }
                else
                {
                    txtPremium3.Text = "$0.00";
                }


                if (taskControl.premium4.ToString() != "")
                {
                    if (taskControl.premium4.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.premium4.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtPremium4.Text = var.ToString("$##,###,###.00");
                        else
                            txtPremium4.Text = "$0.00";
                    }
                    else
                    {
                        txtPremium4.Text = "$0.00";
                    }

                }
                else
                {
                    txtPremium4.Text = "$0.00";
                }

                if (taskControl.totalLimit.ToString() != "")
                {
                    if (taskControl.totalLimit.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.totalLimit.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtTotalLimit.Text = var.ToString("$##,###,###.00");
                        else
                            txtTotalLimit.Text = "$0.00";
                    }
                    else
                    {
                        txtTotalLimit.Text = "$0.00";
                    }

                }
                else
                {
                    txtTotalLimit.Text = "$0.00";
                }

                if (taskControl.totalWindstorm.ToString() != "")
                {
                    if (taskControl.totalWindstorm.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.totalWindstorm.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtTotalWind.Text = var.ToString("$##,###,###.00");
                        else
                            txtTotalLimit.Text = "$0.00";
                    }
                    else
                    {
                        txtTotalWind.Text = "$0.00";
                    }

                }
                else
                {
                    txtTotalWind.Text = "$0.00";
                }

                if (taskControl.totalEarthquake.ToString() != "")
                {
                    if (taskControl.totalEarthquake.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.totalEarthquake.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtTotalEarth.Text = var.ToString("$##,###,###.00");
                        else
                            txtTotalEarth.Text = "$0.00";
                    }
                    else
                    {
                        txtTotalEarth.Text = "$0.00";
                    }

                }
                else
                {
                    txtTotalEarth.Text = "$0.00";
                }

                if (taskControl.totalPremium.ToString() != "")
                {
                    if (taskControl.totalPremium.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.totalPremium.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtTotalPremium.Text = var.ToString("$##,###,###.00");
                        else
                            txtTotalPremium.Text = "$0.00";
                    }
                    else
                    {
                        txtTotalPremium.Text = "$0.00";
                    }

                }
                else
                {
                    txtTotalPremium.Text = "$0.00";
                }



                if (taskControl.liaPremium.ToString() != "")
                {
                    if (taskControl.liaPremium.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.liaPremium.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtLiaPremium.Text = var.ToString("$##,###,###.00");
                        else
                            txtLiaPremium.Text = "$0.00";
                    }
                    else
                    {
                        txtLiaPremium.Text = "$0.00";
                    }

                }
                else
                {
                    txtLiaPremium.Text = "$0.00";
                }

                if (taskControl.premium.ToString() != "")
                {
                    if (taskControl.premium.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.premium.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtPremium.Text = var.ToString("$##,###,###.00");
                        else
                            txtPremium.Text = "$0.00";
                    }
                    else
                    {
                        txtPremium.Text = "$0.00";
                    }

                }
                else
                {
                    txtPremium.Text = "$0.00";
                }

                if (taskControl.liaTotalPremium.ToString() != "")
                {
                    if (taskControl.liaTotalPremium.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.liaTotalPremium.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                            txtLiaTotalPremium.Text = var.ToString("$##,###,###.00");
                        else
                            txtLiaTotalPremium.Text = "$0.00";
                    }
                    else
                    {
                        txtLiaTotalPremium.Text = "$0.00";
                    }
                }
                else
                {
                    txtLiaTotalPremium.Text = "$0.00";
                }

                txtWindstormDedPer1.Text = taskControl.windstormDedPer1.ToString().Trim();
                txtWindstormDedPer2.Text = taskControl.windstormDedPer2.ToString().Trim();
                txtWindstormDedPer3.Text = taskControl.windstormDedPer3.ToString().Trim();
                txtWindstormDedPer4.Text = taskControl.windstormDedPer4.ToString().Trim();

                txtEarthQuakeDedPer1.Text = taskControl.earthquakeDedper1.ToString().Trim();
                txtEarthQuakeDedPer2.Text = taskControl.earthquakeDedper2.ToString().Trim();
                txtEarthQuakeDedPer3.Text = taskControl.earthquakeDedper3.ToString().Trim();
                txtEarthQuakeDedPer4.Text = taskControl.earthquakeDedper4.ToString().Trim();

                txtCoinsurance1.Text = taskControl.coinsurance1.ToString().Trim();
                txtCoinsurance2.Text = taskControl.coinsurance2.ToString().Trim();
                txtCoinsurance3.Text = taskControl.coinsurance3.ToString().Trim();
                txtCoinsurance4.Text = taskControl.coinsurance4.ToString().Trim();

                txtLiaPropertyType.Text = taskControl.propertyType.ToString().Trim();
                txtLiaPolicyType.Text = taskControl.PolicyType.ToString().Trim();
                txtLiaNumOfFamilies.Text = taskControl.liaNumOfFamilies.ToString().Trim();

                if (taskControl.isRenew == true)
                {
                    if (taskControl.PreviousPolicy.Trim() != "")
                    {
                        DivPreviousPolicy1.Visible = true;
                        DivPreviousPolicy2.Visible = true;
                        DivPreviousPolicy3.Visible = true;
                        String[] arrayPolicyNoRenewal;
                        if (taskControl.PreviousPolicy.Trim().Contains("HOM"))
                        {
                            arrayPolicyNoRenewal = taskControl.PreviousPolicy.Trim().Replace("HOM", "").Split('-');
                            txtPreviousPolicyType.Text = "HOM";
                        }
                        else
                        {
                            arrayPolicyNoRenewal = taskControl.PreviousPolicy.Trim().Replace("INC", "").Split('-');
                            txtPreviousPolicyType.Text = "INC";
                        }
                        txtPreviousPolicyNo.Text = arrayPolicyNoRenewal[0];
                        txtPreviousPolicySuffix.Text = arrayPolicyNoRenewal[1];

                        txtPolicyToRenewType.Text = txtPreviousPolicyType.Text.Trim();
                        txtPolicyNoToRenew.Text = arrayPolicyNoRenewal[0];
                        txtPolicyNoToRenewSuffix.Text = arrayPolicyNoRenewal[1];
                            
                    }
                }
                else
                {
                    DivPreviousPolicy1.Visible = false;
                    DivPreviousPolicy2.Visible = false;
                    DivPreviousPolicy3.Visible = false;
                }

                //if (taskControl.PolicyType.ToString().Trim() != "INC")
                //{

                //    ddlLiaLimit.SelectedIndex = ddlLiaLimit.Items.IndexOf(
                //       ddlLiaLimit.Items.FindByValue("$" + string.Format("{0:n0}", taskControl.liaLimit)));

                //    txtLiaMedicalPayments.Text = taskControl.liaMedicalPayments.ToString().Trim();
                //}
                //else
                //{
                //    ddlLiaLimit.Items[0].Text = "NO COVERAGE";
                //    ddlLiaLimit.SelectedIndex = 0;
                //    txtLiaMedicalPayments.Text = "NO COVERAGE";
                //    txtLiaPremium.Text = "$0.00";
                //}

                if (taskControl.TaskControlID != 0)
                {
                    if (taskControl.liaLimit != 0)
                    {

                        ddlLiaLimit.SelectedIndex = ddlLiaLimit.Items.IndexOf(
                           ddlLiaLimit.Items.FindByValue("$" + string.Format("{0:n0}", taskControl.liaLimit)));

                        txtLiaMedicalPayments.Text = taskControl.liaMedicalPayments.ToString().Trim();
                    }
                    else
                    {
                        ddlLiaLimit.Items[0].Text = "NO COVERAGE";
                        ddlLiaLimit.SelectedIndex = 0;
                        txtLiaMedicalPayments.Text = "NO COVERAGE";
                        txtLiaPremium.Text = "$0.00";
                    //}

                   // if (txtPolicyType.Text.Trim() == "INC")
                   // {
                   //     ddlLiaLimit.Items[0].Text = "NO COVERAGE";
                    //    ddlLiaLimit.SelectedIndex = 0;
                   //     txtLiaMedicalPayments.Text = "NO COVERAGE";
                    //    txtliaPremium.Text = "$0.00";
                   //     ddlLiaLimit.Enabled = false;
                   // }
                   // else
                   // {
                        ddlLiaLimit.Enabled = true;
                    }
                }
				 else
                {
                    if (taskControl.isQuote != true)
                    {
                        if (taskControl.liaLimit != 0)
                        {

                            ddlLiaLimit.SelectedIndex = ddlLiaLimit.Items.IndexOf(
                               ddlLiaLimit.Items.FindByValue("$" + string.Format("{0:n0}", taskControl.liaLimit)));

                            txtLiaMedicalPayments.Text = taskControl.liaMedicalPayments.ToString().Trim();
                        }
                        else
                        {
                            ddlLiaLimit.Items[0].Text = "NO COVERAGE";
                            ddlLiaLimit.SelectedIndex = 0;
                            txtLiaMedicalPayments.Text = "NO COVERAGE";
                            txtLiaPremium.Text = "$0.00";
                        }
                    }
					else
					{
                        if (taskControl.liaLimit != 0)
                        {
                            ddlLiaLimit.SelectedIndex = ddlLiaLimit.Items.IndexOf(
                             ddlLiaLimit.Items.FindByValue("$" + string.Format("{0:n0}", taskControl.liaLimit)));

                            txtLiaMedicalPayments.Text = taskControl.liaMedicalPayments.ToString().Trim();
                        }
                        else
                        {
                            ddlLiaLimit.SelectedIndex = ddlLiaLimit.Items.IndexOf(
                                                     ddlLiaLimit.Items.FindByValue("$50,000"));
                        }
					}
                
                }

                txtComment.Text = taskControl.comment;

                ddlIsland.SelectedIndex = ddlIsland.Items.IndexOf(ddlIsland.Items.FindByText(taskControl.Island));


                System.Data.DataTable AdditionalInsured =null;
                if (taskControl.TaskControlID != 0)
                {
                    if (taskControl.isQuote)
                    {
                        AdditionalInsured = GetHomeOwnersAdditionalInsuredQuoteByTaskControlID(taskControl.TaskControlID);
                    }
                    else
                    {
                        AdditionalInsured = GetHomeOwnersAdditionalInsuredPolicyByTaskControlID(taskControl.TaskControlID);
                    }
                  

                    GridInsured.DataSource = AdditionalInsured;
                    GridInsured.DataBind();
                    GridInsured.Visible = true;

                }
                else
                {
                    if (Session["AdditionalInsured"] != null)
                    {
                        AdditionalInsured = (System.Data.DataTable)Session["AdditionalInsured"];

                        GridInsured.DataSource = AdditionalInsured;
                        GridInsured.DataBind();
                        GridInsured.Visible = true;
                    }
                
                }
                

                if (taskControl.GrossTax.ToString() != "")
                {
                    if (taskControl.GrossTax.ToString() != "0")
                    {
                        double var = Double.Parse(taskControl.GrossTax.ToString().Replace("$", "").Replace(",", "").Replace(",", ""));
                        if (var > 0)
                        {
                            txtGrossTax.Text = var.ToString("$##,###,###.00");
                            txtLiaTotalPremium.Text = (Double.Parse(txtLiaTotalPremium.Text.ToString().Replace("$", "").Replace(",", "").Replace(",", "")) + var).ToString("$##,###,###.00");
                        }
                        else
                            txtGrossTax.Text = "$0.00";
                    }
                    else
                    {
                        txtGrossTax.Text = "$0.00";
                    }
                }
                else
                {
                    txtGrossTax.Text = "$0.00";
                }

                if (taskControl.DiscountsHomeOwners != null)
                {
                    ddlDiscount.SelectedIndex = ddlDiscount.Items.IndexOf(ddlDiscount.Items.FindByText((Convert.ToInt32((taskControl.DiscountsHomeOwners * 100))).ToString())); 
                }

                FillGridDocuments(false);

            }
            catch (Exception exp)
            {
                throw new Exception("Could not fill Text Controls.", exp);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            try
            {
                if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
                {
                    VerifyPolicyExist();
                }
                ModifyClick();
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message + "";
                mpeSeleccion.Show();
            }
        }


        private List<string> imprimirQuote(List<string> mergePaths, int counter, string name)
        {
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                int  s = taskControl.TaskControlID;

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/HomeOwners/" + name + ".rdlc");
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;

                GetReportHomeOwnersQuoteTableAdapters.GetReportHomeOwnersQuoteTableAdapter ds1 = new GetReportHomeOwnersQuoteTableAdapters.GetReportHomeOwnersQuoteTableAdapter();
                GetReportHomeOwnersQuote.GetReportHomeOwnersQuoteDataTable ta = new GetReportHomeOwnersQuote.GetReportHomeOwnersQuoteDataTable();

                GetReportHomeOwnersPolicyAdditionalInsuredTableAdapters.GetReportHomeOwnersPolicyAdditionalInsuredTableAdapter ta3 = new GetReportHomeOwnersPolicyAdditionalInsuredTableAdapters.GetReportHomeOwnersPolicyAdditionalInsuredTableAdapter();
                GetReportHomeOwnersPolicyAdditionalInsured.GetReportHomeOwnersPolicyAdditionalInsuredDataTable dt3 = new GetReportHomeOwnersPolicyAdditionalInsured.GetReportHomeOwnersPolicyAdditionalInsuredDataTable();

              
                ReportDataSource rds4 = new ReportDataSource();
                rds4 = new ReportDataSource("GetReportHomeOwnersPolicyAdditionalInsured", (System.Data.DataTable)ta3.GetData(s));

                ReportDataSource rds2 = new ReportDataSource();
                rds2 = new ReportDataSource("GetReportHomeOwnersQuote", (System.Data.DataTable)ds1.GetData(s));
                EPolicy.TaskControl.HomeOwners taskControl1 = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                viewer1.LocalReport.DataSources.Add(rds4);
                viewer1.LocalReport.DataSources.Add(rds2);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;

                string _FileName1 = "QuoteNo- " + s + "-" + name + ".pdf";

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

        private List<string> imprimirPolicy(List<string> mergePaths, int counter, string name)
        {
            try
            {

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                int s = taskControl.TaskControlID;

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/HomeOwners/" + name + ".rdlc");
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;

                GetReportHomeOwnersPolicyTableAdapters.GetReportHomeOwnersPolicyTableAdapter ta = new GetReportHomeOwnersPolicyTableAdapters.GetReportHomeOwnersPolicyTableAdapter();
                GetReportHomeOwnersPolicy.GetReportHomeOwnersPolicyDataTable dt1 = new GetReportHomeOwnersPolicy.GetReportHomeOwnersPolicyDataTable();

                ReportDataSource rds2 = new ReportDataSource();
                rds2 = new ReportDataSource("GetReportHomeOwnersPolicy", (System.Data.DataTable)ta.GetData(s));

                GetInvoiceInfoByTaskControlIDTableAdapters.GetInvoiceInfoByTaskControlIDTableAdapter ta2 = new GetInvoiceInfoByTaskControlIDTableAdapters.GetInvoiceInfoByTaskControlIDTableAdapter();
                GetInvoiceInfoByTaskControlID.GetInvoiceInfoByTaskControlIDDataTable dt2 = new GetInvoiceInfoByTaskControlID.GetInvoiceInfoByTaskControlIDDataTable();

                ReportDataSource rds3 = new ReportDataSource();
                rds3 = new ReportDataSource("GetInvoiceInfoByTaskControlID", (System.Data.DataTable)ta2.GetData(s));

                GetReportHomeOwnersPolicyAdditionalInsuredTableAdapters.GetReportHomeOwnersPolicyAdditionalInsuredTableAdapter ta3 = new GetReportHomeOwnersPolicyAdditionalInsuredTableAdapters.GetReportHomeOwnersPolicyAdditionalInsuredTableAdapter();
                GetReportHomeOwnersPolicyAdditionalInsured.GetReportHomeOwnersPolicyAdditionalInsuredDataTable dt3 = new GetReportHomeOwnersPolicyAdditionalInsured.GetReportHomeOwnersPolicyAdditionalInsuredDataTable();

                ReportDataSource rds4 = new ReportDataSource();
                rds4 = new ReportDataSource("GetReportHomeOwnersPolicyAdditionalInsured", (System.Data.DataTable)ta3.GetData(s));

                EPolicy.TaskControl.HomeOwners taskControl1 = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                if (name == "AgentInvoice_VI")
                {
                    string ImgPath = "", AgentDesc = "";

                    Uri pathAsUri = null;

                    System.Data.DataTable dt = null, dtAgent = null;

                    dt = EPolicy.TaskControl.TaskControl.GetImageLogoByAgentID(taskControl1.Agent.ToString().Trim());

                    dtAgent = EPolicy.TaskControl.TaskControl.GetAgentByAgentID(taskControl1.Agent.ToString().Trim());

                    if (dtAgent.Rows.Count > 0)
                    {
                        AgentDesc = dtAgent.Rows[0]["AgentDesc"].ToString().Trim();
                    }

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
                    else
                    {

                    }

                    ReportParameter[] parameters = new ReportParameter[1];
                    parameters[0] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");


                    viewer1.LocalReport.EnableExternalImages = true;
                    viewer1.LocalReport.DataSources.Add(rds3);
                    viewer1.LocalReport.SetParameters(parameters);
                }
                else
                {
                    viewer1.LocalReport.DataSources.Add(rds2);
                    viewer1.LocalReport.DataSources.Add(rds4);
                }

                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                //string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + name; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1";
                string _FileName1 = "PolicyNo- " + s + "-" + name + ".pdf"; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1" + ".pdf";

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

        private List<string> imprimirMortgagee(List<string> mergePaths, int counter, string name)
        {
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                if (taskControl.bank2.Trim() != "" && taskControl.bank2.Trim() != "NO PAYEE STATED ( )")
                {
                    string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                    int s = taskControl.TaskControlID;

                    ReportViewer viewer1 = new ReportViewer();
                    viewer1.LocalReport.ReportPath = Server.MapPath("Reports/HomeOwners/" + name + ".rdlc");
                    viewer1.LocalReport.DataSources.Clear();
                    viewer1.ProcessingMode = ProcessingMode.Local;

                    GetReportHomeOwnersPolicyTableAdapters.GetReportHomeOwnersPolicyTableAdapter ta = new GetReportHomeOwnersPolicyTableAdapters.GetReportHomeOwnersPolicyTableAdapter();
                    GetReportHomeOwnersPolicy.GetReportHomeOwnersPolicyDataTable dt1 = new GetReportHomeOwnersPolicy.GetReportHomeOwnersPolicyDataTable();

                    ReportDataSource rds2 = new ReportDataSource();
                    rds2 = new ReportDataSource("GetReportHomeOwnersPolicy", (System.Data.DataTable)ta.GetData(s));

                    viewer1.LocalReport.DataSources.Add(rds2);
                    viewer1.LocalReport.Refresh();

                    Warning[] warnings = null;
                    string[] streamIds = null;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    string filetype = string.Empty;


                    //string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + name; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1";
                    string _FileName1 = "PolicyNo- " + s + "-" + name + ".pdf"; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1" + ".pdf";

                    if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                        File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

                    byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    using (FileStream fs1 = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1, FileMode.Create))
                    {
                        fs1.Write(bytes1, 0, bytes1.Length);
                        fs1.Close();
                    }

                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);
                }

                return mergePaths;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnPrint_Click(object sendegr, EventArgs e)
        {
            try
            {
                System.Web.UI.WebControls.Button clickedButton = (System.Web.UI.WebControls.Button)sendegr;

                List<string> mergePaths = new List<string>();
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                int taskControlID = taskControl.TaskControlID;
                FileInfo mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "E&O\\E&OPolicy.pdf");
                int Counter = 1;

                if (clickedButton.Text.ToString() == "PRINT")
                {
                    mergePaths = imprimirPolicy(mergePaths, Counter, "Policy");
                   
                    Counter++;

                    #region Policy Mandatory Forms
                    if (txtPropertyForm.Text == "HO 6")
                    {
                        //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Schedule A.pdf");
                        //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf", true);
                        //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf");
                        mergePaths = imprimirPolicy(mergePaths, Counter, "SCHEDULE A");
                       // mergePaths = imprimirMortgagee(mergePaths, Counter, "Mortgagee_AdditionalIntrest");
                        Counter++;

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\HO6-4-84.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO6-4-84" + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO6-4-84" + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC -3.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 4.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mergePaths = imprimirPolicy(mergePaths, Counter, "Endorsement 1 - HO");//HO
                        Counter++;

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 21.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 30.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\MAP.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 31.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Electronic_Data_Endorsement_NMA2915.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf");

                    }
                    else if (txtPropertyForm.Text == "HO 2")
                    {
                        //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Schedule A.pdf");
                        //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf", true);
                        //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf");
                        mergePaths = imprimirPolicy(mergePaths, Counter, "SCHEDULE A");
                       // mergePaths = imprimirMortgagee(mergePaths, Counter, "Mortgagee_AdditionalIntrest");
                        Counter++;

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\HO 2 (4-84).pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 2 (4-84)" + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 2 (4-84)" + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC -3.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 4.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mergePaths = imprimirPolicy(mergePaths, Counter, "Endorsement 1 - HO");//HO
                        Counter++;

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 21.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 30.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\MAP.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 31.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Electronic_Data_Endorsement_NMA2915.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf");

                    }
                    else if (txtPropertyForm.Text == "HO 4")
                    {
                        //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Schedule A.pdf");
                        //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf", true);
                        //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf");
                        mergePaths = imprimirPolicy(mergePaths, Counter, "SCHEDULE A");
                        //mergePaths = imprimirMortgagee(mergePaths, Counter, "Mortgagee_AdditionalIntrest");
                        Counter++;

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\HO 4 (4-84).pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 4 (4-84)" + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 4 (4-84)" + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC -3.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 4.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mergePaths = imprimirPolicy(mergePaths, Counter, "Endorsement 1 - HO");//HO
                        Counter++;

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 21.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 30.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\MAP.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 31.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf" + taskControl.TaskControlID.ToString() + ".pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Electronic_Data_Endorsement_NMA2915.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf");

                    }
                    else if (txtPropertyForm.Text == "INC")
                    {

                        //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Schedule A.pdf");
                        //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf", true);
                        //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Schedule A.pdf");
                        mergePaths = imprimirPolicy(mergePaths, Counter, "SCHEDULE A");
                        //mergePaths = imprimirMortgagee(mergePaths, Counter, "Mortgagee_AdditionalIntrest");
                        Counter++;

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\INC Policy Form.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "INC Policy Form.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "INC Policy Form.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\CF 0011 (01-83).pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "CF 0011 (01-83).pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "CF 0011 (01-83).pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Endorsement A.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Endorsement A.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Endorsement A.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC -3.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC -3.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 4.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 4.pdf");

                        mergePaths = imprimirPolicy(mergePaths, Counter, "Endorsement 1 - INC");//INC
                        Counter++;

                        //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Endorsement 1 - INC.pdf");
                        //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Endorsement 1 - INC.pdf", true);
                        //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Endorsement 1 - INC.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 21.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 21.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 30.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 30.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\GIC - 31.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "GIC - 31.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\MAP.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MAP.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Electronic_Data_Endorsement_NMA2915.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Electronic_Data_Endorsement_NMA2915.pdf");

                        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\Endorsement 2.pdf");
                        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Endorsement 2.pdf", true);
                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Endorsement 2.pdf");

                    }
                    else if (txtPropertyForm.Text == "RES")
                    {

                    }
                    #endregion Policy Mandatory Forms

                    #region Additional Forms
                    if (txtPropertyForm.Text == "INC")
                    {
                        if (ddlCatastropheDeduc.SelectedItem.Value == "No Coverage")
                        {
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\WEQ.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WEQ.pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WEQ.pdf");
                            mergePaths = imprimirPolicy(mergePaths, Counter, "WEQ");
                            Counter++;
                        }
                        else
                        {
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\CF 10 41 (12-81).pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "CF 10 41 (12-81).pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "CF 10 41 (12-81).pdf");
                        }
                        if (1 == 0)//rdbYesStruct.Checked
                        {
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\HO 70 (4-84).pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 70 (4-84).pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 70 (4-84).pdf");
                        }
                        if (ddlCatastropheDeduc.SelectedItem.Value.Contains("EQ") && !ddlCatastropheDeduc.SelectedItem.Value.Contains("Wind"))
                        {
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\WindX.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WindX.pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WindX.pdf");
                            mergePaths = imprimirPolicy(mergePaths, Counter, "WindX");
                            Counter++;
                        }
                        if (txtLimit4.Text != "$0.00")
                        {
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\CP00324L-Business income.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "CP00324L-Business income.pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "CP00324L-Business income.pdf");
                        }
                    }
                    else
                    {
                        if (ddlCatastropheDeduc.SelectedItem.Value == "No Coverage")
                        {
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\WEQ.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WEQ.pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WEQ.pdf");
                            mergePaths = imprimirPolicy(mergePaths, Counter, "WEQ");
                            Counter++;
                        }
                        else
                        {
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\HO 54 (4-84).pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 54 (4-84).pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 54 (4-84).pdf");
                        }
                        if (1 == 0)//rdbYesStruct.Checked
                        {
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\HO 70 (4-84).pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 70 (4-84).pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 70 (4-84).pdf");
                        }
                        if (ddlCatastropheDeduc.SelectedItem.Value.Contains("EQ") && !ddlCatastropheDeduc.SelectedItem.Value.Contains("Wind"))
                        {
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\WindX.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WindX.pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "WindX.pdf");
                            mergePaths = imprimirPolicy(mergePaths, Counter, "WindX");
                            Counter++;
                        }
                        if (ddlPropertyType.SelectedItem.Text == "Condo - Rented to Others")
                        {
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\HO 33 - 4-84.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 33 - 4-84.pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "HO 33 - 4-84.pdf");
                        }
                    }
                    #endregion Additional Forms


                    #region Policy Forms for All


                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\SEEPAGE_POLLUTION_CONTAMINATION_EXCLUSION.pdf");
                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "SEEPAGE_POLLUTION_CONTAMINATION_EXCLUSION" + ".pdf", true);
                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "SEEPAGE_POLLUTION_CONTAMINATION_EXCLUSION" + ".pdf");

                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\TRANSMISSION_AND_DISTRIBUTION_LINES_EXCLUSION.pdf");
                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "TRANSMISSION_AND_DISTRIBUTION_LINES_EXCLUSION" + ".pdf", true);
                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "TRANSMISSION_AND_DISTRIBUTION_LINES_EXCLUSION" + ".pdf");

                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\LMA5393-Communicable_Disease_Endorsement_Property.pdf");
                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "LMA5393-Communicable_Disease_Endorsement_Property" + ".pdf", true);
                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "LMA5393-Communicable_Disease_Endorsement_Property" + ".pdf");

                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\ASBESTOS_EXCLUSION.pdf");
                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "ASBESTOS_EXCLUSION" + ".pdf", true);
                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "ASBESTOS_EXCLUSION" + ".pdf");


                    mergePaths = imprimirMortgagee(mergePaths, Counter, "Mortgagee_AdditionalIntrest");

                    #endregion Policy Forms for All

                }
                else if (clickedButton.Text.ToString().Contains("INVOICE"))
                {
                    mergePaths = imprimirPolicy(mergePaths, Counter, "AgentInvoice_VI");
                    Counter++;
                }
                else
                {
                    mergePaths = imprimirPolicy(mergePaths, Counter, "Policy");
                    Counter++;
                }

               

                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
                //Generar PDF
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

        protected void btnPrintQuote_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> mergePaths = new List<string>();
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                int taskControlID = taskControl.TaskControlID;
                int Counter = 1;

                //if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnPrintQuote")

                if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnPrintApplication")
                    mergePaths = imprimirQuote(mergePaths, Counter, "BlankQuote");
                else
                {
                    mergePaths = imprimirQuote(mergePaths, Counter, "Quote");
                    Counter++;
                    FileInfo mFileIndex = null;
                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"].ToString() + "HomeOwners\\NOTICE OF CONDITIONS OF UNDERINSURANCE FINAL VERSION.pdf");
                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NOTICE OF CONDITIONS OF UNDERINSURANCE FINAL VERSION" + ".pdf", true);
                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NOTICE OF CONDITIONS OF UNDERINSURANCE FINAL VERSION" + ".pdf");

                    if (ddlCatastropheDeduc.SelectedItem.Value == "No Coverage")
                    {
                        mergePaths = imprimirQuote(mergePaths, Counter, "WEQQ");
                        Counter++;
                    }
                    if (ddlCatastropheDeduc.SelectedItem.Value.Contains("EQ") && !ddlCatastropheDeduc.SelectedItem.Value.Contains("Wind"))
                    {
                        mergePaths = imprimirQuote(mergePaths, Counter, "WindXQ");
                        Counter++;
                    }
                }

                //lblFirstName.Text = "Hello";
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
                //Generar PDF
                OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                string FinalFile = "";
                FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());

                if (sender == "")
                    TextBox1.Text = FinalFile;
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
            }
            catch (Exception exc)
            {
                lblRecHeader.Text = exc.Message.Trim() + " - ";
                mpeSeleccion.Show();
            }
        }

        protected void connectWithExcel()
        {
            //COMO FUNCIONA EN SERVIDOR
            //https://stackoverflow.com/questions/28578642/excel-file-on-server-not-found-when-opened-by-client
            //https://www.google.com/search?q=excel+interop+not+working+server+folder&rlz=1C1CHBF_enPR744PR744&oq=excel+interop+not+working+server+folder&aqs=chrome..69i57j69i64.9623j0j1&sourceid=chrome&ie=UTF-8
            //https://stackoverflow.com/questions/17785063/retrieving-the-com-class-factory-for-component-error-80070005-access-is-de

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            Microsoft.Office.Interop.Excel.Application oExcel = null;
            Microsoft.Office.Interop.Excel._Workbook oBook = null;
            Microsoft.Office.Interop.Excel._Worksheet oSheet = null;

            
            try
            {
                string filenametemp = System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString() + "ePPSProperty_Temp" + txtFirstName.Text.ToString().Trim() + txtLastName.Text.ToString().Trim() + "_" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Replace(" ", "") + ".xlsx";

                //string filenametemp = @"C:\Users\Victor\Desktop\Guardian-HO\GuardianHO.xlsx";
                if (!File.Exists(filenametemp))
                    //File.Copy(System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString() + "ePPSProperty_last.xlsx", filenametemp, true);
                    File.Copy(System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString() + "ePPSPropertyTemplateLanzaVersion18-21.xlsx", filenametemp, true);
                else
                {
                    File.Delete(filenametemp);
                    //File.Copy(System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString() + "ePPSProperty_last.xlsx", filenametemp, true);
                    File.Copy(System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString() + "ePPSPropertyTemplateLanzaVersion18-21.xlsx", filenametemp, true);
                }


                try
                {
                    oExcel = new Microsoft.Office.Interop.Excel.Application();
                    oExcel.Visible = true;
                }
                catch (Exception exp)
                {
                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                    foreach (System.Diagnostics.Process p in process)
                    {
                        if (!string.IsNullOrEmpty(p.ProcessName))
                        {
                            try
                            {
                                p.Kill();
                            }
                            catch { }
                        }
                    }
                    connectWithExcel();
                }

                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                oBook = oExcel.Workbooks.Open(filenametemp);

                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oBook.Worksheets[3];
                // e.g. Change value in A2 cell

                //Solo considera la fecha del issueDate Quote para que no cambie la el % (8% -> 3%) de descuento en la hoja de excel
                if (taskControl.isQuote == true)
                    //oSheet.Range["B10"].Value = txtIssueDate.Text.Trim();
                    oSheet.Range["B12"].Value = txtIssueDate.Text.Trim();
                else
                {
                    int quoteTCID = (int)Session["TaskControlIDOLD"];
                    TaskControl.TaskControl tc = TaskControl.TaskControl.GetTaskControlByTaskControlID(quoteTCID, 1);
                    //oSheet.Range["B10"].Value = tc.EntryDate.ToShortDateString();
                    oSheet.Range["B12"].Value = tc.EntryDate.ToShortDateString();
                }

                //oSheet.Range["B13"].Value = ddlCatastropheDeduc.SelectedItem.Text.Trim();
                oSheet.Range["B15"].Value = ddlCatastropheDeduc.SelectedItem.Text.Trim();

                //oSheet.Range["B17"].Value = ddlConstructionType.SelectedItem.Text.Trim();
                oSheet.Range["B19"].Value = ddlConstructionType.SelectedItem.Text.Trim();


                //oSheet.Range["B25"].Value = ddlRoofDwelling.SelectedItem.Text.Trim();
                oSheet.Range["B27"].Value = ddlRoofDwelling.SelectedItem.Text.Trim();


                //oSheet.Range["E18"].Value = ddlPropertyType.SelectedItem.Text.Trim();
                oSheet.Range["E20"].Value = ddlPropertyType.SelectedItem.Text.Trim();


               // oSheet.Range["E53"].Value = ddlLiaLimit.SelectedItem.Text.Trim();
				 
				string lialimit = "0";
				 if (ddlLiaLimit.SelectedItem.Value != "NO COVERAGE")
					lialimit = ddlLiaLimit.SelectedItem.Text.Trim();
				
                oSheet.Range["E55"].Value = lialimit; //ddlLiaLimit.SelectedItem.Text.Trim();

                //oSheet.Range["E21"].Value = ddlLosses3Years.SelectedItem.Text.Trim();
                oSheet.Range["E23"].Value = ddlLosses3Years.SelectedItem.Text.Trim();
                //oSheet.Range["E25"].Value = ddlAutoPolicyWitGuardian.SelectedItem.Text.Trim();
                oSheet.Range["E27"].Value = ddlAutoPolicyWitGuardian.SelectedItem.Text.Trim();

                // Property Information



                if (rdbYes.Checked)
                {
                    //oSheet.Range["B21"].Value = "Yes";
                    oSheet.Range["B23"].Value = "Yes";
                }
                else
                {
                   // oSheet.Range["B21"].Value = "No";
                    oSheet.Range["B23"].Value = "No";
                }

                //oSheet.Range["B22"].Value = txtIfYes.Text.ToString().Trim();
                //oSheet.Range["B23"].Value = txtLivingArea.Text.ToString().Trim();
                //oSheet.Range["B24"].Value = txtPorches.Text.ToString().Trim();
                //oSheet.Range["E22"].Value = txtOtherStruct.Text.ToString().Trim();
                //oSheet.Range["E23"].Value = ddlPropertyShuttered.SelectedItem.Text.Trim();
                //oSheet.Range["E24"].Value = ddlRoofOverhang.SelectedItem.Text.Trim();
                //oSheet.Range["E26"].Value = txtComments.Text.ToString().Trim();

                oSheet.Range["B24"].Value = txtIfYes.Text.ToString().Trim();
                oSheet.Range["B25"].Value = txtLivingArea.Text.ToString().Trim();
                oSheet.Range["B26"].Value = txtPorches.Text.ToString().Trim();
                oSheet.Range["E24"].Value = txtOtherStruct.Text.ToString().Trim();
                oSheet.Range["E25"].Value = ddlPropertyShuttered.SelectedItem.Text.Trim();
                oSheet.Range["E26"].Value = ddlRoofOverhang.SelectedItem.Text.Trim();
                oSheet.Range["E28"].Value = txtComments.Text.ToString().Trim();


                // Customer Information
                //oSheet.Range["B29"].Value = txtFirstName.Text.ToString().Trim() + " " + txtLastName.Text.ToString().Trim();
                //oSheet.Range["B30"].Value = txtMailingAddress.Text.ToString().Trim();
                //oSheet.Range["B31"].Value = txtMailingAddress2.Text.ToString().Trim();

                oSheet.Range["B31"].Value = txtFirstName.Text.ToString().Trim() + " " + txtLastName.Text.ToString().Trim();
                oSheet.Range["B32"].Value = txtMailingAddress.Text.ToString().Trim();
                oSheet.Range["B33"].Value = txtMailingAddress2.Text.ToString().Trim();

                //oSheet.Range["B32"].Value = txtCity.Text.ToString().Trim();
                oSheet.Range["B34"].Value = txtCity.Text.ToString().Trim();


                //oSheet.Range["B33"].Value = txtState.Text.ToString().Trim();
                //oSheet.Range["B34"].Value = txtZipCode.Text.ToString().Trim();

                oSheet.Range["B35"].Value = txtState.Text.ToString().Trim();
                oSheet.Range["B36"].Value = txtZipCode.Text.ToString().Trim();

                //oSheet.Range["B36"].Value = txtOccupation.Text.ToString().Trim();
                //oSheet.Range["B37"].Value = txtBusinessPhone.Text.ToString().Trim();

                oSheet.Range["B38"].Value = txtOccupation.Text.ToString().Trim();
                oSheet.Range["B39"].Value = txtBusinessPhone.Text.ToString().Trim();

                //oSheet.Range["B38"].Value = txtMobilePhone.Text.ToString().Trim();
                //oSheet.Range["B39"].Value = txtEmail.Text.ToString().Trim();

                oSheet.Range["B40"].Value = txtMobilePhone.Text.ToString().Trim();
                oSheet.Range["B41"].Value = txtEmail.Text.ToString().Trim();

                //oSheet.Range["D30"].Value = txtPhysicalAddress1.Text.ToString().Trim();
                oSheet.Range["D32"].Value = txtPhysicalAddress1.Text.ToString().Trim();


                //oSheet.Range["D31"].Value = txtPhysicalAddress2.Text.ToString().Trim();
                //oSheet.Range["D32"].Value = txtCity2.Text.ToString().Trim();

                oSheet.Range["D33"].Value = txtPhysicalAddress2.Text.ToString().Trim();
                oSheet.Range["D34"].Value = txtCity2.Text.ToString().Trim();

                //oSheet.Range["D33"].Value = txtState2.Text.ToString().Trim();
                //oSheet.Range["D34"].Value = txtZipCode2.Text.ToString().Trim();

                oSheet.Range["D35"].Value = txtState2.Text.ToString().Trim();
                oSheet.Range["D36"].Value = txtZipCode2.Text.ToString().Trim();

                //oSheet.Range["F30"].Value = txtBank.Text.ToString().Trim();
                oSheet.Range["F32"].Value = txtBank.Text.ToString().Trim();

                //oSheet.Range["F31"].Value = txtLoanNo.Text.ToString().Trim();
                oSheet.Range["F33"].Value = txtLoanNo.Text.ToString().Trim();


                //oSheet.Range["F32"].Value = txtBank2.Text.ToString().Trim();
                //oSheet.Range["F33"].Value = txtLoanNo2.Text.ToString().Trim();

                oSheet.Range["F34"].Value = txtBank2.Text.ToString().Trim();
                oSheet.Range["F35"].Value = txtLoanNo2.Text.ToString().Trim();

                //oSheet.Range["F34"].Value = ddlTypeOfInterest.SelectedItem.Value;
                //oSheet.Range["F35"].Value = ddlMortgageeBilled.SelectedItem.Value;

                oSheet.Range["F36"].Value = ddlTypeOfInterest.SelectedItem.Value;
                oSheet.Range["F37"].Value = ddlMortgageeBilled.SelectedItem.Value;




               oSheet.Range["F39"].Value = (double.Parse(ddlDiscount.SelectedItem.Text) / 100).ToString().Trim();

               oSheet.Range["B10"].Value = ddlTypeOfInsured.SelectedItem.Text.Trim();


               txtLimit1.Text = FormatCurrency(txtLimit1.Text);
               txtLimit2.Text = FormatCurrency(txtLimit2.Text);
               txtLimit3.Text = FormatCurrency(txtLimit3.Text);
               txtLimit4.Text = FormatCurrency(txtLimit4.Text);
                //FormatCurrency(txtLimit2);
                //FormatCurrency(txtLimit3);
                //FormatCurrency(txtLimit4);

                if (double.Parse(txtLimit1.Text.ToString().Replace("$", "")) > 0)
                {
                    //oSheet.Range["B43"].Value = txtLimit2.Text.Replace("$", "").Replace(",", "").Trim();

                    //txtLimit2.Text = Math.Round(decimal.Parse(oSheet.Range["B43"].Value.ToString()), 0).ToString("$##,###,###.00");

                    oSheet.Range["B45"].Value = txtLimit1.Text.Replace("$", "").Replace(",", "").Trim();

                    txtLimit1.Text = Math.Round(decimal.Parse(oSheet.Range["B45"].Value.ToString()), 0).ToString("$##,###,###.00");

                    if (ddlCatastropheDeduc.SelectedItem.Value == "No Coverage")
                    {
                        txtWindstormDed1.Text = "";
                        txtWindstormDedPer1.Text = "NO COVERAGE";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtCatastropheCoverage.Text = "NO COVERAGE";
                        txtEarthquakeDed1.Text = "";
                        txtEarthQuakeDedPer1.Text = "NO COVERAGE";
                        txtTotalEarth.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "10% EQ")
                    {
                        txtWindstormDed1.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer1.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        txtEarthquakeDed1.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F43"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer1.Text = double.Parse(oSheet.Range["E43"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDedPer1.Text = double.Parse(oSheet.Range["E45"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = oSheet.Range["E14"].Value.ToString();
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = oSheet.Range["E16"].Value.ToString();
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");

                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "5% EQ")
                    {
                        txtWindstormDed1.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer1.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        txtEarthquakeDed1.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F43"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer1.Text = double.Parse(oSheet.Range["E43"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");

                        txtEarthQuakeDedPer1.Text = double.Parse(oSheet.Range["E45"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");

                    }
                    else
                    {
                        txtWindstormDed1.Text = "";//Math.Round(decimal.Parse(oSheet.Range["D43"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtWindstormDedPer1.Text = double.Parse(oSheet.Range["D43"].Value.ToString()).ToString("#0.##%");
                        txtWindstormDedPer1.Text = double.Parse(oSheet.Range["D45"].Value.ToString()).ToString("#0.##%");
                        txtEarthquakeDed1.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F43"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer1.Text = double.Parse(oSheet.Range["E43"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDedPer1.Text = double.Parse(oSheet.Range["E45"].Value.ToString()).ToString("#0.##%");

                        //txtWindstormDeductible.Text = double.Parse(oSheet.Range["B14"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");

                        txtWindstormDeductible.Text = double.Parse(oSheet.Range["B16"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");
                    }
                    //txtAOPDed1.Text = Math.Round(decimal.Parse(oSheet.Range["C43"].Value.ToString()), 0).ToString("$##,###,###.00");
                    //txtCoinsurance1.Text = double.Parse(oSheet.Range["F43"].Value.ToString()).ToString("#0.##%");

                    txtAOPDed1.Text = Math.Round(decimal.Parse(oSheet.Range["C45"].Value.ToString()), 0).ToString("$##,###,###.00");
                    txtCoinsurance1.Text = double.Parse(oSheet.Range["F45"].Value.ToString()).ToString("#0.##%");

                    //if (Math.Round(double.Parse(oSheet.Range["G43"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["G43"].Value.ToString()), 2) < 0.50)
                    if (Math.Round(double.Parse(oSheet.Range["H45"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["H45"].Value.ToString()), 2) < 0.50)
                    {
                        txtPremium1.Text = "$1.00";
                    }
                    else
                    {
                        txtPremium1.Text = String.Format("{0:C}", double.Parse(oSheet.Range["H45"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium1.Text = String.Format("{0:C}", double.Parse(oSheet.Range["G43"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium1.Text = String.Format("{0:C}", CalculateDiscount(double.Parse(oSheet.Range["G43"].Value2.ToString())).ToString("$#,###"));
                    }

                }
                else
                {
                    oSheet.Range["B43"].Value = 0;
                    txtAOPDed1.Text = "$0.00";
                    txtWindstormDed1.Text = "$0.00";
                    txtWindstormDedPer1.Text = "0%";
                    txtEarthquakeDed1.Text = "$0.00";
                    txtEarthQuakeDedPer1.Text = "0%";
                    txtCoinsurance1.Text = "0%";
                    txtPremium1.Text = "$0.00";
                }


                if (double.Parse(txtLimit2.Text.ToString().Replace("$", "")) > 0)
                {
                    //oSheet.Range["B44"].Value = txtLimit2.Text.Replace("$", "").Replace(",", "").Trim();
                    //txtTotalLimit.Text = Math.Round(decimal.Parse(oSheet.Range["B48"].Value.ToString()), 0).ToString("$##,###,###.00");
                    oSheet.Range["B46"].Value = txtLimit2.Text.Replace("$", "").Replace(",", "").Trim();
                    txtTotalLimit.Text = Math.Round(decimal.Parse(oSheet.Range["B50"].Value.ToString()), 0).ToString("$##,###,###.00");
                    if (ddlCatastropheDeduc.SelectedItem.Value == "No Coverage")
                    {
                        txtWindstormDed2.Text = "";
                        txtWindstormDedPer2.Text = "NO COVERAGE";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtCatastropheCoverage.Text = "NO COVERAGE";
                        txtEarthquakeDed2.Text = "";
                        txtEarthQuakeDedPer2.Text = "NO COVERAGE";
                        txtTotalEarth.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "10% EQ")
                    {
                        txtWindstormDed2.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer2.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        txtEarthquakeDed2.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F44"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer2.Text = double.Parse(oSheet.Range["E44"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDedPer2.Text = double.Parse(oSheet.Range["E46"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");


                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "5% EQ")
                    {
                        txtWindstormDed2.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer2.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        txtEarthquakeDed2.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F44"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer2.Text = double.Parse(oSheet.Range["E44"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDedPer2.Text = double.Parse(oSheet.Range["E46"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");

                    }
                    else
                    {

                        txtWindstormDed2.Text = "";//Math.Round(decimal.Parse(oSheet.Range["D44"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtWindstormDedPer2.Text = double.Parse(oSheet.Range["D44"].Value.ToString()).ToString("#0.##%");
                        txtWindstormDedPer2.Text = double.Parse(oSheet.Range["D46"].Value.ToString()).ToString("#0.##%");
                        txtEarthquakeDed2.Text = "";//double.Parse(oSheet.Range["F44"].Value.ToString()).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer2.Text = double.Parse(oSheet.Range["E44"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDedPer2.Text = double.Parse(oSheet.Range["E46"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");


                    }

                    //txtAOPDed2.Text = Math.Round(decimal.Parse(oSheet.Range["C44"].Value.ToString()), 0).ToString("$##,###,###.00");
                    txtAOPDed2.Text = Math.Round(decimal.Parse(oSheet.Range["C46"].Value.ToString()), 0).ToString("$##,###,###.00");

                    //txtCoinsurance2.Text = double.Parse(oSheet.Range["F44"].Value.ToString()).ToString("#0.##%");
                    txtCoinsurance2.Text = double.Parse(oSheet.Range["F46"].Value.ToString()).ToString("#0.##%");


                    //if (Math.Round(double.Parse(oSheet.Range["G44"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["G44"].Value.ToString()), 2) < 0.50)
                    if (Math.Round(double.Parse(oSheet.Range["H46"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["H46"].Value.ToString()), 2) < 0.50)
                    {
                        txtPremium2.Text = "$1.00";
                    }
                    else
                    {
                         txtPremium2.Text = String.Format("{0:C}", double.Parse(oSheet.Range["H46"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium2.Text = String.Format("{0:C}", double.Parse(oSheet.Range["G44"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium2.Text = String.Format("{0:C}", CalculateDiscount(double.Parse(oSheet.Range["G44"].Value2.ToString())).ToString("$#,###"));
                    }


                }
                else
                {
                    //oSheet.Range["B44"].Value = 0;
                    oSheet.Range["B46"].Value = 0;
                    txtAOPDed2.Text = "$0.00";
                    txtWindstormDed2.Text = "$0.00";
                    txtWindstormDedPer2.Text = "0%";
                    txtEarthquakeDed2.Text = "$0.00";
                    txtEarthQuakeDedPer2.Text = "0%";
                    txtCoinsurance2.Text = "0%";
                    txtPremium2.Text = "$0.00";
                }

                if (double.Parse(txtLimit3.Text.ToString().Replace("$", "")) > 0)
                {
                    //oSheet.Range["B45"].Value = txtLimit3.Text.Replace("$", "").Replace(",", "").Trim();
                    oSheet.Range["B47"].Value = txtLimit3.Text.Replace("$", "").Replace(",", "").Trim();
                    if (ddlCatastropheDeduc.SelectedItem.Value == "No Coverage")
                    {
                        txtWindstormDed3.Text = "";
                        txtWindstormDedPer3.Text = "NO COVERAGE";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtCatastropheCoverage.Text = "NO COVERAGE";
                        txtEarthquakeDed3.Text = "";
                        txtEarthQuakeDedPer3.Text = "NO COVERAGE";
                        txtTotalEarth.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "10% EQ")
                    {
                        txtWindstormDed3.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer3.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        txtEarthquakeDed3.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F45"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer3.Text = double.Parse(oSheet.Range["E45"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");

                        txtEarthQuakeDedPer3.Text = double.Parse(oSheet.Range["E47"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");
                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "5% EQ")
                    {
                        txtWindstormDed3.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer3.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        txtEarthquakeDed3.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F45"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer3.Text = double.Parse(oSheet.Range["E45"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");
                        //txtTotalEarth.Text = Math.Round(decimal.Parse(oSheet.Range["E48"].Value.ToString()), 0).ToString("$##,###,###.00");

                        txtEarthQuakeDedPer3.Text = double.Parse(oSheet.Range["E47"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");
                        txtTotalEarth.Text = Math.Round(decimal.Parse(oSheet.Range["E50"].Value.ToString()), 0).ToString("$##,###,###.00");
                    }
                    else
                    {

                        txtWindstormDed3.Text = "";//Math.Round(decimal.Parse(oSheet.Range["D45"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtWindstormDedPer3.Text = double.Parse(oSheet.Range["D45"].Value.ToString()).ToString("#0.##%");
                        txtWindstormDedPer3.Text = double.Parse(oSheet.Range["D47"].Value.ToString()).ToString("#0.##%");
                        txtEarthquakeDed3.Text = "";//double.Parse(oSheet.Range["F45"].Value.ToString()).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer3.Text = double.Parse(oSheet.Range["E45"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDedPer3.Text = double.Parse(oSheet.Range["E47"].Value.ToString()).ToString("#0.##%");
                    }

                    //txtAOPDed3.Text = Math.Round(decimal.Parse(oSheet.Range["C45"].Value.ToString()), 0).ToString("$##,###,###.00");
                    //txtCoinsurance3.Text = oSheet.Range["F45"].Value != null ? double.Parse(oSheet.Range["F45"].Value.ToString()).ToString("#0.##%") : "0%"; ;
                    txtAOPDed3.Text = Math.Round(decimal.Parse(oSheet.Range["C47"].Value.ToString()), 0).ToString("$##,###,###.00");
                    txtCoinsurance3.Text = oSheet.Range["F47"].Value != null ? double.Parse(oSheet.Range["F47"].Value.ToString()).ToString("#0.##%") : "0%";

                    //if (Math.Round(double.Parse(oSheet.Range["G45"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["G45"].Value.ToString()), 2) < 0.50)
                    if (Math.Round(double.Parse(oSheet.Range["H47"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["H47"].Value.ToString()), 2) < 0.50)
                    {
                        txtPremium3.Text = "$1.00";
                    }
                    else
                    {
                        txtPremium3.Text = String.Format("{0:C}", double.Parse(oSheet.Range["H47"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium3.Text = String.Format("{0:C}", double.Parse(oSheet.Range["G45"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium3.Text = String.Format("{0:C}", CalculateDiscount(double.Parse(oSheet.Range["G45"].Value2.ToString())).ToString("$#,###"));
                    }

                }
                else
                {
                    //oSheet.Range["B45"].Value = 0;
                    oSheet.Range["B47"].Value = 0;
                    txtAOPDed3.Text = "$0.00";
                    txtWindstormDed3.Text = "$0.00";
                    txtWindstormDedPer3.Text = "0%";
                    txtEarthquakeDed3.Text = "$0.00";
                    txtEarthQuakeDedPer3.Text = "0%";
                    txtCoinsurance3.Text = "0%";
                    txtPremium3.Text = "$0.00";
                }

                if (double.Parse(txtLimit4.Text.ToString().Replace("$", "")) > 0)
                {
                    //oSheet.Range["B46"].Value = txtLimit4.Text.Replace("$", "").Replace(",", "").Trim();
                    oSheet.Range["B48"].Value = txtLimit4.Text.Replace("$", "").Replace(",", "").Trim();
                    if (ddlCatastropheDeduc.SelectedItem.Value == "No Coverage")
                    {
                        txtWindstormDed4.Text = "";
                        txtWindstormDedPer4.Text = "NO COVERAGE";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtCatastropheCoverage.Text = "NO COVERAGE";
                        txtEarthquakeDed4.Text = "";
                        txtEarthQuakeDedPer4.Text = "NO COVERAGE";
                        txtTotalEarth.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "10% EQ")
                    {
                        txtWindstormDed4.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer4.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        //txtEarthquakeDed4.Text = oSheet.Range["F46"].Value.ToString();
                        //txtEarthQuakeDedPer4.Text = oSheet.Range["E46"].Value.ToString();
                        txtEarthQuakeDedPer4.Text = oSheet.Range["E48"].Value.ToString();

                        txtEarthquakeDed4.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F46"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer4.Text = double.Parse(oSheet.Range["E46"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");

                        txtEarthQuakeDedPer4.Text = double.Parse(oSheet.Range["E48"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");
                    }
                    else if (ddlCatastropheDeduc.SelectedItem.Value == "5% EQ")
                    {
                        txtWindstormDed4.Text = "";
                        txtWindstormDeductible.Text = "NO COVERAGE";
                        txtWindstormDedPer4.Text = "NO COVERAGE";
                        txtTotalWind.Text = "NO COVERAGE";
                        txtEarthquakeDed4.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F46"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer4.Text = double.Parse(oSheet.Range["E46"].Value.ToString()).ToString("#0.##%");
                        //txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E14"].Value.ToString()).ToString("#0.##%");

                        txtEarthQuakeDedPer4.Text = double.Parse(oSheet.Range["E48"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDeductible.Text = double.Parse(oSheet.Range["E16"].Value.ToString()).ToString("#0.##%");
                    }
                    else
                    {
                        txtWindstormDed4.Text = "";//Math.Round(decimal.Parse(oSheet.Range["D46"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtWindstormDedPer4.Text = double.Parse(oSheet.Range["D46"].Value.ToString()).ToString("#0.##%");
                        txtWindstormDedPer4.Text = double.Parse(oSheet.Range["D48"].Value.ToString()).ToString("#0.##%");
                        txtEarthquakeDed4.Text = "";//Math.Round(decimal.Parse(oSheet.Range["F46"].Value.ToString()), 0).ToString("$##,###,###.00");
                        //txtEarthQuakeDedPer4.Text = double.Parse(oSheet.Range["E46"].Value.ToString()).ToString("#0.##%");
                        txtEarthQuakeDedPer4.Text = double.Parse(oSheet.Range["E48"].Value.ToString()).ToString("#0.##%");
                    }

                    //txtAOPDed4.Text = Math.Round(decimal.Parse(oSheet.Range["C46"].Value.ToString()), 0).ToString("$##,###,###.00");
                    txtAOPDed4.Text = Math.Round(decimal.Parse(oSheet.Range["C48"].Value.ToString()), 0).ToString("$##,###,###.00");

                    //txtCoinsurance4.Text = oSheet.Range["F46"].Value != null ? double.Parse(oSheet.Range["F46"].Value.ToString()).ToString("#0.##%") : "0%";
                    txtCoinsurance4.Text = oSheet.Range["F48"].Value != null ? double.Parse(oSheet.Range["F48"].Value.ToString()).ToString("#0.##%") : "0%";

                    //if (Math.Round(double.Parse(oSheet.Range["G46"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["G46"].Value.ToString()), 2) < 0.50)
                    if (Math.Round(double.Parse(oSheet.Range["H48"].Value.ToString()), 2) > 0.0 && Math.Round(double.Parse(oSheet.Range["H48"].Value.ToString()), 2) < 0.50)
                    {
                        txtPremium4.Text = "$1.00";
                    }
                    else
                    {
                        txtPremium4.Text = String.Format("{0:C}", double.Parse(oSheet.Range["H48"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium4.Text = String.Format("{0:C}", double.Parse(oSheet.Range["G46"].Value2.ToString()).ToString("$#,###"));
                        //txtPremium4.Text = String.Format("{0:C}", CalculateDiscount(double.Parse(oSheet.Range["G46"].Value2.ToString())).ToString("$#,###"));
                    }

                }
                else
                {
                    //oSheet.Range["B46"].Value = 0;
                    oSheet.Range["B48"].Value = 0;
                    txtAOPDed4.Text = "$0.00";
                    txtWindstormDed4.Text = "$0.00";
                    txtWindstormDedPer4.Text = "0%";
                    txtEarthquakeDed4.Text = "$0.00";
                    txtEarthQuakeDedPer4.Text = "0%";
                    txtCoinsurance4.Text = "0%";
                    txtPremium4.Text = "$0.00";
                }

                txtTotalPremium.Text = Math.Round(decimal.Parse(oSheet.Range["H50"].Value.ToString()), 0).ToString("$##,###,###.00");
                //txtTotalPremium.Text = Math.Round(decimal.Parse(oSheet.Range["G48"].Value.ToString()), 0).ToString("$##,###,###.00");
                //txtTotalPremium.Text = Math.Round(CalculateDiscount(decimal.Parse(oSheet.Range["G48"].Value.ToString())), 0).ToString("$##,###,###.00");
                //txtTotalPremium.Text = (double.Parse(ClearString(txtPremium1.Text)) + double.Parse(ClearString(txtPremium2.Text)) + double.Parse(ClearString(txtPremium3.Text)) + double.Parse(ClearString(txtPremium4.Text))).ToString("$##,###,###.00");
                //txtTotalLimit.Text = Math.Round(decimal.Parse(oSheet.Range["B48"].Value.ToString()), 0).ToString("$##,###,###.00");
                txtTotalLimit.Text = Math.Round(decimal.Parse(oSheet.Range["B50"].Value.ToString()), 0).ToString("$##,###,###.00");

                txtLiaPropertyType.Text = ddlPropertyType.SelectedItem.Value;
                txtLiaNumOfFamilies.Text = ddlNumOfFamilies.SelectedItem.Value;
                //txtLiaMedicalPayments.Text = oSheet.Range["F53"].Value.ToString().Replace("$", "").Replace(",", "").Trim() != "" ? Math.Round(decimal.Parse(oSheet.Range["F53"].Value.ToString().Replace("$", "").Replace(",", "")), 0).ToString("$##,###,###.00") : "";
                txtLiaMedicalPayments.Text = oSheet.Range["F55"].Value.ToString().Replace("$", "").Replace(",", "").Trim() != "" ? Math.Round(decimal.Parse(oSheet.Range["F55"].Value.ToString().Replace("$", "").Replace(",", "")), 0).ToString("$##,###,###.00") : "";

               // if (oSheet.Range["G58"].Value.ToString() != "NO COVERAGE")
                if (oSheet.Range["G60"].Value.ToString() != "NO COVERAGE")
                {
                    //if (oSheet.Range["G53"].Value.ToString() == "NO COVERAGE")
                    if (oSheet.Range["G55"].Value.ToString() == "NO COVERAGE")
                    {
                        txtPremium.Text = "NO COVERAGE";
                    }
                    else
                    {
                        //if (oSheet.Range["G53"].Value.ToString().ToString().Trim() != "")
                        if (oSheet.Range["G55"].Value.ToString().ToString().Trim() != "")
                        {
                            //txtLiaPremium2.Text = Math.Round(decimal.Parse(oSheet.Range["G53"].Value.ToString()), 0).ToString("$##,###,###.00");
                            txtLiaPremium.Text = Math.Round(decimal.Parse(oSheet.Range["G55"].Value.ToString()), 0).ToString("$##,###,###.00");
                        }
                        else
                        {
                            txtLiaPremium.Text = "$0.00";
                        }

                        //if (oSheet.Range["G58"].Value.ToString().ToString().Trim() != "")
                        if (oSheet.Range["G60"].Value.ToString().ToString().Trim() != "")
                        {
                            txtPremium.Text = Math.Round(decimal.Parse(oSheet.Range["G60"].Value.ToString()), 0).ToString("$##,###,###.00");
                        }
                        else
                        {
                            txtPremium.Text = "$0.00";
                        }

                    }


                }
                else
                {
                    txtLiaPremium.Text = "NO COVERAGE";

                }

                //if (oSheet.Range["D48"].Value.ToString() != "NO COVERAGE")
                if (oSheet.Range["D50"].Value.ToString() != "NO COVERAGE")
                {
                    //txtTotalWind.Text = Math.Round(decimal.Parse(oSheet.Range["D48"].Value.ToString()), 2).ToString("$##,###,###.00");
                    txtTotalWind.Text = Math.Round(decimal.Parse(oSheet.Range["D50"].Value.ToString()), 2).ToString("$##,###,###.00");
                   
                }
                else
                {
                    txtTotalWind.Text = "NO COVERAGE";
                }

                //if (oSheet.Range["E48"].Value.ToString() != "NO COVERAGE")
                if (oSheet.Range["E50"].Value.ToString() != "NO COVERAGE")
                {
                    //txtTotalEarth.Text = Math.Round(decimal.Parse(oSheet.Range["E48"].Value.ToString()), 2).ToString("$##,###,###.00");
                    txtTotalEarth.Text = Math.Round(decimal.Parse(oSheet.Range["E50"].Value.ToString()), 2).ToString("$##,###,###.00");
                }
                else
                {
                    txtTotalEarth.Text = "NO COVERAGE";
                }

                //txtLiaTotalPremium.Text = Math.Round(decimal.Parse(oSheet.Range["G60"].Value.ToString()), 0).ToString("$##,###,###.00");
                txtLiaTotalPremium.Text = (double.Parse(ClearString(txtTotalPremium.Text)) + double.Parse(ClearString(txtPremium.Text))).ToString("$##,###,###.00");

                double GrossTax = 0, LiaTotalPremium = 0;
                double.TryParse(txtLiaTotalPremium.Text,
                    System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol,
                    System.Globalization.CultureInfo.CreateSpecificCulture("en-US"),
                    out LiaTotalPremium);

                txtGrossTax.Text =
                   LiaTotalPremium > 0.0 ?
                   (Math.Round(CalculateAgentCommision(ddlAgency.SelectedItem.Value, LiaTotalPremium), 2)).ToString()
                   : "0";

                double.TryParse(txtGrossTax.Text,
                    System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol,
                    System.Globalization.CultureInfo.CreateSpecificCulture("en-US"),
                    out GrossTax);

                if (GrossTax > 0)
                {
                    txtLiaTotalPremium.Text = (LiaTotalPremium + GrossTax).ToString("$##,###,###.00");
                    txtGrossTax.Text = (GrossTax).ToString("$##,###,###.00");
                }

                //txtCatastropheCoverage.Text = oSheet.Range["B12"].Value.ToString();
                //txtPropertyForm.Text = oSheet.Range["E19"].Value.ToString();
                //txtPolicyType.Text = oSheet.Range["E20"].Value.ToString().Replace("HO", "HOM");

                txtCatastropheCoverage.Text = oSheet.Range["B14"].Value.ToString();
                txtPropertyForm.Text = oSheet.Range["E21"].Value.ToString();
                txtPolicyType.Text = oSheet.Range["E22"].Value.ToString().Replace("HO", "HOM");

                txtLiaPolicyType.Text = txtPolicyType.Text;

                //if (oSheet.Range["E19"].Value.ToString() == "INC")
                //if (oSheet.Range["E21"].Value.ToString() == "INC")
                //{
                //if (oSheet.Range["G53"].Value.ToString() == "NO COVERAGE")
                //if (oSheet.Range["G55"].Value.ToString().Trim() == "NO COVERAGE" || oSheet.Range["G55"].Value.ToString().Trim() == "0" || oSheet.Range["G55"].Value.ToString().Trim() == "0.0000")
                //    {
                //        ddlLiaLimit.Items[0].Text = "NO COVERAGE";
                //        ddlLiaLimit.SelectedIndex = 0;
                //        txtLiaMedicalPayments.Text = "NO COVERAGE";
                //        txtLiaPremium.Text = "NO COVERAGE";
                //    }

                if (oSheet.Range["E55"].Value.ToString().Trim() == "NO COVERAGE" || oSheet.Range["E55"].Value.ToString().Trim() == "0" || oSheet.Range["E55"].Value.ToString().Trim() == "0.0000")
                {
                    ddlLiaLimit.Items[0].Text = "NO COVERAGE";
                    ddlLiaLimit.SelectedIndex = 0;
                    txtLiaMedicalPayments.Text = "NO COVERAGE";
                    txtLiaPremium.Text = "NO COVERAGE";
                }

                string PolicyType = "";

                //PolicyType = oSheet.Range["E19"].Value.ToString();
                PolicyType = oSheet.Range["E21"].Value.ToString();

                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oBook.Worksheets[4];
                System.Data.DataTable dtReinsAsl = new System.Data.DataTable();


                #region CoverageCodes

                string Lim1 = "", Lim2 = "", Lim3 = "", Lim4 = "", Lim5 = "";
                double EarthQuakePerc = 0, WindPerc = 0;
                //Lim1 Propiedad
                //Lim2 Pers Property
                //Lim3 Loss of USE
                //Lim4 Deducibles & (Liability/Medical Limit)
                //Lim5 OtherStructures

                Lim1 = txtLimit1.Text.ToString().Replace("$", "").Replace(",", "");
                Lim2 = txtLimit3.Text.ToString().Replace("$", "").Replace(",", "");
                Lim3 = txtLimit4.Text.ToString().Replace("$", "").Replace(",", "");

                if (ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "") == "NO COVERAGE")
                     Lim4 = "0.00";
                else
                    Lim4 = ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "");
               
                Lim5 = txtLimit2.Text.ToString().Replace("$", "").Replace(",", "");


                if (txtEarthQuakeDedPer1.Text.Replace("%", "").ToString() != "0")
                    EarthQuakePerc = (txtEarthQuakeDedPer1.Text.Replace("%", "").ToString() == "" || txtEarthQuakeDedPer1.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtEarthQuakeDedPer1.Text.Replace("%", "")) / 100;
                else if (txtEarthQuakeDedPer2.Text.Replace("%", "").ToString() != "0")
                    EarthQuakePerc = (txtEarthQuakeDedPer2.Text.Replace("%", "").ToString() == "" || txtEarthQuakeDedPer2.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtEarthQuakeDedPer2.Text.Replace("%", "")) / 100;
                else if (txtEarthQuakeDedPer3.Text.Replace("%", "").ToString() != "0")
                    EarthQuakePerc = (txtEarthQuakeDedPer3.Text.Replace("%", "").ToString() == "" || txtEarthQuakeDedPer3.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtEarthQuakeDedPer3.Text.Replace("%", "")) / 100;
                else if (txtEarthQuakeDedPer4.Text.Replace("%", "").ToString() != "0")
                    EarthQuakePerc = (txtEarthQuakeDedPer4.Text.Replace("%", "").ToString() == "" || txtEarthQuakeDedPer4.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtEarthQuakeDedPer4.Text.Replace("%", "")) / 100;


                if (txtWindstormDedPer1.Text.Replace("%", "").ToString() != "0")
                    WindPerc = (txtWindstormDedPer1.Text.Replace("%", "").ToString() == "" || txtWindstormDedPer1.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtWindstormDedPer1.Text.Replace("%", "")) / 100;
                else if (txtWindstormDedPer2.Text.Replace("%", "").ToString() != "0")
                    WindPerc = (txtWindstormDedPer2.Text.Replace("%", "").ToString() == "" || txtWindstormDedPer2.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtWindstormDedPer2.Text.Replace("%", "")) / 100;
                else if (txtWindstormDedPer3.Text.Replace("%", "").ToString() != "0")
                    WindPerc = (txtWindstormDedPer3.Text.Replace("%", "").ToString() == "" || txtWindstormDedPer3.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtWindstormDedPer3.Text.Replace("%", "")) / 100;
                else if (txtWindstormDedPer4.Text.Replace("%", "").ToString() != "0")
                    WindPerc = (txtWindstormDedPer4.Text.Replace("%", "").ToString() == "" || txtWindstormDedPer4.Text.Replace("%", "").ToString() == "NO COVERAGE")
                        ? 0 : double.Parse(txtWindstormDedPer4.Text.Replace("%", "")) / 100;

                dtReinsAsl = new System.Data.DataTable();
                dtReinsAsl.TableName = "Coverages";
                dtReinsAsl.Columns.Add(oSheet.Range["A206"].Value.ToString());
                dtReinsAsl.Columns.Add(oSheet.Range["B206"].Value.ToString());
                dtReinsAsl.Columns.Add(oSheet.Range["C206"].Value.ToString());
                dtReinsAsl.Columns.Add("Total");
                dtReinsAsl.Columns.Add("Lim1");
                dtReinsAsl.Columns.Add("Lim2");
                dtReinsAsl.Columns.Add("Lim3");
                dtReinsAsl.Columns.Add("Lim4");
                dtReinsAsl.Columns.Add("Lim5");
                dtReinsAsl.Columns.Add("Deductible");
                dtReinsAsl.Columns.Add("MinDeductible");


                string WindDeductible = "0", EQDeductible = "0";

                if (txtWindstormDedPer1.Text.ToString().Trim() != "NO COVERAGE")
                {
                    WindDeductible = txtWindstormDedPer1.Text.ToString().Trim().Replace("%", "");
                    WindDeductible = WindDeductible != "" ? (double.Parse(WindDeductible) / 100).ToString() : "0";
                }

                if (txtEarthQuakeDedPer1.Text.ToString().Trim() != "NO COVERAGE")
                {
                    EQDeductible = txtEarthQuakeDedPer1.Text.ToString().Trim().Replace("%", "");
                    EQDeductible = EQDeductible != "" ? (double.Parse(EQDeductible) / 100).ToString() : "0";
                }


                // se alcula para validar si las primas de las cubiertas cuadran con el total premium, y si no se austara.
                string prem1 = "0.00";
                string prem2 = "0.00";
                string prem3 = "0.00";
                string prem4 = "0.00";
                string prem5 = "0.00";
                string prem6 = "0.00";
                string totprem6FromSheet = oSheet.Range["C213"].Value.ToString().Trim();
                double TotPremtemp = 0.0;
                double difPremTemp = 0.0;

                if (PolicyType != "INC" && oSheet.Range["A207"].Value.ToString().Trim() != "0")
                    prem1 = oSheet.Range["C207"].Value.ToString().Trim();

                if (oSheet.Range["A208"].Value.ToString().Trim() != "0")
                    prem2 = oSheet.Range["C208"].Value.ToString().Trim();

                if (oSheet.Range["A209"].Value.ToString().Trim() != "0")
                    prem3 = oSheet.Range["C209"].Value.ToString().Trim();

                if (oSheet.Range["A210"].Value.ToString().Trim() != "0")
                    prem4 = oSheet.Range["C210"].Value.ToString().Trim();

                if (oSheet.Range["A211"].Value.ToString().Trim() != "0")
                    prem5 = oSheet.Range["C211"].Value.ToString().Trim();

                if (oSheet.Range["A212"].Value.ToString().Trim() != "0")
                    prem6 = oSheet.Range["C212"].Value.ToString().Trim();


                TotPremtemp = double.Parse(prem1) + double.Parse(prem2) + double.Parse(prem3) + double.Parse(prem4) + double.Parse(prem5) + double.Parse(prem6);

                if (TotPremtemp != double.Parse(totprem6FromSheet))
                    difPremTemp = double.Parse(totprem6FromSheet) - TotPremtemp;

                //Termina la validacion si las primas de las cubiertas cuadran con el total premium, y si no se austara.

                //Personal Liability
                DataRow row = dtReinsAsl.NewRow();

                //HOM(33040)
                if (PolicyType != "INC" && oSheet.Range["A207"].Value.ToString().Trim() != "0")
                {
                    row[0] = oSheet.Range["A207"].Value.ToString();
                    row[1] = oSheet.Range["B207"].Value.ToString();
                    row[2] = oSheet.Range["C207"].Value.ToString();
                    row[3] = oSheet.Range["C213"].Value.ToString();

                    row[4] = "0";//Lim1;
                    row[5] = "0";//Lim2;
                    row[6] = "0";//Lim3;

                    if (ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "") == "NO COVERAGE")
                         row[7] = "0.00";
                    else
                        row[7] = ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "");

                   
                    row[8] = "0";//Lim5;
                    row[9] = "0";//Deductible;
                    row[10] = "0";//MinDeductible;
                    dtReinsAsl.Rows.Add(row);
                    row = dtReinsAsl.NewRow();
                }

                Lim4 = "1000";//ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "");


                //HOM(31260)
                if (oSheet.Range["A208"].Value.ToString().Trim() != "0")
                {
                    //Theft
                    row[0] = oSheet.Range["A208"].Value.ToString();
                    row[1] = oSheet.Range["B208"].Value.ToString();
                    row[2] = oSheet.Range["C208"].Value.ToString();
                    row[3] = oSheet.Range["C213"].Value.ToString();

                    row[4] = "0";//Lim1;
                    row[5] = Lim2;
                    row[6] = "0";//Lim3;
                    row[7] = Lim4;
                    row[8] = Lim5;//Lim5;
                    row[9] = "1000";//Deductible;
                    row[10] = "1000";//MinDeductible;
                    dtReinsAsl.Rows.Add(row);
                }

                //HOM(30120) OR INC(17120) 
                if (oSheet.Range["A209"].Value.ToString().Trim() != "0")
                {
                    //Earthquake                

                    string primaEarthquake = (double.Parse(prem3) + difPremTemp).ToString();
                    row = dtReinsAsl.NewRow();
                    row[0] = oSheet.Range["A209"].Value.ToString();
                    row[1] = oSheet.Range["B209"].Value.ToString();
                    row[2] = primaEarthquake; // oSheet.Range["C209"].Value.ToString();
                    row[3] = oSheet.Range["C213"].Value.ToString();

                    row[4] = Lim1;
                    row[5] = Lim2;
                    row[6] = Lim3;
                    row[7] = EarthQuakePerc.ToString();//Lim4;
                    row[8] = Lim5;//"0";//Lim5;
                    row[9] = EQDeductible;//Deductible;
                    row[10] = "2500";//MinDeductible;
                    dtReinsAsl.Rows.Add(row);
                }

                //HOM(29040) OR INC(16021) 
                if (oSheet.Range["A210"].Value.ToString().Trim() != "0")
                {
                    //Winds Perils
                    row = dtReinsAsl.NewRow();
                    row[0] = oSheet.Range["A210"].Value.ToString();
                    row[1] = oSheet.Range["B210"].Value.ToString();
                    row[2] = oSheet.Range["C210"].Value.ToString();
                    row[3] = oSheet.Range["C213"].Value.ToString();

                    row[4] = Lim1;
                    row[5] = Lim2;
                    row[6] = Lim3;
                    row[7] = WindPerc.ToString();//Lim4;
                    row[8] = Lim5;//Lim5;
                    row[9] = WindDeductible;//Deductible;
                    row[10] = "2500";//MinDeductible;
                    dtReinsAsl.Rows.Add(row);
                }

                if (oSheet.Range["A211"].Value.ToString().Trim() != "0")
                {
                    //Fire, Extended Coverage and Vandalism and Malicious Mischief 
                    row = dtReinsAsl.NewRow();
                    row[0] = oSheet.Range["A211"].Value.ToString();
                    row[1] = oSheet.Range["B211"].Value.ToString();
                    row[2] = oSheet.Range["C211"].Value.ToString();
                    row[3] = oSheet.Range["C213"].Value.ToString();

                    row[4] = Lim1;
                    row[5] = Lim2;
                    row[6] = Lim3;
                    row[7] = txtAOPDed1.Text.ToString().Replace("$", "").Replace(",", "");
                    row[8] = Lim5;//"0";//Lim5;
                    row[9] = "1000";//Deductible;
                    row[10] = "1000";//MinDeductible;
                    dtReinsAsl.Rows.Add(row);
                }

                //INC(15021)
                if (PolicyType == "INC" && oSheet.Range["A212"].Value.ToString().Trim() != "0")
                {
                    //Extended Coverage
                    row = dtReinsAsl.NewRow();
                    row[0] = oSheet.Range["A212"].Value.ToString();
                    row[1] = oSheet.Range["B212"].Value.ToString();
                    row[2] = oSheet.Range["C212"].Value.ToString();
                    row[3] = oSheet.Range["C213"].Value.ToString();

                    row[4] = Lim1;
                    row[5] = Lim2;
                    row[6] = Lim3;
                    row[7] = Lim4;
                    row[8] = Lim5;
                    row[9] = "1000";//Deductible;
                    row[10] = "1000";//MinDeductible;
                    dtReinsAsl.Rows.Add(row);
                }

                if (PolicyType != "INC")
                {

                    if (txtLiaMedicalPayments.Text.ToString().Replace("$", "").Replace(",", "") == "NO COVERAGE")
                        Lim4 = "0.00";
                    else
                        Lim4 = txtLiaMedicalPayments.Text.ToString().Trim().Replace("$", "").Replace(",", "");

                    //Medical Payments to Others
                    row = dtReinsAsl.NewRow();
                    row[0] = "36040";
                    row[1] = "Medical Payments to Others";
                    row[2] = "0";
                    row[3] = oSheet.Range["C213"].Value.ToString();

                    row[4] = "0";//Lim1;
                    row[5] = "0";//Lim2;
                    row[6] = "0";//Lim3;
                    row[7] = txtAOPDed1.Text.ToString().Replace("$", "").Replace(",", "");
                    row[8] = "0";//Lim5;
                    row[9] = "0";//Deductible;
                    row[10] = "0";//MinDeductible;
                    dtReinsAsl.Rows.Add(row);
                }
                Session["dtReinsAsl"] = dtReinsAsl.Copy(); 

                #endregion CoverageCodes

                oBook.Save();
                //oBook.Close(0);
                //oExcel.Quit();

            }
            catch (Exception exc)
            {
                throw new Exception("Error calculating. Please verify that the limits are correctly written " + exc.ToString() + exc.Message.ToString(), exc);
            }
            finally
            {
                if (oBook != null)
                    oBook.Close(0);
                if (oExcel != null)
                    oExcel.Quit();

                if (oExcel != null)
                    Marshal.FinalReleaseComObject(oExcel);
                if (oBook != null)
                    Marshal.FinalReleaseComObject(oBook);
                if (oSheet != null)
                    Marshal.FinalReleaseComObject(oSheet);
            }
        }

        protected void chkMailSame_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMailSame.Checked)
            {
                txtPhysicalAddress1.Text = txtMailingAddress.Text.ToUpper().Trim();
            }
        }

        protected void chkBank_CheckedChange(object sender, EventArgs e)
        {
            if (CheckBank.Checked == true)
            {
                lblBank.ForeColor = System.Drawing.Color.Red;
                lblLoanNo.ForeColor = System.Drawing.Color.Red;
                lblTypeOfInterest.ForeColor = System.Drawing.Color.Red;

                btnBankList.Enabled = true;
                txtBank.Enabled = true;
                txtLoanNo.Enabled = true;
                txtBank2.Enabled = true;
                txtLoanNo2.Enabled = true;
                ddlTypeOfInterest.Enabled = true;

            }
            else
            {
                lblBank.ForeColor = System.Drawing.Color.Gray;
                lblLoanNo.ForeColor = System.Drawing.Color.Gray;
                lblTypeOfInterest.ForeColor = System.Drawing.Color.Gray;

                btnBankList.Enabled = false;
                txtBank.Enabled = false;
                txtLoanNo.Enabled = false;
                txtBank2.Enabled = false;
                txtLoanNo2.Enabled = false;
                ddlTypeOfInterest.Enabled = false;
                ddlBankList.SelectedIndex = -1;
                ddlBankList2.SelectedIndex = -1;
                //ddlBankList3.SelectedIndex = -1;
                //ddlBankList4.SelectedIndex = -1;
                txtBank.Text = "";
                txtBank2.Text = "";
                txtLoanNo.Text = "";
                txtLoanNo2.Text = "";
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            Recalculate();
        }

        protected void ModifyClick()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            taskControl.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.UPDATE;

            Session.Add("TaskControl", taskControl);

            if (taskControl.isQuote)
            {
                DateTime start = taskControl.EntryDate;
                DateTime end = DateTime.Now;
                TimeSpan difference = end - start;
                if (difference.Days > 15)
                    throw new Exception(String.Concat(
                    @"<p>This quote has expired and is not valid after 15 days after issued.</p>",
                  @"<p>If you want, use the <b>Renewal Clone Quote</b> button for obtain a valid new quote with the same information.</p>"));
            }

            EnableControls();
            FillTextControl();
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

        private static System.Data.DataTable GetAgency()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[0];

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
                dt = exec.GetQuery("GetAgencyHO", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data from database.", ex);
            }

        }

        protected bool validateCustomerInfo()
        {
            if (txtFirstName.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill First Name";
                mpeSeleccion.Show();
                return false;
            }
            if (txtLastName.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Last Name";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtMailingAddress.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Mailing Address";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtCity.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill City";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtState.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill State";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtZipCode.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please Fill Zip Code";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtOccupation.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please Fill Occupation";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtPhysicalAddress1.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Property Address";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtCity2.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Property City";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtState2.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Property State";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtZipCode2.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Property Zip Code";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtBusinessPhone.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Business Phone";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtMobilePhone.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Mobile Phone";
                mpeSeleccion.Show();
                return false;
            }
            else if (ddlBankList.SelectedItem.Text.ToString().Trim() != "")
            {
                if (txtLoanNo.Text.ToString().Trim() == "")
                {
                    lblRecHeader.Text = "Please fill Loan Number ";
                    mpeSeleccion.Show();
                    return false;
                }
            }
            else if (ddlBankList2.SelectedItem.Text.ToString().Trim() != "")
            {
                if (txtLoanNo2.Text.ToString().Trim() == "")
                {
                    lblRecHeader.Text = "Please fill Loan Number 2 ";
                    mpeSeleccion.Show();
                    return false;
                }
            }
            else if (CheckBank.Checked)
            {
                if (ddlBankList.SelectedItem.Text.ToString().Trim() == "")
                {
                    lblRecHeader.Text = "Please select a Bank";
                    mpeSeleccion.Show();
                    return false;
                }

                if (ddlTypeOfInterest.SelectedItem.Value == "")
                {
                    lblRecHeader.Text = "Please fill Type of Interest";
                    mpeSeleccion.Show();
                    return false;
                }
            }
            else if (ddlMortgageeBilled.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Mortgagee Billed";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtEmail.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Email";
                mpeSeleccion.Show();
                return false;
            }

            return true;

        }

        protected bool validateGeneralInfo()
        {
            if (ddlCatastropheDeduc.SelectedItem.Value.Trim() == "")
            {
                lblRecHeader.Text = "Please fill Catastrophe Deductible";
                mpeSeleccion.Show();
                return false;
            }

            else if (!rdbNo.Checked && !rdbYes.Checked)
            {
                lblRecHeader.Text = "Please fill if any ongoing construction and/or upgrades being made curently or in the previous previous year";
                mpeSeleccion.Show();
                return false;
            }

            else if (!rdbNoStruct.Checked && !rdbYesStruct.Checked)
            {
                lblRecHeader.Text = "Please fill of there are additional structures in the property";
                mpeSeleccion.Show();
                return false;
            }

            else if (ddlRoofOverhang.SelectedItem.Value.Trim() == "")
            {
                lblRecHeader.Text = "Please fill Roof Overhang";
                mpeSeleccion.Show();
                return false;
            }

            else if (ddlConstructionType.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Construction Type";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtConstructionYear.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Construction Year";
                mpeSeleccion.Show();
                return false;
            }
            else if (ddlNumberOfStories.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Number of Stories";
                mpeSeleccion.Show();
                return false;
            }
            else if (ddlNumOfFamilies.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Number of Families";
                mpeSeleccion.Show();
                return false;
            }
            else if (rdbYes.Checked)
            {
                if (txtIfYes.Text.ToString().Trim() == "")
                {
                    lblRecHeader.Text = "Please explain the ongoing construction and/or upgrades being made curently or in the previous previous year";
                    mpeSeleccion.Show();
                    return false;
                }

            }

            else if (txtLivingArea.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Living Area";
                mpeSeleccion.Show();
                return false;
            }
            else if (txtPorches.Text.ToString().Trim() == "")
            {
                lblRecHeader.Text = "Please fill Porches";
                mpeSeleccion.Show();
                return false;
            }
            else if (rdbYesStruct.Checked)
            {
                if (txtOtherStruct.Text.ToString().Trim() == "")
                {
                    lblRecHeader.Text = "Please fill Other Structure Types";
                    mpeSeleccion.Show();
                    return false;
                }
                else
                {
                    return true;
                }
            }

            else if (ddlRoofDwelling.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Roof of Dwelling";
                mpeSeleccion.Show();
                return false;
            }

            else if (ddlResidence.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Residence";
                mpeSeleccion.Show();
                return false;
            }

            else if (ddlPropertyType.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Property Type";
                mpeSeleccion.Show();
                return false;
            }
            else if (ddlIsland.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please select Island";
                mpeSeleccion.Show();
                return false;
            }

            return true;
        }

        protected bool validatePropertyInfo()
        {
            //FormatCurrency(txtLimit1);
            //FormatCurrency(txtLimit2);
            //FormatCurrency(txtLimit3);
            //FormatCurrency(txtLimit4);
            txtLimit1.Text = FormatCurrency(txtLimit1.Text);
            txtLimit2.Text = FormatCurrency(txtLimit2.Text);
            txtLimit3.Text = FormatCurrency(txtLimit3.Text);
            txtLimit4.Text = FormatCurrency(txtLimit4.Text);

            if (Double.Parse(txtLimit1.Text.ToString().Trim().Replace("$", "").Replace(",", "")) < 1 && Double.Parse(txtLimit2.Text.ToString().Trim().Replace("$", "").Replace(",", "")) < 1 && Double.Parse(txtLimit3.Text.ToString().Trim().Replace("$", "").Replace(",", "")) < 1 && Double.Parse(txtLimit4.Text.ToString().Trim().Replace("$", "").Replace(",", "")) < 1)
            {
                lblRecHeader.Text = "Please fill Limits";
                mpeSeleccion.Show();
                return false;
            }
            else
                return true;
        }

        protected bool validateTypeOfInsured()
        {
            if (ddlTypeOfInsured.SelectedItem.Text.Trim() == "")
            {
                lblRecHeader.Text = "Please select Type of Insured";
                mpeSeleccion.Show();
                return false;
            }
            else{
                return true;
            
            }
        }

        protected bool validateAlertDate()
        {
            if (txtAlertEntryDate.Text.Trim() == "") 
            {
                lblRecHeader.Text = "Please, enter a date for the";
                mpeSeleccion.Show();
                return false;
            }
            else
            {
                return true;

            }
        }

        protected bool validateLiabilityInfo()
        {
            if (txtPolicyType.Text != "INC" || ddlPropertyType.SelectedItem.Text != "Dwelling - Rented to Others")
            {
                if (ddlLiaLimit.SelectedItem.Value == "NO COVERAGE")
                    ddlLiaLimit.Items[0].Text = "$0.00";

                if (Double.Parse(ddlLiaLimit.SelectedItem.Value.Replace("$", "").Replace(".", "").Replace(",", "")) < 1)
                {
                    lblRecHeader.Text = "Please fill Liability Limit";
                    mpeSeleccion.Show();
                    return false;
                }
                else
                    return true;
            }
            else
                return true;
        }

        protected bool validatePolicyInfo()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            if (ddlOffice.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Office";
                mpeSeleccion.Show();
                return false;
            }
            else if (ddlAgency.SelectedItem.Value == "")
            {
                lblRecHeader.Text = "Please fill Agency";
                mpeSeleccion.Show();
                return false;
            }
            else if (taskControl.isRenew && txtPolicyNoToRenew.Text.Trim() == "" && txtPreviousPolicyNo.Text.Trim() == "")
            {
                lblRecHeader.Text = "Policy number to renew is missing.";
                mpeSeleccion.Show();
                return false;
            }
            
            else{
                DivRenew1.Visible = false;
                DivRenew2.Visible = false;
                DivRenew3.Visible = false;
                return true;
            }
                

        }

        protected void VerifyPolicyExist()
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                System.Data.DataTable dt = null;
                dt = GetVerifyPolicyExist(taskControl.TaskControlID);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        throw new Exception("This application has already been converted to Policy.");
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("This application has already been converted to Policy.");
            }
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {

                EPolicy.TaskControl.HomeOwners taskControlQuote = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                System.Data.DataTable dt = null;
                System.Data.DataTable dt2 = null;
                dt = GetVerifyPolicyExist(taskControlQuote.TaskControlID);
                dt2 = GetHomeOwnersAdditionalInsuredQuoteByTaskControlID(taskControlQuote.TaskControlID);
                if (dt != null)
                {
                    //If this quote has been converted to policy (Read Only)
                    if (dt.Rows.Count > 0)
                    {
                        throw new Exception("This quote has already been converted to Policy.");
                        return;
                    }
                }

                taskControlQuote.approved = true;
                Session.Clear();
                Session["TaskControlOLD"] = taskControlQuote;
                Session["TaskControlIDOLD"] = taskControlQuote.TaskControlID;
                EPolicy.TaskControl.HomeOwners taskControl = new EPolicy.TaskControl.HomeOwners(false);


                taskControl.Mode = 1; //ADD
                taskControl.isQuote = false;
                taskControl.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Home Owners Policy"));
                taskControl.InsuranceCompany = taskControl.InsuranceCompany;
                taskControl.Agency = taskControlQuote.Agency;
                taskControl.Agent = taskControlQuote.Agent;
                taskControl.AgentDesc = taskControlQuote.AgentDesc;
                taskControl.OriginatedAt = taskControlQuote.OriginatedAt;
                taskControl.office = taskControlQuote.office;
                taskControl.DepartmentID = 1;
                taskControl.EntryDate = DateTime.Now;//taskControlQuote.EntryDate;
                taskControl.PreviousPolicy = taskControlQuote.PreviousPolicy;
                taskControl.isRenew = taskControlQuote.isRenew;

                taskControl.CustomerNo = taskControlQuote.CustomerNo;
                taskControl.Customer.CustomerNo = taskControlQuote.Customer.CustomerNo;
                taskControl.Customer.FirstName = taskControlQuote.Customer.FirstName;
                taskControl.Customer.LastName1 = taskControlQuote.Customer.LastName1;
                taskControl.Customer.LastName2 = taskControlQuote.Customer.LastName2;

                taskControl.Customer.AddressPhysical1 = taskControlQuote.Customer.AddressPhysical1;
                taskControl.Customer.AddressPhysical2 = taskControlQuote.Customer.AddressPhysical2;
                taskControl.Customer.CityPhysical = taskControlQuote.Customer.CityPhysical;
                taskControl.Customer.StatePhysical = taskControlQuote.Customer.StatePhysical;
                taskControl.Customer.ZipPhysical = taskControlQuote.Customer.ZipPhysical;
                taskControl.Customer.Address1 = taskControlQuote.Customer.Address1;
                taskControl.Customer.Address2 = taskControlQuote.Customer.Address2;
                taskControl.Customer.City = taskControlQuote.Customer.City;
                taskControl.Customer.State = taskControlQuote.Customer.State;
                taskControl.Customer.ZipCode = taskControlQuote.Customer.ZipCode;
                taskControl.Customer.LocationID = taskControlQuote.Customer.LocationID;
                taskControl.Customer.Description = taskControlQuote.Customer.Description;

                taskControl.TaskStatusID = 31;

                taskControl.bank = taskControlQuote.bank;
                taskControl.bank2 = taskControlQuote.bank2;
                taskControl.loanNo = taskControlQuote.loanNo;
                taskControl.loanNo2 = taskControlQuote.loanNo2;
                taskControl.typeOfInterest = taskControlQuote.typeOfInterest;

                taskControl.mortgageeBilled = taskControlQuote.mortgageeBilled;
                if (taskControl.isRenew)
                {
                    taskControl.EffectiveDate = taskControlQuote.EffectiveDate;
                    taskControl.ExpirationDate = taskControlQuote.ExpirationDate;
                }
                else
                {
                    taskControl.EffectiveDate = System.DateTime.Today.ToShortDateString();//taskControlQuote.EffectiveDate;
                    taskControl.ExpirationDate = System.DateTime.Today.AddYears(1).ToShortDateString();//taskControlQuote.ExpirationDate;
                }
                taskControl.catastropheCov = taskControlQuote.catastropheCov;
                taskControl.catastropheDeduc = taskControlQuote.catastropheDeduc;
                taskControl.windstormDeduc = taskControlQuote.windstormDeduc;
                taskControl.allOtherPerDeduc = taskControlQuote.allOtherPerDeduc;
                taskControl.constructionType = taskControlQuote.constructionType;
                taskControl.constructionYear = taskControlQuote.constructionYear;
                taskControl.numOfStories = taskControlQuote.numOfStories;
                taskControl.numOfFamilies = taskControlQuote.numOfFamilies;
                taskControl.ifYes = taskControlQuote.ifYes;
                taskControl.livingArea = taskControlQuote.livingArea;
                taskControl.porshcesDeck = taskControlQuote.porshcesDeck;
                taskControl.roofDwelling = taskControlQuote.roofDwelling;
                taskControl.earthquakeDeduc = taskControlQuote.earthquakeDeduc;
                taskControl.residence = taskControlQuote.residence;
                taskControl.propertyType = taskControlQuote.propertyType;
                taskControl.propertyForm = taskControlQuote.propertyForm;
                taskControl.PolicyType = taskControlQuote.PolicyType;
                taskControl.losses3Year = taskControlQuote.losses3Year;
                taskControl.otherStructuresType = taskControlQuote.otherStructuresType;
                taskControl.isPropShuttered = taskControlQuote.isPropShuttered;
                taskControl.roofOverhang = taskControlQuote.roofOverhang;
                taskControl.autoPolicy = taskControlQuote.autoPolicy;

                //taskControl.autoPolicyNo = taskControlQuote.autoPolicyNo;

                taskControl.limit1 = taskControlQuote.limit1;
                taskControl.limit2 = taskControlQuote.limit2;
                taskControl.limit3 = taskControlQuote.limit3;
                taskControl.limit4 = taskControlQuote.limit4;

                taskControl.aopDed1 = taskControlQuote.aopDed1;
                taskControl.aopDed2 = taskControlQuote.aopDed2;
                taskControl.aopDed3 = taskControlQuote.aopDed3;
                taskControl.aopDed4 = taskControlQuote.aopDed4;

                taskControl.windstormDed1 = taskControlQuote.windstormDed1;
                taskControl.windstormDed2 = taskControlQuote.windstormDed2;
                taskControl.windstormDed3 = taskControlQuote.windstormDed3;
                taskControl.windstormDed4 = taskControlQuote.windstormDed4;

                taskControl.windstormDedPer1 = taskControlQuote.windstormDedPer1;
                taskControl.windstormDedPer2 = taskControlQuote.windstormDedPer2;
                taskControl.windstormDedPer3 = taskControlQuote.windstormDedPer3;
                taskControl.windstormDedPer4 = taskControlQuote.windstormDedPer4;

                taskControl.earthquakeDed1 = taskControlQuote.earthquakeDed1;
                taskControl.earthquakeDed2 = taskControlQuote.earthquakeDed2;
                taskControl.earthquakeDed3 = taskControlQuote.earthquakeDed3;
                taskControl.earthquakeDed4 = taskControlQuote.earthquakeDed4;

                taskControl.earthquakeDedper1 = taskControlQuote.earthquakeDedper1;
                taskControl.earthquakeDedper2 = taskControlQuote.earthquakeDedper2;
                taskControl.earthquakeDedper3 = taskControlQuote.earthquakeDedper3;
                taskControl.earthquakeDedper4 = taskControlQuote.earthquakeDedper4;

                taskControl.coinsurance1 = taskControlQuote.coinsurance1;
                taskControl.coinsurance2 = taskControlQuote.coinsurance2;
                taskControl.coinsurance3 = taskControlQuote.coinsurance3;
                taskControl.coinsurance4 = taskControlQuote.coinsurance4;

                taskControl.premium1 = taskControlQuote.premium1;
                taskControl.premium2 = taskControlQuote.premium2;
                taskControl.premium3 = taskControlQuote.premium3;
                taskControl.premium4 = taskControlQuote.premium4;

                taskControl.totalLimit = taskControlQuote.totalLimit;
                taskControl.totalWindstorm = taskControlQuote.totalWindstorm;
                taskControl.totalEarthquake = taskControlQuote.totalEarthquake;
                taskControl.totalPremium = taskControlQuote.totalPremium;
                taskControl.TotalPremium = taskControlQuote.liaTotalPremium;

                taskControl.liaPropertyType = taskControlQuote.liaPropertyType;
                taskControl.liaPolicyType = taskControlQuote.liaPolicyType;
                taskControl.liaNumOfFamilies = taskControlQuote.liaNumOfFamilies;

                taskControl.liaLimit = taskControlQuote.liaLimit;
                taskControl.liaMedicalPayments = taskControlQuote.liaMedicalPayments;
                taskControl.liaPremium = taskControlQuote.liaPremium;
                taskControl.premium = taskControlQuote.premium;
                taskControl.liaTotalPremium = taskControlQuote.liaTotalPremium;
                taskControl.renewalNo = taskControlQuote.renewalNo;
                taskControl.isUpgraded = taskControlQuote.isUpgraded;
                taskControl.occupation = taskControlQuote.occupation;
                taskControl.Customer.JobPhone = taskControlQuote.Customer.JobPhone;
                taskControl.Customer.HomePhone = taskControlQuote.Customer.HomePhone;
                taskControl.Customer.Email = taskControlQuote.Customer.Email;
                taskControl.comments = taskControlQuote.comments;
                taskControl.comment = taskControlQuote.comment;
                taskControl.additionalStructure = taskControlQuote.additionalStructure;
                taskControl.Island = taskControlQuote.Island;

                taskControl.Inspector = taskControlQuote.Inspector;
                taskControl.InspectionDate = taskControlQuote.InspectionDate;
                taskControl.DiscountsHomeOwners = taskControlQuote.DiscountsHomeOwners;
                taskControl.TypeOfInsuredID = taskControlQuote.TypeOfInsuredID;
                taskControl.SubmittedDate = taskControlQuote.SubmittedDate;

                btnSavePolicy.Visible = true; btnSavePolicy2.Visible = true;
                btnSavePolicy.Enabled = true; btnSavePolicy2.Enabled = true;
                //btnSavePolicy.Enabled = false; btnSavePolicy2.Enabled = false;
                //btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;
                btnQuote.Visible = false; btnQuote2.Visible = false;
                btnQuote.Enabled = false; btnQuote2.Enabled = false;


                Session.Add("AdditionalInsured", dt2);
                Session.Add("FromConvert", "");
                Session.Add("TaskControl", taskControl);
                Response.Redirect("HomeOwners.aspx", false);

            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }

        protected void SaveClick()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            int userID = 0;

            try
            {
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
            }

            try
            {
                if (validatePolicyInfo())
                {
                    if (validateCustomerInfo())
                    {
                        if (validateGeneralInfo())
                        {
                            if (validatePropertyInfo())
                            {
                                //if (validateLiabilityInfo())
                                //{
                                if (validateTypeOfInsured())
                                {
                                    connectWithExcel();
                                    FillProperties();
                                    taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                                    taskControl.saveHomeOwners(userID);  //(userID);
                                    TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                                    UpdatePolicyTC_VI(taskControl2.TaskControlID, Session["TaskControlIDOLD"].ToString());
                                    Session["TaskControl"] = taskControl2;

                                    if (Session["AdditionalInsured"] != null)
                                    {
                                        System.Data.DataTable dt = (System.Data.DataTable)Session["AdditionalInsured"];
                                        SaveAdditionalInsured(dt, "Policy");
                                    }
                                    DisableControls();
                                    FillTextControl();
                                    //lblRecHeader.Text = "Policy saved succesfully!";
                                    //mpeSeleccion.Show();
                                }
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message + "";
                mpeSeleccion.Show();
            }
        }
        protected void btnSavePolicy_Click(object sender, EventArgs e)
        {
            lblRecHeader.Text = "";
            EPolicy.TaskControl.HomeOwners taskControlQuote = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            try
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

                try
                {
                    if (DateTime.Parse(txtEffectiveDate.Text.ToString().Trim()) < System.DateTime.Today && DateTime.Parse(txtEffectiveDate.Text.ToString().Trim()) < DateTime.Parse(((DateTime)taskControl.EntryDate).ToShortDateString()))
                    {
                        lblRecHeader.Text = "Policy Effective Date should be Current Date.";
                        mpeSeleccion.Show();
                        return;
                    }
                    else
                    {
                        txtExpirationDate.Text = DateTime.Parse(txtEffectiveDate.Text).AddYears(1).ToShortDateString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Policy Effective Date should be Current Date.", ex);
                }
				
				//if ((taskControl.BankPPSID != null && taskControl.BankPPSID2 != null) && (taskControl.BankPPSID != "" && taskControl.BankPPSID2 != ""))
               // if (ddlBankList.SelectedItem.Text != "" && ddlBankList2.SelectedItem.Text != "")
               // {
                    //if (ddlBankList.SelectedItem.Value == ddlBankList2.SelectedItem.Value)
				//	if (taskControl.BankPPSID == taskControl.BankPPSID2) 						
                //    {
                //        lblRecHeader.Text = "The selected banks CANNOT be the same.";
                //        mpeSeleccion.Show();
                //        return;
                 //   }

                ///}

                SaveClick();
                taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                if (taskControl.TaskControlID == 0)
                    EnableControls();
                var EffectiveDate = txtEffectiveDate.Text;
                FillTextControl();
                if (taskControl.TaskControlID == 0)
                {
                    txtEffectiveDate.Text = EffectiveDate;
                    BtnPremiumFinance.Visible = false;
                    Session["FromConvert"] = null;
                    CheckBank.Checked = true;
                    chkBank_CheckedChange(String.Empty, EventArgs.Empty);
                }

                if (lblRecHeader.Text != "")
                    throw new Exception(lblRecHeader.Text);

                try
                {

                    taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                    if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                    {
                        if (!SendPolicyToPPS(taskControl.TaskControlID))
                        {
                            try
                            {
                                if (!SendPolicyToPPS(taskControl.TaskControlID))
                                    throw new Exception("PPS CONNECTION ERROR");
                            }
                            catch (Exception exc)
                            {
                                EPolicy.TaskControl.Policy.DeletePolicyPPSError(taskControl.TaskControlID != 0 ? taskControl.TaskControlID : 0);
                                throw new Exception(exc.Message.ToString() + " DELETED CONTROL #:" + taskControl.TaskControlID);
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    LogError(exp);
                    Session.Remove("TaskControl");
                    Session.Add("TaskControl", (EPolicy.TaskControl.HomeOwners)Session["TaskControlOLD"]);
                    DisableControls();
                    FillTextControl();
                    lblRecHeader.Text = "The policy could not be issued due to an error in the internet connection. Please try again later.";
                    mpeSeleccion.Show();
                    return;
                }

                lblRecHeader.Text = "Policy saved succesfully!";
                mpeSeleccion.Show();
            }
            catch (Exception exc)
            {
                lblRecHeader.Text = exc.Message; //+ " " + exc;
                mpeSeleccion.Show();
            }
        }

        protected void btnQuote_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;

            try
            {
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
            }

            if (taskControl.isRenew == false)
            {
                if (taskControl.isQuote == true && taskControl.approved == false && taskControl.submitted == false)
                {
                    if (txtConfirmEmailAddress.Text.Trim() == "")
                    {
                        lblRecHeader.Text = "The Email has not been confirmed.";

                        mpeSeleccion.Show();
                        return;
                    }

                    if (txtEmail.Text.Trim().ToUpper() != txtConfirmEmailAddress.Text.Trim().ToUpper())
                    {
                        txtConfirmEmailAddress.Text = "";
                        lblRecHeader.Text = "The emails did not match. Please try again.";
                        mpeSeleccion.Show();
                        return;
                    }
                    
                }
            }

            try
            {
                if (validatePolicyInfo())
                {
                    if (validateCustomerInfo())
                    {
                        if (validateGeneralInfo())
                        {
                            if (validatePropertyInfo())
                            {
                                //if (validateLiabilityInfo())
                                //{
                                if (validateTypeOfInsured())
                                {
                                    connectWithExcel();
                                    FillProperties();
                                    taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                                    taskControl.submitted = false;
                                    taskControl.rejected = false;
                                    taskControl.saveHomeOwners(userID);  //(userID);
                                    if (Session["AdditionalInsured"] != null)
                                    {
                                        System.Data.DataTable dt = (System.Data.DataTable)Session["AdditionalInsured"];
                                        SaveAdditionalInsured(dt, "Quote");
                                    }
                                    TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                                    Session["TaskControl"] = taskControl2;
                                    DisableControls();
                                    FillTextControl();
                                    lblRecHeader.Text = "Quote saved succesfully!";
                                    mpeSeleccion.Show();
                                }
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message + "";
                mpeSeleccion.Show();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            txtAlertComment.Visible = false;
            lblAlertComment.Visible = false;
            lblAlertEntryDate.Visible = true;
            txtAlertEntryDate.Visible = true;


            if (taskControl.rejected)
            {
                lblRecHeader.Text = "This quote has been declined";
                mpeSeleccion.Show();
                return;
            }

            if (((System.Web.UI.WebControls.Button)sender).ID.ToString() != "btnSubmitAlert")
            {
                Mirror2.Visible = true;
                btnSubmitAlert.Visible = true;
                btnApprove.Visible = false;
                btnRejected.Visible = false;
                btnRevert.Visible = false;

                string DivAlert;
                DivAlert = @"<div align=""center"">";
                DivAlert += "<br>";
                DivAlert += @"<h3><b>Please remember to attach all required documents.</b></h3>";
                DivAlert += "<br>";
                DivAlert += @"<p><b>•	Current Photos </b></p>";
                DivAlert += "<br>";
                DivAlert += @"<p><b>•	Signed Quote/Application </b></p>";
                DivAlert += "<br>";
                DivAlert += @"<p><b>•	Signed Notice of Conditions of Underinsurance </b></p>";
                DivAlert += "<br>";
                DivAlert += "<div>";
                phAlert.Controls.Add(new Literal() { Text = DivAlert });
                mpeAlert.Show();

                System.Data.DataTable DtCert = new System.Data.DataTable();
                DtCert = EPolicy.Customer.Customer.GetDocumentsByCustomerNo(taskControl.CustomerNo, taskControl.TaskControlID, taskControl.isQuote ? 0 : taskControl.TCIDQuotes);
                if (DtCert != null)
                    if (DtCert.Rows.Count > 0)
                        btnSubmitAlert.Enabled = true;
                    else
                        btnSubmitAlert.Enabled = false;
            }
            else
            {
                Submit();
            }
        }

        private void Submit()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            int userID = 0;

            try
            {
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
            }

            //if (taskControl.submitted)
            //{
            //    lblRecHeader.Text = "This quote has already been submitted";
            //    mpeSeleccion.Show();
            //}

            try
            {
                if (validatePolicyInfo())
                {
                    if (validateCustomerInfo())
                    {
                        if (validateGeneralInfo())
                        {
                            if (validatePropertyInfo())
                            {
                                //if (validateLiabilityInfo())
                                //{
                                if (validateTypeOfInsured())
                                {
                                    connectWithExcel();
                                    FillProperties();
                                    taskControl.submitted = true;
                                    taskControl.approved = false;
                                    taskControl.SubmittedDate = txtAlertEntryDate.Text.Trim();
                                    taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                                    taskControl.saveHomeOwners(userID);  //(userID);
                                    TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                                    Session["TaskControl"] = taskControl2;
                                    DisableControls();
                                    FillTextControl();
                                    btnPrintQuote_Click(String.Empty, EventArgs.Empty);
                                    SubmitRequestEmail();
                                    HomeOwnerStatusLog(taskControl.TaskControlID,GetUserEmail(taskControl.EnteredBy), "Submitted", DateTime.Parse(txtAlertEntryDate.Text.Trim()));
                                    string CommentLine = cp.Identity.Name.Split("|".ToCharArray())[0].ToString() + "- SUBMITTED " + "QUOTE#: " + taskControl2.TaskControlID.ToString() + " - SUBMITTED Date: " + txtAlertEntryDate.Text.Trim();
                                    //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine ,txtAlertEntryDate.Text.Trim());
                                    EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine);
                                    DisableControls();
                                    lblRecHeader.Text = "Your message has been sent successfully";
                                    mpeSeleccion.Show();
                                }
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message.ToString();
                mpeSeleccion.Show();
            }
        }

        private System.Data.DataTable UpdateHomeOwnersStatus(string TaskControlID, bool Rejected, bool Revert)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rejected",
                SqlDbType.Bit, 0, Rejected.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Revert",
                SqlDbType.Bit, 0, Revert.ToString(),
                ref cookItems);

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                dt = Executor.GetQuery("UpdateHomeOwnersStatus", xmlDoc);
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            int userID = 0;

            try
            {
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
            }
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            Session["TaskControl"] = taskControl;
            DisableControls();
            FillTextControl();
        }

        private string FormatCurrency(string tocurrency)
        {
            //if (((System.Web.UI.WebControls.TextBox)TextBox).Text == "")
            //{
            //    ((System.Web.UI.WebControls.TextBox)TextBox).Text = "$0.00";
            //    return ((System.Web.UI.WebControls.TextBox)TextBox);
            //}
            //else
            //    return ((System.Web.UI.WebControls.TextBox)TextBox);
            double number = 0.00;
            string currency = "";

            if (tocurrency.Trim() != "")
            {
                number = double.Parse(tocurrency.Replace("$", "").Replace(",", "").ToString());
                currency = number.ToString("c0");
                //if (currency == "$0")
                //    currency = "";
                return currency + ".00";
            }
            else
                return currency;
        }

        private static System.Data.DataTable GetAgentByUserID(string UserID)
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
            System.Data.DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetAgentByUserID_VI", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

        private static System.Data.DataTable GetEmailByUserID(string UserID)
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
            System.Data.DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetEmailByUserID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

        private void StatusEmail()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            string logStatus = "";
            string UserEmail = "";

            try
            {
                // TODO ANADIR EMAIL DE USUARIO Y ENVIAR LA POLIZA

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                //System.Data.DataTable dtEmail = GetEmailByUserID(cp.UserID.ToString());

                //string Email = dtEmail.Rows[0]["Email"].ToString() ?? "";

                //if (Email == "" || Email == "N/A")
                //    return;

                System.Data.DataTable dt = EPolicy.Customer.Customer.GetDocumentsByCustomerNo(taskControl.CustomerNo, taskControl.TaskControlID, taskControl.isQuote ? 0 : taskControl.TCIDQuotes);

                string emailNoreplay = "guardian.insurance@guardianinsurance.com";
                string EmailNoReplayPass = "gicSTTGI1";
                string emailSend = "lsemailservice@gmail.com";
                string msg = "";
                string msgComment = "";
                string pdf = TextBox1.Text;
                string ExportFilesPath = ConfigurationManager.AppSettings["ExportsFilesPathName"].ToString().Trim();
                string DocumentPath = System.Configuration.ConfigurationManager.AppSettings["DocumentsPath"];

                MailMessage SM = new MailMessage();
                string InsuredName = taskControl.Customer.FirstName.ToString() == "" && taskControl.Customer.LastName1.ToString() == "" ? taskControl.Customer.LastName2.ToString() : taskControl.Customer.FirstName.ToString() + " " + taskControl.Customer.LastName1.ToString();

                SM.Subject = "RESIDENTIAL POLICY REQUEST – " + InsuredName;
                SM.From = new System.Net.Mail.MailAddress(emailNoreplay);

                if (taskControl.approved)
                {
                    logStatus = "Approved";
                    msg = "We have reviewed the required documentation on the subject prospect and we hereby APPROVE the risk. Please note that cover is BOUND WHEN POLICY IS ISSUED BY YOUR OFFICE as you are now able to issue the policy in ePPS.";
                }
                else if (taskControl.rejected)
                {
                    logStatus = "Declined";
                    msg = "We have reviewed the required documentation on the subject prospect and we hereby DECLINED the risk.";
                }

                msgComment = " <br><br>"+ "Comment:" + "<br>"+ txtAlertComment.Text.Trim().ToUpper();
                

                int text = msgComment.IndexOf("\n");
                //msgComment.Replace("\n","<br>");
                msgComment = Regex.Replace(msgComment, @"\r\n?|\n", "<br>");

                if (txtAlertComment.Text.Trim()!="")
                     //SM.Body = "<p>" + msg + msgComment +"<br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + taskControl.AgentDesc.ToString().Trim() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                    SM.Body = "<p>" + msg + msgComment + "<br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + GetUserAgencyDesc() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                else
                     //SM.Body = "<p>" + msg +"<br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + taskControl.AgentDesc.ToString().Trim() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                    SM.Body = "<p>" + msg + "<br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + GetUserAgencyDesc() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                SM.IsBodyHtml = true;
                //SM.Attachments.Add(new Attachment(ExportFilesPath + pdf));
                //SM.Attachments.Add(new Attachment(Server.MapPath("Reports/HomeOwners/NOTICE OF CONDITIONS OF UNDERINSURANCE FINAL VERSION.pdf")));
                //AddDocuments(SM, dt, DocumentPath);


                System.Data.DataTable dtuser = GetUsernameByUserID(cp.UserID);
                string dUserName = "";

                if (dtuser.Rows.Count > 0)
                {
                    dUserName = dtuser.Rows[0]["UserName"].ToString();
                }

                UserEmail = GetUserEmail(taskControl.EnteredBy);
                if (bool.Parse(ConfigurationManager.AppSettings["isProduction"]))
                {
                    //====PROD====
                    SM.To.Add(UserEmail);
                    //============
                }
                else
                {
                    // ====TEST=====
                    //SM.To.Add("ivelez@guardianinsurance.com");
					//SM.To.Add("dmoyett@lanzasoftware.com");
                    SM.To.Add(UserEmail);
                    SM.CC.Add("ivelez@guardianinsurance.com");
					SM.Bcc.Add("dmoyett@lanzasoftware.com");
                    SM.CC.Add("rcruz@guardianinsurance.com");
                    //SM.Bcc.Add("jbenitez@guardianinsurance.com");
                    //SM.To.Add(GetUserEmail(UserEmail));
                    //============
                }

                try
                {
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailNoreplay, EmailNoReplayPass);
                    client.Host = ConfigurationManager.AppSettings["IPMail"].ToString().Trim();
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(SM);

                    HomeOwnerStatusLog(taskControl.TaskControlID, UserEmail, logStatus, DateTime.Parse(txtAlertEntryDate.Text.Trim()));
                }
                catch (Exception exc)
                {
                    HomeOwnerStatusLog(taskControl.TaskControlID, UserEmail, logStatus, DateTime.Now);
                    msg = exc.InnerException.ToString() + " " + exc.Message;
                }

                lblRecHeader.Text = "Quote sent for submission confirmation!";
                mpeSeleccion.Show();
            }
            catch (Exception exp)
            {
                HomeOwnerStatusLog(taskControl.TaskControlID, UserEmail, logStatus,DateTime.Now);
                lblRecHeader.Text = exp.Message.ToString();
                mpeSeleccion.Show();
            }
        }

        private void HomeOwnerStatusLog(int TaskControlID, string email, string status,DateTime date)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Email",
                SqlDbType.VarChar, 50, email.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Status",
            SqlDbType.VarChar, 50, status.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EntryDate",
            SqlDbType.DateTime, 0, date.ToString(),
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
            try
            {
                exec.BeginTrans();
                exec.Insert("AddHomeOwnersStatusLog", xmlDoc);
                exec.CommitTrans();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not update optional.", ex);
            }
        }

        private void SubmitRequestEmail()
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                System.Data.DataTable dt = EPolicy.Customer.Customer.GetDocumentsByCustomerNo(taskControl.CustomerNo, taskControl.TaskControlID, taskControl.isQuote ? 0 : taskControl.TCIDQuotes);

                string emailNoreplay = "guardian.insurance@guardianinsurance.com";
                string EmailNoReplayPass = "gicSTTGI1";
                string emailSend = "lsemailservice@gmail.com";
                string msg = "";
                string pdf = TextBox1.Text;
                string ExportFilesPath = ConfigurationManager.AppSettings["ExportsFilesPathName"].ToString().Trim();
                string DocumentPath = System.Configuration.ConfigurationManager.AppSettings["DocumentsPath"];

                MailMessage SM = new MailMessage();
                string InsuredName = taskControl.Customer.FirstName.ToString() == "" && taskControl.Customer.LastName1.ToString() == "" ? taskControl.Customer.LastName2.ToString() : taskControl.Customer.FirstName.ToString() + " " + taskControl.Customer.LastName1.ToString();

                SM.Subject = "RESIDENTIAL POLICY REQUEST – " + InsuredName;
                SM.From = new System.Net.Mail.MailAddress(emailNoreplay);
                //SM.Body = "<p>We have submitted a Residential Quote in ePPS for the subject prospect and are hereby requesting Guardian’s acceptance in order to issue the Policy. Your prompt response is appreciated. Comment: " + taskControl.comment + "<br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + taskControl.AgentDesc.ToString().Trim() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                SM.Body = "<p>We have submitted a Residential Quote in ePPS for the subject prospect and are hereby requesting Guardian’s acceptance in order to issue the Policy. Your prompt response is appreciated. Comment: " + taskControl.comment + "<br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + GetUserAgencyDesc() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                SM.IsBodyHtml = true;
                SM.Attachments.Add(new Attachment(ExportFilesPath + pdf));
                //SM.Attachments.Add(new Attachment(Server.MapPath("Reports/HomeOwners/NOTICE OF CONDITIONS OF UNDERINSURANCE FINAL VERSION.pdf")));
                //AddDocuments(SM, dt, DocumentPath);

                //====PROD====
                //SM.To.Add("wbowers@guardianinsurance.com");
                //SM.To.Add("mhedrington@guardianinsurance.com");
                //SM.CC.Add("jcarney@guardianinsurance.com");
                //if (ddlAgency.SelectedItem.Text.Trim() == "MIDOCEAN INSURANCE AGENCY"
                //|| ddlAgency.SelectedItem.Text.Trim() == "VELA INSURANCE")
                //{
                //    SM.CC.Add("jdavila@guardianinsurance.com ");
                //    SM.CC.Add("amartinez@guardianinsurance.com");
                //}

                //============
                // ====TEST=====
                //SM.To.Add("dmoyett@lanzasoftware.com");
				
				SM.To.Add("ivelez@guardianinsurance.com");
				SM.Bcc.Add("dmoyett@lanzasoftware.com");
                SM.CC.Add("rcruz@guardianinsurance.com");
				SM.To.Add("rcruz@guardianinsurance.com");

                if (ddlAgency.SelectedItem.Text.Trim() == "MIDOCEAN INSURANCE AGENCY"
                || ddlAgency.SelectedItem.Text.Trim() == "VELA INSURANCE")
                {
                    SM.CC.Add("hramos@lanzasoftware.com");
                }
                //============

                try
                {
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailNoreplay, EmailNoReplayPass);
                    client.Host = ConfigurationManager.AppSettings["IPMail"].ToString().Trim();
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(SM);
                }
                catch (Exception exc)
                {
                    msg = exc.InnerException.ToString() + " " + exc.Message;
                }

                lblRecHeader.Text = "Quote sent for submission confirmation!";
                mpeSeleccion.Show();
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message.ToString();
                mpeSeleccion.Show();
            }
        }

        private MailMessage AddDocuments(MailMessage SM, System.Data.DataTable dt, string DocumentPath)
        {
            foreach (System.Data.DataRow Row in dt.Rows)
            {
                string[] Files = Directory.GetFiles(DocumentPath, "*" + Row["DocumentsID"].ToString() + "_" + Row["CustomerNo"].ToString() + "*");

                if (Files.Length > 0)
                {
                    string FilePath = Files[0].ToString();
                    if (FilePath.ToString().Contains(".jpg"))
                    {
                        Attachment inline = new Attachment(FilePath);
                        inline.ContentDisposition.Inline = true;
                        inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                        inline.ContentId = "Picture";
                        inline.ContentType.MediaType = "image/png";
                        inline.ContentType.Name = Path.GetFileName(FilePath);
                        SM.Attachments.Add(inline);
                    }
                    else
                        SM.Attachments.Add(new Attachment(DocumentPath + Files[0].ToString()));
                }
            }

            return SM;
        }

        private void UpdatePolicyTC_VI(int TaskControlID, string ApplicationTC)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ApplicationTC",
                SqlDbType.VarChar, 50, ApplicationTC.ToString(),
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
            try
            {
                exec.GetQuery("UpdatePolicyApplicationTC", xmlDoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not update optional.", ex);
            }

        }

        void UpdatePolicyPPSError(int TaskControlID)
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
            try
            {
                exec.GetQuery("UpdatePolicyPPSError", xmlDoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
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
            string path = Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public bool SendPolicyToPPS(int TaskControlID)
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                System.Data.DataTable PPSPolicy = new System.Data.DataTable();
                System.Data.DataTable dt = GetHomeOwnersToPPSByTaskControlID(TaskControlID);

                System.Data.DataTable dtReinsAsl = new System.Data.DataTable();
                System.Data.DataTable dtOtherCvrgDetail = new System.Data.DataTable();
                System.Data.DataTable dtAdditionalInsured = new System.Data.DataTable();

                string CoverageCodesXml = "";
                string OtherCvrgDetailXml = "";
                string AdditionalInsuredXml = "";

                if (Session["dtReinsAsl"] != null)
                {
                    dtReinsAsl = ((System.Data.DataTable)Session["dtReinsAsl"]).Copy();
                    dtReinsAsl.DefaultView.Sort = "ReinsAsl DESC";
                    dtReinsAsl = dtReinsAsl.DefaultView.ToTable();

                    dtOtherCvrgDetail.TableName = "OtherCvrgDetail";
                    dtOtherCvrgDetail.Columns.Add("ReinsAsl");
                    dtOtherCvrgDetail.Columns.Add("DisplayAs");
                    dtOtherCvrgDetail.Columns.Add("IndexNo");
                    dtOtherCvrgDetail.Columns.Add("Limit1");
                    dtOtherCvrgDetail.Columns.Add("Limit2");

                    string Lim1 = "", Lim2 = "", Lim3 = "", Lim4 = "", Lim5 = "";

                    Lim1 = txtLimit1.Text.ToString().Replace("$", "").Replace(",", ""); 
                    Lim2 = txtLimit2.Text.ToString().Replace("$", "").Replace(",", "");
                    Lim3 = txtLimit3.Text.ToString().Replace("$", "").Replace(",", "");

                    if (ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "") == "NO COVERAGE")
                        Lim4 = "0.00";
                    else
                        Lim4 = ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "");

                    Lim5 = txtLimit4.Text.ToString().Replace("$", "").Replace(",", "");

                    if (dtReinsAsl.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtReinsAsl.Rows.Count; i++)
                        {
                            int Count = 1;
                            DataRow row = dtOtherCvrgDetail.NewRow();
                            switch (dtReinsAsl.Rows[i]["ReinsAsl"].ToString())
                            {
                                case "28040": //Fire, Extended Coverage
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Dwelling";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim1;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Other Structures";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim2;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Personal Property";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Loss of Use";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim5;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "29040": //Wind Perils
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Dwelling";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim1;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Other Structures";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim2;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Personal Property";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Loss of Use";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim5;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "30120": //Earthquake
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Dwelling";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim1;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Other Structures";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim2;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Personal Property";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Loss of Use";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim5;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "31260": //theft
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Personal Property";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "33040": //Personal Liability
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Personal Liability";
                                    row[2] = (Count++).ToString();
                                    if (ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "") == "NO COVERAGE")
                                        row[3] = "0.00";
                                    else
                                        row[3] = ddlLiaLimit.SelectedItem.Text.ToString().Replace("$", "").Replace(",", "");
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = "36040";
                                    row[1] = "Medical Payments to Others";
                                    row[2] = (Count++).ToString();
									
                                     if (txtLiaMedicalPayments.Text.ToString().Replace("$", "").Replace(",", "") == "NO COVERAGE")
                                        row[3] = "0.00";
                                    else
                                        row[3] = txtLiaMedicalPayments.Text.ToString().Trim().Replace("$", "").Replace(",", "");
									
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "14010": //Fire
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Building";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim1;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Other Structures";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim2;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Contents";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Loss Of Use";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim5;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "15021": // Extended Coverage
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Building";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim1;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Other Structures";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim2;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Contents";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Loss Of Use";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim5;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "16021": //Wind Perils
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Building";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim1;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Other Structures";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim2;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Contents";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Loss Of Use";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim5;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;

                                case "17120": //Earthquake
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Building";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim1;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Other Structures";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim2;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Contents";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim3;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    row = dtOtherCvrgDetail.NewRow();
                                    row[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                                    row[1] = "Loss Of Use";
                                    row[2] = (Count++).ToString();
                                    row[3] = Lim5;
                                    row[4] = "0";
                                    dtOtherCvrgDetail.Rows.Add(row);
                                    break;
                            }
                        }
                    }

                    using (StringWriter sw = new StringWriter())
                    {
                        dtReinsAsl.WriteXml(sw);
                        CoverageCodesXml = sw.ToString();
                    }

                    using (StringWriter sw = new StringWriter())
                    {
                        dtOtherCvrgDetail.WriteXml(sw);
                        OtherCvrgDetailXml = sw.ToString();
                    }
                }

                if (Session["AdditionalInsured"] != null)
                {
                    dtAdditionalInsured = ((System.Data.DataTable)Session["AdditionalInsured"]).Copy();
                    //dtReinsAsl.DefaultView.Sort = "ReinsAsl DESC";
                    //dtReinsAsl = dtReinsAsl.DefaultView.ToTable();

                    dtAdditionalInsured.TableName = "AdditionalInsured";
                    //dtAdditionalInsured.Columns.Add("AdditionalInsuredFirstName");
                    //dtAdditionalInsured.Columns.Add("AdditionalInsuredLastName");


                    if (dtAdditionalInsured.Rows.Count > 0)
                    {
                        using (StringWriter sw = new StringWriter())
                        {
                            dtAdditionalInsured.WriteXml(sw);
                            AdditionalInsuredXml = sw.ToString();
                        } 
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    cmd.CommandText = "sproc_ConsumeXMLePPS-HOMEOWNER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    string ComRate = "0.0000000e+000";
					

                    System.Data.DataTable DtCommision = GetCommissionAgentRateByAgentID(TaskControlID.ToString(), "25"); //GetCommissionAgentRateByAgentID(AgentID.ToString().Trim(), "25");

                    if (DtCommision.Rows.Count > 0)
                    {
                        ComRate = DtCommision.Rows[0]["CommissionRate"].ToString();

                        ComRate = (double.Parse(ComRate) / 100).ToString();
                    }
                    else
                    {
                        ComRate = "0.0000000e+000";

                    }

                    //Insert OtherCvrgDetail
                    //Reinsasl  IndexNo DisplayAs
                    //28040	    1	    Dwelling
                    //28040	    2	    Other Structures
                    //28040	    3	    Personal Property
                    //28040	    4	    Loss of Use
                    //29040	    1	    Dwelling
                    //29040	    2	    Other Structures
                    //29040	    3	    Personal Property
                    //29040	    4	    Loss of Use
                    //30120	    1	    Dwelling
                    //30120	    2	    Other Structures
                    //30120	    3	    Personal Property
                    //30120	    4	    Loss of Use
                    //31260	    1	    Personal Property
                    //33040	    1	    Personal Liability
                    //36040	    1	    Medical Payments to Others

                    cmd.Parameters.Clear();
                    // Add the parameters for Policy
                    if (taskControl.isRenew == true)
                    {
                        cmd.Parameters.AddWithValue("@PolicyID", txtPreviousPolicyType.Text.Trim() + txtPreviousPolicyNo.Text.Trim() + "-" + DateTime.Parse(txtEffectiveDate.Text.Trim()).Year.ToString().Substring(2).ToString());
                        if (txtPreviousPolicySuffix.Text.Trim() != "" && txtPreviousPolicySuffix.Text.Trim() != "00")
                        {
                            cmd.Parameters.AddWithValue("@RenewalOf", txtPreviousPolicyType.Text.Trim() + txtPreviousPolicyNo.Text.Trim() + "-" + txtPreviousPolicySuffix.Text.Trim());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RenewalOf", txtPreviousPolicyType.Text.Trim() + txtPreviousPolicyNo.Text.Trim());
                        }
                        cmd.Parameters.AddWithValue("@TransType", "REN");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@TransType", "NEW");
                        cmd.Parameters.AddWithValue("@RenewalOf", "");
                    }
                    //cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Incept", DateTime.Parse(dt.Rows[0]["EffectiveDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                    cmd.Parameters.AddWithValue("@Expire", DateTime.Parse(dt.Rows[0]["ExpirationDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                    cmd.Parameters.AddWithValue("@BrokerID", dt.Rows[0]["CarsID"].ToString().Trim());
                    cmd.Parameters.Add("@CanDate", SqlDbType.DateTime).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@CanDate", "");
                    cmd.Parameters.Add("@TmpTime", SqlDbType.DateTime).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@TmpTime", "");
                    cmd.Parameters.Add("@BinderID", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@BinderID", "true");
                    cmd.Parameters.AddWithValue("@ComRate", ComRate);
                    cmd.Parameters.Add("@Tag", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@Tag", "");
                    cmd.Parameters.AddWithValue("@Premium", dt.Rows[0]["TotalPremium"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@DispImage", "Policy");
                    cmd.Parameters.Add("@SpecEndorse", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@SpecEndorse", "");
                    cmd.Parameters.AddWithValue("@SID", "0");
                    cmd.Parameters.Add("@UDPolicyID", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@UDPolicyID", "0");
                    cmd.Parameters.AddWithValue("@PreparedBy", dt.Rows[0]["EnteredBy"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@ExcessLink", "0");
                    cmd.Parameters.AddWithValue("@PolSubType", dt.Rows[0]["PolicyType"].ToString().Trim() == "INC" ? "Cml" : dt.Rows[0]["PropertyForm"].ToString().Trim().Replace(" ", "-") + "(4/84)");
                    cmd.Parameters.AddWithValue("@ReinsPcnt", "0.0000000e+000");
                    cmd.Parameters.AddWithValue("@Assessment", "0.0000");
                    cmd.Parameters.Add("@PayDate", SqlDbType.DateTime).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@PayDate", "");
                    // Add the parameters for EntNames
                    cmd.Parameters.AddWithValue("@LastName", dt.Rows[0]["LastNa1"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@FirstName", dt.Rows[0]["FirstNa"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Middle", dt.Rows[0]["Initial"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Upid", "0");
                    cmd.Parameters.AddWithValue("@Dob", dt.Rows[0]["Birthday"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Sex", dt.Rows[0]["Sex"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Marital", dt.Rows[0]["MaritalStatus"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Yrsexp", "0");
                    cmd.Parameters.Add("@License", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@License", "true");
                    cmd.Parameters.AddWithValue("@State", dt.Rows[0]["State"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Ssn", "SSn");
                    cmd.Parameters.AddWithValue("@BusFlag", "0");
                    cmd.Parameters.AddWithValue("@Nsbyt", "0");
                    cmd.Parameters.Add("@BusOther", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@BusOther", "0");
                    cmd.Parameters.Add("@BusType", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@BusType", "0");
                    cmd.Parameters.AddWithValue("@Polrelat", "NI");
                    // Add the parameters for Client
                    cmd.Parameters.AddWithValue("@Client", dt.Rows[0]["ClientIDPPS"].ToString().Trim() == "" ? "0" : dt.Rows[0]["ClientIDPPS"].ToString().Trim());
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

                    //TWO STORY, TWO FAMILY DWELLING OF MASONRY CONSTRUCTION WITH AN APPROVED ROOF LOCATED AT:
                    // Add the parameters for Bldgs
                    cmd.Parameters.AddWithValue("@Descrip", dt.Rows[0]["NumOfStories"].ToString().Trim().ToUpper() + ", " + dt.Rows[0]["NumOfFamilies"].ToString().Trim().ToUpper() + " " + dt.Rows[0]["ConstructionType"].ToString().Trim().ToUpper() + " " + dt.Rows[0]["RoofDwelling"].ToString().Trim().ToUpper()); //2 Stories 2 Families Brick, Stone, Masonry construction	Concrete Roofdt.Rows[0]["NumOfFamilies"].ToString().Trim().ToUpper() 
                    cmd.Parameters.AddWithValue("@Location", dt.Rows[0]["PhysicalAddress"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Island", dt.Rows[0]["Island1"].ToString().Trim());//VI ISLANDS
                    cmd.Parameters.AddWithValue("@InsVal", dt.Rows[0]["TotalLimit"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@AnyNum", "0");
                    cmd.Parameters.AddWithValue("@PayeeID", dt.Rows[0]["BankPPSID"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@LoanNo", dt.Rows[0]["LoanNo"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@PayeeID2", dt.Rows[0]["BankPPSID2"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@LoanNo2", dt.Rows[0]["LoanNo2"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@Families", dt.Rows[0]["NumOfFamilies"].ToString().Trim().Replace("Families", "").Replace("Family", ""));
                    cmd.Parameters.AddWithValue("@RowHouse", "false");
                    cmd.Parameters.AddWithValue("@Rented", dt.Rows[0]["PropertyType"].ToString().Trim().Contains("Rented") ? "true" : "false");
                    if (ddlConstructionType.SelectedItem.Value == "Frame Construction")
                        cmd.Parameters.AddWithValue("@Construction", "1");
                    else if (ddlConstructionType.SelectedItem.Value == "Brick, Stone, Masonry Construction")
                        cmd.Parameters.AddWithValue("@Construction", "3");
                    else if (ddlConstructionType.SelectedItem.Value == "Mixed Construction")
                        cmd.Parameters.AddWithValue("@Construction", "8");
                    else
                        cmd.Parameters.AddWithValue("@Construction", "0");
                    cmd.Parameters.AddWithValue("@ProtectionClass", "0");
                    int intValue = int.TryParse(dt.Rows[0]["ConstructionYear"].ToString().Trim(), out intValue) ? intValue : 0;
                    cmd.Parameters.AddWithValue("@YearBuilt", intValue.ToString());
                    cmd.Parameters.AddWithValue("@FireDistrict", "0");
                    cmd.Parameters.AddWithValue("@Occupancy", "0");
                    cmd.Parameters.Add("@NavLimit", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@NavLimit", "null");
                    cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@TenderText", "null");
                    cmd.Parameters.Add("@TrailerText", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@TrailerText", "null");
                    cmd.Parameters.Add("@VName", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@VName", "null");
                    cmd.Parameters.Add("@Make", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@Make", "null");
                    cmd.Parameters.Add("@Model", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@Model", "null");
                    cmd.Parameters.Add("@HIN", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@HIN", "null");
                    cmd.Parameters.AddWithValue("@LOA", "0.0000000e+000");
                    cmd.Parameters.Add("@VesselProp", SqlDbType.NVarChar).Value = DBNull.Value;//cmd.Parameters.AddWithValue("@VesselProp", "null");
                    cmd.Parameters.AddWithValue("@Storeys", dt.Rows[0]["NumOfStories"].ToString().Replace("Story", "").Replace("Stories", "").Trim());
                    //OtherCoverage & OtherCovrgDetail
                    cmd.Parameters.AddWithValue("@CoverageCodesXml", CoverageCodesXml);
                    cmd.Parameters.AddWithValue("@OtherCvrgDetailXml", OtherCvrgDetailXml);
                    cmd.Parameters.AddWithValue("@AdditionalInsuredXml", AdditionalInsuredXml);

                    //@Descrip varchar(255),
                    //@Location varchar(255),
                    //@Island tinyint, --Island ID (ST. Thomas = 1 St Croix = 2, ST. John= 3, PR = 4)
                    //@InsVal money,
                    //@AnyNum int,
                    //@PayeeID int,
                    //@Families smallint,
                    //@RowHouse bit,
                    //@Rented bit,
                    //@Construction smallint,
                    //@ProtectionClass smallint,
                    //@YearBuilt smallint,
                    //@FireDistrict smallint,
                    //@Occupancy smallint,
                    //@NavLimit varchar(50),
                    //@TenderText varchar(255),
                    //@TrailerText varchar(255),
                    //@VName varchar(50),
                    //@Make varchar(50),
                    //@Model varchar(50),
                    //@HIN varchar(50),
                    //@LOA real,
                    //@VesselProp varchar(255),
                    //@Storeys smallint

                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(PPSPolicy);
                }

                sqlConnection1.Close();

                if (PPSPolicy.Rows.Count > 0)
                {
                    //txtAutoPolicyNo.Text = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("HOM", "").Replace("INC", "");
                    string ClientID = PPSPolicy.Rows[0]["Client"].ToString().Trim();
                    taskControl.Customer.Description = ClientID;
                    //taskControl.PolicyNo = txtAutoPolicyNo.Text;
                    //UpdatePolicyFromPPSByTaskControlID(TaskControlID, txtAutoPolicyNo.Text, ClientID);
                    //txtAutoPolicyNo.Text = txtAutoPolicyNo.Text.Contains("-") ?
                    //    int.Parse(txtAutoPolicyNo.Text.ToString().Substring(0, txtAutoPolicyNo.Text.ToString().Trim().IndexOf("-"))).ToString("")//txtAutoPolicyNo.Text.ToString().Trim().IndexOf("-"))).ToString("0000000")
                    //    : int.Parse(txtAutoPolicyNo.Text).ToString("");//: int.Parse(txtAutoPolicyNo.Text).ToString("0000000");
                    if (PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Contains('-'))
                    {
                        String[] arrayPolicyNo;
                        arrayPolicyNo = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("HOM", "").Replace("INC","").Split('-');
                        txtAutoPolicyNo.Text = arrayPolicyNo[0];
                        txtSuffix.Text = arrayPolicyNo[1];
                        taskControl.PolicyNo = arrayPolicyNo[0];
                        taskControl.Suffix = arrayPolicyNo[1];
                        UpdatePolicyHORenewFromPPSByTaskControlID(TaskControlID, arrayPolicyNo[0], ClientID, arrayPolicyNo[1]);
                    }
                    else
                    {
                        txtAutoPolicyNo.Text = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("HOM", "").Replace("INC","");
                        txtSuffix.Text = "00";
                        taskControl.PolicyNo = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("HOM", "").Replace("INC", "");
                        taskControl.Suffix = "00";
                        UpdatePolicyFromPPSByTaskControlID(TaskControlID, PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("HOM", "").Replace("INC", ""), ClientID);
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                LogError(exc);
                return false;
            }
        }

        private static System.Data.DataTable UpdatePolicyHORenewFromPPSByTaskControlID(int TaskControl, string PolicyNo, string ClientID, string Sufijo)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

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

        private static System.Data.DataTable GetHomeOwnersToPPSByTaskControlID(int TaskControlID)
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
            System.Data.DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetHomeOwnersToPPSByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the Stored Procedure called GetHomeOwnersToPPSByTaskControlID.", ex);
            }
        }

        private static System.Data.DataTable UpdatePolicyFromPPSByTaskControlID(int TaskControl, string PolicyNo, string ClientID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
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
                throw new Exception("Could not cook items.", ex);
            }
        }

        #region UPLOAD DOCUMENTS
        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            FillGridDocuments(true);
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            customer = taskControl.Customer;

            //var uc = (UserControl)Page.LoadControl("~/AddDocuments.ascx");
            //Panel1.Controls.Add(uc);
            //ModalPopupExtender1.Show();
            //return;

            if (customer.CustomerNo == "0")
            {
                lblRecHeader.Text = "You must save customer in order to proceed.";
                mpeSeleccion.Show();
            }
            else
            {
                txtDocumentDesc.Text = "";
                mpeAdjunto.Show();
            }
        }

        protected void btnAdjuntarCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
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
                int docid = EPolicy.Customer.Customer.Savedocuments(customer.CustomerNo.ToString(), txtDocumentDesc.Text.Trim(), ddlTransaction.SelectedItem.Value.Trim());

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

        private void FillGridDocuments(bool Refresh)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
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
                        foreach (ListItem item in ddlTransaction.Items)
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

        protected void gvAdjuntar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            customer = taskControl.Customer;
            int documentID = 0;
            try
            {
                if (e.CommandName.Trim() == "View")
                {
                    int index = Int32.Parse(e.CommandArgument.ToString());
                    GridViewRow row = gvAdjuntar.Rows[index];
                    TableCell cell = row.Cells[1]; //ID is displayed in 2nd column  
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
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                customer = taskControl.Customer;
                int index = e.RowIndex;
                GridViewRow row = gvAdjuntar.Rows[index];
                TableCell cell = row.Cells[1]; // ID is displayed in 2nd column  
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
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }

        protected void ddlTransaction_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Session["Transaction"] = ddlTransaction.SelectedIndex;
            mpeAdjunto.Show();
        }
        #endregion UPLOAD DOCUMENTS

        protected void lbCustomerNo_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

            if (taskControl.CustomerNo.ToString() != "")
            {
                Customer.Customer customer;
                customer = taskControl.Customer;
                customer.Mode = 3;    //Else
                Session["Customer"] = customer;
                RemoveSessionLookUp();
                Session.Add("HomeOwners", "HomeOwners");  // Para indicar en la pantalla de Customer que tiene que regresar al Application Auto.
                Response.Redirect("ClientIndividual.aspx");
            }
        }

        //ONLY WHEN THERE IS AN EXISTING POLICY FOR THIS QUOTE
        private void ReadOnly()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            if (taskControl.isQuote)
            {
                if (cp.IsInRole("HOME OWNERS REQUEST"))//&& !taskControl.approved
                {
                    BtnEdit.Visible = false; BtnEdit2.Visible = false;
                    btnPrintQuote.Visible = true; btnPrintQuote2.Visible = true;
                    btnPrintApplication.Visible = true;
                    //    btnSubmit.Visible = false; btnSubmit2.Visible = false;
                    //    BtnConvert.Visible = false; BtnConvert2.Visible = false;
                }
                else
                {
                    BtnEdit.Visible = true; BtnEdit2.Visible = true;
                    btnPrintQuote.Visible = true; btnPrintQuote2.Visible = true;
                    btnPrintApplication.Visible = true;
                    //btnSubmit.Visible = false; btnSubmit2.Visible = false;
                    //BtnConvert.Visible = true; BtnConvert2.Visible = true;//true when policy
                }

                btnSubmit.Visible = true; btnSubmit2.Visible = true;

                if (taskControl.approved)
                {
                    BtnConvert.Visible = true; BtnConvert2.Visible = true;
                }
                else
                {
                    BtnConvert.Visible = false; BtnConvert2.Visible = false;
                }


                btnAdjuntar.Visible = true;
                btnQuote.Visible = false; btnQuote2.Visible = false;
                btnSavePolicy.Enabled = false; btnSavePolicy2.Enabled = false;
                btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;
                BtnPrintPolicy.Visible = false; BtnPrintPolicy2.Visible = false;
                btnPrintDec.Visible = false;
                btnPrintInvoice.Visible = false;

                if (1 == 1)
                    BtnPremiumFinance.Visible = true;
                btnFM2.Enabled = false;
                btnFM5.Enabled = false;
                btnFM8.Enabled = false;
            }
            else
            {
                btnAdjuntar.Visible = true;
                btnSubmit.Visible = false; btnSubmit2.Visible = false;
                BtnEdit.Visible = false; BtnEdit2.Visible = false;
                BtnPrintPolicy.Visible = true; BtnPrintPolicy2.Visible = true;
                btnPrintDec.Visible = true;
                btnPrintInvoice.Visible = true;
                btnQuote.Visible = false; btnQuote2.Visible = false;
                btnSavePolicy.Enabled = false; btnSavePolicy2.Enabled = false;
                btnSavePolicy.Visible = false; btnSavePolicy2.Visible = false;
                btnPrintQuote.Visible = false; btnPrintQuote2.Visible = false;
                btnPrintApplication.Visible = true;// false

                if (1 == 1)
                    BtnPremiumFinance.Visible = true;
                btnFM2.Enabled = true;
                btnFM5.Enabled = true;
                btnFM8.Enabled = true;
            }


            if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
            {
                ddlOffice.Enabled = false;
                lblOffice.Visible = false;
                ddlOffice.Visible = false;
            }
            else
            {
                ddlOffice.Enabled = false;
            }

            ddlAgency.Enabled = false;

            ////BtnSave.Visible = false;
            BtnExit.Visible = true; BtnExit2.Visible = true;
            BtnCancel.Visible = false; BtnCancel2.Visible = false;
            btnCalculate.Visible = false; btnCalculate2.Visible = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtPhysicalAddress1.Enabled = false;
            txtPhysicalAddress1.Enabled = false;
            txtMailingAddress.Enabled = false;
            txtMailingAddress2.Enabled = false;
            txtCity.Enabled = false;
            txtCity2.Enabled = false;
            txtState.Enabled = false;
            txtState2.Enabled = false;
            txtZipCode.Enabled = false;
            txtZipCode2.Enabled = false;
            btnBankList.Enabled = false;
            txtBank.Enabled = false;
            txtBank2.Enabled = false;
            txtLoanNo.Enabled = false;
            txtLoanNo2.Enabled = false;
            ddlTypeOfInterest.Enabled = false;
            ddlMortgageeBilled.Enabled = false;
            txtEffectiveDate.Enabled = false;
            txtExpirationDate.Enabled = false;
            txtCatastropheCoverage.Enabled = false;
            ddlCatastropheDeduc.Enabled = false;
            txtWindstormDeductible.Enabled = false;
            txtAllOtherPerilsDeductible.Enabled = false;
            ddlConstructionType.Enabled = false;
            txtConstructionYear.Enabled = false;
            ddlNumberOfStories.Enabled = false;
            ddlNumOfFamilies.Enabled = false;
            txtIfYes.Enabled = false;
            txtLivingArea.Enabled = false;
            txtPorches.Enabled = false;
            ddlRoofDwelling.Enabled = false;
            rdbNo.Enabled = false;
            rdbYes.Enabled = false;
            txtEarthQuakeDeductible.Enabled = false;
            ddlResidence.Enabled = false;
            ddlPropertyType.Enabled = false;
            txtPropertyForm.Enabled = false;
            txtPolicyType.Enabled = false;
            ddlLosses3Years.Enabled = false;
            txtOtherStruct.Enabled = false;
            ddlPropertyShuttered.Enabled = false;
            ddlRoofOverhang.Enabled = false;
            ddlAutoPolicyWitGuardian.Enabled = false;
            txtAutoPolicyNo.Enabled = false;
            txtLimit1.Enabled = false;
            txtLimit2.Enabled = false;
            txtLimit3.Enabled = false;
            txtLimit4.Enabled = false;
            txtAOPDed1.Enabled = false;
            txtAOPDed2.Enabled = false;
            txtAOPDed3.Enabled = false;
            txtAOPDed4.Enabled = false;
            txtWindstormDed1.Enabled = false;
            txtWindstormDed2.Enabled = false;
            txtWindstormDed3.Enabled = false;
            txtWindstormDed4.Enabled = false;
            txtWindstormDedPer1.Enabled = false;
            txtWindstormDedPer2.Enabled = false;
            txtWindstormDedPer3.Enabled = false;
            txtWindstormDedPer4.Enabled = false;
            txtEarthquakeDed1.Enabled = false;
            txtEarthquakeDed2.Enabled = false;
            txtEarthquakeDed3.Enabled = false;
            txtEarthquakeDed4.Enabled = false;
            txtEarthQuakeDedPer1.Enabled = false;
            txtEarthQuakeDedPer2.Enabled = false;
            txtEarthQuakeDedPer3.Enabled = false;
            txtEarthQuakeDedPer4.Enabled = false;
            txtCoinsurance1.Enabled = false;
            txtCoinsurance2.Enabled = false;
            txtCoinsurance3.Enabled = false;
            txtCoinsurance4.Enabled = false;
            txtPremium1.Enabled = false;
            txtPremium2.Enabled = false;
            txtPremium3.Enabled = false;
            txtPremium4.Enabled = false;
            txtTotalLimit.Enabled = false;
            txtTotalWind.Enabled = false;
            txtTotalEarth.Enabled = false;
            //txtTotalPremium.Enabled = false;
            txtLiaPropertyType.Enabled = false;
            txtLiaPolicyType.Enabled = false;
            txtLiaNumOfFamilies.Enabled = false;
            ddlLiaLimit.Enabled = false;
            txtLiaMedicalPayments.Enabled = false;
            txtLiaPremium.Enabled = false;
            txtPremium.Enabled = false;
            rdbNoStruct.Enabled = false;
            rdbYesStruct.Enabled = false;
            txtOtherStruct.Enabled = false;
            txtTotalPremium.Enabled = false;
            chkMailSame.Enabled = false;
            txtOccupation.Enabled = false;
            txtBusinessPhone.Enabled = false;
            txtMobilePhone.Enabled = false;
            txtEmail.Enabled = false;
            CheckBank.Enabled = false;
            txtComments.Enabled = false;
            txtComment.Enabled = false;
            ddlIsland.Enabled = false;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Search.aspx");
        }
        protected void btnStatus_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            int userID = 0;
            string CommentLine;
            txtAlertComment.Visible = true;
            lblAlertComment.Visible = true;
            lblAlertEntryDate.Visible = false;
            txtAlertEntryDate.Visible = false;

            try
            {
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
            }

            //FillProperties();

            if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnStatus")
            {

                Mirror2.Visible = false;
                btnSubmitAlert.Visible = false;
                btnApprove.Visible = true;
                btnRejected.Visible = true;
                btnRevert.Visible = true;

                string Message = "";

                if (taskControl.approved)
                    Message = "been <b>Approved</b>";
                else if (taskControl.rejected)
                    Message = "been <b>Declined</b>";
                else if (!taskControl.submitted)
                    Message = "not been submitted";
                else
                    Message = "not been approved";

                string DivAlert;
                DivAlert = @"<div align=""center"">";
                DivAlert += "<br>";
                DivAlert += @"<h3><b>This Quote has " + Message + @".</b></h3>";
                DivAlert += "<br>";
                //DivAlert += @"<p><b>•	Current Photos </b></p>";
                //DivAlert += "<br>";
                //DivAlert += @"<p><b>•	Signed Quote/Application </b></p>";
                //DivAlert += "<br>";
                //DivAlert += @"<p><b>•	Signed Notice of Conditions of Underinsurance </b></p>";
                //DivAlert += "<br>";
                DivAlert += "</div>";
                phAlert.Controls.Add(new Literal() { Text = DivAlert });
                mpeAlert.Show();
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnApprove")
            {
                UpdateHomeOwnersStatus(taskControl.TaskControlID.ToString(), false, false);
                taskControl.approved = true;
                taskControl.rejected = false;
                TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                taskControl2 = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                Session["TaskControl"] = taskControl2;
                StatusEmail();
                CommentLine = cp.Identity.Name.Split("|".ToCharArray())[0].ToString() + "- APPROVED " + "QUOTE#: " + taskControl2.TaskControlID.ToString();
                //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine + " - COMMENT: " + txtAlertComment.Text.Trim(), txtAlertEntryDate.Text.Trim());
                //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine,txtAlertEntryDate.Text.Trim());

                EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine);
                if (txtAlertComment.Text.Trim() != "")
                {
                    EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine + " - COMMENT: " + txtAlertComment.Text.Trim().ToUpper());
                }
               
                lblRecHeader.Text = "Your message has been sent successfully";
                mpeSeleccion.Show();
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnRejected")
            {
                UpdateHomeOwnersStatus(taskControl.TaskControlID.ToString(), true, false);
                taskControl.approved = false;
                taskControl.rejected = true;
                TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                Session["TaskControl"] = taskControl2;
                StatusEmail();
                CommentLine = cp.Identity.Name.Split("|".ToCharArray())[0].ToString() + "- DECLINED " + "QUOTE#: " + taskControl2.TaskControlID.ToString();
                //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine + " - COMMENT: " + txtAlertComment.Text.Trim(), txtAlertEntryDate.Text.Trim());
                //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine,txtAlertEntryDate.Text.Trim());
                EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine);
                if (txtAlertComment.Text.Trim() != "")
                {
                    EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine + " - COMMENT: " + txtAlertComment.Text.Trim().ToUpper());
                }
                lblRecHeader.Text = "Your message has been sent successfully";
                mpeSeleccion.Show();
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnRevert")
            {
                UpdateHomeOwnersStatus(taskControl.TaskControlID.ToString(), false, true);
                taskControl.approved = false;
                taskControl.rejected = false;
                taskControl.submitted = false;
                TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                Session["TaskControl"] = taskControl2;
				StatusEmail();
                CommentLine = cp.Identity.Name.Split("|".ToCharArray())[0].ToString() + "- REVERTED " + "QUOTE#: " + taskControl2.TaskControlID.ToString();
                //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine, txtAlertEntryDate.Text.Trim());
                //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine + " - COMMENT: " + txtAlertComment.Text.Trim(), txtAlertEntryDate.Text.Trim());
                //EPolicy.Customer.Customer.AddCustomerCommentsWithDateByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine,txtAlertEntryDate.Text.Trim());
                EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine);
                if (txtAlertComment.Text.Trim() != "")
                {
                    EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(taskControl2.TaskControlID.ToString(), CommentLine + " - COMMENT: " + txtAlertComment.Text.Trim().ToUpper());
                }
                lblRecHeader.Text = "Quote has been reverted successfully";
                mpeSeleccion.Show();
            }
        }

        private static System.Data.DataTable GetCommissionAgentRateByAgentID(string TaskControlID, string PolicyClassID)
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
            System.Data.DataTable dt = null;
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
                                    //DivAlert += @"<p><b>•	Insured ID </b></p>";
                                    //DivAlert += "<br>";
                                    //DivAlert += @"<p><b>•	Vehicle Registration </b></p>";
                                    //DivAlert += "<br>";
                                    //DivAlert += @"<p><b>•	Signed Application </b></p>";

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
            FinanceMaster.Bank[] Banks = GetFMBanks();

            FM.AgencyCode = (new FinanceMaster()).GetFMID("Agent", String.Format(" LTRIM(RTRIM(AgentID)) = '{0}'", ddlAgency.SelectedItem.Value));
            FM.PolicyNumber = txtPolicyType.Text.Trim() + txtAutoPolicyNo.Text.Trim();
            FM.PolicyPremium = txtLiaTotalPremium.Text.Trim();
            FM.PolicyFee = "0.00";
            FM.PolicyTax = "0.00";
            FM.PersonalOrComm = "Private";
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
            FM.Coverage1 = "HOME OWNERS";
            return FM;
        }

        private FinanceMaster.Bank[] GetFMBanks()
        {
            FinanceMaster.Bank[] Banks = new FinanceMaster.Bank[2].Select(h => new FinanceMaster.Bank()).ToArray();
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            if (taskControl != null)
            {

                if (ddlBankList.SelectedIndex > 0 && ddlBankList2.SelectedIndex > 0)
                {
                    Banks[0].FMID = (new FinanceMaster()).
                        GetFMID("Bank_VI",
                        String.Format(" LTRIM(RTRIM(PPSID)) = '{0}'",
                        ddlBankList.SelectedItem.Value)
                        );
                    Banks[0].PPSID = ddlBankList.SelectedItem.Value;
                    if (!string.IsNullOrEmpty(Banks[0].PPSID.Trim()))
                        FillBank(Banks[0]);

                    Banks[1].FMID = (new FinanceMaster()).
                        GetFMID("Bank_VI",
                        String.Format(" LTRIM(RTRIM(PPSID)) = '{0}'",
                        ddlBankList2.SelectedItem.Value)
                        );
                    Banks[1].PPSID = ddlBankList2.SelectedItem.Value;
                    if (!string.IsNullOrEmpty(Banks[1].PPSID.Trim()))
                        FillBank(Banks[1]);
                }
                else if (ddlBankList.SelectedIndex > 0)
                {
                    Banks[0].FMID = (new FinanceMaster()).
                        GetFMID("Bank_VI",
                        String.Format(" LTRIM(RTRIM(PPSID)) = '{0}'",
                        ddlBankList.SelectedItem.Value)
                        );
                    Banks[0].PPSID = ddlBankList.SelectedItem.Value;
                    if (!string.IsNullOrEmpty(Banks[0].PPSID.Trim()))
                        FillBank(Banks[0]);
                }

            }

            return Banks;
        }

        private FinanceMaster.Bank FillBank(FinanceMaster.Bank Bank)
        {
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
                    String.Format("SELECT * FROM BANK_VI WHERE PPSID = {0}", Bank.PPSID),
                    connection))
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
                                if (tb.Rows[0]["BankDesc"].ToString().Trim() != "")
                                    Bank.Name = tb.Rows[0]["BankDesc"].ToString().Trim();
                                if (tb.Rows[0]["Address1"].ToString().Trim() != "")
                                    Bank.Address = tb.Rows[0]["Address1"].ToString().Trim();
                                if (tb.Rows[0]["Address2"].ToString().Trim() != "")
                                    Bank.Address = tb.Rows[0]["Address2"].ToString().Trim();
                                if (tb.Rows[0]["City"].ToString().Trim() != "")
                                    Bank.City = tb.Rows[0]["City"].ToString().Trim();
                                if (tb.Rows[0]["State"].ToString().Trim() != "")
                                    Bank.State = tb.Rows[0]["State"].ToString().Trim();
                                if (tb.Rows[0]["ZipCode"].ToString().Trim() != "")
                                    Bank.Zip = tb.Rows[0]["ZipCode"].ToString().Trim();
                            }
                        }
                    }
                }
            }

            return Bank;
        }

        protected void btnFinanceMaster_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                FinanceMaster FM = new FinanceMaster();
                string redirUrl = "";
                //NUMBER OF PAYMENTS
                var SelectedBtn = (int.Parse(((System.Web.UI.WebControls.Button)sender).ID.ToString().Replace("btnFM", "")) + 1).ToString();
                var Params = Session["FMQuoteCode"].ToString() + "&Name=" +
                    (txtFirstName.Text.Trim() + txtLastName.Text.Trim() == "" ? txtLastName.Text.Trim() : txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim()) +
                    "&Address1=" + txtMailingAddress.Text.Trim() + "&City=" + txtCity.Text.Trim() + "&State=" + txtState.Text.Trim().Replace(".", "") +
                    "&Zip=" + txtZipCode.Text.Trim() + "&HomePhone=" + txtBusinessPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") +
                    "&WorkPhone=" + txtMobilePhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") +
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
                double chg = (totpay + Convert.ToDouble(DownPayment)) - double.Parse(txtLiaTotalPremium.Text.Trim().Replace("$", "")); //TotalPremium
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

        private void SendFinanceMasterInfoToPPS(TaskControl.HomeOwners taskControl)
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
            System.Data.DataTable dtfm = fm.GetFinanceMasterByTaskcontrolIDAllData(taskControl.TaskControlID);

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

        protected void BtnPremiumFinance_Click(object sender, EventArgs e)
        {
            if (double.Parse(txtLiaTotalPremium.Text.Replace("$", "")) < 500)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = String.Concat(
                    @"<p>Option not available for <b>Total Premium</b> less than $500.</p>",
                    @"<p>Current <b>Total Premium:</b> $",
                    txtLiaTotalPremium.Text, @"</p>"
                );
            }
            else
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
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
        protected void ddlBankList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBankListSelected.Text = GetBankListInfo(ddlBankList.SelectedItem.Value);
            lblBankListSelected3.Text = lblBankListSelected.Text;
            if (sender != String.Empty)// && e != EventArgs.Empty)
                mpeBankList.Show();
        }

        protected void ddlBankList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBankListSelected2.Text = GetBankListInfo(ddlBankList2.SelectedItem.Value);
            lblBankListSelected4.Text = lblBankListSelected2.Text;
            if (sender != String.Empty)// && e != EventArgs.Empty)
                mpeBankList.Show();
        }

        protected void btnBankList_Click(object sender, EventArgs e)
        {
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

                        var tb = new System.Data.DataTable();
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
                                if (tb.Rows[0]["Address2"].ToString().Trim() != "")
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

        private double GetAgentCommision(string AgentID)
        {
            try
            {
                double ComRate = 0;
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
                        String.Format(@"SELECT top 1 * FROM CommissionAgent 
                                        WHERE RTRIM(LTRIM(AgentID)) = RTRIM(LTRIM({0})) 
                                        and RTRIM(LTRIM(PolicyClassID)) = RTRIM(LTRIM({1})) 
                                        AND PolicyType = {2}
                                        order by EffectiveDate desc", "'" + AgentID + "'", "'25'", "'" + txtPolicyType.Text.Trim() + "'"), connection))
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
                                    ComRate = double.Parse(tb.Rows[0]["CommissionRate"].ToString());

                                    ComRate = (ComRate / 100);
                                }
                                else
                                {
                                    ComRate = 0;
                                }
                            }
                        }
                    }
                }

                return ComRate;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private double CalculateAgentCommision(string AgentID, double TotalPremium)
        {
            try
            {
                double Result = TotalPremium;
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
                        String.Format(@"SELECT top 1 * FROM CommissionAgent 
                                        WHERE RTRIM(LTRIM(AgentID)) = RTRIM(LTRIM({0})) 
                                        and RTRIM(LTRIM(PolicyClassID)) = RTRIM(LTRIM({1})) 
                                        AND PolicyType = {2}
                                        order by EffectiveDate desc", "'" + AgentID + "'", "'25'", "'" + txtPolicyType.Text.Trim() + "'"), connection))
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

        //FINDS USER EMAIL BY username or (FirstName + Lastname)
        private string GetUserEmail(string User)
        {
            string Result = "";
            try
            {
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
                    //using (SqlCommand command = new SqlCommand(
                    //    String.Format(@"SELECT top 1 UE.* FROM UserEmails UE
                    //                    left outer join AuthenticatedUser AU on UE.Username = AU.UserName
                    //                    WHERE RTRIM(LTRIM(CONCAT(RTRIM(FirstName),' ',RTRIM(LastName)))) =  RTRIM(LTRIM({0}))
                    //                    OR RTRIM(LTRIM(UE.Username)) =   RTRIM(LTRIM({0}))"
                    //                   , "'" + User.Trim() + "'"), connection))

                    using (SqlCommand command = new SqlCommand(
                       String.Format(@"SELECT top 1 AU.* FROM 
                                        AuthenticatedUser AU
                                        WHERE AccountDisable = 0 and RTRIM(LTRIM(CONCAT(RTRIM(AU.FirstName),' ',RTRIM(AU.LastName)))) =  RTRIM(LTRIM({0}))"
                                       , "'" + User.Trim() + "'"), connection))
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
                                    Result = tb.Rows[0]["Email"].ToString();
                                }
                                else
                                {
                                    Result = "";
                                }
                            }
                        }
                    }
                }

                return Result;
            }
            catch (Exception)
            {
                return Result;
            }
        }

        private static System.Data.DataTable GetUsernameByUserID(int UserID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("UserID",
                SqlDbType.Int, 0, UserID.ToString(),
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
                dt = exec.GetQuery("GetUserNameByID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

        protected void btnSentToPPS_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
				
                SendPolicyToPPS(taskControl.TaskControlID);
            }
            catch (Exception exp)
            {
                LogError(exp);
                DisableControls();
                FillTextControl();
                lblRecHeader.Text = "The policy was sent to pps, please verify.";
                mpeSeleccion.Show();
            }
        }

        protected void imgCalendarInspectionDate_Click(object sender, EventArgs e)
        {
            if (Calendar1.Visible == false)
                Calendar1.Visible = true;
            else
                Calendar1.Visible = false;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtInspectionDate.Text = Calendar1.SelectedDate.ToShortDateString();
            Calendar1.Visible = false;
        }

        protected void btnCloneQuote_Click(object sender, EventArgs e)
        {
            try
            {
                CloneQuote();

            }
            catch (Exception exp)
            {
                LogError(exp);
                DisableControls();
                FillTextControl();
                lblRecHeader.Text = "The policy was sent to pps, please verify.";
                mpeSeleccion.Show();
            }
        }

        private void CloneQuote()
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            EPolicy.TaskControl.HomeOwners taskControlQuote = new EPolicy.TaskControl.HomeOwners(false);

            int tcID = taskControl.TaskControlID;
            taskControlQuote = taskControl;
			taskControlQuote.liaLimit = taskControl.liaLimit;
			taskControlQuote.otherStructuresType = taskControl.otherStructuresType;

            taskControlQuote.Prospect = taskControl.Prospect;
            taskControlQuote.Prospect.FirstName = taskControl.Prospect.FirstName;
            taskControlQuote.Prospect.LastName1 = taskControl.Prospect.LastName1;
            taskControlQuote.Prospect.LastName2 = taskControl.Prospect.LastName2;
            taskControlQuote.Prospect.HomePhone = taskControl.Prospect.HomePhone;
            taskControlQuote.Prospect.WorkPhone = taskControl.Prospect.WorkPhone;
            taskControlQuote.Prospect.Cellular = taskControl.Prospect.Cellular;
            taskControlQuote.Prospect.Email = taskControl.Prospect.Email;

            taskControlQuote = taskControl;
            taskControlQuote.Mode = 1; //ADD
            taskControlQuote.TaskControlID = 0;
            taskControlQuote.isQuote = true;//.IsPolicy = false;
            taskControlQuote.IsEndorsement = false;
            taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Home Owners Policy Quote"));
            taskControlQuote.EntryDate = DateTime.Now;

            taskControlQuote.Customer = taskControl.Customer;
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

            taskControlQuote.GrossTax = 0.00;
            taskControlQuote.liaTotalPremium = 0.0;
            taskControlQuote.totalPremium = 0.0;
            taskControlQuote.TotalPremium = 0.0;
            taskControlQuote.premium = 0.0;
            taskControlQuote.premium1 = 0.0;
            taskControlQuote.premium2 = 0.0;
            taskControlQuote.premium3 = 0.0;
            taskControlQuote.premium4 = 0.0;
            taskControlQuote.renewalNo = "";

            taskControlQuote.approved = false;
            taskControlQuote.submitted = false;
            taskControlQuote.rejected = false;

            taskControlQuote.Mode = 1; //ADD
            taskControlQuote.Term = taskControl.Term;
            taskControlQuote.TCIDQuotes = 0;

            Session.Remove("TaskControl");
            Session.Add("TaskControl", taskControlQuote);

            RemoveSessionLookUp();
            Response.Redirect("HomeOwners.aspx");
        }

        protected bool CheckHomeOwnersExistPPS()
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
                bool Found = false;
                SqlConnection cn = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
                //SqlConnection cn = new SqlConnection(@"Data Source=192.168.1.22\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
                System.Data.DataTable table = new System.Data.DataTable();
                System.Data.DataTable table2 = new System.Data.DataTable();

                using (var con = cn)
                {
                    using (var cmd = new SqlCommand("sproc_ConsumeXMLePPS-HOMEOWNER_Verify", con))
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

                    using (var cmd1 = new SqlCommand("sproc_ConsumeXMLePPS-HOMEOWNER_Verify_Coverage", con))
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
                }
                if (GetPPSHasEndorsementHO() == true && CheckHomeOwnersExistePPS() == true && table.Rows.Count > 0)
                {
                    int taskcontrolIDEPPS = GetTaskControlByPolicyTypeAndPolicyNoAndSuffix(txtPolicyToRenewType.Text.Trim(), txtPolicyNoToRenew.Text.Trim(), txtPolicyNoToRenewSuffix.Text.Trim());
                    string totalPremiumEPPS = GetPolicyPremiumByTaskcontrolID(taskcontrolIDEPPS);
                    if(!(totalPremiumEPPS.Contains(".00")))
                    {
                        totalPremiumEPPS = totalPremiumEPPS + ".00";
                    }
                    string totalPremiumPPS = table.Rows[0]["Premium"].ToString().Remove(table.Rows[0]["Premium"].ToString().Length - 2);
                    if (totalPremiumPPS == totalPremiumEPPS.Trim())
                    {
                        
                        FillRenHOEPPS(taskcontrolIDEPPS);
                        taskControl.Customer.Description = table.Rows[0]["Client"].ToString();
                        txtFirstName.Text = table.Rows[0]["FirstName"].ToString().Trim();
                        txtLastName.Text = table.Rows[0]["LastName"].ToString().Trim();
                        txtMailingAddress.Text = table.Rows[0]["Maddr1"].ToString().Trim();
                        txtMailingAddress2.Text = table.Rows[0]["Maddr2"].ToString().Trim();
                        txtZipCode.Text = table.Rows[0]["Mzip"].ToString().Trim();
                        txtCity.Text = table.Rows[0]["Mcity"].ToString().Trim();
                        txtState.Text = table.Rows[0]["Mstate"].ToString().Trim();

                        txtPhysicalAddress1.Text = table.Rows[0]["Raddr1"].ToString().Trim();
                        txtPhysicalAddress2.Text = table.Rows[0]["Raddr2"].ToString().Trim();
                        txtZipCode2.Text = table.Rows[0]["Rzip"].ToString().Trim();
                        txtCity2.Text = table.Rows[0]["Rcity"].ToString().Trim();
                        txtState2.Text = table.Rows[0]["Rstate"].ToString().Trim();

                        txtBusinessPhone.Text = table.Rows[0]["Wphone"].ToString().Trim();
                        txtMobilePhone.Text = table.Rows[0]["Cphone"].ToString().Trim();

                        txtEmail.Text = table.Rows[0]["Eaddr"].ToString().Trim();
                        lblRecHeader.Text = "Policy successfully found.";
                        mpeSeleccion.Show();
                        return true;
                    }
                    else
                    {
                        System.Data.DataTable dt = GetHomeOwnersByTaskControlID(taskcontrolIDEPPS, false);
                        taskControl.bank2 = dt.Rows[0]["Bank2"].ToString();
                        taskControl.loanNo2 = dt.Rows[0]["LoanNo2"].ToString();
                        taskControl.typeOfInterest = dt.Rows[0]["TypeOfInterest"].ToString();
                        taskControl.mortgageeBilled = bool.Parse(dt.Rows[0]["MortgageeBilled"].ToString());
                        taskControl.constructionYear = dt.Rows[0]["ConstructionYear"].ToString();
                        taskControl.ifYes = dt.Rows[0]["Ifyes"].ToString();
                        taskControl.livingArea = dt.Rows[0]["LivingArea"].ToString();
                        taskControl.porshcesDeck = dt.Rows[0]["Porsches_Deck"].ToString();
                        taskControl.roofDwelling = dt.Rows[0]["RoofDwelling"].ToString();
                        taskControl.residence = dt.Rows[0]["Residence"].ToString();
                        taskControl.propertyType = dt.Rows[0]["PropertyType"].ToString();
                        taskControl.propertyForm = dt.Rows[0]["PropertyForm"].ToString();
                        taskControl.losses3Year = bool.Parse(dt.Rows[0]["Losses3Years"].ToString());
                        taskControl.otherStructuresType = dt.Rows[0]["OtherStructuresType"].ToString();
                        taskControl.isPropShuttered = bool.Parse(dt.Rows[0]["IsPropShuttered"].ToString());
                        taskControl.roofOverhang = dt.Rows[0]["RoofOverhang"].ToString();
                        taskControl.isUpgraded = bool.Parse(dt.Rows[0]["isUpgraded"].ToString());
                        taskControl.additionalStructure = dt.Rows[0]["AdditionalStructure"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["AdditionalStructure"].ToString());
                        taskControl.comments = dt.Rows[0]["Comments"].ToString();
                        taskControl.occupation = dt.Rows[0]["Occupation"].ToString();
                        taskControl.office = dt.Rows[0]["Office"].ToString();
                        taskControl.approved = dt.Rows[0]["Approved"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["Approved"].ToString());
                        taskControl.comment = dt.Rows[0]["Comment"].ToString();
                        taskControl.submitted = dt.Rows[0]["Submitted"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["Submitted"].ToString());
                        taskControl.rejected = dt.Rows[0]["Rejected"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["Rejected"].ToString());
                        taskControl.Island = dt.Rows[0]["Island"].ToString();
                        taskControl.BankPPSID = dt.Rows[0]["BankPPSID"].ToString();
                        taskControl.BankPPSID2 = dt.Rows[0]["BankPPSID2"].ToString();
                        taskControl.GrossTax = double.Parse(dt.Rows[0]["GrossTax"].ToString());
                        taskControl.Inspector = dt.Rows[0]["Inspector"].ToString();
                        taskControl.InspectionDate = dt.Rows[0]["InspectionDate"].ToString();
                        taskControl.isRenew = true;
                        taskControl.DiscountsHomeOwners = double.Parse(dt.Rows[0]["DiscountsHomeOwners"].ToString());
                        taskControl.TypeOfInsuredID = int.Parse(dt.Rows[0]["TypeOfInsuredID"].ToString());
                        Found = true;

                        taskControl.Customer.Description = table.Rows[0]["Client"].ToString();
                        FillTextControl();

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

                        txtMailingAddress.Text = table.Rows[0]["Maddr1"].ToString().Trim();
                        txtMailingAddress2.Text = table.Rows[0]["Maddr2"].ToString().Trim();
                        txtZipCode.Text = table.Rows[0]["Mzip"].ToString().Trim();
                        txtCity.Text = table.Rows[0]["Mcity"].ToString().Trim();
                        txtState.Text = table.Rows[0]["Mstate"].ToString().Trim();

                        txtPhysicalAddress1.Text = table.Rows[0]["Raddr1"].ToString().Trim();
                        txtPhysicalAddress2.Text = table.Rows[0]["Raddr2"].ToString().Trim();
                        txtZipCode2.Text = table.Rows[0]["Rzip"].ToString().Trim();
                        txtCity2.Text = table.Rows[0]["Rcity"].ToString().Trim();
                        txtState2.Text = table.Rows[0]["Rstate"].ToString().Trim();

                        txtBusinessPhone.Text = table.Rows[0]["Wphone"].ToString().Trim();
                        txtMobilePhone.Text = table.Rows[0]["Cphone"].ToString().Trim();

                        txtEmail.Text = table.Rows[0]["Eaddr"].ToString().Trim();

                        ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue(GetAgentByCarsID(table.Rows[0]["BrokerID"].ToString().Trim())));
                        ddlBankList.SelectedIndex = ddlBankList.Items.IndexOf(ddlBankList.Items.FindByValue(table.Rows[0]["PayeeID"].ToString().Trim()));
                        lblBankListSelected2.Text = GetBankListInfo(ddlBankList.SelectedItem.Value);


                        txtFirstName.Text = table.Rows[0]["FirstName"].ToString().Trim();
                        txtLastName.Text = table.Rows[0]["LastName"].ToString().Trim();
                        if (table.Rows[0]["Island"].ToString().Trim() == "1")
                            ddlIsland.SelectedIndex = 3;
                        else if (table.Rows[0]["Island"].ToString().Trim() == "2")
                            ddlIsland.SelectedIndex = 1;
                        else if (table.Rows[0]["Island"].ToString().Trim() == "3")
                            ddlIsland.SelectedIndex = 2;
                        else
                            ddlIsland.SelectedIndex = 0;
                        txtLoanNo.Text = table.Rows[0]["LoanNo"].ToString().Trim();
                        //txtLoanNo2.Text = table.Rows[0]["LoanNo2"].ToString().Trim();
                        if (table.Rows[0]["Families"].ToString().Trim() == "1")
                        {
                            ddlNumOfFamilies.SelectedItem.Value = "1 Family";
                        }
                        else if (table.Rows[0]["Families"].ToString().Trim() == "2")
                        {
                            ddlNumOfFamilies.SelectedItem.Value = "2 Families";
                        }
                        else if (table.Rows[0]["Families"].ToString().Trim() == "3")
                        {
                            ddlNumOfFamilies.SelectedItem.Value = "3 Families";
                        }
                        else if (table.Rows[0]["Families"].ToString().Trim() == "4")
                        {
                            ddlNumOfFamilies.SelectedItem.Value = "4 Families";
                        }
                        else
                        {
                            ddlNumOfFamilies.SelectedIndex = 0;
                        }
                        txtConstructionYear.Text = table.Rows[0]["YearBuilt"].ToString().Trim();
                        if (table.Rows[0]["Storeys"].ToString().Trim() == "1")
                        {
                            ddlNumberOfStories.SelectedItem.Value = "1 Story";
                        }
                        else if (table.Rows[0]["Storeys"].ToString().Trim() == "2")
                        {
                            ddlNumberOfStories.SelectedItem.Value = "2 Stories";
                        }
                        else if (table.Rows[0]["Storeys"].ToString().Trim() == "3")
                        {
                            ddlNumberOfStories.SelectedItem.Value = "3 Stories";
                        }
                        else if (table.Rows[0]["Storeys"].ToString().Trim() == "4")
                        {
                            ddlNumberOfStories.SelectedItem.Value = "4 Stories";
                        }
                        else
                        {
                            ddlNumberOfStories.SelectedIndex = 0;
                        }

                        txtComments.Text = table.Rows[0]["Descrip"].ToString().Trim().Substring(0,50);

                        if (table.Rows[0]["Construction"].ToString().Trim() == "1" || table.Rows[0]["Construction"].ToString().Trim() == "21" || table.Rows[0]["Construction"].ToString().Trim() == "29")
                        {
                            ddlConstructionType.SelectedItem.Value = "Frame Construction";
                        }
                        else if (table.Rows[0]["Construction"].ToString().Trim() == "8" || table.Rows[0]["Construction"].ToString().Trim() == "28" || table.Rows[0]["Construction"].ToString().Trim() == "36")
                        {
                            ddlConstructionType.SelectedItem.Value = "Mixed Construction";
                        }
                        else if (table.Rows[0]["Construction"].ToString().Trim() == "3" || table.Rows[0]["Construction"].ToString().Trim() == "23" || table.Rows[0]["Construction"].ToString().Trim() == "31")
                        {
                            ddlConstructionType.SelectedItem.Value = "Brick, Stone, Masonry Construction";
                        }

                    }

                    if (table2.Rows.Count > 0)
                    {
                        double windPerilsDeduc = 0.0, earthquakeDeduc = 0.00;
                        int windPerilsDeducInt = 0, earthquakeDeducInt = 0;
                        for (int i = 0; i < table2.Rows.Count; i++)
                        {
                            switch (table2.Rows[i]["ReinsAsl"].ToString().Trim())
                            {
                                case "28040":
                                    if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                    {
                                        txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                        if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                            txtLimit2.Text = "$0.00";
                                        else
                                            txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                        if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                            txtLimit3.Text = "$0.00";
                                        else
                                            txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                        if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                            txtLimit4.Text = "$0.00";
                                        else
                                            txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                    }

                                    break;
                                case "29040":
                                    if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                    {
                                        txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                        if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                            txtLimit2.Text = "$0.00";
                                        else
                                            txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                        if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                            txtLimit3.Text = "$0.00";
                                        else
                                            txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                        if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                            txtLimit4.Text = "$0.00";
                                        else
                                            txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                        if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                        {
                                            windPerilsDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                            windPerilsDeducInt = int.Parse(windPerilsDeduc.ToString());
                                        }
                                    }
                                    break;
                                case "30120":
                                    if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                    {
                                        txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                        if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                            txtLimit2.Text = "$0.00";
                                        else
                                            txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                        if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                            txtLimit3.Text = "$0.00";
                                        else
                                            txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                        if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                            txtLimit4.Text = "$0.00";
                                        else
                                            txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                        if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                        {
                                            earthquakeDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                            earthquakeDeducInt = int.Parse(earthquakeDeduc.ToString());
                                        }
                                    }
                                    break;
                                case "33040": //liability
                                    if (table2.Rows[i]["Lim4"].ToString().Trim() == "0.0000")
                                    {
                                        ddlLiaLimit.SelectedIndex = 0;
                                    }
                                    else if (table2.Rows[i]["Lim4"].ToString().Trim() == "50000.0000")
                                    {
                                        ddlLiaLimit.SelectedIndex = 1;
                                    }
                                    else if (table2.Rows[i]["Lim4"].ToString().Trim() == "100000.0000")
                                    {
                                        ddlLiaLimit.SelectedIndex = 2;
                                    }
                                    else if (table2.Rows[i]["Lim4"].ToString().Trim() == "300000.0000")
                                    {
                                        ddlLiaLimit.SelectedIndex = 3;
                                    }
                                    else if (table2.Rows[i]["Lim4"].ToString().Trim() == "500000.0000")
                                    {
                                        ddlLiaLimit.SelectedIndex = 4;
                                    }
                                    else if (table2.Rows[i]["Lim4"].ToString().Trim() == "1000000.0000")
                                    {
                                        ddlLiaLimit.SelectedIndex = 5;
                                    }
                                    else
                                    {
                                        ddlLiaLimit.SelectedIndex = 0;
                                    }
                                    break;
                                case "14010":
                                    if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                    {
                                        txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                        if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                            txtLimit2.Text = "$0.00";
                                        else
                                            txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                        if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                            txtLimit3.Text = "$0.00";
                                        else
                                            txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                        if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                            txtLimit4.Text = "$0.00";
                                        else
                                            txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                    }
                                    break;
                                case "15021":
                                    if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                    {
                                        txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                        if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                            txtLimit2.Text = "$0.00";
                                        else
                                            txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                        if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                            txtLimit3.Text = "$0.00";
                                        else
                                            txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                        if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                            txtLimit4.Text = "$0.00";
                                        else
                                            txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                    }
                                    break;
                                case "16021":
                                    if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                    {
                                        txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                        if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                            txtLimit2.Text = "$0.00";
                                        else
                                            txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                        if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                            txtLimit3.Text = "$0.00";
                                        else
                                            txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                        if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                            txtLimit4.Text = "$0.00";
                                        else
                                            txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                        if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                        {
                                            windPerilsDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                            windPerilsDeducInt = int.Parse(windPerilsDeduc.ToString());
                                        }
                                    }
                                    break;
                                case "17120":
                                    if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                    {
                                        txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                        if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                            txtLimit2.Text = "$0.00";
                                        else
                                            txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                        if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                            txtLimit3.Text = "$0.00";
                                        else
                                            txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                        if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                            txtLimit4.Text = "$0.00";
                                        else
                                            txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                        if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                        {
                                            earthquakeDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                            earthquakeDeducInt = int.Parse(earthquakeDeduc.ToString());
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }

                        if (windPerilsDeducInt == 0 && earthquakeDeducInt == 0)
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                        }
                        else if (windPerilsDeducInt != 0 && earthquakeDeducInt != 0)
                        {
                            if (windPerilsDeducInt == 10 && earthquakeDeducInt == 10)
                            {
                                ddlCatastropheDeduc.SelectedItem.Value = "10% Wind / 10% EQ";
                            }

                            else if (windPerilsDeducInt == 3 && earthquakeDeducInt == 5)
                            {
                                ddlCatastropheDeduc.SelectedItem.Value = "3% Wind / 5% EQ";
                            }

                            else if (windPerilsDeducInt == 5 && earthquakeDeducInt == 5)
                            {
                                ddlCatastropheDeduc.SelectedItem.Value = "5% Wind / 5% EQ";
                            }

                            else
                            {
                                ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                            }
                        }
                        else if (windPerilsDeducInt == 0 && earthquakeDeducInt != 0)
                        {
                            if (earthquakeDeducInt == 10)
                            {
                                ddlCatastropheDeduc.SelectedItem.Value = "10% EQ";
                            }

                            else if (earthquakeDeducInt == 5)
                            {
                                ddlCatastropheDeduc.SelectedItem.Value = "5% EQ";
                            }

                            else
                            {
                                ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                            }
                        }
                        else
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                        }
                    }
                    lblRecHeader.Text = "Policy successfully found.";
                    mpeSeleccion.Show();
                    return Found;
                }
                else if (table.Rows.Count > 0)
                {
                    taskControl.isRenew = true;
                    Found = true;

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

                    txtMailingAddress.Text = table.Rows[0]["Maddr1"].ToString().Trim();
                    txtMailingAddress2.Text = table.Rows[0]["Maddr2"].ToString().Trim();
                    txtZipCode.Text = table.Rows[0]["Mzip"].ToString().Trim();
                    txtCity.Text = table.Rows[0]["Mcity"].ToString().Trim();
                    txtState.Text = table.Rows[0]["Mstate"].ToString().Trim();

                    txtPhysicalAddress1.Text = table.Rows[0]["Raddr1"].ToString().Trim();
                    txtPhysicalAddress2.Text = table.Rows[0]["Raddr2"].ToString().Trim();
                    txtZipCode2.Text = table.Rows[0]["Rzip"].ToString().Trim();
                    txtCity2.Text = table.Rows[0]["Rcity"].ToString().Trim();
                    txtState2.Text = table.Rows[0]["Rstate"].ToString().Trim();

                    txtBusinessPhone.Text = table.Rows[0]["Wphone"].ToString().Trim();
                    txtMobilePhone.Text = table.Rows[0]["Cphone"].ToString().Trim();

                    txtEmail.Text = table.Rows[0]["Eaddr"].ToString().Trim();

                    ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue(GetAgentByCarsID(table.Rows[0]["BrokerID"].ToString().Trim())));
                    ddlBankList.SelectedIndex = ddlBankList.Items.IndexOf(ddlBankList.Items.FindByValue(table.Rows[0]["PayeeID"].ToString().Trim()));
                    lblBankListSelected2.Text = GetBankListInfo(ddlBankList.SelectedItem.Value);


                    txtFirstName.Text = table.Rows[0]["FirstName"].ToString().Trim();
                    txtLastName.Text = table.Rows[0]["LastName"].ToString().Trim();
                    if (table.Rows[0]["Island"].ToString().Trim() == "1")
                        ddlIsland.SelectedIndex = 3;
                    else if (table.Rows[0]["Island"].ToString().Trim() == "2")
                        ddlIsland.SelectedIndex = 1;
                    else if (table.Rows[0]["Island"].ToString().Trim() == "3")
                        ddlIsland.SelectedIndex = 2;
                    else
                        ddlIsland.SelectedIndex = 0;
                    txtLoanNo.Text = table.Rows[0]["LoanNo"].ToString().Trim();
                    //txtLoanNo2.Text = table.Rows[0]["LoanNo2"].ToString().Trim();
                    if (table.Rows[0]["Families"].ToString().Trim() == "1")
                    {
                        ddlNumOfFamilies.SelectedItem.Value = "1 Family";
                    }
                    else if (table.Rows[0]["Families"].ToString().Trim() == "2")
                    {
                        ddlNumOfFamilies.SelectedItem.Value = "2 Families";
                    }
                    else if (table.Rows[0]["Families"].ToString().Trim() == "3")
                    {
                        ddlNumOfFamilies.SelectedItem.Value = "3 Families";
                    }
                    else if (table.Rows[0]["Families"].ToString().Trim() == "4")
                    {
                        ddlNumOfFamilies.SelectedItem.Value = "4 Families";
                    }
                    else
                    {
                        ddlNumOfFamilies.SelectedIndex = 0;
                    }
                    txtConstructionYear.Text = table.Rows[0]["YearBuilt"].ToString().Trim();
                    if (table.Rows[0]["Storeys"].ToString().Trim() == "1")
                    {
                        ddlNumberOfStories.SelectedItem.Value = "1 Story";
                    }
                    else if (table.Rows[0]["Storeys"].ToString().Trim() == "2")
                    {
                        ddlNumberOfStories.SelectedItem.Value = "2 Stories";
                    }
                    else if (table.Rows[0]["Storeys"].ToString().Trim() == "3")
                    {
                        ddlNumberOfStories.SelectedItem.Value = "3 Stories";
                    }
                    else if (table.Rows[0]["Storeys"].ToString().Trim() == "4")
                    {
                        ddlNumberOfStories.SelectedItem.Value = "4 Stories";
                    }
                    else
                    {
                        ddlNumberOfStories.SelectedIndex = 0;
                    }

                    txtComments.Text = table.Rows[0]["Descrip"].ToString().Trim().Substring(0,50);

                    if (table.Rows[0]["Construction"].ToString().Trim() == "1" || table.Rows[0]["Construction"].ToString().Trim() == "21" || table.Rows[0]["Construction"].ToString().Trim() == "29")
                    {
                        ddlConstructionType.SelectedItem.Value = "Frame Construction";
                    }
                    else if (table.Rows[0]["Construction"].ToString().Trim() == "8" || table.Rows[0]["Construction"].ToString().Trim() == "28" || table.Rows[0]["Construction"].ToString().Trim() == "36")
                    {
                        ddlConstructionType.SelectedItem.Value = "Mixed Construction";
                    }
                    else if (table.Rows[0]["Construction"].ToString().Trim() == "3" || table.Rows[0]["Construction"].ToString().Trim() == "23" || table.Rows[0]["Construction"].ToString().Trim() == "31")
                    {
                        ddlConstructionType.SelectedItem.Value = "Brick, Stone, Masonry Construction";
                    }

                }

                if (table2.Rows.Count > 0)
                {
                    double windPerilsDeduc = 0.0, earthquakeDeduc = 0.00;
                    int windPerilsDeducInt = 0, earthquakeDeducInt = 0;
                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        switch (table2.Rows[i]["ReinsAsl"].ToString().Trim())
                        {
                            case "28040":
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                {
                                    txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                    if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                        txtLimit2.Text = "$0.00";
                                    else
                                        txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                    if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                        txtLimit3.Text = "$0.00";
                                    else
                                        txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                    if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                        txtLimit4.Text = "$0.00";
                                    else
                                        txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                }

                                break;
                            case "29040":
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                {
                                    txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                    if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                        txtLimit2.Text = "$0.00";
                                    else
                                        txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                    if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                        txtLimit3.Text = "$0.00";
                                    else
                                        txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                    if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                        txtLimit4.Text = "$0.00";
                                    else
                                        txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                    if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                    {
                                        windPerilsDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                        windPerilsDeducInt = int.Parse(windPerilsDeduc.ToString());
                                    }
                                }
                                break;
                            case "30120":
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                {
                                    txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                    if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                        txtLimit2.Text = "$0.00";
                                    else
                                        txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                    if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                        txtLimit3.Text = "$0.00";
                                    else
                                        txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                    if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                        txtLimit4.Text = "$0.00";
                                    else
                                        txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                    if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                    {
                                        earthquakeDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                        earthquakeDeducInt = int.Parse(earthquakeDeduc.ToString());
                                    }
                                }
                                break;
                            case "33040": //liability
                                if (table2.Rows[i]["Lim4"].ToString().Trim() == "0.0000")
                                {
                                    ddlLiaLimit.SelectedIndex = 0;
                                }
                                else if (table2.Rows[i]["Lim4"].ToString().Trim() == "50000.0000")
                                {
                                    ddlLiaLimit.SelectedIndex = 1;
                                }
                                else if (table2.Rows[i]["Lim4"].ToString().Trim() == "100000.0000")
                                {
                                    ddlLiaLimit.SelectedIndex = 2;
                                }
                                else if (table2.Rows[i]["Lim4"].ToString().Trim() == "300000.0000")
                                {
                                    ddlLiaLimit.SelectedIndex = 3;
                                }
                                else if (table2.Rows[i]["Lim4"].ToString().Trim() == "500000.0000")
                                {
                                    ddlLiaLimit.SelectedIndex = 4;
                                }
                                else if (table2.Rows[i]["Lim4"].ToString().Trim() == "1000000.0000")
                                {
                                    ddlLiaLimit.SelectedIndex = 5;
                                }
                                else
                                {
                                    ddlLiaLimit.SelectedIndex = 0;
                                }
                                break;
                            case "14010":
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                {
                                    txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                    if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                        txtLimit2.Text = "$0.00";
                                    else
                                        txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                    if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                        txtLimit3.Text = "$0.00";
                                    else
                                        txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                    if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                        txtLimit4.Text = "$0.00";
                                    else
                                        txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                }
                                break;
                            case "15021":
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                {
                                    txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                    if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                        txtLimit2.Text = "$0.00";
                                    else
                                        txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                    if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                        txtLimit3.Text = "$0.00";
                                    else
                                        txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                    if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                        txtLimit4.Text = "$0.00";
                                    else
                                        txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                }
                                break;
                            case "16021":
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                {
                                    txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                    if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                        txtLimit2.Text = "$0.00";
                                    else
                                        txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                    if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                        txtLimit3.Text = "$0.00";
                                    else
                                        txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                    if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                        txtLimit4.Text = "$0.00";
                                    else
                                        txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                    if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                    {
                                        windPerilsDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                        windPerilsDeducInt = int.Parse(windPerilsDeduc.ToString());
                                    }
                                }
                                break;
                            case "17120":
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.00" && table2.Rows[i]["Lim1"].ToString().Trim() != "")
                                {
                                    txtLimit1.Text = table2.Rows[i]["Lim1"].ToString().Trim();
                                    if (table2.Rows[i]["Lim5"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim5"].ToString().Trim() == "")
                                        txtLimit2.Text = "$0.00";
                                    else
                                        txtLimit2.Text = table2.Rows[i]["Lim5"].ToString().Trim();

                                    if (table2.Rows[i]["Lim2"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim2"].ToString().Trim() == "")
                                        txtLimit3.Text = "$0.00";
                                    else
                                        txtLimit3.Text = table2.Rows[i]["Lim2"].ToString().Trim();

                                    if (table2.Rows[i]["Lim3"].ToString().Trim() == "0.00" || table2.Rows[i]["Lim3"].ToString().Trim() == "")
                                        txtLimit4.Text = "$0.00";
                                    else
                                        txtLimit4.Text = table2.Rows[i]["Lim3"].ToString().Trim();
                                    if (table2.Rows[i]["Deductible"].ToString().Trim() != "")
                                    {
                                        earthquakeDeduc = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim()) * 100.0;
                                        earthquakeDeducInt = int.Parse(earthquakeDeduc.ToString());
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (windPerilsDeducInt == 0 && earthquakeDeducInt == 0)
                    {
                        ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                    }
                    else if (windPerilsDeducInt != 0 && earthquakeDeducInt != 0)
                    {
                        if (windPerilsDeducInt == 10 && earthquakeDeducInt == 10)
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "10% Wind / 10% EQ";
                        }

                        else if (windPerilsDeducInt == 3 && earthquakeDeducInt == 5)
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "3% Wind / 5% EQ";
                        }

                        else if (windPerilsDeducInt == 5 && earthquakeDeducInt == 5)
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "5% Wind / 5% EQ";
                        }

                        else
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                        }
                    }
                    else if (windPerilsDeducInt == 0 && earthquakeDeducInt != 0)
                    {
                        if (earthquakeDeducInt == 10)
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "10% EQ";
                        }

                        else if (earthquakeDeducInt == 5)
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "5% EQ";
                        }

                        else
                        {
                            ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                        }
                    }
                    else
                    {
                        ddlCatastropheDeduc.SelectedItem.Value = "No Coverage";
                    }
                }
                lblRecHeader.Text = "Policy successfully found.";
                mpeSeleccion.Show();
                return Found;
            }
            catch (Exception exp)
            {
                
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
                return false;
            }
        }

        private void Recalculate()
        {
            try
            {
                if (ddlCatastropheDeduc.SelectedItem.Value != "" && ddlConstructionType.SelectedItem.Text.Trim() != "" && ddlRoofDwelling.SelectedItem.Text.Trim() != "" && ddlPropertyType.SelectedItem.Text.Trim() != "" && ddlLiaLimit.SelectedItem.Text.Trim() != "" && txtFirstName.Text.Trim() != "")
                {
                    if (txtBank.Text.ToString().Trim() != "")
                    {
                        if (txtLoanNo.Text.ToString().Trim() != "")
                        {
                            connectWithExcel();
                        }
                        else
                        {
                            lblRecHeader.Text = "Please enter Loan No";
                            mpeSeleccion.Show();
                        }
                    }
                    else
                    {
                        connectWithExcel();
                    }

                }

                else
                {
                    lblRecHeader.Text = "Please fill the red fields";
                    mpeSeleccion.Show();
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }

        private double CalculateDiscount(double premium)
        {
            double discount;
            discount =double.Parse(ddlDiscount.SelectedItem.Text)/100;

            return (discount * premium) + premium;
        }
        private string ClearString(string premium)
        {
            premium = premium.Replace("$", "").Replace(",", "").Replace("NO COVERAGE", "");

            if (premium != "")
                return premium;
            else
                return "0.0";
        }


        private decimal CalculateDiscount(decimal premium)
        {
            decimal discount;
            discount = decimal.Parse(ddlDiscount.SelectedItem.Text) / 100;

            return (discount * premium) + premium;
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

            System.Data.DataTable dt;
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            dt = exec.GetQuery("GetAgentByCarsIDBND", xmlDoc);
            string rtAgentID = "0";

            if (dt.Rows.Count > 0)
            {
                rtAgentID = dt.Rows[0]["dAgentID"].ToString();
            }
            return rtAgentID.ToString();
        }

        protected void ddlDiscount_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Recalculate();
        }

        private System.Data.DataTable GetCustomerCommentsByCustomerNo(int CustomerNo)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, CustomerNo.ToString(), ref cookItems);

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
                System.Data.DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetCustomerCommentsByCustomerNo", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		
        //protected void txtEmail_TextChanged(object sender, EventArgs e)
        //{
        //    mpeEmailVerification.Show();
        //}
        //protected void txtEmailVerification_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtEmail.Text.Trim().ToUpper() == txtEmailVerification.Text.Trim().ToUpper())
        //    {
        //        mpeEmailVerification.Hide();
        //        //txtEmailVerification.Text = "";
        //        //return;
        //    }
        //    else
        //    {
        //        txtEmail.Text = "";
        //        txtEmailVerification.Text = "";
        //        mpeEmailVerification.Hide();

        //        lblRecHeader.Text = "The emails did not match. Please try again.";
        //        mpeSeleccion.Show();
        //    }
        //}

        private void FillRenHOEPPS(int TaskcontrolID)
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            System.Data.DataTable dt = GetHomeOwnersByTaskControlID(TaskcontrolID, false);
            System.Data.DataTable dtAgent = GetAgentIDByTaskControlID(TaskcontrolID);
            System.Data.DataTable dtDates = GetEffExpDateByTaskControlID(TaskcontrolID);
            taskControl.bank2 = dt.Rows[0]["Bank2"].ToString();
            taskControl.bank = dt.Rows[0]["Bank"].ToString();
            taskControl.loanNo = dt.Rows[0]["LoanNo"].ToString();
            taskControl.loanNo2 = dt.Rows[0]["LoanNo2"].ToString();
            taskControl.typeOfInterest = dt.Rows[0]["TypeOfInterest"].ToString();
            taskControl.mortgageeBilled = bool.Parse(dt.Rows[0]["MortgageeBilled"].ToString());
            taskControl.catastropheCov = dt.Rows[0]["CatastropheCov"].ToString();
            taskControl.catastropheDeduc = dt.Rows[0]["CatastropheDeduc"].ToString();
            taskControl.windstormDeduc = dt.Rows[0]["WindstormDeduc"].ToString();
            taskControl.allOtherPerDeduc = dt.Rows[0]["AllOtherPerDeduc"].ToString();
            taskControl.constructionType = dt.Rows[0]["ConstructionType"].ToString();
            taskControl.constructionYear = dt.Rows[0]["ConstructionYear"].ToString();
            taskControl.numOfStories = dt.Rows[0]["NumOfStories"].ToString();
            taskControl.numOfFamilies = dt.Rows[0]["NumOfFamilies"].ToString();
            taskControl.ifYes = dt.Rows[0]["Ifyes"].ToString();
            taskControl.livingArea = dt.Rows[0]["LivingArea"].ToString();
            taskControl.porshcesDeck = dt.Rows[0]["Porsches_Deck"].ToString();
            taskControl.roofDwelling = dt.Rows[0]["RoofDwelling"].ToString();
            taskControl.earthquakeDeduc = dt.Rows[0]["EarthquakeDeduc"].ToString();
            taskControl.residence = dt.Rows[0]["Residence"].ToString();
            taskControl.propertyType = dt.Rows[0]["PropertyType"].ToString();
            taskControl.propertyForm = dt.Rows[0]["PropertyForm"].ToString();
            taskControl.losses3Year = bool.Parse(dt.Rows[0]["Losses3Years"].ToString());
            taskControl.otherStructuresType = dt.Rows[0]["OtherStructuresType"].ToString();
            taskControl.isPropShuttered = bool.Parse(dt.Rows[0]["IsPropShuttered"].ToString());
            taskControl.roofOverhang = dt.Rows[0]["RoofOverhang"].ToString();
            taskControl.autoPolicy = bool.Parse(dt.Rows[0]["AutoPolicy"].ToString());
            taskControl.autoPolicyNo = dt.Rows[0]["AutoPolicyNo"].ToString();
            taskControl.limit1 = double.Parse(dt.Rows[0]["Limit1"].ToString());
            taskControl.aopDed1 = double.Parse(dt.Rows[0]["AOPDed1"].ToString());
            taskControl.windstormDed1 = double.Parse(dt.Rows[0]["WindstormDed1"].ToString());
            taskControl.windstormDedPer1 = dt.Rows[0]["WindstormDedPer1"].ToString();
            taskControl.earthquakeDed1 = double.Parse(dt.Rows[0]["EarthquakeDed1"].ToString());
            taskControl.earthquakeDedper1 = dt.Rows[0]["EarthquakeDedper1"].ToString();
            taskControl.coinsurance1 = dt.Rows[0]["Coinsurance1"].ToString();
            taskControl.premium1 = double.Parse(dt.Rows[0]["Premium1"].ToString());
            taskControl.limit2 = double.Parse(dt.Rows[0]["Limit2"].ToString());
            taskControl.aopDed2 = double.Parse(dt.Rows[0]["AOPDed2"].ToString());
            taskControl.windstormDed2 = double.Parse(dt.Rows[0]["WindstormDed2"].ToString());
            taskControl.windstormDedPer2 = dt.Rows[0]["WindstormDedPer2"].ToString();
            taskControl.earthquakeDed2 = double.Parse(dt.Rows[0]["EarthquakeDed2"].ToString());
            taskControl.earthquakeDedper2 = dt.Rows[0]["EarthquakeDedper2"].ToString();
            taskControl.coinsurance2 = dt.Rows[0]["Coinsurance2"].ToString();
            taskControl.premium2 = double.Parse(dt.Rows[0]["Premium2"].ToString());
            taskControl.limit3 = double.Parse(dt.Rows[0]["Limit3"].ToString());
            taskControl.aopDed3 = double.Parse(dt.Rows[0]["AOPDed3"].ToString());
            taskControl.windstormDed3 = double.Parse(dt.Rows[0]["WindstormDed3"].ToString());
            taskControl.windstormDedPer3 = dt.Rows[0]["WindstormDedPer3"].ToString();
            taskControl.earthquakeDed3 = double.Parse(dt.Rows[0]["EarthquakeDed3"].ToString());
            taskControl.earthquakeDedper3 = dt.Rows[0]["EarthquakeDedper3"].ToString();
            taskControl.coinsurance3 = dt.Rows[0]["Coinsurance3"].ToString();
            taskControl.premium3 = double.Parse(dt.Rows[0]["Premium3"].ToString());
            taskControl.limit4 = double.Parse(dt.Rows[0]["Limit4"].ToString());
            taskControl.aopDed4 = double.Parse(dt.Rows[0]["AOPDed4"].ToString());
            taskControl.windstormDed4 = double.Parse(dt.Rows[0]["WindstormDed4"].ToString());
            taskControl.windstormDedPer4 = dt.Rows[0]["WindstormDedPer4"].ToString();
            taskControl.earthquakeDed4 = double.Parse(dt.Rows[0]["EarthquakeDed4"].ToString());
            taskControl.earthquakeDedper4 = dt.Rows[0]["EarthquakeDedper4"].ToString();
            taskControl.coinsurance4 = dt.Rows[0]["Coinsurance4"].ToString();
            taskControl.premium4 = double.Parse(dt.Rows[0]["Premium4"].ToString());
            taskControl.totalLimit = double.Parse(dt.Rows[0]["TotalLimit"].ToString());
            taskControl.totalWindstorm = double.Parse(dt.Rows[0]["TotalWindstorm"].ToString());
            taskControl.totalEarthquake = double.Parse(dt.Rows[0]["TotalEarthquake"].ToString());
            taskControl.totalPremium = double.Parse(dt.Rows[0]["TotalPremium"].ToString());
            taskControl.liaPropertyType = dt.Rows[0]["LiaPropertyType"].ToString();
            taskControl.liaPolicyType = dt.Rows[0]["LiaPolicyType"].ToString();
            taskControl.liaNumOfFamilies = dt.Rows[0]["LiaNumOfFamilies"].ToString();
            taskControl.liaLimit = double.Parse(dt.Rows[0]["LiaLimit"].ToString());
            taskControl.liaMedicalPayments = double.Parse(dt.Rows[0]["LiaMedicalPayments"].ToString());
            taskControl.liaPremium = double.Parse(dt.Rows[0]["LiaPremium"].ToString());
            taskControl.premium = double.Parse(dt.Rows[0]["Premium"].ToString());
            taskControl.liaTotalPremium = double.Parse(dt.Rows[0]["LiaTotalPremium"].ToString());
            taskControl.isUpgraded = bool.Parse(dt.Rows[0]["isUpgraded"].ToString());
            taskControl.additionalStructure = dt.Rows[0]["AdditionalStructure"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["AdditionalStructure"].ToString());
            taskControl.comments = dt.Rows[0]["Comments"].ToString();
            taskControl.occupation = dt.Rows[0]["Occupation"].ToString();
            taskControl.office = dt.Rows[0]["Office"].ToString();
            taskControl.approved = dt.Rows[0]["Approved"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["Approved"].ToString());
            taskControl.comment = dt.Rows[0]["Comment"].ToString();
            taskControl.submitted = dt.Rows[0]["Submitted"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["Submitted"].ToString());
            taskControl.rejected = dt.Rows[0]["Rejected"].ToString() == "" ? false : bool.Parse(dt.Rows[0]["Rejected"].ToString());
            taskControl.Island = dt.Rows[0]["Island"].ToString();
            taskControl.BankPPSID = dt.Rows[0]["BankPPSID"].ToString();
            taskControl.BankPPSID2 = dt.Rows[0]["BankPPSID2"].ToString();
            taskControl.GrossTax = double.Parse(dt.Rows[0]["GrossTax"].ToString());
            taskControl.Inspector = dt.Rows[0]["Inspector"].ToString();
            taskControl.InspectionDate = dt.Rows[0]["InspectionDate"].ToString();
            taskControl.PreviousPolicy = dt.Rows[0]["PreviousPolicy"].ToString();
            taskControl.isRenew = true;
            taskControl.DiscountsHomeOwners = double.Parse(dt.Rows[0]["DiscountsHomeOwners"].ToString());
            taskControl.TypeOfInsuredID = int.Parse(dt.Rows[0]["TypeOfInsuredID"].ToString());
            taskControl.Agent = dtAgent.Rows[0]["AgentID"].ToString();
            FillTextControl();
            if (DateTime.Parse(DateTime.Now.ToShortDateString()) <= DateTime.Parse(dtDates.Rows[0]["ExpirationDate"].ToString()))
            {
                txtEffectiveDate.Text = DateTime.Parse(dtDates.Rows[0]["ExpirationDate"].ToString()).ToShortDateString();
                txtExpirationDate.Text = DateTime.Parse(dtDates.Rows[0]["ExpirationDate"].ToString()).AddYears(1).ToShortDateString();
            }
            else
            {
                txtEffectiveDate.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString();
                txtExpirationDate.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).AddYears(1).ToShortDateString();
            }
            taskControl.renewalNo = txtPolicyNoToRenew.Text.Replace("HOM", "").Replace("INC", "");
        }

        private System.Data.DataTable GetHomeOwnersByTaskControlID(int TaskControlID, bool isQuote)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[2];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                                SqlDbType.Int, 0, TaskControlID.ToString(),
                                ref cookItems);

                DbRequestXmlCooker.AttachCookItem("isQuote",
                                SqlDbType.Bit, 0, isQuote.ToString(),
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
                dt = Executor.GetQuery("GetHomeOwnersByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        private System.Data.DataTable GetEffExpDateByTaskControlID(int TaskControlID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                                SqlDbType.Int, 0, TaskControlID.ToString(),
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
                dt = Executor.GetQuery("GetEffExpDateByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        private int GetTaskControlByPolicyTypeAndPolicyNoAndSuffix(string policyType, string policyNo, string suffix)
        {
            if (suffix == "")
            {
                suffix = "00";
            }
            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[3];

                DbRequestXmlCooker.AttachCookItem("PolicyType",
                                SqlDbType.VarChar, 3, policyType.ToString(),
                                ref cookItems);

                DbRequestXmlCooker.AttachCookItem("PolicyNo",
                                SqlDbType.VarChar, 11, policyNo.ToString(),
                                ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Sufijo",
                                SqlDbType.VarChar, 2, suffix.ToString(),
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
                dt = Executor.GetQuery("GetTaskControlByPolicyNoAndSuffix", xmlDoc);
                if (dt.Rows.Count > 0)
                {
                    return int.Parse(dt.Rows[0]["TaskControlID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        private System.Data.DataTable GetAgentIDByTaskControlID(int TaskControlID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                                SqlDbType.Int, 0, TaskControlID.ToString(),
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
                dt = Executor.GetQuery("GetAgentIDByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }
		
		protected string GetUserAgencyDesc()
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            System.Data.DataTable Agency = GetAgency();
            string agency = "";

            if (cp.AgentVI.ToString().Trim() == null ||cp.AgentVI.ToString().Trim() == "000")
            {
                agency = "GUARDIAN INSURANCE";
            }
            else
            {
                for (int i = 0; i < Agency.Rows.Count; i++)
                {
                    if (Agency.Rows[i]["AgentID"].ToString().Trim() == cp.AgentVI.ToString().Trim())
                    {
                        agency = Agency.Rows[i]["AgentDesc"].ToString().Trim();
                    }
                }
            }
            return agency;
        }


        protected void BtnAddInsured_Click(object sender, EventArgs e)
        {
            try
            {
                BtnAddInsured.Text = "ADD INSURED";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                if (taskControl.TaskControlID == 0)
                {
                    if (Session["AdditionalInsured"] != null)
                    {
                        dt = (System.Data.DataTable)Session["AdditionalInsured"];
                    }
                    else
                    {
                        dt.Columns.Add("HomeOwnersAdditionalInsuredID");
                        dt.Columns.Add("TaskControlID");
                        dt.Columns.Add("InsuredName");
                        dt.Columns.Add("InsuredLastName");
                    }

                    DataRow row = dt.NewRow();
                    row["HomeOwnersAdditionalInsuredID"] = "0";
                    row["TaskControlID"] = taskControl.TaskControlID;
                    row["InsuredName"] = txtAdditionalInsuredName.Text.Trim().ToUpper();
                    row["InsuredLastName"] = txtAdditionalInsuredLastName.Text.Trim().ToUpper();

                    dt.Rows.Add(row);

                    Session.Add("AdditionalInsured", dt);
                    GridInsured.DataSource = dt;
                    GridInsured.DataBind();
                    GridInsured.Visible = true;
                }
                else
                {
                    int ID = 0;

                    if (txtAdditionalInsuredID.Text != "")
                    {
                     ID =    int.Parse(txtAdditionalInsuredID.Text);
                    }

                    AddHomeOwnersAdditionalInsuredQuote(ID, taskControl.TaskControlID, txtAdditionalInsuredName.Text.Trim().ToUpper(), txtAdditionalInsuredLastName.Text.Trim().ToUpper());
                    dt =  GetHomeOwnersAdditionalInsuredQuoteByTaskControlID(taskControl.TaskControlID);

                    GridInsured.DataSource = dt;
                    GridInsured.DataBind();
                    GridInsured.Visible = true;
                
                }

                txtAdditionalInsuredID.Text = "";
                txtAdditionalInsuredName.Text = "";
                txtAdditionalInsuredLastName.Text = "";
            }
            catch (Exception exp)
            {
                throw new Exception("Error Adding Additional Insured", exp);
            }
        }

        protected void SaveAdditionalInsured(System.Data.DataTable dt, string type)
        {
            EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];
            if (dt != null)
            {
                if (type == "Quote")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddHomeOwnersAdditionalInsuredQuote(int.Parse(dt.Rows[i]["HomeOwnersAdditionalInsuredID"].ToString()), taskControl.TaskControlID, dt.Rows[i]["InsuredName"].ToString(),dt.Rows[i]["InsuredLastName"].ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddHomeOwnersAdditionalInsuredPolicy(0, taskControl.TaskControlID, dt.Rows[i]["InsuredName"].ToString(), dt.Rows[i]["InsuredLastName"].ToString());
                    }
                }
                
                
            }


        }

        protected void GridInsured_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }


        protected void AddHomeOwnersAdditionalInsuredQuote(int HomeOwnersAdditionalInsuredID, int TaskControlID, string InsuredName, string InsuredLastName)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[4];

                DbRequestXmlCooker.AttachCookItem("HomeOwnersAdditionalInsuredID",
                                SqlDbType.Int, 0, HomeOwnersAdditionalInsuredID.ToString(),
                                ref cookItems);
                 DbRequestXmlCooker.AttachCookItem("TaskControlID",
                                SqlDbType.Int, 0, TaskControlID.ToString(),
                                ref cookItems);
                 DbRequestXmlCooker.AttachCookItem("InsuredName",
                                SqlDbType.VarChar, 100, InsuredName.ToString(),
                                ref cookItems);
                 DbRequestXmlCooker.AttachCookItem("InsuredLastName",
                                 SqlDbType.VarChar, 100, InsuredLastName.ToString(),
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

                dt = Executor.GetQuery("AddHomeOwnersAdditionalInsuredQuote", xmlDoc);
               
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }
        protected void AddHomeOwnersAdditionalInsuredPolicy(int HomeOwnersAdditionalInsuredID, int TaskControlID, string InsuredName, string InsuredLastName)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[4];

                DbRequestXmlCooker.AttachCookItem("HomeOwnersAdditionalInsuredID",
                                SqlDbType.Int, 0, HomeOwnersAdditionalInsuredID.ToString(),
                                ref cookItems);
                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                               SqlDbType.Int, 0, TaskControlID.ToString(),
                               ref cookItems);
                DbRequestXmlCooker.AttachCookItem("InsuredName",
                               SqlDbType.VarChar, 100, InsuredName.ToString(),
                               ref cookItems);
                DbRequestXmlCooker.AttachCookItem("InsuredLastName",
                               SqlDbType.VarChar, 100, InsuredLastName.ToString(),
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

                dt = Executor.GetQuery("AddHomeOwnersAdditionalInsuredPolicy", xmlDoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }


        protected System.Data.DataTable GetHomeOwnersAdditionalInsuredQuoteByTaskControlID(int TaskControlID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                               SqlDbType.Int, 0, TaskControlID.ToString(),
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

                dt = Executor.GetQuery("GetHomeOwnersAdditionalInsuredQuoteByTaskControlID", xmlDoc);

                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        protected System.Data.DataTable GetHomeOwnersAdditionalInsuredPolicyByTaskControlID(int TaskControlID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                               SqlDbType.Int, 0, TaskControlID.ToString(),
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

                dt = Executor.GetQuery("GetHomeOwnersAdditionalInsuredPolicyByTaskControlID", xmlDoc);

                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        protected void DeleteHomeOwnersAdditionalInsuredQuote(int HomeOwnersAdditionalInsuredID)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("HomeOwnersAdditionalInsuredID",
                               SqlDbType.Int, 0, HomeOwnersAdditionalInsuredID.ToString(),
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

                 Executor.GetQuery("DeleteHomeOwnersAdditionalInsuredQuote", xmlDoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }
       

        protected void GridInsured_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                EPolicy.TaskControl.HomeOwners taskControl = (EPolicy.TaskControl.HomeOwners)Session["TaskControl"];

                for (int i = 0; i < GridInsured.Columns.Count - 1; i++)
                {
                    GridInsured.Columns[i].Visible = true;
                }

                int index = Int32.Parse(e.CommandArgument.ToString());
                GridViewRow row = GridInsured.Rows[index];
                GridInsured.BorderColor = Color.Red;

                switch (e.CommandName)
                {
                    case "Select": //Edit

                        BtnAddInsured.Text = "UPDATE";
                        txtAdditionalInsuredName.Text = row.Cells[3].Text.Trim(); //GridInsured.SelectedRow.Cells[1].Text.Trim();
                        txtAdditionalInsuredLastName.Text = row.Cells[4].Text.Trim(); //GridInsured.SelectedRow.Cells[1].Text.Trim(); 
                        txtAdditionalInsuredID.Text = row.Cells[1].Text.Trim();

                        break;
                    case "Delete":


                        //if (lblModifyInsured.Visible == true)
                        //{
                        //    throw new Exception("Please save or cancel selected Insured first.");
                        //}

                        DeleteHomeOwnersAdditionalInsuredQuote(int.Parse(row.Cells[1].Text.Trim()));

                        int indexd = Int32.Parse(e.CommandArgument.ToString());
                        if (GridInsured.SelectedIndex == -1)
                        {
                            GridInsured.SelectedIndex = 0;
                        }


                        System.Data.DataTable dt = null;
                        dt =  GetHomeOwnersAdditionalInsuredQuoteByTaskControlID(taskControl.TaskControlID);

                        GridInsured.DataSource = dt;
                        GridInsured.DataBind();
                        GridInsured.Visible = true;

                            //Funcion de borrar additional insured por su ID 
                        
                        break;
                    default: //Page
                        break;
                }

            }
            catch (Exception exp)
            {
                throw new Exception("Could not cook items.", exp);
            }

        }

        protected void GridInsured_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        
        }

        protected bool CheckHomeOwnersExistePPS()
        {
            try
            {
                if (GetTaskControlByPolicyTypeAndPolicyNoAndSuffix(txtPolicyToRenewType.Text, txtPolicyNoToRenew.Text, txtPolicyNoToRenewSuffix.Text) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
                return false;
            }

        }

        private bool GetPPSHasEndorsementHO()
        {
            string Message = "";
            bool HasEndorment = false;

            try
            {
                SqlConnection sqlConnection1 = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=TestGic;User ID=urclaims;password=3G@TD@t!1");
                //SqlConnection sqlConnection1 = new SqlConnection(@"Data Source=192.168.1.22\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
                //string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                //SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                System.Data.DataTable PPSPolicy = new System.Data.DataTable();
                System.Data.DataTable dt = new System.Data.DataTable();

                cmd.CommandText = "sproc_CheckHasEndorsement";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                cmd.Parameters.Clear();
                string PolicyNo;
                if (txtPolicyNoToRenewSuffix.Text.Trim() == "00" || txtPolicyNoToRenewSuffix.Text.Trim() == "")
                {
                    PolicyNo = txtPolicyToRenewType.Text + txtPolicyNoToRenew.Text.Trim();
                }
                else
                {
                    PolicyNo = txtPolicyToRenewType.Text + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim();
                }
                cmd.Parameters.AddWithValue("@PolicyID", PolicyNo);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                sqlConnection1.Close();

                if (PPSPolicy.Rows.Count > 0)
                {
                    //Validar si en PPS hay endoso.
                    if (PPSPolicy.Rows[0][0].ToString() == "ENDOSO")
                    {
                        Message = "This Policy already has endorsement in PPS any further endorsements must be done from PPS.";
                        HasEndorment = true;
                    }
                }
                else
                {
                    Message = "Error Checking Endorsment in PPS, Please Try Again.";
                    HasEndorment = false;
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }

            return HasEndorment;
        }

        private string GetPolicyPremiumByTaskcontrolID(int TaskcontrolID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskcontrolID",
                                SqlDbType.Int, 0, TaskcontrolID.ToString(),
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
                dt = Executor.GetQuery("GetPolicyPremiumByTaskcontrolID", xmlDoc);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["TotalPremium"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        private System.Data.DataTable GetPolicyQuoteByTaskControlID(int TaskcontrolID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskcontrolID",
                                SqlDbType.Int, 0, TaskcontrolID.ToString(),
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
                dt = Executor.GetQuery("GetPolicyQuoteByTaskControlID", xmlDoc);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }
    }
}