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

namespace EPolicy
{
    public partial class RoadAssistance : System.Web.UI.Page
    {
        private string NAMECONVENTION = "";
        private string PolicyNumber = "";
        private string ClientID = "", InsuredVin = "", InsuredPlate = "", ReinsAsl = "";
        private string CUSTOMER2;
        private bool HasAccident12 = false;

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
                if (!cp.IsInRole("ROAD ASSISTANCE MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
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

                    int userID = 0;
                    userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                    EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];



                    if (Session["LookUpTables"] == null)
                    {
                        DataTable dtZipCode = null;
                        DataTable dtLocation = EPolicy.LookupTables.LookupTables.GetTable("Location");
                        DataTable dtAgency = EPolicy.LookupTables.LookupTables.GetTable("Agency");
                        DataTable dtAgent = EPolicy.LookupTables.LookupTables.GetTable("AgentAll");
                        DataTable dtAgentVI = EPolicy.LookupTables.LookupTables.GetTable("AgentVI");
                        DataTable dtInsuranceCompany = EPolicy.LookupTables.LookupTables.GetTable("InsuranceCompany");
                        DataTable dtSupplier = EPolicy.LookupTables.LookupTables.GetTable("Supplier");
                        DataTable dtCiudad = EPolicy.LookupTables.LookupTables.GetTable("Ciudad");

                        if (IsSTHOMAS())
                        {
                            dtZipCode = EPolicy.LookupTables.LookupTables.GetTable("VI_Zipcode");
                        }
                        else 
                        {
                            dtZipCode = EPolicy.LookupTables.LookupTables.GetTable("Ciudad");
                        }

                        DataTable dtVehicleModel = EPolicy.LookupTables.LookupTables.GetTable("VehicleModel");
                        DataTable dtVehicleMake = EPolicy.LookupTables.LookupTables.GetTable("VehicleMake");
                        DataTable dtVehicleYear = EPolicy.LookupTables.LookupTables.GetTable("VehicleYear");
                        DataTable dtNewUse = EPolicy.LookupTables.LookupTables.GetTable("NewUse");
                        DataTable dtBank = EPolicy.LookupTables.LookupTables.GetTable("Bank");
                        DataTable dtDealer = EPolicy.Login.Login.GetGroupDealerByUserID(userID);
                        DataTable dtState = EPolicy.LookupTables.LookupTables.GetTable("VI_VehicleTerritory");


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

                        DataTable dtAgt = dtAgent;
                        if (IsSTHOMAS())
                        {
                            dtAgt = dtAgentVI;
                        }
                        //Agent
                        ddlAgent.DataSource = dtAgt;
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

                        //Ciudad
                        ddlCiudad.DataSource = dtCiudad;
                        ddlCiudad.DataTextField = "Ciudad";
                        ddlCiudad.DataValueField = "ZipCode";
                        ddlCiudad.DataBind();
                        ddlCiudad.SelectedIndex = -1;
                        ddlCiudad.Items.Insert(0, "");

                        //Ciudad Fisica
                        ddlPhyCity.DataSource = dtCiudad;
                        ddlPhyCity.DataTextField = "Ciudad";
                        ddlPhyCity.DataValueField = "ZipCode";
                        ddlPhyCity.DataBind();
                        ddlPhyCity.SelectedIndex = -1;
                        ddlPhyCity.Items.Insert(0, "");

                        //ZipCode
                        ddlZip.DataSource = dtZipCode;
                        ddlZip.DataTextField = "Zipcode";
                        ddlZip.DataValueField = "ZipCode";
                        ddlZip.DataBind();
                        ddlZip.SelectedIndex = -1;
                        ddlZip.Items.Insert(0, "");

                        //Zipcode Fisico
                        ddlPhyZipCode.DataSource = dtZipCode;
                        ddlPhyZipCode.DataTextField = "Zipcode";
                        ddlPhyZipCode.DataValueField = "ZipCode";
                        ddlPhyZipCode.DataBind();
                        ddlPhyZipCode.SelectedIndex = -1;
                        ddlPhyZipCode.Items.Insert(0, "");

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

                        //Vehicle Model

                        ddlVehicleModel.DataSource = dtVehicleModel;
                        ddlVehicleModel.DataTextField = "VehicleModelDesc";
                        ddlVehicleModel.DataValueField = "VehicleModelID";
                        ddlVehicleModel.DataBind();
                        ddlVehicleModel.SelectedIndex = -1;
                        ddlVehicleModel.Items.Insert(0, "");

                        //Vehicle Make

                        ddlVehicleMake.DataSource = dtVehicleMake;
                        ddlVehicleMake.DataTextField = "VehicleMakeDesc";
                        ddlVehicleMake.DataValueField = "VehicleMakeID";
                        ddlVehicleMake.DataBind();
                        ddlVehicleMake.SelectedIndex = -1;
                        ddlVehicleMake.Items.Insert(0, "");

                        //Vehicle Year

                        dtVehicleYear = YearLimit(dtVehicleYear, 21).Copy();
                        ddlVehicleYear.DataSource = dtVehicleYear;
                        ddlVehicleYear.DataTextField = "VehicleYearDesc";
                        ddlVehicleYear.DataValueField = "VehicleYearID";
                        ddlVehicleYear.DataBind();
                        ddlVehicleYear.SelectedIndex = -1;
                        ddlVehicleYear.Items.Insert(0, "");

                        //Vehicle New/Use

                        ddlNewUsed.DataSource = dtNewUse;
                        ddlNewUsed.DataTextField = "NewUseDesc";
                        ddlNewUsed.DataValueField = "NewUseID";
                        ddlNewUsed.DataBind();
                        ddlNewUsed.SelectedIndex = -1;
                        ddlNewUsed.Items.Insert(0, "");

                        //State
                        ddlState.DataSource = dtState;
                        ddlState.DataTextField = "VehicleTerritoryCode";
                        ddlState.DataValueField = "VehicleTerritoryID";
                        ddlState.DataBind();
                        ddlState.SelectedIndex = -1;
                        ddlState.Items.Insert(0, "");

                        //State
                        ddlState2.DataSource = dtState;
                        ddlState2.DataTextField = "VehicleTerritoryCode";
                        ddlState2.DataValueField = "VehicleTerritoryID";
                        ddlState2.DataBind();
                        ddlState2.SelectedIndex = -1;
                        ddlState2.Items.Insert(0, "");



                        Session.Add("LookUpTables", "In");
                    }

                    MyAccordion.SelectedIndex = 0;
                    Accordion1.SelectedIndex = -1;
                    Accordion2.SelectedIndex = -1;
                    Accordion3.SelectedIndex = -1;
                    FillRoadAssistVehiclesGridLoad();

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
                        EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
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
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

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

            //if (IsSTHOMAS())
            //{
            //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(ddlAgent.Items.FindByValue("049"));
            //}
            //else
            //{
            //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(ddlAgent.Items.FindByValue("048"));
            //}

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
                        ddlAgent.Items.FindByValue("000"));
                }

            }
            else if (cp.IsInRole("AUTO VI AGENCY"))
            {
                DataTable dtAgent = null;

                DataTable DtAgentVI = null;

                if (IsSTHOMAS())
                    DtAgentVI = Login.Login.GetGroupAgentByUserID(cp.UserID);
                else
                    DtAgentVI = Login.Login.GetGroupAgentVIByUserID(cp.UserID);

                //string Agent = "000";



                if (DtAgentVI.Rows.Count > 0)
                {
                    //ddlAgent.Items.Clear();
                    //Agent
                    ddlAgent.DataSource = DtAgentVI;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
                    {
                        //ddlAgent.Items.FindByValue(taskControl.Agent.Trim()).Selected = true;

                        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                            ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));
                    }
                    //else if (taskControl.Agent.Trim() == "000" && taskControl.Mode == 1)//|| taskControl.Agent.Trim() == "")
                    //{
                    //    ddlAgent.SelectedIndex = 1;
                    //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                    //        ddlAgent.Items.FindByValue(ddlAgent.SelectedItem.Value));
                    //}
                    else
                    {
                        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                            ddlAgent.Items.FindByValue("000"));
                    }

                    //Agent = DtAgentVI.Rows[0]["AgentID"].ToString();

                    //taskControl.Agent = Agent.Trim();
                }
                else
                {
                    //Agent
                    if (ddlAgent.SelectedIndex > 0 && ddlAgent.SelectedItem != null)
                        taskControl.Agent = ddlAgent.SelectedItem.Value;
                    else
                        taskControl.Agent = "000";
                }
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

            ddlCiudad.SelectedIndex = 0;
            if (taskControl.Customer.City != "")
            {
                for (int i = 0; ddlCiudad.Items.Count - 1 >= i; i++)
                {
                    if (ddlCiudad.Items[i].Text.Trim() == taskControl.Customer.City.ToString().Trim())
                    {
                        ddlCiudad.SelectedIndex = i;
                        i = ddlCiudad.Items.Count - 1;
                    }
                }
            }

            //if (taskControl.Customer.CityPhysical != "")
            //{
            //    ddlPhyCity.SelectedIndex = ddlPhyCity.Items.IndexOf(ddlPhyCity.Items.FindByValue(taskControl.Customer.CityPhysical.ToString()));
            //}


            ddlPhyCity.SelectedIndex = 0;
            if (taskControl.Customer.CityPhysical != "")
            {
                for (int i = 0; ddlPhyCity.Items.Count - 1 >= i; i++)
                {
                    if (ddlPhyCity.Items[i].Text.Trim() == taskControl.Customer.CityPhysical.ToString().Trim())
                    {
                        ddlPhyCity.SelectedIndex = i;
                        i = ddlPhyCity.Items.Count - 1;
                    }
                }
            }

            SetDDLValue(ddlCiudad, taskControl.Customer.City, "Text");
            SetDDLValue(ddlZip, taskControl.Customer.ZipCode, "Text");
            SetDDLValue(ddlPhyCity, taskControl.Customer.CityPhysical, "Text");
            SetDDLValue(ddlPhyZipCode, taskControl.Customer.ZipPhysical, "Text");

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
            TxtBirthdate.Text = taskControl.Customer.Birthday;

            TxtFirstName.Text = taskControl.Customer.FirstName;
            TxtInitial.Text = taskControl.Customer.Initial;
            txtLastname1.Text = taskControl.Customer.LastName1;
            txtLastname2.Text = taskControl.Customer.LastName2;
            TxtAddrs1.Text = taskControl.Customer.Address1;
            TxtAddrs2.Text = taskControl.Customer.Address2;


            if (IsSTHOMAS())
            {
                ddlState.SelectedIndex = ddlState.Items.IndexOf(
                    ddlState.Items.FindByValue(taskControl.Customer.State));
                SetDDLValue(ddlState, taskControl.Customer.State, "Text");

                DataTable dt = GetZipcodeByState(ddlState.SelectedItem.Text);

                ddlZip.SelectedIndex = -1;

                ddlZip.DataSource = dt;
                ddlZip.DataTextField = "Zipcode";
                ddlZip.DataValueField = "ZipCode";
                ddlZip.DataBind();
                ddlZip.SelectedIndex = -1;
                ddlZip.Items.Insert(0, "");

                ddlZip.SelectedIndex = ddlZip.Items.IndexOf(
                        ddlZip.Items.FindByValue(taskControl.Customer.ZipCode));
                SetDDLValue(ddlZip, taskControl.Customer.ZipCode, "Text");

                if (taskControl.Customer.StatePhysical != "")
                    ddlState2.SelectedIndex = ddlState2.Items.IndexOf(
                        ddlState2.Items.FindByValue(taskControl.Customer.StatePhysical));
                SetDDLValue(ddlState2, taskControl.Customer.StatePhysical, "Text");

                DataTable dt2 = GetZipcodeByState(ddlState2.SelectedItem.Text);

                ddlPhyZipCode.SelectedIndex = -1;

                ddlPhyZipCode.DataSource = dt2;
                ddlPhyZipCode.DataTextField = "Zipcode";
                ddlPhyZipCode.DataValueField = "ZipCode";
                ddlPhyZipCode.DataBind();
                ddlPhyZipCode.SelectedIndex = -1;
                ddlPhyZipCode.Items.Insert(0, "");

                ddlPhyZipCode.SelectedIndex = ddlPhyZipCode.Items.IndexOf(
                ddlPhyZipCode.Items.FindByValue(taskControl.Customer.ZipPhysical));
                SetDDLValue(ddlPhyZipCode, taskControl.Customer.ZipPhysical, "Text");
            }
            else
            {
                TxtState.Text = taskControl.Customer.State == "" ? "PR" : taskControl.Customer.State;
                txtPhyState.Text = taskControl.Customer.StatePhysical == "" ? "PR" : taskControl.Customer.StatePhysical;
            }

            TxtHomePhone.Text = taskControl.Customer.HomePhone;
            txtWorkPhone.Text = taskControl.Customer.JobPhone;
            TxtCellular.Text = taskControl.Customer.Cellular;
            txtEmail.Text = taskControl.Customer.Email;
            TxtPolicyNo.Text = taskControl.PolicyNo;
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
            txtTotalPremium.Text = taskControl.TotalPremium.ToString("###,###.00"); //Se descomento para que llenara el textbox y se pueda salvar la poliza sin cambiar el dropdown de premium
            txtPremium.Text = taskControl.TotalPremium.ToString("###,###.00"); //Se descomento para que llenara el textbox y se pueda salvar la poliza sin cambiar el dropdown de premium


            txtPhyAddress.Text = taskControl.Customer.AddressPhysical1;
            txtPhyAddress2.Text = taskControl.Customer.AddressPhysical2;

            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
            if (taskControl.Customer.SocialSecurity.Trim() != "")
            {
                TxtSocialSec.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                TxtSocialSec.Text = new string('*', TxtSocialSec.Text.Trim().Length - 4) + TxtSocialSec.Text.Trim().Substring(TxtSocialSec.Text.Trim().Length - 4);
                MaskedEditExtender1.Mask = "???-??-9999";
            }
            else
                TxtSocialSec.Text = "";



            // txtVehicleVIN.Text = taskControl.VIN;
            //txtVehiclePlate.Text = taskControl.Plate;




            DataTable GXAuto = null;

            if (taskControl.TaskControlID != 0)
            {
                // GXAuto = GetAutoRoadAssistByTaskControlID(int.Parse(LblControlNo.Text.Trim() != "" ? LblControlNo.Text : "0"));

                GXAuto = GetXtraAutoByTaskControlID(int.Parse(LblControlNo.Text.Trim() != "" ? LblControlNo.Text : "0"));

                if (GXAuto.Rows.Count > 0)
                {
                    //txtVehicleVIN.Text = GXAuto.Rows[0]["VIN"].ToString().Trim();

                    //rdbDefpaymentYes.Checked = bool.Parse(GXAuto.Rows[0]["DefferedPayment"].ToString().Trim());
                    // rdbDefpaymentNo.Checked = !bool.Parse(GXAuto.Rows[0]["DefferedPayment"].ToString().Trim());

                    //rdbCoverageYes.Checked = bool.Parse(GXAuto.Rows[0]["HasCoverage"].ToString().Trim());
                    //rdbCoverageNo.Checked = !bool.Parse(GXAuto.Rows[0]["HasCoverage"].ToString().Trim());
                    //chkDefpayfour.Checked = bool.Parse(GXAuto.Rows[0]["IsFourPayment"].ToString().Trim());
                    //chkDefpaysix.Checked = bool.Parse(GXAuto.Rows[0]["IsSixPayment"].ToString().Trim());

                    chkSameMailing.Checked = bool.Parse(taskControl.Customer.SamesAsMail.ToString());

                    //chkIsPersonalAuto.Checked = GXAuto.Rows[0]["IsPersonalAuto"] != System.DBNull.Value ? (bool)GXAuto.Rows[0]["IsPersonalAuto"] : false;
                    //chkIsCommercialAuto.Checked = GXAuto.Rows[0]["IsCommercialAuto"] != System.DBNull.Value ? (bool)GXAuto.Rows[0]["IsCommercialAuto"] : false;
                    //chkIsPersonalAuto.Checked = bool.Parse(GXAuto.Rows[0]["IsPersonalAuto"].ToString().Trim());
                    //chkIsCommercialAuto.Checked = bool.Parse(GXAuto.Rows[0]["IsCommercialAuto"].ToString().Trim());
                    chkCredit.Checked = GXAuto.Rows[0]["IsCreditPayment"] != System.DBNull.Value ? (bool)GXAuto.Rows[0]["IsCreditPayment"] : false;
                    chkDebit.Checked = GXAuto.Rows[0]["IsDebitPayment"] != System.DBNull.Value ? (bool)GXAuto.Rows[0]["IsDebitPayment"] : false;
                    chkCash.Checked = GXAuto.Rows[0]["IsCashPayment"] != System.DBNull.Value ? (bool)GXAuto.Rows[0]["IsCashPayment"] : false;

                    TxtExplain.Visible = rdbCoverageYes.Checked;
                    lblExplain.Visible = rdbCoverageYes.Checked;


                    TxtExplain.Visible = !rdbCoverageNo.Checked;
                    lblExplain.Visible = !rdbCoverageNo.Checked;

                    //lblSelectplan.Visible = rdbDefpaymentYes.Checked;
                    //chkDefpayfour.Visible = rdbDefpaymentYes.Checked;
                    // chkDefpaysix.Visible = rdbDefpaymentYes.Checked;


                    //lblSelectplan.Visible = !rdbDefpaymentNo.Checked;
                    //chkDefpayfour.Visible = !rdbDefpaymentNo.Checked;
                    //chkDefpaysix.Visible = !rdbDefpaymentNo.Checked;


                    //for (int i = 0; ddlVehicleMake.Items.Count - 1 >= i; i++)
                    //{
                    //    if (ddlVehicleMake.Items[i].Text.Trim() == GXAuto.Rows[0]["VehicleMake"].ToString().Trim())
                    //    {
                    //        ddlVehicleMake.SelectedIndex = i;
                    //        i = ddlVehicleMake.Items.Count - 1;
                    //    }
                    //}

                    //for (int i = 0; ddlVehicleModel.Items.Count - 1 >= i; i++)
                    //{
                    //    if (ddlVehicleModel.Items[i].Text.Trim() == GXAuto.Rows[0]["VehicleModel"].ToString().Trim())
                    //    {
                    //        ddlVehicleModel.SelectedIndex = i;
                    //        i = ddlVehicleModel.Items.Count - 1;
                    //    }
                    //}

                    //for (int i = 0; ddlVehicleYear.Items.Count - 1 >= i; i++)
                    //{
                    //    if (ddlVehicleYear.Items[i].Text.Trim() == GXAuto.Rows[0]["VehicleYear"].ToString().Trim())
                    //    {
                    //        ddlVehicleYear.SelectedIndex = i;
                    //        i = ddlVehicleYear.Items.Count - 1;
                    //    }
                    //}

                    // txtVehiclePlate.Text = GXAuto.Rows[0]["Plate"].ToString().Trim();
                    txtPremium.Text = "$" + "" + GXAuto.Rows[0]["Premium"].ToString().Trim();
                    //TxtExplain.Text = GXAuto.Rows[0]["HasCoverageExplain"].ToString();


                    //if (GXAuto.Rows[0]["Premium"].ToString().Trim() == "89")
                    //{
                    //    txtDeducible.Text = "$200";
                    //}

                    //else if (GXAuto.Rows[0]["Premium"].ToString().Trim() == "95")
                    //{
                    //    txtDeducible.Text = "$150";
                    //}

                    //else if (GXAuto.Rows[0]["Premium"].ToString().Trim() == "100")
                    //{
                    //    txtDeducible.Text = "$100";
                    //}

                    if (txtPremium.Text != "")
                    {
                        for (int i = 0; ddlDeducible.Items.Count - 1 >= i; i++)
                        {
                            if (ddlDeducible.Items[i].Text.Trim() == "$" + taskControl.TotalPremium.ToString().Trim())
                            {
                                ddlDeducible.SelectedIndex = i;
                                i = ddlDeducible.Items.Count - 1;
                            }
                        }


                    }
                    txtPremium.Text = "$" + taskControl.TotalPremium.ToString();
                }

                DataTable dtHasaccident = null;

                dtHasaccident = GetGuadianXtraHasAccident12(taskControl.TaskControlID);

                if (dtHasaccident.Rows.Count > 0)
                {
                    if (dtHasaccident.Rows[0]["HasAccident12"].ToString() == "False")
                    {
                        chkHasAccident12.Checked = false;
                    }
                    else
                    {
                        chkHasAccident12.Checked = true;
                    }
                }
                ddlAgent.Enabled = false;
            }
        }

        public void DisableControls()
        {

            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            //Botones 

            btnCashPayment.Enabled = false;

            if (taskControl.TaskControlID.ToString() != "0")
            {
                DataTable dtPayment = GetPaymentByTaskControlID();

                btnPrintInvoice.Visible = false;
                btnPrintPolicy.Visible = false; // false Alexis
                ddlPrintOption.Visible = false; 
                btnGuardianPay.Visible = true;


                for (int i = 0; i < dtPayment.Rows.Count; i++) // YA PAGO LA Poliza
                {
                    if (dtPayment.Rows[i]["Result"].ToString().Trim() == "Success")
                    {
                        btnPrintInvoice.Visible = true; //True alexis
                        btnPrintPolicy.Visible = true;
                        ddlPrintOption.Visible = true; // True alexis
                        btnGuardianPay.Visible = false;

                        //try
                        //{
                            
                        //    if (taskControl.Mode == 4 && taskControl.Trams_FL == false)//&& btnCancel.Visible == false)
                        //    {
                        //        PolicyXML(taskControl.TaskControlID);

                        //        taskControl.Trams_FL = true;
                        //    }
                        //}
                        //catch(Exception exp)
                        //{
                        //    LogError(exp);
                        //}


                        break;
                    }
                    else
                    {
                        btnPrintInvoice.Visible = false;
                        btnPrintPolicy.Visible = false; //False Alexis
                        ddlPrintOption.Visible = false;
                        btnGuardianPay.Visible = true;
                    }
                }

                if (dtPayment.Rows.Count == 0)
                {
                    if (chkCash.Checked)
                    {
                        btnCashPayment.Enabled = true;
                        btnCashPayment.Visible = true;
                        btnCashPayment.Text = "Pay Cash: " + txtPremium.Text;

                        btnGuardianPay.Visible = false;
                    }
                }
                else
                {
                    if (chkCash.Checked)
                    {
                        btnCashPayment.Enabled = false;
                        btnCashPayment.Visible = true;
                        btnCashPayment.Text = "PAID";//"Pay Cash: " + txtPremium.Text;

                        btnGuardianPay.Visible = false;
                    }
                }
            }


                btnAdd2.Visible = false;
            //btnNew.Visible = true;
                if (btnPrintPolicy.Visible == true)
                    btnEdit.Visible = false;
                else
                    btnEdit.Visible = true;

                BtnExit.Visible = true;
                BtnSave.Visible = false;
                btnCancel.Visible = false;
                //btnDelete.Visible = true;
                btnReinstallation.Visible = false;
                btnCancellation.Visible = false; // true alexis
                

                //DataTable dt = GetPaymentByTaskControlID();

            //si ya esta pagada por Guardian Pay no se hace visible el boton.

            //if (dt.Rows.Count > 0) // YA PAGO LA Poliza
            //{
            //    if (dt.Rows[0]["Result"].ToString().Trim() == "Success")
            //    {
            //        btnGuardianPay.Visible = false;
            //    }
            //    else
            //    {
            //        btnGuardianPay.Visible = true;
            //    }
            //}
            //else
            //{
            //    btnGuardianPay.Visible = true;
            //}

                chkDefpaysix.Enabled = false;
                chkDefpayfour.Enabled = false;
                chkCredit.Visible = true;
                chkDebit.Visible = false;
                chkCash.Visible = true;
                chkCredit.Enabled = false;
                chkDebit.Enabled = false;
                chkCash.Enabled = false;
                chkSameMailing.Visible = true;
                chkSameMailing.Enabled = false;
                chkIsPersonalAuto.Enabled = false;
                chkIsPersonalAuto.Visible = false;
                chkIsCommercialAuto.Visible = false;
                chkIsCommercialAuto.Enabled = false;
                ChkAutoAssignPolicy.Enabled = false;

                chkHasAccident12.Visible = false;
                chkHasAccident12.Enabled = false;

                rdbCoverageNo.Enabled = false;
                rdbCoverageYes.Enabled = false;
                //rdbDefpaymentYes.Visible = true;
                //rdbDefpaymentYes.Enabled = false;
                //rdbDefpaymentNo.Visible = true;
                //rdbDefpaymentNo.Enabled = false;
                rdbCoverageNo.Visible = false;
                rdbCoverageYes.Visible = false;


                TxtProspectNo.Enabled = false;
                TxtFirstName.Enabled = false;
                txtLastname1.Enabled = false;
                txtLastname2.Enabled = false;
                TxtInitial.Enabled = false;
                TxtAddrs1.Enabled = false;
                TxtAddrs2.Enabled = false;
                TxtState.Enabled = false;

                TxtSocialSec.Enabled = false;

                txtPhyAddress.Enabled = false;
                txtPhyAddress2.Enabled = false;
                txtPhyState.Enabled = false;


                TxtInitial.Visible = true;
                TxtAddrs1.Visible = true;
                TxtAddrs2.Visible = true;
                TxtState.Visible = true;
                TxtTerm.Visible = true;
                txtPhyAddress.Visible = true;
                txtPhyAddress2.Visible = true;
                txtPhyState.Visible = true;

                TxtPolicyNo.Visible = true;
                TxtPolicyType.Visible = true;
                TxtSufijo.Visible = false;
                TxtCity.Enabled = false;
                TxtInitial.Visible = true;
                TxtCity.Visible = false;
                TxtExplain.Enabled = false;

                TxtBirthdate.Visible = true;
                TxtOccupa.Visible = false;
                TxtLicense.Visible = false;
                TxtBirthdate.Enabled = false;
                TxtOccupa.Enabled = false;
                TxtLicense.Enabled = false;
                lblPolicyNo.Visible = true;
                lblPolicyType.Visible = true;
                lblSuffix.Visible = false;
                lblSelectedAgent.Visible = true;
                lblBirthdate.Visible = true;
                lblOccupa.Visible = false;
                lblLicense.Visible = false;
                //lblDefpayment.Visible = true;
                lblCoverage.Visible = false;
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
                imgCalendarEff.Visible = false;

                ddlZip.Enabled = false;
                ddlPhyZipCode.Enabled = false;
                ddlZip.Visible = true;
                ddlPhyZipCode.Visible = true;
                ddlPhyCity.Enabled = false;
                ddlCiudad.Enabled = false;
                ddlCiudad.Visible = true;
                ddlDeducible.Enabled = false;
                ddlPhyCity.Visible = true;
                ddlOriginatedAt.Enabled = false;
                ddlOriginatedAt.Visible = false;
                ddlInsuranceCompany.Enabled = false;
                Label65.Visible = false;
                ddlAgency.Enabled = false;
                ddlAgent.Enabled = false;

                ddlBank.Enabled = false;
                ddlCompanyDealer.Enabled = false;

                // Disable auto control

                txtVehicleVIN.Enabled = false;
                txtVehiclePlate.Enabled = false;
                ddlVehicleMake.Enabled = false;
                ddlVehicleModel.Enabled = false;
                ddlVehicleYear.Enabled = false;
                ddlNewUsed.Enabled = false;

                // Option Print

                lblPrintOption.Visible = false;
                chkInsured.Visible = false;
                chkProducer.Visible = false;
                chkCompany.Visible = false;
                chkAgency.Visible = false;
                chkExtraCopy.Visible = false;

                
                int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);


                if (IsSTHOMAS())
                {
                    Label62.Visible = false;
                    ddlAgency.Visible = false;
                    lblCity.Visible = false;
                    ddlCiudad.Visible = false;
                    TxtState.Visible = false;
                    lblCity4.Visible = false;
                    ddlPhyCity.Visible = false;
                    txtPhyState.Visible = false;
                    ddlState.Visible = true;
                    ddlState2.Visible = true;
                }
                else
                {
                    Label62.Visible = true;
                    ddlAgency.Visible = true;
                    ddlState.Visible = false;
                    ddlState2.Visible = false;
                }
           
                VerifyAccess();
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

        private void VerifyAccess()
        {
            try
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

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

        private DataTable GetXtraAutoByTaskControlID(int TaskControlID)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not retrieve Vehicle Data.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetAutoRoadAssistByTaskControlID", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //ExceptionHandler(ex);
                    throw new Exception(ex.Message);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {

                RemoveSessionLookUp();
                this.litPopUp.Visible = false;
                Session.Clear();
                Response.Redirect("Search.aspx");
            
        }

        private void PrintAfterPay()
        {
            try
            {

                //TaskControl.Policies policy = (TaskControl.Policies)Session["TaskControl"];
                //EPolicy.TaskControl.Laboratory taskControl = (EPolicy.TaskControl.Laboratory)Session["TaskControl"];
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

                Login.Login cp = HttpContext.Current.User as Login.Login;
                string userName = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();



                List<string> mergePaths = new List<string>();
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
                //EPolicy.TaskControl.Laboratory taskControl = (EPolicy.TaskControl.Laboratory)Session["TaskControl"];
                string PathReport = MapPath("Reports/RoadAssist/");
                string PathReportNew = MapPath("Reports/");
                string FileNamePdf = "";
                string PathReportAspen = MapPath("Reports/RoadAssist/");



                GetRoadAssistFileTableAdapters.GetRoadAssistFileTableAdapter ds = new GetRoadAssistFileTableAdapters.GetRoadAssistFileTableAdapter();
                ReportDataSource rpd = new ReportDataSource("GetRoadAssistFile", (DataTable)ds.GetData(taskControl.TaskControlID));


                if (IsSTHOMAS())
                {
                    // english RoadAssistVI
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.DataSources.Clear();
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = PathReport + "RoadAssistVI.rdlc";
                    viewer.LocalReport.DataSources.Add(rpd);
                    viewer.LocalReport.Refresh();
                    mergePaths = WriteRdlcToPDF(viewer, taskControl, mergePaths, 0);
                }
                else
                {

                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.DataSources.Clear();
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = PathReport + "RoadAssist.rdlc";
                    viewer.LocalReport.DataSources.Add(rpd);
                    viewer.LocalReport.Refresh();
                    mergePaths = WriteRdlcToPDF(viewer, taskControl, mergePaths, 0);
                }



                DataTable dt = taskControl.RoadAssistCollection;

                if (taskControl.RoadAssistCollection.Rows.Count == 2)
                {
                    if (IsSTHOMAS())
                    {
                        FileNamePdf = "TCGuardianUSVI.pdf";
                    }
                    else
                    {
                        FileNamePdf = "TERMINOS Y CONDICIONES CUBIERTA EXTENDIDA.pdf";
                    }
                    DeleteFile(ProcessedPath + FileNamePdf);
                    FileInfo file = new FileInfo(PathReport + FileNamePdf);
                    file.CopyTo(ProcessedPath + FileNamePdf);
                    mergePaths.Add(ProcessedPath + FileNamePdf);
                }
                else
                {
                    if (IsSTHOMAS())
                    {
                        FileNamePdf = "TCGuardianUSVI.pdf";
                    }
                    else
                    {
                        FileNamePdf = "TERMINOS Y CONDICIONES CUBIERTA REGULAR.pdf";
                    }
                    DeleteFile(ProcessedPath + FileNamePdf);
                    FileInfo file = new FileInfo(PathReport + FileNamePdf);
                    file.CopyTo(ProcessedPath + FileNamePdf);
                    mergePaths.Add(ProcessedPath + FileNamePdf);

                }
                if (File.Exists(ProcessedPath + taskControl.TaskControlID.ToString() + ".pdf"))
                    File.Delete(ProcessedPath + taskControl.TaskControlID.ToString() + ".pdf");

                //Generar PDF
                OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                string FinalFile = "";
                string Nombre = taskControl.Customer.FirstName.ToString() + taskControl.Customer.LastName1.ToString() + taskControl.Customer.LastName2.ToString();
                FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, Nombre.Trim()); //taskControl.TaskControlID.ToString());
                TextBox1.Text = FinalFile;
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                taskControl.PrintPolicy = true;


            }
            catch (Exception exc)
            {
                lblRecHeader.Text = exc.Message.Trim() + " - " + exc.InnerException.ToString();
                mpeSeleccion.Show();
            }
        }
        protected void BtnCashPayment_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

                string transactionNumber = GetTransactionNumber();
                AddPayment(transactionNumber, "CashPayment", "Success","CashPayment", "", "");

                FillTextControl();
                DisableControls();
                PrintAfterPay();
                string CustomerName = taskControl.Customer.FirstName.ToString();
                string PaymentMethod = "cash transaction";
                string PaymentType = "cash";

                SendEmail(CustomerName, taskControl.Customer.Email.ToString(), PaymentMethod, PaymentType, taskControl.TotalPremium.ToString(), "", DateTime.Now.ToLongDateString(), taskControl.PolicyType.Trim() + taskControl.PolicyNo, DateTime.Today.AddDays(1).ToLongDateString());
                   


                lblRecHeader.Text = "Transaction saved successfully.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
            catch (Exception exp)
            {
                LogError(exp);
            }
        }


        private void SendEmail(string CustomerName, string Email, string PaymentMethod, string PaymentType, string PaymentAmount, string AccNumber, string EntryDate, string PolicyNumber, string DebitDate)
        {
            try
            {

                //EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
                //Email (El email que ve el que recibe)
                string emailNoreplay = "policyconfirmation@midoceanpr.com"; //"lsemailservice@gmail.com"; //"policyconfirmation@midoceanpr.com";//"lsemailservice@gmail.com";
                string EmailNoReplayPass = "Conf1rm@tion";
                //Email (That send the message)
                string emailSend = "lsemailservice@gmail.com";
                string msg = "";
                string pdf = TextBox1.Text;
                MailMessage SM = new MailMessage();

                SM.Subject = "Guardian Insurance - Your payment has been received";
                SM.From = new System.Net.Mail.MailAddress(emailNoreplay);
                SM.Body = "<p>Dear " + CustomerName + " (" + Email + ")</p><p>This email is to inform you that Guardian Insurance has processed a single " + PaymentMethod + " for the amount of $" + PaymentAmount + " on " + EntryDate + ". The description Guardian Insurance has provided for this transaction is as follows: " + PolicyNumber + ".</p><p>If this transaction is an error or is a fraudulent transaction, please contact Guardian Insurance at (340) 776-8050 if you have any questions or concerns. Thank you for your payment.</p>";
                SM.IsBodyHtml = true;
                SM.Attachments.Add(new Attachment(ProcessedPath + pdf));//@"F:\inetpub\wwwroot\EpmsTest\ExportFiles\" + pdf));


                //AlternateView av = AlternateView.CreateAlternateViewFromString(SM.Body, Encoding.UTF8, "text/html");
                //av.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                //LinkedResource logo2 = new LinkedResource(ConfigurationManager.AppSettings["ImagePath"].ToString().Trim() + "blank.png", MediaTypeNames.Image.Jpeg);
                //logo2.ContentId = "BtnPagarEmail";
                //av.LinkedResources.Add(logo2);

                SM.To.Add(Email);

                //SM.Bcc.Add("arosado@lanzasoftware.com");
                SM.Bcc.Add("econcepcion@guardianinsurance.com");
                //SM.Bcc.Add("smartinez@guardianinsurance.com");
                SM.Bcc.Add("rcruz@guardianinsurance.com");
                //SM.Bcc.Add("susana.martinez11@gmail.com");
                //SM.Bcc.Add("jnieves@lanzasoftware.com");
                //SM.Bcc.Add("ydejesus@lanzasoftware.com");


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

        private void AddPayment(string transactionNumber, string PaymentConfirmationNumber, string Result, string ConsoleResult, string RequestInfo, string RequestResponse)
        {
            EncryptClass.EncryptClass encryp = new EncryptClass.EncryptClass();

            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];


            string VendorID = "0";

            //string x = txtAccountNumber.Text.Trim().Substring(txtAccountNumber.Text.Trim().Length - 4);

            //string AccNumber = encryp.Encrypt(x);

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[12];
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControl.TaskControlID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentMethodID", SqlDbType.Int, 0, "0", ref cookItems);//ddlMetodoPago.SelectedItem.Value.Trim()
            DbRequestXmlCooker.AttachCookItem("PaymentDate", SqlDbType.DateTime, 0, DateTime.Now.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentAmount", SqlDbType.Money, 0, txtPremium.Text.Replace("$", "").ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("AccountNumberLast4", SqlDbType.VarChar, 100, "CashPayment", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentConfirmationNumber", SqlDbType.VarChar, 50, PaymentConfirmationNumber, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("TransactionNumberID", SqlDbType.Int, 0, transactionNumber, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("VendedorID", SqlDbType.Int, 0, VendorID, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Result", SqlDbType.VarChar, 50, Result, ref cookItems);
	        DbRequestXmlCooker.AttachCookItem("ConsoleResult", SqlDbType.VarChar, 5000, ConsoleResult, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("RequestInfo", SqlDbType.VarChar, 8000, RequestInfo, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("RequestResponse", SqlDbType.VarChar, 8000, RequestResponse, ref cookItems);
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
            exec.Insert("AddPayment", xmlDoc);//PaymentID =
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
                if (IsSTHOMAS())
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

                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

                //taskControl.SaveGuardianXtra(userID);  //(userID);
                taskControl.SaveRoadAssistance(userID);

               // UpdateGuadianXtraHasAccident12(taskControl.TaskControlID, HasAccident12);

                taskControl = (EPolicy.TaskControl.RoadAssistance)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);

                Session["TaskControl"] = taskControl;

                //if (taskControl.Mode == 4 && taskControl.Trams_FL == false)
                //{
                //    PolicyXML(taskControl.TaskControlID);

                //    taskControl.Trams_FL = true;
                //}

                FillTextControl();
                DisableControls();

                lblRecHeader.Text = "Policy information saved successfully.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
            catch (Exception exp)
            {
                LogError(exp);
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

            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

            try
            {
                //Effective date
                if (this.TxtFirstName.Text == "")
                {
                    errorMessages.Add("First Name is missing." + "\r\n");
                }
                if (this.txtLastname1.Text == "")
                {
                    errorMessages.Add("Last Name is missing." + "\r\n");
                }

                if ((this.TxtCellular.Text == "" || this.TxtCellular.Text == "(") && (this.txtWorkPhone.Text == "" || this.txtWorkPhone.Text == "(") && (this.TxtHomePhone.Text == "" || this.TxtHomePhone.Text == "("))
                {
                    errorMessages.Add("Cellular Phone, Work Phone or Home Phone is missing." + "\r\n");
                }

                if (this.TxtCellular.Text == "(")
                {
                    TxtCellular.Text = "";
                }

                if (this.txtWorkPhone.Text == "(")
                {
                    txtWorkPhone.Text = "";
                }

                if (this.TxtHomePhone.Text == "(")
                {
                    TxtHomePhone.Text = "";
                }

                if (this.TxtTerm.Text == "")
                {
                    errorMessages.Add("Term is missing." + "\r\n");
                }
                else if (this.TxtTerm.Text == "0")
                {
                    errorMessages.Add("Term must be greater than zero." + "\r\n");
                }

                //if (ddlAgency.SelectedIndex <= 0 || ddlAgency.SelectedItem == null || ddlAgency.SelectedItem.Value.Trim() == "000")
                //{
                //    errorMessages.Add("The Agency is missing or wrong." + "\r\n");
                //}
                if (IsSTHOMAS())
                {
                    if (ddlState.SelectedIndex <= 0 || ddlState.SelectedItem == null)
                    {
                        errorMessages.Add("The State is missing or wrong." + "\r\n");
                    }
                }
                else
                {
                    if (ddlCiudad.SelectedIndex <= 0 || ddlCiudad.SelectedItem == null)
                    {
                        errorMessages.Add("The City is missing or wrong." + "\r\n");
                    }
                }

                if (ddlZip.SelectedIndex <= 0 || ddlPhyCity.SelectedItem == null)
                {
                    errorMessages.Add("The Zipcode is missing or wrong." + "\r\n");
                }

                if (IsSTHOMAS())
                {
                    if (ddlState2.SelectedIndex <= 0 || ddlState2.SelectedItem == null)
                    {
                        errorMessages.Add("The State is missing or wrong." + "\r\n");
                    }
                }
                else
                {
                    if (ddlPhyCity.SelectedIndex <= 0 || ddlPhyCity.SelectedItem == null)
                    {
                        errorMessages.Add("The Physical City is missing or wrong." + "\r\n");
                    }
                }

                if (ddlPhyZipCode.SelectedIndex <= 0 || ddlPhyCity.SelectedItem == null)
                {
                    errorMessages.Add("The Physical Zipcode is missing or wrong." + "\r\n");
                }

                if (this.txtTotalPremium.Text == "")
                {
                    errorMessages.Add("TotalPremium is missing or the vehicle is missing." + "\r\n");
                }

                //if (this.TxtBirthdate.Text == "")
                //{
                //    errorMessages.Add("Date of birth is missing." + "\r\n");
                //}

                if (this.TxtState.Text == "" && ddlState.SelectedIndex < 1)
                {
                    errorMessages.Add("State is missing." + "\r\n");
                }

                if (this.TxtAddrs1.Text == "")
                {
                    errorMessages.Add("Address is missing." + "\r\n");
                }

                if (this.txtPhyState.Text == "" && ddlState2.SelectedIndex <1)
                {
                    errorMessages.Add("State is missing." + "\r\n");
                }

                if (this.txtPhyAddress.Text == "")
                {
                    errorMessages.Add("Physical Address is missing." + "\r\n");
                }

                //if (ddlDeducible.SelectedIndex <= 0 || ddlDeducible.SelectedItem == null)
                //{
                //    errorMessages.Add("Deducible is missing or wrong." + "\r\n");
                //}

                //if (this.TxtLicense.Text == "")
                //{
                //    errorMessages.Add("License Number is missing." + "\r\n");
                //}

                //if (ddlAgent.SelectedIndex <= 0 || ddlAgent.SelectedItem == null)
                //{
                //    errorMessages.Add("The Agent is missing or wrong." + "\r\n");
                //}

                //if (this.txtVehicleVIN.Text == "")
                //{
                //    errorMessages.Add("Vehicle VIN Number is missing." + "\r\n");
                //}

                //if (this.txtVehiclePlate.Text == "")
                //{
                //    errorMessages.Add("Vehicle Plate is missing." + "\r\n");
                //}

                //if (ddlVehicleMake.SelectedIndex <= 0 || ddlVehicleMake.SelectedItem == null)
                //{
                //    errorMessages.Add("Vehicle Make is missing or wrong." + "\r\n");
                //}

                //if (ddlVehicleModel.SelectedIndex <= 0 || ddlVehicleModel.SelectedItem == null)
                //{
                //    errorMessages.Add("Vehicle Model is missing or wrong." + "\r\n");
                //}

                //if (ddlVehicleYear.SelectedIndex <= 0 || ddlVehicleYear.SelectedItem == null)
                //{
                //    errorMessages.Add("Vehicle Year is missing or wrong." + "\r\n");
                //}

                if (this.txtEffDt.Text == "")
                {
                    errorMessages.Add("Effective date is missing." + "\r\n");
                }

                if (taskControl.Mode == 1 || taskControl.Mode == 2 && DateTime.Parse(txtEffDt.Text.ToString()) < DateTime.Parse(taskControl.EffectiveDate))
                {
                    if (!cp.IsInRole("ROAD ASSISTANCE RETRO EFFDATE") && !cp.IsInRole("ADMINISTRATOR"))
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

                        if (DateTime.Parse(txtExpDt.Text.ToString()) < DateTime.Parse(DateTime.Now.AddMonths(12).ToShortDateString()))
                        {
                            errorMessages.Add("Expiration Date is wrong please select Effective Date again." + "\r\n");
                        }
                    }
                  
                }

                //if (ddlVehicleModel.SelectedIndex <= 0 || ddlVehicleModel.SelectedItem == null)
                //{
                //    errorMessages.Add("Vehicle Model is missing or wrong." + "\r\n");
                //}

                if (rdbCoverageYes.Checked == true && TxtExplain.Text == "")
                {
                    errorMessages.Add("Please explain additional coverage." + "\r\n");
                }

                //if (rdbDefpaymentYes.Checked == true && chkDefpayfour.Checked == false && chkDefpaysix.Checked == false)
                //{
                //    errorMessages.Add("Please select a deffered payment plan." + "\r\n");
                //}

                //Can no longer be changed
                //if (ddlOriginatedAt.SelectedIndex <= 0 || ddlOriginatedAt.SelectedItem == null)
                //{
                //    errorMessages.Add("The Origin is missing or wrong." + "\r\n");
                //}


                ///if (taskControl.RoadAssistCollection.Rows.Count == 0)
                /// errorMessages.Add("Please add the vehicle information." + "\r\n");

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
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
            Login.Login cp2 = HttpContext.Current.User as Login.Login;

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

            //Make
            if (ddlVehicleMake.SelectedIndex > 0 && ddlVehicleMake.SelectedItem != null)
            {
                taskControl.VehicleMakeID = int.Parse(ddlVehicleMake.SelectedItem.Value.ToString());
               // taskControl.XtraVehicleMakeID = int.Parse(ddlVehicleMake.SelectedItem.Value.ToString());
            }
            //Model
            if (ddlVehicleModel.SelectedIndex > 0 && ddlVehicleModel.SelectedItem != null)
            {
                taskControl.VehicleModelID = int.Parse(ddlVehicleModel.SelectedItem.Value.ToString());
                //taskControl.XtraVehicleModelID = int.Parse(ddlVehicleModel.SelectedItem.Value.ToString());
            }
            //year
            if (ddlVehicleYear.SelectedIndex > 0 && ddlVehicleYear.SelectedItem != null)
            {
                taskControl.VehicleYearID = int.Parse(ddlVehicleYear.SelectedItem.Value.ToString());
                //taskControl.XtraVehicleYearID = int.Parse(ddlVehicleYear.SelectedItem.Value.ToString());
            }
            //NewUse
            if (ddlNewUsed.SelectedIndex > 0 && ddlNewUsed.SelectedItem != null)
            {
                taskControl.NewUsed = int.Parse(ddlNewUsed.SelectedItem.Value.ToString());
            }

            //Ciudad
            if (ddlCiudad.SelectedIndex > 0 && ddlCiudad.SelectedItem != null)
                taskControl.Customer.City = ddlCiudad.SelectedItem.Text.ToString().ToUpper();
            else
                taskControl.Customer.City = "";

            //Ciudad Fisica
            if (ddlPhyCity.SelectedIndex > 0 && ddlPhyCity.SelectedItem != null)
                taskControl.Customer.CityPhysical = ddlPhyCity.SelectedItem.Text.ToString().ToUpper();
            else
                taskControl.Customer.CityPhysical = "";

            //ZipCode
            if (ddlZip.SelectedIndex > 0 && ddlZip.SelectedItem != null)
                taskControl.Customer.ZipCode = ddlZip.SelectedItem.Text;
            else
                taskControl.Customer.ZipCode = "";

            //ZipCode Fisico
            if (ddlPhyZipCode.SelectedIndex > 0 && ddlPhyZipCode.SelectedItem != null)
                taskControl.Customer.ZipPhysical = ddlPhyZipCode.SelectedItem.Text;
            else
                taskControl.Customer.ZipPhysical = "";


            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
            

            if ((cp2.IsInRole("MODIFY SOCIAL SECURITY") || taskControl.TaskControlID == 0))
            {
                if (TxtSocialSec.Text.Trim().Replace("*", "").Replace("-", "").Replace("_", "") != "")
                {
                    string encryptString = TxtSocialSec.Text.Replace("*", "").Replace("-", "").Replace("_", "").Trim().ToUpper();
                    taskControl.Customer.SocialSecurity = encrypt.Encrypt(encryptString.ToUpper());
                }
                else
                    taskControl.Customer.SocialSecurity = "";
            }


            if (taskControl.PolicyClassID == 15) // OSO
            {
                if (taskControl.Mode == 2) // EDIT
                {
                    if ((taskControl.Customer.FirstName.Trim() != TxtFirstName.Text.Trim()) || (taskControl.Customer.LastName1.Trim() != txtLastname1.Text.Trim()) || (taskControl.Customer.LastName2.Trim() != txtLastname2.Text.Trim()))
                    {
                        int endNum = taskControl.Endoso + 1;
                        taskControl.Endoso = endNum;
                    }
                }
            }

            taskControl.TaskControlID = int.Parse(LblControlNo.Text.Trim());

            //Customer Information
            taskControl.Customer.FirstName = TxtFirstName.Text.ToUpper().Trim();
            taskControl.Customer.Initial = TxtInitial.Text.ToUpper().Trim();
            taskControl.Customer.LastName1 = txtLastname1.Text.ToUpper().Trim();
            taskControl.Customer.LastName2 = txtLastname2.Text.ToUpper().Trim();
            taskControl.Customer.Address1 = TxtAddrs1.Text.ToUpper().Trim();
            taskControl.Customer.Address2 = TxtAddrs2.Text.ToUpper().Trim();
            taskControl.Customer.Birthday = TxtBirthdate.Text.ToUpper().Trim();
            taskControl.Customer.Occupation = TxtOccupa.Text.ToUpper().Trim();
            taskControl.Customer.Licence = TxtLicense.Text.ToUpper().Trim();
            taskControl.Customer.Email = txtEmail.Text.ToUpper().Trim();

            if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp2.UserID).ToString()).Contains("THOMAS"))
            {
                if (ddlState.SelectedIndex > 0 && ddlState.SelectedItem != null)
                    taskControl.Customer.State = ddlState.SelectedItem.Text.ToString().ToUpper();
                else
                    taskControl.Customer.State = "";
                if (ddlState2.SelectedIndex > 0 && ddlState2.SelectedItem != null)
                    taskControl.Customer.StatePhysical = ddlState2.SelectedItem.Text.ToString().ToUpper();
                else
                    taskControl.Customer.StatePhysical = "";


            }
            else
            {
                taskControl.Customer.State = TxtState.Text.ToUpper().Trim();
                taskControl.Customer.StatePhysical = txtPhyState.Text.ToString().ToUpper().Trim();
            }

            taskControl.Customer.HomePhone = TxtHomePhone.Text.Trim();
            taskControl.Customer.JobPhone = txtWorkPhone.Text.Trim();
            taskControl.Customer.Cellular = TxtCellular.Text.Trim();

            taskControl.Customer.AddressPhysical1 = txtPhyAddress.Text.ToString().ToUpper().Trim();
            taskControl.Customer.AddressPhysical2 = txtPhyAddress2.Text.ToString().ToUpper().Trim();
            


            if (taskControl.PolicyClassID == 1 || taskControl.PolicyClassID == 16) // VSC, QCertified
            {
                taskControl.PolicyNo = TxtPolicyNo.Text.Trim().ToUpper().Replace(" ", "");
                taskControl.PolicyNo = taskControl.PolicyNo.Trim().ToUpper().Replace("-", "");
            }
            else
            {
                taskControl.PolicyNo = TxtPolicyNo.Text.Trim().ToUpper().Replace(" ", "");
            }
            taskControl.PolicyType = TxtPolicyType.Text.Trim().ToUpper();
            taskControl.Suffix = TxtSufijo.Text.Trim();
            taskControl.Term = int.Parse(TxtTerm.Text.Trim());
            taskControl.EffectiveDate = txtEffDt.Text.Trim();

            //if (taskControl.ExpirationDate.Trim() == string.Empty) // && this.TxtTerm.Text.Trim() != string.Empty)
            if (txtExpDt.Text.Trim() == string.Empty)
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
            

            //Si es menos de un año se puede hacer ovverride premium.
             int totaldays = int.Parse(Math.Round((DateTime.Parse(taskControl.EffectiveDate.Trim()) - DateTime.Parse(taskControl.ExpirationDate)).TotalDays, 0).ToString());
             if ((DateTime.Parse(taskControl.ExpirationDate) - DateTime.Parse(taskControl.EffectiveDate.Trim())).TotalDays == 365)
             {
                 if (GridVehicle.Rows.Count == 1)
                 {
                     txtPremium.Text = "40.0";
                     txtTotalPremium.Text = "40.0";
                     taskControl.TotalPremium = 40.0;


                 }
                 if (GridVehicle.Rows.Count == 2)
                 {

                     txtPremium.Text = "60.0";

                     txtTotalPremium.Text = "60.0";

                     taskControl.TotalPremium = 60.0;


                 }
                 if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp2.UserID).ToString()).Contains("THOMAS"))
                 {

                     txtPremium.Text = "70.0";

                     txtTotalPremium.Text = "70.0";
                     taskControl.TotalPremium = 70.0;

                 }
             }
            else
            {
                taskControl.TotalPremium = double.Parse(txtPremium.Text.Trim().Replace("$","")); 
            }

            //if (GridVehicle.Rows.Count == 1)
            //{
            //    taskControl.TotalPremium = 40.0;
            //}
            //else
            //{
            //    taskControl.TotalPremium = 60.0;
            //}

            //if (txtTotalPremium.Text.Trim() == "")
            //    taskControl.TotalPremium = 0.00;
            //else
            //    taskControl.TotalPremium = double.Parse(txtTotalPremium.Text.ToString().Trim());

           // taskControl.TotalPremium = double.Parse(ddlDeducible.SelectedItem.Text.Replace("$", "").Trim());



            taskControl.VIN = txtVehicleVIN.Text.ToUpper().Trim();
            //taskControl.XtraVIN = txtVehicleVIN.Text.ToUpper().Trim();
            //taskControl.XtraHasCoverageExplain = TxtExplain.Text.ToUpper().Trim();

            taskControl.Customer.SamesAsMail = chkSameMailing.Checked;

           // taskControl.XtraHasCoverage = rdbCoverageYes.Checked;
            //taskControl.XtraDefferedPayment = rdbDefpaymentYes.Checked;
            //taskControl.XtraIsFourPayment = chkDefpayfour.Checked;
           // taskControl.XtraIsSixPayment = chkDefpaysix.Checked;
            taskControl.IsCreditPayment = chkCredit.Checked;
            //taskControl.XtraIsCommercialAuto = chkIsCommercialAuto.Checked;
            //taskControl.XtraIsPersonalAuto = chkIsPersonalAuto.Checked;
            taskControl.IsDebitPayment = chkDebit.Checked;
            taskControl.IsCashPayment = chkCash.Checked;

            taskControl.Plate = txtVehiclePlate.Text.ToUpper().Trim();
            //taskControl.XtraPlate = txtVehiclePlate.Text.ToUpper().Trim();

            #region XtraAuto

            taskControl.VIN = txtVehicleVIN.Text.ToUpper().Trim();

            if (ddlVehicleMake.SelectedItem.Text != "")
            {
                taskControl.VehicleMakeID = int.Parse(ddlVehicleMake.SelectedItem.Value);
            }
            else
            {
                taskControl.VehicleMakeID = 0; 
            }

            //taskControl.XtraVehicleMake = ddlVehicleMake.SelectedItem.Text.Trim();
           // taskControl.VehicleModelID = ddlVehicleModel.SelectedItem.Value == "" ? 0 : int.Parse(ddlVehicleMake.SelectedItem.Value);
            //taskControl.XtraVehicleModel = ddlVehicleModel.SelectedItem.Text.Trim();
            //taskControl.VehicleYearID = ddlVehicleYear.SelectedItem.Value == "" ? 0 : int.Parse(ddlVehicleYear.SelectedItem.Value);
            //taskControl.XtraVehicleYear = ddlVehicleYear.SelectedItem.Text.Trim();
            //taskControl.XtraPlate = txtVehiclePlate.Text.Trim();
            //taskControl.XtraPremium = double.Parse(ddlDeducible.Text.Replace("$", "").Trim());

            #endregion

            if (ChkAutoAssignPolicy.Checked)
                taskControl.AutoAssignPolicy = true;
            else
                taskControl.AutoAssignPolicy = false;

            if (taskControl.Mode == 1)
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0];
            }

            HasAccident12 = chkHasAccident12.Checked;

            Session["TaskControl"] = taskControl;
        }

        private void EnableControls()
        {
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

             
            //Botones  
            btnEdit.Visible = false;
            BtnExit.Visible = false;
            BtnSave.Visible = true;
            btnCancel.Visible = true;
            // btnRenew.Visible = false;
            btnCancellation.Visible = false;      
            btnGuardianPay.Visible = false;
            btnCashPayment.Visible = false;

            // TextBox
            TxtProspectNo.Enabled = false;
            TxtFirstName.Enabled = true;
            txtLastname1.Enabled = true;
            txtLastname2.Enabled = true;

            //rdbDefpaymentYes.Visible = true;
            //rdbDefpaymentYes.Enabled = true;
            //rdbDefpaymentNo.Visible = true;
            //rdbDefpaymentNo.Enabled = true;
            TxtExplain.Enabled = true;
            //lblDefpayment.Visible = true;
            chkDefpaysix.Enabled = true;
            chkDefpayfour.Enabled = true;
            chkIsCommercialAuto.Visible = false;
            chkIsCommercialAuto.Enabled = true;
            chkCredit.Visible = true;
            chkCredit.Enabled = true;
            chkDebit.Visible = false;
            chkDebit.Enabled = true;
            chkSameMailing.Visible = true;
            chkSameMailing.Enabled = true;
            chkCash.Visible = true;
            chkCash.Enabled = true;

            chkHasAccident12.Visible = false;
            chkHasAccident12.Enabled = true;

            rdbCoverageNo.Enabled = true;
            rdbCoverageYes.Enabled = true;
            rdbCoverageNo.Visible = false;
            rdbCoverageYes.Visible = false;
            lblCoverage.Visible = false;
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

            TxtBirthdate.Enabled = true;
            TxtOccupa.Enabled = true;
            TxtLicense.Enabled = true;
            TxtBirthdate.Visible = true;
            TxtOccupa.Visible = false;
            TxtLicense.Visible = false;
            lblBirthdate.Visible = true;
            lblOccupa.Visible = false;
            lblLicense.Visible = false;
            ddlOriginatedAt.Visible = false;
            ddlOriginatedAt.Enabled = false;
            Label65.Visible = false;

            txtPhyAddress.Enabled = true;
            txtPhyAddress2.Enabled = true;
            txtPhyState.Enabled = true;

            ddlPhyZipCode.Enabled = true;
            ddlZip.Enabled = true;
            //ddlPhyCity.Enabled = true;

            txtPhyAddress.Visible = true;
            txtPhyAddress2.Visible = true;
            txtPhyState.Visible = true;

            //TxtSocialSec.Enabled = true;

            ddlPhyZipCode.Visible = true;
            ddlZip.Visible = true;
            ddlPhyCity.Visible = true;

            TxtPolicyNo.Visible = true;
            TxtPolicyType.Visible = true;
            TxtSufijo.Visible = false;
            TxtCity.Enabled = false;
            TxtCity.Visible = false;
            lblPolicyNo.Visible = true;
            lblPolicyType.Visible = true;
            lblSuffix.Visible = false;
            lblSelectedAgent.Visible = true;
            chkIsPersonalAuto.Enabled = true;
            chkIsPersonalAuto.Visible = false;

            //BOTONES PRINT
            btnPrintPolicy.Visible = false; //false alexis
            ddlPrintOption.Visible = false;

            
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
            ddlAgent.Enabled = false;

            TxtPolicyType.Enabled = false;
            TxtSufijo.Enabled = false;
            TxtTerm.Enabled = false;
            txtEffDt.Enabled = true;
            txtExpDt.Enabled = true;
            txtEntryDate.Enabled = false;
            imgCalendarEff.Visible = true;

            //Enable Auto Control

            txtVehicleVIN.Enabled = true;
            txtVehiclePlate.Enabled = true;
            ddlVehicleMake.Enabled = true;
            ddlVehicleModel.Enabled = true;
            ddlVehicleYear.Enabled = true;
            ddlNewUsed.Enabled = true;
            ddlBank.Enabled = true;
            ddlCompanyDealer.Enabled = true;

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);


            if (IsSTHOMAS())
            {
                Label62.Visible = false;
                ddlAgency.Visible = false;
                lblCity.Visible = false;
                ddlCiudad.Visible = false;
                TxtState.Visible = false;
                lblCity4.Visible = false;
                ddlPhyCity.Visible = false;
                txtPhyState.Visible = false;
                ddlState.Visible = true;
                ddlState2.Visible = true;
            }
            else
            {
                Label62.Visible = true;
                ddlAgency.Visible = true;
                ddlState.Visible = false;
                ddlState2.Visible = false;
            }
           
         
            ddlOriginatedAt.Enabled = false;
            ddlInsuranceCompany.Enabled = false;
            ddlAgency.Enabled = false;
            ddlAgent.Enabled = false;

            // Print Option

            lblPrintOption.Visible = false;
            chkInsured.Visible = false;
            chkProducer.Visible = false;
            chkCompany.Visible = false;
            chkAgency.Visible = false;
            chkExtraCopy.Visible = false;
            ddlDeducible.Enabled = true;

            txtTotalPremium.Enabled = true;

            //Si esta pagada la poliza no dispone el policyType, la agencia, agentes y supplier y el totalPremium + Charge.
            //Se cambio dllAgent.Enabled a true por Joshua
            if ((double)taskControl.PaymentsDetail.TotalPaid > 0 && (double)taskControl.PaymentsDetail.TotalPaid >= taskControl.TotalPremium + taskControl.Charge)
            {
                ddlAgency.Enabled = false;
                ddlAgent.Enabled = false;

                ddlInsuranceCompany.Enabled = false;
                //txtTotalPremium.Enabled = false;
                //ddlPremium.Enabled = false;
            }

            if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("GUARIDANROADASSISTANCE"))
                ddlAgent.Enabled = true;
            else
                ddlAgent.Enabled = false;

            //if (cp.IsInRole("GUARIDANROADASSISTANCE"))
            //{
            //    ddlAgent.Enabled = false;
            
            //}

            chkCash_CheckedChanged(String.Empty, EventArgs.Empty);
        }

        ///Commented By Daniel San
        //protected void btnRenew_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        //        int userID = 0;
        //        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

        //        GuardianXtra taskControl = new GuardianXtra();
        //        GuardianXtra roadAssistance = (GuardianXtra)Session["TaskControl"];

        //        taskControl.Mode = 1;
        //        taskControl.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Road Assistance"));

        //        //Customer information

        //        taskControl.Customer.CustomerNo = roadAssistance.Customer.CustomerNo;
        //        taskControl.Customer.FirstName = roadAssistance.Customer.FirstName;
        //        taskControl.Customer.Initial = roadAssistance.Customer.Initial;
        //        taskControl.Customer.LastName1 = roadAssistance.Customer.LastName1;
        //        taskControl.Customer.LastName2 = roadAssistance.Customer.LastName2;
        //        taskControl.Customer.Address1 = roadAssistance.Customer.Address1;
        //        taskControl.Customer.Address2 = roadAssistance.Customer.Address2;
        //        taskControl.Customer.City = roadAssistance.Customer.City;
        //        taskControl.Customer.State = roadAssistance.Customer.State;
        //        taskControl.Customer.ZipCode = roadAssistance.Customer.ZipCode;
        //        taskControl.Customer.HomePhone = roadAssistance.Customer.HomePhone;
        //        taskControl.Customer.JobPhone = roadAssistance.Customer.JobPhone;
        //        taskControl.Customer.Cellular = roadAssistance.Customer.Cellular;
        //        taskControl.Customer.Email = roadAssistance.Customer.Email;

        //        //Policy Detail

        //        // CANCELLATION POLICY
        //        taskControl.CancellationDate = "";
        //        taskControl.CancellationEntryDate = "";
        //        taskControl.CancellationUnearnedPercent = 0.00;
        //        taskControl.CancellationMethod = 0;
        //        taskControl.CancellationReason = 0;
        //        taskControl.ReturnCharge = 0.00;
        //        taskControl.ReturnPremium = 0.00;
        //        taskControl.CancellationAmount = 0.00;

        //        taskControl.PolicyNo = roadAssistance.PolicyNo;
        //        taskControl.PolicyType = roadAssistance.PolicyType;
        //        taskControl.Suffix = roadAssistance.Suffix;

        //        int suffix = int.Parse(taskControl.Suffix.ToString().Trim());
        //        suffix += 1;
        //        taskControl.Suffix = suffix.ToString().PadLeft(2, '0');

        //        taskControl.Term = roadAssistance.Term;
        //        taskControl.EffectiveDate = (DateTime.Parse(roadAssistance.EffectiveDate).AddMonths(12)).ToShortDateString();
        //        taskControl.ExpirationDate = DateTime.Parse(roadAssistance.ExpirationDate).AddMonths(12).ToShortDateString();
        //        taskControl.EntryDate = DateTime.Now;
        //        taskControl.TotalPremium = roadAssistance.TotalPremium;
        //        taskControl.InsuranceCompany = roadAssistance.InsuranceCompany;
        //        taskControl.Agency = roadAssistance.Agency;
        //        taskControl.Agent = roadAssistance.Agent;
        //        taskControl.OriginatedAt = roadAssistance.OriginatedAt;
        //        taskControl.Bank = roadAssistance.Bank;
        //        taskControl.Dealer = roadAssistance.Dealer;
        //        taskControl.PolicyType = roadAssistance.PolicyType;
        //        taskControl.AutoAssignPolicy = false;

        //        //Auto Collection
        //        taskControl.RoadAssistCollection = roadAssistance.RoadAssistCollection;

        //        taskControl.TaskControlID = 0;

        //        Session.Clear();
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("GuardianXtra.aspx", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblRecHeader.Text = ex.Message;
        //        mpeSeleccion.Show();
        //    }
        //}

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            Session.Clear();
            EPolicy.TaskControl.RoadAssistance taskControl = new EPolicy.TaskControl.RoadAssistance();

            taskControl.Mode = 1; //ADD
            taskControl.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Road Assistance"));

            Session.Add("TaskControl", taskControl);
            Response.Redirect("GuardianRoadAssist.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            taskControl.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.UPDATE;

            Session.Add("TaskControl", taskControl);

            btnRenew.Visible = false;

            EnableControls();

            if (cp.IsInRole("MODIFY SOCIAL SECURITY"))
            {
                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();

                TxtSocialSec.Enabled = true;

                if (taskControl.Customer.SocialSecurity.Trim() != "")
                    TxtSocialSec.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                else
                    TxtSocialSec.Text = "";

                MaskedEditExtender1.Mask = "999-99-9999";
            }
            else if (taskControl.Customer.SocialSecurity.Trim().Replace("*", "").Replace("-", "").Replace("_", "") != "")
            {
                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
                TxtSocialSec.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                TxtSocialSec.Text = new string('*', TxtSocialSec.Text.Trim().Length - 4) + TxtSocialSec.Text.Trim().Substring(TxtSocialSec.Text.Trim().Length - 4);
                MaskedEditExtender1.Mask = "???-??-9999";
            }
        }

        ///Commented. Don't know if it's needed now
        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkInsured.Checked == false && chkProducer.Checked == false && chkAgency.Checked == false && chkCompany.Checked == false
        //            && chkExtraCopy.Checked == false)
        //        {
        //            throw new Exception("Please select one o more print option.");
        //        }
        //        else
        //        {
        //            ArrayList Copy = new ArrayList();
        //            bool IsEmployee = false;
        //            List<string> mergePaths = new List<string>();

        //            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
        //            int taskControlID = taskControl.TaskControlID;

        //            //Check Copy 
        //            if (chkInsured.Checked)
        //                Copy.Add("Insured Copy");
        //            if (chkProducer.Checked)
        //                Copy.Add("Producer Copy");
        //            if (chkAgency.Checked)
        //                Copy.Add("Agency Copy");
        //            if (chkCompany.Checked)
        //                Copy.Add("Company Copy");
        //            if (chkExtraCopy.Checked)
        //                Copy.Add("Extra Copy");

        //            DataTable dtIsEmployee = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);

        //            for (int i = 0; i < dtIsEmployee.Rows.Count; i++)
        //            {
        //                if (dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim() != "")
        //                {
        //                    IsEmployee = bool.Parse(dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim());
        //                }
        //            }


        //            for (int i = 0; i < Copy.Count; i++)
        //            {
        //                mergePaths = ImprimirRoadAssistance(mergePaths, taskControl, Copy[i].ToString().Trim());

        //                FileInfo mFileIndex;

        //                if (IsEmployee)
        //                {
        //                    //MIC1005-2
        //                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/POLITICA-CONDICIONES-GENERALES.pdf");
        //                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf", true);
        //                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf");

        //                }
        //                else
        //                {
        //                    //MIC1005-2
        //                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/MIC1005-2.pdf");
        //                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
        //                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf");
        //                }
        //            }

        //            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

        //            //Generar PDF
        //            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
        //            string FinalFile = "";
        //            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        lblRecHeader.Text = exc.Message;
        //        mpeSeleccion.Show();
        //    }
        //}

        private DataTable GetReportbAutoQuote4ByTaskControlID(int taskControlID)
        {
            DataTable dt = new DataTable();

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControlID.ToString(), ref cookItems);


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

            dt = exec.GetQuery("GetReportAutoPolicy4ByTaskControlID", xmlDoc);


            return dt;
        }
        public List<string> ImprimirRoadAssistance(List<string> mergePaths, EPolicy.TaskControl.RoadAssistance taskControl, string Copy)
        {
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

            int taskControlID = taskControl.TaskControlID;

            //ObjectDataSource ob = new ObjectDataSource("GetReportRoadAssistanceByTaskControlID.GetReportRoadAssistanceByTaskControlIDTableAdapter", "GetData");

            GetReportRoadAssistanceByTaskControlIDTableAdapters.GetReportRoadAssistanceByTaskControlIDTableAdapter ds1 =
                new GetReportRoadAssistanceByTaskControlIDTableAdapters.GetReportRoadAssistanceByTaskControlIDTableAdapter();

            ReportDataSource rds1 = new ReportDataSource();
            ReportParameter parameter1 = new ReportParameter("Copy", Copy);
            try
            {
                rds1 = new ReportDataSource("GetReportRoadAssistanceByTaskControlID", (DataTable)ds1.GetData(taskControlID));

            }
            catch (Exception ex)
            {

            }

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/MIC1005.rdlc");
            viewer1.LocalReport.DataSources.Add(rds1);
            viewer1.LocalReport.SetParameters(parameter1);

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + Copy;
            string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + Copy + ".pdf";

            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

            byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

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
                lblRecHeader.Text = ecp.Message;
                mpeSeleccion.Show();
            }

            return mergePaths;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

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
                taskControl = (EPolicy.TaskControl.RoadAssistance)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                taskControl.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                Session["TaskControl"] = taskControl;
                FillTextControl();
                DisableControls();

            }
        }

        private void DeleteRoadAssist(int PoliciesID)
        {
          
            ///////////////////////////////////////////////////////

            try
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

                DataTable dt = null;

                taskControl.RoadAssistCollection.Rows.RemoveAt(PoliciesID);
                taskControl.RoadAssistCollection.AcceptChanges();


                DataView dv = taskControl.RoadAssistCollection.DefaultView;
                taskControl.RoadAssistCollection = dv.ToTable();
                taskControl.RoadAssistCollection.AcceptChanges();


                dt = taskControl.RoadAssistCollection;
                for (int i = 0; i < GridVehicle.Columns.Count; i++)
                {
                    GridVehicle.Columns[i].Visible = true;
                }

                FillRoadAssistVehiclesGrid(dt);

                GridVehicle.Columns[6].Visible = false;
                GridVehicle.Columns[7].Visible = false;
                GridVehicle.Columns[8].Visible = false;
                GridVehicle.Columns[9].Visible = false;
                GridVehicle.Columns[10].Visible = false;

            }
            catch (Exception xcp)
            {
                throw new Exception("Error deleting row.", xcp);
            }

            /////////////////////////////////////////////////////
        }
        public void SelectModel()
        {
            try
            {
                Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
                DataTable dtVehicleModel = new DataTable();
                int VehicleMakeID = int.Parse(ddlVehicleMake.SelectedItem.Value.ToString().ToString());
                DbRequestXmlCookRequestItem[] cookItems =
                      new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
                    SqlDbType.Int, 0, VehicleMakeID.ToString(),
                    ref cookItems);

                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                    // dtVehicleModel = Executor.GetQuery("GetVehicleModelByVehicleModelID", xmlDoc);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                dtVehicleModel = Executor.GetQuery("GetVehicleModelByVehicleMakeID", xmlDoc);

                ddlVehicleModel.DataSource = dtVehicleModel;
                ddlVehicleModel.DataTextField = "VehicleModelDesc";
                ddlVehicleModel.DataValueField = "VehicleModelID";
                ddlVehicleModel.DataBind();
                ddlVehicleModel.SelectedIndex = -1;
                ddlVehicleModel.Items.Insert(0, "");
            }
            catch (Exception ex)
            {

            }
        }

        protected void ChkAutoAssignPolicy_CheckedChanged(object sender, EventArgs e)
        {
            VerifyAssignPolicyFields();
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

        protected void ddlVehicleMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectModel();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = GetZipcodeByState(ddlState.SelectedItem.Text);

            ddlZip.DataSource = dt;
            ddlZip.DataTextField = "Zipcode";
            ddlZip.DataValueField = "ZipCode";
            ddlZip.DataBind();
            ddlZip.SelectedIndex = -1;
            ddlZip.Items.Insert(0, "");

        }

        protected void ddlState2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = GetZipcodeByState(ddlState2.SelectedItem.Text);

            ddlPhyZipCode.DataSource = dt;
            ddlPhyZipCode.DataTextField = "Zipcode";
            ddlPhyZipCode.DataValueField = "ZipCode";
            ddlPhyZipCode.DataBind();
            ddlPhyZipCode.SelectedIndex = -1;
            ddlPhyZipCode.Items.Insert(0, "");
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

        public void ValidationCar()
        {
            ArrayList errorMessages = new ArrayList();
            try
            {
                //if (txtVehicleVIN.Text == "")
                //    errorMessages.Add("Please enter the Vehicle VIN Number" + "\r\n");

                if (ddlVehicleModel.SelectedIndex <= 0 || ddlVehicleModel.SelectedItem == null)
                    errorMessages.Add("Please enter Vehicle Make" + "\r\n");

                if (ddlVehicleMake.SelectedIndex <= 0 || ddlVehicleMake.SelectedItem == null)
                    errorMessages.Add("Please enter Vehicle Model" + "\r\n");

                if (ddlVehicleYear.SelectedIndex <= 0 || ddlVehicleYear.SelectedItem == null)
                    errorMessages.Add("Please enter Vehicle Year" + "\r\n");

                //if (ddlNewUsed.SelectedIndex <= 0 || ddlNewUsed.SelectedItem == null)
                //    errorMessages.Add("Please choose if Vehicle is New or Used" + "\r\n");

                if (txtVehiclePlate.Text == "")
                    errorMessages.Add("Please enter Vehicle Plate" + "\r\n");

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
            }
        }
        protected void btnCancellation_Click(object sender, EventArgs e)
        {

            RemoveSessionLookUp();
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
            Session.Add("TaskControl", taskControl);
            Session.Add("FromUI", "GuardianXtra.aspx");
            Session.Add("CancFromGuardianXtra", "CancFromGuardianXtra");
            // Session.Add("CancFromRoadAssistanceExit", "CancFromRoadAssistanceExit");
            Response.Redirect("CancellationPolicy.aspx", false);
        }
        //protected void Button5_Click(object sender, EventArgs e)
        //{

        //}

        private XmlDocument GetInsertOPPEndorsementXml(int OPPTaskControlID, int OPPQuotesID, double mFactor, double NewProRataTotPrem, double NewShotRateTotPrem)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[15];

            DbRequestXmlCooker.AttachCookItem("OPPTaskControlID", SqlDbType.Int, 0, OPPTaskControlID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("OPPQuotesID", SqlDbType.Int, 0, OPPQuotesID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("EndoEffective", SqlDbType.DateTime, 0, " ", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Factor", SqlDbType.Float, 0, mFactor.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ProRataPremium", SqlDbType.Float, 0, NewProRataTotPrem.ToString(), ref cookItems);       //5
            DbRequestXmlCooker.AttachCookItem("ShortRatePremium", SqlDbType.Float, 0, NewShotRateTotPrem.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremPropeties", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremProperties", SqlDbType.Float, 0, "0", ref cookItems);                        //10
            DbRequestXmlCooker.AttachCookItem("PreviousPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremProperties", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);                              //15

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
        private XmlDocument GetUpdateOPPEndorsementXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[11];

            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
            int OPPEndorsementID = (int)Session["OPPEndorsementID"];

            DbRequestXmlCooker.AttachCookItem("OPPEndorsementID", SqlDbType.Int, 0, OPPEndorsementID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("EndoNum", SqlDbType.Char, 4, taskControl.Endoso.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremPropeties", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);        //5
            DbRequestXmlCooker.AttachCookItem("PreviousPremProperties", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremProperties", SqlDbType.Float, 0, "0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremLiability", SqlDbType.Float, 0, "0", ref cookItems);         //10
            DbRequestXmlCooker.AttachCookItem("DiffPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
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
                            EPolicy.TaskControl.RoadAssistance opp = (EPolicy.TaskControl.RoadAssistance)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);
                            opp.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                            opp.IsEndorsement = true;

                            if (Session["TaskControl"] != null)
                            {
                                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
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
                        EPolicy.TaskControl.RoadAssistance newOPP = (EPolicy.TaskControl.RoadAssistance)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

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

                        EPolicy.TaskControl.RoadAssistance taskControl2 = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

                        int s = int.Parse(e.Item.Cells[2].Text);
                        string comments = e.Item.Cells[10].Text.Trim();
                        EPolicy.TaskControl.RoadAssistance newOPP2 = (EPolicy.TaskControl.RoadAssistance)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(s, userID);
                        int OPPEndorID = int.Parse(e.Item.Cells[1].Text);

                        //Print Document
                        try
                        {
                            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

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

                TaskControl.RoadAssistance taskControl = new TaskControl.RoadAssistance();  //Policy
                TaskControl.RoadAssistance GuardianXtra = (TaskControl.RoadAssistance)Session["TaskControl"];

                
                EPolicy.TaskControl.RoadAssistance taskControlEndo = null;

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
                taskControl.EffectiveDate = (DateTime.Parse(GuardianXtra.EffectiveDate).AddMonths(12)).ToShortDateString();
                taskControl.ExpirationDate = DateTime.Parse(GuardianXtra.ExpirationDate).AddMonths(12).ToShortDateString();
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
                taskControl.VIN = txtVehicleVIN.Text.ToString().Trim();
                taskControl.Plate = txtVehiclePlate.Text.ToString().Trim();
               // taskControl.VehicleMake = ddlVehicleMake.SelectedItem.Text.ToString();
               // taskControl.XtraVehicleModel = ddlVehicleModel.SelectedItem.Text.ToString().Trim();
               // taskControl.XtraVehicleYear = ddlVehicleYear.SelectedItem.Text.ToString().Trim();



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

                TaskControl.RoadAssistance taskControl = new TaskControl.RoadAssistance();  //Policy
                TaskControl.RoadAssistance GuardianXtra = (TaskControl.RoadAssistance)Session["TaskControl"];

                
                EPolicy.TaskControl.RoadAssistance taskControlEndo = null;
                
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
                taskControl.ExpirationDate = DateTime.Parse(GuardianXtra.ExpirationDate).AddMonths(12).ToShortDateString();
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
                taskControl.VIN = txtVehicleVIN.Text.ToString().Trim();
                taskControl.Plate = txtVehiclePlate.Text.ToString().Trim();
                taskControl.VehicleMakeID = ddlVehicleMake.SelectedIndex;
                //taskControl.XtraVehicleModel = ddlVehicleModel.SelectedItem.Text.ToString().Trim();
              //  taskControl.XtraVehicleYear = ddlVehicleYear.SelectedItem.Text.ToString().Trim();





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

        private List<string> WriteRdlcToPDF(ReportViewer viewer, EPolicy.TaskControl.RoadAssistance taskControl, List<string> mergePaths, int index)
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

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                
                //TaskControl.Policies policy = (TaskControl.Policies)Session["TaskControl"];
                //EPolicy.TaskControl.Laboratory taskControl = (EPolicy.TaskControl.Laboratory)Session["TaskControl"];
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

                Login.Login cp = HttpContext.Current.User as Login.Login;
                string userName = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();



                List<string> mergePaths = new List<string>();
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
                //EPolicy.TaskControl.Laboratory taskControl = (EPolicy.TaskControl.Laboratory)Session["TaskControl"];
                string PathReport = MapPath("Reports/RoadAssist/");
                string PathReportNew = MapPath("Reports/");
                string FileNamePdf = "";
                string PathReportAspen = MapPath("Reports/RoadAssist/");



                GetRoadAssistFileTableAdapters.GetRoadAssistFileTableAdapter ds = new GetRoadAssistFileTableAdapters.GetRoadAssistFileTableAdapter();
                ReportDataSource rpd = new ReportDataSource("GetRoadAssistFile", (DataTable)ds.GetData(taskControl.TaskControlID));


                if (IsSTHOMAS())
                {
                    // english RoadAssistVI
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.DataSources.Clear();
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = PathReport + "RoadAssistVI.rdlc";
                    viewer.LocalReport.DataSources.Add(rpd);
                    viewer.LocalReport.Refresh();
                    mergePaths = WriteRdlcToPDF(viewer, taskControl, mergePaths, 0);
                }
                else
                {

                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.DataSources.Clear();
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = PathReport + "RoadAssist.rdlc";
                    viewer.LocalReport.DataSources.Add(rpd);
                    viewer.LocalReport.Refresh();
                    mergePaths = WriteRdlcToPDF(viewer, taskControl, mergePaths, 0);
                }



                DataTable dt = taskControl.RoadAssistCollection;

                if (taskControl.RoadAssistCollection.Rows.Count == 2)
                {
                    if (IsSTHOMAS())
                    {
                        FileNamePdf = "TCGuardianUSVI.pdf";
                    }
                    else
                    {
                        FileNamePdf = "TERMINOS Y CONDICIONES CUBIERTA EXTENDIDA.pdf";
                    }
                    DeleteFile(ProcessedPath + FileNamePdf);
                    FileInfo file = new FileInfo(PathReport + FileNamePdf);
                    file.CopyTo(ProcessedPath + FileNamePdf);
                    mergePaths.Add(ProcessedPath + FileNamePdf);
                }
                else
                {
                    if (IsSTHOMAS())
                    {
                        FileNamePdf = "TCGuardianUSVI.pdf";
                    }
                    else
                    {
                        FileNamePdf = "TERMINOS Y CONDICIONES CUBIERTA REGULAR.pdf";
                    }
                    DeleteFile(ProcessedPath + FileNamePdf);
                    FileInfo file = new FileInfo(PathReport + FileNamePdf);
                    file.CopyTo(ProcessedPath + FileNamePdf);
                    mergePaths.Add(ProcessedPath + FileNamePdf);

                }
                    if (File.Exists(ProcessedPath + taskControl.TaskControlID.ToString() + ".pdf"))
                        File.Delete(ProcessedPath + taskControl.TaskControlID.ToString() + ".pdf");

                    //Generar PDF
                    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                    string FinalFile = "";
                    FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, TxtFirstName.Text.Trim() + txtLastname1.Text.Trim()); //taskControl.TaskControlID.ToString());
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                    taskControl.PrintPolicy = true;
                   
                
            }
            catch (Exception exc)
            {
                lblRecHeader.Text = exc.Message.Trim() + " - " + exc.InnerException.ToString();
                mpeSeleccion.Show();
            }
        }


        private void DeleteFile(string pathAndFileName)
        {
            if (File.Exists(pathAndFileName))
                File.Delete(pathAndFileName);
        }



        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {//joshua

                #region Print All
                if (ddlPrintOption.SelectedItem.Text == "Print All")
                {

                    EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                    List<string> mergePaths = new List<string>();
                    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];


                    mergePaths.Add(PrintPreview("SolicitudGuardianXtra.rdlc"));
                    if (chkDefpaysix.Checked)
                    {
                        mergePaths.Add(PrintPreview("Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc"));
                        mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas6Plazos.rdlc"));
                    }

                    if (chkDefpayfour.Checked)
                    {
                        mergePaths.Add(PrintPreview("SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc"));
                        mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas4Plazos.rdlc"));
                    }

                    if (chkCredit.Checked)
                    {
                        mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc"));
                    }

                    if (chkDebit.Checked)
                    {
                        mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc"));
                    }

                    mergePaths.Add(PrintPreview("HojadeDeclaraciones_XTRA.rdlc"));
                    mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina1.rdlc"));
                    mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina2.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina2.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina3.rdlc"));

                    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                    string finalFile = "";
                    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + finalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                }
                #endregion Print All

                #region Insured Information
                else if (ddlPrintOption.SelectedItem.Text == "Insured Information")
                {

                    EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                    List<string> mergePaths = new List<string>();
                    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];


                    mergePaths.Add(PrintPreview("SolicitudGuardianXtra.rdlc"));
                    //if (chkDefpaysix.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc"));
                    //    mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas6Plazos.rdlc"));
                    //}

                    //if (chkDefpayfour.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc"));
                    //    mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas4Plazos.rdlc"));
                    //}

                    //if (chkCredit.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc"));
                    //}

                    //if (chkDebit.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc"));
                    //}

                    //mergePaths.Add(PrintPreview("HojadeDeclaraciones_XTRA.rdlc"));
                    //mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina1.rdlc"));
                    //mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina2.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina2.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina3.rdlc"));

                    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                    string finalFile = "";
                    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + finalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                }
                #endregion Insured Information

                #region Payment Infromation
                else if (ddlPrintOption.SelectedItem.Text == "Payment Information")
                {

                    EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                    List<string> mergePaths = new List<string>();
                    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];


                    //mergePaths.Add(PrintPreview("SolicitudGuardianXtra.rdlc"));
                    if (chkDefpaysix.Checked)
                    {
                        mergePaths.Add(PrintPreview("Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc"));
                        mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas6Plazos.rdlc"));
                    }

                    if (chkDefpayfour.Checked)
                    {
                        mergePaths.Add(PrintPreview("SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc"));
                        mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas4Plazos.rdlc"));
                    }

                    if (chkCredit.Checked)
                    {
                        mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc"));
                    }

                    if (chkDebit.Checked)
                    {
                        mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc"));
                    }

                    //mergePaths.Add(PrintPreview("HojadeDeclaraciones_XTRA.rdlc"));
                    //mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina1.rdlc"));
                    //mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina2.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina2.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina3.rdlc"));

                    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                    string finalFile = "";
                    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + finalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                }
                #endregion Payment Infortmation

                #region Declaration Page
                else if (ddlPrintOption.SelectedItem.Text == "Declaration Page")
                {

                    EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                    List<string> mergePaths = new List<string>();
                    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];


                    //mergePaths.Add(PrintPreview("SolicitudGuardianXtra.rdlc"));
                    //if (chkDefpaysix.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc"));
                    //    mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas6Plazos.rdlc"));
                    //}

                    //if (chkDefpayfour.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc"));
                    //    mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas4Plazos.rdlc"));
                    //}

                    //if (chkCredit.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc"));
                    //}

                    //if (chkDebit.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc"));
                    //}

                    mergePaths.Add(PrintPreview("HojadeDeclaraciones_XTRA.rdlc"));
                    //mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina1.rdlc"));
                    //mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina2.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina2.rdlc"));
                    //mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina3.rdlc"));

                    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                    string finalFile = "";
                    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + finalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                }
                #endregion Declaration Page

                #region Terms & Services
                else if (ddlPrintOption.SelectedItem.Text == "Terms & Services")
                {

                    EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                    List<string> mergePaths = new List<string>();
                    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];


                    //mergePaths.Add(PrintPreview("SolicitudGuardianXtra.rdlc"));
                    //if (chkDefpaysix.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc"));
                    //    mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas6Plazos.rdlc"));
                    //}

                    //if (chkDefpayfour.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc"));
                    //    mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas4Plazos.rdlc"));
                    //}

                    //if (chkCredit.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc"));
                    //}

                    //if (chkDebit.Checked)
                    //{
                    //    mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc"));
                    //}

                    //mergePaths.Add(PrintPreview("HojadeDeclaraciones_XTRA.rdlc"));
                    mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina1.rdlc"));
                    mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina2.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina2.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina3.rdlc"));

                    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                    string finalFile = "";
                    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + finalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                }
                #endregion Terms & Services
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            try
            //{
            //    EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

            //    List<string> mergePaths = new List<string>();
            //    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];


            //    mergePaths.Add(PrintPreview("MidOceanInvoiceES.rdlc"));
                

            //    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            //    string finalFile = "";
            //    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);

            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + finalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
            //}
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                List<string> mergePaths = new List<string>();
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

                if (IsSTHOMAS())
                    mergePaths.Add(PrintPreview("MidOceanInvoiceEN.rdlc"));
                else
                    mergePaths.Add(PrintPreview("MidOceanInvoiceES_GuardianLogo.rdlc"));

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


        protected void GridVehicle_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select": //Edit
                    this.btnAddVehicle.Text = "Save";

                    txtVehicleVIN.Text = e.Item.Cells[4].Text.ToString().Trim();
                    txtVehiclePlate.Text = e.Item.Cells[5].Text.ToString().Trim();

                    ddlVehicleMake.SelectedIndex = ddlVehicleMake.Items.IndexOf(ddlVehicleMake.Items.FindByValue(e.Item.Cells[6].Text.Trim()));
                    SelectModel();
                    ddlVehicleModel.SelectedIndex = ddlVehicleModel.Items.IndexOf(ddlVehicleModel.Items.FindByValue(e.Item.Cells[7].Text.Trim()));
                    ddlVehicleYear.SelectedIndex = ddlVehicleYear.Items.IndexOf(ddlVehicleYear.Items.FindByValue(e.Item.Cells[8].Text.Trim()));
                    ddlNewUsed.SelectedIndex = ddlNewUsed.Items.IndexOf(ddlNewUsed.Items.FindByValue(e.Item.Cells[9].Text.Trim()));
                    txtIDRoadAssist.Text = e.Item.ItemIndex.ToString();

                    break;
                case "Delete":
                    DeleteRoadAssist(e.Item.ItemIndex);
                    break;
                default: //Page
                    break;
            }
        }


        protected void GridVehicle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int rowIndex = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Select")
            {

                        this.btnAddVehicle.Text = "Save";

                        txtVehicleVIN.Text = GridVehicle.DataKeys[rowIndex].Values["VIN"].ToString(); //e.Item.Cells[4].Text.ToString().Trim();
                        txtVehiclePlate.Text = GridVehicle.DataKeys[rowIndex].Values["Plate"].ToString();    //e.Item.Cells[5].Text.ToString().Trim();
                        ddlVehicleMake.SelectedIndex = ddlVehicleMake.Items.IndexOf(ddlVehicleMake.Items.FindByValue(GridVehicle.DataKeys[rowIndex].Values["VehicleMakeID"].ToString()));
                      
                        SelectModel();
                        ddlVehicleModel.SelectedIndex = ddlVehicleModel.Items.IndexOf(ddlVehicleModel.Items.FindByValue(GridVehicle.DataKeys[rowIndex].Values["VehicleModelID"].ToString()));
                        ddlVehicleYear.SelectedIndex = ddlVehicleYear.Items.IndexOf(ddlVehicleYear.Items.FindByValue(GridVehicle.DataKeys[rowIndex].Values["VehicleYearID"].ToString()));
                        //ddlNewUsed.SelectedIndex = int.Parse(GridVehicle.DataKeys[rowIndex].Values["NewUse"].ToString()); //ddlNewUsed.Items.IndexOf(ddlNewUsed.Items.FindByValue(e.Item.Cells[9].Text.Trim()));
                        txtIDRoadAssist.Text = GridVehicle.DataKeys[rowIndex].Values["AsistenciaCarreteraAutoID"].ToString(); //e.Item.ItemIndex.ToString();
                        if (IsSTHOMAS())
                        {
                            btnAddVehicle.Visible = true;
                        }
            }

            else
            {
                //int RoadAssistID = int.Parse(GridVehicle.DataKeys[rowIndex].Values["AsistenciaCarreteraAutoID"].ToString());

                int index = Convert.ToInt32(e.CommandArgument);
                DeleteRoadAssist(index);
                if (IsSTHOMAS())
                {
                    btnAddVehicle.Visible = true;
                }
            }
                     
                }
            


            catch (Exception ex)
            {

            }
        }

        private void FillRoadAssistVehiclesGrid(DataTable dt)
        {
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
            //DataTable dt = EPolicy.TaskControl.RoadAssistance.GetAutoRoadAssistByTaskControlID(taskControl.TaskControlID);

            GridVehicle.DataSource = null;
            //DataView dv = taskControl.RoadAssistCollection.DefaultView;
           // dv.Sort = "VehicleRowNumber";
            //taskControl.RoadAssistCollection.AcceptChanges();
            
            if (dt.Rows.Count != 0)
            {
                GridVehicle.DataSource = dt;
                GridVehicle.DataBind();
                GridVehicle.Visible = true;
            }
            else
            {
                 ddlVehicleModel.Items.Clear();
                ddlVehicleMake.SelectedIndex = -1;
                ddlVehicleModel.SelectedIndex = -1;
                ddlVehicleYear.SelectedIndex = -1;
                ddlNewUsed.SelectedIndex = -1;

                if (dt.Rows.Count != 0)
                {
                    GridVehicle.DataSource = dt;
                    GridVehicle.DataBind();
                }
                else
                {
                    GridVehicle.DataSource = null;
                    GridVehicle.DataBind();
                }
                //this.GridVehicle.Visible = true;
                //this.GridVehicle.DataSource = dt;
                //this.GridVehicle.DataBind();             
               // this.GridVehicle.Visible = true;
            }
        }

        private void FillRoadAssistVehiclesGridLoad()
        {
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
            DataTable dt = EPolicy.TaskControl.RoadAssistance.GetAutoRoadAssistByTaskControlID(taskControl.TaskControlID);

            if (dt.Rows.Count != 0)
            {
                GridVehicle.DataSource = dt;
                GridVehicle.DataBind();
                GridVehicle.Visible = true;
            }
            else
            {
                ddlVehicleModel.Items.Clear();
                ddlVehicleMake.SelectedIndex = -1;
                ddlVehicleModel.SelectedIndex = -1;
                ddlVehicleYear.SelectedIndex = -1;
                ddlNewUsed.SelectedIndex = -1;


                //this.GridVehicle.Visible = true;
                this.GridVehicle.DataSource = dt;
                this.GridVehicle.DataBind();
                // this.GridVehicle.Visible = true;
            }
        }



        protected void btnAddVehicle_Click(object sender, EventArgs e)
       {
            try
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

                ValidationCar();

                //if (txtIDRoadAssist.Text.Trim() != "")
                //{
                //    taskControl.RoadAssistCollection.Rows.RemoveAt(int.Parse(txtIDRoadAssist.Text));
                //    taskControl.RoadAssistCollection.AcceptChanges();
                //}

                ////////////////////////

                string tempVehicleID = "0";
                int MaxVehicleDetailID = 0;


                if (txtIDRoadAssist.Text.Trim() != "")
                {
                    for (int i = 0; i < taskControl.RoadAssistCollection.Rows.Count; i++)
                    {
                        if (taskControl.RoadAssistCollection.Rows[i]["AsistenciaCarreteraAutoID"].ToString() == txtIDRoadAssist.Text.Trim())
                        {
                            tempVehicleID = taskControl.RoadAssistCollection.Rows[i]["AsistenciaCarreteraAutoID"].ToString();
                            taskControl.RoadAssistCollection.Rows.RemoveAt(i);
                        }
                    }

                    taskControl.RoadAssistCollection.AcceptChanges();
                }

                if (taskControl.RoadAssistCollection.Rows.Count > 0)
                {
                    for (int i = 0; i < taskControl.RoadAssistCollection.Rows.Count; i++)
                    {
                        if (int.Parse(taskControl.RoadAssistCollection.Rows[i]["AsistenciaCarreteraAutoID"].ToString()) > MaxVehicleDetailID)
                        {
                            MaxVehicleDetailID = int.Parse(taskControl.RoadAssistCollection.Rows[i]["AsistenciaCarreteraAutoID"].ToString());
                        }
                    }
                }

                //////////////////////
                DataTable dt = null;
                DataRow myRow = taskControl.RoadAssistCollection.NewRow();
                //myRow["DependientesID"] = taskControl.DependientesCollection.Rows.Count + 1;
                myRow["TaskControlID"] = "0";
                myRow["VIN"] = txtVehicleVIN.Text.Trim().ToUpper();
                myRow["VehicleMakeID"] = ddlVehicleMake.SelectedItem.Value.Trim();
                myRow["VehicleModelID"] = ddlVehicleModel.SelectedItem.Value.Trim();
                myRow["VehicleYearID"] = ddlVehicleYear.SelectedItem.Value.Trim(); //LookupTables.LookupTables.GetDescription("Parentesco", ddlRelacionBen.SelectedItem.Value.Trim());
                myRow["VehicleMake"] = EPolicy.LookupTables.LookupTables.GetDescription("VehicleMake", ddlVehicleMake.SelectedItem.Value.Trim());
                myRow["VehicleModel"] = EPolicy.LookupTables.LookupTables.GetDescription("VehicleModel", ddlVehicleModel.SelectedItem.Value.Trim());
                myRow["VehicleYear"] = EPolicy.LookupTables.LookupTables.GetDescription("VehicleYear", ddlVehicleYear.SelectedItem.Value.Trim());
                myRow["NewUse"] = "1"; //ddlNewUsed.SelectedItem.Value.Trim();
                myRow["NewUseDesc"] = "1";// EPolicy.LookupTables.LookupTables.GetDescription("NewUse", ddlNewUsed.SelectedItem.Value.Trim());
                myRow["Plate"] = txtVehiclePlate.Text.Trim().ToUpper();
                myRow["IsCreditPayment"] = chkCredit.Checked;
                myRow["IsDebitPayment"] = chkDebit.Checked;
                myRow["IsCashPayment"] = chkCash.Checked;

                //myRow["HasCoverageExplain"] = TxtExplain.Text.Trim();
                Login.Login cp = HttpContext.Current.User as Login.Login;
                
                if (VerificarAutos(taskControl.RoadAssistCollection))
                {
                    myRow["Premium"] = 00.0;
                }
                else
                    myRow["Premium"] = 00.0;
                if (taskControl.RoadAssistCollection.Rows.Count < 2)
                {
                    if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp.UserID).ToString()).Contains("THOMAS") && taskControl.RoadAssistCollection.Rows.Count == 1)
                    {
                        this.btnAddVehicle.Text = "Add";
                        txtVehiclePlate.Text = "";
                        txtVehicleVIN.Text = "";

                        ddlVehicleModel.Items.Clear();
                        ddlVehicleMake.SelectedIndex = -1;
                        ddlVehicleModel.SelectedIndex = -1;
                        ddlVehicleYear.SelectedIndex = -1;
                        ddlNewUsed.SelectedIndex = -1;
                        throw new Exception("You can only add a maximun of 1 car.");
                    }

                    taskControl.RoadAssistCollection.Rows.Add(myRow);
                    taskControl.RoadAssistCollection.AcceptChanges();

                    dt = taskControl.RoadAssistCollection;
                    FillRoadAssistVehiclesGridLoad();
                    if(IsSTHOMAS())
                    {
                        btnAddVehicle.Visible = false;
                    }
                }

                else
                {
                    //lblRecHeader.Text = "You can only add a maximun of two cars.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                    //mpeSeleccion.Show();
                    this.btnAddVehicle.Text = "Add";
                    txtVehiclePlate.Text = "";
                    txtVehicleVIN.Text = "";

                    ddlVehicleModel.Items.Clear();
                    ddlVehicleMake.SelectedIndex = -1;
                    ddlVehicleModel.SelectedIndex = -1;
                    ddlVehicleYear.SelectedIndex = -1;
                    ddlNewUsed.SelectedIndex = -1;
                    throw new Exception("You can only add a maximun of two cars.");

                }

                this.btnAddVehicle.Text = "Add";
                txtVehiclePlate.Text = "";
                txtVehicleVIN.Text = "";

                ddlVehicleModel.Items.Clear();
                ddlVehicleMake.SelectedIndex = -1;
                ddlVehicleModel.SelectedIndex = -1;
                ddlVehicleYear.SelectedIndex = -1;
                ddlNewUsed.SelectedIndex = -1;

                if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp.UserID).ToString()).Contains("THOMAS") && taskControl.RoadAssistCollection.Rows.Count == 1)
                {
                    btnAddVehicle.Visible = false;

                }


                //this.GridVehicle.CurrentPageIndex = 0;
                this.GridVehicle.Visible = true;
                this.GridVehicle.DataSource = dt;
                this.GridVehicle.DataBind();
                this.GridVehicle.Visible = true;


                txtIDRoadAssist.Text = "";
                CalcTotalPremium(dt);
             //   FillRoadAssistVehiclesGrid();
            }
            catch (Exception xcp)
            {

                lblRecHeader.Text = xcp.Message;  //"You can only add a maximun of two cars.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
        }

        protected void ddlDeducible_SelectedIndexChanged(object sender, EventArgs e)
        {
            int 金 = int.Parse(ddlDeducible.SelectedIndex.ToString());

            switch (金)
            {
                case 1:
                    txtTotalPremium.Text = ddlDeducible.SelectedItem.Text.ToString().Trim().Replace("$", "");
                    txtPremium.Text = ddlDeducible.SelectedItem.Value;
                    break;

                case 2:
                    txtTotalPremium.Text = ddlDeducible.SelectedItem.Text.ToString().Trim().Replace("$", "");
                    txtPremium.Text = ddlDeducible.SelectedItem.Value;
                    break;

                case 3:
                    txtTotalPremium.Text = ddlDeducible.SelectedItem.Text.ToString().Trim().Replace("$", "");
                    txtPremium.Text = ddlDeducible.SelectedItem.Value;
                    break;

                default:
                    break;
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
            //return "";
            try
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                ReportViewer viewer = new ReportViewer();
                string ProcessPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

                viewer.LocalReport.DataSources.Clear();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("Reports/GuardianXtra/" + rdlcDoc);
                Microsoft.Reporting.WebForms.ReportDataSource rds = null;

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

                if (rdlcDoc == "MidOceanInvoiceES_GuardianLogo.rdlc")
                {
                    GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter ds = new GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter();
                    GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable dt = new GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new ReportDataSource("GetPaymentByTaskControlID", (DataTable)dt);

                    //ReportParameter[] param = new ReportParameter[1];

                    //param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                    //viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                if (rdlcDoc == "MidOceanInvoiceEN.rdlc")
                {
                    GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter ds = new GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter();
                    GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable dt = new GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new ReportDataSource("GetPaymentByTaskControlID", (DataTable)dt);

                    //ReportParameter[] param = new ReportParameter[1];

                    //param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                    //viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                // Variables 
                Warning[] warnings;
                string[] streamIds;
                string mimeType;
                string encoding = string.Empty;
                string extension;
                //  string fileName = "C" + taskControl.TaskControlID.ToString() + DateTime.Now.ToString().Trim();
                string _FileName = rdlcDoc.Replace(".rdlc", "") + taskControl.TaskControlID.ToString() + ".pdf";

                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName))
                    File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (FileStream fs = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Dispose();

                }
                return ProcessPath + _FileName;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        protected void txtVehicleVIN_TextChanged(object sender, EventArgs e)
        {

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

        //protected void rdbCubiertaNo_CheckedChanged(object sender, EventArgs e)
        //{
        //    HideDriverSection("hidden");
        //}
        //protected void rdbCubiertaYes_CheckedChanged(object sender, EventArgs e)
        //{
        //    HideDriverSection("");
        //}

        //protected void HideDriverSection(string ClassName)
        //{
        //    DriverLine1.Attributes["class"] = ClassName;
        //    DriverLine2.Attributes["class"] = ClassName;
        //    DriverLine3.Attributes["class"] = ClassName;
        //    DriverLine4.Attributes["class"] = ClassName;
        //    DriverLine5.Attributes["class"] = ClassName;
        //    DriverLine6.Attributes["class"] = ClassName;
        //    DriverLine7.Attributes["class"] = ClassName;
        //}

        protected void rdbCoverageNo_CheckedChanged(object sender, EventArgs e)
        {
            lblExplain.Visible = !rdbCoverageNo.Checked;
            TxtExplain.Visible = !rdbCoverageNo.Checked;

        }
        protected void rdbCoverageYes_CheckedChanged(object sender, EventArgs e)
        {
            lblExplain.Visible = rdbCoverageYes.Checked;
            TxtExplain.Visible = rdbCoverageYes.Checked;
        }
        protected void rdbDefpaymentYes_CheckedChanged(object sender, EventArgs e)
        {
            //lblSelectplan.Visible = rdbDefpaymentYes.Checked;
            //chkDefpayfour.Visible = rdbDefpaymentYes.Checked;
            //chkDefpaysix.Visible = rdbDefpaymentYes.Checked;
            chkDefpaysix.Checked = false;
            chkDefpayfour.Checked = false;
        }

        protected void rdbDefpaymentNo_CheckedChanged(object sender, EventArgs e)
        {
            //lblSelectplan.Visible = !rdbDefpaymentNo.Checked;
            //chkDefpayfour.Visible = !rdbDefpaymentNo.Checked;
            //chkDefpaysix.Visible = !rdbDefpaymentNo.Checked;
            chkDefpaysix.Checked = false;
            chkDefpayfour.Checked = false;
            chkDebit.Checked = false;
        }

        protected void chkDefpayfour_CheckedChanged(object sender, EventArgs e)
        {
            chkDefpaysix.Checked = !chkDefpayfour.Checked;
        }

        protected void chkDefpaysix_CheckedChanged(object sender, EventArgs e)
        {
            chkDefpayfour.Checked = !chkDefpaysix.Checked;
        }

        //Changes Is Commercial to Is Personal and the corresponding PolicyTypes and viceversa
        protected void chkIsCommercialAuto_CheckedChanged(object sender, EventArgs e)
        {
            var Pers = chkIsPersonalAuto.Checked;
            var Com = chkIsCommercialAuto.Checked;
            var PersCom = chkIsPersonalAuto.Checked = !chkIsCommercialAuto.Checked;
            var ComPers = chkIsCommercialAuto.Checked = !chkIsPersonalAuto.Checked;

            chkIsPersonalAuto.Checked = !chkIsCommercialAuto.Checked;
            if (Pers == true || PersCom == true)
            {
                TxtPolicyType.Text = "XPA";
            }
            if (Com == true || ComPers == true)
            {
                TxtPolicyType.Text = "XCA";
            }
        }

        protected void chkIsPersonalAuto_CheckedChanged(object sender, EventArgs e)
        {
            chkIsCommercialAuto.Checked = !chkIsPersonalAuto.Checked;
            if (chkIsPersonalAuto.Checked != true)
            {
                chkIsCommercialAuto.Checked = true;
                chkIsPersonalAuto.Checked = false;
            }
            if (chkIsCommercialAuto.Checked == true)
            {
                TxtPolicyType.Text = "XCA";
            }
            if (chkIsPersonalAuto.Checked == true)
            {
                TxtPolicyType.Text = "XPA";
            }
        }

        protected void chkCredit_CheckedChanged(object sender, EventArgs e)
        {
            chkDebit.Checked = !chkCredit.Checked;
            //se comento a peticion de Susana 10/17/2016
            //if (rdbDefpaymentNo.Checked == true)
            //{
            //    chkDebit.Checked = false;
            //}

            chkCash.Checked = false;
            chkCash_CheckedChanged(String.Empty, EventArgs.Empty);

        }

        protected void chkDebit_CheckedChanged(object sender, EventArgs e)
        {
            //se comento a peticion de Susana 10/17/2016
            //if (rdbDefpaymentNo.Checked == true)
            //{
            //    chkDebit.Checked = false;
            //}
            chkCredit.Checked = !chkDebit.Checked;

            chkCash.Checked = false;
            chkCash_CheckedChanged(String.Empty, EventArgs.Empty);
        }

        protected void chkCash_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCash.Checked)
            {
                chkCredit.Checked = false;
                chkDebit.Checked = false;

                //rdbDefpaymentYes.Enabled = false;
                //rdbDefpaymentNo.Enabled = false;
                chkDefpayfour.Enabled = false;
                chkDefpaysix.Enabled = false;
                //rdbDefpaymentNo.Checked = true;
                //rdbDefpaymentYes.Checked = false;
                rdbDefpaymentNo_CheckedChanged(String.Empty, EventArgs.Empty);
                //btnCashPayment.Visible = true;
                //txtCashPayment.Visible = true;
            }
            else
            {
                //rdbDefpaymentNo.Checked = true;
                //rdbDefpaymentYes.Checked = false;
                //rdbDefpaymentYes.Enabled = true;
               // rdbDefpaymentNo.Enabled = true;
                chkDefpayfour.Enabled = true;
                chkDefpaysix.Enabled = true;
                //btnCashPayment.Visible = false;
                //txtCashPayment.Visible = false;
                //txtCashPayment.Text = "";
            }


        }

        protected void chkSameMailing_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameMailing.Checked)
            {
                txtPhyAddress.Text = TxtAddrs1.Text.ToUpper().Trim();
                txtPhyAddress2.Text = TxtAddrs2.Text.ToUpper().Trim();
                txtPhyState.Text = TxtState.Text.ToUpper().Trim();
                ddlState2.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByText(ddlState.SelectedItem.Text.ToString()));

                if (IsSTHOMAS())
                {
                    DataTable dt2 = GetZipcodeByState(ddlState2.SelectedItem.Text);

                    ddlPhyZipCode.SelectedIndex = -1;

                    ddlPhyZipCode.DataSource = dt2;
                    ddlPhyZipCode.DataTextField = "Zipcode";
                    ddlPhyZipCode.DataValueField = "ZipCode";
                    ddlPhyZipCode.DataBind();
                    ddlPhyZipCode.SelectedIndex = -1;
                    ddlPhyZipCode.Items.Insert(0, "");

                    ddlPhyZipCode.SelectedIndex = ddlPhyZipCode.Items.IndexOf(ddlPhyZipCode.Items.FindByText(ddlZip.SelectedItem.Text.ToString()));
                    SetDDLValue(ddlPhyZipCode, ddlZip.SelectedItem.Text.ToString(), "Text");
                }
                else
                {
                    ddlPhyZipCode.SelectedIndex = ddlPhyZipCode.Items.IndexOf(ddlPhyZipCode.Items.FindByText(ddlZip.SelectedItem.Text.ToString()));
                }
                ddlPhyCity.SelectedIndex = ddlCiudad.Items.IndexOf(ddlCiudad.Items.FindByText(ddlCiudad.SelectedItem.Text.ToString()));
            }
            if (!chkSameMailing.Checked)
            {
                txtPhyAddress.Text = "";
                txtPhyAddress2.Text = "";
                ddlPhyZipCode.SelectedIndex = ddlPhyZipCode.SelectedIndex = -1;
                ddlPhyCity.SelectedIndex = ddlPhyCity.SelectedIndex = -1;
                ddlState2.SelectedIndex = ddlState2.SelectedIndex = -1;
            }
        }

        protected void txtEffDt_TextChanged(object sender, EventArgs e)
        {
            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

            //if (DateTime.Parse(txtEffDt.Text) < DateTime.Parse(DateTime.Now.ToShortDateString()))
            //{
                ////TxtEffBinderDate.Text = DateTime.Now.ToShortDateString();
                //lblRecHeader.Text = "Policy Effective Date should be Current Date.";
                //mpeSeleccion.Show();
                //return;
            //}

            DateTime Expdate = DateTime.Parse(DateTime.Parse(txtEffDt.Text.ToString()).AddMonths(12).ToShortDateString());
            txtExpDt.Text = Expdate.ToString();

            //DateTime Expdate = DateTime.Parse(txtEffDt.Text.ToString());
            //Expdate = DateTime.Parse(DateTime.Parse(txtEffDt.Text.ToString()).AddMonths(12).ToShortDateString());

            //txtExpDt.Text = Expdate.ToString();
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
            try
            {
                if (ddlZip.SelectedIndex > 0)
                {
                    DataTable dtCiudad = EPolicy.LookupTables.LookupTables.GetCiudadByZipCode(ddlZip.SelectedItem.Text);

                    if (dtCiudad.Rows[0]["Ciudad"].ToString() != "")
                    {
                        for (int i = 0; ddlCiudad.Items.Count - 1 >= i; i++)
                        {
                            if (ddlCiudad.Items[i].Text.Trim() == dtCiudad.Rows[0]["Ciudad"].ToString().Trim())
                            {
                                ddlCiudad.SelectedIndex = i;
                                i = ddlCiudad.Items.Count - 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
            //if (ddlCiudad.SelectedValue != ddlZip.SelectedValue)
            //{
            //    ddlCiudad.SelectedIndex = ddlZip.Items.IndexOf(ddlZip.Items.FindByValue(ddlZip.SelectedValue.ToString()));
            //    //ddlPhyZipCode.SelectedIndex = ddlCiudad.Items.IndexOf(ddlCiudad.Items.FindByValue(ddlCiudad.SelectedItem.Value.ToString()));
            //    //ddlPhyCity.SelectedIndex = ddlCiudad.Items.IndexOf(ddlCiudad.Items.FindByText(ddlCiudad.SelectedItem.Text.ToString()));
            //}
        }

        protected void ddlPhyZipCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkSameMailing.Checked = false;
            if (ddlPhyZipCode.SelectedIndex > 0)
            {
                DataTable dtCiudad = EPolicy.LookupTables.LookupTables.GetCiudadByZipCode(ddlPhyZipCode.SelectedItem.Text);
              if (dtCiudad.Rows.Count > 0)
               {
                
                if (dtCiudad.Rows[0]["Ciudad"].ToString() != "")
                {
                    for (int i = 0; ddlPhyCity.Items.Count - 1 >= i; i++)
                    {
                        if (ddlPhyCity.Items[i].Text.Trim() == dtCiudad.Rows[0]["Ciudad"].ToString().Trim())
                        {
                            ddlPhyCity.SelectedIndex = i;
                            i = ddlPhyCity.Items.Count - 1;
                        }
                    }
                }
                }
            }
        }

        protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCiudad.SelectedIndex == 0)
            {
                DataTable dtZipCode = EPolicy.LookupTables.LookupTables.GetZipCodeByDistinctCiudad(ddlCiudad.SelectedItem.Text);

                ddlZip.DataSource = dtZipCode;
                ddlZip.DataTextField = "ZipCode";
                ddlZip.DataValueField = "ZipCode";
                ddlZip.DataBind();
                ddlZip.Items.Insert(0, "");
                ddlZip.SelectedIndex = 0;
            }
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
            chkSameMailing.Checked = false;
            if (ddlPhyCity.SelectedIndex == 0)
            {
                DataTable dtZipCode = EPolicy.LookupTables.LookupTables.GetZipCodeByDistinctCiudad(ddlPhyCity.SelectedItem.Text);

                ddlPhyZipCode.DataSource = dtZipCode;
                ddlPhyZipCode.DataTextField = "ZipCode";
                ddlPhyZipCode.DataValueField = "ZipCode";
                ddlPhyZipCode.DataBind();
                ddlPhyZipCode.Items.Insert(0, "");
                ddlPhyZipCode.SelectedIndex = 0;
            }
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
        protected void btnGuardianPay_Click(object sender, EventArgs e)
        {
            try
            {
                Session["IsDebitPayment"] = "0";

                if (chkDebit.Checked)
                {
                    Session["IsDebitPayment"] = "1";
                }
                else
                {
                    Session["IsDebitPayment"] = "0";
                }

                RemoveSessionLookUp();
               // TaskControl.GuardianXtra taskControl = (TaskControl.GuardianXtra)Session["TaskControl"];
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                Session.Add("TaskControl", taskControl);
                Session.Add("FromUI", "RoadAssistance.aspx");

                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
                Response.Redirect("PaymentDPRoadAssist.aspx?id=" + encrypt.Encrypt(taskControl.TaskControlID.ToString()), false);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }

        private DataTable GetPaymentByTaskControlID()
        {

                DataTable dt = null;
            try
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

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

        public void PolicyXML(int TaskControlID)
        {
            try
            {

                    EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                    SqlConnection conn4 = null;
                    SqlDataReader rdr4 = null;
                    XmlDocument xmlDoc = new XmlDocument();
                    SqlConnection conn = null;


                    string ConnectionString = ConfigurationManager.ConnectionStrings["GuardianConnectionString"].ConnectionString;

                    conn = new SqlConnection(ConnectionString);
                    conn4 = new SqlConnection(ConnectionString);

                    conn4.Open();

                    SqlCommand cmd4 = new SqlCommand("GetTaskControlByTaskControlID", conn4);

                    cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd4.Parameters.AddWithValue("@TaskControlID", TaskControlID);
                    cmd4.CommandTimeout = 0;
                    rdr4 = cmd4.ExecuteReader();

                    NAMECONVENTION = DateTime.Now.ToString("MM.dd.yyyy_hhmmss");

                    #region XML Policy Info

                    XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-16", null);
                    xmlDoc.AppendChild(docNode);


                    XmlElement xmlPolicy = xmlDoc.CreateElement("Policies");
                    xmlDoc.AppendChild(xmlPolicy);

                    XmlAttribute nsAttribute = xmlDoc.CreateAttribute("xmlns", "xsi",
                        "http://www.w3.org/2000/xmlns/");
                    nsAttribute.Value = "http://www.w3.org/2001/XMLSchema-instance";
                    xmlPolicy.Attributes.Append(nsAttribute);


                    XmlAttribute nsAttribute2 = xmlDoc.CreateAttribute("xmlns",
                        "http://www.w3.org/2000/xmlns/");
                    nsAttribute2.Value = "pps-simple-auto-policy";
                    xmlPolicy.Attributes.Append(nsAttribute2);


                    rdr4.Read();
                
                    SqlDataReader rdr = null;


                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetGuardianXtraXMLReport", conn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                    cmd.CommandTimeout = 0;


                    rdr = cmd.ExecuteReader();



                    while (rdr.Read())
                    {
                        XmlElement xmlPolicy1 = xmlDoc.CreateElement("Policy");  // Creacion del elemento
                        xmlPolicy.AppendChild(xmlPolicy1);  // Abajo de donde vas a "append" el elemento


                        XmlElement xmlPolicyID = xmlDoc.CreateElement("PolicyID");
                        xmlPolicy1.AppendChild(xmlPolicyID);
                        xmlPolicyID.InnerText = rdr["PolicyType"].ToString().Trim();

                        XmlElement xmlIncept = xmlDoc.CreateElement("Incept");
                        xmlPolicy1.AppendChild(xmlIncept);
                        xmlIncept.InnerText = DateTime.Parse(rdr["EffectiveDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";    // A donde y que columna de que reader vas a insertar en el texto

                        XmlElement xmlExpire = xmlDoc.CreateElement("Expire");
                        xmlPolicy1.AppendChild(xmlExpire);
                        xmlExpire.InnerText = DateTime.Parse(rdr["ExpirationDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";

                        XmlElement xmlRenewalOf = xmlDoc.CreateElement("RenewalOf");
                        xmlPolicy1.AppendChild(xmlRenewalOf);
                        xmlRenewalOf.InnerText = "0";//rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + rdr["Sufijo"].ToString().Trim();

                        string AgentID = "000";
                        string ComRate = "0.0000000e+000";

                        if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                        {
                            AgentID = ddlAgent.SelectedItem.Value;

                        }
                        else
                        {
                            AgentID = ddlAgent.SelectedItem.Value;
                        }


                        DataTable DtCommision = GetCommissionAgentRateByAgentID(TaskControlID.ToString(), "23");

                        if (DtCommision.Rows.Count > 0)
                        {
                            ComRate = DtCommision.Rows[0]["CommissionRate"].ToString();

                            ComRate = (double.Parse(ComRate) / 100).ToString();
                        }
                        else
                        {
                            ComRate = "0.0000000e+000";
                        }

                        XmlElement xmlBrokerID = xmlDoc.CreateElement("BrokerID");
                        xmlPolicy1.AppendChild(xmlBrokerID);
                        xmlBrokerID.InnerText = "178";//178 produccion 9 Prueba

                        XmlElement xmlCanDate = xmlDoc.CreateElement("CanDate");
                        XmlAttribute attribute1 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance"); // creacion de un atributo
                        attribute1.Value = "true";  // atributo = a "true"
                        xmlCanDate.Attributes.Append(attribute1); // "appending el atributo"
                        xmlPolicy1.AppendChild(xmlCanDate);

                        XmlElement xmlTmpTime = xmlDoc.CreateElement("TmpTime");
                        XmlAttribute attribute2 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute2.Value = "true";
                        xmlTmpTime.Attributes.Append(attribute2);
                        xmlPolicy1.AppendChild(xmlTmpTime);
                        //xmlTmpTime.InnerText = "0";

                        XmlElement xmlBinderID = xmlDoc.CreateElement("BinderID");
                        //XmlAttribute attribute3 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute3.Value = "true";
                        //xmlBinderID.Attributes.Append(attribute3);
                        xmlPolicy1.AppendChild(xmlBinderID);
                        xmlBinderID.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + rdr["Sufijo"].ToString().Trim();


                        XmlElement xmlComRate = xmlDoc.CreateElement("ComRate");
                        xmlPolicy1.AppendChild(xmlComRate);
                        xmlComRate.InnerText = ComRate;

                        XmlElement xmlClient = xmlDoc.CreateElement("Client");
                        xmlPolicy1.AppendChild(xmlClient);
                        xmlClient.InnerText = "0";

                        XmlElement xmlTag = xmlDoc.CreateElement("Tag");
                        //XmlAttribute attribute4 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute4.Value = "true";
                        //xmlTag.Attributes.Append(attribute4);
                        xmlPolicy1.AppendChild(xmlTag);
                        xmlTag.InnerText = "0";

                        //XmlElement xmlDeduct = xmlDoc.CreateElement("Deductible");
                        //xmlPolicy1.AppendChild(xmlDeduct);
                        //xmlDeduct.InnerText = rdr["Deducible"].ToString().Trim();

                        XmlElement xmlPremium = xmlDoc.CreateElement("Premium");
                        xmlPolicy1.AppendChild(xmlPremium);
                        //xmlPremium.InnerText = rdr["TotalPremium"].ToString().Trim();
                        if (rdr["TotalPremium"].ToString().Trim() == "200")
                        {
                            xmlPremium.InnerText = "89";
                        }
                        else if (rdr["TotalPremium"].ToString().Trim() == "150")
                        {
                            xmlPremium.InnerText = "95";
                        }
                        else if (rdr["TotalPremium"].ToString().Trim() == "100")
                        {
                            xmlPremium.InnerText = "100";
                        }

                        XmlElement xmlDispImage = xmlDoc.CreateElement("DispImage");
                        xmlPolicy1.AppendChild(xmlDispImage);
                        xmlDispImage.InnerText = "Policy";

                        XmlElement xmlSpecEndorse = xmlDoc.CreateElement("SpecEndorse");
                        XmlAttribute attribute5 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute5.Value = "true";
                        xmlSpecEndorse.Attributes.Append(attribute5);
                        xmlPolicy1.AppendChild(xmlSpecEndorse);

                        XmlElement xmlSID = xmlDoc.CreateElement("SID");
                        xmlPolicy1.AppendChild(xmlSID);
                        xmlSID.InnerText = "0";


                        XmlElement xmlUDPolicyID = xmlDoc.CreateElement("UDPolicyID");
                        //XmlAttribute attribute6 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute6.Value = "true";
                        //xmlUDPolicyID.Attributes.Append(attribute6);
                        xmlPolicy1.AppendChild(xmlUDPolicyID);
                        xmlUDPolicyID.InnerText = "0";

                        XmlElement xmlPreparedBy = xmlDoc.CreateElement("PreparedBy");
                        xmlPolicy1.AppendChild(xmlPreparedBy);
                        xmlPreparedBy.InnerText = rdr["EnteredBy"].ToString().Trim();

                        //XmlElement xmlAgent = xmlDoc.CreateElement("Agent");
                        //xmlPolicy1.AppendChild(xmlAgent);
                        //xmlAgent.InnerText = rdr["AgentDesc"].ToString().Trim();

                        //XmlElement xmlAgency = xmlDoc.CreateElement("Agency");
                        //xmlPolicy1.AppendChild(xmlAgency);
                        //xmlAgency.InnerText = rdr["AgencyDesc"].ToString().Trim();

                        XmlElement xmlExcessLink = xmlDoc.CreateElement("ExcessLink");
                        xmlPolicy1.AppendChild(xmlExcessLink);
                        xmlExcessLink.InnerText = "0";

                        XmlElement xmlPolSubType = xmlDoc.CreateElement("PolSubType");
                        xmlPolicy1.AppendChild(xmlPolSubType);
                        xmlPolSubType.InnerText = "Pvt"; //Pvt produccion 0 prueba

                        XmlElement xmlReinsPcnt = xmlDoc.CreateElement("ReinsPcnt");
                        xmlPolicy1.AppendChild(xmlReinsPcnt);
                        xmlReinsPcnt.InnerText = "0.0000000e+000";

                        XmlElement xmlAssessment = xmlDoc.CreateElement("Assessment");
                        xmlPolicy1.AppendChild(xmlAssessment);
                        xmlAssessment.InnerText = "0.0000";

                        XmlElement xmlPayDate = xmlDoc.CreateElement("PayDate");
                        XmlAttribute attribute7 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute7.Value = "true";
                        xmlPayDate.Attributes.Append(attribute7);
                        xmlPolicy1.AppendChild(xmlPayDate);
                        //xmlPayDate.InnerText = "0";

                        XmlElement xmlPolRelTable = xmlDoc.CreateElement("PolRelTable");
                        xmlPolicy1.AppendChild(xmlPolRelTable);

                        XmlElement xmlPolRel = xmlDoc.CreateElement("PolRel");
                        xmlPolRelTable.AppendChild(xmlPolRel);

                        XmlElement xmlPolicy1ID1 = xmlDoc.CreateElement("PolicyID");
                        xmlPolRel.AppendChild(xmlPolicy1ID1);
                        xmlPolicy1ID1.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + rdr["Sufijo"].ToString().Trim();

                        XmlElement xmlUpid = xmlDoc.CreateElement("Upid");
                        xmlPolRel.AppendChild(xmlUpid);
                        xmlUpid.InnerText = "0";

                        XmlElement xmlPolRelat = xmlDoc.CreateElement("Polrelat");
                        //XmlAttribute attribute19 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute19.Value = "true";
                        //xmlPolRelat.Attributes.Append(attribute19);
                        xmlPolRel.AppendChild(xmlPolRelat);
                        xmlPolRelat.InnerText = "NI";

                        XmlElement xmlEntNamesTable = xmlDoc.CreateElement("EntNamesTable");
                        xmlPolRel.AppendChild(xmlEntNamesTable);


                        XmlElement xmlEntNames = xmlDoc.CreateElement("EntNames");
                        xmlEntNamesTable.AppendChild(xmlEntNames);

                        XmlElement xmlLast1Name = xmlDoc.CreateElement("LastName");
                        xmlEntNames.AppendChild(xmlLast1Name);
                        xmlLast1Name.InnerText = rdr["Lastna1"].ToString().Trim();

                        //XmlElement xmlLast2Name = xmlDoc.CreateElement("LastName2");
                        //xmlEntNames.AppendChild(xmlLast2Name);
                        //xmlLast2Name.InnerText = rdr["Lastna2"].ToString().Trim();

                        XmlElement xmlFirstName = xmlDoc.CreateElement("FirstName");
                        xmlEntNames.AppendChild(xmlFirstName);
                        xmlFirstName.InnerText = rdr["Firstna"].ToString().Trim();

                        XmlElement xmlMiddle = xmlDoc.CreateElement("Middle");
                        xmlEntNames.AppendChild(xmlMiddle);
                        xmlMiddle.InnerText = rdr["Initial"].ToString().Trim();

                        XmlElement xmlUpid1 = xmlDoc.CreateElement("Upid");
                        xmlEntNames.AppendChild(xmlUpid1);
                        xmlUpid1.InnerText = "0";

                        XmlElement xmlDob = xmlDoc.CreateElement("Dob");
                        xmlEntNames.AppendChild(xmlDob);
                        xmlDob.InnerText = DateTime.Parse(rdr["Birthday"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";

                        XmlElement xmlSex = xmlDoc.CreateElement("Sex");
                        xmlEntNames.AppendChild(xmlSex);
                        xmlSex.InnerText = "M";

                        XmlElement xmlMarital = xmlDoc.CreateElement("Marital");
                        xmlEntNames.AppendChild(xmlMarital);
                        xmlMarital.InnerText = "S";

                        XmlElement xmlYrsexp = xmlDoc.CreateElement("Yrsexp");
                        xmlEntNames.AppendChild(xmlYrsexp);
                        xmlYrsexp.InnerText = "0";

                        XmlElement xmlLicence = xmlDoc.CreateElement("License");
                       // XmlAttribute attribute9 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute9.Value = "true";
                        //xmlLicence.Attributes.Append(attribute9);
                        xmlEntNames.AppendChild(xmlLicence);
                        xmlLicence.InnerText = rdr["Licence"].ToString().Trim();

                        XmlElement xmlState = xmlDoc.CreateElement("State");
                        xmlEntNames.AppendChild(xmlState);
                        xmlState.InnerText = rdr["State"].ToString().Trim();


                        XmlElement xmlSsn = xmlDoc.CreateElement("Ssn");
                        xmlEntNames.AppendChild(xmlSsn);
                        xmlSsn.InnerText = "Ssn";

                        //SqlConnection conn3 = null;
                        //SqlDataReader rdr3 = null;

                        //conn3 = new SqlConnection(ConnectionString);

                        //conn3.Open();

                        //SqlCommand cmd3 = new SqlCommand("GetReportAutoVehiclesInfoPolicy_VI", conn3);

                        //cmd3.CommandType = System.Data.CommandType.StoredProcedure;

                        //cmd3.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                        //cmd3.CommandTimeout = 0;

                        //rdr3 = cmd3.ExecuteReader();
                        string BusFlag;
                        if (rdr["PolicyType"].ToString().Trim() == "XPA")
                        {
                            XmlElement xmlBusFlag = xmlDoc.CreateElement("BusFlag");
                            xmlEntNames.AppendChild(xmlBusFlag);
                            xmlBusFlag.InnerText = "0";
                            BusFlag = xmlBusFlag.InnerText.ToString();
                        }
                        else
                        {
                            XmlElement xmlBusFlag = xmlDoc.CreateElement("BusFlag");
                            xmlEntNames.AppendChild(xmlBusFlag);
                            xmlBusFlag.InnerText = "1";
                            BusFlag = xmlBusFlag.InnerText.ToString();
                        }

                        //conn3.Close();

                        XmlElement xmlNsbyt = xmlDoc.CreateElement("Nsbyt");
                        xmlEntNames.AppendChild(xmlNsbyt);
                        xmlNsbyt.InnerText = "1";

                        XmlElement xmlBusOther = xmlDoc.CreateElement("BusOther");
                        XmlAttribute attribute10 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute10.Value = "true";
                        xmlBusOther.Attributes.Append(attribute10);
                        xmlEntNames.AppendChild(xmlBusOther);

                        XmlElement xmlBusType = xmlDoc.CreateElement("BusType");
                        if (BusFlag != "1")
                        {
                            XmlAttribute attribute11 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                            attribute11.Value = "true";
                            xmlBusType.Attributes.Append(attribute11);
                            xmlEntNames.AppendChild(xmlBusType);
                        }
                        xmlEntNames.AppendChild(xmlBusType);

                        if (BusFlag == "1" && rdr["PolicyType"].ToString().Trim() == "XPA")
                            xmlBusType.InnerText = "256";
                        else if (BusFlag == "1" && rdr["PolicyType"].ToString().Trim() == "XCA")
                            xmlBusType.InnerText = "32";

                        XmlElement xmlClient1 = xmlDoc.CreateElement("Client");
                        XmlAttribute attribute12 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute12.Value = "true";
                        xmlClient1.Attributes.Append(attribute12);
                        xmlEntNames.AppendChild(xmlClient1);
                        //xmlClient1.InnerText = rdr["CustomerNo"].ToString().Trim();

                        XmlElement xmlPolRelat1 = xmlDoc.CreateElement("PolRelat");
                        XmlAttribute attribute13 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute13.Value = "true";
                        xmlPolRelat1.Attributes.Append(attribute13);
                        xmlEntNames.AppendChild(xmlPolRelat1);

                        XmlElement xmlDispImage1 = xmlDoc.CreateElement("DispImage");
                        xmlEntNames.AppendChild(xmlDispImage1);
                        xmlDispImage1.InnerText = "Person";


                        XmlElement xmlVehicleTable = xmlDoc.CreateElement("VehicleTable");
                        xmlPolicy1.AppendChild(xmlVehicleTable);



                        XmlElement xmlVehicle = xmlDoc.CreateElement("Vehicle");
                        xmlVehicleTable.AppendChild(xmlVehicle);

                        XmlElement xmlVin = xmlDoc.CreateElement("Vin");
                        xmlVehicle.AppendChild(xmlVin);
                        xmlVin.InnerText = rdr["VIN"].ToString().Trim();

                        XmlElement xmlPolicy1Id = xmlDoc.CreateElement("PolicyID");
                        xmlVehicle.AppendChild(xmlPolicy1Id);
                        xmlPolicy1Id.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + rdr["Sufijo"].ToString().Trim();

                        if (rdr["PolicyType"].ToString().Trim() == "XPA")
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "PVT";
                        }
                        else
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "CML";
                        }

                        string Plate = "";

                        if (rdr["Plate"].ToString().Trim() != "")
                        {
                            Plate = rdr["Plate"].ToString().Trim();
                        }
                        else
                        {
                            Plate = "0";
                        }

                        XmlElement xmlLicPlate = xmlDoc.CreateElement("LicPlate");
                        //XmlAttribute attribute14 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute14.Value = "true";
                        //xmlLicPlate.Attributes.Append(attribute14);
                        xmlVehicle.AppendChild(xmlLicPlate);
                        xmlLicPlate.InnerText = Plate;//rdr["Plate"].ToString().Trim();


                        XmlElement xmlPurchDate = xmlDoc.CreateElement("PurchDate");
                        xmlVehicle.AppendChild(xmlPurchDate);
                        xmlPurchDate.InnerText = "1753-01-01T00:00:00";

                        XmlElement xmlActCost = xmlDoc.CreateElement("ActCost");
                        xmlVehicle.AppendChild(xmlActCost);
                        xmlActCost.InnerText = "0.0000";

                        XmlElement xmlInsVal = xmlDoc.CreateElement("InsVal");
                        xmlVehicle.AppendChild(xmlInsVal);
                        xmlInsVal.InnerText = "4000";

                        XmlElement xmlInsValFlag = xmlDoc.CreateElement("InsValFlag");
                        xmlVehicle.AppendChild(xmlInsValFlag);
                        xmlInsValFlag.InnerText = "Actual Cash Value";
                        //XmlAttribute attribute14 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute14.Value = "true";
                        //xmlInsValFlag.Attributes.Append(attribute14);
                        //xmlInsValFlag.InnerText = "Actual Cash Value";

                        XmlElement xmlPayee = xmlDoc.CreateElement("Payee");
                        xmlVehicle.AppendChild(xmlPayee);
                        XmlAttribute attribute15 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute15.Value = "true";
                        xmlPayee.Attributes.Append(attribute15);
                        //xmlPayee.InnerText = rdr["BankDesc"].ToString().Trim();


                        XmlElement xmlIsland = xmlDoc.CreateElement("Island");
                        xmlVehicle.AppendChild(xmlIsland);
                        xmlIsland.InnerText = "4";// PUERTO RICO

                        XmlElement xmlLeased = xmlDoc.CreateElement("Leased");
                        xmlVehicle.AppendChild(xmlLeased);
                        xmlLeased.InnerText = "0";

                        XmlElement xmlRegExp = xmlDoc.CreateElement("RegExp");
                        xmlVehicle.AppendChild(xmlRegExp);
                        xmlRegExp.InnerText = "0";

                        XmlElement xmlPAE = xmlDoc.CreateElement("PAE");
                        xmlVehicle.AppendChild(xmlPAE);
                        xmlPAE.InnerText = "0";

                        XmlElement xmlEnd22 = xmlDoc.CreateElement("End22");
                        xmlVehicle.AppendChild(xmlEnd22);
                        xmlEnd22.InnerText = "0";

                        XmlElement xmlEnd23 = xmlDoc.CreateElement("End23");
                        xmlVehicle.AppendChild(xmlEnd23);
                        xmlEnd23.InnerText = "0";

                        XmlElement xmlPayeeID = xmlDoc.CreateElement("PayeeID");
                        xmlVehicle.AppendChild(xmlPayeeID);
                        xmlPayeeID.InnerText = "274";


                        XmlElement xmlTagNumber = xmlDoc.CreateElement("TagNumber");
                        //XmlAttribute attribute16 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute16.Value = "true";
                        //xmlTagNumber.Attributes.Append(attribute16);
                        xmlVehicle.AppendChild(xmlTagNumber);
                        xmlTagNumber.InnerText = "0";

                        XmlElement xmlPhysVehicleTable = xmlDoc.CreateElement("PhysVehicleTable");
                        xmlVehicle.AppendChild(xmlPhysVehicleTable);

                        XmlElement xmlPhysVehicle = xmlDoc.CreateElement("PhysVehicle");
                        xmlPhysVehicleTable.AppendChild(xmlPhysVehicle);

                        XmlElement xmlVin1 = xmlDoc.CreateElement("Vin");
                        xmlPhysVehicle.AppendChild(xmlVin1);
                        xmlVin1.InnerText = rdr["VIN"].ToString().Trim();

                        XmlElement xmlMYear = xmlDoc.CreateElement("MYear");
                        xmlPhysVehicle.AppendChild(xmlMYear);
                        xmlMYear.InnerText = rdr["VehicleYear"].ToString().Trim();

                        XmlElement xmlMake = xmlDoc.CreateElement("Make");
                        xmlPhysVehicle.AppendChild(xmlMake);
                        xmlMake.InnerText = rdr["VehicleMake"].ToString().Trim();


                        XmlElement xmlModel = xmlDoc.CreateElement("Model");
                        xmlPhysVehicle.AppendChild(xmlModel);
                        xmlModel.InnerText = rdr["VehicleModel"].ToString().Trim();

                        //XmlElement xmlPlate = xmlDoc.CreateElement("Plate");
                        //xmlPhysVehicle.AppendChild(xmlPlate);
                        //xmlPlate.InnerText = rdr["Plate"].ToString().Trim();

                        XmlElement xmlBodyType = xmlDoc.CreateElement("BodyType");
                        xmlPhysVehicle.AppendChild(xmlBodyType);
                        xmlBodyType.InnerText = "PU";

                        XmlElement xmlCylinder = xmlDoc.CreateElement("Cylinder");
                        xmlPhysVehicle.AppendChild(xmlCylinder);
                        xmlCylinder.InnerText = "0";

                        XmlElement xmlPassengers = xmlDoc.CreateElement("Passengers");
                        xmlPhysVehicle.AppendChild(xmlPassengers);
                        xmlPassengers.InnerText = "0";

                        XmlElement xmlTwoTon = xmlDoc.CreateElement("TwoTon");
                        xmlPhysVehicle.AppendChild(xmlTwoTon);
                        xmlTwoTon.InnerText = "0";

                        XmlElement xmlSalvaged = xmlDoc.CreateElement("Salvaged");
                        xmlPhysVehicle.AppendChild(xmlSalvaged);
                        xmlSalvaged.InnerText = "0";

                        XmlElement xmlVehicleCvrgTable = xmlDoc.CreateElement("VehicleCvrgTable");
                        xmlVehicle.AppendChild(xmlVehicleCvrgTable);



                        //SqlConnection conn2 = null;
                        //SqlDataReader rdr2 = null;


                        //conn2 = new SqlConnection(ConnectionString);

                        //conn2.Open();

                        //SqlCommand cmd2 = new SqlCommand("GetReportAutoGeneralInfo_VI", conn2);

                        //cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                        //cmd2.Parameters.AddWithValue("@TaskControlID", "111");

                        //rdr2 = cmd2.ExecuteReader();



                        //   while (rdr2.Read())
                        //{
                        //XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                        //xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                        ////XmlElement xmlPersonal = xmlDoc.CreateElement("Personal");
                        ////xmlVehicleCvrg.AppendChild(xmlPersonal);
                        ////xmlPersonal.InnerText = rdr["IsPersonalAuto"].ToString().Trim();

                        ////XmlElement xmlCommercial = xmlDoc.CreateElement("Commercial");
                        ////xmlVehicleCvrg.AppendChild(xmlCommercial);
                        ////xmlCommercial.InnerText = rdr["IsCommercialAuto"].ToString().Trim();

                        //XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                        //xmlVehicleCvrg.AppendChild(xmlVin2);
                        //xmlVin2.InnerText = rdr["VIN"].ToString().Trim();

                        //joshua
                        SqlConnection conn2 = null;
                        SqlDataReader rdr2 = null;

                        conn2 = new SqlConnection(ConnectionString);

                        conn2.Open();

                        SqlCommand cmd2 = new SqlCommand("GetReportAutoVehiclesInfoPolicy_VI", conn2);

                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd2.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                        cmd2.CommandTimeout = 0;

                        rdr2 = cmd2.ExecuteReader();

                        while (rdr2.Read())
                        {

                            // if (rdr1["PolicyType"].ToString().Trim() != "0")
                            //{
                            //    if (rdr1["PolicyType"].ToString().Trim() == "PAV")
                            //    {
                            string hello = rdr2["PDPremium"].ToString();
                            if (rdr2["PDPremium"].ToString() != "0")
                            {
                                //SAC
                                if (rdr2["PolicyType"].ToString().Trim() == "XPA")
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "01192";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }

                                else //SCC XCA
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "08194";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }
                                //2
                                if (rdr2["PolicyType"].ToString().Trim() == "XPA")
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "02192";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }

                                else //SCC XCA
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "09194";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }
                                //3
                                if (rdr2["PolicyType"].ToString().Trim() == "XPA")
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "04211";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }
                                else //SCC XCA
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "11212";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }
                                //4
                                if (rdr2["PolicyType"].ToString().Trim() == "XPA")
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "05211";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }
                                else //SCC XCA
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "12212";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    xmlPremium1.InnerText = "0";
                                }
                                //5
                                if (rdr2["PolicyType"].ToString().Trim() == "XPA")
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "06211";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = rdr["TotalPremium"].ToString().Trim();

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    if (rdr["TotalPremium"].ToString().Trim() == "200")
                                    {
                                        xmlPremium1.InnerText = "89";
                                    }
                                    else if (rdr["TotalPremium"].ToString().Trim() == "150")
                                    {
                                        xmlPremium1.InnerText = "95";
                                    }
                                    else if (rdr["TotalPremium"].ToString().Trim() == "100")
                                    {
                                        xmlPremium1.InnerText = "100";
                                    }
                                }

                                else if (rdr2["PolicyType"].ToString().Trim() == "XCA")
                                {
                                    XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                    xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                    XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                    xmlVehicleCvrg.AppendChild(xmlVin2);
                                    xmlVin2.InnerText = rdr["VIN"].ToString().Trim();


                                    XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                    xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                    xmlReinsAsl.InnerText = "13212";

                                    XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                    xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                    xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();


                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = rdr["TotalPremium"].ToString().Trim();

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0";

                                    XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                    xmlVehicleCvrg.AppendChild(xmlPremium1);
                                    if (rdr["TotalPremium"].ToString().Trim() == "200")
                                    {
                                        xmlPremium1.InnerText = "89";
                                    }
                                    else if (rdr["TotalPremium"].ToString().Trim() == "150")
                                    {
                                        xmlPremium1.InnerText = "95";
                                    }
                                    else if (rdr["TotalPremium"].ToString().Trim() == "100")
                                    {
                                        xmlPremium1.InnerText = "100";
                                    }
                                }
                            }
                        }


                        //XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                        //xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                        //xmlReinsAsl.InnerText = "09194";


                        //}
                        conn2.Close();



                        XmlElement xmlClientTable = xmlDoc.CreateElement("ClientTable");
                        xmlPolicy1.AppendChild(xmlClientTable);

                        XmlElement xmlClient0 = xmlDoc.CreateElement("Client");
                        xmlClientTable.AppendChild(xmlClient0);

                        XmlElement xmlClient2 = xmlDoc.CreateElement("Client");
                        xmlClient0.AppendChild(xmlClient2);
                        xmlClient2.InnerText = "0";


                        XmlElement xmlMaddr1 = xmlDoc.CreateElement("Maddr1");
                        xmlClient0.AppendChild(xmlMaddr1);
                        xmlMaddr1.InnerText = rdr["Adds1"].ToString().ToUpper().Trim();

                        XmlElement xmlMaddr2 = xmlDoc.CreateElement("Maddr2");
                        xmlClient0.AppendChild(xmlMaddr2);
                        xmlMaddr2.InnerText = rdr["Adds2"].ToString().ToUpper().Trim();

                        XmlElement xmlMaddr3 = xmlDoc.CreateElement("Maddr3");
                        xmlClient0.AppendChild(xmlMaddr3);
                        xmlMaddr3.InnerText = "";

                        XmlElement xmlMcity = xmlDoc.CreateElement("Mcity");
                        xmlClient0.AppendChild(xmlMcity);
                        xmlMcity.InnerText = rdr["City"].ToString().Trim();

                        XmlElement xmlMstate = xmlDoc.CreateElement("Mstate");
                        xmlClient0.AppendChild(xmlMstate);
                        xmlMstate.InnerText = rdr["State"].ToString().Trim();

                        XmlElement xmlMnation = xmlDoc.CreateElement("Mnation");
                        xmlClient0.AppendChild(xmlMnation);
                        xmlMnation.InnerText = "";

                        XmlElement xmlMzip = xmlDoc.CreateElement("Mzip");
                        xmlClient0.AppendChild(xmlMzip);
                        xmlMzip.InnerText = rdr["Zip"].ToString().Trim();



                        XmlElement xmlRaddr1 = xmlDoc.CreateElement("Raddr1");
                        xmlClient0.AppendChild(xmlRaddr1);
                        xmlRaddr1.InnerText = rdr["RAddrs1"].ToString().ToUpper().Trim();
                        //xmlRaddr1.InnerText = "8744 LINBERG BAY";

                        XmlElement xmlRaddr2 = xmlDoc.CreateElement("Raddr2");
                        xmlClient0.AppendChild(xmlRaddr2);
                        xmlRaddr2.InnerText = rdr["RAddrs2"].ToString().ToUpper().Trim();


                        XmlElement xmlRaddr3 = xmlDoc.CreateElement("Raddr3");
                        xmlClient0.AppendChild(xmlRaddr3);
                        xmlRaddr3.InnerText = "";

                        XmlElement xmlRcity = xmlDoc.CreateElement("Rcity");
                        xmlClient0.AppendChild(xmlRcity);
                        xmlRcity.InnerText = rdr["RCity"].ToString().ToUpper().Trim();
                        //xmlRcity.InnerText = "ST  THOMAS";

                        XmlElement xmlRstate = xmlDoc.CreateElement("Rstate");
                        xmlClient0.AppendChild(xmlRstate);
                        xmlRstate.InnerText = rdr["RState"].ToString().ToUpper().Trim();

                        //xmlRstate.InnerText = "VI";

                        XmlElement xmlRnation = xmlDoc.CreateElement("Rnation");
                        xmlClient0.AppendChild(xmlRnation);
                        xmlRnation.InnerText = "";

                        XmlElement xmlRzip = xmlDoc.CreateElement("Rzip");
                        xmlClient0.AppendChild(xmlRzip);
                        xmlRzip.InnerText = rdr["RZip"].ToString().Trim();
                        //xmlRzip.InnerText = "00802";

                        XmlElement xmlWphone = xmlDoc.CreateElement("Wphone");
                        xmlClient0.AppendChild(xmlWphone);
                        xmlWphone.InnerText = rdr["Jobph"].ToString().Trim().Replace("(", "").Replace(" ", "-").Replace(")", "").Trim();

                        XmlElement xmlRphone = xmlDoc.CreateElement("Rphone");
                        xmlClient0.AppendChild(xmlRphone);
                        xmlRphone.InnerText = rdr["Homeph"].ToString().Trim().Replace("(", "").Replace(" ", "-").Replace(")", "").Trim();

                        XmlElement xmlCsbyt = xmlDoc.CreateElement("Csbyt");
                        //XmlAttribute attribute18 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute14.Value = "true";
                        //xmlCsbyt.Attributes.Append(attribute18);
                        xmlClient0.AppendChild(xmlCsbyt);
                        xmlCsbyt.InnerText = "0";

                        XmlElement xmlCphone = xmlDoc.CreateElement("Cphone");
                        xmlClient0.AppendChild(xmlCphone);
                        xmlCphone.InnerText = rdr["Cellular"].ToString().Trim().Replace("(", "").Replace(" ", "-").Replace(")", "").Trim();
                        // xmlCphone.InnerText = "340-776-7798";

                        XmlElement xmlEaddr = xmlDoc.CreateElement("Eaddr");
                        xmlClient0.AppendChild(xmlEaddr);
                        xmlEaddr.InnerText = rdr["Email"].ToString().Trim();


                    }
            #endregion XML Policy Info

                    conn.Close();
                    // cierra las conecciones


                xmlDoc.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + NAMECONVENTION + ".xml"); // save

                string XMLFile = NAMECONVENTION + ".xml";

                string fileName = "XMLFile11.xsd";

                string fileName1 = NAMECONVENTION + ".XSD";
                string sourcePath = System.Configuration.ConfigurationManager.AppSettings["XMLPathName"];
                string targetPath = System.Configuration.ConfigurationManager.AppSettings["XMLPathName"];

                // Use Path class to manipulate file and directory paths.
                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName1);

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);

                string XMLdestFile = System.IO.Path.Combine(targetPath, XMLFile);

                System.IO.StreamReader XML = new System.IO.StreamReader(XMLdestFile);
                string xmlString = XML.ReadToEnd();
                xmlString = xmlString.Replace("\r\n", "");
                //string xmlString = System.IO.File.ReadAllText(XMLdestFile);

                conn4.Close();

                if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                {
                    XMLInsert(xmlString, TaskControlID);
                }
            }
            catch (Exception ecp)
            {
                throw new Exception(ecp.Message.ToString());
            }
        }

        public void XMLInsert(string xmlString, int TaskControlID)
        {
            try
            {
                EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];
                XmlDocument XmlDoc = new XmlDocument();

                System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();

                //cn.ConnectionString = @"Data Source=GIC-MSQL\PPSSQLSERVER;Initial Catalog=GICPPSDATA;User ID=URClaims;password=3G@TD@t!1";
                //Producción
                //cn.ConnectionString = @"Data Source=GIC-MSQL\PPSSQLSERVER;Initial Catalog=GICPPSDATA;User ID=URClaims;password=3G@TD@t!1";
                //Prueba

                cn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                //cn.ConnectionString = @"Data Source=GIC-MSQL\PPSSQLSERVER;Initial Catalog=AgentTestData;User ID=URClaims;password=3G@TD@t!1";


                cn.Open();

                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.LoadXml(xmlString);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "exec sproc_ConsumeXMLePPS @xmlIn, @xmlOut = @x output";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@x", SqlDbType.Xml).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@xmlIN", xmlString);

                int c = cmd.ExecuteNonQuery();
                string f = cmd.Parameters[0].Value.ToString();

                if (f.Trim() == "")
                {
                    cn.Close();
                    throw new Exception("Policy not found in PPS");
                }

                XmlDoc.LoadXml(f);
                cn.Close();

                bool FilledName = false, FilledAddress = false, FilledAsl = false;

                #region Xml

                XmlNodeList XmlBase = XmlDoc.GetElementsByTagName("Policy");

                foreach (XmlNode XmlPolicyBase in XmlBase)
                {
                    //CUSTOMER2 = XmlPolicyBase["PolicyID"].InnerText;
                    //CUSTOMER2 = CUSTOMER2 + XmlPolicyBase["Client"].InnerText;
                    PolicyNumber = XmlPolicyBase["PolicyID"].InnerText;
                    ClientID = XmlPolicyBase["Client"].InnerText;
                    
                    //CUSTOMER2 = CUSTOMER2.ToString() + XmlPolicyBase["Client"].InnerText;
                    UpdatePolicyFromPPSByTaskControlID(TaskControlID, PolicyNumber, ClientID);


                    //taskControl.CustomerNo = ClientID.ToString();
                    //taskControl.Customer.CustomerNo = ClientID.ToString();
                    PolicyNumber = PolicyNumber.ToString().Replace("XPA", "").Replace("XCA", "");
                    taskControl.PolicyNo = int.Parse(PolicyNumber).ToString("0000000");
                    Session["TaskControl"] = taskControl;
                    //Falta CustomerNo El id es unico en nuestra base de datos
                    
                    //txtPolicyNoVerification.Text = PolicyNumber;
                    //PolicyPrefix = PolicyNumber.Substring(0, 3);
                    //EffDate = DateTime.Parse(XmlPolicyBase["Incept"].InnerText).ToShortDateString();
                    //ExpDate = DateTime.Parse(XmlPolicyBase["Expire"].InnerText).ToShortDateString();

                    //if (XmlPolicyBase["CanDate"].InnerText != "")
                    //    CanDate = DateTime.Parse(XmlPolicyBase["CanDate"].InnerText).ToShortDateString();

                    if (XmlPolicyBase.HasChildNodes)
                    {
                        //XmlNode Child = XmlPolicyBase.FirstChild;
                        foreach (XmlNode Childs in XmlPolicyBase.ChildNodes)
                        {
                            if (Childs.Name == "PolRelTable")
                            {
                                foreach (XmlElement ChildsElement in Childs)
                                {
                                    if (ChildsElement.HasChildNodes)
                                    {
                                        foreach (XmlElement GrandChildElements in ChildsElement)
                                        {
                                            if (GrandChildElements.Name == "EntNamesTable")
                                            {
                                                foreach (XmlElement GreatGrandElements in GrandChildElements)
                                                {
                                                    if (!(FilledName))  //Solo leerá los campos una vez, tomando los primeros que lea
                                                    {
                                                        //txtName.Text = GreatGrandElements["FirstName"].InnerText +
                                                        //    ((GreatGrandElements["Middle"].InnerText != "") ? " " + GreatGrandElements["Middle"].InnerText : "") + " " +
                                                        //    GreatGrandElements["LastName"].InnerText;

                                                        //txtInsuredDOB.Text = DateTime.Parse(GreatGrandElements["Dob"].InnerText).ToShortDateString();
                                                        //txtInsuredLicense.Text = GreatGrandElements["License"].InnerText;
                                                        //txtState.Text = GreatGrandElements["State"].InnerText;

                                                        //InsuredName = txtName.Text;
                                                        //InsuredState = txtState.Text;
                                                        FilledName = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            else if (Childs.Name == "VehicleTable")
                            {
                                foreach (XmlElement ChildsElement in Childs)
                                {
                                    //Para validar
                                    //if ((ChildsElement["LicPlate"].InnerText == txtPlateVerification.Text.Trim().ToUpper()) ||
                                    //    (ChildsElement["Vin"].InnerText == txtVINVerification.Text.Trim().ToUpper()))
                                    {
                                        InsuredVin = ChildsElement["Vin"].InnerText;
                                        InsuredPlate = ChildsElement["LicPlate"].InnerText;

                                        //txtVIN.Text = txtVINVerification.Text.Trim();
                                        //txtPlate.Text = txtPlateVerification.Text.Trim().ToUpper();

                                        //InsuredVin = txtVINVerification.Text;
                                        //InsuredPlate = txtPlateVerification.Text;

                                        if (ChildsElement.HasChildNodes)
                                        {
                                            foreach (XmlElement GrandChilds in ChildsElement)
                                            {
                                                if (GrandChilds.Name == "PhysVehicleTable")
                                                {
                                                    foreach (XmlElement GrandChildsElements in GrandChilds)
                                                    {
                                                        ddlVehicleMake.SelectedIndex = ddlVehicleMake.Items.IndexOf(ddlVehicleMake.Items.FindByText((GrandChildsElements["Make"].InnerText.Trim()).ToString()));

                                                        if (int.Parse(ddlVehicleMake.SelectedItem.Value) > 0)
                                                            //FillModelDDListByPPS(ddlVehicleMake.SelectedItem.Value.ToString());

                                                            ddlVehicleModel.SelectedIndex = ddlVehicleModel.Items.IndexOf(ddlVehicleModel.Items.FindByText((GrandChildsElements["Model"].InnerText.TrimEnd()).ToString()));
                                                        ddlVehicleYear.SelectedIndex = ddlVehicleYear.Items.IndexOf(ddlVehicleYear.Items.FindByText((GrandChildsElements["MYear"].InnerText.TrimEnd()).ToString())); //GrandChildsElements["MYear"].InnerText;
                                                        ///ddlBroker.Items.IndexOf(ddlBroker.Items.FindByValue(int.Parse(XmlPolicyBase["BrokerID"].InnerText).ToString()));                                                                
                                                    }
                                                }
                                                else if (GrandChilds.Name == "VehicleCvrgTable")
                                                {
                                                    foreach (XmlElement GrandChildsElements in GrandChilds)
                                                    {
                                                        if (!(FilledAsl))  //Solo leerá los campos una vez, tomando los primeros que lea
                                                        {
                                                            ReinsAsl = GrandChildsElements["ReinsAsl"].InnerText.ToString();

                                                            //GetCoverage();

                                                            FilledAsl = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            else if (Childs.Name == "ClientTable")
                            {
                                foreach (XmlElement ChildsElement in Childs)
                                {
                                    //ClientID = XmlPolicyBase["Client"].InnerText;
                                    if (!(FilledAddress))   //Solo leerá los campos una vez, tomando los primeros que lea
                                    {
                                        //txtInsuredAddrs1.Text = ChildsElement["Maddr1"].InnerText;
                                        //txtAddrs2.Text = ChildsElement["Maddr2"].InnerText;
                                        ////ddlCityFullInfo.Text = ChildsElement["Mcity"].InnerText; //txtInsuredCity.Text =
                                        ////txtInsuredState.Text = ChildsElement["Mstate"].InnerText;
                                        //txtZipCode.Text = ChildsElement["Mzip"].InnerText;
                                        //txtWorkPhone.Text = ChildsElement["Wphone"].InnerText;
                                        //txtCellular.Text = ChildsElement["Cphone"].InnerText;

                                        //InsuredAddress1 = txtInsuredAddrs1.Text;
                                        //InsuredAddress2 = txtAddrs2.Text;
                                        //InsuredZip = txtZipCode.Text;
                                        //InsuredWorkPhone = txtWorkPhone.Text;
                                        //InsuredCellular = txtCellular.Text;

                                        FilledAddress = true;
                                    }
                                }
                            }
                        }
                    }
                    ///Todas las variables Insured mencionadas existen así para la ocasión en que llame el reclamante
                    ///La información del asegurado no se pierda, si no que siga hacia adelante en el proceso. 
                }

                #endregion

                XmlDoc.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + NAMECONVENTION + "PPS" + "XTRA" + ".xml");
            }

            catch (Exception exc)
            {
                LogError(exc);
                throw new Exception(exc.Message.ToString());
                //CUSTOMER2 = CUSTOMER2.ToString() + " HELLO "+ exc.Message + " " + exc;
                //EventLog.WriteEntry("XMLINSERT", exc.InnerException.ToString() + " " + exc.Message.ToString());
                
                //lblRecHeader.Text = exc.Message + " " + exc;
                //mpeSeleccion.Show();
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
                throw new Exception("Could not cook items.", ex);
            }

            return dt;

        }

        private void UpdateGuadianXtraHasAccident12(int TaskControlID, bool HasAccident12)
        {
            EncryptClass.EncryptClass encryp = new EncryptClass.EncryptClass();

            EPolicy.TaskControl.RoadAssistance taskControl = (EPolicy.TaskControl.RoadAssistance)Session["TaskControl"];

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControl.TaskControlID.ToString(), ref cookItems);//TaskControlID.ToString()
            DbRequestXmlCooker.AttachCookItem("HasAccident12", SqlDbType.Bit, 0, HasAccident12.ToString(), ref cookItems);//TaskControlID.ToString()

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
            exec.Insert("UpdateGuadianXtraHasAccident12", xmlDoc);
        }

        public static DataTable GetZipcodeByState(string State)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("State",
                SqlDbType.VarChar, 100, State.Trim(),
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

            DataTable dt = exec.GetQuery("GetZipcodeByState", xmlDoc);
            return dt;
        }

        private static DataTable GetGuadianXtraHasAccident12(int TaskControlID)
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
                dt = exec.GetQuery("GetGuadianXtraHasAccident12", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not Has Accidents.", ex);
            }
        }

        public void GridVehicle_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
           
        }

        private bool IsSTHOMAS()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp.UserID).ToString()).Contains("THOMAS"))
                return true;
            else
                return false;
        }

        // Filters Years to specific number (Example: 3 Years = "2018, 2017, 2016, 2015") - JNF
        private DataTable YearLimit(DataTable dtYears,int Years)
        {
            if (dtYears.Rows.Count > 0)
            {
                do
                {
                    dtYears.Rows.RemoveAt(dtYears.Rows.Count - 1);
                }
                while (Years + 1 != dtYears.Rows.Count);
            }
            return dtYears;
        }
    }
}