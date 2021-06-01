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
    public class Yacht : Policy
    {
        public Yacht(bool isQuote)
        {
            this.isQuote = isQuote;
            this.DepartmentID = 1;     //AutoGap
            this.PolicyClassID = 27;
            this.PolicyType = "MAR";
            this.InsuranceCompany = "001";
            this.Agency = "000";
            this.Agent = "000";
            this.Bank = "000";
            this.Dealer = "000";
            this.CompanyDealer = "000";
            this.Status = "Inforce";
            this.TaskStatusID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskStatus", "Open"));
            this.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Yacht").ToString());
            this.TotalPremium = 0.00;
            this.Term = 12;

            if (this.isQuote)
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Yacht Quote"));
            else
            {
                this.DepartmentID = 1;
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Yacht"));
            }
            // Para el History
            this._mode = (int)TaskControlMode.ADD;
        }

        #region Variable

        private int _mode = (int)TaskControlMode.CLEAR;
        private DataTable _YachtCollection = null;

        private DataTable _tenderLimitCollection = null;

        public DataTable TenderLimitCollection
        {
            get
            {
                if (this._tenderLimitCollection == null)
                    this._tenderLimitCollection = DataTableTenderLimitTemp();
                return (this._tenderLimitCollection);
            }
            set
            {
                this._tenderLimitCollection = value;
            }
        }

        private DataTable _surveyCollection = null;

        public DataTable SurveyCollection
        {
            get
            {
                if (this._surveyCollection == null)
                    this._surveyCollection = DataTableSurveyTemp();
                return (this._surveyCollection);
            }
            set
            {
                this._surveyCollection = value;
            }
        }

        private DataTable _navigationLimitCollection = null;

        public DataTable NavigationLimitCollection
        {
            get
            {
                if (this._navigationLimitCollection == null)
                    this._navigationLimitCollection = DataTableNavigationLimitTemp();
                return (this._navigationLimitCollection);
            }
            set
            {
                this._navigationLimitCollection = value;
            }
        }

        public DataTable YachtCollection
        {
            get { return _YachtCollection; }
            set { _YachtCollection = value; }
        }
        //private DataTable _BondsReqDocCollection = null;
        private DataTable _dtYacht = null;

        public DataTable DtYacht
        {
            get { return _dtYacht; }
            set { _dtYacht = value; }
        }
        //private TaskControl _oldPolices = null;

        //private int _TypeOfBond = 0;
        private string _boatName = "";

        public string BoatName
        {
            get { return _boatName; }
            set { _boatName = value; }
        }
        private string _boatYear = "";

        public string BoatYear
        {
            get { return _boatYear; }
            set { _boatYear = value; }
        }
        private string _boatModel = "";

        public string BoatModel
        {
            get { return _boatModel; }
            set { _boatModel = value; }
        }
        private string _boatBuilder = "";

        public string BoatBuilder
        {
            get { return _boatBuilder; }
            set { _boatBuilder = value; }
        }
        private string _loa = "";

        public string Loa
        {
            get { return _loa; }
            set { _loa = value; }
        }
        private int _homeportID = 0;

        public int HomeportID
        {
            get { return _homeportID; }
            set { _homeportID = value; }
        }

        private string _homeportAddress = "";

        public string HomeportAddress
        {
            get { return _homeportAddress; }
            set { _homeportAddress = value; }
        }

        private int _navigationLimitID = 0;

        public int NavigationLimitID
        {
            get { return _navigationLimitID; }
            set { _navigationLimitID = value; }
        }
        private string _hullLimit = "";

        public string HullLimit
        {
            get { return _hullLimit; }
            set { _hullLimit = value; }
        }
        private string _hullNumberRegistration = "";

        public string HullNumberRegistration
        {
            get { return _hullNumberRegistration; }
            set { _hullNumberRegistration = value; }
        }
        private int _deductibleID1 = 0;

        public int DeductibleID1
        {
            get { return _deductibleID1; }
            set { _deductibleID1 = value; }
        }

        private int _deductibleID2 = 0;

        public int DeductibleID2
        {
            get { return _deductibleID2; }
            set { _deductibleID2 = value; }
        }

        private string _watercraftLimit1 = "";

        public string WatercraftLimit1
        {
            get { return _watercraftLimit1; }
            set { _watercraftLimit1 = value; }
        }

        private string _watercraftLimit2 = "";

        public string WatercraftLimit2
        {
            get { return _watercraftLimit2; }
            set { _watercraftLimit2 = value; }
        }

        private string _rate1 = "";

        public string Rate1
        {
            get { return _rate1; }
            set { _rate1 = value; }
        }

        private string _rate2 = "";

        public string Rate2
        {
            get { return _rate2; }
            set { _rate2 = value; }
        }

        private string _watercraftLimitTotal1 = "";

        public string WatercraftLimitTotal1
        {
            get { return _watercraftLimitTotal1; }
            set { _watercraftLimitTotal1 = value; }
        }

        private string _watercraftLimitTotal2 = "";

        public string WatercraftLimitTotal2
        {
            get { return _watercraftLimitTotal2; }
            set { _watercraftLimitTotal2 = value; }
        }

        private int _pIID = 0;

        public int PIID
        {
            get { return _pIID; }
            set { _pIID = value; }
        }
        private int _pILiabilityOnlyID = 0;

        public int PILiabilityOnlyID
        {
            get { return _pILiabilityOnlyID; }
            set { _pILiabilityOnlyID = value; }
        }

        private string _otherPI = "";

        public string OtherPI
        {
            get { return _otherPI; }
            set { _otherPI = value; }
        }

        private string _piPremium = "";

        public string PIPremium
        {
            get { return _piPremium; }
            set { _piPremium = value; }
        }
        private int _medicalPaymentID = 0;

        public int MedicalPaymentID
        {
            get { return _medicalPaymentID; }
            set { _medicalPaymentID = value; }
        }
        private string _otherMedicalPayment = "";

        public string OtherMedicalPayment
        {
            get { return _otherMedicalPayment; }
            set { _otherMedicalPayment = value; }
        }

        private string _medicalPaymentPremiumTotal = "";

        public string MedicalPaymentPremiumTotal
        {
            get { return _medicalPaymentPremiumTotal; }
            set { _medicalPaymentPremiumTotal = value; }
        }
        private string _personalEffects = "";

        public string PersonalEffects
        {
            get { return _personalEffects; }
            set { _personalEffects = value; }
        }

        private string _personalEffectsPremium = "";

        public string PersonalEffectsPremium
        {
            get { return _personalEffectsPremium; }
            set { _personalEffectsPremium = value; }
        }
        private string _trailer = "";

        public string Trailer
        {
            get { return _trailer; }
            set { _trailer = value; }
        }
        private string _trailerPremium = "";

        public string TrailerPremium
        {
            get { return _trailerPremium; }
            set { _trailerPremium = value; }
        }

        private string _trailerModel = "";

        public string TrailerModel
        {
            get { return _trailerModel; }
            set { _trailerModel = value; }
        }

        private string _trailerSerial = "";

        public string TrailerSerial
        {
            get { return _trailerSerial; }
            set { _trailerSerial = value; }
        }

        private int _uninsuredBoaterID = 0;

        public int UninsuredBoaterID
        {
            get { return _uninsuredBoaterID; }
            set { _uninsuredBoaterID = value; }
        }
        private string _otherUninsuredBoater = "";

        public string OtherUninsuredBoater
        {
            get { return _otherUninsuredBoater; }
            set { _otherUninsuredBoater = value; }
        }
        private string _otherUninsuredBoaterPremium = "";

        public string OtherUninsuredBoaterPremium
        {
            get { return _otherUninsuredBoaterPremium; }
            set { _otherUninsuredBoaterPremium = value; }
        }

        private string _tripTransit = "";

        public string TripTransit
        {
            get { return _tripTransit; }
            set { _tripTransit = value; }
        }

        private string _tripTransitNotes = "";

        public string TripTransitNotes
        {
            get { return _tripTransitNotes; }
            set { _tripTransitNotes = value; }
        }

        private double _totalPremium1 = 0.00;

        public double TotalPremium1
        {
            get { return _totalPremium1; }
            set { _totalPremium1 = value; }
        }

        private double _totalPremium2 = 0.00;

        public double TotalPremium2
        {
            get { return _totalPremium2; }
            set { _totalPremium2 = value; }
        }

        private string _miscellaneous = "";

        public string Miscellaneous
        {
            get { return _miscellaneous; }
            set { _miscellaneous = value; }
        }
        private string _miscellaneousNotes = "";

        public string MiscellaneousNotes
        {
            get { return _miscellaneousNotes; }
            set { _miscellaneousNotes = value; }
        }

        private string _pEDeductible = "";

        public string PEDeductible
        {
            get { return _pEDeductible; }
            set { _pEDeductible = value; }
        }

        private string _subjectivityNotes = "";

        public string SubjectivityNotes
        {
            get { return _subjectivityNotes; }
            set { _subjectivityNotes = value; }
        }

        private double _totalPremiumPoliza = 0.00;

        public double TotalPremiumPoliza
        {
            get { return _totalPremiumPoliza; }
            set { _totalPremiumPoliza = value; }
        }

        private int _deductibleIDPoliza = 0;

        public int DeductibleIDPoliza
        {
            get { return _deductibleIDPoliza; }
            set { _deductibleIDPoliza = value; }
        }


        private string _watercraftLimitPoliza = "";

        public string WatercraftLimitPoliza
        {
            get { return _watercraftLimitPoliza; }
            set { _watercraftLimitPoliza = value; }
        }

        private string _ratePoliza = "";

        public string RatePoliza
        {
            get { return _ratePoliza; }
            set { _ratePoliza = value; }
        }

        private string _watercraftLimitTotalPoliza = "";

        public string WatercraftLimitTotalPoliza
        {
            get { return _watercraftLimitTotalPoliza; }
            set { _watercraftLimitTotalPoliza = value; }
        }

        private bool _renewalOfYacht = false;

        public bool RenewalOfYacht
        {
            get
            {
                return this._renewalOfYacht;
            }
            set
            {
                this._renewalOfYacht = value;
            }
        }

        private string _engine = "";

        public string Engine
        {
            get { return _engine; }
            set { _engine = value; }
        }

        private string _engineSerialNumber = "";

        public string EngineSerialNumber
        {
            get { return _engineSerialNumber; }
            set { _engineSerialNumber = value; }
        }

        private string _producer = "";

        public string Producer
        {
            get { return _producer; }
            set { _producer = value; }
        }

        private string _homeportLocation = "";

        public string HomeportLocation
        {
            get { return _homeportLocation; }
            set { _homeportLocation = value; }
        }


        private bool _isCommercial = false;

        public bool IsCommercial
        {
            get
            {
                return this._isCommercial;
            }
            set
            {
                this._isCommercial = value;
            }
        }

        private string _bankPPSID = "";

        public string BankPPSID
        {
            get { return _bankPPSID; }
            set { _bankPPSID = value; }
        }

        private string _previousPolicy = "";

        public string PreviousPolicy
        {
            get { return _previousPolicy; }
            set { _previousPolicy = value; }
        }

        private bool _isAcceptQuote = false;

        public bool isAcceptQuote
        {
            get
            {
                return this._isAcceptQuote;
            }
            set
            {
                this._isAcceptQuote = value;
            }
        }

        private bool _isQuote = true;
        
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

        private string _hullDeductibleOption1 = "";

        public string HullDeductibleOption1
        {
            get { return _hullDeductibleOption1; }
            set { _hullDeductibleOption1 = value; }
        }

        private string _hullDeductibleOption2 = "";

        public string HullDeductibleOption2
        {
            get { return _hullDeductibleOption2; }
            set { _hullDeductibleOption2 = value; }
        }

        private string _policyToRenew = "";

        public string PolicyToRenew
        {
            get { return _policyToRenew; }
            set { _policyToRenew = value; }
        }

        #endregion



        #region Public Method


        public void SaveYacht(int UserID)
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

                //if (this._mode == 2)
                //    oldAutos = (Autos)Autos.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

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
            {
                base.SaveOPPQuote(UserID);    // Validate and Save Quote in Policy Class
                this.SaveYachtQuoteDB(UserID, this.TaskControlID);
            }
            else
            {
                base.SavePol(UserID);	// Validate and Save Policy
                this.SaveYachtDB(UserID, this.TaskControlID);
            } //AQUI VA LO DE POLIZA

            this.SaveNavigationLimitCollectionDB(this.TaskControlID);
            this.SaveTenderLimitDB(this.TaskControlID);
            this.SaveSurveyDB(this.TaskControlID);

            this._mode = (int)TaskControlMode.UPDATE;
            this.Mode = (int)TaskControlMode.CLEAR;

        }


        #endregion

        public void ValidateQuote()
        {
            string errorMessage = String.Empty;

            //if (this.EffectiveDate == "")
            //    errorMessage = "Effective Date is missing or wrong.";
            //else
            //    if (this.Term == 0)
            //        errorMessage = "Term is missing or wrong.";
            //    else
            //        if (this.Prospect.FirstName == "") // && this.isBusinessPolicy == false)
            //            errorMessage = "First Name is missing or wrong.";
            //        //else
            //        //    if (this.Prospect.LastName1 == "")
            //        //        errorMessage = "Last Name is missing or wrong.";
            //        else
            //            if (this.OriginatedAt == 0)
            //                errorMessage = "Originated is missing.";
            //            else
            //                if (this.TotalPremium == 0)
            //                    errorMessage = "TotalPremium must be greater than 0.";

            //throw the exception.
            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
        }

        public static Yacht GetYacht(int TaskControlID, bool isQuote)
        {
            Yacht Yat = null;
            DataTable dt = null;

            if (isQuote)
                dt = GetYachtQuoteByTaskControlIDDB(TaskControlID);
            else
                dt = GetYachtByTaskControlIDDB(TaskControlID);

            Yat = new Yacht(isQuote);

            if (isQuote)
                Yat = (Yacht)Policy.GetPolicyQuoteByTaskControlID(TaskControlID, Yat);  //PolicyQuote
            else
                Yat = (Yacht)Policy.GetPolicyByTaskControlID(TaskControlID, Yat);  //Policy

            Yat._dtYacht = dt;

            Yat = FillProperties(Yat, TaskControlID, isQuote);

            return Yat;
        }

        public static DataTable GetYachtQuoteByTaskControlIDDB(int TaskControlID)
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

            DataTable dt = exec.GetQuery("GetYachtQuoteByTaskControlID", xmlDoc);
            return dt;
        }

        public static DataTable GetYachtByTaskControlIDDB(int TaskControlID)
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

            DataTable dt = exec.GetQuery("GetYachtByTaskControlID", xmlDoc);
            return dt;
        }

        private static Yacht FillProperties(Yacht yat, int taskControlID, bool isQuote)
        {
            try
            {
                if (isQuote)
                {
                    yat.TaskControlID = int.Parse(yat._dtYacht.Rows[0]["TaskControlID"].ToString());
                    yat.BoatName = yat._dtYacht.Rows[0]["BoatName"].ToString();
                    yat.BoatYear = yat._dtYacht.Rows[0]["BoatYear"].ToString();
                    yat.BoatModel = yat._dtYacht.Rows[0]["BoatModel"].ToString();
                    yat.BoatBuilder = yat._dtYacht.Rows[0]["BoatBuilder"].ToString();
                    yat.Loa = yat._dtYacht.Rows[0]["LOA"].ToString();
                    yat.HomeportID = int.Parse(yat._dtYacht.Rows[0]["HomeportID"].ToString());
                    yat.HomeportAddress = yat._dtYacht.Rows[0]["HomeportAddress"].ToString();
                    yat.NavigationLimitID = int.Parse(yat._dtYacht.Rows[0]["NavigationLimitID"].ToString());
                    yat.HullLimit = yat._dtYacht.Rows[0]["HullLimit"].ToString();
                    yat.HullNumberRegistration = yat._dtYacht.Rows[0]["HullNumberRegistration"].ToString();
                    yat.DeductibleID1 = int.Parse(yat._dtYacht.Rows[0]["DeductibleID1"].ToString());
                    yat.DeductibleID2 = int.Parse(yat._dtYacht.Rows[0]["DeductibleID2"].ToString());
                    yat.WatercraftLimit1 = yat._dtYacht.Rows[0]["WatercraftLimit1"].ToString();
                    yat.WatercraftLimit2 = yat._dtYacht.Rows[0]["WatercraftLimit2"].ToString();
                    yat.Rate1 = yat._dtYacht.Rows[0]["Rate1"].ToString();
                    yat.Rate2 = yat._dtYacht.Rows[0]["Rate2"].ToString();
                    yat.WatercraftLimitTotal1 = yat._dtYacht.Rows[0]["WatercraftLimitTotal1"].ToString();
                    yat.WatercraftLimitTotal2 = yat._dtYacht.Rows[0]["WatercraftLimitTotal2"].ToString();
                    yat.PIID = int.Parse(yat._dtYacht.Rows[0]["PIID"].ToString());
                    yat.PILiabilityOnlyID = int.Parse(yat._dtYacht.Rows[0]["PILiabilityOnlyID"].ToString());
                    yat.OtherPI = yat._dtYacht.Rows[0]["OtherPI"].ToString();
                    yat.PIPremium = yat._dtYacht.Rows[0]["PIPremium"].ToString();
                    yat.MedicalPaymentID = int.Parse(yat._dtYacht.Rows[0]["MedicalPaymentID"].ToString());
                    yat.OtherMedicalPayment = yat._dtYacht.Rows[0]["OtherMedicalPayment"].ToString();
                    yat.MedicalPaymentPremiumTotal = yat._dtYacht.Rows[0]["MedicalPaymentPremiumTotal"].ToString();
                    yat.PersonalEffects = yat._dtYacht.Rows[0]["PersonalEffects"].ToString();
                    yat.PersonalEffectsPremium = yat._dtYacht.Rows[0]["PersonalEffectsPremium"].ToString();
                    yat.Trailer = yat._dtYacht.Rows[0]["Trailer"].ToString();
                    yat.TrailerPremium = yat._dtYacht.Rows[0]["TrailerPremium"].ToString();
                    yat.TrailerModel = yat._dtYacht.Rows[0]["TrailerModel"].ToString();
                    yat.TrailerSerial = yat._dtYacht.Rows[0]["TrailerSerial"].ToString();
                    yat.UninsuredBoaterID = int.Parse(yat._dtYacht.Rows[0]["UninsuredBoaterID"].ToString());
                    yat.OtherUninsuredBoater = yat._dtYacht.Rows[0]["OtherUninsuredBoater"].ToString();
                    yat.OtherUninsuredBoaterPremium = yat._dtYacht.Rows[0]["OtherUninsuredBoaterPremium"].ToString();
                    yat.TripTransit = yat._dtYacht.Rows[0]["TripTransit"].ToString();
                    yat.TripTransitNotes = yat._dtYacht.Rows[0]["TripTransitNotes"].ToString();
                    yat.TotalPremium1 = double.Parse(yat._dtYacht.Rows[0]["TotalPremium"].ToString());
                    yat.TotalPremium2 = double.Parse(yat._dtYacht.Rows[0]["TotalPremium2"].ToString());
                    yat.Miscellaneous = yat._dtYacht.Rows[0]["Miscellaneous"].ToString();
                    yat.MiscellaneousNotes = yat._dtYacht.Rows[0]["MiscellaneousNotes"].ToString();
                    yat.PEDeductible = yat._dtYacht.Rows[0]["PEDeductible"].ToString();
                    yat.SubjectivityNotes = yat._dtYacht.Rows[0]["SubjectivityNotes"].ToString();
                    yat.RenewalOfYacht = bool.Parse(yat._dtYacht.Rows[0]["RenewalOfYacht"].ToString());
                    yat.Producer = yat._dtYacht.Rows[0]["Producer"].ToString();
                    yat.HomeportLocation = yat._dtYacht.Rows[0]["HomeportLocation"].ToString();
                    yat.IsCommercial = bool.Parse(yat._dtYacht.Rows[0]["IsCommercial"].ToString());
                    yat.isAcceptQuote = bool.Parse(yat._dtYacht.Rows[0]["isAcceptQuote"].ToString());
                    yat.BankPPSID = yat._dtYacht.Rows[0]["BankPPSID"].ToString();
                    yat.PreviousPolicy = yat._dtYacht.Rows[0]["PreviousPolicy"].ToString();
                    yat.Engine = yat._dtYacht.Rows[0]["Engine"].ToString();
                    yat.EngineSerialNumber = yat._dtYacht.Rows[0]["EngineSerialNumber"].ToString();
                    yat.HullDeductibleOption1 = yat._dtYacht.Rows[0]["HullDeductibleOption1"].ToString();
                    yat.HullDeductibleOption2 = yat._dtYacht.Rows[0]["HullDeductibleOption2"].ToString();
                    yat.PolicyToRenew = yat._dtYacht.Rows[0]["PolicyToRenew"].ToString();
                }
                else
                {
                    yat.TaskControlID = int.Parse(yat._dtYacht.Rows[0]["TaskControlID"].ToString());
                    yat.BoatName = yat._dtYacht.Rows[0]["BoatName"].ToString();
                    yat.BoatYear = yat._dtYacht.Rows[0]["BoatYear"].ToString();
                    yat.BoatModel = yat._dtYacht.Rows[0]["BoatModel"].ToString();
                    yat.BoatBuilder = yat._dtYacht.Rows[0]["BoatBuilder"].ToString();
                    yat.Loa = yat._dtYacht.Rows[0]["LOA"].ToString();
                    yat.HomeportID = int.Parse(yat._dtYacht.Rows[0]["HomeportID"].ToString());
                    yat.HomeportAddress = yat._dtYacht.Rows[0]["HomeportAddress"].ToString();
                    yat.NavigationLimitID = int.Parse(yat._dtYacht.Rows[0]["NavigationLimitID"].ToString());
                    yat.HullLimit = yat._dtYacht.Rows[0]["HullLimit"].ToString();
                    yat.HullNumberRegistration = yat._dtYacht.Rows[0]["HullNumberRegistration"].ToString();
                    yat.PIID = int.Parse(yat._dtYacht.Rows[0]["PIID"].ToString());
                    yat.PILiabilityOnlyID = int.Parse(yat._dtYacht.Rows[0]["PILiabilityOnlyID"].ToString());
                    yat.OtherPI = yat._dtYacht.Rows[0]["OtherPI"].ToString();
                    yat.PIPremium = yat._dtYacht.Rows[0]["PIPremium"].ToString();
                    yat.MedicalPaymentID = int.Parse(yat._dtYacht.Rows[0]["MedicalPaymentID"].ToString());
                    yat.OtherMedicalPayment = yat._dtYacht.Rows[0]["OtherMedicalPayment"].ToString();
                    yat.MedicalPaymentPremiumTotal = yat._dtYacht.Rows[0]["MedicalPaymentPremiumTotal"].ToString();
                    yat.PersonalEffects = yat._dtYacht.Rows[0]["PersonalEffects"].ToString();
                    yat.PersonalEffectsPremium = yat._dtYacht.Rows[0]["PersonalEffectsPremium"].ToString();
                    yat.Trailer = yat._dtYacht.Rows[0]["Trailer"].ToString();
                    yat.TrailerPremium = yat._dtYacht.Rows[0]["TrailerPremium"].ToString();
                    yat.TrailerModel = yat._dtYacht.Rows[0]["TrailerModel"].ToString();
                    yat.TrailerSerial = yat._dtYacht.Rows[0]["TrailerSerial"].ToString();
                    yat.UninsuredBoaterID = int.Parse(yat._dtYacht.Rows[0]["UninsuredBoaterID"].ToString());
                    yat.OtherUninsuredBoater = yat._dtYacht.Rows[0]["OtherUninsuredBoater"].ToString();
                    yat.OtherUninsuredBoaterPremium = yat._dtYacht.Rows[0]["OtherUninsuredBoaterPremium"].ToString();
                    yat.TripTransit = yat._dtYacht.Rows[0]["TripTransit"].ToString();
                    yat.TripTransitNotes = yat._dtYacht.Rows[0]["TripTransitNotes"].ToString();
                    yat.Miscellaneous = yat._dtYacht.Rows[0]["Miscellaneous"].ToString();
                    yat.MiscellaneousNotes = yat._dtYacht.Rows[0]["MiscellaneousNotes"].ToString();
                    yat.PEDeductible = yat._dtYacht.Rows[0]["PEDeductible"].ToString();
                    yat.SubjectivityNotes = yat._dtYacht.Rows[0]["SubjectivityNotes"].ToString();
                    yat.RenewalOfYacht = bool.Parse(yat._dtYacht.Rows[0]["RenewalOfYacht"].ToString());
                    yat.TotalPremiumPoliza = double.Parse(yat._dtYacht.Rows[0]["TotalPremium"].ToString());
                    yat.DeductibleIDPoliza = int.Parse(yat._dtYacht.Rows[0]["DeductibleID"].ToString());
                    yat.WatercraftLimitPoliza = yat._dtYacht.Rows[0]["WatercraftLimit"].ToString();
                    yat.RatePoliza = yat._dtYacht.Rows[0]["Rate"].ToString();
                    yat.WatercraftLimitTotalPoliza = yat._dtYacht.Rows[0]["WatercraftLimitTotal"].ToString();
                    yat.Engine = yat._dtYacht.Rows[0]["Engine"].ToString();
                    yat.EngineSerialNumber = yat._dtYacht.Rows[0]["EngineSerialNumber"].ToString();
                    yat.Producer = yat._dtYacht.Rows[0]["Producer"].ToString();
                    yat.HomeportLocation = yat._dtYacht.Rows[0]["HomeportLocation"].ToString();
                    yat.IsCommercial = bool.Parse(yat._dtYacht.Rows[0]["IsCommercial"].ToString());
                    yat.BankPPSID = yat._dtYacht.Rows[0]["BankPPSID"].ToString();
                    yat.PreviousPolicy = yat._dtYacht.Rows[0]["PreviousPolicy"].ToString();
                }

                yat.TenderLimitCollection = GetTenderLimitByTaskControlID(taskControlID, isQuote);
                yat.SurveyCollection = GetSurveyByTaskControlID(taskControlID, isQuote);
                yat.NavigationLimitCollection = GetNavigationLimitCollectionByTaskControlID(taskControlID, isQuote);

                return yat;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not fill properties. ", ex);
            }
        }

        public void SaveYachtDB(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            Executor.BeginTrans();
            Executor.Insert("AddYacht", this.GetInsertYachtXml());

            Executor.CommitTrans();
        }

        private XmlDocument GetInsertYachtXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[46];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatName",
                SqlDbType.VarChar, 250, BoatName.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatYear",
                SqlDbType.VarChar, 50, BoatYear.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatModel",
                SqlDbType.VarChar, 500, BoatModel.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatBuilder",
                SqlDbType.VarChar, 500, BoatBuilder.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LOA",
                SqlDbType.VarChar, 100, Loa.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HomeportID",
                SqlDbType.Int, 0, HomeportID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HomeportAddress",
                SqlDbType.VarChar, 500, HomeportAddress.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NavigationLimitID",
                SqlDbType.Int, 0, NavigationLimitID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HullLimit",
                SqlDbType.VarChar, 500, HullLimit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HullNumberRegistration",
                SqlDbType.VarChar, 500, HullNumberRegistration.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DeductibleID",
                SqlDbType.Int, 0, DeductibleIDPoliza.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WatercraftLimit",
                SqlDbType.VarChar, 100, WatercraftLimitPoliza.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate",
                SqlDbType.VarChar, 50, RatePoliza.ToString(),
                ref cookItems);


            DbRequestXmlCooker.AttachCookItem("WatercraftLimitTotal",
                SqlDbType.VarChar, 100, WatercraftLimitTotalPoliza.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PIID",
                SqlDbType.Int, 0, PIID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PILiabilityOnlyID",
                SqlDbType.Int, 0, PILiabilityOnlyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherPI",
                SqlDbType.VarChar, 50, OtherPI.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PIPremium",
                SqlDbType.VarChar, 50, PIPremium.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPaymentID",
                SqlDbType.Int, 0, MedicalPaymentID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherMedicalPayment",
                SqlDbType.VarChar, 100, OtherMedicalPayment.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPaymentPremiumTotal",
               SqlDbType.VarChar, 100, MedicalPaymentPremiumTotal.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PersonalEffects",
               SqlDbType.VarChar, 50, PersonalEffects.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PersonalEffectsPremium",
               SqlDbType.VarChar, 50, PersonalEffectsPremium.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Trailer",
              SqlDbType.VarChar, 50, Trailer.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrailerPremium",
              SqlDbType.VarChar, 50, TrailerPremium.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrailerModel",
              SqlDbType.VarChar, 500, TrailerModel.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrailerSerial",
              SqlDbType.VarChar, 500, TrailerSerial.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UninsuredBoaterID",
                SqlDbType.Int, 0, UninsuredBoaterID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherUninsuredBoater",
              SqlDbType.VarChar, 100, OtherUninsuredBoater.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherUninsuredBoaterPremium",
              SqlDbType.VarChar, 100, OtherUninsuredBoaterPremium.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TripTransit",
                SqlDbType.VarChar, 100, TripTransit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TripTransitNotes",
                SqlDbType.VarChar, 5000, TripTransitNotes.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
             SqlDbType.Float, 0, TotalPremiumPoliza.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Miscellaneous",
                SqlDbType.VarChar, 500, Miscellaneous.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MiscellaneousNotes",
               SqlDbType.VarChar, 5000, MiscellaneousNotes.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PEDeductible",
                SqlDbType.VarChar, 50, PEDeductible.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SubjectivityNotes",
              SqlDbType.VarChar, 5000, SubjectivityNotes.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalOfYacht",
             SqlDbType.Bit, 0, RenewalOfYacht.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Engine",
                SqlDbType.VarChar, 500, Engine.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EngineSerialNumber",
                SqlDbType.VarChar, 500, EngineSerialNumber.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Producer",
                SqlDbType.VarChar, 500, Producer.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HomeportLocation",
               SqlDbType.VarChar, 500, HomeportLocation.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsCommercial",
            SqlDbType.Bit, 0, IsCommercial.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankPPSID",
               SqlDbType.VarChar, 50, BankPPSID.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PreviousPolicy",
               SqlDbType.VarChar, 50, PreviousPolicy.ToString(),
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


        public void SaveYachtQuoteDB(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


            Executor.BeginTrans();
            Executor.Insert("AddYachtQuote", this.GetInsertYachtQuoteXml());

            Executor.CommitTrans();
        }

        private XmlDocument GetInsertYachtQuoteXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[55];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatName",
                SqlDbType.VarChar, 250, BoatName.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatYear",
                SqlDbType.VarChar, 50, BoatYear.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatModel",
                SqlDbType.VarChar, 500, BoatModel.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BoatBuilder",
                SqlDbType.VarChar, 500, BoatBuilder.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LOA",
                SqlDbType.VarChar, 100, Loa.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HomeportID",
                SqlDbType.Int, 0, HomeportID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HomeportAddress",
                 SqlDbType.VarChar, 500, HomeportAddress.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NavigationLimitID",
                SqlDbType.Int, 0, NavigationLimitID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HullLimit",
                SqlDbType.VarChar, 500, HullLimit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HullNumberRegistration",
                SqlDbType.VarChar, 500, HullNumberRegistration.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DeductibleID1",
                SqlDbType.Int, 0, DeductibleID1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DeductibleID2",
                SqlDbType.Int, 0, DeductibleID2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WatercraftLimit1",
                SqlDbType.VarChar, 100, WatercraftLimit1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WatercraftLimit2",
                SqlDbType.VarChar, 100, WatercraftLimit2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate1",
                SqlDbType.VarChar, 50, Rate1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rate2",
                SqlDbType.VarChar, 50, Rate2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WatercraftLimitTotal1",
                SqlDbType.VarChar, 100, WatercraftLimitTotal1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WatercraftLimitTotal2",
                SqlDbType.VarChar, 100, WatercraftLimitTotal2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PIID",
                SqlDbType.Int, 0, PIID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PILiabilityOnlyID",
                SqlDbType.Int, 0, PILiabilityOnlyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherPI",
                SqlDbType.VarChar, 50, OtherPI.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PIPremium",
                SqlDbType.VarChar, 50, PIPremium.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPaymentID",
                SqlDbType.Int, 0, MedicalPaymentID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherMedicalPayment",
                SqlDbType.VarChar, 100, OtherMedicalPayment.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPaymentPremiumTotal",
               SqlDbType.VarChar, 100, MedicalPaymentPremiumTotal.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PersonalEffects",
               SqlDbType.VarChar, 50, PersonalEffects.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PersonalEffectsPremium",
               SqlDbType.VarChar, 50, PersonalEffectsPremium.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Trailer",
              SqlDbType.VarChar, 50, Trailer.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrailerPremium",
              SqlDbType.VarChar, 50, TrailerPremium.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UninsuredBoaterID",
                SqlDbType.Int, 0, UninsuredBoaterID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherUninsuredBoater",
              SqlDbType.VarChar, 100, OtherUninsuredBoater.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherUninsuredBoaterPremium",
              SqlDbType.VarChar, 100, OtherUninsuredBoaterPremium.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TripTransit",
                SqlDbType.VarChar, 100, TripTransit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TripTransitNotes",
                SqlDbType.VarChar, 5000, TripTransitNotes.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
             SqlDbType.Float, 0, TotalPremium1.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium2",
                SqlDbType.Float, 0, TotalPremium2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Miscellaneous",
                SqlDbType.VarChar, 500, Miscellaneous.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MiscellaneousNotes",
               SqlDbType.VarChar, 5000, MiscellaneousNotes.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PEDeductible",
                SqlDbType.VarChar, 50, PEDeductible.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SubjectivityNotes",
              SqlDbType.VarChar, 5000, SubjectivityNotes.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalOfYacht",
                SqlDbType.Bit, 0, RenewalOfYacht.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Producer",
                SqlDbType.VarChar, 500, Producer.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HomeportLocation",
                SqlDbType.VarChar, 500, HomeportLocation.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsCommercial",
                SqlDbType.Bit, 0, IsCommercial.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isAcceptQuote",
               SqlDbType.Bit, 0, isAcceptQuote.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrailerModel",
              SqlDbType.VarChar, 500, TrailerModel.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TrailerSerial",
              SqlDbType.VarChar, 500, TrailerSerial.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Engine",
                SqlDbType.VarChar, 500, Engine.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EngineSerialNumber",
                SqlDbType.VarChar, 500, EngineSerialNumber.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankPPSID",
               SqlDbType.VarChar, 50, BankPPSID.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PreviousPolicy",
              SqlDbType.VarChar, 50, PreviousPolicy.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HullDeductibleOption1",
              SqlDbType.VarChar, 100, HullDeductibleOption1.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HullDeductibleOption2",
                SqlDbType.VarChar, 100, HullDeductibleOption2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyToRenew",
                SqlDbType.VarChar, 100, PolicyToRenew.ToString(),
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

        public static void DeleteYachtByTaskControlID(int taskControlID, bool isQuote)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                Executor.BeginTrans();
                Executor.Update("DeleteYachtByTaskControlID", DeleteYachtByTaskControlIDXml(taskControlID, isQuote));
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error. Please try again. " + xcp.Message, xcp);
            }
        }

        private static XmlDocument DeleteYachtByTaskControlIDXml(int taskControlID, bool isQuote)
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

        //AQUI EMPIEZA SURVEY

        public void SaveSurveyDB(int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            Executor.Update("DeleteSurveyByTaskControlID", DeleteSurveyByTaskControlIDXml(taskControlID));

            for (int i = 0; i < SurveyCollection.Rows.Count; i++)
            {
                this.Mode = 1; //Add

                Executor.BeginTrans();
                Executor.Insert("AddSurvey", this.GetInsertSurveyXml(i));
                Executor.CommitTrans();
            }
        }

        private XmlDocument DeleteSurveyByTaskControlIDXml(int taskControlID)
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

        private XmlDocument GetInsertSurveyXml(int index)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[7];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("No",
               SqlDbType.Int, 0, SurveyCollection.Rows[index]["No"].ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SurveyorName",
                SqlDbType.VarChar, 100, SurveyCollection.Rows[index]["SurveyorName"].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SurveyFee",
                SqlDbType.VarChar, 100, SurveyCollection.Rows[index]["SurveyFee"].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SurveyDate",
                SqlDbType.VarChar, 20, SurveyCollection.Rows[index]["SurveyDate"].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SurveyDateCompleted",
                SqlDbType.VarChar, 20, SurveyCollection.Rows[index]["SurveyDateCompleted"].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Recomendaciones",
               SqlDbType.Bit, 0, SurveyCollection.Rows[index]["Recomendaciones"].ToString(),
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

        public static DataTable GetSurveyByTaskControlID(int TaskControlID, bool isQuote)
        {
            DataTable dt = GetSurveyByTaskControlIDDB(TaskControlID);

            return dt;
        }

        public static DataTable GetSurveyByTaskControlIDDB(int TaskControlID)
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

            DataTable dt = exec.GetQuery("GetSurveyByTaskControlID", xmlDoc);
            return dt;
        }


        private DataTable DataTableSurveyTemp()
        {
            DataTable myDataTable = new DataTable("DataTableSurveyTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "No";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "No";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
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
            myDataColumn.ColumnName = "SurveyorName";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SurveyorName";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "SurveyFee";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SurveyFee";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "SurveyDate";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SurveyDate";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "SurveyDateCompleted";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SurveyDateCompleted";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Recomendaciones";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Recomendaciones";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }


       //AQUI EMPIEZA TENDER LIMIT


        public void SaveTenderLimitDB(int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            Executor.Update("DeleteTenderLimitByTaskControlID", DeleteTenderLimitByTaskControlIDXml(taskControlID));

            for (int i = 0; i < TenderLimitCollection.Rows.Count; i++)
            {
                this.Mode = 1; //Add

                Executor.BeginTrans();
                Executor.Insert("AddTenderLimit", this.GetInsertTenderLimitXml(i));
                Executor.CommitTrans();
            }
        }

        private XmlDocument DeleteTenderLimitByTaskControlIDXml(int taskControlID)
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

        private XmlDocument GetInsertTenderLimitXml(int index)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[5];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("No",
               SqlDbType.Int, 0, TenderLimitCollection.Rows[index]["No"].ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TenderLimitAmount",
                SqlDbType.VarChar, 100, TenderLimitCollection.Rows[index]["TenderLimitAmount"].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TenderDesc",
                SqlDbType.VarChar, 100, TenderLimitCollection.Rows[index]["TenderDesc"].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TenderSerial",
               SqlDbType.VarChar, 100, TenderLimitCollection.Rows[index]["TenderSerial"].ToString(),
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

        public static DataTable GetTenderLimitByTaskControlID(int TaskControlID, bool isQuote)
        {
            DataTable dt = GetTenderLimitByTaskControlIDDB(TaskControlID);

            return dt;
        }

        public static DataTable GetTenderLimitByTaskControlIDDB(int TaskControlID)
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

            DataTable dt = exec.GetQuery("GetTenderLimitByTaskControlID", xmlDoc);
            return dt;
        }


        private DataTable DataTableTenderLimitTemp()
        {
            DataTable myDataTable = new DataTable("DataTableTenderLimitTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "No";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "No";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
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
            myDataColumn.ColumnName = "TenderLimitAmount";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TenderLimitAmount";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }

        //AQUI EMPIEZA NAVIGATION LIMIT COLLECTION

        public void SaveNavigationLimitCollectionDB(int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            Executor.Update("DeleteNavigationLimitCollectionByTaskControlID", DeleteNavigationLimitCollectionByTaskControlIDXml(taskControlID));

            for (int i = 0; i < NavigationLimitCollection.Rows.Count; i++)
            {
                this.Mode = 1; //Add

                Executor.BeginTrans();
                Executor.Insert("AddNavigationLimitCollection", this.GetInsertNavigationLimitCollectionXml(i));
                Executor.CommitTrans();
            }
        }

        private XmlDocument DeleteNavigationLimitCollectionByTaskControlIDXml(int taskControlID)
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

        private XmlDocument GetInsertNavigationLimitCollectionXml(int index)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("No",
               SqlDbType.Int, 0, NavigationLimitCollection.Rows[index]["No"].ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NavigationLimitID",
                SqlDbType.Int, 0, NavigationLimitCollection.Rows[index]["NavigationLimitID"].ToString(),
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

        public static DataTable GetNavigationLimitCollectionByTaskControlID(int TaskControlID, bool isQuote)
        {
            DataTable dt = GetNavigationLimitCollectionByTaskControlIDDB(TaskControlID);

            return dt;
        }

        public static DataTable GetNavigationLimitCollectionByTaskControlIDDB(int TaskControlID)
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

            DataTable dt = exec.GetQuery("GetNavigationLimitCollectionByTaskControlID", xmlDoc);
            return dt;
        }

        private DataTable DataTableNavigationLimitTemp()
        {
            DataTable myDataTable = new DataTable("DataTableNavigationLimitTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "No";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "No";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
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
            myDataColumn.ColumnName = "NavigationLimitID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NavigationLimitID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NavigationLimitDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NavigationLimitDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }

    }
}
