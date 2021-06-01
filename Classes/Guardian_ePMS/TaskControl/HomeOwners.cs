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
    public class HomeOwners : Policy
    {
        public HomeOwners(bool isQuote)
        {
            this.isQuote = isQuote;
            this.EffectiveDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            this.ExpirationDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now.AddMonths(12));
            this.PolicyType = "HOM";
            this.PolicyClassID = 25;
            this.InsuranceCompany = "001";
            if (this.isQuote)
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Home Owners Policy Quote"));
            else
            {
                this.DepartmentID = 1;
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Home Owners Policy"));
            }

            this.TaskStatusID = int.Parse(LookupTables.LookupTables.GetID("TaskStatus", "Open"));
            this.AutoAssignPolicy = true;
        }

        private int _homeID = 0;
        private string _RenewalNo = "";
        private HomeOwners oldHome = null;
        private int _mode = (int)TaskControlMode.CLEAR;
        private bool _isQuote = false;
        private string _bank = "";
        private string _bank2 = "";
        private string _loanNo = "";
        private string _loanNo2 = "";
        private string _typeOfInterest = "";
        private bool _mortgageeBilled = false;
        private string _catastropheCov = "";
        private string _catastropheDeduc = "";
        private string _windstormDeduc = "";
        private string _allOtherPerDeduc = "";
        private string _constructionType = "";
        private string _constructionYear = "";
        private string _numOfStories = "";
        private string _numOfFamilies = "";
        private string _ifYes = "";
        private string _livingArea = "";
        private string _porsches_deck = "";
        private string _roofDwelling = "";
        private string _earthquakeDeduc = "";
        private string _residence = "";
        private string _propertyType = "";
        private string _propertyForm = "";
        private bool _losses3Years = false;
        private string _otherStructuresType = "";
        private bool _isPropShuttered = false;
        private string _roofOverhang = "";
        private bool _autoPolicy = false;
        private string _autoPolicyNo = "";
        private double _limit1 = 0.0;
        private double _aopDed1 = 0.0;
        private double _windstormDed1 = 0.0;
        private string _windstormDedPer1 = "0%";
        private double _earthquakeDed1 = 0.0;
        private string _earthquakeDedper1 = "0%";
        private string _coinsurance1 = "0%";
        private double _premium1 = 0.0;
        private double _limit2 = 0.0;
        private double _aopDed2 = 0.0;
        private double _windstormDed2 = 0.0;
        private string _windstormDedPer2 = "0%";
        private double _earthquakeDed2 = 0.0;
        private string _earthquakeDedper2 = "0%";
        private string _coinsurance2 = "0%";
        private double _premium2 = 0.0;
        private double _limit3 = 0.0;
        private double _aopDed3 = 0.0;
        private double _windstormDed3 = 0.0;
        private string _windstormDedPer3 = "0%";
        private double _earthquakeDed3 = 0.0;
        private string _earthquakeDedper3 = "0%";
        private string _coinsurance3 = "0%";
        private double _premium3 = 0.0;
        private double _limit4 = 0.0;
        private double _aopDed4 = 0.0;
        private double _windstormDed4 = 0.0;
        private string _windstormDedPer4 = "0%";
        private double _earthquakeDed4 = 0.0;
        private string _earthquakeDedper4 = "0%";
        private string _coinsurance4 = "0%";
        private double _premium4 = 0.0;
        private double _totalLimit = 0.0;
        private double _totalWindstorm = 0.0;
        private double _totalEarthquake = 0.0;
        private double _totalPremium = 0.0;
        private string _liaPropertyType = "";
        private string _liaPolicyType = "";
        private string _liaNumOfFamilies = "";
        private double _liaLimit = 0.0;
        private double _liaMedicalPayments = 0.0;
        private double _liaPremium = 0.0;
        private double _premium = 0.0;
        private double _liaTotalPremium = 0.0;
        private bool _isUpgraded = false;
        private string _comments = "";
        private string _occupation = "";
        private bool _ongoingConst = false;
        private bool _additionalStructure = false;
        private string _office = "";
        private bool _approved = false;
        private string _comment = "";
        private bool _submitted = false;
        private bool _rejected = false;
        private string _island = "";
        private string _BankPPSID = "";
        private string _BankPPSID2 = "";
        private double _GrossTax = 0.0;

        private string _Inspector = "";
        private string _InspectionDate = "";

        private string _PreviousPolicy = "";

        private bool _isRenew = false;


        private double _DiscountsHomeOwners = 0.0;

        private int _TypeOfInsuredID = 0;

        private string _SubmittedDate = "";

        public string SubmittedDate
        {
            get { return this._SubmittedDate; }
            set { this._SubmittedDate = value; }
        }

        public int TypeOfInsuredID
        {
            get { return this._TypeOfInsuredID; }
            set { this._TypeOfInsuredID = value; }
        }

        public double DiscountsHomeOwners
        {
            get { return this._DiscountsHomeOwners; }
            set { this._DiscountsHomeOwners = value; }
        }

        public bool isRenew
        {
            get { return this._isRenew; }
            set { this._isRenew = value; }
        }

        public string PreviousPolicy
        {
            get { return this._PreviousPolicy; }
            set { this._PreviousPolicy = value; }
        }

        public string Inspector
        {
            get { return this._Inspector; }
            set { this._Inspector = value; }
        }
        public string InspectionDate
        {
            get { return this._InspectionDate; }
            set { this._InspectionDate = value; }
        }

        public double GrossTax
        {
            get { return this._GrossTax; }
            set { this._GrossTax = value; }
        }

        public string BankPPSID
        {
            get { return this._BankPPSID; }
            set { this._BankPPSID = value; }
        }

        public string BankPPSID2
        {
            get { return this._BankPPSID2; }
            set { this._BankPPSID2 = value; }
        }

        public string Island
        {
            get
            {
                return this._island;
            }
            set
            {
                this._island = value;
            }
        }

        private DataTable _dtHomeOwners;

        public int homeID
        {
            get
            {
                return this._homeID;
            }
            set
            {
                this._homeID = value;
            }
        }

        public string renewalNo
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

        public string bank2
        {
            get
            {
                return this._bank2;
            }
            set
            {
                this._bank2 = value;
            }
        }

        public string bank
        {
            get
            {
                return this._bank;
            }
            set
            {
                this._bank = value;
            }
        }

        public string loanNo
        {
            get
            {
                return this._loanNo;
            }
            set
            {
                this._loanNo = value;
            }
        }

        public string loanNo2
        {
            get
            {
                return this._loanNo2;
            }
            set
            {
                this._loanNo2 = value;
            }
        }

        public string typeOfInterest
        {
            get
            {
                return this._typeOfInterest;
            }
            set
            {
                this._typeOfInterest = value;
            }
        }

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

        public bool mortgageeBilled
        {
            get
            {
                return this._mortgageeBilled;
            }
            set
            {
                this._mortgageeBilled = value;
            }
        }

        public string catastropheCov
        {
            get
            {
                return this._catastropheCov;
            }
            set
            {
                this._catastropheCov = value;
            }
        }

        public string catastropheDeduc
        {
            get
            {
                return this._catastropheDeduc;
            }
            set
            {
                this._catastropheDeduc = value;
            }
        }

        public string windstormDeduc
        {
            get
            {
                return this._windstormDeduc;
            }
            set
            {
                this._windstormDeduc = value;
            }
        }

        public string allOtherPerDeduc
        {
            get
            {
                return this._allOtherPerDeduc;
            }
            set
            {
                this._allOtherPerDeduc = value;
            }
        }

        public string constructionType
        {
            get
            {
                return this._constructionType;
            }
            set
            {
                this._constructionType = value;
            }
        }

        public string constructionYear
        {
            get
            {
                return this._constructionYear;
            }
            set
            {
                this._constructionYear = value;
            }
        }


        public string numOfStories
        {
            get
            {
                return this._numOfStories;
            }
            set
            {
                this._numOfStories = value;
            }
        }

        public string numOfFamilies
        {
            get
            {
                return this._numOfFamilies;
            }
            set
            {
                this._numOfFamilies = value;
            }
        }

        public string ifYes
        {
            get
            {
                return this._ifYes;
            }
            set
            {
                this._ifYes = value;
            }
        }

        public string livingArea
        {
            get
            {
                return this._livingArea;
            }
            set
            {
                this._livingArea = value;
            }
        }

        public string porshcesDeck
        {
            get
            {
                return this._porsches_deck;
            }
            set
            {
                this._porsches_deck = value;
            }
        }

        public string roofDwelling
        {
            get
            {
                return this._roofDwelling;
            }
            set
            {
                this._roofDwelling = value;
            }
        }

        public string earthquakeDeduc
        {
            get
            {
                return this._earthquakeDeduc;
            }
            set
            {
                this._earthquakeDeduc = value;
            }
        }

        public string residence
        {
            get
            {
                return this._residence;
            }
            set
            {
                this._residence = value;
            }
        }

        public string propertyType
        {
            get
            {
                return this._propertyType;
            }
            set
            {
                this._propertyType = value;
            }
        }

        public string propertyForm
        {
            get
            {
                return this._propertyForm;
            }
            set
            {
                this._propertyForm = value;
            }
        }

        public bool losses3Year
        {
            get
            {
                return this._losses3Years;
            }
            set
            {
                this._losses3Years = value;
            }
        }

        public string otherStructuresType
        {
            get
            {
                return this._otherStructuresType;
            }
            set
            {
                this._otherStructuresType = value;
            }
        }

        public bool isPropShuttered
        {
            get
            {
                return this._isPropShuttered;
            }
            set
            {
                this._isPropShuttered = value;
            }
        }

        public string roofOverhang
        {
            get
            {
                return this._roofOverhang;
            }
            set
            {
                this._roofOverhang = value;
            }
        }

        public bool autoPolicy
        {
            get
            {
                return this._autoPolicy;
            }
            set
            {
                this._autoPolicy = value;
            }
        }

        public string autoPolicyNo
        {
            get
            {
                return this._autoPolicyNo;
            }
            set
            {
                this._autoPolicyNo = value;
            }
        }

        public double limit1
        {
            get
            {
                return this._limit1;
            }
            set
            {
                this._limit1 = value;
            }
        }

        public double aopDed1
        {
            get
            {
                return this._aopDed1;
            }
            set
            {
                this._aopDed1 = value;
            }
        }

        public double windstormDed1
        {
            get
            {
                return this._windstormDed1;
            }
            set
            {
                this._windstormDed1 = value;
            }
        }

        public string windstormDedPer1
        {
            get
            {
                return this._windstormDedPer1;
            }
            set
            {
                this._windstormDedPer1 = value;
            }
        }

        public double earthquakeDed1
        {
            get
            {
                return this._earthquakeDed1;
            }
            set
            {
                this._earthquakeDed1 = value;
            }
        }

        public string earthquakeDedper1
        {
            get
            {
                return this._earthquakeDedper1;
            }
            set
            {
                this._earthquakeDedper1 = value;
            }
        }

        public string coinsurance1
        {
            get
            {
                return this._coinsurance1;
            }
            set
            {
                this._coinsurance1 = value;
            }
        }

        public double premium1
        {
            get
            {
                return this._premium1;
            }
            set
            {
                this._premium1 = value;
            }
        }

        public double limit2
        {
            get
            {
                return this._limit2;
            }
            set
            {
                this._limit2 = value;
            }
        }

        public double aopDed2
        {
            get
            {
                return this._aopDed2;
            }
            set
            {
                this._aopDed2 = value;
            }
        }

        public double windstormDed2
        {
            get
            {
                return this._windstormDed2;
            }
            set
            {
                this._windstormDed2 = value;
            }
        }

        public string windstormDedPer2
        {
            get
            {
                return this._windstormDedPer2;
            }
            set
            {
                this._windstormDedPer2 = value;
            }
        }

        public double earthquakeDed2
        {
            get
            {
                return this._earthquakeDed2;
            }
            set
            {
                this._earthquakeDed2 = value;
            }
        }

        public string earthquakeDedper2
        {
            get
            {
                return this._earthquakeDedper2;
            }
            set
            {
                this._earthquakeDedper2 = value;
            }
        }

        public string coinsurance2
        {
            get
            {
                return this._coinsurance2;
            }
            set
            {
                this._coinsurance2 = value;
            }
        }

        public double premium2
        {
            get
            {
                return this._premium2;
            }
            set
            {
                this._premium2 = value;
            }
        }

        public double limit3
        {
            get
            {
                return this._limit3;
            }
            set
            {
                this._limit3 = value;
            }
        }

        public double aopDed3
        {
            get
            {
                return this._aopDed3;
            }
            set
            {
                this._aopDed3 = value;
            }
        }

        public double windstormDed3
        {
            get
            {
                return this._windstormDed3;
            }
            set
            {
                this._windstormDed3 = value;
            }
        }

        public string windstormDedPer3
        {
            get
            {
                return this._windstormDedPer3;
            }
            set
            {
                this._windstormDedPer3 = value;
            }
        }

        public double earthquakeDed3
        {
            get
            {
                return this._earthquakeDed3;
            }
            set
            {
                this._earthquakeDed3 = value;
            }
        }

        public string earthquakeDedper3
        {
            get
            {
                return this._earthquakeDedper3;
            }
            set
            {
                this._earthquakeDedper3 = value;
            }
        }

        public string coinsurance3
        {
            get
            {
                return this._coinsurance3;
            }
            set
            {
                this._coinsurance3 = value;
            }
        }

        public double premium3
        {
            get
            {
                return this._premium3;
            }
            set
            {
                this._premium3 = value;
            }
        }

        public double limit4
        {
            get
            {
                return this._limit4;
            }
            set
            {
                this._limit4 = value;
            }
        }

        public double aopDed4
        {
            get
            {
                return this._aopDed4;
            }
            set
            {
                this._aopDed4 = value;
            }
        }

        public double windstormDed4
        {
            get
            {
                return this._windstormDed4;
            }
            set
            {
                this._windstormDed4 = value;
            }
        }

        public string windstormDedPer4
        {
            get
            {
                return this._windstormDedPer4;
            }
            set
            {
                this._windstormDedPer4 = value;
            }
        }

        public double earthquakeDed4
        {
            get
            {
                return this._earthquakeDed4;
            }
            set
            {
                this._earthquakeDed4 = value;
            }
        }

        public string earthquakeDedper4
        {
            get
            {
                return this._earthquakeDedper4;
            }
            set
            {
                this._earthquakeDedper4 = value;
            }
        }

        public string coinsurance4
        {
            get
            {
                return this._coinsurance4;
            }
            set
            {
                this._coinsurance4 = value;
            }
        }

        public double premium4
        {
            get
            {
                return this._premium4;
            }
            set
            {
                this._premium4 = value;
            }
        }

        public double totalLimit
        {
            get
            {
                return this._totalLimit;
            }
            set
            {
                this._totalLimit = value;
            }
        }

        public double totalWindstorm
        {
            get
            {
                return this._totalWindstorm;
            }
            set
            {
                this._totalWindstorm = value;
            }
        }

        public double totalEarthquake
        {
            get
            {
                return this._totalEarthquake;
            }
            set
            {
                this._totalEarthquake = value;
            }
        }

        public double totalPremium
        {
            get
            {
                return this._totalPremium;
            }
            set
            {
                this._totalPremium = value;
            }
        }

        public string liaPropertyType
        {
            get
            {
                return this._liaPropertyType;
            }
            set
            {
                this._liaPropertyType = value;
            }
        }

        public string liaPolicyType
        {
            get
            {
                return this._liaPolicyType;
            }
            set
            {
                this._liaPolicyType = value;
            }
        }

        public string liaNumOfFamilies
        {
            get
            {
                return this._liaNumOfFamilies;
            }
            set
            {
                this._liaNumOfFamilies = value;
            }
        }

        public double liaLimit
        {
            get
            {
                return this._liaLimit;
            }
            set
            {
                this._liaLimit = value;
            }
        }

        public double liaMedicalPayments
        {
            get
            {
                return this._liaMedicalPayments;
            }
            set
            {
                this._liaMedicalPayments = value;
            }
        }

        public double liaPremium
        {
            get
            {
                return this._liaPremium;
            }
            set
            {
                this._liaPremium = value;
            }
        }

        public double premium
        {
            get
            {
                return this._premium;
            }
            set
            {
                this._premium = value;
            }
        }

        public double liaTotalPremium
        {
            get
            {
                return this._liaTotalPremium;
            }
            set
            {
                this._liaTotalPremium = value;
            }
        }

        public bool isUpgraded
        {
            get
            {
                return this._isUpgraded;
            }
            set
            {
                this._isUpgraded = value;
            }
        }


        public bool additionalStructure
        {
            get
            {
                return this._additionalStructure;
            }
            set
            {
                this._additionalStructure = value;
            }
        }
        public string comments
        {
            get
            {
                return this._comments;
            }
            set
            {
                this._comments = value;
            }
        }

        public string occupation
        {
            get
            {
                return this._occupation;
            }
            set
            {
                this._occupation = value;
            }
        }

        public string office
        {
            get
            {
                return this._office;
            }
            set
            {
                this._office = value;
            }
        }

        public bool approved
        {
            get
            {
                return this._approved;
            }
            set
            {
                this._approved = value;
            }
        }

        public string comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public bool submitted
        {
            get
            {
                return this._submitted;
            }
            set
            {
                this._submitted = value;
            }
        }

        public bool rejected
        {
            get
            {
                return this._rejected;
            }
            set
            {
                this._rejected = value;
            }
        }

        public void ValidateQuote()
        {


            string errorMessage = String.Empty;



            if (this.EffectiveDate == "")
                errorMessage = "Effective Date is missing or wrong.";

            else if (this.Customer.FirstName == "")
                errorMessage = "Name insured is missing or wrong.";





            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
            //throw the exception.

        }


        public void saveHomeOwners(int UserID)
        {
            this._mode = (int)this.Mode;  // Se le asigna el mode de taskControl.
            this.PolicyMode = (int)this.Mode;  // Se le asigna el mode de taskControl.

            if (isQuote)
            {
                this.ValidateQuote();


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
                    oldHome = (HomeOwners)HomeOwners.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

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

            this.saveHomeOwnersDB();

        }

        private void saveHomeOwnersDB()
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this.Mode)
                {
                    case 1:  //ADD
                        this.homeID = Executor.Insert("AddHomeOwners", this.getInsertHomeXML());
                        //this.History(this._mode, UserID);
                        break;

                    case 3:  //DELETE
                        //Executor.Update("DeleteAutoGuardServicesContract",this.GetDeletePoliciesXml());
                        break;

                    default: //UPDATE
                        //this.History(this._mode, UserID);
                        this.homeID = Executor.Insert("AddHomeOwners", this.getInsertHomeXML());
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


        private static HomeOwners FillProperties(HomeOwners home, int taskControlID, bool isQuote)
        {
            try
            {
                home.TaskControlID = int.Parse(home._dtHomeOwners.Rows[0]["TaskControlID"].ToString());
                home.bank2 = home._dtHomeOwners.Rows[0]["Bank2"].ToString();
                home.bank = home._dtHomeOwners.Rows[0]["Bank"].ToString();
                home.loanNo = home._dtHomeOwners.Rows[0]["LoanNo"].ToString();
                home.loanNo2 = home._dtHomeOwners.Rows[0]["LoanNo2"].ToString();
                home.typeOfInterest = home._dtHomeOwners.Rows[0]["TypeOfInterest"].ToString();
                home.mortgageeBilled = bool.Parse(home._dtHomeOwners.Rows[0]["MortgageeBilled"].ToString());
                home.catastropheCov = home._dtHomeOwners.Rows[0]["CatastropheCov"].ToString();
                home.catastropheDeduc = home._dtHomeOwners.Rows[0]["CatastropheDeduc"].ToString();
                home.windstormDeduc = home._dtHomeOwners.Rows[0]["WindstormDeduc"].ToString();
                home.allOtherPerDeduc = home._dtHomeOwners.Rows[0]["AllOtherPerDeduc"].ToString();
                home.constructionType = home._dtHomeOwners.Rows[0]["ConstructionType"].ToString();
                home.constructionYear = home._dtHomeOwners.Rows[0]["ConstructionYear"].ToString();
                home.numOfStories = home._dtHomeOwners.Rows[0]["NumOfStories"].ToString();
                home.numOfFamilies = home._dtHomeOwners.Rows[0]["NumOfFamilies"].ToString();
                home.ifYes = home._dtHomeOwners.Rows[0]["Ifyes"].ToString();
                home.livingArea = home._dtHomeOwners.Rows[0]["LivingArea"].ToString();
                home.porshcesDeck = home._dtHomeOwners.Rows[0]["Porsches_Deck"].ToString();
                home.roofDwelling = home._dtHomeOwners.Rows[0]["RoofDwelling"].ToString();
                home.earthquakeDeduc = home._dtHomeOwners.Rows[0]["EarthquakeDeduc"].ToString();
                home.residence = home._dtHomeOwners.Rows[0]["Residence"].ToString();
                home.propertyType = home._dtHomeOwners.Rows[0]["PropertyType"].ToString();
                home.propertyForm = home._dtHomeOwners.Rows[0]["PropertyForm"].ToString();
                home.losses3Year = bool.Parse(home._dtHomeOwners.Rows[0]["Losses3Years"].ToString());
                home.otherStructuresType = home._dtHomeOwners.Rows[0]["OtherStructuresType"].ToString();
                home.isPropShuttered = bool.Parse(home._dtHomeOwners.Rows[0]["IsPropShuttered"].ToString());
                home.roofOverhang = home._dtHomeOwners.Rows[0]["RoofOverhang"].ToString();
                home.autoPolicy = bool.Parse(home._dtHomeOwners.Rows[0]["AutoPolicy"].ToString());
                home.autoPolicyNo = home._dtHomeOwners.Rows[0]["AutoPolicyNo"].ToString();
                home.limit1 = double.Parse(home._dtHomeOwners.Rows[0]["Limit1"].ToString());
                home.aopDed1 = double.Parse(home._dtHomeOwners.Rows[0]["AOPDed1"].ToString());
                home.windstormDed1 = double.Parse(home._dtHomeOwners.Rows[0]["WindstormDed1"].ToString());
                home.windstormDedPer1 = home._dtHomeOwners.Rows[0]["WindstormDedPer1"].ToString();
                home.earthquakeDed1 = double.Parse(home._dtHomeOwners.Rows[0]["EarthquakeDed1"].ToString());
                home.earthquakeDedper1 = home._dtHomeOwners.Rows[0]["EarthquakeDedper1"].ToString();
                home.coinsurance1 = home._dtHomeOwners.Rows[0]["Coinsurance1"].ToString();
                home.premium1 = double.Parse(home._dtHomeOwners.Rows[0]["Premium1"].ToString());
                home.limit2 = double.Parse(home._dtHomeOwners.Rows[0]["Limit2"].ToString());
                home.aopDed2 = double.Parse(home._dtHomeOwners.Rows[0]["AOPDed2"].ToString());
                home.windstormDed2 = double.Parse(home._dtHomeOwners.Rows[0]["WindstormDed2"].ToString());
                home.windstormDedPer2 = home._dtHomeOwners.Rows[0]["WindstormDedPer2"].ToString();
                home.earthquakeDed2 = double.Parse(home._dtHomeOwners.Rows[0]["EarthquakeDed2"].ToString());
                home.earthquakeDedper2 = home._dtHomeOwners.Rows[0]["EarthquakeDedper2"].ToString();
                home.coinsurance2 = home._dtHomeOwners.Rows[0]["Coinsurance2"].ToString();
                home.premium2 = double.Parse(home._dtHomeOwners.Rows[0]["Premium2"].ToString());
                home.limit3 = double.Parse(home._dtHomeOwners.Rows[0]["Limit3"].ToString());
                home.aopDed3 = double.Parse(home._dtHomeOwners.Rows[0]["AOPDed3"].ToString());
                home.windstormDed3 = double.Parse(home._dtHomeOwners.Rows[0]["WindstormDed3"].ToString());
                home.windstormDedPer3 = home._dtHomeOwners.Rows[0]["WindstormDedPer3"].ToString();
                home.earthquakeDed3 = double.Parse(home._dtHomeOwners.Rows[0]["EarthquakeDed3"].ToString());
                home.earthquakeDedper3 = home._dtHomeOwners.Rows[0]["EarthquakeDedper3"].ToString();
                home.coinsurance3 = home._dtHomeOwners.Rows[0]["Coinsurance3"].ToString();
                home.premium3 = double.Parse(home._dtHomeOwners.Rows[0]["Premium3"].ToString());
                home.limit4 = double.Parse(home._dtHomeOwners.Rows[0]["Limit4"].ToString());
                home.aopDed4 = double.Parse(home._dtHomeOwners.Rows[0]["AOPDed4"].ToString());
                home.windstormDed4 = double.Parse(home._dtHomeOwners.Rows[0]["WindstormDed4"].ToString());
                home.windstormDedPer4 = home._dtHomeOwners.Rows[0]["WindstormDedPer4"].ToString();
                home.earthquakeDed4 = double.Parse(home._dtHomeOwners.Rows[0]["EarthquakeDed4"].ToString());
                home.earthquakeDedper4 = home._dtHomeOwners.Rows[0]["EarthquakeDedper4"].ToString();
                home.coinsurance4 = home._dtHomeOwners.Rows[0]["Coinsurance4"].ToString();
                home.premium4 = double.Parse(home._dtHomeOwners.Rows[0]["Premium4"].ToString());
                home.totalLimit = double.Parse(home._dtHomeOwners.Rows[0]["TotalLimit"].ToString());
                home.totalWindstorm = double.Parse(home._dtHomeOwners.Rows[0]["TotalWindstorm"].ToString());
                home.totalEarthquake = double.Parse(home._dtHomeOwners.Rows[0]["TotalEarthquake"].ToString());
                home.totalPremium = double.Parse(home._dtHomeOwners.Rows[0]["TotalPremium"].ToString());
                home.liaPropertyType = home._dtHomeOwners.Rows[0]["LiaPropertyType"].ToString();
                home.liaPolicyType = home._dtHomeOwners.Rows[0]["LiaPolicyType"].ToString();
                home.liaNumOfFamilies = home._dtHomeOwners.Rows[0]["LiaNumOfFamilies"].ToString();
                home.liaLimit = double.Parse(home._dtHomeOwners.Rows[0]["LiaLimit"].ToString());
                home.liaMedicalPayments = double.Parse(home._dtHomeOwners.Rows[0]["LiaMedicalPayments"].ToString());
                home.liaPremium = double.Parse(home._dtHomeOwners.Rows[0]["LiaPremium"].ToString());
                home.premium = double.Parse(home._dtHomeOwners.Rows[0]["Premium"].ToString());
                home.liaTotalPremium = double.Parse(home._dtHomeOwners.Rows[0]["LiaTotalPremium"].ToString());
                home.renewalNo = home._dtHomeOwners.Rows[0]["RenewalNo"].ToString();
                home.isUpgraded = bool.Parse(home._dtHomeOwners.Rows[0]["isUpgraded"].ToString());
                home.additionalStructure = home._dtHomeOwners.Rows[0]["AdditionalStructure"].ToString() == "" ? false : bool.Parse(home._dtHomeOwners.Rows[0]["AdditionalStructure"].ToString());
                home.comments = home._dtHomeOwners.Rows[0]["Comments"].ToString();
                home.occupation = home._dtHomeOwners.Rows[0]["Occupation"].ToString();
                home.office = home._dtHomeOwners.Rows[0]["Office"].ToString();
                home.approved = home._dtHomeOwners.Rows[0]["Approved"].ToString() == "" ? false : bool.Parse(home._dtHomeOwners.Rows[0]["Approved"].ToString());
                home.comment = home._dtHomeOwners.Rows[0]["Comment"].ToString();
                home.submitted = home._dtHomeOwners.Rows[0]["Submitted"].ToString() == "" ? false : bool.Parse(home._dtHomeOwners.Rows[0]["Submitted"].ToString());
                home.rejected = home._dtHomeOwners.Rows[0]["Rejected"].ToString() == "" ? false : bool.Parse(home._dtHomeOwners.Rows[0]["Rejected"].ToString());
                home.Island = home._dtHomeOwners.Rows[0]["Island"].ToString();
                home.BankPPSID = home._dtHomeOwners.Rows[0]["BankPPSID"].ToString();
                home.BankPPSID2 = home._dtHomeOwners.Rows[0]["BankPPSID2"].ToString();
                home.GrossTax = double.Parse(home._dtHomeOwners.Rows[0]["GrossTax"].ToString());

                home.Inspector = home._dtHomeOwners.Rows[0]["Inspector"].ToString();
                home.InspectionDate = home._dtHomeOwners.Rows[0]["InspectionDate"].ToString();
                home.PreviousPolicy = home._dtHomeOwners.Rows[0]["PreviousPolicy"].ToString();
                home.isRenew = bool.Parse(home._dtHomeOwners.Rows[0]["isRenew"].ToString());
                home.DiscountsHomeOwners = double.Parse(home._dtHomeOwners.Rows[0]["DiscountsHomeOwners"].ToString());
                home.TypeOfInsuredID = int.Parse(home._dtHomeOwners.Rows[0]["TypeOfInsuredID"].ToString());
                home.SubmittedDate = home._dtHomeOwners.Rows[0]["SubmittedDate"].ToString();

                return home;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not fill properties. ", ex);
            }


        }

        public static HomeOwners GetHomeOwners(int TaskControlID, bool isQuote)
        {
            HomeOwners home = null;

            DataTable dt = GetHomeOwnersByTaskControlIDDB(TaskControlID, isQuote);

            home = new HomeOwners(isQuote);

            if (isQuote)
                home = (HomeOwners)Policy.GetPolicyQuoteByTaskControlID(TaskControlID, home);  //PolicyQuote
            else                                                                                   //else
                home = (HomeOwners)Policy.GetPolicyByTaskControlID(TaskControlID, home);  //Policy

            home._dtHomeOwners = dt;

            home = FillProperties(home, TaskControlID, isQuote);

            return home;
        }

        public static DataTable GetHomeOwnersByTaskControlIDDB(int TaskControlID, bool isQuote)
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

            DataTable dt = exec.GetQuery("GetHomeOwnersByTaskControlID", xmlDoc);
            return dt;
        }

        private XmlDocument getInsertHomeXML()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[95];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
               SqlDbType.Int, 0, this.TaskControlID.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
               SqlDbType.Bit, 0, this.isQuote.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Bank",
                SqlDbType.VarChar, 50, this.bank.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankPPSID",
             SqlDbType.VarChar, 10, this.BankPPSID.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Bank2",
                SqlDbType.VarChar, 50, this.bank2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankPPSID2",
             SqlDbType.VarChar, 10, this.BankPPSID2.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LoanNo",
               SqlDbType.VarChar, 50, this.loanNo.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LoanNo2",
                SqlDbType.VarChar, 50, this.loanNo2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TypeOfInterest",
               SqlDbType.VarChar, 50, this.typeOfInterest.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MortgageeBilled",
               SqlDbType.Bit, 0, this.mortgageeBilled.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CatastropheCov",
               SqlDbType.VarChar, 50, this.catastropheCov.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CatastropheDeduc",
               SqlDbType.VarChar, 50, this.catastropheDeduc.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDeduc",
               SqlDbType.VarChar, 50, this.windstormDeduc.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AllOtherPerDeduc",
               SqlDbType.VarChar, 50, this.allOtherPerDeduc.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ConstructionType",
               SqlDbType.VarChar, 50, this.constructionType.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ConstructionYear",
               SqlDbType.VarChar, 50, this.constructionYear.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NumOfStories",
               SqlDbType.VarChar, 50, this.numOfStories.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NumOfFamilies",
               SqlDbType.VarChar, 50, this.numOfFamilies.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Ifyes",
               SqlDbType.VarChar, 50, this.ifYes.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LivingArea",
               SqlDbType.VarChar, 50, this.livingArea.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Porsches_Deck",
               SqlDbType.VarChar, 50, this.porshcesDeck.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RoofDwelling",
             SqlDbType.VarChar, 50, this.roofDwelling.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDeduc",
               SqlDbType.VarChar, 50, this.earthquakeDeduc.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Residence",
              SqlDbType.VarChar, 50, this.residence.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertyType",
             SqlDbType.VarChar, 50, this.propertyType.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertyForm",
               SqlDbType.VarChar, 50, this.propertyForm.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Losses3Years",
               SqlDbType.Bit, 0, this.losses3Year.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherStructuresType",
               SqlDbType.VarChar, 50, this.otherStructuresType.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsPropShuttered",
               SqlDbType.Bit, 0, this.isPropShuttered.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RoofOverhang",
             SqlDbType.VarChar, 50, this.roofOverhang.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoPolicy",
               SqlDbType.Bit, 0, this.autoPolicy.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoPolicyNo",
              SqlDbType.VarChar, 50, this.autoPolicyNo.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limit1",
              SqlDbType.Float, 0, this.limit1.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AOPDed1",
               SqlDbType.Float, 0, this.aopDed1.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDed1",
                SqlDbType.Float, 0, this.windstormDed1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDedPer1",
                SqlDbType.VarChar, 50, this.windstormDedPer1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDed1",
               SqlDbType.Float, 0, this.earthquakeDed1.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDedper1",
                SqlDbType.VarChar, 50, this.earthquakeDedper1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Coinsurance1",
               SqlDbType.VarChar, 50, this.coinsurance1.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium1",
               SqlDbType.Float, 0, this.premium1.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limit2",
               SqlDbType.Float, 0, this.limit2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AOPDed2",
               SqlDbType.Float, 0, this.aopDed2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDed2",
               SqlDbType.Float, 0, this.windstormDed2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDedPer2",
               SqlDbType.VarChar, 50, this.windstormDedPer2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDed2",
               SqlDbType.Float, 0, this.earthquakeDed2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDedper2",
               SqlDbType.VarChar, 50, this.earthquakeDedper2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Coinsurance2",
               SqlDbType.VarChar, 50, this.coinsurance2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium2",
               SqlDbType.Float, 0, this.premium2.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limit3",
               SqlDbType.Float, 0, this.limit3.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AOPDed3",
               SqlDbType.Float, 0, this.aopDed3.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDed3",
               SqlDbType.Float, 0, this.windstormDed3.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDedPer3",
             SqlDbType.VarChar, 50, this.windstormDedPer3.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDed3",
               SqlDbType.Float, 0, this.earthquakeDed3.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDedper3",
              SqlDbType.VarChar, 50, this.earthquakeDedper3.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Coinsurance3",
             SqlDbType.VarChar, 50, this.coinsurance3.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium3",
               SqlDbType.Float, 0, this.premium3.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limit4",
               SqlDbType.Float, 0, this.limit4.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AOPDed4",
               SqlDbType.Float, 50, this.aopDed4.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDed4",
               SqlDbType.Float, 0, this.windstormDed4.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WindstormDedPer4",
             SqlDbType.VarChar, 50, this.windstormDedPer4.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDed4",
               SqlDbType.Float, 0, this.earthquakeDed4.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EarthquakeDedper4",
              SqlDbType.VarChar, 50, this.earthquakeDedper4.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Coinsurance4",
               SqlDbType.VarChar, 50, this.coinsurance4.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium4",
               SqlDbType.Float, 0, this.premium4.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalLimit",
                SqlDbType.Float, 0, this.totalLimit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalWindstorm",
                SqlDbType.Float, 0, this.totalWindstorm.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalEarthquake",
               SqlDbType.Float, 0, this.totalEarthquake.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
                SqlDbType.Float, 0, this.totalPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiaPropertyType",
               SqlDbType.VarChar, 50, this.liaPropertyType.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiaPolicyType",
               SqlDbType.VarChar, 50, this.liaPolicyType.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiaNumOfFamilies",
               SqlDbType.VarChar, 50, this.liaNumOfFamilies.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiaLimit",
               SqlDbType.Float, 0, this.liaLimit.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiaMedicalPayments",
               SqlDbType.Float, 0, this.liaMedicalPayments.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiaPremium",
               SqlDbType.Float, 0, this.liaPremium.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium",
               SqlDbType.Float, 0, this.premium.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiaTotalPremium",
               SqlDbType.Float, 0, this.liaTotalPremium.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalNo",
              SqlDbType.VarChar, 50, this.renewalNo.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isUpgraded",
              SqlDbType.Bit, 0, this.isUpgraded.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Comments",
             SqlDbType.VarChar, 50, this.comments.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Occupation",
              SqlDbType.VarChar, 50, this.occupation.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AdditionalStructure",
              SqlDbType.Bit, 0, this.additionalStructure.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Office",
             SqlDbType.VarChar, 100, this.office.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Approved",
                 SqlDbType.Bit, 0, this.approved.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Comment",
             SqlDbType.VarChar, 500, this.comment.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Submitted",
             SqlDbType.Bit, 0, this.submitted.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rejected",
             SqlDbType.Bit, 0, this.rejected.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Island",
             SqlDbType.VarChar, 50, this.Island.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("GrossTax",
             SqlDbType.Float, 0, this.GrossTax.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Inspector",
             SqlDbType.VarChar, 50, this.Inspector.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InspectionDate",
             SqlDbType.VarChar, 25, this.InspectionDate.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PreviousPolicy",
                SqlDbType.VarChar, 100, this.PreviousPolicy.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isRenew",
                SqlDbType.Bit, 0, this.isRenew.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DiscountsHomeOwners",
            SqlDbType.Float, 0, this.DiscountsHomeOwners.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TypeOfInsuredID",
            SqlDbType.Int, 0, this.TypeOfInsuredID.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SubmittedDate",
            SqlDbType.VarChar, 25, this.SubmittedDate.ToString(),
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

    }
}
