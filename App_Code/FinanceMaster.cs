using EPolicy.XmlCooker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FinanceMaster
/// </summary>
public class FinanceMaster
{

    string Result = "";
    string WSURL = "";
    public string AgencyCode     { get; set; }
    public string PolicyNumber   { get; set; }
    public string PolicyPremium  { get; set; }
    public string PolicyFee      { get; set; }
    public string PolicyTax      { get; set; }
    public string PersonalOrComm { get; set; }
    public string NewOrRenewal   { get; set; }
    public string AssignedRisk   { get; set; }
    public string Othernner      { get; set; }
    public string Auditable      { get; set; }
    public string ParameterCode  { get; set; }
    public string Term           { get; set; }
    public string ShortRate      { get; set; }
    public string WebQuoteODBC   { get; set; }
    public string FMQODBCnn      { get; set; }
    public string ProcessType    { get; set; }
    public string NumOfPayments  { get; set; }
    public string InsuranceCode  { get; set; }
    public string Lien1          { get; set; }
    public string Lien1Name      { get; set; }
    public string Lien1Address   { get; set; }
    public string Lien1City      { get; set; }
    public string Lien1State     { get; set; }
    public string Lien1Zip       { get; set; }
    public string Lien1b         { get; set; }
    public string Lien1bName     { get; set; }
    public string Lien1bAddress  { get; set; }
    public string Lien1bCity     { get; set; }
    public string Lien1bState    { get; set; }
    public string Lien1bZip      { get; set; }
    public string Coverage1      { get; set; }

    public FinanceMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public class Bank
    {
        public Bank()
        {
            BankID  = "";
            PPSID   = "";
            FMID    = "";
            Name    = "";
            Address = "";
            Address2= "";
            City    = "";
            State   = "";
            Zip     = "";
        }

        public string BankID    { get; set; }
        public string PPSID     { get; set; }
        public string FMID      { get; set; }
        public string Name      { get; set; }
        public string Address   { get; set; }
        public string Address2  { get; set; }
        public string City      { get; set; }
        public string State     { get; set; }
        public string Zip       { get; set; }
    }

    public string GetPremiumFinanceWs()
    {
        Result = "";
        WSURL = "https://www.fmweb2.com/FMWebSvcXML4Payments/service.asmx/WebFMFastQuote?xmlInput=" + CreateXMlFM();
        WebClient ws = new WebClient();// Encoding = System.Text.Encoding.UTF8 };
        Result = ws.DownloadString(WSURL);//System.Net.WebUtility.HtmlDecode(ws.DownloadString(WSURL)).ToString();
        if (Result.ToUpper().Contains("WEBSERVICE_ERROR") || Result.ToUpper().Contains("ERROR"))
        { 
            if(Result.Contains("<string"))
                throw new Exception(RemoveXmlns(Result).InnerXml.Replace("<string xmlns=\"http://microsoft.com/webservices/\">", "").Replace("</string>", ""));
            else
                throw new Exception("WEBSERVICE_ERROR");
        }
            
        return Result;
    }

    public string CreateXMlFM()
    {
        
        XmlDocument doc = new XmlDocument();
        XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("Quote"));
        //el.SetAttribute("Bar", "some & value");
        el.AppendChild(doc.CreateElement("AgencyCode")).InnerText           = this.AgencyCode    ; 
        el.AppendChild(doc.CreateElement("PolicyNumber")).InnerText         = this.PolicyNumber  ; 
        el.AppendChild(doc.CreateElement("PolicyPremium")).InnerText        = this.PolicyPremium ; 
        el.AppendChild(doc.CreateElement("PolicyFee")).InnerText            = this.PolicyFee     ; 
        el.AppendChild(doc.CreateElement("PolicyTax")).InnerText            = this.PolicyTax     ; 
        el.AppendChild(doc.CreateElement("PersonalOrCommercial")).InnerText = this.PersonalOrComm; 
        el.AppendChild(doc.CreateElement("NewOrRenewal")).InnerText         = this.NewOrRenewal  ; 
        el.AppendChild(doc.CreateElement("AssignedRisk")).InnerText         = this.AssignedRisk  ; 
        el.AppendChild(doc.CreateElement("Other")).InnerText                = this.Othernner     ; 
        el.AppendChild(doc.CreateElement("Auditable")).InnerText            = this.Auditable     ; 
        el.AppendChild(doc.CreateElement("ParameterCode")).InnerText        = this.ParameterCode ; 
        el.AppendChild(doc.CreateElement("Term")).InnerText                 = this.Term          ; 
        el.AppendChild(doc.CreateElement("ShortRate")).InnerText            = this.ShortRate     ; 
        el.AppendChild(doc.CreateElement("WebQuoteODBC")).InnerText         = this.WebQuoteODBC  ; 
        el.AppendChild(doc.CreateElement("FMQODBC")).InnerText              = this.FMQODBCnn     ; 
        el.AppendChild(doc.CreateElement("ProcessType")).InnerText          = this.ProcessType   ; 
        el.AppendChild(doc.CreateElement("NumOfPayments")).InnerText        = this.NumOfPayments ;
        el.AppendChild(doc.CreateElement("InsuranceCode")).InnerText        = this.InsuranceCode ;
        el.AppendChild(doc.CreateElement("Lien1")).InnerText                = this.Lien1         ;

