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
    public partial class Bonds : System.Web.UI.Page
    {
        private string NAMECONVENTION = "";
        private string PolicyNumber = "";

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
                if (!cp.IsInRole("BONDS") && !cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("BONDSVI"))
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

            if (cp.IsInRole("BONDS"))
            {
                LabelSignature.Visible = false;
                ddlSignature.Visible = false;
            }
            else if (cp.IsInRole("BONDSVI") || cp.IsInRole("ADMINISTRATOR"))
            {
                LabelSignature.Visible = true;
                ddlSignature.Visible = true;
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



                    EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

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
                        DataTable dtObligee = EPolicy.LookupTables.LookupTables.GetTable("BondsObligee");
                        DataTable dtPolicyClass = EPolicy.LookupTables.LookupTables.GetTable("PolicyClass");
                        DataTable dtDealer = EPolicy.Login.Login.GetGroupDealerByUserID(userID);
                        DataTable dtTypeOfBond = GetTypeOfBond();

                        for (int y = dtObligee.Rows.Count - 1; y >= 0; y--)
                        {
                            if (dtObligee.Rows[y]["Island"].ToString() == "1" && cp.IsInRole("BONDSVI"))
                            {
                                dtObligee.Rows[y].Delete();
                            }
                            else if (dtObligee.Rows[y]["Island"].ToString() == "2" && cp.IsInRole("BONDS"))
                            {
                                dtObligee.Rows[y].Delete();
                            }
                        }

                        for (int y = dtTypeOfBond.Rows.Count - 1; y >= 0; y--)
                        {
                            if (dtTypeOfBond.Rows[y]["Island"].ToString() == "1" && cp.IsInRole("BONDSVI"))
                            {
                                dtTypeOfBond.Rows[y].Delete();
                            }
                            else if (dtTypeOfBond.Rows[y]["Island"].ToString() == "2" && cp.IsInRole("BONDS"))
                            {
                                dtTypeOfBond.Rows[y].Delete();
                            }
                        }



                        List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>();
                        items.Add(new System.Web.UI.WebControls.ListItem("Individual", "1"));
                        items.Add(new System.Web.UI.WebControls.ListItem("Corporate", "2"));
                        items.Add(new System.Web.UI.WebControls.ListItem("DBA", "3"));
                        ddlType.Items.AddRange(items.ToArray());

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

                        //PolicyClass
                        ddlPolicyClass.DataSource = dtPolicyClass;
                        ddlPolicyClass.DataTextField = "PolicyClassDesc";
                        ddlPolicyClass.DataValueField = "PolicyClassID";
                        ddlPolicyClass.DataBind();
                        ddlPolicyClass.SelectedValue = taskControl.PolicyClassID.ToString();
                        ddlPolicyClass.Items.Insert(0, "");

                        //Agency
                        ddlAgency.DataSource = dtAgency;
                        ddlAgency.DataTextField = "AgencyDesc";
                        ddlAgency.DataValueField = "AgencyID";
                        ddlAgency.DataBind();
                        ddlAgency.SelectedIndex = -1;
                        ddlAgency.Items.Insert(0, "");

                        //Agent
                        if (cp.IsInRole("BONDSVI") || cp.IsInRole("ADMINISTRATOR"))
                        {
                            ddlAgent.DataSource = dtAgentVI;
                            ddlAgent.DataTextField = "AgentDesc";
                            ddlAgent.DataValueField = "AgentID";
                            ddlAgent.DataBind();
                            ddlAgent.SelectedIndex = -1;
                            ddlAgent.Items.Insert(0, "");
                        }
                        else if (cp.IsInRole("BONDS"))
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


                        //Ciudad
                        //ddlCiudad.DataSource = dtCiudad;
                        //ddlCiudad.DataTextField = "Ciudad";
                        //ddlCiudad.DataValueField = "ZipCode";
                        //ddlCiudad.DataBind();
                        //ddlCiudad.SelectedIndex = -1;
                        //ddlCiudad.Items.Insert(0, "");

                        //Ciudad Fisica
                        //ddlPhyCity.DataSource = dtCiudad;
                        //ddlPhyCity.DataTextField = "Ciudad";
                        //ddlPhyCity.DataValueField = "ZipCode";
                        //ddlPhyCity.DataBind();
                        //ddlPhyCity.SelectedIndex = -1;
                        //ddlPhyCity.Items.Insert(0, "");

                        //ZipCode
                        //ddlZip.DataSource = dtZipCode;
                        //ddlZip.DataTextField = "Zipcode";
                        //ddlZip.DataValueField = "ZipCode";
                        //ddlZip.DataBind();
                        //ddlZip.SelectedIndex = -1;
                        //ddlZip.Items.Insert(0, "");

                        //Zipcode Fisico
                        //ddlPhyZipCode.DataSource = dtZipCode;
                        //ddlPhyZipCode.DataTextField = "Zipcode";
                        //ddlPhyZipCode.DataValueField = "ZipCode";
                        //ddlPhyZipCode.DataBind();
                        //ddlPhyZipCode.SelectedIndex = -1;
                        //ddlPhyZipCode.Items.Insert(0, "");

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

                        //Type Of Bonds
                        ddlTypeBonds.DataSource = dtTypeOfBond;
                        ddlTypeBonds.DataTextField = "Description";
                        ddlTypeBonds.DataValueField = "TypeOfBondID";
                        ddlTypeBonds.DataBind();
                        ddlTypeBonds.SelectedIndex = -1;
                        ddlTypeBonds.Items.Insert(0, "");

                        //Obligee
                        ddlObligee.DataSource = dtObligee;
                        ddlObligee.DataTextField = "ObligeeDesc";
                        ddlObligee.DataValueField = "ObligeeID";
                        ddlObligee.DataBind();
                        ddlObligee.SelectedIndex = -1;
                        ddlObligee.Items.Insert(0, "");

                        Session.Add("LookUpTables", "In");
                    }

                    MyAccordion.SelectedIndex = 0;
                    Accordion1.SelectedIndex = -1;
                    Accordion2.SelectedIndex = -1;
                    Accordion3.SelectedIndex = -1;

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

                }
                else
                {
                    if (Session["TaskControl"] != null)
                    {
                        EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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

        private void FillTextControl()
        {
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

            LblControlNo.Text = taskControl.TaskControlID.ToString().Trim();

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

            //DataTable dtAgentByUserID = GetAgentByUserID(cp.UserID.ToString());
            //string Agent = "000";

            //if (dtAgentByUserID.Rows.Count > 0)
            //{
            //    Agent = dtAgentByUserID.Rows[0]["AgentID"].ToString();
            //}

            //if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
            //{
            //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //        ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));
            //}
            //else
            //{
            //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //        ddlAgent.Items.FindByValue(Agent.Trim()));
            //}

            //if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp.UserID).ToString()).Contains("THOMAS"))
            //{
            //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(ddlAgent.Items.FindByValue("057"));
            //}
            //else
            //{
            //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(ddlAgent.Items.FindByValue("060"));
            //}

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

            //if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
            //{
            //    if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
            //    {
            //        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //            ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));
            //    }
            //    else
            //    {
            //        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //            ddlAgent.Items.FindByValue("000"));
            //    }

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
            //        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(ddlAgent.Items.FindByValue(Agent.Trim()));
            //    }
            //}

            //if (taskControl.Agent != "")
            //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //        ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));

            if (taskControl.Agency != "")
                ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                    ddlAgency.Items.FindByValue(taskControl.Agency));

            if (taskControl.Signature != "")
                ddlSignature.SelectedIndex = ddlSignature.Items.IndexOf(
                    ddlSignature.Items.FindByValue(taskControl.Signature));

            //if (taskControl.PaymentAmount != 0.0)
            //{
            //    if (ddlPaymentAmount.Items.IndexOf(ddlPaymentAmount.Items.FindByValue(taskControl.PaymentAmount.ToString())) != -1 && taskControl.PaymentAmount == 25000.0)
            //        ddlPaymentAmount.SelectedIndex = 1;
            //    if (ddlPaymentAmount.Items.IndexOf(ddlPaymentAmount.Items.FindByValue(taskControl.PaymentAmount.ToString())) != -1 && taskControl.PaymentAmount == 50000.0) 
            //        ddlPaymentAmount.SelectedIndex = 2;
            //}


            if (taskControl.Bank != "")
            {
                ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(taskControl.Bank));
            }

            if (taskControl.CompanyDealer != "")
            {
                ddlCompanyDealer.SelectedIndex = ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(taskControl.CompanyDealer));
            }

            //ddlCiudad.SelectedIndex = 0;
            //if (taskControl.Customer.City != "")
            //{
            //    for (int i = 0; ddlCiudad.Items.Count - 1 >= i; i++)
            //    {
            //        if (ddlCiudad.Items[i].Text.Trim() == taskControl.Customer.City.ToString().Trim())
            //        {
            //            ddlCiudad.SelectedIndex = i;
            //            i = ddlCiudad.Items.Count - 1;
            //        }
            //    }
            //}

            ddlCiudad.Text = taskControl.Customer.City.Trim();

            //if (taskControl.Customer.CityPhysical != "")
            //{
            //    ddlPhyCity.SelectedIndex = ddlPhyCity.Items.IndexOf(ddlPhyCity.Items.FindByValue(taskControl.Customer.CityPhysical.ToString()));
            //}


            //ddlPhyCity.SelectedIndex = 0;
            //if (taskControl.Customer.CityPhysical != "")
            //{
            //    for (int i = 0; ddlPhyCity.Items.Count - 1 >= i; i++)
            //    {
            //        if (ddlPhyCity.Items[i].Text.Trim() == taskControl.Customer.CityPhysical.ToString().Trim())
            //        {
            //            ddlPhyCity.SelectedIndex = i;
            //            i = ddlPhyCity.Items.Count - 1;
            //        }
            //    }
            //}

            ddlPhyCity.Text = taskControl.Customer.CityPhysical.Trim();

            //SetDDLValue(ddlCiudad, taskControl.Customer.City, "Text");
            //SetDDLValue(ddlZip, taskControl.Customer.ZipCode, "Text");
            //SetDDLValue(ddlPhyCity, taskControl.Customer.CityPhysical, "Text");
            //SetDDLValue(ddlPhyZipCode, taskControl.Customer.ZipPhysical, "Text");

            ddlZip.Text = taskControl.Customer.ZipCode;
            ddlCiudad.Text = taskControl.Customer.City;
            ddlPhyZipCode.Text = taskControl.Customer.ZipPhysical;
            ddlPhyCity.Text = taskControl.Customer.CityPhysical;

            //if (taskControl.Ren_Rei == "RE")
            //    LblStatus.Text = taskControl.Status.ToString() + "/Reinst.";
            //else
            //    LblStatus.Text = taskControl.Status;


            DataTable dtTask = new DataTable();

            //dtTask = GetTransactionAmount(taskControl.TaskControlID);

            //if (dtTask.Rows.Count > 0)

            ////se descomento porque aún no se ha creado método de pago

            //{
            //    double paidammount = 0;
            //    paidammount = taskControl.TotalPremium - double.Parse(dtTask.Rows[0]["TransactionAmmount"].ToString());

            //    if (paidammount == 0)
            //    {
            //        LblStatus.Text = taskControl.Status.Split("/"[0]).ToString() + "/Paid";
            //    }
            //    else
            //    {
            //        LblStatus.Text = taskControl.Status.Split("/"[0]).ToString() + "/Unpaid";
            //    }
            //}
            //else
            //{
            //    if (taskControl.Ren_Rei == "RE")
            //        LblStatus.Text = taskControl.Status.ToString() + "/Reinst.";
            //    else
            //        LblStatus.Text = taskControl.Status;
            //}


            LblControlNo.Text = taskControl.TaskControlID.ToString();
            TxtProspectNo.Text = taskControl.Customer.CustomerNo;

            TxtLicense.Text = taskControl.Customer.Licence;
            TxtOccupa.Text = taskControl.Customer.Occupation;
            //TxtBirthdate.Text = taskControl.Customer.Birthday;

            TxtFirstName.Text = taskControl.Customer.FirstName;
            TxtInitial.Text = taskControl.Customer.Initial;
            txtLastname1.Text = taskControl.Customer.LastName1;
            //txtLastname2.Text = taskControl.Customer.LastName2;
            txtCompanyName.Text = taskControl.Customer.LastName2;
            TxtAddrs1.Text = taskControl.Customer.Address1;
            TxtAddrs2.Text = taskControl.Customer.Address2;
            if (cp.IsInRole("BONDSVI"))
                TxtState.Text = taskControl.Customer.State == "" ? "St.Thomas" : taskControl.Customer.State;
            if (cp.IsInRole("BONDS") || cp.IsInRole("ADMINISTRATOR"))
                TxtState.Text = taskControl.Customer.State == "" ? "PR" : taskControl.Customer.State;
            TxtHomePhone.Text = taskControl.Customer.HomePhone;
            txtWorkPhone.Text = taskControl.Customer.JobPhone;
            TxtCellular.Text = taskControl.Customer.Cellular;
            txtEmail.Text = taskControl.Customer.Email;
            TxtPolicyNo.Text = taskControl.PolicyNo;

            if (TxtPolicyNo.Text.Trim() != "")
            {
                chkIsRenew.Visible = false;
                txtPolicyNoToRenew.Visible = false;
                btnVerifyBondInPPS.Visible = false;
                //TxtPolicyNo.Visible = true;
            }
            else
            {
                chkIsRenew.Visible = true;
                txtPolicyNoToRenew.Visible = true;
                btnVerifyBondInPPS.Visible = true;
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
                txtPremium.Text = taskControl.TotalPremium.ToString("###,###"); //Se descomento para que llenara el textbox y se pueda salvar la poliza sin cambiar el dropdown de premium
            else
                txtPremium.Text = taskControl.TotalPremium.ToString("$###,###.00");


            txtPhyAddress.Text = taskControl.Customer.AddressPhysical1;
            txtPhyAddress2.Text = taskControl.Customer.AddressPhysical2;

            if (cp.IsInRole("BONDSVI"))
                txtPhyState.Text = taskControl.Customer.State == "" ? "St.Thomas" : taskControl.Customer.State;
            if (cp.IsInRole("BONDS") || cp.IsInRole("ADMINISTRATOR"))
                txtPhyState.Text = taskControl.Customer.State == "" ? "PR" : taskControl.Customer.State;


            txtTotalPremium.Text = taskControl.TotalPremium.ToString();
            txtDescriptionBond.Text = taskControl.BondDescription.Trim();
            txtReqDocuments.Text = taskControl.BondRequiredDocuments.Trim();

            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();


            if (taskControl.TypeOfBond != 0)
                ddlTypeBonds.SelectedIndex = ddlTypeBonds.Items.IndexOf(
                    ddlTypeBonds.Items.FindByValue(taskControl.TypeOfBond.ToString()));

            //if (taskControl.Limits != "")
            //    ddlLimite.SelectedIndex = ddlLimite.Items.IndexOf(
            //        ddlLimite.Items.FindByText(taskControl.Limits.ToString()));

            txtPenalty.Text = taskControl.Limits;
            //txtObligee.Text = taskControl.Obligee;
            if (taskControl.Obligee != "0" && taskControl.Obligee.Trim() != "")
                ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                    ddlObligee.Items.FindByValue(taskControl.Obligee.ToString()));

            if (ddlObligee.SelectedItem.Text == "Autoridad de Acueductos y Alcantarillados" || ddlObligee.SelectedItem.Text == "Autoridad de Energía Eléctrica" ||
                ddlObligee.SelectedItem.Text == "Guarantee" || ddlObligee.SelectedItem.Text == "Lease" || ddlObligee.SelectedItem.Text == "Notary Public" ||
                ddlObligee.SelectedItem.Text == "Notary Public 2" || ddlObligee.SelectedItem.Text == "Financial Guarantee - Miscellaneous")
            {
                HideControls(txtAccNumber, true);
                HideControls(lblAccNumber, true);
                txtAccNumber.Text = taskControl.AccountNumber.ToString();
                divAcctNumber.Visible = true;

                if (ddlObligee.SelectedItem.Text == "Guarantee" || ddlObligee.SelectedItem.Text == "Lease" || ddlObligee.SelectedItem.Text == "Notary Public" ||
                    ddlObligee.SelectedItem.Text == "Notary Public 2" || ddlObligee.SelectedItem.Text == "Financial Guarantee - Miscellaneous")
                {
                    lblAccNumber.Text = "Obligee Description";
                }
                else
                {
                    lblAccNumber.Text = "Account Number";
                }
            }
            else
            {
                HideControls(txtAccNumber, false);
                HideControls(lblAccNumber, false);
                txtAccNumber.Text = taskControl.AccountNumber.ToString();
                divAcctNumber.Visible = false;
            }

            if (ddlObligee.SelectedItem.Text.Contains("ASC") || ddlObligee.SelectedItem.Text == "Hacienda - Marbetes")
            {
                HideControls(txtLocationName, true);
                HideControls(lblLocationName, true);
                txtLocationName.Text = taskControl.NombreEstacion.ToString();
                divLocationName.Visible = true;

                HideControls(txtCantidadPrestada, true);
                HideControls(lblCantidadPrestada, true);
                txtCantidadPrestada.Text = taskControl.CantidadPrestadaSolicitante.ToString();
                divCantidadPrestada.Visible = true;


            }
            else
            {
                HideControls(txtLocationName, false);
                HideControls(lblLocationName, false);
                txtLocationName.Text = taskControl.NombreEstacion.ToString();
                divLocationName.Visible = false;

                HideControls(txtCantidadPrestada, false);
                HideControls(lblCantidadPrestada, false);
                txtCantidadPrestada.Text = taskControl.CantidadPrestadaSolicitante.ToString();
                divCantidadPrestada.Visible = false;
            }

            txtPolicyNoToRenew.Text = taskControl.RenewalOfBnd;

            if (txtPolicyNoToRenew.Text.Trim() != "")
            {
                chkIsRenew.Checked = true;
            }
            else
            {
                chkIsRenew.Checked = false;
            }

            txtAccNumber.Text = taskControl.AccountNumber.ToString();

            txtCompanyName.Text = taskControl.CompanyName;
            txtEmail.Text = taskControl.Customer.Email;
            TxtCellular.Text = taskControl.Customer.Cellular;
            ddlType.SelectedValue = taskControl.CustomerType.ToString();



            if (taskControl.Customer.SocialSecurity.Trim() != "")
            {
                txtSocSec.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                txtSocSec.Text = new string('*', txtSocSec.Text.Trim().Length - 4) + txtSocSec.Text.Trim().Substring(txtSocSec.Text.Trim().Length - 4);
                MaskedEditExtender1.Mask = "???-??-9999";
            }
            else
                txtSocSec.Text = "";

            //if (txtCompanyName.Text.Trim() == "")
            //    chkIsBusinessAutoQuote.Checked = false;
            //else
            //    chkIsBusinessAutoQuote.Checked = true;
            if (cp.IsInRole("BONDSVI") && taskControl.BondsReqDocCollection.Rows.Count == 0)
            {
                DataRow myRow = taskControl.BondsReqDocCollection.NewRow();
                myRow["TaskControlID"] = "0";
                myRow["RequiredDocumentDesc"] = "Application";
                myRow["Checked"] = false;

                taskControl.BondsReqDocCollection.Rows.Add(myRow);
                taskControl.BondsReqDocCollection.AcceptChanges();

                DataRow myRow2 = taskControl.BondsReqDocCollection.NewRow();
                myRow2["TaskControlID"] = "0";
                myRow2["RequiredDocumentDesc"] = "Picture Identification";
                myRow2["Checked"] = false;

                taskControl.BondsReqDocCollection.Rows.Add(myRow2);
                taskControl.BondsReqDocCollection.AcceptChanges();

                DataRow myRow3 = taskControl.BondsReqDocCollection.NewRow();
                myRow3["TaskControlID"] = "0";
                myRow3["RequiredDocumentDesc"] = "Approval VI Government";
                myRow3["Checked"] = false;

                taskControl.BondsReqDocCollection.Rows.Add(myRow3);
                taskControl.BondsReqDocCollection.AcceptChanges();

                DataRow myRow4 = taskControl.BondsReqDocCollection.NewRow();
                myRow4["TaskControlID"] = "0";
                myRow4["RequiredDocumentDesc"] = "Signed Indemnity";
                myRow4["Checked"] = false;

                taskControl.BondsReqDocCollection.Rows.Add(myRow4);
                taskControl.BondsReqDocCollection.AcceptChanges();

                DataRow myRow5 = taskControl.BondsReqDocCollection.NewRow();
                myRow5["TaskControlID"] = "0";
                myRow5["RequiredDocumentDesc"] = "Current Financial Statements";
                myRow5["Checked"] = false;

                taskControl.BondsReqDocCollection.Rows.Add(myRow5);
                taskControl.BondsReqDocCollection.AcceptChanges();

                this.GridReqDocs.DataSource = taskControl.BondsReqDocCollection;
                this.GridReqDocs.DataBind();
                this.GridReqDocs.Visible = true;
            }

            FillReqDocsGrid();
            CustomerTypeSelection();

            if (cp.IsInRole("BONDSVI"))
            {
                Label1.Visible = false;
                ddlObligee.Visible = false;
                //ddlObligee.Enabled = false;
                LabelSignature.ForeColor = System.Drawing.Color.Red;
            }

            if (ddlObligee.SelectedItem.Value.ToString() == "12" || ddlObligee.SelectedItem.Value.ToString() == "13" || ddlObligee.SelectedItem.Value.ToString() == "14" ||
                ddlObligee.SelectedItem.Value.ToString() == "15" || ddlObligee.SelectedItem.Value.ToString() == "16" || ddlObligee.SelectedItem.Value.ToString() == "17" ||
                ddlObligee.SelectedItem.Value.ToString() == "18" || ddlObligee.SelectedItem.Value.ToString() == "19" || ddlObligee.SelectedItem.Value.ToString() == "20" ||
                ddlObligee.SelectedItem.Value.ToString() == "21")
            {
                LabelSignature.ForeColor = System.Drawing.Color.Red;
            }

            if (taskControl.TaskControlID != 0 && BtnSave.Text != "ISSUE BOND")
            {
                btnAdjuntar.Visible = true;
                btnAdjuntar.Enabled = true;
            }
        }

        public void DisableControls()
        {
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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
                ddlPrintOption.Visible = false;
                //btnGuardianPay.Visible = false;


                //for (int i = 0; i < dtPayment.Rows.Count; i++) // YA PAGO LA Poliza
                //{
                //    if (dtPayment.Rows[i]["Result"].ToString().Trim() == "Success")
                //    {
                //        btnPrintInvoice.Visible = false; //True alexis
                //        btnPrintPolicy.Visible = true;
                //        ddlPrintOption.Visible = false; // True alexis
                //        btnGuardianPay.Visible = false;

                //        //try
                //        //{

                //        //    if (taskControl.Mode == 4 && taskControl.Trams_FL == false)//&& btnCancel.Visible == false)
                //        //    {
                //        //        PolicyXML(taskControl.TaskControlID);

                //        //        taskControl.Trams_FL = true;
                //        //    }
                //        //}
                //        //catch(Exception exp)
                //        //{
                //        //    LogError(exp);
                //        //}


                //        break;
                //    }
                //    else
                //    {
                //        btnPrintInvoice.Visible = false;
                //        btnPrintPolicy.Visible = false; //False Alexis
                //        ddlPrintOption.Visible = false;
                //        btnGuardianPay.Visible = false;
                //    }
                //}

                //if (dtPayment.Rows.Count == 0)
                //{
                //    if (chkCash.Checked)
                //    {
                //        btnCashPayment.Enabled = true;
                //        btnCashPayment.Visible = true;
                //        btnCashPayment.Text = "Pay Cash: " + txtPremium.Text;

                //        btnGuardianPay.Visible = false;
                //    }
                //}
                //else
                //{
                //    if (chkCash.Checked)
                //    {
                //        btnCashPayment.Enabled = false;
                //        btnCashPayment.Visible = true;
                //        btnCashPayment.Text = "PAID";//"Pay Cash: " + txtPremium.Text;

                //        btnGuardianPay.Visible = false;
                //    }
                //}
            }


            btnAdd2.Visible = false;
            //btnNew.Visible = true;
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
                    btnPrintPolicy.Visible = false;
                    btnInvoice.Visible = false;
                    //btnIndemnityPolicy.Visible = false;

                }
                else
                {
                    //if (!lblBondFound.Visible)
                    //{
                    btnAcceptQuote.Visible = false;
                    btnEdit.Visible = false;
                    btnPreview.Visible = false;
                    //btnIndemnityQuote.Visible = false;
                    btnPrintPolicy.Visible = true;
                    //btnIndemnityPolicy.Visible = true;
                    btnInvoice.Visible = true;
                    //}
                }
            }

            BtnExit.Visible = true;
            BtnSave.Visible = false;
            btnCancel.Visible = false;
            //btnDelete.Visible = true;
            btnReinstallation.Visible = false;
            btnCancellation.Visible = false;

            //chkDefpaysix.Enabled = false;
            //chkDefpayfour.Enabled = false;
            //chkCredit.Visible = true;
            //chkDebit.Visible = true;
            //chkCash.Visible = true;
            //chkCredit.Enabled = false;
            //chkDebit.Enabled = false;
            //chkCash.Enabled = false;
            chkSameMailing.Visible = true;
            chkSameMailing.Enabled = false;
            ChkAutoAssignPolicy.Enabled = false;

            TxtProspectNo.Enabled = false;
            TxtFirstName.Enabled = false;
            txtLastname1.Enabled = false;
            //txtLastname2.Enabled = false;
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
                chkIsRenew.Visible = false;
                txtPolicyNoToRenew.Visible = false;
                btnVerifyBondInPPS.Visible = false;
                //TxtPolicyNo.Visible = true;
            }
            else
            {
                chkIsRenew.Visible = true;
                txtPolicyNoToRenew.Visible = true;
                btnVerifyBondInPPS.Visible = true;
                txtPolicyNoToRenew.Enabled = false;
                //TxtPolicyNo.Visible = false;
            }

            TxtPolicyType.Visible = false;
            TxtSufijo.Visible = false;
            TxtCity.Enabled = false;
            TxtInitial.Visible = true;
            TxtCity.Visible = false;

            //TxtBirthdate.Visible = true;
            TxtOccupa.Visible = false;
            TxtLicense.Visible = false;
            //TxtBirthdate.Enabled = false;
            TxtOccupa.Enabled = false;
            TxtLicense.Enabled = false;
            lblPolicyNo.Visible = true;
            lblPolicyType.Visible = false;
            lblSuffix.Visible = false;
            lblSelectedAgent.Visible = true;
            //lblBirthdate.Visible = true;
            lblOccupa.Visible = false;
            lblLicense.Visible = false;
            //lblDefpayment.Visible = true;
            lblInitial.Visible = true;
            lbladdress1.Visible = true;
            lbladdress2.Visible = true;
            lblCity.Visible = true;
            lblZipCode.Visible = true;
            lblState.Visible = true;
            lblTerm.Visible = true;


            //BOTONES PRINT
            //btnPrintPolicy.Visible = true;
            //ddlPrintOption.Visible = true;

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
            txtPremium.Enabled = false;
            imgCalendarEff.Visible = false;

            ddlZip.Enabled = false;
            ddlPhyZipCode.Enabled = false;
            ddlZip.Visible = true;
            ddlPhyZipCode.Visible = true;
            ddlPhyCity.Enabled = false;
            ddlCiudad.Enabled = false;
            ddlCiudad.Visible = true;
            txtPenalty.Enabled = false;
            ddlPhyCity.Visible = true;
            ddlOriginatedAt.Enabled = false;
            ddlInsuranceCompany.Enabled = false;
            ddlAgency.Enabled = false;
            ddlAgent.Enabled = false;
            ddlSignature.Enabled = false;
            //ddlPaymentAmount.Enabled = false;

            ddlBank.Enabled = false;
            ddlCompanyDealer.Enabled = false;

            // Disable auto control

            txtDescriptionBond.Enabled = false;
            txtReqDocuments.Enabled = false;

            // Option Print

            lblPrintOption.Visible = false;
            chkInsured.Visible = false;
            chkProducer.Visible = false;
            chkCompany.Visible = false;
            chkAgency.Visible = false;
            chkExtraCopy.Visible = false;

            ddlTypeBonds.Enabled = false;
            //txtObligee.Enabled = false;
            ddlObligee.Enabled = false;
            txtAccNumber.Enabled = false;
            txtDescriptionBond.Enabled = false;
            txtReqDocuments.Enabled = false;
            btnAddVehicle.Enabled = false;
            GridReqDocs.Enabled = false;
            ddlType.Enabled = false;
            txtCompanyName.Enabled = false;
            txtLocationName.Enabled = false;
            txtCantidadPrestada.Enabled = false;

            VerifyAccess();
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
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

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
                    //if (State != (int)States.READWRITE && State != 0)
                    //    this.btnReinstallation.Visible = true;
                    //else
                    //    this.btnReinstallation.Visible = false;
                }
            }
            catch (Exception xp)
            {

            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {

            RemoveSessionLookUp();
            this.litPopUp.Visible = false;
            Session.Clear();
            Response.Redirect("Search.aspx");

        }

        private void SendEmail(string CustomerName, string Email, string PaymentMethod, string PaymentType, string PaymentAmount, string AccNumber, string EntryDate, string PolicyNumber, string DebitDate)
        {
            try
            {

                //EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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


                //AlternateView av = AlternateView.CreateAlternateViewFromString(SM.Body, Encoding.UTF8, "text/html");
                //av.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                //LinkedResource logo2 = new LinkedResource(ConfigurationManager.AppSettings["ImagePath"].ToString().Trim() + "blank.png", MediaTypeNames.Image.Jpeg);
                //logo2.ContentId = "BtnPagarEmail";
                //av.LinkedResources.Add(logo2);

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

        private void CalcTotalPremium(DataTable dt)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;

                if (dt.Rows.Count == 1)
                {
                    txtPremium.Text = "40.0";
                    txtTotalPremium.Text = "40.0";

                }
                if (dt.Rows.Count == 2)
                {

                    txtPremium.Text = "60.0";

                    txtTotalPremium.Text = "60.0";

                }
                if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp.UserID).ToString()).Contains("THOMAS"))
                {

                    txtPremium.Text = "70.0";

                    txtTotalPremium.Text = "70.0";
                }
                //else
                //{
                //    txtPremium.Text = "0.0";
                //    txtTotalPremium.Text = "00.0";
                //}

            }
            catch (Exception ex)
            {
            }
            //}
            //double Prem = 0.0;
            //try
            //{
            //    if (dt.Rows.Count != 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            Prem += double.Parse(dt.Rows[i]["Premium"].ToString());
            //        }
            //    }
            //    txtTotalPremium.Text = Prem.ToString().Trim();
            //}
            //catch (Exception ex)
            //{
            //}

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

                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                //taskControl.SaveGuardianXtra(userID);  //(userID);
                taskControl.SaveBonds(userID);

                // UpdateGuadianXtraHasAccident12(taskControl.TaskControlID, HasAccident12);

                taskControl = (EPolicy.TaskControl.Bonds)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);

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
                lblRecHeader.Text = "Bond information saved successfully.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
            catch (Exception exp)
            {
                LogError(exp);
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }

        protected void btnAcceptQuote_Click(object sender, EventArgs e)
        {

            EPolicy.TaskControl.Bonds taskContro1 = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;


                //VerifyPolicyExist();


                EPolicy.TaskControl.Bonds taskControlQuote = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                Session.Clear();
                EPolicy.TaskControl.Bonds taskControl = new EPolicy.TaskControl.Bonds(false);

                //taskControl = taskControlQuote;


                taskControl.Mode = 1; //ADD
                taskControl.isQuote = false;
                taskControl.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Bonds"));
                taskControl.InsuranceCompany = taskControl.InsuranceCompany;
                taskControl.Agency = taskControlQuote.Agency;
                taskControl.Agent = taskControlQuote.Agent;
                taskControl.OriginatedAt = taskControlQuote.OriginatedAt;
                taskControl.DepartmentID = 1;
                taskControl.EffectiveDate = taskControlQuote.EffectiveDate;
                taskControl.ExpirationDate = taskControlQuote.ExpirationDate;
                taskControl.Signature = taskControlQuote.Signature;
                taskControl.PaymentAmount = taskControlQuote.PaymentAmount;

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

                taskControl.Customer.SocialSecurity = taskControlQuote.Customer.SocialSecurity;



                taskControl.PolicyType = "BND";

                taskControl.CompanyDealer = taskControlQuote.CompanyDealer;
                taskControl.InsuranceCompany = taskControlQuote.InsuranceCompany;
                taskControl.OriginatedAt = taskControlQuote.OriginatedAt;
                taskControl.Agent = taskControlQuote.Agent;
                taskControl.Agency = taskControlQuote.Agency;
                taskControl.PaymentAmount = taskControlQuote.PaymentAmount;
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

                taskControl.BondDescription = taskControlQuote.BondDescription; //txtDescriptionBond.Text.Trim();
                taskControl.BondRequiredDocuments = taskControlQuote.BondRequiredDocuments; //txtReqDocuments.Text.Trim();
                taskControl.Limits = taskControlQuote.Limits; //txtPenalty.Text.Trim(); //ddlLimite.SelectedItem.Text.Trim();

                taskControl.Obligee = taskControlQuote.Obligee; //txtObligee.Text.Trim();
                taskControl.CompanyName = taskControlQuote.CompanyName; //txtCompanyName.Text.Trim();
                taskControl.CustomerType = taskControlQuote.CustomerType;

                taskControl.Customer.Email = taskControlQuote.Customer.Email;
                taskControl.Customer.Cellular = taskControlQuote.Customer.Cellular;
                taskControl.TypeOfBond = taskControlQuote.TypeOfBond;

                taskControl.BondsReqDocCollection = taskControlQuote.BondsReqDocCollection;
                taskControl.AccountNumber = taskControlQuote.AccountNumber.ToString().Trim();

                taskControl.NombreEstacion = taskControlQuote.NombreEstacion.ToString().Trim();
                taskControl.CantidadPrestadaSolicitante = taskControlQuote.CantidadPrestadaSolicitante.ToString().Trim();

                taskControl.RenewalOfBnd = taskControlQuote.RenewalOfBnd;


                Session.Add("TaskControl", taskControl);

                try
                {
                    //if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                    //{
                    //PolicyXML(taskControl.TaskControlID);
                    Response.Redirect("Bonds.aspx");
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

        private void ValidateFields()
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);


            ArrayList errorMessages = new ArrayList();

            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

            try
            {

                //if (ddlObligee.SelectedItem.Text.Trim() != "Mortgage Lenders" && ddlObligee.SelectedItem.Text.Trim() != "Mortgage Loan Originator")
                //{
                //    ddlPaymentAmount.SelectedIndex = 0;
                //    ddlPaymentAmount.Enabled = false;
                //}

                //else
                //{
                //    if (ddlObligee.SelectedItem.Text.Trim() == "Mortgage Lenders" || ddlObligee.SelectedItem.Text.Trim() == "Mortgage Loan Originator")
                //    {
                //        if (ddlPaymentAmount.SelectedValue != "25000" && ddlPaymentAmount.SelectedValue != "50000") {
                //            throw new Exception("You must select $25,000.00 or $50,000.00 in Payment Amount for this type of Obligee");
                //        }
                //    }
                //}

                //Effective date

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

                if (lblCompanyName.ForeColor == System.Drawing.Color.Red && txtCompanyName.Text == "")
                {
                    throw new Exception("You must enter a company name for this type of bond.");
                }

                //if ((this.TxtCellular.Text == "" || this.TxtCellular.Text == "(") && (this.txtWorkPhone.Text == "" || this.txtWorkPhone.Text == "(") && (this.TxtHomePhone.Text == "" || this.TxtHomePhone.Text == "("))
                //{
                //    errorMessages.Add("Cellular Phone, Work Phone or Home Phone is missing." + "\r\n");
                //}

                //if (this.TxtCellular.Text == "(")
                //{
                //    TxtCellular.Text = "";
                //}

                //if (this.txtWorkPhone.Text == "(")
                //{
                //    txtWorkPhone.Text = "";
                //}

                //if (this.TxtHomePhone.Text == "(")
                //{
                //    TxtHomePhone.Text = "";
                //}

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

                if (txtPremium.Text == "")
                    throw new Exception("You must select a total premium amount");

                //if (ddlCiudad.SelectedIndex <= 0 || ddlCiudad.SelectedItem == null)
                //{
                //    errorMessages.Add("The City is missing or wrong." + "\r\n");
                //}

                if (ddlCiudad.Text == "")
                {
                    errorMessages.Add("The City is missing or wrong." + "\r\n");
                }

                //if (ddlZip.SelectedIndex <= 0 || ddlPhyCity.SelectedItem == null)
                //{
                //    errorMessages.Add("The Zipcode is missing or wrong." + "\r\n");
                //}

                if (ddlZip.Text.Trim() == "")
                {
                    errorMessages.Add("The Zipcode is missing or wrong." + "\r\n");
                }

                if (ddlSignature.SelectedItem.Text.Trim() == "" && cp.IsInRole("BONDSVI"))
                {
                    throw new Exception("You must select a signature");
                }

                //if (ddlPhyCity.SelectedIndex <= 0 || ddlPhyCity.SelectedItem == null)
                //{
                //    errorMessages.Add("The Physical City is missing or wrong." + "\r\n");
                //}

                if (ddlPhyCity.Text.Trim() == "")
                {
                    errorMessages.Add("The Physical City is missing or wrong." + "\r\n");
                }

                //if (ddlPhyZipCode.SelectedIndex <= 0 || ddlPhyCity.SelectedItem == null)
                //{
                //    errorMessages.Add("The Physical Zipcode is missing or wrong." + "\r\n");
                //}

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

                if (taskControl.Mode == 1 || taskControl.Mode == 2 && DateTime.Parse(txtEffDt.Text.ToString()) < DateTime.Parse(taskControl.EffectiveDate))
                {
                    if (this.txtEffDt.Text == "")
                    {
                        errorMessages.Add("Effective date is missing." + "\r\n");
                    }

                    if (!cp.IsInRole("BONDS_ALLOWBACKDATE") && !cp.IsInRole("ADMINISTRATOR"))
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

                if (ddlTypeBonds.SelectedIndex == 0)
                {
                    errorMessages.Add("Choose an Type of Bond to continue." + "\r\n");
                }

                if (ddlObligee.SelectedItem.Text == "" || ddlObligee.SelectedItem.Text == "0")
                {
                    errorMessages.Add("Choose an Obligee to continue." + "\r\n");
                }

                DataTable dt = GetBondsPenaltyLimitByUserID(ddlAgent.SelectedItem.Value.ToString());
                if (dt.Rows.Count > 0)
                {
                    double PenaltyLimit = double.Parse(dt.Rows[0]["Limit"].ToString().Trim());

                    if (PenaltyLimit > 0)
                    {
                        if (double.Parse(txtPenalty.Text.Replace(",", "").Replace("$", "")) > PenaltyLimit)
                        {
                            errorMessages.Add("The maximum penalty limit is " + String.Format("{0:c0}", int.Parse(PenaltyLimit.ToString(), System.Globalization.NumberStyles.Currency)) + "\r\n");
                        }
                    }
                }


                if (cp.IsInRole("BONDSVI"))
                {
                    if (txtPenalty.Text == "" && (ddlTypeBonds.SelectedItem.Value == "5" || ddlTypeBonds.SelectedItem.Value == "17" || ddlTypeBonds.SelectedItem.Value == "602"))
                    {
                        throw new Exception("You must select a $25,000.00 or $50,000.00 penalty for this type of bond.");
                    }

                    else if (ddlTypeBonds.SelectedItem.Value == "5" || ddlTypeBonds.SelectedItem.Value == "17" || ddlTypeBonds.SelectedItem.Value == "602")
                    {
                        if (!(txtPenalty.Text.Contains("$25,000.00")) && !(txtPenalty.Text.Contains("$50,000.00")))
                        {
                            txtPenalty.Text = "";
                            throw new Exception("You must select a $25,000.00 or $50,000.00 penalty for this type of bond.");
                        }
                    }
                }
                else
                {
                    if (txtPenalty.Text == "" && (ddlObligee.SelectedItem.Text.Contains("Mortgage Brokers Bond") || ddlObligee.SelectedItem.Text.Contains("Mortgage Loan Originator")))
                    {
                        throw new Exception("You must select a $25,000.00 or $50,000.00 penalty for this obligee.");
                    }

                    else if (ddlObligee.SelectedItem.Text.Contains("Mortgage Brokers Bond") || ddlObligee.SelectedItem.Text.Contains("Mortgage Loan Originator"))
                    {
                        if (!(txtPenalty.Text.Contains("$25,000.00")) && !(txtPenalty.Text.Contains("$50,000.00")))
                        {
                            txtPenalty.Text = "";
                            throw new Exception("You must select a $25,000.00 or $50,000.00 penalty for this obligee.");
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

        private void FillProperties()
        {
            try
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

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

                //PaymentAmount
                //if (ddlPaymentAmount.SelectedIndex >= 0 && ddlPaymentAmount.SelectedItem != null)
                //{
                //    if (ddlPaymentAmount.SelectedItem.Value == "N/A")
                //    {
                //        taskControl.PaymentAmount = 0;
                //    }

                //    if (ddlPaymentAmount.SelectedItem.Value == "$25,000.00")
                //    {
                //        taskControl.PaymentAmount = 25000.00;
                //    }

                //    if (ddlPaymentAmount.SelectedItem.Value == "$50,000.00")
                //    {
                //        taskControl.PaymentAmount = 50000;
                //    }
                //}
                //else
                //    taskControl.PaymentAmount = 0.0;


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

                //Ciudad
                //if (ddlCiudad.SelectedIndex > 0 && ddlCiudad.SelectedItem != null)
                //    taskControl.Customer.City = ddlCiudad.SelectedItem.Text.ToString();
                //else
                //    taskControl.Customer.City = "";

                if (ddlCiudad.Text.Trim() != "")
                    taskControl.Customer.City = ddlCiudad.Text.ToString().Trim();
                else
                    taskControl.Customer.City = "";

                //Ciudad Fisica
                //if (ddlPhyCity.SelectedIndex > 0 && ddlPhyCity.SelectedItem != null)
                //    taskControl.Customer.CityPhysical = ddlPhyCity.SelectedItem.Text.ToString();
                //else
                //    taskControl.Customer.CityPhysical = "";

                if (ddlPhyCity.Text.Trim() != "")
                    taskControl.Customer.CityPhysical = ddlPhyCity.Text.ToString().Trim();
                else
                    taskControl.Customer.CityPhysical = "";

                //ZipCode
                //if (ddlZip.SelectedIndex > 0 && ddlZip.SelectedItem != null)
                //    taskControl.Customer.ZipCode = ddlZip.SelectedItem.Text;
                //else
                //    taskControl.Customer.ZipCode = "";

                if (ddlZip.Text != "")
                    taskControl.Customer.ZipCode = ddlZip.Text.Trim();
                else
                    taskControl.Customer.ZipCode = "";

                //ZipCode Fisico
                //if (ddlPhyZipCode.SelectedIndex > 0 && ddlPhyZipCode.SelectedItem != null)
                //    taskControl.Customer.ZipPhysical = ddlPhyZipCode.SelectedItem.Text;
                //else
                //    taskControl.Customer.ZipPhysical = "";

                //if (taskControl.PolicyClassID == 15) // OSO
                //{
                //    if (taskControl.Mode == 2) // EDIT
                //    {
                //        if ((taskControl.Customer.FirstName.Trim() != TxtFirstName.Text.Trim()) || (taskControl.Customer.LastName1.Trim() != txtLastname1.Text.Trim()))
                //        {
                //            int endNum = taskControl.Endoso + 1;
                //            taskControl.Endoso = endNum;
                //        }
                //    }
                //}

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

                if (ddlType.SelectedItem.Text.ToString().Trim() == "Corporate" || ddlType.SelectedItem.Text.ToString().Trim() == "DBA")
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


                taskControl.TotalPremium = double.Parse(txtPremium.Text.ToString().Trim().Replace("$", ""));

                taskControl.Customer.SamesAsMail = chkSameMailing.Checked;

                if (ChkAutoAssignPolicy.Checked)
                    taskControl.AutoAssignPolicy = true;
                else
                    taskControl.AutoAssignPolicy = false;

                if (taskControl.Mode == 1)
                {
                    //EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                    taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0];
                }

                if (ddlTypeBonds.SelectedIndex > 0 && ddlTypeBonds.SelectedItem != null)
                {
                    taskControl.TypeOfBond = int.Parse(ddlTypeBonds.SelectedItem.Value);
                }

                taskControl.BondDescription = txtDescriptionBond.Text.Trim();
                taskControl.BondRequiredDocuments = txtReqDocuments.Text.Trim();
                taskControl.Limits = txtPenalty.Text.Trim(); //ddlLimite.SelectedItem.Text.Trim();

                //taskControl.Obligee = txtObligee.Text.Trim();
                taskControl.Obligee = ddlObligee.SelectedItem.Value.ToString();
                taskControl.AccountNumber = txtAccNumber.Text.Trim();
                taskControl.CompanyName = txtCompanyName.Text.Trim();
                taskControl.CustomerType = int.Parse(ddlType.SelectedValue.ToString());
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


                taskControl.CantidadPrestadaSolicitante = txtCantidadPrestada.Text.Trim();
                taskControl.NombreEstacion = txtLocationName.Text.Trim();
                taskControl.RenewalOfBnd = txtPolicyNoToRenew.Text.Trim();
                taskControl.Customer.LocationID = EPolicy.Login.Login.GetLocationByUserID(userID);
                taskControl.Signature = ddlSignature.SelectedItem.Value.ToString();

                //if (ddlPaymentAmount.SelectedItem.Text.Trim() == "")
                //{
                //    taskControl.PaymentAmount = 0.0;
                //}
                //else
                //{
                //    taskControl.PaymentAmount = double.Parse(ddlPaymentAmount.SelectedItem.Value);
                //}


                //if (ddlPaymentAmount.SelectedItem.ToString() == "N/A")
                //{
                //    taskControl.PaymentAmount = 0;
                //}

                //if (ddlPaymentAmount.SelectedItem.ToString() == "$25,000.00")
                //{
                //     taskControl.PaymentAmount = 25000.00;
                //}
                //if (ddlPaymentAmount.SelectedItem.ToString() == "$50,000.00")
                //{
                //    taskControl.PaymentAmount = 50000.00;
                //}
                //taskControl.PaymentAmount = double.Parse(ddlPaymentAmount.SelectedItem.ToString());

                if (TxtFirstName.Text.Trim() == "")
                {
                    taskControl.Customer.FirstName = txtCompanyName.Text.Trim();

                }
                if (txtLastname1.Text.Trim() == "")
                {
                    taskControl.Customer.LastName1 = ".";
                    //YDJ nose
                }

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
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];


            //Botones  
            btnEdit.Visible = false;
            btnPreview.Visible = false;
            //btnIndemnityQuote.Visible = false;
            BtnExit.Visible = false;
            BtnSave.Visible = true;
            btnCancel.Visible = true;
            // btnRenew.Visible = false;
            btnCancellation.Visible = false;
            //btnGuardianPay.Visible = false;
            //btnCashPayment.Visible = false;

            // TextBox
            TxtProspectNo.Enabled = false;
            TxtFirstName.Enabled = true;
            txtLastname1.Enabled = true;
            //txtLastname2.Enabled = true;

            //chkDefpaysix.Enabled = true;
            //chkDefpayfour.Enabled = true;
            //chkCredit.Visible = true;
            //chkCredit.Enabled = true;
            //chkDebit.Visible = true;
            //chkDebit.Enabled = true;
            chkSameMailing.Visible = true;
            chkSameMailing.Enabled = true;
            //chkCash.Visible = true;
            //chkCash.Enabled = true;

            TxtInitial.Enabled = true;
            TxtAddrs1.Enabled = true;
            TxtAddrs2.Enabled = true;
            TxtState.Enabled = true;
            //ddlCiudad.Enabled = true;
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

            //TxtBirthdate.Enabled = true;
            TxtOccupa.Enabled = true;
            TxtLicense.Enabled = true;
            //TxtBirthdate.Visible = true;
            TxtOccupa.Visible = false;
            TxtLicense.Visible = false;
            //lblBirthdate.Visible = true;
            lblOccupa.Visible = false;
            lblLicense.Visible = false;

            txtPhyAddress.Enabled = true;
            txtPhyAddress2.Enabled = true;
            txtPhyState.Enabled = true;


            //al entrar un bond nuevo el SS puede ser añadido por cualquier usario
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
                chkIsRenew.Visible = false;
                txtPolicyNoToRenew.Visible = false;
                btnVerifyBondInPPS.Visible = false;
                //TxtPolicyNo.Visible = true;
            }
            else
            {
                chkIsRenew.Visible = true;
                txtPolicyNoToRenew.Visible = true;
                btnVerifyBondInPPS.Visible = true;
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
            txtPremium.Enabled = true;
            imgCalendarEff.Visible = true;

            //Enable Auto Control

            txtDescriptionBond.Enabled = true;
            txtReqDocuments.Enabled = true;
            ddlBank.Enabled = true;
            ddlCompanyDealer.Enabled = true;

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            ddlOriginatedAt.Enabled = false;
            ddlInsuranceCompany.Enabled = false;
            ddlAgency.Enabled = false;
            ddlSignature.Enabled = true;

            //if (ddlObligee.SelectedItem.Text.Contains("Mortgage Lenders") || ddlObligee.SelectedItem.Text.Contains("Mortgage Loan Originator"))
            //{
            //    ddlPaymentAmount.Enabled = true;
            //}
            //else
            //{
            //    ddlPaymentAmount.Enabled = false;
            //}
            //ddlAgent.Enabled = true;

            // Print Option

            lblPrintOption.Visible = false;
            chkInsured.Visible = false;
            chkProducer.Visible = false;
            chkCompany.Visible = false;
            chkAgency.Visible = false;
            chkExtraCopy.Visible = false;

            if (ddlObligee.SelectedItem.Text.Contains("Non Resident Brokers") || ddlObligee.SelectedItem.Text.Contains("Surplus Lines Broker") || ddlObligee.SelectedItem.Text.Contains("Process Server")
                || ddlObligee.SelectedItem.Text.Contains("Private Investigative Agency Surety") || ddlObligee.SelectedItem.Text.Contains("Power of Attorney") || ddlObligee.SelectedItem.Text.Contains("Resident Line Brokers")
                || ddlObligee.SelectedItem.Text.Contains("Notary Public - VI"))
                txtPenalty.Enabled = false;
            else
                txtPenalty.Enabled = true;

            //Si esta pagada la poliza no dispone el policyType, la agencia, agentes y supplier y el totalPremium + Charge.
            //Se cambio dllAgent.Enabled a true por Joshua
            //if ((double)taskControl.PaymentsDetail.TotalPaid > 0 && (double)taskControl.PaymentsDetail.TotalPaid >= taskControl.TotalPremium + taskControl.Charge)
            //{
            //    ddlAgency.Enabled = false;
            //    ddlAgent.Enabled = false;

            //    ddlInsuranceCompany.Enabled = false;
            //    txtTotalPremium.Enabled = false;
            //    //ddlPremium.Enabled = false;

            //}
            //else
            //{
            //    txtTotalPremium.Enabled = true;
            //}

            if (taskControl.TaskControlID != 0 && btnAcceptQuote.Visible == false)
            {
                BtnSave.Text = "ISSUE BOND";
            }

            //if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("GUARIDANROADASSISTANCE"))
            //    ddlAgent.Enabled = true;
            //else
            //    ddlAgent.Enabled = false;

            //if (cp.IsInRole("GUARIDANROADASSISTANCE"))
            //{
            //    ddlAgent.Enabled = false;

            //}

            ddlTypeBonds.Enabled = true;
            //txtObligee.Enabled = true;
            ddlObligee.Enabled = true;
            txtAccNumber.Enabled = true;
            txtDescriptionBond.Enabled = true;
            txtReqDocuments.Enabled = true;
            btnAddVehicle.Enabled = true;
            GridReqDocs.Enabled = true;
            ddlType.Enabled = true;
            txtCompanyName.Enabled = true;
            txtLocationName.Enabled = true;
            txtCantidadPrestada.Enabled = true;

            if (cp.IsInRole("BONDSVI"))
            {
                lblDealer0.Visible = false;
                txtDescriptionBond.Visible = false;
                txtReqDocuments.Visible = false;
                btnAddVehicle.Visible = false;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            //Session.Clear();
            //EPolicy.TaskControl.Bonds taskControl = new EPolicy.TaskControl.Bonds();

            //taskControl.Mode = 1; //ADD
            //taskControl.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Road Assistance"));

            //Session.Add("TaskControl", taskControl);
            //Response.Redirect("GuardianRoadAssist.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            DataTable dtObligee = EPolicy.LookupTables.LookupTables.GetTable("BondsObligee");
            string BondPRorVI = "";
            for (int i = 0; i < dtObligee.Rows.Count; i++)
            {
                if (ddlObligee.SelectedItem.Value.ToString() == dtObligee.Rows[i]["ObligeeID"].ToString())
                {
                    if (dtObligee.Rows[i]["Island"].ToString() == "1")
                    {
                        BondPRorVI = "PR";
                    }
                    else
                    {
                        BondPRorVI = "VI";
                    }
                }
            }

            if (BondPRorVI == "PR")
            {
                PrintBonds("1");
            }
            else
            {
                PrintBondsQuote();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

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
                taskControl = (EPolicy.TaskControl.Bonds)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
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
            txtPolicyNoToRenew.Visible = chkIsRenew.Checked;
            btnVerifyBondInPPS.Visible = chkIsRenew.Checked;
            txtPolicyNoToRenew.Enabled = chkIsRenew.Checked;

            DivRenew.Visible = chkIsRenew.Checked;
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

        protected void btnCancellation_Click(object sender, EventArgs e)
        {

            RemoveSessionLookUp();
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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
                            EPolicy.TaskControl.Bonds opp = (EPolicy.TaskControl.Bonds)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);
                            opp.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                            opp.IsEndorsement = true;

                            if (Session["TaskControl"] != null)
                            {
                                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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
                        EPolicy.TaskControl.Bonds newOPP = (EPolicy.TaskControl.Bonds)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

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

                        EPolicy.TaskControl.Bonds taskControl2 = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                        int s = int.Parse(e.Item.Cells[2].Text);
                        string comments = e.Item.Cells[10].Text.Trim();
                        EPolicy.TaskControl.Bonds newOPP2 = (EPolicy.TaskControl.Bonds)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(s, userID);
                        int OPPEndorID = int.Parse(e.Item.Cells[1].Text);

                        //Print Document
                        try
                        {
                            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                            GetReportEndosoTableAdapters.GetReportEndosoTableAdapter ds = new GetReportEndosoTableAdapters.GetReportEndosoTableAdapter();
                            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportEndoso_GetReportEndoso", (DataTable)ds.GetData(OPPEndorID));

                            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                            viewer.LocalReport.DataSources.Clear();
                            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                            viewer.LocalReport.ReportPath = Server.MapPath("Reports/Endoso.rdlc");
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

                TaskControl.Bonds taskControl = new TaskControl.Bonds(false);  //Policy
                TaskControl.Bonds GuardianXtra = (TaskControl.Bonds)Session["TaskControl"];


                EPolicy.TaskControl.Bonds taskControlEndo = null;

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

                //Added by Joshua
                taskControl.Suffix = DateTime.Parse(taskControl.EffectiveDate).Year.ToString().Remove(0, 2);

                //int msufijo;
                //int sufijo = int.Parse(GuardianXtra.Suffix);
                //msufijo = sufijo + 1;
                //taskControl.Suffix = "0".ToString() + msufijo.ToString();
                //taskControl.Suffix = GuardianXtra.Suffix;

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

                TaskControl.Bonds taskControl = new TaskControl.Bonds(false);  //Policy
                TaskControl.Bonds GuardianXtra = (TaskControl.Bonds)Session["TaskControl"];


                EPolicy.TaskControl.Bonds taskControlEndo = null;

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

        private List<string> WriteRdlcToPDF(ReportViewer viewer, EPolicy.TaskControl.Bonds taskControl, List<string> mergePaths, int index)
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

        private DataTable GetBondInfoPolicy(int TaskControlID)
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
                dt = exec.GetQuery("GetBondInfoPolicy", xmlDoc);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data from database.", ex);
            }

        }

        protected void BtnPrintInvoice_Click(object sender, EventArgs e)
        {
            DataTable dtObligee = EPolicy.LookupTables.LookupTables.GetTable("BondsObligee");
            string BondPRorVI = "";
            for (int i = 0; i < dtObligee.Rows.Count; i++)
            {
                if (ddlObligee.SelectedItem.Value.ToString().Trim() == dtObligee.Rows[i]["ObligeeID"].ToString().Trim())
                {
                    if (dtObligee.Rows[i]["Island"].ToString() == "1")
                    {
                        BondPRorVI = "PR";
                    }
                    else
                    {
                        BondPRorVI = "VI";
                    }
                }
            }

            if (BondPRorVI == "PR")
            {
                PrintBonds("0");
            }
            else
            {
                PrintBondsPolicy();
            }

        }

        protected void PrintBondsPolicy()
        {
            try
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
                List<string> mergePaths = new List<string>();
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
                string signature = "";
                string predOrVice = "";

                if (ddlSignature.SelectedItem.Text == "President")
                {
                    signature = "RAYMOND L. FOURNIER, PRESIDENT";
                    predOrVice = "PRESIDENT";
                }
                else if (ddlSignature.SelectedItem.Text == "Vice President")
                {
                    signature = "OCTAVIO ESTRADA, VICE PRESIDENT";
                    predOrVice = "VICE PRESIDENT";
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "12")
                {
                    string limit = txtPenalty.Text.Trim();
                    string limitwords = "";
                    if (limit == "$25,000.00")
                    {
                        limitwords = "TWENTY-FIVE THOUSAND DOLLARS";
                    }
                    else if (limit == "$50,000.00")
                    {
                        limitwords = "FIFTY THOUSAND DOLLARS";
                    }
                    else
                    {
                        limitwords = "";
                    }
                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()}, { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice},{ "PredVic2", predOrVice},
                {"Limit",limit},{"Limit2",limit},{"Limit3",limit},{"LimitWords",limitwords},{"LimitWords2",limitwords},{"LimitWords3",limitwords}, { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Mortgage_Loan_Broker", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "21")
                {
                    string limit = txtPenalty.Text.Trim();
                    string limitwords = "";
                    if (limit == "$25,000.00")
                    {
                        limitwords = "TWENTY-FIVE THOUSAND DOLLARS";
                    }
                    else if (limit == "$50,000.00")
                    {
                        limitwords = "FIFTY THOUSAND DOLLARS";
                    }
                    else
                    {
                        limitwords = "";
                    }
                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()}, { "CompanyName2", dt.Rows[0]["CompanyName"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice},{ "PredVic2", predOrVice},
                {"Limit",limit},{"Limit2",limit},{"Limit3",limit},{"LimitWords",limitwords},{"LimitWords2",limitwords},{"LimitWords3",limitwords},{ "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Mortgage_Loan_Broker_Company", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "16")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()}, { "NameToPrint2", dt.Rows[0]["CompanyName"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic2", predOrVice},{ "PredVic3", predOrVice}, { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Mortgage_Loan_Originator", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "15")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Signature", signature}, { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Resident_Line_Brokers", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "18")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, 
                { "Signature", signature}, { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Non_Resident_Brokers", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "17")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Signature", signature}, { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Surplus_Lines_Broker", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "14")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "NameToPrint3", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Signature", signature}, { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Notary_Public_VI", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "13")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "19")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "NameToPrint3", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Signature", signature}, { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Process_Server", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "20")
                {
                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()}, 
                { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice},{ "PredVic2", predOrVice},{ "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                    PrintBondsMSWord("Private_Investigative_Agency_Surety", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityPolicy(mergePaths, ProcessedPath);
                }


                PrintBondsPDFMerge(mergePaths, ProcessedPath);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void PrintBondsQuote()
        {
            try
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
                List<string> mergePaths = new List<string>();
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
                string signature = "";
                string predOrVice = "";

                if (ddlSignature.SelectedItem.Text == "President")
                {
                    signature = "RAYMOND L. FOURNIER, PRESIDENT";
                    predOrVice = "PRESIDENT";
                }
                else if (ddlSignature.SelectedItem.Text == "Vice President")
                {
                    signature = "OCTAVIO ESTRADA, VICE PRESIDENT";
                    predOrVice = "VICE PRESIDENT";
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "12")
                {
                    string limit = txtPenalty.Text.Trim();
                    string limitwords = "";
                    if (limit == "$25,000.00")
                    {
                        limitwords = "TWENTY-FIVE THOUSAND DOLLARS";
                    }
                    else if (limit == "$50,000.00")
                    {
                        limitwords = "FIFTY THOUSAND DOLLARS";
                    }
                    else
                    {
                        limitwords = "";
                    }
                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()}, { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice},{ "PredVic2", predOrVice},
                {"Limit",limit},{"Limit2",limit},{"Limit3",limit},{"LimitWords",limitwords},{"LimitWords2",limitwords},{"LimitWords3",limitwords}};

                    PrintBondsMSWord("Mortgage_Loan_Broker_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "21")
                {
                    string limit = txtPenalty.Text.Trim();
                    string limitwords = "";
                    if (limit == "$25,000.00")
                    {
                        limitwords = "TWENTY-FIVE THOUSAND DOLLARS";
                    }
                    else if (limit == "$50,000.00")
                    {
                        limitwords = "FIFTY THOUSAND DOLLARS";
                    }
                    else
                    {
                        limitwords = "";
                    }
                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()}, { "CompanyName2", dt.Rows[0]["CompanyName"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice},{ "PredVic2", predOrVice},
                {"Limit",limit},{"Limit2",limit},{"Limit3",limit},{"LimitWords",limitwords},{"LimitWords2",limitwords},{"LimitWords3",limitwords}};

                    PrintBondsMSWord("Mortgage_Loan_Broker_Company_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "16")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()}, { "NameToPrint2", dt.Rows[0]["CompanyName"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic2", predOrVice},{ "PredVic3", predOrVice}};

                    PrintBondsMSWord("Mortgage_Loan_Originator_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "15")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Signature", signature}};

                    PrintBondsMSWord("Resident_Line_Brokers_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "18")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()},
                { "Signature", signature}};

                    PrintBondsMSWord("Non_Resident_Brokers_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "17")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Signature", signature}};

                    PrintBondsMSWord("Surplus_Lines_Broker_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "14")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "NameToPrint3", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Signature", signature}};

                    PrintBondsMSWord("Notary_Public_VI_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "13")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "19")
                {

                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},  
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, {"ExpDate", dt.Rows[0]["CompleteExpirationDateEnglish"].ToString().Trim()}, 
                { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "NameToPrint3", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Signature", signature}};

                    PrintBondsMSWord("Process_Server_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                if (ddlObligee.SelectedItem.Value.ToString() == "20")
                {
                    DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                    Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()}, { "CompanyName", dt.Rows[0]["CompanyName"].ToString().Trim()},   
                { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},
                { "EffDate", dt.Rows[0]["CompleteEffectiveDateEnglish"].ToString().Trim()}, 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Signature", signature},{ "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice},{ "PredVic2", predOrVice}};

                    PrintBondsMSWord("Private_Investigative_Agency_Surety_Quote", bookmarks, mergePaths, ProcessedPath);

                    Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()}, { "Signature", signature},
                { "Day2", DateTime.Now.Day.ToString()},{ "Month2", DateTime.Now.ToString("MMMM")},{ "Year2", DateTime.Now.Year.ToString()},
                { "Day3", DateTime.Now.Day.ToString()},{ "Month3", DateTime.Now.ToString("MMMM")},{ "Year3", DateTime.Now.Year.ToString()},
                { "Day4", DateTime.Now.Day.ToString()},{ "Month4", DateTime.Now.ToString("MMMM")},{ "Year4", DateTime.Now.Year.ToString()},
                { "PredVic", predOrVice}};

                    PrintBondsMSWord("Power_Of_Attorney", bookmarks2, mergePaths, ProcessedPath);
                    PrintIndemnityQuote(mergePaths, ProcessedPath);
                }

                PrintBondsPDFMerge(mergePaths, ProcessedPath);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void PrintBondsMSWord(string reportName, Dictionary<string, string> bookmarks, List<string> mergePaths, string ProcessedPath)
        {
            try
            {

                string ProcessedPath2 = ConfigurationManager.AppSettings["ReportPathName"];
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
                int tc_id = taskControl.TaskControlID;
                string copyFileName = CopyFile(reportName);
                string fileName = ProcessedPath2 + "\\Bonds\\Copy\\" + copyFileName;
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

        protected void PrintBondsPDFMerge(List<string> mergePaths, string ProcessedPath)
        {
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFileMerge = "";
            FinalFileMerge = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFileMerge + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
        }

        private string CopyFile(string reportName)
        {
            string fileName = reportName;
            string ProcessedPath = ConfigurationManager.AppSettings["ReportPathName"];
            string sourcePath = ProcessedPath + @"\Bonds\";
            string targetPath = ProcessedPath + @"\Bonds\Copy\";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName + ".docx");
            string copyFileName = fileName + DateTime.Now.ToString().Replace("/", "-").Replace(":", "").Replace(" ", "") + ".docx";
            string destFile = System.IO.Path.Combine(targetPath, copyFileName);

            System.IO.File.Copy(sourceFile, destFile, true);

            return copyFileName;
        }

        protected void PrintBonds(string isPreview)
        {
            try
            {//joshua

                List<string> mergePaths = new List<string>();
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
                int tc_id = taskControl.TaskControlID;
                decimal Amount = decimal.Parse(taskControl.Limits.ToString().Replace("$", ""));

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "AEEFianza.rdlc", Amount);
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEFianza2.rdlc");
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEFianza3.rdlc");
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEFianza4.rdlc");

                #region TEST ALL PRINT
                //mergePaths = ImprimirAmount(mergePaths, tc_id, "AEEbond.rdlc", Amount);
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond2.rdlc");
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond3.rdlc");
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond4.rdlc");
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond5.rdlc");
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond6.rdlc");

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "AAAFianza.rdlc", Amount);
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "AAAFianza2.rdlc");
                ////mergePaths = ImprimirCertification(mergePaths, tc_id, "AAAFianza3.rdlc");

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "ASCFianza.rdlc", Amount);
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "ASCFianza2.rdlc");
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "ASCFianza3.rdlc");

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "FianzaELA.rdlc", Amount);
                //mergePaths = ImprimirCertification(mergePaths, tc_id, "FianzaELA2.rdlc");

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "GuranteeBond.rdlc", Amount);

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "LeaseBond.rdlc", Amount);

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "NotaryPublicBond.rdlc", Amount);
                //mergePaths = ImprimirAmount(mergePaths, tc_id, "NotaryPublicBond2.rdlc", Amount);

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "TravelAgencyBond.rdlc", Amount);

                //mergePaths = ImprimirAmount(mergePaths, tc_id, "CertificadoRenovacion.rdlc", Amount);
                //mergePaths = ImprimirAmount(mergePaths, tc_id, "ContinousBond.rdlc", Amount);
                //mergePaths = ImprimirAmount(mergePaths, tc_id, "FianzaRenovacion.rdlc", Amount);
                #endregion TEST ALL PRINT

                if (chkIsRenew.Checked)
                {
                    if (ddlObligee.SelectedItem.Value.ToString() == "5" ||
                        ddlObligee.SelectedItem.Value.ToString() == "6" ||
                        ddlObligee.SelectedItem.Value.ToString() == "8" ||
                        ddlObligee.SelectedItem.Value.ToString() == "10")
                    {
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "CertificadoRenovacionENG.rdlc", Amount, isPreview, "");
                    }
                    else
                    {
                        if (ddlObligee.SelectedItem.Value.ToString() == "1" || ddlObligee.SelectedItem.Value.ToString() == "2") // AEE OR AAA
                        {
                            mergePaths = ImprimirAmount(mergePaths, tc_id, "CertificadoRenovacion.rdlc", Amount, isPreview, "AAA");
                        }
                        else
                        {
                            if (ddlObligee.SelectedItem.Value.ToString() == "7") // NOTARY PUBLIC
                            {
                                mergePaths = ImprimirAmount(mergePaths, tc_id, "CertificadoRenovacionENG.rdlc", Amount, isPreview, "NotaryPublic");
                            }
                            else if (ddlObligee.SelectedItem.Value.ToString() == "9") // NOTARY PUBLIC 2
                            {
                                mergePaths = ImprimirAmount(mergePaths, tc_id, "CertificadoRenovacionENG.rdlc", Amount, isPreview, "NotaryPublic2");
                            }
                            else
                            {
                                mergePaths = ImprimirAmount(mergePaths, tc_id, "CertificadoRenovacion.rdlc", Amount, isPreview, "");
                            }
                        }
                    }
                }
                else
                {

                    #region AEE_Fianza
                    if (ddlObligee.SelectedItem.Value.ToString() == "1") // AEE
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "AEEbond.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond2.rdlc", isPreview);
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond3.rdlc", isPreview);
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond4.rdlc", isPreview);
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond5.rdlc", isPreview);
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "AEEbond6.rdlc", isPreview);

                    }
                    #endregion

                    #region AAA_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "2") // AAA
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "AAAFianza.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "AAAFianza2.rdlc", isPreview);
                        //mergePaths = ImprimirCertification(mergePaths, tc_id, "AAAFianza3.rdlc");

                    }
                    #endregion

                    #region ASC_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "3" || ddlObligee.SelectedItem.Value.ToString() == "11") // ASC
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "ASCFianza.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "ASCFianza2.rdlc", isPreview);
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "ASCFianza3.rdlc", isPreview);

                    }
                    #endregion

                    #region ELA_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "4") // ELA
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "FianzaELA.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirCertification(mergePaths, tc_id, "FianzaELA2.rdlc", isPreview);

                    }
                    #endregion

                    #region Guarantee_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "5") // Guarantee
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "GuranteeBond.rdlc", Amount, isPreview, "");

                    }
                    #endregion

                    #region Financial_Guarantee_Miscellaneous

                    if (ddlObligee.SelectedItem.Value.ToString() == "10") // Guarantee Financial Misc
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "FinancialGuarantee.rdlc", Amount, isPreview, "");

                    }
                    #endregion

                    #region Lease_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "6") // Lease
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "LeaseBond.rdlc", Amount, isPreview, "");

                    }
                    #endregion

                    #region NotaryPublic_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "7") // Notary
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "NotaryPublicBond.rdlc", Amount, isPreview, "");

                    }
                    #endregion

                    #region NotaryPublic2_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "9") // Notary Public 2
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "NotaryPublicBond2.rdlc", Amount, isPreview, "");

                    }
                    #endregion

                    #region Travel_Fianza

                    if (ddlObligee.SelectedItem.Value.ToString() == "8") // Travel
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "TravelAgencyBond.rdlc", Amount, isPreview, "");

                    }
                    #endregion

                    if (ddlObligee.SelectedItem.Value.ToString() == "12") // Mortgage Lender Bond
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "MortageLendersBond.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");


                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "13") // Power of attorney
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");


                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "14") // Notary Public Bond VI
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "NOTARY-PUBLIC-BOND.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");

                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "15") // Resident Line Broker
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "RESIDENT-LINE-BROKERS-BOND.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");

                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "16") // Mortgage Loan Originators
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "MortgageLoanOriginatorsBond.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");


                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "17") // Surplus
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "SurplusLinesBrokerBond.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");

                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "18") // Non Resident Broker Bond
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "Non-ResidentBrokersBond.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");

                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "19") // Process Server Bond
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "ProcessServerBond.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");

                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "20") // Process Server Bond
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "PrivateInvestigativeAgencySurety.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");

                    }

                    if (ddlObligee.SelectedItem.Value.ToString() == "21") // Mortgage Lender Bond
                    {

                        mergePaths = ImprimirAmount(mergePaths, tc_id, "MortageLendersBondCompany.rdlc", Amount, isPreview, "");
                        mergePaths = ImprimirAmount(mergePaths, tc_id, "POWER-OF-ATTORNEY.rdlc", Amount, isPreview, "");


                    }



                }

                //if (taskControl.Suffix != "00")
                //{
                //    mergePaths = ImprimirAmount(mergePaths, tc_id, "CertificadoRenovacion.rdlc", Amount);
                //    mergePaths = ImprimirAmount(mergePaths, tc_id, "ContinousBond.rdlc", Amount);
                //    mergePaths = ImprimirAmount(mergePaths, tc_id, "FianzaRenovacion.rdlc", Amount);
                //}

                //Generar PDF
                OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                string FinalFile = "";
                FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());

                //Process myProcess = new Process();
                //myProcess.StartInfo.FileName = ProcessedPath + FinalFile; //PDF path
                //myProcess.Start();
                System.Diagnostics.Debug.Write("");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
                int taskControlID = taskControl.TaskControlID;
                List<string> mergePaths = new List<string>();
                mergePaths = imprimirPolicy(mergePaths, "AgentInvoice_VI");
                mergePaths = imprimirPolicy(mergePaths, "AgentInvoice2_VI");

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

        private List<string> imprimirPolicy(List<string> mergePaths, string name)
        {
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                int s = taskControl.TaskControlID;

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/Bonds/" + name + ".rdlc");
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;

                GetInvoiceBondInfoByTaskControlIDTableAdapters.GetInvoiceBondInfoByTaskControlIDTableAdapter ta = new GetInvoiceBondInfoByTaskControlIDTableAdapters.GetInvoiceBondInfoByTaskControlIDTableAdapter();
                GetInvoiceBondInfoByTaskControlID.GetInvoiceBondInfoByTaskControlIDDataTable dt2 = new GetInvoiceBondInfoByTaskControlID.GetInvoiceBondInfoByTaskControlIDDataTable();

                ReportDataSource rds = new ReportDataSource();
                rds = new ReportDataSource("GetInvoiceBondInfoByTaskControlID", (System.Data.DataTable)ta.GetData(s));

                EPolicy.TaskControl.Bonds taskControl1 = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

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
                else if (AgentDesc.ToUpper().Contains("MIDOCEAN"))
                {
                    ImgPath = Server.MapPath("Images2\\AgencyLogos\\Midocean1.png");
                    pathAsUri = new Uri(ImgPath);
                }
                else
                {

                }

                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");
                parameters[1] = new ReportParameter("Obligee", ddlObligee.SelectedItem.Text);
                parameters[2] = new ReportParameter("AccountNumber", txtAccNumber.Text);


                viewer1.LocalReport.EnableExternalImages = true;
                viewer1.LocalReport.DataSources.Add(rds);
                viewer1.LocalReport.SetParameters(parameters);

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

        private void DeleteFile(string pathAndFileName)
        {
            if (File.Exists(pathAndFileName))
                File.Delete(pathAndFileName);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private List<string> ImprimirCertification(List<string> mergePaths, int taskControl, string RDLC, string IsPreview)
        {
            try
            {
                GetBondInfoPolicyTableAdapters.GetBondInfoPolicyTableAdapter ta = new GetBondInfoPolicyTableAdapters.GetBondInfoPolicyTableAdapter();

                ReportDataSource rpd = new ReportDataSource();
                rpd = new ReportDataSource("GetBondInfoPolicy", (DataTable)ta.GetData(taskControl));

                ReportParameter[] param = new ReportParameter[1];
                param[0] = new ReportParameter("IsPreview", IsPreview);

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/Bonds/" + RDLC);
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
                GetBondInfoPolicyTableAdapters.GetBondInfoPolicyTableAdapter ta = new GetBondInfoPolicyTableAdapters.GetBondInfoPolicyTableAdapter();
                ReportDataSource rpd = new ReportDataSource();
                rpd = new ReportDataSource("GetBondInfoPolicy", (DataTable)ta.GetData(taskControl));

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

                    if (txtCantidadPrestada.Text.ToString().Replace("$", "").Replace(",", "") == "")
                    {
                        txtCantidadPrestada.Text = "0.00";
                    }
                    ParametersCount = 7;
                    Amt1 = NumberToCurrencyTextESP(double.Parse(txtCantidadPrestada.Text.ToString().Replace("$", "").Replace(",", "")));
                    Amt1 = "**" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Amt1.ToLower()) + " y Cero Centavos" + "**";

                    AmtTotal = (double.Parse(txtCantidadPrestada.Text.ToString().Replace("$", "").Replace(",", "")) + double.Parse(txtPenalty.Text.ToString().Replace("$", "").Replace(",", ""))).ToString();
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
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Substring(0, TxtPolicyNo.Text.Trim().IndexOf("-")).Replace("bnd", ""));
                        }
                        else
                        {
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Replace("bnd", ""));
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
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }
                        if (txtPenalty.Text.Contains("$25,000.00"))
                        {
                            param[4] = new ReportParameter("LimitsWords", "Twenty-Five Thousand Dollars");
                        }
                        if (txtPenalty.Text.Contains("$50,000.00"))
                        {
                            param[4] = new ReportParameter("LimitsWords", "Fifty Thousand Dollars");
                        }
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

                    else if (RDLC == "MortageLendersBondCompany.rdlc")
                    {
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }
                        if (txtPenalty.Text.Contains("$25,000.00"))
                        {
                            param[4] = new ReportParameter("LimitsWords", "Twenty-Five Thousand Dollars");
                        }
                        if (txtPenalty.Text.Contains("$50,000.00"))
                        {
                            param[4] = new ReportParameter("LimitsWords", "Fifty Thousand Dollars");
                        }
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
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }
                        if (txtPenalty.Text.Contains("$25,000.00"))
                        {
                            param[4] = new ReportParameter("LimitsWords", "Twenty-Five Thousand Dollars");
                        }
                        if (txtPenalty.Text.Contains("$50,000.00"))
                        {
                            param[4] = new ReportParameter("LimitsWords", "Fifty Thousand Dollars");
                        }
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
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }

                    }

                    else if (RDLC == "POWER-OF-ATTORNEY.rdlc")
                    {
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }

                    }

                    else if (RDLC == "SurplusLinesBrokerBond.rdlc")
                    {
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }

                    }

                    else if (RDLC == "Non-ResidentBrokersBond.rdlc")
                    {
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }

                    }

                    else if (RDLC == "RESIDENT-LINE-BROKERS-BOND.rdlc")
                    {
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }

                    }

                    else if (RDLC == "ProcessServerBond.rdlc")
                    {
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }

                    }

                    else if (RDLC == "PrivateInvestigativeAgencySurety.rdlc")
                    {
                        if (ddlSignature.SelectedItem.Text == "President")
                        {
                            param[3] = new ReportParameter("Signature", "Raymond L. Fournier, President");
                        }
                        if (ddlSignature.SelectedItem.Text == "Vice President")
                        {
                            param[3] = new ReportParameter("Signature", "Octavio Estrada, Vice President");
                        }

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

        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            try
            //{
            //    EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

            //    List<string> mergePaths = new List<string>();
            //    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];


            //    mergePaths.Add(PrintPreview("MidOceanInvoiceES.rdlc"));


            //    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            //    string finalFile = "";
            //    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);

            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + finalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
            //}
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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

        protected void GridReqDocs_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void GridReqDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());

                if (e.CommandName.ToString() == "Select")
                {

                    this.btnAddVehicle.Text = "Save";

                    txtReqDocuments.Text = GridReqDocs.DataKeys[rowIndex].Values["RequiredDocumentDesc"].ToString(); //e.Item.Cells[4].Text.ToString().Trim();
                    txtIDRoadAssist.Text = GridReqDocs.DataKeys[rowIndex].Values["BondRequiredDocumentID"].ToString(); //e.Item.ItemIndex.ToString();
                }

                else
                {
                    int index = Convert.ToInt32(e.CommandArgument); // int.Parse(GridReqDocs.DataKeys[rowIndex].Values["BondRequiredDocumentID"].ToString()); //
                    DeleteRoadAssist(index);
                }

            }



            catch (Exception ex)
            {

            }
        }

        private void FillReqDocsGrid()
        {
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

            GridReqDocs.DataSource = null;
            DataTable dt = null;

            taskControl.BondsReqDocCollection.AcceptChanges();

            dt = taskControl.BondsReqDocCollection;

            if (dt.Rows.Count != 0)
            {
                GridReqDocs.DataSource = dt;
                GridReqDocs.DataBind();

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    ((CheckBox)GridReqDocs.Rows[i].FindControl("chkSelect")).Checked = (bool)dt.Rows[i]["Checked"];
                }

                GridReqDocs.Visible = true;
            }
            else
            {
                txtReqDocuments.Text = "";

                //this.GridVehicle.Visible = true;
                this.GridReqDocs.DataSource = null;
                this.GridReqDocs.DataBind();

            }
        }

        protected void GridReqDocs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void FillReqDocsGridLoad()
        {

        }

        protected void btnAddVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                if (txtReqDocuments.Text.Trim() == "")
                {
                    return;
                }

                string tempDocID = "0";

                if (txtIDRoadAssist.Text.Trim() != "")
                {
                    for (int i = 0; i < taskControl.BondsReqDocCollection.Rows.Count; i++)
                    {
                        if (taskControl.BondsReqDocCollection.Rows[i]["BondRequiredDocumentID"].ToString() == txtIDRoadAssist.Text.Trim())
                        {
                            tempDocID = taskControl.BondsReqDocCollection.Rows[i]["BondRequiredDocumentID"].ToString();
                            taskControl.BondsReqDocCollection.Rows.RemoveAt(i);
                        }
                    }
                    taskControl.BondsReqDocCollection.AcceptChanges();
                }


                //////////////////////
                DataTable dt = null;
                DataRow myRow = taskControl.BondsReqDocCollection.NewRow();
                myRow["TaskControlID"] = "0";
                myRow["RequiredDocumentDesc"] = txtReqDocuments.Text.Trim();
                myRow["Checked"] = false;

                //myRow["HasCoverageExplain"] = TxtExplain.Text.Trim();
                Login.Login cp = HttpContext.Current.User as Login.Login;


                this.btnAddVehicle.Text = "Add";
                txtReqDocuments.Text = "";

                taskControl.BondsReqDocCollection.Rows.Add(myRow);
                taskControl.BondsReqDocCollection.AcceptChanges();

                dt = taskControl.BondsReqDocCollection;
                //FillBondReqDocumentsGridLoad();


                //this.GridVehicle.CurrentPageIndex = 0;
                this.GridReqDocs.Visible = true;
                this.GridReqDocs.DataSource = dt;
                this.GridReqDocs.DataBind();
                this.GridReqDocs.Visible = true;


                txtIDRoadAssist.Text = "";
            }
            catch (Exception xcp)
            {

                lblRecHeader.Text = xcp.Message;  //"You can only add a maximun of two cars.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
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

            //    if (rdlcDoc == "SolicitudGuardianXtra.rdlc")
            //    {

            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

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

            //        viewer.LocalReport.SetParameters(parameters);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();
            //    }

            //    if (rdlcDoc == "ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc")
            //    {

            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

            //        ReportParameter[] param = new ReportParameter[1];

            //        param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();
            //    }

            //    if (rdlcDoc == "PolizaAutoPersonalPR3Pagina1.rdlc")
            //    {


            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        ReportParameter[] param = new ReportParameter[1];

            //        param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();
            //    }

            //    if (rdlcDoc == "MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc")
            //    {

            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


            //        ReportParameter[] param = new ReportParameter[3];

            //        param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
            //        param[2] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();
            //    }

            //    if (rdlcDoc == "MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc")
            //    {

            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


            //        ReportParameter[] param = new ReportParameter[3];

            //        param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
            //        param[2] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();
            //    }

            //    if (rdlcDoc == "MidOceanInvoiceES.rdlc")
            //    {
            //        GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter ds = new GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter();
            //        GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable dt = new GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new ReportDataSource("GetPaymentByTaskControlID", (DataTable)dt);

            //        //ReportParameter[] param = new ReportParameter[1];

            //        //param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


            //        //viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();
            //    }

            //    // Variables 
            //    Warning[] warnings;
            //    string[] streamIds;
            //    string mimeType;
            //    string encoding = string.Empty;
            //    string extension;
            //    //  string fileName = "C" + taskControl.TaskControlID.ToString() + DateTime.Now.ToString().Trim();
            //    string _FileName = rdlcDoc.Replace(".rdlc", "") + taskControl.TaskControlID.ToString() + ".pdf";

            //    if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName))
            //        File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);

            //    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            //    using (FileStream fs = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName, FileMode.Create))
            //    {
            //        fs.Write(bytes, 0, bytes.Length);
            //        fs.Dispose();

            //    }
            //    return ProcessPath + _FileName;
            //}
            //catch (Exception ex)
            //{
            //    return "";
            //}

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

        protected void chkSameMailing_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameMailing.Checked)
            {
                txtPhyAddress.Text = TxtAddrs1.Text.Trim();
                txtPhyAddress2.Text = TxtAddrs2.Text.Trim();
                txtPhyState.Text = TxtState.Text.Trim();
                //ddlPhyZipCode.SelectedIndex = ddlZip.Items.IndexOf(ddlZip.Items.FindByText(ddlZip.SelectedItem.Text.ToString()));
                //ddlPhyCity.SelectedIndex = ddlCiudad.Items.IndexOf(ddlCiudad.Items.FindByText(ddlCiudad.SelectedItem.Text.ToString()));

                ddlPhyZipCode.Text = ddlZip.Text.Trim();
                ddlPhyCity.Text = ddlCiudad.Text.Trim();
            }
            if (!chkSameMailing.Checked)
            {
                txtPhyAddress.Text = "";
                txtPhyAddress2.Text = "";
                //ddlPhyZipCode.SelectedIndex = ddlPhyZipCode.SelectedIndex = -1;
                //ddlPhyCity.SelectedIndex = ddlPhyCity.SelectedIndex = -1;

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

        protected void txtExpDt_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtEffDt.Text.ToString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
            //{ 
            //    txtExpDt.Text = txtEffDt.Text; 
            //}
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

        protected void ddlZip_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (ddlZip.SelectedIndex > 0)
            //    {
            //        DataTable dtCiudad = EPolicy.LookupTables.LookupTables.GetCiudadByZipCode(ddlZip.SelectedItem.Text);

            //        if (dtCiudad.Rows[0]["Ciudad"].ToString() != "")
            //        {
            //            for (int i = 0; ddlCiudad.Items.Count - 1 >= i; i++)
            //            {
            //                if (ddlCiudad.Items[i].Text.Trim() == dtCiudad.Rows[0]["Ciudad"].ToString().Trim())
            //                {
            //                    ddlCiudad.SelectedIndex = i;
            //                    i = ddlCiudad.Items.Count - 1;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        protected void ddlPhyZipCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chkSameMailing.Checked = false;
            //if (ddlPhyZipCode.SelectedIndex > 0)
            //{
            //    DataTable dtCiudad = EPolicy.LookupTables.LookupTables.GetCiudadByZipCode(ddlPhyZipCode.SelectedItem.Text);

            //    if (dtCiudad.Rows[0]["Ciudad"].ToString() != "")
            //    {
            //        for (int i = 0; ddlPhyCity.Items.Count - 1 >= i; i++)
            //        {
            //            if (ddlPhyCity.Items[i].Text.Trim() == dtCiudad.Rows[0]["Ciudad"].ToString().Trim())
            //            {
            //                ddlPhyCity.SelectedIndex = i;
            //                i = ddlPhyCity.Items.Count - 1;
            //            }
            //        }
            //    }
            //}
        }

        protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlCiudad.SelectedIndex == 0)
            //{
            //    DataTable dtZipCode = EPolicy.LookupTables.LookupTables.GetZipCodeByDistinctCiudad(ddlCiudad.SelectedItem.Text);

            //    ddlZip.DataSource = dtZipCode;
            //    ddlZip.DataTextField = "ZipCode";
            //    ddlZip.DataValueField = "ZipCode";
            //    ddlZip.DataBind();
            //    ddlZip.Items.Insert(0, "");
            //    ddlZip.SelectedIndex = 0;
            //}
            //Comentado por Joshua
            ////if (ddlCiudad.SelectedIndex == 0)
            ////{
            ////    DataTable dtZipCode = EPolicy.LookupTables.LookupTables.GetTable("Ciudad");

            ////    ddlZip.DataSource = dtZipCode;
            ////    ddlZip.DataTextField = "ZipCode";
            ////    ddlZip.DataValueField = "ZipCode";
            ////    ddlZip.DataBind();
            ////    ddlZip.Items.Insert(0, "");
            ////    ddlZip.SelectedIndex = 0;
            //}
        }

        protected void ddlPhyCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chkSameMailing.Checked = false;
            //if (ddlPhyCity.SelectedIndex == 0)
            //{
            //    DataTable dtZipCode = EPolicy.LookupTables.LookupTables.GetZipCodeByDistinctCiudad(ddlPhyCity.SelectedItem.Text);

            //    ddlPhyZipCode.DataSource = dtZipCode;
            //    ddlPhyZipCode.DataTextField = "ZipCode";
            //    ddlPhyZipCode.DataValueField = "ZipCode";
            //    ddlPhyZipCode.DataBind();
            //    ddlPhyZipCode.Items.Insert(0, "");
            //    ddlPhyZipCode.SelectedIndex = 0;
            //}
            //Comentado por Joshua
            //if (ddlPhyCity.SelectedIndex == 0)
            //{
            //    DataTable dtZipCode = EPolicy.LookupTables.LookupTables.GetTable("Ciudad");

            //    ddlPhyZipCode.DataSource = dtZipCode;
            //    ddlPhyZipCode.DataTextField = "ZipCode";
            //    ddlPhyZipCode.DataValueField = "ZipCode";
            //    ddlPhyZipCode.DataBind();
            //    ddlPhyZipCode.Items.Insert(0, "");
            //    ddlPhyZipCode.SelectedIndex = 0;
            //}
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

        protected void ddlTypeBonds_SelectedIndexChanged(object sender, EventArgs e)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            if (cp.IsInRole("BONDSVI"))
            {
                switch (ddlTypeBonds.SelectedItem.Value)
                {
                    case "1":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("14"));
                        txtPenalty.Text = "$5,000.00";
                        txtPenalty.Enabled = false;
                        break;
                    case "3":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("15"));
                        txtPenalty.Text = "$10,000.00";
                        txtPenalty.Enabled = false;
                        break;
                    case "5":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("12"));
                        lblCompanyName.ForeColor = System.Drawing.Color.Red;
                        txtCompanyName.Enabled = true;
                        txtPenalty.Text = "";
                        txtPenalty.Enabled = true;
                        break;
                    case "17":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("21"));
                        lblCompanyName.ForeColor = System.Drawing.Color.Red;
                        txtCompanyName.Enabled = true;
                        txtPenalty.Text = "";
                        txtPenalty.Enabled = true;
                        break;
                    case "12":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("20"));
                        lblCompanyName.ForeColor = System.Drawing.Color.Red;
                        txtCompanyName.Enabled = true;
                        txtPenalty.Text = "$50,000.00";
                        txtPenalty.Enabled = false;
                        break;
                    case "16":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("18"));
                        txtPenalty.Text = "$10,000.00";
                        txtPenalty.Enabled = false;
                        break;
                    case "602":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("16"));
                        lblCompanyName.ForeColor = System.Drawing.Color.Red;
                        txtCompanyName.Enabled = true;
                        txtPenalty.Text = "";
                        txtPenalty.Enabled = true;
                        break;
                    case "4":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("17"));
                        txtPenalty.Text = "$10,000.00";
                        txtPenalty.Enabled = false;
                        break;
                    case "2":
                        ddlObligee.SelectedIndex = ddlObligee.Items.IndexOf(
                            ddlObligee.Items.FindByValue("19"));
                        txtPenalty.Text = "$1,000.00";
                        txtPenalty.Enabled = false;
                        break;
                    default:
                        ddlObligee.SelectedIndex = 0;
                        break;
                }
            }
        }


        protected void ddlObligee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedItem.Text == "Autoridad de Acueductos y Alcantarillados" || ((DropDownList)sender).SelectedItem.Text == "Autoridad de Energía Eléctrica" ||
                ddlObligee.SelectedItem.Text == "Guarantee" || ddlObligee.SelectedItem.Text == "Lease" || ddlObligee.SelectedItem.Text == "Notary Public" ||
                ddlObligee.SelectedItem.Text == "Notary Public 2" || ddlObligee.SelectedItem.Text == "Financial Guarantee - Miscellaneous")
            {
                HideControls(txtAccNumber, true);
                HideControls(lblAccNumber, true);
                divAcctNumber.Visible = true;

                if (ddlObligee.SelectedItem.Text == "Guarantee" || ddlObligee.SelectedItem.Text == "Lease" || ddlObligee.SelectedItem.Text == "Notary Public" ||
                    ddlObligee.SelectedItem.Text == "Notary Public 2" || ddlObligee.SelectedItem.Text == "Financial Guarantee - Miscellaneous")
                {
                    lblAccNumber.Text = "Obligee Description";
                }
                else
                {
                    lblAccNumber.Text = "Account Number";
                }
            }
            else
            {
                HideControls(txtAccNumber, false);
                HideControls(lblAccNumber, false);
                txtAccNumber.Text = "";
                divAcctNumber.Visible = false;
            }

            if (ddlObligee.SelectedItem.Text.Contains("ASC") || ddlObligee.SelectedItem.Text == "Hacienda - Marbetes")
            {
                HideControls(txtLocationName, true);
                HideControls(lblLocationName, true);
                txtLocationName.Text = "";
                divLocationName.Visible = true;

                HideControls(txtCantidadPrestada, true);
                HideControls(lblCantidadPrestada, true);
                txtCantidadPrestada.Text = "";
                divCantidadPrestada.Visible = true;


                if (ddlObligee.SelectedItem.Text.Contains("10,000"))
                {
                    txtPenalty.Text = "$10,000";
                }
                else if (ddlObligee.SelectedItem.Text.Contains("24,000"))
                {
                    txtPenalty.Text = "$24,000";
                }
            }
            else
            {
                HideControls(txtLocationName, false);
                HideControls(lblLocationName, false);
                txtLocationName.Text = "";
                divLocationName.Visible = false;

                HideControls(txtCantidadPrestada, false);
                HideControls(lblCantidadPrestada, false);
                txtCantidadPrestada.Text = "";
                divCantidadPrestada.Visible = false;
            }

            //if (ddlObligee.SelectedItem.Text.Contains("Mortgage Lenders") || ddlObligee.SelectedItem.Text.Contains("Mortgage Loan Originator"))
            //{
            //    ddlPaymentAmount.Enabled = true;
            //}
            //else
            //{
            //    ddlPaymentAmount.SelectedIndex = 0;
            //    ddlPaymentAmount.Enabled = false;
            //}

            if (ddlObligee.SelectedItem.Text.Contains("Non Resident Brokers") || ddlObligee.SelectedItem.Text.Contains("Resident Line Brokers") ||
                ddlObligee.SelectedItem.Text.Contains("Surplus Lines Broker"))
            {
                txtPenalty.Text = "$10,000.00";
                txtPenalty.Enabled = false;
            }

            if (ddlObligee.SelectedItem.Text.Contains("Power of Attorney"))
            {
                txtPenalty.Text = "$0.00";
                txtPenalty.Enabled = false;
            }

            if (ddlObligee.SelectedItem.Text.Contains("Process Server"))
            {
                txtPenalty.Text = "$1,000.00";
                txtPenalty.Enabled = false;
            }

            if (ddlObligee.SelectedItem.Text.Contains("Private Investigative Agency Surety"))
            {
                txtPenalty.Text = "$50,000.00";
                txtPenalty.Enabled = false;
                lblCompanyName.ForeColor = System.Drawing.Color.Red;
                txtCompanyName.Enabled = true;
            }

            if (ddlObligee.SelectedItem.Text.Contains("Notary Public - VI"))
            {
                txtPenalty.Text = "$5,000.00";
                txtPenalty.Enabled = false;
            }

            if (ddlObligee.SelectedItem.Text.Contains("Mortgage Brokers Bond") || ddlObligee.SelectedItem.Text.Contains("Mortgage Loan Originator"))
            {
                txtPenalty.Text = "";
                txtPenalty.Enabled = true;
            }

            if (ddlObligee.SelectedItem.Value.ToString() == "21" || ddlObligee.SelectedItem.Value.ToString() == "12" || ddlObligee.SelectedItem.Value.ToString() == "20" ||
                ddlObligee.SelectedItem.Value.ToString() == "16")
            {
                lblCompanyName.ForeColor = System.Drawing.Color.Red;
                txtCompanyName.Enabled = true;
            }

            if (ddlObligee.SelectedItem.Value.ToString() == "12" || ddlObligee.SelectedItem.Value.ToString() == "13" || ddlObligee.SelectedItem.Value.ToString() == "14" ||
                ddlObligee.SelectedItem.Value.ToString() == "15" || ddlObligee.SelectedItem.Value.ToString() == "16" || ddlObligee.SelectedItem.Value.ToString() == "17" ||
                ddlObligee.SelectedItem.Value.ToString() == "18" || ddlObligee.SelectedItem.Value.ToString() == "19" || ddlObligee.SelectedItem.Value.ToString() == "20" ||
                ddlObligee.SelectedItem.Value.ToString() == "21")
            {
                LabelSignature.ForeColor = System.Drawing.Color.Red;
            }



        }

        private DataTable GetPaymentByTaskControlID()
        {

            DataTable dt = null;
            try
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

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

                if (chkIsRenew.Checked)
                {
                    //cmd.Parameters.AddWithValue("@RenewalOf", TxtPolicyNo.Text.Trim().Substring(0, TxtPolicyNo.Text.Trim().IndexOf("-")).Replace("-", ""));

                    if (txtPolicyNoToRenew.Text.Trim().Contains("-"))
                    {
                        cmd.Parameters.AddWithValue("@PolicyID", txtPolicyNoToRenew.Text.Trim().Substring(0, txtPolicyNoToRenew.Text.Trim().IndexOf("-")).Replace("-", "") + "-" + DateTime.Parse(txtEffDt.Text.Trim()).Year.ToString().Substring(2).ToString());//txtPolicyNoToRenew.Text.Trim().Substring(0, txtPolicyNoToRenew.Text.Trim().IndexOf("-")).Replace("-", ""));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PolicyID", txtPolicyNoToRenew.Text.Trim() + "-" + DateTime.Parse(txtEffDt.Text.Trim()).Year.ToString().Substring(2).ToString());//txtPolicyNoToRenew.Text.Trim().Substring(0, txtPolicyNoToRenew.Text.Trim().IndexOf("-")).Replace("-", ""));
                    }
                    cmd.Parameters.AddWithValue("@RenewalOf", txtPolicyNoToRenew.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@RenewalOf", "");
                }

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
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
                taskControl.Customer.Description = ClientID;
                taskControl.PolicyNo = TxtPolicyNo.Text;
                UpdatePolicyFromPPSByTaskControlID(TaskControlID, TxtPolicyNo.Text, ClientID);
            }
        }

        protected void txtPremium_TextChanged(object sender, EventArgs e)
        {
            txtPremium.Text = String.Format("{0:c2}", int.Parse(txtPremium.Text.ToString().Trim(), System.Globalization.NumberStyles.Currency));
            txtPremium.Focus();
            Page.MaintainScrollPositionOnPostBack = true;
        }

        protected void txtPenalty_TextChanged(object sender, EventArgs e)
        {
            txtPenalty.Text = String.Format("{0:c2}", double.Parse(txtPenalty.Text.ToString().Trim(), System.Globalization.NumberStyles.Currency));
            txtPenalty.Focus();
            Page.MaintainScrollPositionOnPostBack = true;
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

        //protected void chkIsBusinessAutoQuote_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtCompanyName.Enabled = chkIsBusinessAutoQuote.Checked;
        //}

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
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                DataTable dt = null;

                taskControl.BondsReqDocCollection.Rows.RemoveAt(DocID);
                taskControl.BondsReqDocCollection.AcceptChanges();

                dt = taskControl.BondsReqDocCollection;

                FillReqDocsGrid();
            }
            catch (Exception xcp)
            {
                throw new Exception("Error deleting row.", xcp);
            }

        }

        protected void DocumentChecked_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
                GridViewRow row = (sender as CheckBox).Parent.Parent as GridViewRow;
                int index = row.RowIndex;
                taskControl.BondsReqDocCollection.Rows[index]["Checked"] = ((CheckBox)sender).Checked;
                taskControl.BondsReqDocCollection.AcceptChanges();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomerTypeSelection();
        }

        protected void CustomerTypeSelection()
        {
            try
            {
                if (ddlType.SelectedItem.Text.ToString() != "Individual")
                {
                    lblFirstName.ForeColor = System.Drawing.Color.Black;
                    lblLastName.ForeColor = System.Drawing.Color.Black;
                    lblCompanyName.ForeColor = System.Drawing.Color.Red;
                    txtCompanyName.Enabled = true;
                }
                else
                {
                    lblFirstName.ForeColor = System.Drawing.Color.Red;
                    lblLastName.ForeColor = System.Drawing.Color.Red;
                    lblCompanyName.ForeColor = System.Drawing.Color.Black;
                    txtCompanyName.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString(), ex);
            }
        }
        protected void txtCantidadPrestada_TextChanged(object sender, EventArgs e)
        {
            txtCantidadPrestada.Text = String.Format("{0:c0}", int.Parse(txtCantidadPrestada.Text.ToString().Trim(), System.Globalization.NumberStyles.Currency));
        }

        protected bool CheckIfBondExistInPPS()
        {
            bool Found = false;

            try
            {
                string cn = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                //@"Data Source=GIC-MSQL\PPSSQLSERVER;Initial Catalog=AgentTestData;User ID=urclaims;password=3G@TD@t!1";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(cn))
                using (var cmd = new SqlCommand("sproc_ConsumeXMLePPS-BONDS_Verify", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("PolicyID", txtPolicyNoToRenew.Text.Trim());
                    da.Fill(table);
                }

                if (table.Rows.Count > 0)
                {
                    EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                    Found = true;

                    //FILL FIELDS WITH PPS INFO

                    taskControl.Customer.Description = table.Rows[0]["Client"].ToString();

                    if (DateTime.Parse(DateTime.Now.ToShortDateString()) <= DateTime.Parse(table.Rows[0]["Expire"].ToString()))
                    {
                        txtEffDt.Text = DateTime.Parse(table.Rows[0]["Expire"].ToString()).ToShortDateString();
                        txtExpDt.Text = DateTime.Parse(table.Rows[0]["Expire"].ToString()).AddYears(1).ToShortDateString();
                    }
                    else
                    {
                        txtEffDt.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString();
                        txtExpDt.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).AddYears(1).ToShortDateString();
                    }



                    TxtAddrs1.Text = table.Rows[0]["Maddr1"].ToString();
                    TxtAddrs2.Text = table.Rows[0]["Maddr2"].ToString();
                    ddlZip.Text = table.Rows[0]["Mzip"].ToString();
                    ddlCiudad.Text = table.Rows[0]["Mcity"].ToString();
                    TxtState.Text = table.Rows[0]["Mstate"].ToString();

                    txtPhyAddress.Text = table.Rows[0]["Raddr1"].ToString();
                    txtPhyAddress2.Text = table.Rows[0]["Raddr2"].ToString();
                    ddlPhyZipCode.Text = table.Rows[0]["Rzip"].ToString();
                    ddlPhyCity.Text = table.Rows[0]["Rcity"].ToString();
                    txtPhyState.Text = table.Rows[0]["Rstate"].ToString();

                    TxtHomePhone.Text = table.Rows[0]["Wphone"].ToString();
                    txtWorkPhone.Text = table.Rows[0]["Wphone"].ToString();
                    TxtCellular.Text = table.Rows[0]["Cphone"].ToString();

                    txtEmail.Text = table.Rows[0]["Eaddr"].ToString();

                    txtPenalty.Text = double.Parse(table.Rows[0]["Lim1"].ToString()).ToString("###,###.00");
                    txtPremium.Text = double.Parse(table.Rows[0]["Premium"].ToString()).ToString("###,###.00");

                    ddlTypeBonds.SelectedIndex = ddlTypeBonds.Items.IndexOf(
                        ddlTypeBonds.Items.FindByValue(table.Rows[0]["BNDType"].ToString()));

                    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(ddlAgent.Items.FindByValue(GetAgentByCarsID(table.Rows[0]["BrokerID"].ToString())));


                    if (bool.Parse(table.Rows[0]["BusFlag"].ToString()) == false) // INDIVIDUAL
                    {
                        TxtFirstName.Text = table.Rows[0]["FirstName"].ToString();
                        txtLastname1.Text = table.Rows[0]["LastName"].ToString();
                        TxtInitial.Text = table.Rows[0]["Middle"].ToString();
                        ddlType.SelectedValue = "1"; // table.Rows[0]["BusType"].ToString();

                        lblFirstName.ForeColor = System.Drawing.Color.Red;
                        lblLastName.ForeColor = System.Drawing.Color.Red;
                        lblCompanyName.ForeColor = System.Drawing.Color.Black;
                        txtCompanyName.Enabled = false;

                    }
                    else if (bool.Parse(table.Rows[0]["BusFlag"].ToString()) == true) // CORPORATE
                    {
                        TxtFirstName.Text = "";
                        txtLastname1.Text = "";
                        txtCompanyName.Text = table.Rows[0]["LastName"].ToString();
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

        protected void btnVerifyBondInPPS_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckIfBondExistInPPS())
                {
                    lblBondFound.Visible = true;
                    lblBondFound.Text = "Bond Verified!";
                    //TxtPolicyNo.Text = TxtPolicyNo.Text.Trim() + "-" + DateTime.Parse(txtEffDt.Text.Trim()).Year.ToString().Substring(2).ToString();
                }
                else
                {
                    lblBondFound.Visible = true;
                    lblBondFound.Text = "Bond NOT Verified";
                }
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

        private DataTable GetBondsPenaltyLimitByUserID(string AgentID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("AgentID", SqlDbType.VarChar, 50, AgentID.ToString(), ref cookItems);

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
                dt = exec.GetQuery("GetBondsPenaltyLimitByUserID", xmlDoc);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("There is no information to display, please try again (GetBondsPenaltyLimitByUserID).", ex);
            }
        }

        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            FillGridDocuments(true);
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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

        private void FillGridDocuments(bool Refresh)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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

        protected void btnAdjuntarCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
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

        //protected void btnIndemnityQuote_Click(object sender, EventArgs e)
        //{
        //    PrintIndemnityQuote();
        //}

        //protected void btnIndemnityPolicy_Click(object sender, EventArgs e)
        //{
        //    PrintIndemnityPolicy();
        //}

        public void PrintIndemnityQuote(List<string> mergePaths, string ProcessedPath)
        {
            try
            {
                FileInfo mFileIndex;
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/Bonds/Indemnity_Bond_Quote.pdf");
                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Indemnity_Bond_Quote" + taskControl.TaskControlID.ToString() + ".pdf", true);
                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Indemnity_Bond_Quote" + taskControl.TaskControlID.ToString() + ".pdf");

                DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);

                Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},
                { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}};

                PrintBondsMSWord("Indemnity_Bond_2", bookmarks, mergePaths, ProcessedPath);

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
                EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];

                DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}};

                PrintBondsMSWord("Indemnity_Bond", bookmarks, mergePaths, ProcessedPath);

                Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},
                { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}};

                PrintBondsMSWord("Indemnity_Bond_2", bookmarks2, mergePaths, ProcessedPath);

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }
}