using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPolicy.Customer;
using EPolicy.TaskControl;
using EPolicy2.Reports;
using Baldrich.DBRequest;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using EPolicy.Quotes;
using EPolicy.XmlCooker;
using EPolicy.Audit;
using System.Xml;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace EPolicy
{
    /// <summary>
    /// Summary description for QuoteAuto.
    /// </summary>
    public partial class QuoteAuto : System.Web.UI.Page
    {
        #region Variable Definition
        protected System.Web.UI.HtmlControls.HtmlForm ProspectBusiness;
        private DataTable DtEndorsement;
        #endregion

        private string PolicyNumber = "", InsuredName = "", InsuredAddress1 = "", InsuredAddress2 = "";
        private string InsuredCity = "", InsuredState = "", InsuredZip = "", InsuredPlate = "", InsuredVin = "";
        private string InsuredWorkPhone = "", InsuredCellular = "", PolicyPrefix = "", ReinsAsl = "";
        private string ClientID = "", NAMECONVENTION = "";
        private int VehicleMake = 0, VehicleModel = 0;
        private string EffDate = "", ExpDate = "", RenewalNo = "", RenewalPremium = "", Current_XML = "", ClientIDPPS = "", CustomerNumberPPS = "";
        private bool RenewalDriverAdded = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Accordion6.Visible = false;
            AccordionEndorsement.Visible = false;
            //DataGridGroup.Visible = false;
            btnReinstatement.Visible = false;
            btnCancellation.Visible = false;

            //litPopUp.Visible = true;
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
                if (!cp.IsInRole("AUTO PERSONAL POLICY") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
                    Response.Redirect("Default.aspx?001");
                }
            }

            if (!Page.IsPostBack)
            {
                if (Session["LookUpTables"] == null)
                {
                    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    if (QA.IsPolicy)
                    {
                        DataTable dtInsuranceCompany = LookupTables.LookupTables.GetTable("InsuranceCompany");
                        DataTable dtAgency = LookupTables.LookupTables.GetTable("Agency");
                        DataTable dtAgent = EPolicy.LookupTables.LookupTables.GetTable("Agent");
                        DataTable dtDept = LookupTables.LookupTables.GetTable("Department");
                        DataTable dtPaymentType = LookupTables.LookupTables.GetTable("PolicyPaymentType");
                        DataTable dtMaritalStatus = LookupTables.LookupTables.GetTable("MaritalStatus");
                        DataTable dtGender = LookupTables.LookupTables.GetTable("Gender");
                        DataTable dtLocation = LookupTables.LookupTables.GetTable("Location");

                        DataTable dtFBPosition = LookupTables.LookupTables.GetTable("FBPosition");
                        DataTable dtFBSubsidiary = LookupTables.LookupTables.GetTable("FBSubsidiary");
                        DataTable dtFBBranches = LookupTables.LookupTables.GetTable("FBBranches");

                        //InsuranceCompany
                        ddlInsuranceCompany.DataSource = dtInsuranceCompany;
                        ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
                        ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
                        ddlInsuranceCompany.DataBind();
                        ddlInsuranceCompany.SelectedIndex = -1;
                        ddlInsuranceCompany.Items.Insert(0, "");

                        //Agency
                        ddlAgency.DataSource = dtAgency;
                        ddlAgency.DataTextField = "AgencyDesc";
                        ddlAgency.DataValueField = "AgencyID";
                        ddlAgency.DataBind();
                        ddlAgency.SelectedIndex = -1;
                        ddlAgency.Items.Insert(0, "");

                        for (int i = 0; ddlAgency.Items.Count - 1 >= i; i++)
                        {
                            if (ddlAgency.Items[i].Text.Trim() == "MIDOCEAN INSURANCE AGENCY")
                            {
                                ddlAgency.SelectedIndex = i;
                                i = ddlAgency.Items.Count - 1;
                            }
                        }

                        //Agent
                        ddlAgent.DataSource = dtAgent;
                        ddlAgent.DataTextField = "AgentDesc";
                        ddlAgent.DataValueField = "AgentID";
                        ddlAgent.DataBind();
                        ddlAgent.SelectedIndex = -1;
                        ddlAgent.Items.Insert(0, "");

                        //Department
                        ddlDept.DataSource = dtDept;
                        ddlDept.DataTextField = "DepartmentDesc";
                        ddlDept.DataValueField = "DepartmentID";
                        ddlDept.DataBind();
                        ddlDept.SelectedIndex = -1;
                        ddlDept.Items.Insert(0, "");

                        //PolicyPaymentType
                        ddlPaymentType.DataSource = dtPaymentType;
                        ddlPaymentType.DataTextField = "PolicyPaymentTypeDesc";
                        ddlPaymentType.DataValueField = "PolicyPaymentTypeID";
                        ddlPaymentType.DataBind();
                        ddlPaymentType.SelectedIndex = -1;
                        ddlPaymentType.Items.Insert(0, "");

                        //MaritalStatus
                        ddlMaritalSt.DataSource = dtMaritalStatus;
                        ddlMaritalSt.DataTextField = "MaritalStatusDesc";
                        ddlMaritalSt.DataValueField = "MaritalStatusID";
                        ddlMaritalSt.DataBind();
                        ddlMaritalSt.SelectedIndex = -1;
                        ddlMaritalSt.Items.Insert(0, "");

                        //Gender
                        ddlGender.DataSource = dtGender;
                        ddlGender.DataTextField = "GenderDesc";
                        ddlGender.DataValueField = "GenderID";
                        ddlGender.DataBind();
                        ddlGender.SelectedIndex = -1;
                        ddlGender.Items.Insert(0, "");


                        //Location
                        ddlLocation.DataSource = dtLocation;
                        ddlLocation.DataTextField = "locationDesc";
                        ddlLocation.DataValueField = "locationID";
                        ddlLocation.DataBind();
                        ddlLocation.SelectedIndex = -1;
                        ddlLocation.Items.Insert(0, "");

                        //FBPosition
                        ddlFBPosition.DataSource = dtFBPosition;
                        ddlFBPosition.DataTextField = "FBPositionDesc";
                        ddlFBPosition.DataValueField = "FBPositionID";
                        ddlFBPosition.DataBind();
                        ddlFBPosition.SelectedIndex = -1;
                        ddlFBPosition.Items.Insert(0, "");

                        //FBSubsidiary
                        ddFBSubsidiary.DataSource = dtFBSubsidiary;
                        ddFBSubsidiary.DataTextField = "FBSubsidiaryDesc";
                        ddFBSubsidiary.DataValueField = "FBSubsidiaryID";
                        ddFBSubsidiary.DataBind();
                        ddFBSubsidiary.SelectedIndex = -1;
                        ddFBSubsidiary.Items.Insert(0, "");

                        //FBBranches
                        ddlFBBranches.DataSource = dtFBBranches;
                        ddlFBBranches.DataTextField = "FBBranchesDesc";
                        ddlFBBranches.DataValueField = "FBBranchesID";
                        ddlFBBranches.DataBind();
                        ddlFBBranches.SelectedIndex = -1;
                        ddlFBBranches.Items.Insert(0, "");


                        if (cp.UserID == 1)
                        {
                            BtnSendPPS.Visible = true;
                        }

                        //FirstFillDllAgentList();

                        Session.Add("LookUpTables", "In");
                    }
                }


                ClearFields();
                if (Session["Prospect"] != null) // it's a NEW Quote
                {

                    Customer.Prospect prs = (Prospect)Session["Prospect"];
                    DisplayProspect(prs);

                    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    this.SetDefaultFieldValues();
                    DisplayQuote(QA);
                    SetAtribute(QA);
                    //isNewQuote = true;
                }
                else if (((TaskControl.TaskControl)Session["TaskControl"]).TaskControlID == 0)
                {
                    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    //if (QA.Policy.PolicyType.Trim() == "29")
                    //{ QA.Policy.PolicyType = "PAP";}
                    
                    this.SetDefaultFieldValues();
                    DisplayQuote(QA);
                    SetExpirationDate(QA);
                    SetAtribute(QA);
                }
                else if (Session["TaskControl"] != null && ((TaskControl.TaskControl)Session["TaskControl"]).TaskControlID != 0)
                // It's an OLD Quote
                {
                    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                    DisplayQuote(QA);
                    SetAtribute(QA);
                    //(QA.Prospect);

                    //isNewQuote = false;
                }
                else
                {
                    //Se comentó para poder entrar la información del prospecto desde la pantalla inicial del Quote.
                    //Response.Redirect("SearchProspect.aspx");
                }

                //txtEntryDt.Text = DateTime.Today.ToString("M/d/yyyy");
                //txtEntryDt.Text = DateTime.Today.AddYears(1).ToString("M/d/yyyy");

                if (this.IsNewQuote())
                {
                    this.SetControlState((int)States.NEW);
                }
                else
                {
                    this.SetControlState((int)States.READONLY);
                }
                // :~*

                this.Calculate(true);

                if (txtPolicyNo.Text != "")
                {
                    btnEdit.Visible = false;
                    btnVehicles.Visible = false;
                    BtnDrivers.Visible = false;
                }
            }

            if (cp.UserID == 1)
            {
                BtnSendPPS.Visible = true;
            }
        }

        private void SetAtribute(TaskControl.QuoteAuto QA)
        {
            try
            {
                if (QA.Policy.Ren_Rei != "REI")
                {
                    txtCharge.Attributes.Add("onblur", "addCharges()");
                    txtPremium.Attributes.Add("onblur", "addCharges()");
                    txtTerm.Attributes.Add("onblur", "getExpDt()");
                    txtEffDt.Attributes.Add("onblur", "getExpDt()");
                    //imgEffDt.Attributes.Add("onBlur", "getExpDt()");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region VerifyAccess
        private void VerifyAccess(int State)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            Login.Login cp = HttpContext.Current.User as Login.Login;

            if (!cp.IsInRole("PLANDIFERIDO") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnDefferredPayPlan.Visible = false;
                this.chkDeferred.Visible = false;
            }
            else
            {
                this.btnDefferredPayPlan.Visible = false;
                this.chkDeferred.Visible = false;
            }

            if (!cp.IsInRole("AUTO PERSONAL RENEW") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnRenew.Visible = false;
            }
            else
            {
                if (State != (int)States.READWRITE && State != 0)
                    this.btnRenew.Visible = true;
                else
                    this.btnRenew.Visible = false;
            }

            if (!cp.IsInRole("AUTO PERSONAL REINSTATEMENT") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnReinstatement.Visible = false;
            }
            else
            {
                if (State != (int)States.READWRITE && State != 0)
                    this.btnReinstatement.Visible = false;//debe estar en true 
                else
                    this.btnReinstatement.Visible = false;
            }

            if (!cp.IsInRole("AUTO PERSONAL MODIFY POLICY") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnEdit.Visible = false;
            }
            else
            {
                if (State != (int)States.READWRITE && State != 0)
                    this.btnEdit.Visible = false;
                else
                    this.btnEdit.Visible = false;
            }
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            //  Benny:	this.InitializeDriverObjectSessionVariable();

            Page.Header.DataBind();
            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);
            //this.Placeholder1.Controls.Add(Banner);
            //this.BindDdls();
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        private void SetExpirationDate(TaskControl.QuoteAuto QA)
        {
            if (QA.EffectiveDate.Trim() != "")
            {
                if (QA.Mode == 1)
                {
                    if (TxtMasterCode.Text == "9999" || int.Parse(txtTerm.Text.Trim()) <= 12)  //Master Cabrera Universal DI
                    {
                        txtEffDt.Text = QA.EffectiveDate;
                        txtExpDt.Text = QA.ExpirationDate;
                    }
                    else
                    {
                        if (int.Parse(txtTerm.Text.Trim()) == 12)
                        {
                            txtExpDt.Text = DateTime.Parse(this.txtEffDt.Text).AddMonths(12).ToShortDateString();
                        }
                    }
                }
                else
                {
                    txtEffDt.Text = QA.EffectiveDate;
                    txtExpDt.Text = QA.ExpirationDate;
                }
            }
        }

        private void SetDefaultFieldValues()
        {
            //this.txtEffDt.Text = DateTime.Today.ToShortDateString();
            this.txtTerm.Text = "12";
            if (this.txtEffDt.Text.Trim() != string.Empty && this.txtTerm.Text.Trim() != string.Empty)
                this.txtExpDt.Text = DateTime.Parse(this.txtEffDt.Text).AddMonths(int.Parse(this.txtTerm.Text.Trim())).ToShortDateString();
        }

        private void BindDdls()
        {
            //	this.BindDdl(this.ddlAgency, "Agency", "AgencyID", "AgencyDesc");
            //	this.BindDdl(this.ddlAgent, "Agent", "AgentID", "AgentDesc");
            //	this.BindDdl(this.ddlDepartment, "Department", "DepartmentID", "DepartmentDesc");
        }
        //		private void BindDdl(
        //			System.Web.UI.WebControls.DropDownList DropDownList,
        //			string TableName, string ValueFieldName, 
        //			string TextFieldName)
        //		{
        //			DataTable dtResults;
        //
        //			try
        //			{
        //				dtResults =	LookupTables.LookupTables.GetTable(TableName);
        //
        //				if(dtResults != null && dtResults.Rows.Count > 0)
        //				{
        //					DropDownList.DataSource = dtResults;
        //					DropDownList.DataValueField = ValueFieldName;
        //					DropDownList.DataTextField = TextFieldName;
        //					DropDownList.DataBind();
        //					DropDownList.SelectedIndex = -1;
        //					DropDownList.Items.Insert(0,"");
        //				}
        //			}
        //			catch(Exception e)
        //			{
        //				string a=e.Message;
        //			}
        //		}

        //RPR 2004-03-19
        private string ValidateThis()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            ArrayList errorMessages = new ArrayList();
            //char padChar = ' ';

            if (txtLastNm1.Text == "")
                errorMessages.Add("First last name is missing." + "\r\n");
            if (TxtAddress1.Text == "")
                errorMessages.Add("Address 1 is missing." + "\r\n");
            if (TxtAddress2.Text == "")
                errorMessages.Add("Address 2 is missing." + "\r\n");
            if (TxtCity.Text == "")
                errorMessages.Add("City is missing." + "\r\n");
            if (TxtZipCode.Text == "")
                errorMessages.Add("Zip Code is missing." + "\r\n");
            //Effective date
            if (this.txtEffDt.Text == "")
            {
                errorMessages.Add("Effective date is missing." + "\r\n");
            }
            else if (!this.IsSamsValidDate(this.txtEffDt.Text))
            {
                errorMessages.Add("Effective date is invalid.  The " +
                    "correct format is \"mm/dd/yyyy\".");
            }
            else if (DateTime.Parse(this.txtEffDt.Text) > DateTime.Today)
            {
                //errorMessages.Add("Effective date cannot be prospective." + "\r\n");
            }

            //if (this.txtEffDt.Text != "")
            //{
            //    if (DateTime.Parse(this.txtEffDt.Text) < DateTime.Today)
            //    {
            //        DateTime date = DateTime.Parse(this.txtEffDt.Text).AddDays(5);
            //        if (date < DateTime.Today)
            //        {
            //            if (!cp.IsInRole("AUTOPOLICIESEFFECTIVEDATERETRO") && !cp.IsInRole("ADMINISTRATOR"))
            //            {
            //                errorMessages.Add("Effective date cannot be older than 5 days retroactive." + "\r\n");
            //            }
            //        }
            //    }
            //}

            //Term
            if (this.txtTerm.Text == "")
            {
                errorMessages.Add("Term is missing." + "\r\n");
            }
            else if (this.txtTerm.Text == "0")
            {
                errorMessages.Add("Term must be greater than zero." + "\r\n");
            }

            if (QA.IsPolicy)  // Verifica cuando es una póliza.
            {
                //OriginatedAt
                //if (this.ddlLocation.SelectedItem.Text.Trim() == "")
                //{
                //    errorMessages.Add("Originated At is missing." + "\r\n");
                //}

                if (!this.ChkAutoAssignPolicy.Checked)
                {
                    if (this.txtPolicyType.Text.Trim() == "")
                    {
                        errorMessages.Add("The policy Type is missing." + "\r\n");
                    }

                    if (this.txtPolicyNo.Text.Trim() == "")
                    {
                        errorMessages.Add("The policy number is missing." + "\r\n");
                    }

                    if (this.txtSuffix.Text.Trim() == "")
                    {
                        errorMessages.Add("The suffix is missing." + "\r\n");
                    }
                }
                else
                {
                    if (this.txtPolicyType.Text.Trim() == "")
                    {
                        errorMessages.Add("The policy Type is missing." + "\r\n");
                    }
                }

                //Verifica si falta el VIN o el Purchase Date en los vehiculos.
                Quotes.AutoCover AC2 = null;
                if (QA.AutoCovers.Count != 0)
                {
                    for (int i = 0; i < QA.AutoCovers.Count; i++)
                    {
                        Quotes.AutoCover ACPolicy = new Quotes.AutoCover();
                        AC2 = (AutoCover)QA.AutoCovers[i];

                        //if (QA.Policy.IsMaster)
                        //{
                        //    LookupTables.Bank bank = new LookupTables.Bank();
                        //    bank = bank.GetBank(AC2.Bank.Trim());

                        //    if (bank.MasterPolicyID.Trim() == "")
                        //    {
                        //        errorMessages.Add("This Bank have not Master Number configured." + "\r\n");
                        //    }
                        //}      

                        if (AC2.VIN.Trim() == "")
                        {
                            string make = LookupTables.LookupTables.GetDescription("VehicleMake", AC2.VehicleMake.ToString());
                            string model = LookupTables.LookupTables.GetDescription("VehicleModel", AC2.VehicleModel.ToString());
                            errorMessages.Add("The VIN is empty for the vehicle " + make + " " + model + ".\r\n");
                        }

                        if (AC2.PurchaseDate.Trim() == "")
                        {
                            string make = LookupTables.LookupTables.GetDescription("VehicleMake", AC2.VehicleMake.ToString());
                            string model = LookupTables.LookupTables.GetDescription("VehicleModel", AC2.VehicleModel.ToString());
                            errorMessages.Add("The Purchaser Date is empty for the vehicle " + make + " " + model + ".\r\n");
                        }
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

                lblRecHeader.Text = popUpString;
                mpeSeleccion.Show();
            }
            return popUpString;
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

        private void VerifyPolicyNumber()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            //Solo verfica el num. de póliza si es asignado automaticamente.
            if (QA.Policy.AutoAssignPolicy)
            {
                //Verificación para los master
                if (QA.Policy.IsMaster)
                {
                    if (QA.Policy.MasterPolicyID == "0047") // Si es master de Guaranty no tiene certificado.
                    {
                        if (QA.Policy.PolicyNo.Trim() == "")
                        {
                            QA.Policy.AssignPolicyNo();
                            QA.Policy.SaveOnlyPolicy(UserID);
                        }
                    }
                    else
                    {
                        if (QA.Policy.Certificate.Trim() == "")
                        {
                            QA.Policy.AssignPolicyNo();
                            QA.Policy.SaveOnlyPolicy(UserID);
                        }
                    }
                }
                else // Verificacin para las polizas no master
                {
                    if (QA.Policy.PolicyNo.Trim() == "")
                    {
                        QA.Policy.AssignPolicyNo();
                        QA.Policy.SaveOnlyPolicy(UserID);
                    }
                }
                Session["TaskControl"] = QA;
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

        private int GetBasePolicySubClassFromPolicySubClassID(int PolicySubClassID)
        {
            DbRequestXmlCookRequestItem[] items = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("ID", SqlDbType.Int, 0,
                PolicySubClassID.ToString(), ref items);
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetBasePolicySubClassFromPolicySubClassID",
                DbRequestXmlCooker.Cook(items)).Rows.Count > 0 ?
                int.Parse(exec.GetQuery("GetBasePolicySubClassFromPolicySubClassID",
                DbRequestXmlCooker.Cook(items)).Rows[0]["AutoPolicyBaseSubClassID"].ToString().Trim()) : 0;
        }

        private void SaveQuoteData()
        {
            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                Session.Remove("AUTOCHARGE");
                Session.Add("AUTOCHARGE", QA.Charge.ToString());

                decimal charge = QA.Charge;
                QA.Save(UserID);
                QA.Charge = charge;
                QA.Policy.Charge = double.Parse(charge.ToString());
                Session["TaskControl"] = QA;
            }
        }

        private void LoadFromForm()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
             Login.Login cp = HttpContext.Current.User as Login.Login;
             int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);


           

            //			QA.QuoteId = ;
            //			QA.TaskControlID = ;
            //			QA.PolicyClassID = ;

            //			if (QA.Mode == 1)
            //			{
            if (txtEffDt.Text.Trim() != "")
            {
                if (QA.ExpirationDate.Trim() == string.Empty) // && this.TxtTerm.Text.Trim() != string.Empty)
                {
                    if (this.txtTerm.Text.Trim() == string.Empty)
                    {
                        this.txtTerm.Text = "0";
                    }
                    QA.ExpirationDate = DateTime.Parse(this.txtEffDt.Text).AddMonths(int.Parse(this.txtTerm.Text.Trim())).ToShortDateString();
                    this.txtExpDt.Text = QA.ExpirationDate;
                }
                else
                {
                    if (this.txtExpDt.Text.Trim() != string.Empty)
                    {
                        //QA.ExpirationDate = this.txtExpDt.Text.Trim();
                        if (QA.Policy.Ren_Rei != "REI")
                            QA.ExpirationDate = DateTime.Parse(this.txtEffDt.Text).AddMonths(int.Parse(this.txtTerm.Text.Trim())).ToShortDateString();
                    }
                }

                if (txtTerm.Text.Trim() != "")
                    QA.Term = int.Parse(txtTerm.Text);

                QA.EffectiveDate = txtEffDt.Text;
                //QA.ExpirationDate = DateTime.Parse(this.txtEffDt.Text).AddMonths(12).ToShortDateString();
            }

            //QA.EntryDate = DateTime.Parse(txtEntryDt.Text.Trim()+" 12:01:00 AM");
            //QA.EntryDate = DateTime.Parse(date);



            QA.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();



            if (txtCharge.Text.Trim() != "")
                QA.Charge = decimal.Parse(txtCharge.Text, System.Globalization.NumberStyles.Currency);
            if (txtPremium.Text.Trim() != string.Empty)
                QA.TotalPremium = decimal.Parse(txtPremium.Text, System.Globalization.NumberStyles.Currency);

            txtTtlPremium.Text = String.Format("{0:c}", Math.Round(QA.TotalPremium, 0) + QA.Charge);
            QA.ConvertToPolicyDate = null;

            if (QA.IsPolicy)
            {
                QA.Policy.IsDeferred = chkDeferred.Checked;

                QA.Policy.AutoAssignPolicy = ChkAutoAssignPolicy.Checked;
                QA.Policy.IsMaster = ChkIsmaster.Checked;
                QA.Policy.PolicyType = txtPolicyType.Text.Trim().ToUpper();
                QA.Policy.PolicyNo = txtPolicyNo.Text.Trim();
                QA.Policy.Certificate = txtCertificate.Text.Trim();
                QA.Policy.Suffix = txtSuffix.Text.Trim();
                QA.Policy.LoanNo = txtLoanNo.Text.Trim();
                //QA.Policy.PMT1	    = decimal.Parse(TxtPMT1.Text.Trim());
                QA.Policy.InvoiceNumber = txtInvoiceNo.Text.Trim();
                QA.Policy.FileNumber = txtFileNumber.Text.Trim();
                //QA.Policy.LbxAgentSelected = ddlSelectedAgent;
                QA.Policy.IsFamily = chkFBfamilia.Checked;
                QA.Policy.IsEmployee = chkFBEmployee.Checked;
                QA.Policy.EmployeeName = TxtFBEmployeeName.Text.Trim().ToUpper();


                


                if (QA.Policy.IsMaster)
                {
                    QA.Policy.MasterPolicyID = TxtMasterCode.Text.Trim();
                    //QA.Policy.MasterPolicyID = GetMasterPolicyID(QA);
                }
                else
                    QA.Policy.MasterPolicyID = "";


                if (ddFBSubsidiary.SelectedIndex > 0 && ddFBSubsidiary.SelectedItem != null)
                    QA.Policy.FBSubsidiaryID = int.Parse(ddFBSubsidiary.SelectedItem.Value.ToString());
                else
                    QA.Policy.FBSubsidiaryID = 0;

                if (ddlFBPosition.SelectedIndex > 0 && ddlFBPosition.SelectedItem != null)
                    QA.Policy.FBPositionID = int.Parse(ddlFBPosition.SelectedItem.Value.ToString());
                else
                    QA.Policy.FBPositionID = 0;

                if (ddlFBBranches.SelectedIndex > 0 && ddlFBBranches.SelectedItem != null)
                    QA.Policy.FBBranchesID = int.Parse(ddlFBBranches.SelectedItem.Value.ToString());
                else
                    QA.Policy.FBBranchesID = 0;

                //if (ddlAgent.Items.Count > 0)
                //    for (int i = 0; ddlAgent.Items.Count - 1 >= i; i++)
                //    {
                //        if (ddlAgent.Items[i].Text.Trim() == "MIDOCEAN INSURANCE AGENCY") //ASEGURATEC MIDOCEAN INSURANCE AGENCY
                //        {
                //            ddlAgent.SelectedIndex = i;
                //            i = ddlAgent.Items.Count - 1;//continue;
                //        }
                //    }

                //Agent
                if (ddlAgent.SelectedIndex > 0 && ddlAgent.SelectedItem != null)
                {
                    QA.Agent = ddlAgent.SelectedItem.Value;
                    QA.Policy.Agent = ddlAgent.SelectedItem.Value;
                }
                else
                {
                    QA.Agent = "000";
                    QA.Policy.Agent = "000";
                }
                //if (ddlAgency.Items.Count > 0)
                //{
                //    for (int i = 0; ddlAgency.Items.Count - 1 >= i; i++)
                //    {
                //        if (ddlAgency.Items[i].Text.Trim() == "MidOcean Insurance Agency") //ASEGURATEC MIDOCEAN INSURANCE AGENCY
                //        {
                //            ddlAgency.SelectedIndex = i;
                //            i = ddlAgency.Items.Count - 1;//continue;
                //        }
                //    }
                //}

                if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
                {
                    QA.Policy.Agency = ddlAgency.SelectedItem.Value.ToString();
                    QA.Agency = ddlAgency.SelectedItem.Value.ToString();
                }
                if (ddlInsuranceCompany.SelectedIndex > 0 && ddlInsuranceCompany.SelectedItem != null)
                {
                    QA.Policy.InsuranceCompany = ddlInsuranceCompany.SelectedItem.Value.ToString();
                    QA.InsuranceCompany = ddlInsuranceCompany.SelectedItem.Value.ToString();
                }
                if (ddlDept.SelectedIndex > 0 && ddlDept.SelectedItem != null)
                {
                    QA.Policy.DepartmentID = int.Parse(ddlDept.SelectedItem.Value.ToString());
                }
                if (ddlLocation.SelectedIndex > 0 && ddlLocation.SelectedItem != null)
                {
                    QA.Policy.OriginatedAt = int.Parse(ddlLocation.SelectedItem.Value.ToString());
                }
                if (ddlPaymentType.SelectedIndex > 0 && ddlPaymentType.SelectedItem != null)
                    QA.Policy.PaymentType = int.Parse(ddlPaymentType.SelectedItem.Value.ToString());
            }

            Session["TaskControl"] = QA;
        }

        private string GetMasterPolicyID(TaskControl.QuoteAuto QA)
        {
            string result = "";
            if (QA.Policy.IsMaster)
            {
                result = "0004"; //FirstBank
            }
            //{
            //    Quotes.AutoCover AC2 = null;
            //    if (QA.AutoCovers.Count != 0)
            //    {
            //        for (int i = 0; i < QA.AutoCovers.Count; i++)
            //        {
            //            Quotes.AutoCover ACPolicy = new Quotes.AutoCover();
            //            AC2 = (AutoCover)QA.AutoCovers[i];

            //            LookupTables.Bank bank = new LookupTables.Bank();
            //            bank = bank.GetBank(AC2.Bank.Trim());

            //            if (bank.MasterPolicyID.Trim() != "")
            //            {
            //                result =  bank.MasterPolicyID;
            //            }
            //        }
            //    }
            //}
            return result;
        }

        private void DisplayProspect(Prospect prs)
        {
            if (prs != null)
            {
                txtFirstNm.Text = prs.FirstName;
                //	txtInitial.Text = prs.Initial;
                txtLastNm1.Text = prs.LastName1;
                txtLastNm2.Text = prs.LastName2;
                txtHomePhone.Text = prs.HomePhone;
                txtWorkPhone.Text = prs.WorkPhone;
                txtCellular.Text = prs.Cellular;
            }
        }

        private void DisplayQuote(TaskControl.QuoteAuto QA)
        {
            if (QA != null)
            {
                if (QA.TaskControlID != 0 && !QA.IsPolicy)
                {
                    txtTaskControlID.Text = QA.TaskControlID.ToString();
                    // lblTaskControlID.Visible = true;
                }
                else
                {
                    txtTaskControlID.Text = QA.TaskControlID.ToString();
                    //txtTaskControlID.Text = "";
                    txtTaskControlID.Visible = true;
                    //lblTaskControlID.Visible = true;
                }

                if (QA.EffectiveDate != string.Empty)
                {
                    txtEffDt.Text = QA.EffectiveDate;
                    //txtEffDt.Text = QA.Policy.CancellationDate.ToString().Trim();
                }

                if (QA.Term != 0)
                    txtTerm.Text = QA.Term.ToString();
                if (QA.ExpirationDate != string.Empty)
                    txtExpDt.Text = QA.ExpirationDate;
                txtEntryDt.Text = QA.EntryDate.ToString("d");

                if (QA.Policy.Ren_Rei == "REI")
                {
                    txtCharge.Text = String.Format("{0:c}", Math.Round(QA.Charge, 0));
                    txtPremium.Text = String.Format("{0:c}", Math.Round(QA.Policy.Rein_Amt, 0));
                    txtTtlPremium.Text = String.Format("{0:c}", Math.Round(QA.Policy.Rein_Amt, 0)); //+ Math.Round(double.Parse(QA.Charge.ToString()), 0));

                }
                else
                {
                    txtCharge.Text = String.Format("{0:c}", Math.Round(QA.Charge, 0));
                    //txtTtlPremium.Text = String.Format("{0:c}", Math.Round(QA.TotalPremium, 0) + Math.Round(QA.Charge, 0));
                    //txtPremium.Text = String.Format("{0:c}", Math.Round(QA.TotalPremium, 0));FF
                    txtTtlPremium.Text = String.Format("{0:c}", Math.Round(QA.TotalPremium, 0));
                    txtPremium.Text = String.Format("{0:c}", Math.Round(QA.TotalPremium, 0) - Math.Round(QA.Charge, 0));
                }

                LblTaskType.Text = "Auto Quote";
                lblA.Text = "Prospect Info";
                LblQuoteInformation.Text = "Quote Information";

                if (QA.IsPolicy)
                {
                    //Insurance Company
                    ddlInsuranceCompany.SelectedIndex = 0;
                    if (QA.InsuranceCompany != "000")
                    {
                        for (int i = 0; ddlInsuranceCompany.Items.Count - 1 >= i; i++)
                        {
                            if (ddlInsuranceCompany.Items[i].Value == QA.InsuranceCompany.ToString())
                            {
                                ddlInsuranceCompany.SelectedIndex = i;
                                i = ddlInsuranceCompany.Items.Count - 1;
                            }
                        }
                    }

                    //Agency
                    ddlAgency.SelectedIndex = 0;
                    if (QA.Agency != "000")
                    {
                        for (int i = 0; ddlAgency.Items.Count - 1 >= i; i++)
                        {
                            if (ddlAgency.Items[i].Value == QA.Agency.ToString())
                            {
                                ddlAgency.SelectedIndex = i;
                                i = ddlAgency.Items.Count - 1;
                            }
                        }
                    }

                    //Agent
                    if (QA.Agent.Trim() == "001")
                    {
                        for (int i = 0; ddlAgent.Items.Count - 1 >= i; i++)
                        {
                            if (ddlAgent.Items[i].Text.Trim() == "MIDOCEAN INSURANCE AGENCY") //ASEGURATEC MIDOCEAN INSURANCE AGENCY
                            {
                                ddlAgent.SelectedIndex = i;
                                i = ddlAgent.Items.Count - 1;
                                //QA.Agent = ddlAgent.SelectedItem.Value;
                                //QA.Policy.Agent = ddlAgent.SelectedItem.Value;
                            }
                        }
                    }
                    else
                    {

                        for (int i = 0; ddlAgent.Items.Count - 1 >= i; i++)
                        {
                            if (ddlAgent.Items[i].Value.ToString().Trim()== QA.Agent.ToString().Trim()) //ASEGURATEC MIDOCEAN INSURANCE AGENCY
                            {
                                ddlAgent.SelectedIndex = i;
                                i = ddlAgent.Items.Count - 1;
                                //QA.Agent = ddlAgent.SelectedItem.Value;
                                //QA.Policy.Agent = ddlAgent.SelectedItem.Value;
                            }
                        }
                    }
                    
                    //if (QA.Agent != "000") //"000" 
                    //{
                    //    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                    //        ddlAgent.Items.FindByValue(QA.Agent.Trim()));
                    //}

                    //Department
                    ddlDept.SelectedIndex = 0;
                    if (QA.Policy.DepartmentID != 0)
                    {
                        for (int i = 0; ddlDept.Items.Count - 1 >= i; i++)
                        {
                            if (ddlDept.Items[i].Value == QA.Policy.DepartmentID.ToString())
                            {
                                ddlDept.SelectedIndex = i;
                                i = ddlDept.Items.Count - 1;
                            }
                        }
                    }

                    //OriginatedAt
                    ddlLocation.SelectedIndex = 0;
                    if (QA.Policy.OriginatedAt != 0)
                    {
                        for (int i = 0; ddlLocation.Items.Count - 1 >= i; i++)
                        {
                            if (ddlLocation.Items[i].Value == QA.Policy.OriginatedAt.ToString())
                            {
                                ddlLocation.SelectedIndex = i;
                                i = ddlLocation.Items.Count - 1;
                            }
                        }
                    }

                    //PolicyPaymentType
                    ddlPaymentType.SelectedIndex = 0;
                    if (QA.Policy.PaymentType != 0)
                    {
                        for (int i = 0; ddlPaymentType.Items.Count - 1 >= i; i++)
                        {
                            if (ddlPaymentType.Items[i].Value == QA.Policy.PaymentType.ToString())
                            {
                                ddlPaymentType.SelectedIndex = i;
                                i = ddlPaymentType.Items.Count - 1;
                            }
                        }
                    }

                    //MaritalStatus
                    ddlMaritalSt.SelectedIndex = 0;
                    if (QA.Customer.MaritalStatus != 0)
                    {
                        for (int i = 0; ddlMaritalSt.Items.Count - 1 >= i; i++)
                        {
                            if (ddlMaritalSt.Items[i].Value == QA.Customer.MaritalStatus.ToString())
                            {
                                ddlMaritalSt.SelectedIndex = i;
                                i = ddlMaritalSt.Items.Count - 1;
                            }
                        }
                    }

                    int mSex = 0;
                    if (QA.Customer.Sex != "")
                    {
                        if (QA.Customer.Sex == "M")
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

                    //Gender
                    //ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(mSex.ToString()));
                    ddlGender.SelectedIndex = 0;
                    if (mSex != 0)
                    {
                        for (int i = 0; ddlGender.Items.Count - 1 >= i; i++)
                        {
                            if (ddlGender.Items[i].Value == mSex.ToString())
                            {
                                ddlGender.SelectedIndex = i;
                                i = ddlGender.Items.Count - 1;
                            }
                        }
                    }

                    //FBPosition
                    ddlFBPosition.SelectedIndex = 0;
                    if (QA.Policy.FBPositionID != 0)
                    {
                        for (int i = 0; ddlFBPosition.Items.Count - 1 >= i; i++)
                        {
                            if (ddlFBPosition.Items[i].Value == QA.Policy.FBPositionID.ToString())
                            {
                                ddlFBPosition.SelectedIndex = i;
                                i = ddlFBPosition.Items.Count - 1;
                            }
                        }
                    }

                    //FBSubsidiary
                    ddFBSubsidiary.SelectedIndex = 0;
                    if (QA.Policy.FBSubsidiaryID != 0)
                    {
                        for (int i = 0; ddFBSubsidiary.Items.Count - 1 >= i; i++)
                        {
                            if (ddFBSubsidiary.Items[i].Value == QA.Policy.FBSubsidiaryID.ToString())
                            {
                                ddFBSubsidiary.SelectedIndex = i;
                                i = ddFBSubsidiary.Items.Count - 1;
                            }
                        }
                    }

                    //FBBranches
                    ddlFBBranches.SelectedIndex = 0;
                    if (QA.Policy.FBBranchesID != 0)
                    {
                        for (int i = 0; ddlFBBranches.Items.Count - 1 >= i; i++)
                        {
                            if (ddlFBBranches.Items[i].Value == QA.Policy.FBBranchesID.ToString())
                            {
                                ddlFBBranches.SelectedIndex = i;
                                i = ddlFBBranches.Items.Count - 1;
                            }
                        }
                    }

                    LblTaskType.Text = "Auto Policy";
                    lblA.Text = "Customer Info";
                    LblQuoteInformation.Text = "Policy Information";
                    LblStatus.Text = QA.Policy.Status;

                    chkDeferred.Checked = QA.Policy.IsDeferred;
                    ChkAutoAssignPolicy.Checked = QA.Policy.AutoAssignPolicy;
                    ChkIsmaster.Checked = QA.Policy.IsMaster;
                    txtPolicyType.Text = QA.Policy.PolicyType.Trim().ToUpper();
                    TxtMasterCode.Text = QA.Policy.MasterPolicyID.Trim().ToUpper();
                    txtPolicyNo.Text = QA.Policy.PolicyNo.Trim();
                    txtCertificate.Text = QA.Policy.Certificate.Trim();
                    txtSuffix.Text = QA.Policy.Suffix.Trim();
                    txtLoanNo.Text = QA.Policy.LoanNo.Trim();
                    //TxtPMT1.Text		= QA.Policy.PMT1.ToString("###,###.00").Trim();
                    txtInvoiceNo.Text = QA.Policy.InvoiceNumber.Trim();
                    txtFileNumber.Text = QA.Policy.FileNumber.Trim();

                    chkFBfamilia.Checked = QA.Policy.IsFamily;
                    chkFBEmployee.Checked = QA.Policy.IsEmployee;
                    TxtFBEmployeeName.Text = QA.Policy.EmployeeName.Trim();

                    VerifyAssignPolicyFields();
                    //VerifyIsDeferred();
                    //VerifyIsFamily();
                    //VerifyIsEmployee();
                    //VerifyBranches();

                    //Customer Information
                    LblCustomerNo.Text = QA.Customer.CustomerNo.Trim();
                    txtFileNo.Text = QA.Customer.FileNo.Trim();
                    txtFirstNm.Text = QA.Customer.FirstName.Trim();
                    txtLastNm1.Text = QA.Customer.LastName1.Trim();
                    txtLastNm2.Text = QA.Customer.LastName2.Trim();
                    txtHomePhone.Text = QA.Customer.HomePhone;
                    txtWorkPhone.Text = QA.Customer.JobPhone;
                    txtCellular.Text = QA.Customer.Cellular;
                    TxtAddress1.Text = QA.Customer.Address1.Trim();
                    TxtAddress2.Text = QA.Customer.Address2.Trim();
                    TxtCity.Text = QA.Customer.City.Trim();
                    TxtZipCode.Text = QA.Customer.ZipCode.Trim();
                    txtSocSec.Text = QA.Customer.SocialSecurity.Trim();
                    txtLicense.Text = QA.Customer.Licence.Trim();
                    //txtBirthDt.Text = QA.Customer.Birthday;

                    txtBirthDt.Text = String.Format("{0:d}", DateTime.Parse(QA.Customer.Birthday));


                        

                    

                    FillDataGrid();
                }
            }
        }
        private void ClearFields()
        {
            //txtQuoteID.Text = "";
            //lblQuoteID.Visible = false;
            txtTaskControlID.Text = "";
            //lblTaskControlID.Visible = false;
            txtEffDt.Text = "";
            txtTerm.Text = "";
            txtExpDt.Text = "";
            txtEntryDt.Text = DateTime.Now.ToShortDateString(); //DateTime.Today.ToString("d");
            txtCharge.Text = "";
            txtTtlPremium.Text = "";
            txtPremium.Text = "";
        }

        /*private void cvEffDt_ServerValidate(
            object source, 
            System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string str = txtEffDt.Text;
            if (str != "")
            {
                DateTime dt = DateTime.Parse(str);
                DateTime now = DateTime.Now;
                TimeSpan ts = dt - now;
                if ( ts.Days < 0 || ts.Days > 5)
                {
                    args.IsValid = false;
                    if (litPopUp.Text == "")
                    {
                        litPopUp.Text = 
                            Utilities.MakeLiteralPopUpString(
                            cvEffDt.ErrorMessage);
                    }
                    else 
                    {
                        litPopUp.Text = 
                            Utilities.MakeLiteralPopUpString(
                            cvEffDt.ErrorMessage + @"\n" + cvTerm.ErrorMessage);
                    }
                    litPopUp.Visible = true;
                }
            }
        }*/

        /*private void cvTerm_ServerValidate(
            object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string str = txtTerm.Text;
            if (str != "")
            {
                if (int.Parse(str) <= 0)
                {
                    args.IsValid=false;
                    if (litPopUp.Text == "")
                    {
                        litPopUp.Text = 
                            Utilities.MakeLiteralPopUpString(cvTerm.ErrorMessage);
                    }
                    else
                    {
                        litPopUp.Text = 
                            Utilities.MakeLiteralPopUpString(
                            cvEffDt.ErrorMessage + @"\n" + cvTerm.ErrorMessage);
                    }
                    litPopUp.Visible = true;
                }
            }
        }*/

        private void Calculate(bool Silent)
        {
            LoadFromForm();
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            QA.Calculate();

            /*RPR 2004-03-16
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int UserID = 
                int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            QA.Save(UserID);
            Session["TaskControl"] = QA;
            //:~*/

            Session["TaskControl"] = QA;

            string effdate = "";
            if (QA.EffectiveDate.Trim() == "")
                effdate = "10/01/2013";
            else
                effdate = QA.EffectiveDate.Trim();

            if (QA.EntryDate >= DateTime.Parse("10/01/2013") && DateTime.Parse(effdate) >= DateTime.Parse("10/01/2013"))
            {
                QA.Charge = CalculateCharge();
                QA.Policy.Charge = double.Parse(QA.Charge.ToString());
                UpdateDerramaAuto(QA);
            }
            else
            {
                QA.Charge = 0;
                txtCharge.Text = "0";
            }

            if (QA.Policy.Ren_Rei == "REI")
            {
                txtPremium.Text = String.Format("{0:c}",
                    (int)QA.Policy.Rein_Amt);
                txtCharge.Text = String.Format("{0:c}",
                    Math.Round(QA.Charge));
                txtTtlPremium.Text = String.Format("{0:c}",
                    ((int)QA.Policy.Rein_Amt) +
                    Math.Round(QA.Charge, 0));
            }
            else
            {

                txtPremium.Text = String.Format("{0:c}",
                    (int)QA.TotalPremium);
                txtCharge.Text = String.Format("{0:c}",
                    Math.Round(QA.Charge));
                txtTtlPremium.Text = String.Format("{0:c}",
                    ((int)QA.TotalPremium) +
                    Math.Round(QA.Charge, 0));
            }
            //Session["TaskControl"] = QA;
            Session.Remove("TaskControl");
            Session.Add("TaskControl", QA);

            /* RPR 2004-05-28
            txtPremium.Text = String.Format("{0:c}", 
                Math.Round(QA.TotalPremium, 0));
            txtCharge.Text = String.Format("{0:c}", 
                Math.Round(QA.Charge));
            txtTtlPremium.Text = String.Format("{0:c}", 
                Math.Round(QA.TotalPremium, 0) + 
                Math.Round(QA.Charge, 0));*/

            if (!Silent)
            {
                lblRecHeader.Text = "Quote was calculated.";
                mpeSeleccion.Show();
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
                Executor.Update("UpdateDerramaAutoPolicy", xmlDoc);
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save Charge. " + xcp.Message, xcp);
            }
        }

        private decimal CalculateCharge()
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
                decimal charge2 = 0;
                try
                {
                    dt = exec.GetQuery("GetChargeNew", xmlDoc);
                    if (dt.Rows.Count > 0)
                    {
                        double charge = ((double)dt.Rows[0]["ChargePercent"]) / 100;
                        double totcharge = Math.Round((double.Parse(QA.TotalPremium.ToString()) * charge), 0);
                        txtCharge.Text = totcharge.ToString("###,###.00");
                        QA.Charge = decimal.Parse(totcharge.ToString());
                        charge2 = QA.Charge;
                    }
                    else
                    {
                        txtCharge.Text = "0.0";
                    }
                    return charge2;
                }
                catch (Exception ex)
                {
                    throw new Exception("No pudo encontrar la información, Por favor trate de nuevo.", ex);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool IsNewQuote()
        {
            TaskControl.QuoteAuto quoteAuto =
                this.GetQuoteObjectFromSession();

            if (quoteAuto != null)
            {
                if (quoteAuto.QuoteId == 0)
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
                return true;
            }
        }

        private bool IsDriversEmpty()
        {
            TaskControl.QuoteAuto quoteAuto =
                this.GetQuoteObjectFromSession();
            if (quoteAuto != null)
            {
                if (quoteAuto.Drivers != null &&
                    quoteAuto.Drivers.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        private bool IsVehiclesEmpty()
        {
            TaskControl.QuoteAuto quoteAuto =
                this.GetQuoteObjectFromSession();
            if (quoteAuto != null)
            {
                if (quoteAuto.AutoCovers != null &&
                    quoteAuto.AutoCovers.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void ShowPolicyFields(TaskControl.QuoteAuto QA, int State)
        {
            if (QA.IsPolicy)
            {
                this.LblAddr1.Visible = true;
                this.LblAddr2.Visible = true;
                this.LblCity.Visible = true;
                //this.LblZip.Visible			= true;
                this.lblPolicyNo.Visible = true;
                this.lblPolicyType.Visible = true;
                this.lblCertificate.Visible = true;
                this.lblSuffix.Visible = true;
                this.lblLoanNo.Visible = true;
                this.lblInvoiceNo.Visible = true;
                this.lblFileNumber.Visible = true;
                this.lblDept.Visible = false;
                this.lblPaymentType.Visible = false;
                this.lblInsCo.Visible = true;
                this.lblAgency.Visible = true;
                this.LblSelectAgent.Visible = false;
                this.LblStatus.Visible = true;
                this.LblStatus1.Visible = true;
                this.lblGender.Visible = true;
                this.lblMaritalSt.Visible = true;
                // this.LblSogSec.Visible = true;
                this.LblLic.Visible = true;
                this.LblBirth.Visible = true;
                //this.imgEffDt.Visible = true;
                this.btnCalculate.Visible = true;

                this.TxtAddress1.Visible = true;
                this.TxtAddress2.Visible = true;
                this.TxtCity.Visible = true;
                this.TxtZipCode.Visible = true;
                this.txtPolicyType.Visible = true;
                this.TxtMasterCode.Visible = false;
                this.txtPolicyNo.Visible = true;
                this.txtCertificate.Visible = true;
                this.txtSuffix.Visible = true;
                this.txtLoanNo.Visible = true;
                this.TxtPMT1.Visible = false;
                this.txtInvoiceNo.Visible = true;
                this.txtFileNumber.Visible = true;
                //this.txtSocSec.Visible = true;
                this.txtLicense.Visible = true;
                this.txtBirthDt.Visible = true;
                this.txtFileNo.Visible = true;

                this.ddlDept.Visible = false;
                this.ddlPaymentType.Visible = false;
                this.ddlInsuranceCompany.Visible = true;
                this.ddlAgency.Visible = true;
                this.ddlAgent.Visible = true;
                //this.ddlLocation.Visible = true;
                this.ddlGender.Visible = true;
                this.ddlMaritalSt.Visible = true;

                //this.btnNext.Visible   = true;
                //this.imgEffDt.Visible = true;
                this.btnCalculate.Visible = true;

                if (QA.PolicyType == "MFC" || QA.PolicyType == "MFE")
                {
                    this.chkFBfamilia.Visible = true;
                    this.chkFBEmployee.Visible = true;
                    this.chkDeferred.Visible = true;
                    this.ChkAutoAssignPolicy.Visible = true;
                    this.ChkIsmaster.Visible = false;
                    this.HplAdd.Visible = false;
                }
                else //Otros como Scot y Lafitte
                {
                    this.chkFBfamilia.Visible = false;
                    this.TxtFBEmployeeName.Visible = false;
                    this.chkFBEmployee.Visible = false;
                    this.ddlFBPosition.Visible = false;
                    this.ddFBSubsidiary.Visible = false;
                    this.ddlFBBranches.Visible = false;
                }

                if (QA.Policy.IsFamily)
                {
                    this.TxtFBEmployeeName.Visible = true;
                }
                else
                {
                    this.TxtFBEmployeeName.Visible = false;
                }

                if (QA.Policy.IsEmployee)
                {
                    this.ddlFBPosition.Visible = true;
                    this.ddFBSubsidiary.Visible = true;
                }
                else
                {
                    this.ddlFBPosition.Visible = false;
                    this.ddFBSubsidiary.Visible = false;
                }

                if (QA.Policy.FBSubsidiaryID == 5)
                    this.ddlFBBranches.Visible = true;
                else
                    this.ddlFBBranches.Visible = false;

                LblCustomerNo.Visible = true;

                LblTaskType.Text = "Auto Policy";
                lblA.Text = "Customer Info";
                LblQuoteInformation.Text = "Policy Information";

                DataGridGroup.Visible = true;//true
            }
            else
            {
                this.LblAddr1.Visible = false;
                this.LblAddr2.Visible = false;
                this.LblCity.Visible = false;
                //this.LblZip.Visible			= false;
                this.lblPolicyNo.Visible = false;
                this.lblPolicyType.Visible = false;
                this.lblCertificate.Visible = false;
                this.lblSuffix.Visible = false;
                this.lblLoanNo.Visible = false;
                this.lblInvoiceNo.Visible = false;
                this.lblFileNumber.Visible = false;
                this.lblDept.Visible = false;
                this.lblPaymentType.Visible = false;
                this.lblInsCo.Visible = false;
                this.lblAgency.Visible = false;
                this.LblSelectAgent.Visible = false;
                this.LblStatus.Visible = false;
                this.LblStatus1.Visible = false;
                this.lblGender.Visible = false;
                this.lblMaritalSt.Visible = false;
                this.LblSogSec.Visible = false;
                this.LblLic.Visible = false;
                this.LblBirth.Visible = false;
                //this.imgEffDt.Visible = false;
                this.btnCalculate.Visible = false;

                this.TxtAddress1.Visible = false;
                this.TxtAddress2.Visible = false;
                this.TxtCity.Visible = false;
                this.TxtZipCode.Visible = false;
                this.txtPolicyType.Visible = false;
                this.TxtMasterCode.Visible = false;
                this.txtPolicyNo.Visible = false;
                this.txtCertificate.Visible = false;
                this.txtSuffix.Visible = false;
                this.txtLoanNo.Visible = false;
                this.TxtPMT1.Visible = false;
                this.txtInvoiceNo.Visible = false;
                this.txtFileNumber.Visible = false;
                this.txtSocSec.Visible = false;
                this.txtLicense.Visible = false;
                this.txtBirthDt.Visible = false;
                this.txtFileNo.Visible = false;

                this.ddlDept.Visible = false;
                this.ddlPaymentType.Visible = false;
                this.ddlInsuranceCompany.Visible = false;
                this.ddlAgency.Visible = false;
                this.ddlAgent.Visible = false;
                //this.ddlLocation.Visible = false;

                this.ddlGender.Visible = false;
                this.ddlMaritalSt.Visible = false;

                //this.btnNext.Visible   = false;
                //this.imgEffDt.Visible = false;
                this.btnCalculate.Visible = false;

                this.chkDeferred.Visible = false;
                this.ChkAutoAssignPolicy.Visible = false;
                this.ChkIsmaster.Visible = false;
                this.HplAdd.Visible = false;

                this.chkFBfamilia.Visible = false;
                this.TxtFBEmployeeName.Visible = false;
                this.chkFBEmployee.Visible = false;
                this.ddlFBPosition.Visible = false;
                this.ddFBSubsidiary.Visible = false;
                this.ddlFBBranches.Visible = false;

                LblCustomerNo.Visible = false;

                LblTaskType.Text = "Auto Quote";
                lblA.Text = "Prospect Info";
                LblQuoteInformation.Text = "Quote Information";
                DataGridGroup.Visible = false;//false
            }

            if (QA.IsPolicy)
            {
                switch (State)
                {
                    case (int)States.NEW:
                        this.TxtAddress1.Enabled = false;
                        this.TxtAddress2.Enabled = false;
                        this.TxtCity.Enabled = false;
                        this.TxtZipCode.Enabled = false;
                        this.txtPolicyType.Enabled = true;
                        this.TxtMasterCode.Enabled = true;
                        //						this.txtPolicyNo.Enabled = true;
                        //						this.txtCertificate.Enabled = true;
                        //						this.txtSuffix.Enabled = true;
                        this.txtLoanNo.Enabled = true;
                        this.TxtPMT1.Enabled = true;
                        this.txtInvoiceNo.Enabled = true;
                        this.txtFileNumber.Enabled = true;
                        this.txtSocSec.Enabled = false;
                        this.txtLicense.Enabled = false;
                        this.txtBirthDt.Enabled = false;
                        this.txtFileNo.Enabled = false;

                        this.ddlDept.Enabled = true;
                        this.ddlPaymentType.Enabled = true;
                        this.ddlInsuranceCompany.Enabled = true;
                        this.ddlAgency.Enabled = true;
                        this.ddlAgent.Enabled = true;
                      //  this.ddlLocation.Enabled = true;
                        this.ddlGender.Enabled = false;
                        this.ddlMaritalSt.Enabled = false;

                        this.chkFBfamilia.Enabled = true;
                        this.chkFBEmployee.Enabled = true;
                        this.chkDeferred.Enabled = true;
                        this.ChkAutoAssignPolicy.Enabled = true;
                        this.ChkIsmaster.Enabled = true;

                        //this.btnNext.Visible = true;
                        this.HplAdd.Enabled = true;
                        //this.imgEffDt.Visible = true;
                        this.btnCalculate.Visible = true;

                        if (QA.Policy.PolicyCicleEnd == 0)
                        {
                            this.btnIncentive.Visible = false;
                            this.btnPayments.Visible = false;
                            this.btnPrintPolicy.Visible = false;
                            this.btnPrintInvoice.Visible = false;
                            this.btnEndor.Visible = false;
                            this.btnCancellation.Visible = false;
                            this.btnEdit.Visible = false;
                            this.btnRenew.Visible = false;
                            this.btnEdit.Visible = false;
                            this.btnSave.Visible = false;
                            this.btnCancel.Visible = false;
                            //this.imgEffDt.Visible = false;
                            this.btnCalculate.Visible = false;
                            this.btnPrint.Visible = false;
                            this.BtnDrivers.Visible = true;//false
                            this.btnVehicles.Visible = true;//false
                            this.btnAuditTrail.Visible = false;

                            //this.btnNext.Visible = true;
                        }
                        else
                        {
                            this.btnIncentive.Visible = false;
                            this.btnPayments.Visible = false;
                            this.btnPrintPolicy.Visible = false;
                            this.btnPrintInvoice.Visible = false;
                            this.btnRenew.Visible = false;
                            this.btnEndor.Visible = false;
                            this.btnCancellation.Visible = false;
                            this.btnEdit.Visible = false;
                            this.btnSave.Visible = true;
                            this.btnCancel.Visible = true;
                            //this.imgEffDt.Visible = true;
                            this.btnCalculate.Visible = true;
                            this.btnPrint.Visible = false;
                            this.BtnDrivers.Visible = true;
                            this.btnVehicles.Visible = true;
                            this.btnAuditTrail.Visible = false;
                            //this.btnNext.Visible = false;
                        }

                        if (QA.Policy.IsFamily)
                        {
                            this.TxtFBEmployeeName.Enabled = true;
                        }
                        else
                        {
                            this.TxtFBEmployeeName.Enabled = false;
                        }

                        if (QA.Policy.IsEmployee)
                        {
                            this.ddlFBPosition.Enabled = true;
                            this.ddFBSubsidiary.Enabled = true;
                        }
                        else
                        {
                            this.ddlFBPosition.Enabled = false;
                            this.ddFBSubsidiary.Enabled = false;
                        }

                        if (QA.Policy.FBSubsidiaryID == 5)
                            this.ddlFBBranches.Enabled = true;
                        else
                            this.ddlFBBranches.Enabled = false;
                        break;

                    case (int)States.READONLY:
                        this.TxtAddress1.Enabled = false;
                        this.TxtAddress2.Enabled = false;
                        this.TxtCity.Enabled = false;
                        this.TxtZipCode.Enabled = false;
                        this.txtPolicyType.Enabled = false;
                        this.TxtMasterCode.Enabled = false;
                        this.txtPolicyNo.Enabled = false;
                        this.txtCertificate.Enabled = false;
                        this.txtSuffix.Enabled = false;
                        this.txtLoanNo.Enabled = false;
                        this.TxtPMT1.Enabled = false;
                        this.txtInvoiceNo.Enabled = false;
                        this.txtFileNumber.Enabled = false;
                        this.txtSocSec.Enabled = false;
                        this.txtLicense.Enabled = false;
                        this.txtBirthDt.Enabled = false;
                        this.txtFileNo.Enabled = false;

                        this.ddlDept.Enabled = false;
                        this.ddlPaymentType.Enabled = false;
                        this.ddlInsuranceCompany.Enabled = false;
                        this.ddlAgency.Enabled = false;
                        this.ddlAgent.Enabled = false;
                       // this.ddlLocation.Enabled = false;
                        this.ddlGender.Enabled = false;
                        this.ddlMaritalSt.Enabled = false;

                        this.chkFBfamilia.Enabled = false;
                        this.chkFBEmployee.Enabled = false;
                        this.chkDeferred.Enabled = false;
                        this.ChkAutoAssignPolicy.Enabled = false;
                        this.ChkIsmaster.Enabled = false;

                        //this.btnNext.Visible = false;
                        this.HplAdd.Enabled = false;
                        //this.imgEffDt.Visible = false;
                        this.btnCalculate.Visible = false;
                        this.btnIncentive.Visible = false;
                        this.btnPayments.Visible = false;
                        this.btnPrintPolicy.Visible = true;
                        this.btnPrintInvoice.Visible = true;
                        this.btnRenew.Visible = true;
                        this.btnEndor.Visible = false;//this.btnEndor.Visible = true;
                        this.btnCancellation.Visible = false; //debe estar en true
                        this.btnEdit.Visible = false;//true
                        this.btnSave.Visible = false;
                        this.btnCancel.Visible = false;
                        //this.imgEffDt.Visible = false;
                        this.btnPrint.Visible = false;
                        this.BtnDrivers.Visible = false;//true
                        this.btnVehicles.Visible = false;//true
                        this.btnAuditTrail.Visible = true;

                        this.TxtFBEmployeeName.Enabled = false;
                        this.ddlFBPosition.Enabled = false;
                        this.ddFBSubsidiary.Enabled = false;
                        this.ddlFBBranches.Enabled = false;
                        break;

                    case (int)States.READWRITE:
                        this.TxtAddress1.Enabled = false;
                        this.TxtAddress2.Enabled = false;
                        this.TxtCity.Enabled = false;
                        this.TxtZipCode.Enabled = false;
                        this.txtPolicyType.Enabled = true;
                        this.TxtMasterCode.Enabled = true;
                        this.txtLoanNo.Enabled = true;
                        this.TxtPMT1.Enabled = true;
                        this.txtInvoiceNo.Enabled = true;
                        this.txtFileNumber.Enabled = true;
                        this.txtSocSec.Enabled = false;
                        this.txtLicense.Enabled = false;
                        this.txtBirthDt.Enabled = false;
                        this.txtFileNo.Enabled = false;

                        this.ddlDept.Enabled = true;
                        this.ddlPaymentType.Enabled = true;
                        this.ddlInsuranceCompany.Enabled = true;
                        this.ddlAgency.Enabled = true;
                        this.ddlAgent.Enabled = true;
                       // this.ddlLocation.Enabled = true;
                        this.ddlGender.Enabled = false;
                        this.ddlMaritalSt.Enabled = false;

                        this.chkFBfamilia.Enabled = true;
                        this.chkFBEmployee.Enabled = true;
                        this.chkDeferred.Enabled = true;
                        this.ChkAutoAssignPolicy.Enabled = true;
                        this.ChkIsmaster.Enabled = true;

                        //this.btnNext.Visible = true;
                        this.HplAdd.Enabled = true;
                        //this.imgEffDt.Visible = true;
                        this.btnCalculate.Visible = true;

                        this.btnIncentive.Visible = false;
                        this.btnPayments.Visible = false;
                        this.btnPrintPolicy.Visible = false;
                        this.btnPrintInvoice.Visible = false;
                        this.btnRenew.Visible = false;
                        this.btnEndor.Visible = false;
                        this.btnCancellation.Visible = false;
                        this.btnEdit.Visible = false;
                        this.btnSave.Visible = true;
                        this.btnCancel.Visible = true;
                        //this.imgEffDt.Visible = true;
                        this.btnPrint.Visible = false;
                        this.BtnDrivers.Visible = false;
                        this.btnVehicles.Visible = false;
                        this.btnAuditTrail.Visible = false;
                        //this.btnNext.Visible = false;
                        if (QA.Policy.IsFamily)
                        {
                            this.TxtFBEmployeeName.Enabled = true;
                        }
                        else
                        {
                            this.TxtFBEmployeeName.Enabled = false;
                        }

                        if (QA.Policy.IsEmployee)
                        {
                            this.ddlFBPosition.Enabled = true;
                            this.ddFBSubsidiary.Enabled = true;
                        }
                        else
                        {
                            this.ddlFBPosition.Enabled = false;
                            this.ddFBSubsidiary.Enabled = false;
                        }

                        if (QA.Policy.FBSubsidiaryID == 5)
                            this.ddlFBBranches.Enabled = true;
                        else
                            this.ddlFBBranches.Enabled = false;
                        break;
                    default:
                        //
                        break;
                }
            }
        }

        private void SetControlState(int State)
        {
            //Para visualizar los campos y labels de la pantalla

            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            if (QA.Mode == 1)
            {
                ViewState.Add("Status", "NEW");
                State = (int)States.NEW;  //New
            }

            switch (State)
            {
                case (int)States.NEW:
                    this.txtEffDt.Enabled = true;
                    //this.imgEffDt.Visible = true;
                    this.btnCalculate.Visible = true;
                    this.txtTerm.Enabled = true;
                    this.btnEdit.Visible = false;
                    this.btnSave.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnDefferredPayPlan.Enabled = false;
                    this.btnRenew.Visible = false;
                    this.lblPrintOption.Visible = false;
                    this.chkAgency.Visible = false;
                    this.chkAsegurado.Visible = false;
                    this.chkCompany.Visible = false;
                    this.chkProductor.Visible = false;
                    this.chkLossPaye.Visible = false;
                    break;
                case (int)States.READONLY:
                    this.txtEffDt.Enabled = false;
                    //this.imgEffDt.Visible = false;
                    this.btnCalculate.Visible = false;
                    this.txtTerm.Enabled = false;
                   // this.btnEdit.Visible = true;
                    this.btnSave.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnRenew.Visible = true;
                    this.lblPrintOption.Visible = true;
                    this.chkAgency.Visible = true;
                    this.chkAsegurado.Visible = true;
                    this.chkCompany.Visible = true;
                    this.chkProductor.Visible = true;
                    this.chkLossPaye.Visible = true;
                    if (QA.Policy.IsDeferred)
                        this.btnDefferredPayPlan.Enabled = true;
                    else
                        this.btnDefferredPayPlan.Enabled = false;

                    if (QA.Policy.IsFamily)
                    {
                        this.chkFBfamilia.Enabled = true;
                        this.TxtFBEmployeeName.Enabled = true;
                    }
                    else
                    {
                        this.chkFBfamilia.Enabled = false;
                        this.TxtFBEmployeeName.Enabled = false;
                    }

                    if (QA.Policy.IsEmployee)
                    {
                        this.chkFBEmployee.Enabled = true;
                        this.ddlFBPosition.Enabled = true;
                        this.ddFBSubsidiary.Enabled = true;
                        this.ddlFBBranches.Enabled = true;
                    }
                    else
                    {
                        this.chkFBEmployee.Enabled = false;
                        this.ddlFBPosition.Enabled = false;
                        this.ddFBSubsidiary.Enabled = false;
                        this.ddlFBBranches.Enabled = false;
                    }
                    break;
                case (int)States.READWRITE:
                    this.txtEffDt.Enabled = true;
                    //this.imgEffDt.Visible = true;
                    this.btnCalculate.Visible = true;
                    this.txtTerm.Enabled = true;
                    this.btnEdit.Visible = false;
                    this.btnSave.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnDefferredPayPlan.Enabled = false;
                    this.btnRenew.Visible = false;
                    this.lblPrintOption.Visible = false;
                    this.chkAgency.Visible = false;
                    this.chkAsegurado.Visible = false;
                    this.chkCompany.Visible = false;
                    this.chkProductor.Visible = false;
                    this.chkLossPaye.Visible = false;

                    break;
                default:
                    //
                    break;
            }

            ShowPolicyFields(QA, State);

            if (Session["TaskControl"] != null)
            {
                TaskControl.QuoteAuto autoQuote = (TaskControl.QuoteAuto)Session["TaskControl"];
                if (autoQuote.ReadyForCalculation())
                {
                    //Has covers, drivers; All covers
                    //have at least a driver.
                    if (autoQuote.IsPolicy)
                    {
                        this.btnCalculate.Visible = false;
                        this.btnPrint.Visible = false;
                    }
                    else
                    {
                        this.btnCalculate.Visible = true;
                        this.btnPrint.Visible = true;
                    }
                }
                else
                {
                    this.btnCalculate.Visible = false;
                    this.btnPrint.Visible = false;
                }
            }
            else
            {
                this.btnCalculate.Visible = false;
                this.btnPrint.Visible = false;
            }

            VerifyAccess(State);
        }

        private TaskControl.QuoteAuto GetQuoteObjectFromSession()
        {
            if (Session["TaskControl"] != null)
            {
                return (TaskControl.QuoteAuto)Session["TaskControl"];
            }
            else
            {
                return null;
            }
        }

        private void HplAdd_Click(object sender, System.EventArgs e)
        {
            this.AddMainDriver();

            LoadFromForm();
            Response.Redirect("QuoteAutoDrivers.aspx");
        }


        //Codigo que estaba dentro del boton del next.
        //	try
        //	{
        //		this.AddMainDriver();
        //
        //		LoadFromForm();
        //		RemoveSessionLookUp();
        //		Response.Redirect("QuoteAutoVehicles.aspx");
        //	}
        //	catch(Exception xcp)
        //  {
        //	this.litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message);
        //	this.litPopUp.Visible = true;
        //  }

        private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                this.AddMainDriver();

                LoadFromForm();
                RemoveSessionLookUp();
                Response.Redirect("QuoteAutoVehicles.aspx");
            }
            catch (Exception xcp)
            {
                lblRecHeader.Text = xcp.Message;
                mpeSeleccion.Show();
            }
        }

        public enum States { NEW, READONLY, READWRITE };
        // :~*


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
                    throw new Exception(
                        "Could not parse user id from cp.Identity.Name.",
                        ex);
                }

                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                if (this.VerifyMianDriver() && QA.Drivers.Count == 0)
                {

                    AutoDriver AD = new AutoDriver();

                    AD.FirstName = txtFirstNm.Text.ToUpper();
                    AD.LastName1 = txtLastNm1.Text.ToUpper();
                    AD.LastName2 = txtLastNm2.Text.ToUpper();

                    AD.HomePhone = QA.Customer.HomePhone;
                    AD.WorkPhone = QA.Customer.JobPhone;
                    AD.Cellular = QA.Customer.Cellular;
                    AD.LocationID = QA.Customer.LocationID;

                   // AD.BirthDate = txtBirthDt.Text;
                    AD.BirthDate = String.Format("{0:d}", DateTime.Parse(txtBirthDt.Text));
                    AD.Gender = int.Parse(ddlGender.SelectedItem.Value);
                    AD.MaritalStatus = int.Parse(ddlMaritalSt.SelectedItem.Value);
                    AD.License = txtLicense.Text.ToUpper();
                    AD.SocialSecurity = txtSocSec.Text;

                    AD.QuoteID = ((TaskControl.QuoteAuto)Session["TaskControl"]).QuoteId;

                    AD.ProspectID = QA.Customer.ProspectID;
                    ((EPolicy.Customer.Prospect)AD).Mode = (int)EPolicy.Customer.Prospect.ProspectMode.CLEAR;
                    AD.Mode = (int)Enumerators.Modes.Insert;

                    QA.RemoveDriver(AD);
                    QA.AddDriver(AD);

                    QA.Mode = (int)Enumerators.Modes.Insert;

                    QA.Save(userID, null, AD, false);
                }
                // Update Customer Information
                UpdateCustomerInformation(QA, userID);
            }
            catch (Exception xcp)
            {
                throw new Exception(xcp.Message);
            }
        }

        private bool VerifyMianDriver()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            ArrayList errorMessages = new ArrayList();

            //First Name
            if (QA.Customer.FirstName.Trim() == "")
            {
                errorMessages.Add("First Name is missing.");
            }

            //Last Name 1
            if (QA.Customer.LastName1.Trim() == "")
            {
                errorMessages.Add("Last Name is missing.");
            }

            //BirthDate
            if (txtBirthDt.Text.Trim() == "")
            {
                errorMessages.Add("Birthdate is missing.");
            }

            else if (this.CalcAge(txtBirthDt.Text.Trim()) < 18)
            {
                errorMessages.Add("Invalid birthdate. A driver " +
                    "must be 18 years old or more.");
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

            //Social Security
            if (this.txtSocSec.Text.Trim() == string.Empty)
            {
                errorMessages.Add("Social Security is missing.");
            }

            //License
            if (this.txtLicense.Text.Trim() == string.Empty)
            {
                errorMessages.Add("License is missing.");
            }

            if (errorMessages.Count > 0)
            {
                string popUpString = "";

                foreach (string message in errorMessages)
                {
                    popUpString += message + " ";
                }

                throw new Exception(popUpString);

                //				this.litPopUp.Text = 
                //					Utilities.MakeLiteralPopUpString(popUpString);
                //				this.litPopUp.Visible = true;

                //return false;
            }
            return true;
        }

        private int CalcAge(string birthDT)
        {
            DateTime pdt = DateTime.Parse(birthDT);
            DateTime now = DateTime.Now;
            TimeSpan ts = now - pdt;
            int Years = (int)(((float)ts.Days) / 365.25f);
            return Years;
        }

        public void UpdateCustomerInformation(TaskControl.QuoteAuto QA, int userID)
        {
            Customer.Customer cust = Customer.Customer.GetCustomer(QA.Customer.CustomerNo, userID);
            cust = QA.Customer;

            cust.Address1 = TxtAddress1.Text.Trim().ToUpper();
            cust.Address2 = TxtAddress2.Text.Trim().ToUpper();
            cust.City = TxtCity.Text.Trim().ToUpper();
            cust.ZipCode = TxtZipCode.Text.Trim().ToUpper();
            //cust.Birthday = txtBirthDt.Text;
            cust.Birthday = String.Format("{0:d}", DateTime.Parse(txtBirthDt.Text));

            cust.MaritalStatus = int.Parse(ddlMaritalSt.SelectedItem.Value);
            cust.Licence = txtLicense.Text.ToUpper();
            cust.SocialSecurity = txtSocSec.Text;
            cust.FileNo = txtFileNo.Text;

            switch (int.Parse(ddlGender.SelectedItem.Value))
            {
                case 1:
                    cust.Sex = "M";  //1
                    break;

                case 2:
                    cust.Sex = "F"; //2
                    break;

                default:
                    cust.Sex = "";
                    break;
            }
            cust.Mode = 2;  //Update
            cust.Save(userID);
        }


        private void VerifyAssignPolicyFields()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (this.ChkAutoAssignPolicy.Checked)
            {
                this.txtPolicyType.Enabled = true;
                this.txtPolicyNo.Enabled = false;
                this.txtCertificate.Enabled = false;
                this.txtSuffix.Enabled = false;
                QA.Policy.AutoAssignPolicy = false;
            }
            else
            {
                this.txtPolicyType.Enabled = true;
                this.txtPolicyNo.Enabled = true;
                this.txtCertificate.Enabled = true;
                this.txtSuffix.Enabled = false;
                QA.Policy.AutoAssignPolicy = true;
            }
            Session["TaskControl"] = QA;
        }

        private void RemoveSessionLookUp()
        {
            Session.Remove("LookUpTables");
        }

        protected void btnEdit_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (QA.IsPolicy)
            {
                QA.Mode = 2;
                QA.Policy.Mode = 2;
            }
            this.SetControlState((int)States.READWRITE);
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                string resultMess = this.ValidateThis();
                if (resultMess != "")
                {
                    throw new Exception(resultMess);
                }
                else
                {
                    LoadFromForm();
                    this.Calculate(true);

                    try
                    {
                        SaveQuoteData();
                        ClearFields();

                        TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                        if (!this.TermCorrespondsAdequatelyToBaseTypes(QA))
                        {
                            AutoCover cover = (AutoCover)QA.AutoCovers[0];
                            if (this.GetBasePolicySubClassFromPolicySubClassID(
                                cover.PolicySubClassId) == 1)//DI
                            {
                                //if (this.litPopUpB == null)
                                //    this.litPopUpB = new Literal();

                                //this.litPopUpB.Text =
                                //    Utilities.MakeLiteralPopUpString(
                                //    "The selected term ('" + QA.Term.ToString() +
                                //    "') is incompatible with a Double Interest " +
                                //    "base type. SAMS will automatically set the " +
                                //    "term for this policy to '24'.  You may " +
                                //    "select a term between 24 and 84 months from " +
                                //    "the main screen.");
                                //this.litPopUpB.Visible = true;
                                //QA.Term = 24;
                                //this.txtTerm.Text = "24";
                            }
                            else //FC, LI
                            {
                                throw new Exception(
                                    "The selected term ('" + QA.Term.ToString() +
                                    "') is incompatible with a Liability or Full " +
                                    "Cover base type. SAMS will automatically set the " +
                                    "term for this policy to '12'.");
                                QA.Term = 12;
                                this.txtTerm.Text = "12";
                            }
                        }

                        DisplayQuote(QA);
                        this.DisplayProspect(QA.Prospect);

                        if (QA.IsPolicy)
                        {
                            VerifyPolicyNumber();
                            // Se carga el objeto de nuevo porque si hubo cambio en el verify para que actualice la info.
                            QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                            // Se recalcula la prima para que actualice el QuoteID por el TaskControlID recalcalcule con los rates Nuevos.
                            Login.Login cp = HttpContext.Current.User as Login.Login;
                            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                            QA.Mode = 2;
                            QA.Policy.Mode = 2;
                            this.Calculate(true);
                            QA.Save(UserID, null, null, true);
                            Session["TaskControl"] = QA;
                            
                                btnEdit.Visible = false;
                                btnVehicles.Visible = false;
                                BtnDrivers.Visible = false;

                                PolicyXML(QA.TaskControlID);
                                RemoveSessionLookUp();
                                Response.Redirect("QuoteAuto.aspx", false);
                           
                        }

                        if (QA.IsPolicy)
                        {
                            lblRecHeader.Text = "Policy has been saved.";
                            mpeSeleccion.Show();
                        }
                        else
                        {
                            lblRecHeader.Text = "Quote has been saved.";
                            mpeSeleccion.Show();
                        }
                        //litPopUp.Visible = true;
                        this.SetControlState((int)States.READONLY);
                    }
                    catch (Exception xcp)
                    {
                        lblRecHeader.Text = xcp.Message.Trim();
                        mpeSeleccion.Show();
                    }
                    
                }
            }
            catch (Exception ecp)
            {
                lblRecHeader.Text = ecp.Message.Trim();
                mpeSeleccion.Show();
            }
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto quoteAuto = this.GetQuoteObjectFromSession();

            if (quoteAuto != null)
            {
                DisplayQuote(quoteAuto);
                this.DisplayProspect(quoteAuto.Prospect);

                this.txtEffDt.Text = quoteAuto.EffectiveDate.Trim();
                this.txtTerm.Text = quoteAuto.Term.ToString();
            }

            this.SetControlState((int)States.READONLY);
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
        protected void btnCalculate_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                this.Calculate(false);
            }
        }



        protected void btnPrint_Click(object sender, System.EventArgs e)
        {
            //			Login.Login cp = HttpContext.Current.User as Login.Login;
            //			string userName = string.Empty;
            //
            //			try
            //			{
            //				userName = cp.Identity.Name.Split("|".ToCharArray())[0];
            //			}
            //			catch(Exception ex)
            //			{
            //				throw new Exception(
            //					"Could not parse user name from cp.Identity.Name.", ex);
            //			}
            //			TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto) Session["TaskControl"];
            //		
            //			Reports.QuoteAuto.rptQuoteAuto rpt = new Reports.QuoteAuto.rptQuoteAuto(quoteAuto, userName);
            //			rpt.Run(false);
            //			
            //			Session.Add("Report", rpt);
            //			Session.Add("FromPage", "QuoteAuto.aspx");
            //
            //			RemoveSessionLookUp();
            //
            //			Response.Redirect("ActiveXViewer.aspx",false);
        }

        protected void BtnDrivers_Click(object sender, System.EventArgs e)
        {
            LoadFromForm();
            RemoveSessionLookUp();
            Response.Redirect("QuoteAutoDrivers.aspx");
        }

        protected void btnIncentive_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            TaskControl.Policy pol = QA.Policy;

            Session.Add("TaskControlCommission", pol);
            Session.Add("FromPage", "QuoteAuto.aspx");
            Response.Redirect("ViewCommission.aspx");
        }

        protected void BtnSendPPS_Click(object sender, System.EventArgs e)
        {
            try
            {
                string resultMess = this.ValidateThis();
                if (resultMess != "")
                {
                    throw new Exception(resultMess);
                }
                else
                {
                    LoadFromForm();
                    this.Calculate(true);

                    try
                    {
                        SaveQuoteData();
                        ClearFields();

                        TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                        if (!this.TermCorrespondsAdequatelyToBaseTypes(QA))
                        {
                            AutoCover cover = (AutoCover)QA.AutoCovers[0];
                            if (this.GetBasePolicySubClassFromPolicySubClassID(
                                cover.PolicySubClassId) == 1)//DI
                            {
                                //if (this.litPopUpB == null)
                                //    this.litPopUpB = new Literal();

                                //this.litPopUpB.Text =
                                //    Utilities.MakeLiteralPopUpString(
                                //    "The selected term ('" + QA.Term.ToString() +
                                //    "') is incompatible with a Double Interest " +
                                //    "base type. SAMS will automatically set the " +
                                //    "term for this policy to '24'.  You may " +
                                //    "select a term between 24 and 84 months from " +
                                //    "the main screen.");
                                //this.litPopUpB.Visible = true;
                                //QA.Term = 24;
                                //this.txtTerm.Text = "24";
                            }
                            else //FC, LI
                            {
                                throw new Exception(
                                    "The selected term ('" + QA.Term.ToString() +
                                    "') is incompatible with a Liability or Full " +
                                    "Cover base type. SAMS will automatically set the " +
                                    "term for this policy to '12'.");
                                QA.Term = 12;
                                this.txtTerm.Text = "12";
                            }
                        }

                        DisplayQuote(QA);
                        this.DisplayProspect(QA.Prospect);

                        if (QA.IsPolicy)
                        {
                            VerifyPolicyNumber();
                            // Se carga el objeto de nuevo porque si hubo cambio en el verify para que actualice la info.
                            QA = (TaskControl.QuoteAuto)Session["TaskControl"];

                            // Se recalcula la prima para que actualice el QuoteID por el TaskControlID recalcalcule con los rates Nuevos.
                            Login.Login cp = HttpContext.Current.User as Login.Login;
                            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                            QA.Mode = 2;
                            QA.Policy.Mode = 2;
                            this.Calculate(true);
                            QA.Save(UserID, null, null, true);
                            Session["TaskControl"] = QA;

                            btnEdit.Visible = false;
                            btnVehicles.Visible = false;
                            BtnDrivers.Visible = false;

                            PolicyXML(QA.TaskControlID);
                            RemoveSessionLookUp();
                            //Response.Redirect("QuoteAuto.aspx", false);

                        }

                        if (QA.IsPolicy)
                        {
                            lblRecHeader.Text = "Policy has been saved.";
                            mpeSeleccion.Show();
                        }
                        else
                        {
                            lblRecHeader.Text = "Quote has been saved.";
                            mpeSeleccion.Show();
                        }
                        //litPopUp.Visible = true;
                        this.SetControlState((int)States.READONLY);
                    }
                    catch (Exception xcp)
                    {
                        lblRecHeader.Text = xcp.Message.Trim();
                        mpeSeleccion.Show();
                    }

                }
            }
            catch (Exception ecp)
            {
                lblRecHeader.Text = ecp.Message.Trim();
                mpeSeleccion.Show();
            }

        }

        protected void btnPayCertLetter_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            TaskControl.PaymentCertificationLetter pay = TaskControl.PaymentCertificationLetter.GetPaymentCertificationLetter(this.txtPolicyType.Text.Trim(), this.txtPolicyNo.Text.Trim(), this.txtCertificate.Text.Trim(), this.txtSuffix.Text.Trim());
            Session.Add("PolicyPCL", "FromPolicy");

            if (pay != null)
            {
                Session.Add("TCPolicy", QA);
                Session["TaskControl"] = pay;
            }
            else //new Payment Certification Letter
            {
                pay = new TaskControl.PaymentCertificationLetter();
                pay.Mode = 1; //ADD

                pay.CustomerNo = QA.CustomerNo;
                pay.PolicyType = QA.Policy.PolicyType;
                pay.PolicyNo = QA.Policy.PolicyNo;
                pay.Certificate = QA.Policy.Certificate;
                pay.Suffix = QA.Policy.Suffix;
                pay.FirstName = QA.Customer.FirstName.Trim();
                pay.LastName1 = QA.Customer.LastName1.Trim();
                pay.LastName2 = QA.Customer.LastName2.Trim();
                pay.TotalPremium = QA.TotalPremium + QA.Charge;
                pay.EffectiveDate = QA.EffectiveDate;
                pay.ExpirationDate = QA.ExpirationDate;
                pay.Bank = QA.Bank;
                Session.Add("TCPolicy", QA);
                Session["TaskControl"] = pay;
            }

            //TaskControl.Policy.
            Session.Add("FromPagePol", "QuoteAuto.aspx");

            RemoveSessionLookUp();
            Response.Redirect("PaymentCertificationLetter.aspx", false);
        }

        protected void btnPayments_Click(object sender, System.EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            TaskControl.Policy pol = QA.Policy;

            Session.Add("TaskControlPayment", pol);
            Session.Add("FromPage", "QuoteAuto.aspx");
            Response.Redirect("ViewPayment.aspx");
        }

        protected void btnVehicles_Click(object sender, System.EventArgs e)
        {
            LoadFromForm();

            //Se anadio
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            AutoCover AC = (AutoCover)QA.AutoCovers[0];
            Session["InternalAutoID"] = AC.InternalID;

            RemoveSessionLookUp();
            Response.Redirect("QuoteAutoVehicles.aspx");
        }

        protected void btnPrintInvoice_Click(object sender, System.EventArgs e)
        {
            
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            //int AutoQuotesID = 0;
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
            List<string> mergePaths = new List<string>();
            //for (int i = 0; i < QA.AutoCovers.Count; i++)
            //{
            //    AutoCover AC = (AutoCover)QA.AutoCovers[i];

            //    AutoQuotesID = AC.QuotesAutoId;

                mergePaths = ImprimirFactura(mergePaths, 1, "Invoice");
                
            //}

            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFile = "";
            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, QA.TaskControlID.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
        }

        
        protected void btnPrintPolicy_Click(object sender, System.EventArgs e)
        {

            try
            {
                string[] CopyList = new string[5];
                bool[] CopyValidation = new bool[5];

                bool Productor = false, Agency = false, Asegurado = false, LossPaye = false, Company = false;

                //Count para la cantidad de imprimir copias

                if (chkProductor.Checked == false && chkAgency.Checked == false && chkAsegurado.Checked == false
                    && chkCompany.Checked == false && chkLossPaye.Checked == false)
                {
                    throw new Exception("Please select one or more print option.");
                }

                else
                {

                    if (chkProductor.Checked)
                    {
                        CopyList[1] = "Productor Copy";
                        CopyValidation[1] = true;
                    }
                    else
                    {
                        CopyValidation[1] = false;
                    }
                    if (chkLossPaye.Checked)
                    {
                        CopyList[2] = "Loss Payee Copy";
                        CopyValidation[2] = true;
                    }
                    else
                    {
                        CopyValidation[2] = false;
                    }
                    if (chkAgency.Checked)
                    {
                        CopyList[3] = "Agency Copy";
                        CopyValidation[3] = true;
                    }
                    else
                    {
                        CopyValidation[3] = false;
                    }
                    if (chkAsegurado.Checked)
                    {
                        CopyList[0] = "Insured Copy";
                        CopyValidation[0] = true;
                    }
                    else
                    {
                        CopyValidation[0] = false;
                    }
                    if (chkCompany.Checked)
                    {
                        CopyList[4] = "Company Copy";
                        CopyValidation[4] = true;
                    }
                    else
                    {
                        CopyValidation[4] = false;
                    }
                }

                // End Print Option


                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                List<string> mergePaths = new List<string>();
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

                EPolicy.TaskControl.QuoteAuto deferredPayment;
                EPolicy.TaskControl.QuoteAuto taskControl2 = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
                EPolicy.TaskControl.QuoteAuto taskControl = EPolicy.TaskControl.QuoteAuto.GetQuoteAuto(taskControl2.TaskControlID, userID, true);
                EPolicy.Quotes.AutoCover AC2 = null;
                int AutoQuotesID = 0;


                




                for (int j = 0; j < 5; j++)
                {
                    bool print = false;
                    string CopyFile = "";

                    if (CopyValidation[j] == true)
                    {
                        switch (j)
                        {
                            case 0:
                                Asegurado = true;
                                Productor = false;
                                LossPaye = false;
                                Agency = false;
                                Company = false;
                                CopyFile = CopyList[j];
                                print = true;
                                break;
                            case 1:
                                Asegurado = false;
                                Productor = true;
                                LossPaye = false;
                                Agency = false;
                                Company = false;
                                CopyFile = CopyList[j];
                                print = true;
                                break;
                            case 2:
                                Asegurado = false;
                                Productor = false;
                                LossPaye = true;
                                Agency = false;
                                Company = false;
                                CopyFile = CopyList[j];
                                print = true;
                                break;
                            case 3:
                                Asegurado = false;
                                Productor = false;
                                LossPaye = false;
                                Agency = true;
                                Company = false;
                                CopyFile = CopyList[j];
                                print = true;
                                break;
                            case 4:
                                Asegurado = false;
                                Productor = false;
                                LossPaye = false;
                                Agency = false;
                                Company = true;
                                CopyFile = CopyList[j];
                                print = true;
                                break;


                        }
                        if (print)
                        {
                            //Imprimir las copias solicitada
                            if (taskControl.Term != 12)
                            {
                                for (int i = 0; i < taskControl.VehicleCount; i++)
                                {
                                    AC2 = (Quotes.AutoCover)taskControl.AutoCovers[i];
                                    AutoQuotesID = AC2.QuotesAutoId;
                                }
                            }

                            //TODO FALTA LA FACTURA
                            //Print Invoice
                            //mergePaths = ImprimirFactura(mergePaths, AutoQuotesID, 1, CopyFile);


                            if (taskControl.Term == 12)
                                
                                mergePaths = ImprimirDinamicamenteAuto(mergePaths, taskControl, "AutoPersonalFullCover", true, CopyFile, j);
                                

                            else
                                mergePaths = ImprimirDoubleInteres(mergePaths, AutoQuotesID, 1, CopyFile);

                            //Imprimir reportes que aplican  para todo
                            mergePaths = ImprimirReportesParaTodos(mergePaths, taskControl, j, CopyFile, Agency, Productor, Company, LossPaye, Asegurado,AutoQuotesID);
            
                        }
                    }
                }

                bool assist = false;
                for (int i = 0; i < taskControl.VehicleCount; i++)
                {
                    AC2 = (Quotes.AutoCover)taskControl.AutoCovers[i];



                    if (AC2.AssistancePremium > 0)
                    {
                        assist = true;

                    }

                }

                if (assist == true)
                { mergePaths = ImprimirRoadAssistance(mergePaths, taskControl, AC2.QuotesAutoId, " ", 0); }

                OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                string FinalFile = "";
                FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

            }
            catch (Exception exc)
            {
                lblRecHeader.Text = exc.Message.Trim();
                mpeSeleccion.Show();
            }
        }

        private List<string> ImprimirDinamicamenteAuto(List<string> mergePaths, EPolicy.TaskControl.QuoteAuto taskControl, string PathReport, bool VerReport, string Copy, int order)
        {
            EPolicy.TaskControl.QuoteAuto qa = taskControl;
            EPolicy.Quotes.AutoCover ac = null;

            for (int i = 0; i < qa.AutoCovers.Count; i++)
            {
                ac = (AutoCover)qa.AutoCovers[i];
                

             
                mergePaths = ImprimirDecPageAutoPersonal(mergePaths, ac.QuotesAutoId, 0, 0, i, 0, 0, PathReport, VerReport, Copy, order); //Cambio a Order era i
                //if (ac.AssistancePremium > 0)
                //{
                //    mergePaths = ImprimirRoadAssistance(mergePaths, taskControl, ac.QuotesAutoId, Copy, order);
                //}
            }

            return mergePaths;
        }

        private List<string> ImprimirFactura(List<string> mergePaths, int j2, string Copy)
        {
            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

            EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
            int s = taskControl.TaskControlID;

            //GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
            //GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter();
            //GetReportDoubleInterestTableAdapters.GetReportAutoPolicy1ByTaskControlIDDoubleInterestTableAdapter ds2 = new GetReportDoubleInterestTableAdapters.GetReportAutoPolicy1ByTaskControlIDDoubleInterestTableAdapter();
            //GetReportAutoDITableAdapters.GetReportAutoDITableAdapter ds3 = new GetReportAutoDITableAdapters.GetReportAutoDITableAdapter();

            GetInvoiceInfoByTaskControlIDTableAdapters.GetInvoiceInfoByTaskControlIDTableAdapter ta2 = new GetInvoiceInfoByTaskControlIDTableAdapters.GetInvoiceInfoByTaskControlIDTableAdapter();
            GetInvoiceInfoByTaskControlID.GetInvoiceInfoByTaskControlIDDataTable dt2 = new GetInvoiceInfoByTaskControlID.GetInvoiceInfoByTaskControlIDDataTable();

            ReportDataSource rds3 = new ReportDataSource();
            rds3 = new ReportDataSource("GetInvoiceInfoByTaskControlID", (System.Data.DataTable)ta2.GetData(s));


            ReportDataSource rds1 = new ReportDataSource();
            ReportDataSource rds2 = new ReportDataSource();

            ReportParameter rp = new ReportParameter();

            try
            {
                //rds1 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                //rds2 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID, quoteAuto1, j2));

               // rp = new ReportParameter("ImgPath", "");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            string ImgPath = "", AgentDesc = "";

            Uri pathAsUri = null;

            System.Data.DataTable dt = null, dtAgent = null;

            dt = EPolicy.TaskControl.TaskControl.GetImageLogoByAgentID(taskControl.Agent.ToString().Trim());

            dtAgent = EPolicy.TaskControl.TaskControl.GetAgentByAgentID(taskControl.Agent.ToString().Trim());

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

            //ReportParameter[] parameters = new ReportParameter[1];
            //parameters[0] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");
            rp = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");


            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.LocalReport.EnableExternalImages = true;
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/AgentInvoice_AutoPR.rdlc");

            viewer1.LocalReport.DataSources.Add(rds3);

            //viewer1.LocalReport.DataSources.Add(rds1);
            //viewer1.LocalReport.DataSources.Add(rds2);
            viewer1.LocalReport.SetParameters(rp);
            viewer1.LocalReport.Refresh();

            Warning[] warnings1;
            string[] streamIds1;
            string mimeType1;
            string encoding1 = string.Empty;
            string extension1;

            string fileName1 = "Invoice- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-7" + s;
            string _FileName1 = "Invoice- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-7" + s + ".pdf";

            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

            byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType1, out encoding1, out extension1, out streamIds1, out warnings1);

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
                //litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString(ecp.Message);
                //litPopUp.Visible = true;
                lblRecHeader.Text = ecp.Message;
                mpeSeleccion.Show();
            }

            return mergePaths;

        }

        private List<string> ImprimirDinamicamenteAuto2(List<string> mergePaths, EPolicy.TaskControl.QuoteAuto taskControl, string PathReport, bool VerReport, string Copy, int order)
        {
            if (taskControl.TaskControlID != 0)
            {

                //auto --Imprime dinamicamente
                if (taskControl.VehicleCount > 0)
                {

                    EPolicy.TaskControl.QuoteAuto QA = taskControl;
                    EPolicy.Quotes.AutoCover AC2 = null;
                    EPolicy.Quotes.AutoCover ACPolicy = new EPolicy.Quotes.AutoCover();

                    int quotesAutoId1 = 0;
                    int quotesAutoId2 = 0;
                    int quotesAutoId3 = 0;

                    int j2 = 0;
                    int j3 = 0;
                    int j4 = 0;

                    for (int i = 0; i <= taskControl.VehicleCount; i++)
                    {
                        if (i % 3 == 0 && i < taskControl.VehicleCount)
                        {
                            AC2 = (EPolicy.Quotes.AutoCover)QA.AutoCovers[i];
                            quotesAutoId1 = AC2.QuotesAutoId;

                            //if (AC2.)
                            //{
                            //quotesAutoCubiertaTransporteId1 = AC2.QuotesAutoId;
                            //}
                            j2++;
                            j3++;
                            j4++;

                        }

                        if (i % 3 == 1 && i < taskControl.VehicleCount)
                        {
                            AC2 = (EPolicy.Quotes.AutoCover)QA.AutoCovers[i];
                            quotesAutoId2 = AC2.QuotesAutoId;

                            //if ()
                            //{
                            //quotesAutoCubiertaTransporteId2 = AC2.QuotesAutoId;
                            //}
                            j3++;
                            j4++;
                        }

                        if (i % 3 == 2 && i < taskControl.VehicleCount)
                        {
                            AC2 = (EPolicy.Quotes.AutoCover)QA.AutoCovers[i];
                            quotesAutoId3 = AC2.QuotesAutoId;
                            //if ()
                            //{
                            //quotesAutoCubiertaTransporteId3 = AC2.QuotesAutoId;
                            //}
                            j4++;

                            mergePaths = ImprimirDecPageAutoPersonal(mergePaths, quotesAutoId1, quotesAutoId2, quotesAutoId3, j2, j3, j4, PathReport, VerReport, Copy, order);
                            

                            j2 = j3 = j4;

                            quotesAutoId1 = 0;
                            quotesAutoId2 = 0;
                            quotesAutoId3 = 0;
                        }

                        if (i == taskControl.VehicleCount && (quotesAutoId1 != 0 || quotesAutoId2 != 0 || quotesAutoId3 != 0))
                        {
                            mergePaths = ImprimirDecPageAutoPersonal(mergePaths, quotesAutoId1, quotesAutoId2, quotesAutoId3, j2, j3, j4, PathReport, VerReport, Copy, order);

                        }
                    }
                }
            }
            return mergePaths;
        }

        private List<string> ImprimirDecPageAutoPersonal(List<string> mergePaths, int quoteAuto1, int quoteAuto2, int quoteAuto3, int j2, int j3, int j4, string PathReport, bool VerReportes, string Copy, int order)
        {
            try
            {
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

                EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
                int s = taskControl.TaskControlID;
                string Reportes = "";

                // Validaciones para los tipos de cubierta
                bool IsAccidentalDeath = false;
                bool Equipment = false;
                bool AutoRental = false;
                bool AutoTransporte = false;
                bool AutoRemolque = false;
                bool EquipoEspeciales = false;
                bool Split = false;
                bool LimResponsabilidad = false;
                bool Assistance = false;
                bool Uninsured = false;

                AutoCover AC2 = new AutoCover();



                AC2 = null;
                int j = 0;
                Reportes = "PP0001 PRS(06/98), PP0170 PRS(12/03), PP0326 PRS(06/94), PP0369 PRS(11/99), PP1301 PRS(12/99), PP1379 PRS(08/03), IL0024 (01/99), IL0901 (01/98), PP0001-B";

                //Verificar Collision
                for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                {
                    AC2 = (AutoCover)taskControl.AutoCovers[i];

                    if (taskControl.Term == 12 && VerReportes == true)
                    {
                        j = 1;

                        DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId,i+1);

                        try
                        {
                            if (double.Parse(dt.Rows[0]["CollisionPremium1"].ToString().Trim()) == 0)
                            {

                            }
                            else
                            {
                                Reportes += ",PP0305 PRS(08/86), PP0308 PRS(06/94), CIP1031 (11/00)";

                            }

                            if (double.Parse(dt.Rows[0]["AccidentalDeathPremium1"].ToString().Trim()) > 0)
                                IsAccidentalDeath = true;
                            if (double.Parse(dt.Rows[0]["EquipmentSoundPremium1"].ToString().Trim()) > 0 || double.Parse(dt.Rows[0]["EquipmentAudioPremium1"].ToString().Trim()) > 0 ||
                                double.Parse(dt.Rows[0]["EquipmentTapesPremium1"].ToString()) > 0)
                                Equipment = true;
                            if (double.Parse(dt.Rows[0]["VehicleRental1"].ToString().Trim()) > 0)
                                AutoTransporte = true;
                            if ((double.Parse(dt.Rows[0]["SpecialEquipmentCollPremium1"].ToString().Trim()) + double.Parse(dt.Rows[0]["SpecialEquipmentCompPremium1"].ToString().Trim())) > 0)
                                EquipoEspeciales = true;
                            if (double.Parse(dt.Rows[0]["UninsuredSinglePremium1"].ToString().Trim()) > 0)
                                LimResponsabilidad = true;
                            if (double.Parse(dt.Rows[0]["Arrendamiento1Prima"].ToString().Trim()) > 0)
                                AutoRental = true;
                            if (double.Parse(dt.Rows[0]["AssistancePremium1"].ToString().Trim()) > 0)
                                Assistance = true;
                            if (double.Parse(dt.Rows[0]["TowingPremium1"].ToString().Trim()) > 0)
                                AutoRemolque = true;
                            if (dt.Rows[0]["UninsuredSingleDesc1"].ToString().Trim() != "")
                                Uninsured = true;
                            // break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }


                    }
                }

                if (IsAccidentalDeath)
                    Reportes += ",PAP1001 (0298)";
                if (Equipment)
                    Reportes += ",PP0313 PRS (06/98)";
                if (AutoTransporte)
                    Reportes += ", PP0302 PRS(06/98)";
                if (EquipoEspeciales)
                    Reportes += ",PP0344 PRS(11/99)";
                if (LimResponsabilidad)
                    Reportes += ",PP0311 PRS(03/11)";
                if (AutoRental)
                    Reportes += ",PP0335";
                //if (Assistance)
                //    Reportes += ", MIC1005 (11/14)";
                if (AutoRemolque)
                    Reportes += ", PP0303 (08/86)";
                if (Uninsured)
                    Reportes += ",PP0401 PRS(04/86)";


                GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
                GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter();
                GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy2ByTaskControlIDTableAdapter ds3 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy2ByTaskControlIDTableAdapter();
                GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy3ByTaskControlIDTableAdapter ds4 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy3ByTaskControlIDTableAdapter();

                ReportDataSource rds1 = new ReportDataSource();
                ReportDataSource rds2 = new ReportDataSource();
                ReportDataSource rds3 = new ReportDataSource();
                ReportDataSource rds4 = new ReportDataSource();

                ReportParameter p = new ReportParameter("Reportes", Reportes);
                ReportParameter copy = new ReportParameter("Copy", Copy);


                try
                {
                    rds1 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                    rds2 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID, quoteAuto1, j2));
                    rds3 = new ReportDataSource("GetReportAutoPolicy2ByTaskControlID", (DataTable)ds3.GetData(taskControl.TaskControlID, quoteAuto2, j3));
                    rds4 = new ReportDataSource("GetReportAutoPolicy3ByTaskControlID", (DataTable)ds4.GetData(taskControl.TaskControlID, quoteAuto3, j4));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/" + PathReport + ".rdlc");

                viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.DataSources.Add(rds2);
                viewer1.LocalReport.DataSources.Add(rds3);
                viewer1.LocalReport.DataSources.Add(rds4);
                if (j == 1)
                {
                    viewer1.LocalReport.SetParameters(p);
                    viewer1.LocalReport.SetParameters(copy);
                }
                viewer1.LocalReport.Refresh();

                mergePaths = WriteFileName(mergePaths, taskControl, viewer1, PathReport + quoteAuto1.ToString(), order);


                return mergePaths;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<string> ImprimirDoubleInteres(List<string> mergePaths, int quoteAuto1, int j2, string Copy)
        {
            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

            EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
            int s = taskControl.TaskControlID;

            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
            //GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter();
            GetReportDoubleInterestTableAdapters.GetReportAutoPolicy1ByTaskControlIDDoubleInterestTableAdapter ds2 = new GetReportDoubleInterestTableAdapters.GetReportAutoPolicy1ByTaskControlIDDoubleInterestTableAdapter();
            GetReportAutoDITableAdapters.GetReportAutoDITableAdapter ds3 = new GetReportAutoDITableAdapters.GetReportAutoDITableAdapter();


            ReportDataSource rds1 = new ReportDataSource();
            ReportDataSource rds2 = new ReportDataSource();
            ReportDataSource rds3 = new ReportDataSource();

            ReportParameter rp = new ReportParameter();

            try
            {
                rds1 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                rds2 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDDoubleInterest", (DataTable)ds2.GetData(taskControl.TaskControlID, quoteAuto1, j2));
                rds3 = new ReportDataSource("GetReportAutoDI", (DataTable)ds3.GetData(quoteAuto1));
                rp = new ReportParameter("Copy", Copy);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/DoubleInteres.rdlc");
            viewer1.LocalReport.DataSources.Add(rds1);
            viewer1.LocalReport.DataSources.Add(rds2);
            viewer1.LocalReport.DataSources.Add(rds3);
            viewer1.LocalReport.SetParameters(rp);
            viewer1.LocalReport.Refresh();

            Warning[] warnings1;
            string[] streamIds1;
            string mimeType1;
            string encoding1 = string.Empty;
            string extension1;

            string fileName1 = "PolicyNo- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-7" + quoteAuto1;
            string _FileName1 = "PolicyNo- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-7" + quoteAuto1 + ".pdf";

            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

            byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType1, out encoding1, out extension1, out streamIds1, out warnings1);

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
                //litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString(ecp.Message);
                //litPopUp.Visible = true;
                lblRecHeader.Text = ecp.Message;
                mpeSeleccion.Show();
            }

            return mergePaths;

        }

        public List<string> ImprimirRoadAssistance(List<string> mergePaths, EPolicy.TaskControl.QuoteAuto taskControl, int QuotesAutoId,string copy, int order)
        {
            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

            int taskControlID = taskControl.TaskControlID;
            string FileNamePdf = "";
            string PathReport = MapPath("Reports/RoadAssist/");


            // ObjectDataSource ob = new ObjectDataSource("GetReportRoadAssistanceByTaskControlID.GetReportRoadAssistanceByTaskControlIDTableAdapter", "GetData");
             //ObjectDataSource ob = new ObjectDataSource("=GetReportAutoPolicy1ByTaskControlID.GetReportAutoPolicy1ByTaskControlIDTableAdapter", "GetData");
            //ObjectDataSource ob = new ObjectDataSource("GetReportAutoQuoteDetails.GetReportAutoQuoteDetailsTableAdapter", "GetData");

            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds1 = 
            new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter ds2 =
                new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter();


            GetReportAutoPolicyDetailsTableAdapters.GetReportAutoPolicyDetailsTableAdapter ds3 =
            new GetReportAutoPolicyDetailsTableAdapters.GetReportAutoPolicyDetailsTableAdapter();
           

            ReportDataSource rds1 = new ReportDataSource();
            ReportDataSource rds2 = new ReportDataSource();
            ReportDataSource rds3 = new ReportDataSource();
            ReportParameter rp = new ReportParameter();
            
            // ReportParameter parameter1 = new ReportParameter("Copy", Copy);

            try
            {
           
                rds1 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds1.GetData(taskControlID));
                rds2 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID, QuotesAutoId, order));
                rds3 = new ReportDataSource("GetReportAutoPolicyDetails", (DataTable)ds3.GetData(taskControlID));
                rp = new ReportParameter("Copy", copy);

            }
            catch (Exception ex)
            {

            }


            FileNamePdf = "TERMINOS Y CONDICIONES CUBIERTA REGULAR.pdf";
            DeleteFile(ProcessedPath + FileNamePdf);
            FileInfo file = new FileInfo(PathReport + FileNamePdf);
            file.CopyTo(ProcessedPath + FileNamePdf);
            mergePaths.Add(ProcessedPath + FileNamePdf);

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/AutoPersonalRoadAssist.rdlc");
            viewer1.LocalReport.DataSources.Add(rds1);
            viewer1.LocalReport.DataSources.Add(rds2);
            viewer1.LocalReport.DataSources.Add(rds3);
            viewer1.LocalReport.SetParameters(rp);
            viewer1.LocalReport.Refresh();
            //viewer1.LocalReport.SetParameters(parameter1);
   
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "RoadAssitPolicyNo- " + taskControl.ToString().Trim() + "-" + QuotesAutoId.ToString().Trim()+copy;
            string _FileName1 = "RoadAssitPolicyNo- " + taskControl.ToString().Trim() + "-" + QuotesAutoId.ToString().Trim() + copy+".pdf";

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
        private void DeleteFile(string pathAndFileName)
        {
            if (File.Exists(pathAndFileName))
                File.Delete(pathAndFileName);
        }
        private List<string> ImprimirReportesParaTodos(List<string> mergePaths, EPolicy.TaskControl.QuoteAuto taskControl, int Order, string copy, bool AgencyP, bool ProductorP,
            bool CompanyP, bool LossPayeP, bool AseguradoP,int AutoQuotesID)
        {
            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
            Quotes.AutoCover AC2 = null;
            bool IsLiability = true;
            FileInfo mFileIndex;
            //AutoQuotesID = AC2.QuotesAutoId;
            

            try
            {
                #region Liability
                ////////////////////////////////////////  MANDATORIO  ////////////////////////////////////////////////////
                // Print Liability
                IsLiability = IsLiabilityValidation(taskControl);

                //Imprimir forma mandatoria para el Asegurado
                if (taskControl.Term == 12 && IsLiability)
                {
                    if (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP)
                    {

                        bool Assistance = false;
                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            if (AC2.AssistancePremium > 0)
                                Assistance = true;

                        }

                        //MIC1005
                        if (Assistance && (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP))
                        {
                            // mergePaths = PrintMic1005Auto(mergePaths, taskControl, Order, copy);
                          
                            mergePaths = ImprimirRoadAssistance(mergePaths, taskControl, AC2.QuotesAutoId, copy, Order);


                            bool IsEmployee = false;

                            DataTable dtIsEmployee = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);

                            for (int i = 0; i < dtIsEmployee.Rows.Count; i++)
                            {
                                if (dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim() != "")
                                {
                                    IsEmployee = bool.Parse(dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim());
                                }
                            }

                            //if (IsEmployee)
                            //{
                            //    //MIC1005-2
                            //    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/POLITICA-CONDICIONES-GENERALES.pdf");
                            //    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf");

                            //}
                            //else
                            //{
                            ////MIC1005-2
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/MIC1005-2.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf");
                            //}
                        }
                        if (AseguradoP)
                        {
                            //PP0001
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0001.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0001" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0001" + taskControl.TaskControlID.ToString() + ".pdf");

                            if (taskControl.Term != 12)
                            {
                                for (int i = 0; i < taskControl.VehicleCount; i++)
                                {
                                    AC2 = (Quotes.AutoCover)taskControl.AutoCovers[i];
                                    AutoQuotesID = AC2.QuotesAutoId;
                                }
                            }

                            //mergePaths = ImprimirPP0170(mergePaths, AutoQuotesID, 1, "");

                            ////PP0170
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0170.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0170" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0170" + taskControl.TaskControlID.ToString() + ".pdf");

                            ////PP0326
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0326.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0326" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0326" + taskControl.TaskControlID.ToString() + ".pdf");
                        }
                        if (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP)
                        {
                            //PP0369
                            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            {
                                AC2 = (AutoCover)taskControl.AutoCovers[i];
                                if (AC2.CombinedSingleLimit > 0)
                                {

                                    string PolicyNo = "";
                                    string Pca = "";

                                    DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);

                                    try
                                    {
                                        Pca = dt.Rows[0]["combinedSingleLimitDesc1"].ToString().Trim();
                                        PolicyNo = taskControl.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix;
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }

                                    ReportParameter rp = new ReportParameter();
                                    ReportParameter rp2 = new ReportParameter();

                                    rp = new ReportParameter("PolicyNo", PolicyNo);
                                    rp2 = new ReportParameter("PorCadaAccidente", double.Parse(Pca).ToString());

                                    ReportViewer viewer1 = new ReportViewer();

                                    viewer1.LocalReport.DataSources.Clear();
                                    viewer1.ProcessingMode = ProcessingMode.Local;
                                    viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0369.rdlc");

                                    viewer1.LocalReport.SetParameters(rp);
                                    viewer1.LocalReport.SetParameters(rp2);

                                    viewer1.LocalReport.Refresh();

                                    mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP0369" + i.ToString(), Order);
                                }
                            }
                            // END PP0369
                        }
                        if (AseguradoP)
                        {
                            ////PP1301
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP1301.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1301" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1301" + taskControl.TaskControlID.ToString() + ".pdf");

                            //PP1379
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP1379.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1379" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1379" + taskControl.TaskControlID.ToString() + ".pdf");


                            ////IL0024
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/IL0024.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0024" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0024" + taskControl.TaskControlID.ToString() + ".pdf");

                            //IL0901
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/IL0901.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0901" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0901" + taskControl.TaskControlID.ToString() + ".pdf");

                            //NIC1000
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/NIC1000.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NIC1000" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NIC1000" + taskControl.TaskControlID.ToString() + ".pdf");
                        }
                        //PP0001B
                        if (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP)
                        {
                            //for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            //{
                            //    AC2 = (AutoCover)taskControl.AutoCovers[i];

                            //    if (AC2.CombinedSingleLimit > 0)
                            //    {

                            //        string Asegurado = "";
                            //        string PoliceNo = "";
                            //        DateTime dateTime;
                            //        string Agency = "";
                            //        string EffDate = "";

                            //        DataTable dt = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                            //        try
                            //        {
                            //            PoliceNo = taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix;
                            //            Asegurado = dt.Rows[0]["NameCustomer"].ToString().Trim();
                            //            dateTime = Convert.ToDateTime(dt.Rows[0]["EffectiveDate"].ToString().Trim());
                            //            EffDate = dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + dateTime.Year.ToString();
                            //            Agency = dt.Rows[0]["AgencyDesc"].ToString().Trim();

                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            throw new Exception(ex.Message);
                            //        }

                            //        ReportParameter rp = new ReportParameter();
                            //        ReportParameter rp2 = new ReportParameter();
                            //        ReportParameter rp3 = new ReportParameter();
                            //        ReportParameter rp4 = new ReportParameter();

                            //        rp = new ReportParameter("PolicyNo", PoliceNo);
                            //        rp2 = new ReportParameter("Asegurado", Asegurado);
                            //        rp3 = new ReportParameter("EffDate", EffDate);
                            //        rp4 = new ReportParameter("Agency", Agency);

                            //        ReportViewer viewer1 = new ReportViewer();

                            //        viewer1.LocalReport.DataSources.Clear();
                            //        viewer1.ProcessingMode = ProcessingMode.Local;
                            //        viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0001B.rdlc");

                            //        viewer1.LocalReport.SetParameters(rp);
                            //        viewer1.LocalReport.SetParameters(rp2);

                            //        viewer1.LocalReport.Refresh();

                            //        mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP0001B" + i.ToString(), Order);


                            //        break;
                            //    }
                            //}
                        }
                        if (AseguradoP)
                        {
                            //PAP1000
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PAP1000.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1000" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1000" + taskControl.TaskControlID.ToString() + ".pdf");

                            //PA-ACLL
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PA-ACLL.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PA-ACLL" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PA-ACLL" + taskControl.TaskControlID.ToString() + ".pdf");
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //Imprimir forma mandatoria para el Asegurado
                        if (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP)
                        {
                            ////////////////////////////////////////  ADICIONALES  ////////////////////////////////////////////////////
                            // Validaciones para los tipos de cubierta
                            bool IsAccidentalDeath = false;
                            bool Equipment = false;
                            bool AutoRental = false;
                            bool AutoTransporte = false;
                            bool AutoRemolque = false;
                            bool EquipoEspeciales = false;
                            bool Split = false;
                            bool LimResponsabilidad = false;

                            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            {
                                AC2 = (AutoCover)taskControl.AutoCovers[i];

                                DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);

                                if (double.Parse(dt.Rows[i]["AccidentalDeathPremium1"].ToString().Trim()) > 0)
                                    IsAccidentalDeath = true;
                                if (double.Parse(dt.Rows[i]["EquipmentSoundPremium1"].ToString().Trim()) > 0 || double.Parse(dt.Rows[i]["EquipmentAudioPremium1"].ToString().Trim()) > 0 ||
                                    double.Parse(dt.Rows[i]["EquipmentTapesPremium1"].ToString()) > 0)
                                    Equipment = true;
                                if (double.Parse(dt.Rows[i]["VehicleRental1"].ToString().Trim()) > 0)
                                    AutoTransporte = true;
                                if ((double.Parse(dt.Rows[i]["SpecialEquipmentCollPremium1"].ToString().Trim()) + double.Parse(dt.Rows[i]["SpecialEquipmentCompPremium1"].ToString().Trim())) > 0)
                                    EquipoEspeciales = true;
                                if (double.Parse(dt.Rows[i]["UninsuredSinglePremium1"].ToString().Trim()) > 0)
                                    LimResponsabilidad = true;
                                if (double.Parse(dt.Rows[i]["Arrendamiento1Prima"].ToString().Trim()) > 0)
                                    AutoRental = true;
                                if (double.Parse(dt.Rows[i]["AssistancePremium1"].ToString().Trim()) > 0)
                                    Assistance = true;
                                if (double.Parse(dt.Rows[i]["TowingPremium1"].ToString().Trim()) > 0)
                                    AutoRemolque = true;
                                //if (AC2.AccidentalDeathPremium > 0)
                                //    IsAccidentalDeath = true;
                                //if (AC2.EquipmentAudioPremium > 0 || AC2.EquipmentSoundPremium > 0 || AC2.EquipmentTapesPremium > 0)
                                //    Equipment = true;
                                //if (AC2.VehicleRental > 0)
                                //    AutoTransporte = true;
                                //if (AC2.TowingPremium > 0)
                                //    AutoRemolque = true;
                                //if ((AC2.SpecialEquipmentCollPremium + AC2.SpecialEquipmentCompPremium) > 0)
                                //    EquipoEspeciales = true;
                                //if (AC2.UninsuredSplitPremium > 0)
                                //    Split = true;
                                //if (AC2.UninsuredSinglePremium > 0)
                                //    LimResponsabilidad = true;
                                //if (AC2.LeaseLoanGapId > 0)
                                //    AutoRental = true;
                                //if (AC2.AssistancePremium > 0)
                                //    Assistance = true;

                            }

                            if (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP)
                            {
                                //MIC1005
                                if (Assistance)
                                {

                                    ImprimirRoadAssistance(mergePaths, taskControl, AC2.QuotesAutoId, copy, Order);
                                    // AC2.QuotesAutoId
                                   // mergePaths = ImprimirRoadAssistance(mergePaths, taskControl, AC2.QuotesAutoId, copy, Order);
                                    //GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
                                    //GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy4ByTaskControlIDTableAdapter ds11 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy4ByTaskControlIDTableAdapter();

                                    //ReportDataSource rds;
                                    //ReportDataSource rds11;
                                    //ReportParameter rpd = new ReportParameter("Sufijo", taskControl.Policy.Suffix);
                                    //try
                                    //{
                                    //    rds = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds.GetData(taskControl.TaskControlID));
                                    //    rds11 = new ReportDataSource("GetReportAutoPolicy4ByTaskControlID", (DataTable)ds11.GetData(taskControl.TaskControlID));
                                    //}

                                    //catch (Exception ex)
                                    //{
                                    //    throw new Exception(ex.Message);
                                    //}

                                    //ReportViewer viewer = new ReportViewer();
                                    //viewer.LocalReport.DataSources.Clear();
                                    //viewer.ProcessingMode = ProcessingMode.Local;
                                    //viewer.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/MIC1005Auto.rdlc");
                                    //viewer.LocalReport.DataSources.Add(rds);
                                    //viewer.LocalReport.DataSources.Add(rds11);
                                    //viewer.LocalReport.SetParameters(rpd);
                                    //viewer.LocalReport.Refresh();

                                    //mergePaths = WriteFileName(mergePaths, taskControl, viewer, "MIC1005Auto", Order);

                                }
                                //fin del metodo de MIC1005

                                //PAP1001

                                if (IsAccidentalDeath)
                                {
                                    //string CustomerName = "";
                                    //string AccidentDeathDesc = "";
                                    //string AccidentDeathPrima = "";


                                    //DataTable dt = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);
                                    //DataTable dt2 = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                                    //for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                                    //{
                                    //    AC2 = (AutoCover)taskControl.AutoCovers[i];


                                    //    if (double.Parse(dt.Rows[i]["AccidentalDeathPremium1"].ToString().Trim()) > 0)
                                    //    {
                                    //        CustomerName = dt2.Rows[i]["ConducName"].ToString().Trim();
                                    //        AccidentDeathDesc = dt.Rows[i]["AccidentalDeathPerson1"].ToString().Trim();
                                    //        AccidentDeathPrima = dt.Rows[i]["AccidentalDeathPremium1"].ToString().Trim();

                                    //        ReportParameter rp1 = new ReportParameter();
                                    //        ReportParameter rp2 = new ReportParameter();
                                    //        ReportParameter rp3 = new ReportParameter();

                                    //        try
                                    //        {
                                    //            rp1 = new ReportParameter("CustomerName", CustomerName);
                                    //            rp2 = new ReportParameter("AccidentDeathDesc", AccidentDeathDesc);
                                    //            rp3 = new ReportParameter("AccidentDeathPrima", AccidentDeathPrima);
                                    //        }
                                    //        catch (Exception ex)
                                    //        {
                                    //        }

                                    //        ReportViewer viewer1 = new ReportViewer();
                                    //        viewer1.LocalReport.DataSources.Clear();
                                    //        viewer1.ProcessingMode = ProcessingMode.Local;
                                    //        viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PAP1001-1.rdlc");
                                    //        viewer1.LocalReport.SetParameters(rp1);
                                    //        viewer1.LocalReport.SetParameters(rp2);
                                    //        viewer1.LocalReport.SetParameters(rp3);
                                    //        viewer1.LocalReport.Refresh();

                                    //        mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP1001-1" + i.ToString(), Order);

                                    //        //if (AseguradoP)
                                    //        //{
                                    //            //PAP1001-2
                                    //            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PAP1001-2.pdf");
                                    //            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1001-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                                    //            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1001-2" + taskControl.TaskControlID.ToString() + ".pdf");
                                    //     //   }
                                    //    }
                                    //}
                                }

                                if (taskControl.Term == 12 && Split)
                                {
                                    //mergePaths = ImprimirDinamicamenteAuto(mergePaths, taskControl, "PP0311", false, "", 12);

                                    double SplitPremium = 0.0;
                                    string SplitPorPersona = "";
                                    string SplitPorAccidente = "";
                                    int VehicleCount = 0;
                                    string PolicyNo = "";


                                    ReportParameter rp = new ReportParameter();
                                    ReportParameter rp2 = new ReportParameter();
                                    ReportParameter rp3 = new ReportParameter();
                                    ReportParameter rp4 = new ReportParameter();
                                    ReportParameter rp5 = new ReportParameter();

                                    DataTable dt = null;


                                    for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                                    {
                                        AC2 = (AutoCover)taskControl.AutoCovers[i];

                                        dt = null;
                                        dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);

                                        if (AC2.UninsuredSplitPremium > 0)
                                        {
                                            try
                                            {
                                                SplitPremium = double.Parse(dt.Rows[0]["UninsuredSplitPremium1"].ToString());
                                                SplitPorPersona = dt.Rows[0]["UninsuredSplitDesc1"].ToString().Trim().Split("/".ToCharArray())[0];
                                                SplitPorAccidente = dt.Rows[0]["UninsuredSplitDesc1"].ToString().Trim().Split("/".ToCharArray())[1];
                                                VehicleCount = i + 1;

                                                rp = new ReportParameter("SplitPremium", SplitPremium.ToString());
                                                rp2 = new ReportParameter("SplitPorPersona", SplitPorPersona.ToString());
                                                rp3 = new ReportParameter("SplitPorAccidente", SplitPorAccidente.ToString());
                                                rp4 = new ReportParameter("VehicleCount", VehicleCount.ToString().Trim());
                                                rp5 = new ReportParameter("PolicyNo", taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix.ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                throw new Exception(ex.Message);
                                            }
                                            ReportViewer viewer2 = new ReportViewer();
                                            viewer2.LocalReport.DataSources.Clear();
                                            viewer2.ProcessingMode = ProcessingMode.Local;
                                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0311.rdlc");
                                            viewer2.LocalReport.SetParameters(rp);
                                            viewer2.LocalReport.SetParameters(rp2);
                                            viewer2.LocalReport.SetParameters(rp3);
                                            viewer2.LocalReport.SetParameters(rp4);
                                            viewer2.LocalReport.SetParameters(rp5);
                                            viewer2.LocalReport.Refresh();

                                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0311" + i.ToString(), Order);
                                        }
                                    }
                                }



                                // Reporte PP0401
                                if (taskControl.Term == 12 && LimResponsabilidad)
                                {

                                    DataTable dt = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);
                                    for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                                    {
                                        AC2 = (AutoCover)taskControl.AutoCovers[i];

                                        string UninsuredSingleDesc = "";
                                        double Usd = 0;

                                        UninsuredSingleDesc = dt.Rows[i]["UninsuredSingleDesc1"].ToString().Trim();

                                        if (UninsuredSingleDesc != "")
                                            Usd = double.Parse(UninsuredSingleDesc.ToString().Trim());

                                        if (Usd > 0)
                                        {
                                            ReportParameter rp = new ReportParameter();
                                            ReportParameter rp2 = new ReportParameter();

                                            try
                                            {
                                                rp = new ReportParameter("LimiteResponsabilidad", Usd.ToString().Trim());
                                                rp2 = new ReportParameter("PolicyNo", taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix.ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                throw new Exception(ex.Message);
                                            }
                                            ReportViewer viewer2 = new ReportViewer();
                                            viewer2.LocalReport.DataSources.Clear();
                                            viewer2.ProcessingMode = ProcessingMode.Local;
                                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0401.rdlc");
                                            viewer2.LocalReport.SetParameters(rp);
                                            viewer2.LocalReport.SetParameters(rp2);
                                            viewer2.LocalReport.Refresh();

                                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0401" + i.ToString(), Order);
                                        }
                                    }
                                }//PP0401
                            }// If AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP 2
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        }// 
                    } // End Liability
                }// end if AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP 1
                //////////////////////////////////////////////////////////////
                #endregion

                #region FULL COVER
                /////////////////////////////////////////////////////// FULL COVER ///////////////////////////////////////////////

                else if (taskControl.Term == 12 && IsLiability == false)
                {
                    if (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP)
                    {
                        ////                 MANTATORIO

                        bool Assistance = false;
                        bool IsEmployee = false;
                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            if (AC2.AssistancePremium > 0)
                                Assistance = true;

                        }

                        if (Assistance)
                        {
                            DataTable dtIsEmployee = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);
                          
                           // mergePaths = ImprimirRoadAssistance(mergePaths, taskControl, AC2.QuotesAutoId, copy, Order);
                            
                            
                            for (int i = 0; i < dtIsEmployee.Rows.Count; i++)
                            {
                                if (dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim() != "")
                                {
                                    IsEmployee = bool.Parse(dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim());
                                }
                            }

                            //mergePaths = PrintMic1005Auto(mergePaths, taskControl, Order, copy);

                            //if (IsEmployee)
                            //{
                            //    //MIC1005-2
                            //    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/POLITICA-CONDICIONES-GENERALES.pdf");
                            //    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf");

                            //}
                            //else
                            //{
                            //    //MIC1005-2
                            //    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/MIC1005-2.pdf");
                            //    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf");
                            //}
                        }

                        if (AseguradoP)
                        {
                            //PP0001
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0001.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0001" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0001" + taskControl.TaskControlID.ToString() + ".pdf");


                            //PP0170
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0170.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0170" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0170" + taskControl.TaskControlID.ToString() + ".pdf");
                        }
                        //Reporte PP0305

                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);
                            DataTable dt2 = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                            if (AC2.Bank != "000")
                            {
                                if (double.Parse(dt.Rows[0]["CollisionPremium1"].ToString().Trim()) > 0)
                                {
                                    string Banco = "";

                                    try
                                    {
                                        Banco = dt.Rows[0]["Bank1"].ToString().Trim();

                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }

                                    // Reporte AutoPersonales/PP0305
                                    ReportParameter rp1 = new ReportParameter();
                                    rp1 = new ReportParameter("Banco", Banco);

                                    ReportViewer viewer1 = new ReportViewer();

                                    viewer1.LocalReport.DataSources.Clear();
                                    viewer1.ProcessingMode = ProcessingMode.Local;
                                    viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0305.rdlc");

                                    viewer1.LocalReport.SetParameters(rp1);

                                    viewer1.LocalReport.Refresh();

                                    mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP0305" + i.ToString(), Order);
                                }

                            }
                        }

                        //NO SE ANADIO EN LO QUE ENVIARON
                        if (1 == 0)
                        {
                            // Reporte PP0308
                            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            {
                                AC2 = (AutoCover)taskControl.AutoCovers[i];

                                // DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId);

                                //if (double.Parse(dt.Rows[0]["CollisionPremium1"].ToString().Trim()) > 0)
                                //{
                                GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy4ByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy4ByTaskControlIDTableAdapter();

                                ReportDataSource rds1 = new ReportDataSource();
                                try
                                {
                                    rds1 = new ReportDataSource("GetReportAutoPolicy4ByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }

                                ReportViewer viewer2 = new ReportViewer();
                                viewer2.LocalReport.DataSources.Clear();
                                viewer2.ProcessingMode = ProcessingMode.Local;
                                viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0308.rdlc");
                                viewer2.LocalReport.DataSources.Add(rds1);
                                viewer2.LocalReport.Refresh();

                                mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0308" + i.ToString(), Order);
                                // IsLiability = false;
                                break;
                                // }
                            }
                        }

                        //TODO FALTA PP0309

                        if (AseguradoP)
                        {
                            //PP0326
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0326.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0326" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0326" + taskControl.TaskControlID.ToString() + ".pdf");
                        }
                        //PP0369
                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];
                            if (AC2.CombinedSingleLimit > 0)
                            {

                                string PolicyNo = "";
                                string Pca = "";

                                DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);

                                try
                                {
                                    Pca = dt.Rows[0]["combinedSingleLimitDesc1"].ToString().Trim();
                                    PolicyNo = taskControl.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix;
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }

                                ReportParameter rp = new ReportParameter();
                                ReportParameter rp2 = new ReportParameter();

                                rp = new ReportParameter("PolicyNo", PolicyNo);
                                rp2 = new ReportParameter("PorCadaAccidente", double.Parse(Pca).ToString());

                                ReportViewer viewer1 = new ReportViewer();

                                viewer1.LocalReport.DataSources.Clear();
                                viewer1.ProcessingMode = ProcessingMode.Local;
                                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0369.rdlc");

                                viewer1.LocalReport.SetParameters(rp);
                                viewer1.LocalReport.SetParameters(rp2);

                                viewer1.LocalReport.Refresh();

                                mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP0369" + i.ToString(), Order);
                            }
                        }
                        // END PP0369
                        if (AseguradoP)
                        {
                            //NO SE ANADIO EN LO QUE ENVIARON
                            if (1 == 0)
                            {
                                //PP1301
                                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP1301.pdf");
                                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1301" + taskControl.TaskControlID.ToString() + ".pdf", true);
                                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1301" + taskControl.TaskControlID.ToString() + ".pdf");


                                //PP1379
                                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP1379.pdf");
                                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1379" + taskControl.TaskControlID.ToString() + ".pdf", true);
                                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1379" + taskControl.TaskControlID.ToString() + ".pdf");


                                //IL0024
                                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/IL0024.pdf");
                                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0024" + taskControl.TaskControlID.ToString() + ".pdf", true);
                                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0024" + taskControl.TaskControlID.ToString() + ".pdf");
                            }

                            //IL0901
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/IL0901.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0901" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "IL0901" + taskControl.TaskControlID.ToString() + ".pdf");
                        }
                        //CIP1031

                        //for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        //{
                        //    AC2 = (AutoCover)taskControl.AutoCovers[i];
                        //    DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId);
                        //    DataTable dt2 = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                        //    if (AC2.Bank != "000")
                        //    {
                        //        string Banco = "";
                        //        string CustomerName = "";
                        //        string VIN = "";
                        //        string PoliceNo = "";
                        //        string ExpDate = "";
                        //        string NameAsegurado = "";
                        //        string EffDate = "";
                        //        string EmissionDate = "";
                        //        string Agency = "";
                        //        string AgentID = "";
                        //        DateTime dateTime;
                        //        try
                        //        {
                        //            Banco = dt.Rows[0]["Bank1"].ToString().Trim();
                        //            CustomerName = dt2.Rows[i]["NameCustomer"].ToString().Trim();
                        //            VIN = dt.Rows[0]["VehicleYearDesc1"].ToString().Trim() + " " + dt.Rows[0]["vehicleMakeDesc1"].ToString().Trim() + " " + dt.Rows[0]["VehicleModelDesc1"].ToString().Trim() + " " + "Vin#(" + dt.Rows[0]["identificationNo2"].ToString().Trim() + ")";
                        //            //PoliceNo = dt2.Rows[i]["PolicyNo"].ToString().Trim();
                        //            PoliceNo = taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix;
                        //            NameAsegurado = dt2.Rows[i]["ConducName"].ToString().Trim();

                        //            dateTime = Convert.ToDateTime(dt2.Rows[0]["ExpirationDate"].ToString().Trim());
                        //            ExpDate = dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + dateTime.Year.ToString(); ;

                        //            dateTime = Convert.ToDateTime(dt2.Rows[0]["EffectiveDate"].ToString().Trim());
                        //            EffDate = dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + dateTime.Year.ToString();

                        //            dateTime = Convert.ToDateTime(dt2.Rows[0]["EmisionDate"].ToString().Trim());
                        //            EmissionDate = dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + dateTime.Year.ToString();

                        //            AgentID = dt.Rows[0]["AgentIDNumber"].ToString().Trim();


                        //            Agency = dt2.Rows[0]["AgencyDesc"].ToString().Trim();
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            throw new Exception(ex.Message);
                        //        }

                        //        // Reporte AutoPersonales/CIP1031.rdlc

                        //        ReportParameter rp1 = new ReportParameter();
                        //        ReportParameter rp2 = new ReportParameter();
                        //        ReportParameter rp3 = new ReportParameter();
                        //        ReportParameter rp4 = new ReportParameter();
                        //        ReportParameter rp5 = new ReportParameter();
                        //        ReportParameter rp6 = new ReportParameter();
                        //        ReportParameter rp7 = new ReportParameter();
                        //        ReportParameter rp8 = new ReportParameter();
                        //        ReportParameter rp9 = new ReportParameter();

                        //        try
                        //        {
                        //            rp1 = new ReportParameter("CustomerName", CustomerName);
                        //            rp2 = new ReportParameter("VIN", VIN);
                        //            rp3 = new ReportParameter("PoliceNo", PoliceNo);
                        //            rp4 = new ReportParameter("ExpDate", ExpDate);
                        //            rp5 = new ReportParameter("NameAsegurado", NameAsegurado);
                        //            rp6 = new ReportParameter("EffDate", EffDate);
                        //            rp7 = new ReportParameter("EmissionDate", EmissionDate);
                        //            rp8 = new ReportParameter("Agency", Agency);
                        //            rp9 = new ReportParameter("AgentID", AgentID);

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            throw new Exception(ex.Message);
                        //        }
                        //        ReportViewer viewer1 = new ReportViewer();
                        //        viewer1.Reset();
                        //        viewer1.LocalReport.DataSources.Clear();
                        //        viewer1.ProcessingMode = ProcessingMode.Local;
                        //        viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/CIP1031.rdlc");

                        //        viewer1.LocalReport.SetParameters(rp1);
                        //        viewer1.LocalReport.SetParameters(rp2);
                        //        viewer1.LocalReport.SetParameters(rp3);
                        //        viewer1.LocalReport.SetParameters(rp4);
                        //        viewer1.LocalReport.SetParameters(rp5);
                        //        viewer1.LocalReport.SetParameters(rp6);
                        //        viewer1.LocalReport.SetParameters(rp7);
                        //        viewer1.LocalReport.SetParameters(rp8);
                        //        viewer1.LocalReport.SetParameters(rp9);

                        //        viewer1.LocalReport.Refresh();

                        //        mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "CIP1031" + i.ToString(), Order);

                        //    }// CIP1031
                        //}
                        //if (AseguradoP)
                        //{
                        //    //NIC1000
                        //    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/NIC1000.pdf");
                        //    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NIC1000" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        //    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NIC1000" + taskControl.TaskControlID.ToString() + ".pdf");
                        //}

                        //PP0001B

                        //for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        //{
                        //    AC2 = (AutoCover)taskControl.AutoCovers[i];

                        //    if (AC2.CombinedSingleLimit > 0)
                        //    {

                        //        string Asegurado = "";
                        //        string PoliceNo = "";
                        //        DateTime dateTime;
                        //        string Agency = "";
                        //        string EffDate = "";

                        //        DataTable dt = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                        //        try
                        //        {
                        //            PoliceNo = taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix;
                        //            Asegurado = dt.Rows[0]["NameCustomer"].ToString().Trim();
                        //            dateTime = Convert.ToDateTime(dt.Rows[0]["EffectiveDate"].ToString().Trim());
                        //            EffDate = dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + dateTime.Year.ToString();
                        //            Agency = dt.Rows[0]["AgencyDesc"].ToString().Trim();

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            throw new Exception(ex.Message);
                        //        }

                        //        ReportParameter rp = new ReportParameter();
                        //        ReportParameter rp2 = new ReportParameter();
                        //        ReportParameter rp3 = new ReportParameter();
                        //        ReportParameter rp4 = new ReportParameter();

                        //        rp = new ReportParameter("PolicyNo", PoliceNo);
                        //        rp2 = new ReportParameter("Asegurado", Asegurado);
                        //        rp3 = new ReportParameter("EffDate", EffDate);
                        //        rp4 = new ReportParameter("Agency", Agency);

                        //        ReportViewer viewer1 = new ReportViewer();

                        //        viewer1.LocalReport.DataSources.Clear();
                        //        viewer1.ProcessingMode = ProcessingMode.Local;
                        //        viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0001B.rdlc");

                        //        viewer1.LocalReport.SetParameters(rp);
                        //        viewer1.LocalReport.SetParameters(rp2);
                        //        viewer1.LocalReport.SetParameters(rp3);
                        //        viewer1.LocalReport.SetParameters(rp4);

                        //        viewer1.LocalReport.Refresh();

                        //        mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP0001B" + i.ToString(), Order);


                        //        break;
                        //    }
                        //}
                        if (AseguradoP)
                        {
                            ////PAP1000
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PAP1000.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1000" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1000" + taskControl.TaskControlID.ToString() + ".pdf");


                            ////PAACLL
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PA-ACLL.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PA-ACLL" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PA-ACLL" + taskControl.TaskControlID.ToString() + ".pdf");
                        }

                        bool IsAccidentalDeath = false;
                        bool Equipment = false;
                        bool AutoRental = false;
                        bool AutoTransporte = false;
                        bool AutoRemolque = false;
                        bool EquipoEspeciales = false;
                        bool Split = false;
                        bool LimResponsabilidad = false;

                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);

                            if (double.Parse(dt.Rows[0]["AccidentalDeathPremium1"].ToString().Trim()) > 0)
                                IsAccidentalDeath = true;
                            if (double.Parse(dt.Rows[0]["EquipmentSoundPremium1"].ToString().Trim()) > 0 || double.Parse(dt.Rows[0]["EquipmentAudioPremium1"].ToString().Trim()) > 0 ||
                                double.Parse(dt.Rows[0]["EquipmentTapesPremium1"].ToString()) > 0)
                                Equipment = true;
                            if (double.Parse(dt.Rows[0]["VehicleRental1"].ToString().Trim()) > 0)
                                AutoTransporte = true;
                            if ((double.Parse(dt.Rows[0]["SpecialEquipmentCollPremium1"].ToString().Trim()) + double.Parse(dt.Rows[0]["SpecialEquipmentCompPremium1"].ToString().Trim())) > 0)
                                EquipoEspeciales = true;
                            if (double.Parse(dt.Rows[0]["UninsuredSinglePremium1"].ToString().Trim()) > 0)
                                LimResponsabilidad = true;
                            if (double.Parse(dt.Rows[0]["Arrendamiento1Prima"].ToString().Trim()) > 0)
                                AutoRental = true;
                            if (double.Parse(dt.Rows[0]["AssistancePremium1"].ToString().Trim()) > 0)
                                Assistance = true;
                            if (double.Parse(dt.Rows[0]["TowingPremium1"].ToString().Trim()) > 0)
                                AutoRemolque = true;
                            //if (AC2.AccidentalDeathPremium > 0)
                            //    IsAccidentalDeath = true;
                            //if (AC2.EquipmentAudioPremium > 0 || AC2.EquipmentSoundPremium > 0 || AC2.EquipmentTapesPremium > 0)
                            //    Equipment = true;
                            //if (AC2.VehicleRental > 0)
                            //    AutoTransporte = true;
                            //if (AC2.TowingPremium > 0)
                            //    AutoRemolque = true;
                            //if ((AC2.SpecialEquipmentCollPremium + AC2.SpecialEquipmentCompPremium) > 0)
                            //    EquipoEspeciales = true;
                            //if (AC2.UninsuredSplitPremium > 0)
                            //    Split = true;
                            //if (AC2.UninsuredSinglePremium > 0)
                            //    LimResponsabilidad = true;
                            //if (AC2.LeaseLoanGapId > 0)
                            //    AutoRental = true;
                            //if (AC2.AssistancePremium > 0)
                            //    Assistance = true;

                        }



                        //PAP1001

                        if (IsAccidentalDeath)
                        {
                            string CustomerName = "";
                            string AccidentDeathDesc = "";
                            string AccidentDeathPrima = "";


                            DataTable dt = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);
                            DataTable dt2 = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            {
                                AC2 = (AutoCover)taskControl.AutoCovers[i];


                                if (double.Parse(dt.Rows[0]["AccidentalDeathPremium1"].ToString().Trim()) > 0)
                                {
                                    CustomerName = dt2.Rows[0]["ConducName"].ToString().Trim();
                                    AccidentDeathDesc = dt.Rows[0]["AccidentalDeathPerson1"].ToString().Trim();
                                    AccidentDeathPrima = dt.Rows[0]["AccidentalDeathPremium1"].ToString().Trim();

                                    ReportParameter rp1 = new ReportParameter();
                                    ReportParameter rp2 = new ReportParameter();
                                    ReportParameter rp3 = new ReportParameter();

                                    try
                                    {
                                        rp1 = new ReportParameter("CustomerName", CustomerName);
                                        rp2 = new ReportParameter("AccidentDeathDesc", AccidentDeathDesc);
                                        rp3 = new ReportParameter("AccidentDeathPrima", AccidentDeathPrima);
                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                    ReportViewer viewer1 = new ReportViewer();
                                    viewer1.LocalReport.DataSources.Clear();
                                    viewer1.ProcessingMode = ProcessingMode.Local;
                                    viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PAP1001-1.rdlc");
                                    viewer1.LocalReport.SetParameters(rp1);
                                    viewer1.LocalReport.SetParameters(rp2);
                                    viewer1.LocalReport.SetParameters(rp3);
                                    viewer1.LocalReport.Refresh();

                                    mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP1001-1" + i.ToString(), Order);

                                    //PAP1001-2
                                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PAP1001-2.pdf");
                                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1001-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PAP1001-2" + taskControl.TaskControlID.ToString() + ".pdf");

                                }
                            }
                        }

                        if (Equipment)
                        {
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByEquitmentTableAdapter ds = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByEquitmentTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();


                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds4 = new ReportDataSource();
                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDByEquitment", (DataTable)ds.GetData(taskControl.TaskControlID));
                                rds4 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();
                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0313.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds4);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0313" + "0", Order);

                            //PP0313-2 PDF
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0313-2.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0313-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0313-2" + taskControl.TaskControlID.ToString() + ".pdf");

                        }// equipment

                        if (AutoRental)//Reporte PP0335
                        {

                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();

                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRentalTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRentalTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDByAutoRental", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                            ReportViewer viewer2 = new ReportViewer();
                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0335.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0335" + "0", Order);
                        }// auto rental

                        if (AutoTransporte) // Imprimir PAP0302
                        {
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoTransporteTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoTransporteTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();

                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDByAutoTransporte", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();

                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0302.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0302" + "0", Order);
                        }// auto transporte

                        if (AutoRemolque)// Reporte PP0303
                        {
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRemolqueTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRemolqueTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();

                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDByAutoRemolque", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();

                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0303.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0303" + "0", Order);
                        }// auto remolque

                        // Reporte PP0344
                        if (EquipoEspeciales && taskControl.Term == 12)
                        {
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDBySpecialEquipmentTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDBySpecialEquipmentTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();

                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDBySpecialEquipment", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();

                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0344.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0344" + "0", Order);

                            //PP0344-2
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0344-2.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0344-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0344-2" + taskControl.TaskControlID.ToString() + ".pdf");


                        }// PP0304

                        if (taskControl.Term == 12 && Split)
                        {
                            //mergePaths = ImprimirDinamicamenteAuto(mergePaths, taskControl, "PP0311", false, "", 12);

                            double SplitPremium = 0.0;
                            string SplitPorPersona = "";
                            string SplitPorAccidente = "";
                            int VehicleCount = 0;
                            string PolicyNo = "";


                            ReportParameter rp = new ReportParameter();
                            ReportParameter rp2 = new ReportParameter();
                            ReportParameter rp3 = new ReportParameter();
                            ReportParameter rp4 = new ReportParameter();
                            ReportParameter rp5 = new ReportParameter();

                            DataTable dt = null;


                            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            {
                                AC2 = (AutoCover)taskControl.AutoCovers[i];

                                dt = null; dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);

                                if (AC2.UninsuredSplitPremium > 0)
                                {
                                    try
                                    {
                                        SplitPremium = double.Parse(dt.Rows[0]["UninsuredSplitPremium1"].ToString());
                                        SplitPorPersona = dt.Rows[0]["UninsuredSplitDesc1"].ToString().Trim().Split("/".ToCharArray())[0];
                                        SplitPorAccidente = dt.Rows[0]["UninsuredSplitDesc1"].ToString().Trim().Split("/".ToCharArray())[1];
                                        VehicleCount = i + 1;

                                        rp = new ReportParameter("SplitPremium", SplitPremium.ToString());
                                        rp2 = new ReportParameter("SplitPorPersona", SplitPorPersona.ToString());
                                        rp3 = new ReportParameter("SplitPorAccidente", SplitPorAccidente.ToString());
                                        rp4 = new ReportParameter("VehicleCount", VehicleCount.ToString().Trim());
                                        rp5 = new ReportParameter("PolicyNo", taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }
                                    ReportViewer viewer2 = new ReportViewer();
                                    viewer2.LocalReport.DataSources.Clear();
                                    viewer2.ProcessingMode = ProcessingMode.Local;
                                    viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0311.rdlc");
                                    viewer2.LocalReport.SetParameters(rp);
                                    viewer2.LocalReport.SetParameters(rp2);
                                    viewer2.LocalReport.SetParameters(rp3);
                                    viewer2.LocalReport.SetParameters(rp4);
                                    viewer2.LocalReport.SetParameters(rp5);
                                    viewer2.LocalReport.Refresh();

                                    mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0311" + i.ToString(), Order);
                                }
                            }
                        } // end split 


                        // Reporte PP0401
                        if (taskControl.Term == 12 && LimResponsabilidad)
                        {

                            DataTable dt = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);

                            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                            {
                                AC2 = (AutoCover)taskControl.AutoCovers[i];

                                string UninsuredSingleDesc = "";
                                double Usd = 0;

                                UninsuredSingleDesc = dt.Rows[i]["UninsuredSingleDesc1"].ToString().Trim();

                                if (UninsuredSingleDesc != "")
                                    Usd = double.Parse(UninsuredSingleDesc.ToString().Trim());

                                if (Usd > 0)
                                {
                                    ReportParameter rp = new ReportParameter();
                                    ReportParameter rp2 = new ReportParameter();

                                    try
                                    {
                                        rp = new ReportParameter("LimiteResponsabilidad", Usd.ToString().Trim());
                                        rp2 = new ReportParameter("PolicyNo", taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix.ToString().Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }
                                    ReportViewer viewer2 = new ReportViewer();
                                    viewer2.LocalReport.DataSources.Clear();
                                    viewer2.ProcessingMode = ProcessingMode.Local;
                                    viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0401.rdlc");
                                    viewer2.LocalReport.SetParameters(rp);
                                    viewer2.LocalReport.SetParameters(rp2);
                                    viewer2.LocalReport.Refresh();

                                    mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0401" + i.ToString(), Order);
                                }
                            }// PP0401
                        }
                    }// end if AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP

                } // end full cover

                /////////////////////////// END FULL COVER

                #endregion

                #region DOUBLE INTEREST
                ////////////////////////////////////////////// DOUBLE INTEREST /////////////////////////////////////////////////////
                else if (taskControl.Term > 12 && IsLiability == false)
                {
                    if (AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP)
                    {
                        ////                 MANTATORIO

                        bool Assistance = false;
                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            if (AC2.AssistancePremium > 0)
                                Assistance = true;

                        }

                        //MIC1005

                        //if (Assistance)
                        //{
                        //    mergePaths = PrintMic1005Auto(mergePaths, taskControl, Order, copy);
                        //}

                        //fin del metodo de MIC1005
                        if (AseguradoP)
                        {
                            //PP0001
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0001.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0001" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0001" + taskControl.TaskControlID.ToString() + ".pdf");

                            //PP0170
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0170.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0170" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0170" + taskControl.TaskControlID.ToString() + ".pdf");
                        }
                        //Reporte PP0305

                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1);
                            DataTable dt2 = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                            if (AC2.Bank != "000")
                            {
                                if (double.Parse(dt.Rows[0]["CollisionPremium1"].ToString().Trim()) > 0)
                                {
                                    string Banco = "";

                                    try
                                    {
                                        Banco = dt.Rows[0]["Bank1"].ToString().Trim();

                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }

                                    // Reporte AutoPersonales/PP0305
                                    ReportParameter rp1 = new ReportParameter();
                                    rp1 = new ReportParameter("Banco", Banco);

                                    ReportViewer viewer1 = new ReportViewer();

                                    viewer1.LocalReport.DataSources.Clear();
                                    viewer1.ProcessingMode = ProcessingMode.Local;
                                    viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0305.rdlc");

                                    viewer1.LocalReport.SetParameters(rp1);

                                    viewer1.LocalReport.Refresh();

                                    mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP0305" + i.ToString(), Order);
                                }

                            }
                        }// PP0305


                        // Reporte PP0308
                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            // DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId);

                            //if (double.Parse(dt.Rows[0]["CollisionPremium1"].ToString().Trim()) > 0)
                            //{
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy4ByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy4ByTaskControlIDTableAdapter();

                            ReportDataSource rds1 = new ReportDataSource();
                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy4ByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();
                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0308.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0308" + i.ToString(), Order);
                            IsLiability = false;
                            break;
                            // }
                        }//PP0308
                        if (AseguradoP)
                        {
                            //PP0379
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP0379.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0379" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP0379" + taskControl.TaskControlID.ToString() + ".pdf");

                            //PP1301
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP1301.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1301" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1301" + taskControl.TaskControlID.ToString() + ".pdf");

                            //PP1379
                            mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PP1379.pdf");
                            mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1379" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PP1379" + taskControl.TaskControlID.ToString() + ".pdf");

                            ////NIC1000
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/NIC1000.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NIC1000" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "NIC1000" + taskControl.TaskControlID.ToString() + ".pdf");
                        }

                        //PP0001B

                        //for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        //{
                        //    AC2 = (AutoCover)taskControl.AutoCovers[i];

                        //    if (AC2.CombinedSingleLimit > 0)
                        //    {

                        //        string Asegurado = "";
                        //        string PoliceNo = "";
                        //        DateTime dateTime;
                        //        string Agency = "";
                        //        string EffDate = "";

                        //        DataTable dt = GetReportbAutoQuoteByTaskControlID(taskControl.TaskControlID);

                        //        try
                        //        {
                        //            PoliceNo = taskControl.Policy.PolicyType + " " + taskControl.Policy.PolicyNo + "-" + taskControl.Policy.Suffix;
                        //            Asegurado = dt.Rows[0]["NameCustomer"].ToString().Trim();
                        //            dateTime = Convert.ToDateTime(dt.Rows[0]["EffectiveDate"].ToString().Trim());
                        //            EffDate = dateTime.Month.ToString() + "/" + dateTime.Day.ToString() + "/" + dateTime.Year.ToString();
                        //            Agency = dt.Rows[0]["AgencyDesc"].ToString().Trim();

                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            throw new Exception(ex.Message);
                        //        }

                        //        ReportParameter rp = new ReportParameter();
                        //        ReportParameter rp2 = new ReportParameter();
                        //        ReportParameter rp3 = new ReportParameter();
                        //        ReportParameter rp4 = new ReportParameter();

                        //        rp = new ReportParameter("PolicyNo", PoliceNo);
                        //        rp2 = new ReportParameter("Asegurado", Asegurado);
                        //        rp3 = new ReportParameter("EffDate", EffDate);
                        //        rp4 = new ReportParameter("Agency", Agency);

                        //        ReportViewer viewer1 = new ReportViewer();

                        //        viewer1.LocalReport.DataSources.Clear();
                        //        viewer1.ProcessingMode = ProcessingMode.Local;
                        //        viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0001B.rdlc");

                        //        viewer1.LocalReport.SetParameters(rp);
                        //        viewer1.LocalReport.SetParameters(rp2);
                        //        viewer1.LocalReport.SetParameters(rp3);
                        //        viewer1.LocalReport.SetParameters(rp4);

                        //        viewer1.LocalReport.Refresh();

                        //        mergePaths = WriteFileName(mergePaths, taskControl, viewer1, "PP0001B" + i.ToString(), Order);


                        //        break;
                        //    }
                        //}//PP0001B

                        if (AseguradoP)
                        {
                            ////PAACLL
                            //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/PA-ACLL.pdf");
                            //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PA-ACLL" + taskControl.TaskControlID.ToString() + ".pdf", true);
                            //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "PA-ACLL" + taskControl.TaskControlID.ToString() + ".pdf");
                        }

                        bool IsAccidentalDeath = false;
                        bool Equipment = false;
                        bool AutoRental = false;
                        bool AutoTransporte = false;
                        bool AutoRemolque = false;
                        bool EquipoEspeciales = false;
                        bool Split = false;
                        bool LimResponsabilidad = false;


                        for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                        {
                            AC2 = (AutoCover)taskControl.AutoCovers[i];

                            if (AC2.AccidentalDeathPremium > 0)
                                IsAccidentalDeath = true;
                            if (AC2.EquipmentAudioPremium > 0 || AC2.EquipmentSoundPremium > 0 || AC2.EquipmentTapesPremium > 0)
                                Equipment = true;
                            if (AC2.VehicleRental > 0)
                                AutoTransporte = true;
                            if (AC2.TowingPremium > 0)
                                AutoRemolque = true;
                            if ((AC2.SpecialEquipmentCollPremium + AC2.SpecialEquipmentCompPremium) > 0)
                                EquipoEspeciales = true;
                            if (AC2.UninsuredSplitPremium > 0)
                                Split = true;
                            if (AC2.UninsuredSinglePremium > 0)
                                LimResponsabilidad = true;
                            if (AC2.LeaseLoanGapId > 0)
                                AutoRental = true;
                            if (AC2.AssistancePremium > 0)
                                Assistance = true;

                        }

                        //MIC1005
                        if (Assistance)
                        {
                            ImprimirRoadAssistance(mergePaths, taskControl, AC2.QuotesAutoId, copy, Order);

                        //   // mergePaths = PrintMic1005Auto(mergePaths, taskControl, Order, copy);

                        //    //MIC Para Double Interest
                        //    GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
                        //    GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDAssistanceTableAdapter ds11 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDAssistanceTableAdapter();

                        //    ReportDataSource rds;
                        //    ReportDataSource rds11;
                        //    ReportParameter rpd = new ReportParameter("Sufijo", taskControl.Policy.Suffix);
                        //    ReportParameter rp2 = new ReportParameter("Copy", copy);
                        //    try
                        //    {
                        //        rds = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds.GetData(taskControl.TaskControlID));
                        //        rds11 = new ReportDataSource("GetReportAutoPolicyByTaskControlIDAssistance", (DataTable)ds11.GetData(taskControl.TaskControlID));
                        //    }

                        //    catch (Exception ex)
                        //    {
                        //        throw new Exception(ex.Message);
                        //    }

                        //    ReportViewer viewer = new ReportViewer();
                        //    viewer.LocalReport.DataSources.Clear();
                        //    viewer.ProcessingMode = ProcessingMode.Local;
                        //    viewer.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/MicAutoDoubleInterest.rdlc");
                        //    viewer.LocalReport.DataSources.Add(rds);
                        //    viewer.LocalReport.DataSources.Add(rds11);
                        //    viewer.LocalReport.SetParameters(rpd);
                        //    viewer.LocalReport.SetParameters(rp2);
                        //    viewer.LocalReport.Refresh();

                        //    mergePaths = WriteFileName(mergePaths, taskControl, viewer, "MicAutoDoubleInterest", Order);



                        //    bool IsEmployee = false;

                        //    DataTable dtIsEmployee = GetReportbAutoQuote4ByTaskControlID(taskControl.TaskControlID);

                        //    for (int i = 0; i < dtIsEmployee.Rows.Count; i++)
                        //    {
                        //        if (dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim() != "")
                        //        {
                        //            IsEmployee = bool.Parse(dtIsEmployee.Rows[i]["IsAssistanceEmp"].ToString().Trim());
                        //        }
                        //    }

                        //    if (IsEmployee)
                        //    {
                        //        //MIC1005-2
                        //        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/POLITICA-CONDICIONES-GENERALES.pdf");
                        //        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        //        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "POLITICA-CONDICIONES-GENERALES" + taskControl.TaskControlID.ToString() + ".pdf");

                        //    }
                        //    else
                        //    {
                        //        //MIC1005-2
                        //        mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/AutoPersonales/MIC1005-2.pdf");
                        //        mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        //        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "MIC1005-2" + taskControl.TaskControlID.ToString() + ".pdf");
                        //    }
                        }
                        //fin del metodo de MIC1005

                        if (AutoRental)//Reporte PP0335
                        {

                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();

                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRentalTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRentalTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDByAutoRental", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                            ReportViewer viewer2 = new ReportViewer();
                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0335.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0335" + "0", Order);
                        }//AutoRental

                        if (AutoTransporte) // Imprimir PAP0302
                        {
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoTransporteTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoTransporteTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();

                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDByAutoTransporte", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();

                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0302DI.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0302DI" + "0", Order);
                        }//AutoTransporte

                        if (AutoRemolque)// Reporte PP0303
                        {
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRemolqueTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDByAutoRemolqueTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();

                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();

                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDAutoRemolque", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();

                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0303DI.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "PP0303" + "0", Order);
                        }// Auto Remolque

                    }// end if AseguradoP || AgencyP || CompanyP || ProductorP || LossPayeP
                }
                //////////////////////////////////////////////  END DOUBLE INTEREST /////////////////////////////////////////////////////
                #endregion

                if (AgencyP && taskControl.Policy.TCIDQuotes != 0)
                {
                    //mergePaths = ImprimirAutoQuote(mergePaths, taskControl.Policy.TCIDQuotes);
                    
                }

                //NO SE ANADIO EN LO QUE ENVIARON
                if (1 == 0)
                {
                    // Liability Report
                    // Reporte Liability (Certificado)
                    for (int i = 0; i < taskControl.AutoCovers.Count; i++)
                    {
                        AC2 = (AutoCover)taskControl.AutoCovers[i];

                        // DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC2.QuotesAutoId);

                        //if (double.Parse(dt.Rows[0]["CollisionPremium1"].ToString().Trim()) > 0)
                        //{
                        if (AC2.Bank != "000")
                        {
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
                            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDLiabilityTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDLiabilityTableAdapter();
                            ReportDataSource rds1 = new ReportDataSource();
                            ReportDataSource rds2 = new ReportDataSource();
                            try
                            {
                                rds1 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                                rds2 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDLiability", (DataTable)ds2.GetData(taskControl.TaskControlID, AC2.QuotesAutoId, i + 1));
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            ReportViewer viewer2 = new ReportViewer();
                            viewer2.LocalReport.DataSources.Clear();
                            viewer2.ProcessingMode = ProcessingMode.Local;
                            viewer2.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/Liability.rdlc");
                            viewer2.LocalReport.DataSources.Add(rds1);
                            viewer2.LocalReport.DataSources.Add(rds2);
                            viewer2.LocalReport.Refresh();

                            mergePaths = WriteFileName(mergePaths, taskControl, viewer2, "Liability" + i.ToString(), Order);

                            //break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return mergePaths;
        }
        private List<string> ImprimirAutoQuote(List<string> mergePaths, int taskControl)
        {
            try
            {
                // string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

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
        private List<string> PrintMic1005Auto(List<string> mergePaths, EPolicy.TaskControl.QuoteAuto taskControl, int Order, string copy)
        {
            FileInfo mFileIndex;
            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDAssistanceTableAdapter ds11 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDAssistanceTableAdapter();

            ReportDataSource rds;
            ReportDataSource rds11;
            ReportParameter rpd = new ReportParameter("Sufijo", taskControl.Policy.Suffix);
            ReportParameter rp2 = new ReportParameter("Copy", copy);
            try
            {
                rds = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds.GetData(taskControl.TaskControlID));
                rds11 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlIDAssistance", (DataTable)ds11.GetData(taskControl.TaskControlID));
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.DataSources.Clear();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/MIC1005Auto.rdlc");
            viewer.LocalReport.DataSources.Add(rds);
            viewer.LocalReport.DataSources.Add(rds11);
            viewer.LocalReport.SetParameters(rpd);
            viewer.LocalReport.SetParameters(rp2);
            viewer.LocalReport.Refresh();

            mergePaths = WriteFileName(mergePaths, taskControl, viewer, "MIC1005Auto", Order);


            return mergePaths;
        }
        private bool IsLiabilityValidation(EPolicy.TaskControl.QuoteAuto taskControl)
        {
            bool IsLiability = true;
            AutoCover AC = new AutoCover();

            for (int i = 0; i < taskControl.AutoCovers.Count; i++)
            {
                AC = (AutoCover)taskControl.AutoCovers[i];

                DataTable dt = GetReportbAutoQuote1ByTaskControlID(taskControl.TaskControlID, AC.QuotesAutoId, i + 1);

                if (double.Parse(dt.Rows[0]["CollisionPremium1"].ToString().Trim()) > 0)
                {
                    IsLiability = false;
                    break;
                }

            }
            return IsLiability;
        }

        private List<string> WriteFileName(List<string> mergePaths, EPolicy.TaskControl.QuoteAuto taskControl, ReportViewer viewer1, string FileName, int Order)
        {
            try
            {
                Warning[] warnings1;
                string[] streamIds1;
                string mimeType1;
                string encoding1 = string.Empty;
                string extension1;






                string fileName1 = "PolicyNo- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + FileName.ToString() + Order.ToString();
                string _FileName1 = "PolicyNo- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + FileName.ToString() + Order.ToString() + ".pdf";

                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                    File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

                byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType1, out encoding1, out extension1, out streamIds1, out warnings1);

                using (FileStream fs1 = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1, FileMode.Create))
                {
                    fs1.Write(bytes1, 0, bytes1.Length);
                }

                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

                return mergePaths;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
        private DataTable GetReportbAutoQuote1ByTaskControlID(int taskControlID, int quoteid, int carInt)
        {
            
            DataTable dt = new DataTable();

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControlID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("QuotesAutoID", SqlDbType.Int, 0, quoteid.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("carInt", SqlDbType.Int, 0, carInt.ToString(), ref cookItems);

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

            dt = exec.GetQuery("GetReportAutoPolicy1ByTaskControlID", xmlDoc);

            return dt;
        }

        private DataTable GetReportbAutoQuoteByTaskControlID(int taskControlID)
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

            dt = exec.GetQuery("GetReportAutoPolicyByTaskControlID", xmlDoc);

            return dt;
        }

        protected void btnAuditTrail_Click(object sender, System.EventArgs e)
        {
            if (Session["TaskControl"] != null)
            {
                RemoveSessionLookUp();
                TaskControl.Quote quote = (TaskControl.Quote)Session["TaskControl"];
                Response.Redirect("SearchAuditItems.aspx?type=22&taskControlID=" +
                    quote.TaskControlID.ToString());
            }
        }

        protected void ChkAutoAssignPolicy_CheckedChanged1(object sender, EventArgs e)
        {
            VerifyAssignPolicyFields();
        }

        protected void btnCustInfo_ServerClick(object sender, EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            Customer.Customer customer;
            customer = QA.Customer;

            customer.Mode = 2;    //Update
            Session["Customer"] = customer;
            RemoveSessionLookUp();
            Session.Add("QuoteAuto", "QuoteAuto");  // Para indicar en la pantalla de Customer que tiene que regresar al Application Auto.
            Response.Redirect("ClientIndividual.aspx");
        }
        protected void btnDefferredPayPlan_Click(object sender, EventArgs e)
        {
            LoadFromForm();
            RemoveSessionLookUp();
            Response.Redirect("PagoDiferido.aspx");
        }
        protected void chkDeferred_CheckedChanged(object sender, EventArgs e)
        {
            //VerifyIsDeferred();
        }

        private void VerifyIsDeferred()
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (this.chkDeferred.Checked)
            {
                //this.btnDefferredPayPlan.Enabled = true;
                QA.Policy.IsDeferred = true;
            }
            else
            {
                //this.btnDefferredPayPlan.Enabled = false;
                QA.Policy.IsDeferred = false;
            }
            Session["TaskControl"] = QA;
        }
        protected void chkFBfamilia_CheckedChanged(object sender, EventArgs e)
        {
            VerifyIsFamily();
        }

        private void VerifyIsFamily()
        {
            //TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (this.chkFBfamilia.Checked)
            {
                this.TxtFBEmployeeName.Visible = true;
                this.TxtFBEmployeeName.Enabled = true;
                //QA.Policy.IsFamily = true;
                this.chkFBEmployee.Checked = false;
                this.ddlFBPosition.Visible = false;
                this.ddFBSubsidiary.Visible = false;
                this.ddlFBBranches.Visible = false;
                this.lblSubsidiary.Visible = false;
                this.lblBranches.Visible = false;
                //QA.Policy.IsEmployee = false;
                this.chkFBEmployee.Checked = false;

                this.ddlFBPosition.SelectedIndex = -1;
                this.ddFBSubsidiary.SelectedIndex = -1;
                this.ddlFBBranches.SelectedIndex = -1;
            }
            else
            {
                this.TxtFBEmployeeName.Text = "";
                this.TxtFBEmployeeName.Visible = false;
                //QA.Policy.IsFamily = false;
                this.chkFBEmployee.Checked = false;
            }
            //Session["TaskControl"] = QA;
        }
        protected void chkFBEmployee_CheckedChanged(object sender, EventArgs e)
        {
            VerifyIsEmployee();
        }

        private void VerifyIsEmployee()
        {
            //TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (this.chkFBEmployee.Checked)
            {
                this.ddlFBPosition.Enabled = true;
                this.ddFBSubsidiary.Enabled = true;
                this.ddlFBBranches.Enabled = true;

                this.ddlFBPosition.Visible = true;
                this.ddFBSubsidiary.Visible = true;
                this.ddlFBBranches.Visible = false;
                this.lblSubsidiary.Visible = true;
                this.lblBranches.Visible = false;
                this.TxtFBEmployeeName.Visible = false;
                //QA.Policy.IsFamily = false;
                //QA.Policy.IsEmployee = true;
                this.chkFBfamilia.Checked = false;

                this.ddlFBPosition.SelectedIndex = -1;
                this.ddFBSubsidiary.SelectedIndex = -1;
                this.ddlFBBranches.SelectedIndex = -1;
            }
            else
            {
                this.ddlFBPosition.Visible = false;
                this.ddFBSubsidiary.Visible = false;
                this.ddlFBBranches.Visible = false;
                this.lblSubsidiary.Visible = false;
                this.lblBranches.Visible = false;
                //QA.Policy.IsEmployee = false;

                this.TxtFBEmployeeName.Text = "";
                this.ddlFBPosition.SelectedIndex = -1;
                this.ddFBSubsidiary.SelectedIndex = -1;
                this.ddlFBBranches.SelectedIndex = -1;
            }
            //Session["TaskControl"] = QA;
        }
        protected void ddFBSubsidiary_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerifyBranches();
        }

        private void VerifyBranches()
        {
            if (this.ddFBSubsidiary.SelectedItem.Value.Trim() == "5")
            {
                this.lblBranches.Visible = true;
                this.ddlFBBranches.Visible = true;
            }
            else
            {
                this.lblBranches.Visible = false;
                this.ddlFBBranches.Visible = false;
            }
        }
		
        protected void btnRenew_Click(object sender, EventArgs e)
        {
            try
            {
                btnEdit.Visible = true;
                btnVehicles.Visible = true;
                BtnDrivers.Visible = true;

                Session.Remove("AUTOCHARGE");

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];

                int TCID = taskControl.TaskControlID;

                EPolicy.TaskControl.QuoteAuto taskControlQuote = new EPolicy.TaskControl.QuoteAuto(false);

                int tcID = taskControl.TaskControlID;
                int tcID2 = 0;
                
                //Para aplicar el ultimo endoso, sino a la poliza original
                DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(tcID);
                if (endososList.Rows.Count == 0)
                {
                    taskControlQuote = taskControl;

                    taskControlQuote.Mode = 1; //ADD
                    taskControlQuote.TaskControlID = 0;
                    taskControlQuote.IsPolicy = false;

                    taskControlQuote.Policy.IsEndorsement = false;
                    taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Quote Auto"));
                    taskControlQuote.QuoteId = 0;
                    taskControlQuote.Policy.PolicyNo = taskControl.Policy.PolicyNo;
                    taskControlQuote.Policy.PolicyType = taskControl.Policy.PolicyType;
                    taskControlQuote.PolicyType = taskControl.Policy.PolicyType;
                    taskControlQuote.Policy.Suffix = taskControl.Policy.Suffix;
                    taskControlQuote.EffectiveDate = (DateTime.Parse(taskControl.Policy.EffectiveDate).AddMonths(12)).ToShortDateString();
                    taskControlQuote.ExpirationDate = DateTime.Parse(taskControl.Policy.ExpirationDate).AddMonths(12).ToShortDateString();
                    taskControlQuote.Policy.EffectiveDate = (DateTime.Parse(taskControl.Policy.EffectiveDate).AddMonths(12)).ToShortDateString();
                    taskControlQuote.Policy.ExpirationDate = DateTime.Parse(taskControl.Policy.ExpirationDate).AddMonths(12).ToShortDateString();
                    taskControlQuote.EntryDate = DateTime.Now;
                    taskControlQuote.Policy.TCIDQuotes = 0;
                    taskControl.Policy.Ren_Rei = "RN";
                    taskControl.Policy.AutoAssignPolicy = false;

                    EPolicy.Quotes.AutoCover AC = new EPolicy.Quotes.AutoCover();
                    EPolicy.Quotes.AutoDriver AD = new EPolicy.Quotes.AutoDriver();

                    for (int i = 0; i < taskControlQuote.Drivers.Count; i++)
                    {
                        AD = (EPolicy.Quotes.AutoDriver)taskControlQuote.Drivers[i];
                        AD.Mode = 1;
                        AD.SetIsPolicy(false);
                        taskControlQuote.Save(userID, null, AD, false);
                    }

                    for (int i = 0; i < taskControlQuote.AutoCovers.Count; i++)
                    {
                        AC = (EPolicy.Quotes.AutoCover)taskControlQuote.AutoCovers[i];
                        AC.SetIsPolicy(false);
                        AC.Mode = 1;
                        AC.NewUse = 1;
                        AC.VehicleAge = AC.VehicleAge + 1;
                        AC.DiscountBIPD = AC.DiscountBIPD;
                        AC.DiscountCompColl = AC.DiscountCompColl;

                        double cost = double.Parse(AC.ActualValue.ToString());
                        double totcost = cost * 0.85;
                        AC.Cost = AC.Cost;  //decimal.Parse(totcost.ToString());
                        AC.ActualValue = decimal.Parse(totcost.ToString());
                        taskControlQuote.Calculate();
                        taskControlQuote.Save(userID, AC, null, false);
                    }
                }
                else
                {
                    //Aplica al Ultimo endoso
                    bool isExistEndo = false;
                    EPolicy.TaskControl.QuoteAuto taskControlEndo = null;
                    for (int s = 1; s <= endososList.Rows.Count; s++)
                    {
                        if ((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"] != 0)
                        {
                            taskControlEndo = EPolicy.TaskControl.QuoteAuto.GetQuoteAuto((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], userID, false);
                            tcID2 = taskControlEndo.TaskControlID;
                            isExistEndo = true;
                            s = endososList.Rows.Count;
                        }
                    }

                    if (!isExistEndo)
                    {
                        taskControlQuote = taskControl;

                        taskControlQuote.Mode = 1; //ADD
                        taskControlQuote.TaskControlID = 0;
                        taskControlQuote.IsPolicy = false;
                        taskControlQuote.Policy.IsEndorsement = true;
                        taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Quote Auto"));
                        taskControlQuote.QuoteId = 0;
                        taskControlQuote.Policy.PolicyNo = taskControl.Policy.PolicyNo;
                        taskControlQuote.Policy.PolicyType = taskControl.Policy.PolicyType;
                        taskControlQuote.PolicyType = taskControl.Policy.PolicyType;
                        taskControlQuote.Policy.Suffix = taskControl.Policy.Suffix;
                        taskControlQuote.EffectiveDate = (DateTime.Parse(taskControl.Policy.EffectiveDate).AddMonths(12)).ToShortDateString();
                        taskControlQuote.ExpirationDate = DateTime.Parse(taskControl.Policy.ExpirationDate).AddMonths(12).ToShortDateString();
                        taskControlQuote.Policy.EffectiveDate = (DateTime.Parse(taskControl.Policy.EffectiveDate).AddMonths(12)).ToShortDateString();
                        taskControlQuote.Policy.ExpirationDate = DateTime.Parse(taskControl.Policy.ExpirationDate).AddMonths(12).ToShortDateString();
                        taskControlQuote.EntryDate = DateTime.Now;
                        taskControl.Policy.Ren_Rei = "RN";
                        taskControl.Policy.AutoAssignPolicy = false;

                        EPolicy.Quotes.AutoCover AC = new EPolicy.Quotes.AutoCover();
                        EPolicy.Quotes.AutoDriver AD = new EPolicy.Quotes.AutoDriver();


                        for (int i = 0; i < taskControlQuote.Drivers.Count; i++)
                        {
                            AD = (EPolicy.Quotes.AutoDriver)taskControlQuote.Drivers[i];
                            AD.Mode = 1;
                            AD.SetIsPolicy(false);
                            taskControlQuote.Save(userID, null, AD, false);
                        }

                        for (int i = 0; i < taskControlQuote.AutoCovers.Count; i++)
                        {
                            AC = (EPolicy.Quotes.AutoCover)taskControlQuote.AutoCovers[i];
                            AC.Mode = 1;
                            AC.SetIsPolicy(false);
                            AC.NewUse = 1;
                            AC.VehicleAge = AC.VehicleAge + 1;
                            AC.DiscountBIPD = AC.DiscountBIPD;
                            AC.DiscountCompColl = AC.DiscountCompColl;

                            double cost = double.Parse(AC.ActualValue.ToString());
                            double totcost = cost * 0.85;
                            AC.Cost = AC.Cost;  //decimal.Parse(totcost.ToString());
                            AC.ActualValue = decimal.Parse(totcost.ToString());
                            taskControlQuote.Calculate();
                            taskControlQuote.Save(userID, AC, null, false);
                        }
                    }
                    else
                    {
                        taskControlQuote = taskControlEndo;

                        taskControlQuote.Mode = 1; //ADD
                        taskControlQuote.TaskControlID = 0;
                        taskControlQuote.IsPolicy = false;
                        taskControlQuote.Policy.IsEndorsement = true;
                        taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Quote Auto"));
                        taskControlQuote.QuoteId = 0;
                        taskControlQuote.Policy.PolicyNo = taskControl.Policy.PolicyNo;
                        taskControlQuote.Policy.PolicyType = taskControl.Policy.PolicyType;
                        taskControlQuote.PolicyType = taskControl.Policy.PolicyType;
                        taskControlQuote.Policy.Suffix = taskControl.Policy.Suffix;
                        taskControlQuote.EffectiveDate = (DateTime.Parse(taskControl.Policy.EffectiveDate).AddMonths(12)).ToShortDateString();
                        taskControlQuote.ExpirationDate = DateTime.Parse(taskControl.Policy.ExpirationDate).AddMonths(12).ToShortDateString();
                        taskControlQuote.Policy.EffectiveDate = (DateTime.Parse(taskControl.Policy.EffectiveDate).AddMonths(12)).ToShortDateString();
                        taskControlQuote.Policy.ExpirationDate = DateTime.Parse(taskControl.Policy.ExpirationDate).AddMonths(12).ToShortDateString();
                        taskControlQuote.EntryDate = DateTime.Now;
                        taskControl.Policy.Ren_Rei = "RN";
                        taskControl.Policy.AutoAssignPolicy = false;

                        EPolicy.Quotes.AutoCover AC = new EPolicy.Quotes.AutoCover();
                        EPolicy.Quotes.AutoDriver AD = new EPolicy.Quotes.AutoDriver();

                        for (int i = 0; i < taskControlQuote.Drivers.Count; i++)
                        {
                            AD = (EPolicy.Quotes.AutoDriver)taskControlQuote.Drivers[i];
                            AD.Mode = 1;
                            AD.SetIsPolicy(false);
                            taskControlQuote.Save(userID, null, AD, false);
                        }

                        for (int i = 0; i < taskControlQuote.AutoCovers.Count; i++)
                        {
                            AC = (EPolicy.Quotes.AutoCover)taskControlQuote.AutoCovers[i];
                            EPolicy.Quotes.AutoCover AC2 = (EPolicy.Quotes.AutoCover)taskControlQuote.AutoCovers[i];
                            AC.Mode = 1;
                            AC.VIN = AC2.VIN;
                            AC.PurchaseDate = AC2.PurchaseDate;
                            AC.Plate = AC2.Plate;
                            AC.VehicleAge = AC2.VehicleAge + 1;
                            AC.DiscountBIPD = AC.DiscountBIPD;
                            AC.DiscountCompColl = AC.DiscountCompColl;
                            AC.NewUse = 1;
                            AC.License = AC2.License;
                            AC.LicenseExpDate = AC2.LicenseExpDate;
                            AC.DiscountBIPD = AC2.DiscountBIPD;
                            AC.DiscountCompColl = AC2.DiscountCompColl;

                            double cost = double.Parse(AC.ActualValue.ToString());
                            double totcost = cost * 0.85;
                            AC.Cost = AC.Cost;  //decimal.Parse(totcost.ToString());
                            AC.ActualValue = decimal.Parse(totcost.ToString());

                            AC.SetIsPolicy(false);
                            taskControlQuote.PolicyType = taskControl.PolicyType;
                            taskControlQuote.Calculate();
                            taskControlQuote.Save(userID, AC, null, false);
                        }
                    }
                }

                taskControlQuote.Mode = 2; //ADD
                taskControlQuote.Term = taskControl.Policy.Term;
                taskControlQuote.Policy.TCIDQuotes = 0;

                EPolicy.TaskControl.QuoteAuto taskControl2 = new EPolicy.TaskControl.QuoteAuto(false);

                Session.Clear();
                Session.Add("TaskControl", taskControlQuote);
                Response.Redirect("ExpressAutoQuote.aspx?FromRenewPolicy=" + taskControlQuote.TaskControlID.ToString().Trim(), false);
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
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
            acpolicy.Territory = AC.Territory;
            acpolicy.VehicleClass = AC.VehicleClass;
            acpolicy.Bank = AC.Bank;                                           //this.ddlBank.SelectedItem.Value;			
            acpolicy.CompanyDealer = AC.CompanyDealer;                                  //this.ddlCompanyDealer.SelectedItem.Value;  

            try
            {
                int InternalID = 0;

                InternalID = taskControl.GetNewInternalID();

                //acpolicy.PurchaseDate = AC.PurchaseDate;
                acpolicy.PurchaseDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC.PurchaseDate).ToShortDateString());
                acpolicy.Plate = AC.Plate;
                acpolicy.VehicleAge = AC.VehicleAge + 1;
                acpolicy.NewUse = 1; //Used

                //Calcular Depreciacion
                double cost = double.Parse(AC.ActualValue.ToString());
                double totcost = cost * 0.85;

                acpolicy.Cost = AC.Cost;  //decimal.Parse(totcost.ToString());
                acpolicy.ActualValue = decimal.Parse(totcost.ToString());
                acpolicy.NewUse = 1;//Used
                acpolicy.HomeCity = 0;  //int.Parse(ddlHomeCity.SelectedItem.Value);
                acpolicy.WorkCity = 0;  //int.Parse(ddlWorkCity.SelectedItem.Value);
                acpolicy.AlarmType = AC.AlarmType;
                acpolicy.Depreciation1stYear = AC.Depreciation1stYear;
                acpolicy.DepreciationAllYear = AC.DepreciationAllYear;
                acpolicy.MedicalLimit = AC.MedicalLimit;
                acpolicy.AssistancePremium = AC.AssistancePremium;
                //acpolicy.TowingPremium = AC.TowingPremium;
                acpolicy.TowingPremium = Decimal.Round(AC.TowingPremium, 4); 
                acpolicy.VehicleRental = AC.VehicleRental;
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
        protected void btnCancellation_Click(object sender, EventArgs e)
        {
            RemoveSessionLookUp();
            Session.Add("FromUI", "QuoteAuto.aspx");
            Session.Add("CancFromAuto", "CancFromAuto");
            Session.Add("CancFromAutoExit", "CancFromAutoExit");
            Response.Redirect("CancellationPolicy.aspx", false);
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
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            if (Session["OPPEndorUpdate"] == null)
            {
                // Salvar el num. de Endo en Policy
                EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
                //int endNum = taskControl.Endoso + 1;
                //taskControl.Endoso = endNum;
                taskControl.Policy.SaveOnlyPolicy(userID);
                Session["TaskControl"] = taskControl;
            }

            if (Session["ApplyEndorsement"] != null)
            {
                int a = (int)Session["ApplyEndorsement"];
                EPolicy.TaskControl.QuoteAuto newOPP2 = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

                ApplyEndorsement(newOPP2, userID);
            }

            // Salvar la info en OPP Endorsement.
            if (Session["ONLYAUTOEndorsement"] != null)
            {
                EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
                taskControl.Mode = 2; //EDIT
                int endNum = taskControl.Policy.Endoso + 1;
                taskControl.Policy.Endoso = endNum;
                Session["TaskControl"] = taskControl;
                taskControl.Policy.SaveOnlyPolicy(userID);

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

                            EPolicy.TaskControl.QuoteAuto opp = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);
                            opp.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                            opp.Policy.IsEndorsement = true;

                            //  int OPPEndorsementID2 = int.Parse(e.Item.Cells[1].Text);


                            if (Session["TaskControl"] != null)
                            {
                                EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
                                Session.Clear();
                                Session.Add("AUTOEndorsement", taskControl);
                                Session.Add("OPPEndorUpdate", "Update");
                                //Send EndorsementID
                                //Session.Add("OPPEndorsementID", OPPEndorsementID2);
                                Session.Remove("TaskControl");
                            }

                            Session.Add("TaskControl", opp);
                            Response.Redirect("ExpressAutoQuote.aspx");
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
                        EPolicy.TaskControl.QuoteAuto newOPP = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

                        int OPPEndorsementID = int.Parse(e.Item.Cells[1].Text);
                        Session.Add("OPPEndorsementID", OPPEndorsementID);
                        //Buscar Quotes para endosar
                        // Panel1.Visible = true;
                        AccordionEndorsement.Visible = true;
                        txtEffDtEndorsement.Text = e.Item.Cells[4].Text.Trim(); //DateTime.Now.ToShortDateString();
                        txtFactor.Text = e.Item.Cells[5].Text.Trim();
                        txtProRata.Text = e.Item.Cells[6].Text.Trim();
                        txtShortRate.Text = e.Item.Cells[7].Text.Trim();
                        txtActualPremAuto.Text = e.Item.Cells[14].Text.Trim();
                        txtActualPremTotal.Text = e.Item.Cells[16].Text.Trim();
                        txtPreviousPremAuto.Text = e.Item.Cells[19].Text.Trim();
                        txtPreviousPremTotal.Text = e.Item.Cells[21].Text.Trim();
                        txtDiffPremAuto.Text = e.Item.Cells[24].Text.Trim();
                        txtDiffPremTotal.Text = e.Item.Cells[26].Text.Trim();
                        txtAdditionalPremium.Text = e.Item.Cells[27].Text.Trim();

                        CalculateEndorsement(newOPP);
                        VerifyChanges(newOPP, userID);
                        Session.Add("ApplyEndorsement", a);
                        //ApplyEndorsement(newOPP, userID);
                        break;

                    case "Update":
                        DataGridGroup.DataSource = null;

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
                        //Buscar Quotes para endosar
                        //Panel1.Visible = true;
                        AccordionEndorsement.Visible = true;
                        txtEffDtEndorsement.Text = e.Item.Cells[4].Text.Trim();
                        txtFactor.Text = e.Item.Cells[5].Text.Trim();
                        txtProRata.Text = e.Item.Cells[6].Text.Trim();
                        txtShortRate.Text = e.Item.Cells[7].Text.Trim();
                        txtEndoComments.Text = e.Item.Cells[10].Text.Trim();
                        txtActualPremAuto.Text = e.Item.Cells[14].Text.Trim();
                        txtActualPremTotal.Text = e.Item.Cells[16].Text.Trim();
                        txtPreviousPremAuto.Text = e.Item.Cells[19].Text.Trim();
                        txtPreviousPremTotal.Text = e.Item.Cells[21].Text.Trim();
                        txtDiffPremAuto.Text = e.Item.Cells[24].Text.Trim();
                        txtDiffPremTotal.Text = e.Item.Cells[26].Text.Trim();
                        txtAdditionalPremium.Text = e.Item.Cells[27].Text.Trim();
                        break;

                    case "Print":
                        DataGridGroup.DataSource = null;

                        string date2 = e.Item.Cells[3].Text.Trim();
                        if (date2.ToString().Trim() == "&nbsp;")
                        {
                            throw new Exception("This Endorsement is not Applied.");
                        }

                        EPolicy.TaskControl.QuoteAuto taskControl2 = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];

                        int s = int.Parse(e.Item.Cells[2].Text);
                        string comments = e.Item.Cells[10].Text.Trim();
                        EPolicy.TaskControl.QuoteAuto newOPP2 = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(s, userID);
                        int OPPEndorID = int.Parse(e.Item.Cells[1].Text);

                        //Print Document
                        try
                        {
                            EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];

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
                            throw new Exception(ecp.Message);
                        }

                        break;

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

        private XmlDocument GetInsertOPPEndorsementXml(int OPPTaskControlID, int OPPQuotesID, double mFactor, double NewProRataTotPrem, double NewShotRateTotPrem)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[22];

            DbRequestXmlCooker.AttachCookItem("OPPTaskControlID", SqlDbType.Int, 0, OPPTaskControlID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("OPPQuotesID", SqlDbType.Int, 0, OPPQuotesID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("EndoEffective", SqlDbType.DateTime, 0, txtEffDtEndorsementPrimary.Text, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Factor", SqlDbType.Float, 0, mFactor.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ProRataPremium", SqlDbType.Float, 0, NewProRataTotPrem.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ShortRatePremium", SqlDbType.Float, 0, NewShotRateTotPrem.ToString(), ref cookItems);
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

        private XmlDocument GetUpdateOPPEndorsementXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[23];

            TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto)Session["TaskControl"];
            int OPPEndorsementID = (int)Session["OPPEndorsementID"];

            DbRequestXmlCooker.AttachCookItem("OPPEndorsementID", SqlDbType.Int, 0, OPPEndorsementID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("EndoEffective", SqlDbType.DateTime, 0, txtEffDtEndorsement.Text.Trim(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("EndoNum", SqlDbType.Char, 4, taskControl.Policy.Endoso.ToString(), ref cookItems);
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

        private void CalculateEndorsement(EPolicy.TaskControl.QuoteAuto OpptaskControl)
        {
            TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto)Session["TaskControl"];

            double totprem = SetEndorsementToCalculateDifference(taskControl, OpptaskControl);

            double mFactorProRata = 0.0;
            double mFactorShortRate = 0.0;
            double NewProRataTotPrem = 0.0;
            double NewShotRateTotPrem = 0.0;
            string EndorDate = txtEffDtEndorsement.Text.Trim();
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

                NewProRataTotPrem = Math.Round(totprem * mFactorProRata, 0);
                NewShotRateTotPrem = Math.Round(totprem * mFactorShortRate, 0);

                txtFactor.Text = mFactorProRata.ToString().Trim();
                txtProRata.Text = NewProRataTotPrem.ToString().Trim();
                txtShortRate.Text = NewShotRateTotPrem.ToString().Trim();
                txtAdditionalPremium.Text = NewProRataTotPrem.ToString("###,###,###.00");
            }
            else
            {

                txtAdditionalPremium.Text = "0.0";
            }
        }

        private double SetEndorsementToCalculateDifference(EPolicy.TaskControl.QuoteAuto taskControl, EPolicy.TaskControl.QuoteAuto OpptaskControl)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            txtActualPremAuto.Text = OpptaskControl.TotalPremium.ToString("###,###,###.00");
            txtActualPremTotal.Text = OpptaskControl.TotalPremium.ToString("###,###,###.00");

            //Buscar el endoso anterior
            //Para aplicar el ultimo endoso, sino a la poliza original
            DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(taskControl.TaskControlID);
            EPolicy.TaskControl.QuoteAuto taskControlEndo = null;

            if (endososList.Rows.Count == 1)
            {
                if ((int)endososList.Rows[endososList.Rows.Count - 1]["OPPQuotesID"] != 0)
                {
                    taskControlEndo = EPolicy.TaskControl.QuoteAuto.GetQuoteAuto((int)endososList.Rows[endososList.Rows.Count - 1]["OPPQuotesID"], userID, false);
                    taskControl = taskControlEndo;

                    txtPreviousPremAuto.Text = taskControlEndo.TotalPremium.ToString("###,###,###.00");
                    txtPreviousPremTotal.Text = taskControlEndo.TotalPremium.ToString("###,###,###.00");
                }
                else
                {
                    txtPreviousPremAuto.Text = taskControl.TotalPremium.ToString("###,###,###.00");
                    txtPreviousPremTotal.Text = taskControl.TotalPremium.ToString("###,###,###.00");
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
                            taskControlEndo = EPolicy.TaskControl.QuoteAuto.GetQuoteAuto((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], userID, false);
                            taskControl = taskControlEndo;
                            isExistEndo = true;
                            s = endososList.Rows.Count;
                        }
                    }

                    if (isExistEndo)
                    {
                        txtPreviousPremAuto.Text = taskControlEndo.TotalPremium.ToString("###,###,###.00");
                        txtPreviousPremTotal.Text = taskControlEndo.TotalPremium.ToString("###,###,###.00");
                    }
                    else
                    {
                        txtPreviousPremAuto.Text = taskControl.TotalPremium.ToString("###,###,###.00");
                        txtPreviousPremTotal.Text = taskControl.TotalPremium.ToString("###,###,###.00");
                    }
                }
                else
                {
                    txtPreviousPremAuto.Text = taskControl.TotalPremium.ToString("###,###,###.00");
                    txtPreviousPremTotal.Text = taskControl.TotalPremium.ToString("###,###,###.00");
                }

            //Calculate Difference
            txtDiffPremAuto.Text = CalculateEndorsementDifference(txtActualPremAuto.Text.Trim(), txtPreviousPremAuto.Text);

            double totalPrev = double.Parse(taskControl.TotalPremium.ToString()) + double.Parse(taskControl.Charge.ToString());
            double totalActual = double.Parse(OpptaskControl.TotalPremium.ToString()) + double.Parse(OpptaskControl.Charge.ToString());

            txtDiffPremTotal.Text = CalculateEndorsementDifference(totalActual.ToString(), totalPrev.ToString());

            return double.Parse(txtDiffPremTotal.Text);
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

        private void FillDataGrid()
        {
            TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto)Session["TaskControl"];

            DataGridGroup.DataSource = null;
            DtEndorsement = null;

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
                }
            }
            else
            {
                DataGridGroup.DataSource = null;
                DataGridGroup.DataBind();
            }
        }

        private void VerifyChanges(EPolicy.TaskControl.QuoteAuto newOPP, int userID)
        {
            TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto)Session["TaskControl"];
            EPolicy.Audit.History history = new EPolicy.Audit.History();
            int mode = 2; //Update

            // Campos de TaskControl
            history.BuildNotesForHistory("Agent",
                EPolicy.LookupTables.LookupTables.GetDescription("Agent", taskControl.Agent.ToString()),
                EPolicy.LookupTables.LookupTables.GetDescription("Agent", newOPP.Agent.ToString()),
                mode);

            history.BuildNotesForHistory("InsuranceCompany",
                EPolicy.LookupTables.LookupTables.GetDescription("InsuranceCompany", taskControl.InsuranceCompany.ToString()),
                EPolicy.LookupTables.LookupTables.GetDescription("InsuranceCompany", newOPP.InsuranceCompany.ToString()),
                mode);
            // Terminan Campos TaskControl

            //Campos de Customer
            history.BuildNotesForHistory("Name", taskControl.Customer.FirstName, newOPP.Customer.FirstName, mode);
            history.BuildNotesForHistory("Initial", taskControl.Customer.Initial, newOPP.Customer.Initial, mode);
            history.BuildNotesForHistory("LastName", taskControl.Customer.LastName1, newOPP.Customer.LastName1, mode);
            history.BuildNotesForHistory("LastName2", taskControl.Customer.LastName2, newOPP.Customer.LastName2, mode);
            history.BuildNotesForHistory("Address1", taskControl.Customer.Address1, newOPP.Customer.Address1, mode);
            history.BuildNotesForHistory("Address2", taskControl.Customer.Address2, newOPP.Customer.Address2, mode);
            history.BuildNotesForHistory("City", taskControl.Customer.City, newOPP.Customer.City, mode);
            history.BuildNotesForHistory("State", taskControl.Customer.State, newOPP.Customer.State, mode);
            history.BuildNotesForHistory("ZipCode", taskControl.Customer.ZipCode, newOPP.Customer.ZipCode, mode);
            // Terminan Campos Customer


            // Terminan Campos OPP

            // Campos de Policy
            //history.BuildNotesForHistory("PolicyType", taskControl.Policy.PolicyType, newOPP.PolicyType, mode);
            //history.BuildNotesForHistory("PolicyNo", taskControl.Policy.PolicyNo, newOPP.Policy.PolicyNo, mode);
            //history.BuildNotesForHistory("Certificate", taskControl.Policy.Certificate, newOPP.Policy.Certificate, mode);
            //history.BuildNotesForHistory("Suffix", taskControl.Policy.Suffix, newOPP.Policy.Suffix, mode);
            //history.BuildNotesForHistory("LoanNo", taskControl.Policy.LoanNo.ToString(), newOPP.Policy.LoanNo.ToString(), mode);
            history.BuildNotesForHistory("Term", taskControl.Term.ToString(), newOPP.Term.ToString(), mode);
            history.BuildNotesForHistory("EffectiveDate", taskControl.EffectiveDate.ToString(), newOPP.EffectiveDate.ToString(), mode);
            history.BuildNotesForHistory("ExpirationDate", taskControl.ExpirationDate.ToString(), newOPP.ExpirationDate.ToString(), mode);
            history.BuildNotesForHistory("Charge", taskControl.Charge.ToString("###,###,###.00"), newOPP.Charge.ToString("###,###,###.00"), mode);
            history.BuildNotesForHistory("TotalPremium", taskControl.TotalPremium.ToString("###,###,###.00"), newOPP.TotalPremium.ToString("###,###,###.00"), mode);
            // Terminan Campos Policy

            history.Actions = "EDIT";

            //history.KeyID = this.TaskControlID.ToString();
            //history.Subject = "POLICIES";
            //history.UsersID = userID;
            history.GetNotes();
            if (history.Notes.ToUpper().Trim() != "")
                txtEndoComments.Text = "";//history.Notes.ToUpper().Trim();
            else
                txtEndoComments.Text = "";

            // history.GetSaveHistory();
        }

        private void VerifyChangesONLYEndorsement(EPolicy.TaskControl.QuoteAuto taskControl, int userID)
        {
            TaskControl.QuoteAuto newOPP = (TaskControl.QuoteAuto)Session["TaskControl"];
            EPolicy.Audit.History history = new EPolicy.Audit.History();
            int mode = 2; //Update

            // Campos de TaskControl
            history.BuildNotesForHistory("Agent",
                EPolicy.LookupTables.LookupTables.GetDescription("Agent", taskControl.Agent.ToString()),
                EPolicy.LookupTables.LookupTables.GetDescription("Agent", newOPP.Agent.ToString()),
                mode);

            history.BuildNotesForHistory("InsuranceCompany",
                EPolicy.LookupTables.LookupTables.GetDescription("InsuranceCompany", taskControl.InsuranceCompany.ToString()),
                EPolicy.LookupTables.LookupTables.GetDescription("InsuranceCompany", newOPP.InsuranceCompany.ToString()),
                mode);
            // Terminan Campos TaskControl

            //Campos de Customer
            history.BuildNotesForHistory("Name", taskControl.Customer.FirstName, newOPP.Customer.FirstName, mode);
            history.BuildNotesForHistory("Initial", taskControl.Customer.Initial, newOPP.Customer.Initial, mode);
            history.BuildNotesForHistory("LastName", taskControl.Customer.LastName1, newOPP.Customer.LastName1, mode);
            history.BuildNotesForHistory("LastName2", taskControl.Customer.LastName2, newOPP.Customer.LastName2, mode);
            history.BuildNotesForHistory("Address1", taskControl.Customer.Address1, newOPP.Customer.Address1, mode);
            history.BuildNotesForHistory("Address2", taskControl.Customer.Address2, newOPP.Customer.Address2, mode);
            history.BuildNotesForHistory("City", taskControl.Customer.City, newOPP.Customer.City, mode);
            history.BuildNotesForHistory("State", taskControl.Customer.State, newOPP.Customer.State, mode);
            history.BuildNotesForHistory("ZipCode", taskControl.Customer.ZipCode, newOPP.Customer.ZipCode, mode);
            // Terminan Campos Customer


            // Terminan Campos OPP

            // Campos de Policy
            //history.BuildNotesForHistory("PolicyType", taskControl.Policy.PolicyType, newOPP.PolicyType, mode);
            //history.BuildNotesForHistory("PolicyNo", taskControl.Policy.PolicyNo, newOPP.Policy.PolicyNo, mode);
            //history.BuildNotesForHistory("Certificate", taskControl.Policy.Certificate, newOPP.Policy.Certificate, mode);
            //history.BuildNotesForHistory("Suffix", taskControl.Policy.Suffix, newOPP.Policy.Suffix, mode);
            //history.BuildNotesForHistory("LoanNo", taskControl.Policy.LoanNo.ToString(), newOPP.Policy.LoanNo.ToString(), mode);
            history.BuildNotesForHistory("Term", taskControl.Term.ToString(), newOPP.Term.ToString(), mode);
            history.BuildNotesForHistory("EffectiveDate", taskControl.EffectiveDate.ToString(), newOPP.EffectiveDate.ToString(), mode);
            history.BuildNotesForHistory("ExpirationDate", taskControl.ExpirationDate.ToString(), newOPP.ExpirationDate.ToString(), mode);
            history.BuildNotesForHistory("Charge", taskControl.Charge.ToString("###,###,###.00"), newOPP.Charge.ToString("###,###,###.00"), mode);
            history.BuildNotesForHistory("TotalPremium", taskControl.TotalPremium.ToString("###,###,###.00"), newOPP.TotalPremium.ToString("###,###,###.00"), mode);
            // Terminan Campos Policy

            history.Actions = "EDIT";

            //history.KeyID = this.TaskControlID.ToString();
            //history.Subject = "POLICIES";
            //history.UsersID = userID;
            history.GetNotes();
            if (history.Notes.ToUpper().Trim() != "")
                txtEndoComments.Text = ""; //history.Notes.ToUpper().Trim();
            else
                txtEndoComments.Text = "";

            // history.GetSaveHistory();
        }

        private void ApplyEndorsement(EPolicy.TaskControl.QuoteAuto newOPP, int userID)
        {
            TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto)Session["TaskControl"];
            taskControl.Mode = 2; //EDIT
            int endNum = taskControl.Policy.Endoso + 1;
            taskControl.Policy.Endoso = endNum;
            Session["TaskControl"] = taskControl;
            taskControl.Policy.SaveOnlyPolicy(userID);

            UpdateOPPEndorsement();
            //FillTextControl();
            //DisableControls();

            FillDataGrid();
            Session["TaskControl"] = taskControl;
        }

        //private void AddOPPEndorsement(int OPPTaskControlID, int OPPQuotesID, double mFactor, double NewProRataTotPrem, double NewShotRateTotPrem)
        //{
        //    Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
        //    try
        //    {
        //        Executor.BeginTrans();
        //        int a = Executor.Insert("AddOPPEndorsement", this.GetInsertOPPEndorsementXml(OPPTaskControlID, OPPQuotesID, mFactor, NewProRataTotPrem, NewShotRateTotPrem));
        //        Executor.CommitTrans();
        //    }
        //    catch (Exception xcp)
        //    {
        //        Executor.RollBackTrans();
        //        throw new Exception("Error while trying to save Endorsement Quote. " + xcp.Message, xcp);
        //    }
        //}
        protected void btnEndor_Click(object sender, EventArgs e)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];


            //Session.Clear();
            EPolicy.TaskControl.QuoteAuto taskControlQuote = new EPolicy.TaskControl.QuoteAuto(false);

            int tcID = taskControl.TaskControlID;


            //Para aplicar el ultimo endoso, sino a la poliza original
            DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(tcID);
            if (endososList.Rows.Count == 0)
            {
                taskControlQuote = taskControl;

                taskControlQuote.Mode = 1; //ADD
                taskControlQuote.TaskControlID = 0;
                taskControlQuote.IsPolicy = false;
                taskControlQuote.Policy.IsEndorsement = true;
                taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Quote Auto"));
                taskControlQuote.QuoteId = 0;

                EPolicy.Quotes.AutoCover AC = new EPolicy.Quotes.AutoCover();
                EPolicy.Quotes.AutoDriver AD = new EPolicy.Quotes.AutoDriver();

                for (int i = 0; i < taskControlQuote.Drivers.Count; i++)
                {
                    AD = (EPolicy.Quotes.AutoDriver)taskControlQuote.Drivers[i];
                    AD.Mode = 1;
                    taskControlQuote.Save(userID, null, AD, false);
                }

                for (int i = 0; i < taskControlQuote.AutoCovers.Count; i++)
                {
                    AC = (EPolicy.Quotes.AutoCover)taskControlQuote.AutoCovers[i];
                    AC.Mode = 1;
                    taskControlQuote.Save(userID, AC, null, false);
                }
            }
            else
            {
                //Aplica al Ultimo endoso
                bool isExistEndo = false;
                EPolicy.TaskControl.QuoteAuto taskControlEndo = null; ;
                for (int s = 1; s <= endososList.Rows.Count; s++)
                {
                    if ((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"] != 0)
                    {
                        taskControlEndo = EPolicy.TaskControl.QuoteAuto.GetQuoteAuto((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], userID, false);
                        isExistEndo = true;
                        s = endososList.Rows.Count;
                    }
                }

                if (!isExistEndo)
                {
                    taskControlQuote = taskControl;

                    taskControlQuote.Mode = 1; //ADD
                    taskControlQuote.TaskControlID = 0;
                    taskControlQuote.IsPolicy = false;
                    taskControlQuote.Policy.IsEndorsement = true;
                    taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Quote Auto"));
                    taskControlQuote.QuoteId = 0;

                    EPolicy.Quotes.AutoCover AC = new EPolicy.Quotes.AutoCover();
                    EPolicy.Quotes.AutoDriver AD = new EPolicy.Quotes.AutoDriver();

                    for (int i = 0; i < taskControlQuote.Drivers.Count; i++)
                    {
                        AD = (EPolicy.Quotes.AutoDriver)taskControlQuote.Drivers[i];
                        AD.Mode = 1;
                        taskControlQuote.Save(userID, null, AD, false);
                    }

                    for (int i = 0; i < taskControlQuote.AutoCovers.Count; i++)
                    {
                        AC = (EPolicy.Quotes.AutoCover)taskControlQuote.AutoCovers[i];
                        AC.Mode = 1;
                        taskControlQuote.Save(userID, AC, null, false);
                    }
                }
                else
                {
                    taskControlQuote = taskControlEndo;

                    taskControlQuote.Mode = 1; //ADD
                    taskControlQuote.TaskControlID = 0;
                    taskControlQuote.IsPolicy = false;
                    taskControlQuote.Policy.IsEndorsement = true;
                    taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Quote Auto"));
                    taskControlQuote.QuoteId = 0;

                    EPolicy.Quotes.AutoCover AC = new EPolicy.Quotes.AutoCover();
                    EPolicy.Quotes.AutoDriver AD = new EPolicy.Quotes.AutoDriver();

                    for (int i = 0; i < taskControlQuote.Drivers.Count; i++)
                    {
                        AD = (EPolicy.Quotes.AutoDriver)taskControlQuote.Drivers[i];
                        AD.Mode = 1;
                        taskControlQuote.Save(userID, null, AD, false);
                    }

                    for (int i = 0; i < taskControlQuote.AutoCovers.Count; i++)
                    {
                        AC = (EPolicy.Quotes.AutoCover)taskControlQuote.AutoCovers[i];
                        EPolicy.Quotes.AutoCover AC2 = (EPolicy.Quotes.AutoCover)taskControl.AutoCovers[i];

                        AC.Mode = 1;
                        AC.VIN = AC2.VIN;
                        //AC.PurchaseDate = AC2.PurchaseDate;
                        AC.PurchaseDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC2.PurchaseDate).ToShortDateString());
                        AC.Plate = AC2.Plate;
                        AC.VehicleAge = AC2.VehicleAge;
                        AC.License = AC2.License;
                       // AC.LicenseExpDate = AC2.LicenseExpDate;
                        AC.LicenseExpDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC2.LicenseExpDate).ToShortDateString());

                        taskControlQuote.Save(userID, AC, null, false);
                    }
                }
            }

            taskControlQuote.Mode = 1; //ADD
            taskControlQuote.Term = taskControl.Policy.Term;
            taskControlQuote.Policy.TCIDQuotes = taskControlQuote.TaskControlID;

            EPolicy.TaskControl.QuoteAuto taskControl2 = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(tcID, userID);
            Session.Remove("TaskControl");
            Session.Add("TaskControl", taskControlQuote);
            Session.Add("AUTOEndorsement", taskControl2);

            RemoveSessionLookUp();
            Response.Redirect("ExpressAutoQuote.aspx");
        }

        protected void btnEndor2_Click(object sender, EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            int OPPEndorsement3ID = 0;
            Session.Add("ONLYAUTOEndorsement", "ENDO");
            Session.Add("OPPEndorsementID", OPPEndorsement3ID);
            Session.Add("OPPEndorUpdate", "Update");



            // Panel1.Visible = true;
            AccordionEndorsement.Visible = true;
            txtEffDtEndorsement.Text = DateTime.Now.ToShortDateString();
            txtEffDtEndorsementPrimary.Text = DateTime.Now.ToShortDateString();

            txtFactor.Text = "0.00";
            txtProRata.Text = "0.00";
            txtShortRate.Text = "0.00";
            txtEndoComments.Text = "";
            txtActualPremAuto.Text = "0.00";
            txtActualPremTotal.Text = "0.00";
            txtPreviousPremAuto.Text = "0.00";
            txtPreviousPremTotal.Text = "0.00";
            txtDiffPremAuto.Text = "0.00";
            txtDiffPremTotal.Text = "0.00";
            txtAdditionalPremium.Text = "0.00";
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

        protected void btnReinstatement_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("AUTOCHARGE");

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                TaskControl.QuoteAuto taskControl = new TaskControl.QuoteAuto(true);  //Policy
                TaskControl.QuoteAuto quoteAuto = (TaskControl.QuoteAuto)Session["TaskControl"];

                //Para aplicar el ultimo endoso, sino a la poliza original
                DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(quoteAuto.TaskControlID);

                EPolicy.TaskControl.QuoteAuto taskControlEndo = null;
                if (endososList.Rows.Count > 0)
                {
                    taskControlEndo = EPolicy.TaskControl.QuoteAuto.GetQuoteAuto((int)endososList.Rows[endososList.Rows.Count - 1]["OPPQuotesID"], userID, false);
                    taskControl = taskControlEndo;
                }
                else
                {
                    taskControl = quoteAuto;
                }

                taskControl.IsPolicy = true;
                taskControl.Mode = 1;
                taskControl.TaskControlTypeID = 10; // Auto Policy

                taskControl.Policy.IsEndorsement = false;
                taskControl.Policy.CancellationDate = quoteAuto.Policy.CancellationDate;
                taskControl.Policy.CancellationEntryDate = quoteAuto.Policy.CancellationEntryDate;
                taskControl.Policy.CancellationUnearnedPercent = 0.00;
                taskControl.Policy.CancellationMethod = 0;
                taskControl.Policy.CancellationReason = 0;
                taskControl.Policy.PaidAmount = taskControl.Policy.PaidAmount;
                taskControl.Policy.PaidDate = "";
                taskControl.Policy.Ren_Rei = "REI";
                taskControl.Policy.Rein_Amt = quoteAuto.Policy.CancellationAmount;
                taskControl.Policy.PaidDate = quoteAuto.Policy.PaidDate;
                taskControl.Policy.CommissionAgency = 0.00; // taskControl.ReturnCommissionAgency;
                taskControl.Policy.CommissionAgent = 0.00; // taskControl.ReturnCommissionAgent;
                taskControl.Policy.CommissionAgencyPercent = ""; // taskControl.CommissionAgencyPercent.Trim();
                taskControl.Policy.CommissionAgentPercent = "";  //taskControl.CommissionAgentPercent.Trim();
                taskControl.Policy.TaskControlID = 0;
                taskControl.Policy.Status = "Inforce";

                taskControl.Policy.ReturnCharge = 0.00;
                taskControl.Policy.ReturnPremium = 0.00;
                taskControl.Policy.CancellationAmount = 0.00;
                taskControl.Policy.ReturnCommissionAgency = 0.00;
                taskControl.Policy.ReturnCommissionAgent = 0.00;

                taskControl.Policy.IsDeferred = false;
                taskControl.Agency = quoteAuto.Agency;
                taskControl.Agent = quoteAuto.Agent;
                taskControl.Bank = quoteAuto.Bank;
                taskControl.CompanyDealer = quoteAuto.CompanyDealer;
                taskControl.InsuranceCompany = quoteAuto.InsuranceCompany;

                taskControl.Policy.AgentList = quoteAuto.Policy.AgentList;
                taskControl.Policy.LbxAgentSelected = quoteAuto.Policy.LbxAgentSelected;
                taskControl.Policy.LbxAgentSelected = quoteAuto.Policy.LbxAgentSelected;

                taskControl.Customer = quoteAuto.Customer; // quoteAuto.Customer;
                taskControl.CustomerNo = quoteAuto.Customer.CustomerNo; // quoteAuto.CustomerNo;
                taskControl.Prospect = quoteAuto.Prospect;
                taskControl.ProspectID = quoteAuto.ProspectID;
                taskControl.Term = quoteAuto.Term;
                //taskControl.EffectiveDate = (DateTime.Parse(quoteAuto.Policy.EffectiveDate).AddMonths(12)).ToShortDateString();
                taskControl.EffectiveDate = quoteAuto.Policy.CancellationDate;
                taskControl.ExpirationDate = quoteAuto.Policy.ExpirationDate;//DateTime.Parse(quoteAuto.Policy.ExpirationDate).AddMonths(12).ToShortDateString();
                taskControl.EntryDate = DateTime.Now;
               

                taskControl.PolicyType = quoteAuto.PolicyType;

                if (ddlLocation.SelectedItem.Value != "")
                    taskControl.Policy.OriginatedAt = int.Parse(ddlLocation.SelectedItem.Value);
                else
                    taskControl.Policy.OriginatedAt = 0;

                if (quoteAuto.Policy.MasterPolicyID.Trim() == "")
                    taskControl.Policy.IsMaster = false; // quoteAuto.IsMaster;
                else
                    taskControl.Policy.IsMaster = true;

                taskControl.Policy.MasterPolicyID = quoteAuto.Policy.MasterPolicyID;
                taskControl.Policy.FileNumber = quoteAuto.FileNumber;
                taskControl.Policy.PolicyType = quoteAuto.Policy.PolicyType;
                taskControl.Policy.PolicyNo = quoteAuto.Policy.PolicyNo;
                taskControl.Policy.Certificate = quoteAuto.Policy.Certificate;
                taskControl.Policy.AutoAssignPolicy = false;


                int msufijo;
                int sufijo = int.Parse(quoteAuto.Policy.Suffix);
                msufijo = sufijo + 1;
                taskControl.Policy.Suffix = "0".ToString() + msufijo.ToString();

                taskControl.Policy.PolicyCicleEnd = 1; //Para que en la pantalla de auto no entre de modo new.
                taskControl.Policy.Agent = quoteAuto.Agent;
                taskControl.Policy.Agency = quoteAuto.Agency;
                taskControl.Policy.InsuranceCompany = "001";  //MULTI INS.
                taskControl.Policy.PMT1 = 0;
                taskControl.QuoteId = 0;

                if (endososList.Rows.Count > 0)
                {
                    taskControl.Charge = taskControlEndo.Charge;
                    taskControl.TotalPremium = taskControlEndo.TotalPremium;
                }
                else
                {
                    taskControl.Charge = quoteAuto.Charge;
                    taskControl.TotalPremium = quoteAuto.TotalPremium;
                }

                //Fill Auto Information from Application Auto or of the Quote.
                Quotes.AutoDriver AD = null;
                Quotes.AutoCover AC = null;

                if (endososList.Rows.Count > 0)
                {
                    //Para guardar el auto, driver, assingdriver, breakdown, temporeramente en la poliza.
                    if (quoteAuto.TaskControlID != 0)
                    {
                        //Para asignar un nuevo QuoteID.
                        if (taskControl.QuoteId == 0)
                        {
                            taskControl.QuoteId = taskControl.Policy.GetPolicyIDTemp();
                        }

                        quoteAuto.Drivers = taskControlEndo.Drivers;
                        quoteAuto.AutoCovers = taskControlEndo.AutoCovers;

                        if (quoteAuto.Drivers.Count != 0)	 //Fill Driver Info. From Quote.
                        {
                            for (int i = 0; i < quoteAuto.Drivers.Count; i++)
                            {
                                Quotes.AutoDriver ADPolicy = new Quotes.AutoDriver();
                                AD = (AutoDriver)quoteAuto.Drivers[i];
                                AD.Mode = 1;
                                taskControl.Save(userID, null, AD, false);
                                taskControl = FillAutoDriver(AD, ADPolicy, taskControl, quoteAuto);
                            }
                        }

                        if (quoteAuto.AutoCovers.Count != 0)	 //Fill Auto Info. From Quote.
                        {
                            for (int i = 0; i < quoteAuto.AutoCovers.Count; i++)
                            {
                                Quotes.AutoCover ACPolicy = new Quotes.AutoCover();
                                AC = (AutoCover)quoteAuto.AutoCovers[i];
                                Quotes.AutoCover AC2 = (AutoCover)taskControlEndo.AutoCovers[i];
                                taskControl = FillAutoCover(AC, ACPolicy, taskControl, quoteAuto); //double cost = double.Parse(AC.ActualValue.ToString());

                                AC.Mode = 1;
                                AC.VIN = AC2.VIN;
                                //AC.PurchaseDate = AC2.PurchaseDate;
                                AC.PurchaseDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC2.PurchaseDate).ToShortDateString());

                                AC.Plate = AC2.Plate;
                                AC.VehicleAge = AC2.VehicleAge;
                                AC.License = AC2.License;
                                //AC.LicenseExpDate = AC2.LicenseExpDate;
                                AC.LicenseExpDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC2.LicenseExpDate).ToShortDateString());

                                double cost = double.Parse(AC.ActualValue.ToString());
                                double totcost = cost;// * 0.85;
                                AC.Cost = AC.Cost;  //decimal.Parse(totcost.ToString());
                                AC.ActualValue = decimal.Parse(totcost.ToString());

                                taskControl.Save(userID, AC, null, false);
                            }
                        }
                    }
                }
                else
                {
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
                                AD.Mode = 1;
                                taskControl.Save(userID, null, AD, false);
                                taskControl = FillAutoDriver(AD, ADPolicy, taskControl, quoteAuto);
                            }
                        }

                        if (quoteAuto.AutoCovers.Count != 0)	 //Fill Auto Info. From Quote.
                        {
                            for (int i = 0; i < quoteAuto.AutoCovers.Count; i++)
                            {
                                Quotes.AutoCover ACPolicy = new Quotes.AutoCover();
                                AC = (AutoCover)quoteAuto.AutoCovers[i];
                                Quotes.AutoCover AC2 = (AutoCover)quoteAuto.AutoCovers[i];

                                taskControl = FillAutoCover(AC, ACPolicy, taskControl, quoteAuto);
                                AC.Mode = 1;
                                AC.VIN = AC2.VIN;
                                //AC.PurchaseDate = AC2.PurchaseDate;
                                AC.PurchaseDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC2.PurchaseDate).ToShortDateString());

                                AC.Plate = AC2.Plate;
                                AC.VehicleAge = AC2.VehicleAge;
                                AC.License = AC2.License;
                               // AC.LicenseExpDate = AC2.LicenseExpDate;
                                AC.LicenseExpDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(AC2.LicenseExpDate).ToShortDateString());

                                double cost = double.Parse(AC.ActualValue.ToString());
                                double totcost = cost;// * 0.85;
                                AC.Cost = AC.Cost;  //decimal.Parse(totcost.ToString());
                                AC.ActualValue = decimal.Parse(totcost.ToString());

                                taskControl.Save(userID, AC, null, false);
                            }
                        }
                    }
                }

                taskControl.TaskControlID = 0;

                Session.Clear();
                Session.Add("Reistatement", "REI");
                Session.Add("TaskControl", taskControl);
                Response.Redirect("QuoteAuto.aspx", false);
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }
        protected void btnCustInfo_Click(object sender, EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            Customer.Customer customer;
            customer = QA.Customer;

            customer.Birthday = Convert.ToDateTime(customer.Birthday).ToString("MM/dd/yyyy"); 
            customer.Mode = 2;    //Update
           // customer.BusinessName1=ddlInsuranceCompany.SelectedItem.Text.Trim();
            //customer.LastName2= ddlInsuranceCompany.SelectedItem.Text.Trim();
            Session["Customer"] = customer;
            RemoveSessionLookUp();
            Session.Add("QuoteAuto", "QuoteAuto");  // Para indicar en la pantalla de Customer que tiene que regresar al Application Auto.
            Response.Redirect("ClientIndividual.aspx");
        }

        private List<string> ImprimirPP0170(List<string> mergePaths, int quoteAuto1, int j2, string Copy)
        {
            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

            EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"];
            int s = taskControl.TaskControlID;

            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter ds1 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicyByTaskControlIDTableAdapter();
            GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter ds2 = new GetReportAutoPolicyByTaskControlIDTableAdapters.GetReportAutoPolicy1ByTaskControlIDTableAdapter();
            //GetReportDoubleInterestTableAdapters.GetReportAutoPolicy1ByTaskControlIDDoubleInterestTableAdapter ds2 = new GetReportDoubleInterestTableAdapters.GetReportAutoPolicy1ByTaskControlIDDoubleInterestTableAdapter();
            //GetReportAutoDITableAdapters.GetReportAutoDITableAdapter ds3 = new GetReportAutoDITableAdapters.GetReportAutoDITableAdapter();


            ReportDataSource rds1 = new ReportDataSource();
            ReportDataSource rds2 = new ReportDataSource();

            ReportParameter rp = new ReportParameter();

            try
            {
                rds1 = new ReportDataSource("GetReportAutoPolicyByTaskControlID", (DataTable)ds1.GetData(taskControl.TaskControlID));
                //rds2 = new ReportDataSource("GetReportAutoPolicy1ByTaskControlID", (DataTable)ds2.GetData(taskControl.TaskControlID, quoteAuto1, j2));

                //rp = new ReportParameter("Copy", Copy);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/AutoPersonales/PP0170.rdlc");
            viewer1.LocalReport.DataSources.Add(rds1);
            //viewer1.LocalReport.DataSources.Add(rds2);
            //viewer1.LocalReport.SetParameters(rp);
            viewer1.LocalReport.Refresh();

            Warning[] warnings1;
            string[] streamIds1;
            string mimeType1;
            string encoding1 = string.Empty;
            string extension1;

            string fileName1 = "PP0170- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-7" + quoteAuto1;
            string _FileName1 = "PP0170- " + taskControl.Policy.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-7" + quoteAuto1 + ".pdf";

            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

            byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType1, out encoding1, out extension1, out streamIds1, out warnings1);

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
                //litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString(ecp.Message);
                //litPopUp.Visible = true;
                lblRecHeader.Text = ecp.Message;
                mpeSeleccion.Show();
            }
            return mergePaths;
        }

        #region XML
        public void PolicyXML(int TaskControlID)
        {
			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                SqlConnection conn4 = null;
                SqlDataReader rdr4 = null;
                XmlDocument xmlDoc = new XmlDocument();
                SqlConnection conn = null;
                SqlConnection conn0 = null;
                SqlConnection conn1 = null;
                SqlConnection conn5 = null;
                SqlConnection conn6 = null;

                string ConnectionString = ConfigurationManager.ConnectionStrings["GuardianConnectionString"].ConnectionString;
                string Encoding = "";
                Session["Gap_Towing_String"] = "";
                

                conn = new SqlConnection(ConnectionString);
                conn0 = new SqlConnection(ConnectionString);
                conn1 = new SqlConnection(ConnectionString);
                conn4 = new SqlConnection(ConnectionString);
                conn5 = new SqlConnection(ConnectionString);
                conn6 = new SqlConnection(ConnectionString);

                conn4.Open();

                SqlCommand cmd4 = new SqlCommand("GetTaskControlByTaskControlID", conn4);

                cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                cmd4.Parameters.AddWithValue("@TaskControlID", TaskControlID);
                cmd4.CommandTimeout = 0;
                rdr4 = cmd4.ExecuteReader();

                NAMECONVENTION = DateTime.Now.ToString("MM.dd.yyyy_hhmmss");

                rdr4.Read();

                SqlDataReader rdr6 = null;

                conn6.Open();

                SqlCommand cmd6 = new SqlCommand("GetReportAutoVehiclesInfoPolicy", conn6); // GetReportAutoVehiclesInfoPolicy_VI

                cmd6.CommandType = System.Data.CommandType.StoredProcedure;

                cmd6.Parameters.AddWithValue("@TaskControlID", TaskControlID);
                cmd6.CommandTimeout = 0;
                rdr6 = cmd6.ExecuteReader();

                int HasMotorcycle = 0;
                string ADDPREMIUM1 = "0";

                while (rdr6.Read())
                {
                    if (rdr6["IsMotorcycleScooter"].ToString().Trim() == "True")
                    {
                        HasMotorcycle = HasMotorcycle + 1;
                    }

                    if (rdr6["ADDPremium"].ToString().Trim() != "0")
                    {
                        ADDPREMIUM1 = "1";
                    }
                    
                }

                
                rdr6.Read();

                SqlDataReader rdr5 = null;

                conn5.Open();

                SqlCommand cmd5 = new SqlCommand("GetReportAutoPolicyByTaskControlID", conn5); //GetReportAdditionalAutoDriversInfoPoliza_VI

                cmd5.CommandType = System.Data.CommandType.StoredProcedure;

                cmd5.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                cmd5.CommandTimeout = 0;
                rdr5 = cmd5.ExecuteReader();


                //if (rdr5.HasRows && rdr6["PolicyType"].ToString().Trim() != "BAP")   //if (rdr5.HasRows && rdr6["PolicyType"].ToString().Trim() != "BAP")
                //    Encoding = "UTF-16";
                //else if (rdr6["PolicyType"].ToString().Trim() == "BAP")
                //    Encoding = "UTF-16";
                //else
                    Encoding = "UTF-16";

                #region XML Policy Info

                XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", Encoding, null);
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

                SqlDataReader rdr = null;

                conn.Open();

                SqlCommand cmd = new SqlCommand("GetReportAutoGeneralInfo", conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                cmd.CommandTimeout = 0;
                rdr = cmd.ExecuteReader();

                SqlDataReader rdr1 = null;

                conn1.Open();

                SqlCommand cmd1 = new SqlCommand("GetReportAutoVehiclesInfoPolicy", conn1); //GetReportAutoVehiclesInfoPolicy_VI

                cmd1.CommandType = System.Data.CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                cmd1.CommandTimeout = 0;
                rdr1 = cmd1.ExecuteReader();

                SqlDataReader rdr0 = null;

                conn0.Open();


                SqlCommand cmd0 = new SqlCommand("GetReportAutoPolicyDriversByTaskControlID", conn0); //GetReportAdditionalAutoDriversInfoPoliza_VI

                cmd0.CommandType = System.Data.CommandType.StoredProcedure;

                cmd0.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                cmd0.CommandTimeout = 0;
                rdr0 = cmd0.ExecuteReader();

                while (rdr.Read())
                {
                    XmlElement xmlPolicy1 = xmlDoc.CreateElement("Policy");  // Creacion del elemento
                    xmlPolicy.AppendChild(xmlPolicy1);  // Abajo de donde vas a "append" el elemento
                    XmlElement xmlPolicyID = xmlDoc.CreateElement("PolicyID");
                    xmlPolicy1.AppendChild(xmlPolicyID);

                     //if (rdr["PRenewalNo"].ToString().Trim() != "")
                    if(QA.Policy.Ren_Rei == "RN")
                    {
                        try
                        {
                            xmlPolicyID.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + txtSuffix.Text.Trim();
                            //if (rdr["Suffix"].ToString().Trim() != "")
                            //    xmlPolicyID.InnerText = rdr["PRenewalNo"].ToString().Contains("-") ? rdr["PRenewalNo"].ToString().Substring(0, rdr["PRenewalNo"].ToString().Trim().IndexOf("-")).Replace("-", "").Trim() + "-" + (int.Parse(rdr["Suffix"].ToString().Trim())).ToString() : rdr["PRenewalNo"].ToString() + "-" + rdr["Suffix"].ToString().Trim();
                            //else
                            //    xmlPolicyID.InnerText = rdr["PRenewalNo"].ToString().Contains("-") ? rdr["PRenewalNo"].ToString().Substring(0, rdr["PRenewalNo"].ToString().Trim().IndexOf("-")).Replace("-", "").Trim() + "-" + (int.Parse(rdr["Suffix"].ToString().Trim()) - 1).ToString() : rdr["PRenewalNo"].ToString() + "-" + rdr["Suffix"].ToString().Trim();m();
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        xmlPolicyID.InnerText = rdr["PolicyType"].ToString().Trim();
                    }

                    string PolicyType = "";
                    string PolSubType = "0";

                    if (rdr["PolicyType"].ToString().Trim() == "BAP")
                    {
                        //PolicyType = "BAP";
                        PolSubType = "Cml";
                    }
                    else
                    {
                        //PolicyType = "PAP";
                        PolSubType = "Pvt";
                    }

                    XmlElement xmlIncept = xmlDoc.CreateElement("Incept");
                    xmlPolicy1.AppendChild(xmlIncept);
                    xmlIncept.InnerText = DateTime.Parse(rdr["EffectiveDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";    // A donde y que columna de que reader vas a insertar en el texto

                    XmlElement xmlExpire = xmlDoc.CreateElement("Expire");
                    xmlPolicy1.AppendChild(xmlExpire);
                    xmlExpire.InnerText = DateTime.Parse(rdr["ExpirationDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";

					  //if (RenewalNo == "")
                    if (QA.Policy.Ren_Rei.Trim() == "")
                    {
                        XmlElement xmlRenewalOf = xmlDoc.CreateElement("RenewalOf");
                        XmlAttribute attribute1R = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance"); // creacion de un atributo
                        if (RenewalNo != "")
                            attribute1R.Value = "true";  // atributo = a "true"
                        else
                            attribute1R.Value = "false";

                        xmlRenewalOf.Attributes.Append(attribute1R); // "appending el atributo"
                        xmlPolicy1.AppendChild(xmlRenewalOf);
                    }
                    else
                    {
                        XmlElement xmlRenewalOf = xmlDoc.CreateElement("RenewalOf");
                        xmlPolicy1.AppendChild(xmlRenewalOf);
						
                        Policy pol = new Policy();
                        pol =  TaskControl.Policy.GetPolicyQuoteByTaskControlID(QA.Policy.TCIDQuotes,  pol);
                        if(pol.Suffix.Trim() == "00")
                            xmlRenewalOf.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim();
                        else
                            xmlRenewalOf.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + pol.Suffix.Trim();
                    }

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

                    DataTable DtCommision = GetCommissionAgentRateByAgentID(TaskControlID.ToString(), "3"); //GetCommissionAgentRateByAgentID(AgentID.ToString().Trim(), "3");

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
                    xmlBrokerID.InnerText = rdr["AgentID"].ToString().Trim();//rdr["AgentID"].ToString().Trim(); = produccion; "9" = prueba

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
                    XmlAttribute attribute3 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                    attribute3.Value = "true";
                    xmlBinderID.Attributes.Append(attribute3);
                    xmlPolicy1.AppendChild(xmlBinderID);

                    XmlElement xmlComRate = xmlDoc.CreateElement("ComRate");
                    xmlPolicy1.AppendChild(xmlComRate);
                    xmlComRate.InnerText = ComRate;

                    XmlElement xmlClient = xmlDoc.CreateElement("Client");//Cuando viene de una renovacion envio el ClientID que me trajo anteriormente
                    xmlPolicy1.AppendChild(xmlClient);
                    xmlClient.InnerText = rdr["ClientIDPPS"].ToString().Trim() == "" ? "0" : rdr["ClientIDPPS"].ToString().Trim();

                    XmlElement xmlTag = xmlDoc.CreateElement("Tag");
                    XmlAttribute attribute4 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                    attribute4.Value = "true";
                    xmlTag.Attributes.Append(attribute4);
                    xmlPolicy1.AppendChild(xmlTag);

                    XmlElement xmlPremium = xmlDoc.CreateElement("Premium");
                    xmlPolicy1.AppendChild(xmlPremium);
                    xmlPremium.InnerText = rdr["XML_NetPremiumP"].ToString().Trim();

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

                    if (rdr["PRenewalNo"].ToString().Trim() != "")
                    {
                        try
                        {
                            if (rdr["Suffix"].ToString().Trim() != "")
                                xmlUDPolicyID.InnerText = rdr["PRenewalNo"].ToString().Contains("-") ? rdr["PRenewalNo"].ToString().Substring(0, rdr["PRenewalNo"].ToString().Trim().IndexOf("-")).Replace("-", "").Trim() + "-" + (int.Parse(rdr["Suffix"].ToString().Trim())).ToString() : rdr["PRenewalNo"].ToString() + "-" + rdr["Suffix"].ToString().Trim();
                            else
                                xmlUDPolicyID.InnerText = rdr["PRenewalNo"].ToString().Contains("-") ? rdr["PRenewalNo"].ToString().Substring(0, rdr["PRenewalNo"].ToString().Trim().IndexOf("-")).Replace("-", "").Trim() + "-" + (int.Parse(rdr["Suffix"].ToString().Trim()) - 1).ToString() : rdr["PRenewalNo"].ToString() + "-" + rdr["Suffix"].ToString().Trim();
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        xmlUDPolicyID.InnerText = "0";
                    }

                    XmlElement xmlPreparedBy = xmlDoc.CreateElement("PreparedBy");
                    xmlPolicy1.AppendChild(xmlPreparedBy);
                    xmlPreparedBy.InnerText = rdr["EnteredBy"].ToString().Trim();

                    XmlElement xmlExcessLink = xmlDoc.CreateElement("ExcessLink");
                    xmlPolicy1.AppendChild(xmlExcessLink);
                    xmlExcessLink.InnerText = "0";

                    XmlElement xmlPolSubType = xmlDoc.CreateElement("PolSubType");
                    xmlPolicy1.AppendChild(xmlPolSubType);
                    xmlPolSubType.InnerText = PolSubType.ToString();//PolSubType.ToString() Pvt personal Cml comercial; "0" = Prueba

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

                    XmlElement xmlPolRelTable = xmlDoc.CreateElement("PolRelTable");
                    xmlPolicy1.AppendChild(xmlPolRelTable);

                    XmlElement xmlPolRel = xmlDoc.CreateElement("PolRel");
                    xmlPolRelTable.AppendChild(xmlPolRel);

                    XmlElement xmlPolicy1ID1 = xmlDoc.CreateElement("PolicyID");
                    xmlPolRel.AppendChild(xmlPolicy1ID1);
                    xmlPolicy1ID1.InnerText = rdr["PolicyType"].ToString().Trim();

                    XmlElement xmlUpid = xmlDoc.CreateElement("Upid");
                    xmlPolRel.AppendChild(xmlUpid);
                    xmlUpid.InnerText = "0";

                    XmlElement xmlPolRelat = xmlDoc.CreateElement("Polrelat");
                    xmlPolRel.AppendChild(xmlPolRelat);
                    xmlPolRelat.InnerText = "NI";

                    XmlElement xmlEntNamesTable = xmlDoc.CreateElement("EntNamesTable");
                    xmlPolRel.AppendChild(xmlEntNamesTable);

                    #region DRIVERS

                    //Primary Driver
                    XmlElement xmlEntNames = xmlDoc.CreateElement("EntNames");
                    xmlEntNamesTable.AppendChild(xmlEntNames);

                    string Bflag;
                    string FirstName = "";
                    string Nsbyt = "0";

                    if (rdr["PolicyType"].ToString().Trim() == "BAP")
                        Bflag = "1";
                    else
                        Bflag = "0";

                    if (rdr["CompanyName"].ToString().Trim() == "" && rdr["LastNa1"].ToString().Trim() != "") //Bflag != "0" && 
                    {
                        XmlElement xmlLastName = xmlDoc.CreateElement("LastName");
                        xmlEntNames.AppendChild(xmlLastName);
                        xmlLastName.InnerText = rdr["LastNa1"].ToString().Trim();
                    }
                    else if (rdr["LastNa1"].ToString().Trim() == "" && rdr["CompanyName"].ToString().Trim() == "")
                    {
                        XmlElement xmlLastName = xmlDoc.CreateElement("LastName");
                        xmlEntNames.AppendChild(xmlLastName);
                        xmlLastName.InnerText = rdr["FirstNa"].ToString().Trim();
                    }
                    else
                    {
                        XmlElement xmlLastName = xmlDoc.CreateElement("LastName");
                        xmlEntNames.AppendChild(xmlLastName);
                        xmlLastName.InnerText = rdr["CompanyName"].ToString().Trim();
                    }

                    if (Bflag == "1" && rdr["CompanyName"].ToString().Trim() != "")
                        FirstName = "";
                    else
                        FirstName = rdr["FirstNa"].ToString().Trim();

                    XmlElement xmlFirstName = xmlDoc.CreateElement("FirstName");
                    xmlEntNames.AppendChild(xmlFirstName);
                    xmlFirstName.InnerText = FirstName;

                    XmlElement xmlMiddle = xmlDoc.CreateElement("Middle");
                    xmlEntNames.AppendChild(xmlMiddle);

                    XmlElement xmlUpid1 = xmlDoc.CreateElement("Upid");
                    xmlEntNames.AppendChild(xmlUpid1);
                    xmlUpid1.InnerText = "0";

                    //string DDB = rdr["Birthday"] != System.DBNull.Value ? (string)rdr["Birthday"] : "1900-01-01"; ;

                    XmlElement xmlDob = xmlDoc.CreateElement("Dob");
                    xmlEntNames.AppendChild(xmlDob);
                    xmlDob.InnerText = DateTime.Parse(rdr["Birthday"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";

                    XmlElement xmlSex = xmlDoc.CreateElement("Sex");
                    xmlEntNames.AppendChild(xmlSex);
                    xmlSex.InnerText = rdr["Sex"].ToString().Trim();

                    XmlElement xmlMarital = xmlDoc.CreateElement("Marital");
                    xmlEntNames.AppendChild(xmlMarital);
                    xmlMarital.InnerText = rdr["MaritalStatus"].ToString().Trim();

                    XmlElement xmlYrsexp = xmlDoc.CreateElement("Yrsexp");
                    xmlEntNames.AppendChild(xmlYrsexp);
                    xmlYrsexp.InnerText = "0";

                    XmlElement xmlLicence = xmlDoc.CreateElement("License");
                    XmlAttribute attribute9 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                    attribute9.Value = "true";
                    xmlLicence.Attributes.Append(attribute9);
                    xmlEntNames.AppendChild(xmlLicence);

                    XmlElement xmlState = xmlDoc.CreateElement("State");
                    xmlEntNames.AppendChild(xmlState);
                    xmlState.InnerText = rdr["State"].ToString().Trim();

                    XmlElement xmlSsn = xmlDoc.CreateElement("Ssn");
                    xmlEntNames.AppendChild(xmlSsn);
                    xmlSsn.InnerText = "SSn";

                    string BusFlag;
                    if (rdr["PolicyType"].ToString().Trim() == "BAP" && HasMotorcycle == 0)
                    {
                        XmlElement xmlBusFlag = xmlDoc.CreateElement("BusFlag");
                        xmlEntNames.AppendChild(xmlBusFlag);
                        xmlBusFlag.InnerText = "1";
                        BusFlag = xmlBusFlag.InnerText.ToString();
                    }
                    else
                    {
                        XmlElement xmlBusFlag = xmlDoc.CreateElement("BusFlag");
                        xmlEntNames.AppendChild(xmlBusFlag);
                        xmlBusFlag.InnerText = "0";
                        BusFlag = xmlBusFlag.InnerText.ToString();
                    }

                    if (ADDPREMIUM1 != "0")
                        Nsbyt = "8";
                    else
                        Nsbyt = "0";

                    XmlElement xmlNsbyt = xmlDoc.CreateElement("Nsbyt");
                    xmlEntNames.AppendChild(xmlNsbyt);
                    xmlNsbyt.InnerText = Nsbyt;

                    XmlElement xmlBusOther = xmlDoc.CreateElement("BusOther");
                    XmlAttribute attribute10 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                    attribute10.Value = "true";
                    xmlBusOther.Attributes.Append(attribute10);
                    xmlEntNames.AppendChild(xmlBusOther);

                    XmlElement xmlBusType = xmlDoc.CreateElement("BusType");
                    if (BusFlag != "1" && HasMotorcycle == 0)
                    {
                        XmlAttribute attribute11 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute11.Value = "true";
                        xmlBusType.Attributes.Append(attribute11);
                        xmlEntNames.AppendChild(xmlBusType);
                    }
                    xmlEntNames.AppendChild(xmlBusType);
                    if (BusFlag == "1" && rdr["PolicyType"].ToString().Trim() == "PAP" || BusFlag == "0" && rdr["PolicyType"].ToString().Trim() == "BAP" && HasMotorcycle > 0)
                        xmlBusType.InnerText = "256";
                    else if (BusFlag == "1" && rdr["PolicyType"].ToString().Trim() == "BAP" && HasMotorcycle == 0)
                        xmlBusType.InnerText = "32";

                    XmlElement xmlClient1 = xmlDoc.CreateElement("Client");
                    XmlAttribute attribute12 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                    attribute12.Value = "true";
                    xmlClient1.Attributes.Append(attribute12);
                    xmlEntNames.AppendChild(xmlClient1);

                    XmlElement xmlPolRelat1 = xmlDoc.CreateElement("PolRelat");
                    XmlAttribute attribute13 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                    attribute13.Value = "true";
                    xmlPolRelat1.Attributes.Append(attribute13);
                    xmlEntNames.AppendChild(xmlPolRelat1);

                    XmlElement xmlDispImage1 = xmlDoc.CreateElement("DispImage");
                    xmlEntNames.AppendChild(xmlDispImage1);
                    xmlDispImage1.InnerText = "Person";

                    #region Aditional Drivers
                    while (rdr0.Read()) // loop Drivers // Todos los additional driver se le cambio el busflag = "0" a peticion de Adam
                    {
                        string Gender = "";
                        string MaritalStatus = "";
                        string AditionalCompanyName = "";

                        XmlElement xmlPolRelDRV = xmlDoc.CreateElement("PolRel");
                        xmlPolRelTable.AppendChild(xmlPolRelDRV);

                        XmlElement xmlPolicy1ID1DRV = xmlDoc.CreateElement("PolicyID");
                        xmlPolRelDRV.AppendChild(xmlPolicy1ID1DRV);
                        xmlPolicy1ID1DRV.InnerText = rdr["PolicyType"].ToString().Trim();

                        XmlElement xmlUpidDRV = xmlDoc.CreateElement("Upid");
                        xmlPolRelDRV.AppendChild(xmlUpidDRV);
                        xmlUpidDRV.InnerText = "0";

                        XmlElement xmlPolRelatDRV = xmlDoc.CreateElement("Polrelat");
                        xmlPolRelDRV.AppendChild(xmlPolRelatDRV);
                       xmlPolRelatDRV.InnerText = rdr0["IsAdditionalInsured"].ToString().Trim() == "" ? "AD" : bool.Parse(rdr0["IsAdditionalInsured"].ToString().Trim()) == true ? "NI" : "AD";
                      


                        XmlElement xmlEntNamesTableDRV = xmlDoc.CreateElement("EntNamesTable");
                        xmlPolRelDRV.AppendChild(xmlEntNamesTableDRV);

                        XmlElement xmlEntNamesDRV = xmlDoc.CreateElement("EntNames");
                        xmlEntNamesTableDRV.AppendChild(xmlEntNamesDRV);

                        string BflagDRV;

                        if (rdr["PolicyType"].ToString().Trim() == "BAP")
                        {
                            BflagDRV = "0";
                        }
                        else
                        {
                            BflagDRV = "0";
                        }

                        if ((rdr0["DriverName"].ToString().Trim() != "" && rdr0["DriverLastName"].ToString().Trim() == "") && rdr0["DriverDateOfBirth"].ToString().Trim() == "01/01/1900")
                        {
                            AditionalCompanyName = rdr0["DriverName"].ToString().Trim();
                        }

                        //if (BflagDRV == "0") // rdr["CompanyName"].ToString().Trim() == "")
                        //{
                        XmlElement xmlLastName = xmlDoc.CreateElement("LastName");
                        xmlEntNamesDRV.AppendChild(xmlLastName);
                        xmlLastName.InnerText = AditionalCompanyName == "" ? rdr0["DriverLastName"].ToString().Trim() : AditionalCompanyName;
                        //}
                        //else
                        //{
                        //    XmlElement xmlLastName = xmlDoc.CreateElement("LastName");
                        //    xmlEntNamesDRV.AppendChild(xmlLastName);
                        //    xmlLastName.InnerText = rdr["CompanyName"].ToString().Trim();
                        //}

                        XmlElement xmlFirstNameDRV = xmlDoc.CreateElement("FirstName");
                        xmlEntNamesDRV.AppendChild(xmlFirstNameDRV);
                        xmlFirstNameDRV.InnerText = AditionalCompanyName == "" ? rdr0["DriverName"].ToString().Trim() : "";

                        XmlElement xmlMiddleDRV = xmlDoc.CreateElement("Middle");
                        xmlEntNamesDRV.AppendChild(xmlMiddleDRV);

                        XmlElement xmlUpid1DRV = xmlDoc.CreateElement("Upid");
                        xmlEntNamesDRV.AppendChild(xmlUpid1DRV);
                        xmlUpid1DRV.InnerText = "0";

                        XmlElement xmlDobDRV = xmlDoc.CreateElement("Dob");
                        xmlEntNamesDRV.AppendChild(xmlDobDRV);
                        xmlDobDRV.InnerText = DateTime.Parse(rdr0["DriverDateOfBirth"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";

                        if (rdr0["DriverGender"].ToString().Trim() == "Male")
                        {
                            Gender = "M";
                        }
                        else if (rdr0["DriverGender"].ToString().Trim() == "Female")
                        {
                            Gender = "F";
                        }
                        else
                        {
                            Gender = "U";
                        }

                        XmlElement xmlSexDRV = xmlDoc.CreateElement("Sex");
                        xmlEntNamesDRV.AppendChild(xmlSexDRV);
                        xmlSexDRV.InnerText = Gender;

                        if (rdr0["DriverMaritalStatus"].ToString().Trim() == "Single")
                        {
                            MaritalStatus = "S";
                        }
                        else
                        {
                            MaritalStatus = "M";
                        }

                        XmlElement xmlMaritalDRV = xmlDoc.CreateElement("Marital");
                        xmlEntNamesDRV.AppendChild(xmlMaritalDRV);
                        xmlMaritalDRV.InnerText = MaritalStatus;

                        XmlElement xmlYrsexpDRV = xmlDoc.CreateElement("Yrsexp");
                        xmlEntNamesDRV.AppendChild(xmlYrsexpDRV);
                        xmlYrsexpDRV.InnerText = "0";

                        XmlElement xmlLicenceDRV = xmlDoc.CreateElement("License");
                        XmlAttribute attribute9DRV = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute9DRV.Value = "true";
                        xmlLicenceDRV.Attributes.Append(attribute9DRV);
                        xmlEntNamesDRV.AppendChild(xmlLicenceDRV);

                        XmlElement xmlStateDRV = xmlDoc.CreateElement("State");
                        xmlEntNamesDRV.AppendChild(xmlStateDRV);
                        xmlStateDRV.InnerText = rdr["State"].ToString().Trim();

                        XmlElement xmlSsnDRV = xmlDoc.CreateElement("Ssn");
                        xmlEntNamesDRV.AppendChild(xmlSsnDRV);
                        xmlSsnDRV.InnerText = "SSn";

                        string BusFlagDRV = "0";
                        if (rdr["PolicyType"].ToString().Trim() == "BAP")
                        {
                            XmlElement xmlBusFlagDRV = xmlDoc.CreateElement("BusFlag");
                            xmlEntNamesDRV.AppendChild(xmlBusFlagDRV);
                            if (AditionalCompanyName != "")
                                xmlBusFlagDRV.InnerText = "1";
                            else
                                xmlBusFlagDRV.InnerText = "0";
                            BusFlagDRV = xmlBusFlagDRV.InnerText.ToString();
                        }
                        else
                        {
                            XmlElement xmlBusFlagDRV = xmlDoc.CreateElement("BusFlag");
                            xmlEntNamesDRV.AppendChild(xmlBusFlagDRV);
                            xmlBusFlagDRV.InnerText = "0";
                            BusFlagDRV = xmlBusFlagDRV.InnerText.ToString();
                        }

                        XmlElement xmlNsbytDRV = xmlDoc.CreateElement("Nsbyt");
                        xmlEntNamesDRV.AppendChild(xmlNsbytDRV);
                        xmlNsbytDRV.InnerText = "0";

                        XmlElement xmlBusOtherDRV = xmlDoc.CreateElement("BusOther");
                        XmlAttribute attribute10DRV = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute10DRV.Value = "true";
                        xmlBusOtherDRV.Attributes.Append(attribute10DRV);
                        xmlEntNamesDRV.AppendChild(xmlBusOtherDRV);

                        XmlElement xmlBusTypeDRV = xmlDoc.CreateElement("BusType");
                        if (BusFlagDRV != "0")
                        {
                            XmlAttribute attribute11 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                            attribute11.Value = "true";
                            xmlBusTypeDRV.Attributes.Append(attribute11);
                            xmlEntNamesDRV.AppendChild(xmlBusTypeDRV);
                        }
                        else
                        {
                            xmlEntNamesDRV.AppendChild(xmlBusTypeDRV);
                            if (rdr["PolicyType"].ToString().Trim() == "PAP")//BusFlagDRV == "1" &&
                                xmlBusTypeDRV.InnerText = "256";
                            else if (rdr["PolicyType"].ToString().Trim() == "BAP")//BusFlagDRV == "1" && 
                                xmlBusTypeDRV.InnerText = "32";
                        }

                        XmlElement xmlClient1DRV = xmlDoc.CreateElement("Client");
                        XmlAttribute attribute12DRV = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute12DRV.Value = "true";
                        xmlClient1DRV.Attributes.Append(attribute12DRV);
                        xmlEntNamesDRV.AppendChild(xmlClient1DRV);

                        XmlElement xmlPolRelat1DRV = xmlDoc.CreateElement("PolRelat");
                        XmlAttribute attribute13DRV = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute13DRV.Value = "true";
                        xmlPolRelat1DRV.Attributes.Append(attribute13DRV);
                        xmlEntNamesDRV.AppendChild(xmlPolRelat1DRV);

                        XmlElement xmlDispImage1DRV = xmlDoc.CreateElement("DispImage");
                        xmlEntNamesDRV.AppendChild(xmlDispImage1DRV);
                        xmlDispImage1DRV.InnerText = "Person";
                    }
                    #endregion Aditional Drivers

                    #endregion Drivers

                    XmlElement xmlVehicleTable = xmlDoc.CreateElement("VehicleTable");
                    xmlPolicy1.AppendChild(xmlVehicleTable);

                    string Payee = "";

                    while (rdr1.Read())
                    {
                        string End22 = "0";
                        string End23 = "0";
                        string InsVal = "0";
                        string LicensePlateNo = "0";

                        XmlElement xmlVehicle = xmlDoc.CreateElement("Vehicle");
                        xmlVehicleTable.AppendChild(xmlVehicle);

                        XmlElement xmlVin = xmlDoc.CreateElement("Vin");
                        xmlVehicle.AppendChild(xmlVin);
                        xmlVin.InnerText = rdr1["VIN"].ToString().Trim();

                        XmlElement xmlPolicy1Id = xmlDoc.CreateElement("PolicyID");
                        xmlVehicle.AppendChild(xmlPolicy1Id);
                        xmlPolicy1Id.InnerText = rdr["PolicyType"].ToString().Trim();

                        if (rdr["HasCommercial"].ToString().Trim() == "1")
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "CML";
                        }
                        else if (rdr["HasPrivatePassenger"].ToString().Trim() == "1")
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "OTH";
                        }
                        else if (rdr["HasPrivate"].ToString().Trim() == "1")
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "PVT";
                        }
                        else if (rdr["HasRental"].ToString().Trim() == "1")
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "RNTL";
                        }
                        else if (rdr["HasTaxi"].ToString().Trim() == "1")
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "TAXI";
                        }
                        else
                        {
                            XmlElement xmlUseClass = xmlDoc.CreateElement("UseClass");
                            xmlVehicle.AppendChild(xmlUseClass);
                            xmlUseClass.InnerText = "0";
                        }

                        if (rdr1["LicensePlateNo"].ToString().Trim() != "")
                        {
                            LicensePlateNo = rdr1["LicensePlateNo"].ToString().Trim();
                        }
                        else
                        {
                            LicensePlateNo = "0";
                        }

                        XmlElement xmlLicPlate = xmlDoc.CreateElement("LicPlate");
                        //XmlAttribute attribute14 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute14.Value = "true";
                        //xmlLicPlate.Attributes.Append(attribute14);
                        xmlVehicle.AppendChild(xmlLicPlate);
                        xmlLicPlate.InnerText = LicensePlateNo;


                        XmlElement xmlPurchDate = xmlDoc.CreateElement("PurchDate");
                        xmlVehicle.AppendChild(xmlPurchDate);
                        xmlPurchDate.InnerText = "1753-01-01T00:00:00";

                        XmlElement xmlActCost = xmlDoc.CreateElement("ActCost");
                        xmlVehicle.AppendChild(xmlActCost);
                        xmlActCost.InnerText = rdr1["VehicleValue"].ToString().Trim();

                        if (rdr1["PDPremium"].ToString() != "0")
                            InsVal = rdr1["VehicleValue"].ToString().Trim();
                        else
                            InsVal = "0.0000";

                        XmlElement xmlInsVal = xmlDoc.CreateElement("InsVal");
                        xmlVehicle.AppendChild(xmlInsVal);
                        xmlInsVal.InnerText = InsVal;

                        XmlElement xmlInsValFlag = xmlDoc.CreateElement("InsValFlag");
                        xmlVehicle.AppendChild(xmlInsValFlag);
                        //XmlAttribute attribute14 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute14.Value = "true";
                        //xmlInsValFlag.Attributes.Append(attribute14);
                        xmlInsValFlag.InnerText = "Actual Cash Value";

                        //if (rdr1["BankDesc"].ToString().Trim() != "")
                        //{
                        Payee = rdr1["BankDesc"].ToString().Trim();
                        //}

                        XmlElement xmlPayee = xmlDoc.CreateElement("Payee");
                        xmlVehicle.AppendChild(xmlPayee);
                        //XmlAttribute attribute15 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        //attribute15.Value = "true";
                        //xmlPayee.Attributes.Append(attribute15);
                        xmlPayee.InnerText = Payee;

                        XmlElement xmlIsland = xmlDoc.CreateElement("Island");
                        xmlVehicle.AppendChild(xmlIsland);
                        string Island = rdr1["Island"].ToString().Trim();
                        if (rdr1["Island"].ToString().Trim() != "")
                        {
                            if (Island == "St. Croix")
                                Island = "2";

                            if (Island == "St. John")
                                Island = "3";

                            if (Island == "St. Thomas")
                                Island = "1";
                        }
                        xmlIsland.InnerText = Island;

                        XmlElement xmlLeased = xmlDoc.CreateElement("Leased");
                        xmlVehicle.AppendChild(xmlLeased);
                        xmlLeased.InnerText = "0";

                        XmlElement xmlRegExp = xmlDoc.CreateElement("RegExp");
                        xmlVehicle.AppendChild(xmlRegExp);
                        xmlRegExp.InnerText = "0";

                        XmlElement xmlPAE = xmlDoc.CreateElement("PAE");
                        xmlVehicle.AppendChild(xmlPAE);
                        xmlPAE.InnerText = "0";

                        if (rdr1["RentalReim"].ToString() != "0" && rdr1["RentalReim"].ToString() != "" && rdr1["RentalReim"].ToString() != "0.0000")
                            End22 = "1";
                        else
                            End22 = "0";

                        XmlElement xmlEnd22 = xmlDoc.CreateElement("End22");
                        xmlVehicle.AppendChild(xmlEnd22);
                        xmlEnd22.InnerText = End22;

                        if (rdr["HasTaxi"].ToString().Trim() == "1" && rdr1["TaxiLossAmount"].ToString() != "0")
                            End23 = "1";
                        else
                            End23 = "0";

                        XmlElement xmlEnd23 = xmlDoc.CreateElement("End23");
                        xmlVehicle.AppendChild(xmlEnd23);
                        xmlEnd23.InnerText = End23;

                        XmlElement xmlPayeeID = xmlDoc.CreateElement("PayeeID");
                        xmlVehicle.AppendChild(xmlPayeeID);
                        xmlPayeeID.InnerText = "274";

                        XmlElement xmlTagNumber = xmlDoc.CreateElement("TagNumber");
                        XmlAttribute attribute16 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                        attribute16.Value = "true";
                        xmlTagNumber.Attributes.Append(attribute16);
                        xmlVehicle.AppendChild(xmlTagNumber);

                        XmlElement xmlPhysVehicleTable = xmlDoc.CreateElement("PhysVehicleTable");
                        xmlVehicle.AppendChild(xmlPhysVehicleTable);

                        XmlElement xmlPhysVehicle = xmlDoc.CreateElement("PhysVehicle");
                        xmlPhysVehicleTable.AppendChild(xmlPhysVehicle);

                        XmlElement xmlVin1 = xmlDoc.CreateElement("Vin");
                        xmlPhysVehicle.AppendChild(xmlVin1);
                        xmlVin1.InnerText = rdr1["VIN"].ToString().Trim();

                        XmlElement xmlMYear = xmlDoc.CreateElement("MYear");
                        xmlPhysVehicle.AppendChild(xmlMYear);
                        xmlMYear.InnerText = rdr1["VehicleYear"].ToString().Trim();

                        XmlElement xmlMake = xmlDoc.CreateElement("Make");
                        xmlPhysVehicle.AppendChild(xmlMake);
                        xmlMake.InnerText = rdr1["VehicleMake"].ToString().Trim();

                        XmlElement xmlModel = xmlDoc.CreateElement("Model");
                        xmlPhysVehicle.AppendChild(xmlModel);
                        xmlModel.InnerText = rdr1["VehicleModel"].ToString().Trim();

                        XmlElement xmlBodyType = xmlDoc.CreateElement("BodyType");
                        xmlPhysVehicle.AppendChild(xmlBodyType);
                        xmlBodyType.InnerText = HasMotorcycle == 0 ? "PU" : "MC";//PU = Vehicle MC = Motorcycle

                        XmlElement xmlCylinder = xmlDoc.CreateElement("Cylinder");
                        xmlPhysVehicle.AppendChild(xmlCylinder);
                        xmlCylinder.InnerText = "0";

                        XmlElement xmlPassengers = xmlDoc.CreateElement("Passengers");
                        xmlPhysVehicle.AppendChild(xmlPassengers);
                        xmlPassengers.InnerText = rdr1["PassengersNo"].ToString().Trim();

                        XmlElement xmlTwoTon = xmlDoc.CreateElement("TwoTon");
                        string TwoTon = rdr1["Over2Tons"].ToString().Trim();
                        xmlPhysVehicle.AppendChild(xmlTwoTon);
                        if (rdr1["Over2Tons"].ToString().Trim() != "")
                        {
                            if (TwoTon == "True")
                                TwoTon = "1";

                            if (TwoTon == "False")
                                TwoTon = "0";
                        }
                        xmlTwoTon.InnerText = TwoTon;

                        XmlElement xmlSalvaged = xmlDoc.CreateElement("Salvaged");
                        xmlPhysVehicle.AppendChild(xmlSalvaged);
                        xmlSalvaged.InnerText = "0";

                        XmlElement xmlVehicleCvrgTable = xmlDoc.CreateElement("VehicleCvrgTable");
                        xmlVehicle.AppendChild(xmlVehicleCvrgTable);

                        SqlConnection conn2 = null;
                        SqlDataReader rdr2 = null;

                        conn2 = new SqlConnection(ConnectionString);

                        conn2.Open();

                        SqlCommand cmd2 = new SqlCommand("GetReportAutoVehiclesInfoPolicy", conn2); //GetReportAutoVehiclesInfoPolicy_VI

                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd2.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                        cmd2.CommandTimeout = 0;

                        rdr2 = cmd2.ExecuteReader();

                        #region All Coverages

                        //while (rdr2.Read())
                        //{
                        //rdr2.Read();

                        double TotalAgeSurcharge = 0;
                        double RentalReim = 0;
                        double TowingPrem=0.00;
                        double TaxiLossIncome = 0;
                        double ADDPremium = 0;
                        double AssistancePremium = 0;
                        double Gap = 0;
                        bool Towing;
                        bool Assistance;


                        

                        if (rdr1["UnderAgeSurcharge"].ToString().Trim() != "NULL")
                        {
                            if (double.Parse(rdr1["UnderAgeSurcharge"].ToString().Trim()) != 0.0)
                            {
                                int CoverageCount = 0;

                                double UnderAgeSurcharge = 0; //CompPremium = 0, CollPremium = 0, BIPremium = 0, PDPremium = 0, MPPremium = 0 ;

                                if (rdr1["CompPremium"].ToString() != "0")
                                {
                                    CoverageCount++;
                                }

                                if (rdr1["CollPremium"].ToString() != "0")
                                {
                                    CoverageCount++;
                                }

                                if (rdr1["BIPremium"].ToString() != "0")
                                {
                                    CoverageCount++;
                                }

                                if (rdr1["PDPremium"].ToString() != "0")
                                {
                                    CoverageCount++;
                                }

                                if (rdr1["MPPremium"].ToString() != "0")
                                {
                                    CoverageCount++;
                                }

                                if (CoverageCount > 0)
                                {
                                    UnderAgeSurcharge = double.Parse(rdr1["UnderAgeSurcharge"].ToString().Trim()) / CoverageCount;

                                    TotalAgeSurcharge = Math.Round(UnderAgeSurcharge, 0);
                                }
                            }
                        }

                        if (rdr1["LeaseLoanGapPremium"].ToString() != "0")
                        {
                            if (double.Parse(rdr1["LeaseLoanGapPremium"].ToString()) > 0.00)
                            {
                                Gap = double.Parse(rdr1["LeaseLoanGapPremium"].ToString());
                                Session["Gap"] = Gap;
                            }
                            else
                            {
                                Gap = 0.00;
                                Session["Gap"] = 0.00;
                            }

                            Session["Gap_Towing_String"]=
                            Session["Gap_Towing_String"].ToString().Trim()+
                            rdr1["VehicleNumber"].ToString().Trim() +"|"+
                            rdr1["VIN"].ToString().Trim() + "|" +
                            Gap.ToString();
                        }

                        if (rdr1["TowingPremium"].ToString() != "0")
                        {
                            if (double.Parse(rdr1["TowingPremium"].ToString()) > 0.00)
                            {
                                Towing = true;
                                Session["Towing"] = "1";
                                TowingPrem=double.Parse(rdr1["TowingPremium"].ToString());

                            }
                            else
                            {
                                Towing = false;
                                Session["Towing"] = "0";
                                TowingPrem = 0.00;
                            }
                            Session["Gap_Towing_String"] = Session["Gap_Towing_String"].ToString().Trim() + "|" + Session["Towing"].ToString().Trim() + "|";

                        }
                        //aqui
                        if (rdr1["AssistancePremium"].ToString() != "0")
                        {
                            if (double.Parse(rdr1["AssistancePremium"].ToString()) > 0.00)
                            {
                                Assistance = true;
                                Session["AssistancePremium"] = rdr1["AssistancePremium"].ToString().Trim();
                                AssistancePremium = double.Parse(rdr1["AssistancePremium"].ToString());

                            }
                            else
                            {
                                Assistance = false;
                                Session["AssistancePremium"] = rdr1["AssistancePremium"].ToString().Trim(); // "0";
                                AssistancePremium = 0.00;
                            }
                            Session["Gap_Towing_String"] = Session["Gap_Towing_String"].ToString().Trim() + Session["AssistancePremium"].ToString().Trim() + "|";

                        }


                        if (rdr1["TaxiLossAmount"].ToString() != "NULL")
                        {
                            if (rdr1["TaxiLossAmount"].ToString() == "0")
                            {
                                if (rdr1["RentalReim"].ToString() != "0" && rdr1["RentalReim"].ToString() != "" && rdr1["RentalReim"].ToString() != "0.0000")
                                {
                                    RentalReim = double.Parse(rdr1["RentalReim"].ToString());

                                }
                            }

                            if (rdr1["TaxiLossAmount"].ToString() != "0")
                            {
                                TaxiLossIncome = double.Parse(rdr1["TaxiLossAmount"].ToString());
                            }
                        }
                        if (rdr1["ADDPremium"].ToString() != "0")
                        {
                            ADDPremium = double.Parse(rdr1["ADDPremium"].ToString());
                        }

                        string hello = rdr1["CompPremium"].ToString();
                        if (rdr1["CompPremium"].ToString() != "0")
                        {
                            //35%
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "12212";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                if (rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "") != "")
                                { xmlLim1.InnerText = decimal.Parse(rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "")).ToString(); }
                                else { xmlLim1.InnerText = "0.0000"; }

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                if ( rdr1["NetCompPremium"].ToString().Trim()!= "")
                                {xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse(rdr1["NetCompPremium"].ToString().Trim())) * 0.35), 0).ToString();  }
                                else { xmlPremium1.InnerText = "0.0000"; }
                                

                                //Math.Round((double.Parse(rdr1["NetCompPremium"].ToString().Trim()) * (1 - (35 / 100))), 0).ToString();
                                //Math.Round(((double.Parse(rdr1["CompPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1 - (double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))), 0).ToString();
                            }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "05211";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                if (rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "") != "")
                                { xmlLim1.InnerText = decimal.Parse(rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "")).ToString(); }
                                else { xmlLim1.InnerText = "0.0000"; }

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                if ( rdr1["NetCompPremium"].ToString().Trim()!= "")
                                {xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse(rdr1["NetCompPremium"].ToString().Trim())) * 0.35), 0).ToString();}
                                else { xmlPremium1.InnerText = "0.0000"; }
                                
                                //Math.Round(((double.Parse(rdr1["CompPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1-(double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))),0).ToString();
                            }

                            //65%
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "11212";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                if (rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "") != "")
                                { xmlLim1.InnerText = decimal.Parse(rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "")).ToString(); }
                                else { xmlLim1.InnerText = "0.0000"; }

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                 if (rdr1["NetCompPremium"].ToString().Trim() != "")
                                {  xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse(rdr1["NetCompPremium"].ToString().Trim())) * 0.65), 0).ToString();}
                                 else { xmlPremium1.InnerText  = "0.0000"; }
                               
                                //Math.Round((double.Parse(rdr1["NetCompPremium"].ToString().Trim()) * (1 - (35 / 100))), 0).ToString();
                                //Math.Round(((double.Parse(rdr1["CompPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1 - (double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))), 0).ToString();
                            }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "04211";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                if (rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "") != "")
                                { xmlLim1.InnerText = decimal.Parse(rdr1["ComprehensiveDedu"].ToString().Trim().Replace("$", "")).ToString(); }
                                else { xmlLim1.InnerText = "0.0000"; }

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                if (rdr1["NetCompPremium"].ToString().Trim() != "")
                                {xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse(rdr1["NetCompPremium"].ToString().Trim())) * 0.65), 0).ToString();  }
                                else { xmlPremium1.InnerText= "0.0000"; }
                                
                                //Math.Round(((double.Parse(rdr1["CompPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1-(double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))),0).ToString();
                            }
                        }

                        if (rdr1["CollPremium"].ToString() != "0")
                        {
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "13212";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                 if (rdr1["CollisionDedu"].ToString().Trim().Replace("$", "") != "")
                                {xmlLim1.InnerText = decimal.Parse(rdr1["CollisionDedu"].ToString().Trim().Replace("$", "")).ToString();}
                                 else { xmlLim1.InnerText = "0.0000"; }
                                

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                xmlPremium1.InnerText = rdr1["NetCollPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["CollPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1-(double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))),0).ToString();
                                 if (rdr1["NetCollPremium"].ToString().Trim() != "")
                                {xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse(rdr1["NetCollPremium"].ToString().Trim())) + ApplySurcharge(RentalReim) + ApplySurcharge(TaxiLossIncome) + ApplySurcharge(ADDPremium) + Gap + TowingPrem + AssistancePremium), 0).ToString();
                              }
                                 else { xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse("0.0000")) + ApplySurcharge(RentalReim) + ApplySurcharge(TaxiLossIncome) + ApplySurcharge(ADDPremium) + Gap + TowingPrem + AssistancePremium), 0).ToString(); }
                                }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "06211";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                if (rdr1["CollisionDedu"].ToString().Trim().Replace("$", "") != "")
                                {xmlLim1.InnerText = decimal.Parse(rdr1["CollisionDedu"].ToString().Trim().Replace("$", "")).ToString();}
                                else {xmlLim1.InnerText  = "0.0000"; }
                                

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                xmlPremium1.InnerText = rdr1["NetCollPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["CollPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1-(double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))),0).ToString();
                                 if ( rdr1["NetCollPremium"].ToString().Trim()!= "")
                                {xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse(rdr1["NetCollPremium"].ToString().Trim())) + ApplySurcharge(RentalReim) + ApplySurcharge(TaxiLossIncome) + ApplySurcharge(ADDPremium) + Gap + TowingPrem + AssistancePremium), 0).ToString();}
                                 else { xmlPremium1.InnerText = Math.Round((ApplySurcharge(double.Parse("0.0000")) + ApplySurcharge(RentalReim) + ApplySurcharge(TaxiLossIncome) + ApplySurcharge(ADDPremium) + Gap + TowingPrem + AssistancePremium), 0).ToString(); }
                                }
                        }

                        if (rdr1["BIPremium"].ToString() != "0")
                        {
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "08194";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                string coll = rdr1["BIPPLimit"].ToString();
                                if (rdr1["BIPPLimit"].ToString() == "10/20") //"$10,000/$20,000"
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "10000.0000"; 

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "20000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "10/25") //"$10,000/$25,000"
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "10000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "25000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "10/50")//$10,000/$50,000
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "10000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "50000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "15/30")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "15000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "30000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "20/40")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "20000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "40000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "25/50")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "25000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "50000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "50/100")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "50000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "100000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "100/200")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "100000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "200000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "100/300")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "100000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "300000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "250/500")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "250000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "500000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "300/300")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "300000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "300000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "500/1000")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "500000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "1000000.0000";
                                }

                                else if (rdr1["BIPPLimit"].ToString() == "0")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0.0000";
                                }
                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                 if (rdr1["NetBIPremium"].ToString().Trim() != "")
                                { xmlPremium1.InnerText = Math.Round(ApplySurcharge(double.Parse(rdr1["NetBIPremium"].ToString().Trim())), 0).ToString();  }
                                 else { xmlPremium1.InnerText = "0.0000"; }
                               
                                //rdr1["NetBIPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["BIPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1-(double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))),0).ToString();
                            }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "01192";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                if (rdr1["BIPPLimit"].ToString() == "10/20") //$10,000/$20,000
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "10000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "20000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "10/25") //$10,000/$25,000
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "10000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "25000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "10/50")//$10,000/$50,000
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "10000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "50000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "15/30")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "15000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "30000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "20/40")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "20000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "40000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "25/50")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "25000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "50000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "50/100")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "50000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "100000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "100/200")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "100000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "200000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "100/300")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "100000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "300000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "250/500")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "250000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "500000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "300/300")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "300000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "300000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "500/1000")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "500000.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "1000000.0000";
                                }
                                else if (rdr1["BIPPLimit"].ToString() == "0")
                                {
                                    XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                    xmlVehicleCvrg.AppendChild(xmlLim1);
                                    xmlLim1.InnerText = "0.0000";

                                    XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                    xmlVehicleCvrg.AppendChild(xmlLim2);
                                    xmlLim2.InnerText = "0.0000";
                                }

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                 if (rdr1["NetBIPremium"].ToString().Trim() != "")
                                { xmlPremium1.InnerText = Math.Round(ApplySurcharge(double.Parse(rdr1["NetBIPremium"].ToString().Trim())), 0).ToString();  }
                                 else { xmlPremium1.InnerText = "0.0000"; }
                               
                                //rdr1["NetBIPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["BIPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1 - (double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))), 0).ToString();
                            }
                        }

                        if (rdr1["PDPremium"].ToString() != "0")
                        {
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "09194";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                if (rdr1["PDLimit"].ToString().Trim().Replace("$", "") != "")
                                {xmlLim1.InnerText = decimal.Parse(rdr1["PDLimit"].ToString().Trim().Replace("$", "")).ToString();}
                                else {xmlLim1.InnerText = "0.0000"; }
                                

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                 if (rdr1["NetPDPremium"].ToString().Trim() != "")
                                {xmlPremium1.InnerText = Math.Round(ApplySurcharge(double.Parse(rdr1["NetPDPremium"].ToString().Trim())), 0).ToString();  }
                                 else { xmlPremium1.InnerText = "0.0000"; }
                                
                                //rdr1["NetPDPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["PDPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1 - (double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))), 0).ToString();
                            }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "02192";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                if (rdr1["PDLimit"].ToString().Trim().Replace("$", "")!="")
                                {xmlLim1.InnerText = decimal.Parse(rdr1["PDLimit"].ToString().Trim().Replace("$", "")).ToString();}
                                else {xmlLim1.InnerText  = "0.0000"; }

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                if (rdr1["NetPDPremium"].ToString().Trim() != "")
                                {xmlPremium1.InnerText = Math.Round(ApplySurcharge(double.Parse(rdr1["NetPDPremium"].ToString().Trim())), 0).ToString();}
                                else {xmlPremium1.InnerText = "0.0000"; }
                                
                                //rdr1["NetPDPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["PDPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1 - (double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))), 0).ToString();
                            }
                        }

                        if (rdr1["MPPremium"].ToString() != "0")
                        {
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "10194";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                xmlLim1.InnerText = "1000.0000";

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "5000.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                 if (rdr1["NetMPPremium"].ToString().Trim() != "")
                                {xmlPremium1.InnerText = Math.Round(ApplySurcharge(double.Parse(rdr1["NetMPPremium"].ToString().Trim())), 0).ToString();}
                                else {xmlPremium1.InnerText = "0.0000"; }
                                
                                //rdr1["NetMPPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["MPPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1 - (double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))), 0).ToString();
                            }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "03192";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                xmlLim1.InnerText = "1000.0000";

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "5000.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                 if ( rdr1["NetMPPremium"].ToString().Trim()!= "")
                                {xmlPremium1.InnerText = Math.Round(ApplySurcharge(double.Parse(rdr1["NetMPPremium"].ToString().Trim())), 0).ToString();  }
                                else {xmlPremium1.InnerText = "0.0000"; }
                                
                                //rdr1["NetMPPremium"].ToString().Trim();
                                //Math.Round(((double.Parse(rdr1["MPPremium"].ToString().Trim()) + TotalAgeSurcharge) * (1 - (double.Parse(rdr1["TotalDiscountPct"].ToString().Trim()) / 100))), 0).ToString();
                            }
                        }

                        if (rdr1["MotoristPremium"].ToString() != "0")
                        {
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "81194";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                xmlLim1.InnerText = "0.0000";

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                xmlPremium1.InnerText = rdr1["MotoristPremium"].ToString().Trim() != "" ? ApplySurcharge(double.Parse(rdr1["MotoristPremium"].ToString().Trim())).ToString() : "";
                            }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "78192";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                xmlLim1.InnerText = "0.0000";

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                xmlPremium1.InnerText = rdr1["MotoristPremium"].ToString().Trim() != "" ? ApplySurcharge(double.Parse(rdr1["MotoristPremium"].ToString().Trim())).ToString() : "";
                            }
                        }
                        else
                        {
                            if (rdr1["PolicyType"].ToString().Trim() == "BAP")
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "81194";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                xmlLim1.InnerText = "0.0000";

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                xmlPremium1.InnerText = "0";
                            }
                            else //PAP
                            {
                                XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                xmlVehicleCvrg.AppendChild(xmlVin2);
                                xmlVin2.InnerText = rdr1["VIN"].ToString().Trim();

                                XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                xmlReinsAsl.InnerText = "78192";

                                XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                xmlLim1.InnerText = "0.0000";

                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                xmlLim2.InnerText = "0.0000";

                                XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                xmlVehicleCvrg.AppendChild(xmlPremium1);
                                xmlPremium1.InnerText = "0";
                            }
                        }

                        #endregion

                        conn2.Close();
                    }

                    XmlElement xmlClientTable = xmlDoc.CreateElement("ClientTable");
                    xmlPolicy1.AppendChild(xmlClientTable);

                    XmlElement xmlClient0 = xmlDoc.CreateElement("Client");
                    xmlClientTable.AppendChild(xmlClient0);

                    XmlElement xmlClient2 = xmlDoc.CreateElement("Client");
                    xmlClient0.AppendChild(xmlClient2);
                    xmlClient2.InnerText = "0";//rdr["CustomerNo"].ToString().Trim();

                    XmlElement xmlMaddr1 = xmlDoc.CreateElement("Maddr1");
                    xmlClient0.AppendChild(xmlMaddr1);
                    xmlMaddr1.InnerText = rdr["Adds1"].ToString().Trim();

                    XmlElement xmlMaddr2 = xmlDoc.CreateElement("Maddr2");
                    xmlClient0.AppendChild(xmlMaddr2);
                    xmlMaddr2.InnerText = rdr["Adds2"].ToString().Trim();

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
                    xmlRaddr1.InnerText = rdr["Adds1PH"].ToString().Trim();

                    XmlElement xmlRaddr2 = xmlDoc.CreateElement("Raddr2");
                    xmlClient0.AppendChild(xmlRaddr2);
                    xmlRaddr2.InnerText = rdr["Adds2PH"].ToString().Trim();

                    XmlElement xmlRaddr3 = xmlDoc.CreateElement("Raddr3");
                    xmlClient0.AppendChild(xmlRaddr3);
                    xmlRaddr3.InnerText = "";

                    XmlElement xmlRcity = xmlDoc.CreateElement("Rcity");
                    xmlClient0.AppendChild(xmlRcity);
                    xmlRcity.InnerText = rdr["CityPH"].ToString().Trim();

                    XmlElement xmlRstate = xmlDoc.CreateElement("Rstate");
                    xmlClient0.AppendChild(xmlRstate);
                    xmlRstate.InnerText = rdr["StatePH"].ToString().Trim();

                    XmlElement xmlRnation = xmlDoc.CreateElement("Rnation");
                    xmlClient0.AppendChild(xmlRnation);
                    xmlRnation.InnerText = "";

                    XmlElement xmlRzip = xmlDoc.CreateElement("Rzip");
                    xmlClient0.AppendChild(xmlRzip);
                    xmlRzip.InnerText = rdr["ZipPH"].ToString().Trim();

                    XmlElement xmlWphone = xmlDoc.CreateElement("Wphone");
                    xmlClient0.AppendChild(xmlWphone);
                    xmlWphone.InnerText = rdr["JobPhone"].ToString().Trim();

                    XmlElement xmlRphone = xmlDoc.CreateElement("Rphone");
                    xmlClient0.AppendChild(xmlRphone);
                    xmlRphone.InnerText = rdr["Cellular"].ToString().Trim();

                    XmlElement xmlCsbyt = xmlDoc.CreateElement("Csbyt");
                    xmlClient0.AppendChild(xmlCsbyt);
                    xmlCsbyt.InnerText = "0";

                    XmlElement xmlCphone = xmlDoc.CreateElement("Cphone");
                    xmlClient0.AppendChild(xmlCphone);
                    xmlCphone.InnerText = rdr["Cellular"].ToString().Trim();

                    XmlElement xmlEaddr = xmlDoc.CreateElement("Eaddr");
                    xmlClient0.AppendChild(xmlEaddr);
                    xmlEaddr.InnerText = rdr["Email"].ToString().Trim();
                }
                #endregion

                conn.Close();
                conn0.Close(); // cierra las conecciones
                conn1.Close();

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

                conn5.Close();
                conn6.Close();
                conn4.Close();

                if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                {
                    if (!XMLInsert(xmlString, TaskControlID))
                    {
                        try
                        {
                            if (!XMLInsert(xmlString, TaskControlID))
                                throw new Exception("PPS CONNECTION ERROR");
                        }
                        catch (Exception exc)
                        {
                            EPolicy.TaskControl.Policy.DeletePolicyPPSError(TaskControlID != 0 ? TaskControlID : 0);
                            throw new Exception(exc.Message.ToString() + " DELETED CONTROL #:" + TaskControlID);
                        }
                    }
                }
            }
            catch (Exception ecp)
            {			
				LogError(ecp);
                throw new Exception(ecp.Message.ToString());
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

        public bool XMLInsert(string xmlString, int TaskControlID)
        {
            try
            {
                EPolicy.TaskControl.QuoteAuto taskControl = (EPolicy.TaskControl.QuoteAuto)Session["TaskControl"]; 

                //if (taskControl.RenewalNo != "")
                //{
                //    //UpdatePolicyFromPPSByTaskControlID(TaskControlID, taskControl.RenewalNo.ToString().Substring(0, taskControl.RenewalNo.ToString().Trim().IndexOf("-")) , "");
                //    //string RenewalPolicyNumber = "";
                //    //RenewalPolicyNumber = taskControl.RenewalNo.ToString().Substring(0, taskControl.RenewalNo.ToString().Trim().IndexOf("-")).Replace("-", "").Replace("BAP", "").Replace("PAP", "").Trim();
                //    //taskControl.PolicyNo = int.Parse(RenewalPolicyNumber).ToString("0000000");
                //    //return;
                //}

                XmlDocument XmlDoc = new XmlDocument();
                System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
                cn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;

                var AdditionalPremium = txtAdditionalPremium.Text.Trim() == ".00" || txtAdditionalPremium.Text.Trim() == "" ? "0" : txtAdditionalPremium.Text.Trim();
                //cmd.CommandText = "exec sproc_ConsumeXMLePPS @xmlIn, @xmlOut = @x output, @PremiumEndorAmount = " + AdditionalPremium + ", @GapTowing = " + Session["Gap_Towing_String"].ToString();
                cmd.CommandText = "exec sproc_ConsumeXMLePPS_AutoPR @xmlIn, @xmlOut = @x output, @PremiumEndorAmount = " + AdditionalPremium + ", @GapTowing = '" + Session["Gap_Towing_String"].ToString()+"'";
                

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@x", SqlDbType.Xml).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@xmlIN", xmlString);
                cmd.CommandTimeout = 0;

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

                    PolicyNumber = XmlPolicyBase["PolicyID"].InnerText;
                    ClientID = XmlPolicyBase["Client"].InnerText;

					//UpdatePolicyFromPPSByTaskControlID(TaskControlID, PolicyNumber.Replace("PAP", "").Replace("BAP", ""), ClientID);
                    //UpdatePolicyFromPPSByTaskControlID(TaskControlID, txtPolicyNo.Text.Trim(), ClientID);
					 UpdatePolicyFromPPSByTaskControlID(TaskControlID,
                       PolicyNumber.Contains("-") ?
                       (PolicyNumber.ToString().Replace("PAP", "").Replace("BAP", "").Substring(0, PolicyNumber.Replace("PAP", "").Replace("BAP", "").ToString().Trim().IndexOf("-"))).ToString()
                       : PolicyNumber.ToString().Replace("PAP", "").Replace("BAP", ""), ClientID);
					   
					   
                    taskControl.Customer.Description = ClientID;// ClientID de PPS se guarda en Description del Customer

                    PolicyNumber = PolicyNumber.ToString().Replace("PAP", "").Replace("BAP", "");
                    taskControl.Policy.PolicyNo = PolicyNumber.Contains("-") ?
                        int.Parse(PolicyNumber.ToString().Substring(0, PolicyNumber.ToString().Trim().IndexOf("-"))).ToString("") //.ToString("0000000")
                        : int.Parse(PolicyNumber).ToString("");//: int.Parse(PolicyNumber).ToString("0000000");
                    Session["TaskControl"] = taskControl;

                    XmlDoc.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + NAMECONVENTION + "PPS" + ".xml");
                    ///Todas las variables Insured mencionadas existen así para la ocasión en que llame el reclamante
                    ///La información del asegurado no se pierda, si no que siga hacia adelante en el proceso. 
                }
                #endregion
                return true;
            }
            catch (Exception exc)
            {
				LogError(exc);
                return false;
            }
        }

        private double ApplySurcharge(double CoveragePremium)
        {
            //TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto)Session["TaskControl"]; 
    
            //if (taskControl.SurchargePct != 0.0)
            //{
            //    CoveragePremium = Math.Round(CoveragePremium * ((taskControl.SurchargePct / 100.0) + 1), 0);
            //}

            return CoveragePremium;
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

        void UpdateNetTotalPremiumRenewal(int TaskControlID, float RenewalPremium)
        {

            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalPremium",
                SqlDbType.Float, 0, RenewalPremium.ToString(),
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
                exec.GetQuery("UpdateNetTotalPremiumRenewal", xmlDoc);
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
        }

        public void XMLUpdate()
        {
            //// http://stackoverflow.com/questions/16514067/update-xml-stored-in-a-xml-column-in-sql-server
            //XmlDocument xmlDoc = new XmlDocument();

            //xmlDoc.Load(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + NAMECONVENTION + ".xml");

            //XmlElement xmlPolicy = xmlDoc.CreateElement("Policies");
            //xmlDoc.AppendChild(xmlPolicy);

            //XmlElement xmlPolicy1 = xmlDoc.CreateElement("Policy");  // Creacion del elemento
            //xmlPolicy.AppendChild(xmlPolicy1);  // Abajo de donde vas a "append" el elemento

            //XmlElement xmlPolicyID = xmlDoc.CreateElement("PolicyID");
            //xmlPolicy1.AppendChild(xmlPolicyID);
            //xmlPolicyID.InnerText = PolicyNumber;

            //XmlElement xmlClient = xmlDoc.CreateElement("Client");
            //xmlPolicy1.AppendChild(xmlClient);
            //xmlClient.InnerText = ClientID;

            //xmlDoc.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + NAMECONVENTION + ".xml");
        }
        #endregion XML

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
    }
}