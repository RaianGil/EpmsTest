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
using System.Globalization;
using System.Linq;

namespace EPolicy
{
    /// <summary>
    /// Summary description for AutoGuardServicesContractReport.
    /// </summary>
    public partial class RenewalSearch : System.Web.UI.Page
    {
        private string PolicyNumber = "", InsuredName = "", InsuredAddress1 = "", InsuredAddress2 = "";
        private string InsuredCity = "", InsuredState = "", InsuredZip = "", InsuredPlate = "", InsuredVin = "";
        private string InsuredWorkPhone = "", InsuredCellular = "", PolicyPrefix = "", ReinsAsl = "";
        private string ClientID = "", NAMECONVENTION = "";
        private int VehicleMake = 0, VehicleModel = 0;
        private string EffDate = "", ExpDate = "";
        private string Querystring = "";
        private string Current_XML = "";

        #region Web Form Designer Generated Code

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

        }

        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    Session.Abandon();
        //}

        private void InitializeComponent()
        {

        }

        #endregion Web Form Designer Generated Code

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.litPopUp.Visible = false;

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "SetWaitCursor();", true);

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            if (cp == null)
            {
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                if (!cp.IsInRole("AUTOS") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    Response.Redirect("Default.aspx?001");
                }
            }

            try
            {
                if (!this.Page.IsPostBack)
                {
                    if (Request.QueryString["Error"] != null)
                        if (Request.QueryString["Error"] != "")
                        {
                            if (Request.QueryString["Error"].ToString().Contains("INSERT"))
                            {
                                throw new Exception("Renewal Policy " + Request.QueryString["RenewalNo"].ToString() + " could not be processed" + System.Environment.NewLine + " because it is a non standard policy or is missing required fields in PPS." + " Please contact (340) 776-8050 for assistance.");
                            }
                            else if (Request.QueryString["Error"].ToString().Contains("NONSTANDARD"))
                            {
                                throw new Exception("Policy renewal could not be processed because it is a non-standard policy or is missing required information in our system. Please call our underwriting department at (340) 776-8050 for assistance.");
                            }
                            else
                            {
                                throw new Exception("Renewal Policy " + Request.QueryString["RenewalNo"].ToString() + " could not be processed." + System.Environment.NewLine + Request.QueryString["Error"].ToString() + " Please call Technical Support at (787)520-6175.");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //Se verifica si la poliza tiene mas de 45 dias, no se debe dejar renovar.
                if (GetLastPolicyFromPPS())
                    throw new Exception("This policy has more than 45 days. Please make a new policy.");

                if (GetPolicyFromPPS())
                {
                    lblRecHeader.Text = "Policy loaded successfully";
                    mpeSeleccion.Show();
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message.ToString();
                mpeSeleccion.Show();
            }
        }

        private bool GetLastPolicyFromPPS()
        {
            bool Result = false;
            //EPolicy.TaskControl.TaskControl taskControl = (EPolicy.TaskControl.TaskControl)Session["TaskControl"];
            DataTable dt = new DataTable();
            SqlConnection con = null;
            if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
            {
                try
                {
                    string connection = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();//@"Data Source=ASPIREJ;Initial Catalog=ClaimNext;User ID=sa;password=sqlserver;";
                    con = new SqlConnection(connection);
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sproc_Consume_ePPS_LastPolicy", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@PolicyID", SqlDbType.VarChar).Value = txtPolicyNo.Text.ToString().Trim();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            string expdt = dt.Rows[0]["Expire"].ToString();

                            txtPolicyNo.Text = dt.Rows[0]["PolicyID"].ToString();
                            int totaldays = int.Parse(Math.Round((DateTime.Now - DateTime.Parse(expdt)).TotalDays,0).ToString());
                            if ((DateTime.Now - DateTime.Parse(expdt)).TotalDays > 45)
                                Result = true;
                        }
                        con.Close();
                    }
                }
                catch (Exception exp)
                {
                    con.Close();
                }
            }
            return Result;
        }

        private bool GetPolicyFromPPS()
        {
            bool Return = false;
            try
            {
                NAMECONVENTION = DateTime.Now.ToString("MM.dd.yyyy_hhmmss");

                Session.Clear();
                //Customer.Customer customer = (Customer.Customer)Session["Customer"];

                 EPolicy.TaskControl.Autos taskControl =  taskControl = new EPolicy.TaskControl.Autos(true);
                
                XmlDocument XmlDoc = new XmlDocument();

                if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                {
                    System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();

                    cn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "exec sproc_GetPPSPolicyXML " +
                        "@LossDate= '" + String.Empty + "', " +
                        "@Plate='" + String.Empty + "', " +
                        "@VIN='" + String.Empty + "'," +
                        "@PolicyID= '" + txtPolicyNo.Text.ToString().Trim() + "', @xmldata=@xmldata output";
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@xmldata", SqlDbType.Xml).Direction = ParameterDirection.Output;

                    int c = cmd.ExecuteNonQuery();
                    string f = cmd.Parameters[0].Value.ToString();

                    if (f.Trim() == "")
                    {
                        cn.Close();
                        throw new Exception("Policy not found in PPS");
                    }

                    XmlDoc.LoadXml(f);

                    XmlDoc.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + "RenewalXml\\" + NAMECONVENTION + "RenewalPPS" + ".xml");
                    Current_XML = f;
                    cn.Close();

                }
                else
                {
                    XmlDoc.Load("C:\\inetpub\\wwwroot\\09.08.2020_022414RenewalPPS.xml"); ////C:\\inetpub\\wwwroot\\ClaimNextPR\\Reports\\Xml2.xml");
                    Current_XML = System.IO.File.ReadAllText("C:\\inetpub\\wwwroot\\09.08.2020_022414RenewalPPS.xml");
                }

                string CanDate = "";
                bool FilledName = false, FilledAddress = false, FilledAsl = false;

                #region Xml

                XmlNodeList XmlBase = XmlDoc.GetElementsByTagName("Policy");

                foreach (XmlNode XmlPolicyBase in XmlBase)
                {
                    taskControl.Mode = 1; //ADD
                    taskControl.isQuote = true;

                    taskControl.PolicyNo = XmlPolicyBase["PolicyID"].InnerText.Replace("PAP", "").Replace("BAP", "");



                    taskControl.EffectiveDate = DateTime.Parse(XmlPolicyBase["Incept"].InnerText).ToShortDateString();
                    taskControl.ExpirationDate = DateTime.Parse(XmlPolicyBase["Expire"].InnerText).ToShortDateString();

                    PolicyNumber = XmlPolicyBase["PolicyID"].InnerText;
                    txtPolicyNo.Text = PolicyNumber;
                    PolicyPrefix = PolicyNumber.Substring(0, 3);
                    taskControl.PolicyType = PolicyNumber.Substring(0, 3);
                    EffDate = DateTime.Parse(XmlPolicyBase["Incept"].InnerText).ToShortDateString();
                    ExpDate = DateTime.Parse(XmlPolicyBase["Expire"].InnerText).ToShortDateString();

                    if (XmlPolicyBase["CanDate"].InnerText != "")
                        taskControl.CancellationDate = DateTime.Parse(XmlPolicyBase["CanDate"].InnerText).ToShortDateString();

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
                                                        taskControl.Customer.FirstName = GreatGrandElements["FirstName"].InnerText;
                                                        taskControl.Customer.Initial = GreatGrandElements["Middle"].InnerText == "" ? "" : GreatGrandElements["Middle"].InnerText;
                                                        taskControl.Customer.LastName1 = GreatGrandElements["LastName"].InnerText;
                                                        //+
                                                        //((GreatGrandElements["Middle"].InnerText != "") ? " " + GreatGrandElements["Middle"].InnerText : "") + " " +
                                                        //GreatGrandElements["LastName"].InnerText

                                                        taskControl.Customer.Birthday = GreatGrandElements["Dob"].InnerText == "" ? "" : DateTime.Parse(GreatGrandElements["Dob"].InnerText).ToShortDateString();

                                                        if (GrandChildElements["Sex"] != null)
                                                        {
                                                            taskControl.Customer.Sex = GrandChildElements["Sex"].InnerText == null ? "" : GrandChildElements["Sex"].InnerText;
                                                        }

                                                        if (GrandChildElements["Marital"] != null)
                                                        {
                                                            taskControl.Customer.MaritalStatus = GrandChildElements["Marital"].InnerText == "S" ? 1 : 2;
                                                        }
                                                        if (GrandChildElements["License"] != null)
                                                        {
                                                            taskControl.Customer.Licence = GreatGrandElements["License"].InnerText;
                                                        }
                                                        //taskControl.Customer.State = GreatGrandElements["State"].InnerText;

                                                        InsuredName = taskControl.Customer.FirstName;
                                                        InsuredState = taskControl.Customer.State;
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
                                    //if ((ChildsElement["LicPlate"].InnerText == txtPlateVerification.Text.Trim().ToUpper()) ||
                                    //    (ChildsElement["Vin"].InnerText == txtVINVerification.Text.Trim().ToUpper()))
                                    //{
                                    //txtVINVerification.Text 
                                    InsuredVin = ChildsElement["Vin"].InnerText;
                                    //txtPlateVerification.Text 
                                    InsuredPlate = ChildsElement["LicPlate"].InnerText;

                                    //txtVIN.Text = txtVINVerification.Text.Trim();
                                    //txtPlate.Text = txtPlateVerification.Text.Trim().ToUpper();

                                    InsuredVin = "";//txtVINVerification.Text;
                                    InsuredPlate = "";//txtPlateVerification.Text;

                                    if (ChildsElement.HasChildNodes)
                                    {
                                        foreach (XmlElement GrandChilds in ChildsElement)
                                        {
                                            if (GrandChilds.Name == "PhysVehicleTable")
                                            {
                                                foreach (XmlElement GrandChildsElements in GrandChilds)
                                                {
                                                    //ddlVehicleMake.SelectedIndex = ddlVehicleMake.Items.IndexOf(ddlVehicleMake.Items.FindByText((GrandChildsElements["Make"].InnerText.Trim()).ToString()));

                                                    //if (ddlVehicleMake.SelectedItem.Value != "")
                                                    //{
                                                    //if (int.Parse(ddlVehicleMake.SelectedItem.Value) > 0)
                                                    //FillModelDDListByPPS(ddlVehicleMake.SelectedItem.Value.ToString());
                                                    //}

                                                    //ddlVehicleModel.SelectedIndex = ddlVehicleModel.Items.IndexOf(ddlVehicleModel.Items.FindByText((GrandChildsElements["Model"].InnerText.TrimEnd()).ToString()));
                                                    //ddlVehicleYear.SelectedIndex = ddlVehicleYear.Items.IndexOf(ddlVehicleYear.Items.FindByText((GrandChildsElements["MYear"].InnerText.TrimEnd()).ToString())); 
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
                                    // }
                                }
                            }

                            else if (Childs.Name == "ClientTable")
                            {
                                foreach (XmlElement ChildsElement in Childs)
                                {
                                    if (!(FilledAddress))   //Solo leerá los campos una vez, tomando los primeros que lea
                                    {
                                        taskControl.Customer.Address1 = ChildsElement["Maddr1"].InnerText;
                                        taskControl.Customer.Address2 = ChildsElement["Maddr2"].InnerText;

                                        taskControl.Customer.City = ChildsElement["Mcity"].InnerText; //txtInsuredCity.Text =
                                        taskControl.Customer.State = ChildsElement["Mstate"].InnerText;
                                        taskControl.Customer.ZipCode = ChildsElement["Mzip"].InnerText;

                                        taskControl.Customer.AddressPhysical1 = ChildsElement["Raddr1"].InnerText;
                                        taskControl.Customer.AddressPhysical2 = ChildsElement["Raddr2"].InnerText;

                                        taskControl.Customer.CityPhysical = ChildsElement["Rcity"].InnerText; //txtInsuredCity.Text =
                                        taskControl.Customer.StatePhysical = ChildsElement["Rstate"].InnerText;
                                        taskControl.Customer.ZipPhysical = ChildsElement["Rzip"].InnerText;

                                        taskControl.Customer.JobPhone = ChildsElement["Wphone"].InnerText;
                                        taskControl.Customer.Cellular = ChildsElement["Cphone"].InnerText;

                                        InsuredAddress1 = "";// txtInsuredAddrs1.Text;
                                        InsuredAddress2 = "";// txtAddrs2.Text;
                                        InsuredZip = "";// txtZipCode.Text;
                                        InsuredWorkPhone = "";//txtWorkPhone.Text;
                                        InsuredCellular = "";// txtCellular.Text;

                                        FilledAddress = true;
                                    }
                                }
                            }
                        }
                    }
                    ///Todas las variables Insured mencionadas existen así para la ocasión en que llame el reclamante
                    ///La información del asegurado no se pierda, si no que siga hacia adelante en el proceso. 

                    if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")) && (1 == 0))
                    {
                        DataTable DtClaims = new DataTable();

                        DtClaims = GetFlagFromClaimNext(txtPolicyNo.Text.ToString().Trim(), EffDate, ExpDate);
                        if (DtClaims != null)
                        {
                            if (DtClaims.Rows.Count > 0)
                            {
                                lblRecHeader.Text = ("Renewal Policy " + txtPolicyNo.Text.ToString().Trim() + " has claims and can not be processed." + System.Environment.NewLine + " Please call Technical Support at (787)520-6175.");
                                mpeSeleccion.Show();
                                Return = false;
                            }
                            else
                            {
                                lblRecHeader.Text = "Policy loaded successfully";
                                mpeSeleccion.Show();
                                Return = true;
                            }
                        }
                        else
                        {
                            lblRecHeader.Text = "Policy loaded successfully";
                            mpeSeleccion.Show();
                            Return = true;
                        }
                    }
                    else
                    {
                        lblRecHeader.Text = "Policy loaded successfully";
                        mpeSeleccion.Show();
                        Return = true;
                    }
                }
                #endregion

                return Return;
            }
            catch (Exception exp)
            {
                Return = false;
                LogError(exp,"2");                               

                return Return;
            }
        }

        //protected void AddVehicle()
        //{
        //    try
        //    {
        //        EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
        //        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        //        int userID = 0;
        //        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

        //        string BI = "0.00";
        //        string PD = "0.00";
        //        string MP = "0.00";
        //        string Coll = "0.00";
        //        string Comp = "0.00";
        //        string Mot = "0.00";
        //        string Add = "0.00";
        //        string TotPrem = "0.00";
        //        string RentReim = "0.00";
        //        string TaxiLoss = "0.00";
        //        string tempVehicleID = "0";

        //        //SetCompCollDeductible();
        //        int MaxVehicleDetailID = 0;
        //        int VehicleRowNumber = 0;

        //        DataTable dt = null;
        //        DataRow myRow = taskControl.VehicleCollection.NewRow();

        //        myRow["VehicleDetailID"] = MaxVehicleDetailID + 1;
        //        myRow["TaskControlID"] = "0";//
        //        myRow["VehicleYear"] = "0";//ddlVehicleYear.SelectedItem.Text.Trim();
        //        myRow["VehicleMake"] = "0";//ddlVehicleMake.SelectedItem.Text.Trim().ToUpper();
        //        myRow["VehicleModel"] = "0";//ddlVehicleModel.SelectedItem.Text.Trim().ToUpper();
        //        myRow["BankDesc"] = "0";//ddlBankList.SelectedItem.Text.Trim().ToUpper();
        //        myRow["VIN"] = "0";//TxtVINNo.Text.Trim().ToUpper();
        //        myRow["LicensePlateNo"] = "0";//TxtLicensePlateNo.Text.Trim().ToUpper();

        //        myRow["Island"] = "0";//ddlIsland.SelectedItem.Text.ToString().Trim();
        //        myRow["Status"] = "0";//ddlStatus.SelectedItem.Text.ToString().Trim();
        //        myRow["VehicleValue"] = "0";//double.Parse(TxtVehicleValue.Text.ToString().Trim(), System.Globalization.NumberStyles.Currency);
        //        myRow["VehicleUse"] = "0";//ddlVehicleUse.SelectedItem.Text.ToString().Trim();

        //        //if (BtnAddVehicle.Text != "Save Vehicle") //Session["VehicleRowNumber"] == null && 
        //        // {
        //        if (taskControl.VehicleCollection.Rows.Count == 0)
        //            myRow["VehicleRowNumber"] = 1;
        //        else
        //            myRow["VehicleRowNumber"] = taskControl.VehicleCollection.Rows.Count + 1;
        //        //}
        //        //else
        //        //{
        //        //myRow["VehicleRowNumber"] = Session["VehicleRowNumber"].ToString();
        //        //}

        //        //if (BtnAddVehicle.Text == "Save Vehicle")
        //        //{
        //        //Si la el asegurado principal tiene 16 o 17 años la cubierta rental reim. no aplica
        //        //  if (TxtAge.Text.Trim() == "16" || TxtAge.Text.Trim() == "17")
        //        //{
        //        //  lblPrimGrade.Visible = true;
        //        //        txtPrimGrade.Visible = true;
        //        //        txtPrimGrade.Enabled = true;
        //        //        lblRentalReim.Visible = true;
        //        //        radRentalReimCov.Visible = true;
        //        //        radRentalReimCov.Enabled = false;
        //        //        radRentalReimCov.SelectedIndex = 1;
        //        //        //txtPrimGrade.Text = "";
        //        //    }
        //        //    if (radRentalReimCov.SelectedItem.Text == "Yes")
        //        //    {

        //        //    }
        //        //    else
        //        //    {
        //        //        RentReim = "0.00";
        //        //    }
        //        //}

        //        //if (TxtPassengerNo.Visible == true)
        //        //{
        //        myRow["PassengersNo"] = "0";//TxtPassengerNo.Text.ToString().Trim();
        //        //}
        //        //else
        //        //{
        //        myRow["PassengersNo"] = 0;
        //        //}

        //        //if (radGenTons.SelectedItem.Text == "Yes")
        //        //{
        //        myRow["Over2Tons"] = true;
        //        //}
        //        //else
        //        //{
        //        myRow["Over2Tons"] = false;
        //        //}

        //        //if (radGenMedicalPayment.SelectedItem.Text == "Yes")
        //        //{
        //        //    myRow["MedicalPayment"] = true;
        //        //}
        //        //else
        //        //{
        //        myRow["MedicalPayment"] = false;
        //        //}

        //        //if (radGenMotoristCoverage.SelectedItem.Text == "Yes")
        //        //{
        //        //    myRow["MotoristCoverage"] = true;
        //        //}
        //        //else
        //        //{
        //        myRow["MotoristCoverage"] = false;
        //        //}

        //        //if (radRentalReimCov.SelectedItem.Text == "Yes")
        //        //{
        //        //    myRow["RentalReimCoverage"] = true;
        //        //}
        //        //else
        //        //{
        //        myRow["RentalReimCoverage"] = false;
        //        //}

        //        //if (radADD.SelectedItem.Text == "Yes")
        //        //{
        //        //    myRow["DeathDismembermentCoverage"] = true;
        //        //}
        //        //else
        //        //{
        //        myRow["DeathDismembermentCoverage"] = false;
        //        //}

        //        //if (radMotorcycle.SelectedItem.Text == "Yes")
        //        //{
        //        myRow["IsMotorcycleScooter"] = true;
        //        //}
        //        //else
        //        //{
        //        myRow["IsMotorcycleScooter"] = false;
        //        //}

        //        //if (radTaxiDriverloss.SelectedItem.Text == "Yes")
        //        //{
        //        //    //myRow["TaxiLossAmount"] = true;
        //        //  myRow["TaxiLossAmount"] = Math.Round(double.Parse(TaxiLoss), 0).ToString();
        //        //}
        //        //else
        //        //{
        //        //    // myRow["TaxiLossAmount"] = false;

        //        myRow["TaxiLossAmount"] = 0.0;
        //        //}


        //        myRow["PDLimit"] = "0";//TxtPDLimit.Text.ToString().Trim();
        //        myRow["ComprehensiveDedu"] = "0";//ddlDedComp.SelectedItem.Value.ToString(); //TxtDeductibleComprehensive.Text.ToString().Trim();
        //        myRow["CollisionDedu"] = "0";//ddlDedColl.SelectedItem.Value.ToString(); //TxtDeductibleCollision.Text.ToString().Trim();
        //        myRow["BIPPLimit"] = "0";//TxtBILimitpp.Text.ToString().Trim();
        //        myRow["BIPOLimit"] = "0";//TxtBILimitpo.Text.ToString().Trim().Replace("&amp;", "").Replace("amp;", "").Replace("nbsp;", "").Replace("&", "").Replace("&nbsp;", "");
        //        myRow["MPLimit"] = "0";//txtMPLimit.Text.ToString().Trim();

        //        myRow["CompPremium"] = "0";//Math.Round(double.Parse(Comp), 0).ToString(); //txtComp.Text.ToString().Trim();
        //        myRow["CollPremium"] = "0";//Math.Round(double.Parse(Coll), 0).ToString(); //txtCollision.Text.ToString().Trim();
        //        myRow["BIPremium"] = "0";//Math.Round(double.Parse(BI), 0).ToString();  //txtBIRate.Text.ToString().Trim();
        //        myRow["PDPremium"] = "0";//Math.Round(double.Parse(PD), 0).ToString();  //txtPDRate.Text.ToString().Trim();

        //        //if (radGenMedicalPayment.SelectedItem.Text == "Yes")
        //        //{
        //        //    myRow["MPPremium"] = Math.Round(double.Parse(MP), 0).ToString(); //txtMPRate.Text.ToString().Trim();
        //        //}
        //        //else
        //        //{
        //        myRow["MPPremium"] = 0.0; //txtMPRate.Text.ToString().Trim();
        //        //}

        //        myRow["MotoristPremium"] = "0";//Math.Round(double.Parse(Mot), 0).ToString();  //txtMotorist.Text.ToString().Trim();
        //        myRow["ADDPremium"] = "0";//Math.Round(double.Parse(Add), 0).ToString(); //txtADD.Text.ToString().Trim();
        //        myRow["RentalReim"] = "0";//Math.Round(double.Parse(RentReim), 0).ToString();  //txtADD.Text.ToString().Trim();
        //        myRow["TaxiLossAmount"] = "0";//Math.Round(double.Parse(TaxiLoss), 0).ToString();

        //        TotPrem = (double.Parse(Comp) + double.Parse(Coll) +
        //            double.Parse(BI) + double.Parse(PD) +
        //            double.Parse(MP) + double.Parse(Mot) +
        //            double.Parse(Add) + double.Parse(RentReim) +
        //            double.Parse(TaxiLoss)).ToString();

        //        myRow["ViolationSurcharge"] = 0.0;
        //        myRow["UnderAgeSurcharge"] = 0.0;

        //        myRow["TotalPremium"] = "0";//Math.Round(double.Parse(TotPrem), 0).ToString();  //txtTotalPremiumVehicle.Text.ToString().Trim();
        //        myRow["OtherSurchargePct"] = "0";//double.Parse(txtOtherSurcharge.Text.ToString());
        //        myRow["OtherSurcharge"] = "0";//(Math.Round((double.Parse(txtOtherSurcharge.Text.ToString().Trim()) / 100) * Math.Round(double.Parse(TotPrem), 0), 0)).ToString();


        //        taskControl.VehicleCollection.Rows.Add(myRow);
        //        taskControl.VehicleCollection.AcceptChanges();

        //        DataView dv = taskControl.VehicleCollection.DefaultView;
        //        dv.Sort = "VehicleRowNumber";
        //        taskControl.VehicleCollection = dv.ToTable();
        //        taskControl.VehicleCollection.AcceptChanges();

        //        dt = taskControl.VehicleCollection;

        //        //for (int i = 0; i < GridVehicle.Columns.Count; i++)
        //        //{
        //        //    GridVehicle.Columns[i].Visible = true;
        //        //}

        //        ////this.GridDrivers.CurrentPageIndex = 0;
        //        //this.GridVehicle.Visible = true;
        //        //this.GridVehicle.DataSource = dt;
        //        //this.GridVehicle.DataBind();
        //        //this.GridVehicle.Visible = true;


        //        //for (int i = 6; i < 40; i++)
        //        //{
        //        //    GridVehicle.Columns[i].Visible = false;
        //        //}
        //        //GridVehicle.Columns[33].Visible = false;
        //        //GridVehicle.Columns[38].Visible = false;
        //        //GridVehicle.Columns[40].Visible = false;
        //        //GridVehicle.Columns[41].Visible = false;
        //        //GridVehicle.Columns[6].Visible = false;
        //        //GridVehicle.Columns[42].Visible = false;
        //        //GridVehicle.Columns[44].Visible = false;

        //        //GridVehicle.Columns[39].Visible = true;
        //        //GridVehicle.Columns[26].Visible = true;
        //        //GridVehicle.Columns[27].Visible = true;
        //        //GridVehicle.Columns[28].Visible = true;
        //        //GridVehicle.Columns[29].Visible = true;
        //        //GridVehicle.Columns[30].Visible = true;
        //        //GridVehicle.Columns[31].Visible = true;
        //        //GridVehicle.Columns[32].Visible = true;
        //        //GridVehicle.Columns[34].Visible = true;



        //        //CalculatePremium();



        //        //if (txtTotalPremiumVehicle.Text.ToString().Trim() == "")
        //        //    txtTotalPremiumVehicle.Text = "0.00";


        //        //if (BtnAddVehicle.Text == "Save Vehicle")
        //        //{
        //        //    TxtBirthDate_TextChanged(String.Empty, EventArgs.Empty);
        //        //}

        //        //this.BtnAddVehicle.Text = "Add Vehicle";
        //        //lblModifyVehicle.Visible = false;
        //        //GridVehicle.BorderColor = Color.Black;
        //        //GridDrivers.BorderColor = Color.Black;


        //        //// se puso en comentario ya que Maureen no queria que los valores se fueran a su estado "basic", se dejo la informacion que ya estaba entrada (limites, cubiertas, etc)

        //        //ddlVehicleYear.SelectedIndex = 0;
        //        //ddlVehicleMake.SelectedIndex = 0;
        //        //ddlVehicleModel.SelectedIndex = 0;
        //        //ddlBankList.SelectedIndex = 0;
        //        //TxtVINNo.Text = "";
        //        //TxtLicensePlateNo.Text = "";

        //        //ddlVehicleUse.SelectedIndex = 0;
        //        //ddlIsland.SelectedIndex = 0;

        //        //TxtVehicleValue.Text = "";


        //        //if (TxtPassengerNo.Visible == true || LblPassengersNo.Visible == true)
        //        //{
        //        //    TxtPassengerNo.Visible = false;
        //        //    LblPassengersNo.Visible = false;

        //        //    if (TxtPassengerNo.Text != "0")
        //        //        TxtPassengerNo.Text = "0";

        //        //    lblTaxiDriverloss.Visible = false;
        //        //    radTaxiDriverloss.Visible = false;

        //        //}

        //        //if (radRentalReimCov.Visible == false)
        //        //{
        //        //    radRentalReimCov.Visible = true;
        //        //    lblRentalReim.Visible = true;
        //        //}

        //        //if (radMotorcycle.Enabled)
        //        //{
        //        //    radMotorcycle.SelectedIndex = 1;
        //        //    IsBusinessCheckedChanged();
        //        //}


        //        //TxtVehicleValue.Enabled = true;

        //        //TxtVehicleID.Text = "";

        //        //ValidateBI();
        //        //EnableDisableBusinessAuto();

        //        //}
        //    }
        //    catch (Exception exp)
        //    {
        //        lblRecHeader.Text = exp.Message;
        //        mpeSeleccion.Show();
        //    }
        //}

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Search.aspx");
        }
		
		private int GetPolicyFromPPSFromEPPS(bool IsAutoHighLimit)
        {
            int TaskControlID = 0;
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);               

                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("PolicyNo ", SqlDbType.VarChar, 50, txtPolicyNo.Text.ToString().Trim(), ref cookItems);
                XmlDocument xmlDoc;

                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                DataTable dt = exec.GetQuery("GetPolicyByPolicyNoForRenew", xmlDoc);

                if (dt.Rows.Count > 0)
                {
                    if (IsAutoHighLimit)
                    {
                        TaskControl.TaskControl taskControl = TaskControl.TaskControl.GetTaskControlByTaskControlID(int.Parse(dt.Rows[0]["TaskControlID"].ToString()), userID);
                        TaskControlID = RenewalFromEPPSAutoHighLimit((TaskControl.AutoHighLimit)taskControl);
                    }
                    else
                    {
                        TaskControl.TaskControl taskControl = TaskControl.TaskControl.GetTaskControlByTaskControlID(int.Parse(dt.Rows[0]["TaskControlID"].ToString()), userID);
                        TaskControlID = RenewalFromEPPS((TaskControl.Autos)taskControl);
                    }
                }
            }
            catch (Exception exc)
            {
                TaskControlID = 0;
				  LogError(exc, "1");
            }
            return TaskControlID;
        }
		
        protected void btnNewQuote_Click(object sender, EventArgs e)
        {						
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            try
            {
                if (GetLastPolicyFromPPS())
                    throw new Exception("This policy has more than 45 days. Please make a new policy.");

                //Se busca poliza en ePPS para su renovacion y se evalua si tiene END o este cancelada en PPS.
                int TaskControlID = 0;
                string destinationURL = "";

                if (IsAutoHighLimit())
                {
                    TaskControlID = GetPolicyFromPPSFromEPPS(true);
					
					 if (TaskControlID != 0)	
					{
                        if (!cp.IsInRole("AUTO HIGH LIMIT"))
                            throw new Exception("Policy renewal could not be processed because it is a non-standard policy or is missing required information in our system. Please call our underwriting department at (340) 776-8050 for assistance.");
                        else
                            Response.Redirect("AutoHighLimit.aspx" + "?" + TaskControlID.ToString() + "&isRenewFromEPPS=" + txtPolicyNo.Text.Trim(), false);
                    }
                }
                else
                {
                    TaskControlID = GetPolicyFromPPSFromEPPS(false);
					
					 if (TaskControlID != 0)
						Response.Redirect("Autos.aspx" + "?" + TaskControlID.ToString() + "&isRenewFromEPPS=" + txtPolicyNo.Text.Trim(), false);
                }

                if (TaskControlID == 0)
                {
                    if (GetPolicyFromPPS())
                    {
                        if (IsAutoHighLimit())
                        {
                            if (!cp.IsInRole("AUTO HIGH LIMIT"))
                                throw new Exception("Policy renewal could not be processed because it is a non-standard policy or is missing required information in our system. Please call our underwriting department at (340) 776-8050 for assistance.");
                            else
                            {
                                Session.Clear();
                                TaskControl.AutoHighLimit taskControl = new TaskControl.AutoHighLimit(true);
                                Session.Add("TaskControl", taskControl);

                                Querystring = Current_XML.Replace(System.Environment.NewLine, String.Empty).Replace("&amp;", "").Replace("%", "");
                                destinationURL = "AutoHighLimit.aspx" + "?XML=";
                                Querystring = txtPolicyNo.Text.Trim();
                                Response.Redirect(destinationURL + Querystring, false);//"Autos.aspx" + "?XML=" + Querystring
                            }
                        }
                        else
                        {

                            Querystring = Current_XML.Replace(System.Environment.NewLine, String.Empty).Replace("&amp;", "").Replace("%", "");//.Replace("&amp;", "").Replace("amp;", "").Replace("nbsp;", "").Replace("&", "").Replace("&nbsp;", "")
                            destinationURL = "Autos.aspx" + "?XML="; //+ Querystring;
                            Querystring = txtPolicyNo.Text.Trim();//System.Web.HttpUtility.UrlEncode(Querystring);
                            //HttpContext.Current.Server.UrlEncode(destinationURL);
                            Response.Redirect(destinationURL + Querystring, false);//"Autos.aspx" + "?XML=" + Querystring
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogError(exp,"3");
                lblRecHeader.Text = exp.Message.ToString();
                mpeSeleccion.Show();
            }
        }

        private bool IsAutoHighLimit()
        {
            EPolicy.TaskControl.TaskControl taskControl = (EPolicy.TaskControl.TaskControl)Session["TaskControl"];
            DataTable dt = new DataTable();
            bool Result = false;
            SqlConnection con = null;

            try
            {
                string connection = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                con = new SqlConnection(connection);
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sproc_Consume_ePPS_IsAutoHightLimit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@PolicyID", SqlDbType.VarChar).Value = PolicyNumber.ToString().Trim() == "" ? txtPolicyNo.Text.ToString().Trim() : txtPolicyNo.Text.ToString().Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        Result = true; // Is Auto High Limit
                    }

                    con.Close();
                }
            }
            catch (Exception exc)
            {
                con.Close();
            }

            return Result;
        }

        private DataTable GetFlagFromClaimNext(string PolicyNumber, string EffectiveDate, string ExpirationDate)
        {
            EPolicy.TaskControl.TaskControl taskControl = (EPolicy.TaskControl.TaskControl)Session["TaskControl"];
            DataTable dt = new DataTable();

            try
            {
                string connection = System.Configuration.ConfigurationManager.AppSettings["ConnStrClaimNext"].ToString();//@"Data Source=ASPIREJ;Initial Catalog=ClaimNext;User ID=sa;password=sqlserver;";
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetFlagFromClaimNext", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@PolicyNumber", SqlDbType.VarChar).Value = PolicyNumber.ToString().Trim() == "" ? PolicyNumber.ToString().Trim() : PolicyNumber.ToString().Trim();
                    cmd.Parameters.Add("@EffectiveDate", SqlDbType.VarChar).Value = EffectiveDate.ToString().Trim() == "" ? EffectiveDate.ToString().Trim() : EffectiveDate.ToString().Trim();
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = ExpirationDate.ToString().Trim() == "" ? ExpirationDate.ToString().Trim() : ExpirationDate.ToString().Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        //dt.Rows[0]["Flag"].ToString();
                    }

                    con.Close();
                }
            }
            catch (Exception exp)
            {
            }

            return dt;
        }

    protected void  btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            GetPolicyFromPPS();

            string strFullPath = Server.MapPath("~/temp.xml");
            string strContents = Current_XML;
            System.IO.StreamReader objReader = default(System.IO.StreamReader);
            objReader = new System.IO.StreamReader(strFullPath);
            strContents = objReader.ReadToEnd();
            objReader.Close();

            string attachment = "attachment; filename=test.xml";
            Response.ClearContent();
            Response.ContentType = "application/xml";
            Response.AddHeader("content-disposition", attachment);
            Response.Write(strContents);
            Response.Flush();
            Response.End();   

            //Response.Clear();
            //string fileName = String.Format("data-{0}.csv", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            //Response.ContentType = "text/xml";
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.Write(Current_XML);
            //Response.Flush(); // Sends all currently buffered output to the client.
            //HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            //HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            //Response.End();

        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message.ToString();
            mpeSeleccion.Show();
        }
    }

    private void LogError(Exception exp, string msg)
    {
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
		 message += Environment.NewLine;
        message += string.Format("Message: {0}", msg);
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
	
	 private int RenewalFromEPPSAutoHighLimit(TaskControl.AutoHighLimit taskControl )
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                    
            string Message;

            try
            {
                if (!IsLocalHost())
                {
                    if (GetPPSHasEndorsementAutoVI(out Message))
                    {
                        if (Message != "")
                        {
                            return 0;
                        }
                    }

                    if (GetPPSCancelledPolicy(out Message))
                    {
                        if (Message != "")
                        {
                            return 0;
                        }
                    }
                }

                TaskControl.AutoHighLimit taskControlQuote =  new EPolicy.TaskControl.AutoHighLimit(false);

                int tcID = taskControl.TaskControlID;

                taskControlQuote.Prospect.FirstName = taskControl.Customer.FirstName.Trim().ToUpper();
                taskControlQuote.Prospect.LastName1 = taskControl.Customer.LastName1.Trim().ToUpper();
                taskControlQuote.Prospect.LastName2 = taskControl.Customer.LastName2.Trim().ToUpper();
                taskControlQuote.Prospect.HomePhone = taskControl.Customer.HomePhone.Trim().ToUpper();
                taskControlQuote.Prospect.WorkPhone = taskControl.Customer.JobPhone.Trim().ToUpper();
                taskControlQuote.Prospect.Cellular = taskControl.Customer.Cellular.Trim().ToUpper();
                taskControlQuote.Prospect.Email = taskControl.Customer.Email.Trim().ToUpper();

                //Para aplicar el ultimo endoso, sino a la poliza original
                DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(tcID);
                if (endososList.Rows.Count == 0)
                {
                    taskControlQuote = taskControl;
                    taskControlQuote.Mode = 1; //ADD
                    taskControlQuote.TaskControlID = 0;
                    taskControlQuote.isQuote = true;//.IsPolicy = false;
                    taskControlQuote.IsEndorsement = false;
                    taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Auto High Limit Quote"));
                    taskControlQuote.DriversCollection = taskControl.DriversCollection.Copy();
                    taskControlQuote.VehicleCollection = taskControl.VehicleCollection.Copy();
                }
                else
                {
                    //Aplica al Ultimo endoso
                    bool isExistEndo = false;
                    EPolicy.TaskControl.AutoHighLimit taskControlEndo = null; ;
                    for (int s = 1; s <= endososList.Rows.Count; s++)
                    {
                        if ((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"] != 0)
                        {
                            TaskControl.TaskControl tcTemp = EPolicy.TaskControl.Autos.GetAutos((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], true);
                            taskControlEndo = (TaskControl.AutoHighLimit) tcTemp; // EPolicy.TaskControl.Autos.GetAutos((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], true);
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
                        taskControlQuote.IsEndorsement = false;
                        taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Auto High Limit Quote"));
                        taskControlQuote.DriversCollection = taskControl.DriversCollection.Copy();
                        taskControlQuote.VehicleCollection = taskControl.VehicleCollection.Copy();
                    }
                    else
                    {
                        taskControlQuote = taskControlEndo;
                        taskControlQuote.Mode = 1; //ADD
                        taskControlQuote.TaskControlID = 0;
                        taskControlQuote.isQuote = true;//.IsPolicy = false;
                        taskControlQuote.IsEndorsement = false;
                        taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Auto High Limit Quote"));

                        taskControlQuote.Prospect.FirstName = taskControl.Customer.FirstName.Trim().ToUpper();
                        taskControlQuote.Prospect.LastName1 = taskControl.Customer.LastName1.Trim().ToUpper();
                        taskControlQuote.Prospect.LastName2 = taskControl.Customer.LastName2.Trim().ToUpper();
                        taskControlQuote.Prospect.HomePhone = taskControl.Customer.HomePhone.Trim().ToUpper();
                        taskControlQuote.Prospect.WorkPhone = taskControl.Customer.JobPhone.Trim().ToUpper();
                        taskControlQuote.Prospect.Cellular = taskControl.Customer.Cellular.Trim().ToUpper();
                        taskControlQuote.Prospect.Email = taskControl.Customer.Email.Trim().ToUpper();

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
                        taskControlQuote.TotalDiscounts = taskControl.TotalDiscounts;
                        taskControlQuote.TotalDiscountsPct = taskControl.TotalDiscountsPct;
                        taskControlQuote.PolicyClassID = taskControl.PolicyClassID;

                        taskControlQuote.DriversCollection = taskControlEndo.DriversCollection.Copy();
                        taskControlQuote.VehicleCollection = taskControlEndo.VehicleCollection.Copy();
                    }
                }

                taskControlQuote.Mode = 1; //ADD
                taskControlQuote.Term = taskControl.Term;
                taskControlQuote.TCIDQuotes = tcID;//taskControlQuote.TaskControlID;
                taskControlQuote.CustomerCollection = "0";
                
				 if (DateTime.Parse(taskControl.ExpirationDate).AddYears(1) < DateTime.Today)
                {
                    taskControlQuote.EffectiveDate = DateTime.Now.ToShortDateString();
                    taskControlQuote.ExpirationDate = DateTime.Now.AddYears(1).ToShortDateString();
                    taskControlQuote.EffectiveDateApp = DateTime.Now.ToShortDateString();
                    taskControlQuote.ExpirationDateApp = DateTime.Now.AddYears(1).ToShortDateString();
                }
                else
                {
                    taskControlQuote.EffectiveDate = DateTime.Parse(taskControl.EffectiveDate).AddYears(1).ToShortDateString();
                    taskControlQuote.ExpirationDate = DateTime.Parse(taskControl.ExpirationDate).AddYears(1).ToShortDateString();
                    taskControlQuote.EffectiveDateApp = DateTime.Parse(taskControl.EffectiveDate).AddYears(1).ToShortDateString();
                    taskControlQuote.ExpirationDateApp = DateTime.Parse(taskControl.ExpirationDate).AddYears(1).ToShortDateString();
                }
				
                taskControlQuote.Suffix = DateTime.Parse(taskControl.EffectiveDate.ToString()).Year.ToString().Substring(2, 2); //"0".ToString() + msufijo.ToString();
                taskControlQuote.RenewalNo = txtPolicyNo.Text.Trim(); // taskControl.PolicyType.ToString().Trim() + int.Parse(taskControl.PolicyNo.ToString().Trim()).ToString() + "-" + (int.Parse(taskControlQuote.Suffix.ToString().Trim()) - 1).ToString();
                
				//Calculate Customers Age
                TimeSpan ts = DateTime.Parse(DateTime.Now.ToShortDateString()) - DateTime.Parse(taskControl.Customer.Birthday.ToString());

                // Difference in days.
                double totdays = double.Parse(ts.Days.ToString());
                double tot = 0;

                if (totdays < 0)
                {
                    totdays = 0;
                }
                else
                {
                    tot = Math.Floor(totdays / 365);
                    taskControlQuote.Customer.Age = tot.ToString().Trim();
                }

                //Driver and Add Insured
                if (taskControlQuote.DriversCollection.Rows.Count > 0)
                {
                    for (int d = 0; d < taskControlQuote.DriversCollection.Rows.Count; d++)
                    {
                        TimeSpan ts2 = DateTime.Parse(DateTime.Now.ToShortDateString()) - DateTime.Parse(taskControlQuote.DriversCollection.Rows[d]["DriverDateOfBirth"].ToString());

                        // Difference in days.
                        double totdays2 = double.Parse(ts2.Days.ToString());
                        double tot2 = 0;

                        if (totdays2 < 0)
                            totdays2 = 0;
                        else
                        {
                            tot2 = Math.Floor(totdays2 / 365);
                            taskControlQuote.DriversCollection.Rows[d]["DriverAge"]  = int.Parse(tot2.ToString().Trim());
                        }
                    }

                     taskControlQuote.DriversCollection.AcceptChanges();
                }


                //Vehicles
                if (taskControlQuote.VehicleCollection.Rows.Count > 0)
                {
                    for (int e = 0; e < taskControlQuote.VehicleCollection.Rows.Count; e++)
                    {
                        //TODO TERMINAR COMPARACION DE EFFDATE y VEHICLEYEAR ENTRE MESES SEPTIEMBRE LLEGAN AUTOS DEL PROX. ANO SEP 2018 salen 2019
                        //Si es el Primer año la depreciación es 7.5% (0.925)
                        double Depreciation = 0;
                        DateTime CarDate = new DateTime(int.Parse(taskControlQuote.VehicleCollection.Rows[e]["VehicleYear"].ToString().Trim()) - 1, 9, 1);

                        int dateMonths = 0;
                        dateMonths = ((DateTime.Parse(taskControl.EffectiveDate.ToString()).Year - CarDate.Year) * 12) + DateTime.Parse(taskControl.EffectiveDate.ToString()).Month - CarDate.Month;

                        //Si es el Primer año la depreciación es 7.5%
                        if (dateMonths >= 12)
                        {
                            //==============================================//
                            //(0.975) = depreciación de 2.5% FULLCOVER ONLY //
                            //==============================================//
                            Depreciation = 0.975;
                        }
                        else
                        {
                            //==============================================//
                            //(0.925) = depreciación de 7.5% FULLCOVER ONLY //
                            //==============================================//
                            Depreciation = 0.925;
                        }
                        taskControlQuote.VehicleCollection.Rows[e]["VehicleValue"] = (Math.Round(float.Parse(taskControlQuote.VehicleCollection.Rows[e]["VehicleValue"].ToString()) * Depreciation, 0)).ToString();
                    }
                    taskControlQuote.VehicleCollection.AcceptChanges();
                }

                double Discounts = 0.00;
                taskControlQuote.TotalDiscountsPct = Discounts;
               
                taskControlQuote.SaveAutos(userID);  //(userID);
                taskControlQuote.Mode = 2;

                Session.Remove("TaskControl");
                Session.Add("TaskControl", taskControlQuote);

                return taskControlQuote.TaskControlID;
            }
            catch (Exception exc)
            {
                return 0;
            }
        }

        private int RenewalFromEPPS(TaskControl.Autos taskControl)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            string Message;

            try
            {
                if (!IsLocalHost())
                {
                    if (GetPPSHasEndorsementAutoVI(out Message))
                    {
                        if (Message != "")
                        {
                            return 0;
                        }
                    }

                    if (GetPPSCancelledPolicy(out Message))
                    {
                        if (Message != "")
                        {
                            return 0;
                        }
                    }
                }


                TaskControl.Autos taskControlQuote = new EPolicy.TaskControl.Autos(false);

                int tcID = taskControl.TaskControlID;

                taskControlQuote.Prospect.FirstName = taskControl.Customer.FirstName.Trim().ToUpper();
                taskControlQuote.Prospect.LastName1 = taskControl.Customer.LastName1.Trim().ToUpper();
                taskControlQuote.Prospect.LastName2 = taskControl.Customer.LastName2.Trim().ToUpper();
                taskControlQuote.Prospect.HomePhone = taskControl.Customer.HomePhone.Trim().ToUpper();
                taskControlQuote.Prospect.WorkPhone = taskControl.Customer.JobPhone.Trim().ToUpper();
                taskControlQuote.Prospect.Cellular = taskControl.Customer.Cellular.Trim().ToUpper();
                taskControlQuote.Prospect.Email = taskControl.Customer.Email.Trim().ToUpper();

                //Para aplicar el ultimo endoso, sino a la poliza original
                DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(tcID);
                if (endososList.Rows.Count == 0)
                {
                    taskControlQuote = taskControl;
                    taskControlQuote.Mode = 1; //ADD
                    taskControlQuote.TaskControlID = 0;
                    taskControlQuote.isQuote = true;//.IsPolicy = false;
                    taskControlQuote.IsEndorsement = false;
                    taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                    taskControlQuote.DriversCollection = taskControl.DriversCollection.Copy();
                    taskControlQuote.VehicleCollection = taskControl.VehicleCollection.Copy();
                }
                else
                {
                    //Aplica al Ultimo endoso
                    bool isExistEndo = false;
                    EPolicy.TaskControl.Autos taskControlEndo = null; ;
                    for (int s = 1; s <= endososList.Rows.Count; s++)
                    {
                        if ((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"] != 0)
                        {
                            taskControlEndo = EPolicy.TaskControl.Autos.GetAutos((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], true);
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
                        taskControlQuote.IsEndorsement = false;
                        taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                        taskControlQuote.DriversCollection = taskControl.DriversCollection.Copy();
                        taskControlQuote.VehicleCollection = taskControl.VehicleCollection.Copy();
                    }
                    else
                    {
                        taskControlQuote = taskControlEndo;
                        taskControlQuote.Mode = 1; //ADD
                        taskControlQuote.TaskControlID = 0;
                        taskControlQuote.isQuote = true;//.IsPolicy = false;
                        taskControlQuote.IsEndorsement = false;
                        taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));

                        taskControlQuote.Prospect.FirstName = taskControl.Customer.FirstName.Trim().ToUpper();
                        taskControlQuote.Prospect.LastName1 = taskControl.Customer.LastName1.Trim().ToUpper();
                        taskControlQuote.Prospect.LastName2 = taskControl.Customer.LastName2.Trim().ToUpper();
                        taskControlQuote.Prospect.HomePhone = taskControl.Customer.HomePhone.Trim().ToUpper();
                        taskControlQuote.Prospect.WorkPhone = taskControl.Customer.JobPhone.Trim().ToUpper();
                        taskControlQuote.Prospect.Cellular = taskControl.Customer.Cellular.Trim().ToUpper();
                        taskControlQuote.Prospect.Email = taskControl.Customer.Email.Trim().ToUpper();

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
                        taskControlQuote.TotalDiscounts = taskControl.TotalDiscounts;
                        taskControlQuote.TotalDiscountsPct = taskControl.TotalDiscountsPct;
                        taskControlQuote.PolicyClassID = taskControl.PolicyClassID;

                        taskControlQuote.DriversCollection = taskControlEndo.DriversCollection.Copy();
                        taskControlQuote.VehicleCollection = taskControlEndo.VehicleCollection.Copy();
                    }
                }

                taskControlQuote.Mode = 1; //ADD
                taskControlQuote.Term = taskControl.Term;
                taskControlQuote.TCIDQuotes = tcID;//taskControlQuote.TaskControlID;
                taskControlQuote.CustomerCollection = "0";
                
				 if (DateTime.Parse(taskControl.ExpirationDate).AddYears(1) < DateTime.Today)
                {
                    taskControlQuote.EffectiveDate = DateTime.Now.ToShortDateString();
                    taskControlQuote.ExpirationDate = DateTime.Now.AddYears(1).ToShortDateString();
                    taskControlQuote.EffectiveDateApp = DateTime.Now.ToShortDateString();
                    taskControlQuote.ExpirationDateApp = DateTime.Now.AddYears(1).ToShortDateString();
                }
                else
                {
                    taskControlQuote.EffectiveDate = DateTime.Parse(taskControl.EffectiveDate).AddYears(1).ToShortDateString();
                    taskControlQuote.ExpirationDate = DateTime.Parse(taskControl.ExpirationDate).AddYears(1).ToShortDateString();
                    taskControlQuote.EffectiveDateApp = DateTime.Parse(taskControl.EffectiveDate).AddYears(1).ToShortDateString();
                    taskControlQuote.ExpirationDateApp = DateTime.Parse(taskControl.ExpirationDate).AddYears(1).ToShortDateString();
                }
				
                taskControlQuote.Suffix = DateTime.Parse(taskControl.EffectiveDate.ToString()).Year.ToString().Substring(2, 2); //"0".ToString() + msufijo.ToString();
                taskControlQuote.RenewalNo = txtPolicyNo.Text.Trim(); // taskControl.PolicyType.ToString().Trim() + int.Parse(taskControl.PolicyNo.ToString().Trim()).ToString() + "-" + (int.Parse(taskControlQuote.Suffix.ToString().Trim()) - 1).ToString();
                
				//Calculate Customers Age
                TimeSpan ts = DateTime.Parse(DateTime.Now.ToShortDateString()) - DateTime.Parse(taskControl.Customer.Birthday.ToString());

                // Difference in days.
                double totdays = double.Parse(ts.Days.ToString());
                double tot = 0;

                if (totdays < 0)
                {
                    totdays = 0;
                }
                else
                {
                    tot = Math.Floor(totdays / 365);
                    taskControlQuote.Customer.Age = tot.ToString().Trim();
                }

                //Driver and Add Insured
                if (taskControlQuote.DriversCollection.Rows.Count > 0)
                {
                    for (int d = 0; d < taskControlQuote.DriversCollection.Rows.Count; d++)
                    {
                        TimeSpan ts2 = DateTime.Parse(DateTime.Now.ToShortDateString()) - DateTime.Parse(taskControlQuote.DriversCollection.Rows[d]["DriverDateOfBirth"].ToString());

                        // Difference in days.
                        double totdays2 = double.Parse(ts2.Days.ToString());
                        double tot2 = 0;

                        if (totdays2 < 0)
                            totdays2 = 0;
                        else
                        {
                            tot2 = Math.Floor(totdays2 / 365);
                            taskControlQuote.DriversCollection.Rows[d]["DriverAge"]  = int.Parse(tot2.ToString().Trim());
                        }
                    }

                     taskControlQuote.DriversCollection.AcceptChanges();
                }


                //Vehicles
                if (taskControlQuote.VehicleCollection.Rows.Count > 0)
                {
                    for (int e = 0; e < taskControlQuote.VehicleCollection.Rows.Count; e++)
                    {
                        //TODO TERMINAR COMPARACION DE EFFDATE y VEHICLEYEAR ENTRE MESES SEPTIEMBRE LLEGAN AUTOS DEL PROX. ANO SEP 2018 salen 2019
                        //Si es el Primer año la depreciación es 7.5% (0.925)
                        double Depreciation = 0;
                        DateTime CarDate = new DateTime(int.Parse(taskControlQuote.VehicleCollection.Rows[e]["VehicleYear"].ToString().Trim()) - 1, 9, 1);

                        int dateMonths = 0;
                        dateMonths = ((DateTime.Parse(taskControl.EffectiveDate.ToString()).Year - CarDate.Year) * 12) + DateTime.Parse(taskControl.EffectiveDate.ToString()).Month - CarDate.Month;

                        //Si es el Primer año la depreciación es 7.5%
                        if (dateMonths >= 12)
                        {
                            //==============================================//
                            //(0.975) = depreciación de 2.5% FULLCOVER ONLY //
                            //==============================================//
                            Depreciation = 0.975;
                        }
                        else
                        {
                            //==============================================//
                            //(0.925) = depreciación de 7.5% FULLCOVER ONLY //
                            //==============================================//
                            Depreciation = 0.925;
                        }
                        taskControlQuote.VehicleCollection.Rows[e]["VehicleValue"] = (Math.Round(float.Parse(taskControlQuote.VehicleCollection.Rows[e]["VehicleValue"].ToString()) * Depreciation, 0)).ToString();
                    }
                    taskControlQuote.VehicleCollection.AcceptChanges();
                }
                
                double Discounts = 0.00;
                DataTable dtDiscount = GetDiscounts("22", false, false, DateTime.Parse(taskControlQuote.EffectiveDate));
                 if (dtDiscount.Rows.Count > 0)
                 {
                     taskControlQuote.TotalDiscountsPct = double.Parse(dtDiscount.Rows[0]["Discount"].ToString());
                 }

                 taskControlQuote.SaveAutos(userID);  //(userID);
                 taskControlQuote.Mode = 2;

                Session.Remove("TaskControl");
                Session.Add("TaskControl", taskControlQuote);

                return taskControlQuote.TaskControlID;
            }
            catch (Exception exc)
            {
				LogError(exc,"4");
                return 0;
            }
        }
		
		private bool GetPPSHasEndorsementAutoVI(out string Message)
        {
            EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
            Message = "";
            bool HasEndorment = false;

            try
            {
                string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                System.Data.DataTable PPSPolicy = new System.Data.DataTable();
                System.Data.DataTable dt = new DataTable();

                cmd.CommandText = "sproc_CheckHasEndorsement";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                cmd.Parameters.Clear();
                //var PolicyNo = this.PolicyNo;
                //string PolicyNo = taskControl.Suffix.Trim() == "00" ? taskControl.PolicyType.Trim() + taskControl.PolicyNo.Trim() : taskControl.PolicyType.Trim() + taskControl.PolicyNo.Trim() + "-" + taskControl.Suffix.Trim();
                string PolicyNo = txtPolicyNo.Text.Trim();
                //if (this.PolicyNo[0].ToString() == "0")
                //{
                //    //Removes Policy Sufix to identify the policy as it is in PPS (Without 0000000 format)
                //    PolicyNo = this.PolicyNo.Contains("-") ? int.Parse(this.PolicyNo.Substring(0, this.PolicyNo.IndexOf("-")).Replace("-", "")).ToString() : int.Parse(this.PolicyNo).ToString();
                //}
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
                    HasEndorment = true;
                }

                //if (HasEndorment == false && DateTime.Parse(taskControl.ExpirationDate) < DateTime.Now)
                //{
                //    Message = "Error: This policy is already expired.";
                //    HasEndorment = true;
                //}
            }
            catch (Exception)
            {

                throw;
            }

            return HasEndorment;
        }

        private bool GetPPSCancelledPolicy(out string Message)
        {
            //EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
            Message = "";
            bool HasCancelled = false;

            try
            {
                string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                System.Data.DataTable PPSPolicy = new System.Data.DataTable();
                System.Data.DataTable dt = new DataTable();

                cmd.CommandText = "sproc_CheckPolicyCancelled";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                cmd.Parameters.Clear();
                //var PolicyNo = this.PolicyNo;
                //string PolicyNo = taskControl.Suffix.Trim() == "00" ? taskControl.PolicyType.Trim() + taskControl.PolicyNo.Trim() : taskControl.PolicyType.Trim() + taskControl.PolicyNo.Trim() + "-" + taskControl.Suffix.Trim();
                string PolicyNo = txtPolicyNo.Text.Trim();
                //if (this.PolicyNo[0].ToString() == "0")
                //{
                //    //Removes Policy Sufix to identify the policy as it is in PPS (Without 0000000 format)
                //    PolicyNo = this.PolicyNo.Contains("-") ? int.Parse(this.PolicyNo.Substring(0, this.PolicyNo.IndexOf("-")).Replace("-", "")).ToString() : int.Parse(this.PolicyNo).ToString();
                //}
                cmd.Parameters.AddWithValue("@PolicyID", PolicyNo);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                sqlConnection1.Close();

                if (PPSPolicy.Rows.Count > 0)
                {
                    //Validar si en PPS esta cancelada.
                    if (PPSPolicy.Rows[0][0].ToString() == "CANCELLED")
                    {
                        Message = "This Policy is Cancelled in PPS, Please verify.";
                        HasCancelled = true;
                    }
                }
                else
                {
                    Message = "Error Checking if the policy is cancelled in PPS, Please Try Again.";
                    HasCancelled = true;
                }

                //if (HasCancelled == false && DateTime.Parse(taskControl.ExpirationDate) < DateTime.Now)
                //{
                //    Message = "Error: This policy is already expired.";
                //    HasCancelled = true;
                //}
            }
            catch (Exception)
            {

                throw;
            }

            return HasCancelled;
        }

        private static bool IsLocalHost()
        {
            return //false;
                   HttpContext.Current.Request.Url.ToString().Contains("localhost");
        }

        private static DataTable GetDiscounts(string PolicyClassID, bool Renewal, bool IsLiabilityOnly, DateTime EffectiveDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
                SqlDbType.VarChar, 10, PolicyClassID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Renewal",
               SqlDbType.Bit, 0, Renewal.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsLiabilityOnly",
               SqlDbType.Bit, 0, IsLiabilityOnly.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
              SqlDbType.DateTime, 0, EffectiveDate.ToString(),
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
                dt = exec.GetQuery("GetVI_Discounts", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }
}
}