using System;
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
    public class Umbrella
    {
        public Umbrella()
        {

        }

        #region Variable
        private Umbrella oldUmbrella = null;
        private DataTable _dtUmbrella;
        private int _UmbrellaID = 0;
        private int _TaskControlID = 0;
        private int _UmbrellaLimit = 0;

        private int _OppAdditionalResidence = 0;
        private int _OppResidenceRented = 0;
        private int _OppAdditionalAuto = 0;
        private int _OppYoungDrivers = 0;
        private int _OppRecreationalVehicles = 0;
        private int _OppSwimmingPool = 0;
        private int _OppWatercraft1 = 0;
        private int _OppWatercraft2 = 0;
        private int _OppWatercraft3 = 0;

        private int _OtherAdditionalResidence = 0;
        private int _OtherResidenceRented = 0;
        private int _OtherAdditionalAuto = 0;
        private int _OtherYoungDrivers = 0;
        private int _OtherRecreationalVehicles = 0;
        private int _OtherSwimmingPool = 0;
        private int _OtherWatercraft1 = 0;
        private int _OtherWatercraft2 = 0;
        private int _OtherWatercraft3 = 0;

        private double _CostAdditionalResidence = 0.00;
        private double _CostResidenceRented = 0.00;
        private double _CostAdditionalAuto = 0.00;
        private double _CostYoungDrivers = 0.00;
        private double _CostRecreationalVehicles = 0.00;
        private double _CostSwimmingPool = 0.00;
        private double _CostWatercraft1 = 0.00;
        private double _CostWatercraft2 = 0.00;
        private double _CostWatercraft3 = 0.00;

        private double _TotalAdditionalResidence = 0.00;
        private double _TotalResidenceRented = 0.00;
        private double _TotalAdditionalAuto = 0.00;
        private double _TotalYoungDrivers = 0.00;
        private double _TotalRecreationalVehicles = 0.00;
        private double _TotalSwimmingPool = 0.00;
        private double _TotalWatercraft1 = 0.00;
        private double _TotalWatercraft2 = 0.00;
        private double _TotalWatercraft3 = 0.00;

        private double _BasicPremium = 0.00;

        private string _EffDt1 = "";
        private string _EffDt2 = "";
        private string _EffDt3 = "";
        private string _EffDt4 = "";
        private string _ExpDt1 = "";
        private string _ExpDt2 = "";
        private string _ExpDt3 = "";
        private string _ExpDt4 = "";
        private string _LiabilityLimit1 = "";
        private string _LiabilityLimit2 = "";
        private string _LiabilityLimit3 = "";
        private string _LiabilityLimit4 = "";
        private string _PolicyNo1 = "";
        private string _PolicyNo2 = "";
        private string _PolicyNo3 = "";
        private string _PolicyNo4 = "";

        private double _TotalUmbrellaPremium = 0.00;

        private int _Mode = (int)UmbrellaMode.CLEAR;
   
        #endregion

        #region Public Enumeration

        public enum UmbrellaMode { ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4 };

        #endregion

        #region Properties

        public int UmbrellaID
        {
            get
            {
                return this._UmbrellaID;
            }
            set
            {
                this._UmbrellaID = value;
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

        public int UmbrellaLimit
        {
            get
            {
                return this._UmbrellaLimit;
            }
            set
            {
                this._UmbrellaLimit = value;
            }
        }

        public int OppAdditionalResidence
        {
            get
            {
                return this._OppAdditionalResidence;
            }
            set
            {
                this._OppAdditionalResidence = value;
            }
        }
        public int OppResidenceRented
        {
            get
            {
                return this._OppResidenceRented;
            }
            set
            {
                this._OppResidenceRented = value;
            }
        }
        public int OppAdditionalAuto
        {
            get
            {
                return this._OppAdditionalAuto;
            }
            set
            {
                this._OppAdditionalAuto = value;
            }
        }
        public int OppYoungDrivers
        {
            get
            {
                return this._OppYoungDrivers;
            }
            set
            {
                this._OppYoungDrivers = value;
            }
        }
        public int OppRecreationalVehicles
        {
            get
            {
                return this._OppRecreationalVehicles;
            }
            set
            {
                this._OppRecreationalVehicles = value;
            }
        }
        public int OppSwimmingPool
        {
            get
            {
                return this._OppSwimmingPool;
            }
            set
            {
                this._OppSwimmingPool = value;
            }
        }
        public int OppWatercraft1
        {
            get
            {
                return this._OppWatercraft1;
            }
            set
            {
                this._OppWatercraft1 = value;
            }
        }
        public int OppWatercraft2
        {
            get
            {
                return this._OppWatercraft2;
            }
            set
            {
                this._OppWatercraft2 = value;
            }
        }
        public int OppWatercraft3
        {
            get
            {
                return this._OppWatercraft3;
            }
            set
            {
                this._OppWatercraft3 = value;
            }
        }

        public int OtherAdditionalResidence
        {
            get
            {
                return this._OtherAdditionalResidence;
            }
            set
            {
                this._OtherAdditionalResidence = value;
            }
        }
        public int OtherResidenceRented
        {
            get
            {
                return this._OtherResidenceRented;
            }
            set
            {
                this._OtherResidenceRented = value;
            }
        }
        public int OtherAdditionalAuto
        {
            get
            {
                return this._OtherAdditionalAuto;
            }
            set
            {
                this._OtherAdditionalAuto = value;
            }
        }
        public int OtherYoungDrivers
        {
            get
            {
                return this._OtherYoungDrivers;
            }
            set
            {
                this._OtherYoungDrivers = value;
            }
        }
        public int OtherRecreationalVehicles
        {
            get
            {
                return this._OtherRecreationalVehicles;
            }
            set
            {
                this._OtherRecreationalVehicles = value;
            }
        }
        public int OtherSwimmingPool
        {
            get
            {
                return this._OtherSwimmingPool;
            }
            set
            {
                this._OtherSwimmingPool = value;
            }
        }
        public int OtherWatercraft1
        {
            get
            {
                return this._OtherWatercraft1;
            }
            set
            {
                this._OtherWatercraft1 = value;
            }
        }
        public int OtherWatercraft2
        {
            get
            {
                return this._OtherWatercraft2;
            }
            set
            {
                this._OtherWatercraft2 = value;
            }
        }
        public int OtherWatercraft3
        {
            get
            {
                return this._OtherWatercraft3;
            }
            set
            {
                this._OtherWatercraft3 = value;
            }
        }

        public double CostAdditionalResidence
        {
            get
            {
                return this._CostAdditionalResidence;
            }
            set
            {
                this._CostAdditionalResidence = value;
            }
        }
        public double CostResidenceRented
        {
            get
            {
                return this._CostResidenceRented;
            }
            set
            {
                this._CostResidenceRented = value;
            }
        }
        public double CostAdditionalAuto
        {
            get
            {
                return this._CostAdditionalAuto;
            }
            set
            {
                this._CostAdditionalAuto = value;
            }
        }
        public double CostYoungDrivers
        {
            get
            {
                return this._CostYoungDrivers;
            }
            set
            {
                this._CostYoungDrivers = value;
            }
        }
        public double CostRecreationalVehicles
        {
            get
            {
                return this._CostRecreationalVehicles;
            }
            set
            {
                this._CostRecreationalVehicles = value;
            }
        }
        public double CostSwimmingPool
        {
            get
            {
                return this._CostSwimmingPool;
            }
            set
            {
                this._CostSwimmingPool = value;
            }
        }
        public double CostWatercraft1
        {
            get
            {
                return this._CostWatercraft1;
            }
            set
            {
                this._CostWatercraft1 = value;
            }
        }
        public double CostWatercraft2
        {
            get
            {
                return this._CostWatercraft2;
            }
            set
            {
                this._CostWatercraft2 = value;
            }
        }
        public double CostWatercraft3
        {
            get
            {
                return this._CostWatercraft3;
            }
            set
            {
                this._CostWatercraft3 = value;
            }
        }

        public double TotalAdditionalResidence
        {
            get
            {
                return this._TotalAdditionalResidence;
            }
            set
            {
                this._TotalAdditionalResidence = value;
            }
        }
        public double TotalResidenceRented
        {
            get
            {
                return this._TotalResidenceRented;
            }
            set
            {
                this._TotalResidenceRented = value;
            }
        }
        public double TotalAdditionalAuto
        {
            get
            {
                return this._TotalAdditionalAuto;
            }
            set
            {
                this._TotalAdditionalAuto = value;
            }
        }
        public double TotalYoungDrivers
        {
            get
            {
                return this._TotalYoungDrivers;
            }
            set
            {
                this._TotalYoungDrivers = value;
            }
        }
        public double TotalRecreationalVehicles
        {
            get
            {
                return this._TotalRecreationalVehicles;
            }
            set
            {
                this._TotalRecreationalVehicles = value;
            }
        }
        public double TotalSwimmingPool
        {
            get
            {
                return this._TotalSwimmingPool;
            }
            set
            {
                this._TotalSwimmingPool = value;
            }
        }
        public double TotalWatercraft1
        {
            get
            {
                return this._TotalWatercraft1;
            }
            set
            {
                this._TotalWatercraft1 = value;
            }
        }
        public double TotalWatercraft2
        {
            get
            {
                return this._TotalWatercraft2;
            }
            set
            {
                this._TotalWatercraft2 = value;
            }
        }
        public double TotalWatercraft3
        {
            get
            {
                return this._TotalWatercraft3;
            }
            set
            {
                this._TotalWatercraft3 = value;
            }
        }

        public double BasicPremium
        {
            get
            {
                return this._BasicPremium;
            }
            set
            {
                this._BasicPremium = value;
            }
        }

        
        public double TotalUmbrellaPremium
        {
            get
            {
                return this._TotalUmbrellaPremium;
            }
            set
            {
                this._TotalUmbrellaPremium = value;
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

        public string EffDt1
        {
            get
            {
                return this._EffDt1;
            }
            set
            {
                this._EffDt1 = value;
            }
        }

        public string EffDt2
        {
            get
            {
                return this._EffDt2;
            }
            set
            {
                this._EffDt2 = value;
            }
        }

        public string EffDt3
        {
            get
            {
                return this._EffDt3;
            }
            set
            {
                this._EffDt3 = value;
            }
        }

        public string EffDt4
        {
            get
            {
                return this._EffDt4;
            }
            set
            {
                this._EffDt4 = value;
            }
        }

        public string ExpDt1
        {
            get
            {
                return this._ExpDt1;
            }
            set
            {
                this._ExpDt1 = value;
            }
        }

        public string ExpDt2
        {
            get
            {
                return this._ExpDt2;
            }
            set
            {
                this._ExpDt2 = value;
            }
        }

        public string ExpDt3
        {
            get
            {
                return this._ExpDt3;
            }
            set
            {
                this._ExpDt3 = value;
            }
        }

        public string ExpDt4
        {
            get
            {
                return this._ExpDt4;
            }
            set
            {
                this._ExpDt4 = value;
            }
        }

        public string LiabilityLimit1
        {
            get
            {
                return this._LiabilityLimit1;
            }
            set
            {
                this._LiabilityLimit1 = value;
            }
        }

        public string LiabilityLimit2
        {
            get
            {
                return this._LiabilityLimit2;
            }
            set
            {
                this._LiabilityLimit2 = value;
            }
        }

        public string LiabilityLimit3
        {
            get
            {
                return this._LiabilityLimit3;
            }
            set
            {
                this._LiabilityLimit3 = value;
            }
        }

        public string LiabilityLimit4
        {
            get
            {
                return this._LiabilityLimit4;
            }
            set
            {
                this._LiabilityLimit4 = value;
            }
        }

        public string PolicyNo1
        {
            get
            {
                return this._PolicyNo1;
            }
            set
            {
                this._PolicyNo1 = value;
            }
        }

        public string PolicyNo2
        {
            get
            {
                return this._PolicyNo2;
            }
            set
            {
                this._PolicyNo2 = value;
            }
        }

        public string PolicyNo3
        {
            get
            {
                return this._PolicyNo3;
            }
            set
            {
                this._PolicyNo3 = value;
            }
        }

        public string PolicyNo4
        {
            get
            {
                return this._PolicyNo4;
            }
            set
            {
                this._PolicyNo4 = value;
            }
        }

        #endregion

        #region Public Methods

        public static Umbrella GetUmbrellaByTaskControlID(int TaskControlID, bool IsOppQuote)
        {
            Umbrella umbrella = null;

            DataTable dt = GetUmbrellaByTaskControlIDDB(TaskControlID, IsOppQuote);
            umbrella = new Umbrella();

            umbrella._dtUmbrella = dt;
            
            if(dt.Rows.Count != 0)
                umbrella = FillProperties(umbrella, IsOppQuote);

            return umbrella;
        }

        public void SaveUmbrella(int UserID, bool IsOppQuote)
        {
            this.Save(UserID, IsOppQuote);
        }

        public static void DeleteUmbrellaByTaskControlID(int TaskControlID, bool IsOppQuote)
        {
            DBRequest Executor = new DBRequest();
            Executor.Update("DeleteUmbrellaByTaskControlID", DeleteUmbrellaByTaskControlIDXml(TaskControlID, IsOppQuote));
        }

        #endregion

        #region Private Methods

        private static XmlDocument DeleteUmbrellaByTaskControlIDXml(int TaskControlID, bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOppQuote.ToString(),
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

        private void Save(int UserID, bool IsOppQuote)
        {
            //if (this.Mode == 2)
            //    oldUmbrella = (Umbrella)Umbrella.GetUmbrellaByTaskControlID(this.TaskControlID, IsOppQuote);

            DBRequest Executor = new DBRequest();
            try
            {
                Executor.BeginTrans();

                this.UmbrellaID = Executor.Insert("AddUmbrella", this.GetInsertUmbrellaXml(IsOppQuote));
   
                Executor.CommitTrans();
            }

            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the umbrella Policy. " + xcp.Message, xcp);
            }

            this.Mode = 4;
        }

        private XmlDocument GetUpdateUmbrellaXml(bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[58];

            DbRequestXmlCooker.AttachCookItem("UmbrellaID",
                SqlDbType.Int, 0, this.UmbrellaID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UmbrellaLimit",
                SqlDbType.Int, 0, this.UmbrellaLimit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppAdditionalResidence",
                SqlDbType.Int, 0, this.OppAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppResidenceRented",
                SqlDbType.Int, 0, this.OppResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppAdditionalAuto",
                SqlDbType.Int, 0, this.OppAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppYoungDrivers",
                SqlDbType.Int, 0, this.OppYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppRecreationalVehicles",
                SqlDbType.Int, 0, this.OppRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppSwimmingPool",
                SqlDbType.Int, 0, this.OppSwimmingPool.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppWatercraft1",
                SqlDbType.Int, 0, this.OppWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppWatercraft2",
                SqlDbType.Int, 0, this.OppWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppWatercraft3",
                SqlDbType.Int, 0, this.OppWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherAdditionalResidence",
                SqlDbType.Int, 0, this.OtherAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherResidenceRented",
                SqlDbType.Int, 0, this.OtherResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherAdditionalAuto",
                SqlDbType.Int, 0, this.OtherAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherYoungDrivers",
                SqlDbType.Int, 0, this.OtherYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherRecreationalVehicles",
                SqlDbType.Int, 0, this.OtherRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherSwimmingPool",
                SqlDbType.Int, 0, this.OtherSwimmingPool.ToString(),
                ref cookItems);
            DbRequestXmlCooker.AttachCookItem("OtherWatercraft1",
                SqlDbType.Int, 0, this.OtherWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherWatercraft2",
                SqlDbType.Int, 0, this.OtherWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherWatercraft3",
                SqlDbType.Int, 0, this.OtherWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostAdditionalResidence",
                SqlDbType.Float, 0, this.CostAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostResidenceRented",
                SqlDbType.Float, 0, this.CostResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostAdditionalAuto",
                SqlDbType.Float, 0, this.CostAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostYoungDrivers",
                SqlDbType.Float, 0, this.CostYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostRecreationalVehicles",
                SqlDbType.Float, 0, this.CostRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostSwimmingPool",
                SqlDbType.Float, 0, this.CostSwimmingPool.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostWatercraft1",
                SqlDbType.Float, 0, this.CostWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostWatercraft2",
                SqlDbType.Float, 0, this.CostWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostWatercraft3",
                SqlDbType.Float, 0, this.CostWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalAdditionalResidence",
                SqlDbType.Float, 0, this.TotalAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalResidenceRented",
                SqlDbType.Float, 0, this.TotalResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalAdditionalAuto",
                SqlDbType.Float, 0, this.TotalAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalYoungDrivers",
                SqlDbType.Float, 0, this.TotalYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalRecreationalVehicles",
                SqlDbType.Float, 0, this.TotalRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalSwimmingPool",
                SqlDbType.Float, 0, this.TotalSwimmingPool.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalWatercraft1",
                SqlDbType.Float, 0, this.TotalWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalWatercraft2",
                SqlDbType.Float, 0, this.TotalWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalWatercraft3",
                SqlDbType.Float, 0, this.TotalWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BasicPremium",
                SqlDbType.Float, 0, this.BasicPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalUmbrellaPremium",
                SqlDbType.Float, 0, this.TotalUmbrellaPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt1",
                SqlDbType.VarChar, 20, this.EffDt1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt2",
                SqlDbType.VarChar, 20, this.EffDt2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt3",
                SqlDbType.VarChar, 20, this.EffDt3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt4",
                SqlDbType.VarChar, 20, this.EffDt4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt1",
                SqlDbType.VarChar, 20, this.ExpDt1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt2",
                SqlDbType.VarChar, 20, this.ExpDt2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt3",
                SqlDbType.VarChar, 20, this.ExpDt3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt4",
                SqlDbType.VarChar, 20, this.ExpDt4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit1",
                SqlDbType.VarChar, 20, this.LiabilityLimit1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit2",
                SqlDbType.VarChar, 20, this.LiabilityLimit2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit3",
                SqlDbType.VarChar, 20, this.LiabilityLimit3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit4",
                SqlDbType.VarChar, 20, this.LiabilityLimit4.ToString(),
                ref cookItems);


            DbRequestXmlCooker.AttachCookItem("PolicyNo1",
                SqlDbType.VarChar, 20, this.PolicyNo1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo2",
                SqlDbType.VarChar, 20, this.PolicyNo2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo3",
                SqlDbType.VarChar, 20, this.PolicyNo3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo4",
                SqlDbType.VarChar, 20, this.PolicyNo4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOppQuote.ToString(),
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

        private XmlDocument GetInsertUmbrellaXml(bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[57];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UmbrellaLimit",
                SqlDbType.Int, 0, this.UmbrellaLimit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppAdditionalResidence",
                SqlDbType.Int, 0, this.OppAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppResidenceRented",
                SqlDbType.Int, 0, this.OppResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppAdditionalAuto",
                SqlDbType.Int, 0, this.OppAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppYoungDrivers",
                SqlDbType.Int, 0, this.OppYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppRecreationalVehicles",
                SqlDbType.Int, 0, this.OppRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppSwimmingPool",
                SqlDbType.Int, 0, this.OppSwimmingPool.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppWatercraft1",
                SqlDbType.Int, 0, this.OppWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppWatercraft2",
                SqlDbType.Int, 0, this.OppWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OppWatercraft3",
                SqlDbType.Int, 0, this.OppWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherAdditionalResidence",
                SqlDbType.Int, 0, this.OtherAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherResidenceRented",
                SqlDbType.Int, 0, this.OtherResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherAdditionalAuto",
                SqlDbType.Int, 0, this.OtherAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherYoungDrivers",
                SqlDbType.Int, 0, this.OtherYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherRecreationalVehicles",
                SqlDbType.Int, 0, this.OtherRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherSwimmingPool",
                SqlDbType.Int, 0, this.OtherSwimmingPool.ToString(),
                ref cookItems);
            DbRequestXmlCooker.AttachCookItem("OtherWatercraft1",
                SqlDbType.Int, 0, this.OtherWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherWatercraft2",
                SqlDbType.Int, 0, this.OtherWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherWatercraft3",
                SqlDbType.Int, 0, this.OtherWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostAdditionalResidence",
                SqlDbType.Float, 0, this.CostAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostResidenceRented",
                SqlDbType.Float, 0, this.CostResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostAdditionalAuto",
                SqlDbType.Float, 0, this.CostAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostYoungDrivers",
                SqlDbType.Float, 0, this.CostYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostRecreationalVehicles",
                SqlDbType.Float, 0, this.CostRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostSwimmingPool",
                SqlDbType.Float, 0, this.CostSwimmingPool.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostWatercraft1",
                SqlDbType.Float, 0, this.CostWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostWatercraft2",
                SqlDbType.Float, 0, this.CostWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CostWatercraft3",
                SqlDbType.Float, 0, this.CostWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalAdditionalResidence",
                SqlDbType.Float, 0, this.TotalAdditionalResidence.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalResidenceRented",
                SqlDbType.Float, 0, this.TotalResidenceRented.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalAdditionalAuto",
                SqlDbType.Float, 0, this.TotalAdditionalAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalYoungDrivers",
                SqlDbType.Float, 0, this.TotalYoungDrivers.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalRecreationalVehicles",
                SqlDbType.Float, 0, this.TotalRecreationalVehicles.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalSwimmingPool",
                SqlDbType.Float, 0, this.TotalSwimmingPool.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalWatercraft1",
                SqlDbType.Float, 0, this.TotalWatercraft1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalWatercraft2",
                SqlDbType.Float, 0, this.TotalWatercraft2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalWatercraft3",
                SqlDbType.Float, 0, this.TotalWatercraft3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BasicPremium",
                SqlDbType.Float, 0, this.BasicPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalUmbrellaPremium",
                SqlDbType.Float, 0, this.TotalUmbrellaPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt1",
                SqlDbType.VarChar, 20, this.EffDt1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt2",
                SqlDbType.VarChar, 20, this.EffDt2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt3",
                SqlDbType.VarChar, 20, this.EffDt3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDt4",
                SqlDbType.VarChar, 20, this.EffDt4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt1",
                SqlDbType.VarChar, 20, this.ExpDt1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt2",
                SqlDbType.VarChar, 20, this.ExpDt2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt3",
                SqlDbType.VarChar, 20, this.ExpDt3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpDt4",
                SqlDbType.VarChar, 20, this.ExpDt4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit1",
                SqlDbType.VarChar, 20, this.LiabilityLimit1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit2",
                SqlDbType.VarChar, 20, this.LiabilityLimit2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit3",
                SqlDbType.VarChar, 20, this.LiabilityLimit3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityLimit4",
                SqlDbType.VarChar, 20, this.LiabilityLimit4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo1",
                SqlDbType.VarChar, 20, this.PolicyNo1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo2",
                SqlDbType.VarChar, 20, this.PolicyNo2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo3",
                SqlDbType.VarChar, 20, this.PolicyNo3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo4",
                SqlDbType.VarChar, 20, this.PolicyNo4.ToString(),
                ref cookItems);
            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOppQuote.ToString(),
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



        public static DataTable GetUmbrellaByTaskControlIDDB(int TaskControlID, bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOppQuote.ToString(),
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
                dt = exec.GetQuery("GetUmbrellaByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the data by criteria.", ex);
            }
        }

        private void History(int mode, int userID)
        {
            //Audit.Audit history = new Audit.Audit();

            //if (mode == 2)
            //{
            //    history.BuildNotesForHistory("Customer",
            //        LookupTables.LookupTables.GetDescription("Location", oldumbrella.LocationID.ToString()),
            //        LookupTables.LookupTables.GetDescription("Location", this.LocationID.ToString()),
            //        mode);
            //    history.BuildNotesForHistory("Description", oldumbrella.Description, this.Description, mode);
            //    history.BuildNotesForHistory("Schedule Job", oldumbrella.ScheduleJob.ToString(), this.ScheduleJob.ToString(), mode);
            //    history.BuildNotesForHistory("Duration", oldumbrella.Duration.ToString(), this.Duration.ToString(), mode);
            //    history.BuildNotesForHistory("Maintenance Date", oldumbrella.MaintenanceDate.ToString(), this.MaintenanceDate.ToString(), mode);

            //    history.Actions = "EDIT";
            //}
            //else  //ADD & DELETE
            //{
            //    history.BuildNotesForHistory("JobID.", "", this.JobID.ToString(), mode);
            //    history.Actions = "ADD";
            //}

            //history.KeyID = this.JobID.ToString();
            //history.Subject = "PMS";
            //history.UsersID = userID;
            //history.GetSaveHistory();
        }

        private static Umbrella FillProperties(Umbrella umbrella, bool IsOppQuote)
        {
            umbrella.UmbrellaID = (umbrella._dtUmbrella.Rows[0]["UmbrellaID"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["UmbrellaID"] : 0;
            umbrella.TaskControlID = (umbrella._dtUmbrella.Rows[0]["TaskControlID"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["TaskControlID"] : 0;
            umbrella.UmbrellaLimit = (umbrella._dtUmbrella.Rows[0]["UmbrellaLimit"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["UmbrellaLimit"] : 0;
            umbrella.OppAdditionalResidence = (umbrella._dtUmbrella.Rows[0]["OppAdditionalResidence"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppAdditionalResidence"] : 0;
            umbrella.OppResidenceRented = (umbrella._dtUmbrella.Rows[0]["OppResidenceRented"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppResidenceRented"] : 0;
            umbrella.OppAdditionalAuto = (umbrella._dtUmbrella.Rows[0]["OppAdditionalAuto"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppAdditionalAuto"] : 0;
            umbrella.OppYoungDrivers = (umbrella._dtUmbrella.Rows[0]["OppYoungDrivers"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppYoungDrivers"] : 0;
            umbrella.OppRecreationalVehicles = (umbrella._dtUmbrella.Rows[0]["OppRecreationalVehicles"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppRecreationalVehicles"] : 0;
            umbrella.OppSwimmingPool = (umbrella._dtUmbrella.Rows[0]["OppSwimmingPool"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppSwimmingPool"] : 0;
            umbrella.OppWatercraft1 = (umbrella._dtUmbrella.Rows[0]["OppWatercraft1"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppWatercraft1"] : 0;
            umbrella.OppWatercraft2 = (umbrella._dtUmbrella.Rows[0]["OppWatercraft2"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppWatercraft2"] : 0;
            umbrella.OppWatercraft3 = (umbrella._dtUmbrella.Rows[0]["OppWatercraft3"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OppWatercraft3"] : 0;
            umbrella.OtherAdditionalResidence = (umbrella._dtUmbrella.Rows[0]["OtherAdditionalResidence"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherAdditionalResidence"] : 0;
            umbrella.OtherResidenceRented = (umbrella._dtUmbrella.Rows[0]["OtherResidenceRented"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherResidenceRented"] : 0;
            umbrella.OtherAdditionalAuto = (umbrella._dtUmbrella.Rows[0]["OtherAdditionalAuto"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherAdditionalAuto"] : 0;
            umbrella.OtherYoungDrivers = (umbrella._dtUmbrella.Rows[0]["OtherYoungDrivers"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherYoungDrivers"] : 0;
            umbrella.OtherRecreationalVehicles = (umbrella._dtUmbrella.Rows[0]["OtherRecreationalVehicles"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherRecreationalVehicles"] : 0;
            umbrella.OtherSwimmingPool = (umbrella._dtUmbrella.Rows[0]["OtherSwimmingPool"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherSwimmingPool"] : 0;
            umbrella.OtherWatercraft1 = (umbrella._dtUmbrella.Rows[0]["OtherWatercraft1"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherWatercraft1"] : 0;
            umbrella.OtherWatercraft2 = (umbrella._dtUmbrella.Rows[0]["OtherWatercraft2"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherWatercraft2"] : 0;
            umbrella.OtherWatercraft3 = (umbrella._dtUmbrella.Rows[0]["OtherWatercraft3"] != System.DBNull.Value) ? (int)umbrella._dtUmbrella.Rows[0]["OtherWatercraft3"] : 0;
            umbrella.CostAdditionalResidence = (umbrella._dtUmbrella.Rows[0]["CostAdditionalResidence"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostAdditionalResidence"] : 0.00;
            umbrella.CostResidenceRented = (umbrella._dtUmbrella.Rows[0]["CostResidenceRented"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostResidenceRented"] : 0.00;
            umbrella.CostAdditionalAuto = (umbrella._dtUmbrella.Rows[0]["CostAdditionalAuto"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostAdditionalAuto"] : 0.00;
            umbrella.CostYoungDrivers = (umbrella._dtUmbrella.Rows[0]["CostYoungDrivers"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostYoungDrivers"] : 0.00;
            umbrella.CostRecreationalVehicles = (umbrella._dtUmbrella.Rows[0]["CostRecreationalVehicles"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostRecreationalVehicles"] : 0.00;
            umbrella.CostSwimmingPool = (umbrella._dtUmbrella.Rows[0]["CostSwimmingPool"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostSwimmingPool"] : 0.00;
            umbrella.CostWatercraft1 = (umbrella._dtUmbrella.Rows[0]["CostWatercraft1"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostWatercraft1"] : 0.00;
            umbrella.CostWatercraft2 = (umbrella._dtUmbrella.Rows[0]["CostWatercraft2"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostWatercraft2"] : 0.00;
            umbrella.CostWatercraft3 = (umbrella._dtUmbrella.Rows[0]["CostWatercraft3"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["CostWatercraft3"] : 0.00;
            umbrella.TotalAdditionalResidence = (umbrella._dtUmbrella.Rows[0]["TotalAdditionalResidence"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalAdditionalResidence"] : 0.00;
            umbrella.TotalResidenceRented = (umbrella._dtUmbrella.Rows[0]["TotalResidenceRented"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalResidenceRented"] : 0.00;
            umbrella.TotalAdditionalAuto = (umbrella._dtUmbrella.Rows[0]["TotalAdditionalAuto"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalAdditionalAuto"] : 0.00;
            umbrella.TotalYoungDrivers = (umbrella._dtUmbrella.Rows[0]["TotalYoungDrivers"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalYoungDrivers"] : 0.00;
            umbrella.TotalRecreationalVehicles = (umbrella._dtUmbrella.Rows[0]["TotalRecreationalVehicles"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalRecreationalVehicles"] : 0.00;
            umbrella.TotalSwimmingPool = (umbrella._dtUmbrella.Rows[0]["TotalSwimmingPool"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalSwimmingPool"] : 0.00;
            umbrella.TotalWatercraft1 = (umbrella._dtUmbrella.Rows[0]["TotalWatercraft1"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalWatercraft1"] : 0.00;
            umbrella.TotalWatercraft2 = (umbrella._dtUmbrella.Rows[0]["TotalWatercraft2"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalWatercraft2"] : 0.00;
            umbrella.TotalWatercraft3 = (umbrella._dtUmbrella.Rows[0]["TotalWatercraft3"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalWatercraft3"] : 0.00;
            umbrella.BasicPremium = (umbrella._dtUmbrella.Rows[0]["BasicPremium"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["BasicPremium"] : 0.00;
            umbrella.TotalUmbrellaPremium = (umbrella._dtUmbrella.Rows[0]["TotalUmbrellaPremium"] != System.DBNull.Value) ? (double)umbrella._dtUmbrella.Rows[0]["TotalUmbrellaPremium"] : 0.00;

            umbrella.EffDt1 = umbrella._dtUmbrella.Rows[0]["EffDt1"].ToString();
            umbrella.EffDt2 = umbrella._dtUmbrella.Rows[0]["EffDt2"].ToString();
            umbrella.EffDt3 = umbrella._dtUmbrella.Rows[0]["EffDt3"].ToString();
            umbrella.EffDt4 = umbrella._dtUmbrella.Rows[0]["EffDt4"].ToString();

            umbrella.ExpDt1 = umbrella._dtUmbrella.Rows[0]["ExpDt1"].ToString();
            umbrella.ExpDt2 = umbrella._dtUmbrella.Rows[0]["ExpDt2"].ToString();
            umbrella.ExpDt3 = umbrella._dtUmbrella.Rows[0]["ExpDt3"].ToString();
            umbrella.ExpDt4 = umbrella._dtUmbrella.Rows[0]["ExpDt4"].ToString();

            umbrella.LiabilityLimit1 = umbrella._dtUmbrella.Rows[0]["LiabilityLimit1"].ToString();
            umbrella.LiabilityLimit2 = umbrella._dtUmbrella.Rows[0]["LiabilityLimit2"].ToString();
            umbrella.LiabilityLimit3 = umbrella._dtUmbrella.Rows[0]["LiabilityLimit3"].ToString();
            umbrella.LiabilityLimit4 = umbrella._dtUmbrella.Rows[0]["LiabilityLimit4"].ToString();

            umbrella.PolicyNo1 = umbrella._dtUmbrella.Rows[0]["PolicyNo1"].ToString();
            umbrella.PolicyNo2 = umbrella._dtUmbrella.Rows[0]["PolicyNo2"].ToString();
            umbrella.PolicyNo3 = umbrella._dtUmbrella.Rows[0]["PolicyNo3"].ToString();
            umbrella.PolicyNo4 = umbrella._dtUmbrella.Rows[0]["PolicyNo4"].ToString();

            return umbrella;
        }

        #endregion
    }
}
