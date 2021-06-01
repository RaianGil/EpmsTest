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
namespace EPolicy
{
    /// <summary>
    /// Summary description for ExpressAutoQuote.
    /// </summary>
    public partial class ExpressAutoQuote : System.Web.UI.Page
    {
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void DdlDriverFill()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            DataTable DtDriver = BuildDriversTable();
            DataRow row;

            for (int i = 0; i < QA.Drivers.Count; i++)
            {
                AutoDriver AD = (AutoDriver)QA.Drivers[i];
                row = DtDriver.NewRow();

                row["DriverName"] = AD.FirstName.Trim() + " " + AD.LastName1.Trim() + " " + AD.LastName2.Trim();
                row["QuotesDriversID"] = AD.DriverID;

                DtDriver.Rows.Add(row);
            }
            ddlDriver.DataSource = DtDriver;
            ddlDriver.DataTextField = "DriverName";
            ddlDriver.DataValueField = "QuotesDriversID";
            ddlDriver.DataBind();
            ddlDriver.SelectedIndex = -1;
            ddlDriver.Items.Insert(0, "");
        }

        private DataTable BuildDriversTable()
        {
            DataSet ds = new DataSet("DSQuotesDrivers");
            DataTable dt = ds.Tables.Add("QuotesDrivers");

            dt.Columns.Add("QuotesDriversID", typeof(int));
            dt.Columns.Add("DriverName", typeof(string));

            return dt;
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary> 
        /// //this.Load += new System.EventHandler(this.Page_Load);
        ///   this.Load += new System.EventHandler(this.Page_Load);
        private void InitializeComponent()
        {

        }
        #endregion

        #region Global Variables

        //protected System.Web.UI.WebControls.TextBox Textbox1;
        protected System.Web.UI.WebControls.RadioButton rdoPARno;
        protected System.Web.UI.WebControls.RadioButton rdoPARyes;
        protected System.Web.UI.WebControls.RadioButton rdoSingle;
        protected System.Web.UI.WebControls.RadioButton rdoMarried;
        protected System.Web.UI.WebControls.Label Label4;
        //private TaskControl.QuoteAuto _currentQuoteAuto = null;
        //private Customer.Prospect _currentProspect = null;
        protected string today = DateTime.Now.ToString("MM/dd/yyyy");
        private int EndorsementID = 0;
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Page.Header.DataBind();
            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            //this.Placeholder1.Controls.Add(Banner);
            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (cp == null)
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                if (!cp.IsInRole("AUTO PERSONAL MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
                    Response.Redirect("Default.aspx?001");
                }
            }

            this.ddlBI.Attributes.Add("onchange", "SetDDLCSL()");
            this.ddlCSL.Attributes.Add("onchange", "SetDDLBI()");
            //this.ddlYear.Attributes.Add("onchange","getVehicleAge();CalculateOriginalCost();");
            this.txtBirthDt.Attributes.Add("onchange", "getAge()");
            this.rdo15percent.Attributes.Add("onclick", "SetDepreciation15();CalculateOriginalCost()");
            this.rdo20percent.Attributes.Add("onclick", "SetDepreciation20();CalculateOriginalCost()");
            this.ddlCollision.Attributes.Add("onchange", "SetCompCollValue()");
            this.txtTerm.Attributes.Add("onblur", "SetTermPolicy()");

            //this.ddlRental.Attributes.Add("onchange", "SetRentalValue()");

            this.txtCost.Attributes.Add("onchange", "SetActualValueFromCost()");
            this.txtCost.Attributes.Add("onblur", "SetActualValueFromCost()");
            this.ddlNewUsed.Attributes.Add("onchange", "SetActualValueFromCost()");
            this.txtActualValue.Attributes.Add("onblur", "SetActualValueFromCost");
            this.ddlYear.Attributes.Add("onchange", "SetActualValueFromCost()");
            this.txtEffDt.Attributes.Add("onblur", "getEffectiveDate()");
            this.TxtpurchaseDate.Attributes.Add("onblur", "SetActualValueFromCost()");

            //			this.txtAge.Attributes.Add("onblur", "CalculateOriginalCost()");

            //			this.ddlNewUsed.Attributes.Add("onchange","SetActualValueFromCost();CalculateOriginalCost()");
            //			this.txtActualValue.Attributes.Add("onblur", "SetActualValueFromCost(); CalculateOriginalCost()");
            //			this.txtAge.Attributes.Add("onblur", "CalculateOriginalCost()");

            //			this.ddlNewUsed.Attributes.Add("onchange", "SetActualValue()");
            //			this.txtCost.Attributes.Add("onblur", "SetActualValueFromCost();CalculateOriginalCost()");
            //			this.txtActualValue.Attributes.Add("onblur", "CalculateOriginalCost();");
            //			this.txtAge.Attributes.Add("onblur", "CalculateOriginalCost();");
            //

            //this.TxtVehiclesCount.Attributes.Add("onblur", "EnableControlInNewMode()");	
            //this.TxtInsCode.Attributes.Add("onblur", "SetInsCode()");	
            //this.ddlInsuranceCompany.Attributes.Add("onchange", "SetPolClass()");	
            //this.txtEffDt.Attributes.Add("onchange","getExpDt()");
            //this.txtTerm.Attributes.Add("onchange","getExpDt()");
		
           


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

            litPopUp.Visible = false;

            if (Session["AutoPostBack"] == null)
            {
                if (!IsPostBack)
                {
                    if (Session["LookUpTables"] == null)
                    {
                        this.BindDdls();

                        TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                        DataTable dtMaritalStatus = LookupTables.LookupTables.GetTable("MaritalStatus");
                        DataTable dtGender = LookupTables.LookupTables.GetTable("Gender");
                        DataTable dtLocation = LookupTables.LookupTables.GetTable("Location");
                        DataTable dtPolicy = LookupTables.LookupTables.GetTable("Location");
                        DataTable dtInsuranceCompany = LookupTables.LookupTables.GetTable("InsuranceCompany");

                        DataTable dtRoadAssist = LookupTables.LookupTables.GetTable("RoadAssist");
                        DataTable dtRoadAssistEmp = LookupTables.LookupTables.GetTable("RoadAssistEmp");
                        DataTable dtAccidentalDeath = LookupTables.LookupTables.GetTable("AccidentalDeath");
                        DataTable dtEquitmentAudio = LookupTables.LookupTables.GetTable("EquitmentAudio");
                        DataTable dtEquitmentSonido = LookupTables.LookupTables.GetTable("EquitmentSonido");
                        DataTable dtUninsuredSingle = LookupTables.LookupTables.GetTable("UninsuredSingle");
                        DataTable dtUninsuredSplit = LookupTables.LookupTables.GetTable("UninsuredSplit");
                        DataTable dtCSL = LookupTables.LookupTables.GetTable("CombinedSingleLimit");

                        DataTable dtVehicleRental;
                        if (Session["OptimaPersonalPackage"] != null)
                        {
                            dtVehicleRental = LookupTables.LookupTables.GetTable("VehicleRentalOPP");
                        }
                        else
                        {
                            dtVehicleRental = LookupTables.LookupTables.GetTable("VehicleRental");
                        }
                        //if (VerifyConvertPolicy(QA.TaskControlID))
                        //{
                        //    btnEdit.Visible=false;
                        //}

                        //RoadAssist
                        ddlRoadAssist.DataSource = dtRoadAssist;
                        ddlRoadAssist.DataTextField = "RoadAssistDesc";
                        ddlRoadAssist.DataValueField = "RoadAssistID";
                        ddlRoadAssist.DataBind();
                        foreach (ListItem t in ddlRoadAssist.Items)
                            t.Text = UppercaseFirst(t.Text);

                        ddlRoadAssist.SelectedIndex = 0;
                        //ddlRoadAssist.Items.Insert(0, "");

                        //RoadAssistEmp
                        ddlRoadAssistEmp.DataSource = dtRoadAssistEmp;
                        ddlRoadAssistEmp.DataTextField = "RoadAssistEmpDesc";
                        ddlRoadAssistEmp.DataValueField = "RoadAssistEmpID";
                        ddlRoadAssistEmp.DataBind();
                        foreach (ListItem t in ddlRoadAssist.Items)
                            t.Text = UppercaseFirst(t.Text);

                        ddlRoadAssistEmp.SelectedIndex = 0;
                        // ddlRoadAssistEmp.Items.Insert(0, "");

                        //AccidentalDeath
                        ddlAccidentDeath.DataSource = dtAccidentalDeath;
                        ddlAccidentDeath.DataTextField = "AccidentalDeathDesc";
                        ddlAccidentDeath.DataValueField = "AccidentalDeathID";
                        ddlAccidentDeath.DataBind();
                        ddlAccidentDeath.SelectedIndex = -1;
                        //ddlAccidentDeath.Items.Insert(0, "");

                        //EquitmentAudio
                        ddlEquitmentAudio.DataSource = dtEquitmentAudio;
                        ddlEquitmentAudio.DataTextField = "EquitmentAudioDesc";
                        ddlEquitmentAudio.DataValueField = "EquitmentAudioID";
                        ddlEquitmentAudio.DataBind();
                        ddlEquitmentAudio.SelectedIndex = -1;
                        //ddlEquitmentAudio.Items.Insert(0, "");

                        //EquitmentSonido
                        ddlEquitmentSonido.DataSource = dtEquitmentSonido;
                        ddlEquitmentSonido.DataTextField = "EquitmentSonidoDesc";
                        ddlEquitmentSonido.DataValueField = "EquitmentSonidoID";
                        ddlEquitmentSonido.DataBind();
                        ddlEquitmentSonido.SelectedIndex = -1;
                        //ddlEquitmentSonido.Items.Insert(0, "");

                        //UninsuredSingle
                        ddlUninsuredSingle.DataSource = dtUninsuredSingle;
                        ddlUninsuredSingle.DataTextField = "UninsuredSingleDesc";
                        ddlUninsuredSingle.DataValueField = "UninsuredSingleID";
                        ddlUninsuredSingle.DataBind();
                        ddlUninsuredSingle.SelectedIndex = -1;
                        //ddlUninsuredSingle.Items.Insert(0, "");

                        //UninsuredSplit
                        ddlUninsuredSplit.DataSource = dtUninsuredSplit;
                        ddlUninsuredSplit.DataTextField = "UninsuredSplitDesc";
                        ddlUninsuredSplit.DataValueField = "UninsuredSplitID";
                        ddlUninsuredSplit.DataBind();
                        ddlUninsuredSplit.SelectedIndex = -1;
                        //ddlUninsuredSplit.Items.Insert(0, "");

                        //VehicleRental
                        ddlRental.DataSource = dtVehicleRental;
                        ddlRental.DataTextField = "VehicleRentalDesc";
                        ddlRental.DataValueField = "VehicleRentalID";
                        ddlRental.DataBind();
                        ddlRental.SelectedIndex = 0;
                        //ddlRental.Items.Insert(0, "");

                        ////CSL
                        //ddlCSL.DataSource = dtCSL;
                        //ddlCSL.DataTextField = "combinedSingleLimitDesc";
                        //ddlCSL.DataValueField = "combinedSingleLimitID";
                        //ddlCSL.DataBind();
                        //ddlCSL.SelectedIndex = 0;
                        ////ddlCSL.Items.Insert(0, "");

                        //MaritalStatus
                        ddlMaritalSt.DataSource = dtMaritalStatus;
                        ddlMaritalSt.DataTextField = "MaritalStatusDesc";
                        ddlMaritalSt.DataValueField = "MaritalStatusID";
                        ddlMaritalSt.DataBind();
                        foreach (ListItem t in ddlMaritalSt.Items)
                            t.Text = UppercaseFirst(t.Text);
                        ddlMaritalSt.SelectedIndex = -1;
                        ddlMaritalSt.Items.Insert(0, "");

                        //Gender
                        ddlGender.DataSource = dtGender;
                        ddlGender.DataTextField = "GenderDesc";
                        ddlGender.DataValueField = "GenderID";
                        ddlGender.DataBind();
                        foreach (ListItem t in ddlGender.Items)
                            t.Text = UppercaseFirst(t.Text);
                        ddlGender.SelectedIndex = -1;
                        ddlGender.Items.Insert(0, "");

                        //Location
                        ddlLocation.DataSource = dtLocation;
                        ddlLocation.DataTextField = "locationDesc";
                        ddlLocation.DataValueField = "locationID";
                        ddlLocation.DataBind();
                        ddlLocation.SelectedIndex = -1;
                        ddlLocation.Items.Insert(0, "");

                        //InsuranceCompany
                        ddlInsuranceCompany.DataSource = dtInsuranceCompany;
                        ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
                        ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
                        ddlInsuranceCompany.DataBind();
                        ddlInsuranceCompany.SelectedIndex = -1;
                        ddlInsuranceCompany.Items.Insert(0, "");

                        DataTable dtBank = LookupTables.LookupTables.GetTable("Bank");
                        DataTable dtCompanyDealer = LookupTables.LookupTables.GetTable("CompanyDealer");
                        //Bank
                        ddlBank.DataSource = dtBank;
                        ddlBank.DataTextField = "BankDesc";
                        ddlBank.DataValueField = "BankID";
                        ddlBank.DataBind();
                        ddlBank.SelectedIndex = -1;
                        ddlBank.Items.Insert(0, "");

                        //CompanyDealer
                        ddlCompanyDealer.DataSource = dtCompanyDealer;
                        ddlCompanyDealer.DataTextField = "CompanyDealerDesc";
                        ddlCompanyDealer.DataValueField = "CompanyDealerID";
                        ddlCompanyDealer.DataBind();
                        ddlCompanyDealer.SelectedIndex = -1;
                        ddlCompanyDealer.Items.Insert(0, "");

                        //Drivers
                        DdlDriverFill();

                        //VehicleModel
                        FillModel();

                        //PolicySubClass
                        FillPolicySubClass();

                     //   fillDataGrid(QA.AutoCovers);

                        Session.Add("LookUpTables", "In");
                    }
                    if (Session["TaskControl"] != null)
                    {
                        TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                        FillDDLDiscount();

                        switch (QA.Mode)
                        {
                            case 1:  //ADD
                                DisableControls();
                                FillTextControl();
                                DefaultValue();

                                //								btnNew.Visible		= false;
                                //								btnEdit.Visible		= false;
                                //								BtnExit.Visible		= false;
                                //								btnSave.Visible		= true;
                                //								btnCancel.Visible	= true;
                                //								HplAdd.Enabled		= true;
                                //						  		Linkbutton1.Enabled = true;
                                //								btnVehicles.Visible = false;
                                //								btnDrivers.Visible  = false;
                                //								btnPrint.Visible	= false;
                                //
                                //								txtExpDt.Visible	 = false;
                                //								txtPurchaseDt.Visible = false;

                                EnableControls();
                                ddlPolicySubClass.Enabled = false;
                                ddlDriver.Enabled = false;
                                break;

                            case 2: //Update
                                FillTextControl();
                                EnableControls();
                                break;

                            default:
                                //this.SetInternalID();
                                FillTextControl();
                                DisableControls();
                                break;
                        }

                        //this.Calculate(true);
                    }
                }
                else
                {
                    if (Session["TaskControl"] != null)
                    {
                        TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                        if (QA.Mode == 4)
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
			
			 if (!IsPostBack)
            {
                if (Request.QueryString["FromRenewPolicy"] != null)
                {
                    TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
                    if (taskControl.IsPolicy == false)
                    {
                        AutoCover AC3;
                        if (taskControl.AutoCovers.Count != 0)
                        {
                            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            {
                                AC3 = null;
                                AC3 = (AutoCover)taskControl.AutoCovers[0];
                                FillTextAC(AC3);
                                SaveAllQuote();
                            }
                        }

                        ModifyQuotes();
                        SaveAllQuote();
                        FillTextControl();
                        DisableControls();

                        fillDataGrid(taskControl.AutoCovers);
                        lblRecHeader.Text = "Quote saved Successfully!";
                        mpeSeleccion.Show();
                    }
                }
            }      
        }

        private void FillDDLDiscount()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            int disc = 0;

            if (cp.IsInRole("AUTO PERSONAL DISCOUNT 10"))
                disc = 10;

            if (cp.IsInRole("AUTO PERSONAL DISCOUNT 20"))
                disc = 20;

            if (cp.IsInRole("AUTO PERSONAL DISCOUNT 30"))
                disc = 30;

            if (cp.IsInRole("AUTO PERSONAL DISCOUNT 40"))
                disc = 40;

            if (cp.IsInRole("AUTO PERSONAL DISCOUNT 50") || cp.IsInRole("ADMINISTRATOR"))
                disc = 50;


            for (int i = 1; i <= disc; i++)
            {
                ddlExperienceDiscount.Items.Add("-" + i.ToString());
            }
            ddlExperienceDiscount.SelectedIndex = -1;
            ddlExperienceDiscount.Items.Insert(0, "0");
        }

        private void SetInternalID() // No esta en uso.
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            //AutoCover AC = (AutoCover) QA.AutoCovers[0];

            //DataTable dt = GetQuotesAutoForQuote(QuotesID, ispolicy);
            //ArrayList AL = new ArrayList(QA.AutoCovers.Count);
            for (int i = 0; i < QA.AutoCovers.Count; i++)
            {
                AutoCover AC = (AutoCover)QA.AutoCovers[i];
                AC.InternalID = 1 + i;

                QA.RemoveAutoCover(AC);
                QA.AutoCovers.Add(AC);
            }
            Session["TaskControl"] = QA;
        }

        private void DefaultValue()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            _QuoteAutoID.Value = "0";
            _InternalID.Value = "0";

            TxtVehiclesCount.Text = "1";
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(Login.Login.GetLocationByUserID(userID).ToString()));

            //this.ChkPrincipalOperator.Checked = false;
            this.txtEntryDt.Text = DateTime.Today.ToShortDateString();

            if (Session["OptimaPersonalPackage"] != null)
            {
                cp = HttpContext.Current.User as Login.Login;
                if (cp.IsInRole("UNDERWRITTERRULESOPP"))
                {
                    DataTable dt = EPolicy.TaskControl.Policy.GetUnderwritterRulesByUnderwritterRulesID(1);

                    txtDiscountCollComp.Text = dt.Rows[0]["AutoDisc1"].ToString().Trim();
                    txtDiscountBIPD.Text = dt.Rows[0]["AutoDisc1"].ToString().Trim();
                }
                else
                {
                    txtDiscountCollComp.Text = "0";
                    txtDiscountBIPD.Text = "0";
                }

                this.txtDeprec1st.Value = "15";
                this.txtDeprecAll.Value = "15";
                this.rdo15percent.Checked = true;
                this.rdo20percent.Checked = false;

                ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(
                   ddlTowing.Items.FindByValue("0"));
            }
            else
            {
                //ddlInsuranceCompany.Items.IndexOf(ddlInsuranceCompany.Items.FindByValue("001"));
                //TxtInsCode.Text = "001";
                if (QA.IsPolicy == false && Session["AUTOEndorsement"] != null)
                {
                    //this.txtEffDt.Text = DateTime.Today.ToShortDateString();
                    //this.txtTerm.Text = "";
                }
                else
                {
                    this.txtEffDt.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(DateTime.Today.ToShortDateString()));
                    this.txtTerm.Text = "12";
                }

                this.txtDeprec1st.Value = "15";
                this.txtDeprecAll.Value = "15";
                this.rdo15percent.Checked = true;
                this.rdo20percent.Checked = false;

                txtDiscountCollComp.Text = "0";
                txtDiscountBIPD.Text = "0";

