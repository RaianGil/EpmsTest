using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using Baldrich.DBRequest;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Quotes;
using EPolicy.Audit;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
    public class AutoHighLimit : Policy
    {
        public AutoHighLimit(bool isQuote)
        {
            this.isQuote = isQuote;
            this.AgentList = Policy.GetAgentListByPolicyClassID(22);

            this.DepartmentID = 1;
            this.PolicyClassID = 22;
            this.PolicyType = "PAP";
            this.InsuranceCompany = "001";
            this.Agency = "000";
            this.Agent = "";
            this.SupplierID = "000";
            this.Bank = "000";
            this.Dealer = "000";
            this.CompanyDealer = "000";
            this.Status = "Inforce";
            this.Term = 12;
            this.EffectiveDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            this.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now.AddMonths(12)); ;
            this.TaskStatusID = int.Parse(LookupTables.LookupTables.GetID("TaskStatus", "Open"));
            this.AutoAssignPolicy = true;

            if (this.isQuote)
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto High Limit Quote"));
            else
            {
                this.DepartmentID = 1;
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto High Limit Policy"));
            }

            // Para el History
            this._mode = (int)TaskControlMode.ADD;
        }

        #region Variable

        #region DataTables

        private DataTable _InsuredCollection = null;
        private int _InsuredID = 0;
        private DataTable _DriversCollection = null;
        private int _DriverID = 0;
        private DataTable _VehicleCollection = null;
        private int _VehicleID = 0;
        private DataTable _dtAutos;

        #endregion DataTables

        #region Policy

        private int _BinderNo = 0;
        private string _PolicyClaim = "";
        private string _RenewalNo = "";
        private string _CustomerCollection = ""; // Se utiliza para identificar si Renewal se recalcula
        private int _TrafficViolationPoints = 0;
        private double _TrafficViolationSurcharge = 0.0;
        private double _UnderageSurcharge = 0.0;
        private bool _isBusinessPolicy = false;
        private double _TotalDiscounts = 0.0;
        private double _TotalDiscountsPct = 0.0;
        private bool _AnyAccidentsOrLosses = false;
        private bool _IsNewCustomer = true;
        private string _YearsWithNoClaims = "";
        private string _YearsWithClaims = "";
        private bool _AnyDriverUnder26 = false;
        private bool _Q1 = false;
        private bool _Q2 = false;
        private bool _Q3 = false;
        private string _EffectiveDateApp;
        private string _ExpirationDateApp;
        private bool _UseCompanyAsNamedInsured;
        private double _Surcharge = 0.0;
        private double _SurchargePct = 0.0;
        private double _AffinityDiscount = 0.0;
        private double _AffinityDiscountPct = 0.0;
        private double _GrossTax = 0.0;

        #endregion Policy

        private AutoHighLimit oldAutos = null;
        private int _AutosID = 0;
        private bool _isQuote = false;

        //private int _mode = (int)AutosMode.CLEAR;
        private int _mode = (int)TaskControlMode.CLEAR;

        #endregion

        #region Properties

        #region Policy

        public int BinderNo
        {
            get
            {
                return this._BinderNo;
            }
            set
            {
                this._BinderNo = value;
            }
        }

        public string PolicyClaim
        {
            get
            {
                return this._PolicyClaim;
            }
            set
            {
                this._PolicyClaim = value;
            }
        }

        public string RenewalNo
        {
            get
            {
                return this._RenewalNo;
            }
            set
            {
                this._RenewalNo = value;
            }
        }

        public string CustomerCollection
        {
            get
            {
                return this._CustomerCollection;
            }
            set
            {
                this._CustomerCollection = value;
            }
        }

        public int TrafficViolationPoints
        {
            get
            {
                return this._TrafficViolationPoints;
            }
            set
            {
                this._TrafficViolationPoints = value;
            }
        }

        public double TrafficViolationSurcharge
        {
            get
            {
                return this._TrafficViolationSurcharge;
            }
            set
            {
                this._TrafficViolationSurcharge = value;
            }
        }

        public double UnderageSurcharge
        {
            get
            {
                return this._UnderageSurcharge;
            }
            set
            {
                this._UnderageSurcharge = value;
            }
        }

        public bool isBusinessPolicy
        {
            get
            {
                return this._isBusinessPolicy;
            }
            set
            {
                this._isBusinessPolicy = value;
            }
        }

        public double TotalDiscounts
        {
            get
            {
                return this._TotalDiscounts;
            }
            set
            {
                this._TotalDiscounts = value;
            }
        }

        public double TotalDiscountsPct
        {
            get
            {
                return this._TotalDiscountsPct;
            }
            set
            {
                this._TotalDiscountsPct = value;
            }
        }

        public bool AnyAccidentsOrLosses
        {
            get
            {
                return this._AnyAccidentsOrLosses;
            }
            set
            {
                this._AnyAccidentsOrLosses = value;
            }
        }

        public bool IsNewCustomer
        {
            get
            {
                return this._IsNewCustomer;
            }
            set
            {
                this._IsNewCustomer = value;
            }
        }

        public string YearsWithNoClaims
        {
            get
            {
                return this._YearsWithNoClaims;
            }
            set
            {
                this._YearsWithNoClaims = value;
            }
        }

        public string YearsWithClaims
        {
            get
            {
                return this._YearsWithClaims;
            }
            set
            {
                this._YearsWithClaims = value;
            }
        }

        public bool AnyDriverUnder26
        {
            get
            {
                return this._AnyDriverUnder26;
            }
            set
            {
                this._AnyDriverUnder26 = value;
            }
        }

        public bool Q1
        {
            get
            {
                return this._Q1;
            }
            set
            {
                this._Q1 = value;
            }
        }

        public bool Q2
        {
            get
            {
                return this._Q2;
            }
            set
            {
                this._Q2 = value;
            }
        }

        public bool Q3
        {
            get
            {
                return this._Q3;
            }
            set
            {
                this._Q3 = value;
            }
        }

        public string EffectiveDateApp
        {
            get
            {
                return this._EffectiveDateApp;
            }
            set
            {
                this._EffectiveDateApp = value;
            }
        }

        public string ExpirationDateApp
        {
            get
            {
                return this._ExpirationDateApp;
            }
            set
            {
                this._ExpirationDateApp = value;
            }
        }

        public bool UseCompanyAsNamedInsured
        {
            get
            {
                return this._UseCompanyAsNamedInsured;
            }
            set
            {
                this._UseCompanyAsNamedInsured = value;
            }
        }

        public double Surcharge
        {
            get
            {
                return this._Surcharge;
            }
            set
            {
                this._Surcharge = value;
            }
        }

        public double SurchargePct
        {
            get
            {
                return this._SurchargePct;
            }
            set
            {
                this._SurchargePct = value;
            }
        }

        public double AffinityDiscount
        {
            get
            {
                return this._AffinityDiscount;
            }
            set
            {
                this._AffinityDiscount = value;
            }
        }

        public double AffinityDiscountPct
        {
            get
            {
                return this._AffinityDiscountPct;
            }
            set
            {
                this._AffinityDiscountPct = value;
            }
        }

        public double GrossTax
        {
            get
            {
                return this._GrossTax;
            }
            set
            {
                this._GrossTax = value;
            }
        }

        #endregion Policy

        #region Vehicles

        public int VehicleID
        {
            get
            {
                return this._VehicleID;
            }
            set
            {
                this._VehicleID = value;
            }
        }

        public DataTable VehicleCollection
        {
            get
            {
                if (this._VehicleCollection == null)
                    this._VehicleCollection = DataTableVehicleTemp();
                return (this._VehicleCollection);
            }
            set
            {
                this._VehicleCollection = value;
            }
        }

        #endregion Vehicles

        #region Drivers

        public DataTable DriversCollection
        {
            get
            {
                if (this._DriversCollection == null)
                    this._DriversCollection = DataTableDriversTemp();
                return (this._DriversCollection);
            }
            set
            {
                this._DriversCollection = value;
            }
        }

        public int DriverID
        {
            get
            {
                return this._DriverID;
            }
            set
            {
                this._DriverID = value;
            }
        }

        #endregion Drivers

        //#region Additional Insured

        //public DataTable InsuredCollection
        //{
        //    get
        //    {
        //        if (this._InsuredCollection == null)
        //            this._InsuredCollection = DataTableInsuredTemp();
        //        return (this._InsuredCollection);
        //    }
        //    set
        //    {
        //        this._InsuredCollection = value;
        //    }
        //}

        //public int InsuredID
        //{
        //    get
        //    {
        //        return this._InsuredID;
        //    }
        //    set
        //    {
        //        this._InsuredID = value;
        //    }
        //}

        //#endregion Additional Insured

        public bool isQuote
        {
            get
            {
                return this._isQuote;
            }
            set
            {
                this._isQuote = value;
            }
        }

        public int AutosID
        {
            get
            {
                return this._AutosID;
            }
            set
            {
                this._AutosID = value;
            }
        }

        #endregion

        #region Public Enumeration

        public enum AutosMode { ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4 };

        #endregion Public Enumeration

        #region Public Methods

        public static double GetNetTotalPremium(int TaskControlID)
        {
            double Total = 0;
            DataTable dt = null;

            dt = GetNetTotalPremiumByTaskControlID(TaskControlID);
            if (dt.Rows.Count > 0)
            {
                //When modifing Verifies premium claculated, if the premium is not equal or does not have 1 dollar less or 1 dollar more the total premium shwon will be the one calculated inside the application
                if (double.Parse(dt.Rows[0]["XML_NetPremium"].ToString()).ToString("##,###.00") != "0")
                {
                    Total = double.Parse(dt.Rows[0]["XML_NetPremium"].ToString());
                }
            }

            return Total;
        }

        public void SaveAutos(int UserID)
        {
            this._mode = (int)this.Mode;  // Se le asigna el mode de taskControl.
            this.PolicyMode = (int)this.Mode;  // Se le asigna el mode de taskControl.

            if (isQuote)
            {
                this.ValidateQuote();
                if (this.Prospect.ProspectID == 0)
                    this.Prospect.Mode = 1;
                else
                    this.Prospect.Mode = 2;

                this.Prospect.IsBusiness = false;
                this.Prospect.LocationID = this.OriginatedAt;
                this.Prospect.SaveProspect(UserID);

                this.ProspectID = this.Prospect.ProspectID;

                //if (IsEndorsement)
                //{
                if (this.Customer.CustomerNo.Trim() == "")
                    this.Customer.Mode = 1;
                else
                    this.Customer.Mode = 2;

                this.Customer.IsBusiness = false;
                this.Customer.Save(UserID);

                this.CustomerNo = this.Customer.CustomerNo;
                this.ProspectID = this.Customer.ProspectID;
                //}
            }
            else
            {
                this.Validate();
                base.ValidatePolicy();

                if (this._mode == 2)
                    oldAutos = (AutoHighLimit)AutoHighLimit.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

                if (this.Customer.CustomerNo.Trim() == "")
                    this.Customer.Mode = 1;
                else
                    this.Customer.Mode = 2;

                this.Customer.IsBusiness = false;
                this.Customer.Save(UserID);

                this.CustomerNo = this.Customer.CustomerNo;
                this.ProspectID = this.Customer.ProspectID;
            }

            base.Save(); // AQUI

            if (isQuote) // AQUI
                base.SaveOPPQuote(UserID);    // Validate and Save Quote in Policy Class
            else
                base.SavePol(UserID);	// Validate and Save Policy

            this.SaveAutosDB();
            this.SaveDriverDetail(UserID, this.TaskControlID);
            this.SaveVehiclesDetail(UserID, this.TaskControlID);
        }

        public void ValidateQuote()
        {
            string errorMessage = String.Empty;

            if (this.EffectiveDate == "")
                errorMessage = "Effective Date is missing or wrong.";
            else
                if (this.Term == 0)
                    errorMessage = "Term is missing or wrong.";
                else
                    if (this.Prospect.FirstName == "" && this.isBusinessPolicy == false)
                        errorMessage = "First Name is missing or wrong.";
                    //else
                    //    if (this.Prospect.LastName1 == "")
                    //        errorMessage = "Last Name is missing or wrong.";
                    else
                        if (this.OriginatedAt == 0)
                            errorMessage = "Originated is missing.";
                        else
                            if (this.TotalPremium == 0)
                                errorMessage = "TotalPremium must be greater than 0.";

            //throw the exception.
            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
        }

        public static DataTable GetNetTotalPremiumByTaskControlID(int TaskControlID)
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
                dt = exec.GetQuery("GetNetTotalPremiumByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }
        public static AutoHighLimit GetAutoHighLimit(int TaskControlID, bool isQuote)
        {
            AutoHighLimit autos = null;

            DataTable dt = GetAutosByTaskControlIDDB(TaskControlID, isQuote);

            autos = new AutoHighLimit(isQuote);

            if (isQuote)
                autos = (AutoHighLimit)Policy.GetPolicyQuoteByTaskControlID(TaskControlID, autos);  //PolicyQuote
            else
                autos = (AutoHighLimit)Policy.GetPolicyByTaskControlID(TaskControlID, autos);  //Policy

            autos._dtAutos = dt;

            autos = FillProperties(autos, TaskControlID, isQuote);

            return autos;
        }

        public static void DeleteAutosByTaskControlID(int taskControlID, bool isQuote)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                Executor.BeginTrans();
                Executor.Update("DeleteAutosByTaskControlID", DeleteAutosByTaskControlIDXml(taskControlID, isQuote));
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error. Please try again. " + xcp.Message, xcp);
            }
        }

        public static DataTable GetOPPByTaskControlID(int taskControlID)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                DataTable dt = GetOPPByTaskControlIDDB(taskControlID);
                return dt;
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error, Please try again. " + xcp.Message, xcp);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static DataTable GetOPPByTaskControlIDDB(int TaskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

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

            DataTable dt = exec.GetQuery("GetOPPByTaskControlID", xmlDoc);
            return dt;
        }

        public static DataTable GetAutosByTaskControlIDDB(int TaskControlID, bool isQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
                SqlDbType.Bit, 0, isQuote.ToString(),
                ref cookItems);

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

            DataTable dt = exec.GetQuery("GetAutosByTaskControlID", xmlDoc);
            return dt;
        }

        private void SaveAutosDB()
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this.Mode)
                {
                    case 1:  //ADD
                        this.AutosID = Executor.Insert("AddAutos", this.GetInsertAutosXml());
                        //this.History(this._mode, UserID);
                        break;

                    case 3:  //DELETE
                        //Executor.Update("DeleteAutoGuardServicesContract",this.GetDeletePoliciesXml());
                        break;

                    default: //UPDATE
                        //this.History(this._mode, UserID);
                        this.AutosID = Executor.Insert("AddAutos", this.GetInsertAutosXml());
                        break;
                }
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the Policy. " + xcp.Message, xcp);
            }
        }

        private XmlDocument GetInsertAutosXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[28];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
               SqlDbType.Int, 0, this.TaskControlID.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutosID",
               SqlDbType.Int, 0, this.AutosID.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
               SqlDbType.Bit, 0, this.isQuote.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClaim",
                SqlDbType.VarChar, 3, this.PolicyClaim.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalNo",
                SqlDbType.VarChar, 50, this.RenewalNo.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CustomerCollection",
                SqlDbType.VarChar, 50, this.CustomerCollection.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrafficViolationPoints",
               SqlDbType.Int, 0, this.TrafficViolationPoints.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrafficViolationSurcharge",
               SqlDbType.Float, 0, this.TrafficViolationSurcharge.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UnderageSurcharge",
               SqlDbType.Float, 0, this.UnderageSurcharge.ToString(),
               ref cookItems);


            DbRequestXmlCooker.AttachCookItem("isBusiness",
               SqlDbType.Bit, 0, this.isBusinessPolicy.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalDiscounts",
               SqlDbType.Float, 0, this.TotalDiscounts.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalDiscountsPct",
               SqlDbType.Float, 0, this.TotalDiscountsPct.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AnyAccidentsOrLosses",
               SqlDbType.Bit, 0, this.AnyAccidentsOrLosses.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("YearsWithNoClaims",
               SqlDbType.VarChar, 0, this.YearsWithNoClaims.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AnyDriverUnder26",
               SqlDbType.Bit, 0, this.AnyDriverUnder26.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Q1",
               SqlDbType.Bit, 0, this.Q1.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Q2",
               SqlDbType.Bit, 0, this.Q2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Q3",
               SqlDbType.Bit, 0, this.Q3.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
               SqlDbType.VarChar, 50, this.EffectiveDateApp.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpirationDate",
               SqlDbType.VarChar, 50, this.ExpirationDateApp.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UseCompanyAsNamedInsured",
               SqlDbType.Bit, 0, this.UseCompanyAsNamedInsured.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsNewCustomer",
               SqlDbType.Bit, 0, this.IsNewCustomer.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("YearsWithClaims",
               SqlDbType.VarChar, 10, this.YearsWithClaims.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Surcharge",
             SqlDbType.Float, 0, this.Surcharge.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SurchargePct",
               SqlDbType.Float, 0, this.SurchargePct.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AffinityDiscount",
             SqlDbType.Float, 0, this.AffinityDiscount.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AffinityDiscountPct",
               SqlDbType.Float, 0, this.AffinityDiscountPct.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("GrossTax",
                SqlDbType.Float, 0, this.isQuote == false ? "0" : this.GrossTax.ToString(),
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

        private static XmlDocument DeleteAutosByTaskControlIDXml(int taskControlID, bool isQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
                SqlDbType.Bit, 0, isQuote.ToString(),
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

        private static AutoHighLimit FillProperties(AutoHighLimit autos, int taskControlID, bool isQuote)
        {
            try
            {
                autos.TaskControlID = int.Parse(autos._dtAutos.Rows[0]["TaskControlID"].ToString());
                autos.PolicyClaim = autos._dtAutos.Rows[0]["PolicyClaim"].ToString();
                autos.RenewalNo = autos._dtAutos.Rows[0]["RenewalNo"].ToString();
                autos.CustomerCollection = autos._dtAutos.Rows[0]["CustomerCollection"].ToString();
                autos.TrafficViolationPoints = int.Parse(autos._dtAutos.Rows[0]["TrafficViolationPoints"].ToString());
                autos.TrafficViolationSurcharge = double.Parse(autos._dtAutos.Rows[0]["TrafficViolationSurcharge"].ToString());
                autos.UnderageSurcharge = double.Parse(autos._dtAutos.Rows[0]["UnderageSurcharge"].ToString());
                autos.isBusinessPolicy = bool.Parse(autos._dtAutos.Rows[0]["isBusiness"].ToString());
                autos.TotalDiscounts = double.Parse(autos._dtAutos.Rows[0]["TotalDiscounts"].ToString());
                autos.TotalDiscountsPct = double.Parse(autos._dtAutos.Rows[0]["TotalDiscountsPct"].ToString());
                autos.AnyAccidentsOrLosses = bool.Parse(autos._dtAutos.Rows[0]["AnyAccidentsOrLosses"].ToString());
                autos.YearsWithNoClaims = autos._dtAutos.Rows[0]["YearsWithNoClaims"].ToString();
                autos.AnyDriverUnder26 = bool.Parse(autos._dtAutos.Rows[0]["AnyDriverUnder26"].ToString());
                autos.Q1 = bool.Parse(autos._dtAutos.Rows[0]["Q1"].ToString());
                autos.Q2 = bool.Parse(autos._dtAutos.Rows[0]["Q2"].ToString());
                autos.Q3 = bool.Parse(autos._dtAutos.Rows[0]["Q3"].ToString());
                autos.EffectiveDateApp = autos._dtAutos.Rows[0]["EffectiveDate"].ToString();
                autos.ExpirationDateApp = autos._dtAutos.Rows[0]["ExpirationDate"].ToString();
                autos.UseCompanyAsNamedInsured = bool.Parse(autos._dtAutos.Rows[0]["UseCompanyAsNamedInsured"].ToString());
                autos.IsNewCustomer = bool.Parse(autos._dtAutos.Rows[0]["IsNewCustomer"].ToString());
                autos.YearsWithClaims = autos._dtAutos.Rows[0]["YearsWithClaims"].ToString();
                autos.Surcharge = autos._dtAutos.Rows[0]["Surcharge"].ToString() != "" ? double.Parse(autos._dtAutos.Rows[0]["Surcharge"].ToString()) : 0.0;
                autos.SurchargePct = autos._dtAutos.Rows[0]["SurchargePct"].ToString() != "" ? double.Parse(autos._dtAutos.Rows[0]["SurchargePct"].ToString()) : 0.0;
                autos.AffinityDiscount = autos._dtAutos.Rows[0]["AffinityDiscount"].ToString() != "" ? double.Parse(autos._dtAutos.Rows[0]["AffinityDiscount"].ToString()) : 0.0;
                autos.AffinityDiscountPct = autos._dtAutos.Rows[0]["AffinityDiscountPct"].ToString() != "" ? double.Parse(autos._dtAutos.Rows[0]["AffinityDiscountPct"].ToString()) : 0.0;
                autos.GrossTax = autos._dtAutos.Rows[0]["GrossTax"].ToString() != "" ? double.Parse(autos._dtAutos.Rows[0]["GrossTax"].ToString()) : 0.0;

                autos.DriversCollection = GetDriverDetailByTaskControlID(taskControlID, isQuote);
                autos.VehicleCollection = GetVehicleDetailByTaskControlID(taskControlID, isQuote);

                return autos;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not fill properties. ", ex);
            }


        }

        public DataTable GetAgentByUserID(string UserID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("UserID",
                SqlDbType.VarChar, 10, UserID.ToString(),
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
                dt = exec.GetQuery("GetAgentByUserID_VI", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

        #endregion Private Methods

        #region Drivers

        public void SaveDriverDetail(int UserID, int taskControlID)
        {
            DBRequest Executor = new DBRequest();
            Executor.Update("DeleteDriverDetailByTaskControlID", DeleteDriverDetailByTaskControlIDXml(taskControlID));

            for (int i = 0; i < DriversCollection.Rows.Count; i++)
            {
                this.Mode = 1; //Add

                Executor.BeginTrans();
                this.DriverID = Executor.Insert("AddDriverDetail", this.GetInsertDriverDetailXml(i));
                Executor.CommitTrans();
            }
        }

        private XmlDocument DeleteDriverDetailByTaskControlIDXml(int taskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
            SqlDbType.Int, 0, taskControlID.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
            SqlDbType.Bit, 0, isQuote.ToString(),
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

        private XmlDocument GetInsertDriverDetailXml(int index)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[17];

            DbRequestXmlCooker.AttachCookItem("DriverDetailID",
              SqlDbType.Int, 0, DriversCollection.Rows[index]["DriverDetailID"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DriverName",
            SqlDbType.VarChar, 100, DriversCollection.Rows[index]["DriverName"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DriverLastName",
            SqlDbType.VarChar, 100, DriversCollection.Rows[index]["DriverLastName"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Company",
            SqlDbType.VarChar, 100, DriversCollection.Rows[index]["Company"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DriverGender",
            SqlDbType.VarChar, 20, DriversCollection.Rows[index]["DriverGender"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DriverMaritalStatus",
            SqlDbType.VarChar, 20, DriversCollection.Rows[index]["DriverMaritalStatus"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DriverAge",
            SqlDbType.Int, 0, DriversCollection.Rows[index]["DriverAge"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DriverDateOfBirth",
            SqlDbType.VarChar, 20, DriversCollection.Rows[index]["DriverDateOfBirth"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
              SqlDbType.Bit, 0, this.isQuote.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrafficViolationPoints",
            SqlDbType.Int, 0, DriversCollection.Rows[index]["TrafficViolationPoints"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrafficViolationSurcharge",
            SqlDbType.Float, 0, DriversCollection.Rows[index]["TrafficViolationSurcharge"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UnderageSurcharge",
            SqlDbType.Float, 0, DriversCollection.Rows[index]["UnderageSurcharge"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SumOfSurcharges",
            SqlDbType.Float, 0, DriversCollection.Rows[index]["SumOfSurcharges"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PrimaryDriver",
              SqlDbType.Bit, 0, DriversCollection.Rows[index]["PrimaryDriver"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("GradePointAvg",
            SqlDbType.Float, 0, DriversCollection.Rows[index]["GradePointAvg"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsAdditionalInsured",
            SqlDbType.Bit, 0, DriversCollection.Rows[index]["IsAdditionalInsured"].ToString(),
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

        public static DataTable GetDriverDetailByTaskControlID(int TaskControlID, bool isQuote)
        {
            DataTable dt = GetDriverDetailByTaskControlIDDB(TaskControlID, isQuote);
            return dt;
        }

        private static DataTable GetDriverDetailByTaskControlIDDB(int taskControlID, bool isQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
                SqlDbType.Bit, 0, isQuote.ToString(),
                ref cookItems);

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

            DataTable dt = exec.GetQuery("GetDriverDetailByTaskControlID", xmlDoc);

            return dt;
        }

        private DataTable DataTableDriversTemp()
        {
            DataTable myDataTable = new DataTable("DataTableDriversTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DriverDetailID";
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementSeed = 1;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.AllowDBNull = false;
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = true;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "TaskControlID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TaskControlID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "DriverName";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DriverName";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "DriverLastName";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DriverLastName";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Company";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Company";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "DriverGender";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DriverGender";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "DriverMaritalStatus";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DriverMaritalStatus";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DriverAge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DriverAge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "DriverDateOfBirth";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DriverDateOfBirth";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "TrafficViolationPoints";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TrafficViolationPoints";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "TrafficViolationSurcharge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TrafficViolationSurcharge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "UnderageSurcharge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "UnderageSurcharge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "SumOfSurcharges";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SumOfSurcharges";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "PrimaryDriver";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PrimaryDriver";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "GradePointAvg";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "GradePointAvg";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsAdditionalInsured";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsAdditionalInsured";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }

        #endregion

        //#region Additional Insured

        //private DataTable DataTableInsuredTemp()
        //{
        //    DataTable myDataTable = new DataTable("DataTableInsuredTemp");
        //    DataColumn myDataColumn;

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Int32");
        //    myDataColumn.ColumnName = "AdditionalInsuredDetailID";
        //    myDataColumn.AutoIncrement = true;
        //    myDataColumn.AutoIncrementSeed = 1;
        //    myDataColumn.AutoIncrementStep = 1;
        //    myDataColumn.AllowDBNull = false;
        //    myDataColumn.ReadOnly = true;
        //    myDataColumn.Unique = true;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Int32");
        //    myDataColumn.ColumnName = "TaskControlID";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "TaskControlID";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.String");
        //    myDataColumn.ColumnName = "AdditionalInsuredName";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "AdditionalInsuredName";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.String");
        //    myDataColumn.ColumnName = "AdditionalInsuredLastName";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "AdditionalInsuredLastName";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.String");
        //    myDataColumn.ColumnName = "Company";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "Company";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.String");
        //    myDataColumn.ColumnName = "AdditionalInsuredGender";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "AdditionalInsuredGender";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.String");
        //    myDataColumn.ColumnName = "AdditionalInsuredMaritalStatus";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "AdditionalInsuredMaritalStatus";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Int32");
        //    myDataColumn.ColumnName = "AdditionalInsuredAge";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "AdditionalInsuredAge";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.String");
        //    myDataColumn.ColumnName = "AdditionalInsuredDateOfBirth";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "AdditionalInsuredDateOfBirth";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Int32");
        //    myDataColumn.ColumnName = "TrafficViolationPoints";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "TrafficViolationPoints";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Double");
        //    myDataColumn.ColumnName = "TrafficViolationSurcharge";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "TrafficViolationSurcharge";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Double");
        //    myDataColumn.ColumnName = "UnderageSurcharge";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "UnderageSurcharge";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Double");
        //    myDataColumn.ColumnName = "SumOfSurcharges";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "SumOfSurcharges";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Boolean");
        //    myDataColumn.ColumnName = "PrimaryInsured";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "PrimaryInsured";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    myDataColumn = new DataColumn();
        //    myDataColumn.DataType = System.Type.GetType("System.Double");
        //    myDataColumn.ColumnName = "GradePointAvg";
        //    myDataColumn.AutoIncrement = false;
        //    myDataColumn.Caption = "GradePointAvg";
        //    myDataColumn.ReadOnly = false;
        //    myDataColumn.Unique = false;
        //    myDataTable.Columns.Add(myDataColumn);

        //    // Make the ID column the primary key column.
        //    DataColumn[] PrimaryKeyColumns = new DataColumn[1];
        //    PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
        //    myDataTable.PrimaryKey = PrimaryKeyColumns;

        //    return myDataTable;
        //}

        //#endregion Additional Insured

        #region Vehicles

        public void SaveVehiclesDetail(int UserID, int taskControlID)
        {
            DBRequest Executor = new DBRequest();
            Executor.Update("DeleteVehicleDetailByTaskControlID", DeleteVehiclesDetailByTaskControlIDXml(taskControlID));

            for (int i = 0; i < VehicleCollection.Rows.Count; i++)
            {
                this.Mode = 1; //Add

                Executor.BeginTrans();
                this.VehicleID = Executor.Insert("AddVehicleDetail", this.GetInsertVehicleDetailXml(i));
                Executor.CommitTrans();
            }
        }

        private XmlDocument DeleteVehiclesDetailByTaskControlIDXml(int taskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
            SqlDbType.Int, 0, taskControlID.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
            SqlDbType.Bit, 0, isQuote.ToString(),
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

        private XmlDocument GetInsertVehicleDetailXml(int index)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[52];

            DbRequestXmlCooker.AttachCookItem("VehicleDetailID",
              SqlDbType.Int, 0, VehicleCollection.Rows[index]["VehicleDetailID"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleYear",
              SqlDbType.Int, 0, VehicleCollection.Rows[index]["VehicleYear"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleMake",
            SqlDbType.VarChar, 50, VehicleCollection.Rows[index]["VehicleMake"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleModel",
            SqlDbType.VarChar, 50, VehicleCollection.Rows[index]["VehicleModel"].ToString(), //5
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VIN",
            SqlDbType.VarChar, 20, VehicleCollection.Rows[index]["VIN"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LicensePlateNo",
            SqlDbType.VarChar, 20, VehicleCollection.Rows[index]["LicensePlateNo"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
              SqlDbType.Bit, 0, this.isQuote.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Island",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["Island"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Status",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["Status"].ToString(), //10
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleValue",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["VehicleValue"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleUse",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["VehicleUse"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PassengersNo",
            SqlDbType.Int, 0, VehicleCollection.Rows[index]["PassengersNo"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Over2Tons",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["Over2Tons"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["MedicalPayment"].ToString(), //15
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MotoristCoverage",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["MotoristCoverage"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RentalReimCoverage",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["RentalReimCoverage"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DeathDismembermentCoverage",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["DeathDismembermentCoverage"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ViolationPoints",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["ViolationPoints"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ViolationSurcharge",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["ViolationSurcharge"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UnderAgeSurcharge",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["UnderAgeSurcharge"].ToString(), //20
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherSurcharge",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["OtherSurcharge"].ToString(), //20
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherSurchargePct",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["OtherSurchargePct"].ToString(), //20
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PDLimit",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["PDLimit"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ComprehensiveDedu",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["ComprehensiveDedu"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CollisionDedu",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["CollisionDedu"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BIPPLimit",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["BIPPLimit"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BIPOLimit",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["BIPOLimit"].ToString(), //25
            ref cookItems);

            //
            DbRequestXmlCooker.AttachCookItem("MPLimit",
            SqlDbType.VarChar, 30, VehicleCollection.Rows[index]["MPLimit"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CompPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["CompPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CollPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["CollPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BIPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["BIPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PDPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["PDPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MPPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["MPPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MotoristPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["MotoristPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RentalReim",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["RentalReim"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ADDPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["ADDPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["TotalPremium"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankDesc",
            SqlDbType.VarChar, 100, VehicleCollection.Rows[index]["BankDesc"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankPPSID",
            SqlDbType.VarChar, 10, VehicleCollection.Rows[index]["BankPPSID"].ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsMotorcycleScooter",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["IsMotorcycleScooter"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaxiLossAmount",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["TaxiLossAmount"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleRowNumber",
           SqlDbType.Int, 0, VehicleCollection.Rows[index]["VehicleRowNumber"].ToString(),
           ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalNo",
            SqlDbType.VarChar, 50, this.RenewalNo.ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CustomerCollection",
            SqlDbType.VarChar, 50, this.CustomerCollection.ToString(), //25
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsHeavyTruck",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["IsHeavyTruck"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NewUse",
            SqlDbType.Int, 0, VehicleCollection.Rows[index]["NewUse"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsSalvage",
            SqlDbType.Bit, 0, VehicleCollection.Rows[index]["IsSalvage"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NewOver2Ton",
            SqlDbType.Int, 0, VehicleCollection.Rows[index]["NewOver2Ton"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VIPA",
            SqlDbType.Float, 0, VehicleCollection.Rows[index]["VIPA"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DiscountPhys",
            SqlDbType.Int, 0, VehicleCollection.Rows[index]["DiscountPhys"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DiscountLiab",
            SqlDbType.Int, 0, VehicleCollection.Rows[index]["DiscountLiab"].ToString(),
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

        public static DataTable GetVehicleDetailByTaskControlID(int TaskControlID, bool isQuote)
        {
            DataTable dt = GetVehicleDetailByTaskControlIDDB(TaskControlID, isQuote);
            return dt;
        }

        private static DataTable GetVehicleDetailByTaskControlIDDB(int taskControlID, bool isQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
                SqlDbType.Bit, 0, isQuote.ToString(),
                ref cookItems);

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

            DataTable dt = exec.GetQuery("GetVehicleDetailByTaskControlID", xmlDoc);

            return dt;
        }

        private DataTable DataTableVehicleTemp()
        {
            DataTable myDataTable = new DataTable("DataTableVehicleTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleDetailID";
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementSeed = 1;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.AllowDBNull = false;
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = true;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "TaskControlID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TaskControlID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleYear";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleYear";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleMake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleMake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleModel";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleModel";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VIN";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VIN";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "LicensePlateNo";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "LicensePlateNo";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Island";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Island";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Status";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Status";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleValue";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleValue";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleUse";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleUse";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PassengersNo";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PassengersNo";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Over2Tons";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Over2Tons";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "MedicalPayment";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "MotoristCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MotoristCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "RentalReimCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "RentalReimCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "DeathDismembermentCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DeathDismembermentCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ViolationPoints";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ViolationPoints";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ViolationSurcharge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ViolationSurcharge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "UnderAgeSurcharge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "UnderAgeSurcharge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "PDLimit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PDLimit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ComprehensiveDedu";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ComprehensiveDedu";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "CollisionDedu";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "CollisionDedu";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "BIPPLimit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BIPPLimit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "BIPOLimit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BIPOLimit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "MPLimit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MPLimit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "CompPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "CompPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "CollPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "CollPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BIPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BIPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "PDPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PDPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MPPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MPPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MotoristPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MotoristPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ADDPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ADDPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "RentalReim";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "RentalReim";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "TotalPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TotalPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "OtherSurcharge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "OtherSurcharge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "OtherSurchargePct";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "OtherSurchargePct";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "BankDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BankDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "BankPPSID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BankPPSID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsMotorcycleScooter";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsMotorcycleScooter";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "TaxiLossAmount";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TaxiLossAmount";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleRowNumber";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleRowNumber";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsHeavyTruck";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsHeavyTruck";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NewUse";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NewUse";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsSalvage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsSalvage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NewOver2Ton";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NewOver2Ton";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "VIPA";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VIPA";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountPhys";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountPhys";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountLiab";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountLiab";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }

        #endregion
    }
}