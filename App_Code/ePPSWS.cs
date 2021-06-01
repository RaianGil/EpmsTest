using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using System.Net.Mail;
using WebMail = System.Web.Mail;
using System.Net;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Net.Mime;

/// <summary>
/// Summary description for ePPSWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ePPSWS : System.Web.Services.WebService
{
    private string NAMECONVENTION = "";
    private string PolicyNumber = "";
    private string PolicyNo = "";
    private string ClientID = "", InsuredVin = "", InsuredPlate = "", ReinsAsl = "";
    private string CUSTOMER2;

    private int TaskControlID = 0;
    private int PaymentID = 0;
    private double Payment1 = 0.0;
    private double Payment2 = 0.0;
    private double Payment3 = 0.0;
    private double Payment4 = 0.0;
    private double Payment5 = 0.0;
    private double Payment6 = 0.0;
    private string Fecha1 = "";
    private string Fecha2 = "";
    private string Fecha3 = "";
    private string Fecha4 = "";
    private string Fecha5 = "";
    private string Fecha6 = "";
    private string CustomerNumber = "";
    private string ResultMessage = "";
    private string ResultMessagePV = "";
    private string ddlPaymentAmount = "";
    private string txtAccountNombre = "";
    private string ddlMetodoPago = "";
    private string txtAccountNumber = "";
    private string ddlMes = "";
    private string txtSecurityCode = "";
    private string ddlYear = "";
    private string ddlRoutingNumber = "";  
    private string Email = "";
    private string RequestInfo = "";
    private string RequestResponse = "";
    private string PdfFileName = "";
    private string PaymentMethod = "";
    private string CustomerName = "";
    private string PaymentType = "";
    private string AccNumber = "";

    public ePPSWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod (EnableSession = true)]
    public string AddPolicy(string Password, string FirstName, string Initial, string LastName, string LastName2, string BirthDate, string Cellular, string Email, bool HasAccident12, string License, string Address1, string Address2, string ZipCode, string City, string State, bool SameAsMailling, string PhysicalAddress1, string PhysicalAddress2, string PhysicalZipCode, string PhysicalCity, string PhysicalState, double Deductible, double TotalPremium, bool IsCommercial, bool IsPersonal, string VIN, int VehicleMake, int VehicleModel, int VehicleYear, string VehiclePlate, int NumberOfPayments, string CreditCardType, string CreditCardName, string CreditCardNumber, string CreditCardMonth, string CreditCardYear, string CreditCardCode, bool ChkTermsAndConditions)
    {
        try
        {
            string Result = "";

            if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
            {
                //ValidateFields();

                //This are default fields
                string Agent = "000", AgentDesc = "NO AGENT", Agency = "002", InsuranceCompany = "001", Bank = "000", CompanyDealer = "000", MasterPolicyID = "";
                int OriginatedAt = 207, NewUsed = 0;

                //Se eliminaron parametros de los requeridos.
                string HomePhone = "";
                string WorkPhone = "";
                bool HasOtherCoverage = false;
                string OtherCoverageExplain = "";
                string Occupation = "";

                if (Cellular.Trim() == "")
                {
                    Cellular = "(999) 999-9999";
                }


                FillProperties(Password, FirstName, Initial, LastName, LastName2, BirthDate, HomePhone, WorkPhone, Cellular, Email, License, Occupation, Address1, Address2, ZipCode, City, State, SameAsMailling, PhysicalAddress1, PhysicalAddress2, PhysicalZipCode, PhysicalCity, PhysicalState, Deductible, TotalPremium, IsCommercial, IsPersonal, VIN, VehicleMake, VehicleModel, VehicleYear, VehiclePlate, HasOtherCoverage, OtherCoverageExplain, Agent, AgentDesc, Agency, InsuranceCompany, Bank, CompanyDealer, MasterPolicyID, OriginatedAt, NewUsed, NumberOfPayments);

                EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

                try
                {
                    taskControl.Customer.Save(1);
                    SetPaymentAmount();
                    SubmitPayment(CreditCardType, CreditCardName, CreditCardNumber, CreditCardMonth, CreditCardYear, CreditCardCode, ChkTermsAndConditions);

                    if (Session["MsgHeader"] == "Su pago fue procesado exitosamente")
                    {
                        taskControl.SaveGuardianXtra(1);
                        UpdateGuadianXtraHasAccident12(taskControl.TaskControlID, HasAccident12);
                        UpdatePayment();
                        PolicyXML(taskControl.TaskControlID);

                        taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

                        if (taskControl.Customer.Email.ToString() != "")
                        {
                            string EntryDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month).ToString() + " " + (DateTime.Today.Day.ToString().Length == 1 ? "0" + DateTime.Today.Day.ToString() : DateTime.Today.Day.ToString()) + ", " + DateTime.Today.Year;
                            string DebitDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.AddDays(1).Month).ToString() + " " + (DateTime.Today.AddDays(1).Day.ToString().Length == 1 ? "0" + DateTime.Today.AddDays(1).Day.ToString() : DateTime.Today.AddDays(1).Day.ToString()) + ", " + DateTime.Today.AddDays(1).Year;

                            PrintAfterPay();
                            SendEmail(CustomerName, taskControl.Customer.Email.ToString(), PaymentMethod, PaymentType, ddlPaymentAmount.ToString(), AccNumber, EntryDate, taskControl.PolicyType + taskControl.PolicyNo, DebitDate);
                            PdfFileName = "";
                        }

                        Result = "Numero de Poliza: " + PolicyNo +" " +  Session["MsgHeader"].ToString() + " " + Session["MsgDetail"].ToString() + " " + Session["MsgDetail2"].ToString();
                    }
                    else 
                    {
                        DeletePayment();
                        if (Session["MsgHeader"] != null && Session["MsgDetail"] != null && Session["MsgDetail2"] != null)
                            Result = Session["MsgHeader"].ToString() + " " + Session["MsgDetail"].ToString() + " " + Session["MsgDetail2"].ToString();
                    }


                    
                }
                catch (Exception ex)
                {
                    Result = ex.Message + "-" + ex.ToString();
                }

            }
            else
            {
                Result = "Wrong Password";
            }

        
            return Result;

        }
        catch (Exception ex)
        {
            LogError(ex);
            throw new Exception(ex.Message + "-" + ex.ToString());
        }
    }

    [WebMethod(EnableSession = true)]
    public string DecPageByPolicyNo(string Password, string PolicyNo)
    {
        try
        {
            string Result = "";

            if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
            {
                DataTable DtTaskPolicy = new DataTable();
                
                DtTaskPolicy = null;
                string PolicyType = "";
				int policyClass = 0;
                int userID = 0;
                int TaskControlID = 0;
				
                if (PolicyNo.Contains("BAP") || PolicyNo.Contains("PAP"))
                    policyClass = 22;
                
                PolicyType = PolicyNo.ToString().Substring(0,3);
			
				DtTaskPolicy = EPolicy.TaskControl.Policy.GetPolicies(policyClass,PolicyType,
					int.Parse(PolicyNo.Substring(3)).ToString("0000000"),"",	"",
					"","","",userID);

                if(DtTaskPolicy.Rows.Count == 1)
                    TaskControlID = int.Parse(DtTaskPolicy.Rows[0]["TaskControlID"].ToString());
                else
                    return "";

                EPolicy.TaskControl.TaskControl taskControl = EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(TaskControlID, 0);

                Session["TaskControl"] = taskControl;

                if (PolicyNo.Contains("BAP") || PolicyNo.Contains("PAP"))
                {
                    if (taskControl != null)
                    {
                        if (PolicyNo.Contains("BAP"))
                        {
                            Result = PrintPolicyBusinessDeclaration();
                        }
                        else if (PolicyNo.Contains("PAP"))
                        {
                            Result = PrintPolicyPersonalDeclaration();
                        }
                    }
                }
            }
            else
            {
                Result = "Wrong Password";
            }

            return Result;
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw new Exception(ex.Message + "-" + ex.ToString());
        }
    }

    private void FillProperties(string Password, string FirstName, string Initial, string LastName, string LastName2, string BirthDate, string HomePhone, string WorkPhone, string Cellular, string Email, string License, string Occupation, string Address1, string Address2, string ZipCode, string City, string State, bool SameAsMailling, string PhysicalAddress1, string PhysicalAddress2, string PhysicalZipCode, string PhysicalCity, string PhysicalState, double Deductible, double TotalPremium, bool IsCommercial, bool IsPersonal, string VIN, int VehicleMake, int VehicleModel, int VehicleYear, string VehiclePlate, bool HasOtherCoverage, string OtherCoverageExplain, string Agent, string AgentDesc, string Agency, string InsuranceCompany, string Bank, string CompanyDealer, string MasterPolicyID, int OriginatedAt, int NewUsed, int NumberOfPayments)
    {
        EPolicy.TaskControl.GuardianXtra taskControl = new EPolicy.TaskControl.GuardianXtra();//(EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

        //Agent
        //if (ddlAgent.SelectedIndex > 0 && ddlAgent.SelectedItem != null)
        //{
        //    taskControl.Agent = ddlAgent.SelectedItem.Value;
        //    taskControl.AgentDesc = ddlAgent.SelectedItem.Text.Trim();
        //}
        //else
        //{

        taskControl.Agent = Agent;
        taskControl.AgentDesc = AgentDesc;

       // }


        //Agency
        //if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
        //    taskControl.Agency = ddlAgency.SelectedItem.Value;
        //else
        taskControl.Agency = Agency;

        //InsuranceCompany
        //if (ddlInsuranceCompany.SelectedIndex > 0 && ddlInsuranceCompany.SelectedItem != null)
        //    taskControl.InsuranceCompany = ddlInsuranceCompany.SelectedItem.Value;
        //else
        taskControl.InsuranceCompany = InsuranceCompany;

        //Bank
        //if (ddlBank.SelectedIndex > 0 && ddlBank.SelectedItem != null)
        //    taskControl.Bank = ddlBank.SelectedItem.Value;
        //else
        taskControl.Bank = Bank;

        //SupplierID
        taskControl.SupplierID = "000";

        //CompanyDealer
        //if (ddlCompanyDealer.SelectedIndex > 0 && ddlCompanyDealer.SelectedItem != null)
        //    taskControl.CompanyDealer = ddlCompanyDealer.SelectedItem.Value;
        //else
        taskControl.CompanyDealer = CompanyDealer;

        if (taskControl.IsMaster)
        {
            //EPolicy.LookupTables.CompanyDealer dealer = new EPolicy.LookupTables.CompanyDealer();
            //dealer = dealer.GetCompanyDealer(taskControl.CompanyDealer);

            taskControl.MasterPolicyID = MasterPolicyID;
        }

        //Originated At
        //if (ddlOriginatedAt.SelectedIndex > 0 && ddlOriginatedAt.SelectedItem != null)
        //{

        taskControl.OriginatedAt = OriginatedAt;

        //}

        //Make
        //if (ddlVehicleMake.SelectedIndex > 0 && ddlVehicleMake.SelectedItem != null)
        //{
        taskControl.VehicleMakeID = VehicleMake;
        taskControl.XtraVehicleMakeID = VehicleMake;
        //}

        //Model
        //if (ddlVehicleModel.SelectedIndex > 0 && ddlVehicleModel.SelectedItem != null)
        //{
        taskControl.VehicleModelID = VehicleModel;
        taskControl.XtraVehicleModelID = VehicleModel;
        //}

        //year
        //if (ddlVehicleYear.SelectedIndex > 0 && ddlVehicleYear.SelectedItem != null)
        //{
        taskControl.VehicleYearID = VehicleYear;
        taskControl.XtraVehicleYearID = VehicleYear;
        //}

        //NewUse
        //if (ddlNewUsed.SelectedIndex > 0 && ddlNewUsed.SelectedItem != null)
        //{
        taskControl.NewUsed = NewUsed;
        //}

        //Ciudad
        //if (ddlCiudad.SelectedIndex > 0 && ddlCiudad.SelectedItem != null)
        //    taskControl.Customer.City = ddlCiudad.SelectedItem.Text.ToString();
        //else
        taskControl.Customer.City = City;

        //Ciudad Fisica
        //if (ddlPhyCity.SelectedIndex > 0 && ddlPhyCity.SelectedItem != null)
        //    taskControl.Customer.CityPhysical = ddlPhyCity.SelectedItem.Text.ToString();
        //else
        taskControl.Customer.CityPhysical = PhysicalCity;

        //ZipCode
        //if (ddlZip.SelectedIndex > 0 && ddlZip.SelectedItem != null)
        //    taskControl.Customer.ZipCode = ddlZip.SelectedItem.Text;
        //else
        taskControl.Customer.ZipCode = ZipCode;

        //ZipCode Fisico
        //if (ddlPhyZipCode.SelectedIndex > 0 && ddlPhyZipCode.SelectedItem != null)
        //    taskControl.Customer.ZipPhysical = ddlPhyZipCode.SelectedItem.Text;
        //else
        taskControl.Customer.ZipPhysical = PhysicalZipCode;

        if (taskControl.PolicyClassID == 15) // OSO
        {
            if (taskControl.Mode == 2) // EDIT
            {
                if ((taskControl.Customer.FirstName.Trim() != FirstName) || (taskControl.Customer.LastName1.Trim() != LastName) || (taskControl.Customer.LastName2.Trim() != LastName2))
                {
                    int endNum = taskControl.Endoso + 1;
                    taskControl.Endoso = endNum;
                }
            }
        }

        taskControl.TaskControlID = 0;//int.Parse(LblControlNo.Text.Trim());

        //Customer Information
        taskControl.Customer.FirstName = FirstName.ToUpper().Trim();
        taskControl.Customer.Initial = Initial.ToUpper().Trim();
        taskControl.Customer.LastName1 = LastName.ToUpper().Trim();
        taskControl.Customer.LastName2 = LastName2.ToUpper().Trim();
        taskControl.Customer.Address1 = Address1.ToUpper().Trim();
        taskControl.Customer.Address2 = Address2.ToUpper().Trim();
        taskControl.Customer.Birthday = BirthDate.ToUpper().Trim();
        taskControl.Customer.Occupation = Occupation.ToUpper().Trim();
        taskControl.Customer.Licence = License.ToUpper().Trim();
        taskControl.Customer.Email = Email.ToUpper().Trim();

        taskControl.Customer.State = State.ToUpper().Trim();

        taskControl.Customer.HomePhone = HomePhone.Trim();
        taskControl.Customer.JobPhone = WorkPhone.Trim();
        taskControl.Customer.Cellular = Cellular.Trim();

        taskControl.Customer.AddressPhysical1 = PhysicalAddress1.ToString().Trim();
        taskControl.Customer.AddressPhysical2 = PhysicalAddress2.ToString().Trim();
        taskControl.Customer.StatePhysical = PhysicalState.ToString().Trim();


        if (taskControl.PolicyClassID == 1 || taskControl.PolicyClassID == 16) // VSC, QCertified
        {
            taskControl.PolicyNo = "";//TxtPolicyNo.Text.Trim().ToUpper().Replace(" ", "");
            taskControl.PolicyNo = "";//taskControl.PolicyNo.Trim().ToUpper().Replace("-", "");
        }
        else
        {
            taskControl.PolicyNo = "";//TxtPolicyNo.Text.Trim().ToUpper().Replace(" ", "");
        }

        if (IsCommercial == true)
        {
            taskControl.PolicyType = "XCA";//TxtPolicyType.Text.Trim().ToUpper();
        }
        else
        {
            taskControl.PolicyType = "XPA";
        }


        taskControl.Suffix = "00";//TxtSufijo.Text.Trim();
        taskControl.Term = 12;//int.Parse(TxtTerm.Text.Trim());
        taskControl.EffectiveDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(DateTime.Now.ToShortDateString()));

        if (taskControl.ExpirationDate.Trim() == string.Empty) // && this.TxtTerm.Text.Trim() != string.Empty)
        {
            //if (this.TxtTerm.Text.Trim() == string.Empty)
            //{
            //    this.TxtTerm.Text = "0";
            //}
            taskControl.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(DateTime.Parse(taskControl.EffectiveDate).AddMonths(taskControl.Term).ToShortDateString()));
            //this.txtExpDt.Text = taskControl.ExpirationDate;
        }
        else
        {
            //if (this.txtExpDt.Text.Trim() != string.Empty)
                taskControl.ExpirationDate = taskControl.ExpirationDate;
        }

        taskControl.EntryDate = DateTime.Now;

        taskControl.TotalPremium = Deductible;//double.Parse(ddlDeducible.SelectedItem.Text.Replace("$", "").Trim());



        taskControl.VIN = VIN;
        taskControl.XtraVIN = VIN;
        taskControl.XtraHasCoverageExplain = OtherCoverageExplain;

        taskControl.Customer.SamesAsMail = SameAsMailling;

        taskControl.XtraHasCoverage = HasOtherCoverage;//rdbCoverageYes.Checked;

        switch (NumberOfPayments)
        {
            case 1:
                taskControl.XtraDefferedPayment = false;
                taskControl.XtraIsFourPayment = false;//chkDefpayfour.Checked;
                taskControl.XtraIsSixPayment = false;//chkDefpaysix.Checked;
                taskControl.XtraIsCreditPayment = true;//chkCredit.Checked;
                taskControl.XtraIsDebitPayment = false;//chkDebit.Checked;
                taskControl.XtraIsCashPayment = false;//chkCash.Checked;
                break;

            case 4:
                taskControl.XtraDefferedPayment = true;
                taskControl.XtraIsFourPayment = true;//chkDefpayfour.Checked;
                taskControl.XtraIsSixPayment = false;//chkDefpaysix.Checked;
                taskControl.XtraIsCreditPayment = true;//chkCredit.Checked;
                taskControl.XtraIsDebitPayment = false;//chkDebit.Checked;
                taskControl.XtraIsCashPayment = false;//chkCash.Checked;
                break;

            case 6:
                taskControl.XtraDefferedPayment = true;
                taskControl.XtraIsFourPayment = false;//chkDefpayfour.Checked;
                taskControl.XtraIsSixPayment = true;//chkDefpaysix.Checked;
                taskControl.XtraIsCreditPayment = true;//chkCredit.Checked;
                taskControl.XtraIsDebitPayment = false;//chkDebit.Checked;
                taskControl.XtraIsCashPayment = false;//chkCash.Checked;
                break;

            default:
                taskControl.XtraDefferedPayment = false;
                taskControl.XtraIsFourPayment = false;//chkDefpayfour.Checked;
                taskControl.XtraIsSixPayment = false;//chkDefpaysix.Checked;
                taskControl.XtraIsCreditPayment = true;//chkCredit.Checked;
                taskControl.XtraIsDebitPayment = false;//chkDebit.Checked;
                taskControl.XtraIsCashPayment = false;//chkCash.Checked;
                break;
        }

        if (IsCommercial == true)
        {
            taskControl.XtraIsCommercialAuto = true;//chkIsCommercialAuto.Checked;
            taskControl.XtraIsPersonalAuto = false;//chkIsPersonalAuto.Checked;
        }
        else
        {
            taskControl.XtraIsCommercialAuto = false;//chkIsCommercialAuto.Checked;
            taskControl.XtraIsPersonalAuto = true;//chkIsPersonalAuto.Checked;
        }

        //NESECITAS ESTO JNF
        //taskControl.XtraDefferedPayment = rdbDefpaymentYes.Checked;
        //taskControl.XtraIsFourPayment = chkDefpayfour.Checked;
        //taskControl.XtraIsSixPayment = chkDefpaysix.Checked;
        //taskControl.XtraIsCreditPayment = true;//chkCredit.Checked;
        //taskControl.XtraIsCommercialAuto = chkIsCommercialAuto.Checked;
        //taskControl.XtraIsPersonalAuto = chkIsPersonalAuto.Checked;
        //taskControl.XtraIsDebitPayment = chkDebit.Checked;
        //taskControl.XtraIsCashPayment = chkCash.Checked;

        taskControl.Plate = VehiclePlate;//txtVehiclePlate.Text.ToUpper().Trim();
        taskControl.XtraPlate = VehiclePlate;

        #region XtraAuto

        taskControl.XtraVIN = VIN;//txtVehicleVIN.Text.ToUpper().Trim();

        if (VehicleMake != 0)
        {
            taskControl.XtraVehicleMakeID = VehicleMake;
        }
        else
        {
            taskControl.XtraVehicleMakeID = 0;
        }

        DataTable dtVehicleMake = GetVehicleMakeByMakeID(Password, VehicleMake);

        if (dtVehicleMake.Columns.Count > 0)
        {
            taskControl.XtraVehicleMake = dtVehicleMake.Rows[0]["VehicleMakeDesc"].ToString();
        }
        else
        {
            taskControl.XtraVehicleMake = "";
        }


        DataTable dtVehicleModel = GetVehicleModelByVehicleModelID(Password, VehicleModel);

        if (dtVehicleModel.Columns.Count > 0)
        {
            taskControl.XtraVehicleModel = dtVehicleModel.Rows[0]["VehicleModelDesc"].ToString();
        }
        else
        {
            taskControl.XtraVehicleModel = "";
        }

        DataTable dtVehicleYear = GetVehicleYearIDByVehicleYearDesc(Password, VehicleYear.ToString());

        if (dtVehicleYear.Columns.Count > 0)
        {
            taskControl.XtraVehicleYearID = int.Parse(dtVehicleYear.Rows[0]["VehicleYearID"].ToString());
        }
        else
        {
            taskControl.XtraVehicleYearID = 0;
        }


        //ddlVehicleMake.SelectedItem.Text.Trim();
        taskControl.XtraVehicleModelID = VehicleModel.ToString() == "" ? 0 : VehicleModel;

        //taskControl.XtraVehicleModel = ddlVehicleModel.SelectedItem.Text.Trim();
        //taskControl.XtraVehicleYearID = ddlVehicleYear.SelectedItem.Value == "" ? 0 : int.Parse(ddlVehicleYear.SelectedItem.Value);
        taskControl.XtraVehicleYear = VehicleYear.ToString();
        taskControl.XtraPremium = TotalPremium;//double.Parse(ddlDeducible.Text.Replace("$", "").Trim());

        #endregion

        //if (ChkAutoAssignPolicy.Checked)
        //taskControl.AutoAssignPolicy = true;
        //else
        taskControl.AutoAssignPolicy = false;

        taskControl.Customer.Mode = 1;
        taskControl.Mode = 1;

        if (taskControl.Mode == 1)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            taskControl.EnteredBy = "ePPSWebService";//cp.Identity.Name.Split("|".ToCharArray())[0];
        }

        Session["TaskControl"] = taskControl;
    }

    public void PolicyXML(int TaskControlID)
    {
        try
        {
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
                xmlComRate.InnerText = "0.0000000e+000";

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
                xmlMaddr1.InnerText = rdr["Adds1"].ToString().Trim();

                XmlElement xmlMaddr2 = xmlDoc.CreateElement("Maddr2");
                xmlClient0.AppendChild(xmlMaddr2);
                xmlMaddr2.InnerText = rdr["Adds2"].ToString().Trim();

                XmlElement xmlMaddr3 = xmlDoc.CreateElement("Maddr3");
                xmlClient0.AppendChild(xmlMaddr3);
                xmlMaddr3.InnerText = "0";

                XmlElement xmlMcity = xmlDoc.CreateElement("Mcity");
                xmlClient0.AppendChild(xmlMcity);
                xmlMcity.InnerText = rdr["City"].ToString().Trim();

                XmlElement xmlMstate = xmlDoc.CreateElement("Mstate");
                xmlClient0.AppendChild(xmlMstate);
                xmlMstate.InnerText = rdr["State"].ToString().Trim();

                XmlElement xmlMnation = xmlDoc.CreateElement("Mnation");
                xmlClient0.AppendChild(xmlMnation);
                xmlMnation.InnerText = "0";

                XmlElement xmlMzip = xmlDoc.CreateElement("Mzip");
                xmlClient0.AppendChild(xmlMzip);
                xmlMzip.InnerText = rdr["Zip"].ToString().Trim();



                XmlElement xmlRaddr1 = xmlDoc.CreateElement("Raddr1");
                xmlClient0.AppendChild(xmlRaddr1);
                xmlRaddr1.InnerText = rdr["RAddrs1"].ToString().Trim();
                //xmlRaddr1.InnerText = "8744 LINBERG BAY";

                XmlElement xmlRaddr2 = xmlDoc.CreateElement("Raddr2");
                xmlClient0.AppendChild(xmlRaddr2);
                xmlRaddr2.InnerText = rdr["RAddrs2"].ToString().Trim();


                XmlElement xmlRaddr3 = xmlDoc.CreateElement("Raddr3");
                xmlClient0.AppendChild(xmlRaddr3);
                xmlRaddr3.InnerText = "0";

                XmlElement xmlRcity = xmlDoc.CreateElement("Rcity");
                xmlClient0.AppendChild(xmlRcity);
                xmlRcity.InnerText = rdr["RCity"].ToString().Trim();
                //xmlRcity.InnerText = "ST  THOMAS";

                XmlElement xmlRstate = xmlDoc.CreateElement("Rstate");
                xmlClient0.AppendChild(xmlRstate);
                xmlRstate.InnerText = rdr["RState"].ToString().Trim();

                //xmlRstate.InnerText = "VI";

                XmlElement xmlRnation = xmlDoc.CreateElement("Rnation");
                xmlClient0.AppendChild(xmlRnation);
                xmlRnation.InnerText = "0";

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
            EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
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
                PolicyNo = XmlPolicyBase["PolicyID"].InnerText;
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
                                    //InsuredVin = ChildsElement["Vin"].InnerText;
                                   // InsuredPlate = ChildsElement["LicPlate"].InnerText;

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
                                                    //ddlVehicleMake.SelectedIndex = ddlVehicleMake.Items.IndexOf(ddlVehicleMake.Items.FindByText((GrandChildsElements["Make"].InnerText.Trim()).ToString()));

                                                   // if (int.Parse(ddlVehicleMake.SelectedItem.Value) > 0)
                                                        //FillModelDDListByPPS(ddlVehicleMake.SelectedItem.Value.ToString());

                                                     //   ddlVehicleModel.SelectedIndex = ddlVehicleModel.Items.IndexOf(ddlVehicleModel.Items.FindByText((GrandChildsElements["Model"].InnerText.TrimEnd()).ToString()));
                                                    //ddlVehicleYear.SelectedIndex = ddlVehicleYear.Items.IndexOf(ddlVehicleYear.Items.FindByText((GrandChildsElements["MYear"].InnerText.TrimEnd()).ToString())); //GrandChildsElements["MYear"].InnerText;
                                                                                                                                                                                                                 ///ddlBroker.Items.IndexOf(ddlBroker.Items.FindByValue(int.Parse(XmlPolicyBase["BrokerID"].InnerText).ToString()));                                                                
                                                }
                                            }
                                            else if (GrandChilds.Name == "VehicleCvrgTable")
                                            {
                                                foreach (XmlElement GrandChildsElements in GrandChilds)
                                                {
                                                    if (!(FilledAsl))  //Solo leerá los campos una vez, tomando los primeros que lea
                                                    {
                                                       // ReinsAsl = GrandChildsElements["ReinsAsl"].InnerText.ToString();

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
            //LogError(exc);
            throw new Exception(exc.Message.ToString());
            //CUSTOMER2 = CUSTOMER2.ToString() + " HELLO "+ exc.Message + " " + exc;
            //EventLog.WriteEntry("XMLINSERT", exc.InnerException.ToString() + " " + exc.Message.ToString());

            //lblRecHeader.Text = exc.Message + " " + exc;
            //mpeSeleccion.Show();
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

        //return dt;

    }

    [WebMethod]
    public string GetVehicleYear(string Password)
    {
        string Result = "";
        int YearFilter = 20;
            if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
            {
                Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
                DataTable dt = new DataTable();

                DbRequestXmlCookRequestItem[] cookItems =
                       new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("YearFilter",
                    SqlDbType.Int, 0, YearFilter.ToString(),
                    ref cookItems);

                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                    dt = Executor.GetQuery("GetVehicleYearByYearFilter", xmlDoc);

                    dt.TableName = "VehicleYear";

                    StringWriter sw = new StringWriter();
                    dt.WriteXml(sw);
                    Result = sw.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
            }
            else
            {
                Result = "Wrong Password.";
            }

        return Result;
    }

    [WebMethod]
    public string GetVehicleMake(string Password)
    {
        string Result = "";
        if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
        {
            DataTable dt = EPolicy.LookupTables.LookupTables.GetTable("VehicleMake");
            dt.TableName = "VehicleMake";

            StringWriter sw = new StringWriter();
            dt.WriteXml(sw);
            Result = sw.ToString();
        }
        else
        {
            Result = "Wrong Password.";
        }

        return Result;
    }

    [WebMethod]
    public string GetClaimStatusByClaimPlate(string Password, string Plate)
    {
        string Result = "";
        if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
        {
            string connection = @"Data Source=GICPR-SERVER2\GuardianDB;Initial Catalog=ClaimNext;User ID=sa;password=SQL2008123$;";
            DataTable dt;
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            using (SqlCommand cmd = new SqlCommand("GetClaimStatusByClaimPlateOrClaimNumber", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ClaimNumber_Plate", SqlDbType.VarChar).Value = Plate;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Result = dt.Rows[0]["ClaimSubstatusID"].ToString();
                    switch (Result)
                    {
                        //La reclamación se encuantra en estatus inicial. 
                        case "1":
                            Result = "En proceso de validar tu cubierta. Un representante de servicio se comunicará contigo próximamente.";//"COORDINATOR";
                                break;
                        //La reclamación se encuantra en estatus de tasador. 
                        case "2":
                            Result = "Estamos evaluando los daños a tú vehículo. Un ajustador te estará llamando próximamente.";//"APPRAISER / VEHICLE INSPECTION";
                            break;
                        //La reclamación se encuantra en estatus de ajustador. 
                        case "3":
                            Result = "Estamos analizando tú reclamación. Un ajustador se comunicará contigo para explicarte los resultados.";//"ADJUSTER";
                            break;
                        //La reclamación se encuantra en estatus de autorización de pago.
                        case "4":
                            Result = "En proceso de autorizar tú pago.";//"AUTHORIZATION";
                            break;
                        //La reclamación se encuantra en estatus de emisión de cheque. 
                        case "5":
                            Result = "Ya estamos en proceso de emitir tu pago. Llama al 787-520-6175 si deseas recoger el pago en nuestra oficina o si deseas que te lo enviemos por correo.";//"ISSUE PAY";
                            break;
                        //La reclamación ha sido pagada.
                        case "7":
                            Result = "Tú pago fue procesado.";//"CLAIM PAID";
                            break;
                        //La reclamación ha sido cerrada sin pago. 
                        case "8":
                            Result = "Tú reclamación fue cerrada. Para más información favor comunicarse al 787-520-6175.";//"CLOSED WITHOUT PAYMENT";
                            break;
                        //La reclamación ha sido cerrada.
                        case "9":
                            Result = "Tú reclamación fue cerrada. Para más información favor comunicarse al 787-520-6175.";//"CLOSED NOTICE";
                            break;
                    }
                }
                else
                {
                    Result = "Tú tablilla no fue encontrada.";
                }

                con.Close();

            }
        }
        else
        {
            Result = "Wrong Password.";
        }

        return Result;
    }

    private static DataTable GetVehicleMakeByMakeID(string Password, int VehicleMakeID)
    {
        DataTable Result = null;
        if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
        {

            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            DataTable dt = new DataTable();

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

            dt = Executor.GetQuery("GetVehicleMakeByMakeID", xmlDoc);

            dt.TableName = "VehicleMake";

            Result = dt;
            //StringWriter sw = new StringWriter();
            //dt.WriteXml(sw);
            //Result = sw.ToString();
        }
        else
        {
            //Result = "Wrong Password.";
        }

        return Result;
    }

    private static DataTable GetVehicleYearIDByVehicleYearDesc(string Password, string VehicleYear)
    {
        DataTable Result = null;
        if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
        {

            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            DataTable dt = new DataTable();

            DbRequestXmlCookRequestItem[] cookItems =
                   new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("VehicleYearDesc",
                SqlDbType.VarChar, 20, VehicleYear.ToString(),
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

            dt = Executor.GetQuery("GetVehicleYearIDByVehicleYearDesc", xmlDoc);

            dt.TableName = "VehicleYear";

            Result = dt;
            //StringWriter sw = new StringWriter();
            //dt.WriteXml(sw);
            //Result = sw.ToString();
        }
        else
        {
            //Result = "Wrong Password.";
        }

        return Result;
    }

    [WebMethod]
    public string GetVehicleModel(string Password, string VehicleMakeID)
    {
        string Result = "";
        if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            DataTable dt = new DataTable();

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

            dt = Executor.GetQuery("GetVehicleModelByVehicleMakeID", xmlDoc);

            dt.TableName = "VehicleModel";


            StringWriter sw = new StringWriter();
            dt.WriteXml(sw);
            Result = sw.ToString();
        }
        else
        {
            Result = "Wrong Password.";
        }

        return Result;
    }

    private static DataTable GetVehicleModelByVehicleModelID(string Password, int VehicleModelID)
    {
        DataTable Result = null;
        if (Password == "3D9EB8E3-BE03-4B4C-9DD1-D77C02155896")
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            DataTable dt = new DataTable();

            DbRequestXmlCookRequestItem[] cookItems =
                   new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("VehicleModelID",
                SqlDbType.Int, 0, VehicleModelID.ToString(),
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

            dt = Executor.GetQuery("GetVehicleModelByVehicleModelID", xmlDoc);

            dt.TableName = "VehicleModel";

            Result = dt;
            //StringWriter sw = new StringWriter();
            //dt.WriteXml(sw);
            //Result = sw.ToString();
        }
        else
        {
           // Result = "Wrong Password.";
        }

        return Result;
    }

    public void SubmitPayment(string CreditCardType, string CreditCardName, string CreditCardNumber, string CreditCardMonth, string CreditCardYear, string CreditCardCode, bool ChkTermsAndConditions)
    {
        try
        {


            if (!ValidFields())
            {
                if (!ChkTermsAndConditions)
                {
                    throw new Exception("Favor acceptar los terminos y servicios para completar la transacción.");
                }
                else
                {
                    if (ProcesarPago(CreditCardType,CreditCardName,CreditCardNumber,CreditCardMonth,CreditCardYear,CreditCardCode))
                    {
                        Session.Add("MsgHeader", "Su pago fue procesado exitosamente");
                        Session.Add("MsgDetail", "Su número de referencia es " + ResultMessage.Trim());

                        Session.Add("MsgDetail2", ResultMessagePV.Trim());

                        
                    }
                    else
                    {
                        Session.Add("MsgHeader", "Su pago no pudo ser procesado");
                        Session.Add("MsgDetail", "Error: " + ResultMessage.Trim());
                        Session.Add("MsgDetail2", ResultMessagePV.Trim());
                    }

                    //Response.Redirect("Message.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

        }
        catch (Exception exp)
        {
            LogError(exp);
        }
    }

    private bool ProcesarPago(string CreditCardType, string CreditCardName, string CreditCardNumber, string CreditCardMonth, string CreditCardYear, string CreditCardCode)
    {
        //Visa 
        //4485055377466669
        //4556775235363865
        //4716804944927443
        //4556874611555796
        //4556107555806127

        //Master Card
        //5435606987460936
        //5341399955705614
        //5416329763111127
        //5309242750164407
        //5385945943252171

        //AMEX
        //346424054417717
        //344791662301372
        //340889953628836
        //377508409211437
        //345281936379189

        //http://www.getcreditcardnumbers.com/  

        EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
        PaymentMethod = "";
        CustomerName = "";
        PaymentType = "";

        if (CreditCardType.ToString().ToUpper().Trim().Contains("VISA"))
        {
            ddlMetodoPago = "3";
        }
        else
        {
            ddlMetodoPago = "4";//Master Card
        }

        txtAccountNombre = CreditCardName;
        txtAccountNumber = CreditCardNumber;
        ddlMes = CreditCardMonth;
        txtSecurityCode = CreditCardCode;
        ddlYear = CreditCardYear;
        ddlRoutingNumber = "";  
        TaskControlID = taskControl.TaskControlID;

        //if (taskControl.XtraIsDebitPayment == true)
        //{
        //    PaymentMethod = "ACH debit transaction to your checking/savings account";
        //    PaymentType = "account";

        //}
        //else
        //{
            PaymentMethod = "credit card debit transaction";
            PaymentType = "tarjeta de crédito";
        //}

        if (taskControl.Customer.FirstName.ToString() != "")
        {
            CustomerName = taskControl.Customer.FirstName.ToString() + " " + taskControl.Customer.LastName1.ToString() + " " + taskControl.Customer.LastName2.ToString();
        }
        else if (taskControl.Customer.Initial.ToString() != "")
        {
            CustomerName = taskControl.Customer.FirstName.ToString() + " " + taskControl.Customer.Initial.ToString() + " " + taskControl.Customer.LastName1.ToString() + " " + taskControl.Customer.LastName2.ToString();
        }



        bool Result = false;

        //Payment Transaction
        string transactionNumber = GetTransactionNumber(); //Debe ser un consecutivo y Unico
        string Reqdata = "";
        //string xmlResult = PaymentTransaction("LS91A2QS-7522-4DWD-4219-A515469D569P", Reqdata, 1, transactionNumber);
        //AddPayment
        //string[] splitData = xmlResult.Split('|');
        //txtAccountNombre.ToString() = xmlResult.ToString();
        //AddPayment(transactionNumber, splitData[1].Trim(), splitData[0].Trim());

        //string[] splitData;

        //Create Customer and InstallPayment
        //xmlResult = "";



        string xmlResult = PaymentVault("LS91A2QS-7522-4DWD-4219-A515469D569P", Reqdata, 1, transactionNumber);
        string[] splitData = xmlResult.Split('|');

        if (xmlResult.Contains("Success"))
        {
            //AddPayment

            txtAccountNumber = xmlResult.ToString();
            AddPayment(transactionNumber, splitData[1].Trim(), splitData[0].Trim(), xmlResult.ToString(), RequestInfo, RequestResponse);
            RequestInfo = "";
            RequestResponse = "";
        }
        else
        {
            //txtAccountNombre.Text = xmlResult.ToString();
            if (splitData[1] != null)
            {
                AddPayment(transactionNumber, "", "", splitData[0].Trim() + " - " + splitData[1].Trim(), RequestInfo, RequestResponse);
                RequestInfo = "";
                RequestResponse = "";
            }
            else
            {
                AddPayment(transactionNumber, "", "", xmlResult.ToString(), RequestInfo, RequestResponse);
                RequestInfo = "";
                RequestResponse = "";
            }
            ResultMessagePV = "Could not set installment payments, Please verify in Dynamic Payment Console";
        }


        if (xmlResult.Contains("Success"))
        {
            ResultMessage = splitData[1].Trim();
            Result = true;

            AccNumber = txtAccountNumber.ToString().Trim().Substring(txtAccountNumber.ToString().Trim().Length - 4);
        }
        else
        {
            ResultMessage = splitData[0].Trim() + " - " + splitData[1].Trim();
            Result = false;
        }

        return Result;
    }

    private void PrintAfterPay()
    {
        try
        {

            EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
            List<string> mergePaths = new List<string>();
            string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

            mergePaths.Add(PrintPreview("SolicitudGuardianXtra.rdlc"));
            if (taskControl.XtraIsSixPayment)
            {
                mergePaths.Add(PrintPreview("Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc"));
                mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas6Plazos.rdlc"));
            }

            if (taskControl.XtraIsFourPayment)
            {
                mergePaths.Add(PrintPreview("SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc"));
                mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas4Plazos.rdlc"));
            }

            if (taskControl.XtraIsCreditPayment)
            {
                mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc"));
            }

            if (taskControl.XtraIsDebitPayment)
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
            PdfFileName = finalFile;
            taskControl.PrintPolicy = true;

        }
        catch (Exception exc)
        {
            //lblRecHeader.Text = exc.Message.Trim() + " - " + exc.InnerException.ToString();
            //mpeSeleccion.Show();
        }
    }

    private string PrintPreview(string rdlcDoc)
    {
        try
        {
            EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
            ReportViewer viewer = new ReportViewer();
            string ProcessPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

            viewer.LocalReport.DataSources.Clear();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("Reports/GuardianXtra/" + rdlcDoc);
            Microsoft.Reporting.WebForms.ReportDataSource rds = null;

            if (rdlcDoc == "SolicitudGuardianXtra.rdlc")
            {

                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                ReportParameter[] parameters = new ReportParameter[26];

                parameters[0] = new ReportParameter("Prefix", taskControl.PolicyType.ToString().Trim());
                parameters[1] = new ReportParameter("Term", taskControl.Term.ToString().Trim());
                parameters[2] = new ReportParameter("PolicyNo", taskControl.PolicyNo.ToString().Trim());
                parameters[3] = new ReportParameter("EffDate", taskControl.EffectiveDate.ToString().Trim());
                parameters[4] = new ReportParameter("ExpDate", taskControl.ExpirationDate.ToString().Trim());
                parameters[5] = new ReportParameter("VehicleMake", taskControl.XtraVehicleMake.ToString().Trim());
                parameters[6] = new ReportParameter("VehicleModel", taskControl.XtraVehicleModel.ToString().Trim());
                parameters[7] = new ReportParameter("VehicleYear", taskControl.XtraVehicleYear.ToString().Trim());
                parameters[8] = new ReportParameter("VehicleVIN", taskControl.XtraVIN.ToString().Trim());
                parameters[9] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());
                parameters[10] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                parameters[11] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.ToString().Trim());
                parameters[12] = new ReportParameter("CustomerInitial", taskControl.Customer.Initial.ToString().Trim());
                parameters[13] = new ReportParameter("CustomerLastName1", taskControl.Customer.LastName1.ToString().Trim());
                parameters[14] = new ReportParameter("CustomerLastName2", taskControl.Customer.LastName2.ToString().Trim());
                parameters[15] = new ReportParameter("CustomerAddrs1", taskControl.Customer.Address1.ToString().Trim());
                parameters[16] = new ReportParameter("CustomerAddrs2", taskControl.Customer.Address2.ToString().Trim());
                parameters[17] = new ReportParameter("CustomerCity", taskControl.Customer.City.ToString().Trim());
                parameters[18] = new ReportParameter("CustomerState", taskControl.Customer.State.ToString().Trim());
                parameters[19] = new ReportParameter("CustomerZip", taskControl.Customer.ZipCode.ToString().Trim());
                parameters[20] = new ReportParameter("Agency", taskControl.Agency.ToString().Trim());
                parameters[21] = new ReportParameter("Agent", taskControl.AgentDesc.ToString().Trim());
                parameters[22] = new ReportParameter("AgentNo", taskControl.AgentCode.ToString().Trim());
                parameters[23] = new ReportParameter("Premium", taskControl.TotalPremium.ToString().Trim());
                parameters[24] = new ReportParameter("Deducible", taskControl.XtraPremium.ToString().Trim());
                parameters[25] = new ReportParameter("PhysicalAddrs", taskControl.Customer.AddressPhysical1.ToString().Trim() + ", " + taskControl.Customer.AddressPhysical2.ToString().Trim() + " " + taskControl.Customer.CityPhysical.ToString().Trim() + ", " + taskControl.Customer.StatePhysical.ToString().Trim() + " " + taskControl.Customer.ZipPhysical.ToString().Trim());

                // viewer.LocalReport.ReportPath = Server.MapPath("Reports/GuardianXtra/SolicitudGuardianXtra.rdlc");
                viewer.LocalReport.SetParameters(parameters);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();

            }

            if (rdlcDoc == "Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc")
            {

                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                ReportParameter[] param = new ReportParameter[3];

                param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                param[2] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());


                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();

            }

            if (rdlcDoc == "PlandePagoDiferidodePrimas6Plazos.rdlc")
            {


                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                string Fecha = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();

                ReportParameter[] param = new ReportParameter[8];

                param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                param[2] = new ReportParameter("Sufix", taskControl.Suffix.Trim());
                param[3] = new ReportParameter("Date1", Fecha);
                param[4] = new ReportParameter("Date2", DateTime.Parse(Fecha).AddMonths(1).ToShortDateString());
                param[5] = new ReportParameter("Date3", DateTime.Parse(Fecha).AddMonths(2).ToShortDateString());
                param[6] = new ReportParameter("Date4", DateTime.Parse(Fecha).AddMonths(3).ToShortDateString());
                param[7] = new ReportParameter("Date5", DateTime.Parse(Fecha).AddMonths(4).ToShortDateString());

                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();

            }

            if (rdlcDoc == "SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc")
            {


                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


                ReportParameter[] param = new ReportParameter[3];

                param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                param[2] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());


                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();

            }

            if (rdlcDoc == "PlandePagoDiferidodePrimas4Plazos.rdlc")
            {


                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                string Fecha = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();

                ReportParameter[] param = new ReportParameter[6];

                param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                param[2] = new ReportParameter("Sufix", taskControl.Suffix.Trim());
                param[3] = new ReportParameter("Date1", Fecha);
                param[4] = new ReportParameter("Date2", DateTime.Parse(Fecha).AddMonths(1).ToShortDateString());
                param[5] = new ReportParameter("Date3", DateTime.Parse(Fecha).AddMonths(2).ToShortDateString());

                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();

            }

            if (rdlcDoc == "HojadeDeclaraciones_XTRA.rdlc")
            {

                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                ReportParameter[] parameters = new ReportParameter[25];

                parameters[0] = new ReportParameter("Prefix", taskControl.PolicyType.ToString().Trim());
                parameters[1] = new ReportParameter("Term", taskControl.Term.ToString().Trim());
                parameters[2] = new ReportParameter("PolicyNo", taskControl.PolicyNo.ToString().Trim());
                parameters[3] = new ReportParameter("EffDate", taskControl.EffectiveDate.ToString().Trim());
                parameters[4] = new ReportParameter("ExpDate", taskControl.ExpirationDate.ToString().Trim());
                parameters[5] = new ReportParameter("VehicleMake", taskControl.XtraVehicleMake.ToString().Trim());
                parameters[6] = new ReportParameter("VehicleModel", taskControl.XtraVehicleModel.ToString().Trim());
                parameters[7] = new ReportParameter("VehicleYear", taskControl.XtraVehicleYear.ToString().Trim());
                parameters[8] = new ReportParameter("VehicleVIN", taskControl.XtraVIN.ToString().Trim());
                parameters[9] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());
                parameters[10] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                parameters[11] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.ToString().Trim());
                parameters[12] = new ReportParameter("CustomerInitial", taskControl.Customer.Initial.ToString().Trim());
                parameters[13] = new ReportParameter("CustomerLastName1", taskControl.Customer.LastName1.ToString().Trim());
                parameters[14] = new ReportParameter("CustomerLastName2", taskControl.Customer.LastName2.ToString().Trim());
                parameters[15] = new ReportParameter("CustomerAddrs1", taskControl.Customer.Address1.ToString().Trim());
                parameters[16] = new ReportParameter("CustomerAddrs2", taskControl.Customer.Address2.ToString().Trim());
                parameters[17] = new ReportParameter("CustomerCity", taskControl.Customer.City.ToString().Trim());
                parameters[18] = new ReportParameter("CustomerState", taskControl.Customer.State.ToString().Trim());
                parameters[19] = new ReportParameter("CustomerZip", taskControl.Customer.ZipCode.ToString().Trim());
                parameters[20] = new ReportParameter("Agency", taskControl.Agency.ToString().Trim());
                parameters[21] = new ReportParameter("Agent", taskControl.AgentDesc.ToString().Trim());
                parameters[22] = new ReportParameter("AgentNo", taskControl.AgentCode.ToString().Trim());
                parameters[23] = new ReportParameter("Premium", taskControl.TotalPremium.ToString().Trim());
                parameters[24] = new ReportParameter("Deducible", taskControl.XtraPremium.ToString().Trim());

                viewer.LocalReport.SetParameters(parameters);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
            }

            if (rdlcDoc == "ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc")
            {

                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                ReportParameter[] param = new ReportParameter[1];

                param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
            }

            if (rdlcDoc == "PolizaAutoPersonalPR3Pagina1.rdlc")
            {


                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                ReportParameter[] param = new ReportParameter[1];

                param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
            }

            if (rdlcDoc == "MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc")
            {

                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


                ReportParameter[] param = new ReportParameter[3];

                param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                param[2] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
            }

            if (rdlcDoc == "MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc")
            {

                GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                ds.Fill(dt, taskControl.TaskControlID);
                rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


                ReportParameter[] param = new ReportParameter[3];

                param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                param[2] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                viewer.LocalReport.SetParameters(param);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
            }

            if (rdlcDoc == "MidOceanInvoiceES.rdlc")
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

    private void DeleteFile(string pathAndFileName)
    {
        if (File.Exists(pathAndFileName))
            File.Delete(pathAndFileName);
    }

    private List<string> WriteRdlcToPDF(ReportViewer viewer, EPolicy.TaskControl.GuardianXtra taskControl, List<string> mergePaths, int index)
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

    public string PaymentVault(string Password, string Transaction, int UserID, string transactionNumber)
    {
        string[] fields = new string[76];
        string[] retVal = new string[5];
        string status = "0";
        //string transactionNumber = ConfigurationManager.AppSettings["Terminal"].Trim().Trim() + GetTransactionNumber(); // "100008"; //Debe ser un consecutivo
        string returnMessage = "";
        string decryptTransValue = "";
        //EncryptClass.EncryptClass encryptClass = new EncryptClass.EncryptClass();
        //decryptTransValue = encryptClass.Encrypt(Transaction);

        //decryptTransValue = encryptClass.Decrypt(Transaction.Trim());
        string[] RequestTransaction = Transaction.Split('|'); // decryptTransValue.Split('|');

        EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

        if (Session["TaskControl"] != null)
        {
            fields[0] = ddlPaymentAmount.Trim(); //"25.00";// "0.01";// txtPhone.Text.Trim(); //RequestTransaction[0]; //"15.00";
            fields[1] = "";//taskControl.Customer.Address1.Trim(); //dt.Rows[0]["BusinessAddress1"].ToString().Trim();
            fields[2] = "";//taskControl.Customer.Address2.Trim(); //dt.Rows[0]["BusinessAddress2"].ToString().Trim();
            fields[3] = "";// taskControl.Customer.City.Trim();  //dt.Rows[0]["BusinessCity"].ToString().Trim();
            fields[4] = "";// taskControl.Customer.ZipCode.Trim();  // dt.Rows[0]["BusinessZipCode"].ToString().Trim();
            fields[8] = txtAccountNombre.ToString().Trim();//txtAccountNombre.ToString().Trim();// "VICTOR R. LANZA AMARAL"; //NameOnAccount
            fields[50] = taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim(); //dt.Rows[0]["BusinessName"].ToString().Trim();
            fields[51] = taskControl.Customer.CustomerNo.Trim();  //ddlPaymentAmount.Trim();
            fields[52] = taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim(); //dt.Rows[0]["BusinessContact"].ToString().Trim();
            fields[53] = taskControl.Customer.Email.Trim(); //dt.Rows[0]["BusinessEmail"].ToString().Trim();
            fields[54] = taskControl.Customer.Cellular.Trim();  //dt.Rows[0]["BusinessPhone"].ToString().Trim();

            if (ddlMetodoPago.ToString().Trim() == "3" || ddlMetodoPago.ToString().Trim() == "4")
            {
                fields[5] = "True"; //Tarjeta de Credito
                fields[9] = GetMonth(); //ddlMes.SelectedItem.Value.Trim();// "02"; //CardExpiresMonth
                fields[10] = ddlYear.ToString().Trim(); //"2018"; //CardExpiresYear
                fields[11] = txtSecurityCode.ToString().Trim();  // "000"; //CVVNumber
                fields[7] = txtAccountNumber.ToString().Trim();  // "5347650005981017"; //Tarjeta de Credito - AccountNumber

                //Visa=1, MasterCard=2, American_Express=3
                switch (ddlMetodoPago.ToString().Trim())
                {
                    case "3": //Visa
                        fields[6] = "1";
                        break;
                    case "4": //MC
                        fields[6] = "2";
                        break;
                }
            }
            else
            {
                if (ddlMetodoPago.ToString().Trim() == "1" || ddlMetodoPago.ToString().Trim() == "2") //Checking or Saving Account
                {
                    fields[5] = "False"; //No es tarjeta de Credito

                    if (ddlMetodoPago.ToString().Trim() == "1")
                    {
                        fields[12] = "True"; //Checking
                        fields[13] = "False"; //Saving
                    }
                    else
                    {
                        fields[12] = "False"; //Checking
                        fields[13] = "True"; //Saving
                    }
                    fields[14] = ddlRoutingNumber.ToString().Trim(); // "021502011"; //RoutingNumber
                    fields[15] = txtAccountNumber.ToString().Trim(); // "041799936"; //AccountNumber - Bank Account
                }
            }
        }


        if (Password == "LS91A2QS-7522-4DWD-4219-A515469D569P")
        {
            try
            {
                using (PV.PaymentVaultSoapClient paymentClient = new PV.PaymentVaultSoapClient())
                {
                    if (paymentClient.TestConnection())
                    {
                        PV.WSUpdateResult result = paymentClient.TestCredentials(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), ConfigurationManager.AppSettings["locationId"].ToString(), "__WebService");

                        if (result.returnValue == PV.ReturnValue.Success)
                        {
                            PV.WSCustomer cust = new PV.WSCustomer();
                            cust.CustomerNumber = taskControl.Customer.CustomerNo.Trim();
                            cust.FirstName = taskControl.Customer.FirstName.Trim(); //fields[52];
                            cust.LastName = taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim();
                            cust.EntityId = Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]);
                            cust.Email = fields[53];
                            cust.Address1 = fields[1];
                            cust.Address2 = fields[2];
                            cust.City = fields[3];
                            cust.StateRegion = "";//"PR";
                            cust.PostalCode = fields[4];
                            cust.DaytimePhone = fields[54];
                            cust.IsCompany = false;
                            cust.Field1 = taskControl.Customer.CustomerNo.Trim();//TaskControlID.ToString();

                            PV.WSUpdateResult responseCust = new PV.WSUpdateResult();
                            responseCust = paymentClient.RegisterCustomer(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), cust);

                            //Success or Exist Customer
                            if (responseCust.returnValue == PV.ReturnValue.Success || responseCust.returnValue == PV.ReturnValue.Error_UniqueConstraint)
                            {
                                PV.WSAccount account = new PV.WSAccount();
                                account.CustomerNumber = taskControl.Customer.CustomerNo.Trim();

                                //Se busca si ya el cliente tiene esta cuenta registrada para no hacer la transaccion de cuenta nueva
                                // PV.WSAccount[] responseRegAccount;
                                //responseRegAccount = paymentClient.GetRegisteredAccounts(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), account.CustomerNumber);

                                if (fields[5] == "True")
                                {
                                    switch (fields[6])
                                    {
                                        case "1":
                                            account.AccountType = PV.WSAccountType.Visa;
                                            break;
                                        case "2":
                                            account.AccountType = PV.WSAccountType.MasterCard;
                                            break;
                                        case "3":
                                            account.AccountType = PV.WSAccountType.American_Express;
                                            break;
                                    }

                                    account.ExpirationMonth = Convert.ToByte(fields[9]);
                                    account.ExpirationYear = Convert.ToByte(fields[10].Substring(fields[10].ToString().Length - 2));
                                    account.NameOnAccount = fields[8];
                                    account.AccountNumber = fields[7];
                                    //account.CVVNumber = int.Parse(fields[11]);
                                }

                                if (fields[12] == "True" || fields[13] == "True")
                                {
                                    if (fields[12] == "True")
                                        account.AccountType = PV.WSAccountType.Checking;
                                    else if (fields[13] == "True")
                                        account.AccountType = PV.WSAccountType.Savings;
                                    account.NameOnAccount = fields[8];
                                    account.RoutingNumber = int.Parse(fields[14]);
                                    account.AccountNumber = fields[15];
                                }

                                account.AccountName = fields[52];
                                account.BillAddress1 = fields[1];
                                account.BillAddress2 = fields[2];
                                account.BillCity = fields[3];
                                account.BillStateRegion = "";//"PR";
                                account.BillPostalCode = fields[4];
                                string AccountReference = GetAccountReference();
                                account.AccountReferenceID = AccountReference;

                                RequestInfo += "CustomerNo: " + account.CustomerNumber + " | ";
                                RequestInfo += "NameOnAccount: " + account.NameOnAccount + " | ";
                                RequestInfo += "AccountType: " + account.AccountType.ToString() + " | ";
                                RequestInfo += "ExpirationMonth: " + account.ExpirationMonth.ToString() + " | ";
                                RequestInfo += "ExpirationYear: " + account.ExpirationYear.ToString() + " | ";
                                RequestInfo += "AccountNumber: " + account.AccountNumber + " | ";
                                RequestInfo += "RoutingNumber: " + account.RoutingNumber.ToString() + " | ";

                                RequestInfo += "AccountName: " + account.AccountName + " | ";
                                RequestInfo += "BillAddress1: " + account.BillAddress1 + " | ";
                                RequestInfo += "BillAddress2: " + account.BillAddress2 + " | ";
                                RequestInfo += "BillCity: " + account.BillCity + " | ";
                                RequestInfo += "BillStateRegion: " + account.BillStateRegion + " | ";
                                RequestInfo += "BillPostalCode: " + account.BillPostalCode + " | ";
                                RequestInfo += "AccountReferenceID: " + account.AccountReferenceID + " | ";


                                PV.WSUpdateResult responseAccount = new PV.WSUpdateResult();
                                responseAccount = paymentClient.RegisterAccount(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), account);

                                if (responseAccount.returnValue == PV.ReturnValue.Success || responseAccount.returnValue == PV.ReturnValue.Error_UniqueConstraint)
                                {
                                    string resultTranPay = "";
                                    if (fields[5] == "True") //Tarjeta de Credito
                                        resultTranPay = SaleFromCardAccount(paymentClient, AccountReference, transactionNumber);
                                    else
                                        resultTranPay = SaleFromBankAccount(paymentClient, AccountReference, transactionNumber);

                                    fields[75] = resultTranPay;
                                }

                                //Set Recurring Payment
                                if ((responseAccount.returnValue == PV.ReturnValue.Success || responseAccount.returnValue == PV.ReturnValue.Error_UniqueConstraint) && Payment1 != 0.0 &
                                    fields[75].Contains("Success"))
                                {
                                    PV.WSRecurring transaction = new PV.WSRecurring();
                                    transaction.LocationID = Convert.ToInt32(ConfigurationManager.AppSettings["locationId"]);
                                    transaction.CustomerNumber = taskControl.Customer.CustomerNo.Trim();
                                    transaction.AccountReferenceID = AccountReference;
                                    transaction.Description = taskControl.PolicyType.Trim() + " " + taskControl.PolicyNo.Trim(); // "Guardian Xtra Def Pay";
                                    transaction.Amount = Convert.ToDecimal(Payment1.ToString()); //Convert.ToDecimal(fields[0]);  //36.00m;
                                    transaction.InvoiceNumber = transactionNumber;
                                    transaction.Frequency = PV.WSFrequency.Once_a_Month;
                                    transaction.PaymentDay = 1;
                                    transaction.StartDate = DateTime.Parse(Fecha1); // (DateTime.Parse(DateTime.Now.AddMonths(1).Month.ToString() + "/01/" + DateTime.Now.AddMonths(1).Year.ToString()));
                                    transaction.Field1 = taskControl.Customer.CustomerNo.Trim();//TaskControlID.ToString();

                                    transaction.NumPayments = 0;
                                    if (taskControl.XtraIsSixPayment)//(bool)taskControl.GuardianXtraCollection.Rows[0]["IsSixPayment"])
                                    {
                                        transaction.NumPayments = 5;
                                    }

                                    if (taskControl.XtraIsFourPayment)//(bool)taskControl.GuardianXtraCollection.Rows[0]["IsFourPayment"])
                                    {
                                        transaction.NumPayments = 3;
                                    }

                                    transaction.PaymentsToDate = 0;
                                    transaction.NotificationMethod = PV.WSNotificationMethod.Email;
                                    transaction.NextPaymentDate = DateTime.Parse(Fecha2);
                                    transaction.Enabled = true;
                                    transaction.PaymentOrigin = PV.WSPaymentOrigin.Internet;

                                    string RecurringReference = GetRecurringReference();
                                    transaction.RecurringReferenceID = RecurringReference; //"100032";//PolicyNo";
                                    transaction.Field1 = transactionNumber;
                                    retVal[1] = transactionNumber;

                                    PV.WSUpdateResult response = new PV.WSUpdateResult();
                                    response = paymentClient.SetupRecurringPayment(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), transaction);

                                    if (response.returnValue == PV.ReturnValue.Success)
                                    {
                                        //fields[74] = response.message;
                                        //fields[75] = response.returnValue.ToString();
                                        //retVal[0] = "S";
                                        //status = "2";                                          
                                    }
                                    else
                                    {
                                        //retVal[0] = "F";
                                        //fields[74] = response.message;
                                        //fields[75] = response.returnValue.ToString();
                                        //status = "4";
                                        //SendTransactionErrorEmail(int.Parse(fields[73]));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //retVal[0] = "C";
                    }

                    return fields[75].Trim();
                }
            }
            catch (Exception exc)
            {
                LogError(exc);
                returnMessage = "Transaction Fail.";
                if (exc.Message.Trim().Contains("The message was"))
                {
                    int index = exc.Message.Trim().IndexOf("The message was");
                    returnMessage = exc.Message.Trim().Substring(index + 16);

                    index = returnMessage.Trim().IndexOf(".");
                    returnMessage = returnMessage.Substring(0, index + 1);
                }

                return returnMessage;
            }
        }
        else
        {
            return "Wrong Password.";
        }
    }

    private void AddPayment(string transactionNumber, string PaymentConfirmationNumber, string Result, string ConsoleResult, string RequestInfo, string RequestResponse)
    {
        EncryptClass.EncryptClass encryp = new EncryptClass.EncryptClass();

        EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

        string VendorID = "0";

        string x = txtAccountNumber.ToString().Trim().Substring(txtAccountNumber.ToString().Trim().Length - 4);

        string AccNumber = encryp.Encrypt(x);

        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[12];
        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, "-99", ref cookItems);//TaskControlID.ToString()
        DbRequestXmlCooker.AttachCookItem("PaymentMethodID", SqlDbType.Int, 0, ddlMetodoPago.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PaymentDate", SqlDbType.DateTime, 0, DateTime.Now.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PaymentAmount", SqlDbType.Money, 0, ddlPaymentAmount.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("AccountNumberLast4", SqlDbType.VarChar, 100, AccNumber, ref cookItems);
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
        PaymentID = exec.Insert("AddPayment", xmlDoc);
    }

    private void UpdatePayment()
    {
        EncryptClass.EncryptClass encryp = new EncryptClass.EncryptClass();

        EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControl.TaskControlID.ToString(), ref cookItems);//TaskControlID.ToString()
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
        PaymentID = exec.Insert("UpdatePayment", xmlDoc);
    }


    private void UpdateGuadianXtraHasAccident12(int TaskControlID, bool HasAccident12)
    {
        EncryptClass.EncryptClass encryp = new EncryptClass.EncryptClass();

        EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

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


    private void DeletePayment()
    {
        EncryptClass.EncryptClass encryp = new EncryptClass.EncryptClass();

        EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
        DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, taskControl.CustomerNo.ToString(), ref cookItems);//TaskControlID.ToString()
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
        PaymentID = exec.Insert("DeletePayment", xmlDoc);
    }

    private void SendEmail(string CustomerName, string Email, string PaymentMethod, string PaymentType, string PaymentAmount, string AccNumber, string EntryDate, string PolicyNumber, string DebitDate)
    {
        try
        {
            EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

            var format = "MMMM dd, yyyy";
            var dt = DateTime.ParseExact(EntryDate, format, new CultureInfo("en-US"));
            EntryDate = dt.ToString(format, new CultureInfo("es-ES"));
            dt = DateTime.ParseExact(DebitDate, format, new CultureInfo("en-US"));
            DebitDate = dt.ToString(format, new CultureInfo("es-ES"));

            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
            string ImagePath = System.Configuration.ConfigurationManager.AppSettings["RootURL"] + "Images2\\MidOcean_logo.png";

            LinkedResource logo = new LinkedResource(ImagePath, MediaTypeNames.Image.Jpeg);
            logo.ContentId = Guid.NewGuid().ToString();

            string htmlBody = "<html><body><p>Estimado " + CustomerName + " (" + Email + ")</p><p>Este correo electrónico es para informarle que la Agencia de Seguros MidOcean ha procesado electrónicamente una sola transacción por la cantidad de $" + PaymentAmount + " de la " + PaymentType + " que finaliza con " + AccNumber + " por su autorización en " + EntryDate + ". El número de póliza que la Agencia de Seguros MidOcean ha proporcionado para esta transacción es la siguiente: " + PolicyNumber + ".</p><p>Esta transacción se debitará de su cuenta en " + DebitDate + " y aparecerá en su estado bancario en la sección de transacciones electrónicas. Si esta transacción es un error o es una transacción fraudulenta, comuníquese con la Agencia de Seguros MidOcean al 787-520-6178 si tiene alguna pregunta o inquietud. Gracias por su pago.</p><br><img src=\"cid:" + logo.ContentId + "\"/></body></html>";
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString
               (htmlBody, null, MediaTypeNames.Text.Html);
            avHtml.LinkedResources.Add(logo);

            //Email (El email que ve el que recibe)
            string emailNoreplay = "policyconfirmation@midoceanpr.com";//"lsemailservice@gmail.com";
            //Email (That send the message)
            string emailSend = "lsemailservice@gmail.com";
            string msg = "";
            string pdf = PdfFileName;
            MailMessage SM = new MailMessage();

            SM.Subject = "MidOcean Insurance - Su pago ha sido recibido";
            SM.From = new System.Net.Mail.MailAddress(emailNoreplay);

            SM.AlternateViews.Add(avHtml);
            SM.IsBodyHtml = true;
            SM.Attachments.Add(new Attachment(ProcessedPath + pdf));
            SM.To.Add(Email);

            //SM.Bcc.Add("econcepcion@guardianinsurance.com");
            //SM.Bcc.Add("smartinez@guardianinsurance.com");
            //SM.Bcc.Add("rcruz@guardianinsurance.com");
            //SM.Bcc.Add("susana.martinez11@gmail.com");

            try
            {
                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailNoreplay, "Conf1rm@tion");// new NetworkCredential(emailNoreplay, "L@nzaSoft1$");
                client.Host = ConfigurationManager.AppSettings["IPMail"].ToString().Trim();    //client.Host = "smtp.gmail.com";
                client.Port = 587; // 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(SM);
                msg = "0001";
            }
            catch (Exception exc)
            {
                msg = exc.InnerException.ToString() + " " + exc.Message;
            }

        }
        catch (Exception exc)
        {

        }
    }

    private string GetMonth()
    {
        string mes = "";
        switch (ddlMes.ToString().ToLower().Trim())
        {

            case "enero":
            case "january": 
            case "1": 
            case "01":
                mes = "01";
                break;
            case "febrero":
            case "february":
            case "2":
            case "02":
                mes = "02";
                break;
            case "marzo":
            case "march":
            case "3":
            case "03":
                mes = "03";
                break;
            case "abril":
            case "april":
            case "4":
            case "04":
                mes = "04";
                break;
            case "mayo":
            case "may":
            case "5":
            case "05":
                mes = "05";
                break;
            case "junio":
            case "june":
            case "6":
            case "06":
                mes = "06";
                break;
            case "julio":
            case "july":
            case "7":
            case "07":
                mes = "07";
                break;
            case "agosto":
            case "august":
            case "8":
            case "08":
                mes = "08";
                break;
            case "septiembre":
            case "september":
            case "9":
            case "09":
                mes = "09";
                break;
            case "octubre":
            case "october":
            case "10":
                mes = "10";
                break;
            case "noviembre":
            case "november":
            case "11":
                mes = "11";
                break;
            case "diciembre":
            case "december":
            case "12":
                mes = "12";
                break;
        }

        return mes;
    }

    private string SaleFromCardAccount(PV.PaymentVaultSoap paymentClient, string AccountReference, string transactionNumber)
    {
        string result = "Fail";
        try
        {
            if (Session["TaskControl"] != null)
            {
                EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                //PV.WSUpdateResult response = new PV.WSUpdateResult();
                // using (PV.PaymentVaultSoapClient paymentClient2 = new PV.PaymentVaultSoapClient())
                //{
                PV.SaleFromCardAccountRequest transaction = new PV.SaleFromCardAccountRequest();
                PV.SaleFromCardAccountRequestBody body = new PV.SaleFromCardAccountRequestBody();

                body.storeId = Convert.ToInt32(ConfigurationManager.AppSettings["storeId"]);
                body.storeKey = ConfigurationManager.AppSettings["storeKey"].ToString();
                body.entityId = Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]);
                body.locationId = Convert.ToInt32(ConfigurationManager.AppSettings["locationId"]);
                body.accountReferenceId = AccountReference;
                body.paymentOrigin = PV.WSPaymentOrigin.Internet;
                body.Amount = Convert.ToDecimal(ddlPaymentAmount.Trim());
                body.terminalNumber = "__WebService";
                body.TransactionNumber = GetTransactionNumber(); // transactionNumber;
                body.Description = taskControl.PolicyType.Trim() + " " + taskControl.PolicyNo.Trim(); // "Guardian Xtra Pay";;

                body.Field1 = "";//taskControl.Customer.Address1.Trim(); // dt.Rows[0]["BusinessAddress1"].ToString().Trim();
                body.Field2 = "";//taskControl.Customer.Address2.Trim(); //dt.Rows[0]["BusinessAddress2"].ToString().Trim();
                body.Field3 = "";//taskControl.Customer.City.Trim();

                if (txtSecurityCode.ToString().Trim() == "")
                    body.CCVS = 0;
                else
                    body.CCVS = int.Parse(txtSecurityCode.ToString().Trim());

                body.ownerApplication = PV.WSOwnerApplication.Web_Service;

                RequestInfo += "CCVS: " + body.CCVS.ToString() + " | ";
                RequestInfo += "storeId: " + body.storeId.ToString() + " | ";
                RequestInfo += "storeKey: " + body.storeKey + " | ";
                RequestInfo += "entityId: " + body.entityId.ToString() + " | ";
                RequestInfo += "locationId: " + body.locationId.ToString() + " | ";
                RequestInfo += "accountReferenceId: " + body.accountReferenceId + " | ";
                RequestInfo += "paymentOrigin: " + body.paymentOrigin.ToString() + " | ";
                RequestInfo += "Amount: " + body.Amount.ToString() + " | ";

                RequestInfo += "terminalNumber: " + body.terminalNumber + " | ";
                RequestInfo += "TransactionNumber: " + body.TransactionNumber + " | ";
                RequestInfo += "Description: " + body.Description + " | ";
                RequestInfo += "ownerApplication: " + body.ownerApplication.ToString() + " | ";

                transaction.Body = body;
                PV.SaleFromCardAccountResponse response = new PV.SaleFromCardAccountResponse();
                response = paymentClient.SaleFromCardAccount(transaction);
                if (response.Body.SaleFromCardAccountResult.Success == true)
                {
                    result = response.Body.SaleFromCardAccountResult.ResponseCode + "|" + response.Body.SaleFromCardAccountResult.ReferenceNumber;
                    RequestResponse = result;
                }
                else
                {
                    result = response.Body.SaleFromCardAccountResult.ResponseCode + "|" + response.Body.SaleFromCardAccountResult.ReferenceNumber;
                    RequestResponse = result;
                }
            }

            return result;
        }
        catch (Exception exc)
        {
            LogError(exc);
            return exc.Message;
        }
    }

    private string SaleFromBankAccount(PV.PaymentVaultSoap paymentClient, string AccountReference, string transactionNumber)
    {
        string result = "Fail";
        try
        {
            if (Session["TaskControl"] != null)
            {
                EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                //PV.WSUpdateResult response = new PV.WSUpdateResult();
                // using (PV.PaymentVaultSoapClient paymentClient2 = new PV.PaymentVaultSoapClient())
                //{
                PV.SaleFromBankAccountRequest transaction = new PV.SaleFromBankAccountRequest();
                PV.SaleFromBankAccountRequestBody body = new PV.SaleFromBankAccountRequestBody();

                body.storeId = Convert.ToInt32(ConfigurationManager.AppSettings["storeId"]);
                body.storeKey = ConfigurationManager.AppSettings["storeKey"].ToString();
                body.entityId = Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]);
                body.locationId = Convert.ToInt32(ConfigurationManager.AppSettings["locationId"]);
                body.accountReferenceId = AccountReference;
                body.paymentOrigin = PV.WSPaymentOrigin.Internet;
                body.notificationMethod = PV.WSNotificationMethod.Email;
                body.Amount = Convert.ToDecimal(ddlPaymentAmount.Trim());
                body.terminalNumber = "__WebService";
                body.TransactionNumber = GetTransactionNumber(); // transactionNumber;
                body.Description = taskControl.PolicyType.Trim() + " " + taskControl.PolicyNo.Trim(); // "Guardian Xtra Pay";;

                body.Field1 = "";//taskControl.Customer.Address1.Trim(); // dt.Rows[0]["BusinessAddress1"].ToString().Trim();
                body.Field2 = "";//taskControl.Customer.Address2.Trim(); //dt.Rows[0]["BusinessAddress2"].ToString().Trim();
                body.Field3 = "";//taskControl.Customer.City.Trim();    
                body.ownerApplication = PV.WSOwnerApplication.Web_Service;
                transaction.Body = body;
                PV.SaleFromBankAccountResponse response = new PV.SaleFromBankAccountResponse();
                response = paymentClient.SaleFromBankAccount(transaction);
                if (response.Body.SaleFromBankAccountResult.Success == true)
                {
                    result = response.Body.SaleFromBankAccountResult.ResponseCode + "|" + response.Body.SaleFromBankAccountResult.ReferenceNumber;

                }
                else
                {
                    result = response.Body.SaleFromBankAccountResult.ResponseCode + "|" + response.Body.SaleFromBankAccountResult.ReferenceNumber;
                }
            }

            return result;
        }
        catch (Exception exc)
        {
            return exc.Message;
        }
    }

    private string GetAccountReference()
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
        int transNo = exec.Insert("AddAccountReference", xmlDoc);

        return transNo.ToString();
    }

    private void SetPaymentAmount()
    {
        EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
        CustomerNumber = taskControl.Customer.CustomerNo.Trim();

        if (taskControl.PolicyClassID == 23) //Guardian Xtra
        {
            if (taskControl.XtraIsFourPayment)//(bool)taskControl.GuardianXtraCollection.Rows[0]["IsFourPayment"])
            {
                Fecha1 = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();
                Fecha2 = DateTime.Parse(Fecha1).AddMonths(1).ToShortDateString();
                Fecha3 = DateTime.Parse(Fecha1).AddMonths(2).ToShortDateString();


                if (taskControl.XtraPremium == 89)//(double)taskControl.GuardianXtraCollection.Rows[0]["Premium"]
                {
                    ddlPaymentAmount = "22.25";
                    Payment1 = 25.25;
                    Payment2 = 25.25;
                    Payment3 = 25.25;
                }

                if (taskControl.XtraPremium == 95)
                {
                    ddlPaymentAmount = "23.75";
                    Payment1 = 26.75;
                    Payment2 = 26.75;
                    Payment3 = 26.75;
                }

                if (taskControl.XtraPremium == 100)
                {
                    ddlPaymentAmount = "25.00";
                    Payment1 = 28.00;
                    Payment2 = 28.00;
                    Payment3 = 28.00;
                }
            }
            else
                if (taskControl.XtraIsSixPayment)//(bool)taskControl.GuardianXtraCollection.Rows[0]["IsSixPayment"]
                {
                    Fecha1 = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();
                    Fecha2 = DateTime.Parse(Fecha1).AddMonths(1).ToShortDateString();
                    Fecha3 = DateTime.Parse(Fecha1).AddMonths(2).ToShortDateString();
                    Fecha4 = DateTime.Parse(Fecha1).AddMonths(3).ToShortDateString();
                    Fecha5 = DateTime.Parse(Fecha1).AddMonths(4).ToShortDateString();
                    Fecha6 = DateTime.Parse(Fecha1).AddMonths(5).ToShortDateString();

                    if (taskControl.XtraPremium == 89)
                    {
                        ddlPaymentAmount = "14.85";
                        Payment1 = 18.83;
                        Payment2 = 18.83;
                        Payment3 = 18.83;
                        Payment4 = 18.83;
                        Payment5 = 18.83;
                        Payment6 = 18.83;
                    }

                    if (taskControl.XtraPremium == 95)
                    {
                        ddlPaymentAmount = "15.85";
                        Payment1 = 19.83;
                        Payment2 = 19.83;
                        Payment3 = 19.83;
                        Payment4 = 19.83;
                        Payment5 = 19.83;
                        Payment6 = 19.83;
                    }

                    if (taskControl.XtraPremium == 100)
                    {
                        ddlPaymentAmount = "16.70";
                        Payment1 = 20.66;
                        Payment2 = 20.66;
                        Payment3 = 20.66;
                        Payment4 = 20.66;
                        Payment5 = 20.66;
                        Payment6 = 20.66;
                    }
                }
                else
                {
                    ddlPaymentAmount = (taskControl.XtraPremium).ToString("###,###.00");
                }
        }
    }

    private string GetRecurringReference()
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
        int transNo = exec.Insert("AddRecurringReference", xmlDoc);

        return transNo.ToString();
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

    private bool ValidFields()
    {
     
        ArrayList errorMessages = new ArrayList();
        bool IsError = false;

        try
        {

            //if (ddlMetodoPago.ToString().Trim() == "")
            //{
            //    errorMessages.Add("Debes de escoger un Método de Pago." + "\r\n");
            //    IsError = true;
            //    ddlMetodoPago.BorderColor = System.Drawing.Color.Red;
            //}

            //if (!IsError)
            //{
            //    if (txtAccountNombre.ToString().Trim() == "")
            //    {
            //        errorMessages.Add("Debes de entrar el nombre de la cuenta." + "\r\n");
            //        IsError = true;
            //        txtAccountNombre.BorderColor = System.Drawing.Color.Red;
            //    }

            //    if (txtAccountNumber.ToString().Trim() == "")
            //    {
            //        errorMessages.Add("Debes de entrar el número de cuenta." + "\r\n");
            //        IsError = true;
            //        txtAccountNumber.BorderColor = System.Drawing.Color.Red;
            //    }

            //    if (ddlPaymentAmount.Trim() == "")
            //    {
            //        errorMessages.Add("Debes de escoger la cantidad a pagar." + "\r\n");
            //        IsError = true;
            //        ddlPaymentAmount.BorderColor = System.Drawing.Color.Red;
            //    }

            //    if (ddlMetodoPago.ToString().Trim() == "1" || ddlMetodoPago.ToString().Trim() == "2") //Checking & Saving
            //    {
            //        if (ddlRoutingNumber.SelectedItem.Text.Trim() == "")
            //        {
            //            errorMessages.Add("Debes de escoger la ruta y transito de la cuenta." + "\r\n");
            //            IsError = true;
            //            ddlRoutingNumber.BorderColor = System.Drawing.Color.Red;
            //        }

            //        //if (txtAccountNumber.ToString().Trim().Length != 9)
            //        //{
            //        //    errorMessages.Add("Error en el número de cuenta de banco, deben ser 9 digitos." + "\r\n");
            //        //}
            //    }

            //    if (int.Parse(ddlMetodoPago.ToString().Trim()) > 2) //TarjetaCredito
            //    {
            //        if (ddlYear.SelectedItem.Text.Trim() == "")
            //        {
            //            errorMessages.Add("Debes de seleccionar el año de expiración de la tarjeta de crédito." + "\r\n");
            //            IsError = true;
            //            ddlYear.BorderColor = System.Drawing.Color.Red;
            //        }
            //        if (ddlMes.SelectedItem.Value.Trim() == "")
            //        {
            //            errorMessages.Add("Debes de seleccionar el mes de expiración de la tarjeta de crédito." + "\r\n");
            //            IsError = true;
            //            ddlMes.BorderColor = System.Drawing.Color.Red;
            //        }
            //        if (txtSecurityCode.Text.Trim() == "")
            //        {
            //            errorMessages.Add("Debes de entrar el número de seguridad." + "\r\n");
            //            IsError = true;
            //            txtSecurityCode.BorderColor = System.Drawing.Color.Red;
            //        }
            //    }

            //    if (!IsError)
            //    {
            //        if (int.Parse(ddlMetodoPago.ToString().Trim()) > 2) //TarjetaCredito
            //        {
            //            if (ddlYear.SelectedItem.Text.Trim() == "")
            //            {
            //                errorMessages.Add("Debes de seleccionar el año de expiración de la tarjeta de crédito." + "\r\n");
            //                IsError = true;
            //                ddlYear.BorderColor = System.Drawing.Color.Red;
            //            }
            //            if (ddlMes.SelectedItem.Text.Trim() == "")
            //            {
            //                errorMessages.Add("Debes de seleccionar el mes de expiración de la tarjeta de crédito." + "\r\n");
            //                IsError = true;
            //                ddlMes.BorderColor = System.Drawing.Color.Red;
            //            }

            //            if (ddlMes.SelectedItem.Value.Trim() != "" && ddlYear.SelectedItem.Value.Trim() != "")
            //            {
            //                string mes = GetMonth();

            //                if (int.Parse(mes) < DateTime.Now.Month &&
            //                    int.Parse(ddlYear.SelectedItem.Text.Trim()) == DateTime.Now.Year)
            //                {
            //                    errorMessages.Add("Error, Tarjeta expirada, Favor de verificar el mes y año de la tarjeta de crédito." + "\r\n");
            //                    IsError = true;
            //                    ddlYear.BorderColor = System.Drawing.Color.Red;
            //                    ddlMes.BorderColor = System.Drawing.Color.Red;
            //                }
            //            }

            //            if (ddlMetodoPago.ToString().Trim() == "3") //Visa
            //            {
            //                if (txtAccountNumber.ToString().Trim().Length != 16)
            //                {
            //                    errorMessages.Add("Error en el número de la tarjeta de crédito, deben ser 16 digitos." + "\r\n");
            //                    IsError = true;
            //                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
            //                }

            //                if (txtAccountNumber.ToString().Trim().Substring(0, 1) != "4")
            //                {
            //                    errorMessages.Add("Error, este número de tarjeta de crédito no pertenece a " +
            //                            ddlMetodoPago.SelectedItem.Text.Trim() + ".\r\n");
            //                    IsError = true;
            //                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
            //                }
            //            }

            //            if (ddlMetodoPago.ToString().Trim() == "4") //MasterCard
            //            {
            //                if (txtAccountNumber.ToString().Trim().Length != 16)
            //                {
            //                    errorMessages.Add("Error en el número de la tarjeta de crédito, deben ser 16 digitos." + "\r\n");
            //                    IsError = true;
            //                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
            //                }

            //                if (txtAccountNumber.ToString().Trim().Substring(0, 1) != "5")
            //                {
            //                    errorMessages.Add("Error, este número de tarjeta de crédito no pertenece a " +
            //                       ddlMetodoPago.SelectedItem.Text.Trim() + ".\r\n");
            //                    IsError = true;
            //                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
            //                }
            //            }

            //            if (ddlMetodoPago.ToString().Trim() == "5") //Amex
            //            {
            //                if (txtAccountNumber.ToString().Trim().Length != 15)
            //                {
            //                    errorMessages.Add("Error en el número de la tarjeta de crédito, deben ser 15 digitos." + "\r\n");
            //                    IsError = true;
            //                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
            //                }

            //                if (txtAccountNumber.ToString().Trim().Substring(0, 1) != "3")
            //                {
            //                    errorMessages.Add("Error, este número de tarjeta de crédito no pertenece a " +
            //                        ddlMetodoPago.SelectedItem.Text.Trim() + ".\r\n");
            //                    IsError = true;
            //                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
            //                }
            //            }
            //        }
            //    }
            //}

            //if (errorMessages.Count > 0)
            //{
            //    string popUpString = "";
            //    int maxMess = 1;

            //    foreach (string message in errorMessages)
            //    {
            //        popUpString += " " + maxMess.ToString().Trim() + ". " + message + "";
            //        maxMess++;
            //    }

            //    int a = maxMess * 30;

            //    if (a < 61)
            //        a = 65;

            //    Unit u = new Unit(a.ToString() + "px");

            //    lblerror.Height = u;
            //    lblerror.Visible = true;
            //    lblerror.Text = popUpString.ToString();
            //    // ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "ResetScrollPosition();", true);
            //}

            return IsError;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private string PrintPolicyBusinessDeclaration()
    {
        string PDFPath = "";
        try
        {
            List<string> mergePaths = new List<string>();
            EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];

            int taskControlID = taskControl.TaskControlID;

            mergePaths = ImprimirDecPageComercial(mergePaths, taskControlID);

            mergePaths = ImprimirDecPageComercialP2(mergePaths, taskControlID);
            mergePaths = ImprimirDecPageComercialP3(mergePaths, taskControlID);


            //for (int i = 0; i < taskControl.VehicleCollection.Rows.Count; i++)
            //{
            //    mergePaths = ImprimirIDCards(mergePaths, taskControlID, int.Parse(taskControl.VehicleCollection.Rows[i]["VehicleDetailID"].ToString()));
            //}

            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
            //Generar PDF
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFile = "";
            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());

            PDFPath = ProcessedPath + FinalFile;
            //Process myProcess = new Process();
            //myProcess.StartInfo.FileName = ProcessedPath + FinalFile; //PDF path
            //myProcess.Start();

            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

        }
        catch (Exception exc)
        {
            LogError(exc);
            //lblRecHeader.Text = exc.Message.Trim() + " - " + exc.InnerException.ToString();
            //mpeSeleccion.Show();
        }

        return PDFPath;
    }

    private string PrintPolicyPersonalDeclaration()
    {
        string PDFPath = "";
        try
        {
            List<string> mergePaths = new List<string>();
            EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];

            int taskControlID = taskControl.TaskControlID;

            mergePaths = ImprimirDecPagePersonal(mergePaths, taskControlID);

            //for (int i = 0; i < taskControl.VehicleCollection.Rows.Count; i++)
            //{
            //    mergePaths = ImprimirIDCards(mergePaths, taskControlID, int.Parse(taskControl.VehicleCollection.Rows[i]["VehicleDetailID"].ToString()));
            //}

            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
            //Generar PDF
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFile = "";
            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());

            PDFPath = ProcessedPath + FinalFile;
            //Process myProcess = new Process();
            //myProcess.StartInfo.FileName = ProcessedPath + FinalFile; //PDF path
            //myProcess.Start();

            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

        }
        catch (Exception exc)
        {
            LogError(exc);
            //lblRecHeader.Text = exc.Message.Trim() + " - " + exc.InnerException.ToString();
            //mpeSeleccion.Show();
        }

        return PDFPath;
    }

    private List<string> ImprimirDecPagePersonal(List<string> mergePaths, int taskControl)
    {
        try
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

            int s = taskControl;

            GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter ds1 = new GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter();
            GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter ds2 = new GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter();
            GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter ds3 = new GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter();
            GetReportNameInsuredTableAdapters.GetReportNameInsuredTableAdapter ds4 = new
GetReportNameInsuredTableAdapters.GetReportNameInsuredTableAdapter();

            ReportDataSource rds1 = new ReportDataSource();
            ReportDataSource rds2 = new ReportDataSource();
            ReportDataSource rds3 = new ReportDataSource();
            ReportDataSource rds4 = new ReportDataSource();


            rds1 = new ReportDataSource("GetReportAutoVehiclesInfo_VI", (DataTable)ds1.GetData(s));
            rds2 = new ReportDataSource("GetReportAutoGeneralInfo_VI", (DataTable)ds2.GetData(s));
            rds3 = new ReportDataSource("GetReportAutoDriversInfo_VI", (DataTable)ds3.GetData(s));
            rds4 = new ReportDataSource("GetReportNameInsured", (DataTable)ds4.GetData(s));

            //Nuevo
            string Endosos = "";
            Session["Endosos"] = "";
            Session["MotoristPremium"] = "";

            ReportParameter[] parameters = new ReportParameter[2];

            EPolicy.TaskControl.Autos taskControl1 = (EPolicy.TaskControl.Autos)Session["TaskControl"];


            //DataTable dt = GetUsernameByUserID(cp.UserID);
            string dUserName = "";

            //if (dt.Rows.Count > 0)
            //{
            //    dUserName = dt.Rows[0]["UserName"].ToString();
            //}

            //Verifies the endorsements for each vehicle to add it in the dec page
            for (int i = 0; taskControl1.VehicleCollection.Rows.Count > i; i++)
            {
                if (taskControl1.VehicleCollection.Rows[i]["MotoristPremium"].ToString() != "0")
                {
                    Session["MotoristPremium"] = "80";
                }

                if (Endosos.Contains("Endorsements made part of this Policy at this time of issue : \r\n"))
                {
                    Endosos += "";
                }
                else
                {
                    Endosos += "Endorsements made part of this Policy at this time of issue : \r\n"; //+ Environment.NewLine;
                }

                if (taskControl1.VehicleCollection.Rows[i]["BIPremium"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["IsMotorcycleScooter"].ToString() == "True")
                {
                    if (Endosos.Contains("CA0001, GIC3, GIC17, GIC21, GIC30, PP03260886, Under26excl"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "Endorsements made part of this Policy at this time of issue : \r\n")
                            Endosos += ", CA0001, GIC3, GIC17, GIC21, GIC30, PP03260886, Under26excl";
                        else
                            Endosos += " CA0001, GIC3, GIC17, GIC21, GIC30, PP03260886, Under26excl";
                    }
                }
                else if (taskControl1.VehicleCollection.Rows[i]["BIPremium"].ToString() != "0") //&& taskControl1.VehicleCollection.Rows[i]["CollPremium"].ToString() == "0" && taskControl1.VehicleCollection.Rows[i]["CompPremium"].ToString() == "0") // liability only
                {
                    if (Endosos.Contains("A117, GIC3, GIC17, GIC21, GIC30, PP03260886, Under26excl"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "Endorsements made part of this Policy at this time of issue : \r\n")
                            Endosos += ", A117, GIC3, GIC17, GIC21, GIC30, PP03260886, Under26excl";
                        else
                            Endosos += " A117, GIC3, GIC17, GIC21, GIC30, PP03260886, Under26excl";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["CollPremium"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["CompPremium"].ToString() != "0")
                {
                    if (Endosos.Contains("GIC12, A4555, GIC20, GIC25"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "Endorsements made part of this Policy at this time of issue : \r\n")
                            Endosos += ", GIC12, A4555, GIC20, GIC25";
                        else
                            Endosos += " GIC12, A4555, GIC20, GIC25";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["RentalReim"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Private")
                {
                    if (Endosos.Contains("GIC22"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "Endorsements made part of this Policy at this time of issue : \r\n")
                            Endosos += ", GIC22";
                        else
                            Endosos += " GIC22";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["ADDPremium"].ToString() != "0" && (taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Private" || taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Commercial"))
                {
                    if (Endosos.Contains("UIP248"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "Endorsements made part of this Policy at this time of issue : \r\n")
                            Endosos += ", UIP248";
                        else
                            Endosos += " UIP248";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Rental")// && taskControl1.VehicleCollection.Rows[i]["IsMotorcycleScooter"].ToString() != "True")
                {
                    if (Endosos.Contains("GIC16"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "Endorsements made part of this Policy at this time of issue : \r\n")
                            Endosos += ", GIC16";
                        else
                            Endosos += " GIC16";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["TaxiLossAmount"].ToString() != "0")
                {
                    if (Endosos.Contains("GIC23"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "Endorsements made part of this Policy at this time of issue : \r\n")
                            Endosos += ", GIC23";
                        else
                            Endosos += " GIC23";
                    }

                    if (taskControl1.VehicleCollection.Rows[i]["Island"].ToString() == "St. Croix")
                    {
                        if (Endosos.Contains("VIPA"))
                        {
                            Endosos += "";
                        }
                        else
                        {
                            if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                                Endosos += ", VIPA";
                            else
                                Endosos += " VIPA";
                        }
                    }
                }
            }

            Session["Endosos"] = Endosos;

            parameters[0] = new ReportParameter("Endosos", Endosos.ToString().Trim());

            parameters[1] = new ReportParameter("Username", dUserName.ToString().Trim());

            //Nuevo

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/DecPage-PAP256507.rdlc");
            viewer1.LocalReport.SetParameters(parameters);
            viewer1.LocalReport.DataSources.Add(rds1);
            viewer1.LocalReport.DataSources.Add(rds2);
            viewer1.LocalReport.DataSources.Add(rds3);
            viewer1.LocalReport.DataSources.Add(rds4);
            viewer1.LocalReport.Refresh();

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-PAP";
            string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-PAP" + ".pdf";

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

    private List<string> ImprimirDecPageComercial(List<string> mergePaths, int taskControl) //, int VehicleDetailID)
    {
        try
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

            int s = taskControl;

            GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter ds1 = new GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter();
            GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter ds2 = new GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter();
            GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter ds3 = new GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter();
            GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicy_VITableAdapter ds4 = new GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicy_VITableAdapter();
            GetReportNameInsuredTableAdapters.GetReportNameInsuredTableAdapter ds5 = new
GetReportNameInsuredTableAdapters.GetReportNameInsuredTableAdapter();

            //ReportDataSource rds1 = new ReportDataSource();
            ReportDataSource rds2 = new ReportDataSource();
            // ReportDataSource rds3 = new ReportDataSource();
            ReportDataSource rds4 = new ReportDataSource();
            ReportDataSource rds5 = new ReportDataSource();


            //rds1 = new ReportDataSource("GetReportAutoVehiclesInfo_VI", (DataTable)ds1.GetData(s));
            rds2 = new ReportDataSource("GetReportAutoGeneralInfo_VI", (DataTable)ds2.GetData(s));
            //rds3 = new ReportDataSource("GetReportAutoDriversInfo_VI", (DataTable)ds3.GetData(s));
            rds4 = new ReportDataSource("GetReportAutoVehicleInfoPolicy_VI", (DataTable)ds4.GetData(s)); //, VehicleDetailID));
            rds5 = new ReportDataSource("GetReportNameInsured", (DataTable)ds5.GetData(s));

            //Nuevo JNF
            string Endosos = "";
            Session["Endosos"] = "";
            Session["Island"] = "";
            Session["VehicleUse"] = "";
            Session["MotoristPremium"] = "";

            ReportParameter[] parameters = new ReportParameter[2];

            EPolicy.TaskControl.Autos taskControl1 = (EPolicy.TaskControl.Autos)Session["TaskControl"];

            //DataTable dt = GetUsernameByUserID(cp.UserID);
            string dUserName = "";

            //if (dt.Rows.Count > 0)
            //{
            //    dUserName = dt.Rows[0]["UserName"].ToString();
            //}

            //Verifies the endorsements for each vehicle to add it in the dec page
            for (int i = 0; taskControl1.VehicleCollection.Rows.Count > i; i++)
            {
                if (taskControl1.VehicleCollection.Rows[i]["Island"].ToString() != "")
                {
                    if (taskControl1.VehicleCollection.Rows[i]["Island"].ToString().Trim() == "St. Croix")
                    {
                        Session["Island"] = "Croix";
                    }

                }

                if (taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Taxi")
                {
                    Session["VehicleUse"] = "Taxi";
                }

                if (taskControl1.VehicleCollection.Rows[i]["MotoristPremium"].ToString() != "0")
                {
                    Session["MotoristPremium"] = "80";
                }

                if (Endosos.Contains("FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n"))
                {
                    Endosos += "";
                }
                else
                {
                    Endosos += "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n"; //+ Environment.NewLine;
                }

                if (taskControl1.VehicleCollection.Rows[i]["BIPremium"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["IsMotorcycleScooter"].ToString() == "True")
                {
                    if (Endosos.Contains("CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                        else
                            Endosos += " CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                    }
                }
                else if (taskControl1.VehicleCollection.Rows[i]["BIPremium"].ToString() != "0") //&& taskControl1.VehicleCollection.Rows[i]["CollPremium"].ToString() == "0" && taskControl1.VehicleCollection.Rows[i]["CompPremium"].ToString() == "0") // liability only
                {
                    if (Endosos.Contains("CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                        else
                            Endosos += " CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["CollPremium"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["CompPremium"].ToString() != "0")
                {
                    if (Endosos.Contains("GIC11, A4555, GIC20, GIC24"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC11, A4555, GIC20, GIC24";
                        else
                            Endosos += " GIC11, A4555, GIC20, GIC24";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["RentalReim"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Private")
                {
                    if (Endosos.Contains("GIC22"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC22";
                        else
                            Endosos += " GIC22";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["ADDPremium"].ToString() != "0" && (taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Private" || taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Commercial"))
                {
                    if (Endosos.Contains("UIP248"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", UIP248";
                        else
                            Endosos += " UIP248";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Rental")// && taskControl1.VehicleCollection.Rows[i]["IsMotorcycleScooter"].ToString() != "True")
                {
                    if (Endosos.Contains("GIC16"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC16";
                        else
                            Endosos += " GIC16";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["TaxiLossAmount"].ToString() != "0")
                {
                    if (Endosos.Contains("GIC23"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC23";
                        else
                            Endosos += " GIC23";
                    }

                    if (taskControl1.VehicleCollection.Rows[i]["Island"].ToString() == "St. Croix")
                    {
                        if (Endosos.Contains("VIPA"))
                        {
                            Endosos += "";
                        }
                        else
                        {
                            if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                                Endosos += ", VIPA";
                            else
                                Endosos += " VIPA";
                        }
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["MPPremium"].ToString() != "0")
                {
                    if (Endosos.Contains("AP328A, CA583"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", AP328A, CA583";
                        else
                            Endosos += " AP328A, CA583";
                    }
                }

            }

            Session["Endosos"] = Endosos;

            parameters[0] = new ReportParameter("Endosos", Endosos.ToString().Trim());

            parameters[1] = new ReportParameter("Username", dUserName.ToString().Trim());

            //Nuevo JNF

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/DecPage-BAP67077.rdlc");
            viewer1.LocalReport.SetParameters(parameters);
            //viewer1.LocalReport.DataSources.Add(rds1);
            viewer1.LocalReport.DataSources.Add(rds2);
            //viewer1.LocalReport.DataSources.Add(rds3);
            viewer1.LocalReport.DataSources.Add(rds4);
            viewer1.LocalReport.DataSources.Add(rds5);
            viewer1.LocalReport.Refresh();

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-Com1"; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1";
            string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-Com1" + ".pdf"; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1" + ".pdf";

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

    private List<string> ImprimirDecPageComercialP2(List<string> mergePaths, int taskControl)
    {
        try
        {
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

            int s = taskControl;

            //GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter ds1 = new GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter();
            //GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter ds2 = new GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter();
            //GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter ds3 = new GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter();
            //GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicy_VITableAdapter ds4 = new GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicy_VITableAdapter();

            //ReportDataSource rds1 = new ReportDataSource();
            //ReportDataSource rds2 = new ReportDataSource();
            // ReportDataSource rds3 = new ReportDataSource();
            //ReportDataSource rds4 = new ReportDataSource();


            //rds1 = new ReportDataSource("GetReportAutoVehiclesInfo_VI", (DataTable)ds1.GetData(s));
            //rds2 = new ReportDataSource("GetReportAutoGeneralInfo_VI", (DataTable)ds2.GetData(s));
            //rds3 = new ReportDataSource("GetReportAutoDriversInfo_VI", (DataTable)ds3.GetData(s));
            //rds4 = new ReportDataSource("GetReportAutoVehicleInfoPolicy_VI", (DataTable)ds4.GetData(s, VehicleDetailID));

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/DecPage-BAP67077page2.rdlc");
            //viewer1.LocalReport.DataSources.Add(rds1);
            //viewer1.LocalReport.DataSources.Add(rds2);
            //viewer1.LocalReport.DataSources.Add(rds3);
            //viewer1.LocalReport.DataSources.Add(rds4);
            viewer1.LocalReport.Refresh();

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-Com2";
            string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";

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

    private List<string> ImprimirDecPageComercialP3(List<string> mergePaths, int taskControl)
    {
        try
        {
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

            int s = taskControl;

            GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter ds1 = new GetReportAutoVehiclesInfods_VITableAdapters.GetReportAutoVehiclesInfo_VITableAdapter();
            GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter ds2 = new GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter();
            //GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter ds3 = new GetReportAutoDriversInfods_VITableAdapters.GetReportAutoDriversInfo_VITableAdapter();
            //GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicy_VITableAdapter ds4 = new GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicy_VITableAdapter();

            ReportDataSource rds1 = new ReportDataSource();
            ReportDataSource rds2 = new ReportDataSource();
            // ReportDataSource rds3 = new ReportDataSource();
            //ReportDataSource rds4 = new ReportDataSource();


            rds1 = new ReportDataSource("GetReportAutoVehiclesInfo_VI", (DataTable)ds1.GetData(s));
            rds2 = new ReportDataSource("GetReportAutoGeneralInfo_VI", (DataTable)ds2.GetData(s));
            //rds3 = new ReportDataSource("GetReportAutoDriversInfo_VI", (DataTable)ds3.GetData(s));
            //rds4 = new ReportDataSource("GetReportAutoVehicleInfoPolicy_VI", (DataTable)ds4.GetData(s, VehicleDetailID));

            //Nuevo JNF

            string Endosos = "";

            ReportParameter[] parameters = new ReportParameter[1];

            EPolicy.TaskControl.Autos taskControl1 = (EPolicy.TaskControl.Autos)Session["TaskControl"];


            //Verifies the endorsements for each vehicle to add it in the dec page
            for (int i = 0; taskControl1.VehicleCollection.Rows.Count > i; i++)
            {
                if (Endosos.Contains("FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n"))
                {
                    Endosos += "";
                }
                else
                {
                    Endosos += "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n"; //+ Environment.NewLine;
                }

                if (taskControl1.VehicleCollection.Rows[i]["BIPremium"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["IsMotorcycleScooter"].ToString() == "True")
                {
                    if (Endosos.Contains("CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                        else
                            Endosos += " CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                    }
                }
                else if (taskControl1.VehicleCollection.Rows[i]["BIPremium"].ToString() != "0") //&& taskControl1.VehicleCollection.Rows[i]["CollPremium"].ToString() == "0" && taskControl1.VehicleCollection.Rows[i]["CompPremium"].ToString() == "0") // liability only
                {
                    if (Endosos.Contains("CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                        else
                            Endosos += " CA0001, GIC3, GIC18, GIC21, GIC30, PP03260886";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["CollPremium"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["CompPremium"].ToString() != "0")
                {
                    if (Endosos.Contains("GIC11, A4555, GIC20, GIC24"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC11, A4555, GIC20, GIC24";
                        else
                            Endosos += " GIC11, A4555, GIC20, GIC24";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["RentalReim"].ToString() != "0" && taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Private")
                {
                    if (Endosos.Contains("GIC22"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC22";
                        else
                            Endosos += " GIC22";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["ADDPremium"].ToString() != "0" && (taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Private" || taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Commercial"))
                {
                    if (Endosos.Contains("UIP248"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", UIP248";
                        else
                            Endosos += " UIP248";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["VehicleUse"].ToString() == "Rental")// && taskControl1.VehicleCollection.Rows[i]["IsMotorcycleScooter"].ToString() != "True")
                {
                    if (Endosos.Contains("GIC16"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC16";
                        else
                            Endosos += " GIC16";
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["TaxiLossAmount"].ToString() != "0")
                {
                    if (Endosos.Contains("GIC23"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", GIC23";
                        else
                            Endosos += " GIC23";
                    }

                    if (taskControl1.VehicleCollection.Rows[i]["Island"].ToString() == "St. Croix")
                    {
                        if (Endosos.Contains("VIPA"))
                        {
                            Endosos += "";
                        }
                        else
                        {
                            if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                                Endosos += ", VIPA";
                            else
                                Endosos += " VIPA";
                        }
                    }
                }

                if (taskControl1.VehicleCollection.Rows[i]["MPPremium"].ToString() != "0")
                {
                    if (Endosos.Contains("AP328A, CA583"))
                    {
                        Endosos += "";
                    }
                    else
                    {
                        if (Endosos.ToString() != "FORMS AND ENDORSEMENTS CONTAINED IN THIS POLICY AT ITS INCEPTION: \r\n")
                            Endosos += ", AP328A, CA583";
                        else
                            Endosos += " AP328A, CA583";
                    }
                }

            }

            parameters[0] = new ReportParameter("Endosos", Endosos.ToString().Trim());

            //Nuevo JNF

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/DecPage-BAP67077page3.rdlc");
            viewer1.LocalReport.SetParameters(parameters);
            viewer1.LocalReport.DataSources.Add(rds1);
            viewer1.LocalReport.DataSources.Add(rds2);
            //viewer1.LocalReport.DataSources.Add(rds3);
            //viewer1.LocalReport.DataSources.Add(rds4);
            viewer1.LocalReport.Refresh();

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-Com3";
            string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-Com3" + ".pdf";

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
}
