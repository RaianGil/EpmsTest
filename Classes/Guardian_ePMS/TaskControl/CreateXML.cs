using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Epolicy.TaskControl
{
    public class CreateXML
    {

        public CreateXML()
        {

        }

        public void createXMLDoc_PR(String BeginDate, String EndDate)
        {




            SqlConnection conn4 = null;
            SqlDataReader rdr4 = null;
            XmlDocument xmlDoc = new XmlDocument();
            SqlConnection conn = null;


            string ConnectionString = ConfigurationManager.ConnectionStrings["GuardianConnectionString"].ConnectionString;
            
            conn = new SqlConnection(ConnectionString);
            conn4 = new SqlConnection(ConnectionString);

            conn4.Open();

            SqlCommand cmd4 = new SqlCommand("GetTaskControlFromDate_VI", conn4);

            cmd4.CommandType = System.Data.CommandType.StoredProcedure;

            cmd4.Parameters.AddWithValue("@BeginDate", BeginDate);
            cmd4.Parameters.AddWithValue("@EndDate", EndDate);
            cmd4.Parameters.AddWithValue("@PolicyClassID", 23);
            cmd4.CommandTimeout = 0;
            rdr4 = cmd4.ExecuteReader();

            string NAMECONVENTION = DateTime.Now.ToString("MM.dd.yyyy_hhmmss");//yyyyMMddhhmmss");

            XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xmlDoc.AppendChild(docNode);


                    XmlElement xmlPolicy = xmlDoc.CreateElement("Policies");
                    xmlDoc.AppendChild(xmlPolicy);

                    //xmlPolicy.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    //xmlPolicy.SetAttribute("xsi:schemaLocation", "urn:pps-simple-auto-policy XMLFile11.xsd");
                    //xmlPolicy.SetAttribute("xmlns", "urn:pps-simple-auto-policy");




                    XmlAttribute nsAttribute = xmlDoc.CreateAttribute("xmlns", "xsi",
                        "http://www.w3.org/2000/xmlns/");
                    nsAttribute.Value = "http://www.w3.org/2001/XMLSchema-instance";
                    xmlPolicy.Attributes.Append(nsAttribute);

                    //XmlAttribute nsAttribute1 = xmlDoc.CreateAttribute("xsi", "schemaLocation",      // Los NameSpaces
                    //    "http://www.w3.org/2001/XMLSchema-instance");
                    //nsAttribute1.Value = "pps-simple-auto-policy" + " " + NAMECONVENTION + ".xsd";
                    //xmlPolicy.Attributes.Append(nsAttribute1);

                    XmlAttribute nsAttribute2 = xmlDoc.CreateAttribute("xmlns",
                        "http://www.w3.org/2000/xmlns/");
                    nsAttribute2.Value = "pps-simple-auto-policy";
                    xmlPolicy.Attributes.Append(nsAttribute2);

            try
            {
                while (rdr4.Read())
                {
                    SqlDataReader rdr = null;


                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetGuardianXtraXMLReport", conn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                    cmd.CommandTimeout = 0;


                    rdr = cmd.ExecuteReader();


                    //conn1 = new SqlConnection(ConnectionString);

                    //conn1.Open();

                    //SqlCommand cmd1 = new SqlCommand("GetGuardianXtraDeclarationReport", conn1);

                    //cmd1.CommandType = System.Data.CommandType.StoredProcedure;

                    //cmd1.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                    //cmd1.CommandTimeout = 0;

                    //rdr1 = cmd1.ExecuteReader();


                    //SqlConnection conn0 = null;
                    //SqlDataReader rdr0 = null;


                    //conn0 = new SqlConnection(ConnectionString);

                    //conn0.Open();

                    //SqlCommand cmd0 = new SqlCommand("GetGuardianXtraDeclarationReport", conn0);

                    //cmd0.CommandType = System.Data.CommandType.StoredProcedure;

                    //cmd0.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                    //cmd0.CommandTimeout = 0;


                    //rdr0 = cmd0.ExecuteReader();

                    

                    //XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    //xmlDoc.AppendChild(docNode);


                    //XmlElement xmlPolicy = xmlDoc.CreateElement("Policies");
                    //xmlDoc.AppendChild(xmlPolicy);

                    ////xmlPolicy.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    ////xmlPolicy.SetAttribute("xsi:schemaLocation", "urn:pps-simple-auto-policy XMLFile11.xsd");
                    ////xmlPolicy.SetAttribute("xmlns", "urn:pps-simple-auto-policy");




                    //XmlAttribute nsAttribute = xmlDoc.CreateAttribute("xmlns", "xsi",
                    //    "http://www.w3.org/2000/xmlns/");
                    //nsAttribute.Value = "http://www.w3.org/2001/XMLSchema-instance";
                    //xmlPolicy.Attributes.Append(nsAttribute);

                    ////XmlAttribute nsAttribute1 = xmlDoc.CreateAttribute("xsi", "schemaLocation",      // Los NameSpaces
                    ////    "http://www.w3.org/2001/XMLSchema-instance");
                    ////nsAttribute1.Value = "pps-simple-auto-policy" + " " + NAMECONVENTION + ".xsd";
                    ////xmlPolicy.Attributes.Append(nsAttribute1);

                    //XmlAttribute nsAttribute2 = xmlDoc.CreateAttribute("xmlns",
                    //    "http://www.w3.org/2000/xmlns/");
                    //nsAttribute2.Value = "pps-simple-auto-policy";
                    //xmlPolicy.Attributes.Append(nsAttribute2);

                   
                   



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
                        xmlRenewalOf.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + rdr["Sufijo"].ToString().Trim();

                        XmlElement xmlBrokerID = xmlDoc.CreateElement("BrokerID");
                        xmlPolicy1.AppendChild(xmlBrokerID);
                        xmlBrokerID.InnerText = rdr["AgentID"].ToString().Trim();

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
                        xmlPolSubType.InnerText = "0";

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
                            XmlAttribute attribute9 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                            attribute9.Value = "true";
                            xmlLicence.Attributes.Append(attribute9);
                            xmlEntNames.AppendChild(xmlLicence);

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

                            XmlElement xmlLicPlate = xmlDoc.CreateElement("LicPlate");
                            //XmlAttribute attribute14 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                            //attribute14.Value = "true";
                            //xmlLicPlate.Attributes.Append(attribute14);
                            xmlVehicle.AppendChild(xmlLicPlate);
                            xmlLicPlate.InnerText = rdr["Licence"].ToString().Trim();


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
                            xmlIsland.InnerText = "1";

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




                    conn.Close();
                    // cierra las conecciones
                    
                }
                xmlDoc.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + NAMECONVENTION + ".xml"); // save

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

                conn4.Close();
            }
            catch (Exception ecp)
            {
                throw new Exception(ecp.Message.ToString());
            }
        }

        public void createXMLDoc(String BeginDate, String EndDate)
        {

            SqlConnection conn4 = null;
            SqlDataReader rdr4 = null;
            XmlDocument xmlDoc = new XmlDocument();
            SqlConnection conn = null;
            SqlConnection conn0 = null;
            SqlConnection conn1 = null;


            string ConnectionString = ConfigurationManager.ConnectionStrings["GuardianConnectionString"].ConnectionString;

            conn = new SqlConnection(ConnectionString);
            conn0 = new SqlConnection(ConnectionString);
            conn1 = new SqlConnection(ConnectionString);
            conn4 = new SqlConnection(ConnectionString);

            conn4.Open();

            SqlCommand cmd4 = new SqlCommand("GetTaskControlFromDate_VI", conn4);

            cmd4.CommandType = System.Data.CommandType.StoredProcedure;

            cmd4.Parameters.AddWithValue("@BeginDate", BeginDate);
            cmd4.Parameters.AddWithValue("@EndDate", EndDate);
            cmd4.Parameters.AddWithValue("@PolicyClassID", 22);
            cmd4.CommandTimeout = 0;
            rdr4 = cmd4.ExecuteReader();

            string NAMECONVENTION = DateTime.Now.ToString("MM.dd.yyyy_hhmmss");//yyyyMMddhhmmss");
            //rdr4["PolicyType"].ToString().Trim() + rdr4["PolicyNo"].ToString().Trim() + "-" + rdr4["Sufijo"].ToString().Trim() + DateTime.Now.ToString("MM.dd.yyyy_hhmmss");//yyyyMMddhhmmss");

            XmlNode docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(docNode);


            XmlElement xmlPolicy = xmlDoc.CreateElement("Policies");
            xmlDoc.AppendChild(xmlPolicy);

            //xmlPolicy.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            //xmlPolicy.SetAttribute("xsi:schemaLocation", "urn:pps-simple-auto-policy XMLFile11.xsd");
            //xmlPolicy.SetAttribute("xmlns", "urn:pps-simple-auto-policy");




            XmlAttribute nsAttribute = xmlDoc.CreateAttribute("xmlns", "xsi",
                "http://www.w3.org/2000/xmlns/");
            nsAttribute.Value = "http://www.w3.org/2001/XMLSchema-instance";
            xmlPolicy.Attributes.Append(nsAttribute);

            //XmlAttribute nsAttribute1 = xmlDoc.CreateAttribute("xsi", "schemaLocation",      // Los NameSpaces
            //    "http://www.w3.org/2001/XMLSchema-instance");
            //nsAttribute1.Value = "urn:pps-simple-auto-policy" + " " + NAMECONVENTION + ".xsd";
            //xmlPolicy.Attributes.Append(nsAttribute1);

            XmlAttribute nsAttribute2 = xmlDoc.CreateAttribute("xmlns",
                "http://www.w3.org/2000/xmlns/");
            nsAttribute2.Value = "pps-simple-auto-policy";
            xmlPolicy.Attributes.Append(nsAttribute2);


            try
            {
                while (rdr4.Read())
                {
                    SqlDataReader rdr = null;


                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetReportAutoGeneralInfo_VI", conn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                    cmd.CommandTimeout = 0;


                    rdr = cmd.ExecuteReader();
                    SqlDataReader rdr1 = null;

                    conn1.Open();

                    SqlCommand cmd1 = new SqlCommand("GetReportAutoVehiclesInfoPolicy_VI", conn1);

                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd1.Parameters.AddWithValue("@TaskControlID", rdr4["TaskControlID"].ToString().Trim());
                    cmd1.CommandTimeout = 0;

                    rdr1 = cmd1.ExecuteReader();


                    SqlDataReader rdr0 = null;



                    conn0.Open();

                    SqlCommand cmd0 = new SqlCommand("GetReportAutoDriversInfo_VI", conn0);

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
                            xmlPolicyID.InnerText = rdr["PolicyType"].ToString().Trim();

                            XmlElement xmlIncept = xmlDoc.CreateElement("Incept");
                            xmlPolicy1.AppendChild(xmlIncept);
                            xmlIncept.InnerText = DateTime.Parse(rdr["EffectiveDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";    // A donde y que columna de que reader vas a insertar en el texto

                            XmlElement xmlExpire = xmlDoc.CreateElement("Expire");
                            xmlPolicy1.AppendChild(xmlExpire);
                            xmlExpire.InnerText = DateTime.Parse(rdr["ExpirationDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";

                            XmlElement xmlRenewalOf = xmlDoc.CreateElement("RenewalOf");
                            xmlPolicy1.AppendChild(xmlRenewalOf);
                            xmlRenewalOf.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + rdr["Suffix"].ToString().Trim();

                            XmlElement xmlBrokerID = xmlDoc.CreateElement("BrokerID");
                            xmlPolicy1.AppendChild(xmlBrokerID);
                            xmlBrokerID.InnerText = rdr["AgentID"].ToString().Trim();

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
                            xmlBinderID.InnerText = rdr["PolicyType"].ToString().Trim() + rdr["PolicyNo"].ToString().Trim() + "-" + rdr["Suffix"].ToString().Trim();



                            XmlElement xmlComRate = xmlDoc.CreateElement("ComRate");
                            xmlPolicy1.AppendChild(xmlComRate);
                            xmlComRate.InnerText = "0.0000000e+000";

                            XmlElement xmlClient = xmlDoc.CreateElement("Client");
                            xmlPolicy1.AppendChild(xmlClient);
                            xmlClient.InnerText = "0";

                            XmlElement xmlTag = xmlDoc.CreateElement("Tag");
                            XmlAttribute attribute4 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                            attribute4.Value = "true";
                            xmlTag.Attributes.Append(attribute4);
                            xmlPolicy1.AppendChild(xmlTag);

                            XmlElement xmlPremium = xmlDoc.CreateElement("Premium");
                            xmlPolicy1.AppendChild(xmlPremium);
                            xmlPremium.InnerText = rdr["PTotalPremium"].ToString().Trim();

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

                            XmlElement xmlExcessLink = xmlDoc.CreateElement("ExcessLink");
                            xmlPolicy1.AppendChild(xmlExcessLink);
                            xmlExcessLink.InnerText = "0";

                            XmlElement xmlPolSubType = xmlDoc.CreateElement("PolSubType");
                            xmlPolicy1.AppendChild(xmlPolSubType);
                            xmlPolSubType.InnerText = "0";

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
                            xmlPolicy1ID1.InnerText = rdr["PolicyType"].ToString().Trim();

                            XmlElement xmlUpid = xmlDoc.CreateElement("Upid");
                            xmlPolRel.AppendChild(xmlUpid);
                            xmlUpid.InnerText = rdr["CustomerNo"].ToString().Trim();

                            XmlElement xmlPolRelat = xmlDoc.CreateElement("Polrelat");
                            xmlPolRel.AppendChild(xmlPolRelat);
                            xmlPolRelat.InnerText = "NI";

                            XmlElement xmlEntNamesTable = xmlDoc.CreateElement("EntNamesTable");
                            xmlPolRel.AppendChild(xmlEntNamesTable);



                            //while (rdr0.Read()) // loop
                            //{
                            XmlElement xmlEntNames = xmlDoc.CreateElement("EntNames");
                            xmlEntNamesTable.AppendChild(xmlEntNames);

                            string Bflag;

                            if (rdr["PolicyType"].ToString().Trim() == "XPA")
                            {
                                Bflag = "1";
                            }
                            else
                            {
                                Bflag = "0";
                            }

                            if (Bflag == "1")
                            {
                                XmlElement xmlLastName = xmlDoc.CreateElement("LastName");
                                xmlEntNames.AppendChild(xmlLastName);
                                xmlLastName.InnerText = rdr["LastNa1"].ToString().Trim();
                            }
                            else
                            {
                                XmlElement xmlLastName = xmlDoc.CreateElement("LastName");
                                xmlEntNames.AppendChild(xmlLastName);
                                xmlLastName.InnerText = rdr["CompanyName"].ToString().Trim();
                            }

                            XmlElement xmlFirstName = xmlDoc.CreateElement("FirstName");
                            xmlEntNames.AppendChild(xmlFirstName);
                            xmlFirstName.InnerText = rdr["FirstNa"].ToString().Trim();

                            XmlElement xmlMiddle = xmlDoc.CreateElement("Middle");
                            xmlEntNames.AppendChild(xmlMiddle);

                            XmlElement xmlUpid1 = xmlDoc.CreateElement("Upid");
                            xmlEntNames.AppendChild(xmlUpid1);
                            xmlUpid1.InnerText = "0";

                            //string DDB = rdr["Birthday"] != System.DBNull.Value ? (string)rdr["Birthday"] : "1900-01-01"; ;

                            XmlElement xmlDob = xmlDoc.CreateElement("Dob");
                            xmlEntNames.AppendChild(xmlDob);
                            xmlDob.InnerText = DateTime.Parse(rdr["Birthday"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00";
                            //GXAuto.Rows[0]["IsPersonalAuto"] != System.DBNull.Value ? (bool)GXAuto.Rows[0]["IsPersonalAuto"] : false;

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
                            xmlSsn.InnerText = "109511";

                            string BusFlag;
                            if (rdr["PolicyType"].ToString().Trim() == "PAP")
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

                            XmlElement xmlNsbyt = xmlDoc.CreateElement("Nsbyt");
                            xmlEntNames.AppendChild(xmlNsbyt);
                            xmlNsbyt.InnerText = "0";

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
                            if (BusFlag == "1" && rdr["PolicyType"].ToString().Trim() == "PAP")
                                xmlBusType.InnerText = "256";
                            else if (BusFlag == "1" && rdr["PolicyType"].ToString().Trim() == "BAP")
                                xmlBusType.InnerText = "32";

                            XmlElement xmlClient1 = xmlDoc.CreateElement("Client");
                            XmlAttribute attribute12 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                            attribute12.Value = "true";
                            xmlClient1.Attributes.Append(attribute12);
                            xmlEntNames.AppendChild(xmlClient1);
                            //xmlClient1.InnerText = rdr0["CustomerNo"].ToString().Trim();

                            XmlElement xmlPolRelat1 = xmlDoc.CreateElement("PolRelat");
                            XmlAttribute attribute13 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                            attribute13.Value = "true";
                            xmlPolRelat1.Attributes.Append(attribute13);
                            xmlEntNames.AppendChild(xmlPolRelat1);

                            XmlElement xmlDispImage1 = xmlDoc.CreateElement("DispImage");
                            xmlEntNames.AppendChild(xmlDispImage1);
                            xmlDispImage1.InnerText = "Person";
                            //}

                            XmlElement xmlVehicleTable = xmlDoc.CreateElement("VehicleTable");
                            xmlPolicy1.AppendChild(xmlVehicleTable);


                            while (rdr1.Read())
                            {
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

                                XmlElement xmlLicPlate = xmlDoc.CreateElement("LicPlate");
                                //XmlAttribute attribute14 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                                //attribute14.Value = "true";
                                //xmlLicPlate.Attributes.Append(attribute14);
                                xmlVehicle.AppendChild(xmlLicPlate);
                                xmlLicPlate.InnerText = rdr1["LicensePlateNo"].ToString().Trim();


                                XmlElement xmlPurchDate = xmlDoc.CreateElement("PurchDate");
                                xmlVehicle.AppendChild(xmlPurchDate);
                                xmlPurchDate.InnerText = "1753-01-01T00:00:00";

                                XmlElement xmlActCost = xmlDoc.CreateElement("ActCost");
                                xmlVehicle.AppendChild(xmlActCost);
                                xmlActCost.InnerText = "0.0000";

                                XmlElement xmlInsVal = xmlDoc.CreateElement("InsVal");
                                xmlVehicle.AppendChild(xmlInsVal);
                                xmlInsVal.InnerText = "0.0000";

                                XmlElement xmlInsValFlag = xmlDoc.CreateElement("InsValFlag");
                                xmlVehicle.AppendChild(xmlInsValFlag);
                                XmlAttribute attribute14 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                                attribute14.Value = "true";
                                xmlInsValFlag.Attributes.Append(attribute14);
                                //xmlInsValFlag.InnerText = "Actual Cash Value";

                                XmlElement xmlPayee = xmlDoc.CreateElement("Payee");
                                xmlVehicle.AppendChild(xmlPayee);
                                XmlAttribute attribute15 = xmlDoc.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
                                attribute15.Value = "true";
                                xmlPayee.Attributes.Append(attribute15);
                                //xmlPayee.InnerText = rdr1["BankDesc"].ToString().Trim();


                                XmlElement xmlIsland = xmlDoc.CreateElement("Island");
                                xmlVehicle.AppendChild(xmlIsland);
                                string Island = rdr1["Island"].ToString().Trim();
                                if (rdr1["Island"].ToString().Trim() != "")
                                {
                                    if (Island == "St. Croix")
                                        Island = "1";

                                    if (Island == "St. John")
                                        Island = "2";

                                    if (Island == "St. Thomas")
                                        Island = "3";
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
                                xmlBodyType.InnerText = "PU";

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
                                    string hello = rdr2["CompPremium"].ToString();
                                    if (rdr2["CompPremium"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "12212";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                            xmlVehicleCvrg.AppendChild(xmlLim1);
                                            xmlLim1.InnerText = decimal.Parse(rdr2["ComprehensiveDedu"].ToString().Trim().Replace("$", "")).ToString();

                                            XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                            xmlVehicleCvrg.AppendChild(xmlLim2);
                                            xmlLim2.InnerText = "0.0000";

                                            XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                            xmlVehicleCvrg.AppendChild(xmlPremium1);
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "05211";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                            xmlVehicleCvrg.AppendChild(xmlLim1);
                                            xmlLim1.InnerText = decimal.Parse(rdr2["ComprehensiveDedu"].ToString().Trim().Replace("$", "")).ToString();

                                            XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                            xmlVehicleCvrg.AppendChild(xmlLim2);
                                            xmlLim2.InnerText = "0.0000";

                                            XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                            xmlVehicleCvrg.AppendChild(xmlPremium1);
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }

                                    if (rdr2["CollPremium"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "13212";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                            xmlVehicleCvrg.AppendChild(xmlLim1);
                                            xmlLim1.InnerText = decimal.Parse(rdr2["CollisionDedu"].ToString().Trim().Replace("$", "")).ToString();

                                            XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                            xmlVehicleCvrg.AppendChild(xmlLim2);
                                            xmlLim2.InnerText = "0.0000";

                                            XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                            xmlVehicleCvrg.AppendChild(xmlPremium1);
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "06211";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                            xmlVehicleCvrg.AppendChild(xmlLim1);
                                            xmlLim1.InnerText = decimal.Parse(rdr2["CollisionDedu"].ToString().Trim().Replace("$", "")).ToString();

                                            XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                            xmlVehicleCvrg.AppendChild(xmlLim2);
                                            xmlLim2.InnerText = "0.0000";

                                            XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                            xmlVehicleCvrg.AppendChild(xmlPremium1);
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }

                                    if (rdr2["BIPremium"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "08194";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            string coll = rdr2["BIPPLimit"].ToString();
                                            if (rdr2["BIPPLimit"].ToString() == "$10,000/$20,000")
                                            {
                                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                                xmlLim1.InnerText = "10000.0000";

                                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                                xmlLim2.InnerText = "20000.0000";
                                            }
                                            else if (rdr2["BIPPLimit"].ToString() == "$10,000/$25,000")
                                            {
                                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                                xmlLim1.InnerText = "10000.0000";

                                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                                xmlLim2.InnerText = "25000.0000";
                                            }
                                            else if (rdr2["BIPPLimit"].ToString() == "$10,000/$50,000")
                                            {
                                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                                xmlLim1.InnerText = "10000.0000";

                                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                                xmlLim2.InnerText = "50000.0000";
                                            }
                                            else if (rdr2["BIPPLimit"].ToString() == "0")
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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "01192";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            if (rdr2["BIPPLimit"].ToString() == "$10,000/$20,000")
                                            {
                                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                                xmlLim1.InnerText = "10000.0000";

                                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                                xmlLim2.InnerText = "20000.0000";
                                            }
                                            else if (rdr2["BIPPLimit"].ToString() == "$10,000/$25,000")
                                            {
                                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                                xmlLim1.InnerText = "10000.0000";

                                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                                xmlLim2.InnerText = "25000.0000";
                                            }
                                            else if (rdr2["BIPPLimit"].ToString() == "$10,000/$50,000")
                                            {
                                                XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                                xmlVehicleCvrg.AppendChild(xmlLim1);
                                                xmlLim1.InnerText = "10000.0000";

                                                XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                                xmlVehicleCvrg.AppendChild(xmlLim2);
                                                xmlLim2.InnerText = "50000.0000";
                                            }
                                            else if (rdr2["BIPPLimit"].ToString() == "0")
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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }

                                    if (rdr2["PDPremium"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "09194";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                            xmlVehicleCvrg.AppendChild(xmlLim1);
                                            xmlLim1.InnerText = decimal.Parse(rdr2["PDLimit"].ToString().Trim().Replace("$", "")).ToString();

                                            XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                            xmlVehicleCvrg.AppendChild(xmlLim2);
                                            xmlLim2.InnerText = "0.0000";

                                            XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                            xmlVehicleCvrg.AppendChild(xmlPremium1);
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "02192";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                            xmlVehicleCvrg.AppendChild(xmlLim1);
                                            xmlLim1.InnerText = decimal.Parse(rdr2["PDLimit"].ToString().Trim().Replace("$", "")).ToString();

                                            XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                            xmlVehicleCvrg.AppendChild(xmlLim2);
                                            xmlLim2.InnerText = "0.0000";

                                            XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                            xmlVehicleCvrg.AppendChild(xmlPremium1);
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }

                                    if (rdr2["MPPremium"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }

                                    if (rdr2["MotoristPremium"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }

                                    if (rdr2["ADDPremium"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "08194";

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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "01192";

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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }

                                    if (rdr2["RentalReim"].ToString() != "0")
                                    {
                                        if (rdr2["PolicyType"].ToString().Trim() == "BAP")
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

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
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                        else //PAP
                                        {
                                            XmlElement xmlVehicleCvrg = xmlDoc.CreateElement("VehicleCvrg");
                                            xmlVehicleCvrgTable.AppendChild(xmlVehicleCvrg);

                                            XmlElement xmlVin2 = xmlDoc.CreateElement("Vin");
                                            xmlVehicleCvrg.AppendChild(xmlVin2);
                                            xmlVin2.InnerText = rdr2["VIN"].ToString().Trim();

                                            XmlElement xmlReinsAsl = xmlDoc.CreateElement("ReinsAsl");
                                            xmlVehicleCvrg.AppendChild(xmlReinsAsl);
                                            xmlReinsAsl.InnerText = "04211";

                                            XmlElement xmlPolicy1ID2 = xmlDoc.CreateElement("PolicyID");
                                            xmlVehicleCvrg.AppendChild(xmlPolicy1ID2);
                                            xmlPolicy1ID2.InnerText = rdr["PolicyType"].ToString().Trim();

                                            XmlElement xmlLim1 = xmlDoc.CreateElement("Lim1");
                                            xmlVehicleCvrg.AppendChild(xmlLim1);
                                            xmlLim1.InnerText = "10000.0000";

                                            XmlElement xmlLim2 = xmlDoc.CreateElement("Lim2");
                                            xmlVehicleCvrg.AppendChild(xmlLim2);
                                            xmlLim2.InnerText = "0.0000";

                                            XmlElement xmlPremium1 = xmlDoc.CreateElement("Premium");
                                            xmlVehicleCvrg.AppendChild(xmlPremium1);
                                            xmlPremium1.InnerText = rdr2["TotalPremium"].ToString().Trim();
                                        }
                                    }
                                }
                                conn2.Close();
                            }

                            XmlElement xmlClientTable = xmlDoc.CreateElement("ClientTable");
                            xmlPolicy1.AppendChild(xmlClientTable);

                            XmlElement xmlClient0 = xmlDoc.CreateElement("Client");
                            xmlClientTable.AppendChild(xmlClient0);

                            XmlElement xmlClient2 = xmlDoc.CreateElement("Client");
                            xmlClient0.AppendChild(xmlClient2);
                            xmlClient2.InnerText = rdr["CustomerNo"].ToString().Trim();


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
                            xmlMstate.InnerText = rdr["City"].ToString().Trim();

                            XmlElement xmlMnation = xmlDoc.CreateElement("Mnation");
                            xmlClient0.AppendChild(xmlMnation);
                            xmlMnation.InnerText = "0";

                            XmlElement xmlMzip = xmlDoc.CreateElement("Mzip");
                            xmlClient0.AppendChild(xmlMzip);
                            xmlMzip.InnerText = rdr["Zip"].ToString().Trim();

                            XmlElement xmlRaddr1 = xmlDoc.CreateElement("Raddr1");
                            xmlClient0.AppendChild(xmlRaddr1);
                            xmlRaddr1.InnerText = "8744 LINBERG BAY";

                            XmlElement xmlRaddr2 = xmlDoc.CreateElement("Raddr2");
                            xmlClient0.AppendChild(xmlRaddr2);
                            xmlRaddr2.InnerText = "0";

                            XmlElement xmlRaddr3 = xmlDoc.CreateElement("Raddr3");
                            xmlClient0.AppendChild(xmlRaddr3);
                            xmlRaddr3.InnerText = "0";

                            XmlElement xmlRcity = xmlDoc.CreateElement("Rcity");
                            xmlClient0.AppendChild(xmlRcity);
                            xmlRcity.InnerText = "ST  THOMAS";

                            XmlElement xmlRstate = xmlDoc.CreateElement("Rstate");
                            xmlClient0.AppendChild(xmlRstate);
                            xmlRstate.InnerText = "VI";

                            XmlElement xmlRnation = xmlDoc.CreateElement("Rnation");
                            xmlClient0.AppendChild(xmlRnation);
                            xmlRnation.InnerText = "0";

                            XmlElement xmlRzip = xmlDoc.CreateElement("Rzip");
                            xmlClient0.AppendChild(xmlRzip);
                            xmlRzip.InnerText = "00802";

                            XmlElement xmlWphone = xmlDoc.CreateElement("Wphone");
                            xmlClient0.AppendChild(xmlWphone);
                            xmlWphone.InnerText = "0";

                            XmlElement xmlRphone = xmlDoc.CreateElement("Rphone");
                            xmlClient0.AppendChild(xmlRphone);
                            xmlRphone.InnerText = "0";

                            XmlElement xmlCsbyt = xmlDoc.CreateElement("Csbyt");
                            xmlClient0.AppendChild(xmlCsbyt);
                            xmlCsbyt.InnerText = "0";

                            XmlElement xmlCphone = xmlDoc.CreateElement("Cphone");
                            xmlClient0.AppendChild(xmlCphone);
                            xmlCphone.InnerText = "340-776-7798";

                            XmlElement xmlEaddr = xmlDoc.CreateElement("Eaddr");
                            xmlClient0.AppendChild(xmlEaddr);
                            xmlEaddr.InnerText = rdr["Email"].ToString().Trim();


                        
                    }
                    conn.Close();
                    conn0.Close(); // cierra las conecciones
                    conn1.Close();

                }

                xmlDoc.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + NAMECONVENTION + ".xml"); // save

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




                conn4.Close();
            }
            catch (Exception ecp)
            {
                throw new Exception(ecp.Message.ToString());
            }
        }
    }
}
