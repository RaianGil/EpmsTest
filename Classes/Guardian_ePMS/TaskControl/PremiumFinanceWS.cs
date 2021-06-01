using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using EPolicy.XmlCooker;
using System.Net;

namespace EPolicy.TaskControl
{
    public class PremiumFinanceWS : TaskControl
    {
        public PremiumFinanceWS()
        {
            CreateXMl();
            //CallWs();
        }

        #region Variables
        
        public string _FMQuoteID;
        public string _WSCall;
        public string _WSResult;
        public string _PaymentOption;

        public string FMQuoteID
        {
            get
            {
                return this._FMQuoteID;
            }
            set
            {
                this._FMQuoteID = value;
            }
        }

        public string WSCall
        {
            get
            {
                return this._WSCall;
            }
            set
            {
                this._WSCall = value;
            }
        }

        public string WSResult
        {
            get
            {
                return this._WSResult;
            }
            set
            {
                this._WSResult = value;
            }
        }

        public string PaymentOption
        {
            get
            {
                return this._PaymentOption;
            }
            set
            {
                this._PaymentOption = value;
            }
        }

        string xml = @"<Quote><AgencyCode>A0002</AgencyCode><PolicyNumber>P123456</PolicyNumber><PolicyPremium>1000.00</PolicyPremium>
<PolicyFee>10.00</PolicyFee><PolicyTax>5.00</PolicyTax><PersonalOrCommercial>Commercial</PersonalOrCommercial><NewOrRenewal>N</NewOrRenewal><AssignedRisk>Y</AssignedRisk><Other>N</Other><Auditable>Y</Auditable><ParameterCode>VI</ParameterCode><Term>12</Term><ShortRate>N</ShortRate><WebQuoteODBC>WebQuotesPFC</WebQuoteODBC><FMQODBC>FMQWebDBPFCSQL</FMQODBC><ProcessType>WebService</ProcessType><NumOfPayments>0</NumOfPayments> 
<InsuranceCode>C102</InsuranceCode></Quote>";

        string XMLRECEIVE = @" <string xmlns=""http://microsoft.com/webservices/""><?xml version=""1.0"" encoding=""u -8"" ?
        ><Quote><QuoteCode>**89733</QuoteCode><DownPay10>190.00</DownPay10><NumberOfPay10>10</
        NumberOfPay10><MonthlyPayment10>99.21</MonthlyPayment10><APR10>42.03</APR10><Interest10>142.10</
        Interest10><SetupFee10>25.00</SetupFee10><DownPay9>190.00</DownPay9><NumberOfPay9>9</
        NumberOfPay9><MonthlyPayment9>108.73</MonthlyPayment9><APR9>42.69</APR9><Interest9>128.57</
        Interest9><SetupFee9>25.00</SetupFee9><DownPay8>190.00</DownPay8><NumberOfPay8>8</
        NumberOfPay8><MonthlyPayment8>120.65</MonthlyPayment8><APR8>43.51</APR8><Interest8>115.20</
        Interest8><SetupFee8>25.00</SetupFee8><DownPay6>190.00</DownPay6><NumberOfPay6>6</
        NumberOfPay6><MonthlyPayment6>156.46</MonthlyPayment6><APR6>45.85</APR6><Interest6>88.76</
        Interest6><SetupFee6>25.00</SetupFee6><DownPay4>190.00</DownPay4><NumberOfPay4>4</
        NumberOfPay4><MonthlyPayment4>228.19</MonthlyPayment4><APR4>50.04</APR4><Interest4>62.76</
        Interest4><SetupFee4>25.00</SetupFee4><DownPay2>190.00</DownPay2><NumberOfPay2>2</
        NumberOfPay2><MonthlyPayment2>443.65</MonthlyPayment2><APR2>59.93</APR2><Interest2>37.30</
        Interest2><SetupFee2>25.00</SetupFee2><AmountFinanced>825.00</AmountFinanced><QuoteAgreement>Https://www.fmweb2.com/FMFPI/FMQuoteNewFastUpdate.asp?QuoteCode=**89733</QuoteAgreement></Quote></string> ";

        #endregion Variables

        public DataTable GetPaymentOptions()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load(new StringReader(xml));
            Console.WriteLine(Doc.SelectSingleNode("/Quote/AgencyCode").InnerText);
            Console.WriteLine(Doc.SelectSingleNode("/Quote/QuoteCode").InnerText);
            Console.WriteLine(Doc.SelectSingleNode("/Quote/DownPay10").InnerText);
            Console.WriteLine(Doc.SelectSingleNode("/Quote/NumeberOfPay10").InnerText);

            return null;
        }

        public void CreateXMl()
        {
            using (XmlWriter writer = XmlWriter.Create("FinanceMaster.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Quote");

                //foreach (Employee employee in employees)
                //{
                writer.WriteElementString("AgencyCode", "1234");// <-- These are new
                writer.WriteElementString("PolicyNumber", "P1234567");
                writer.WriteElementString("PolicyPremium", "P1234567");
                writer.WriteElementString("PolicyFee", "4567");
                writer.WriteElementString("PolicyTax", "4567");
                writer.WriteElementString("PersonalOrCommercial", "4567");
                writer.WriteElementString("NewOrRenewal", "4567");

                writer.WriteElementString("AssignedRisk", "4567");
                writer.WriteElementString("Other", "4567");
                writer.WriteElementString("Auditable", "4567");
                writer.WriteElementString("ParameterCode", "4567");
                writer.WriteElementString("Term", "4567");
                writer.WriteElementString("ShortRate", "4567");
                writer.WriteElementString("FMQODBC", "4567");
                writer.WriteElementString("ProcessType", "4567");
                writer.WriteElementString("NumOfPayments", "4567");
                writer.WriteElementString("InsuranceCode", "4567");

                //}

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public DataTable AddFinanceMaster()
        {
            DataTable dt = null;

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[5];
            DbRequestXmlCooker.AttachCookItem("FMQuoteID", SqlDbType.VarChar, 20, this._FMQuoteID, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, this.TaskControlID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("WSCall", SqlDbType.Xml, 0, this._WSCall, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("WSResult", SqlDbType.Xml, 0, this._WSResult, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentOption", SqlDbType.Xml, 0, this._PaymentOption, ref cookItems);

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

        private void CallWs()
        {
            string WSURL = "https://www.fmweb2.com/FMWebSvcXML4Payments/service.asmx/WebFMFastQuote?xmlInput=" + xml;//"ttps://www.fmweb2.com/FMWebSvcXML4Payments/service.asmx?op=WebFMFastQuote";
            //WebRequest request = WebRequest.Create(WSURL);
            //request.Method = "GET";
            //WebResponse response = request.GetResponse();
            WebClient ws = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string st = System.Net.WebUtility.HtmlDecode(ws.DownloadString(WSURL)).ToString();
        }
    }
}
