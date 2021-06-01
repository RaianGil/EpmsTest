using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Quotes;
using EPolicy.Audit;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
    public class DeferredPayment
    {
        public DeferredPayment()
        {

        }

        #region Variable
        private DataTable _dtDeferredPayment;
        private int _DeferredPaymentID = 0;
        private int _TaskControlID = 0;
        private string _EffectiveDate = "";
        private double _TotalPremium = 0.0;
        private double _SSO = 0.0;
        private double _DownPayment = 0.0;
        private double _TotalDownPayment = 0.0;
        private double _FinanceAmount = 0.0;
        private int _Term = 4;
        private string _PaymentDate2 = "";
        private string _PaymentDate3 = "";
        private string _PaymentDate4 = "";
        private string _PaymentDate5 = "";
        private string _PaymentDate6 = "";
        private string _PaymentDate7 = "";
        private string _PaymentDate8 = "";
        private string _PaymentDate9 = "";
        private double _Principal2 = 0.0;
        private double _Principal3 = 0.0;
        private double _Principal4 = 0.0;
        private double _Principal5 = 0.0;
        private double _Principal6 = 0.0;
        private double _Principal7 = 0.0;
        private double _Principal8 = 0.0;
        private double _Principal9 = 0.0;
        private double _Rate2 = 0.0;
        private double _Rate3 = 0.0;
        private double _Rate4 = 0.0;
        private double _Rate5 = 0.0;
        private double _Rate6 = 0.0;
        private double _Rate7 = 0.0;
        private double _Rate8 = 0.0;
        private double _Rate9 = 0.0;
        private double _PaymentAmt2 = 0.0;
        private double _PaymentAmt3 = 0.0;
        private double _PaymentAmt4 = 0.0;
        private double _PaymentAmt5 = 0.0;
        private double _PaymentAmt6 = 0.0;
        private double _PaymentAmt7 = 0.0;
        private double _PaymentAmt8 = 0.0;
        private double _PaymentAmt9 = 0.0;
        private int _Mode = (int)DeferredPaymentMode.CLEAR;
        #endregion

        #region Public Enumeration

        public enum DeferredPaymentMode { ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4 };

        #endregion

        #region Properties
        public int DeferredPaymentID
        {
            get
            {
                return this._DeferredPaymentID;
            }
            set
            {
                this._DeferredPaymentID = value;
            }
        }
        public int TaskControlID
        {
            get
            {
                return this._TaskControlID;
            }
            set
            {
                this._TaskControlID = value;
            }
        }
        public string EffectiveDate
        {
            get
            {
                return this._EffectiveDate;
            }
            set
            {
                this._EffectiveDate = value;
            }
        }
        public double TotalPremium
        {
            get
            {
                return this._TotalPremium;
            }
            set
            {
                this._TotalPremium = value;
            }
        }
        public double SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                this._SSO = value;
            }
        }
        public double DownPayment
        {
            get
            {
                return this._DownPayment;
            }
            set
            {
                this._DownPayment = value;
            }
        }
        public double TotalDownPayment
        {
            get
            {
                return this._TotalDownPayment;
            }
            set
            {
                this._TotalDownPayment = value;
            }
        }
        public double FinanceAmount
        {
            get
            {
                return this._FinanceAmount;
            }
            set
            {
                this._FinanceAmount = value;
            }
        }
        public int Term
        {
            get
            {
                return this._Term;
            }
            set
            {
                this._Term = value;
            }
        }
        public string PaymentDate2
        {
            get
            {
                return this._PaymentDate2;
            }
            set
            {
                this._PaymentDate2 = value;
            }
        }
        public string PaymentDate3
        {
            get
            {
                return this._PaymentDate3;
            }
            set
            {
                this._PaymentDate3 = value;
            }
        }
        public string PaymentDate4
        {
            get
            {
                return this._PaymentDate4;
            }
            set
            {
                this._PaymentDate4 = value;
            }
        }
        public string PaymentDate5
        {
            get
            {
                return this._PaymentDate5;
            }
            set
            {
                this._PaymentDate5 = value;
            }
        }
        public string PaymentDate6
        {
            get
            {
                return this._PaymentDate6;
            }
            set
            {
                this._PaymentDate6 = value;
            }
        }
        public string PaymentDate7
        {
            get
            {
                return this._PaymentDate7;
            }
            set
            {
                this._PaymentDate7 = value;
            }
        }
        public string PaymentDate8
        {
            get
            {
                return this._PaymentDate8;
            }
            set
            {
                this._PaymentDate8 = value;
            }
        }
        public string PaymentDate9
        {
            get
            {
                return this._PaymentDate9;
            }
            set
            {
                this._PaymentDate9 = value;
            }
        }
        public double Principal2
        {
            get
            {
                return this._Principal2;
            }
            set
            {
                this._Principal2 = value;
            }
        }
        public double Principal3
        {
            get
            {
                return this._Principal3;
            }
            set
            {
                this._Principal3 = value;
            }
        }
        public double Principal4
        {
            get
            {
                return this._Principal4;
            }
            set
            {
                this._Principal4 = value;
            }
        }
        public double Principal5
        {
            get
            {
                return this._Principal5;
            }
            set
            {
                this._Principal5 = value;
            }
        }
        public double Principal6
        {
            get
            {
                return this._Principal6;
            }
            set
            {
                this._Principal6 = value;
            }
        }
        public double Principal7
        {
            get
            {
                return this._Principal7;
            }
            set
            {
                this._Principal7 = value;
            }
        }
        public double Principal8
        {
            get
            {
                return this._Principal8;
            }
            set
            {
                this._Principal8 = value;
            }
        }
        public double Principal9
        {
            get
            {
                return this._Principal9;
            }
            set
            {
                this._Principal9 = value;
            }
        }
        public double Rate2
        {
            get
            {
                return this._Rate2;
            }
            set
            {
                this._Rate2 = value;
            }
        }
        public double Rate3
        {
            get
            {
                return this._Rate3;
            }
            set
            {
                this._Rate3 = value;
            }
        }
        public double Rate4
        {
            get
            {
                return this._Rate4;
            }
            set
            {
                this._Rate4 = value;
            }
        }
        public double Rate5
        {
            get
            {
                return this._Rate5;
            }
            set
            {
                this._Rate5 = value;
            }
        }
        public double Rate6
        {
            get
            {
                return this._Rate6;
            }
            set
            {
                this._Rate6 = value;
            }
        }
        public double Rate7
        {
            get
            {
                return this._Rate7;
            }
            set
            {
                this._Rate7 = value;
            }
        }
        public double Rate8
        {
            get
            {
                return this._Rate8;
            }
            set
            {
                this._Rate8 = value;
            }
        }
        public double Rate9
        {
            get
            {
                return this._Rate9;
            }
            set
            {
                this._Rate9 = value;
            }
        }
        public double PaymentAmt2
        {
            get
            {
                return this._PaymentAmt2;
            }
            set
            {
                this._PaymentAmt2 = value;
            }
        }
        public double PaymentAmt3
        {
            get
            {
                return this._PaymentAmt3;
            }
            set
            {
                this._PaymentAmt3 = value;
            }
        }
        public double PaymentAmt4
        {
            get
            {
                return this._PaymentAmt4;
            }
            set
            {
                this._PaymentAmt4 = value;
            }
        }
        public double PaymentAmt5
        {
            get
            {
                return this._PaymentAmt5;
            }
            set
            {
                this._PaymentAmt5 = value;
            }
        }
        public double PaymentAmt6
        {
            get
            {
                return this._PaymentAmt6;
            }
            set
            {
                this._PaymentAmt6 = value;
            }
        }
        public double PaymentAmt7
        {
            get
            {
                return this._PaymentAmt7;
            }
            set
            {
                this._PaymentAmt7 = value;
            }
        }
        public double PaymentAmt8
        {
            get
            {
                return this._PaymentAmt8;
            }
            set
            {
                this._PaymentAmt8 = value;
            }
        }
        public double PaymentAmt9
        {
            get
            {
                return this._PaymentAmt9;
            }
            set
            {
                this._PaymentAmt9 = value;
            }
        }
        public int Mode
        {
            get
            {
                return this._Mode;
            }
            set
            {
                this._Mode = value;
            }
        }
        #endregion

        #region Public Methods
        public static DeferredPayment GetDeferredPaymentByTaskControlID(int TaskControlID)
        {
            DeferredPayment deferredPayment = null;

            DataTable dt = GetDeferredPaymentByTaskControlIDDB(TaskControlID);
            deferredPayment = new DeferredPayment();
            deferredPayment._dtDeferredPayment = dt;

            if (dt.Rows.Count != 0)
            {
                deferredPayment = FillProperties(deferredPayment);
            }
            else
            {
                deferredPayment.Mode = 1;
            }

            return deferredPayment;
        }

        public void SaveDeferredPayment(int UserID)
        {
            this.Save(UserID);
        }
        #endregion

        #region Private Methods
        private void Save(int UserID)
        {
            //if (this.Mode == 2)
            //    oldUmbrella = (Umbrella)Umbrella.GetUmbrellaByTaskControlID(this.TaskControlID, IsOppQuote);

            DBRequest Executor = new DBRequest();
            try
            {
                Executor.BeginTrans();

                switch (this.Mode)
                {
                    case 1:  //ADD
                        this.DeferredPaymentID = Executor.Insert("AddDeferredPayment", this.GetInsertDeferredPaymentXml());
                        break;
                    default: //UPDATE
                        //this.History(this._mode, UserID);
                        Executor.Update("UpdateDeferredPayment", this.GetUpdateDeferredPaymentXml());
                        break;
                }
                Executor.CommitTrans();
            }

            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the umbrella Policy. " + xcp.Message, xcp);
            }

            this.Mode = 4;
        }

        private XmlDocument GetInsertDeferredPaymentXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[40];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
                SqlDbType.Float, 0, this.TotalPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SSO",
                SqlDbType.Float, 0, this.SSO.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DownPayment",
                SqlDbType.Float, 0, this.DownPayment.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalDownPayment",
                SqlDbType.Float, 0, this.TotalDownPayment.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FinanceAmount",
                SqlDbType.Float, 0, this.FinanceAmount.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
                SqlDbType.Int, 0, this.Term.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate2",
                SqlDbType.DateTime, 0, this.PaymentDate2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate3",
                SqlDbType.DateTime, 0, this.PaymentDate3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate4",
                SqlDbType.DateTime, 0, this.PaymentDate4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate5",
                SqlDbType.DateTime, 0, this.PaymentDate5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate6",
                SqlDbType.DateTime, 0, this.PaymentDate6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate7",
                SqlDbType.DateTime, 0, this.PaymentDate7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate8",
                SqlDbType.DateTime, 0, this.PaymentDate8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate9",
                SqlDbType.DateTime, 0, this.PaymentDate9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal2",
                SqlDbType.Float, 0, this.Principal2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal3",
                SqlDbType.Float, 0, this.Principal3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal4",
                SqlDbType.Float, 0, this.Principal4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal5",
                SqlDbType.Float, 0, this.Principal5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal6",
                SqlDbType.Float, 0, this.Principal6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal7",
                SqlDbType.Float, 0, this.Principal7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal8",
                SqlDbType.Float, 0, this.Principal8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal9",
                SqlDbType.Float, 0, this.Principal9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate2",
                SqlDbType.Float, 0, this.Rate2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate3",
                SqlDbType.Float, 0, this.Rate3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate4",
                SqlDbType.Float, 0, this.Rate4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate5",
                SqlDbType.Float, 0, this.Rate5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate6",
                SqlDbType.Float, 0, this.Rate6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate7",
                SqlDbType.Float, 0, this.Rate7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate8",
                SqlDbType.Float, 0, this.Rate8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate9",
                SqlDbType.Float, 0, this.Rate9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt2",
                SqlDbType.Float, 0, this.PaymentAmt2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt3",
                SqlDbType.Float, 0, this.PaymentAmt3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt4",
                SqlDbType.Float, 0, this.PaymentAmt4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt5",
                SqlDbType.Float, 0, this.PaymentAmt5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt6",
                SqlDbType.Float, 0, this.PaymentAmt6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt7",
                SqlDbType.Float, 0, this.PaymentAmt7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt8",
                SqlDbType.Float, 0, this.PaymentAmt8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt9",
                SqlDbType.Float, 0, this.PaymentAmt9.ToString(),
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

            return xmlDoc;
        }

        private XmlDocument GetUpdateDeferredPaymentXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[40];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
                SqlDbType.Float, 0, this.TotalPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SSO",
                SqlDbType.Float, 0, this.SSO.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DownPayment",
                SqlDbType.Float, 0, this.DownPayment.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalDownPayment",
                SqlDbType.Float, 0, this.TotalDownPayment.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FinanceAmount",
                SqlDbType.Float, 0, this.FinanceAmount.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
                SqlDbType.Int, 0, this.Term.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate2",
                SqlDbType.DateTime, 0, this.PaymentDate2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate3",
                SqlDbType.DateTime, 0, this.PaymentDate3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate4",
                SqlDbType.DateTime, 0, this.PaymentDate4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate5",
                SqlDbType.DateTime, 0, this.PaymentDate5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate6",
                SqlDbType.DateTime, 0, this.PaymentDate6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate7",
                SqlDbType.DateTime, 0, this.PaymentDate7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate8",
                SqlDbType.DateTime, 0, this.PaymentDate8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentDate9",
                SqlDbType.DateTime, 0, this.PaymentDate9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal2",
                SqlDbType.Float, 0, this.Principal2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal3",
                SqlDbType.Float, 0, this.Principal3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal4",
                SqlDbType.Float, 0, this.Principal4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal5",
                SqlDbType.Float, 0, this.Principal5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal6",
                SqlDbType.Float, 0, this.Principal6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal7",
                SqlDbType.Float, 0, this.Principal7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal8",
                SqlDbType.Float, 0, this.Principal8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Principal9",
                SqlDbType.Float, 0, this.Principal9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate2",
                SqlDbType.Float, 0, this.Rate2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate3",
                SqlDbType.Float, 0, this.Rate3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate4",
                SqlDbType.Float, 0, this.Rate4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate5",
                SqlDbType.Float, 0, this.Rate5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate6",
                SqlDbType.Float, 0, this.Rate6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate7",
                SqlDbType.Float, 0, this.Rate7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate8",
                SqlDbType.Float, 0, this.Rate8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate9",
                SqlDbType.Float, 0, this.Rate9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt2",
                SqlDbType.Float, 0, this.PaymentAmt2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt3",
                SqlDbType.Float, 0, this.PaymentAmt3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt4",
                SqlDbType.Float, 0, this.PaymentAmt4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt5",
                SqlDbType.Float, 0, this.PaymentAmt5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt6",
                SqlDbType.Float, 0, this.PaymentAmt6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt7",
                SqlDbType.Float, 0, this.PaymentAmt7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt8",
                SqlDbType.Float, 0, this.PaymentAmt8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmt9",
                SqlDbType.Float, 0, this.PaymentAmt9.ToString(),
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

            return xmlDoc;
        }

        public static DataTable GetDeferredPaymentByTaskControlIDDB(int TaskControlID)
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
                dt = exec.GetQuery("GetDeferredPaymentByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the data by criteria.", ex);
            }
        }

        private static DeferredPayment FillProperties(DeferredPayment deferredPayment)
        {
            deferredPayment.DeferredPaymentID = (deferredPayment._dtDeferredPayment.Rows[0]["DeferredPaymentID"] != System.DBNull.Value) ? (int)deferredPayment._dtDeferredPayment.Rows[0]["DeferredPaymentID"] : 0;
            deferredPayment.TaskControlID = (deferredPayment._dtDeferredPayment.Rows[0]["TaskControlID"] != System.DBNull.Value) ? (int)deferredPayment._dtDeferredPayment.Rows[0]["TaskControlID"] : 0;
            deferredPayment.EffectiveDate = (deferredPayment._dtDeferredPayment.Rows[0]["EffectiveDate"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["EffectiveDate"]).ToShortDateString() : "";
            deferredPayment.TotalPremium = (deferredPayment._dtDeferredPayment.Rows[0]["TotalPremium"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["TotalPremium"] : 0.00;
            deferredPayment.SSO = (deferredPayment._dtDeferredPayment.Rows[0]["SSO"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["SSO"] : 0.00;
            deferredPayment.DownPayment = (deferredPayment._dtDeferredPayment.Rows[0]["DownPayment"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["DownPayment"] : 0.00;
            deferredPayment.TotalDownPayment = (deferredPayment._dtDeferredPayment.Rows[0]["TotalDownPayment"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["TotalDownPayment"] : 0.00;
            deferredPayment.FinanceAmount = (deferredPayment._dtDeferredPayment.Rows[0]["FinanceAmount"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["FinanceAmount"] : 0.00;
            deferredPayment.Term = (deferredPayment._dtDeferredPayment.Rows[0]["Term"] != System.DBNull.Value) ? (int)deferredPayment._dtDeferredPayment.Rows[0]["Term"] : 0;
            deferredPayment.PaymentDate2 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate2"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate2"]).ToShortDateString() : "";
            deferredPayment.PaymentDate3 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate3"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate3"]).ToShortDateString() : "";
            deferredPayment.PaymentDate4 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate4"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate4"]).ToShortDateString() : "";
            deferredPayment.PaymentDate5 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate5"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate5"]).ToShortDateString() : "";
            deferredPayment.PaymentDate6 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate6"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate6"]).ToShortDateString() : "";
            deferredPayment.PaymentDate7 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate7"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate7"]).ToShortDateString() : "";
            deferredPayment.PaymentDate8 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate8"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate8"]).ToShortDateString() : "";
            deferredPayment.PaymentDate9 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate9"] != System.DBNull.Value) ? ((DateTime)deferredPayment._dtDeferredPayment.Rows[0]["PaymentDate9"]).ToShortDateString() : "";
            deferredPayment.Principal2 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal2"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal2"] : 0.00;
            deferredPayment.Principal3 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal3"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal3"] : 0.00;
            deferredPayment.Principal4 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal4"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal4"] : 0.00;
            deferredPayment.Principal5 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal5"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal5"] : 0.00;
            deferredPayment.Principal6 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal6"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal6"] : 0.00;
            deferredPayment.Principal7 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal7"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal7"] : 0.00;
            deferredPayment.Principal8 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal8"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal8"] : 0.00;
            deferredPayment.Principal9 = (deferredPayment._dtDeferredPayment.Rows[0]["Principal9"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Principal9"] : 0.00;
            deferredPayment.Rate2 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate2"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate2"] : 0.00;
            deferredPayment.Rate3 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate3"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate3"] : 0.00;
            deferredPayment.Rate4 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate4"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate4"] : 0.00;
            deferredPayment.Rate5 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate5"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate5"] : 0.00;
            deferredPayment.Rate6 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate6"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate6"] : 0.00;
            deferredPayment.Rate7 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate7"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate7"] : 0.00;
            deferredPayment.Rate8 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate8"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate8"] : 0.00;
            deferredPayment.Rate9 = (deferredPayment._dtDeferredPayment.Rows[0]["Rate9"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["Rate9"] : 0.00;
            deferredPayment.PaymentAmt2 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt2"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt2"] : 0.00;
            deferredPayment.PaymentAmt3 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt3"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt3"] : 0.00;
            deferredPayment.PaymentAmt4 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt4"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt4"] : 0.00;
            deferredPayment.PaymentAmt5 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt5"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt5"] : 0.00;
            deferredPayment.PaymentAmt6 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt6"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt6"] : 0.00;
            deferredPayment.PaymentAmt7 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt7"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt7"] : 0.00;
            deferredPayment.PaymentAmt8 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt8"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt8"] : 0.00;
            deferredPayment.PaymentAmt9 = (deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt9"] != System.DBNull.Value) ? (double)deferredPayment._dtDeferredPayment.Rows[0]["PaymentAmt9"] : 0.00;

            return deferredPayment;
        }
        #endregion

    }
}