                this.TxtpurchaseDate.Text = "5/1/" + DateTime.Now.Year.ToString();
            }

            txtRoadsideAssitance.Text = "0";

            if (this.txtEffDt.Text.Trim() != string.Empty && this.txtTerm.Text.Trim() != string.Empty)
                this.txtExpDt.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(this.txtEffDt.Text).AddMonths(int.Parse(this.txtTerm.Text.Trim())).ToShortDateString());

        }

        private int CalcAge(string birthDT)
        {
            DateTime pdt = DateTime.Parse(birthDT);
            DateTime now = DateTime.Now;
            TimeSpan ts = now - pdt;
            int Years = (int)(((float)ts.Days) / 365.25f);
            return Years;
        }

        private void FillDriver()
        {
            
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            

            AutoDriver AD;
            if (Session["OptimaPersonalPackage"] != null && QA.Mode == 1)  // Si viene desde OptimaPersonalPackage.
            {
                EPolicy.TaskControl.PersonalPackage op = (EPolicy.TaskControl.PersonalPackage)Session["OptimaPersonalPackage"];

                txtFirstNm.Text = op.Prospect.FirstName;
                txtLastNm1.Text = op.Prospect.LastName1;
                txtLastNm2.Text = op.Prospect.LastName2;
                txtDriverPhone.Text = op.Prospect.HomePhone.Trim();
                txtDriverEmail.Text = op.Prospect.Email.Trim();
            }

            if (Session["Prospect"] != null)  // Si viene desde prospecto.
            {
                Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];

                txtFirstNm.Text = prospect.FirstName;
                txtLastNm1.Text = prospect.LastName1;
                txtLastNm2.Text = prospect.LastName2;
                txtDriverPhone.Text = prospect.HomePhone.Trim();
                txtDriverEmail.Text = prospect.Email.Trim();

                if (prospect.CustomerNumber != "None")
                {
                    Customer.Customer cust = Customer.Customer.GetCustomer(prospect.CustomerNumber, 1);
                   //txtBirthDt.Text = cust.Birthday;
                   //txtBirthDt.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(cust.Birthday).ToShortDateString());
                    txtBirthDt.Text = String.Format("{0:MM/dd/yyyy}", cust.Birthday.ToString());

                    
                    txtSocialSecurity.Text = cust.SocialSecurity;
                    txtDriverEmail.Text = cust.Email;
                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(cust.LocationID.ToString()));
                    ddlMaritalSt.SelectedIndex = ddlMaritalSt.Items.IndexOf(ddlMaritalSt.Items.FindByValue(cust.MaritalStatus.ToString()));

                    int mSex = 0;
                    if (cust.Sex != "")
                    {
                        if (cust.Sex == "M")
                        {
                            mSex = 1;
                        }
                        else
                        {
                            mSex = 2;
                        }
                    }
                    else
                    {
                        mSex = 0;
                    }
                    ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(mSex.ToString()));

                }

                Session.Remove("Prospect");   //Se elimina para que no vuelva a cargar esta info.
            }

            for (int i = 0; i < QA.Drivers.Count; i++)
            {
                AD = (AutoDriver)QA.Drivers[i];

                if (QA.Prospect.ProspectID == AD.ProspectID)
                {
                    txtFirstNm.Text = AD.FirstName;
                    txtLastNm1.Text = AD.LastName1;
                    txtLastNm2.Text = AD.LastName2;
                    //txtBirthDt.Text = AD.BirthDate;
                    //txtBirthDt.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AD.BirthDate).ToShortDateString());
                    txtBirthDt.Text = DateTime.Parse(AD.BirthDate).ToString("MM/dd/yyyy");
                   

                    txtSocialSecurity.Text = AD.SocialSecurity;
                    ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(AD.Gender.ToString()));
                    ddlMaritalSt.SelectedIndex = ddlMaritalSt.Items.IndexOf(ddlMaritalSt.Items.FindByValue(AD.MaritalStatus.ToString()));
                    txtDriverAge.Text = CalcAge(AD.BirthDate).ToString();
                    txtDriverPhone.Text = AD.HomePhone.Trim();
                    txtDriverEmail.Text = AD.Email.Trim();

                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(AD.LocationID.ToString()));
                }
            }
        }

        private void FillAutoCover(TaskControl.QuoteAuto QA)
        {
            AutoCover AC;

            if (QA.AutoCovers.Count != 0)
            {
                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {
                    AC = (AutoCover)QA.AutoCovers[0];  // Siempre es cero para cargar solamente el primer vehiculo.

                    FillTextAC(AC);
                }
            }
            else
            {
                _QuoteAutoID.Value = "0";
            }
        }

        private void FillTextAC(AutoCover AC)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            //FillPolicySubClass();
            ddlPolicySubClass.SelectedIndex = ddlPolicySubClass.Items.IndexOf(ddlPolicySubClass.Items.FindByValue(
                AC.PolicySubClassId.ToString()));

            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(
                AC.VehicleYear.ToString()));
            ddlMake.SelectedIndex = ddlMake.Items.IndexOf(ddlMake.Items.FindByValue(
                AC.VehicleMake.ToString()));
            FillModel();
            ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByValue
                (AC.VehicleModel.ToString()));
            txtAge.Text = AC.VehicleAge.ToString();
            ddlNewUsed.SelectedIndex = ddlNewUsed.Items.IndexOf(ddlNewUsed.Items.FindByValue(
                AC.NewUse.ToString()));

            txtVIN.Text = AC.VIN;
            txtVIN2.Text = AC.VIN;
            txtPlate.Text = AC.Plate;
            TxtpurchaseDate.Text = AC.PurchaseDate;
            txtLicenseNumber.Text = AC.License;
            //txtExpDate.Text = AC.LicenseExpDate;
            if (txtExpDate.Text!="")
            txtExpDate.Text = Convert.ToDateTime(AC.LicenseExpDate).ToString("MM/dd/yyyy"); 

            try
            {
                ddlCompanyDealer.SelectedIndex =
                        ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(
                        AC.CompanyDealer.ToString()));
            }
            catch //NAT! 
            { }
            try
            {
                ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(AC.Bank.ToString()));
            }
            catch //NAT! 
            { }

            if (QA.Mode == 2 || QA.Mode == 1)
            {
                if (AC.Cost.ToString().Trim() != "")
                    txtCost.Text = AC.Cost.ToString("######");         //("###,###")
                if (AC.ActualValue.ToString().Trim() != "")
                    txtActualValue.Text = AC.ActualValue.ToString("######");   //("###,###")
            }
            else
            {
                txtCost.Text = AC.Cost.ToString("###,###");
                txtActualValue.Text = AC.ActualValue.ToString("###,###");
            }

            TxtpurchaseDate.Text = AC.PurchaseDate;

            AutoCover AC2 = new AutoCover();
            AC2.QuotesAutoId = AC.QuotesAutoId;
            AC2.InternalID = AC.InternalID;
            AC2 = QA.GetAutoCover(AC2);
            bool IsOneVehicle=false;

            if (QA.AutoCovers.Count == 1)
            {
                IsOneVehicle = true;
            }

            if (AC.QuotesAutoId.ToString().Trim() == "")
            {
                _QuoteAutoID.Value = "0";
            }
            else
            {
                _QuoteAutoID.Value = AC.QuotesAutoId.ToString().Trim();
            }
            _InternalID.Value = AC.InternalID.ToString().Trim();

            ArrayList Assigned = AC2.AssignedDrivers;
            if (Assigned != null && Assigned.Count > 0)
            {
                for (int i = 0; i < Assigned.Count; i++)
                {
                    Quotes.AssignedDriver AD = (Quotes.AssignedDriver)Assigned[i];
                    Quotes.AutoDriver Driver = AD.AutoDriver;

                    ddlDriver.SelectedIndex =
                        ddlDriver.Items.IndexOf(ddlDriver.Items.FindByValue(
                        Driver.DriverID.ToString()));


                    if (IsOneVehicle==true )
                    {
                        string Tempdriver=ddlDriver.SelectedItem.Text;
                        //comparar el driver viejo con el nuevo;
                        //
                        SetPrimaryDriver(QA.QuoteId);

                        if (Tempdriver != ddlDriver.SelectedItem.Text)
                        {
                            AssignedDriver(QA, AC);
                        }
                    }

                    //this.chkOnlyOperator.Checked = AD.OnlyOperator;
                    //this.ChkPrincipalOperator.Checked = AD.PrincipalOperator;

                    if (AD.OnlyOperator)
                    {
                        rdoOnlyOperatorY.Checked = true;
                        rdoOnlyOperatorN.Checked = false;
                    }
                    else
                    {
                        rdoOnlyOperatorY.Checked = false;
                        rdoOnlyOperatorN.Checked = true;
                    }

                    if (AD.PrincipalOperator)
                    {
                        rdoPrincipalOperatorY.Checked = true;
                        rdoPrincipalOperatorN.Checked = false;
                    }
                    else
                    {
                        rdoPrincipalOperatorY.Checked = false;
                        rdoPrincipalOperatorN.Checked = true;
                    }
                }
            }

            bool dirty = false;

            foreach (AssignedDriver assignedDriver in AC.AssignedDrivers)
            {
                dirty = false;
                if (assignedDriver.PrincipalOperator)
                {
                    //					this.lblPrimaryDriver.Text =
                    //						assignedDriver.AutoDriver.FirstName.Trim() + 
                    //						" " + assignedDriver.AutoDriver.LastName1.Trim() +
                    //						" " + assignedDriver.AutoDriver.LastName2.Trim();
                    dirty = true;
                }
            }

            if (!dirty)
            {

            }

            //this.lblPrimaryDriver.Text = "Not assigned";

            string ac_vehicleClass = string.Empty;
            ac_vehicleClass = AC.VehicleClass.Length != 2 ?
                AC.VehicleClass + " " :
                AC.VehicleClass;

            if (AC.VehicleClass != "")
                ddlVehicleClass.SelectedIndex =
                    ddlVehicleClass.Items.IndexOf(
                    ddlVehicleClass.Items.FindByValue(ac_vehicleClass));

            if (AC.Territory != 0)
                ddlTerritory.SelectedIndex =
                    ddlTerritory.Items.IndexOf(
                    ddlTerritory.Items.FindByValue(
                    AC.Territory.ToString()));

            ddlAlarm.SelectedIndex = ddlAlarm.Items.IndexOf(ddlAlarm.Items.FindByValue(AC.AlarmType.ToString()));
            txtDeprec1st.Value = AC.Depreciation1stYear.ToString();
            txtDeprecAll.Value = AC.DepreciationAllYear.ToString();

            if (this.txtDeprec1st.Value == "15")
            {
                this.rdo15percent.Checked = true;
                this.rdo20percent.Checked = false;
            }
            else
            {
                this.rdo20percent.Checked = true;
                this.rdo15percent.Checked = false;
            }

            if (AC.MedicalLimit != 0)
                ddlMedical.SelectedIndex = ddlMedical.Items.IndexOf(ddlMedical.Items.FindByValue(
                    AC.MedicalLimit.ToString()));


            //TxtTowing.Text = AC.TowingPremium.ToString("###,###");
            if (txtTerm.Text.ToString().Trim() != "")
            {
                decimal Term = int.Parse(this.txtTerm.Text.ToString().Trim());
                decimal Towing = Decimal.Round(Convert.ToDecimal((AC.OriginalTowingPremium * Math.Round((Term / 12), 0))), 4); 
                TxtTowing.Text = Towing.ToString("###.#####");
                
            }
            else
            {
                TxtTowing.Text = AC.TowingPremium.ToString("####.#####").Trim();
                
            }

            if (AC.TowingPremium != 0)
                ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(ddlTowing.Items.FindByValue(AC.TowingID.ToString()));

            if (AC.LeaseLoanGapId != 0 && AC.LeaseLoanGapId != 1)
            {
                ddlLoanGap.SelectedIndex = ddlLoanGap.Items.IndexOf(ddlLoanGap.Items.FindByValue(AC.LeaseLoanGapId.ToString()));
                chkLLG.Checked = true;
            }
            else
                chkLLG.Checked = false;

            if (AC.SeatBelt != 0)
                ddlSeatBelt.SelectedIndex = ddlSeatBelt.Items.IndexOf(ddlSeatBelt.Items.FindByValue(
                    AC.SeatBelt.ToString()));
            if (AC.PersonalAccidentRider != 0)
                ddlPAR.SelectedIndex = ddlPAR.Items.IndexOf(ddlPAR.Items.FindByValue(
                    AC.PersonalAccidentRider.ToString()));
            if (AC.CollisionDeductible != 0)
                ddlCollision.SelectedIndex = ddlCollision.Items.IndexOf(ddlCollision.Items.FindByValue(
                    AC.CollisionDeductible.ToString()));
            if (AC.ComprehensiveDeductible != 0)
                ddlComprehensive.SelectedIndex = ddlComprehensive.Items.IndexOf(ddlComprehensive.Items.FindByValue(
                    AC.ComprehensiveDeductible.ToString()));
            txtDiscountCollComp.Text = AC.DiscountCompColl.ToString();
            if (AC.BodilyInjuryLimit != 0)
                ddlBI.SelectedIndex = ddlBI.Items.IndexOf(ddlBI.Items.FindByValue(
                    AC.BodilyInjuryLimit.ToString()));
            if (AC.PropertyDamageLimit != 0)
                ddlPD.SelectedIndex = ddlPD.Items.IndexOf(ddlPD.Items.FindByValue(
                    AC.PropertyDamageLimit.ToString()));
            if (AC.CombinedSingleLimit != 0)
                ddlCSL.SelectedIndex = ddlCSL.Items.IndexOf(ddlCSL.Items.FindByValue(
                    AC.CombinedSingleLimit.ToString()));
            txtDiscountBIPD.Text = AC.DiscountBIPD.ToString();

            if (AC.ExperienceDiscount != 0 )
            {
                //ddlExperienceDiscount.SelectedIndex = -1; // ddlExperienceDiscount.Items.IndexOf(ddlExperienceDiscount.Items.FindByValue(AC.ExperienceDiscount.ToString()));
                TxtExpDisc.Text = AC.ExperienceDiscount.ToString();
               // TxtExpDisc.Text = ddlExperienceDiscount.SelectedItem.Text.Trim();
            }
            else
            {
                TxtExpDisc.Text = "0";
            }

            if (AC.EmployeeDiscount != 0)
                ddlEmployeeDiscount.SelectedIndex = ddlEmployeeDiscount.Items.IndexOf(ddlEmployeeDiscount.Items.FindByValue(
                   AC.EmployeeDiscount.ToString()));

            txtMiscDiscount.Text = AC.MiscDiscount.ToString("00.00");

            //////////
            TxtVehicleRental.Text = AC.VehicleRental.ToString("###,###");
            if (AC.VehicleRental != 0)
                ddlRental.SelectedIndex = ddlRental.Items.IndexOf(ddlRental.Items.FindByValue(AC.VehicleRentalID.ToString()));

            //Quitar la opcion Road Assistance Para double interest
            if (!(AC.Term > 12))
            {
                if (AC.IsAssistanceEmp)
                {
                    if (AC.AssistancePremium != 0)
                    {
                        chkAssistEmp.Checked = true;
                        chkAssist.Checked = false;
                        ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue(AC.AssistanceID.ToString()));
                    }
                }
                else
                {
                    if (AC.AssistancePremium != 0)
                    {
                        chkAssistEmp.Checked = false;
                        chkAssist.Checked = true;
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue(AC.AssistanceID.ToString()));
                    }
                }
            }

            TxtAccidentDeathPremium.Text = AC.AccidentalDeathPremium.ToString("###,###");
            if (AC.AccidentalDeathPremium != 0)
                ddlAccidentDeath.SelectedIndex = ddlAccidentDeath.Items.IndexOf(ddlAccidentDeath.Items.FindByValue(AC.AccidentalDeathID.ToString()));

            ddlADPersons.SelectedIndex = ddlADPersons.Items.IndexOf(ddlADPersons.Items.FindByValue(AC.AccidentalDeathPerson.ToString()));

            TxtEquitmentSonido.Text = AC.EquipmentSoundPremium.ToString("###,###");
            if (AC.EquipmentSoundPremium != 0)
                ddlEquitmentSonido.SelectedIndex = ddlEquitmentSonido.Items.IndexOf(ddlEquitmentSonido.Items.FindByValue(AC.EquipmentSoundID.ToString()));

            TxtEquitmentAudio.Text = AC.EquipmentAudioPremium.ToString("###,###");
            if (AC.EquipmentAudioPremium != 0)
                ddlEquitmentAudio.SelectedIndex = ddlEquitmentAudio.Items.IndexOf(ddlEquitmentAudio.Items.FindByValue(AC.EquipmentAudioID.ToString()));

            TxtEquitmentTapes.Text = AC.EquipmentTapesPremium.ToString("###,###");
            chkEquipTapes.Checked = AC.EquipmentTapes;

            TxtEquipColl.Text = AC.SpecialEquipmentCollPremium.ToString("###,###");
            chkEquipColl.Checked = AC.SpecialEquipmentColl;
            TxtEquipComp.Text = AC.SpecialEquipmentCompPremium.ToString("###,###");
            chkEquipComp.Checked = AC.SpecialEquipmentComp;
            TxtEquipTotal.Text = (AC.SpecialEquipmentCollPremium + AC.SpecialEquipmentCompPremium).ToString("###,###");
            TxtCustomizeEquipLimit.Text = AC.CustomizeEquipLimit.ToString("###,###");

            TxtUninsuredSingle.Text = AC.UninsuredSinglePremium.ToString("###,###");
            if (AC.UninsuredSinglePremium != 0)
                ddlUninsuredSingle.SelectedIndex = ddlUninsuredSingle.Items.IndexOf(ddlUninsuredSingle.Items.FindByValue(AC.UninsuredSingleID.ToString()));

            TxtUninsuredSplit.Text = AC.UninsuredSplitPremium.ToString("###,###");
            if (AC.UninsuredSplitPremium != 0)
                ddlUninsuredSplit.SelectedIndex = ddlUninsuredSplit.Items.IndexOf(ddlUninsuredSplit.Items.FindByValue(AC.UninsuredSplitID.ToString()));

            txtIsAssistanceEmp.Text = AC.IsAssistanceEmp.ToString().Trim();

            txtLoJackCertificate.Text = AC.LoJackCertificate;
            TxtLojackExpDate.Text = AC.LojackExpDate;
            chkLoJack.Checked = AC.LoJack;

            this.txt1stISO0.Text = this.Get1stPeriodISOCode(AC);
        }
        private int CalculatePeriodAmounts()
        {
            // If Remainder
            int qterm = int.Parse(txtTerm.Text.Trim());
            int Remainder = 0;
            if ((qterm % 12) > 0)
            {
                Remainder = (qterm % 12);    // Get Remainder
                qterm += (12 - (int)Remainder); // Complete to Full Years (in Months)

            }

            return qterm / 12;  // Calculate Years

        } // End: CalculatePeriodAmounts()
        private void FillTextControl()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (Session["Prospect"] != null)  // Si viene desde prospecto.
            {
                Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];

                QA.Prospect.ProspectID = prospect.ProspectID;
                QA.ProspectID = prospect.ProspectID;
            }

            if (QA.ProspectID != 0)
                LblProspectID.Text = QA.ProspectID.ToString();
            else
                LblProspectID.Text = "";

           
            FillDriver();


            if (QA.EffectiveDate.Trim() != "")
                txtEffDt.Text = Convert.ToDateTime(QA.EffectiveDate.Trim()).ToString("MM/dd/yyyy"); 

            if (QA.ExpirationDate.Trim()!="")
                TextBox1.Text = Convert.ToDateTime(QA.ExpirationDate.Trim()).ToString("MM/dd/yyyy");

            txtTerm.Text = QA.Term.ToString();
            txtEntryDt.Text = QA.EntryDate.ToShortDateString();

            FillAutoCover(QA);

            this.TxtVehiclesCount.Text = QA.VehicleCount.ToString();

            if (QA.InsuranceCompany == "000")
                TxtInsCode.Text = "";
            else
                TxtInsCode.Text = QA.InsuranceCompany;

            if (QA.InsuranceCompany != "0")
                ddlInsuranceCompany.SelectedIndex = ddlInsuranceCompany.Items.IndexOf(
                    ddlInsuranceCompany.Items.FindByValue(QA.InsuranceCompany.ToString()));



            ShowTotals(QA);

            lblTaskControlID.Text = "Control ID: " + QA.TaskControlID.ToString().Trim();
            lblTaskControlID.Visible = true;

            if (int.Parse(this.txtTerm.Text) > 12)
            {
                this.ddlBI.Enabled = false;
                this.ddlPD.Enabled = false;
                this.ddlCSL.Enabled = false;
                this.txtDiscountBIPD.Enabled = false;
            }
            else
            {
                this.ddlBI.Enabled = true;
                this.ddlPD.Enabled = true;
                this.ddlCSL.Enabled = true;
                this.txtDiscountBIPD.Enabled = true;
            }

            Session["TaskControl"] = QA;
        }

        private void AddMainDriver()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                try
                {
                    userID =
                        int.Parse(
                        cp.Identity.Name.Split("|".ToCharArray())[1]);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
                }

                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                if (QA.Prospect.ProspectID == 0)
                {
                    if (this.VerifyMianDriver())
                    {
                        Customer.Prospect p = new Customer.Prospect();

                        if (ddlLocation.SelectedItem.Value != "")
                            p.LocationID = int.Parse(ddlLocation.SelectedItem.Value);
                        else
                            p.LocationID = 0;

                        p.ReferredID = 0;
                        p.ReferredDesc = "";
                        p.FirstName = txtFirstNm.Text.ToUpper();
                        p.LastName1 = txtLastNm1.Text.ToUpper();
                        p.LastName2 = txtLastNm2.Text.ToUpper();
                        p.HouseholdIncomeID = 0;
                        p.HomePhone = txtDriverPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "");
                        p.Email = txtDriverEmail.Text.Trim();
                        p.Modify = true;
                        p.IsBusiness = false;

                        p.Mode = (int)Customer.Prospect.ProspectMode.ADD;
                        p.SaveProspect(userID, false);

                        QA.ProspectID = p.ProspectID;
                        QA.Prospect.ProspectID = p.ProspectID;
                    }
                }

                if (this.VerifyMianDriver() && QA.Drivers.Count == 0)
                {
                    AutoDriver AD = new AutoDriver();

                    AD.FirstName = txtFirstNm.Text.ToUpper();
                    AD.LastName1 = txtLastNm1.Text.ToUpper();
                    AD.LastName2 = txtLastNm2.Text.ToUpper();

                    AD.HomePhone = txtDriverPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "");

                    if (ddlLocation.SelectedItem.Value != "")
                        AD.LocationID = int.Parse(ddlLocation.SelectedItem.Value);
                    else
                        AD.LocationID = 0;

                  //  AD.BirthDate = txtBirthDt.Text;
                    AD.BirthDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(txtBirthDt.Text).ToShortDateString());
                     
                    AD.SocialSecurity = txtSocialSecurity.Text;
                    AD.Gender = int.Parse(ddlGender.SelectedItem.Value);
                    AD.MaritalStatus = int.Parse(ddlMaritalSt.SelectedItem.Value);
                    AD.Email = txtDriverEmail.Text.Trim();
                    AD.License = "";

                    AD.QuoteID = ((TaskControl.QuoteAuto)Session["TaskControl"]).QuoteId;

                    if (QA.Prospect.ProspectID != 0) //Si viene desde prospecto ya tiene su num. de prospecto.
                    {
                        AD.ProspectID = QA.Prospect.ProspectID;
                        ((EPolicy.Customer.Prospect)AD).Mode = (int)EPolicy.Customer.Prospect.ProspectMode.UPDATE;
                        AD.Mode = (int)Enumerators.Modes.Insert; // Insert
                    }
                    else
                    {
                        AD.ProspectID = QA.Prospect.ProspectID;
                        ((EPolicy.Customer.Prospect)AD).Mode = (int)EPolicy.Customer.Prospect.ProspectMode.ADD;
                        AD.Mode = (int)Enumerators.Modes.Insert;
                    }

                    QA.RemoveDriver(AD);
                    QA.AddDriver(AD);

                    QA.Mode = (int)Enumerators.Modes.Insert;

                    QA.Save(userID, null, AD, false);

                    DdlDriverFill();

                    Session["TaskControl"] = QA;
                }
                else
                {
                    if (this.VerifyMianDriver() && QA.Drivers.Count > 0)
                    {
                        AutoDriver AD = null;
                        for (int i = 0; i < QA.Drivers.Count; i++)
                        {
                            AD = (AutoDriver)QA.Drivers[i];
                            if (QA.Prospect.ProspectID == AD.ProspectID)
                            {
                                AD.FirstName = txtFirstNm.Text.ToUpper();
                                AD.LastName1 = txtLastNm1.Text.ToUpper();
                                AD.LastName2 = txtLastNm2.Text.ToUpper();

                                AD.HomePhone = txtDriverPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "");
                                AD.LocationID = int.Parse(ddlLocation.SelectedItem.Value);

                                if (ddlLocation.SelectedItem.Value != "")
                                    AD.LocationID = int.Parse(ddlLocation.SelectedItem.Value);
                                else
                                    AD.LocationID = 0;

                               // AD.BirthDate = txtBirthDt.Text;
                                AD.BirthDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(txtBirthDt.Text).ToShortDateString()) ;
                                AD.SocialSecurity = txtSocialSecurity.Text;
                                AD.Gender = int.Parse(ddlGender.SelectedItem.Value);
                                AD.MaritalStatus = int.Parse(ddlMaritalSt.SelectedItem.Value);
                                AD.Email = txtDriverEmail.Text.Trim();
                                AD.License = "";

                                AD.QuoteID = ((TaskControl.QuoteAuto)Session["TaskControl"]).QuoteId;

                                AD.ProspectID = QA.Prospect.ProspectID;
                                ((EPolicy.Customer.Prospect)AD).Mode = (int)EPolicy.Customer.Prospect.ProspectMode.UPDATE;
                                AD.Mode = (int)Enumerators.Modes.Update;

                                QA.RemoveDriver(AD);
                                QA.AddDriver(AD);

                                QA.Mode = (int)Enumerators.Modes.Update;

                                QA.Save(userID, null, AD, false);

                                if (ddlDriver.Items.Count == 1)
                                {
                                    ddlDriver.Items.Clear();
                                    DdlDriverFill();
                                }

                                Session["TaskControl"] = QA;
                            }
                        }
                    }
                }
            }
            catch (Exception xcp)
            {
                throw new Exception(xcp.Message.Trim());
            }
        }

        private bool VerifyMianDriver()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            ArrayList errorMessages = new ArrayList();

            //First Name
            if (txtFirstNm.Text.Trim() == "")
            {
                errorMessages.Add("First Name is missing.");
            }

            //Last Name 1
            if (txtLastNm1.Text.Trim() == "")
            {
                errorMessages.Add("Last Name is missing.");
            }

            if (txtDriverPhone.Text.Trim() == "")
            {
                //errorMessages.Add("Main driver phone is missing.");
            }

            //BirthDate
            if (txtBirthDt.Text.Trim() == "")
            {
                errorMessages.Add("Birthdate is missing.");
            }

            else if (this.CalcAge(txtBirthDt.Text.Trim()) < 16)// < 18
            {
                errorMessages.Add("Invalid birthdate. A driver " +
                    "must be 16 years old or more.");
            }


            //SS
            if (txtSocialSecurity.Text.Trim() == "")
            {
                //errorMessages.Add("The Last 4 Social Security digit is missing.");
            }

            //Gender
            if (ddlGender.SelectedItem.Text.Trim() == string.Empty)
            {
                errorMessages.Add("Gender is missing.");
            }

            //Marital Status
            if (ddlMaritalSt.SelectedItem.Value == "")
            {
                errorMessages.Add("Marital status is missing.");
            }

            //Location At
            //if (ddlLocation.SelectedItem.Value == "")
            //{
            //    errorMessages.Add("Location is missing.");
            //}

            //Check email address validity
            //if (this.txtDriverEmail.Text.Trim() != "" && !this.isEmail(this.txtDriverEmail.Text.Trim()))
            //{
            //    errorMessages.Add("Email address is invalid.");
            //}

            if (errorMessages.Count > 0)
            {
                string popUpString = "";

                foreach (string message in errorMessages)
                {
                    popUpString += message + " ";
                }

                throw new Exception(popUpString);
            }
            return true;
        }

        private bool isEmail(string inputEmail)
        {
            //inputEmail  = NulltoString(inputEmail);
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            System.Text.RegularExpressions.Regex re =
                new System.Text.RegularExpressions.Regex(strRegex);
            if (re.IsMatch(inputEmail.Trim()))
                return (true);
            else
                return (false);
        }

        private void RemoveSessionLookUp()
        {
            Session.Remove("LookUpTables");
        }

        private void BindDdls()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            this.BindDdl(this.ddlVehicleClass, "VehicleUse",
                "VehicleUseID", "VehicleUseDesc", null);
            this.BindDdl(this.ddlNewUsed, "NewUse",
                "NewUseID", "NewUseDesc", null);
            this.BindDdl(this.ddlTerritory, "Territory",
                "TerritoryID", "TerritoryDesc", null);
            this.BindDdl(this.ddlAlarm, "AlarmType",
                "AlarmTypeID", "AlarmTypeDesc", null);
            this.BindDdl(this.ddlCollision,
                "CollisionDeductible", "CollisionDeductibleID",
                "CollisionDeductibleDesc", null);
            this.BindDdl(this.ddlComprehensive, "ComprehensiveDeductible",
                "ComprehensiveDeductibleID",
                "ComprehensiveDeductibleDesc", null);
            this.BindDdl(this.ddlBI, "BodilyInjuryLimit",
                "BodilyInjuryLimitID", "BodilyInjuryLimitDesc", null);
            this.BindDdl(this.ddlPD, "PropertyDamageLimit",
                "PropertyDamageLimitID", "PropertyDamageLimitDesc", null);
            this.BindDdl(this.ddlCSL, "CombinedSingleLimit",
                "CombinedSingleLimitID", "CombinedSingleLimitDesc", null);
            this.BindDdl(this.ddlMake, "VehicleMake", "VehicleMakeID",
                "VehicleMakeDesc", null);
            this.BindDdl(this.ddlYear, "VehicleYear", "VehicleYearID",
                "VehicleYearDesc", null);
            this.BindDdl(this.ddlSeatBelt, "SeatBelt", "SeatBeltID",
                "SeatBeltPremium", null);
            this.BindDdl(this.ddlPAR, "PersonalAccidentRider",
                "PersonalAccidentRiderID", "ParPremium", null);
            this.BindDdl(this.ddlLoanGap, "LeaseLoanGap", "LeaseLoanGapID",
                "LeaseLoanGapDesc", null);
            this.BindDdl(this.ddlMedical, "MedicalLimit", "MedicalLimitID",
                "MedicalLimitDesc", null);
            this.BindDdl(this.ddlAssistancePremium, "AssistancePremium",
                "AssistancePremiumID", "AssistancePremiumDesc", null);
            this.BindDdl(this.ddlPolicySubClass, "PolicySubClass",
                "PolicySubClassID", "PolicySubClassDesc", null);
        }

        private void BindDdl(System.Web.UI.WebControls.DropDownList DropDownList,
            string TableName, string ValueFieldName, string TextFieldName, DataTable Data)
        {
            DataTable dtResults;
            try
            {
                if (Data == null)
                {
                    dtResults = LookupTables.LookupTables.GetTable(TableName);
                }
                else
                {
                    dtResults = Data;
                }

                if (dtResults != null && dtResults.Rows.Count > 0)
                {
                    DropDownList.DataSource = dtResults;
                    DropDownList.DataValueField = ValueFieldName;
                    DropDownList.DataTextField = TextFieldName;
                    DropDownList.DataBind();
                    foreach (ListItem t in DropDownList.Items)
                        t.Text = UppercaseFirst(t.Text);
                    DropDownList.SelectedIndex = -1;
                    if (DropDownList.Items.FindByValue("0") == null)
                        DropDownList.Items.Insert(0, "");
                }
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
        }

        private void FillModel()
        {
            if (ddlMake.Items[ddlMake.SelectedIndex].Text != "")
            {
                int makeID = Int32.Parse(ddlMake.SelectedItem.Value.ToString());
                DataTable dtModel = LookupTables.LookupTables.GetTable("VehicleModel");
                DataTable dt = dtModel.Clone();
                DataRow[] DrModel = dtModel.Select("VehicleMakeID = " + makeID, "VehicleModelDesc");

                for (int i = 0; i <= DrModel.Length - 1; i++)
                {
                    DataRow myRow = dt.NewRow();
                    myRow["VehicleModelID"] = (int)DrModel[i].ItemArray[0];
                    myRow["VehicleMakeID"] = (int)DrModel[i].ItemArray[1];
                    myRow["VehicleModelDesc"] = DrModel[i].ItemArray[2].ToString();

                    dt.Rows.Add(myRow);
                    dt.AcceptChanges();
                }

                ddlModel.SelectedIndex = -1;
                ddlModel.Items.Clear();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "VehicleModelDesc";
                ddlModel.DataValueField = "VehicleModelID";
                ddlModel.DataBind();
                foreach (ListItem t in ddlModel.Items)
                    t.Text = UppercaseFirst(t.Text);
                ddlModel.SelectedIndex = -1;
                ddlModel.Items.Insert(0, "");
            }
            else
            {
                DataTable dtModel = LookupTables.LookupTables.GetTable("VehicleModel");

                ddlModel.DataSource = dtModel;
                ddlModel.DataTextField = "VehicleModelDesc";
                ddlModel.DataValueField = "VehicleModelID";
                ddlModel.DataBind();
                foreach (ListItem t in ddlModel.Items)
                    t.Text = UppercaseFirst(t.Text);
                ddlModel.SelectedIndex = -1;
                ddlModel.Items.Insert(0, "");
            }
        }

        private string Get1stPeriodISOCode(AutoCover AC)
        {
            Quotes.CoverBreakdown srch = new Quotes.CoverBreakdown();
            srch.Type = (int)Enumerators.Premiums.IsoCode;
            int index = (AC.Breakdown.IndexOf(srch));
            if (index >= 0)
            {
                Quotes.CoverBreakdown ISOC =
                    (Quotes.CoverBreakdown)AC.Breakdown[index];
                return ISOC.Breakdown[0].ToString();
            }
            return string.Empty;
        }

        private void SetDdlSelectedItemByText(
            System.Web.UI.WebControls.DropDownList Ddl, string Text)
        {
            for (int i = 0; i <= Ddl.Items.Count - 1; i++)
            {
                if (Ddl.Items[i].Text.Trim() == Text.Trim())
                {
                    Ddl.SelectedIndex = i;
                    return;
                }
            }
        }

        protected void HplAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                FillProperties();
                this.AddMainDriver();
                RemoveSessionLookUp();
                Response.Redirect("QuoteAutoDrivers.aspx", false);
            }
            catch (Exception xcp)
            {
                //litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message.Trim());
                //litPopUp.Visible = true;
                lblRecHeader.Text = xcp.Message;
                mpeSeleccion.Show();
            }

        }

        private void FillProperties()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (TxtVehiclesCount.Text.Trim() == "")
            {
                QA.VehicleCount = 1;
            }
            else
            {
                QA.VehicleCount = int.Parse(TxtVehiclesCount.Text);
            }

            if (txtTerm.Text.Trim() != "")
                QA.Term = int.Parse(txtTerm.Text);

            //QA.EffectiveDate = txtEffDt.Text;  
             QA.EffectiveDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(this.txtEffDt.Text).ToShortDateString());
            //QA.EffectiveDate = Convert.ToDateTime(this.txtEffDt.Text).ToString("MM/dd/yyyy");

            if (this.txtEffDt.Text.Trim() != string.Empty && this.txtTerm.Text.Trim() != string.Empty)
                this.txtExpDt.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(this.txtEffDt.Text).AddMonths(int.Parse(this.txtTerm.Text.Trim())).ToShortDateString());

            QA.ExpirationDate = txtExpDt.Text;
            QA.EntryDate = DateTime.Parse(txtEntryDt.Text.Trim());

            if (TxtInsCode.Text.Trim() != "")
            {
                QA.InsuranceCompany = TxtInsCode.Text.Trim();
            }
            else
            {
                QA.InsuranceCompany = "000";
            }

            if (ddlInsuranceCompany.SelectedIndex > 0 && ddlInsuranceCompany.SelectedItem != null)
                QA.InsuranceCompany = ddlInsuranceCompany.SelectedItem.Value;
            else
                QA.InsuranceCompany = "000";

            if (txtCharge.Text.Trim() != "")
                QA.Charge = decimal.Parse(txtCharge.Text, System.Globalization.NumberStyles.Currency);
            if (txtPremium.Text.Trim() != string.Empty)
                QA.TotalPremium = decimal.Parse(txtPremium.Text, System.Globalization.NumberStyles.Currency);
            txtTtlPremium.Text = String.Format("{0:c}", Math.Round(QA.TotalPremium + QA.Charge, 0));

            QA.ConvertToPolicyDate = null;

            Login.Login cp = HttpContext.Current.User as Login.Login;
            QA.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();


            if (QA.Term > 12)
                QA.PolicyType = "PAP"; //29 PAP
            else
                QA.PolicyType = "PAP"; //29 PAP

            Session["TaskControl"] = QA;
        }

        protected void Linkbutton1_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            try
            {
                FillProperties();
                this.AddMainDriver();

                //DdlDriverFill();
                if (ddlDriver.SelectedIndex <= 0 || ddlDriver.SelectedItem == null)
                    ddlDriver.SelectedIndex = 1;

                this.AddAutoCover();

                if (ddlCollision.SelectedItem.Text != "" && ddlBI.SelectedItem.Text == "")
                {
                    //Si es Double Interest No se debe  a�adir otro veh�culo.
                    litPopUp.Text = Utilities.MakeLiteralPopUpString("For Double Interest policy is allowed only one vehicle.");
                    litPopUp.Visible = true;
                }
                else
                {
                    RemoveSessionLookUp();
                    Response.Redirect("QuoteAutoVehicles.aspx");
                }
            }
            catch (Exception xcp)
            {
                //litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message.Trim());
                //litPopUp.Visible = true;
                lblRecHeader.Text = xcp.Message;
                mpeSeleccion.Show();
            }
        }

        private void AddAutoCover()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                try
                {
                    userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
                }

                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                if (this.VerifyAutoCover())
                {
                    AutoCover AC = new AutoCover();
                    int InternalID = 0;

                    if (QA.AutoCovers.Count == 0)
                    {
                        InternalID = QA.GetNewInternalID();
                    }
                    else
                    {
                        if (Session["AUTOEndorsement"] != null)
                        {
                            if (QA.AutoCovers.Count != 0)
                            {
                                AutoCover AC2 = (AutoCover)QA.AutoCovers[0];  // Siempre es cero para cargar solamente el primer vehiculo.

                                AC.QuotesAutoId = AC2.QuotesAutoId;
                                AC.InternalID = 1;
                                InternalID = AC.InternalID;
                            }
                        }
                        else
                        {
                            if (QA.Mode == 1)
                            {
                                QA.Mode = 1;
                                AC.InternalID = 0;
                            }
                            else
                            {
                                AC.QuotesAutoId = int.Parse(_QuoteAutoID.Value.ToString());
                                AC.InternalID = int.Parse(_InternalID.Value.ToString());
                                InternalID = AC.InternalID;
                            }
                        }
                    }

                    AC.VIN = txtVIN2.Text.Trim();
                    AC.Plate = txtPlate.Text.Trim();
                    AC.License = txtLicenseNumber.Text.Trim();
                    //AC.LicenseExpDate = txtExpDate.Text.Trim();
                    if(txtExpDate.Text.Trim()!="")
                    AC.LicenseExpDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(txtExpDate.Text.Trim()).ToShortDateString());
                   // AC.LicenseExpDate = Convert.ToDateTime(TextBox1.Text.Trim()).ToString("MM/dd/yyyy");


                    AC.PurchaseDate = this.TxtpurchaseDate.Text = "5/1/" + ddlYear.SelectedItem.Text.Trim(); //TxtpurchaseDate.Text;

                    if (ddlYear.SelectedIndex > 0 && ddlYear.SelectedItem != null)
                        AC.VehicleYear = int.Parse(ddlYear.SelectedItem.Value);
                    if (ddlMake.SelectedIndex > 0 && ddlMake.SelectedItem != null)
                        AC.VehicleMake = int.Parse(ddlMake.SelectedItem.Value);
                    if (ddlModel.SelectedIndex > 0 && ddlModel.SelectedItem != null)
                        AC.VehicleModel = int.Parse(ddlModel.SelectedItem.Value);
                    if (ddlBank.SelectedIndex > 0 && ddlBank.SelectedItem != null)
                        AC.Bank = (ddlBank.SelectedItem.Value.ToString());
                    if (ddlCompanyDealer.SelectedIndex > 0 && ddlCompanyDealer.SelectedItem != null)
                        AC.CompanyDealer = (ddlCompanyDealer.SelectedItem.Value.ToString());

                    if (txtAge.Text != "")
                        AC.VehicleAge = int.Parse(txtAge.Text);
                    if (ddlNewUsed.SelectedIndex > 0 && ddlNewUsed.SelectedItem != null)
                        AC.NewUse = int.Parse(ddlNewUsed.SelectedItem.Value);
                    if (txtCost.Text != "")
                        AC.Cost = decimal.Parse(txtCost.Text);
                    if (txtActualValue.Text != "")
                        AC.ActualValue = decimal.Parse(txtActualValue.Text);

                    AC.HomeCity = 0;  //int.Parse(ddlHomeCity.SelectedItem.Value);
                    AC.WorkCity = 0;  //int.Parse(ddlWorkCity.SelectedItem.Value);
                    if (ddlVehicleClass.SelectedIndex > 0 && ddlVehicleClass.SelectedItem != null)
                        AC.VehicleClass = ddlVehicleClass.SelectedItem.Value;
                    if (ddlTerritory.SelectedIndex > 0 && ddlTerritory.SelectedItem != null)
                        AC.Territory = int.Parse(ddlTerritory.SelectedItem.Value);
                    if (ddlAlarm.SelectedIndex > 0 && ddlAlarm.SelectedItem != null)
                        AC.AlarmType = int.Parse(ddlAlarm.SelectedItem.Value);
                    if (txtDeprec1st.Value != "")
                        AC.Depreciation1stYear = decimal.Parse(txtDeprec1st.Value);
                    if (txtDeprecAll.Value != "")
                        AC.DepreciationAllYear = decimal.Parse(this.txtDeprec1st.Value);
                    if (ddlMedical.SelectedIndex > 0 && ddlMedical.SelectedItem != null)
                        AC.MedicalLimit = int.Parse(ddlMedical.SelectedItem.Value);
                    if (this.ddlAssistancePremium.SelectedItem != null &&
                        this.ddlAssistancePremium.SelectedItem.Text != string.Empty)
                    {
                        // decimal Assistance = (decimal.Parse(this.ddlAssistancePremium.SelectedItem.Text.Trim()) * (int.Parse(txtTerm.Text.Trim()) / 12));
                        AC.AssistancePremium = decimal.Parse(this.ddlAssistancePremium.SelectedItem.Text.Trim());
                        // AC.AssistancePremium = Assistance;
                    }

                    //if (ddlLoanGap.SelectedIndex > 0 && ddlLoanGap.SelectedItem != null)
                    //    AC.LeaseLoanGapId = int.Parse(ddlLoanGap.SelectedItem.Value);

                    if (chkLLG.Checked)
                        AC.LeaseLoanGapId = 2;
                    else
                        AC.LeaseLoanGapId = 1;

                    if (ddlSeatBelt.SelectedIndex > 0 && ddlSeatBelt.SelectedItem != null)
                        AC.SeatBelt = int.Parse(ddlSeatBelt.SelectedItem.Value);
                    if (ddlPAR.SelectedIndex > 0 && ddlPAR.SelectedItem != null)
                        AC.PersonalAccidentRider = int.Parse(ddlPAR.SelectedItem.Value);
                    if (ddlCollision.SelectedIndex > 0 && ddlCollision.SelectedItem != null)
                        AC.CollisionDeductible = int.Parse(ddlCollision.SelectedItem.Value);
                    if (ddlComprehensive.SelectedIndex > 0 && ddlComprehensive.SelectedItem != null)
                        AC.ComprehensiveDeductible = int.Parse(ddlComprehensive.SelectedItem.Value);
                    if (txtDiscountCollComp.Text != "")
                        AC.DiscountCompColl = decimal.Parse(txtDiscountCollComp.Text);
                    if (ddlBI.SelectedIndex > 0 && ddlBI.SelectedItem != null)
                        AC.BodilyInjuryLimit = int.Parse(ddlBI.SelectedItem.Value);
                    if (ddlPD.SelectedIndex > 0 && ddlPD.SelectedItem != null)
                        AC.PropertyDamageLimit = int.Parse(ddlPD.SelectedItem.Value);
                    if (ddlCSL.SelectedIndex > 0 && ddlCSL.SelectedItem != null)
                        AC.CombinedSingleLimit = int.Parse(ddlCSL.SelectedItem.Value);
                    if (txtDiscountBIPD.Text != "")
                        AC.DiscountBIPD = decimal.Parse(txtDiscountBIPD.Text);

                    if (TxtVehicleRental.Text.Trim() == "")
                        TxtVehicleRental.Text = "0";

                    if (TxtAccidentDeathPremium.Text.Trim() == "")
                        TxtAccidentDeathPremium.Text = "0";

                    if (TxtEquitmentSonido.Text.Trim() == "")
                        TxtEquitmentSonido.Text = "0";

                    if (TxtEquitmentAudio.Text.Trim() == "")
                        TxtEquitmentAudio.Text = "0";

                    if (TxtEquitmentTapes.Text.Trim() == "")
                        TxtEquitmentTapes.Text = "0";

                    if (TxtEquipColl.Text.Trim() == "")
                        TxtEquipColl.Text = "0";

                    if (TxtEquipComp.Text.Trim() == "")
                        TxtEquipComp.Text = "0";

                    if (TxtCustomizeEquipLimit.Text.Trim() == "")
                        TxtCustomizeEquipLimit.Text = "0";

                    if (TxtUninsuredSingle.Text.Trim() == "")
                        TxtUninsuredSingle.Text = "0";

                    if (TxtUninsuredSplit.Text.Trim() == "")
                        TxtUninsuredSplit.Text = "0";

                    if (TxtTowing.Text.Trim() == "")
                        TxtTowing.Text = "0.0000";


                   // AC.OriginalTowingPremium = decimal.Parse(ddlTowing.SelectedItem.Value);

                    AC.OriginalTowingPremium = Decimal.Round(decimal.Parse(ddlTowing.SelectedItem.Value), 4);

                    AC.OriginalVehicleRental = decimal.Parse(GetOriginalVehicleRentaPremium());
                    AC.TowingID = int.Parse(ddlTowing.SelectedItem.Value);

                  //  AC.TowingPremium = decimal.Parse(TxtTowing.Text.Trim());

                    AC.TowingPremium = Decimal.Round(decimal.Parse(TxtTowing.Text.Trim()), 4);

                    AC.VehicleRentalID = int.Parse(ddlRental.SelectedItem.Value);
                    AC.VehicleRental = decimal.Parse(TxtVehicleRental.Text.Trim());
                    AC.AccidentalDeathID = int.Parse(ddlAccidentDeath.SelectedItem.Value);
                    AC.AccidentalDeathPremium = decimal.Parse(TxtAccidentDeathPremium.Text.Trim());
                    AC.AccidentalDeathPerson = int.Parse(ddlADPersons.SelectedItem.Value);

                    AC.EquipmentSoundID = int.Parse(ddlEquitmentSonido.SelectedItem.Value);
                    AC.EquipmentSoundPremium = decimal.Parse(TxtEquitmentSonido.Text.Trim());
                    AC.EquipmentAudioID = int.Parse(ddlEquitmentAudio.SelectedItem.Value);
                    AC.EquipmentAudioPremium = decimal.Parse(TxtEquitmentAudio.Text.Trim());
                    AC.EquipmentTapesPremium = decimal.Parse(TxtEquitmentTapes.Text);
                    AC.EquipmentTapes = chkEquipTapes.Checked;
                    AC.SpecialEquipmentCollPremium = decimal.Parse(TxtEquipColl.Text);
                    AC.SpecialEquipmentColl = chkEquipColl.Checked;
                    AC.SpecialEquipmentCompPremium = decimal.Parse(TxtEquipComp.Text);
                    AC.SpecialEquipmentComp = chkEquipComp.Checked;
                    AC.CustomizeEquipLimit = int.Parse(TxtCustomizeEquipLimit.Text);

                    AC.UninsuredSingleID = int.Parse(ddlUninsuredSingle.SelectedItem.Value);
                    AC.UninsuredSinglePremium = decimal.Parse(TxtUninsuredSingle.Text.Trim());
                    AC.UninsuredSplitID = int.Parse(ddlUninsuredSplit.SelectedItem.Value);
                    AC.UninsuredSplitPremium = decimal.Parse(TxtUninsuredSplit.Text.Trim());

                    AC.LoJack = chkLoJack.Checked;
                    AC.LojackExpDate = TxtLojackExpDate.Text.Trim();
                    AC.LoJackCertificate = txtLoJackCertificate.Text.Trim();

                    //NO SE UTILIZA
                    ddlRoadAssistEmp.SelectedIndex = -1;
                    if (ddlRoadAssistEmp.SelectedItem.Text.Trim() != "0" && 1 == 0)
                    {
                        AC.IsAssistanceEmp = true;
                        AC.AssistanceID = int.Parse(ddlRoadAssistEmp.SelectedItem.Value);
                        // AC.AssistancePremium = decimal.Parse(ddlRoadAssistEmp.SelectedItem.Text.Trim());
                        decimal Assistance = (decimal.Parse(this.ddlRoadAssistEmp.SelectedItem.Text.Trim()) * CalculatePeriodAmounts());
                        //AC.AssistancePremium = decimal.Parse(this.ddlRoadAssistEmp.SelectedItem.Text.Trim());
                        AC.AssistancePremium = Assistance;
                    }
                    else
                    {
                        AC.IsAssistanceEmp = false;
                        AC.AssistanceID = int.Parse(ddlRoadAssist.SelectedItem.Value);
                        // AC.AssistancePremium = decimal.Parse(ddlRoadAssist.SelectedItem.Text.Trim());
                        decimal Assistance = (decimal.Parse(this.ddlRoadAssist.SelectedItem.Text.Trim()) * CalculatePeriodAmounts());
                        //AC.AssistancePremium = decimal.Parse(this.ddlRoadAssist.SelectedItem.Text.Trim());
                        AC.AssistancePremium = Assistance;
                    }

                    // if (ddlExperienceDiscount.SelectedIndex > 0 && ddlExperienceDiscount.SelectedItem != null)
                    if (TxtExpDisc.Text.Trim() != "")
                        AC.ExperienceDiscount = int.Parse(TxtExpDisc.Text.Trim()); // int.Parse(ddlExperienceDiscount.SelectedItem.Value);
                    else
                        AC.ExperienceDiscount = 0;

                    if (ddlEmployeeDiscount.SelectedIndex > 0 && ddlEmployeeDiscount.SelectedItem != null)
                        AC.EmployeeDiscount = int.Parse(ddlEmployeeDiscount.SelectedItem.Value);
                    else
                        AC.EmployeeDiscount = 0;

                    if (txtMiscDiscount.Text != "")
                        AC.MiscDiscount = double.Parse(txtMiscDiscount.Text);

                    if (ddlPolicySubClass.SelectedIndex > 0 && ddlPolicySubClass.SelectedItem != null && TxtInsCode.Text.Trim() != "")
                    {
                        AC.PolicySubClassId = int.Parse(ddlPolicySubClass.SelectedItem.Value);
                    }
                    else
                    {
                        //FullCover
                        if (AC.ComprehensiveDeductible != 0 && (AC.BodilyInjuryLimit != 0 || AC.CombinedSingleLimit != 0))
                            AC.PolicySubClassId = 3;
                        //Double Interest
                        if (AC.ComprehensiveDeductible != 0 && (AC.BodilyInjuryLimit == 0 && AC.CombinedSingleLimit == 0))
                            AC.PolicySubClassId = 1;
                        //Liability
                        if (AC.ComprehensiveDeductible == 0 && (AC.BodilyInjuryLimit != 0 || AC.CombinedSingleLimit != 0))
                            AC.PolicySubClassId = 2;
                    }

                    if (InternalID != 0)
                    {
                        AC.InternalID = InternalID;
                    }
                    else
                    {
                        AC.InternalID = 0;
                    }

                    bool IsUpdate = false;

                    if (this._QuoteAutoID.Value.Trim() != "0" || Session["AUTOEndorsement"] != null)
                    //if (QA.Mode != 1 || Session["AUTOEndorsement"] != null)
                    {
                        AC.Mode = 2; // Update
                        IsUpdate = true;

                        AutoCover TmpAC = QA.GetAutoCover(AC);
                        if (TmpAC != null)
                        {
                            AC.AssignedDrivers = TmpAC.AssignedDrivers;
                            AC.Breakdown = TmpAC.Breakdown;
                        }
                        QA.RemoveAutoCover(AC);
                    }
                    else
                    {
                        AC.Mode = 1; // Insert
                        IsUpdate = false;
                    }

                    QA.AddAutoCover(AC, IsUpdate);
                    QA.SaveAutoCover(userID, AC, null, false);

                    AC.Mode = 2; // Se da valor de update para que no siga insertando el mismo record.
                    AssignedDriver(QA, AC);

                    Session["TaskControl"] = QA;
                }
            }
            catch (Exception xcp)
            {
                throw new Exception(xcp.Message.Trim());
            }
        }

        private bool VerifyAutoCover()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            ArrayList errorMessages = new ArrayList();
            Login.Login cp = HttpContext.Current.User as Login.Login;

            //Vehicle Use
            if (ddlVehicleClass.SelectedItem.Text.Trim() == string.Empty)
            {
                errorMessages.Add("Vehicle use is missing.");
            }

            //Territorry
            if (ddlTerritory.SelectedItem.Value == "")
            {
                errorMessages.Add("Territory is missing.");
            }

            if (ddlCollision.SelectedItem.Value == "" && ddlComprehensive.SelectedItem.Value == "" &&
                ddlBI.SelectedItem.Value == "" && ddlPD.SelectedItem.Value == "" &&
                ddlCSL.SelectedItem.Value == "")
            {
                errorMessages.Add("The following covers are missing; Collision, Comprehensive, Bodily Injury, Physical Damage.");
            }

            if (ddlCollision.SelectedItem.Value != "" && ddlComprehensive.SelectedItem.Value != "" &&
                ddlBI.SelectedItem.Value == "" && ddlPD.SelectedItem.Value == "" &&
                ddlCSL.SelectedItem.Value == "")
            //				ddlCSL.SelectedItem.Value == "" && int.Parse(this.txtTerm.Text) <= 12)
            {
                if (!cp.IsInRole("EXPRESSAUTOQUOTETERMS") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    if (int.Parse(this.txtTerm.Text) < 24)
                    {
                        errorMessages.Add("The term for double interest is wrong, please verify.");
                    }
                }
            }

            //Para uso solamente de Double Interest o Full Cover.
            if (ddlCollision.SelectedItem.Value != "" || ddlComprehensive.SelectedItem.Value != "")
            {
                //Vehicle Year
                //				if(ddlYear.SelectedItem.Text.Trim() == "")
                //				{
                //					errorMessages.Add("Vehicle year is missing.");
                //				}

                //NewUse
                if (ddlNewUsed.SelectedItem.Text.Trim() == "")
                {
                    errorMessages.Add("New / Use is missing.");
                }

                //Cost
                if (txtCost.Text.Trim() == "")
                {
                    errorMessages.Add("Vehicle cost is missing.");
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
            return true;
        }

        private void AssignedDriver(TaskControl.QuoteAuto QA, Quotes.AutoCover ACC)
        {
           
            AutoCover srch = new AutoCover();
            srch.QuotesAutoId = ACC.QuotesAutoId;
            srch.InternalID = ACC.InternalID;
            AutoCover AC = QA.GetAutoCover(srch);

            int InternalID = 0;
            AutoDriver AD = new AutoDriver();

            if (QA.Drivers != null && QA.Drivers.Count > 0)
            {
                for (int i = 0; i < QA.Drivers.Count; i++)
                {
                    AutoDriver Driver = (AutoDriver)QA.Drivers[i];
                    if (Driver.DriverID == int.Parse(this.ddlDriver.SelectedItem.Value))
                    {
                        InternalID = (int)Driver.InternalID;
                        i = QA.Drivers.Count;
                    }
                }
            }

            AD.InternalID = InternalID;

            Quotes.AssignedDriver AsDrv = new Quotes.AssignedDriver();
            AsDrv.AutoDriver = AD;


            ///Si hubo cambio en el assign driver elimina el record que habia y actualiza el nuevo driver asignado.
            ArrayList assdr = Quotes.AssignedDriver.LoadDriversForAutoCover(ACC.QuotesAutoId, QA.Drivers, false);
            if (assdr.Count != 0)
            {
                EPolicy.TaskControl.QuoteAuto q = new EPolicy.TaskControl.QuoteAuto(false);
                q.Drivers = assdr;

                Quotes.AssignedDriver driver = (Quotes.AssignedDriver)q.Drivers[0];

                if (driver.AutoDriver.DriverID != int.Parse(this.ddlDriver.SelectedItem.Value) && this.ddlDriver.SelectedItem.Text.Trim() != "")
                {
                    Quotes.AssignedDriver AsDrvRemove = new Quotes.AssignedDriver();
                    AsDrvRemove.AutoDriver = driver.AutoDriver;

                    AC.RemoveAssignedDriver(AsDrvRemove);
                }
            }

            if (!AC.AssignedDrivers.Contains(AsDrv))
            {
                AsDrv.AutoDriver = QA.GetDriver(AD);
                AsDrv.AutoCoverID = ACC.QuotesAutoId;

                if (rdoPrincipalOperatorY.Checked)
                {
                    AsDrv.PrincipalOperator = true;
                }

                if (rdoPrincipalOperatorN.Checked)
                {
                    AsDrv.PrincipalOperator = false;
                }

                if (rdoOnlyOperatorY.Checked)
                {
                    AsDrv.OnlyOperator = true;
                }

                if (rdoOnlyOperatorN.Checked)
                {
                    AsDrv.OnlyOperator = false;
                }

                AsDrv.Mode = (int)Enumerators.Modes.Insert;

                AC.AssignedDrivers.Add(AsDrv);
            }
            else
            {
                //////
                EPolicy.TaskControl.QuoteAuto qTemp = new EPolicy.TaskControl.QuoteAuto(false);
                qTemp.Drivers = assdr;
                Quotes.AssignedDriver driverUpdate = (Quotes.AssignedDriver)qTemp.Drivers[0];
                AsDrv = driverUpdate;
                //////

                //AsDrv.OnlyOperator = chkOnlyOperator.Checked;
                if (rdoOnlyOperatorY.Checked)
                {
                    AsDrv.OnlyOperator = true;
                }

                if (rdoOnlyOperatorN.Checked)
                {
                    AsDrv.OnlyOperator = false;
                }

                //AsDrv.PrincipalOperator = ChkPrincipalOperator.Checked;
                if (rdoPrincipalOperatorY.Checked)
                {
                    AsDrv.PrincipalOperator = true;
                }

                if (rdoPrincipalOperatorN.Checked)
                {
                    AsDrv.PrincipalOperator = false;
                }

                AC.RemoveAssignedDriver(AsDrv);
                AC.AssignedDrivers.Add(AsDrv);

                //AsDrv.UpdateAssignedDriverByAssignedDriver(AsDrv.AssignedDriverID);

                AsDrv = AC.GetAssignedDriver(AsDrv);
                if (AsDrv.Mode == (int)Enumerators.Modes.Delete)
                    AsDrv.Mode = (int)Enumerators.Modes.Nothing;
            }

            Login.Login cp = HttpContext.Current.User as Login.Login;
            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            QA.Save(UserID, AC, null, false);
            //QA.SaveAutoCover(UserID, AC, null, false);
            Session["TaskControl"] = QA;
        }

        private void Calculate(bool Silent)
        {
            try
            {
                //				this.FillProperties();
                //				this.AddMainDriver();
                //
                //				//DdlDriverFill();
                //				if (ddlDriver.SelectedIndex <= 0 || ddlDriver.SelectedItem == null)
                //					ddlDriver.SelectedIndex = 1;
                //
                //				this.AddAutoCover();

                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                QA.Calculate();

                //Para la derrama
                string effdate = "";
                if (QA.EffectiveDate.Trim() == "")
                    effdate = "10/01/2013";
                else
                    effdate = QA.EffectiveDate.Trim();

                if (QA.EntryDate >= DateTime.Parse("10/01/2013") && DateTime.Parse(effdate) >= DateTime.Parse("10/01/2013"))
                {
                    CalculateCharge();
                    decimal charge = decimal.Parse(txtCharge.Text.Trim());

                    //decimal totalPremium = decimal.Parse(txtTtlPremium.Text.Trim().Replace("$", "").Replace(",", "")) + charge;
                    //txtTtlPremium.Text = totalPremium.ToString("###,###.00");

                    //Para la derrama
                    QA.Charge = decimal.Parse(txtCharge.Text.Trim());
                   // QA.TotalPremium = decimal.Parse(txtPremium.Text.Trim().Replace("$", "").Replace(",", ""));
                    QA.TotalPremium = Decimal.Round(decimal.Parse(txtPremium.Text.Trim().Replace("$", "").Replace(",", "")), 2); 
                    


                    UpdateDerramaAuto(QA);
                }
                else
                {
                    txtCharge.Text = "0";
                    QA.Charge = 0;
                    UpdateDerramaAuto(QA);
                }

                Session["TaskControl"] = QA;

                ShowTotals(QA);

                //				if(!Silent)
                //				{
                //					litPopUp.Text = Utilities.MakeLiteralPopUpString("Quote was calculated.");
                //					litPopUp.Visible = true;
                //				}
            }
            catch (Exception xcp)
            {
                //litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message.Trim());
                //litPopUp.Visible = true;
                lblRecHeader.Text = xcp.Message;
                mpeSeleccion.Show();
            }
        }

        private void VerifyProspectID()
        {
            //Verificar si el ProspectId fue creado por el sistema.
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                try
                {
                    userID =
                        int.Parse(
                        cp.Identity.Name.Split("|".ToCharArray())[1]);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not parse user id from cp.Identity.Name.", ex);
                }

                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                if (QA.Prospect.ProspectID == 0 && QA.ProspectID == 0)
                {
                    if (QA.Drivers.Count > 0)
                    {
                        AutoDriver AD;
                        for (int i = 0; i < QA.Drivers.Count; i++)
                        {
                            AD = (AutoDriver)QA.Drivers[i];
                            if (QA.Prospect.ProspectID == AD.ProspectID)
                            {
                                AD.FirstName = txtFirstNm.Text.ToUpper();
                                AD.LastName1 = txtLastNm1.Text.ToUpper();
                                AD.LastName2 = txtLastNm2.Text.ToUpper();

                                AD.HomePhone = txtDriverPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "");

                                if (ddlLocation.SelectedItem.Value != "")
                                    AD.LocationID = int.Parse(ddlLocation.SelectedItem.Value);
                                else
                                    AD.LocationID = 0;

                               // AD.BirthDate = txtBirthDt.Text;
                               //AD.BirthDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(txtBirthDt.Text).ToShortDateString());
                                AD.BirthDate = String.Format("{0:MM/dd/yyyy}", txtBirthDt.Text);
                                AD.SocialSecurity = txtSocialSecurity.Text;
                                AD.Gender = int.Parse(ddlGender.SelectedItem.Value);
                                AD.MaritalStatus = int.Parse(ddlMaritalSt.SelectedItem.Value);
                                AD.Email = txtDriverEmail.Text.Trim();
                                AD.License = "";

                                AD.QuoteID = ((TaskControl.QuoteAuto)Session["TaskControl"]).QuoteId;

                                AD.ProspectID = QA.Prospect.ProspectID;
                                ((EPolicy.Customer.Prospect)AD).Mode = (int)EPolicy.Customer.Prospect.ProspectMode.ADD;
                                AD.Mode = (int)Enumerators.Modes.Update;

                                QA.RemoveDriver(AD);
                                QA.AddDriver(AD);

                                QA.Mode = (int)Enumerators.Modes.Update;

                                QA.Save(userID, null, AD, false);

                                Session["TaskControl"] = QA;
                            }
                        }
                    }
                    else
                    {
                        if (QA.Drivers.Count == 0)
                        {
                            AutoDriver AD = new AutoDriver();

                            AD.FirstName = txtFirstNm.Text.ToUpper();
                            AD.LastName1 = txtLastNm1.Text.ToUpper();
                            AD.LastName2 = txtLastNm2.Text.ToUpper();

                            AD.HomePhone = txtDriverPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "");

                            if (ddlLocation.SelectedItem.Value != "")
                                AD.LocationID = int.Parse(ddlLocation.SelectedItem.Value);
                            else
                                AD.LocationID = 0;

                           // AD.BirthDate = txtBirthDt.Text;

                            //AD.BirthDate =  String.Format("{0:MM/dd/yyyy}", DateTime.Parse(txtBirthDt.Text).ToShortDateString());
                            AD.BirthDate = String.Format("{0:MM/dd/yyyy}", txtBirthDt.Text);

                             
                            AD.Gender = int.Parse(ddlGender.SelectedItem.Value);
                            AD.MaritalStatus = int.Parse(ddlMaritalSt.SelectedItem.Value);
                            AD.Email = txtDriverEmail.Text.Trim();
                            AD.License = "";
                            AD.SocialSecurity = txtSocialSecurity.Text;

                            AD.QuoteID = ((TaskControl.QuoteAuto)Session["TaskControl"]).QuoteId;

                            AD.ProspectID = QA.Prospect.ProspectID;
                            ((EPolicy.Customer.Prospect)AD).Mode = (int)EPolicy.Customer.Prospect.ProspectMode.ADD;
                            AD.Mode = (int)Enumerators.Modes.Update;

                            QA.RemoveDriver(AD);
                            QA.AddDriver(AD);

                            QA.Mode = (int)Enumerators.Modes.Update;

                            QA.Save(userID, null, AD, false);

                            DdlDriverFill();

                            Session["TaskControl"] = QA;
                        }
                    }
                }
            }
            catch (Exception xcp)
            {
                throw new Exception(xcp.Message.Trim());
            }
        }

        private void VerifyCoverVsPremium()
        {
            //Verificaci�n de Cubiertas vs. Prima
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            ArrayList errorMessages = new ArrayList();

            AutoCover AC = null;

            for (int i = 0; i < QA.AutoCovers.Count; i++)
            {
                AC = (AutoCover)QA.AutoCovers[i];

                if (AC.ComprehensiveDeductible != 0 && AC.ComprehensivePremium() == 0)
                {
                    errorMessages.Add("AVISO: Por Favor verifique que la cubierta de Comprensive no sea cero($0.00).");
                }

                if (AC.CollisionDeductible != 0 && AC.CollisionPremium() == 0)
                {
                    errorMessages.Add("AVISO: Por Favor verifique que la cubierta de Collision no sea cero($0.00).");
                }

                if (AC.BodilyInjuryLimit != 0 && AC.BodilyInjuryPremium() == 0)
                {
                    errorMessages.Add("AVISO: Por Favor verifique que la cubierta de Bodily Injury no sea cero($0.00).");
                }

                if (AC.PropertyDamageLimit != 0 && AC.PropertyDamagePremium() == 0)
                {
                    errorMessages.Add("AVISO: Por Favor verifique que la cubierta de Property Damage no sea cero($0.00).");
                }

                if (AC.MedicalLimit != 0 && AC.MedicalPremium() == 0)
                {
                    errorMessages.Add("AVISO: Por Favor verifique que la cubierta de Medical no sea cero($0.00).");
                }

                if (AC.LeaseLoanGapId != 0 && AC.LeaseLoanGapPremium() == 0)
                {
                    errorMessages.Add("AVISO: Por Favor verifique que la cubierta de Lease Loan Gap no sea cero($0.00).");
                }
            }

            if (errorMessages.Count > 0)
            {
                string popUpString = "";

                foreach (string message in errorMessages)
                {
                    popUpString += message + "\r\n";
                }

                //this.litPopUp.Text = Utilities.MakeLiteralPopUpString(popUpString);
                //this.litPopUp.Visible = true;
                lblRecHeader.Text = popUpString;
                mpeSeleccion.Show();
                //return false;
            }
        }

        private void SaveQuoteData()
        {
            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                //int modeTemp = QA.Mode;
                this.FillProperties();
                this.AddMainDriver();

                //DdlDriverFill();
                if (ddlDriver.SelectedIndex <= 0 || ddlDriver.SelectedItem == null)
                    ddlDriver.SelectedIndex = 1;

                // QA.Mode = modeTemp;
                //Session["TaskControl"] = QA;

                this.AddAutoCover();

                //QA.Save(UserID);

                //Para la derrama
                string effdate = "";
                if (QA.EffectiveDate.Trim() == "")
                    effdate = "10/01/2013";
                else
                    effdate = QA.EffectiveDate.Trim();

                if (QA.EntryDate >= DateTime.Parse("10/01/2013") && DateTime.Parse(effdate) >= DateTime.Parse("10/01/2013"))
                {
                    CalculateCharge();
                    decimal charge = decimal.Parse(txtCharge.Text.Trim());
                }
                else
                    txtCharge.Text = "0";

                //decimal totalPremium = decimal.Parse(txtTtlPremium.Text.Trim().Replace("$", "").Replace(",", "")) + charge;
                //txtTtlPremium.Text = totalPremium.ToString("###,###.00");

                QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                //Para la derrama
                if (QA.EntryDate >= DateTime.Parse("10/01/2013") && DateTime.Parse(QA.EffectiveDate) >= DateTime.Parse("10/01/2013"))
                {
                    QA.Charge = decimal.Parse(txtCharge.Text.Trim());
                    //QA.TotalPremium = decimal.Parse(txtTtlPremium.Text.Trim().Replace("$", "").Replace(",", ""));
                }
                else
                {
                    QA.Charge = 0;
                    UpdateDerramaAuto(QA);
                }


                Session["TaskControl"] = QA;

                ShowTotals(QA);

                QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                Session["TaskControl"] = QA;

                
                
                FillTextControl();
                DisableControls();

                QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                QA.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                Session["TaskControl"] = QA;
                
            }
        }

        private void UpdateDerramaAuto(TaskControl.QuoteAuto QA)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];
                DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, QA.TaskControlID.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("TotalPremium", SqlDbType.Float, 0, QA.TotalPremium.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Charge", SqlDbType.Float, 0, QA.Charge.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                Executor.BeginTrans();
                Executor.Update("UpdateDerramaAuto", xmlDoc);
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save Charge. " + xcp.Message, xcp);
            }
        }

        private void ShowTotals(TaskControl.QuoteAuto QA)
        {
            AutoCover AC = null;
            decimal coll = 0;
            decimal comp = 0;
            decimal bi = 0;
            decimal pd = 0;
            decimal med = 0;
            decimal llg = 0;
            decimal par = 0;
            decimal ass = 0;
            decimal tow = 0;
            decimal ren = 0;
            decimal oth = 0;

            for (int i = 0; i < QA.AutoCovers.Count; i++)
            {
                AC = (AutoCover)QA.AutoCovers[i];
                comp = comp + AC.ComprehensivePremium();
                coll = coll + AC.CollisionPremium();
                bi = bi + AC.BodilyInjuryPremium();
                pd = pd + AC.PropertyDamagePremium();
                med = med + AC.MedicalPremium();
                llg = llg + System.Math.Round(AC.LeaseLoanGapPremium(), 0);
                par = par + AC.PersonalAccidentRiderPremium();
                ass = ass + AC.AssistancePremium;
                tow = Decimal.Round(tow + AC.TowingPremium, 4);
                ren = ren + AC.VehicleRental;
                oth = oth + AC.AccidentalDeathPremium + AC.UninsuredSinglePremium + AC.UninsuredSplitPremium + AC.EquipmentSoundPremium + AC.EquipmentAudioPremium +
                    AC.EquipmentTapesPremium + AC.SpecialEquipmentCollPremium + AC.SpecialEquipmentCompPremium;

               

                txtPremComprehensive.Text = String.Format("{0:c}", comp);
                txtPremCollision.Text = String.Format("{0:c}", coll);
                txtPremBodilyInjury.Text = String.Format("{0:c}", bi);
                txtPremPropertyDamage.Text = String.Format("{0:c}", pd);
                txtPremMedical.Text = String.Format("{0:c}", med);
                txtPremLLG.Text = String.Format("{0:c}", llg);
                txtPremPAR.Text = String.Format("{0:c}", par);
                txtPremRoadsideAssistance.Text = String.Format("{0:c}", ass);
                txtPremTowing.Text = String.Format("{0:c}", tow);
                TxtPremRental.Text = String.Format("{0:c}", ren);
                txtPremOthers.Text = String.Format("{0:c}", oth);

                if (i == 0)
                {
                    this.txt1stISO0.Text = this.Get1stPeriodISOCode(AC);
                }
            }

            txtPremium.Text = String.Format("{0:c}", (int)QA.TotalPremium);
            double totdisc = GetTotalDiscount();
            TxtTotDiscount.Text = String.Format("{0:c}", Math.Round(totdisc));
            txtCharge.Text = String.Format("{0:c}", Math.Round(QA.Charge));
            txtTtlPremium.Text = String.Format("{0:c}", ((int)QA.TotalPremium) + Math.Round(QA.Charge, 0) + decimal.Parse(totdisc.ToString()));

        }

        private bool ValidateThis()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            // verificar si es subscriptor, tarifador, supervisor
            bool IsSupervisor = false;
            bool IsTarifador = false;
            bool IsSubscriptor = false;

            DataTable dtUnderWritterRules = null;

            if (cp.IsInRole("UNDERWRITTER RULES AUTO SUPERVISOR"))
            {
                IsSupervisor = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(1);
            }
            if (cp.IsInRole("UNDERWRITTER RULES AUTO SUBSCRIBER"))
            {
                IsSubscriptor = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(2);
            }
            if (cp.IsInRole("UNDERWRITTER RULES AUTO TARIFADOR"))
            {
                IsTarifador = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(3);
            }

            ArrayList errorMessages = new ArrayList();

            //Effective date
            if (this.txtEffDt.Text == "")
            {
                errorMessages.Add("Effective date is missing.");
            }
            else if (!this.IsSamsValidDate(this.txtEffDt.Text))
            {
                errorMessages.Add("Effective date is invalid.  The " +
                    "correct format is \"mm/dd/yyyy\".");
            }
            else if (DateTime.Parse(this.txtEffDt.Text) > DateTime.Today)
            {
                //errorMessages.Add("Effective date cannot be prospective.");
            }

            //Verificar Term
            if (this.txtTerm.Text == "")
            {
                errorMessages.Add("Term is missing.");
            }
            else if (this.txtTerm.Text == "0")
            {
                errorMessages.Add("Term must be greater than zero.");
            }
            else
            {
                decimal term = decimal.Parse(txtTerm.Text.ToString().Trim());
                //Verificar si es double Interest (term tiene que ser mayor de 12)
                if (term > 12)
                {
                    if ((IsSubscriptor || IsTarifador) && dtUnderWritterRules.Rows.Count > 0)
                    {
                        if (term > int.Parse(dtUnderWritterRules.Rows[0]["Term"].ToString().Trim()))
                        {
                            errorMessages.Add("The term can not be greater than " + dtUnderWritterRules.Rows[0]["Term"].ToString().Trim() + ".");
                        }
                    }
                }
            }


            if (txtDriverPhone.Text.Trim() == "")
                errorMessages.Add("The Phone Number is missing.");

            //if (txtDriverEmail.Text.Trim() == "")
            //    errorMessages.Add("The Email is missing.");


            if (!rdo15percent.Checked && !rdo20percent.Checked)
            {
                errorMessages.Add("Please select 15% or 20% Depreciation.");
            }

            if (ddlNewUsed.SelectedItem.Value.Trim() == "")
                errorMessages.Add("The New / Used Car is missing.");

            if (TxtpurchaseDate.Text.Trim() == "")
                errorMessages.Add("The Purchase Date is missing.");

            //Verifica si el vehiculo es usado entonces el vehicleAge debe ser mayor que 0.
            //if (ddlNewUsed.SelectedItem.Value.Trim() != "")
            //{
            //    if (int.Parse(ddlNewUsed.SelectedItem.Value) == 1)//Used
            //    {
            //        if (txtAge.Text.Trim() == "")
            //        {
            //            errorMessages.Add("The Vehicle Age must be greatest than zero.");
            //        }
            //        else
            //        {
            //            if (int.Parse(txtAge.Text.Trim()) == 0)
            //            {
            //                errorMessages.Add("The Vehicle Age must be greatest than zero.");
            //            }
            //        }

            //        //Para uso solamente de Double Interest o Full Cover.
            //        if (ddlCollision.SelectedItem.Value != "" || ddlComprehensive.SelectedItem.Value != "")
            //        {
            //            if (txtActualValue.Text == "")
            //            {
            //                errorMessages.Add("The actual value must be greatest than zero.");
            //            }
            //            else
            //            {
            //                if (int.Parse(txtActualValue.Text.Trim()) == 0)
            //                {
            //                    errorMessages.Add("The actual value must be greatest than zero.");
            //                }
            //            }
            //        }
            //    }
            //    else  //Si el vehiculo es nuevo el VehicleAge debe ser 0.
            //    {
            //        if (txtAge.Text.Trim() == "")
            //        {
            //            errorMessages.Add("The Vehicle Age must be zero.");
            //        }
            //        //else
            //        //{
            //        //    if (int.Parse(txtAge.Text.Trim()) > 0)
            //        //    {
            //        //        errorMessages.Add("The Vehicle Age must be zero.");
            //        //    }
            //        //}
            //    }
            //}
            //Para uso solamente de Double Interest o Full Cover.
            if (ddlCollision.SelectedItem.Value != "" || ddlComprehensive.SelectedItem.Value != "")
            {
                //NewUse
                if (ddlNewUsed.SelectedItem.Text.Trim() == "")
                {
                    errorMessages.Add("New / Use is missing.");
                }

                //Verifica el costo del vehiculo.
                if (txtCost.Text == "")
                {
                    errorMessages.Add("The vehicle cost must be greatest than zero.");
                }
                else
                {
                    if (int.Parse(txtCost.Text.Trim()) == 0)
                    {
                        errorMessages.Add("The vehicle cost must be greatest than zero.");
                    }
                    else if (int.Parse(txtCost.Text.Trim()) > 0)
                    {
                        if ((IsSubscriptor || IsTarifador) && dtUnderWritterRules.Rows.Count > 0)
                        {
                            if (int.Parse(txtCost.Text.Trim()) > int.Parse(dtUnderWritterRules.Rows[0]["AutoCostNuevoMax"].ToString().Trim()))
                            {
                                errorMessages.Add("The vehicle cost must be greatest than $" + dtUnderWritterRules.Rows[0]["AutoCostNuevoMax"].ToString().Trim() + ".");
                            }
                        }
                    }
                    else
                        //Discount
                        if (QA.PolicyType == "M02" || QA.PolicyType == "M03" ||
                            QA.PolicyType == "M04" || QA.PolicyType == "M05" ||
                            QA.PolicyType == "M06" || QA.PolicyType == "M07" ||
                            QA.PolicyType == "M08" || QA.PolicyType == "M09" ||
                            QA.PolicyType == "M10" || QA.PolicyType == "M11" ||
                            QA.PolicyType == "M12" || QA.PolicyType == "M13" ||
                            QA.PolicyType == "M14" || QA.PolicyType == "M15" ||
                            QA.PolicyType == "M16")
                    {
                        if (int.Parse(txtActualValue.Text.Trim()) > 55000 &&
                            (!cp.IsInRole("AUTOACV55000") && !cp.IsInRole("ADMINISTRATOR")))
                        {
                            errorMessages.Add("The Actual Cash Value must be equal or less than $55,000.");
                        }
                    }
                }
            }
            // Determinar Collision 
            if (ddlCollision.SelectedItem.Value != "")
            {
                if ((IsTarifador || IsSubscriptor) && dtUnderWritterRules.Rows.Count > 0)
                {
                    int Deducible = int.Parse(ddlCollision.SelectedItem.Text.ToString().Trim());

                    if (Deducible < 250 || Deducible > int.Parse(dtUnderWritterRules.Rows[0]["AutoDeducible"].ToString().Trim()))
                    {
                        errorMessages.Add("The deductible cannot be less than $250 and greater than $" + dtUnderWritterRules.Rows[0]["AutoDeducible"].ToString().Trim() + ".");
                    }
                }
            }
            if (int.Parse(this.txtTerm.Text) > 12)
            {
                if (ddlBI.SelectedItem.Text.Trim() != "")
                    errorMessages.Add("The Bodily Injury cover does not apply to the Double Interest cover.");

                if (ddlPD.SelectedItem.Text.Trim() != "")
                    errorMessages.Add("The Property Damage cover does not apply to the Double Interest cover.");

                if (ddlCSL.SelectedItem.Text.Trim() != "")
                    errorMessages.Add("The Combined Single Limit cover does not apply to the Double Interest cover.");

                if (ddlMedical.SelectedItem.Text.Trim() != "")
                    errorMessages.Add("The Medical Limit cover does not apply to the Double Interest cover.");
            }

            if (int.Parse(this.txtTerm.Text) == 12)
            {
                if (ddlBI.SelectedItem.Value == "" && ddlCSL.SelectedItem.Value == "")
                {
                    errorMessages.Add(@"For Liability or Full Cover you must choose Bodily Injury\Property Damage or Combined Single Limit coverage.");
                }
                else
                {
                    if ((IsSubscriptor || IsTarifador) && dtUnderWritterRules.Rows.Count > 0)
                    {
                        if (ddlBI.SelectedItem.Text != "")
                        {
                            if (int.Parse(ddlBI.SelectedItem.Value.ToString().Trim()) > int.Parse(dtUnderWritterRules.Rows[0]["AutoBI"].ToString().Trim()))
                            {
                                errorMessages.Add("The Bodily Injury can't be greater than " + dtUnderWritterRules.Rows[0]["BIDesc"].ToString().Trim() + ".");
                            }
                        }
                        if (ddlCSL.SelectedItem.Value != "")
                        {
                            if ((int.Parse(ddlCSL.SelectedItem.Text.ToString().Trim()) * 1000) > int.Parse(dtUnderWritterRules.Rows[0]["CSL"].ToString().Trim()))
                            {
                                errorMessages.Add("The Combined Single Limit cannot be greater than $" + dtUnderWritterRules.Rows[0]["CSL"].ToString().Trim() + ".");
                            }
                        }
                    }
                }

                // Property Damages

                if (ddlPD.SelectedItem.Value != "")
                {
                    if ((IsTarifador || IsSubscriptor) && dtUnderWritterRules.Rows.Count > 0)
                    {
                        if (int.Parse(ddlPD.SelectedItem.Text.ToString().Trim()) > int.Parse(dtUnderWritterRules.Rows[0]["AutoPD"].ToString().Trim()))
                        {
                            errorMessages.Add("The Properties Damages cannot be greater than $" + dtUnderWritterRules.Rows[0]["AutoPD"].ToString().Trim() + ".");
                        }
                    }
                }

            }

            //Verifica T�rmino y PolicySubClass			
            if (ddlPolicySubClass.SelectedIndex > 0 && ddlPolicySubClass.SelectedItem != null && TxtInsCode.Text.Trim() != "")
            {
                ////Double Interest
                //if (ddlComprehensive.SelectedItem.Value != "" && (ddlBI.SelectedItem.Value == "" && ddlCSL.SelectedItem.Value == ""))
                //{
                //    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 1)
                //    {
                //        if (int.Parse(this.txtTerm.Text) <= 12)
                //            errorMessages.Add("Please verify the term for Double Interest Cover.");
                //    }
                //    else
                //    {
                //        //errorMessages.Add("The Policy Type must be Double Interest Cover.");
                //        ddlPolicySubClass.SelectedIndex = 1;
                //    }
                //}

                //Liability
                //if (ddlComprehensive.SelectedItem.Value == "" && (ddlBI.SelectedItem.Value != "" || ddlCSL.SelectedItem.Value != ""))
                //{
                //    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 2)
                //    {
                //        if (int.Parse(this.txtTerm.Text) > 12)
                //            errorMessages.Add("Please verify the term for Liability Cover.");
                //    }
                //    else
                //    {
                //        //errorMessages.Add("The Policy Type must be Liability Cover.");
                //        ddlPolicySubClass.SelectedIndex = 3;
                //        string a = ddlPolicySubClass.SelectedItem.Value;
                //    }
                //}

                ////FullCover
                //if (ddlComprehensive.SelectedItem.Value != "" && (ddlBI.SelectedItem.Value != "" || ddlCSL.SelectedItem.Value != ""))
                //{
                //    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 3)
                //    {
                //        if (int.Parse(this.txtTerm.Text) > 12)
                //            errorMessages.Add("Please verify the term for FullCover.");
                //    }
                //    else
                //    {
                //        //errorMessages.Add("The Policy Type must be FullCover.");
                //        ddlPolicySubClass.SelectedIndex = 2;
                //    }
                //}
            }

            if (ddlGender.SelectedItem.Value.Trim() == "")
                errorMessages.Add("The Gender is missing.");

            if (txtBirthDt.Text.Trim() == "")
                errorMessages.Add("The Birthdate is missing.");

            //Only Operator se usa solo para femina y que sea mayor de 28 y menor de 50 a�os.
            if (ddlGender.SelectedItem.Value.Trim() != "" && txtBirthDt.Text.Trim() != "")
            {
                if (!rdoOnlyOperatorY.Checked && !rdoOnlyOperatorN.Checked)
                {
                    if (ddlDriver.SelectedIndex <= 0 || ddlDriver.SelectedItem == null)
                    {
                        if (int.Parse(ddlGender.SelectedItem.Value) == 2 && (CalcAge(txtBirthDt.Text) > 28 && CalcAge(txtBirthDt.Text) < 50))
                        {
                            errorMessages.Add("Please select Yes or No in Only Operator for Female driver.");
                        }
                        else
                        {
                            rdoOnlyOperatorY.Checked = false;
                            rdoOnlyOperatorN.Checked = true;
                        }
                    }
                    else
                    {
                        if (IsOnlyOperator())
                        {
                            errorMessages.Add("Please select Yes or No in Only Operator for Female driver.");
                        }
                        else
                        {
                            rdoOnlyOperatorY.Checked = false;
                            rdoOnlyOperatorN.Checked = true;
                        }
                    }
                }
            }

            //Validar Otras cubiertas por Tipo de Seguro
            //Double Interest
            if (int.Parse(txtTerm.Text.ToString().Trim()) > 12)
            {
                if (ddlComprehensive.SelectedItem.Text.Trim() != "" && (ddlBI.SelectedItem.Text.Trim() == "" && ddlCSL.SelectedItem.Text.Trim() == ""))
                {
                    if (ddlAccidentDeath.SelectedItem.Text.Trim() != "")
                        errorMessages.Add("The Accident Death cover does not apply to the Double Interest cover.");

                    if (ddlEquitmentSonido.SelectedItem.Text.Trim() != "")
                        errorMessages.Add("The Equipment Sound cover does not apply to the Double Interest cover.");

                    if (ddlEquitmentAudio.SelectedItem.Text.Trim() != "")
                        errorMessages.Add("The Equipment Audio cover does not apply to the Double Interest cover.");

                    if (chkEquipTapes.Checked)
                        errorMessages.Add("The Equipment Tapes, Disc. cover does not apply to the Double Interest cover.");

                    if (TxtCustomizeEquipLimit.Text.Trim() != "")
                        errorMessages.Add("The Customize Equipment cover does not apply to the Double Interest cover.");

                    if (ddlUninsuredSingle.SelectedItem.Text.Trim() != "")
                        errorMessages.Add("The Uninsured Single cover does not apply to the Double Interest cover.");

                    if (ddlUninsuredSplit.SelectedItem.Text.Trim() != "")
                        errorMessages.Add("The Uninsured Split cover does not apply to the Double Interest cover.");
                }
            }
            //Liability
            if (ddlComprehensive.SelectedItem.Value == "" && (ddlBI.SelectedItem.Value != "" || ddlCSL.SelectedItem.Value != ""))
            {
                if (chkLLG.Checked)
                    errorMessages.Add("The lease Loan Gap cover does not apply to the liability cover.");

                if (ddlRental.SelectedItem.Text.Trim() != "0")
                    errorMessages.Add("The Transportation Expenses cover does not apply to the liability cover.");

                if (double.Parse(ddlTowing.SelectedItem.Value.Trim()) > 0)
                    errorMessages.Add("The Towing cover does not apply to the liability cover.");

                if (ddlEquitmentSonido.SelectedItem.Text.Trim() != "")
                    errorMessages.Add("The Equipment Sound cover does not apply to the liability cover.");

                if (ddlEquitmentAudio.SelectedItem.Text.Trim() != "")
                    errorMessages.Add("The Equipment Audio cover does not apply to the liability cover.");

                if (chkEquipTapes.Checked)
                    errorMessages.Add("The Equipment Tapes, Disc. cover does not apply to the liability cover.");

                if (TxtCustomizeEquipLimit.Text.Trim() != "")
                    errorMessages.Add("The Customize Equipment cover does not apply to the liability cover.");

            }

            //FullCover
            if (ddlComprehensive.SelectedItem.Value != "" && (ddlBI.SelectedItem.Value != "" || ddlCSL.SelectedItem.Value != ""))
            {
                //Aplican todas las cubiertas
            }

            if (!rdoPrincipalOperatorY.Checked && !rdoPrincipalOperatorN.Checked)
            {
                errorMessages.Add("Please select Yes or No in Pricipal Operator.");
            }

            if (cp.IsInRole("AUTOLIMITDISCOUNT"))
            {
                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {
                    if (QA.PolicyType == "MFC")
                    {
                        if (i == 0)
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 50.0) //35.0
                            {
                                errorMessages.Add("The Coll/Comp Discount limit is 50%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 50.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit is 50%.");
                            }
                        }

                        if (i >= 1)
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 55.0)
                            {
                                errorMessages.Add("The Coll/Comp Discount limit is 55%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 55.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit is 55%.");
                            }
                        }
                    }

                    if (QA.PolicyType == "MFE")
                    {
                        if (i == 0)
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 50.0)
                            {
                                errorMessages.Add("The Coll/Comp Discount limit is 50%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 50.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit is 50%.");
                            }
                        }

                        if (i >= 1)
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 60.0)
                            {
                                errorMessages.Add("The Coll/Comp Discount limit is 60%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 60.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit is 60%.");
                            }
                        }
                    }
                }
            }

            if (cp.IsInRole("AUTOLIMITDISCOUNT"))
            {
                if (QA.AutoCovers.Count == 0)
                {
                    if (QA.PolicyType == "MFC")
                    {
                        if (double.Parse(txtDiscountCollComp.Text.Trim()) > 50.0)
                        {
                            errorMessages.Add("The Coll/Comp Discount limit is 50%.");
                        }
                        if (double.Parse(txtDiscountBIPD.Text.Trim()) > 50.0)
                        {
                            errorMessages.Add("The BI/PD Discount limit is 50%.");
                        }
                    }

                    if (QA.PolicyType == "MFE")
                    {
                        if (double.Parse(txtDiscountCollComp.Text.Trim()) > 50.0)
                        {
                            errorMessages.Add("The Coll/Comp Discount limit is 50%.");
                        }
                        if (double.Parse(txtDiscountBIPD.Text.Trim()) > 50.0)
                        {
                            errorMessages.Add("The BI/PD Discount limit is 50%.");
                        }
                    }
                }
            }

            if (Session["OptimaPersonalPackage"] != null)
            {
                cp = HttpContext.Current.User as Login.Login;
                if (cp.IsInRole("UNDERWRITTERRULESOPP"))
                {
                    DataTable dt = EPolicy.TaskControl.Policy.GetUnderwritterRulesByUnderwritterRulesID(1);
                    if (txtActualValue.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoBI2"].ToString().Trim()) < double.Parse(txtActualValue.Text.Trim()))
                            errorMessages.Add("The limit for Actual Cash Value is $" + dt.Rows[0]["AutoBI2"].ToString().Trim() + ".");
                    }

                    if (ddlBI.SelectedItem.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoBI"].ToString().Trim()) < double.Parse(ddlBI.SelectedItem.Value.Trim()))
                            errorMessages.Add("The limit for Bodily Injury coves is $250,000.");
                    }

                    if (ddlPD.SelectedItem.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoPD"].ToString().Trim()) < double.Parse(ddlPD.SelectedItem.Text.Trim()))
                            errorMessages.Add("The limit for Property Damage coves is $" + dt.Rows[0]["AutoPD"].ToString().Trim() + ",000" + ".");
                    }

                    if (ddlCollision.SelectedItem.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoDeducible"].ToString().Trim()) < double.Parse(ddlCollision.SelectedItem.Text.Trim()))
                            errorMessages.Add("The deductible for this vehicle is $" + dt.Rows[0]["AutoDeducible"].ToString().Trim() + ".");
                    }
                }
            }

            if (errorMessages.Count > 0)
            {
                string popUpString = "";

                foreach (string message in errorMessages)
                {
                    popUpString += message + "\r\n";
                }

                //this.litPopUp.Text = Utilities.MakeLiteralPopUpString(popUpString);
                //this.litPopUp.Visible = true;
                //lblRecHeader.Text = popUpString;
                //mpeSeleccion.Show();
                throw new Exception(popUpString);
                return false;
            }
            return true;
        }
        private DataTable GetUnderWritterRulesByUnderWritterRuleID(int id)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("UnderwritterRulesID", SqlDbType.Int, 0, id.ToString(), ref cookItems);

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
                dt = exec.GetQuery("GetUnderwritterRulesByUnderwritterRulesID", xmlDoc);
            }

            catch (Exception ex)
            {
                throw new Exception("No pudo encontrar la informaci�n, Por favor trate de nuevo.", ex);
            }

            return dt;
        }
        private bool IsOnlyOperator()
        {
            bool IsTrue = false;
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            AutoCover AC = (AutoCover)QA.AutoCovers[0];  // Siempre es cero para cargar solamente el primer vehiculo.

            AutoCover AC2 = new AutoCover();
            AC2.QuotesAutoId = AC.QuotesAutoId;
            AC2.InternalID = AC.InternalID;
            AC2 = QA.GetAutoCover(AC2);

            if (AC.QuotesAutoId.ToString().Trim() == "")
            {
                _QuoteAutoID.Value = "0";
            }
            else
            {
                _QuoteAutoID.Value = AC.QuotesAutoId.ToString().Trim();
            }
            _InternalID.Value = AC.InternalID.ToString().Trim();

            ArrayList Assigned = AC2.AssignedDrivers;
            if (Assigned != null && Assigned.Count > 0)
            {
                for (int i = 0; i < Assigned.Count; i++)
                {
                    Quotes.AssignedDriver AD = (Quotes.AssignedDriver)Assigned[i];
                    Quotes.AutoDriver Driver = AD.AutoDriver;

                    ddlDriver.SelectedIndex =
                        ddlDriver.Items.IndexOf(ddlDriver.Items.FindByValue(
                        Driver.DriverID.ToString()));

                    if (AD.AutoDriver.Gender == 2 && (CalcAge(AD.AutoDriver.BirthDate.ToString()) > 28 && CalcAge(AD.AutoDriver.BirthDate.ToString()) < 50))
                    {
                        IsTrue = true;
                    }
                    else
                    {
                        IsTrue = false;
                    }
                }
            }

            return IsTrue;
        }

        private bool IsSamsValidDate(string sdate)
        {
            DateTime dt = DateTime.Today;
            bool isDate = true;
            char[] splitter = { '/' };

            try
            {
                dt = DateTime.Parse(sdate);
            }
            catch
            {
                isDate = false;
                return isDate;
            }

            if (sdate.Split(splitter).Length != 3 ||
                sdate.Split(splitter, 3)[2].Length != 4)
            {
                isDate = false;
            }
            return isDate;
        }

        private void EnableControls()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            bool IsSupervisor = false;
            bool IsTarifador = false;
            bool IsSubscriptor = false;

            DataTable dtUnderWritterRules = null;

            if (cp.IsInRole("UNDERWRITTER RULES AUTO SUPERVISOR"))
            {
                IsSupervisor = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(1);
            }
            if (cp.IsInRole("UNDERWRITTER RULES AUTO SUBSCRIBER"))
            {
                IsSubscriptor = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(2);
            }
            if (cp.IsInRole("UNDERWRITTER RULES AUTO TARIFADOR"))
            {
                IsTarifador = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(3);
            }

            //text date 
            TextBox1.Enabled = true;

            // ddl DropDownList 
            ddlGender.Enabled = true;
            ddlLocation.Enabled = false;
            ddlInsuranceCompany.Enabled = false;
            ddlMake.Enabled = true;
            ddlModel.Enabled = true;
            ddlYear.Enabled = true;
            ddlCompanyDealer.Enabled = true;
            ddlBank.Enabled = true;
            ddlAlarm.Enabled = true;
            ddlVehicleClass.Enabled = true;
            ddlTerritory.Enabled = true;
            ddlSeatBelt.Visible = false;
            ddlAssistancePremium.Visible = false;
            ddlDriver.Enabled = true;
            ddlCollision.Enabled = true;
            ddlComprehensive.Enabled = true;
            ddlBI.Enabled = true;
            ddlPD.Enabled = true;
            ddlCSL.Enabled = true;
            ddlMedical.Enabled = true;
            ddlLoanGap.Enabled = true;
            ddlPAR.Enabled = true;
            //ddlExperienceDiscount.Enabled = true;
            //ddlEmployeeDiscount.Enabled = true;
            //ddlRoadAssistEmp.Enabled = true;
            ddlRoadAssistEmp.Enabled = false;
            //ddlRoadAssist.Enabled = true;
            ddlRoadAssist.Enabled = false;
            ddlAccidentDeath.Enabled = true;
            ddlADPersons.Enabled = true;

            if (IsSubscriptor && dtUnderWritterRules.Rows.Count > 0)
            {
                ddlExperienceDiscount.Enabled = (bool.Parse(dtUnderWritterRules.Rows[0]["ExperienceDiscount"].ToString().Trim()));
                TxtExpDisc.Enabled = false;
                //ddlEmployeeDiscount.Enabled = (bool.Parse(dtUnderWritterRules.Rows[0]["EmployeeDiscount"].ToString().Trim()));
                //txtMiscDiscount.Enabled = (bool.Parse(dtUnderWritterRules.Rows[0]["MiscelaneoDiscount"].ToString().Trim()));
            }
            else if (IsTarifador && dtUnderWritterRules.Rows.Count > 0)
            {
                ddlExperienceDiscount.Enabled = (bool.Parse(dtUnderWritterRules.Rows[0]["ExperienceDiscount"].ToString().Trim()));
                TxtExpDisc.Enabled = false;
                // ddlEmployeeDiscount.Enabled = (bool.Parse(dtUnderWritterRules.Rows[0]["EmployeeDiscount"].ToString().Trim()));
                //txtMiscDiscount.Enabled = (bool.Parse(dtUnderWritterRules.Rows[0]["MiscelaneoDiscount"].ToString().Trim()));
            }
            else
            {
                ddlExperienceDiscount.Enabled = true;
                TxtExpDisc.Enabled = false;
                //ddlEmployeeDiscount.Enabled = true;
                //txtMiscDiscount.Enabled = true;
            }
            //Button
            btnNew.Visible = false;
            btnEdit.Visible = false;
            BtnExit.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            HplAdd.Enabled = true;
            Linkbutton1.Enabled = true;
            btnVehicles.Visible = false;
            btnDrivers.Visible = false;
            btnPrint.Visible = false;
            btnViewCvr.Visible = false;
            btnAuditTrail.Visible = false;
            BtnChangeToCustomer.Visible = false;
            cmdDefPlan.Visible = false;
            cmdConvertToPolicy.Visible = false;

            //Driver Info	
            txtEffDt.Enabled = true; //false
            txtTerm.Enabled = true;
            txtFirstNm.Enabled = true;
            txtLastNm1.Enabled = true;
            txtLastNm2.Enabled = true;
            ddlMaritalSt.Enabled = true;
            txtBirthDt.Enabled = true;
            txtSocialSecurity.Enabled = true;
            txtDriverAge.Enabled = false;
            //chkOnlyOperator.Enabled = true;
            //ChkPrincipalOperator.Enabled = true;
            rdoOnlyOperatorY.Enabled = true;
            rdoOnlyOperatorN.Enabled = true;
            rdoPrincipalOperatorY.Enabled = true;
            rdoPrincipalOperatorN.Enabled = true;
            rdo15percent.Enabled = true;
            rdo20percent.Enabled = true;
            txtDriverPhone.Enabled = true;
            txtDriverEmail.Enabled = true;
            TxtpurchaseDate.Enabled = true;

            //Vehicle Info

            TxtInsCode.Enabled = false;
            TxtVehicleRental.Enabled = true;
            TxtTowing.Enabled = true;
            ddlRental.Enabled = true;
            TxtVehiclesCount.Enabled = true;

            if (QA.InsuranceCompany.ToString().Trim() == "000")
                ddlPolicySubClass.Enabled = false;
            else
                ddlPolicySubClass.Enabled = true;

            txtAge.Enabled = false;
            ddlNewUsed.Enabled = true;
            txtCost.Enabled = true;
            txtPlate.Enabled = true;
            txtPurchaseDt.Enabled = true;
            txtLicenseNumber.Enabled = true;
            txtExpDate.Enabled = true;
            txtVIN2.Enabled = true;
            txtActualValue.Enabled = true;
            rdo15percent.Enabled = true;
            rdo20percent.Enabled = true;
            txtEntryDt.Enabled = false;
            txtExpDt.Visible = false;
            //LblExpirationDT.Visible = false;
            txtPurchaseDt.Visible = false;

            //Deductible & Limits
            txtDiscountCollComp.Enabled = true;

            SetEnableBIPDCLS();

            txtDiscountBIPD.Enabled = true;

            chkLLG.Enabled = true;

            txtRoadsideAssitance.Enabled = true;
            txtTowingPrm.Enabled = true;
            TxtVehicleRental.Enabled = true;
            TxtTowing.Enabled = true;
            ddlRental.Enabled = true;

            imgCalendarEff.Enabled = true;

            if (Session["OptimaPersonalPackage"] != null)
            {
                ddlTowing.Visible = true;
                ddlTowing.Enabled = true;
                txtTowingPrm.Visible = false;
                txtTowingPrm.Enabled = false;
            }
            else
            {
                ddlTowing.Visible = true;
                ddlTowing.Enabled = true;
                txtTowingPrm.Visible = false;
                txtTowingPrm.Enabled = true;
            }

            if (QA.IsPolicy == false && Session["AUTOEndorsement"] != null)
            {
                txtEffDtEndorsementPrimary.Visible = true;
                txtEffDtEndorsementPrimary.Enabled = true;
                Label16.Visible = true;
                imgCalendarEnd.Visible = true;
                imgCalendarEnd.Enabled = true;
                txtEffDt.Enabled = true;//false
                imgCalendarEff.Enabled = false;

            }
            else
            {
                txtEffDtEndorsementPrimary.Visible = false;
                Label16.Visible = false;
                imgCalendarEnd.Visible = false;
                imgCalendarEnd.Enabled = false;
                txtEffDt.Enabled = true;
                imgCalendarEff.Enabled = true;
            }
            txtDiscountCollComp.Visible = false;
            txtDiscountBIPD.Visible = false;
            lblCollCompDiscount.Visible = false;
            lblBiPdDiscount.Visible = false;
            txtMiscDiscount.Enabled = true;

            if (!cp.IsInRole("AUTO PERSONAL MISC DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                txtMiscDiscount.Visible = false;
                lblMiscDisc.Visible = false;
            }
            else
            {
                txtMiscDiscount.Visible = false;
                lblMiscDisc.Visible = false;
            }

            if (!cp.IsInRole("AUTO PERSONAL EMPLOYEE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlEmployeeDiscount.Visible = false;
                lblEmployeeDiscount.Visible = false;
            }
            else
            {
                ddlEmployeeDiscount.Visible = false;
                lblEmployeeDiscount.Visible = false;
            }

            if (!cp.IsInRole("AUTO PERSONAL EXPERIENCE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlExperienceDiscount.Visible = false;
                lblExperienceDiscount.Visible = false;
                TxtExpDisc.Visible = false;
            }
            else
            {
                ddlExperienceDiscount.Visible = true;
                lblExperienceDiscount.Visible = true;
                TxtExpDisc.Visible = true;
            }

            TxtAccidentDeathPremium.Enabled = true;
            ddlUninsuredSingle.Enabled = true;
            TxtUninsuredSingle.Enabled = true;
            ddlUninsuredSplit.Enabled = true;
            TxtUninsuredSplit.Enabled = true;
            ddlEquitmentSonido.Enabled = true;
            TxtEquitmentSonido.Enabled = true;
            ddlEquitmentAudio.Enabled = true;
            TxtEquitmentAudio.Enabled = true;
            chkEquipTapes.Enabled = true;
            TxtEquitmentTapes.Enabled = true;
            chkEquipColl.Enabled = true;
            TxtEquipColl.Enabled = true;
            chkEquipComp.Enabled = true;
            TxtEquipComp.Enabled = true;
            TxtCustomizeEquipLimit.Enabled = true;
            TxtEquipTotal.Enabled = true;

            //Desabilitar Road Assistance si Double Interest
            decimal Term = 0;
            if (txtTerm.Text != "")
            {
                Term = decimal.Parse(txtTerm.Text.ToString().Trim());
            }

            if (Term > 12 || QA.Term > 12)
            {
                chkAssist.Enabled = false;
                chkAssistEmp.Enabled = false;
            }
            else
            {
                chkAssistEmp.Enabled = true;
                chkAssist.Enabled = true;
            }

            chkLoJack.Enabled = true;
            txtLoJackCertificate.Enabled = true;
            TxtLojackExpDate.Enabled = true;
            //imgCalendarLJExp.Visible = true;

            SetEnableUninsured();
            SetEnableLojackFields();

            if (txtIsAssistanceEmp.Text.Trim() == "True")
            {
                ddlRoadAssistEmp.Enabled = false;
                ddlRoadAssist.Enabled = false;

            }
            else
            {
                ddlRoadAssistEmp.Enabled = false;
                ddlRoadAssist.Enabled = false;
            }
        }

        private void SetEnableBIPDCLS()
        {
            ddlBI.Enabled = true;
            ddlPD.Enabled = true;
            ddlCSL.Enabled = true;

            if (ddlBI.SelectedItem.Text.Trim() != "")
            {
                ddlBI.Enabled = true;
                ddlPD.Enabled = true;
                ddlCSL.Enabled = false;
            }

            if (ddlCSL.SelectedItem.Text.Trim() != "")
            {
                ddlBI.Enabled = false;
                ddlPD.Enabled = false;
                ddlCSL.Enabled = true;
            }
        }

        private void SetEnableUninsured()
        {
            ddlUninsuredSingle.Enabled = true;
            ddlUninsuredSplit.Enabled = true;

            if (ddlUninsuredSingle.SelectedItem.Text.Trim() != "")
            {
                ddlUninsuredSingle.Enabled = true;
                ddlUninsuredSplit.Enabled = false;
            }

            if (ddlUninsuredSplit.SelectedItem.Text.Trim() != "")
            {
                ddlUninsuredSingle.Enabled = false;
                ddlUninsuredSplit.Enabled = true;
            }
        }


        private void DisableControls()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];


            bool IsSupervisor = false;
            bool IsTarifador = false;
            bool IsSubscriptor = false;

            DataTable dtUnderWritterRules = null;

            if (cp.IsInRole("UNDERWRITTER RULES AUTO SUPERVISOR"))
            {
                IsSupervisor = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(1);
            }
            if (cp.IsInRole("UNDERWRITTER RULES AUTO SUBSCRIBER"))
            {
                IsSubscriptor = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(2);
            }
            if (cp.IsInRole("UNDERWRITTER RULES AUTO TARIFADOR"))
            {
                IsTarifador = true;
                dtUnderWritterRules = GetUnderWritterRulesByUnderWritterRuleID(3);
            }

            //Button
            btnNew.Visible = false;
            BtnExit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            HplAdd.Enabled = false;
            Linkbutton1.Enabled = false;
            btnVehicles.Visible = true;
            btnDrivers.Visible = true;
            btnPrint.Visible = true;
            btnViewCvr.Visible = true;

            if (!cp.IsInRole("AUTO PERSONAL QUOTE MODIFY") && !cp.IsInRole("ADMINISTRATOR"))
            {
                btnEdit.Visible = false;
            }
            else
            {
                btnEdit.Visible = true;
            }

            if (Session["OptimaPersonalPackage"] != null)
            {
                btnPrint.Visible = false;
                btnViewCvr.Visible = false;
                btnAuditTrail.Visible = false;
                cmdConvertToPolicy.Visible = false;
                BtnChangeToCustomer.Visible = false;
                cmdDefPlan.Visible = false;
                ddlTowing.Visible = true;
                ddlTowing.Enabled = false;
                txtTowingPrm.Visible = false;
                txtTowingPrm.Enabled = false;
            }
            else
            {
                btnPrint.Visible = true;
                btnViewCvr.Visible = true;
                btnAuditTrail.Visible = true;

                if (cp.IsInRole("CONVERT AUTO QUOTE TO POLICY") || cp.IsInRole("ADMINISTRATOR"))
                    cmdConvertToPolicy.Visible = true;
                else
                {
                    cmdConvertToPolicy.Visible = false;
                }


                if (Session["AUTOEndorsement"] != null)
                    cmdConvertToPolicy.Visible = false;

                this.BtnChangeToCustomer.Visible = false; // !this.ProspectHasParentCustomer();
                cmdDefPlan.Visible = false; //true;
                ddlTowing.Visible = true;
                ddlTowing.Enabled = false;
                txtTowingPrm.Visible = false;
                txtTowingPrm.Enabled = false;
            }

            //Driver Info	
            txtEffDt.Enabled = true;//FALSE
            txtTerm.Enabled = false;
            txtFirstNm.Enabled = false;
            txtLastNm1.Enabled = false;
            txtLastNm2.Enabled = false;
            ddlMaritalSt.Enabled = false;
            txtBirthDt.Enabled = false;
            txtSocialSecurity.Enabled = false;
            txtDriverAge.Enabled = false;
            ddlGender.Enabled = false;
            //chkOnlyOperator.Enabled = false;
            //ChkPrincipalOperator.Enabled = false;
            rdoOnlyOperatorY.Enabled = false;
            rdoOnlyOperatorN.Enabled = false;
            rdoPrincipalOperatorY.Enabled = false;
            rdoPrincipalOperatorN.Enabled = false;
            txtDriverPhone.Enabled = false;
            txtDriverEmail.Enabled = false;
            ddlLocation.Enabled = false;

            TxtpurchaseDate.Enabled = false;

            //Vehicle Info
            ddlInsuranceCompany.Enabled = false;
            TxtInsCode.Enabled = false;
            TxtVehicleRental.Enabled = false;
            TxtTowing.Enabled = false;
            ddlRental.Enabled = false;
            TxtVehiclesCount.Enabled = false;
            ddlPolicySubClass.Enabled = false;
            ddlMake.Enabled = false;
            ddlModel.Enabled = false;
            ddlYear.Enabled = false;
            txtAge.Enabled = false;
            ddlNewUsed.Enabled = false;
            txtPlate.Enabled = false;
            txtPurchaseDt.Enabled = false;
            ddlCompanyDealer.Enabled = false;
            ddlBank.Enabled = false;
            txtLicenseNumber.Enabled = false;
            txtExpDate.Enabled = false;
            txtCost.Enabled = false;
            txtVIN2.Enabled = false;
            txtActualValue.Enabled = false;
            ddlAlarm.Enabled = false;
            ddlVehicleClass.Enabled = false;
            ddlTerritory.Enabled = false;
            rdo15percent.Enabled = false;
            rdo20percent.Enabled = false;
            ddlSeatBelt.Visible = false;
            ddlAssistancePremium.Visible = false;
            //txtDeprec1st.Visible = false;
            //txtDeprecAll.Visible = false;
            ddlDriver.Enabled = false;
            txtEntryDt.Enabled = false;
            txtExpDt.Visible = false;
            //LblExpirationDT.Visible = false;
            txtPurchaseDt.Visible = false;
            TextBox1.Enabled = false;
            //Deductible & Limits
            ddlCollision.Enabled = false;
            ddlComprehensive.Enabled = false;
            txtDiscountCollComp.Enabled = false;
            ddlBI.Enabled = false;
            ddlPD.Enabled = false;
            ddlCSL.Enabled = false;
            txtDiscountBIPD.Enabled = false;
            ddlMedical.Enabled = false;
            ddlLoanGap.Enabled = false;
            chkLLG.Enabled = false;
            ddlPAR.Enabled = false;
            txtRoadsideAssitance.Enabled = false;
            txtTowingPrm.Enabled = false;
            TxtVehicleRental.Enabled = false;
            TxtTowing.Enabled = false;
            ddlRental.Enabled = false;
            imgCalendarEff.Enabled = false;

            if (QA.IsPolicy == false && Session["AUTOEndorsement"] != null)
            {
                txtEffDtEndorsementPrimary.Visible = true;
                txtEffDtEndorsementPrimary.Enabled = false;
                Label16.Visible = true;
                imgCalendarEnd.Enabled = false;
                imgCalendarEnd.Visible = true;
                cmdConvertToPolicy.Visible = false;

            }
            else
            {
                txtEffDtEndorsementPrimary.Visible = false;
                Label16.Visible = false;
                imgCalendarEnd.Enabled = false;
                imgCalendarEnd.Visible = false;
                // cmdConvertToPolicy.Visible = true;       

                if (cp.IsInRole("CONVERT AUTO QUOTE TO POLICY") || cp.IsInRole("ADMINISTRATOR"))
                    cmdConvertToPolicy.Visible = true;
                else
                {
                    cmdConvertToPolicy.Visible = false;
                }

                if (Session["AUTOEndorsement"] != null)
                    cmdConvertToPolicy.Visible = false;
            }

            ddlExperienceDiscount.Enabled = false;
            TxtExpDisc.Enabled = false;
            ddlEmployeeDiscount.Enabled = false;
            txtMiscDiscount.Enabled = false;

            if (!cp.IsInRole("AUTO PERSONAL MISC DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                txtMiscDiscount.Visible = false;
                lblMiscDisc.Visible = false;
            }
            else
            {
                //txtMiscDiscount.Visible = true;
                //lblMiscDisc.Visible = true;
            }

            if (!cp.IsInRole("AUTO PERSONAL EMPLOYEE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlEmployeeDiscount.Visible = false;
                lblEmployeeDiscount.Visible = false;
            }
            else
            {
                // ddlEmployeeDiscount.Visible = true;
                //lblEmployeeDiscount.Visible = true;
            }

            if (!cp.IsInRole("AUTO PERSONAL EXPERIENCE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlExperienceDiscount.Visible = false;
                lblExperienceDiscount.Visible = false;
                TxtExpDisc.Visible = false;
            }
            else
            {
                ddlExperienceDiscount.Visible = true;
                lblExperienceDiscount.Visible = true;
                TxtExpDisc.Visible = true;
            }
            //if (VerifyConvertPolicy(QA.TaskControlID))
            //{
            //    btnEdit.Visible = false;
            //    cmdConvertToPolicy.Visible = false;
            //    btnDrivers.Visible = false;
            //    btnVehicles.Visible = false;
            //}

            ddlRoadAssistEmp.Enabled = false;
            ddlRoadAssist.Enabled = false;
            ddlAccidentDeath.Enabled = false;
            ddlADPersons.Enabled = false;
            TxtAccidentDeathPremium.Enabled = false;
            ddlUninsuredSingle.Enabled = false;
            TxtUninsuredSingle.Enabled = false;
            ddlUninsuredSplit.Enabled = false;
            TxtUninsuredSplit.Enabled = false;
            ddlEquitmentSonido.Enabled = false;
            TxtEquitmentSonido.Enabled = false;
            ddlEquitmentAudio.Enabled = false;
            TxtEquitmentAudio.Enabled = false;
            chkEquipTapes.Enabled = false;
            TxtEquitmentTapes.Enabled = false;
            chkEquipColl.Enabled = false;
            TxtEquipColl.Enabled = false;
            chkEquipComp.Enabled = false;
            TxtEquipComp.Enabled = false;
            TxtCustomizeEquipLimit.Enabled = false;
            TxtEquipTotal.Enabled = false;
            chkAssist.Enabled = false;
            chkAssistEmp.Enabled = false;
            chkLoJack.Enabled = false;
            txtLoJackCertificate.Enabled = false;
            TxtLojackExpDate.Enabled = false;
            imgCalendarLJExp.Visible = false;
        }

        private void btnNew_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

        }

        protected void ddlInsuranceCompany_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TxtInsCode.Text = ddlInsuranceCompany.SelectedItem.Value.Trim();

            if (this.ddlInsuranceCompany.SelectedItem.Value != "")
            {
                this.ddlPolicySubClass.Enabled = true;
                //FillPolicySubClass();
            }
            else
                if (this.ddlInsuranceCompany.SelectedItem.Value == "")
            {
                this.ddlPolicySubClass.Enabled = false;
                ddlPolicySubClass.SelectedIndex = -1;
                //FillPolicySubClass();
            }
        }

        private void FillPolicySubClass()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            //if(ddlInsuranceCompany.Items[ddlInsuranceCompany.SelectedIndex].Text !="")
            //{
            //    DataTable dtPolSubClass = LookupTables.LookupTables.GetTable("PolicySubClass");
            //    DataTable dt = dtPolSubClass.Clone();
            //    DataRow row = null;

            //    for (int i = 0; i < dtPolSubClass.Rows.Count; i++)
            //    {					
            //        if (dtPolSubClass.Rows[i]["InsuranceCompanyID"].ToString().Trim() == ddlInsuranceCompany.SelectedItem.Value.Trim()
            //            || dtPolSubClass.Rows[i]["InsuranceCompanyID"].ToString().Trim() == "000")
            //        {
            //            if (ddlInsuranceCompany.SelectedItem.Value.Trim() == "200" &&
            //                dtPolSubClass.Rows[i]["InsuranceCompanyID"].ToString().Trim() == "000")
            //            {
            //                // No llena el combo con la opcion de FULL,DI y RP normales.
            //            }
            //            else
            //            {
            //                if (GetCurrentNumberOfAutos()>1 && dtPolSubClass.Rows[i]["AutoPolicyType"].ToString().Trim() == "DOUBLE INTEREST")
            //                {
            //                }
            //                else
            //                {
            //                    row = dt.NewRow();	

            //                    row["PolicySubClassID"]			= (int)dtPolSubClass.Rows[i]["PolicySubClassID"];
            //                    row["PolicySubClassDesc"]		= dtPolSubClass.Rows[i]["PolicySubClassDesc"].ToString();
            //                    row["AutoPolicyBaseSubClassID"]	= (int)dtPolSubClass.Rows[i]["AutoPolicyBaseSubClassID"];
            //                    row["IsMaster"]					= (bool)dtPolSubClass.Rows[i]["IsMaster"];
            //                    row["IsSpecial"]				= (bool)dtPolSubClass.Rows[i]["IsSpecial"];
            //                    row["CSLonly"]					= (bool)dtPolSubClass.Rows[i]["CSLonly"];
            //                    row["InsuranceCompanyID"]		= dtPolSubClass.Rows[i]["InsuranceCompanyID"].ToString();

            //                    dt.Rows.Add(row);
            //                }
            //            }
            //        }
            //    }

            //    ddlPolicySubClass.Items.Clear();

            //    ddlPolicySubClass.DataSource = dt; 
            //    ddlPolicySubClass.DataTextField = "PolicySubClassDesc";
            //    ddlPolicySubClass.DataValueField = "PolicySubClassID";
            //    ddlPolicySubClass.DataBind();
            //    ddlPolicySubClass.SelectedIndex = -1;
            //    ddlPolicySubClass.Items.Insert(0,"");
            //}
            //else
            //{
            DataTable dtPolSubClass = LookupTables.LookupTables.GetTable("PolicySubClass");

            ddlPolicySubClass.Items.Clear();

            ddlPolicySubClass.DataSource = dtPolSubClass;
            ddlPolicySubClass.DataTextField = "PolicySubClassDesc";
            ddlPolicySubClass.DataValueField = "PolicySubClassID";
            ddlPolicySubClass.DataBind();
            ddlPolicySubClass.SelectedIndex = -1;
            ddlPolicySubClass.Items.Insert(0, "");
            //}
        }

        protected void ddlMake_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillModel();
        }

        #region SetPolicyClassDiscount

        private void ddlPolicySubClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.SetDefaultFieldValues(this.ddlPolicySubClass.SelectedItem.Value.Trim());
        }

        private void SetDefaultFieldValues(string SelectedSubPolicyItemValue)
        {
            this.ResetDefaultValueFields();

            this.TxtpurchaseDate.Text = "5/1/" + DateTime.Now.Year.ToString();

            this.ddlYear.SelectedIndex = this.ddlYear.Items.IndexOf(
                this.ddlYear.Items.FindByText(DateTime.Now.Year.ToString()));

            this.ddlNewUsed.SelectedIndex = this.ddlNewUsed.Items.IndexOf(
                this.ddlNewUsed.Items.FindByText("New"));

            this.txtAge.Text = "0";

            rdo15percent.Checked = true;
            rdo20percent.Checked = false;
            this.txtDeprec1st.Value = "15";
            this.txtDeprecAll.Value = "15";

            try
            {
                AutoCover cover = new AutoCover();

                string insco = "000";
                if (ddlInsuranceCompany.SelectedItem.Value.Trim() == null || ddlInsuranceCompany.SelectedItem.Value.Trim() == "")
                {
                    insco = "000";
                }
                else
                {
                    insco = ddlInsuranceCompany.SelectedItem.Value.Trim();
                }

                if (SelectedSubPolicyItemValue.Trim() == "")
                    this.SetDiscountRelatedValues(0, false, ref cover, insco);
                else
                    this.SetDiscountRelatedValues(int.Parse(SelectedSubPolicyItemValue.Trim()), false, ref cover, insco);


                //Verifica PolicySubClass con el termino.
                if (ddlPolicySubClass.SelectedIndex > 0 && ddlPolicySubClass.SelectedItem != null && TxtInsCode.Text.Trim() != "")
                {
                    if (txtTerm.Text.Trim() == "")
                        txtTerm.Text = "0";

                    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 1)//DI
                    {
                        if (int.Parse(txtTerm.Text) < 24)
                        {
                            txtTerm.Text = "";
                        }
                    }
                    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 2)//LI
                    {
                        if (int.Parse(txtTerm.Text) > 12)
                        {
                            txtTerm.Text = "12";
                        }
                        else
                        {
                            txtTerm.Text = "12";
                        }
                    }
                    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 3)//FC
                    {
                        if (int.Parse(txtTerm.Text) > 12)
                        {
                            txtTerm.Text = "12";
                        }
                        else
                        {
                            txtTerm.Text = "12";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not parse SubPolicyID value into integer.", ex);
            }
        }

        private void ResetDefaultValueFields()
        {
            this.txtDiscountBIPD.Text = string.Empty;
            this.txtDiscountCollComp.Text = string.Empty;
            this.SetDdlSelectedItemByText(this.ddlLoanGap, string.Empty);
            this.SetDdlSelectedItemByText(this.ddlCSL, string.Empty);
            this.SetDdlSelectedItemByText(this.ddlMedical, string.Empty);
            //this.ddlAssistancePremium.SelectedIndex = 0;
            this.txtRoadsideAssitance.Text = string.Empty;
            this.txtTowingPrm.Text = string.Empty;
            this.TxtVehicleRental.Text = string.Empty;
            TxtTowing.Text = string.Empty;
            this.SetDdlSelectedItemByText(this.ddlRental, string.Empty);
            this.SetDdlSelectedItemByText(this.ddlPAR, string.Empty);

            ddlRoadAssistEmp.SelectedIndex = -1;
            ddlRoadAssist.SelectedIndex = -1;
            ddlAccidentDeath.SelectedIndex = -1;
            ddlADPersons.SelectedIndex = -1;
            TxtAccidentDeathPremium.Text = "";
            ddlUninsuredSingle.SelectedIndex = -1;
            TxtUninsuredSingle.Text = "";
            ddlUninsuredSplit.SelectedIndex = -1;
            TxtUninsuredSplit.Text = "";
            ddlEquitmentSonido.SelectedIndex = -1;
            TxtEquitmentSonido.Text = "";
            ddlEquitmentAudio.SelectedIndex = -1;
            TxtEquitmentAudio.Text = "";
            chkEquipTapes.Checked = false;
            TxtEquitmentTapes.Text = "";
            chkEquipColl.Checked = false;
            TxtEquipColl.Text = "";
            chkEquipComp.Checked = false;
            TxtEquipComp.Text = "";
            TxtCustomizeEquipLimit.Text = "";
            TxtEquipTotal.Text = "";

            chkLoJack.Checked = false;
            txtLoJackCertificate.Text = "";
            TxtLojackExpDate.Text = "";

            this.ddlEmployeeDiscount.SelectedIndex = -1;
            this.ddlExperienceDiscount.SelectedIndex = -1;
            TxtExpDisc.Text = "0";
            this.txtMiscDiscount.Text = string.Empty;
        }

        private void SetDiscountRelatedValues(int PolicySubClassID, bool ApplyToCover, ref AutoCover CoverToApply, string insco)
        {
            //if (Session["TaskControl"] != null)
            //{
            //    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            //    bool qualifiesForMultiAutoBIPDdiscount = this.QualifiesFor_MoreThanOneAuto_Discount(true,
            //        QA.AutoCovers, CoverToApply);
            //    bool qualifiesForMultiAutoCOLLCOMPdiscount = this.QualifiesFor_MoreThanOneAuto_Discount(false,
            //        QA.AutoCovers, CoverToApply);
            //    int currentNumberOfAutos = this.GetCurrentNumberOfAutos();

            //    // 1 - DI
            //    // 2 - Liability
            //    // 3 - Full Cover

            //    switch (insco)
            //    {
            //        case "052": //AIICO - EMPLEADO; DI
            //            if (PolicySubClassID == 1)      //DI
            //            {
            //                this.SetValues(0, 10, 0, 0, 0.00M, 0, 0, 0, 0, 30, ApplyToCover, ref CoverToApply);
            //            }
            //            else if (PolicySubClassID == 3)  //FC
            //            {
            //                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 48.0M : 35.0M,
            //                    qualifiesForMultiAutoCOLLCOMPdiscount ? 48.0M : 35.0M,
            //                    0, 0, 0.00M, 0, 0, 0, 0, 30, ApplyToCover, ref CoverToApply);
            //            }
            //            else
            //            {
            //                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 20 : 0,
            //                    qualifiesForMultiAutoCOLLCOMPdiscount ? 20 : 0, 0, 0,
            //                    0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //            }
            //            break;

            //        case "056": //PRICO - LI

            //            if (PolicySubClassID == 1)      //DI
            //            {
            //                this.SetValues(0, 17.5M, 0, 0, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //            }
            //            else if (PolicySubClassID == 2)  //LI
            //            {
            //                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34 : 17.5M,
            //                    0, 0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0, 0, 0, 0,
            //                    ApplyToCover, ref CoverToApply);
            //            }
            //            else if (PolicySubClassID == 3)  //FC
            //            {
            //                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34M : 17.5M,
            //                    qualifiesForMultiAutoCOLLCOMPdiscount ? 34M : 17.5M,
            //                    0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0,
            //                    0, 0, 0, ApplyToCover, ref CoverToApply);
            //            }
            //            break;

            //        case "066": //PRICO - LI

            //            if (PolicySubClassID == 1)      //DI
            //            {
            //                this.SetValues(0, 17.5M, 0, 0, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //            }
            //            else if (PolicySubClassID == 2)  //LI
            //            {
            //                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34 : 17.5M,
            //                    0, 0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0, 0, 0, 0,
            //                    ApplyToCover, ref CoverToApply);
            //            }
            //            else if (PolicySubClassID == 3)  //FC
            //            {
            //                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34M : 17.5M,
            //                    qualifiesForMultiAutoCOLLCOMPdiscount ? 34M : 17.5M,
            //                    0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0,
            //                    0, 0, 0, ApplyToCover, ref CoverToApply);
            //            }
            //            break;

            //        default:
            //            if (Session["OptimaPersonalPackage"] != null)
            //            {
            //                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 40 : 20,
            //                qualifiesForMultiAutoCOLLCOMPdiscount ? 40 : 20, 0, 0,
            //                0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //            }
            //            else
            //            {
            //                if (QA.PolicyType.Trim() == "MFE")
            //                {
            //                    this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 40 : 60,
            //                        qualifiesForMultiAutoCOLLCOMPdiscount ? 40 : 60, 0, 0,
            //                        0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //                }
            //                else
            //                {
            //                    this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 0 : 0,
            //                        qualifiesForMultiAutoCOLLCOMPdiscount ? 0 : 0, 0, 0,
            //                        0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //                }
            //            }
            //            break;
            //        //
            //        //					case 38: //PRAICO - AUTO PLUS 1; LI (CSL ONLY)
            //        //						this.SetValues(0, 0 , 5, 25, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //        //						break;
            //        //					case 39: //PRAICO - AUTO PLUS 2; LI (CSL ONLY)
            //        //						this.SetValues(0, 0 , 10, 25, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //        //						break;
            //        //					case 40: //PRAICO - AUTO PLUS 3; LI (CSL ONLY)
            //        //						this.SetValues(0, 0 , 15, 25, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //        //						break;
            //        //					case 41: //PRAICO - AUTO PLUS 4; LI (CSL ONLY)
            //        //						this.SetValues(0, 0 , 20, 25, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //        //						break;
            //        //					case 63: //MASTER EUROLEASE (AIICO) - LI
            //        //						this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 
            //        //							36 : 20, 0, 0, 25, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //        //						break;
            //        //					case 64: //MASTER EUROLEASE (AIICO) - FC
            //        //						this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 36 : 20, 
            //        //							qualifiesForMultiAutoCOLLCOMPdiscount ? 36 : 20, 0, 25, 
            //        //							0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
            //        //						break;
            //    }
            //}
        }

        private bool QualifiesFor_MoreThanOneAuto_Discount(bool BIPD, ArrayList AutoCovers, AutoCover ProposedCover)
        {
            if (this.GetCurrentNumberOfAutos() > 1)
                return true;
            else
                return false;
        }

        private int GetCurrentNumberOfAutos()
        {
            if (TxtVehiclesCount.Text.Trim() == "")
            {
                return 1;
            }
            else
            {
                return int.Parse(TxtVehiclesCount.Text);
            }
        }

        public enum DiscountType { BIPD, COLLCOMP }

        private void SetValues(decimal BiPdDisc, decimal CollCompDisc, int CslLimit,
            int VehicleRental, decimal LlGap, int Medical, int RoadAssistance,
            int TowPremium, int SeatBelt, int PAR, bool ApplyToCover,
            ref AutoCover CoverToApply)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            if (ApplyToCover && CoverToApply != null)
            {
                if (!cp.IsInRole("AUTOEDITDISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    CoverToApply.DiscountBIPD = BiPdDisc;
                    CoverToApply.DiscountCompColl = CollCompDisc;
                }
            }
            else
            {
                if (!cp.IsInRole("AUTOEDITDISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    if (BiPdDisc != 0)
                        this.txtDiscountBIPD.Text = BiPdDisc.ToString();
                    if (CollCompDisc != 0)
                        this.txtDiscountCollComp.Text = CollCompDisc.ToString();
                }
                if (CslLimit != 0)
                    this.SetDdlSelectedItemByValue(this.ddlCSL, CslLimit.ToString());
                if (LlGap != 0)
                    this.SetDdlSelectedItemByValue(this.ddlLoanGap, LlGap.ToString());
                //this.MakeOnlyChoice(this.ddlLoanGap, LlGap.ToString(), string.Empty);
                if (Medical != 0)
                    this.SetDdlSelectedItemByValue(this.ddlMedical, Medical.ToString());
                //this.MakeOnlyChoice(this.ddlMedical, Medical.ToString(), string.Empty);
                if (RoadAssistance != 0)
                    txtRoadsideAssitance.Text = RoadAssistance.ToString();
                //this.SetDdlSelectedItemByValue(this.ddlAssistancePremium,RoadAssistance.ToString());
                //this.MakeOnlyChoice(this.ddlAssistancePremium,RoadAssistance.ToString(), string.Empty);			

                if (Session["OptimaPersonalPackage"] != null)
                {
                    if (TowPremium != 0)
                        ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(ddlTowing.Items.FindByValue(TowPremium.ToString()));
                }
                else
                {
                    if (TowPremium != 0)
                    {
                        //this.TxtTowing.Text = TowPremium.ToString();
                        this.TxtTowing.Text = TowPremium.ToString();
                        ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(ddlTowing.Items.FindByValue(TowPremium.ToString()));
                    }
                }

                if (VehicleRental != 0)
                {
                    this.TxtVehicleRental.Text = VehicleRental.ToString();
                    ddlRental.SelectedIndex = ddlRental.Items.IndexOf(ddlRental.Items.FindByValue(VehicleRental.ToString()));
                }
                if (SeatBelt != 0)
                    this.SetDdlSelectedItemByText(this.ddlSeatBelt, SeatBelt.ToString());
                if (PAR != 0)
                    this.SetDdlSelectedItemByText(this.ddlPAR, PAR.ToString());
                //this.MakeOnlyChoice(this.ddlPAR, PAR.ToString(), string.Empty);
            }
        }

        private void SetDdlSelectedItemByValue(System.Web.UI.WebControls.DropDownList Ddl, string Val)
        {
            for (int i = 0; i < Ddl.Items.Count; i++)
            {
                if (Ddl.Items[i].Text.Trim() == Val.Trim())
                {
                    Ddl.SelectedIndex = i;
                    return;
                }
            }
        }

        private int GetBasePolicySubClassFromPolicySubClassID(int PolicySubClassID)
        {
            DbRequestXmlCookRequestItem[] items = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("ID", SqlDbType.Int, 0,
                PolicySubClassID.ToString(), ref items);
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetBasePolicySubClassFromPolicySubClassID",
                DbRequestXmlCooker.Cook(items)).Rows.Count > 0 ?
                int.Parse(exec.GetQuery("GetBasePolicySubClassFromPolicySubClassID",
                DbRequestXmlCooker.Cook(items)).Rows[0]["AutoPolicyBaseSubClassID"].ToString().Trim()) :
                0;
        }

        private void MakeOnlyChoice(System.Web.UI.WebControls.DropDownList list, string ChoiceText, string ChoiceValue)
        {
            if (list.Items.Count > 0)
            {
                ArrayList itemsToDelete = new ArrayList();

                if (ChoiceText != string.Empty)
                {
                    foreach (System.Web.UI.WebControls.ListItem item in list.Items)
                    {
                        if (item.Text.Trim() != ChoiceText.Trim() &&
                            item.Text != string.Empty)
                            itemsToDelete.Add(item);
                    }
                }
                else if (ChoiceValue != string.Empty)
                {
                    foreach (System.Web.UI.WebControls.ListItem item in list.Items)
                    {
                        if (item.Value.Trim() != ChoiceValue.Trim() &&
                            item.Text != string.Empty)
                            itemsToDelete.Add(item);
                    }
                }
                for (int i = 0; i < itemsToDelete.Count; i++)
                    list.Items.Remove((System.Web.UI.WebControls.ListItem)
                        itemsToDelete[i]);
            }
        }
        #endregion

        protected void TxtInsCode_TextChanged(object sender, System.EventArgs e)
        {
            SetInsCo(this.TxtInsCode.Text.Trim());

            if (this.TxtInsCode.Text != "")
            {
                this.ddlPolicySubClass.Enabled = true;
                //FillPolicySubClass();
            }
            else
                if (this.TxtInsCode.Text == "")
            {
                this.ddlPolicySubClass.Enabled = false;
                ddlPolicySubClass.SelectedIndex = -1;
                //FillPolicySubClass();
            }
        }

        private void SetInsCo(string InsCode)
        {
            if (InsCode != "0" && InsCode != "")
                ddlInsuranceCompany.SelectedIndex = ddlInsuranceCompany.Items.IndexOf(
                    ddlInsuranceCompany.Items.FindByValue(InsCode.ToString()));
            else
                ddlInsuranceCompany.SelectedIndex = -1;
        }

        private void TxtVehiclesCount_TextChanged(object sender, System.EventArgs e)
        {
            EnableControls();

            if (ddlPolicySubClass.SelectedItem.Text == "")
            {
                TxtInsCode.Text = "";
                ddlInsuranceCompany.SelectedIndex = -1;
                ddlPolicySubClass.SelectedIndex = -1;
                txtDiscountBIPD.Text = "";
                txtDiscountCollComp.Text = "";
            }
            else
            {
                this.SetDefaultFieldValues(this.ddlPolicySubClass.SelectedItem.Value.Trim());
            }
        }

        private void ddlModel_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void txtTerm_TextChanged(object sender, System.EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, System.EventArgs e)
        {
            ModifyQuotes();
        }

        private void ModifyQuotes()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            QA.Mode = (int)TaskControl.TaskControl.TaskControlMode.UPDATE;

            //Se le quit� la coma para que pudiera calcular el costo original.
            if (txtCost.Text.Trim() != "")
                txtCost.Text = (decimal.Parse(txtCost.Text)).ToString("######");         //("###,###")
            if (txtActualValue.Text.Trim() != "")
                txtActualValue.Text = (decimal.Parse(txtActualValue.Text)).ToString("######");   //("###,###")

            Session["TaskControl"] = QA;

            string a = _InternalID.Value.ToString();

            EnableControls();
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
			SaveAllQuote();
        }

		private void SaveAllQuote()
        {
            try
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                if (this.ValidateThis())
                {
                    //Es para calcular el Endoso prorrateado antes de guardar el quotes.
                    Login.Login cp = HttpContext.Current.User as Login.Login;
                    int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                    double mFactor = 0.0;
                    double NewProRataTotPrem = 0.0;
                    double NewShotRateTotPrem = 0.0;

                    if (QA.IsPolicy == false && Session["AUTOEndorsement"] != null)
                    {
                        if (txtEffDtEndorsementPrimary.Text.Trim() == "")
                            throw new Exception("Endorsement Date is missing or wrong.");
                    }

                    SaveQuoteData();
                    this.VerifyCoverVsPremium();
                    this.VerifyProspectID();

                    //////////////////
                    //Salvar AUTOEndorsement
                    QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    int taskControlIDPolicy = 0;
                    if (QA.IsPolicy == false && Session["AUTOEndorsement"] != null)
                    {
                        // Esta seccion es porque existe ya en la base de datos
                        // y no hay que insertar nuevamente el quotes.
                        if (Session["OPPEndorUpdate"] == null)
                        {
                            EPolicy.TaskControl.QuoteAuto OpptaskControl = (EPolicy.TaskControl.QuoteAuto)Session["AUTOEndorsement"];
                            AddOPPEndorsement(OpptaskControl.TaskControlID, QA.TaskControlID, mFactor, NewProRataTotPrem, NewShotRateTotPrem);
                            taskControlIDPolicy = OpptaskControl.TaskControlID;
                        }
                        else
                        {
                            EPolicy.TaskControl.QuoteAuto OpptaskControl = (EPolicy.TaskControl.QuoteAuto)Session["AUTOEndorsement"];
                            taskControlIDPolicy = OpptaskControl.TaskControlID;
                            Session.Remove("OPPEndorUpdate");
                        }
                    }
                   
                }

                // REMOVER SI NO FUNCIONA

                fillDataGrid(QA.AutoCovers);

                lblRecHeader.Text = "Quote saved Successfully!";
                mpeSeleccion.Show();
            }
            catch (Exception xcp)
            {
                //litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message.Trim());
                //litPopUp.Visible = true;
                lblRecHeader.Text = xcp.Message.Trim();
                mpeSeleccion.Show();
            }            
        }

        private void CalculateCharge()
        {
            try
            {
                EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];

                bool isnew = true;
                //if (taskControl.Suffix != "00")
                //isnew = false;

                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[4];
                DbRequestXmlCooker.AttachCookItem("policyClassID", SqlDbType.Int, 0, taskControl.PolicyClassID.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("PolicyType", SqlDbType.Char, 3, "AUT", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("IsNew", SqlDbType.Bit, 0, isnew.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("effectiveDate", SqlDbType.DateTime, 0, txtEffDt.Text.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetChargeNew", xmlDoc);
                    if (dt.Rows.Count > 0)
                    {
                        double charge = ((double)dt.Rows[0]["ChargePercent"]) / 100;
                        string totpre = taskControl.TotalPremium.ToString();
                        double totcharge = Math.Round((double.Parse(totpre) * charge), 0);
                        txtCharge.Text = totcharge.ToString("###,###.00");
                    }
                    else
                    {
                        txtCharge.Text = "0.0";
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("No pudo encontrar la informaci�n, Por favor trate de nuevo.", ex);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (QA.Mode == 1) //ADD
            {
                if (Session["OptimaPersonalPackage"] != null)
                {
                    RemoveSessionLookUp();
                    EPolicy.TaskControl.PersonalPackage optimaPersonalPackage = (EPolicy.TaskControl.PersonalPackage)Session["OptimaPersonalPackage"];
                    Session.Remove("TaskControl");
                    Session.Add("TaskControl", optimaPersonalPackage);
                    Response.Redirect("PersonalPackage.aspx");
                }
                else
                {
                    Session.Clear();
                    Response.Redirect("Search.aspx");
                }
            }
            else
            {
                QA.Mode = (int)TaskControl.TaskControl.TaskControlMode.CLEAR;
                Session["TaskControl"] = QA;

                FillTextControl();
                DisableControls();
            }
        }

        protected void BtnExit_Click(object sender, System.EventArgs e)
        {
            this.litPopUp.Visible = false;
            RemoveSessionLookUp();

            if (Session["OptimaPersonalPackage"] != null)
            {
                EPolicy.TaskControl.QuoteAuto QA = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
                EPolicy.TaskControl.PersonalPackage optimaPersonalPackage = (EPolicy.TaskControl.PersonalPackage)Session["OptimaPersonalPackage"];

                optimaPersonalPackage.QuoteAuto = QA;
                optimaPersonalPackage.TaskControlIDQuoteAuto = QA.TaskControlID;
                optimaPersonalPackage.ProspectID = QA.Prospect.ProspectID;
                optimaPersonalPackage.Prospect.ProspectID = QA.Prospect.ProspectID;

                Session.Remove("TaskControl");
                Session.Add("TaskControl", optimaPersonalPackage);
                Response.Redirect("PersonalPackage.aspx");
            }
            else
            {
                if (Session["AUTOEndorsement"] != null)
                {
                    //Para que vuelva a la poliza original.
                    Login.Login cp = HttpContext.Current.User as Login.Login;
                    int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                    EPolicy.TaskControl.QuoteAuto OpptaskControl = (EPolicy.TaskControl.QuoteAuto)Session["AUTOEndorsement"];
                    int taskControlIDPolicy = OpptaskControl.TaskControlID;
                    Session.Remove("AUTOEndorsement");
                    Session.Clear();

                    EPolicy.TaskControl.QuoteAuto QA = (EPolicy.TaskControl.QuoteAuto)TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControlIDPolicy, UserID);
                    QA.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                    QA.Policy.IsEndorsement = false;
                    Session["TaskControl"] = QA;
                    Response.Redirect("QuoteAuto.aspx", false);
                }
                else
                {
                    Session.Clear();
                    Response.Redirect("Search.aspx");
                }
            }
        }

        protected void btnVehicles_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            AutoCover AC = (AutoCover)QA.AutoCovers[0];

            if (ddlCollision.SelectedItem.Text != "" && ddlBI.SelectedItem.Text == "" && ddlCSL.SelectedItem.Text == "")
            {
                //Si es Double Interest No se debe a�adir otro veh�culo.
                litPopUp.Text = Utilities.MakeLiteralPopUpString("For Double Interest policy is allowed only one vehicle.");
                litPopUp.Visible = true;
                lblRecHeader.Text = "For Double Interest policy is allowed only one vehicle.";
                mpeSeleccion.Show();
            }
            else
            {
                RemoveSessionLookUp();
                Session["InternalAutoID"] = AC.InternalID;
                Response.Redirect("QuoteAutoVehicles.aspx");
            }
        }

        protected void btnDrivers_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            AutoCover AC = (AutoCover)QA.AutoCovers[0];
            Session["InternalAutoID"] = AC.InternalID;

            RemoveSessionLookUp();
            Response.Redirect("QuoteAutoDrivers.aspx");
        }

        protected void btnPrint_Click(object sender, System.EventArgs e)
        {
            try
            {
                List<string> mergePaths = new List<string>();
                EPolicy.TaskControl.Quote taskControl = (EPolicy.TaskControl.Quote)Session["TaskControl"];
                EPolicy.TaskControl.QuoteAuto qa = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];

                if (Session["AUTOEndorsement"] == null)
                {
                    int taskControlID = taskControl.TaskControlID;

                    if (taskControl.Term > 12)
                    {
                        mergePaths = ImprimirDoubleInterestQuotes(mergePaths, qa, taskControl);
                    }
                    else
                    {
                        mergePaths = ImprimirAutoQuote(mergePaths, taskControlID);
                        mergePaths = ImprimirAutoQuoteSolicitud(mergePaths, taskControlID);
                       

                    }
                }
                else
                {
                    // EndorsementID = int.Parse(Session["OPPEndorsementID"].ToString());
                    mergePaths = ImprimirAutoQuoteEndorsement(mergePaths, taskControl.TaskControlID, EndorsementID);
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
                lblRecHeader.Text = exc.Message.Trim() + " - " + exc.InnerException.ToString();
                mpeSeleccion.Show();
            }
        }

        protected void btnAuditTrail_Click(object sender, System.EventArgs e)
        {
            if (Session["TaskControl"] != null)
            {
                RemoveSessionLookUp();
                TaskControl.Quote quote = (TaskControl.Quote)Session["TaskControl"];
                Response.Redirect("SearchAuditItems.aspx?type=17&taskControlID=" +
                    quote.TaskControlID.ToString());
            }
        }

        protected void btnViewCvr_Click(object sender, System.EventArgs e)
        {
            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                QA.Calculate();

                AutoCover AC = (AutoCover)QA.AutoCovers[0];

                if (AC.Breakdown.Count > 0)
                {
                    Session.Add("InternalAutoID", AC.InternalID.ToString());
                    Session.Add("FromPage", "ExpressAutoQuote.aspx");
                    RemoveSessionLookUp();
                    Response.Redirect("QuoteAutoBreakdown.aspx");
                }
                else
                {
                    lblRecHeader.Text = "The selected vehicle has no calculated premium.";
                    mpeSeleccion.Show();
                }
            }
        }

        protected void cmdConvertToPolicy_Click(object sender, System.EventArgs e)
        {
            try
            {
                TaskControl.QuoteAuto taskControl = new TaskControl.QuoteAuto(true);  //Policy
                TaskControl.QuoteAuto TempPolicy; //= new TaskControl.QuoteAuto(true);  //Policy Temp Object

                TempPolicy = taskControl;
                TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto)Session["TaskControl"];
                Login.Login cp = HttpContext.Current.User as Login.Login;
                            

                if (VerifyConvertPolicy(quoteAuto.TaskControlID))
                {
                    throw new Exception("This quotes has already been converted to policy.");
                }
                else
                {

                    taskControl.IsPolicy = true;

                    taskControl.Agency = quoteAuto.Agency;
                    taskControl.Agent = quoteAuto.Agent;
                    taskControl.Bank = quoteAuto.Bank;
                    taskControl.CompanyDealer = quoteAuto.CompanyDealer;
                    taskControl.InsuranceCompany = quoteAuto.InsuranceCompany;

                    taskControl.Customer = quoteAuto.Customer;
                    taskControl.Customer = SaveCustomer(); // quoteAuto.Customer;
                    taskControl.CustomerNo = quoteAuto.Customer.CustomerNo; // quoteAuto.CustomerNo;
                    
                    taskControl.Prospect = quoteAuto.Prospect;
                    taskControl.ProspectID = quoteAuto.ProspectID;
                    taskControl.Term = quoteAuto.Term;
                    taskControl.EffectiveDate = quoteAuto.EffectiveDate;
                    taskControl.ExpirationDate = quoteAuto.ExpirationDate;
                    taskControl.Charge = quoteAuto.Charge;
                    taskControl.TotalPremium = Decimal.Round(quoteAuto.TotalPremium, 2);
                    taskControl.PolicyType = quoteAuto.PolicyType;


                    
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt = GetPolicyQuoteByTaskControlID(quoteAuto.TaskControlID);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Ren_Rei"].ToString().Trim()=="RN")
                        {
                                Policy pol = Policy.GetPolicyQuoteByTaskControlID(quoteAuto.TaskControlID, taskControl.Policy); 
                                taskControl.Policy.AutoAssignPolicy = false;
                                taskControl.Policy.PolicyType = pol.PolicyType;
                                taskControl.Policy.PolicyNo = pol.PolicyNo;
                                taskControl.Policy.Suffix = DateTime.Parse(taskControl.EffectiveDate.ToString()).Year.ToString().Substring(2, 2);
                            //taskControl.Policy.AutoAssignPolicy = false;
                            //taskControl.Policy.PolicyType =dt.Rows[0]["PolicyType"].ToString();
                            //taskControl.Policy.PolicyNo = dt.Rows[0]["PolicyNo"].ToString();
                            //taskControl.Policy.Suffix = DateTime.Parse(taskControl.EffectiveDate.ToString()).Year.ToString().Substring(2, 2);  
                        }
                    }
                    
					
                    if (ddlLocation.SelectedItem.Value != "")
                        taskControl.Policy.OriginatedAt = int.Parse(ddlLocation.SelectedItem.Value);
                    else
                        taskControl.Policy.OriginatedAt = 0;

                    taskControl.Policy.IsMaster = false; // quoteAuto.IsMaster;
                    taskControl.Policy.MasterPolicyID = "";
                    taskControl.Policy.PolicyType = quoteAuto.PolicyType;
                    taskControl.Policy.FileNumber = quoteAuto.FileNumber;

                    taskControl.Policy.PolicyCicleEnd = 1; //Para que en la pantalla de auto no entre de modo new.
                    taskControl.Policy.Agent = "000";//quoteAuto.Agent;
                    taskControl.Policy.Agency = quoteAuto.Agency;
                    taskControl.Policy.InsuranceCompany = "001";  //MISC INS.
                    taskControl.Policy.TCIDQuotes = quoteAuto.TaskControlID;
                    taskControl.Policy.PMT1 = 0;

                    taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();
                    
                    //Fill Auto Information from Application Auto or of the Quote.
                    Quotes.AutoDriver AD = null;
                    Quotes.AutoCover AC = null;

                    //Para guardar el auto, driver, assingdriver, breakdown, temporeramente en la poliza.
                    if (quoteAuto.TaskControlID != 0)
                    {
                        //Para asignar un nuevo QuoteID.
                        if (taskControl.QuoteId == 0)
                        {
                            taskControl.QuoteId = taskControl.Policy.GetPolicyIDTemp();
                        }

                        if (quoteAuto.Drivers.Count != 0)	 //Fill Driver Info. From Quote.
                        {
                            for (int i = 0; i < quoteAuto.Drivers.Count; i++)
                            {
                                Quotes.AutoDriver ADPolicy = new Quotes.AutoDriver();
                                AD = (AutoDriver)quoteAuto.Drivers[i];

                                taskControl = FillAutoDriver(AD, ADPolicy, taskControl, quoteAuto);
                            }
                        }

                        if (quoteAuto.AutoCovers.Count != 0)	 //Fill Auto Info. From Quote.
                        {
                            ArrayList sorted = new ArrayList();

                            for (int i = 0; i < quoteAuto.AutoCovers.Count; i++)
                            {
                                sorted.Add(((AutoCover)quoteAuto.AutoCovers[i]).QuotesAutoId);
                            }

                            sorted.Sort();


                            for (int i = 0; i < quoteAuto.AutoCovers.Count; i++)  //taskControl.AutoCovers.Count
                            {

                                for (int j = 0; j < quoteAuto.AutoCovers.Count; j++)

                                    if (int.Parse(sorted[i].ToString().Trim()) == ((AutoCover)quoteAuto.AutoCovers[j]).QuotesAutoId)
                                    {
                                        Quotes.AutoCover ACPolicy = new Quotes.AutoCover();
                                        AC = (AutoCover)quoteAuto.AutoCovers[j];
                                        taskControl = FillAutoCover(AC, ACPolicy, taskControl, quoteAuto);

                                    }

                            }
                        }
                    }
                    else							  //Fill Auto Info. From Application Auto.
                    {
                        //						AC = new Quotes.AutoCover();
                        //					
                        //						AC.VIN			= applicationAuto.VIN;
                        //						AC.VehicleMake	= applicationAuto.VehicleMakeID;
                        //						AC.VehicleModel = applicationAuto.VehicleModelID;
                        //						AC.VehicleYear  = applicationAuto.VehicleYearID;
                        //						AC.Territory    = applicationAuto.Territory;
                        //						AC.VehicleClass = applicationAuto.VehicleUse;
                        //						AC.Bank			= applicationAuto.Bank;
                        //						AC.CompanyDealer= applicationAuto.CompanyDealer;
                        //
                        //						Session.Add("AutoCoverFromApp",AC);
                    }
                    Session.Remove("TaskControl");
                    Session["TaskControl"] = taskControl;
                    RemoveSessionLookUp();

                    //Response.Redirect("ExpressAutoQuote.aspx", false);
                    Response.Redirect("QuoteAuto.aspx", false);
                }
            }
            catch (Exception ex)
            {
                //this.litPopUp.Text = Utilities.MakeLiteralPopUpString(ex.Message);
                //this.litPopUp.Visible = true;
                lblRecHeader.Text = ex.Message.Trim();
                mpeSeleccion.Show();
            }
        }

       
        public bool VerifyConvertPolicy(int TCIDQuotes)
        {
            bool Exist = false;

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlQuotes", SqlDbType.Int, 0, TCIDQuotes.ToString(), ref cookItems);

            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }


            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                //Executor.BeginTrans();
                DataTable dt = Executor.GetQuery("GetVerifyConvertToPolicy", xmlDoc);
                Executor.CommitTrans();

                if (int.Parse(dt.Rows[0]["Available"].ToString()) == 0)
                    Exist = false;
                else
                    Exist = true;
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save Endorsement Quote. " + xcp.Message, xcp);
            }


            return Exist;
        }
        private bool ProspectHasParentCustomer()
        {
            //TaskControl.ApplicationAuto taskControl = (TaskControl.ApplicationAuto) Session["TaskControl"];
            TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (taskControl.Prospect != null)
            {
                EPolicy.Customer.Prospect prospect = (EPolicy.Customer.Prospect)taskControl.Prospect;

                if (prospect.CustomerNumber.Trim() != "None")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private TaskControl.QuoteAuto FillAutoDriver(Quotes.AutoDriver AD, Quotes.AutoDriver adpolicy,
            TaskControl.QuoteAuto taskControl, TaskControl.QuoteAuto quoteAuto)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            adpolicy.FirstName = AD.FirstName;
            adpolicy.LastName1 = AD.LastName1;
            adpolicy.LastName2 = AD.LastName2;

            adpolicy.HomePhone = AD.HomePhone;
            adpolicy.LocationID = AD.LocationID;

            adpolicy.BirthDate = AD.BirthDate;
            adpolicy.Gender = AD.Gender;
            adpolicy.MaritalStatus = AD.MaritalStatus;
            adpolicy.Email = AD.Email;
            adpolicy.License = AD.License;
            adpolicy.SocialSecurity = AD.SocialSecurity;

            adpolicy.QuoteID = taskControl.QuoteId;

            adpolicy.ProspectID = AD.ProspectID;
            ((EPolicy.Customer.Prospect)AD).Mode = (int)EPolicy.Customer.Prospect.ProspectMode.UPDATE;
            adpolicy.Mode = (int)Enumerators.Modes.Insert;

            taskControl.RemoveDriver(adpolicy);
            taskControl.AddDriver(adpolicy);

            taskControl.Mode = (int)Enumerators.Modes.Insert;

            taskControl.Save(userID, null, adpolicy, false);

            return taskControl;
        }

        private TaskControl.QuoteAuto FillAutoCover(Quotes.AutoCover AC, Quotes.AutoCover acpolicy,
            TaskControl.QuoteAuto taskControl, TaskControl.QuoteAuto quoteAuto)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            acpolicy.QuotesId = taskControl.QuoteId;
            acpolicy.VIN = AC.VIN;
            acpolicy.VehicleMake = AC.VehicleMake;
            acpolicy.VehicleModel = AC.VehicleModel;
            acpolicy.VehicleYear = AC.VehicleYear;
            acpolicy.Territory = AC.Territory; int.Parse(this.ddlTerritory.SelectedItem.Value);   //AC.Territory;
            acpolicy.VehicleClass = AC.VehicleClass; // this.ddlVehicleClass.SelectedItem.Value;		        //AC.VehicleClass;
            acpolicy.Bank = AC.Bank;                                           //this.ddlBank.SelectedItem.Value;			
            acpolicy.CompanyDealer = AC.CompanyDealer;                                  //this.ddlCompanyDealer.SelectedItem.Value;  

            acpolicy.License = AC.License;
           // acpolicy.LicenseExpDate = AC.LicenseExpDate;
            if (AC.LicenseExpDate.Trim() != "")
                acpolicy.LicenseExpDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC.LicenseExpDate).ToShortDateString());
            else
                acpolicy.LicenseExpDate = "";

           
            //acpolicy.LicenseExpDate = Convert.ToDateTime(AC.LicenseExpDate).ToString("MM/dd/yyyy");
            acpolicy.IsLeasing = AC.IsLeasing;
            acpolicy.PurchaseDate = acpolicy.PurchaseDate;

            try
            {
                int InternalID = 0;

                InternalID = taskControl.GetNewInternalID();

                acpolicy.PurchaseDate = String.Empty;  //AC.EffectiveDate;
                acpolicy.VehicleAge = AC.VehicleAge;
                acpolicy.NewUse = AC.NewUse;
                acpolicy.Cost = AC.Cost;
                acpolicy.ActualValue = AC.ActualValue;
                acpolicy.HomeCity = 0;  //int.Parse(ddlHomeCity.SelectedItem.Value);
                acpolicy.WorkCity = 0;  //int.Parse(ddlWorkCity.SelectedItem.Value);
                acpolicy.AlarmType = AC.AlarmType;
                acpolicy.Depreciation1stYear = AC.Depreciation1stYear;
                acpolicy.DepreciationAllYear = AC.DepreciationAllYear;
                acpolicy.MedicalLimit = AC.MedicalLimit;
                //acpolicy.AssistancePremium = AC.AssistancePremium;
                //acpolicy.TowingPremium = AC.TowingPremium;
                acpolicy.TowingPremium = Decimal.Round(AC.TowingPremium, 4); 
                acpolicy.TowingID = AC.TowingID;
                acpolicy.OriginalTowingPremium = AC.OriginalTowingPremium;
                acpolicy.LeaseLoanGapId = AC.LeaseLoanGapId;
                acpolicy.SeatBelt = AC.SeatBelt;
                acpolicy.PersonalAccidentRider = AC.PersonalAccidentRider;
                acpolicy.CollisionDeductible = AC.CollisionDeductible;
                acpolicy.ComprehensiveDeductible = AC.ComprehensiveDeductible;
                acpolicy.DiscountCompColl = AC.DiscountCompColl;
                acpolicy.BodilyInjuryLimit = AC.BodilyInjuryLimit;
                acpolicy.PropertyDamageLimit = AC.PropertyDamageLimit;
                acpolicy.CombinedSingleLimit = AC.CombinedSingleLimit;
                acpolicy.DiscountBIPD = AC.DiscountBIPD;
                acpolicy.PolicySubClassId = AC.PolicySubClassId;
                acpolicy.EffectiveDate = String.Empty;  //quoteAuto.EffectiveDate;
                acpolicy.ExperienceDiscount = AC.ExperienceDiscount;
                acpolicy.EmployeeDiscount = AC.EmployeeDiscount;
                acpolicy.MiscDiscount = AC.MiscDiscount;

                acpolicy.Plate = AC.Plate;
                acpolicy.VIN = AC.VIN;
                acpolicy.PurchaseDate = AC.PurchaseDate;
                acpolicy.LoJack = AC.LoJack;
                acpolicy.LojackExpDate = AC.LojackExpDate;
                acpolicy.LoJackCertificate = AC.LoJackCertificate;

                acpolicy.VehicleRental = AC.VehicleRental;
                acpolicy.VehicleRentalID = AC.VehicleRentalID;
                acpolicy.IsAssistanceEmp = AC.IsAssistanceEmp;
                acpolicy.AssistancePremium = AC.AssistancePremium;
                acpolicy.AssistanceID = AC.AssistanceID;
                acpolicy.AccidentalDeathPremium = AC.AccidentalDeathPremium;
                acpolicy.AccidentalDeathID = AC.AccidentalDeathID;
                acpolicy.AccidentalDeathPerson = AC.AccidentalDeathPerson;
                acpolicy.EquipmentSoundPremium = AC.EquipmentSoundPremium;
                acpolicy.EquipmentSoundID = AC.EquipmentSoundID;
                acpolicy.EquipmentAudioPremium = AC.EquipmentAudioPremium;
                acpolicy.EquipmentAudioID = AC.EquipmentAudioID;
                acpolicy.EquipmentTapesPremium = AC.EquipmentTapesPremium;
                acpolicy.EquipmentTapes = AC.EquipmentTapes;
                acpolicy.SpecialEquipmentCollPremium = AC.SpecialEquipmentCollPremium;
                acpolicy.SpecialEquipmentColl = AC.SpecialEquipmentColl;
                acpolicy.SpecialEquipmentCompPremium = AC.SpecialEquipmentCompPremium;
                acpolicy.SpecialEquipmentComp = AC.SpecialEquipmentComp;
                acpolicy.CustomizeEquipLimit = AC.CustomizeEquipLimit;
                acpolicy.UninsuredSinglePremium = AC.UninsuredSinglePremium;
                acpolicy.UninsuredSingleID = AC.UninsuredSingleID;
                acpolicy.UninsuredSplitPremium = AC.UninsuredSplitPremium;
                acpolicy.UninsuredSplitID = AC.UninsuredSplitID;

                if (InternalID != 0)
                {
                    acpolicy.InternalID = InternalID;
                }
                else
                {
                    acpolicy.InternalID = 0;
                }

                acpolicy.Mode = 1;   //Insert

                taskControl.AddAutoCover(acpolicy, false);
                taskControl.Save(userID, acpolicy, null, false);
                //taskControl.SaveAutoCover(userID, acpolicy, null, false);

                acpolicy.Mode = 2; // Se da valor de update para que no siga insertando el mismo record.
                taskControl = this.AssignedDriver(taskControl, acpolicy, AC, quoteAuto);

            }
            catch (Exception xcp)
            {
                throw new Exception(xcp.Message.Trim());
            }

            return taskControl;
        }

        private TaskControl.QuoteAuto AssignedDriver(TaskControl.QuoteAuto QA, Quotes.AutoCover ACC, Quotes.AutoCover ACOriginal, TaskControl.QuoteAuto QAOriginal)
        {
            AutoCover srch = new AutoCover();
            srch.QuotesAutoId = ACC.QuotesAutoId;
            srch.InternalID = ACC.InternalID;
            AutoCover AC = QA.GetAutoCover(srch);
            string[] assTemp = null;

            int InternalID = 0;
            AutoDriver AD = new AutoDriver();

            if (QA.Drivers != null && QA.Drivers.Count > 0)
            {
                for (int i = 0; i < QA.Drivers.Count; i++)
                {
                    AutoDriver Driver = (AutoDriver)QA.Drivers[i];
                    assTemp = SelectAssingDriver(ACOriginal, QAOriginal);

                    //if(Driver.DriverID == int.Parse(assTemp[0].ToString())) //int.Parse(this.ddlDriver.SelectedItem.Value))
                    if (Driver.ProspectID == int.Parse(assTemp[3].ToString()))
                    {
                        InternalID = (int)Driver.InternalID;
                        i = QA.Drivers.Count;
                    }
                }
            }

            AD.InternalID = InternalID;

            Quotes.AssignedDriver AsDrv = new Quotes.AssignedDriver();
            AsDrv.AutoDriver = AD;

            if (!AC.AssignedDrivers.Contains(AsDrv))
            {
                AsDrv.AutoDriver = QA.GetDriver(AD);
                AsDrv.AutoCoverID = ACC.QuotesAutoId;

                AsDrv.OnlyOperator = bool.Parse(assTemp[1]);
                AsDrv.PrincipalOperator = bool.Parse(assTemp[2]);

                AsDrv.Mode = (int)Enumerators.Modes.Insert;

                AC.AssignedDrivers.Add(AsDrv);
            }

            Login.Login cp = HttpContext.Current.User as Login.Login;
            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            QA.Save(UserID, AC, null, false);

            return QA;
        }

        private string[] SelectAssingDriver(Quotes.AutoCover AC, TaskControl.QuoteAuto QA)
        {
            
            AutoCover AC2 = new AutoCover();
            AC2.QuotesAutoId = AC.QuotesAutoId;
            AC2.InternalID = AC.InternalID;
            AC2 = QA.GetAutoCover(AC2);

            string[] assTemp = new string[4];

            ArrayList Assigned = AC2.AssignedDrivers;
            if (Assigned != null && Assigned.Count > 0)
            {
                for (int i = 0; i < Assigned.Count; i++)
                {
                    Quotes.AssignedDriver AD = (Quotes.AssignedDriver)Assigned[i];
                    Quotes.AutoDriver Driver = AD.AutoDriver;

                    assTemp[0] = Driver.DriverID.ToString();
                    assTemp[1] = AD.OnlyOperator.ToString();
                    assTemp[2] = AD.PrincipalOperator.ToString();
                    assTemp[3] = AD.AutoDriver.ProspectID.ToString();
                }
            }
            return assTemp;
        }

        protected void BtnChangeToCustomer_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto)Session["TaskControl"];

            EPolicy.Customer.Prospect prospect = (EPolicy.Customer.Prospect)quoteAuto.Prospect;
            DataTable dtCustomersMatchingPhoneNumbers = prospect.GetClientsWithMatchingPhoneNumbers();

            if (dtCustomersMatchingPhoneNumbers.Rows.Count == 0)
            {
                Customer.Customer customer = prospect.GetPopulatedCustomerFromProspect();
                if (quoteAuto.TaskControlID.ToString().Trim() != "")  // Si tiene una cotizacion se busca la info del prospecto del quote.
                {
                    //TaskControl.QuoteAuto quoteAuto = TaskControl.QuoteAuto.GetQuoteAuto(int.Parse(this.TxtTCIDQuote.Text.Trim()),0,false);

                    if (quoteAuto != null && quoteAuto.TaskControlID != 0)
                    {
                        AutoDriver AD;

                        for (int i = 0; i < quoteAuto.Drivers.Count; i++)
                        {
                            AD = (AutoDriver)quoteAuto.Drivers[i];

                            if (quoteAuto.Prospect.ProspectID == AD.ProspectID)
                            {
                                //Nombre del prospecto ya viene desde el application auto.
                                //								customer.FirstName = AD.FirstName;
                                //								customer.LastName1 = AD.LastName1;
                                //								customer.LastName2 = AD.LastName2;
                                customer.Birthday = AD.BirthDate;

                                if (AD.Gender == 1)	//Male
                                    customer.Sex = "M";
                                else				//Female
                                    customer.Sex = "F";
                                customer.ProspectID = AD.ProspectID;
                                customer.MaritalStatus = AD.MaritalStatus;
                                customer.Age = CalcAge(AD.BirthDate).ToString();
                                customer.HomePhone = AD.HomePhone.Trim();
                                customer.Email = AD.Email.Trim();
                            }
                        }
                    }
                }
                customer.Mode = 1;    //ADD
                Session["Customer"] = customer;
                RemoveSessionLookUp();
                Session.Add("ExpressAutoQuote", "ExpressAutoQuote");  // Para indicar en la pantalla de Customer que tiene que regresar al Application Auto.
                Response.Redirect("ClientIndividual.aspx");
            }
            else
            {
                prospect.CustomersMatchingPhoneNumbers = dtCustomersMatchingPhoneNumbers;
                Session["Prospect"] = prospect;
                RemoveSessionLookUp();
                Session.Add("ApplicationAuto", "ApplicationAuto");  // Para indicar en la pantalla de Customer que tiene que regresar al Application Auto.
                Response.Redirect("MatchingEntities.aspx");
            }
        }

        private Customer.Customer SaveCustomer()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto)Session["TaskControl"];
            EPolicy.Customer.Prospect prospect = (EPolicy.Customer.Prospect)quoteAuto.Prospect;

            Customer.Customer customer = prospect.GetPopulatedCustomerFromProspect();
            if (quoteAuto.TaskControlID.ToString().Trim() != "")  // Si tiene una cotizacion se busca la info del prospecto del quote.
            {
                //TaskControl.QuoteAuto quoteAuto = TaskControl.QuoteAuto.GetQuoteAuto(int.Parse(this.TxtTCIDQuote.Text.Trim()),0,false);

                if (quoteAuto != null && quoteAuto.TaskControlID != 0)
                {
                    AutoDriver AD;

                    for (int i = 0; i < quoteAuto.Drivers.Count; i++)
                    {
                        AD = (AutoDriver)quoteAuto.Drivers[i];

                        if (quoteAuto.Prospect.ProspectID == AD.ProspectID)
                        {
                            //Nombre del prospecto ya viene desde el application auto.
                            //								customer.FirstName = AD.FirstName;
                            //								customer.LastName1 = AD.LastName1;
                            //								customer.LastName2 = AD.LastName2;
                            customer.Birthday = AD.BirthDate;
                            customer.SocialSecurity = AD.SocialSecurity;
                            if (AD.Gender == 1)	//Male
                                customer.Sex = "M";
                            else				//Female
                                customer.Sex = "F";
                            customer.ProspectID = AD.ProspectID;
                            customer.MaritalStatus = AD.MaritalStatus;
                            customer.Age = CalcAge(AD.BirthDate).ToString();
                            customer.HomePhone = AD.HomePhone.Trim();
                            customer.Email = AD.Email.Trim();
                        }
                    }
                }
            }
            customer.Mode = 1;    //ADD
            customer.Save(UserID);
            //Session["Customer"] = customer;
            //RemoveSessionLookUp();
            //Session.Add("ExpressAutoQuote", "ExpressAutoQuote");  // Para indicar en la pantalla de Customer que tiene que regresar al Application Auto.
            //Response.Redirect("ClientIndividual.aspx");

            return customer;
        }


        private void ddlCSL_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
        protected void cmdDefPlan_Click(object sender, EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            AutoCover AC = (AutoCover)QA.AutoCovers[0];

            RemoveSessionLookUp();
            Session["InternalAutoID"] = AC.InternalID;
            Response.Redirect("PlanDiferido.aspx", false);
        }


        private void AddOPPEndorsement(int OPPTaskControlID, int OPPQuotesID, double mFactor, double NewProRataTotPrem, double NewShotRateTotPrem)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                int a = Executor.Insert("AddOPPEndorsement", this.GetInsertOPPEndorsementXml(OPPTaskControlID, OPPQuotesID, mFactor, NewProRataTotPrem, NewShotRateTotPrem));
                Executor.CommitTrans();
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
            DbRequestXmlCooker.AttachCookItem("EndoEffective", SqlDbType.DateTime, 0, txtEffDtEndorsementPrimary.Text, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Factor", SqlDbType.Float, 0, mFactor.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ProRataPremium", SqlDbType.Float, 0, NewProRataTotPrem.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ShortRatePremium", SqlDbType.Float, 0, NewShotRateTotPrem.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremPropeties", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremLiability", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremAuto", SqlDbType.Float, 0, txtTtlPremium.Text.ToString().Replace("$", ""), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremUmbrella", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ActualPremTotal", SqlDbType.Float, 0, txtTtlPremium.Text.ToString().Replace("$", ""), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremProperties", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremLiability", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremAuto", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremUmbrella", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PreviousPremTotal", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremProperties", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremLiability", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremAuto", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremUmbrella", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiffPremTotal", SqlDbType.Float, 0, "0.0", ref cookItems);
            DbRequestXmlCooker.AttachCookItem("AdditionalPremium", SqlDbType.Float, 0, "0.0", ref cookItems);

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

        /* NEW VEHICLE */
        protected void dgVehicle_ItemCommand1(object source, DataGridCommandEventArgs e)
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
            // 0  btn
            // 1  Make
            // 2  Model
            // 3  VIN
            // 4  Plate
            // 5  Year
            // 6  btnRemove
            // 7  QuotesAutoID
            // 8  QuotesID
            // 9 InternalID
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)(TaskControl.QuoteAuto)Session["TaskControl"];

            if (e.Item.ItemType.ToString() != "Pager")
            {
                // Search on QA Auto Cover List for matching & Display it
                AutoCover AC = new AutoCover();
                if (e.Item.Cells[3].Text.ToLower() != "&nbsp;")
                    AC.VIN = e.Item.Cells[3].Text;

                if (e.Item.Cells[7].Text != "0")
                    AC.QuotesAutoId = int.Parse(e.Item.Cells[7].Text);

                if (e.Item.Cells[8].Text != "0")
                    AC.QuotesId = int.Parse(e.Item.Cells[8].Text);

                AC.InternalID = int.Parse(e.Item.Cells[9].Text);
                AC = QA.GetAutoCover(AC);

                if (e.CommandName == "Select") // Select
                {
                    this.SelectVehicle(e);
                    this.ddlPolicySubClass.BackColor = System.Drawing.Color.White;
                }
                if (e.CommandName == "Remove") // Select
                {
                    // Remove from QA Auto Cover List 
                    if (AC.Mode == (int)Enumerators.Modes.Insert)  //(int)Enumerators.Modes.Insert)
                    {
                        QA.RemoveAutoCover(AC);
                    }
                    else
                        AC.Mode = (int)Enumerators.Modes.Delete;

                    ViewState.Add("Status", "NEW");
                    clearFields();

                    QA.Save(userID);
                    QA = (TaskControl.QuoteAuto)
                        TaskControl.TaskControl.GetTaskControlByTaskControlID(
                        QA.TaskControlID, userID);

                    Session["TaskControl"] = QA;

                    if (this.GetCurrentNumberOfAutos() == 1)
                    {
                        //this.ApplyDiscountToExistingCovers(0,0);

                        //  this.applyToAll.Value = "1";
                        SetDiscountForOneVehicleOnly();

                        //						this.litPopUp.Text = 
                        //							Utilities.MakeLiteralPopUpString(
                        //							"Only one vehicle remaining: removing " +
                        //							"multiple-vehicle-dependent BI/PD and Coll/Comp discounts.");
                        //						this.litPopUp.Visible = true;						
                    }

                    QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    //QA.Calculate();
                    fillDataGrid(QA.AutoCovers);
                    this.SelectLastVehicle();
                }
            }
            else //Pager
            {
                this.dgVehicle.CurrentPageIndex =
                    int.Parse(e.CommandArgument.ToString()) - 1;
            }
            fillDataGrid(QA.AutoCovers);

            Session["TaskControl"] = QA;
        }
        protected void dgVehicle_ItemCreated1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem ||
                e.Item.ItemType == ListItemType.EditItem)
            {
                TableCell tableCell = new TableCell();
                tableCell = e.Item.Cells[6];
                Button button = new Button();
                button = (Button)tableCell.Controls[0];
                button.Attributes.Add("onclick",
                    "return confirm( " +
                    "\"Are you sure you want to delete this vehicle?\")");
            }
        }

        private DataTable getDisplayDataTable()
        {
            DataSet ds = new DataSet("DSQuotesDrivers");
            DataTable dt = ds.Tables.Add("QuotesDrivers");

            // 0  Btn
            // 1  VehicleMake
            // 2  VehicleModel
            // 3  VIN
            // 4  Plate
            // 5  VehicleYear
            // 6  BtnRemove
            // 7  QuotesAutoID
            // 8  QuotesID
            // 9  VehicleMakeID
            // 10 VehicleModelID
            // 11 VehicleYearID
            // 12 InternalID
            dt.Columns.Add("VehicleMake", typeof(string));
            dt.Columns.Add("VehicleModel", typeof(string));
            dt.Columns.Add("VIN", typeof(string));
            dt.Columns.Add("Plate", typeof(string));
            dt.Columns.Add("VehicleYear", typeof(string));
            dt.Columns.Add("QuotesAutoID", typeof(int));
            dt.Columns.Add("QuotesID", typeof(int));
            dt.Columns.Add("VehicleMakeID", typeof(int));
            dt.Columns.Add("VehicleModelID", typeof(int));
            dt.Columns.Add("VehicleYearID", typeof(int));
            dt.Columns.Add("InternalID", typeof(int));
            return dt;
        }

        private void enableFields(bool status)
        {
            ddlPolicySubClass.Enabled = status;

            ddlCompanyDealer.Enabled = status;
            ddlBank.Enabled = status;

            txtVIN2.Enabled = status;
            txtPlate.Enabled = status;
            ddlYear.Enabled = status;
            ddlMake.Enabled = status;
            ddlModel.Enabled = status;
            txtPurchaseDt.Enabled = status;
            //txtAge.Enabled = status;
            ddlNewUsed.Enabled = status;
            txtCost.Enabled = status;
            txtPlate.Enabled = status;
            txtPurchaseDt.Enabled = status;
            ddlCompanyDealer.Enabled = status;
            ddlBank.Enabled = status;
            txtLicenseNumber.Enabled = status;
            txtExpDate.Enabled = status;
            txtVIN2.Enabled = status;
            txtActualValue.Enabled = status;
            //       ddlHomeCity.Enabled = status;
            //         ddlWorkCity.Enabled = status;

            ddlVehicleClass.Enabled = status;
            ddlTerritory.Enabled = status;
            ddlAlarm.Enabled = status;
            //txtDeprec1st.Enabled = status;
            //txtDeprecAll.Enabled = status;
            ddlMedical.Enabled = status;
            //this.ddlAssistancePremium.Enabled = status;
            txtRoadsideAssitance.Enabled = status;
            //           TxtAssistance.Enabled = status;
            txtTowingPrm.Enabled = status;
            //txtVehicleRental.Enabled = status;
            ddlRental.Enabled = status;
            ddlLoanGap.Enabled = status;
            ddlSeatBelt.Enabled = status;
            ddlPAR.Enabled = status;

            ddlCollision.Enabled = status;
            ddlComprehensive.Enabled = status;

            ddlBI.Enabled = status;
            ddlPD.Enabled = status;
            ddlCSL.Enabled = status;


            //this.btnCalendar.Disabled = !status;

            txtLicenseNumber.Enabled = status;
            txtExpDate.Enabled = status;
            chkIsLeasing.Enabled = status;

            if (status)
            {
                //imgExpDate.Visible = false;
                //imgPurchaseDt.Visible = false;
            }
            else
            {
                //imgExpDate.Visible = true;
                // imgPurchaseDt.Visible = true;
            }


            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (!cp.IsInRole("AUTO PERSONAL MISC DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                txtMiscDiscount.Visible = false;
                lblMiscDisc.Visible = false;
            }
            else
            {
                //txtMiscDiscount.Visible = true;
                //lblMiscDisc.Visible = true;
            }

            if (!cp.IsInRole("AUTO PERSONAL EMPLOYEE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlEmployeeDiscount.Visible = false;
                lblEmployeeDiscount.Visible = false;
            }
            else
            {
                //ddlEmployeeDiscount.Visible = true;
                //lblEmployeeDiscount.Visible = true;
            }

            if (!cp.IsInRole("AUTO PERSONAL EXPERIENCE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlExperienceDiscount.Visible = false;
                lblExperienceDiscount.Visible = false;
                TxtExpDisc.Visible = false;
            }
            else
            {
                ddlExperienceDiscount.Visible = true;
                lblExperienceDiscount.Visible = true;
                TxtExpDisc.Visible = true;
            }
        }

        private void fillDataGrid(ArrayList AL)
        {
            DataTable DT = getDisplayDataTable();

            if (AL != null && AL.Count > 0)
            {
                this.dgVehicle.Visible = false;
                DataRow row;
                // Add all values in one shot
                for (int i = 0; i < AL.Count; i++)
                {
                    AutoCover AC = (AutoCover)AL[i];
                    if (AC.Mode != (int)Enumerators.Modes.Delete)
                    {
                        row = DT.NewRow();
                        row["QuotesAutoID"] = AC.QuotesAutoId;
                        row["QuotesID"] = AC.QuotesId;
                        row["VIN"] = AC.VIN;
                        row["Plate"] = AC.Plate;
                        row["VehicleMakeID"] = AC.VehicleMake;
                        row["VehicleModelID"] = AC.VehicleModel;
                        row["VehicleYearID"] = AC.VehicleYear;
                        try
                        {
                            //RPR 2004-02-11 (Separate members for individual
                            //retrieval.)
                            MakeYearModel mym =
                                AutoCover.GetMakeYearModel(AC.VehicleMake,
                                AC.VehicleModel, AC.VehicleYear);
                            row["VehicleMake"] = mym.Make;
                            row["VehicleModel"] = mym.Model;
                            row["VehicleYear"] = mym.Year;
                        }
                        catch //me if you can?
                        {
                        }
                        row["InternalID"] = AC.InternalID;
                        DT.Rows.Add(row);
                    }
                }
            }

            else
            {
                this.dgVehicle.Visible = false;
            }

            dgVehicle.DataSource = DT;
            dgVehicle.DataBind();
        }
        private void SelectVehicle(System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            AutoCover AC = new AutoCover();
            if (e.Item.Cells[3].Text.ToLower() != "&nbsp;")
                AC.VIN = e.Item.Cells[3].Text;
            if (e.Item.Cells[7].Text != "0")
                AC.QuotesAutoId = int.Parse(e.Item.Cells[7].Text);
            if (e.Item.Cells[8].Text != "0")
                AC.QuotesId = int.Parse(e.Item.Cells[8].Text);
            AC.InternalID = int.Parse(e.Item.Cells[9].Text);
            AC = QA.GetAutoCover(AC);

            Login.Login cp = HttpContext.Current.User as Login.Login;
            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            clearFields();
            enableFields(true);
            ShowAutoCover(AC);
            ViewState.Add("Status", "UPDATE");
            ViewState.Add("QuotesAutoId", AC.QuotesAutoId);
            ViewState.Add("InternalAutoID", AC.InternalID);
            Session["InternalAutoID"] = AC.InternalID;
            btnSave.Visible = true;
            //btnAssignDrv.Visible = false;//true;
            btnViewCvr.Visible = true;
            this.SetControlState((int)States.READONLY);
        }

        private void clearFields()
        {
            ddlPolicySubClass.SelectedIndex = 0;

            txtVIN2.Text = "";
            txtPlate.Text = "";
            ddlCompanyDealer.SelectedIndex = 0;
            ddlBank.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ddlMake.SelectedIndex = 0;
            ddlModel.SelectedIndex = 0;
            TxtpurchaseDate.Text = "";
            txtAge.Text = "";
            ddlNewUsed.SelectedIndex = 0;
            txtCost.Text = "";
            txtActualValue.Text = "";

            ddlVehicleClass.SelectedIndex = 0;
            ddlTerritory.SelectedIndex = 0;
            ddlAlarm.SelectedIndex = 0;
            ddlMedical.SelectedIndex = 0;
            //this.ddlAssistancePremium.SelectedIndex = 0;
            txtRoadsideAssitance.Text = "";
            txtTowingPrm.Text = "";
            TxtVehicleRental.Text = "";
            TxtTowing.Text = "";
            ddlRental.SelectedIndex = 0;
            ddlLoanGap.SelectedIndex = 0;
            ddlSeatBelt.SelectedIndex = 0;
            ddlPAR.SelectedIndex = 0;

            ddlTowing.SelectedIndex = 0;
            ddlCollision.SelectedIndex = 0;
            ddlComprehensive.SelectedIndex = 0;
            txtDiscountCollComp.Text = "";
            ddlBI.SelectedIndex = 0;
            ddlPD.SelectedIndex = 0;
            ddlCSL.SelectedIndex = 0;
            txtDiscountBIPD.Text = "";
            this.txtPremOthers.Text = "";
            this.txt1stISO0.Text = "";
            txtPartialCharge.Text = "";
            txtTotalPremium.Text = "";
            txtPartialPremium.Text = string.Empty;
            //RPR 2004-03-26
            this.lblPrimaryDriver.Text = string.Empty;

            txtLicenseNumber.Text = "";
            txtExpDate.Text = "";
            chkIsLeasing.Checked = false;

            txtIsAssistanceEmp.Text = "False";
            ddlRoadAssistEmp.SelectedIndex = 0;
            ddlRoadAssist.SelectedIndex = 0;
            chkAssistEmp.Enabled = false;
            chkAssistEmp.Enabled = false;

            ddlAccidentDeath.SelectedIndex = 0;
            TxtAccidentDeathPremium.Text = "";
            ddlADPersons.SelectedIndex = 0;
            TxtEquitmentSonido.Text = "";
            ddlEquitmentSonido.SelectedIndex = 0;
            TxtEquitmentAudio.Text = "";
            ddlEquitmentAudio.SelectedIndex = 0;
            TxtEquitmentTapes.Text = "";
            chkEquipTapes.Checked = false;
            TxtEquipColl.Text = "";
            chkEquipColl.Checked = false;
            TxtEquipComp.Text = "";
            chkEquipComp.Checked = false;
            TxtCustomizeEquipLimit.Text = "";
            TxtEquipTotal.Text = "";

            TxtUninsuredSingle.Text = "";
            ddlUninsuredSingle.SelectedIndex = 0;
            TxtUninsuredSplit.Text = "";
            ddlUninsuredSplit.SelectedIndex = 0;

            chkLoJack.Checked = false;
            txtLoJackCertificate.Text = "";
            TxtLojackExpDate.Text = "";

            ddlExperienceDiscount.SelectedIndex = 0;
            ddlEmployeeDiscount.SelectedIndex = 0;
            txtMiscDiscount.Text = "";
            TxtExpDisc.Text = "0";
        }

        private void fillModel()
        {
            if (ddlMake.Items[ddlMake.SelectedIndex].Text != "")
            {
                int makeID = Int32.Parse(ddlMake.SelectedItem.Value.ToString());
                DataTable dtModel = LookupTables.LookupTables.GetTable("VehicleModel");
                DataTable dt = dtModel.Clone();
                DataRow[] DrModel = dtModel.Select("VehicleMakeID = " + makeID, "VehicleModelDesc");

                for (int i = 0; i <= DrModel.Length - 1; i++)
                {
                    DataRow myRow = dt.NewRow();
                    myRow["VehicleModelID"] = (int)DrModel[i].ItemArray[0];
                    myRow["VehicleMakeID"] = (int)DrModel[i].ItemArray[1];
                    myRow["VehicleModelDesc"] = DrModel[i].ItemArray[2].ToString();

                    dt.Rows.Add(myRow);
                    dt.AcceptChanges();
                }

                ddlModel.Items.Clear();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "VehicleModelDesc";
                ddlModel.DataValueField = "VehicleModelID";
                ddlModel.DataBind();
                ddlModel.SelectedIndex = -1;
                ddlModel.Items.Insert(0, "");
            }
            else
            {
                DataTable dtModel = LookupTables.LookupTables.GetTable("VehicleModel");

                ddlModel.DataSource = dtModel;
                ddlModel.DataTextField = "VehicleModelDesc";
                ddlModel.DataValueField = "VehicleModelID";
                ddlModel.DataBind();
                ddlModel.SelectedIndex = -1;
                ddlModel.Items.Insert(0, "");
            }
        }

        private void ShowAutoCover(AutoCover AC)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            this.TxtVehicleCount.Text = QA.VehicleCount.ToString();

            ddlPolicySubClass.SelectedIndex =
                ddlPolicySubClass.Items.IndexOf(
                ddlPolicySubClass.Items.FindByValue(
                AC.PolicySubClassId.ToString()));
            //	txtVIN.Text = AC.VIN;
            txtVIN2.Text = AC.VIN;
            try
            {
                ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(
                              AC.Bank.ToString()));
            }
            catch { }
            try
            {
                ddlCompanyDealer.SelectedIndex = ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(
                    AC.CompanyDealer.ToString()));
            }
            catch { }
            //	txtPlate.Text = AC.Plate;
            txtPlate.Text = AC.Plate;

            ddlYear.SelectedIndex =
                ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(
                AC.VehicleYear.ToString()));
            //txtVehicleYear.Text = ddlYear.SelectedItem.Text;
            ddlMake.SelectedIndex =
                ddlMake.Items.IndexOf(
                ddlMake.Items.FindByValue(AC.VehicleMake.ToString()));
            //	txtVehicleMake.Text = ddlMake.SelectedItem.Text;
            fillModel();
            ddlModel.SelectedIndex =
                ddlModel.Items.IndexOf(
                ddlModel.Items.FindByValue(AC.VehicleModel.ToString()));
            //	txtVehicleModel.Text = ddlModel.SelectedItem.Text;
            TxtpurchaseDate.Text = AC.PurchaseDate;

            try
            {
                DateTime purchaseDate = DateTime.Parse(AC.PurchaseDate);
                string purchaseDateConverted = purchaseDate.ToString("MM/dd/yyyy");
                string purchaseDateTrimmed = purchaseDateConverted.Replace(@"/", "");
                TxtpurchaseDate.Text = purchaseDateTrimmed;
            }
            catch { }

            try
            {
                DateTime licExpDate = DateTime.Parse(AC.LicenseExpDate);
                string licExpDateConverted = licExpDate.ToString("MM/dd/yyyy");
                string licExpDateTrimmed = licExpDateConverted.Replace(@"/", "");
                //txtExpDate.Text = licExpDateTrimmed;
                txtExpDate.Text = Convert.ToDateTime(licExpDateTrimmed).ToString("MM/dd/yyyy");
            }
            catch { }


            txtAge.Text = AC.VehicleAge.ToString();
            ddlNewUsed.SelectedIndex =
                ddlNewUsed.Items.IndexOf(
                ddlNewUsed.Items.FindByValue(AC.NewUse.ToString()));
            txtCost.Text = AC.Cost.ToString("###,###");
            txtActualValue.Text = AC.ActualValue.ToString("###,###");


            if (QA.IsPolicy || Session["OptimaPersonalPackage"] != null)
            {
                ddlBank.SelectedIndex =
                    ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(
                    AC.Bank.ToString()));

                ddlCompanyDealer.SelectedIndex =
                    ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(
                    AC.CompanyDealer.ToString()));
            }

            txtLicenseNumber.Text = AC.License;
            chkIsLeasing.Checked = AC.IsLeasing;
           // txtExpDate.Text = AC.LicenseExpDate;
            txtExpDate.Text = Convert.ToDateTime(AC.LicenseExpDate).ToString("MM/dd/yyyy");

            AutoCover AC2 = new AutoCover();
            AC2.QuotesAutoId = AC.QuotesAutoId;
            AC2.InternalID = AC.InternalID;
            AC2 = QA.GetAutoCover(AC2);

            ArrayList Assigned = AC2.AssignedDrivers;
            if (Assigned != null && Assigned.Count > 0)
            {
                for (int i = 0; i < Assigned.Count; i++)
                {
                    Quotes.AssignedDriver AD = (Quotes.AssignedDriver)Assigned[i];
                    Quotes.AutoDriver Driver = AD.AutoDriver;

                    ddlDriver.SelectedIndex =
                        ddlDriver.Items.IndexOf(ddlDriver.Items.FindByValue(
                        Driver.DriverID.ToString()));

                    //this.chkOnlyOperator.Checked = AD.OnlyOperator;
                    //this.chkPrincipalOperator.Checked = AD.PrincipalOperator;
                }
            }

            bool dirty = false;

            //RPR 2004-03-26
            foreach (AssignedDriver assignedDriver in AC.AssignedDrivers)
            {
                if (assignedDriver.PrincipalOperator)
                {
                    this.lblPrimaryDriver.Text =
                        assignedDriver.AutoDriver.FirstName.Trim() +
                        " " + assignedDriver.AutoDriver.LastName1.Trim() +
                        " " + assignedDriver.AutoDriver.LastName2.Trim();
                    dirty = true;
                }
            }

            if (!dirty)
                this.lblPrimaryDriver.Text = "Not assigned";

            //RPR 2004-04-27
            string ac_vehicleClass = string.Empty;
            ac_vehicleClass = AC.VehicleClass.Length != 2 ?
                AC.VehicleClass + " " :
                AC.VehicleClass;

            if (AC.VehicleClass != "")
                ddlVehicleClass.SelectedIndex =
                    ddlVehicleClass.Items.IndexOf(
                    ddlVehicleClass.Items.FindByValue(ac_vehicleClass));
            if (AC.Territory != 0)
                ddlTerritory.SelectedIndex =
                    ddlTerritory.Items.IndexOf(
                    ddlTerritory.Items.FindByValue(
                    AC.Territory.ToString()));
            // if (AC.AlarmType != 0)
            ddlAlarm.SelectedIndex =
                ddlAlarm.Items.IndexOf(
                ddlAlarm.Items.FindByValue(AC.AlarmType.ToString()));

            if (AC.MedicalLimit != 0)
                ddlMedical.SelectedIndex = ddlMedical.Items.IndexOf(ddlMedical.Items.FindByValue(AC.MedicalLimit.ToString()));

            //txtTowingPrm.Text = AC.TowingPremium.ToString("###,###");
            //ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(ddlTowing.Items.FindByValue(AC.TowingPremium.ToString().Replace("0", "").Replace(".", "")));


            //TxtTowing.Text = AC.TowingPremium.ToString("###,###");
            if (txtTerm.Text.ToString().Trim() != "")
            {
                decimal Term = int.Parse(this.txtTerm.ToString().Trim());
                decimal Towing = (AC.OriginalTowingPremium * CalculatePeriodAmounts());

                TxtTowing.Text = Towing.ToString("###,###");
            }
            else
            {
                TxtTowing.Text = AC.TowingPremium.ToString("###,###").Trim();
            }

            if (AC.TowingPremium != 0)
                ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(ddlTowing.Items.FindByValue(AC.TowingID.ToString()));

            if (AC.LeaseLoanGapId != 0)
            {
                ddlLoanGap.SelectedIndex = ddlLoanGap.Items.IndexOf(ddlLoanGap.Items.FindByValue(AC.LeaseLoanGapId.ToString()));
                chkLLG.Checked = true;
            }
            else
                chkLLG.Checked = false;

            if (AC.SeatBelt != 0)
                ddlSeatBelt.SelectedIndex =
                    ddlSeatBelt.Items.IndexOf(
                    ddlSeatBelt.Items.FindByValue(
                    AC.SeatBelt.ToString()));
            if (AC.PersonalAccidentRider != 0)
                ddlPAR.SelectedIndex =
                    ddlPAR.Items.IndexOf(
                    ddlPAR.Items.FindByValue(
                    AC.PersonalAccidentRider.ToString()));
            if (AC.CollisionDeductible != 0)
                ddlCollision.SelectedIndex =
                    ddlCollision.Items.IndexOf(
                    ddlCollision.Items.FindByValue(
                    AC.CollisionDeductible.ToString()));
            if (AC.ComprehensiveDeductible != 0)
                ddlComprehensive.SelectedIndex =
                    ddlComprehensive.Items.IndexOf(
                    ddlComprehensive.Items.FindByValue(
                    AC.ComprehensiveDeductible.ToString()));
            txtDiscountCollComp.Text = AC.DiscountCompColl.ToString();
            if (AC.BodilyInjuryLimit != 0)
                ddlBI.SelectedIndex =
                    ddlBI.Items.IndexOf(
                    ddlBI.Items.FindByValue(
                    AC.BodilyInjuryLimit.ToString()));
            if (AC.PropertyDamageLimit != 0)
                ddlPD.SelectedIndex =
                    ddlPD.Items.IndexOf(
                    ddlPD.Items.FindByValue(
                    AC.PropertyDamageLimit.ToString()));
            if (AC.CombinedSingleLimit != 0)
                ddlCSL.SelectedIndex =
                    ddlCSL.Items.IndexOf(
                    ddlCSL.Items.FindByValue(
                    AC.CombinedSingleLimit.ToString()));
            txtDiscountBIPD.Text = AC.DiscountBIPD.ToString();

            TxtVehicleRental.Text = AC.VehicleRental.ToString("###,###");
            if (AC.VehicleRental != 0)
                ddlRental.SelectedIndex = ddlRental.Items.IndexOf(ddlRental.Items.FindByValue(AC.VehicleRentalID.ToString()));

            // Desabilitar la opcion de road assistance para double interest
            if (!(AC.Term > 12))
            {
                if (AC.IsAssistanceEmp)
                {
                    if (AC.AssistancePremium != 0)
                    {
                        chkAssistEmp.Checked = true;
                        chkAssist.Checked = false;
                        ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue(AC.AssistanceID.ToString()));
                    }
                }
                else
                {
                    if (AC.AssistancePremium != 0)
                    {
                        chkAssistEmp.Checked = false;
                        chkAssist.Checked = true;
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue(AC.AssistanceID.ToString()));
                    }
                }
            }
            TxtAccidentDeathPremium.Text = AC.AccidentalDeathPremium.ToString("###,###");
            if (AC.AccidentalDeathPremium != 0)
                ddlAccidentDeath.SelectedIndex = ddlAccidentDeath.Items.IndexOf(ddlAccidentDeath.Items.FindByValue(AC.AccidentalDeathID.ToString()));

            ddlADPersons.SelectedIndex = ddlADPersons.Items.IndexOf(ddlADPersons.Items.FindByValue(AC.AccidentalDeathPerson.ToString()));

            TxtEquitmentSonido.Text = AC.EquipmentSoundPremium.ToString("###,###");
            if (AC.EquipmentSoundPremium != 0)
                ddlEquitmentSonido.SelectedIndex = ddlEquitmentSonido.Items.IndexOf(ddlEquitmentSonido.Items.FindByValue(AC.EquipmentSoundID.ToString()));

            TxtEquitmentAudio.Text = AC.EquipmentAudioPremium.ToString("###,###");
            if (AC.EquipmentAudioPremium != 0)
                ddlEquitmentAudio.SelectedIndex = ddlEquitmentAudio.Items.IndexOf(ddlEquitmentAudio.Items.FindByValue(AC.EquipmentAudioID.ToString()));

            TxtEquitmentTapes.Text = AC.EquipmentTapesPremium.ToString("###,###");
            chkEquipTapes.Checked = AC.EquipmentTapes;

            TxtEquipColl.Text = AC.SpecialEquipmentCollPremium.ToString("###,###");
            chkEquipColl.Checked = AC.SpecialEquipmentColl;
            TxtEquipComp.Text = AC.SpecialEquipmentCompPremium.ToString("###,###");
            chkEquipComp.Checked = AC.SpecialEquipmentComp;
            TxtCustomizeEquipLimit.Text = AC.CustomizeEquipLimit.ToString();
            TxtEquipTotal.Text = (AC.SpecialEquipmentCollPremium + AC.SpecialEquipmentCompPremium).ToString("###,###");

            chkLoJack.Checked = AC.LoJack;
            txtLoJackCertificate.Text = AC.LoJackCertificate.ToString();
            TxtLojackExpDate.Text = AC.LojackExpDate.ToString();

            TxtUninsuredSingle.Text = AC.UninsuredSinglePremium.ToString("###,###");
            if (AC.UninsuredSinglePremium != 0)
                ddlUninsuredSingle.SelectedIndex = ddlUninsuredSingle.Items.IndexOf(ddlUninsuredSingle.Items.FindByValue(AC.UninsuredSingleID.ToString()));

            TxtUninsuredSplit.Text = AC.UninsuredSplitPremium.ToString("###,###");
            if (AC.UninsuredSplitPremium != 0)
                ddlUninsuredSplit.SelectedIndex = ddlUninsuredSplit.Items.IndexOf(ddlUninsuredSplit.Items.FindByValue(AC.UninsuredSplitID.ToString()));

            txtIsAssistanceEmp.Text = AC.IsAssistanceEmp.ToString().Trim();

            //if (AC.ExperienceDiscount != 0)
            //    ddlExperienceDiscount.SelectedIndex = ddlExperienceDiscount.Items.IndexOf(ddlExperienceDiscount.Items.FindByValue(AC.ExperienceDiscount.ToString()));

            TxtExpDisc.Text = AC.ExperienceDiscount.ToString();

            if (AC.EmployeeDiscount != 0)
                ddlEmployeeDiscount.SelectedIndex = ddlEmployeeDiscount.Items.IndexOf(ddlEmployeeDiscount.Items.FindByValue(AC.EmployeeDiscount.ToString()));

            txtMiscDiscount.Text = AC.MiscDiscount.ToString("00.00");
            TxtTotDiscount.Text = AC.TotDiscount.ToString();

            this.txt1stISO0.Text = this.Get1stPeriodISOCode(AC);

            this.txtPartialPremium.Text = String.Format("{0:c}", ((int)AC.TotalAmount));
            this.txtPartialDiscount.Text = String.Format("{0:c}", decimal.Parse(AC.TotDiscount.ToString()));
            this.txtPartialCharge.Text = String.Format("{0:c}", Math.Round(AC.Charge, 0));
            this.txtTotalPremium.Text = String.Format("{0:c}", ((int)AC.TotalAmount + decimal.Parse(AC.TotDiscount.ToString())) + Math.Round(AC.Charge, 0));


            //RPR 2004-05-28
            /*String.Format("{0:c}", (Math.Round(AC.TotalAmount, 0) + 
                Math.Round(AC.Charge, 0)));*/
            SetPolicySubClass();
        }

        private bool AreBaseClassesCompatible(int BaseClassA, int BaseClassB)
        {
            if (BaseClassA == 1 && BaseClassB != BaseClassA)
                return false;
            if (BaseClassB == 1 && BaseClassA != BaseClassB)
                return false;
            /*if(AutoCover.IsMasterCover(BaseClassA) &&
                    BaseClassA != BaseClassB)
                    return false;
                if(AutoCover.IsMasterCover(BaseClassB) &&
                    BaseClassA != BaseClassB)
                    return false;
                if(AutoCover.IsSpecialCover(BaseClassA) &&
                    BaseClassA != BaseClassB)
                    return false;
                if(AutoCover.IsSpecialCover(BaseClassB) &&
                    BaseClassA != BaseClassB)
                    return false;*/
            //As requested by JDP.
            return true;
        }

        private bool ValidCoverMix(int CoverType)
        {
            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                if (QA.AutoCovers.Count >= 1 /* 0->1 RPR 2004-05-20*/)
                {
                    AutoCover AC = (AutoCover)QA.AutoCovers[0];
                    if (AutoCover.IsSpecialCover(AC.PolicySubClassId) /*&&
						AC.PolicySubClassId == CoverType*/)//Is special.
                    {
                        int autoCoverBaseClassA =
                            AutoCover.GetAutoPolicySubClassBaseClassID(
                            AC.PolicySubClassId);
                        int autoCoverBaseClassB =
                            AutoCover.GetAutoPolicySubClassBaseClassID(CoverType);

                        return this.AreBaseClassesCompatible(autoCoverBaseClassA,
                            autoCoverBaseClassB);
                    }
                    else if (AC.PolicySubClassId == CoverType ||
                        (AC.PolicySubClassId == 2 && CoverType == 3) ||
                        (AC.PolicySubClassId == 3 && CoverType == 2))
                    //Same cover type or base compatible.
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            throw new Exception("Could not determine cover mix validity due to " +
                "missing session variable (TaskControl).");
        }

        private void SetAsterisks(int ClassID)
        {
            int baseClassID = AutoCover.GetAutoPolicySubClassBaseClassID(ClassID);

            switch (baseClassID)
            {
                case 1: //DI
                    //					this.lblAsteriskCollision.Visible = true;
                    //					this.lblAsteriskComprehensive.Visible = true;
                    //					this.lblAsteriskBILimit.Visible = false;
                    //					this.lblAsteriskPDLimit.Visible = false;
                    //					this.lblAsteriskCSLlimit.Visible = false;
                    break;
                case 2: //LI
                    //					this.lblAsteriskCollision.Visible = false;
                    //					this.lblAsteriskComprehensive.Visible = false;

                    if (AutoCover.IsCSLonly(ClassID))
                    {
                        //						this.lblAsteriskBILimit.Visible = false;
                        //						this.lblAsteriskPDLimit.Visible = false;
                    }
                    else
                    {
                        //						this.lblAsteriskBILimit.Visible = true;
                        //						this.lblAsteriskPDLimit.Visible = true;
                    }
                    //					this.lblAsteriskCSLlimit.Visible = true;
                    break;
                case 3: //FC
                    //					this.lblAsteriskCollision.Visible = true;
                    //					this.lblAsteriskComprehensive.Visible = true;
                    //					this.lblAsteriskBILimit.Visible = true;
                    //					this.lblAsteriskPDLimit.Visible = true;
                    //					this.lblAsteriskCSLlimit.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private bool SetPolicySubClass()
        {
            if (ViewState["Status"] != null)
            {
                switch (ViewState["Status"].ToString().Trim())
                {
                    case "NEW":
                        this.SetControlState((int)States.NEW);
                        break;
                    case "UPDATE":
                        this.SetControlState((int)States.READWRITE);
                        break;
                    default:
                        //
                        break;
                }
            }//:~			

            enableFields(true);

            TaskControl.QuoteAuto QA = null;
            bool valid = false;

            if ((TaskControl.QuoteAuto)Session["TaskControl"] != null)
            {
                QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            }

            if (this.ViewState["Status"] != null &&
                this.ViewState["Status"].ToString() != "UPDATE" && QA != null &&
                QA.AutoCovers.Count > 0)
            {
                if (ddlPolicySubClass.SelectedItem.Value.Trim() == "")
                {
                    ValidCoverMix(0);
                }
                else
                {
                    valid = ValidCoverMix(int.Parse(ddlPolicySubClass.SelectedItem.Value));
                }
            }
            else
            {
                valid = true;
            }

            if (ddlPolicySubClass.SelectedIndex == 0 || !valid)
            {
                if (QA.IsPolicy)
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                }

                enableFields(false);
                ddlPolicySubClass.Enabled = true;
                ViewState.Add("DI_Cover", false);
                ViewState.Add("Liab_Cover", false);
                ViewState.Add("FC_Cover", false);
                if (!valid)
                {
                    lblRecHeader.Text = Utilities.MakeLiteralPopUpString("The selected base policy type is " +
                        "not compatible with an existing policy type on this quote. " +
                        "Incompatible base policy types cannot be mixed " +
                        "under the same policy.");
                    mpeSeleccion.Show();
                }
            }
            else
            {
                if (QA.IsPolicy)
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                }

                enableFields(true);

                //  Values Out of the Database!!!!
                //       Table: PolicySubClass
                //	1 - Double Interest
                //	2 - Public Liability
                //	3 - Full Cover

                ///TODO: visibility of asterisks!!!

                //RPR 2004-04-21
                int baseClassID =
                    AutoCover.GetAutoPolicySubClassBaseClassID(
                    int.Parse(this.ddlPolicySubClass.SelectedItem.Value.Trim()));

                this.SetAsterisks(int.Parse(
                    this.ddlPolicySubClass.SelectedItem.Value.Trim()));

                if (baseClassID == 1 /*DI*/)
                {
                    ddlCollision.Enabled = true;
                    ddlComprehensive.Enabled = true;
                    txtDiscountCollComp.Enabled = true;
                    ddlBI.Enabled = false;
                    ddlPD.Enabled = false;
                    ddlCSL.Enabled = false;
                    txtDiscountBIPD.Enabled = false;

                    ddlBI.SelectedIndex = 0;
                    ddlPD.SelectedIndex = 0;
                    ddlCSL.SelectedIndex = 0;



                    ViewState.Add("DI_Cover", true);
                    ViewState.Add("Liab_Cover", false);
                    ViewState.Add("FC_Cover", false);
                }
                else if (baseClassID == 2 /*LI*/)
                {
                    ddlCollision.Enabled = false;
                    ddlComprehensive.Enabled = false;
                    txtDiscountCollComp.Enabled = false;
                    //RPR 2004-05-25
                    if (AutoCover.IsCSLonly(int.Parse(
                        this.ddlPolicySubClass.SelectedItem.Value.Trim())))
                    {
                        ddlBI.Enabled = false;
                        ddlPD.Enabled = false;
                    }
                    else
                    {
                        ddlBI.Enabled = true;
                        ddlPD.Enabled = true;
                    }
                    ddlCSL.Enabled = true;
                    txtDiscountBIPD.Enabled = true;

                    ddlCollision.SelectedIndex = 0;
                    ddlComprehensive.SelectedIndex = 0;

                    //txtDeprec1st.Text = "";
                    //txtDeprec1st.Enabled = false;
                    //txtDeprecAll.Text = "";
                    //txtDeprecAll.Enabled = false;

                    ViewState.Add("DI_Cover", false);
                    ViewState.Add("Liab_Cover", true);
                    ViewState.Add("FC_Cover", false);
                }
                else if (baseClassID == 3 /*FC*/)
                {
                    ddlCollision.Enabled = true;
                    ddlComprehensive.Enabled = true;
                    txtDiscountCollComp.Enabled = true;
                    ddlBI.Enabled = true;
                    ddlPD.Enabled = true;
                    ddlCSL.Enabled = true;
                    txtDiscountBIPD.Enabled = true;



                    ViewState.Add("DI_Cover", false);
                    ViewState.Add("Liab_Cover", false);
                    ViewState.Add("FC_Cover", true);
                }
                //Commented by RPR 2004-05-21
                /*else if (!ddlPolicySubClass.SelectedItem.Value.Equals("0")) // Other
                    {
                        ddlCollision.Enabled = true;
                        ddlComprehensive.Enabled = true;
                        txtDiscountCollComp.Enabled = true;
                        ddlBI.Enabled = true;
                        ddlPD.Enabled = true;
                        ddlCSL.Enabled = true;
                        txtDiscountBIPD.Enabled = true;

                        txtDeprec1st.Enabled = true;
                        txtDeprecAll.Enabled = true;

                        ViewState.Add("DI_Cover", true); 
                        ViewState.Add("Liab_Cover", true); 
                        ViewState.Add("FC_Cover", true);
                    }*/
            }
            btnSave.Visible = true;
            return valid;
        }

        private AutoCover LoadFromForm(int QuotesAutoID, int InternalID)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (TxtVehicleCount.Text.Trim() == "")
            {
                QA.VehicleCount = 1;
            }
            else
            {
                QA.VehicleCount = int.Parse(TxtVehicleCount.Text);
            }

            AutoCover AC = new AutoCover();

            AC.VIN = txtVIN2.Text.Trim().ToUpper();
            AC.Plate = txtPlate.Text.Trim().ToUpper();
            if (ddlYear.SelectedIndex > 0 && ddlYear.SelectedItem != null)
                AC.VehicleYear = int.Parse(ddlYear.SelectedItem.Value);
            if (ddlMake.SelectedIndex > 0 && ddlMake.SelectedItem != null)
                AC.VehicleMake = int.Parse(ddlMake.SelectedItem.Value);
            if (ddlModel.SelectedIndex > 0 && ddlModel.SelectedItem != null)
                AC.VehicleModel = int.Parse(ddlModel.SelectedItem.Value);
            //RPR 2004-05-26
            if (ddlPolicySubClass.SelectedIndex > 0 &&
                ddlPolicySubClass.SelectedItem != null)
                AC.PolicySubClassId =
                    int.Parse(ddlPolicySubClass.SelectedItem.Value.Trim());
            AC.PurchaseDate = this.TxtpurchaseDate.Text = "5/1/" + ddlYear.SelectedItem.Text.Trim(); //TxtpurchaseDate.Text;
            if (txtAge.Text != "")
                AC.VehicleAge = int.Parse(txtAge.Text);
            if (ddlNewUsed.SelectedIndex > 0 && ddlNewUsed.SelectedItem != null)
                AC.NewUse = int.Parse(ddlNewUsed.SelectedItem.Value);
            if (txtCost.Text != "")
                AC.Cost = decimal.Parse(txtCost.Text);

            if (txtActualValue.Text != "")
                AC.ActualValue = decimal.Parse(txtActualValue.Text);

            if (int.Parse(ddlNewUsed.SelectedItem.Value) == 2) //new
                AC.ActualValue = AC.Cost;



            if (ddlVehicleClass.SelectedIndex > 0 && ddlVehicleClass.SelectedItem != null)
                AC.VehicleClass = ddlVehicleClass.SelectedItem.Value;
            if (ddlTerritory.SelectedIndex > 0 && ddlTerritory.SelectedItem != null)
                AC.Territory = int.Parse(ddlTerritory.SelectedItem.Value);
            if (ddlAlarm.SelectedIndex > 0 && ddlAlarm.SelectedItem != null)
                AC.AlarmType = int.Parse(ddlAlarm.SelectedItem.Value);
            //if (txtDeprec1st.Text != "")
            //  AC.Depreciation1stYear = decimal.Parse(txtDeprec1st.Text);

            //JDP pidi� igualar campos.
            //  if (txtDeprecAll.Text != "")
            //    AC.DepreciationAllYear = decimal.Parse(this.txtDeprec1st.Text);

            if (ddlMedical.SelectedIndex > 0 && ddlMedical.SelectedItem != null)
                AC.MedicalLimit = int.Parse(ddlMedical.SelectedItem.Value);
            //			if (this.ddlAssistancePremium.SelectedItem != null &&
            //				this.ddlAssistancePremium.SelectedItem.Text != string.Empty)
            //				AC.AssistancePremium = decimal.Parse(
            //					this.ddlAssistancePremium.SelectedItem.Text.Trim());


            //AC.TowingPremium = decimal.Parse(ddlTowing.SelectedItem.Value);
            AC.TowingPremium = Decimal.Round(decimal.Parse(ddlTowing.SelectedItem.Value), 4); 

            //if (ddlLoanGap.SelectedIndex > 0 && ddlLoanGap.SelectedItem != null)
            //    AC.LeaseLoanGapId = int.Parse(ddlLoanGap.SelectedItem.Value);
            if (chkLLG.Checked)
                AC.LeaseLoanGapId = 2;
            else
                AC.LeaseLoanGapId = 1;

            if (ddlSeatBelt.SelectedIndex > 0 && ddlSeatBelt.SelectedItem != null)
                AC.SeatBelt = int.Parse(ddlSeatBelt.SelectedItem.Value);
            if (ddlPAR.SelectedIndex > 0 && ddlPAR.SelectedItem != null)
                AC.PersonalAccidentRider = int.Parse(ddlPAR.SelectedItem.Value);
            if (ddlCollision.SelectedIndex > 0 && ddlCollision.SelectedItem != null)
                AC.CollisionDeductible = int.Parse(ddlCollision.SelectedItem.Value);
            if (ddlComprehensive.SelectedIndex > 0 && ddlComprehensive.SelectedItem != null)
                AC.ComprehensiveDeductible = int.Parse(ddlComprehensive.SelectedItem.Value);
            if (txtDiscountCollComp.Text != "")
                AC.DiscountCompColl = decimal.Parse(txtDiscountCollComp.Text);
            if (ddlBI.SelectedIndex > 0 && ddlBI.SelectedItem != null)
                AC.BodilyInjuryLimit = int.Parse(ddlBI.SelectedItem.Value);
            if (ddlPD.SelectedIndex > 0 && ddlPD.SelectedItem != null)
                AC.PropertyDamageLimit = int.Parse(ddlPD.SelectedItem.Value);
            if (ddlCSL.SelectedIndex > 0 && ddlCSL.SelectedItem != null)
                AC.CombinedSingleLimit = int.Parse(ddlCSL.SelectedItem.Value);
            if (txtDiscountBIPD.Text != "")
                AC.DiscountBIPD = decimal.Parse(txtDiscountBIPD.Text);

            if (TxtTowing.Text.Trim() == "")
                TxtTowing.Text = "0";

            if (TxtVehicleRental.Text.Trim() == "")
                TxtVehicleRental.Text = "0";

            if (TxtAccidentDeathPremium.Text.Trim() == "")
                TxtAccidentDeathPremium.Text = "0";

            if (TxtEquitmentSonido.Text.Trim() == "")
                TxtEquitmentSonido.Text = "0";

            if (TxtEquitmentAudio.Text.Trim() == "")
                TxtEquitmentAudio.Text = "0";

            if (TxtEquitmentTapes.Text.Trim() == "")
                TxtEquitmentTapes.Text = "0";

            if (TxtEquipColl.Text.Trim() == "")
                TxtEquipColl.Text = "0";

            if (TxtEquipComp.Text.Trim() == "")
                TxtEquipComp.Text = "0";

            if (TxtUninsuredSingle.Text.Trim() == "")
                TxtUninsuredSingle.Text = "0";

            if (TxtUninsuredSplit.Text.Trim() == "")
                TxtUninsuredSplit.Text = "0";

            AC.OriginalTowingPremium = decimal.Parse(ddlTowing.SelectedItem.Value);
           
            AC.OriginalVehicleRental = decimal.Parse(GetOriginalVehicleRentaPremium());
            AC.TowingID = int.Parse(ddlTowing.SelectedItem.Value);

            //AC.TowingPremium = decimal.Parse(TxtTowing.Text.Trim());
            AC.TowingPremium = Decimal.Round(decimal.Parse(TxtTowing.Text.Trim()), 4);
            
            AC.VehicleRentalID = int.Parse(ddlRental.SelectedItem.Value);
            AC.VehicleRental = decimal.Parse(TxtVehicleRental.Text.Trim());
            AC.AccidentalDeathID = int.Parse(ddlAccidentDeath.SelectedItem.Value);
            AC.AccidentalDeathPremium = decimal.Parse(TxtAccidentDeathPremium.Text.Trim());
            AC.AccidentalDeathPerson = int.Parse(ddlADPersons.SelectedItem.Value);

            AC.EquipmentSoundID = int.Parse(ddlEquitmentSonido.SelectedItem.Value);
            AC.EquipmentSoundPremium = decimal.Parse(TxtEquitmentSonido.Text.Trim());
            AC.EquipmentAudioID = int.Parse(ddlEquitmentAudio.SelectedItem.Value);
            AC.EquipmentAudioPremium = decimal.Parse(TxtEquitmentAudio.Text.Trim());
            AC.EquipmentTapesPremium = decimal.Parse(TxtEquitmentTapes.Text);
            AC.EquipmentTapes = chkEquipTapes.Checked;
            AC.SpecialEquipmentCollPremium = decimal.Parse(TxtEquipColl.Text);
            AC.SpecialEquipmentColl = chkEquipColl.Checked;
            AC.SpecialEquipmentCompPremium = decimal.Parse(TxtEquipComp.Text);
            AC.SpecialEquipmentComp = chkEquipComp.Checked;
            AC.UninsuredSingleID = int.Parse(ddlUninsuredSingle.SelectedItem.Value);
            AC.UninsuredSinglePremium = decimal.Parse(TxtUninsuredSingle.Text.Trim());
            AC.UninsuredSplitID = int.Parse(ddlUninsuredSplit.SelectedItem.Value);
            AC.UninsuredSplitPremium = decimal.Parse(TxtUninsuredSplit.Text.Trim());
            AC.LoJack = chkLoJack.Checked;
            AC.LoJackCertificate = txtLoJackCertificate.Text.Trim();
            AC.LojackExpDate = TxtLojackExpDate.Text.Trim();

            //NO SE UTILIZA
            ddlRoadAssistEmp.SelectedIndex = -1;
            if (ddlRoadAssistEmp.SelectedItem.Text.Trim() != "0" && 1 == 0)
            {
                AC.IsAssistanceEmp = true;
                AC.AssistanceID = int.Parse(ddlRoadAssistEmp.SelectedItem.Value);
                AC.AssistancePremium = decimal.Parse(ddlRoadAssistEmp.SelectedItem.Text.Trim());
            }
            else
            {
                AC.IsAssistanceEmp = false;
                AC.AssistanceID = int.Parse(ddlRoadAssist.SelectedItem.Value);
                AC.AssistancePremium = decimal.Parse(ddlRoadAssist.SelectedItem.Text.Trim());
            }

            if (ddlEmployeeDiscount.SelectedIndex > 0 && ddlEmployeeDiscount.SelectedItem != null)
                AC.EmployeeDiscount = int.Parse(ddlEmployeeDiscount.SelectedItem.Value);

            //if (ddlExperienceDiscount.SelectedIndex > 0 && ddlExperienceDiscount.SelectedItem != null)
            //    AC.ExperienceDiscount = int.Parse(ddlExperienceDiscount.SelectedItem.Value);

            if (TxtExpDisc.Text.Trim() != "")
                AC.ExperienceDiscount = int.Parse(TxtExpDisc.Text.Trim()); // int.Parse(ddlExperienceDiscount.SelectedItem.Value);
            else
                AC.ExperienceDiscount = 0;


            if (txtMiscDiscount.Text != "")
                AC.MiscDiscount = double.Parse(txtMiscDiscount.Text);

            AC.License = txtLicenseNumber.Text.Trim();
            AC.IsLeasing = chkIsLeasing.Checked;
           // AC.LicenseExpDate = txtExpDate.Text.Trim();
            //AC.LicenseExpDate = Convert.ToDateTime(txtExpDate.Text.Trim()).ToString("MM/dd/yyyy");
            AC.LicenseExpDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(txtExpDate.Text.Trim()).ToShortDateString());

            if (ddlBank.SelectedIndex > 0 && ddlBank.SelectedItem != null)
                AC.Bank = ddlBank.SelectedItem.Value.ToString();

            if (ddlCompanyDealer.SelectedIndex > 0 && ddlCompanyDealer.SelectedItem != null)
                AC.CompanyDealer = ddlCompanyDealer.SelectedItem.Value.ToString();

            if (QuotesAutoID != 0)
            {
                AC.QuotesAutoId = QuotesAutoID;
                AC.Mode = 2; // Update
            }
            else
                AC.Mode = 1; // Insert

            if (InternalID != 0)
            {
                AC.InternalID = InternalID;
            }
            else  //?++
            {
                AC.InternalID = 0;
            }
            return AC;
        }

        private void SetDiscountForOneVehicleOnly()
        {
            //TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            //AutoCover autoCover = this.LoadFromForm(int.Parse(
            //    ViewState["QuotesAutoId"].ToString()),
            //    int.Parse(Session["InternalAutoID"].ToString().Trim()));

            //Login.Login cp = HttpContext.Current.User as Login.Login;
            //int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            //this.ReapplyDiscounts();

            //QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            //foreach (AutoCover aC in QA.AutoCovers)
            //{
            //    aC.Mode = 2; //Update
            //}

            //if (QA.IsPolicy)
            //    QA.Save(UserID, autoCover, null, false);
            //else
            //    QA.Save(UserID);

            //Session["TaskControl"] = QA;

            //this.ShowAutoCover((AutoCover)QA.AutoCovers[
            //    QA.AutoCovers.IndexOf(autoCover)]);

            ////  this.applyToAll.Value = "0";
            //this.SetControlState((int)States.READONLY);
        }
        private void ReapplyDiscounts()
        {
            //if (Session["TaskControl"] != null)
            //{
            //    TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto)Session["TaskControl"];
            //    AutoCover cover;

            //    for (int i = 0; i < quoteAuto.AutoCovers.Count; i++)
            //    {
            //        cover = (AutoCover)quoteAuto.AutoCovers[i];

            //        this.SetDiscount(quoteAuto, ref cover);
            //        //this.SetDiscountRelatedValues(cover.PolicySubClassId, true, ref cover);
            //    }

            //    //quoteAuto.Calculate();
            //    Session["TaskControl"] = quoteAuto;
            //}
        }
        private void SetDiscount(TaskControl.QuoteAuto qAuto, ref AutoCover CoverToApply)
        {
            if (CoverToApply.CollisionDeductible.ToString().Trim() != "0")
            {
                //CoverToApply.DiscountCompColl = CollCompDisc;
                //CoverToApply.DiscountBIPD = BiPdDisc;
            }
        }
        private enum States { NEW, READONLY, READWRITE, REST };
        private void SetInputState(bool Enabled)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            this.txtVIN2.Enabled = Enabled;
            this.ddlPolicySubClass.Enabled = Enabled;
            this.ddlVehicleClass.Enabled = Enabled;
            this.ddlCollision.Enabled = Enabled;
            this.txtPlate.Enabled = Enabled;
            this.ddlTerritory.Enabled = Enabled;
            this.ddlComprehensive.Enabled = Enabled;
            this.ddlYear.Enabled = Enabled;
            this.ddlAlarm.Enabled = Enabled;
            this.txtDiscountCollComp.Enabled = Enabled;
            this.ddlMake.Enabled = Enabled;

            this.ddlExperienceDiscount.Enabled = Enabled;
            this.ddlEmployeeDiscount.Enabled = Enabled;
            this.txtMiscDiscount.Enabled = Enabled;
            // TODO Depreciation

            this.ddlBI.Enabled = Enabled;
            this.ddlModel.Enabled = Enabled;

            this.ddlPD.Enabled = Enabled;
            this.txtPurchaseDt.Enabled = Enabled;
            this.ddlMedical.Enabled = Enabled;
            this.ddlCSL.Enabled = Enabled;
            //this.txtAge.Enabled = Enabled;
            //this.ddlAssistancePremium.Enabled = Enabled;

            this.txtDiscountBIPD.Enabled = Enabled;
            this.ddlNewUsed.Enabled = Enabled;
            this.txtTowingPrm.Enabled = Enabled;
            //this.txtVehicleRental.Enabled = Enabled;
            this.ddlRental.Enabled = Enabled;
            this.txtCost.Enabled = Enabled;
            this.txtPlate.Enabled = Enabled;
            this.txtPurchaseDt.Enabled = Enabled;
            this.ddlCompanyDealer.Enabled = Enabled;
            this.ddlBank.Enabled = Enabled;
            this.txtLicenseNumber.Enabled = Enabled;
            this.txtExpDate.Enabled = Enabled;
            this.txtVIN2.Enabled = Enabled;
            this.ddlLoanGap.Enabled = Enabled;
            this.txtActualValue.Enabled = Enabled;
            this.ddlSeatBelt.Enabled = Enabled;

            this.ddlPAR.Enabled = Enabled;

            //this.btnCalendar.Disabled = !Enabled;

            txtLicenseNumber.Enabled = Enabled;
            txtExpDate.Enabled = Enabled;
            chkIsLeasing.Enabled = Enabled;


        }
        private void SetControlState(int State)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            switch (State)
            {
                case (int)States.NEW:
                    this.btnSave.Visible = false;
                    this.btnDrivers.Visible = false;
                    //  this.btnAssignDrv.Visible = false;
                    this.btnEdit.Visible = false;
                    this.btnCancel.Visible = true;
                    this.SetInputState(false);
                    this.ddlPolicySubClass.Enabled = true;
                    this.btnViewCvr.Visible = false;
                    //  this.btnAddVhcl.Visible = false;
                    this.ddlDriver.Enabled = true;
                    // this.btnBack.Visible = false;
                    // this.chkOnlyOperator.Enabled = true;
                    // this.chkPrincipalOperator.Enabled = true;
                    // this.TxtVehicleCount.Enabled = true;
                    break;
                case (int)States.READONLY:
                    this.btnSave.Visible = false;
                    this.btnDrivers.Visible = true;
                    // this.btnAssignDrv.Visible = false; //true;
                    this.btnEdit.Visible = true;
                    this.btnCancel.Visible = false;
                    this.SetInputState(false);
                    this.btnViewCvr.Visible = true;
                    // this.btnAddVhcl.Visible = true;
                    this.ddlDriver.Enabled = false;
                    // this.btnBack.Visible = true;
                    // this.chkOnlyOperator.Enabled = false;
                    // this.chkPrincipalOperator.Enabled = false;
                    //this.TxtVehicleCount.Enabled = false;
                    break;
                case (int)States.READWRITE:
                    this.btnSave.Visible = true;
                    this.btnDrivers.Visible = false;
                    //this.btnAssignDrv.Visible = false; //true;
                    this.btnEdit.Visible = false;
                    this.btnCancel.Visible = true;
                    this.SetInputState(true);
                    this.btnViewCvr.Visible = false;
                    //this.btnAddVhcl.Visible = false;
                    this.ddlDriver.Enabled = true;
                    //this.btnBack.Visible = false;
                    //this.chkOnlyOperator.Enabled = true;
                    //this.chkPrincipalOperator.Enabled = true;
                    //this.TxtVehicleCount.Enabled = true;
                    break;
                case (int)States.REST:
                    this.btnSave.Visible = false;
                    this.btnDrivers.Visible = false;
                    //this.btnAssignDrv.Visible = false;
                    this.btnEdit.Visible = false;
                    this.btnCancel.Visible = false;
                    this.SetInputState(false);
                    this.btnViewCvr.Visible = false;
                    //this.btnAddVhcl.Visible = true;
                    this.ddlDriver.Enabled = true;
                    //this.btnBack.Visible = false;
                    //this.chkOnlyOperator.Enabled = true;
                    //this.chkPrincipalOperator.Enabled = true;
                    //this.TxtVehicleCount.Enabled = false;
                    break;
                default:
                    //
                    break;
            }

            ShowPolicyFields(QA, State);
        }
        private void ShowPolicyFields(TaskControl.QuoteAuto QA, int State)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            if (!cp.IsInRole("AUTO PERSONAL MISC DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                txtMiscDiscount.Visible = false;
                lblMiscDisc.Visible = false;
            }
            else
            {
                //txtMiscDiscount.Visible = true;
                //lblMiscDisc.Visible = true;
            }

            if (!cp.IsInRole("AUTO PERSONAL EMPLOYEE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlEmployeeDiscount.Visible = false;
                lblEmployeeDiscount.Visible = false;
            }
            else
            {
                //ddlEmployeeDiscount.Visible = true;
                //lblEmployeeDiscount.Visible = true;
            }

            if (!cp.IsInRole("AUTO PERSONAL EXPERIENCE DISCOUNT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlExperienceDiscount.Visible = false;
                lblExperienceDiscount.Visible = false;
                TxtExpDisc.Visible = false;
            }
            else
            {
                ddlExperienceDiscount.Visible = true;
                lblExperienceDiscount.Visible = true;
                TxtExpDisc.Visible = true;
            }

            if (QA.IsPolicy)
            {
                //this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.
                lblPrimaryDriver.Visible = false;

                //this.LblBank.Visible = true;
                //this.LblDealer.Visible = true;

                this.ddlBank.Visible = true;
                this.ddlCompanyDealer.Visible = true;
                this.ddlDriver.Visible = true;

                //this.btnNext.Visible = true;

                this.HplAdd.Visible = false; // true;
                //this.chkOnlyOperator.Visible = true;
                //this.chkPrincipalOperator.Visible = true;
            }
            else
            {
                //this.btnAssignDrv.Visible = false;
                lblPrimaryDriver.Visible = false;
                //this.LblBank.Visible = false;
                // this.LblDealer.Visible = false;


                this.ddlBank.Visible = true;
                this.ddlCompanyDealer.Visible = true;

                this.ddlDriver.Visible = true;
                //      this.btnNext.Visible = false;
                this.HplAdd.Visible = false;

                //    this.chkOnlyOperator.Visible = true;
                //   this.chkPrincipalOperator.Visible = true;
            }

            if (Session["OptimaPersonalPackage"] != null)
            {
                switch (State)
                {
                    case (int)States.NEW:
                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        break;
                    case (int)States.READONLY:
                        this.ddlBank.Enabled = false;
                        this.ddlCompanyDealer.Enabled = false;
                        break;
                    case (int)States.READWRITE:
                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        break;
                    case (int)States.REST:
                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        break;
                }
            }

            if (QA.IsPolicy)
            {
                switch (State)
                {
                    case (int)States.NEW:
                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        this.ddlDriver.Enabled = true;

                        this.HplAdd.Enabled = false; // true;
                        //               this.chkOnlyOperator.Enabled = true;
                        //             this.chkPrincipalOperator.Enabled = true;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            //               lblPrimaryDriver.Visible = false;
                            //             this.btnAssignDrv.Visible = false;
                            this.btnEdit.Visible = false;
                            this.btnSave.Visible = false;
                            this.btnCancel.Visible = false;
                            //           this.btnAddVhcl.Visible = false;
                            this.btnViewCvr.Visible = false;
                            this.btnDrivers.Visible = false;
                            //         this.btnBack.Visible = false;
                            //       this.btnNext.Visible = true;
                        }
                        else
                        {
                            lblPrimaryDriver.Visible = false;
                            //     this.btnAssignDrv.Visible = false;
                            this.btnEdit.Visible = false;
                            this.btnSave.Visible = true;
                            this.btnCancel.Visible = true;
                            //   this.btnAddVhcl.Visible = false;
                            this.btnViewCvr.Visible = false;
                            this.btnDrivers.Visible = false;
                            // this.btnBack.Visible = true;
                            // this.btnNext.Visible = false;
                        }
                        break;
                    case (int)States.READONLY:
                        //this.chkOnlyOperator.Enabled = false;
                        //this.chkPrincipalOperator.Enabled = false;
                        lblPrimaryDriver.Visible = false;
                        //this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.

                        this.ddlBank.Enabled = false;
                        this.ddlCompanyDealer.Enabled = false;
                        this.ddlDriver.Enabled = false;

                        this.HplAdd.Enabled = false;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            //  this.btnNext.Visible = true;
                        }
                        else
                        {
                            //this.btnNext.Visible = false;
                            //this.btnBack.Visible = true;
                            this.btnEdit.Visible = true;
                            this.btnViewCvr.Visible = true;
                        }
                        break;

                    case (int)States.READWRITE:
                        //this.chkOnlyOperator.Enabled = true;
                        //this.chkPrincipalOperator.Enabled = true;
                        lblPrimaryDriver.Visible = false;
                        //this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.

                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        this.ddlDriver.Enabled = true;
                        //this.btnBack.Visible = false;

                        this.HplAdd.Enabled = false; // true;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            //  this.btnNext.Visible = false;
                        }
                        else
                        {
                            // this.btnNext.Visible = false;
                        }

                        break;

                    case (int)States.REST:
                        //this.chkOnlyOperator.Enabled = true;
                        //this.chkPrincipalOperator.Enabled = true;
                        lblPrimaryDriver.Visible = false;
                        //this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.

                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        this.ddlDriver.Enabled = true;

                        // this.btnNext.Visible = false;
                        this.HplAdd.Enabled = false; // true;

                        //this.btnDrivers.Visible = false;
                        //this.btnAddVhcl.Visible = false;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            //   this.btnNext.Visible = false;
                        }
                        else
                        {
                            // this.btnNext.Visible = false;
                            //this.btnBack.Visible = true;
                            this.btnEdit.Visible = true;
                            this.btnViewCvr.Visible = true;
                        }
                        break;

                    default:
                        //
                        break;
                }
            }
        }
        private void SelectLastVehicle()
        {
            System.Web.UI.WebControls.DataGridCommandEventArgs e;
            object argument = new Object();
            System.Web.UI.WebControls.CommandEventArgs originalArgs =
                new System.Web.UI.WebControls.CommandEventArgs("dbDriverList_ItemCommand", argument);

            if (this.dgVehicle.Items.Count > 0)
            {
                e = new System.Web.UI.WebControls.DataGridCommandEventArgs(
                    this.dgVehicle.Items[this.dgVehicle.Items.Count - 1],
                    this.dgVehicle, originalArgs);
                this.SelectVehicle(e);
                this.dgVehicle.SelectedIndex = this.dgVehicle.Items.Count - 1;
            }
        }
        protected void btnAddVhcl_Click(object sender, EventArgs e)
        {
            IspolicyNewButtonClick();
        }

        private void IspolicyNewButtonClick()
        {
            if (this.AdditionOfCoverAllowedOnGroundsOfBaseType((TaskControl.QuoteAuto)Session["TaskControl"]))
            {
                ViewState.Add("Status", "NEW");
                clearFields();

                enableFields(true);
                // enableFieldsForAddNewAuto();
                ddlPolicySubClass.Enabled = true;
                btnSave.Visible = false;
                btnViewCvr.Visible = false;
                this.SetControlState((int)States.NEW);
                //this.ddlPolicySubClass.BackColor = System.Drawing.Color.Red;
                this.dgVehicle.SelectedIndex = -1;

                if (TxtVehicleCount.Text == "")
                    TxtVehicleCount.Text = "1";

                if (Session["TaskControl"] != null)
                {
                    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)(TaskControl.QuoteAuto)Session["TaskControl"];

                    ddlDriver.Enabled = false;  //For Policy & Quote					

                    if (QA.AutoCovers.Count > 0)
                    {
                        AutoCover cover = (AutoCover)QA.AutoCovers[QA.AutoCovers.Count - 1];
                        System.Web.UI.WebControls.ListItem item = this.ddlPolicySubClass.Items.FindByValue(
                            cover.PolicySubClassId.ToString());

                        this.ddlPolicySubClass.SelectedIndex = this.ddlPolicySubClass.Items.IndexOf(item);
                        this.ddlPolicySubClass_SelectedIndexChanged(this, new EventArgs());
                    }
                    else
                    {
                        // Fill data from ApplicationAuto of the first vehicle.
                        Quotes.AutoCover ACTemp = (Quotes.AutoCover)Session["AutoCoverFromApp"];

                        txtVIN2.Text = ACTemp.VIN;

                        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(
                            ACTemp.VehicleYear.ToString()));

                        ddlMake.SelectedIndex = ddlMake.Items.IndexOf(ddlMake.Items.FindByValue(
                            ACTemp.VehicleMake.ToString()));

                        fillModel();

                        ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByValue(
                            ACTemp.VehicleModel.ToString()));



                        ddlVehicleClass.SelectedIndex = ddlVehicleClass.Items.IndexOf(ddlVehicleClass.Items.FindByValue(
                            ACTemp.VehicleClass.ToString()));

                        ddlTerritory.SelectedIndex = ddlTerritory.Items.IndexOf(ddlTerritory.Items.FindByValue(
                            ACTemp.Territory.ToString()));

                        ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(
                            ACTemp.Bank.ToString()));

                        ddlCompanyDealer.SelectedIndex = ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(
                            ACTemp.CompanyDealer.ToString()));

                        //Seleccionar el primer driver por default siempre y cuando haya en la lista un driver.
                        // Seleccionara el driver que tenga mayor prioridad
                        //aqui
                        if (ddlDriver.Items.Count >1)
                        {

                            ddlDriver.SelectedIndex = 1;
                        }
                    }
                }
            }
            else
            {
                lblRecHeader.Text = Utilities.MakeLiteralPopUpString(
                    "Only one cover is allowed on a Double Interest based policy. " +
                    "In order to add an additional cover, edit the existing Double " +
                    "Interest cover and change it's Policy Sub Class to one based on " +
                    "Full Cover or Liability base types.");
                mpeSeleccion.Show();
            }
        }
        private bool AdditionOfCoverAllowedOnGroundsOfBaseType(
            TaskControl.QuoteAuto QA)
        {
            foreach (AutoCover cover in QA.AutoCovers)
            {
                if (this.GetBasePolicySubClassFromPolicySubClassID(
                    cover.PolicySubClassId) == 1) //DI
                    return false;
            }
            return true;
        }

        // CUSTOM ---- **** *#&#******
        protected void enableFieldsForAddNewAuto()
        {
            ddlMake.Enabled = true;
            ddlModel.Enabled = true;
            ddlInsuranceCompany.Enabled = true;
            ddlYear.Enabled = true;
            //txtAge.Enabled = true;
            ddlNewUsed.Enabled = true;
            txtCost.Enabled = true;

            txtPlate.Enabled = true;
            txtPurchaseDt.Enabled = true;
            ddlCompanyDealer.Enabled = true;
            ddlBank.Enabled = true;
            txtLicenseNumber.Enabled = true;
            txtExpDate.Enabled = true;

            txtActualValue.Enabled = true;
            ddlCompanyDealer.Enabled = true;
            ddlCompanyDealer.Enabled = true;
            txtVIN2.Enabled = true;
            txtPlate.Enabled = true;
            TxtpurchaseDate.Enabled = true;
            ddlAlarm.Enabled = true;
            ddlVehicleClass.Enabled = true;
            ddlTerritory.Enabled = true;
            rdo15percent.Enabled = true;
            rdo20percent.Enabled = true;
            txtExpDate.Enabled = true;
            ddlBank.Enabled = true;
            ddlCollision.Enabled = true;
            ddlComprehensive.Enabled = true;
            txtDiscountCollComp.Enabled = true;
            ddlBI.Enabled = true;
            ddlPD.Enabled = true;
            ddlCSL.Enabled = true;
            txtDiscountBIPD.Enabled = true;
            ddlMedical.Enabled = true;
            ddlLoanGap.Enabled = true;
            chkLLG.Enabled = true;
            txtRoadsideAssitance.Enabled = true;
            txtTowingPrm.Enabled = true;
            ddlTowing.Enabled = true;
            ddlRental.Enabled = true;
            TxtVehicleRental.Enabled = true;
            TxtTowing.Enabled = true;

            ddlExperienceDiscount.Enabled = true;
            TxtExpDisc.Enabled = false;
            ddlEmployeeDiscount.Enabled = true;
            txtMiscDiscount.Enabled = true;
        }



        protected void btnSaveAuto_Click(object sender, EventArgs e)
        {
            try
            {
                SaveVehicle();

            }
            catch (Exception ecp)
            {
                lblRecHeader.Text = ecp.Message.Trim();
                mpeSeleccion.Show();
            }
        }

        private void ApplyDiscountSecondDiscount(TaskControl.QuoteAuto QA)
        {

            Login.Login cp = HttpContext.Current.User as Login.Login;

            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            bool IsBIPD = false;

            bool IsCollComp = false;



            if (QA.AutoCovers.Count > 1)
            {

                int nimQuoteAutoID = 0;

                int maxQuoteAutoID = 0;

                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {

                    Quotes.AutoCover cover = (AutoCover)QA.AutoCovers[i];

                    maxQuoteAutoID = cover.QuotesAutoId;

                    if (nimQuoteAutoID < maxQuoteAutoID)
                    {

                        if (nimQuoteAutoID == 0)
                        {

                            nimQuoteAutoID = maxQuoteAutoID;

                        }

                    }

                    else
                    {

                        if (maxQuoteAutoID != 0)
                        {

                            nimQuoteAutoID = maxQuoteAutoID;

                        }

                    }

                    cover = null;

                }



                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {

                    Quotes.AutoCover cover = (AutoCover)QA.AutoCovers[i];

                    if ((nimQuoteAutoID < cover.QuotesAutoId) || cover.QuotesAutoId == 0)
                    {

                        if (cover.BodilyInjuryPremium() > 0)
                        {

                            IsBIPD = true;

                        }



                        if (cover.CollisionPremium() > 0)
                        {

                            IsCollComp = true;

                        }

                    }

                    cover = null;

                }



                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {

                    Quotes.AutoCover cover = (AutoCover)QA.AutoCovers[i];

                    string insco = QA.InsuranceCompany;

                    decimal OldDiscBIPD = 0;

                    decimal OldDiscComp = 0;



                    bool qualifiesForMultiAutoBIPDdiscount = this.QualifiesFor_MoreThanOneAuto_Discount(true,

                    QA.AutoCovers, cover);

                    bool qualifiesForMultiAutoCOLLCOMPdiscount = this.QualifiesFor_MoreThanOneAuto_Discount(false,

                        QA.AutoCovers, cover);



                    OldDiscBIPD = -20; //cover.DiscountBIPD;

                    OldDiscComp = -20; //cover.DiscountCompColl;



                    cover.DiscountBIPD = OldDiscBIPD;

                    cover.DiscountCompColl = OldDiscComp;

                    //cover.MiscDiscount = double.Parse(OldDiscBIPD.ToString());                  



                    if (IsBIPD == false)

                        cover.DiscountBIPD = OldDiscBIPD;

                    //cover.MiscDiscount = double.Parse(OldDiscBIPD.ToString());



                    if (IsCollComp == false)

                        cover.DiscountCompColl = OldDiscComp;

                    // cover.MiscDiscount = double.Parse(OldDiscBIPD.ToString());



                    //Verifica si no tiene cubierta poene en 0 el descuento.

                    if (cover.BodilyInjuryLimit == 0)//BodilyInjuryPremium() == 0)

                        cover.DiscountBIPD = 0;

                    //cover.MiscDiscount = 0;



                    if (cover.CollisionDeductible == 0) //CollisionPremium() == 0)

                        cover.DiscountCompColl = 0;

                    //cover.MiscDiscount = 0;



                    cover.Mode = (int)Enumerators.Modes.Update;



                    if (QA.TaskControlID != 0)
                    {

                        QA.Mode = (int)Enumerators.Modes.Update;

                    }

                    else
                    {

                        QA.Mode = (int)Enumerators.Modes.Insert;

                    }



                    QA.Save(UserID, cover, null, false);

                    cover = null;

                }

            }

            else
            {

                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {

                    Quotes.AutoCover cover = (AutoCover)QA.AutoCovers[i];

                    string insco = QA.InsuranceCompany;

                    decimal OldDiscBIPD = 0;

                    decimal OldDiscComp = 0;





                    OldDiscBIPD = 0; //cover.DiscountBIPD;

                    OldDiscComp = 0; //cover.DiscountCompColl;



                    cover.DiscountBIPD = OldDiscBIPD;

                    cover.DiscountCompColl = OldDiscComp;

                    //cover.MiscDiscount = double.Parse(OldDiscBIPD.ToString());                                   



                    cover.Mode = (int)Enumerators.Modes.Update;



                    if (QA.TaskControlID != 0)
                    {

                        QA.Mode = (int)Enumerators.Modes.Update;

                    }

                    else
                    {

                        QA.Mode = (int)Enumerators.Modes.Insert;

                    }



                    QA.Save(UserID, cover, null, false);

                    cover = null;

                }

            }

        }

        private string ValidateThisForMoreThanOneAuto()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)(TaskControl.QuoteAuto)Session["TaskControl"];

            ArrayList errorMessages = new ArrayList();
            int term = 0;
            try
            {
                term = int.Parse(txtTerm.Text.Trim());
            }
            catch
            {
                term = 0;
            }


            if (QA.IsPolicy) //Valida solo cuando es poliza.
            {
                if (txtTerm.Text.Length == 0 || term == 0)
                {
                    errorMessages.Add("Please, fill term");
                }

                if (this.txtVIN2.Text.Trim() == "")
                {
                    errorMessages.Add("Please the VIN is empty." + ".\r\n");
                }

                if (this.txtVIN2.Text.Length < 16)
                {
                    errorMessages.Add("VIN must be 17 or 16 " +
                        " characters in length." + ".\r\n");
                }

                if (this.TxtpurchaseDate.Text.Trim() == "")
                {
                    errorMessages.Add("Please The Purchase Date is empty." + ".\r\n");
                }

                if (!this.IsSamsValidDate(this.TxtpurchaseDate.Text))
                {
                    errorMessages.Add("Purchase date is invalid.  The " +
                        "correct format is \"mm/dd/yyyy\"." + ".\r\n");
                }
            }

            if (ddlDriver.SelectedItem.Text.Trim() == "")
            {
                errorMessages.Add("Please choose the driver for this vehicle." + ".\r\n");
            }

            //Verifica si el vehiculo es usado entonces el vehicleAge debe ser mayor que 0.
            if (int.Parse(ddlNewUsed.SelectedItem.Value) == 1)//Used
            {
                if (txtAge.Text.Trim() == "")
                {
                    errorMessages.Add("The Vehicle Age must be greatest than zero.");
                }
                else
                {
                    if (int.Parse(txtAge.Text.Trim()) == 0)
                    {
                        errorMessages.Add("The Vehicle Age must be greatest than zero.");
                    }
                }

                //Para uso solamente de Double Interest o Full Cover.
                if (ddlCollision.SelectedItem.Value != "" || ddlComprehensive.SelectedItem.Value != "")
                {
                    if (txtActualValue.Text == "")
                    {
                        errorMessages.Add("The actual value must be greatest than zero.");
                    }
                    else
                    {
                        if (int.Parse(txtActualValue.Text.Trim()) == 0)
                        {
                            errorMessages.Add("The actual value must be greatest than zero.");
                        }
                    }
                }
            }
            else  //Si el vehiculo es nuevo el VehicleAge debe ser cero.
            {
                if (txtAge.Text.Trim() == "")
                {
                    errorMessages.Add("The Vehicle Age must be zero.");
                }
                else
                {
                    if (int.Parse(txtAge.Text.Trim()) > 0)
                    {
                        errorMessages.Add("The Vehicle Age must be zero.");
                    }
                }
            }

            //Territorry
            if (ddlTerritory.SelectedItem.Value == "")
            {
                errorMessages.Add("Territory is missing.");
            }

            if (ddlCollision.SelectedItem.Value == "" && ddlComprehensive.SelectedItem.Value == "" &&
                ddlBI.SelectedItem.Value == "" && ddlPD.SelectedItem.Value == "" &&
                ddlCSL.SelectedItem.Value == "")
            {
                errorMessages.Add("The following covers are missing; Collision, Comprehensive, Bodily Injury, Physical Damage.");
            }

            if (ddlCollision.SelectedItem.Value != "" && ddlComprehensive.SelectedItem.Value != "" &&
                ddlBI.SelectedItem.Value == "" && ddlPD.SelectedItem.Value == "" &&
                ddlCSL.SelectedItem.Value == "" && term < 12)
            {
                errorMessages.Add("The term for double interest is wrong, please verify.");
            }

            //Para uso solamente de Double Interest o Full Cover.
            if (ddlCollision.SelectedItem.Value != "" || ddlComprehensive.SelectedItem.Value != "")
            {
                //NewUse
                if (ddlNewUsed.SelectedItem.Text.Trim() == "")
                {
                    errorMessages.Add("New / Use is missing.");
                }

                //Verifica el costo del vehiculo.
                if (txtCost.Text == "")
                {
                    errorMessages.Add("The vehicle cost must be greatest than zero.");
                }
                else
                {
                    if (int.Parse(txtCost.Text.Trim()) == 0)
                    {
                        errorMessages.Add("The vehicle cost must be greatest than zero.");
                    }
                    else
                        //Mendez,El Morro, Open Mobile, Wesleyan, Interfood,Acaa,Fundacion
                        if (QA.PolicyType == "M02" || QA.PolicyType == "M03" ||
                            QA.PolicyType == "M04" || QA.PolicyType == "M05" ||
                            QA.PolicyType == "M06" || QA.PolicyType == "M07" ||
                            QA.PolicyType == "M08" || QA.PolicyType == "M09" ||
                            QA.PolicyType == "M10" || QA.PolicyType == "M11" ||
                            QA.PolicyType == "M12" || QA.PolicyType == "M13" ||
                            QA.PolicyType == "M14" || QA.PolicyType == "M15" ||
                            QA.PolicyType == "M16")
                    {
                        if (int.Parse(txtActualValue.Text.Trim()) > 55000 &&
                                                      (!cp.IsInRole("AUTOACV55000") && !cp.IsInRole("ADMINISTRATOR")))
                        {
                            errorMessages.Add("The Actual Cash Value must be equal or less than $55,000.");
                        }
                    }
                }
            }

            //Verifica el costo del vehiculo.
            if (ddlCollision.SelectedItem.Value != "" || ddlComprehensive.SelectedItem.Value != "")
            {
                //NewUse
                if (ddlNewUsed.SelectedItem.Text.Trim() == "")
                {
                    errorMessages.Add("New / Use is missing.");
                }

                if (txtCost.Text == "")
                {
                    errorMessages.Add("The vehicle cost must be greatest than zero.");
                }
                else
                {
                    if (int.Parse(txtCost.Text.Trim()) == 0)
                    {
                        errorMessages.Add("The vehicle cost must be greatest than zero.");
                    }
                }
            }


            //Verifica T�rmino y PolicySubClass			
            if (ddlPolicySubClass.SelectedIndex > 0 && ddlPolicySubClass.SelectedItem != null)  // && TxtInsCode.Text.Trim() != "")
            {
                //Double Interest
                if (ddlComprehensive.SelectedItem.Value != "" && (ddlBI.SelectedItem.Value == "" && ddlCSL.SelectedItem.Value == ""))
                {
                    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 1)
                    {
                        if (term <= 12)
                            errorMessages.Add("Please verify the term for Double Interest Cover.");
                    }
                    else
                    {
                        errorMessages.Add("The Policy Type must be Double Interest Cover.");
                    }
                }

                //Liability
                if (ddlComprehensive.SelectedItem.Value == "" && (ddlBI.SelectedItem.Value != "" || ddlCSL.SelectedItem.Value != ""))
                {
                    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 2)
                    {
                        if (term > 12)
                            errorMessages.Add("Please verify the term for Liability Cover.");
                    }
                    else
                    {
                        errorMessages.Add("The Policy Type must be Liability Cover.");
                    }
                }

                //FullCover
                if (ddlComprehensive.SelectedItem.Value != "" && (ddlBI.SelectedItem.Value != "" || ddlCSL.SelectedItem.Value != ""))
                {
                    if (int.Parse(ddlPolicySubClass.SelectedItem.Value) == 3)
                    {
                        if (term > 12)
                            errorMessages.Add("Please verify the term for FullCover.");
                    }
                    else
                    {
                        errorMessages.Add("The Policy Type must be FullCover.");
                    }
                }
            }

            if (cp.IsInRole("AUTOLIMITDISCOUNT"))
            {
                if (QA.PolicyType == "MFC")
                {
                    if (QA.AutoCovers.Count == 1 && ViewState["Status"].ToString() != "NEW")
                    {
                        if (double.Parse(txtDiscountCollComp.Text.Trim()) > 40.0) //50
                        {
                            errorMessages.Add("The Coll/Comp Discount limit must be 40%.");
                        }
                        if (double.Parse(txtDiscountBIPD.Text.Trim()) > 40.0)
                        {
                            errorMessages.Add("The BI/PD Discount limit must be 40%.");
                        }
                    }

                    if (QA.AutoCovers.Count > 1 || (QA.AutoCovers.Count == 1 && ViewState["Status"].ToString() == "NEW"))
                    {
                        if (IsFirstVehicleHasDisc(QA, 40)) //50
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 40.0) //55
                            {
                                errorMessages.Add("The Coll/Comp Discount limit must be 40%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 40.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit must be 40%.");
                            }
                        }
                        else //Si existe un auto con menos de 50 debe de asignar un 50%.
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 40.0) //50
                            {
                                errorMessages.Add("The Coll/Comp Discount limit must be 40%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 40.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit must be 40%.");
                            }
                        }
                    }
                }

                if (QA.PolicyType == "MFE")
                {
                    if (QA.AutoCovers.Count == 1 && ViewState["Status"].ToString() != "NEW")
                    {
                        if (double.Parse(txtDiscountCollComp.Text.Trim()) > 40.0) //50
                        {
                            errorMessages.Add("The Coll/Comp Discount limit must be 40%.");
                        }
                        if (double.Parse(txtDiscountBIPD.Text.Trim()) > 40.0)
                        {
                            errorMessages.Add("The BI/PD Discount limit must be 40%.");
                        }
                    }

                    if (QA.AutoCovers.Count > 1 || (QA.AutoCovers.Count == 1 && ViewState["Status"].ToString() == "NEW"))
                    {
                        if (IsFirstVehicleHasDisc(QA, 40)) //50
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 40.0) //60
                            {
                                errorMessages.Add("The Coll/Comp Discount limit must be 40%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 40.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit must be 40%.");
                            }
                        }
                        else //Si existe un auto con menos de 50 debe de asignar un 50%.
                        {
                            if (double.Parse(txtDiscountCollComp.Text.Trim()) > 40.0) //50
                            {
                                errorMessages.Add("The Coll/Comp Discount limit must be 40%.");
                            }
                            if (double.Parse(txtDiscountBIPD.Text.Trim()) > 40.0)
                            {
                                errorMessages.Add("The BI/PD Discount limit must be 40%.");
                            }
                        }
                    }
                }
            }

            if (Session["OptimaPersonalPackage"] != null)
            {
                cp = HttpContext.Current.User as Login.Login;
                if (cp.IsInRole("UNDERWRITTERRULESOPP"))
                {
                    DataTable dt = EPolicy.TaskControl.Policy.GetUnderwritterRulesByUnderwritterRulesID(1);

                    if (txtActualValue.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoBI2"].ToString().Trim()) < double.Parse(txtActualValue.Text.Trim()))
                            errorMessages.Add("The limit for Actual Cash Value is $" + dt.Rows[0]["AutoBI2"].ToString().Trim() + ".");
                    }

                    if (ddlBI.SelectedItem.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoBI"].ToString().Trim()) < double.Parse(ddlBI.SelectedItem.Value.Trim()))
                            errorMessages.Add("The limit for Bodily Injury coves is $250,000.");
                    }

                    if (ddlPD.SelectedItem.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoPD"].ToString().Trim()) < double.Parse(ddlPD.SelectedItem.Text.Trim()))
                            errorMessages.Add("The limit for Property Damage coves is $" + dt.Rows[0]["AutoPD"].ToString().Trim() + ",000" + ".");
                    }

                    if (ddlCollision.SelectedItem.Text.Trim() != "")
                    {
                        if (double.Parse(dt.Rows[0]["AutoDeducible"].ToString().Trim()) < double.Parse(ddlCollision.SelectedItem.Text.Trim()))
                            errorMessages.Add("The deductible for this vehicle is $" + dt.Rows[0]["AutoDeducible"].ToString().Trim() + ".");
                    }
                }
            }

            string popUpString = "";
            if (errorMessages.Count > 0)
            {
                foreach (string message in errorMessages)
                {
                    popUpString += message + " ";
                }

            }
            return popUpString;
        }

        private bool IsFirstVehicleHasDisc(TaskControl.QuoteAuto QA, decimal discAmt)
        {
            bool result = false;

            for (int a = 0; a < QA.AutoCovers.Count; a++)
            {
                AutoCover ac = (AutoCover)QA.AutoCovers[a];
                if (ac.DiscountCompColl <= discAmt)
                {
                    result = true;
                    a = QA.AutoCovers.Count;
                }
            }

            return result;
        }

        private void SaveVehicle()
        {

            string resultMess = this.ValidateThisForMoreThanOneAuto();
            if (resultMess != "")
            {
                throw new Exception(resultMess);
            }
            else
            {
                /*if (Page.IsValid) 
                {*/
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)(TaskControl.QuoteAuto)Session["TaskControl"];
                AutoCover AC = null;
                switch (ViewState["Status"].ToString())
                {
                    case "NEW":
                        AC = LoadFromForm(0, QA.GetNewInternalID());
                        AC.Mode = (int)Enumerators.Modes.Insert;
                        ViewState.Add("Status", "NEW");

                        if (ValidateNew(AC))
                        {
                            QA.AddAutoCover(AC, false);
                            ViewState.Add("Status", "NEW");

                            //RPR 2004-03-23,24
                            //if(this.DifferentCoverPercentages(AC, QA))
                            if (this.PendingMultipleVehicleDiscount(QA))
                            {
                                //ApplyDiscountSecondDiscount(QA);
                                /*this.ApplyDiscountToExistingCovers(
                                    AC.DiscountBIPD, AC.DiscountCompColl);*/

                                //QA.Calculate();
                                Session["TaskControl"] = QA;
                            }

                            //RPR 2004-03-16
                            ViewState.Add("InternalAutoID",
                                AC.InternalID.ToString());

                            btnSave.Visible = false;
                            // btnAssignDrv.Visible = false;
                            btnViewCvr.Visible = false;
                        }
                        break;
                    case "UPDATE":
                        // Save Cover
                        int t = 0, iid = 0;
                        if (ViewState["QuotesAutoId"] != null)
                            t = int.Parse(ViewState["QuotesAutoId"].ToString());

                        if (ViewState["InternalAutoID"] != null)
                            iid = int.Parse(ViewState["InternalAutoID"].ToString());
                        else
                            iid = QA.GetNewInternalID();

                        AC = LoadFromForm(t, iid);

                        AC.Mode = (int)Enumerators.Modes.Update;

                        // reload Assigned Drivers & Breakdowns.
                        AutoCover TmpAC = QA.GetAutoCover(AC);
                        if (TmpAC != null)
                        {
                            AC.AssignedDrivers = TmpAC.AssignedDrivers;
                            AC.Breakdown = TmpAC.Breakdown;
                        }
                        QA.RemoveAutoCover(AC);
                        QA.AddAutoCover(AC, true);
                        ViewState.Add("Status", "NEW");

                        /*RPR 2004-03-15 clearFields();*/
                        btnSave.Visible = false;
                        // btnAssignDrv.Visible = false;
                        btnViewCvr.Visible = false;

                        Session["TaskControl"] = QA;

                        break;
                    default:
                        // Change to Edit Mode & add new
                        break;
                }

                //if(DifferentCoverPercentages(AC, QA))
                if (this.PendingMultipleVehicleDiscount(QA))
                {

                    Login.Login cp = HttpContext.Current.User as Login.Login;
                    int UserID =
                        int.Parse(
                        cp.Identity.Name.Split("|".ToCharArray())[1]);

                    if (QA.TaskControlID != 0)
                    {
                        QA.Mode = (int)Enumerators.Modes.Update;
                    }
                    else
                    {
                        QA.Mode = (int)Enumerators.Modes.Insert;
                    }

                    string effdate = "";
                    if (QA.EffectiveDate.Trim() == "")
                        effdate = "10/01/2013";
                    else
                        effdate = QA.EffectiveDate.Trim();

                    if (QA.EntryDate >= DateTime.Parse("10/01/2013") && DateTime.Parse(effdate) >= DateTime.Parse("10/01/2013"))
                    {
                        CalculateCharge();
                        QA.Charge = decimal.Parse(txtPartialCharge.Text.ToString());
                    }
                    else
                        QA.Charge = 0;

                    QA.Save(UserID, AC, null, false);

                    AssignedDriver(QA, AC);

                    AC = (AutoCover)QA.AutoCovers[QA.AutoCovers.IndexOf(AC)];
                    ViewState.Add("QuotesAutoId", AC.QuotesAutoId.ToString());

                    //Para actualizar el banco y el company dealer en taskcontrol.
                    QA.Policy.Bank = AC.Bank;
                    QA.Policy.CompanyDealer = AC.CompanyDealer;
                    QA.Bank = AC.Bank;
                    QA.CompanyDealer = AC.CompanyDealer;

                    if (QA.Policy.TaskControlID != 0)
                    {
                        QA.Save(UserID);
                    }

                    //Solo aplica para AutoPrivado y no para OPP.
                    if (Session["OptimaPersonalPackage"] == null)
                    {
                        if (this.PendingMultipleVehicleDiscount(QA))
                        {
                            ApplyDiscountSecondDiscount(QA);
                        }
                    }

                    Session["TaskControl"] = QA;
                    fillDataGrid(QA.AutoCovers);

                    this.SelectLastVehicle();

                    this.SetControlState((int)States.READONLY);
                }
            }
        }
        private bool ValidateNew(AutoCover AC)
        {
            bool result = true;
            // Validate if Driver or Prospect exists on DB

            //Se comento porque se debe considerar un try catch en el save para que
            // recoja el mensaje de error del VIN Duplicado. Tambien hay que corregir 
            // que cuando busque en la base de datos solo traiga los VIN Inforce y no
            // considere los cancelados.
            //if((AutoCover.GetQuotesAutoByCriteria(AC)).Rows.Count > 0)
            //{
            //    result = false;
            //    string tmpMesg = "A vehicle with this VIN already exists. \\n Do you want to bring existing data?";
            //    litPopUp.Text = MakeConfirmPopUpString(tmpMesg, 
            //        (int)ConfirmationType.DUPLICATE_VEHICLE);
            //    litPopUp.Visible = true;
            //}
            return result;
        }
        private bool PendingMultipleVehicleDiscount(TaskControl.QuoteAuto QA)
        {
            if (QA.AutoCovers.Count > 1)
            {
                int[] subPolicyID = new int[QA.AutoCovers.Count];
                AutoCover cover = null;

                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {
                    cover = (AutoCover)QA.AutoCovers[i];
                    subPolicyID[i] = cover.PolicySubClassId;
                }

                return this.PendingMultipleVehicleDiscountFound(subPolicyID,
                    QA.AutoCovers);
            }
            return false;
        }

        public struct HasMultipleVehicleDiscount
        {
            public decimal BiPd;
            public decimal CollComp;

            public HasMultipleVehicleDiscount(decimal BiPd, decimal CollComp)
            {
                this.BiPd = BiPd;
                this.CollComp = CollComp;
            }
        }
        private int Duplicate(int[] IntArray, int TestInt, int CurrentIndex)
        {
            if (CurrentIndex > 0)
            {
                for (int i = 0; i < CurrentIndex; i++)
                {
                    if (IntArray[i] == TestInt)
                        return i;
                }
            }
            return -1;
        }

        private DataTable GetMultipleVehicleDiscounts(int SubPolicyID)
        {
            if (SubPolicyID != 0)
            {
                XmlCooker.DbRequestXmlCookRequestItem[] cookItems = new
                    XmlCooker.DbRequestXmlCookRequestItem[1];

                XmlCooker.DbRequestXmlCooker.AttachCookItem("SubPolicyID", SqlDbType.Int,
                    0, SubPolicyID.ToString(), ref cookItems);

                Baldrich.DBRequest.DBRequest executer =
                    new Baldrich.DBRequest.DBRequest();

                return executer.GetQuery("QuoteAutoGetMultipleVehicleDiscounts",
                    XmlCooker.DbRequestXmlCooker.Cook(cookItems));
            }
            return null;
        }


        private bool PendingMultipleVehicleDiscountFound(
            int[] SubPolicyID, ArrayList AutoCovers)
        {
            HasMultipleVehicleDiscount[]
                hasMultipleVehicleDiscount =
                new HasMultipleVehicleDiscount[SubPolicyID.Length];
            DataTable results = null;
            decimal biPd = 0M;
            decimal collComp = 0M;
            int duplicateIndex = 0;

            for (int i = 0; i < SubPolicyID.Length; i++)
            {
                duplicateIndex = this.Duplicate(SubPolicyID, SubPolicyID[i], i);
                //if same SubPolicyType index don't fetch twice from DB

                if (duplicateIndex == -1)
                {
                    results = this.GetMultipleVehicleDiscounts(SubPolicyID[i]);
                    if (results != null && results.Rows.Count > 0)
                    {
                        biPd = results.Rows[0]["BiPd"].ToString() ==
                            string.Empty ? 0 :
                            decimal.Parse(results.Rows[0]["BiPd"].ToString().Trim());
                        collComp = results.Rows[0]["CollComp"].ToString() ==
                            string.Empty ? 0 :
                            decimal.Parse(
                            results.Rows[0]["CollComp"].ToString().Trim());
                    }

                    hasMultipleVehicleDiscount[i] =
                        results == null ?
                        new HasMultipleVehicleDiscount(0, 0) :
                        new HasMultipleVehicleDiscount(biPd, collComp);
                }
                else
                {
                    hasMultipleVehicleDiscount[i] =
                        hasMultipleVehicleDiscount[duplicateIndex];
                }
            }
            return FoundPendingDiscountInList(hasMultipleVehicleDiscount,
                AutoCovers);
        }

        private bool FoundPendingDiscountInList(HasMultipleVehicleDiscount[] list,
            ArrayList AutoCovers)
        {
            AutoCover autoCoverA = null;
            AutoCover autoCoverB = null;

            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    autoCoverA = (AutoCover)AutoCovers[i];
                    autoCoverB = (AutoCover)AutoCovers[j];

                    if (((list[i].BiPd > 0 && //coverA has BiPd discount
                        list[j].BiPd > 0) && //coverB has BiPd discount
                        (list[i].BiPd != autoCoverA.DiscountBIPD || //Cover A or B's actual BiPd- 
                        list[j].BiPd != autoCoverB.DiscountBIPD)) ||//discount is different-
                        ((list[i].CollComp > 0 &&//Repeat for CC.	//from the aplicable discount.
                        list[j].CollComp > 0) &&
                        (list[i].CollComp != autoCoverA.DiscountCompColl ||
                        list[j].CollComp != autoCoverB.DiscountCompColl)))
                        return true;
                }
            }
            return false;
        }

        private List<string> ImprimirAutoQuote(List<string> mergePaths, int taskControl)
        {
            try
            {
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                // EPolicy.TaskControl.PersonalPackage taskControl = (EPolicy.TaskControl.PersonalPackage)Session["TaskControl"];

                int s = taskControl;

                ObjectDataSource ob = new ObjectDataSource("GetReportAutoQuoteDetails.GetReportAutoQuoteDetailsTableAdapter", "GetData");

                GetReportAutoQuoteDetailsTableAdapters.GetReportAutoQuoteDetailsTableAdapter ds1 = new GetReportAutoQuoteDetailsTableAdapters.GetReportAutoQuoteDetailsTableAdapter();
                ReportDataSource rds1 = new ReportDataSource();

                rds1 = new ReportDataSource("GetReportAutoQuoteDetails", (DataTable)ds1.GetData(s));

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonalQuote.rdlc");
                viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-2GDN";
                string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-2GDN" + ".pdf";

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
        private List<string> ImprimirAutoQuoteEndorsement(List<string> mergePaths, int taskControl, int EndorsementID)
        {
            try
            {
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                // EPolicy.TaskControl.PersonalPackage taskControl = (EPolicy.TaskControl.PersonalPackage)Session["TaskControl"];

                int s = taskControl;
                int QuoteID = taskControl;
                //ObjectDataSource ob = new ObjectDataSource("GetReportAutoQuoteDetails.GetReportAutoQuoteDetailsTableAdapter", "GetData");

                GetReportAutoQuoteDetailsTableAdapters.GetReportAutoQuoteDetailsTableAdapter ds1 = new GetReportAutoQuoteDetailsTableAdapters.GetReportAutoQuoteDetailsTableAdapter();
                GetReportEndosoByQuotesID_TableAdapters.GetReportEndosoByQuotesIDTableAdapter ds2 = new GetReportEndosoByQuotesID_TableAdapters.GetReportEndosoByQuotesIDTableAdapter();
                ReportDataSource rds1 = new ReportDataSource();
                ReportDataSource rds2 = new ReportDataSource();

                rds1 = new ReportDataSource("GetReportAutoQuoteDetails", (DataTable)ds1.GetData(s));
                rds2 = new ReportDataSource("GetReportEndosoByQuotesID", (DataTable)ds2.GetData(QuoteID));

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoQuoteDI.rdlc");
                viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.DataSources.Add(rds2);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-2GDN";
                string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-2GDN" + ".pdf";

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
        private List<string> ImprimirDoubleInterestQuotes(List<string> mergePaths, EPolicy.TaskControl.QuoteAuto q, EPolicy.TaskControl.Quote quote)
        {
            try
            {
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                // EPolicy.TaskControl.PersonalPackage taskControl = (EPolicy.TaskControl.PersonalPackage)Session["TaskControl"];
                EPolicy.TaskControl.QuoteAuto qa = q;
                AutoCover ac = null;
                //ObjectDataSource ob = new ObjectDataSource("GetReportAutoQuoteDetails.GetReportAutoQuoteDetailsTableAdapter", "GetData");

                GetReportDoubleInterestTableAdapters.GetReportDoubleInterestTableAdapter dt1 = new GetReportDoubleInterestTableAdapters.GetReportDoubleInterestTableAdapter();
                GetReportDoubleInterestTableAdapters.GetReportAutoQuoteDetailsTableAdapter dt2 = new GetReportDoubleInterestTableAdapters.GetReportAutoQuoteDetailsTableAdapter();

                ReportDataSource rds1 = new ReportDataSource();
                ReportDataSource rds2 = new ReportDataSource();

                int QuotesAutoID = 0;

                for (int i = 0; i < q.AutoCovers.Count; i++)
                {

                    try
                    {
                        ac = (AutoCover)q.AutoCovers[i];
                        QuotesAutoID = ac.QuotesAutoId;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }


                rds1 = new ReportDataSource("GetReportAutoQuoteDetails", (DataTable)dt2.GetData(quote.TaskControlID));
                rds2 = new ReportDataSource("GetReportDoubleInterest", (DataTable)dt1.GetData(QuotesAutoID));

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/QuotesDoubleInterest.rdlc");
                viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.DataSources.Add(rds2);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                string fileName1 = "PolicyNo- " + quote.ToString().Trim() + "-" + quote.ToString().Trim() + "-2GDN";
                string _FileName1 = "PolicyNo- " + quote.ToString().Trim() + "-" + quote.ToString().Trim() + "-2GDN" + ".pdf";

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
        private List<string> ImprimirAutoQuoteSolicitud(List<string> mergePaths, int taskControl)
        {
            try
            {
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                // EPolicy.TaskControl.PersonalPackage taskControl = (EPolicy.TaskControl.PersonalPackage)Session["TaskControl"];

                int s = taskControl;

                ObjectDataSource ob = new ObjectDataSource("GetReportAutoQuoteDetails.GetReportAutoQuoteDetailsTableAdapter", "GetData");

                GetReportAutoQuoteDetailsTableAdapters.GetReportAutoQuoteDetailsTableAdapter ds1 = new GetReportAutoQuoteDetailsTableAdapters.GetReportAutoQuoteDetailsTableAdapter();
                ReportDataSource rds1 = new ReportDataSource();

                rds1 = new ReportDataSource("GetReportAutoQuoteDetails", (DataTable)ds1.GetData(s));

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/SolicitudDeSeguroAutomovilPersonal.rdlc");
                
                viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-Solicitud";
                string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-Solicitud" + ".pdf";

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
        private double GetTotalDiscount()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)(TaskControl.QuoteAuto)Session["TaskControl"];
            Quotes.AutoCover AC = null;
            double TotDiscount = 0.00;

            if (QA.AutoCovers.Count != 0)	 //Fill Auto Info. From Quote.
            {
                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {
                    Quotes.AutoCover ACPolicy = new Quotes.AutoCover();
                    AC = (AutoCover)QA.AutoCovers[i];
                    TotDiscount = TotDiscount + AC.TotDiscount;
                }
            }

            return TotDiscount;
        }
        protected void ddlRental_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetVehicleRentaPremium();
        }


        protected void TxtVehicleRental_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlAccidentDeath_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccidentDeathPremium();
        }

        protected void ddlADPersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccidentDeathPremium();
        }

        protected void txtTerm_TextChanged1(object sender, EventArgs e)
        {
            if (txtTerm.Text.Trim() == "" || txtTerm.Text.Trim() == "0")
                txtTerm.Text = "12";

            if (decimal.Parse(txtTerm.Text.ToString().Trim()) > 12)
            {
                chkAssist.Enabled = false;
                chkAssistEmp.Enabled = false;
            }

            GetVehicleRentaPremium();
            GetTowingPremium();
        }

        protected void ddlUninsuredSingle_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUninsuredSinglePremium();
            SetEnableUninsured();
        }

        protected void ddlUninsuredSplit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUninsuredSplitPremium();
            SetEnableUninsured();
        }

        protected void ddlEquitmentSonido_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEquipmentSoundPremium();
        }

        protected void ddlEquitmentAudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEquipmentAudioPremium();
        }

        protected void chkEquipTapes_CheckedChanged(object sender, EventArgs e)
        {
            GetEquipmentTapesPremium();
        }

        protected void ddlRoadAssistEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoadAssistEmp.SelectedItem.Text.Trim() != "0" && 1 == 0)
            {
                ddlRoadAssist.SelectedIndex = -1;
                ddlRoadAssist.Enabled = false;
            }
            else
            {
                //ddlRoadAssist.Enabled = true;
                ddlRoadAssist.Enabled = false;
            }
        }
        protected void ddlRoadAssist_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlRoadAssist.SelectedItem.Text.Trim() != "0")
            {
                ddlRoadAssistEmp.SelectedIndex = -1;
                ddlRoadAssistEmp.Enabled = false;
            }
            else
            {
                //ddlRoadAssistEmp.Enabled = true;
                ddlRoadAssistEmp.Enabled = false;
            }
        }

        private string GetOriginalVehicleRentaPremium()
        {
            string VehicleRentalID = ddlRental.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("VehicleRentalID", SqlDbType.Int, 0, VehicleRentalID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetVehicleRentalByVehicleRentalID", xmlDoc);

                string VehicleRental = dt.Rows[0]["VehicleRentalPremium"].ToString();

                if (txtTerm.Text == "12")
                    return VehicleRental.Trim();
                else
                {
                    if (txtTerm.Text != "")
                        return (int.Parse(VehicleRental.Trim()) * CalculatePeriodAmounts()).ToString();
                    else
                        return VehicleRental.Trim();
                }
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Transportation Expenses." + xcp.Message, xcp);
            }
        }

        private void GetVehicleRentaPremium()
        {
            string VehicleRentalID = ddlRental.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("VehicleRentalID", SqlDbType.Int, 0, VehicleRentalID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetVehicleRentalByVehicleRentalID", xmlDoc);

                string VehicleRental = dt.Rows[0]["VehicleRentalPremium"].ToString();

                if (txtTerm.Text == "12")
                    TxtVehicleRental.Text = VehicleRental.Trim();
                else
                {
                    if (txtTerm.Text != "")
                        TxtVehicleRental.Text = (int.Parse(VehicleRental.Trim()) * CalculatePeriodAmounts()).ToString();
                    else
                        TxtVehicleRental.Text = VehicleRental.Trim();
                }
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Transportation Expenses." + xcp.Message, xcp);
            }
        }
        private void GetTowingPremium()
        {
            try
            {
                string TowingP = ddlTowing.SelectedItem.Value.Trim().Replace('$', ' ');
                TowingP = TowingP.TrimStart();
                if (txtTerm.Text == "12")
                {
                    if (double.Parse(TowingP) == 0.0)
                    {
                        TxtTowing.Text = "0";
                    }
                    else
                       // TxtTowing.Text = Math.Round(double.Parse(TowingP), 0).ToString("###,###");
                        TxtTowing.Text = Math.Round(decimal.Parse(TowingP), 0).ToString("###,###");
                }
                else
                {
                    if (txtTerm.Text != "" && TowingP != "")
                    {
                        decimal Towing = decimal.Parse(TowingP.Trim()) * CalculatePeriodAmounts();
                        TxtTowing.Text = Math.Round(Towing, 0).ToString("###,###").Trim();
                    }

                    else
                        TxtTowing.Text = TowingP.ToString().Trim();
                }
            }
            catch (Exception xcp)
            {
                throw new Exception("Error Get the Towing Premium." + xcp.Message, xcp);
            }
        }
        private void GetTowingPremium2()
        {
            try
            {
                string TowingPrem = ddlTowing.SelectedItem.Value.Trim();

                if (txtTerm.Text == "12")
                    TxtTowing.Text = TowingPrem.Trim();
                else
                {
                    if (txtTerm.Text != "")
                        TxtTowing.Text = (int.Parse(TowingPrem.Trim()) * CalculatePeriodAmounts()).ToString();
                    else
                        TxtTowing.Text = TowingPrem.Trim();
                }
            }
            catch (Exception xcp)
            {
                throw new Exception("Error Get the Towing Premium." + xcp.Message, xcp);
            }
        }

        private void GetAccidentDeathPremium()
        {
            string AccidentalDeathID = ddlAccidentDeath.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("AccidentalDeathID", SqlDbType.Int, 0, AccidentalDeathID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetAccidentalDeathByID", xmlDoc);

                string AccidentDeathPremium = "0";
                if (dt.Rows.Count > 0)
                    AccidentDeathPremium = dt.Rows[0]["AccidentalDeathPrima"].ToString();

                TxtAccidentDeathPremium.Text = (int.Parse(AccidentDeathPremium.Trim()) * int.Parse(ddlADPersons.SelectedItem.Text.Trim())).ToString();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Accidental Death." + xcp.Message, xcp);
            }
        }

        private void GetUninsuredSinglePremium()
        {
            string UninsuredSingleID = ddlUninsuredSingle.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("UninsuredSingleID", SqlDbType.Int, 0, UninsuredSingleID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetUninsuredSingleByID", xmlDoc);

                string UninsuredSinglePremium = "";

                if (IsSecondAutoForApplyPremium("Single"))
                    UninsuredSinglePremium = dt.Rows[0]["UninsuredSinglePrimaAuto2"].ToString();
                else
                    UninsuredSinglePremium = dt.Rows[0]["UninsuredSinglePrimaAuto1"].ToString();

                TxtUninsuredSingle.Text = UninsuredSinglePremium.ToString().Trim();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Uninsured Single." + xcp.Message, xcp);
            }
        }

        private void GetUninsuredSplitPremium()
        {
            string UninsuredSplitID = ddlUninsuredSplit.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("UninsuredSplitID", SqlDbType.Int, 0, UninsuredSplitID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetUninsuredSplitByID", xmlDoc);

                string UninsuredSplitPremium = "";

                if (IsSecondAutoForApplyPremium("Split"))
                    UninsuredSplitPremium = dt.Rows[0]["UninsuredSplitPrimaAuto2"].ToString();
                else
                    UninsuredSplitPremium = dt.Rows[0]["UninsuredSplitPrimaAuto1"].ToString();

                TxtUninsuredSplit.Text = UninsuredSplitPremium.ToString().Trim();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Uninsured Split." + xcp.Message, xcp);
            }
        }

        private void GetEquipmentSoundPremium()
        {
            string EquitmentSonidoID = ddlEquitmentSonido.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("EquitmentSonidoID", SqlDbType.Int, 0, EquitmentSonidoID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetEquitmentSonidoByID", xmlDoc);

                string EquitmentSonido = dt.Rows[0]["EquitmentSonidoPrima"].ToString();
                TxtEquitmentSonido.Text = EquitmentSonido.Trim();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Equipment Sound." + xcp.Message, xcp);
            }
        }

        private void GetEquipmentAudioPremium()
        {
            string EquitmentAudioID = ddlEquitmentAudio.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("EquitmentAudioID", SqlDbType.Int, 0, EquitmentAudioID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetEquitmentAudioByID", xmlDoc);

                string EquitmentAudio = dt.Rows[0]["EquitmentAudioPrima"].ToString();
                TxtEquitmentAudio.Text = EquitmentAudio.Trim();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Equipment Audio." + xcp.Message, xcp);
            }
        }

        private void GetEquipmentTapesPremium()
        {
            try
            {
                if (chkEquipTapes.Checked)
                    TxtEquitmentTapes.Text = "15";
                else
                    TxtEquitmentTapes.Text = "0";
            }
            catch (Exception xcp)
            {
                throw new Exception("Error Get the Equipment Tapes." + xcp.Message, xcp);
            }
        }

        private bool IsSecondAutoForApplyPremium(string othCover)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            bool IsPrima = false;

            Quotes.AutoCover AC = null;

            if (QA.AutoCovers.Count == 0)
                IsPrima = false;
            else
            {
                if (QA.AutoCovers.Count > 1)
                {
                    for (int i = 0; i < QA.AutoCovers.Count; i++)
                    {
                        AC = (AutoCover)QA.AutoCovers[i];

                        if (othCover == "Split")
                            if (AC.UninsuredSinglePremium != 0)
                                IsPrima = true;

                        if (othCover == "Single")
                            if (AC.UninsuredSinglePremium != 0)
                                IsPrima = true;

                        if (othCover == "Road")
                            if (AC.AssistancePremium != 0)
                                IsPrima = true;
                    }
                }
            }
            return IsPrima;
        }



        protected void chkLLG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLLG.Checked)
                ddlLoanGap.SelectedIndex = ddlLoanGap.Items.IndexOf(ddlLoanGap.Items.FindByValue("1"));
            else
                ddlLoanGap.SelectedIndex = -1;
        }
        protected void TxtCustomizeEquipLimit_TextChanged(object sender, EventArgs e)
        {
            GetCustomizeEquipPremium(true);
        }

        private void GetCustomizeEquipPremium(bool message)
        {
            if (ddlCollision.SelectedItem.Text.Trim() != "" && ddlTerritory.SelectedItem.Text.Trim() != "" && TxtCustomizeEquipLimit.Text.Trim() != "")
            {
                decimal tempCollrate = 0;
                decimal tempTerrRate = 0;
                decimal tempLimit = 0;
                decimal totalcoll = 0;
                decimal totalcomp = 0;
                decimal tempComprate = 0;

                //Collision
                switch (int.Parse(ddlCollision.SelectedItem.Text.Trim()))
                {
                    case 150:
                        tempCollrate = 0.93M;
                        break;
                    case 200:
                        tempCollrate = 0.83M;
                        break;
                    case 250:
                        tempCollrate = 0.75M;
                        break;
                    case 500:
                        tempCollrate = 0.54M;
                        break;
                    case 1000:
                        tempCollrate = 0.42M;
                        break;
                    default:
                        tempCollrate = 0;
                        break;
                }

                switch (int.Parse(ddlTerritory.SelectedItem.Value.Trim()))
                {
                    case 1:
                        tempTerrRate = 5.75M;
                        break;
                    case 3:
                        tempTerrRate = 5.18M;
                        break;
                    case 5:
                        tempTerrRate = 5.73M;
                        break;
                    case 6:
                        tempTerrRate = 4.64M;
                        break;
                    case 7:
                        tempTerrRate = 4.50M;
                        break;
                    case 8:
                        tempTerrRate = 5.16M;
                        break;
                    default:
                        tempTerrRate = 0;
                        break;
                }

                tempLimit = decimal.Parse(TxtCustomizeEquipLimit.Text.Trim());
                totalcoll = Math.Round(tempTerrRate * tempCollrate * 0.90M * 0.015M * tempLimit, 0);

                //Comprehensive
                //Collision
                switch (int.Parse(ddlComprehensive.SelectedItem.Text.Trim()))
                {
                    case 200:
                        tempComprate = 0.88M;
                        break;
                    case 250:
                        tempComprate = 0.81M;
                        break;
                    case 500:
                        tempComprate = 0.69M;
                        break;
                    case 1000:
                        tempComprate = 0.63M;
                        break;
                    default:
                        tempComprate = 0;
                        break;
                }

                totalcomp = Math.Round((tempLimit * tempComprate) / 100, 0);

                TxtEquipColl.Text = (totalcoll).ToString().Trim();
                TxtEquipComp.Text = (totalcomp).ToString().Trim();
                TxtEquipTotal.Text = (totalcoll + totalcomp).ToString().Trim();
            }
            else
            {
                if (message)
                {
                    TxtEquipColl.Text = "0";
                    TxtEquipComp.Text = "0";
                    TxtEquipTotal.Text = "0";
                    lblRecHeader.Text = "Please select the Collision Deductibe, Territoty value and Customize Equipment Limit, for this cover";
                    mpeSeleccion.Show();
                }
            }
        }

        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomizeEquipPremium(false);
        }
        protected void ddlCollision_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomizeEquipPremium(false);
        }
        protected void chkAssistEmp_CheckedChanged(object sender, EventArgs e)
        {
            GeRoadAssistPremium(true);
        }
        protected void chkAssist_CheckedChanged(object sender, EventArgs e)
        {
            GeRoadAssistPremium(false);
        }
        
        protected void chkIsLeasing_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsLeasing.Checked == true)
            {
                //ddlBI.SelectedIndex = 7;
               // ddlPD.SelectedIndex = 6;
                for (int i = 0; i<= ddlBI.Items.Count - 1; i++)
                {
                    if (ddlBI.Items[i].Text == "100/300")
                    {
                        ddlBI.SelectedIndex = i;
                    }
                }

                for (int i = 0; i <= ddlPD.Items.Count - 1; i++)
                {
                    if (ddlPD.Items[i].Text == "50")
                    {
                        ddlPD.SelectedIndex = i;
                    }
                }
            }
            else
            {
                ddlBI.SelectedIndex = 0;
                ddlPD.SelectedIndex = 0;
            }
        }

        private void GeRoadAssistPremium(bool IsEmp)
        {
            if (IsSecondAutoForApplyPremium("Road"))
            {
                if (chkAssistEmp.Checked)
                    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("2"));
                else
                    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("1"));

                //if (chkAssist.Checked)
                //    ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("3"));
                //else
                //    ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("1"));
                ApplySecondPriceRoadAssistance();
            }
            else
            {
                if (chkAssistEmp.Checked)
                    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("2"));
                else
                    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("1"));

                ApplySecondPriceRoadAssistance();
                //if (chkAssist.Checked)
                //    ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("2"));
                //else
                //    ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("1"));
            }
        }
        private void ApplySecondPriceRoadAssistance()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            EPolicy.Quotes.AutoCover ac = null;

            bool firstPrima = false;

            for (int i = 0; i < QA.AutoCovers.Count; i++)
            {
                ac = (AutoCover)QA.AutoCovers[i];

                if (!ac.IsAssistanceEmp)
                {
                    if (ac.AssistanceID == 2)
                        firstPrima = true;
                }

            }
            if (chkAssist.Checked == true)
            {
                if (firstPrima)
                {
                    ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("3"));
                }
                else
                {
                    ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("2"));
                }
            }
            else
            {
                ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("0"));
            }

        }
        protected void chkLoJack_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableLojackFields();
        }

        private void SetEnableLojackFields()
        {
            txtLoJackCertificate.Enabled = true;
            TxtLojackExpDate.Enabled = true;
            imgCalendarLJExp.Enabled = true;

            if (chkLoJack.Checked)
            {
                txtLoJackCertificate.Enabled = true;
                TxtLojackExpDate.Enabled = true;
                imgCalendarLJExp.Enabled = true;
            }
            else
            {
                txtLoJackCertificate.Enabled = false;
                TxtLojackExpDate.Enabled = false;
                imgCalendarLJExp.Enabled = false;
            }
        }

        protected void ddlTowing_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTowingPremium();
        }
        protected void ddlExperienceDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExperienceDiscount.SelectedIndex > 0 && ddlExperienceDiscount.SelectedItem != null)
                TxtExpDisc.Text = ddlExperienceDiscount.SelectedItem.Value.Trim();
            else
                TxtExpDisc.Text = "0";
        }
        protected string UppercaseFirst(string s)
        {
            string uppercase = s.ToUpper();
            return uppercase;
        }
        protected void SetPrimaryDriver(int QuotesID)
        {
            DataTable dt = GetQuotesDriversWithAgeForQuote(QuotesID);

            string[] mejorCandidato = new string[2]
               { dt.Rows[0]["Age"].ToString()                  //0= age
                ,dt.Rows[0]["QuotesDriversID"].ToString() };   //1=QuotesAutoID

            string[] mejorCandidata = new string[2]
               { dt.Rows[0]["Age"].ToString()                  //0= age
                ,dt.Rows[0]["QuotesDriversID"].ToString() };   //1=QuotesAutoID

            //Busca el Condcutor Hombre de menor edad
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["Age"].ToString()) <= Convert.ToInt32(mejorCandidato[0]) && dt.Rows[i]["GenderID"].ToString() == "1")
                {
                    mejorCandidato[0] = dt.Rows[i]["Age"].ToString();
                    mejorCandidato[1] = dt.Rows[i]["QuotesDriversID"].ToString();
                }
            }
            //Busca el Condcutor Mujer de menor edad
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["Age"].ToString()) <= Convert.ToInt32(mejorCandidata[0]) && dt.Rows[i]["GenderID"].ToString() == "2")
                {
                    mejorCandidata[0] = dt.Rows[i]["Age"].ToString();
                    mejorCandidata[1] = dt.Rows[i]["QuotesDriversID"].ToString();
                }
            }

            if (Convert.ToInt32(mejorCandidato[0]) <= 25)
            { ddlDriver.SelectedIndex = ddlDriver.Items.IndexOf(ddlDriver.Items.FindByValue(mejorCandidato[1])); }
            else
            {ddlDriver.SelectedIndex = ddlDriver.Items.IndexOf(ddlDriver.Items.FindByValue(mejorCandidata[1]));}

        }

            private static DataTable GetQuotesDriversWithAgeForQuote(int QuotesID)
        {
            DataTable dt = new DataTable();

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("QuotesID", SqlDbType.Int, 0, QuotesID.ToString(), ref cookItems);

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

            dt = exec.GetQuery("GetQuotesDriversWithAgeForQuote", xmlDoc);

            return dt;

        }

            private static DataTable GetPolicyQuoteByTaskControlID(int TaskControlID)
            {
                DataTable dt = new DataTable();

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

                dt = exec.GetQuery("GetPolicyQuoteByTaskControlID", xmlDoc);

                return dt;

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

    }
}
