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
using System.IO;
using EPolicy.Quotes;
using EPolicy.XmlCooker;
using System.Xml;
using System.Web.Security;

namespace EPolicy
{
	/// <summary>
	/// Summary description for QuoteAutoVehicles.
	/// </summary>
	/// 


    public partial class QuoteAutoVehicles : System.Web.UI.Page
    {

        // local variables
        protected System.Web.UI.WebControls.DropDownList ddlAssistancePremium;
        protected string today = DateTime.Now.ToString("MM/dd/yyyy");
        //		bool DI_Cover = false, Liab_Cover = false, FC_Cover = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
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
                if (!cp.IsInRole("AUTO PERSONAL VEHICLES") &&
                    !cp.IsInRole("ADMINISTRATOR"))
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();

                    Response.Redirect("Default.aspx?001");
                }
            }


            //this.ddlYear.Attributes.Add("onchange","getAge();CalculateOriginalCost();");
            this.txtDeprec1st.Attributes.Add("onblur", "SetDepreciation(); CalculateOriginalCost();");
            this.rdo15percent.Attributes.Add("onclick", "SetDepreciation15();CalculateOriginalCost()");
            this.rdo20percent.Attributes.Add("onclick", "SetDepreciation20();CalculateOriginalCost()");
            //this.txtDeprecAll.Attributes.Add("onblur", "SetDepreciation();CalculateOriginalCost();");
            this.ddlCollision.Attributes.Add("onchange", "SetCompCollValue();");

            //this.ddlRental.Attributes.Add("onchange", "SetRentalValue();");	

            this.ddlBI.Attributes.Add("onchange", "SetDDLCSL()");
            this.ddlCSL.Attributes.Add("onchange", "SetDDLBI()");
           // this.txtCost.Attributes.Add("onblur", "SetActualValueFromCost();");
           // this.txtActualValue.Attributes.Add("onblur", "SetActualValueFromCost();");  //onblur
           // this.ddlNewUsed.Attributes.Add("onchange", "SetActualValueFromCost();");
            this.ddlYear.Attributes.Add("onchange", "SetActualValueFromCost();");
            //this.txtAge.Attributes.Add("onblur", "CalculateOriginalCost();");
            //this.txtPurchaseDt.Attributes.Add("onblur", "SetActualValueFromCost()");
            //this.ddlNewUsed.Attributes.Add("onchange","SetCostAndValueControlState();");
            //this.txtCost.Attributes.Add("onblur", "CalculateOriginalCost();SetActualValueFromCost()");
            //this.txtActualValue.Attributes.Add("onblur", "CalculateOriginalCost();");

            //litPopUp.Visible = false;
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (QA == null)
                Response.Redirect("HomePage.aspx");
       
            if (!IsPostBack)
            {
                if (!QA.IsPolicy)
                {
                    if (ViewState["InternalAutoID"] == null)
                    {
                        ViewState.Add("InternalAutoID", (int.Parse(Session["InternalAutoID"].ToString())));
                        AutoCover AC = (AutoCover)QA.AutoCovers[0];
                        AC.InternalID = int.Parse(Session["InternalAutoID"].ToString());
                        QA.RemoveAutoCover(AC);
                        QA.AddAutoCover(AC, false);

                        ViewState["InternalAutoID"] = AC.InternalID;

                        Session["TaskControl"] = QA;
                    }
                }                

                // Show DataGrid
                fillDataGrid(QA.AutoCovers);
                clearFields();
                enableFields(false);
                btnSave.Visible = false;
                btnAssignDrv.Visible = false;
                btnViewCvr.Visible = false;
                this.SetControlState((int)States.REST);

                if (Request.QueryString["vehicle"] == null)
                {
                    this.SelectFirstVehicle();
                }
                else
                {
                    object[] oo = this.GetDataGridItemFromVehicleInternalID(int.Parse(Request.QueryString["vehicle"].Trim()));

                    System.Web.UI.WebControls.DataGridItem item = (System.Web.UI.WebControls.DataGridItem)oo[0];
                    int correspondingItemIndex = (int)oo[1];
                    System.Web.UI.WebControls.DataGridCommandEventArgs f;
                    object argument = new Object();
                    System.Web.UI.WebControls.CommandEventArgs originalArgs = new System.Web.UI.WebControls.CommandEventArgs("dbDriverList_ItemCommand", argument);
                    f = new System.Web.UI.WebControls.DataGridCommandEventArgs
                        (item, this.dgVehicle, originalArgs);
                    this.SelectVehicle(f);
                    this.dgVehicle.SelectedIndex = correspondingItemIndex;
                }

                this.term.Value = QA.Term.ToString();

                /*if(Request.QueryString["referral"] != null &&
                    Request.QueryString["referral"].ToString().Trim() == "assign")
                    this.btnSave_Click(this, new System.Web.UI.ImageClickEventArgs(0,0));*/

                if (Request.QueryString["editMode"] != null &&
                    Request.QueryString["editMode"].ToString().Trim() == "1")
                {
                    this.ViewState["Status"] = "UPDATE";
                    this.SetControlState((int)States.READWRITE);
                }
                else
                {
                    this.ViewState["Status"] = "REST";
                }

                if (QA.IsPolicy && QA.Policy.PolicyCicleEnd == 0)// && this.ViewState["Status"] == "NEW")
                {
                    IspolicyNewButtonClick();
                }
                else
                {
                    if (this.ViewState["Status"].ToString() == "NEW")
                    {
                        IspolicyNewButtonClick();
                    }
                }
            }
            else
            {
                if (Callback.Value == "Y")
                {
                    AutoCover AC = AutoCover.GetQuotesAuto(txtVINEdit.Text);
                    ShowAutoCover(AC);
                    ViewState.Add("Status", "UPDATE");
                    ViewState.Add("QuotesAutoId", AC.QuotesAutoId);
                    ViewState.Add("InternalAutoID", AC.InternalID);
                    btnSave.Visible = true;
                    btnAssignDrv.Visible = false; //true;
                    btnViewCvr.Visible = true;
                    Callback.Value = "N";
                }

                if (this.applyToAll.Value == "1")
                {
                    AutoCover autoCover = this.LoadFromForm(int.Parse(
                        ViewState["QuotesAutoId"].ToString()),
                        int.Parse(
                        Session["InternalAutoID"].ToString().Trim()));
                    int UserID =
                        int.Parse(
                        cp.Identity.Name.Split("|".ToCharArray())[1]);

                    //this.ApplyDiscountToExistingCovers(
                    //	autoCover.DiscountBIPD, autoCover.DiscountCompColl);
                    this.ReapplyDiscounts();

                    QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                    foreach (AutoCover aC in QA.AutoCovers)
                    {
                        aC.Mode = 2; //Update
                    }

                    if (QA.IsPolicy)
                        QA.Save(UserID, autoCover, null, false);
                    else
                        QA.Save(UserID);

                    Session["TaskControl"] = QA;

                    this.ShowAutoCover((AutoCover)QA.AutoCovers[
                        QA.AutoCovers.IndexOf(autoCover)]);

                    this.applyToAll.Value = "0";
                    this.SetControlState((int)States.READONLY);
                }
            }

            if (this.ViewState["Status"].ToString() == "NEW" || this.ViewState["Status"].ToString() == "UPDATE")
                enableFields(true);
            else
                enableFields(false);

            // set TasControlID
            if (QA.TaskControlID != 0 && !QA.IsPolicy)
            {
                txtTaskControlID.Text = QA.TaskControlID.ToString();
                lblTaskControlID.Visible = true;
            }
            else
            {
                txtTaskControlID.Text = "";
                txtTaskControlID.Visible = false;
                lblTaskControlID.Visible = false;
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            this.Load += new System.EventHandler(this.Page_Load);
            /*this.txtPurchaseDt.TextChanged += new System.EventHandler(this.txtPurchaseDt_TextChanged);*/

            base.OnInit(e);

            //  Benny:	this.InitializeDriverObjectSessionVariable();

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.Placeholder1.Controls.Add(Banner);


            if (Session["LookUpTables"] == null)
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                this.BindDdls();

                //if (QA.IsPolicy || Session["OptimaPersonalPackage"] != null)
                // {
                DataTable dtBank = LookupTables.LookupTables.GetTable("Bank");
                DataTable dtCompanyDealer = LookupTables.LookupTables.GetTable("CompanyDealer");

                DataTable dtRoadAssist = LookupTables.LookupTables.GetTable("RoadAssist");
                DataTable dtRoadAssistEmp = LookupTables.LookupTables.GetTable("RoadAssistEmp");
                DataTable dtAccidentalDeath = LookupTables.LookupTables.GetTable("AccidentalDeath");
                DataTable dtEquitmentAudio = LookupTables.LookupTables.GetTable("EquitmentAudio");
                DataTable dtEquitmentSonido = LookupTables.LookupTables.GetTable("EquitmentSonido");
                DataTable dtUninsuredSingle = LookupTables.LookupTables.GetTable("UninsuredSingle");
                DataTable dtUninsuredSplit = LookupTables.LookupTables.GetTable("UninsuredSplit");

                DataTable dtVehicleRental;
                if (Session["OptimaPersonalPackage"] != null)
                {
                    dtVehicleRental = LookupTables.LookupTables.GetTable("VehicleRentalOPP");
                }
                else
                {
                    dtVehicleRental = LookupTables.LookupTables.GetTable("VehicleRental");
                }

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
                // }


                //RoadAssist
                ddlRoadAssist.DataSource = dtRoadAssist;
                ddlRoadAssist.DataTextField = "RoadAssistDesc";
                ddlRoadAssist.DataValueField = "RoadAssistID";
                ddlRoadAssist.DataBind();
                ddlRoadAssist.SelectedIndex = 0;
                //ddlRoadAssist.Items.Insert(0, "");

                //RoadAssistEmp
                ddlRoadAssistEmp.DataSource = dtRoadAssistEmp;
                ddlRoadAssistEmp.DataTextField = "RoadAssistEmpDesc";
                ddlRoadAssistEmp.DataValueField = "RoadAssistEmpID";
                ddlRoadAssistEmp.DataBind();
                ddlRoadAssistEmp.SelectedIndex = 0;
                // ddlRoadAssistEmp.Items.Insert(0, "");

                ////AccidentalDeath
                //ddlAccidentDeath.DataSource = dtAccidentalDeath;
                //ddlAccidentDeath.DataTextField = "AccidentalDeathDesc";
                //ddlAccidentDeath.DataValueField = "AccidentalDeathID";
                //ddlAccidentDeath.DataBind();
                //ddlAccidentDeath.SelectedIndex = -1;
                ////ddlAccidentDeath.Items.Insert(0, "");

                ////EquitmentAudio
                //ddlEquitmentAudio.DataSource = dtEquitmentAudio;
                //ddlEquitmentAudio.DataTextField = "EquitmentAudioDesc";
                //ddlEquitmentAudio.DataValueField = "EquitmentAudioID";
                //ddlEquitmentAudio.DataBind();
                //ddlEquitmentAudio.SelectedIndex = -1;
                ////ddlEquitmentAudio.Items.Insert(0, "");

                ////EquitmentSonido
                //ddlEquitmentSonido.DataSource = dtEquitmentSonido;
                //ddlEquitmentSonido.DataTextField = "EquitmentSonidoDesc";
                //ddlEquitmentSonido.DataValueField = "EquitmentSonidoID";
                //ddlEquitmentSonido.DataBind();
                //ddlEquitmentSonido.SelectedIndex = -1;
                ////ddlEquitmentSonido.Items.Insert(0, "");

                ////UninsuredSingle
                //ddlUninsuredSingle.DataSource = dtUninsuredSingle;
                //ddlUninsuredSingle.DataTextField = "UninsuredSingleDesc";
                //ddlUninsuredSingle.DataValueField = "UninsuredSingleID";
                //ddlUninsuredSingle.DataBind();
                //ddlUninsuredSingle.SelectedIndex = -1;
                ////ddlUninsuredSingle.Items.Insert(0, "");

                ////UninsuredSplit
                //ddlUninsuredSplit.DataSource = dtUninsuredSplit;
                //ddlUninsuredSplit.DataTextField = "UninsuredSplitDesc";
                //ddlUninsuredSplit.DataValueField = "UninsuredSplitID";
                //ddlUninsuredSplit.DataBind();
                //ddlUninsuredSplit.SelectedIndex = -1;
                ////ddlUninsuredSplit.Items.Insert(0, "");

                //VehicleRental
                ddlRental.DataSource = dtVehicleRental;
                ddlRental.DataTextField = "VehicleRentalDesc";
                ddlRental.DataValueField = "VehicleRentalID";
                ddlRental.DataBind();
                ddlRental.SelectedIndex = 0;
                //ddlRental.Items.Insert(0, "");

                //PolicySubClass
                DataTable dtPolicySubClass = FilterPolicySubClassTableByInsCO();

                ddlPolicySubClass.DataSource = dtPolicySubClass;
                ddlPolicySubClass.DataTextField = "PolicySubClassDesc";
                ddlPolicySubClass.DataValueField = "PolicySubClassID";
                ddlPolicySubClass.DataBind();
                ddlPolicySubClass.SelectedIndex = -1;
                ddlPolicySubClass.Items.Insert(0, "");

                FillDDLDiscount();

                //Drivers
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

                Session.Add("LookUpTables", "In");
            }
        }

        private DataTable BuildDriversTable()
        {
            DataSet ds = new DataSet("DSQuotesDrivers");
            DataTable dt = ds.Tables.Add("QuotesDrivers");

            dt.Columns.Add("QuotesDriversID", typeof(int));
            dt.Columns.Add("DriverName", typeof(string));

            return dt;
        }

        private DataTable FilterPolicySubClassTableByInsCO()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            DataTable dt = LookupTables.LookupTables.GetTable("PolicySubClass");
            DataTable dtPSC = dt.Clone();
            DataRow row;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["InsuranceCompanyID"].ToString().Trim() == QA.InsuranceCompany.Trim()
                    || dt.Rows[i]["InsuranceCompanyID"].ToString().Trim() == "000")
                {
                    //					if (QA.InsuranceCompany.Trim() == "004" && dt.Rows[i]["InsuranceCompanyID"].ToString().Trim() == "000")  //Se quito porque daba problema
                    //					{
                    //						// No llena el combo con la opcion de FULL,DI y RP normales.
                    //					}
                    //					else
                    //					{
                    if (GetCurrentNumberOfAutos() > 1 && dt.Rows[i]["AutoPolicyType"].ToString().Trim() == "DOUBLE INTEREST")
                    {
                    }
                    else
                    {
                        row = dtPSC.NewRow();

                        row["PolicySubClassID"] = (int)dt.Rows[i]["PolicySubClassID"];
                        row["PolicySubClassDesc"] = dt.Rows[i]["PolicySubClassDesc"].ToString();
                        row["AutoPolicyBaseSubClassID"] = (int)dt.Rows[i]["AutoPolicyBaseSubClassID"];
                        row["IsMaster"] = (bool)dt.Rows[i]["IsMaster"];
                        row["IsSpecial"] = (bool)dt.Rows[i]["IsSpecial"];
                        row["CSLonly"] = (bool)dt.Rows[i]["CSLonly"];
                        row["InsuranceCompanyID"] = dt.Rows[i]["InsuranceCompanyID"].ToString();

                        dtPSC.Rows.Add(row);
                    }
                    //					}
                }
            }
            return dtPSC;
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //this.dgVehicle.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgVehicle_ItemCreated1);
            //this.dgVehicle.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgVehicle_ItemCommand1);

        }
        // Verify Events
        //		this.cvPage.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.cvPage_ServerValidate);
        //		this.ddlBI.SelectedIndexChanged += new System.EventHandler(this.ddlBI_SelectedIndexChanged);
        //		this.ddlMake.SelectedIndexChanged += new System.EventHandler(this.ddlMake_SelectedIndexChanged);
        //		this.ddlPD.SelectedIndexChanged += new System.EventHandler(this.ddlPD_SelectedIndexChanged);
        //		this.ddlPolicySubClass.SelectedIndexChanged += new System.EventHandler(this.ddlPolicySubClass_SelectedIndexChanged);
        //		this.ddlCSL.SelectedIndexChanged += new System.EventHandler(this.ddlCSL_SelectedIndexChanged);
        //		this.ddlHomeCity.SelectedIndexChanged += new System.EventHandler(this.ddlHomeCity_SelectedIndexChanged);
        //		this.ddlWorkCity.SelectedIndexChanged += new System.EventHandler(this.ddlWorkCity_SelectedIndexChanged);
        //		this.btnSave.Click += new System.Web.UI.ImageClickEventHandler(this.btnSave_Click);
        //		this.btnAssignDrv.Click += new System.Web.UI.ImageClickEventHandler(this.btnAssignDrv_Click);
        //		this.btnViewCvr.Click += new System.Web.UI.ImageClickEventHandler(this.btnViewCvr_Click);
        //		this.btnAddVhcl.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddVhcl_Click);
        //		this.btnBack.Click += new System.Web.UI.ImageClickEventHandler(this.btnBack_Click);
        //		this.dgVehicle.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgVehicle_ItemCommand);
        //		this.Load += new System.EventHandler(this.Page_Load);
        //		this.txtPurchaseDt.TextChanged += new System.EventHandler(this.txtPurchaseDt_TextChanged);
        //			
        #endregion

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

        private object[]
            GetDataGridItemFromVehicleInternalID(int VehicleInternalID)
        {
            object[] oo = new object[2];
            for (int i = 0; i < this.dgVehicle.Items.Count; i++)
            {
                if (this.dgVehicle.Items[i].Cells[9].Text.Trim() ==
                    VehicleInternalID.ToString())
                {
                    oo[0] = this.dgVehicle.Items[i];
                    oo[1] = i;
                    return oo;
                }
            }
            return null;
        }

        private void SelectFirstVehicle()
        {
            System.Web.UI.WebControls.DataGridCommandEventArgs e;
            object argument = new Object();
            System.Web.UI.WebControls.CommandEventArgs originalArgs =
                new System.Web.UI.WebControls.CommandEventArgs("dbDriverList_ItemCommand", argument);

            if (this.dgVehicle.Items.Count > 0)
            {
                e = new System.Web.UI.WebControls.DataGridCommandEventArgs(
                    this.dgVehicle.Items[0], this.dgVehicle, originalArgs);
                this.SelectVehicle(e);
                this.dgVehicle.SelectedIndex = 0;
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

        private void SelectVehicle(System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            AutoCover AC = new AutoCover();
            if (e.Item.Cells[3].Text.ToLower() != "")
                AC.VIN = e.Item.Cells[3].Text;
            if (e.Item.Cells[7].Text != "0")
                AC.QuotesAutoId = int.Parse(e.Item.Cells[8].Text);//7
            if (e.Item.Cells[8].Text != "0")
                AC.QuotesId = int.Parse(e.Item.Cells[9].Text);//8
            AC.InternalID = int.Parse(e.Item.Cells[10].Text);//9
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
            btnAssignDrv.Visible = false;//true;
            btnViewCvr.Visible = true;
            this.SetControlState((int)States.READONLY);
        }

        private DataTable GetTemporarySubPolicyDataTable()
        {
            DataTable data = new DataTable();
            object[] values = new object[2];

            data.Columns.Add("PolicySubClassID");
            data.Columns.Add("PolicySubClassDesc");

            values[0] = 1;
            values[1] = "Double Interest";

            data.Rows.Add(values);

            values[0] = 2;
            values[1] = "Liability";

            data.Rows.Add(values);

            values[0] = 3;
            values[1] = "Full Cover";

            data.Rows.Add(values);

            return data;
        }

        private void BindDdls()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            this.BindDdl(
                this.ddlVehicleClass, "VehicleUse",
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
            this.BindDdl(
                this.ddlComprehensive, "ComprehensiveDeductible",
                "ComprehensiveDeductibleID",
                "ComprehensiveDeductibleDesc", null);
            this.BindDdl(this.ddlBI, "BodilyInjuryLimit",
                "BodilyInjuryLimitID", "BodilyInjuryLimitDesc", null);
            this.BindDdl(this.ddlPD, "PropertyDamageLimit",
                "PropertyDamageLimitID", "PropertyDamageLimitDesc", null);
            this.BindDdl(this.ddlCSL, "CombinedSingleLimit",
                "CombinedSingleLimitID", "CombinedSingleLimitDesc", null);

            if (!QA.IsPolicy) //Use for Quote Only.
            {
                this.BindDdl(this.ddlPolicySubClass, "PolicySubClass",
                    "PolicySubClassID", "PolicySubClassDesc", null
                    /*this.GetTemporarySubPolicyDataTable()*/);
            }

            this.BindDdl(this.ddlLoanGap, "LeaseLoanGap", "LeaseLoanGapID",
                "LeaseLoanGapDesc", null);
            this.BindDdl(this.ddlMedical, "MedicalLimit", "MedicalLimitID",
                "MedicalLimitDesc", null);

            //this.BindDdl(this.ddlAssistancePremium, "AssistancePremium",
            //	"AssistancePremiumID", "AssistancePremiumDesc", null);

            this.BindDdl(this.ddlMake, "VehicleMake", "VehicleMakeID",
                "VehicleMakeDesc", null);
            this.BindDdl(this.ddlYear, "VehicleYear", "VehicleYearID",
                "VehicleYearDesc", null);
            this.BindDdl(this.ddlHomeCity, "City", "CityID", "CityDesc",
                null);
            this.BindDdl(this.ddlWorkCity, "City", "CityID", "CityDesc",
                null);

            this.BindDdl(this.ddlSeatBelt, "SeatBelt", "SeatBeltID",
                "SeatBeltPremium", null);
            this.BindDdl(this.ddlPAR, "PersonalAccidentRider",
                "PersonalAccidentRiderID", "ParPremium", null);
        }

        private void BindDdl(
            System.Web.UI.WebControls.DropDownList DropDownList,
            string TableName, string ValueFieldName, string TextFieldName,
            DataTable Data)
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

        //private void dgVehicle_ItemCreated (
        //    object source, DataGridItemEventArgs e)
        //{	
        //    if(e.Item.ItemType == ListItemType.Item || 
        //        e.Item.ItemType == ListItemType.AlternatingItem ||
        //        e.Item.ItemType  == ListItemType.EditItem)
        //    {
        //        TableCell tableCell = new TableCell();
        //        tableCell =	e.Item.Cells[6];
        //        Button button = new Button();
        //        button = (Button)tableCell.Controls[0];
        //        button.Attributes.Add("onclick", 
        //            "return confirm( " + 
        //            "\"Are you sure you want to delete this vehicle?\")");
        //    }
        //}

        private void ddlPolicySubClass_SelectedIndexChanged(
            object sender, System.EventArgs e)
        {
            if (SetPolicySubClass())
            {
                this.SetDefaultFieldValues(this.ddlPolicySubClass.SelectedItem.Value.Trim());
                this.ddlPolicySubClass.BackColor = System.Drawing.Color.White;

                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                if (QA.IsPolicy && QA.Policy.PolicyCicleEnd != 0 &&
                    ViewState["Status"].ToString().Trim() != "READONLY")
                {
                    this.btnSave.Visible = true;
                }
            }
        }

        /*private void txtPurchaseDt_TextChanged(
            object sender, System.EventArgs e)
        {
            DateTime dt = DateTime.Parse(txtPurchaseDt.Text);
            if (dt > DateTime.Now)
                dt = DateTime.Now;
            // substract Date Entered to Today & display Age
            TimeSpan ts = DateTime.Now - dt;
            DateTime dtAge = new DateTime(ts.Ticks);
            int t = dtAge.Year - 1;
            txtAge.Text = (t < 0 ? 0 : t).ToString();

            // if more than 1 Yr set as Used
            if (dtAge.Year <= 1)
                ddlNewUsed.SelectedIndex = 
                    ddlNewUsed.Items.IndexOf(
                    ddlNewUsed.Items.FindByValue("2")); // New
            else 
                ddlNewUsed.SelectedIndex = 
                    ddlNewUsed.Items.IndexOf(
                    ddlNewUsed.Items.FindByValue("1")); // Used
        }*/

        private void clearFields()
        {
            ddlPolicySubClass.SelectedIndex = 0;

            //	txtVIN.Text = "";
            txtVINEdit.Text = "";
            //	txtPlate.Text = "";
            txtPlateEdit.Text = "";
            //	txtVehicleYear.Text = "";
            ddlYear.SelectedIndex = 0;
            //	txtVehicleMake.Text = "";
            ddlMake.SelectedIndex = 0;
            //	txtVehicleModel.Text = "";
            ddlModel.SelectedIndex = 0;
            txtPurchaseDt.Text = "";
            txtAge.Text = "";
            ddlNewUsed.SelectedIndex = 0;
            txtCost.Text = "";
            txtActualValue.Text = "";
            ddlHomeCity.SelectedIndex = 0;
            ddlWorkCity.SelectedIndex = 0;

            ddlVehicleClass.SelectedIndex = 0;
            ddlTerritory.SelectedIndex = 0;
            ddlAlarm.SelectedIndex = 0;
            txtDeprec1st.Text = "";
            txtDeprecAll.Text = "";
            ddlMedical.SelectedIndex = 0;
            //this.ddlAssistancePremium.SelectedIndex = 0;
            TxtAssistance.Text = "";
            ddlTowing.SelectedIndex = 0;
            txtVehicleRental.Text = "";
            txtTowing.Text = "";
            ddlRental.SelectedIndex = 0;
            ddlLoanGap.SelectedIndex = 0;
            ddlSeatBelt.SelectedIndex = 0;
            ddlPAR.SelectedIndex = 0;
            this.rdo15percent.Checked = true;
            this.rdo20percent.Checked = false;
            this.txtDeprec1st.Text = "15";
            this.txtDeprecAll.Text = "15";

            ddlBank.SelectedIndex = 0;
            ddlCompanyDealer.SelectedIndex = 0;

            ddlCollision.SelectedIndex = 0;
            ddlComprehensive.SelectedIndex = 0;
            txtDiscountCollComp.Text = "";
            ddlBI.SelectedIndex = 0;
            ddlPD.SelectedIndex = 0;
            ddlCSL.SelectedIndex = 0;
            txtDiscountBIPD.Text = "";
            this.txt1stISO.Text = "";
            txtPartialCharge.Text = "";
            txtTotalPremium.Text = "";
            txtPartialPremium.Text = string.Empty;
            //RPR 2004-03-26
            this.lblPrimaryDriver.Text = string.Empty;

            txtLicenseNumber.Text = "";
            txtExpDate.Text = "";
            chkIsLeasing.Checked = false;

            chkLLG.Checked = false;
            txtIsAssistanceEmp.Text = "False";
            ddlRoadAssistEmp.SelectedIndex = 0;
            ddlRoadAssist.SelectedIndex = 0;
            chkAssist.Checked = false;
            chkAssistEmp.Checked = false;
            chkAssistEmp.Enabled = false;
            chkAssistEmp.Enabled = false;

            //ddlAccidentDeath.SelectedIndex = 0;
            //TxtAccidentDeathPremium.Text = "";
            //ddlADPersons.SelectedIndex = 0;
            //TxtEquitmentSonido.Text = "";
            //ddlEquitmentSonido.SelectedIndex = 0;
            //TxtEquitmentAudio.Text = "";
            //ddlEquitmentAudio.SelectedIndex = 0;
            //TxtEquitmentTapes.Text = "";
            //chkEquipTapes.Checked = false;
            //TxtEquipColl.Text = "";
            //chkEquipColl.Checked = false;
            //TxtEquipComp.Text = "";
            //chkEquipComp.Checked = false;
            //TxtCustomizeEquipLimit.Text = "";
            //TxtEquipTotal.Text = "";

            //TxtUninsuredSingle.Text = "";
            //ddlUninsuredSingle.SelectedIndex = 0;
            //TxtUninsuredSplit.Text = "";
            //ddlUninsuredSplit.SelectedIndex = 0;

            chkLoJack.Checked = false;
            txtLoJackCertificate.Text = "";
            TxtLojackExpDate.Text = "";

            ddlExperienceDiscount.SelectedIndex = 0;
            ddlEmployeeDiscount.SelectedIndex = 0;
            txtMiscDiscount.Text = "";
            TxtExpDisc.Text = "0";
        }

        private void enableFields(bool status)
        {

            ddlBank.Enabled = status;

            ddlPolicySubClass.Enabled = status;

            txtVINEdit.Enabled = status;
            txtPlateEdit.Enabled = status;
            ddlYear.Enabled = status;
            ddlMake.Enabled = status;
            ddlModel.Enabled = status;
            txtPurchaseDt.Enabled = status;
            //txtAge.Enabled = status;
            ddlNewUsed.Enabled = status;
            txtCost.Enabled = status;
            txtActualValue.Enabled = status;
            ddlHomeCity.Enabled = status;
            ddlWorkCity.Enabled = status;

            ddlVehicleClass.Enabled = status;
            ddlTerritory.Enabled = status;
            ddlAlarm.Enabled = status;
            txtDeprec1st.Enabled = status;
            txtDeprecAll.Enabled = status;
            ddlMedical.Enabled = status;
            //this.ddlAssistancePremium.Enabled = status;
            TxtAssistance.Enabled = status;
            ddlTowing.Enabled = status;
            //txtVehicleRental.Enabled = status;
            //ddlRental.Enabled = status;
            ddlLoanGap.Enabled = status;
            ddlSeatBelt.Enabled = status;
            ddlPAR.Enabled = status;

            ddlCollision.Enabled = status;
            ddlComprehensive.Enabled = status;

            ddlBI.Enabled = status;
            ddlPD.Enabled = status;
            ddlCSL.Enabled = status;

            SetEnableBIPDCLS(status);
            SetEnableUninsured(status);

            //this.btnCalendar.Disabled = !status;

            txtLicenseNumber.Enabled = status;
            txtExpDate.Enabled = status;
            chkIsLeasing.Enabled = status;
            

            if (status)
            {
                //imgExpDate.Visible = false;
               // imgPurchaseDt.Visible = false;
            }
            else
            {
                //imgExpDate.Visible = true;
               // imgPurchaseDt.Visible = true;
            }

            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (!cp.IsInRole("AUTOEDITDISCOUNT") &&
            !cp.IsInRole("ADMINISTRATOR"))
            {
                txtDiscountCollComp.Visible = false;
                lblDiscountCollComp.Visible = false;
                txtDiscountBIPD.Visible = false;
                lblDiscountBIPD.Visible = false;
            }
            else
            {
                txtDiscountCollComp.Visible = true;
                lblDiscountCollComp.Visible = true;
                txtDiscountBIPD.Visible = true;
                lblDiscountBIPD.Visible = true;
            }

            if (!cp.IsInRole("AUTO PERSONAL DELETE VEHICLES") && !cp.IsInRole("ADMINISTRATOR"))
                this.dgVehicle.Columns[6].Visible = false;
            else
                this.dgVehicle.Columns[6].Visible = true;
        }

        private void SetEnableBIPDCLS(bool status)
        {
            if (status)
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
        }

        private void SetEnableUninsured(bool status)
        {
            //if (status)
            //{
            //    ddlUninsuredSingle.Enabled = true;
            //    ddlUninsuredSplit.Enabled = true;

            //    if (ddlUninsuredSingle.SelectedItem.Text.Trim() != "")
            //    {
            //        ddlUninsuredSingle.Enabled = true;
            //        ddlUninsuredSplit.Enabled = false;
            //    }

            //    if (ddlUninsuredSplit.SelectedItem.Text.Trim() != "")
            //    {
            //        ddlUninsuredSingle.Enabled = false;
            //        ddlUninsuredSplit.Enabled = true;
            //    }
            //}
        }

        private void IspolicyNewButtonClick()
        {
            if (this.AdditionOfCoverAllowedOnGroundsOfBaseType((TaskControl.QuoteAuto)Session["TaskControl"]))
            {
                ViewState.Add("Status", "NEW");
                clearFields();
                enableFields(false);
                ddlPolicySubClass.Enabled = true;
                btnSave.Visible = false;
                btnAssignDrv.Visible = false;
                btnViewCvr.Visible = false;
                this.SetControlState((int)States.NEW);
                //this.ddlPolicySubClass.BackColor = System.Drawing.Color.Red;
                this.dgVehicle.SelectedIndex = -1;

                if (TxtVehicleCount.Text == "")
                    TxtVehicleCount.Text = "1";

                if (Session["TaskControl"] != null)
                {
                    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)(TaskControl.QuoteAuto)Session["TaskControl"];

                    if (QA.IsPolicy || Session["OptimaPersonalPackage"] != null)
                    {
                        ddlBank.Enabled = false;
                        ddlCompanyDealer.Enabled = false;
                    }
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

                        txtVINEdit.Text = ACTemp.VIN;

                        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByValue(
                            ACTemp.VehicleYear.ToString()));

                        ddlMake.SelectedIndex = ddlMake.Items.IndexOf(ddlMake.Items.FindByValue(
                            ACTemp.VehicleMake.ToString()));

                        fillModel();

                        ddlModel.SelectedIndex = ddlModel.Items.IndexOf(ddlModel.Items.FindByValue(
                            ACTemp.VehicleModel.ToString()));

                        ddlHomeCity.SelectedIndex = ddlHomeCity.Items.IndexOf(ddlHomeCity.Items.FindByValue(
                            QA.Customer.CityPhysical.ToString()));

                        ddlWorkCity.SelectedIndex = ddlWorkCity.Items.IndexOf(ddlWorkCity.Items.FindByValue(
                            QA.Customer.EmplCity.ToString()));

                        ddlVehicleClass.SelectedIndex = ddlVehicleClass.Items.IndexOf(ddlVehicleClass.Items.FindByValue(
                            ACTemp.VehicleClass.ToString()));

                        ddlTerritory.SelectedIndex = ddlTerritory.Items.IndexOf(ddlTerritory.Items.FindByValue(
                            ACTemp.Territory.ToString()));

                        ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(
                            ACTemp.Bank.ToString()));

                        ddlCompanyDealer.SelectedIndex = ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(
                            ACTemp.CompanyDealer.ToString()));

                        //Selecciona el driver menor por default siempre y cuando haya en la lista un driver.
                        if (ddlDriver.Items.Count > 1)
                        {
                            SetPrimaryDriver(QA.QuoteId);
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

        //        private void dgVehicle_ItemCommand(
        //            object source, 
        //            System.Web.UI.WebControls.DataGridCommandEventArgs e)
        //        {
        //            //RPR 2004-05-17
        //            Login.Login cp = HttpContext.Current.User as Login.Login;
        //            int userID = 0;

        //            try
        //            {
        //                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
        //            }
        //            catch(Exception ex)
        //            {
        //                throw new Exception(
        //                    "Could not parse user id from cp.Identity.Name.", ex);
        //            }
        //            // 0  btn
        //            // 1  Make
        //            // 2  Model
        //            // 3  VIN
        //            // 4  Plate
        //            // 5  Year
        //            // 6  btnRemove
        //            // 7  QuotesAutoID
        //            // 8  QuotesID
        //            // 9 InternalID
        //            TaskControl.QuoteAuto QA = 
        //                (TaskControl.QuoteAuto)/*2?*/
        //                (TaskControl.QuoteAuto)Session["TaskControl"];

        //            if(e.Item.ItemType.ToString() != "Pager")
        //            {			
        //                // Search on QA Auto Cover List for matching & Display it
        //                AutoCover AC = new AutoCover();
        //                if (e.Item.Cells[3].Text.ToLower() != "")
        //                    AC.VIN = e.Item.Cells[3].Text;
        //                if (e.Item.Cells[7].Text != "0")
        //                    AC.QuotesAutoId = int.Parse(e.Item.Cells[7].Text);
        //                if (e.Item.Cells[8].Text != "0")
        //                    AC.QuotesId = int.Parse(e.Item.Cells[8].Text);
        //                AC.InternalID = int.Parse(e.Item.Cells[9].Text);
        //                AC = QA.GetAutoCover(AC);

        //                if(e.CommandName == "Select") // Select
        //                {
        //                    this.SelectVehicle(e);
        //                    this.ddlPolicySubClass.BackColor = 
        //                        System.Drawing.Color.White;
        //                }
        //                if(e.CommandName == "Remove") // Select
        //                {
        //                    // Remove from QA Auto Cover List 
        //                    if (AC.Mode == (int)Enumerators.Modes.Insert)
        //                    {
        //                        QA.RemoveAutoCover(AC);
        //                    }
        //                    else
        //                        AC.Mode = (int)Enumerators.Modes.Delete;
        //                    ViewState.Add("Status","NEW");
        //                    clearFields();
        //                    //: RPR 2004-03-23

        //                    QA.Save(userID);
        //                    QA = (TaskControl.QuoteAuto)
        //                        TaskControl.TaskControl.GetTaskControlByTaskControlID(
        //                        QA.TaskControlID, userID);

        //                    Session["TaskControl"] = QA;

        //                    if(this.GetCurrentNumberOfAutos() == 1)
        //                    {
        //                        //this.ApplyDiscountToExistingCovers(0,0);

        //                        this.applyToAll.Value = "1";
        //                        SetDiscountForOneVehicleOnly();

        ////						this.litPopUp.Text = 
        ////							Utilities.MakeLiteralPopUpString(
        ////							"Only one vehicle remaining: removing " +
        ////							"multiple-vehicle-dependent BI/PD and Coll/Comp discounts.");
        ////						this.litPopUp.Visible = true;						
        //                    }

        //                    QA = (TaskControl.QuoteAuto) Session["TaskControl"];
        //                    //QA.Calculate();
        //                    fillDataGrid(QA.AutoCovers);
        //                    this.SelectLastVehicle();
        //                }	
        //            }
        //            else //Pager
        //            {
        //                this.dgVehicle.CurrentPageIndex = 
        //                    int.Parse(e.CommandArgument.ToString())-1;
        //            }
        //            fillDataGrid(QA.AutoCovers);
        //            //: RPR 2004-03-23
        //            Session["TaskControl"] = QA;
        //        } 

        private void SetDiscountForOneVehicleOnly()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            AutoCover autoCover = this.LoadFromForm(int.Parse(
                ViewState["QuotesAutoId"].ToString()),
                int.Parse(Session["InternalAutoID"].ToString().Trim()));

            Login.Login cp = HttpContext.Current.User as Login.Login;
            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            this.ReapplyDiscounts();

            QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            foreach (AutoCover aC in QA.AutoCovers)
            {
                aC.Mode = 2; //Update
            }

            if (QA.IsPolicy)
                QA.Save(UserID, autoCover, null, false);
            else
                QA.Save(UserID);

            Session["TaskControl"] = QA;

            this.ShowAutoCover((AutoCover)QA.AutoCovers[
                QA.AutoCovers.IndexOf(autoCover)]);

            this.applyToAll.Value = "0";
            this.SetControlState((int)States.READONLY);
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
            txtVINEdit.Text = AC.VIN;
            //	txtPlate.Text = AC.Plate;
            txtPlateEdit.Text = AC.Plate;
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
            //txtPurchaseDt.Text = AC.PurchaseDate;
            txtPurchaseDt.Text = Convert.ToDateTime(AC.PurchaseDate).ToString("MM/dd/yyyy");
            txtAge.Text = AC.VehicleAge.ToString();
            ddlNewUsed.SelectedIndex =
                ddlNewUsed.Items.IndexOf(
                ddlNewUsed.Items.FindByValue(AC.NewUse.ToString()));
            txtCost.Text = AC.Cost.ToString("###,###");
            txtActualValue.Text = AC.ActualValue.ToString("###,###");
            ddlHomeCity.SelectedIndex =
                ddlHomeCity.Items.IndexOf(
                ddlHomeCity.Items.FindByValue(AC.HomeCity.ToString()));
            ddlWorkCity.SelectedIndex =
                ddlWorkCity.Items.IndexOf(
                ddlWorkCity.Items.FindByValue(
                AC.WorkCity.ToString()));

             

            if (QA.IsPolicy || Session["OptimaPersonalPackage"] != null)
            {
                ddlBank.SelectedIndex =
                    ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(
                    AC.Bank.ToString()));

                ddlCompanyDealer.SelectedIndex =
                    ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(
                    AC.CompanyDealer.ToString()));
            }
            else if (!QA.IsPolicy)
            {
                ddlBank.SelectedIndex =
                   ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(
                   AC.Bank.ToString()));

            }

            txtLicenseNumber.Text = AC.License;
            chkIsLeasing.Checked = AC.IsLeasing;
           // txtExpDate.Text = AC.LicenseExpDate;

            if (AC.LicenseExpDate != "")
                txtExpDate.Text = Convert.ToDateTime(AC.LicenseExpDate).ToString("MM/dd/yyyy");
            else
                txtExpDate.Text = "";


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

                    this.chkOnlyOperator.Checked = AD.OnlyOperator;
                    this.chkPrincipalOperator.Checked = AD.PrincipalOperator;
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

            txtDeprec1st.Text = AC.Depreciation1stYear.ToString();
            txtDeprecAll.Text = AC.DepreciationAllYear.ToString();

            if (this.txtDeprec1st.Text == "15")
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
                ddlMedical.SelectedIndex =
                    ddlMedical.Items.IndexOf(
                    ddlMedical.Items.FindByValue(
                    AC.MedicalLimit.ToString()));
            //this.SetDdlSelectedItemByText(this.ddlAssistancePremium,
            //	AC.AssistancePremium.ToString());			

            //TxtAssistance.Text = AC.AssistancePremium.ToString("###,###");
            //txtTowingPrm.Text = AC.TowingPremium.ToString("###,###");
            //ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(ddlTowing.Items.FindByValue(AC.TowingPremium.ToString().Replace("0", "").Replace(".", "")));

            txtTowing.Text = AC.TowingPremium.ToString("###,###");
            if (AC.TowingPremium != 0)
                ddlTowing.SelectedIndex = ddlTowing.Items.IndexOf(ddlTowing.Items.FindByValue(AC.TowingID.ToString()));

            if (AC.LeaseLoanGapId != 0)
            {
                ddlLoanGap.SelectedIndex = ddlLoanGap.Items.IndexOf(ddlLoanGap.Items.FindByValue(AC.LeaseLoanGapId.ToString()));
                chkLLG.Checked = true;
            }
            else
                chkLLG.Checked = false;


            if (chkLLG.Checked==true)
            {
                ddlLoanGap.Visible = true;
                ddlLoanGap.SelectedIndex = ddlLoanGap.Items.IndexOf(ddlLoanGap.Items.FindByValue(AC.LeaseLoanGapId.ToString()));
            }
            else
                ddlLoanGap.Visible = false;

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
            txtDiscountCollComp.Text = AC.DiscountCompColl.ToString("00.00");
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
            txtDiscountBIPD.Text = AC.DiscountBIPD.ToString("00.00");


            txtVehicleRental.Text = AC.VehicleRental.ToString("###,###");
            if (AC.VehicleRental != 0)
                ddlRental.SelectedIndex = ddlRental.Items.IndexOf(ddlRental.Items.FindByValue(AC.VehicleRentalID.ToString()));

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

            //TxtAccidentDeathPremium.Text = AC.AccidentalDeathPremium.ToString("###,###");
            //if (AC.AccidentalDeathPremium != 0)
            //    ddlAccidentDeath.SelectedIndex = ddlAccidentDeath.Items.IndexOf(ddlAccidentDeath.Items.FindByValue(AC.AccidentalDeathID.ToString()));

            //ddlADPersons.SelectedIndex = ddlADPersons.Items.IndexOf(ddlADPersons.Items.FindByValue(AC.AccidentalDeathPerson.ToString()));

            //TxtEquitmentSonido.Text = AC.EquipmentSoundPremium.ToString("###,###");
            //if (AC.EquipmentSoundPremium != 0)
            //    ddlEquitmentSonido.SelectedIndex = ddlEquitmentSonido.Items.IndexOf(ddlEquitmentSonido.Items.FindByValue(AC.EquipmentSoundID.ToString()));

            //TxtEquitmentAudio.Text = AC.EquipmentAudioPremium.ToString("###,###");
            //if (AC.EquipmentAudioPremium != 0)
            //    ddlEquitmentAudio.SelectedIndex = ddlEquitmentAudio.Items.IndexOf(ddlEquitmentAudio.Items.FindByValue(AC.EquipmentAudioID.ToString()));

            //TxtEquitmentTapes.Text = AC.EquipmentTapesPremium.ToString("###,###");
            //chkEquipTapes.Checked = AC.EquipmentTapes;

            //TxtEquipColl.Text = AC.SpecialEquipmentCollPremium.ToString("###,###");
            //chkEquipColl.Checked = AC.SpecialEquipmentColl;

            //TxtEquipComp.Text = AC.SpecialEquipmentCompPremium.ToString("###,###");
            //chkEquipComp.Checked = AC.SpecialEquipmentComp;
            //TxtCustomizeEquipLimit.Text = AC.CustomizeEquipLimit.ToString();
            //TxtEquipTotal.Text = (AC.SpecialEquipmentCollPremium + AC.SpecialEquipmentCompPremium).ToString("###,###");
            //TxtCustomizeEquipLimit.Text = AC.CustomizeEquipLimit.ToString("###,###");

            //TxtUninsuredSingle.Text = AC.UninsuredSinglePremium.ToString("###,###");
            //if (AC.UninsuredSinglePremium != 0)
            //    ddlUninsuredSingle.SelectedIndex = ddlUninsuredSingle.Items.IndexOf(ddlUninsuredSingle.Items.FindByValue(AC.UninsuredSingleID.ToString()));

            //TxtUninsuredSplit.Text = AC.UninsuredSplitPremium.ToString("###,###");
            //if (AC.UninsuredSplitPremium != 0)
            //    ddlUninsuredSplit.SelectedIndex = ddlUninsuredSplit.Items.IndexOf(ddlUninsuredSplit.Items.FindByValue(AC.UninsuredSplitID.ToString()));

            txtIsAssistanceEmp.Text = AC.IsAssistanceEmp.ToString().Trim();

            chkLoJack.Checked = AC.LoJack;
            TxtLojackExpDate.Text = AC.LojackExpDate;
            txtLoJackCertificate.Text = AC.LoJackCertificate;

            //if (AC.ExperienceDiscount != 0)
            //    ddlExperienceDiscount.SelectedIndex = ddlExperienceDiscount.Items.IndexOf(ddlExperienceDiscount.Items.FindByValue(AC.ExperienceDiscount.ToString()));

            TxtExpDisc.Text = AC.ExperienceDiscount.ToString();       

            if (AC.EmployeeDiscount != 0)
                ddlEmployeeDiscount.SelectedIndex = ddlEmployeeDiscount.Items.IndexOf(ddlEmployeeDiscount.Items.FindByValue(AC.EmployeeDiscount.ToString()));

            txtMiscDiscount.Text = AC.MiscDiscount.ToString("00.00");

            this.txt1stISO.Text = this.Get1stPeriodISOCode(AC);

            this.txtPartialPremium.Text = String.Format("{0:c}", ((int)AC.TotalAmount));
            this.txtPartialDiscount.Text = String.Format("{0:c}", decimal.Parse(AC.TotDiscount.ToString()));
            this.txtPartialCharge.Text = String.Format("{0:c}", Math.Round(AC.Charge, 0));
            this.txtTotalPremium.Text = String.Format("{0:c}", ((int)AC.TotalAmount + decimal.Parse(AC.TotDiscount.ToString())) + Math.Round(AC.Charge, 0));

            SetPolicySubClass();
        }

        private string GetDisplayName(AutoDriver AD)
        {
            return AD.FirstName + " " + AD.LastName1 + " - " + AD.BirthDate;
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

            AC.VIN = txtVINEdit.Text.Trim().ToUpper();
            AC.Plate = txtPlateEdit.Text.Trim().ToUpper();
            if (ddlYear.SelectedIndex > 0 && ddlYear.SelectedItem != null)
                AC.VehicleYear = int.Parse(ddlYear.SelectedItem.Value);
            if (ddlMake.SelectedIndex > 0 && ddlMake.SelectedItem != null)
                AC.VehicleMake = int.Parse(ddlMake.SelectedItem.Value);
            if (ddlModel.SelectedIndex > 0 && ddlModel.SelectedItem != null)
                AC.VehicleModel = int.Parse(ddlModel.SelectedItem.Value);
            //RPR 2004-05-26
            if (ddlPolicySubClass.SelectedIndex > 0 &&
                ddlPolicySubClass.SelectedItem != null)
                AC.PolicySubClassId = int.Parse(ddlPolicySubClass.SelectedItem.Value.Trim());
            AC.PurchaseDate = this.txtPurchaseDt.Text = "5/1/" + ddlYear.SelectedItem.Text.Trim(); //txtPurchaseDt.Text;
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

            if (ddlHomeCity.SelectedIndex > 0 && ddlHomeCity.SelectedItem != null)
                AC.HomeCity = int.Parse(ddlHomeCity.SelectedItem.Value);
            if (ddlWorkCity.SelectedIndex > 0 && ddlWorkCity.SelectedItem != null)
                AC.WorkCity = int.Parse(ddlWorkCity.SelectedItem.Value);
            if (ddlVehicleClass.SelectedIndex > 0 && ddlVehicleClass.SelectedItem != null)
                AC.VehicleClass = ddlVehicleClass.SelectedItem.Value;
            if (ddlTerritory.SelectedIndex > 0 && ddlTerritory.SelectedItem != null)
                AC.Territory = int.Parse(ddlTerritory.SelectedItem.Value);
            if (ddlAlarm.SelectedIndex > 0 && ddlAlarm.SelectedItem != null)
                AC.AlarmType = int.Parse(ddlAlarm.SelectedItem.Value);
            if (txtDeprec1st.Text != "")
                AC.Depreciation1stYear = decimal.Parse(txtDeprec1st.Text);

            //JDP pidi� igualar campos.
            if (txtDeprecAll.Text != "")
                AC.DepreciationAllYear = decimal.Parse(this.txtDeprec1st.Text);

            if (ddlMedical.SelectedIndex > 0 && ddlMedical.SelectedItem != null)
                AC.MedicalLimit = int.Parse(ddlMedical.SelectedItem.Value);
            //			if (this.ddlAssistancePremium.SelectedItem != null &&
            //				this.ddlAssistancePremium.SelectedItem.Text != string.Empty)
            //				AC.AssistancePremium = decimal.Parse(
            //					this.ddlAssistancePremium.SelectedItem.Text.Trim());

            if (TxtAssistance.Text != "")
            {
                //No Aplica para DI
                if (int.Parse(term.Value.Trim()) > 12)
                {
                    AC.AssistancePremium = 0.0m;
                    TxtAssistance.Text = "0.0";

                }
                else
                {
                    AC.AssistancePremium = decimal.Parse(TxtAssistance.Text);
                }
            }

            //AC.TowingPremium = decimal.Parse(ddlTowing.SelectedItem.Value);
            AC.TowingPremium = Decimal.Round(decimal.Parse(ddlTowing.SelectedItem.Value), 4); 

            if (ddlLoanGap.SelectedIndex > 0 && ddlLoanGap.SelectedItem != null)
                AC.LeaseLoanGapId = int.Parse(ddlLoanGap.SelectedItem.Value);
            if (ddlSeatBelt.SelectedIndex > 0 && ddlSeatBelt.SelectedItem != null)
                AC.SeatBelt = int.Parse(ddlSeatBelt.SelectedItem.Value);
            if (ddlPAR.SelectedIndex > 0 && ddlPAR.SelectedItem != null)
                AC.PersonalAccidentRider = int.Parse(ddlPAR.SelectedItem.Value);
            if (ddlCollision.SelectedIndex > 0 && ddlCollision.SelectedItem != null)
                AC.CollisionDeductible = int.Parse(ddlCollision.SelectedItem.Value);
            if (ddlComprehensive.SelectedIndex > 0 && ddlComprehensive.SelectedItem != null)
                AC.ComprehensiveDeductible = int.Parse(ddlComprehensive.SelectedItem.Value);
            if (txtDiscountCollComp.Text != "")
                AC.DiscountCompColl = Decimal.Round(decimal.Parse(txtDiscountCollComp.Text), 0); 
            if (ddlBI.SelectedIndex > 0 && ddlBI.SelectedItem != null)
                AC.BodilyInjuryLimit = int.Parse(ddlBI.SelectedItem.Value);
            if (ddlPD.SelectedIndex > 0 && ddlPD.SelectedItem != null)
                AC.PropertyDamageLimit = int.Parse(ddlPD.SelectedItem.Value);
            if (ddlCSL.SelectedIndex > 0 && ddlCSL.SelectedItem != null)
                AC.CombinedSingleLimit = int.Parse(ddlCSL.SelectedItem.Value);
            if (txtDiscountBIPD.Text != "")
                AC.DiscountBIPD = Decimal.Round(decimal.Parse(txtDiscountBIPD.Text), 0); 
           // decimal.Parse(txtDiscountBIPD.Text)

            if (txtTowing.Text.Trim() == "")
                txtTowing.Text = "0.0000";

            if (txtVehicleRental.Text.Trim() == "")
                txtVehicleRental.Text = "0.0000";

            //if (TxtAccidentDeathPremium.Text.Trim() == "")
            //    TxtAccidentDeathPremium.Text = "0";

            //if (TxtEquitmentSonido.Text.Trim() == "")
            //    TxtEquitmentSonido.Text = "0";

            //if (TxtEquitmentAudio.Text.Trim() == "")
            //    TxtEquitmentAudio.Text = "0";

            //if (TxtEquitmentTapes.Text.Trim() == "")
            //    TxtEquitmentTapes.Text = "0";

            //if (TxtEquipColl.Text.Trim() == "")
            //    TxtEquipColl.Text = "0";

            //if (TxtEquipComp.Text.Trim() == "")
            //    TxtEquipComp.Text = "0";

            //if (TxtUninsuredSingle.Text.Trim() == "")
            //    TxtUninsuredSingle.Text = "0";

            //if (TxtUninsuredSplit.Text.Trim() == "")
            //    TxtUninsuredSplit.Text = "0";

            //if (TxtCustomizeEquipLimit.Text.Trim() == "")
            //    TxtCustomizeEquipLimit.Text = "0";

           // AC.OriginalTowingPremium = decimal.Parse(ddlTowing.SelectedItem.Value); Decimal.Round(decimal.Parse(txtTowing.Text.Trim()), 4);
            AC.OriginalTowingPremium = Decimal.Round(decimal.Parse(ddlTowing.SelectedItem.Value), 4);
            AC.OriginalVehicleRental = decimal.Parse(GetOriginalVehicleRentaPremium());

            AC.TowingID = int.Parse(ddlTowing.SelectedItem.Value);
            //AC.TowingPremium = decimal.Parse(txtTowing.Text.Trim());
            AC.TowingPremium = Decimal.Round(decimal.Parse(txtTowing.Text.Trim()), 4); 
            AC.VehicleRentalID = int.Parse(ddlRental.SelectedItem.Value);
            AC.VehicleRental = decimal.Parse(txtVehicleRental.Text.Trim());
            AC.AccidentalDeathID = 0; // int.Parse(ddlAccidentDeath.SelectedItem.Value);
            AC.AccidentalDeathPremium = 0; // decimal.Parse(TxtAccidentDeathPremium.Text.Trim());
            AC.AccidentalDeathPerson = 0; // int.Parse(ddlADPersons.SelectedItem.Value);

            AC.EquipmentSoundID = 0; // int.Parse(ddlEquitmentSonido.SelectedItem.Value);
            AC.EquipmentSoundPremium = 0; //decimal.Parse(TxtEquitmentSonido.Text.Trim());
            AC.EquipmentAudioID = 0; // int.Parse(ddlEquitmentAudio.SelectedItem.Value);
            AC.EquipmentAudioPremium = 0; // decimal.Parse(TxtEquitmentAudio.Text.Trim());
            AC.EquipmentTapesPremium = 0; // decimal.Parse(TxtEquitmentTapes.Text);
            AC.EquipmentTapes = false; // chkEquipTapes.Checked;
            AC.SpecialEquipmentCollPremium = 0; // decimal.Parse(TxtEquipColl.Text);
            AC.SpecialEquipmentColl = false; // chkEquipColl.Checked;
            AC.SpecialEquipmentCompPremium = 0; // decimal.Parse(TxtEquipComp.Text);
            AC.SpecialEquipmentComp = false; // chkEquipComp.Checked;
            AC.CustomizeEquipLimit = 0; // int.Parse(TxtCustomizeEquipLimit.Text);
            AC.UninsuredSingleID = 0; // int.Parse(ddlUninsuredSingle.SelectedItem.Value);
            AC.UninsuredSinglePremium = 0; // decimal.Parse(TxtUninsuredSingle.Text.Trim());
            AC.UninsuredSplitID = 0; // int.Parse(ddlUninsuredSplit.SelectedItem.Value);
            AC.UninsuredSplitPremium = 0; // decimal.Parse(TxtUninsuredSplit.Text.Trim());

            AC.LoJack = chkLoJack.Checked;
            AC.LojackExpDate = TxtLojackExpDate.Text.Trim();
            AC.LoJackCertificate = txtLoJackCertificate.Text.Trim();

            if (ddlRoadAssistEmp.SelectedItem.Text.Trim() != "0" && 1==0)
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
            //AC.LicenseExpDate = txtExpDate.Text.Trim();
           // AC.LicenseExpDate = Convert.ToDateTime(txtExpDate.Text.Trim()).ToString("MM/dd/yyyy");
            if (txtExpDate.Text.Trim() != "")
                AC.LicenseExpDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(txtExpDate.Text.Trim()).ToShortDateString());
            else
                AC.LicenseExpDate = "";

            if (ddlBank.SelectedIndex > 0 && ddlBank.SelectedItem != null)
                AC.Bank = ddlBank.SelectedItem.Value.ToString();

            if (QA.IsPolicy || Session["OptimaPersonalPackage"] != null)
            {
                if (ddlCompanyDealer.SelectedIndex > 0 && ddlCompanyDealer.SelectedItem != null)
                    AC.CompanyDealer = ddlCompanyDealer.SelectedItem.Value.ToString();
            }

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
            // 13 Premium
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
            dt.Columns.Add("Premium", typeof(string));
            return dt;
        }

        private void fillDataGrid(ArrayList AL)
        {
            //RPR 2004-02-11 (move outside of 
            //'if's exclusive scope)
            DataTable DT = getDisplayDataTable();
            decimal totalPremium = 0;

            if (AL != null && AL.Count > 0)
            {
                this.dgVehicle.Visible = true;
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
                        row["Premium"] = String.Format("{0:c}", AC.TotalAmount);
                        totalPremium += AC.TotalAmount;
                        
                        try
                        {
                            /*RPR 2004-02-11
                            DataTable dt = 
                                GetVehicleMakeModelYear(
                                AC.VehicleMake, AC.VehicleModel, AC.VehicleYear);
                            row["VehicleMake"] = dt.Rows[0]["Make"];
                            row["VehicleModel"] = dt.Rows[0]["Model"];
                            row["VehicleYear"] = dt.Rows[0]["Year"];*/

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
                txtTotalPremiumGV.Text = String.Format("{0:c}", totalPremium);
            }
            //RPR 2004-02-11 (To preserve GUI behavior 
            //consistency with 'QuoteAutoDrivers.aspx'.)
            else
            {
                this.dgVehicle.Visible = false;
            }

            //RPR 2004-02-11 (move outside of 
            //'if's exclusive scope.)  Bound to 
            //empty table even when Visible = false
            //to prevent further 'stateful' disturbances.

            DT.DefaultView.Sort = "QuotesAutoID";
            
            dgVehicle.DataSource = DT;

            dgVehicle.DataBind();
        }

        private static DataTable GetVehicleMakeModelYear(
            int vehicleMakeID, int VehicleModelID, int VehicleYearID)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>vehicleMakeID</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + vehicleMakeID + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>VehicleModelID</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + VehicleModelID + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>VehicleYearID</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + VehicleYearID + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetVehicleMakeModelYear", xmlDoc);
            return dt;
        }

        protected void ddlMake_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            fillModel();
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

                ddlModel.SelectedIndex = -1;
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

        protected void ddlCSL_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlCSL.SelectedIndex > 0)
            {
                ddlPD.SelectedIndex = 0;
                ddlPD.Enabled = false;
                ddlBI.SelectedIndex = 0;
                ddlBI.Enabled = false;
            }
            else
            {
                ddlPD.Enabled = true;
                ddlBI.Enabled = true;
            }
        }

        protected void ddlPD_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlPD.SelectedIndex > 0)
            {
                ddlCSL.SelectedIndex = 0;
                ddlCSL.Enabled = false;
            }
            else if (ddlBI.SelectedIndex <= 0)
            {
                ddlCSL.Enabled = true;
            }
        }

        protected void ddlBI_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddlBI.SelectedIndex > 0)
            {
                ddlCSL.SelectedIndex = 0;
                ddlCSL.Enabled = false;
            }
            else if (ddlPD.SelectedIndex <= 0)
            {
                ddlCSL.Enabled = true;
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

        public enum ConfirmationType
        {
            DUPLICATE_VEHICLE, APPLY_DISCOUNT_TO_ALL
        };

        private string MakeConfirmPopUpString(string message,
            int ConfirmType)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=javascript>if(confirm('");
            message = System.Text.RegularExpressions.Regex.Replace(
                message, "\\r\\n", @"\r\n");
            message = System.Text.RegularExpressions.Regex.Replace(
                message, "\\n\\r", @"\n\r");
            message = System.Text.RegularExpressions.Regex.Replace(
                message, "'", @"\'");
            message = System.Text.RegularExpressions.Regex.Replace(
                message, "\"", "\\\"");
            sb.Append(message);

            switch (ConfirmType)
            {
                case (int)ConfirmationType.DUPLICATE_VEHICLE:
                    sb.Append("')){QuoteAutoVehicles.Callback.value='Y';QuoteAutoVehicles.submit();}else{QuoteAutoVehicles.Callback.value='N';QuoteAutoVehicles.submit();}</script>");
                    break;
                case (int)ConfirmationType.APPLY_DISCOUNT_TO_ALL:
                    sb.Append("')){QuoteAutoVehicles.applyToAll.value='1';QuoteAutoVehicles.submit();}else{QuoteAutoVehicles.applyToAll.value='0';QuoteAutoVehicles.submit();}</script>");
                    break;
                default:
                    //
                    break;
            }
            return (sb.ToString());
        }

        private string ValidateThis()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)(TaskControl.QuoteAuto)Session["TaskControl"];

            ArrayList errorMessages = new ArrayList();

            if (QA.IsPolicy) //Valida solo cuando es poliza.
            {
                if (this.txtVINEdit.Text.Trim() == "")
                {
                    errorMessages.Add("Please the VIN is empty." + ".\r\n");
                }

                if (this.txtVINEdit.Text.Length < 16)
                {
                    errorMessages.Add("VIN must be 17 or 16 " +
                        " characters in length." + ".\r\n");
                }

                if (this.txtPurchaseDt.Text.Trim() == "")
                {
                    errorMessages.Add("Please The Purchase Date is empty." + ".\r\n");
                }

                if (!this.IsSamsValidDate(this.txtPurchaseDt.Text))
                {
                    errorMessages.Add("Purchase date is invalid.  The " +
                        "correct format is \"mm/dd/yyyy\"." + ".\r\n");
                }
                
                if (this.txtPlateEdit.Text.Trim() == "")
                {
                    errorMessages.Add("Please The Plate is empty." + ".\r\n");
                }

                if (this.txtLicenseNumber.Text.Trim() == "")
                {
                    errorMessages.Add("Please The Licence Number is empty." + ".\r\n");
                }

                if (this.txtExpDate.Text.Trim() == "")
                {
                    errorMessages.Add("Please The Expiraion Date Licence is empty." + ".\r\n");
                }
                
            }

            if (ddlDriver.SelectedItem.Text.Trim() == "")
            {
                errorMessages.Add("Please choose the driver for this vehicle." + ".\r\n");
            }


            if (!rdo15percent.Checked && !rdo20percent.Checked)
            {
                errorMessages.Add("Please select 15% or 20% Depreciation.");
            }

            //Verifica si el vehiculo es usado entonces el vehicleAge debe ser mayor que 0.
            //if (int.Parse(ddlNewUsed.SelectedItem.Value) == 1)//Used
            //{
            //    if (txtAge.Text.Trim() == "")
            //    {
            //        errorMessages.Add("The Vehicle Age must be greatest than zero.");
            //    }
            //    else
            //    {
            //        if (int.Parse(txtAge.Text.Trim()) == 0)
            //        {
            //            errorMessages.Add("The Vehicle Age must be greatest than zero.");
            //        }
            //    }

            //    //Para uso solamente de Double Interest o Full Cover.
            //    if (ddlCollision.SelectedItem.Value != "" || ddlComprehensive.SelectedItem.Value != "")
            //    {
            //        if (txtActualValue.Text == "")
            //        {
            //            errorMessages.Add("The actual value must be greatest than zero.");
            //        }
            //        else
            //        {
            //            if (int.Parse(txtActualValue.Text.Trim()) == 0)
            //            {
            //                errorMessages.Add("The actual value must be greatest than zero.");
            //            }
            //        }
            //    }
            //}
            //else  //Si el vehiculo es nuevo el VehicleAge debe ser cero.
            //{
            //    if (txtAge.Text.Trim() == "")
            //    {
            //        errorMessages.Add("The Vehicle Age must be zero.");
            //    }
            //    else
            //    {
            //        if (int.Parse(txtAge.Text.Trim()) > 0)
            //        {
            //            errorMessages.Add("The Vehicle Age must be zero.");
            //        }
            //    }
            //}

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
                ddlCSL.SelectedItem.Value == "" && int.Parse(this.term.Value) < 12)
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
                if (txtCost.Text.Replace(",", "").Replace("$", "") == "")
                {
                    errorMessages.Add("The vehicle cost must be greatest than zero.");
                }
                else
                {
                    if (int.Parse(txtCost.Text.Trim().Replace(",","").Replace("$","")) == 0)
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
                    if (int.Parse(txtCost.Text.Trim().Replace(",", "").Replace("$", "")) == 0)
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
                        if (int.Parse(this.term.Value) <= 12)
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
                        if (int.Parse(this.term.Value) > 12)
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
                        if (int.Parse(this.term.Value) > 12)
                            errorMessages.Add("Please verify the term for FullCover.");
                    }
                    else
                    {
                        errorMessages.Add("The Policy Type must be FullCover.");
                    }
                }
            }
            //Double Interest
            if (QA.Term > 12)
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

            //Validar Otras cubiertas por Tipo de Seguro
            //Double Interest
            if (QA.Term > 12)
            {
                if (ddlComprehensive.SelectedItem.Text.Trim() != "" && (ddlBI.SelectedItem.Text.Trim() == "" && ddlCSL.SelectedItem.Text.Trim() == ""))
                {
                    //if (ddlAccidentDeath.SelectedItem.Text.Trim() != "")
                    //    errorMessages.Add("The Accident Death cover does not apply to the Double Interest cover.");

                    //if (ddlEquitmentSonido.SelectedItem.Text.Trim() != "")
                    //    errorMessages.Add("The Equipment Sound cover does not apply to the Double Interest cover.");

                    //if (ddlEquitmentAudio.SelectedItem.Text.Trim() != "")
                    //    errorMessages.Add("The Equipment Audio cover does not apply to the Double Interest cover.");

                    //if (chkEquipTapes.Checked)
                    //    errorMessages.Add("The Equipment Tapes, Disc. cover does not apply to the Double Interest cover.");

                    //if (TxtCustomizeEquipLimit.Text.Trim() != "")
                    //    errorMessages.Add("The Customize Equipment cover does not apply to the Double Interest cover.");

                    //if (ddlUninsuredSingle.SelectedItem.Text.Trim() != "")
                    //    errorMessages.Add("The Uninsured Single cover does not apply to the Double Interest cover.");

                    //if (ddlUninsuredSplit.SelectedItem.Text.Trim() != "")
                    //    errorMessages.Add("The Uninsured Split cover does not apply to the Double Interest cover.");
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

                //if (ddlEquitmentSonido.SelectedItem.Text.Trim() != "")
                //    errorMessages.Add("The Equipment Sound cover does not apply to the liability cover.");

                //if (ddlEquitmentAudio.SelectedItem.Text.Trim() != "")
                //    errorMessages.Add("The Equipment Audio cover does not apply to the liability cover.");

                //if (chkEquipTapes.Checked)
                //    errorMessages.Add("The Equipment Tapes, Disc. cover does not apply to the liability cover.");

                //if (TxtCustomizeEquipLimit.Text.Trim() != "")
                //    errorMessages.Add("The Customize Equipment cover does not apply to the liability cover.");
            }

            //FullCover
            if (ddlComprehensive.SelectedItem.Value != "" && (ddlBI.SelectedItem.Value != "" || ddlCSL.SelectedItem.Value != ""))
            {
                //Aplican todas las cubiertas
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

        private void SaveVehicle()
        {
            string resultMess = this.ValidateThis();
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
                                ApplyDiscountSecondDiscount(QA);
                                /*this.ApplyDiscountToExistingCovers(
                                    AC.DiscountBIPD, AC.DiscountCompColl);*/

                                //QA.Calculate();
                                Session["TaskControl"] = QA;
                            }

                            //RPR 2004-03-16
                            ViewState.Add("InternalAutoID",
                                AC.InternalID.ToString());

                            btnSave.Visible = false;
                            btnAssignDrv.Visible = false;
                            btnViewCvr.Visible = false;

                            lblRecHeader.Text = "Vehicle Saved Successfully";
                            mpeSeleccion.Show();
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


                         Quotes.AutoCover ACforTotalPremium = null;

                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {
                    ACforTotalPremium = (AutoCover)QA.AutoCovers[i];
                    QA.TotalPremium += ((int)ACforTotalPremium.TotalAmount + decimal.Parse(ACforTotalPremium.TotDiscount.ToString())) + Math.Round(ACforTotalPremium.Charge, 0);
                }
               

                        // reload Assigned Drivers & Breakdowns.
                        //AutoCover TmpAC = QA.GetAutoCover(AC);
                        //if (TmpAC != null)
                        //{
                        //    AC.AssignedDrivers = TmpAC.AssignedDrivers;
                        //    AC.Breakdown = TmpAC.Breakdown;
                        //}
                        QA.RemoveAutoCover(AC);
                        QA.AddAutoCover(AC, true);
                        ViewState.Add("Status", "NEW");

                        /*RPR 2004-03-15 clearFields();*/
                        btnSave.Visible = false;
                        btnAssignDrv.Visible = false;
                        btnViewCvr.Visible = false;

                        Session["TaskControl"] = QA;

                        lblRecHeader.Text = "Vehicle Updated Successfully";
                            mpeSeleccion.Show();
                        break;
                    default:
                        // Change to Edit Mode & add new
                        break;
                }

                //if(DifferentCoverPercentages(AC, QA))
                if (this.PendingMultipleVehicleDiscount(QA))
                {
                    ApplyDiscountSecondDiscount(QA);
                    //					this.litPopUp.Text = 
                    //						this.MakeConfirmPopUpString(
                    //						"Applicable discounts due to the addition " +
                    //						"of this vehicle have been found for at least one " + 
                    //						"existing cover. " +
                    //						"Do you wish to apply them?", 
                    //						(int)
                    //						ConfirmationType.APPLY_DISCOUNT_TO_ALL);
                    //					this.litPopUp.Visible = true;

                    //this.applyToAll.Value = "1";
                    //SetDiscountForOneVehicleOnly();
                }
                else
                {
                    if (QA.AutoCovers.Count == 1) // Para eliminar el descunto del 20% del primer vehiculo.
                        ApplyDiscountSecondDiscount(QA);
                }

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

                //string effdate = "";
                //if (QA.EffectiveDate.Trim() == "")
                //    effdate = "10/01/2013";
                //else
                //    effdate = QA.EffectiveDate.Trim();

                //if (QA.EntryDate >= DateTime.Parse("10/01/2013") && DateTime.Parse(effdate) >= DateTime.Parse("10/01/2013"))
                //{
                //    CalculateCharge();
                //    QA.Charge = decimal.Parse(txtPartialCharge.Text.ToString());
                //}
                //else
                //    QA.Charge = 0;

                //aqui ya debio guardad total premium en QA
                
               
              

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

        private void CalculateCharge()
        {
            try
            {
                EPolicy.TaskControl.QuoteAuto QA = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];

                bool isnew = true;
                if (QA.Policy.Suffix != "00")
                    isnew = false;

                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[4];
                DbRequestXmlCooker.AttachCookItem("policyClassID", SqlDbType.Int, 0, QA.PolicyClassID.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("PolicyType", SqlDbType.Char, 3, "AUT", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("IsNew", SqlDbType.Bit, 0, isnew.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("effectiveDate", SqlDbType.DateTime, 0, QA.EffectiveDate, ref cookItems);

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
                        double totcharge = Math.Round((double.Parse(QA.TotalPremium.ToString()) * charge), 0);
                        txtPartialCharge.Text = totcharge.ToString("###,###.00");
                    }
                    else
                    {
                        txtPartialCharge.Text = "0.0";
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

                   // cover.DiscountBIPD = OldDiscBIPD;
                    cover.DiscountBIPD = Decimal.Round(OldDiscBIPD, 0);
                    //cover.DiscountCompColl = OldDiscComp;
                    cover.DiscountCompColl = Decimal.Round(OldDiscComp, 0); 
                    //cover.MiscDiscount = double.Parse(OldDiscBIPD.ToString());                   

                    if (IsBIPD == false)
                        cover.DiscountBIPD = OldDiscBIPD;
                    //cover.MiscDiscount = double.Parse(OldDiscBIPD.ToString());

                    if (IsCollComp == false)
                        cover.DiscountCompColl = OldDiscComp;
                    // cover.MiscDiscount = double.Parse(OldDiscBIPD.ToString());

                    ////Verifica si no tiene cubierta poene en 0 el descuento.
                    //if (cover.BodilyInjuryLimit == 0)//BodilyInjuryPremium() == 0)
                    //    cover.DiscountBIPD = 0;
                    ////cover.MiscDiscount = 0;

                    //if (cover.CollisionDeductible == 0) //CollisionPremium() == 0)
                    //    cover.DiscountCompColl = 0;
                    ////cover.MiscDiscount = 0;

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

        private bool TermCorrespondsAdequatelyToBaseTypes(
            TaskControl.QuoteAuto QA)
        {
            foreach (AutoCover cover in QA.AutoCovers)
            {
                switch (this.GetBasePolicySubClassFromPolicySubClassID(
                    cover.PolicySubClassId))
                {
                    case 1: //DI
                        if (QA.Term < 24 || QA.Term > 84)
                            return false;
                        break;
                    case 2: //LI
                        if (QA.Term > 12)
                            return false;
                        break;
                    case 3: //FC
                        goto case 2;
                    default:
                        break;
                }
            }
            return true;
        }

        private bool PendingMultipleVehicleDiscount(
            TaskControl.QuoteAuto QA)
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

        private bool DifferentCoverPercentages(AutoCover AC,
            TaskControl.QuoteAuto QA)
        {
            foreach (AutoCover aC in QA.AutoCovers)
            {
                if (aC.DiscountBIPD != AC.DiscountBIPD ||
                    aC.DiscountCompColl != AC.DiscountCompColl)
                {
                    return true;
                }
            }
            return false;
        }

        protected void ddlHomeCity_SelectedIndexChanged(
            object sender, System.EventArgs e)
        {
            // set Vehicle Class
            if (
                ddlWorkCity.SelectedIndex !=
                0 && ddlHomeCity.SelectedIndex != 0)
                doCityDistance();
            // set Territory
            if (ddlHomeCity.SelectedIndex == 0)
                ddlTerritory.SelectedIndex = 0;
            else
            {
                switch (ddlHomeCity.SelectedItem.Value)
                {
                    case "7": // Arecibo
                        ddlTerritory.SelectedIndex =
                            ddlTerritory.Items.IndexOf(
                            ddlTerritory.Items.FindByValue("7"));
                        break;
                    case "11": // Bayamon
                    case "16": // Carolina
                    case "32": // Guaynabo
                    case "65": // San Juan
                    case "71": // Trujillo Alto
                    case "79": // Rio Piedras
                        ddlTerritory.SelectedIndex =
                            ddlTerritory.Items.IndexOf(
                            ddlTerritory.Items.FindByValue("1"));
                        break;
                    case "13": // Caguas
                        ddlTerritory.SelectedIndex =
                            ddlTerritory.Items.IndexOf(
                            ddlTerritory.Items.FindByValue("5"));
                        break;
                    case "50": // Mayaguez
                        ddlTerritory.SelectedIndex =
                            ddlTerritory.Items.IndexOf(
                            ddlTerritory.Items.FindByValue("6"));
                        break;
                    case "58": // Ponce
                        ddlTerritory.SelectedIndex =
                            ddlTerritory.Items.IndexOf(
                            ddlTerritory.Items.FindByValue("3"));
                        break;
                    default: // Island
                        ddlTerritory.SelectedIndex =
                            ddlTerritory.Items.IndexOf(
                            ddlTerritory.Items.FindByValue("8"));
                        break;
                }
            }
        }

        protected void ddlWorkCity_SelectedIndexChanged(
            object sender, System.EventArgs e)
        {
            // set Vehicle Class
            if (ddlWorkCity.SelectedIndex !=
                0 && ddlHomeCity.SelectedIndex != 0)
                doCityDistance();
        }

        private void doCityDistance()
        {
            int d = 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>CityA</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + ddlWorkCity.SelectedItem.Value + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>CityB</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + ddlHomeCity.SelectedItem.Value + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            DataTable dt = exec.GetQuery("GetCityDistance", xmlDoc);

            if (dt.Rows.Count > 0)
                d = (int)dt.Rows[0]["Distance"];

            if (d > 15)
                ddlVehicleClass.SelectedIndex = ddlVehicleClass.Items.IndexOf(ddlVehicleClass.Items.FindByValue("2B"));
            else
                ddlVehicleClass.SelectedIndex = ddlVehicleClass.Items.IndexOf(ddlVehicleClass.Items.FindByValue("2A"));
        }

        private void cvPage_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string tmpMesg = "";

            //			rfvPurchaseDt
            //Commented by RPR 2004-05-21
            /*if (txtPurchaseDt.Text == "" || (DateTime.Parse(txtPurchaseDt.Text)) > DateTime.Now)
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg + "\\n" + "Please enter a valid Purchase Date.";
                }*/

            //			rfvNewUsed
            if (ddlNewUsed.SelectedIndex <= 0)
            {
                args.IsValid = false;
                tmpMesg = tmpMesg + "\\n" + "Please select if vehicle is New or Used.";
            }
            //			rfvCost
            if (txtCost.Text == "")
            {
                args.IsValid = false;
                tmpMesg = tmpMesg + "\\n" + "Please enter Vehicle Cost.";
            }
            //			rfvVehicleClass
            if (ddlVehicleClass.SelectedIndex <= 0)
            {
                args.IsValid = false;
                tmpMesg = tmpMesg + "\\n" + "Please enter Vehicle Class.";
            }
            //			rfvTerritory
            if (ddlTerritory.SelectedIndex <= 0)
            {
                args.IsValid = false;
                tmpMesg = tmpMesg + "\\n" + "Please enter a Territory.";
            }
            //			rfvAlarm
            //			if (ddlAlarm.SelectedIndex <= 0)
            //			{
            //				args.IsValid = false;
            //				tmpMesg = tmpMesg + "\\n" + "Please choose Alarm Type.";
            //			}

            if (((bool)ViewState["DI_Cover"]))
            {
                //			rfvActualValue
                if (txtActualValue.Text == "")
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg + "\\n" +
                        "Please enter Vehicle Actual Value.";
                }
                //			rfvDeprec1st
                if (txtDeprec1st.Text == "")
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg + "\\n" +
                        "Please enter Depreciation for 1st Year.";
                }
                //			rfvDeprecAll
                if (txtDeprecAll.Text == "")
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg +
                        "\\n" + "Please enter Depreciation for All Years.";
                }
            }
            //	1 - Double Interest
            //			(Coll and Comp)
            //	2 - Public Liability
            //			(BI and PD) or (CSL)
            //	3 - Full Cover
            //		(Coll and Comp) and ((BI and PD) or (CSL))

            if (((bool)ViewState["DI_Cover"]) || ((bool)ViewState["FC_Cover"]))
            {
                // Collision Validate
                if (ddlComprehensive.SelectedIndex >
                    0 && ddlCollision.SelectedIndex <= 0)
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg + "\\n" +
                        "Please enter a Collision limit.";
                }
                // Comprehensive Validate
                if (ddlComprehensive.SelectedIndex <=
                    0 && ddlCollision.SelectedIndex > 0)
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg +
                        "\\n" + "Please enter a Comprehensive limit.";
                }
                // No Limit Selected Validate
                if (ddlComprehensive.SelectedIndex <=
                    0 && ddlCollision.SelectedIndex <= 0)
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg +
                        "\\n" +
                        "Please enter Collision and Comprehensive limits.";
                }
            }
            if (((bool)ViewState["Liab_Cover"]) ||
                ((bool)ViewState["FC_Cover"]))
            {
                // BILimit Validate
                if (ddlCSL.SelectedIndex <=
                    0 && ddlPD.SelectedIndex > 0 && ddlBI.SelectedIndex <= 0)
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg + "\\n" + "Please select BI/PD Limits";
                }
                // PDLimit Validate
                if (ddlCSL.SelectedIndex <=
                    0 && ddlPD.SelectedIndex <= 0 && ddlBI.SelectedIndex > 0)
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg + "\\n" + "Please select BI/PD Limits";
                }
                // CSLLimit Validate
                if (ddlCSL.SelectedIndex >
                    0 && (ddlBI.SelectedIndex > 0 || ddlPD.SelectedIndex > 0))
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg + "\\n" +
                        "Please select between BI/PD or CSL limit.";
                }
                // No Limit Selected Validate
                if (ddlCSL.SelectedIndex <=
                    0 && (ddlBI.SelectedIndex <=
                    0 && ddlPD.SelectedIndex <= 0))
                {
                    args.IsValid = false;
                    tmpMesg = tmpMesg +
                        "\\n" + "Please select between BI/PD or CSL limit.";
                }
            }
            // Wrap it Up!!
            if (!args.IsValid)
            {
                lblRecHeader.Text = tmpMesg;
                mpeSeleccion.Show();
            }
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

            //Commented by RPR 2004-05-21
            /*if ((CoverType == 2 || CoverType == 3) &&
                    (AC.PolicySubClassId == 2 || AC.PolicySubClassId == 3)) // liab/fc	2,3
                {
                    return true;
                }
                else if ((CoverType == 4 || CoverType == 7 || CoverType == 8 || CoverType == 9) &&
                    (AC.PolicySubClassId == 4 || AC.PolicySubClassId == 7 || AC.PolicySubClassId == 8 || AC.PolicySubClassId == 9)) // autoplus 4,7,8,9
                {
                    return true;
                }
                else if (CoverType == AC.PolicySubClassId)// all rest alone
                {
                    return true;
                }
                return false;
            }
            return true;*/
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

        private int GetCurrentNumberOfAutos()
        {
            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto quote = (TaskControl.QuoteAuto)Session["TaskControl"];

                if (quote.IsPolicy)
                {
                    return quote.AutoCovers.Count;
                }
                else
                {
                    return quote.AutoCovers.Count + 1; //Para quote en esta pantalla va hacer mas de dos vehiculos,
                    // ya que en la primera pantalla se entra el primer vehiculo.
                }
            }
            return 0;
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

        private void SetDdlSelectedItemByValue(
            System.Web.UI.WebControls.DropDownList Ddl, string Val)
        {
            for (int i = 0; i < Ddl.Items.Count; i++)
            {
                if (Ddl.Items[i].Value == Val)
                {
                    Ddl.SelectedIndex = i;
                    return;
                }
            }
        }

        private void ResetDefaultValueFields()
        {
            this.txtDiscountBIPD.Text = string.Empty;
            this.txtDiscountCollComp.Text = string.Empty;
            this.SetDdlSelectedItemByText(this.ddlCSL, string.Empty);
            this.SetDdlSelectedItemByText(this.ddlMedical, string.Empty);
            //this.ddlAssistancePremium.SelectedIndex = 0;
            TxtAssistance.Text = string.Empty;
            this.SetDdlSelectedItemByText(this.ddlPAR, string.Empty);
        }

        private void ReapplyDiscounts()
        {
            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto)Session["TaskControl"];
                AutoCover cover;

                for (int i = 0; i < quoteAuto.AutoCovers.Count; i++)
                {
                    cover = (AutoCover)quoteAuto.AutoCovers[i];

                    this.SetDiscount(quoteAuto, ref cover);
                    //this.SetDiscountRelatedValues(cover.PolicySubClassId, true, ref cover);
                }

                //quoteAuto.Calculate();
                Session["TaskControl"] = quoteAuto;
            }
        }

        private void SetDiscount(TaskControl.QuoteAuto qAuto, ref AutoCover CoverToApply)
        {
            if (CoverToApply.CollisionDeductible.ToString().Trim() != "0")
            {
                //CoverToApply.DiscountCompColl = CollCompDisc;
                //CoverToApply.DiscountBIPD = BiPdDisc;
            }
        }

        public enum DiscountType { BIPD, COLLCOMP }

        private void SetValues(decimal BiPdDisc, decimal CollCompDisc, int CslLimit,
            int VehicleRental, decimal LlGap, int Medical, int RoadAssistance,
            int TowPremium, int SeatBelt, int PAR, bool ApplyToCover,
            ref AutoCover CoverToApply)
        {
            //            Login.Login cp = HttpContext.Current.User as Login.Login;

            //            if(ApplyToCover && CoverToApply != null)
            //            {
            //                if (!cp.IsInRole("AUTOEDITDISCOUNT") &&
            //                    !cp.IsInRole("ADMINISTRATOR"))
            //                {
            //                    CoverToApply.DiscountCompColl = CollCompDisc;
            //                    CoverToApply.DiscountBIPD = BiPdDisc;
            //                }
            //            }
            //            else
            //            {
            //                if (!cp.IsInRole("AUTOEDITDISCOUNT") &&
            //                    !cp.IsInRole("ADMINISTRATOR"))
            //                {
            //                    if (BiPdDisc != 0)
            //                    {
            //                        this.txtDiscountBIPD.Text = BiPdDisc.ToString();
            //                        CoverToApply.DiscountBIPD = BiPdDisc;
            //                    }
            //                    if (CollCompDisc != 0)
            //                    {
            //                        this.txtDiscountCollComp.Text = CollCompDisc.ToString();
            //                        CoverToApply.DiscountCompColl = CollCompDisc;
            //                    }
            //                }

            //                if(CslLimit != 0)
            //                    this.SetDdlSelectedItemByValue(
            //                        this.ddlCSL, CslLimit.ToString());

            //                if(LlGap != 0)
            //                    this.MakeOnlyChoice(
            //                        this.ddlLoanGap, LlGap.ToString(), string.Empty);
            //                if(Medical != 0)
            //                    this.MakeOnlyChoice(
            //                        this.ddlMedical, Medical.ToString(), string.Empty);
            ////				if(RoadAssistance != 0)
            ////					this.MakeOnlyChoice(this.ddlAssistancePremium,
            ////						RoadAssistance.ToString(), string.Empty);

            //                if (RoadAssistance == 0)
            //                {
            //                    if (this.GetCurrentNumberOfAutos() > 1)
            //                    {
            //                        DataTable VehList = GetAutoVehiclesList();
            //                        if (VehList.Rows.Count > 0)
            //                        {
            //                            for (int a = 0; a < VehList.Rows.Count; a++)
            //                            {
            //                                if ((decimal)VehList.Rows[a]["AssistancePremium"] > 0)
            //                                {
            //                                    this.TxtAssistance.Text = "25";
            //                                    a = VehList.Rows.Count;
            //                                }
            //                                else
            //                                {
            //                                    this.TxtAssistance.Text = "48";
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            this.TxtAssistance.Text = "25";
            //                        }
            //                    }
            //                    else
            //                        this.TxtAssistance.Text = "48";
            //                }
            //                else
            //                {
            //                    this.TxtAssistance.Text = RoadAssistance.ToString();
            //                }


            //                if(TowPremium != 0)
            //                    this.txtTowingPrm.Text = TowPremium.ToString();

            //                if (VehicleRental != 0)
            //                {
            //                    this.txtVehicleRental.Text = VehicleRental.ToString();
            //                    ddlRental.SelectedIndex = ddlRental.Items.IndexOf(ddlRental.Items.FindByValue(VehicleRental.ToString()));
            //                }

            //                if(SeatBelt != 0)
            //                    this.SetDdlSelectedItemByText(this.ddlSeatBelt, 
            //                        SeatBelt.ToString());
            //                if(PAR != 0)
            //                    this.MakeOnlyChoice(this.ddlPAR, PAR.ToString(), string.Empty);
            //            }
        }

        private void MakeOnlyChoice(System.Web.UI.WebControls.DropDownList list,
            string ChoiceText, string ChoiceValue)
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

        private bool QualifiesFor_MoreThanOneAuto_Discount(
            bool BIPD, ArrayList AutoCovers, AutoCover ProposedCover)
        {
            if (this.GetCurrentNumberOfAutos() > 1)
                return true;
            else
                return false;

            //			if(this.GetCurrentNumberOfAutos() >= 1 || 
            //				ProposedCover.isSecondVehicle)
            //			{
            //				foreach(AutoCover cover in AutoCovers)
            //				{
            //					switch(
            //						this.GetBasePolicySubClassFromPolicySubClassID(
            //						cover.PolicySubClassId))
            //					{
            //						case 1: //DI
            //							if((!BIPD && ProposedCover != cover) &&
            //								ProposedCover.InternalID != cover.InternalID)
            //								return true;
            //							break;
            //						case 2: //LI
            //							if((BIPD && ProposedCover != cover) &&
            //								ProposedCover.InternalID != cover.InternalID) 
            //								return true;
            //							break;
            //						case 3: //FC
            //							if(ProposedCover != cover)
            //								return true;
            //							break;
            //						default:
            //							break;
            //					}
            //				}
            //			}
            //			return false;
        }

        private void SetDefaultFieldValues(string SelectedSubPolicyItemValue)
        {
            this.ResetDefaultValueFields();

            if (ViewState["Status"] != null)
            {
                if (ViewState["Status"].ToString() == "NEW")
                {
                    rdo15percent.Checked = true;
                    rdo20percent.Checked = false;

                    this.txtDeprec1st.Text = "15";
                    this.txtDeprecAll.Text = "15";

                    //this.txtPurchaseDt.Text = DateTime.Today.ToShortDateString();
                    txtPurchaseDt.Text = "05/01/" + DateTime.Today.Year.ToString(); // AC.PurchaseDate;
                    
                    this.ddlYear.SelectedIndex =
                        this.ddlYear.Items.IndexOf(
                        this.ddlYear.Items.FindByText(DateTime.Now.Year.ToString()));



                    this.txtAge.Text = "0";
                }
            }
            
            try
            {
                AutoCover cover = new AutoCover();

                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                string insco = QA.InsuranceCompany;

                this.SetDiscountRelatedValues(int.Parse(SelectedSubPolicyItemValue.Trim()), false, ref cover, insco);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Could not parse SubPolicyID value into integer.", ex);
            }
        }

        private void SetDiscountRelatedValues(int PolicySubClassID,
            bool ApplyToCover, ref AutoCover CoverToApply, string insco)
        {
            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                bool qualifiesForMultiAutoBIPDdiscount = this.QualifiesFor_MoreThanOneAuto_Discount(true,
                    QA.AutoCovers, CoverToApply);
                bool qualifiesForMultiAutoCOLLCOMPdiscount = this.QualifiesFor_MoreThanOneAuto_Discount(false,
                    QA.AutoCovers, CoverToApply);
                int currentNumberOfAutos = this.GetCurrentNumberOfAutos();

                // 1 - DI
                // 2 - Liability
                // 3 - Full Cover
                switch (insco)
                {
                    case "052": //AIICO - EMPLEADO; DI
                        if (PolicySubClassID == 1)      //DI
                        {
                            this.SetValues(0, 10, 0, 0, 0.00M, 0, 0, 0, 0, 30, ApplyToCover, ref CoverToApply);
                        }
                        else if (PolicySubClassID == 3)  //FC
                        {
                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 48.0M : 35.0M,
                                qualifiesForMultiAutoCOLLCOMPdiscount ? 48.0M : 35.0M,
                                0, 0, 0.00M, 0, 0, 0, 0, 30, ApplyToCover, ref CoverToApply);
                        }
                        else
                        {
                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 20 : 0,
                                qualifiesForMultiAutoCOLLCOMPdiscount ? 20 : 0, 0, 0,
                                0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                        }
                        break;

                    case "056": //PRICO - LI

                        if (PolicySubClassID == 1)      //DI
                        {
                            this.SetValues(0, 17.5M, 0, 0, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                        }
                        else if (PolicySubClassID == 2)  //LI
                        {
                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34 : 17.5M,
                                0, 0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0, 0, 0, 0,
                                ApplyToCover, ref CoverToApply);
                        }
                        else if (PolicySubClassID == 3)  //FC
                        {
                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34M : 17.5M,
                                qualifiesForMultiAutoCOLLCOMPdiscount ? 34M : 17.5M,
                                0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0,
                                0, 0, 0, ApplyToCover, ref CoverToApply);
                        }
                        break;

                    case "066": //PRICO - LI

                        if (PolicySubClassID == 1)      //DI
                        {
                            this.SetValues(0, 17.5M, 0, 0, 0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                        }
                        else if (PolicySubClassID == 2)  //LI
                        {
                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34 : 17.5M,
                                0, 0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0, 0, 0, 0,
                                ApplyToCover, ref CoverToApply);
                        }
                        else if (PolicySubClassID == 3)  //FC
                        {
                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 34M : 17.5M,
                                qualifiesForMultiAutoCOLLCOMPdiscount ? 34M : 17.5M,
                                0, 0, 0.00M, 0, currentNumberOfAutos >= 1 ? 0 : 0,
                                0, 0, 0, ApplyToCover, ref CoverToApply);
                        }
                        break;

                    default:
                        if (Session["OptimaPersonalPackage"] != null)
                        {
                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 40 : 20,
                                qualifiesForMultiAutoCOLLCOMPdiscount ? 40 : 20, 0, 0,
                                0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                        }
                        else
                        {
                            if (QA.PolicyType.Trim() == "MFE")
                            {
                                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 40 : 35,
                                    qualifiesForMultiAutoCOLLCOMPdiscount ? 40 : 35, 0, 0,
                                    0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                            }
                            else
                                if (QA.PolicyType == "M01" || QA.PolicyType.Trim() == "M13")//Scotia & Federal
                                {
                                    this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 35 : 35,
                                    qualifiesForMultiAutoCOLLCOMPdiscount ? 35 : 35, 0, 0,
                                    0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                                }
                                else
                                    if (QA.PolicyType == "M07")
                                    {
                                        this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 55 : 35,
                                        qualifiesForMultiAutoCOLLCOMPdiscount ? 55 : 35, 0, 0,
                                        0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                                    }
                                    else
                                        if (QA.PolicyType == "M09")
                                        {
                                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 60 : 45,
                                            qualifiesForMultiAutoCOLLCOMPdiscount ? 60 : 45, 0, 0,
                                            0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                                        }
                                        else
                                            if (QA.PolicyType.Trim() == "M02" || QA.PolicyType.Trim() == "M03"
                                                || QA.PolicyType.Trim() == "M04" || QA.PolicyType.Trim() == "M05" || QA.PolicyType.Trim() == "M06"
                                                || QA.PolicyType == "M08" || QA.PolicyType == "M12")
                                            {
                                                this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 50 : 35,
                                                    qualifiesForMultiAutoCOLLCOMPdiscount ? 50 : 35, 0, 0,
                                                    0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                                            }
                                            else
                                                if (QA.PolicyType.Trim() == "M10") //Menonitas
                                                {
                                                    this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 40 : 40,
                                                        qualifiesForMultiAutoCOLLCOMPdiscount ? 40 : 40, 0, 0,
                                                        0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                                                }
                                                else
                                                    if (QA.PolicyType.Trim() == "M11") //Assurance
                                                    {
                                                        this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 55 : 55,
                                                            qualifiesForMultiAutoCOLLCOMPdiscount ? 55 : 55, 0, 0,
                                                            0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                                                    }
                                                    else
                                                        if (QA.PolicyType.Trim() == "M16") //CUD
                                                        {
                                                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 60 : 40,
                                                                qualifiesForMultiAutoCOLLCOMPdiscount ? 60 : 40, 0, 0,
                                                                0.00M, 0, 0, 0, 0, 0, true, ref CoverToApply);
                                                        }
                                                        else
                                                        {
                                                            this.SetValues(qualifiesForMultiAutoBIPDdiscount ? 0 : 0,
                                                                qualifiesForMultiAutoCOLLCOMPdiscount ? 0 : 0, 0, 0,
                                                                0.00M, 0, 0, 0, 0, 0, ApplyToCover, ref CoverToApply);
                                                        }
                        }
                        break;
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

        private bool SetPolicySubClass()
        {
            //RPR 2004-03-15
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

            //RPR 2004-05-27
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

                    txtDeprec1st.Enabled = true;
                    txtDeprecAll.Enabled = true;

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

                    txtDeprec1st.Enabled = true;
                    txtDeprecAll.Enabled = true;

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

        //:RPR 2004-05-25
        private void SetAsterisks(int ClassID)
        {
            int baseClassID =
                AutoCover.GetAutoPolicySubClassBaseClassID(ClassID);

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

        private enum States { NEW, READONLY, READWRITE, REST };

        private void SetInputState(bool Enabled)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            this.txtVINEdit.Enabled = Enabled;
            this.ddlPolicySubClass.Enabled = Enabled;
            this.ddlVehicleClass.Enabled = Enabled;
            this.ddlCollision.Enabled = Enabled;
            this.txtPlateEdit.Enabled = Enabled;
            this.ddlTerritory.Enabled = Enabled;
            this.ddlComprehensive.Enabled = Enabled;
            this.ddlYear.Enabled = Enabled;
            this.ddlAlarm.Enabled = Enabled;
            this.txtDiscountCollComp.Enabled = Enabled;
            this.ddlMake.Enabled = Enabled;
            this.txtDeprec1st.Enabled = Enabled;
            this.ddlBI.Enabled = Enabled;
            this.ddlModel.Enabled = Enabled;
            this.txtDeprecAll.Enabled = Enabled;
            this.ddlPD.Enabled = Enabled;
            this.txtPurchaseDt.Enabled = Enabled;
            this.ddlMedical.Enabled = Enabled;
            this.ddlCSL.Enabled = Enabled;
            //this.txtAge.Enabled = Enabled;
            //this.ddlAssistancePremium.Enabled = Enabled;
            this.TxtAssistance.Enabled = Enabled;
            this.txtDiscountBIPD.Enabled = Enabled;
            this.ddlNewUsed.Enabled = Enabled;
            this.ddlTowing.Enabled = Enabled;
            //this.txtVehicleRental.Enabled = Enabled;
            //this.ddlRental.Enabled = Enabled;
            this.txtCost.Enabled = Enabled;
            this.ddlLoanGap.Enabled = Enabled;
            this.txtActualValue.Enabled = Enabled;
            this.ddlSeatBelt.Enabled = Enabled;
            this.ddlHomeCity.Enabled = Enabled;
            this.ddlPAR.Enabled = Enabled;
            this.ddlWorkCity.Enabled = Enabled;
            //this.btnCalendar.Disabled = !Enabled;

            txtLicenseNumber.Enabled = Enabled;
            txtExpDate.Enabled = Enabled;
            chkIsLeasing.Enabled = Enabled;

            //imgExpDate.Visible = Enabled;
           // imgPurchaseDt.Visible = Enabled;
        }

        private void ShowPolicyFields(TaskControl.QuoteAuto QA, int State)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            //Mendez,El Morro, Open Mobile, Wesleyan, Interfood,Acaa
            if (QA.PolicyType == "M02" || QA.PolicyType == "M03" ||
                QA.PolicyType == "M04" || QA.PolicyType == "M05" ||
                QA.PolicyType == "M06" || QA.PolicyType == "M07" ||
                QA.PolicyType == "M08" || QA.PolicyType == "M09" ||
                QA.PolicyType == "M10" || QA.PolicyType == "M11" ||
                QA.PolicyType == "M12")
            {
                if (!cp.IsInRole("AUTOEDITDISCOUNT") &&
                !cp.IsInRole("ADMINISTRATOR"))
                {
                    txtDiscountCollComp.Visible = false;
                    lblDiscountCollComp.Visible = false;
                    txtDiscountBIPD.Visible = false;
                    lblDiscountBIPD.Visible = false;
                }
                else
                {
                    txtDiscountCollComp.Visible = true;
                    lblDiscountCollComp.Visible = true;
                    txtDiscountBIPD.Visible = true;
                    lblDiscountBIPD.Visible = true;
                }
            }
            else
            {
                if (!cp.IsInRole("AUTOEDITDISCOUNT") &&
                !cp.IsInRole("ADMINISTRATOR"))
                {
                    txtDiscountCollComp.Visible = false;
                    lblDiscountCollComp.Visible = false;
                    txtDiscountBIPD.Visible = false;
                    lblDiscountBIPD.Visible = false;
                }
                else
                {
                    txtDiscountCollComp.Visible = true;
                    lblDiscountCollComp.Visible = true;
                    txtDiscountBIPD.Visible = true;
                    lblDiscountBIPD.Visible = true;
                }
            }

            if (QA.IsPolicy)
            {
                this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.
                lblPrimaryDriver.Visible = false;

                this.LblBank.Visible = true;
                this.LblDealer.Visible = true;

                this.ddlBank.Visible = true;
                this.ddlCompanyDealer.Visible = true;
                this.ddlDriver.Visible = true;

                this.btnNext.Visible = true;

                //this.HplAdd.Visible = false; // true;
                this.chkOnlyOperator.Visible = true;
                this.chkPrincipalOperator.Visible = true;
            }
            else
            {
                this.btnAssignDrv.Visible = false;
                lblPrimaryDriver.Visible = false;
                this.LblBank.Visible = false;
                this.LblDealer.Visible = false;

                if (Session["OptimaPersonalPackage"] != null)
                {
                    this.LblBank.Visible = true;
                    this.LblDealer.Visible = true;
                    this.ddlBank.Visible = true;
                    this.ddlCompanyDealer.Visible = true;
                }
                else
                {
                    this.LblBank.Visible = true;
                    this.ddlBank.Visible = true;
                    this.ddlCompanyDealer.Visible = false;
                }

                this.ddlDriver.Visible = true;
                this.btnNext.Visible = false;
                //this.HplAdd.Visible = false;

                this.chkOnlyOperator.Visible = true;
                this.chkPrincipalOperator.Visible = true;
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

                        //this.HplAdd.Enabled = false; // true;
                        this.chkOnlyOperator.Enabled = true;
                        this.chkPrincipalOperator.Enabled = true;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            lblPrimaryDriver.Visible = false;
                            this.btnAssignDrv.Visible = false;
                            this.btnEdit.Visible = false;
                            this.btnSave.Visible = false;
                            this.btnCancel.Visible = false;
                            this.btnAddVhcl.Visible = false;
                            this.btnViewCvr.Visible = false;
                            this.btnDrivers.Visible = false;
                            this.btnBack.Visible = false;
                            this.btnNext.Visible = true;
                        }
                        else
                        {
                            lblPrimaryDriver.Visible = false;
                            this.btnAssignDrv.Visible = false;
                            this.btnEdit.Visible = false;
                            this.btnSave.Visible = true;
                            this.btnCancel.Visible = true;
                            this.btnAddVhcl.Visible = false;
                            this.btnViewCvr.Visible = false;
                            this.btnDrivers.Visible = false;
                            this.btnBack.Visible = true;
                            this.btnNext.Visible = false;
                        }
                        break;
                    case (int)States.READONLY:
                        this.chkOnlyOperator.Enabled = false;
                        this.chkPrincipalOperator.Enabled = false;
                        lblPrimaryDriver.Visible = false;
                        this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.

                        this.ddlBank.Enabled = false;
                        this.ddlCompanyDealer.Enabled = false;
                        this.ddlDriver.Enabled = false;

                        //this.HplAdd.Enabled = false;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            this.btnNext.Visible = true;
                        }
                        else
                        {
                            this.btnNext.Visible = false;
                            this.btnBack.Visible = true;
                            this.btnEdit.Visible = true;
                            this.btnViewCvr.Visible = true;
                        }
                        break;

                    case (int)States.READWRITE:
                        this.chkOnlyOperator.Enabled = true;
                        this.chkPrincipalOperator.Enabled = true;
                        lblPrimaryDriver.Visible = false;
                        this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.

                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        this.ddlDriver.Enabled = true;
                        this.btnBack.Visible = false;

                        //this.HplAdd.Enabled = false; // true;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            this.btnNext.Visible = false;
                        }
                        else
                        {
                            this.btnNext.Visible = false;
                        }

                        break;

                    case (int)States.REST:
                        this.chkOnlyOperator.Enabled = true;
                        this.chkPrincipalOperator.Enabled = true;
                        lblPrimaryDriver.Visible = false;
                        this.btnAssignDrv.Visible = false; // no se va a utilizar en la poliza.

                        this.ddlBank.Enabled = true;
                        this.ddlCompanyDealer.Enabled = true;
                        this.ddlDriver.Enabled = true;

                        this.btnNext.Visible = false;
                        //this.HplAdd.Enabled = false; // true;

                        //this.btnDrivers.Visible = false;
                        //this.btnAddVhcl.Visible = false;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            this.btnNext.Visible = false;
                        }
                        else
                        {
                            this.btnNext.Visible = false;
                            this.btnBack.Visible = true;
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

        private void SetControlState(int State)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            Login.Login cp = HttpContext.Current.User as Login.Login;
            switch (State)
            {
                case (int)States.NEW:
                    this.btnSave.Visible = false;
                    this.btnDrivers.Visible = false;
                    this.btnAssignDrv.Visible = false;
                    this.btnEdit.Visible = false;
                    this.btnCancel.Visible = true;
                    this.SetInputState(false);
                    this.ddlPolicySubClass.Enabled = true;
                    this.btnViewCvr.Visible = false;
                    this.btnAddVhcl.Visible = false;
                    this.ddlDriver.Enabled = true;
                    this.btnBack.Visible = false;
                    this.chkOnlyOperator.Enabled = true;
                    this.chkPrincipalOperator.Enabled = true;
                    this.TxtVehicleCount.Enabled = true;

                    //ddlRoadAssistEmp.Enabled = true;
                    ddlRoadAssistEmp.Enabled = false;
                    //ddlRoadAssist.Enabled = true;
                    ddlRoadAssist.Enabled = false;

                    //ddlAccidentDeath.Enabled = true;
                    //ddlADPersons.Enabled = true;
                    //TxtAccidentDeathPremium.Enabled = true;
                    //ddlUninsuredSingle.Enabled = true;
                    //TxtUninsuredSingle.Enabled = true;
                    //ddlUninsuredSplit.Enabled = true;
                    //TxtUninsuredSplit.Enabled = true;
                    //ddlEquitmentSonido.Enabled = true;
                    //TxtEquitmentSonido.Enabled = true;
                    //ddlEquitmentAudio.Enabled = true;
                    //TxtEquitmentAudio.Enabled = true;
                    //chkEquipTapes.Enabled = true;
                    //TxtEquitmentTapes.Enabled = true;
                    //chkEquipColl.Enabled = true;
                    //TxtEquipColl.Enabled = true;
                    //chkEquipComp.Enabled = true;
                    //TxtEquipComp.Enabled = true;
                    rdo15percent.Enabled = true;
                    rdo20percent.Enabled = true;
                    ddlRental.Enabled = true;
                    txtVehicleRental.Enabled = true;
                    txtTowing.Enabled = true;
                    //TxtCustomizeEquipLimit.Enabled = true;
                    //TxtEquipTotal.Enabled = true;
                    chkLLG.Enabled = true;
                    chkAssist.Enabled = true;

                    chkLoJack.Enabled = true;
                    TxtLojackExpDate.Enabled = true;
                    txtLoJackCertificate.Enabled = true;
                    imgCalendarLJExp.Visible = false;

                    if (txtIsAssistanceEmp.Text.Trim() == "True")
                    {
                        //ddlRoadAssistEmp.Enabled = true;
                        ddlRoadAssistEmp.Enabled = false;
                        ddlRoadAssist.Enabled = false;
                    }
                    else
                    {
                        ddlRoadAssistEmp.Enabled = false;
                        //ddlRoadAssist.Enabled = true;
                        ddlRoadAssist.Enabled = false;
                    }

                    ddlExperienceDiscount.Enabled = true;
                    ddlEmployeeDiscount.Enabled = true;
                    txtMiscDiscount.Enabled = true;
                    TxtExpDisc.Enabled = false;

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
                    break;
                case (int)States.READONLY:
                    this.btnSave.Visible = false;
                    this.btnDrivers.Visible = true;
                    this.btnAssignDrv.Visible = false; //true;
                    this.btnEdit.Visible = true;
                    this.btnCancel.Visible = false;
                    this.SetInputState(false);
                    this.btnViewCvr.Visible = true;
                    this.btnAddVhcl.Visible = true;
                    this.ddlDriver.Enabled = false;
                    this.btnBack.Visible = true;
                    this.chkOnlyOperator.Enabled = false;
                    this.chkPrincipalOperator.Enabled = false;
                    this.TxtVehicleCount.Enabled = false;

                    ddlBank.Enabled = false;
                    ddlRoadAssistEmp.Enabled = false;
                    ddlRoadAssist.Enabled = false;
                    //ddlAccidentDeath.Enabled = false;
                    //ddlADPersons.Enabled = false;
                    //TxtAccidentDeathPremium.Enabled = false;
                    //ddlUninsuredSingle.Enabled = false;
                    //TxtUninsuredSingle.Enabled = false;
                    //ddlUninsuredSplit.Enabled = false;
                    //TxtUninsuredSplit.Enabled = false;
                    //ddlEquitmentSonido.Enabled = false;
                    //TxtEquitmentSonido.Enabled = false;
                    //ddlEquitmentAudio.Enabled = false;
                    //TxtEquitmentAudio.Enabled = false;
                    //chkEquipTapes.Enabled = false;
                    //TxtEquitmentTapes.Enabled = false;
                    //chkEquipColl.Enabled = false;
                    //TxtEquipColl.Enabled = false;
                    //chkEquipComp.Enabled = false;
                    //TxtEquipComp.Enabled = false;
                    rdo15percent.Enabled = false;
                    rdo20percent.Enabled = false;
                    ddlRental.Enabled = false;
                    txtVehicleRental.Enabled = false;
                    txtTowing.Enabled = false;
                    //TxtCustomizeEquipLimit.Enabled = false;
                    //TxtEquipTotal.Enabled = false;
                    chkLLG.Enabled = false;
                    chkAssist.Enabled = false;

                    chkLoJack.Enabled = false;
                    TxtLojackExpDate.Enabled = false;
                    txtLoJackCertificate.Enabled = false;
                    imgCalendarLJExp.Visible = false;

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

                    ddlExperienceDiscount.Enabled = false;
                    ddlEmployeeDiscount.Enabled = false;
                    txtMiscDiscount.Enabled = false;

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
                    break;
                case (int)States.READWRITE:
                    this.btnSave.Visible = true;
                    this.btnDrivers.Visible = false;
                    this.btnAssignDrv.Visible = false; //true;
                    this.btnEdit.Visible = false;
                    this.btnCancel.Visible = true;
                    this.SetInputState(true);
                    this.btnViewCvr.Visible = false;
                    this.btnAddVhcl.Visible = false;
                    this.ddlDriver.Enabled = true;
                    this.btnBack.Visible = false;
                    this.chkOnlyOperator.Enabled = true;
                    this.chkPrincipalOperator.Enabled = true;
                    this.TxtVehicleCount.Enabled = true;

                   // ddlBank.Enabled = true;
                    
                    //ddlRoadAssistEmp.Enabled = true;
                    ddlRoadAssistEmp.Enabled = false;
                    //ddlRoadAssist.Enabled = true;
                    ddlRoadAssist.Enabled = false;
                    //ddlAccidentDeath.Enabled = true;
                    //ddlADPersons.Enabled = true;
                    //TxtAccidentDeathPremium.Enabled = true;
                    //ddlUninsuredSingle.Enabled = true;
                    //TxtUninsuredSingle.Enabled = true;
                    //ddlUninsuredSplit.Enabled = true;
                    //TxtUninsuredSplit.Enabled = true;
                    //ddlEquitmentSonido.Enabled = true;
                    //TxtEquitmentSonido.Enabled = true;
                    //ddlEquitmentAudio.Enabled = true;
                    //TxtEquitmentAudio.Enabled = true;
                    //chkEquipTapes.Enabled = true;
                    //TxtEquitmentTapes.Enabled = true;
                    //chkEquipColl.Enabled = true;
                    //TxtEquipColl.Enabled = true;
                    //chkEquipComp.Enabled = true;
                    //TxtEquipComp.Enabled = true;
                    rdo15percent.Enabled = true;
                    rdo20percent.Enabled = true;
                    ddlRental.Enabled = true;
                    txtVehicleRental.Enabled = true;
                    txtTowing.Enabled = true;
                    //TxtCustomizeEquipLimit.Enabled = true;
                    //TxtEquipTotal.Enabled = true;
                    chkLLG.Enabled = true;
                    chkAssist.Enabled = true;
                    chkAssistEmp.Enabled = true;
                    chkLoJack.Enabled = true;
                    TxtLojackExpDate.Enabled = true;
                    txtLoJackCertificate.Enabled = true;
                    imgCalendarLJExp.Visible = false;

                    if (txtIsAssistanceEmp.Text.Trim() == "True")
                    {
                        //ddlRoadAssistEmp.Enabled = true;
                        ddlRoadAssistEmp.Enabled = false;
                        ddlRoadAssist.Enabled = false;
                    }
                    else
                    {
                        ddlRoadAssistEmp.Enabled = false;
                        //ddlRoadAssist.Enabled = true;
                        ddlRoadAssist.Enabled = false;
                    }

                    ddlExperienceDiscount.Enabled = true;
                    ddlEmployeeDiscount.Enabled = true;
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
                    break;
                case (int)States.REST:
                    this.btnSave.Visible = false;
                    this.btnDrivers.Visible = false;
                    this.btnAssignDrv.Visible = false;
                    this.btnEdit.Visible = false;
                    this.btnCancel.Visible = false;
                    this.SetInputState(false);
                    this.btnViewCvr.Visible = false;
                    this.btnAddVhcl.Visible = true;
                    this.ddlDriver.Enabled = true;
                    this.btnBack.Visible = false;
                    this.chkOnlyOperator.Enabled = true;
                    this.chkPrincipalOperator.Enabled = true;
                    this.TxtVehicleCount.Enabled = false;

                    ddlBank.Enabled = false;
                    ddlRoadAssistEmp.Enabled = false;
                    ddlRoadAssist.Enabled = false;
                    //ddlAccidentDeath.Enabled = false;
                    //ddlADPersons.Enabled = false;
                    //TxtAccidentDeathPremium.Enabled = false;
                    //ddlUninsuredSingle.Enabled = false;
                    //TxtUninsuredSingle.Enabled = false;
                    //ddlUninsuredSplit.Enabled = false;
                    //TxtUninsuredSplit.Enabled = false;
                    //ddlEquitmentSonido.Enabled = false;
                    //TxtEquitmentSonido.Enabled = false;
                    //ddlEquitmentAudio.Enabled = false;
                    //TxtEquitmentAudio.Enabled = false;
                    //chkEquipTapes.Enabled = false;
                    //TxtEquitmentTapes.Enabled = false;
                    //chkEquipColl.Enabled = false;
                    //TxtEquipColl.Enabled = false;
                    //chkEquipComp.Enabled = false;
                    //TxtEquipComp.Enabled = false;
                    rdo15percent.Enabled = false;
                    rdo20percent.Enabled = false;
                    ddlRental.Enabled = false;
                    txtVehicleRental.Enabled = false;
                    txtTowing.Enabled = false;
                    //TxtCustomizeEquipLimit.Enabled = false;
                    //TxtEquipTotal.Enabled = false;
                    chkLLG.Enabled = false;
                    chkAssist.Enabled = false;

                    chkLoJack.Enabled = false;
                    TxtLojackExpDate.Enabled = false;
                    txtLoJackCertificate.Enabled = false;
                    imgCalendarLJExp.Visible = false;

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

                    ddlExperienceDiscount.Enabled = false;
                    ddlEmployeeDiscount.Enabled = false;
                    txtMiscDiscount.Enabled = false;
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
                    break;
                default:
                    //
                    break;
            }

            ShowPolicyFields(QA, State);
        }

        private void Cancel()
        {
            if (this.ViewState["Status"] != null &&
                this.ViewState["Status"].ToString().Trim() == "UPDATE")
            {
                if (Session["TaskControl"] != null && ViewState["QuotesAutoId"] != null &&
                    ViewState["InternalAutoID"] != null)
                {
                    TaskControl.QuoteAuto autoQuote = (TaskControl.QuoteAuto)/*2?*/
                        (TaskControl.QuoteAuto)Session["TaskControl"];
                    AutoCover autoCover = new AutoCover();

                    try
                    {
                        autoCover.QuotesAutoId = int.Parse(
                            ViewState["QuotesAutoId"].ToString().Trim());
                        autoCover.InternalID = int.Parse(
                            ViewState["InternalAutoID"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        lblRecHeader.Text = "Unable to parse auto cover id. " + ex.Message;
                        mpeSeleccion.Show();
                    }

                    autoCover = autoQuote.GetAutoCover(autoCover);

                    this.ShowAutoCover(autoCover);
                    this.SetControlState((int)States.READONLY);
                }
            }
            else
            {
                this.clearFields();
                this.SetControlState((int)States.REST);
                this.ddlPolicySubClass.BackColor = System.Drawing.Color.White;
            }

            ViewState.Add("Status", "REST");
        }

        private void RemoveSessionLookUp()
        {
            Session.Remove("LookUpTables");
        }

        protected void HplAdd_Click(object sender, System.EventArgs e)
        {
            SaveVehicle();
            IspolicyNewButtonClick();
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
            ArrayList assdr;
            if (QA.IsPolicy)
                assdr = Quotes.AssignedDriver.LoadDriversForAutoCover(ACC.QuotesAutoId, QA.Drivers, true);
            else
                assdr = Quotes.AssignedDriver.LoadDriversForAutoCover(ACC.QuotesAutoId, QA.Drivers, false);

            if (assdr.Count != 0)
            {
                EPolicy.TaskControl.QuoteAuto q;
                if (QA.IsPolicy)
                    q = new EPolicy.TaskControl.QuoteAuto(true);
                else
                    q = new EPolicy.TaskControl.QuoteAuto(false);

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
                AsDrv.PrincipalOperator = true;

                if (chkOnlyOperator.Checked)
                {
                    AsDrv.OnlyOperator = true;
                }
                else
                {
                    AsDrv.OnlyOperator = false;
                }

                if (chkPrincipalOperator.Checked)
                {
                    AsDrv.PrincipalOperator = true;
                }
                else
                {
                    AsDrv.PrincipalOperator = false;
                }

                AsDrv.Mode = (int)Enumerators.Modes.Insert;

                AC.AssignedDrivers.Add(AsDrv);
            }
            else
            {
                EPolicy.TaskControl.QuoteAuto qTemp;
                if (QA.IsPolicy)
                    qTemp = new EPolicy.TaskControl.QuoteAuto(true);
                else
                    qTemp = new EPolicy.TaskControl.QuoteAuto(false);

                qTemp.Drivers = assdr;
                Quotes.AssignedDriver driverUpdate = (Quotes.AssignedDriver)qTemp.Drivers[0];
                AsDrv = driverUpdate;
                //////
                AsDrv.OnlyOperator = chkOnlyOperator.Checked;
                AsDrv.PrincipalOperator = chkPrincipalOperator.Checked;

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
            Session["TaskControl"] = QA;
        }

        protected void ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            txtAge.Text = getVehicleAge();
        }

        private void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Session["TaskControl"] != null)
            {
                RemoveSessionLookUp();
                TaskControl.QuoteAuto quote = (TaskControl.QuoteAuto)Session["TaskControl"];
                Response.Redirect("SearchAuditItems.aspx?type=17&taskControlID=" +
                    quote.TaskControlID.ToString());
            }
        }

        protected void btnAssignDrv_Click(object sender, System.EventArgs e)
        {
            string queryStringElement = string.Empty;

            if (Session["TaskControl"] != null)
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto)Session["TaskControl"];
                AutoCover AC = null;

                int t = 0, iid = 0;
                if (ViewState["QuotesAutoId"] != null)
                    t = int.Parse(ViewState["QuotesAutoId"].ToString());
                if (ViewState["InternalAutoID"] != null)
                    iid = int.Parse(ViewState["InternalAutoID"].ToString());
                else
                    iid = quoteAuto.GetNewInternalID();

                AC = LoadFromForm(t, iid);

                AC.Mode = (int)Enumerators.Modes.Update;

                // reload Assigned Drivers & Breakdowns.
                AutoCover TmpAC = quoteAuto.GetAutoCover(AC);
                if (TmpAC != null)
                {
                    AC.AssignedDrivers = TmpAC.AssignedDrivers;
                    AC.Breakdown = TmpAC.Breakdown;
                }
                quoteAuto.RemoveAutoCover(AC);
                quoteAuto.AddAutoCover(AC, true);

                if (this.ViewState["Status"] != null &&
                    this.ViewState["Status"].ToString().Trim() == "UPDATE")
                {
                    queryStringElement = "?editMode=1";
                }

                ViewState.Add("Status", "NEW");

                //if(DifferentCoverPercentages(AC, quoteAuto))
                {
                    //				this.litPopUp.Text = 
                    //					this.MakeConfirmPopUpString(
                    //					"Applicable discounts due to the addition " +
                    //					"of this vehicle have been found for at least one " + 
                    //					"existing cover. " +
                    //					"Do you wish to apply them?", 
                    //					(int)
                    //					ConfirmationType.APPLY_DISCOUNT_TO_ALL);
                    //				this.litPopUp.Visible = true;

                    this.applyToAll.Value = "0";
                }

                if (quoteAuto.TaskControlID != 0)
                {
                    quoteAuto.Mode = (int)Enumerators.Modes.Update;
                }
                else
                {
                    quoteAuto.Mode = (int)Enumerators.Modes.Insert;
                }

                if (this.ViewState["Status"] != null &&
                    this.ViewState["Status"].ToString() == "UPDATE")
                    quoteAuto.Save(UserID, AC, null, false);

                Session["TaskControl"] = quoteAuto;
            }
            Session.Add("QuotesAutoID", ViewState["QuotesAutoId"]);
            Session.Add("InternalAutoID", ViewState["InternalAutoID"]);

            RemoveSessionLookUp();

            Response.Redirect("QuoteAutoAssignDriver.aspx" + queryStringElement);
        }

        protected void btnEdit_Click(object sender, System.EventArgs e)
        {
            this.ViewState["Status"] = "UPDATE";
            this.SetControlState((int)States.READWRITE);
            enableFields(true);

            //Se le quit� la coma para que pudiera calcular el costo original.
            if (txtCost.Text.Trim() != "")
                txtCost.Text = (decimal.Parse(txtCost.Text)).ToString("######");         //("###,###")
            if (txtActualValue.Text.Trim() != "")
                txtActualValue.Text = (decimal.Parse(txtActualValue.Text)).ToString("######");   //("###,###")		
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            // Se hizo un metodo para el save ya que se va a utilizar el mismo save para otros evento.
            try
            {
                SaveVehicle();

            }
            catch (Exception ecp)
            {
                LogError(ecp);
                lblRecHeader.Text = ecp.Message.Trim();
                mpeSeleccion.Show();
            }
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Cancel();
            this.SelectLastVehicle();
        }

        protected void btnAddVhcl_Click(object sender, System.EventArgs e)
        {
            IspolicyNewButtonClick();

        }

        protected void btnViewCvr_Click(object sender, System.EventArgs e)
        {
            if (Session["TaskControl"] != null && Session["InternalAutoID"] != null)
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                QA.Calculate();
                AutoCover AC = new AutoCover();

                for (int i = 0; i < QA.AutoCovers.Count; i++)
                {
                    AC = (AutoCover)QA.AutoCovers[i];
                    if (AC.InternalID == int.Parse(Session["InternalAutoID"].ToString()))
                    {
                        i = QA.AutoCovers.Count;
                    }
                }

                if (AC.Breakdown.Count > 0)
                {
                    Session.Add("InternalAutoID", ViewState["InternalAutoID"]);
                    Session.Add("FromPage", "QuoteAutoVehicles.aspx");
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

        protected void btnDrivers_Click(object sender, System.EventArgs e)
        {
            RemoveSessionLookUp();
            Response.Redirect("QuoteAutoDrivers.aspx");
        }
                       
        protected void btnBack_Click(object sender, System.EventArgs e)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                this.Cancel();

                RemoveSessionLookUp();

                //updateNewSession(QA.TaskControlID, UserID);

                if (QA.IsPolicy)
                {
                    Response.Redirect("QuoteAuto.aspx");
                }
                else
                {
                    QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    
                    QA.Mode = 4;
                    Session["TaskControl"] = QA;
                    Response.Redirect("ExpressAutoQuote.aspx");
                }
            }
            catch (Exception exc)
            {
                lblRecHeader.Text = exc.Message;
                mpeSeleccion.Show();
            }
        }

        private void updateNewSession(int TaskControlID, int UserID)
        {
            TaskControl.TaskControl taskControl = TaskControl.TaskControl.GetTaskControlByTaskControlID(TaskControlID,UserID);
            Session["TaskControl"] = taskControl;
        }


        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (QA.Policy.PolicyCicleEnd == 0)
            {
                if (QA.AutoCovers.Count == 0)
                {
                    SaveVehicle();
                    QA.Policy.PolicyCicleEnd = 1;  // Para completar el ciclo del wizard.
                    Session["TaskControl"] = QA;
                    RemoveSessionLookUp();
                    Response.Redirect("QuoteAuto.aspx", false);
                }
                else
                {
                    QA.Policy.PolicyCicleEnd = 1;  // Para completar el ciclo del wizard.
                    Session["TaskControl"] = QA;
                    RemoveSessionLookUp();
                    Response.Redirect("QuoteAuto.aspx", false);
                }
            }
        }
        protected void ddlPolicySubClass_SelectedIndexChanged1(object sender, EventArgs e)
        {
            SetPolicySubClass();
        }
        protected void btnExpDate_ServerClick(object sender, EventArgs e)
        {

        }
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
            TaskControl.QuoteAuto QA =
                (TaskControl.QuoteAuto)/*2?*/
                (TaskControl.QuoteAuto)Session["TaskControl"];

            if (e.Item.ItemType.ToString() != "Pager")
            {
                // Search on QA Auto Cover List for matching & Display it
                AutoCover AC = new AutoCover();
                if (e.Item.Cells[3].Text.ToLower() != "")
                    AC.VIN = e.Item.Cells[3].Text;
                if (e.Item.Cells[7].Text != "0")
                    AC.QuotesAutoId = int.Parse(e.Item.Cells[8].Text);//7
                if (e.Item.Cells[8].Text != "0")
                    AC.QuotesId = int.Parse(e.Item.Cells[9].Text);//8
                AC.InternalID = int.Parse(e.Item.Cells[10].Text);//9
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
                    //: RPR 2004-03-23

                    QA.Save(userID);
                    QA = (TaskControl.QuoteAuto)
                        TaskControl.TaskControl.GetTaskControlByTaskControlID(
                        QA.TaskControlID, userID);

                    Session["TaskControl"] = QA;

                    this.applyToAll.Value = "1";
                    //SetDiscountForOneVehicleOnly();
                    ApplyDiscountSecondDiscount(QA);
                   

                    QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    //QA.Calculate();
                    fillDataGrid(QA.AutoCovers);
                    this.SelectLastVehicle();
                    ApplySecondPriceRoadAssistance();//aqui
                    SaveVehicle();
                }
            }
            else //Pager
            {
                this.dgVehicle.CurrentPageIndex =
                    int.Parse(e.CommandArgument.ToString()) - 1;
            }
            fillDataGrid(QA.AutoCovers);
            //: RPR 2004-03-23
            Session["TaskControl"] = QA;
        }
        protected void dgVehicle_ItemCreated1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem ||
                e.Item.ItemType == ListItemType.EditItem)
            {
                TableCell tableCell = new TableCell();
                tableCell = e.Item.Cells[7];//6
                Button button = new Button();
                button = (Button)tableCell.Controls[0];
                button.Attributes.Add("onclick",
                    "return confirm( " +
                    "\"Are you sure you want to delete this vehicle?\")");
            }
        }

        private DataTable GetAutoVehiclesList()
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

                DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

                DbRequestXmlCooker.AttachCookItem("QuotesID", SqlDbType.Int, 0, QA.QuoteId.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("IsPolicy", SqlDbType.Bit, 0, QA.IsPolicy.ToString(), ref cookItems);
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

                //exec.BeginTrans();
                DataTable dt = exec.GetQuery("GetAutoVehicleList", xmlDoc);
                //exec.CommitTrans();

                return dt;
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error in Vehicle List. " + xcp.Message, xcp);
            }

        }


        private void GetVehicleRentaPremium()
        {
            string VehicleRentalID = ddlRental.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("VehicleRentalID", SqlDbType.Int, 0, VehicleRentalID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetVehicleRentalByVehicleRentalID", xmlDoc);

                string VehicleRental = dt.Rows[0]["VehicleRentalPremium"].ToString();

                if (QA.Term.ToString().Trim() == "12")
                    txtVehicleRental.Text = VehicleRental.Trim();
                else
                {
                    if (QA.Term.ToString().Trim() != "")
                        txtVehicleRental.Text = (int.Parse(VehicleRental.Trim()) * Math.Round((double.Parse(QA.Term.ToString().Trim()) / 12), 0)).ToString();
                    else
                        txtVehicleRental.Text = VehicleRental.Trim();
                }
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error Get the Transportation Expenses." + xcp.Message, xcp);
            }
        }

        private void GetAccidentDeathPremium()
        {
            //string AccidentalDeathID = ddlAccidentDeath.SelectedItem.Value.Trim();
            //Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            //try
            //{
            //    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            //    DbRequestXmlCooker.AttachCookItem("AccidentalDeathID", SqlDbType.Int, 0, AccidentalDeathID.ToString(), ref cookItems);
            //    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

            //    DataTable dt = Executor.GetQuery("GetAccidentalDeathByID", xmlDoc);

            //    string AccidentDeathPremium = "0";
            //    if (dt.Rows.Count > 0)
            //        AccidentDeathPremium = dt.Rows[0]["AccidentalDeathPrima"].ToString();

            //    TxtAccidentDeathPremium.Text = (int.Parse(AccidentDeathPremium.Trim()) * int.Parse(ddlADPersons.SelectedItem.Text.Trim())).ToString();
            //}
            //catch (Exception xcp)
            //{
            //    Executor.RollBackTrans();
            //    throw new Exception("Error Get the Accidental Death." + xcp.Message, xcp);
            //}
        }

        private void GetUninsuredSinglePremium()
        {
            //string UninsuredSingleID = ddlUninsuredSingle.SelectedItem.Value.Trim();
            //Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            //try
            //{
            //    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            //    DbRequestXmlCooker.AttachCookItem("UninsuredSingleID", SqlDbType.Int, 0, UninsuredSingleID.ToString(), ref cookItems);
            //    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

            //    DataTable dt = Executor.GetQuery("GetUninsuredSingleByID", xmlDoc);

            //    string UninsuredSinglePremium = "";

            //    if (IsSecondAutoForApplyPremium("Single"))
            //        UninsuredSinglePremium = dt.Rows[0]["UninsuredSinglePrimaAuto2"].ToString();
            //    else
            //        UninsuredSinglePremium = dt.Rows[0]["UninsuredSinglePrimaAuto1"].ToString();

            //    TxtUninsuredSingle.Text = UninsuredSinglePremium.ToString().Trim();
            //}
            //catch (Exception xcp)
            //{
            //    Executor.RollBackTrans();
            //    throw new Exception("Error Get the Uninsured Single." + xcp.Message, xcp);
            //}
        }

        private void GetUninsuredSplitPremium()
        {
            //string UninsuredSplitID = ddlUninsuredSplit.SelectedItem.Value.Trim();
            //Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            //try
            //{
            //    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            //    DbRequestXmlCooker.AttachCookItem("UninsuredSplitID", SqlDbType.Int, 0, UninsuredSplitID.ToString(), ref cookItems);
            //    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

            //    DataTable dt = Executor.GetQuery("GetUninsuredSplitByID", xmlDoc);

            //    string UninsuredSplitPremium = "";

            //    if (IsSecondAutoForApplyPremium("Split"))
            //        UninsuredSplitPremium = dt.Rows[0]["UninsuredSplitPrimaAuto2"].ToString();
            //    else
            //        UninsuredSplitPremium = dt.Rows[0]["UninsuredSplitPrimaAuto1"].ToString();

            //    TxtUninsuredSplit.Text = UninsuredSplitPremium.ToString().Trim();
            //}
            //catch (Exception xcp)
            //{
            //    Executor.RollBackTrans();
            //    throw new Exception("Error Get the Uninsured Split." + xcp.Message, xcp);
            //}
        }

        private void GetEquipmentSoundPremium()
        {
            //string EquitmentSonidoID = ddlEquitmentSonido.SelectedItem.Value.Trim();
            //Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            //try
            //{
            //    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            //    DbRequestXmlCooker.AttachCookItem("EquitmentSonidoID", SqlDbType.Int, 0, EquitmentSonidoID.ToString(), ref cookItems);
            //    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

            //    DataTable dt = Executor.GetQuery("GetEquitmentSonidoByID", xmlDoc);

            //    string EquitmentSonido = dt.Rows[0]["EquitmentSonidoPrima"].ToString();
            //    TxtEquitmentSonido.Text = EquitmentSonido.Trim();
            //}
            //catch (Exception xcp)
            //{
            //    Executor.RollBackTrans();
            //    throw new Exception("Error Get the Equipment Sound." + xcp.Message, xcp);
            //}
        }

        private void GetEquipmentAudioPremium()
        {
            //string EquitmentAudioID = ddlEquitmentAudio.SelectedItem.Value.Trim();
            //Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            //try
            //{
            //    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            //    DbRequestXmlCooker.AttachCookItem("EquitmentAudioID", SqlDbType.Int, 0, EquitmentAudioID.ToString(), ref cookItems);
            //    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

            //    DataTable dt = Executor.GetQuery("GetEquitmentAudioByID", xmlDoc);

            //    string EquitmentAudio = dt.Rows[0]["EquitmentAudioPrima"].ToString();
            //    TxtEquitmentAudio.Text = EquitmentAudio.Trim();
            //}
            //catch (Exception xcp)
            //{
            //    Executor.RollBackTrans();
            //    throw new Exception("Error Get the Equipment Audio." + xcp.Message, xcp);
            //}
        }

        private void GetEquipmentTapesPremium()
        {
            //try
            //{
            //    if (chkEquipTapes.Checked)
            //        TxtEquitmentTapes.Text = "15";
            //    else
            //        TxtEquitmentTapes.Text = "0";
            //}
            //catch (Exception xcp)
            //{
            //    throw new Exception("Error Get the Equipment Tapes." + xcp.Message, xcp);
            //}
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
                if (QA.AutoCovers.Count >= 1)
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
        protected void ddlRental_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetVehicleRentaPremium();
        }

        protected void ddlAccidentDeath_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccidentDeathPremium();
        }
        protected void ddlADPersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAccidentDeathPremium();
        }
        protected void ddlUninsuredSingle_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUninsuredSinglePremium();
            SetEnableUninsured(true);
        }
        protected void ddlUninsuredSplit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUninsuredSplitPremium();
            SetEnableUninsured(true);
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
            if (ddlRoadAssistEmp.SelectedItem.Text.Trim() != "0")
            {
                ddlRoadAssist.SelectedIndex = -1;
                ddlRoadAssist.Enabled = false;
            }
            else
            {
               // ddlRoadAssist.Enabled = true;
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
        protected void chkLLG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLLG.Checked)
            {
                ddlLoanGap.Visible = true;
                //ddlLoanGap.SelectedIndex = ddlLoanGap.Items.IndexOf(ddlLoanGap.Items.FindByValue("1"));
                ddlLoanGap.SelectedIndex = 2;
            }
            else
            {
                ddlLoanGap.Visible = false;
                ddlLoanGap.SelectedIndex = -1;
            }
        }
        protected void TxtCustomizeEquipLimit_TextChanged(object sender, EventArgs e)
        {
            GetCustomizeEquipPremium(true);
        }

        private void GetCustomizeEquipPremium(bool message)
        {
        //    if (ddlCollision.SelectedItem.Text.Trim() != "" && ddlTerritory.SelectedItem.Text.Trim() != "" && TxtCustomizeEquipLimit.Text.Trim() != "")
        //    {

        //        decimal tempCollrate = 0;
        //        decimal tempTerrRate = 0;
        //        decimal tempLimit = 0;
        //        decimal totalcoll = 0;
        //        decimal totalcomp = 0;
        //        decimal tempComprate = 0;

        //        //Collision
        //        switch (int.Parse(ddlCollision.SelectedItem.Text.Trim()))
        //        {
        //            case 150:
        //                tempCollrate = 0.93M;
        //                break;
        //            case 200:
        //                tempCollrate = 0.83M;
        //                break;
        //            case 250:
        //                tempCollrate = 0.75M;
        //                break;
        //            case 500:
        //                tempCollrate = 0.54M;
        //                break;
        //            case 1000:
        //                tempCollrate = 0.42M;
        //                break;
        //            default:
        //                tempCollrate = 0;
        //                break;
        //        }

        //        switch (int.Parse(ddlTerritory.SelectedItem.Value.Trim()))
        //        {
        //            case 1:
        //                tempTerrRate = 5.75M;
        //                break;
        //            case 3:
        //                tempTerrRate = 5.18M;
        //                break;
        //            case 5:
        //                tempTerrRate = 5.73M;
        //                break;
        //            case 6:
        //                tempTerrRate = 4.64M;
        //                break;
        //            case 7:
        //                tempTerrRate = 4.50M;
        //                break;
        //            case 8:
        //                tempTerrRate = 5.16M;
        //                break;
        //            default:
        //                tempTerrRate = 0;
        //                break;
        //        }

        //        tempLimit = decimal.Parse(TxtCustomizeEquipLimit.Text.Trim());
        //        totalcoll = Math.Round(tempTerrRate * tempCollrate * 0.90M * 0.015M * tempLimit, 0);

        //        //Comprehensive
        //        //Collision
        //        switch (int.Parse(ddlComprehensive.SelectedItem.Text.Trim()))
        //        {
        //            case 200:
        //                tempComprate = 0.88M;
        //                break;
        //            case 250:
        //                tempComprate = 0.81M;
        //                break;
        //            case 500:
        //                tempComprate = 0.69M;
        //                break;
        //            case 1000:
        //                tempComprate = 0.63M;
        //                break;
        //            default:
        //                tempComprate = 0;
        //                break;
        //        }

        //        totalcomp = Math.Round((tempLimit * tempComprate) / 100, 0);

        //        TxtEquipColl.Text = (totalcoll).ToString().Trim();
        //        TxtEquipComp.Text = (totalcomp).ToString().Trim();
        //        TxtEquipTotal.Text = (totalcoll + totalcomp).ToString().Trim();
        //    }
        //    else
        //    {
        //        if (message)
        //        {
        //            TxtEquipColl.Text = "0";
        //            TxtEquipComp.Text = "0";
        //            TxtEquipTotal.Text = "0";

        //            lblRecHeader.Text = "Please select the Collision Deductibe, Territoty value and Customize Equipment Limit, for this cover";
        //            mpeSeleccion.Show();
        //        }
        //    }
        }


        protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomizeEquipPremium(false);
        }
        protected void ddlCollision_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetCustomizeEquipPremium(false);
            for (int i = 0; ddlComprehensive.Items.Count - 1 >= i; i++)
            {
                if (ddlComprehensive.Items[i].Value == ddlCollision.SelectedItem.Value)
                {
                    ddlComprehensive.SelectedIndex = i;
                    i = ddlComprehensive.Items.Count - 1;
                }
            }
            
        }

        protected void chkAssistEmp_CheckedChanged(object sender, EventArgs e)
        {
            GeRoadAssistPremium(true);
           
        }

        protected void chkAssist_CheckedChanged(object sender, EventArgs e)
        {

            ApplySecondPriceRoadAssistance();
        }
            
         

        private void GeRoadAssistPremium(bool IsEmp)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (IsSecondAutoForApplyPremium("Road"))
            {
                //if (chkAssist.Checked)
                //    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("2"));
                //else
                //    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("1"));
                
                if (QA.AutoCovers.Count >= 1)
                {
                    if (chkAssist.Checked)
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("3"));
                    else
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("1"));
                }
                else
                {
                    if (chkAssist.Checked)
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("2"));
                    else
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("1"));
                }

               // ApplySecondPriceRoadAssistance();
            }
            else
            {
                //if (chkAssistEmp.Checked)
                //    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("2"));
                //else
                //    ddlRoadAssistEmp.SelectedIndex = ddlRoadAssistEmp.Items.IndexOf(ddlRoadAssistEmp.Items.FindByValue("1"));

               // ApplySecondPriceRoadAssistance();

                if (QA.AutoCovers.Count > 1)
                {
                    if (chkAssist.Checked)
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("3"));
                    else
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("1"));
                }
                else
                {
                    if (chkAssist.Checked)
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("2"));
                    else
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("1"));

                }
               
            }
        }
        private void ApplySecondPriceRoadAssistance()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            EPolicy.Quotes.AutoCover ac = null;
            bool firstPrima = false;
            string Status = ViewState["Status"].ToString().Trim();
            int PageQuotesAutoId = int.Parse(ViewState["QuotesAutoId"].ToString().Trim());
            int PrimaQuotesAutoId = 0;
            

            for (int i = 0; i < QA.AutoCovers.Count; i++)
            {
                ac = (AutoCover)QA.AutoCovers[i];

                if (!ac.IsAssistanceEmp)
                {
                    if (ac.AssistanceID == 2)
                    {
                        firstPrima = true;
                        PrimaQuotesAutoId = ac.QuotesAutoId;
                    }

                }

            }
            if (chkAssist.Checked == true)
            {
               
                if (Status == "NEW")
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
                    //si es solo un vehiculo con 40$ de prima pues simpre seran $40
                    if (firstPrima && QA.AutoCovers.Count == 1)
                    {
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("2"));   
                    }
                    //si hay mas de un vehiculo y seleccionas el de $40 podras marcar y desmarcar sin afectar el precio
                    else if (firstPrima && QA.AutoCovers.Count > 1 && PrimaQuotesAutoId == PageQuotesAutoId)
                    {
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("2"));
                    }
                    //si hay mas de un vehiculo y ya existe una con $40 pues el proximo sera $25
                    else if (firstPrima && QA.AutoCovers.Count > 1)
                    {
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("3"));
                    }
                    //default 
                    else
                    {
                        ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("2"));
                    }
                }
            }
            else
            {
                if (ddlRoadAssist.SelectedItem.Value == "2")
                {
                    if (QA.AutoCovers.Count > 1 && Status != "NEW")
                    {
                        for (int i = 0; i < QA.AutoCovers.Count; i++)
                        {
                            ac = (AutoCover)QA.AutoCovers[i];

                            if (!ac.IsAssistanceEmp)
                            {
                                if (ac.AssistanceID == 3)
                                {
                                    ac.AssistanceID = 2;
                                    ac.AssistancePremium = 40;
                                    QA.AutoCovers[i] = ac;
                                    Session["TaskControl"] = QA;
                                    break;
                                }

                            }
                        }
                    }
                }
                ddlRoadAssist.SelectedIndex = ddlRoadAssist.Items.IndexOf(ddlRoadAssist.Items.FindByValue("0"));

                
            }

        }
        protected void chkLoJack_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableLojackFields();
        }

        protected void chkIsLeasing_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsLeasing.Checked == true)
            {
                //ddlBI.SelectedIndex = 7;
                // ddlPD.SelectedIndex = 6;
                for (int i = 0; i <= ddlBI.Items.Count - 1; i++)
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

        private void GetTowingPremium()
        {
            try
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                string TowingPrem = ddlTowing.SelectedItem.Value.Trim();

                if (QA.Term.ToString().Trim() == "12")
                    txtTowing.Text = TowingPrem.Trim();
                else
                {
                    if (QA.Term.ToString().Trim() != "")
                        txtTowing.Text = (int.Parse(TowingPrem.Trim()) * Math.Round((double.Parse(QA.Term.ToString().Trim()) / 12), 0)).ToString();
                    else
                        txtTowing.Text = TowingPrem.Trim();
                }
            }
            catch (Exception xcp)
            {
                throw new Exception("Error Get the Towing Premium." + xcp.Message, xcp);
            }
        }

        private string GetOriginalVehicleRentaPremium()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            string VehicleRentalID = ddlRental.SelectedItem.Value.Trim();
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("VehicleRentalID", SqlDbType.Int, 0, VehicleRentalID.ToString(), ref cookItems);
                XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                DataTable dt = Executor.GetQuery("GetVehicleRentalByVehicleRentalID", xmlDoc);

                string VehicleRental = dt.Rows[0]["VehicleRentalPremium"].ToString();

                if (QA.Term.ToString().Trim() == "12")
                    return VehicleRental.Trim();
                else
                {
                    if (QA.Term.ToString().Trim() != "")
                        return (int.Parse(VehicleRental.Trim()) * Math.Round((double.Parse(QA.Term.ToString().Trim()) / 12), 0)).ToString();
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


        protected void ddlExperienceDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExperienceDiscount.SelectedIndex > 0 && ddlExperienceDiscount.SelectedItem != null)
                TxtExpDisc.Text = ddlExperienceDiscount.SelectedItem.Value.Trim();
            else
                TxtExpDisc.Text = "0";
        }
        protected string getVehicleAge()
        {
            string year=DateTime.Now.Year.ToString();
            int age=0;
            if(ddlYear.SelectedItem.Text.Trim()=="")
            {
                return txtAge.Text = "";
            }
            else
            {
            age = Convert.ToInt32(year) - Convert.ToInt32(ddlYear.SelectedItem.Text);
            return age.ToString();
            }
        }

        protected void CalculateAVC()
        {
            int years = Convert.ToInt32(getVehicleAge());
            double cost;

            if (years == 0)
            {
                txtActualValue.Text = txtCost.Text;
            }
            else
            {
                if (txtCost.Text.Trim() != "")
                {
                    cost = Convert.ToDouble(txtCost.Text);
                    for (int i = 1; i <= years; i++)
                    {
                        cost = cost * 0.85;
                    }
                    //txtActualValue.Text = cost.ToString();
                    txtActualValue.Text = Math.Round(cost).ToString();
                }
            }
        }

        protected void txtAge_TextChanged(object sender, EventArgs e)
        {
            
        }

        //protected void ddlYear_TextChanged(object sender, EventArgs e)
        //{
        //    getVehicleAge();
        //}
        protected void ddlCollision_TextChanged(object sender, EventArgs e)
        {
            //for (int i = 0; ddlComprehensive.Items.Count - 1 >= i; i++)
            //{
            //    if (ddlComprehensive.Items[i].Value == ddlCollision.SelectedItem.Value)
            //    {
            //        ddlComprehensive.SelectedIndex = i;
            //        i = ddlComprehensive.Items.Count - 1;
            //    }
            //}
        }
        protected void ddlComprehensive_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; ddlCollision.Items.Count - 1 >= i; i++)
            {
                if (ddlCollision.Items[i].Value == ddlComprehensive.SelectedItem.Value)
                {
                    ddlCollision.SelectedIndex = i;
                    i = ddlCollision.Items.Count - 1;
                }
            }
        }
        protected void txtCost_TextChanged(object sender, EventArgs e)
        {
            //this.txtActualValue.Attributes.Add("onblur", "SetActualValueFromCost();");  //onblur
            CalculateAVC();
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
            { ddlDriver.SelectedIndex = ddlDriver.Items.IndexOf(ddlDriver.Items.FindByValue(mejorCandidata[1])); }

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

        //private void AssignedDriver(TaskControl.QuoteAuto QA, Quotes.AutoCover ACC)
        //{
        //    //SetPrimaryDriver(QA.QuoteId);
        //    AutoCover srch = new AutoCover();
        //    srch.QuotesAutoId = ACC.QuotesAutoId;
        //    srch.InternalID = ACC.InternalID;
        //    AutoCover AC = QA.GetAutoCover(srch);

        //    int InternalID = 0;
        //    AutoDriver AD = new AutoDriver();

        //    if (QA.Drivers != null && QA.Drivers.Count > 0)
        //    {
        //        for (int i = 0; i < QA.Drivers.Count; i++)
        //        {
        //            AutoDriver Driver = (AutoDriver)QA.Drivers[i];
        //            if (Driver.DriverID == int.Parse(this.ddlDriver.SelectedItem.Value))
        //            {
        //                InternalID = (int)Driver.InternalID;
        //                i = QA.Drivers.Count;
        //            }
        //        }
        //    }

        //    AD.InternalID = InternalID;

        //    Quotes.AssignedDriver AsDrv = new Quotes.AssignedDriver();
        //    AsDrv.AutoDriver = AD;


        //    ///Si hubo cambio en el assign driver elimina el record que habia y actualiza el nuevo driver asignado.
        //    ArrayList assdr = Quotes.AssignedDriver.LoadDriversForAutoCover(ACC.QuotesAutoId, QA.Drivers, false);
        //    if (assdr.Count != 0)
        //    {
        //        EPolicy.TaskControl.QuoteAuto q = new EPolicy.TaskControl.QuoteAuto(false);
        //        q.Drivers = assdr;

        //        Quotes.AssignedDriver driver = (Quotes.AssignedDriver)q.Drivers[0];

        //        if (driver.AutoDriver.DriverID != int.Parse(this.ddlDriver.SelectedItem.Value) && this.ddlDriver.SelectedItem.Text.Trim() != "")
        //        {
        //            Quotes.AssignedDriver AsDrvRemove = new Quotes.AssignedDriver();
        //            AsDrvRemove.AutoDriver = driver.AutoDriver;

        //            AC.RemoveAssignedDriver(AsDrvRemove);
        //        }
        //    }

        //    if (!AC.AssignedDrivers.Contains(AsDrv))
        //    {
        //        AsDrv.AutoDriver = QA.GetDriver(AD);
        //        AsDrv.AutoCoverID = ACC.QuotesAutoId;

        //        if (rdoPrincipalOperatorY.Checked)
        //        {
        //            AsDrv.PrincipalOperator = true;
        //        }

        //        if (rdoPrincipalOperatorN.Checked)
        //        {
        //            AsDrv.PrincipalOperator = false;
        //        }

        //        if (rdoOnlyOperatorY.Checked)
        //        {
        //            AsDrv.OnlyOperator = true;
        //        }

        //        if (rdoOnlyOperatorN.Checked)
        //        {
        //            AsDrv.OnlyOperator = false;
        //        }

        //        AsDrv.Mode = (int)Enumerators.Modes.Insert;

        //        AC.AssignedDrivers.Add(AsDrv);
        //    }
        //    else
        //    {
        //        //////
        //        EPolicy.TaskControl.QuoteAuto qTemp = new EPolicy.TaskControl.QuoteAuto(false);
        //        qTemp.Drivers = assdr;
        //        Quotes.AssignedDriver driverUpdate = (Quotes.AssignedDriver)qTemp.Drivers[0];
        //        AsDrv = driverUpdate;
        //        //////

        //        //AsDrv.OnlyOperator = chkOnlyOperator.Checked;
        //        if (rdoOnlyOperatorY.Checked)
        //        {
        //            AsDrv.OnlyOperator = true;
        //        }

        //        if (rdoOnlyOperatorN.Checked)
        //        {
        //            AsDrv.OnlyOperator = false;
        //        }

        //        //AsDrv.PrincipalOperator = ChkPrincipalOperator.Checked;
        //        if (rdoPrincipalOperatorY.Checked)
        //        {
        //            AsDrv.PrincipalOperator = true;
        //        }

        //        if (rdoPrincipalOperatorN.Checked)
        //        {
        //            AsDrv.PrincipalOperator = false;
        //        }

        //        AC.RemoveAssignedDriver(AsDrv);
        //        AC.AssignedDrivers.Add(AsDrv);

        //        //AsDrv.UpdateAssignedDriverByAssignedDriver(AsDrv.AssignedDriverID);

        //        AsDrv = AC.GetAssignedDriver(AsDrv);
        //        if (AsDrv.Mode == (int)Enumerators.Modes.Delete)
        //            AsDrv.Mode = (int)Enumerators.Modes.Nothing;
        //    }

        //    Login.Login cp = HttpContext.Current.User as Login.Login;
        //    int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
        //    QA.Save(UserID, AC, null, false);
        //    //QA.SaveAutoCover(UserID, AC, null, false);
        //    Session["TaskControl"] = QA;
        //}
}
}