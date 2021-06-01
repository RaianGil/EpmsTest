using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml;

public partial class XMLPolicy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label7.Visible = false;

        //EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
        
        
        if (!IsPostBack)
        {
            DataTable dtPolicy = EPolicy.LookupTables.LookupTables.GetTable("PolicyClass");

            ddlPolicy.DataSource = dtPolicy;
            ddlPolicy.DataTextField = "PolicyClassDesc";
            ddlPolicy.DataValueField = "PolicyClassID";
            ddlPolicy.DataBind();
            ddlPolicy.SelectedIndex = -1;
            ddlPolicy.Items.Insert(0, "");
        }

    }

    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
     
        base.OnInit(e);

        Control Banner = new Control();
        Banner = LoadControl(@"TopBannerNew.ascx");
        this.phTopBanner.Controls.Add(Banner);
    }


    protected void BtnGenerate_Click(object sender, EventArgs e)
    {


        if (BeginDate.Text != "" && EndDate.Text != "")
        {

            Label7.Visible = false;
            Epolicy.TaskControl.CreateXML createXML = new Epolicy.TaskControl.CreateXML();

            int SelectedPolicy;
            SelectedPolicy = int.Parse(ddlPolicy.SelectedIndex.ToString());
            switch (SelectedPolicy)
            {
                case 1:
                    createXML.createXMLDoc(BeginDate.Text, EndDate.Text);
                    break;

                case 2:
                    createXML.createXMLDoc_PR(BeginDate.Text, EndDate.Text);
                //    this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Customer information saved successfully." + "\r\n" + customer.Warning.Trim());
                //this.litPopUp.Visible = true;
                    break;
            }

        }


        else 
        {
            //Label7.Visible = true;
            //Label7.Text = "Favor entrar ambas fechas.";
            //Label7.ForeColor = System.Drawing.Color.Red;

            lblRecHeader.Text = "Favor entrar ambas fechas.";
            mpeSeleccion.Show();
        }

    }
    //public static string MakeLiteralPopUpString(string message)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<script language=javascript>alert('");
    //    message = Regex.Replace(message, "\\r\\n", @"\r\n");
    //    message = Regex.Replace(message, "\\n\\r", @"\n\r");
    //    message = Regex.Replace(message, "'", @"\'");
    //    message = Regex.Replace(message, "\"", "\\\"");
    //    sb.Append(message);
    //    sb.Append("');</script>");
    //    return (sb.ToString());
    //}
    protected void ddlPolicy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //private void  GetDataFromPPS()
    //    {
    //        //Class.Claim claim = new Class.Claim();
    //        string language = "";

    //        if (Session["Language"] != null)
    //            language = (String)Session["Language"];

    //        //if(txtPlateVerification.Text.ToUpper() != "" && txtAccidentDateVerification.Text != "")
    //        if ((txtPolicyNoVerification.Text != "") || (txtPlateVerification.Text.ToUpper() != "" && txtAccidentDateVerification.Text != "") ||
    //            (txtPlateVerification.Text.ToUpper() != "" && txtVINVerification.Text != "") || (txtVINVerification.Text != "" && txtAccidentDateVerification.Text != ""))   
    //              // (txtAccidentDateVerification.Text != "" && txtPlateVerification.Text.ToUpper() == "TDG376")
    //        {              
    //            try
    //            {                    
    //                XmlDocument XmlDoc = new XmlDocument();

    //                if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
    //                {
    //                        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();

    //                        cn.ConnectionString = @"Data Source=gic-msql\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1";
    //                        //cn.ConnectionString = @"Data Source=GICPR-SERVER2\GuardianDB;Initial Catalog=GICPPSDATA;User ID=sa;password=SQL2008123$";                
    //                        cn.Open();

    //                        SqlCommand cmd = new SqlCommand();
    //                        cmd.Connection = cn;
    //                        //cmd.CommandType = CommandType.StoredProcedure;
    //                        cmd.CommandType = CommandType.Text;
    //                        cmd.CommandText = "exec sproc_GetPPSPolicyXML " +
    //                            "@LossDate= '" + txtAccidentDateVerification.Text.Trim() + "', " +
    //                            "@Plate='" + txtPlateVerification.Text.Trim() + "', " +
    //                            "@VIN='" + txtVINVerification.Text.Trim() + "'," +
    //                            //"@PolicyID=default, @xmldata=@xmldata output" ;
    //                            ((txtPolicyNoVerification.Text.Trim() == "") ?
    //                            "@PolicyID=DEFAULT, @xmldata=@xmldata output" :
    //                            "@PolicyID= '" + txtPolicyNoVerification.Text.Trim() + "', @xmldata=@xmldata output");
    //                        cmd.Parameters.Clear();
    //                        cmd.Parameters.Add("@xmldata", SqlDbType.Xml).Direction = ParameterDirection.Output;

    //                        //XmlDocument XmlDoc = new XmlDocument();
    //                        int c = cmd.ExecuteNonQuery();
    //                        string f = cmd.Parameters[0].Value.ToString();

    //                        if (f.Trim() == "")
    //                        {
    //                            cn.Close();
    //                            throw new Exception("Policy not found in PPS");
    //                        }

    //                        XmlDoc.LoadXml(f);
    //                        cn.Close();
                            
                           
    //                    }
    //                    else
    //                    {
    //                        XmlDoc.Load("C:\\inetpub\\wwwroot\\ClaimNextPR\\Reports\\Xml2.xml");
    //                    }
    //                    //txtPolicyNoVerification.Text = f;

    //                    //string EffDate = "";
    //                    //string ExpDate = "";
    //                    string CanDate = "";
                        
    //                    bool FilledName = false, FilledAddress = false, FilledAsl = false; 

    //                    #region Xml
                        
    //                    XmlNodeList XmlBase = XmlDoc.GetElementsByTagName("Policy");
                       
    //                    foreach (XmlNode XmlPolicyBase in XmlBase)
    //                    {                            
    //                        PolicyNumber = XmlPolicyBase["PolicyID"].InnerText;
    //                        txtPolicyNoVerification.Text = PolicyNumber;
    //                        PolicyPrefix = PolicyNumber.Substring(0, 3);
    //                        EffDate = DateTime.Parse(XmlPolicyBase["Incept"].InnerText).ToShortDateString();
    //                        ExpDate = DateTime.Parse(XmlPolicyBase["Expire"].InnerText).ToShortDateString();

    //                        if (XmlPolicyBase["CanDate"].InnerText != "")
    //                            CanDate = DateTime.Parse(XmlPolicyBase["CanDate"].InnerText).ToShortDateString();
                            
    //                        if (XmlPolicyBase.HasChildNodes)
    //                        {
    //                            //XmlNode Child = XmlPolicyBase.FirstChild;
    //                            foreach (XmlNode Childs in XmlPolicyBase.ChildNodes)
    //                            {
    //                                if (Childs.Name == "PolRelTable")
    //                                {
    //                                    foreach (XmlElement ChildsElement in Childs)
    //                                    {
    //                                        if (ChildsElement.HasChildNodes)
    //                                        {
    //                                            foreach (XmlElement GrandChildElements in ChildsElement)
    //                                            {
    //                                                if (GrandChildElements.Name == "EntNamesTable")
    //                                                {
    //                                                    foreach (XmlElement GreatGrandElements in GrandChildElements)
    //                                                    {
    //                                                        if (!(FilledName))  //Solo leerá los campos una vez, tomando los primeros que lea
    //                                                        {
    //                                                            txtName.Text = GreatGrandElements["FirstName"].InnerText +
    //                                                                ((GreatGrandElements["Middle"].InnerText != "") ? " " + GreatGrandElements["Middle"].InnerText : "") + " " +
    //                                                                GreatGrandElements["LastName"].InnerText;
                                                                
    //                                                            //txtInsuredDOB.Text = DateTime.Parse(GreatGrandElements["Dob"].InnerText).ToShortDateString();
    //                                                            //txtInsuredLicense.Text = GreatGrandElements["License"].InnerText;
    //                                                            txtState.Text = GreatGrandElements["State"].InnerText;

    //                                                            InsuredName = txtName.Text;
    //                                                            InsuredState = txtState.Text;
    //                                                            FilledName = true;
    //                                                        }
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                }

    //                                else if (Childs.Name == "VehicleTable")
    //                                {
    //                                    foreach (XmlElement ChildsElement in Childs)
    //                                    {
    //                                        if ((ChildsElement["LicPlate"].InnerText == txtPlateVerification.Text.Trim().ToUpper()) ||
    //                                            (ChildsElement["Vin"].InnerText == txtVINVerification.Text.Trim().ToUpper()))
    //                                        {
    //                                            txtVINVerification.Text = ChildsElement["Vin"].InnerText;
    //                                            txtPlateVerification.Text = ChildsElement["LicPlate"].InnerText;

    //                                            txtVIN.Text = txtVINVerification.Text.Trim();
    //                                            txtPlate.Text = txtPlateVerification.Text.Trim().ToUpper();

    //                                            InsuredVin = txtVINVerification.Text;
    //                                            InsuredPlate = txtPlateVerification.Text;

    //                                            if (ChildsElement.HasChildNodes)
    //                                            {
    //                                                foreach (XmlElement GrandChilds in ChildsElement)
    //                                                {
    //                                                    if (GrandChilds.Name == "PhysVehicleTable")
    //                                                    {
    //                                                        foreach (XmlElement GrandChildsElements in GrandChilds)
    //                                                        {
    //                                                            ddlVehicleMake.SelectedIndex = ddlVehicleMake.Items.IndexOf(ddlVehicleMake.Items.FindByText((GrandChildsElements["Make"].InnerText.Trim()).ToString()));

    //                                                            if (ddlVehicleMake.SelectedItem.Value != "")
    //                                                            {
    //                                                                if (int.Parse(ddlVehicleMake.SelectedItem.Value) > 0)
    //                                                                    FillModelDDListByPPS(ddlVehicleMake.SelectedItem.Value.ToString());
    //                                                            }

    //                                                            ddlVehicleModel.SelectedIndex = ddlVehicleModel.Items.IndexOf(ddlVehicleModel.Items.FindByText((GrandChildsElements["Model"].InnerText.TrimEnd()).ToString()));
    //                                                            ddlVehicleYear.SelectedIndex = ddlVehicleYear.Items.IndexOf(ddlVehicleYear.Items.FindByText((GrandChildsElements["MYear"].InnerText.TrimEnd()).ToString())); //GrandChildsElements["MYear"].InnerText;
    //                                                            ///ddlBroker.Items.IndexOf(ddlBroker.Items.FindByValue(int.Parse(XmlPolicyBase["BrokerID"].InnerText).ToString()));                                                                
    //                                                        }
    //                                                    }
    //                                                    else if (GrandChilds.Name == "VehicleCvrgTable")
    //                                                    {
    //                                                        foreach (XmlElement GrandChildsElements in GrandChilds)
    //                                                        {
    //                                                            if (!(FilledAsl))  //Solo leerá los campos una vez, tomando los primeros que lea
    //                                                            {
    //                                                                ReinsAsl = GrandChildsElements["ReinsAsl"].InnerText.ToString();

    //                                                                GetCoverage();

    //                                                                FilledAsl = true;
    //                                                            }
    //                                                        }
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                }

    //                                else if (Childs.Name == "ClientTable")
    //                                {
    //                                    foreach (XmlElement ChildsElement in Childs)
    //                                    {
    //                                        if (!(FilledAddress))   //Solo leerá los campos una vez, tomando los primeros que lea
    //                                        {
    //                                            txtInsuredAddrs1.Text = ChildsElement["Maddr1"].InnerText;
    //                                            txtAddrs2.Text = ChildsElement["Maddr2"].InnerText;
    //                                            //ddlCityFullInfo.Text = ChildsElement["Mcity"].InnerText; //txtInsuredCity.Text =
    //                                            //txtInsuredState.Text = ChildsElement["Mstate"].InnerText;
    //                                            txtZipCode.Text = ChildsElement["Mzip"].InnerText;
    //                                            txtWorkPhone.Text = ChildsElement["Wphone"].InnerText;
    //                                            txtCellular.Text = ChildsElement["Cphone"].InnerText;

    //                                            InsuredAddress1 = txtInsuredAddrs1.Text;
    //                                            InsuredAddress2 = txtAddrs2.Text;
    //                                            InsuredZip = txtZipCode.Text;
    //                                            InsuredWorkPhone = txtWorkPhone.Text;
    //                                            InsuredCellular = txtCellular.Text;

    //                                            FilledAddress = true;
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        ///Todas las variables Insured mencionadas existen así para la ocasión en que llame el reclamante
    //                        ///La información del asegurado no se pierda, si no que siga hacia adelante en el proceso. 
    //                    }
    //                    #endregion

    //                    lblClaimFound.Text = "Insured Name: " + txtName.Text.Trim();
    //                    lblClaimFound0.Text = "Policy No: " + txtPolicyNoVerification.Text.Trim();
    //                    lblClaimFound1.Text = "Eff Date: " + EffDate.Trim();
    //                    lblClaimFound2.Text = "Exp Date: " + ExpDate.Trim();

    //                    lblClaimFound.Visible = true;
    //                    lblClaimFound0.Visible = true;
    //                    lblClaimFound1.Visible = true;
    //                    lblClaimFound2.Visible = true;
    //                    lblClaimExist.Visible = false;

    //                    if (CanDate != "")
    //                    {
    //                        lblClaimCancelled.Text = "Policy was cancelled on the date " + CanDate.Trim();
    //                        lblClaimCancelled.Visible = true;
    //                    }
    //            }
    //            catch (Exception exc)
    //            {
    //                lblRecHeader.Text = exc.Message.ToString();// + "/" + exc.InnerException.ToString();     

    //                lblClaimFound.Text =  "";
    //                lblClaimFound0.Text = "";
    //                lblClaimFound1.Text = "";
    //                lblClaimFound2.Text = "";
    //                lblClaimCancelled.Text = "";
    //                lblClaimExist.Text = "";
    //                lblClaimFound.Visible = false;
    //                lblClaimFound0.Visible = false;
    //                lblClaimFound1.Visible = false;
    //                lblClaimFound2.Visible = false;
    //                lblClaimCancelled.Visible = false;
    //                lblClaimExist.Visible = false;
    //                //if (exc.Message != "Root element is missing.")
    //                //{
    //                    mpeSeleccion.Show();
    //                //}
    //            }
    //        }
    //        else
    //        {
    //            claim.ClaimID = 0;
    //            lblClaimFound.Text = "Claim Not Found.";
    //            lblClaimFound.Visible = true;

    //            lblClaimFound0.Text = "";
    //            lblClaimFound1.Text = "";
    //            lblClaimFound2.Text = "";
    //            lblClaimCancelled.Text = "";
    //            lblClaimFound0.Visible = false;
    //            lblClaimFound1.Visible = false;
    //            lblClaimFound2.Visible = false;
    //            lblClaimCancelled.Visible = false;
    //        }

    //        txtAccidentDate.Text = txtAccidentDateVerification.Text;

    //        Session.Remove("Claim");
    //        Session.Add("Claim", claim);
    //    }
}