		string lienNameTemp = this.Lien1Name;
        if (this.Lien1Name.Length > 35)
            lienNameTemp = this.Lien1Name.Substring(0, 35);
		
        el.AppendChild(doc.CreateElement("Lien1Name")).InnerText            = lienNameTemp; 
        el.AppendChild(doc.CreateElement("Lien1Address")).InnerText         = this.Lien1Address  ; 
        el.AppendChild(doc.CreateElement("Lien1City")).InnerText            = this.Lien1City     ; 
        el.AppendChild(doc.CreateElement("Lien1State")).InnerText           = this.Lien1State    ; 
        el.AppendChild(doc.CreateElement("Lien1Zip")).InnerText             = this.Lien1Zip      ; 
        el.AppendChild(doc.CreateElement("Lien1b")).InnerText               = this.Lien1b        ; 
        el.AppendChild(doc.CreateElement("Lien1bName")).InnerText           = this.Lien1bName    ; 
        el.AppendChild(doc.CreateElement("Lien1bAddress")).InnerText        = this.Lien1bAddress ; 
        el.AppendChild(doc.CreateElement("Lien1bCity")).InnerText           = this.Lien1bCity    ; 
        el.AppendChild(doc.CreateElement("Lien1bState")).InnerText          = this.Lien1bState   ; 
        el.AppendChild(doc.CreateElement("Lien1bZip")).InnerText            = this.Lien1bZip     ;
        el.AppendChild(doc.CreateElement("Coverage1")).InnerText            = this.Coverage1     ; 
        return doc.OuterXml.ToString();
    }

    public void FinanceMasterWS(string FMQuoteCode, string Params, int NumeberOfPay, out string redirUrl, int TaskControlID, string Installment, string DownPayment, string UnpaidBalance, string Charge, string TotalPayment)
    {
        Result = "";
        WSURL = "https://www.fmweb2.com/FMPFC/FMQuoteNewFastUpdate.asp?QuoteCode=" + Params;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(WSURL);
        request.AllowAutoRedirect = false;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //Show the redirected url
        redirUrl = response.Headers["Location"];
        response.Close();
        AddFinanceMaster(FMQuoteCode, TaskControlID, NumeberOfPay, redirUrl, Installment, DownPayment, UnpaidBalance, Charge, TotalPayment);
    }

    public System.Data.DataTable AddFinanceMaster(string FMQuoteID, int TaskControlID, int NumberOfPayments, string FMPDF, string Installment, string DownPayment, string UnpaidBalance, string Charge, string TotalPayment)
    {
        System.Data.DataTable dt = null;

        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[9];
        DbRequestXmlCooker.AttachCookItem("FMQuoteID", SqlDbType.VarChar, 20, FMQuoteID, ref cookItems);
        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("NumberOfPayments", SqlDbType.Int, 0, NumberOfPayments.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("FMPDF", SqlDbType.VarChar, 0, FMPDF, ref cookItems);
        DbRequestXmlCooker.AttachCookItem("Installment", SqlDbType.VarChar, 10, Installment, ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DownPayment", SqlDbType.VarChar, 10, DownPayment, ref cookItems);
        DbRequestXmlCooker.AttachCookItem("UnpaidBalance", SqlDbType.VarChar, 10, UnpaidBalance, ref cookItems);
        DbRequestXmlCooker.AttachCookItem("Charge", SqlDbType.VarChar, 10, Charge, ref cookItems);
        DbRequestXmlCooker.AttachCookItem("TotalPayment", SqlDbType.VarChar, 10, TotalPayment, ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;
        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            dt = null;
            dt = exec.GetQuery("AddFinanceMaster", xmlDoc);
        }
        catch (Exception exp)
        {
            throw;
        }

        return dt;
    }

    public System.Data.DataTable GetFinanceMasterByTaskcontrolIDAllData(int TaskControlID)
    {
        System.Data.DataTable dt = null;

        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;
        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            dt = null;
            dt = exec.GetQuery("GetFinanceMasterByTaskcontrolIDAllData", xmlDoc);
        }
        catch (Exception exp)
        {
            throw;
        }

        return dt;
    }


    public System.Data.DataTable GetFinanceMasterByTaskcontrolID(int TaskControlID)
    {
        System.Data.DataTable dt = null;

        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;
        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            dt = null;
            dt = exec.GetQuery("GetFinanceMasterByTaskcontrolID", xmlDoc);
        }
        catch (Exception exp)
        {
            throw;
        }

        return dt;
    }

    public XmlDocument RemoveXmlns(String xml)
    {
        XDocument d = XDocument.Parse(xml);
        d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

        foreach (var elem in d.Descendants())
            elem.Name = elem.Name.LocalName;

        var xmlDocument = new XmlDocument();
        xmlDocument.Load(d.CreateReader());

        return xmlDocument;
    }

    public string GetFMID(string Table, string WhereClause)
    {
        try
        {
            string Result = "";
            string conString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();

            DataTable table = new DataTable();
            using (var con = new SqlConnection(conString))
            using (var cmd = new SqlCommand(
                    String.Format(@"SELECT top 1 * FROM {0} WHERE {1}"
                                    , Table, WhereClause), con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            if (table.Rows.Count > 0)
            {
                var FMID = table.Rows[0]["FMID"].ToString();

                Result = FMID;
            }
            else
            {
                Result = "";
            }

            return Result;
        }
        catch (Exception)
        {
            return "";
        }
    }
}
