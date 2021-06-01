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
    public class Properties 
    {
        public Properties()
		{

        }

        #region Variable

        private Properties oldProperties = null;
        private DataTable _dtProperties;
        private DataTable _OPPPropertiesBankCollection;
        private DataTable _OPPAdditionalCoverageDetailCollection;
        private AditionalCoveragecs _AditionalCoveragecs = null;
        private int _PropertiesID = 0;
        private int _PropertiesID2 = 0;
        private int _PropertiesID3 = 0;
        private int _PropertiesID4 = 0;
        private int _PropertiesID5 = 0;
        private int _PropertiesID6 = 0;
        private int _PropertiesID7 = 0;
        private int _PropertiesID8 = 0;
        private int _PropertiesID9 = 0;
        private int _PropertiesID10 = 0;

        private int _PropertiesID_OLD = 0;
        private int _PropertiesID2_OLD = 0;
        private int _PropertiesID3_OLD = 0;
        private int _PropertiesID4_OLD = 0;
        private int _PropertiesID5_OLD = 0;
        private int _PropertiesID6_OLD = 0;
        private int _PropertiesID7_OLD = 0;
        private int _PropertiesID8_OLD = 0;
        private int _PropertiesID9_OLD = 0;
        private int _PropertiesID10_OLD = 0;

        private int _TaskControlID = 0;
        private bool _Building = false;
        private bool _Contents = false; 
        private bool _Another = false;
        private int _Deductible = 2;
        private int _ConstructionType = 0;
        private string _Address1 = "";
        private string _Address2 = "";
        private string _City = "";
        private string _State = "PR";
        private string _ZipCode = "";
        private string _Description = "";
        private int _Families = 1;
        private bool _Primary = true;
        private bool _Secondary = false;
        private bool _Rented = false;
        private bool _Pool = false;
        private string _Bank = "000";
        private string _LoanNo = "";

        private double _HomeAssistance = 0;
        
        private double _BuildingLimitFire = 0;
        private double _BuildingLimitExtCoverage = 0;
        private double _BuildingLimitVandalism = 0;
        private double _BuildingLimitEarthquake = 0;
        private double _BuildingLimitAOP = 0;
        private double _BuildingLimitExcessAOP = 0;
        private double _BuildingLimitTheft = 0;

        private double _BuildingRateFire = 0.00;
        private double _BuildingRateExtCoverage = 0.00;
        private double _BuildingRateVandalism = 0.00;
        private double _BuildingRateEarthquake = 0.00;
        private double _BuildingRateAOP = 0.00;
        private double _BuildingRateExcessAOP = 0.00;
        private double _BuildingRateTheft = 0.00;

        private double _BuildingFactorFire = 0.00;
        private double _BuildingFactorExtCoverage = 0.00;
        private double _BuildingFactorVandalism = 0.00;
        private double _BuildingFactorEarthquake = 0.00;
        private double _BuildingFactorAOP = 0.00;
        private double _BuildingFactorExcessAOP = 0.00;
        private double _BuildingFactorTheft = 0.00;

        private double _BuildingTotalFire = 0.00;
        private double _BuildingTotalExtCoverage = 0.00;
        private double _BuildingTotalVandalism = 0.00;
        private double _BuildingTotalEarthquake = 0.00;
        private double _BuildingTotalAOP = 0.00;
        private double _BuildingTotalExcessAOP = 0.00;
        private double _BuildingTotalTheft = 0.00;

        private double _BuildingPremiumFire = 0.00;
        private double _BuildingPremiumExtCoverage = 0.00;
        private double _BuildingPremiumVandalism = 0.00;
        private double _BuildingPremiumEarthquake = 0.00;
        private double _BuildingPremiumAOP = 0.00;
        private double _BuildingPremiumExcessAOP = 0.00;
        private double _BuildingPremiumTheft = 0.00;

        private double _ContentsLimitFire = 0;
        private double _ContentsLimitExtCoverage = 0;
        private double _ContentsLimitVandalism = 0;
        private double _ContentsLimitEarthquake = 0;
        private double _ContentsLimitAOP = 0;
        private double _ContentsLimitExcessAOP = 0;
        private double _ContentsLimitTheft = 0;

        private double _ContentsRateFire = 0.00;
        private double _ContentsRateExtCoverage = 0.00;
        private double _ContentsRateVandalism = 0.00;
        private double _ContentsRateEarthquake = 0.00;
        private double _ContentsRateAOP = 0.00;
        private double _ContentsRateExcessAOP = 0.00;
        private double _ContentsRateTheft = 0.00;

        private double _ContentsFactorFire = 0.00;
        private double _ContentsFactorExtCoverage = 0.00;
        private double _ContentsFactorVandalism = 0.00;
        private double _ContentsFactorEarthquake = 0.00;
        private double _ContentsFactorAOP = 0.00;
        private double _ContentsFactorExcessAOP = 0.00;
        private double _ContentsFactorTheft = 0.00;

        private double _ContentsTotalFire = 0.00;
        private double _ContentsTotalExtCoverage = 0.00;
        private double _ContentsTotalVandalism = 0.00;
        private double _ContentsTotalEarthquake = 0.00;
        private double _ContentsTotalAOP = 0.00;
        private double _ContentsTotalExcessAOP = 0.00;
        private double _ContentsTotalTheft = 0.00;

        private double _ContentsPremiumFire = 0.00;
        private double _ContentsPremiumExtCoverage = 0.00;
        private double _ContentsPremiumVandalism = 0.00;
        private double _ContentsPremiumEarthquake = 0.00;
        private double _ContentsPremiumAOP = 0.00;
        private double _ContentsPremiumExcessAOP = 0.00;
        private double _ContentsPremiumTheft = 0.00;

        private double _BuildingPremiumTotal = 0.00;
        private double _ContentsPremiumTotal = 0.00;
        private int _ExperienceCredit = 0;
        private double _SubTotalPremium = 0.00;
        private double _Charge = 0.00;
        private double _TotalPremium = 0.00;
        private int _mode = (int)PropertiesMode.CLEAR;
        private int _DiscAccess  = 0;
        private int _DiscShutter  = 0;
        private int _DiscTheft  = 0;
        private int _DiscFire  = 0;
        private double _DiscUnderwritter = 0;
        private string _ConstructionYear = "";
        private string _NumOfStories = "";
        private bool _Residence = false;
        private bool _Condo = false;
        private bool _IsLiability = false;
        private int _GrdPropertiesID = 0;
        private string _BankID = "";

        #endregion

        #region Public Enumeration

        public enum PropertiesMode { ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4 };

        #endregion

        #region Properties

        public AditionalCoveragecs AditionalCoveragecs
        {
            get
            {
                if (this._AditionalCoveragecs == null)
                    this._AditionalCoveragecs = new AditionalCoveragecs();
                return (this._AditionalCoveragecs);
            }
            set
            {
                this._AditionalCoveragecs = value;
            }
        }

        public int PropertiesID
        {
            get
            {
                return this._PropertiesID;
            }
            set
            {
                this._PropertiesID = value;
            }
        }

        public int PropertiesID2
        {
            get
            {
                return this._PropertiesID2;
            }
            set
            {
                this._PropertiesID2 = value;
            }
        }

        public int PropertiesID3
        {
            get
            {
                return this._PropertiesID3;
            }
            set
            {
                this._PropertiesID3 = value;
            }
        }

        public int PropertiesID4
        {
            get
            {
                return this._PropertiesID4;
            }
            set
            {
                this._PropertiesID4 = value;
            }
        }

        public int PropertiesID5
        {
            get
            {
                return this._PropertiesID5;
            }
            set
            {
                this._PropertiesID5 = value;
            }
        }

        public int PropertiesID6
        {
            get
            {
                return this._PropertiesID6;
            }
            set
            {
                this._PropertiesID6 = value;
            }
        }

        public int PropertiesID7
        {
            get
            {
                return this._PropertiesID7;
            }
            set
            {
                this._PropertiesID7 = value;
            }
        }

        public int PropertiesID8
        {
            get
            {
                return this._PropertiesID8;
            }
            set
            {
                this._PropertiesID8 = value;
            }
        }

        public int PropertiesID9
        {
            get
            {
                return this._PropertiesID9;
            }
            set
            {
                this._PropertiesID9 = value;
            }
        }

        public int PropertiesID10
        {
            get
            {
                return this._PropertiesID10;
            }
            set
            {
                this._PropertiesID10 = value;
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

        public bool Building
        {
            get
            {
                return this._Building;
            }
            set
            {
                this._Building = value;
            }
        }

        public bool Contents
        {
            get
            {
                return this._Contents;
            }
            set
            {
                this._Contents = value;
            }
        }

        public bool Another
        {
            get
            {
                return this._Another;
            }
            set
            {
                this._Another = value;
            }
        }

        public int Deductible
        {
            get
            {
                return this._Deductible;
            }
            set
            {
                this._Deductible = value;
            }
        }
        public int ConstructionType
        {
            get
            {
                return this._ConstructionType;
            }
            set
            {
                this._ConstructionType = value;
            }
        }

        public string Bank
        {
            get
            {
                return this._Bank;
            }
            set
            {
                this._Bank = value;
            }
        }
        public string LoanNo
        {
            get
            {
                return this._LoanNo;
            }
            set
            {
                this._LoanNo = value;
            }
        }

        public string Address1
        {
            get
            {
                return this._Address1;
            }
            set
            {
                this._Address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return this._Address2;
            }
            set
            {
                this._Address2 = value;
            }
        }
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
            }
        }
        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }
        public string ZipCode
        {
            get
            {
                return this._ZipCode;
            }
            set
            {
                this._ZipCode = value;
            }
        }
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        public int Families
        {
            get
            {
                return this._Families;
            }
            set
            {
                this._Families = value;
            }
        }
        public bool Primary
        {
            get
            {
                return this._Primary;
            }
            set
            {
                this._Primary = value;
            }
        }
        public bool Secondary
        {
            get
            {
                return this._Secondary;
            }
            set
            {
                this._Secondary = value;
            }
        }
        public bool Rented
        {
            get
            {
                return this._Rented;
            }
            set
            {
                this._Rented = value;
            }
        }
        public bool Pool
        {
            get
            {
                return this._Pool;
            }
            set
            {
                this._Pool = value;
            }
        }

        public double HomeAssistance
        {
            get
            {
                return this._HomeAssistance;
            }
            set
            {
                this._HomeAssistance = value;
            }
        }

        public double BuildingPremiumFire
        {
            get
            {
                return this._BuildingPremiumFire;
            }
            set
            {
                this._BuildingPremiumFire = value;
            }
        }
        public double BuildingPremiumExtCoverage
        {
            get
            {
                return this._BuildingPremiumExtCoverage;
            }
            set
            {
                this._BuildingPremiumExtCoverage = value;
            }
        }
        public double BuildingPremiumVandalism
        {
            get
            {
                return this._BuildingPremiumVandalism;
            }
            set
            {
                this._BuildingPremiumVandalism = value;
            }
        }
        public double BuildingPremiumEarthquake
        {
            get
            {
                return this._BuildingPremiumEarthquake;
            }
            set
            {
                this._BuildingPremiumEarthquake = value;
            }
        }
        public double BuildingPremiumAOP
        {
            get
            {
                return this._BuildingPremiumAOP;
            }
            set
            {
                this._BuildingPremiumAOP = value;
            }
        }
        public double BuildingPremiumExcessAOP
        {
            get
            {
                return this._BuildingPremiumExcessAOP;
            }
            set
            {
                this._BuildingPremiumExcessAOP = value;
            }
        }
        public double BuildingPremiumTheft
        {
            get
            {
                return this._BuildingPremiumTheft;
            }
            set
            {
                this._BuildingPremiumTheft = value;
            }
        }
        public double ContentsPremiumFire
        {
            get
            {
                return this._ContentsPremiumFire;
            }
            set
            {
                this._ContentsPremiumFire = value;
            }
        }
        public double ContentsPremiumExtCoverage
        {
            get
            {
                return this._ContentsPremiumExtCoverage;
            }
            set
            {
                this._ContentsPremiumExtCoverage = value;
            }
        }
        public double ContentsPremiumVandalism
        {
            get
            {
                return this._ContentsPremiumVandalism;
            }
            set
            {
                this._ContentsPremiumVandalism = value;
            }
        }
        public double ContentsPremiumEarthquake
        {
            get
            {
                return this._ContentsPremiumEarthquake;
            }
            set
            {
                this._ContentsPremiumEarthquake = value;
            }
        }
        public double ContentsPremiumAOP
        {
            get
            {
                return this._ContentsPremiumAOP;
            }
            set
            {
                this._ContentsPremiumAOP = value;
            }
        }
        public double ContentsPremiumExcessAOP
        {
            get
            {
                return this._ContentsPremiumExcessAOP;
            }
            set
            {
                this._ContentsPremiumExcessAOP = value;
            }
        }
        public double ContentsPremiumTheft
        {
            get
            {
                return this._ContentsPremiumTheft;
            }
            set
            {
                this._ContentsPremiumTheft = value;
            }
        }

        public double SubTotalPremium
        {
            get
            {
                return this._SubTotalPremium;
            }
            set
            {
                this._SubTotalPremium = value;
            }
        }
        public double Charge
        {
            get
            {
                return this._Charge;
            }
            set
            {
                this._Charge = value;
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

        public double BuildingLimitFire
        {
            get
            {
                return this._BuildingLimitFire;
            }
            set
            {
                this._BuildingLimitFire = value;
            }
        }
        public double BuildingLimitExtCoverage
        {
            get
            {
                return this._BuildingLimitExtCoverage;
            }
            set
            {
                this._BuildingLimitExtCoverage = value;
            }
        }
        public double BuildingLimitVandalism
        {
            get
            {
                return this._BuildingLimitVandalism;
            }
            set
            {
                this._BuildingLimitVandalism = value;
            }
        }
        public double BuildingLimitEarthquake
        {
            get
            {
                return this._BuildingLimitEarthquake;
            }
            set
            {
                this._BuildingLimitEarthquake = value;
            }
        }
        public double BuildingLimitAOP
        {
            get
            {
                return this._BuildingLimitAOP;
            }
            set
            {
                this._BuildingLimitAOP = value;
            }
        }
        public double BuildingLimitExcessAOP
        {
            get
            {
                return this._BuildingLimitExcessAOP;
            }
            set
            {
                this._BuildingLimitExcessAOP = value;
            }
        }
        public double BuildingLimitTheft
        {
            get
            {
                return this._BuildingLimitTheft;
            }
            set
            {
                this._BuildingLimitTheft = value;
            }
        }

        public double BuildingRateFire
        {
            get
            {
                return this._BuildingRateFire;
            }
            set
            {
                this._BuildingRateFire = value;
            }
        }
        public double BuildingRateExtCoverage
        {
            get
            {
                return this._BuildingRateExtCoverage;
            }
            set
            {
                this._BuildingRateExtCoverage = value;
            }
        }
        public double BuildingRateVandalism
        {
            get
            {
                return this._BuildingRateVandalism;
            }
            set
            {
                this._BuildingRateVandalism = value;
            }
        }
        public double BuildingRateEarthquake
        {
            get
            {
                return this._BuildingRateEarthquake;
            }
            set
            {
                this._BuildingRateEarthquake = value;
            }
        }
        public double BuildingRateAOP
        {
            get
            {
                return this._BuildingRateAOP;
            }
            set
            {
                this._BuildingRateAOP = value;
            }
        }
        public double BuildingRateExcessAOP
        {
            get
            {
                return this._BuildingRateExcessAOP;
            }
            set
            {
                this._BuildingRateExcessAOP = value;
            }
        }
        public double BuildingRateTheft
        {
            get
            {
                return this._BuildingRateTheft;
            }
            set
            {
                this._BuildingRateTheft = value;
            }
        }

        public double BuildingFactorFire
        {
            get
            {
                return this._BuildingFactorFire;
            }
            set
            {
                this._BuildingFactorFire = value;
            }
        }
        public double BuildingFactorExtCoverage
        {
            get
            {
                return this._BuildingFactorExtCoverage;
            }
            set
            {
                this._BuildingFactorExtCoverage = value;
            }
        }
        public double BuildingFactorVandalism
        {
            get
            {
                return this._BuildingFactorVandalism;
            }
            set
            {
                this._BuildingFactorVandalism = value;
            }
        }
        public double BuildingFactorEarthquake
        {
            get
            {
                return this._BuildingFactorEarthquake;
            }
            set
            {
                this._BuildingFactorEarthquake = value;
            }
        }
        public double BuildingFactorAOP
        {
            get
            {
                return this._BuildingFactorAOP;
            }
            set
            {
                this._BuildingFactorAOP = value;
            }
        }
        public double BuildingFactorExcessAOP
        {
            get
            {
                return this._BuildingFactorExcessAOP;
            }
            set
            {
                this._BuildingFactorExcessAOP = value;
            }
        }
        public double BuildingFactorTheft
        {
            get
            {
                return this._BuildingFactorTheft;
            }
            set
            {
                this._BuildingFactorTheft = value;
            }
        }

        public double BuildingTotalFire
        {
            get
            {
                return this._BuildingTotalFire;
            }
            set
            {
                this._BuildingTotalFire = value;
            }
        }
        public double BuildingTotalExtCoverage
        {
            get
            {
                return this._BuildingTotalExtCoverage;
            }
            set
            {
                this._BuildingTotalExtCoverage = value;
            }
        }
        public double BuildingTotalVandalism
        {
            get
            {
                return this._BuildingTotalVandalism;
            }
            set
            {
                this._BuildingTotalVandalism = value;
            }
        }
        public double BuildingTotalEarthquake
        {
            get
            {
                return this._BuildingTotalEarthquake;
            }
            set
            {
                this._BuildingTotalEarthquake = value;
            }
        }
        public double BuildingTotalAOP
        {
            get
            {
                return this._BuildingTotalAOP;
            }
            set
            {
                this._BuildingTotalAOP = value;
            }
        }
        public double BuildingTotalExcessAOP
        {
            get
            {
                return this._BuildingTotalExcessAOP;
            }
            set
            {
                this._BuildingTotalExcessAOP = value;
            }
        }
        public double BuildingTotalTheft
        {
            get
            {
                return this._BuildingTotalTheft;
            }
            set
            {
                this._BuildingTotalTheft = value;
            }
        }

        public double ContentsLimitFire
        {
            get
            {
                return this._ContentsLimitFire;
            }
            set
            {
                this._ContentsLimitFire = value;
            }
        }
        public double ContentsLimitExtCoverage
        {
            get
            {
                return this._ContentsLimitExtCoverage;
            }
            set
            {
                this._ContentsLimitExtCoverage = value;
            }
        }
        public double ContentsLimitVandalism
        {
            get
            {
                return this._ContentsLimitVandalism;
            }
            set
            {
                this._ContentsLimitVandalism = value;
            }
        }
        public double ContentsLimitEarthquake
        {
            get
            {
                return this._ContentsLimitEarthquake;
            }
            set
            {
                this._ContentsLimitEarthquake = value;
            }
        }
        public double ContentsLimitAOP
        {
            get
            {
                return this._ContentsLimitAOP;
            }
            set
            {
                this._ContentsLimitAOP = value;
            }
        }
        public double ContentsLimitExcessAOP
        {
            get
            {
                return this._ContentsLimitExcessAOP;
            }
            set
            {
                this._ContentsLimitExcessAOP = value;
            }
        }
        public double ContentsLimitTheft
        {
            get
            {
                return this._ContentsLimitTheft;
            }
            set
            {
                this._ContentsLimitTheft = value;
            }
        }

        public double ContentsRateFire
        {
            get
            {
                return this._ContentsRateFire;
            }
            set
            {
                this._ContentsRateFire = value;
            }
        }
        public double ContentsRateExtCoverage
        {
            get
            {
                return this._ContentsRateExtCoverage;
            }
            set
            {
                this._ContentsRateExtCoverage = value;
            }
        }
        public double ContentsRateVandalism
        {
            get
            {
                return this._ContentsRateVandalism;
            }
            set
            {
                this._ContentsRateVandalism = value;
            }
        }
        public double ContentsRateEarthquake
        {
            get
            {
                return this._ContentsRateEarthquake;
            }
            set
            {
                this._ContentsRateEarthquake = value;
            }
        }
        public double ContentsRateAOP
        {
            get
            {
                return this._ContentsRateAOP;
            }
            set
            {
                this._ContentsRateAOP = value;
            }
        }
        public double ContentsRateExcessAOP
        {
            get
            {
                return this._ContentsRateExcessAOP;
            }
            set
            {
                this._ContentsRateExcessAOP = value;
            }
        }
        public double ContentsRateTheft
        {
            get
            {
                return this._ContentsRateTheft;
            }
            set
            {
                this._ContentsRateTheft = value;
            }
        }

        public double ContentsFactorFire
        {
            get
            {
                return this._ContentsFactorFire;
            }
            set
            {
                this._ContentsFactorFire = value;
            }
        }
        public double ContentsFactorExtCoverage
        {
            get
            {
                return this._ContentsFactorExtCoverage;
            }
            set
            {
                this._ContentsFactorExtCoverage = value;
            }
        }
        public double ContentsFactorVandalism
        {
            get
            {
                return this._ContentsFactorVandalism;
            }
            set
            {
                this._ContentsFactorVandalism = value;
            }
        }
        public double ContentsFactorEarthquake
        {
            get
            {
                return this._ContentsFactorEarthquake;
            }
            set
            {
                this._ContentsFactorEarthquake = value;
            }
        }
        public double ContentsFactorAOP
        {
            get
            {
                return this._ContentsFactorAOP;
            }
            set
            {
                this._ContentsFactorAOP = value;
            }
        }
        public double ContentsFactorExcessAOP
        {
            get
            {
                return this._ContentsFactorExcessAOP;
            }
            set
            {
                this._ContentsFactorExcessAOP = value;
            }
        }
        public double ContentsFactorTheft
        {
            get
            {
                return this._ContentsFactorTheft;
            }
            set
            {
                this._ContentsFactorTheft = value;
            }
        }

        public double ContentsTotalFire
        {
            get
            {
                return this._ContentsTotalFire;
            }
            set
            {
                this._ContentsTotalFire = value;
            }
        }
        public double ContentsTotalExtCoverage
        {
            get
            {
                return this._ContentsTotalExtCoverage;
            }
            set
            {
                this._ContentsTotalExtCoverage = value;
            }
        }
        public double ContentsTotalVandalism
        {
            get
            {
                return this._ContentsTotalVandalism;
            }
            set
            {
                this._ContentsTotalVandalism = value;
            }
        }
        public double ContentsTotalEarthquake
        {
            get
            {
                return this._ContentsTotalEarthquake;
            }
            set
            {
                this._ContentsTotalEarthquake = value;
            }
        }
        public double ContentsTotalAOP
        {
            get
            {
                return this._ContentsTotalAOP;
            }
            set
            {
                this._ContentsTotalAOP = value;
            }
        }
        public double ContentsTotalExcessAOP
        {
            get
            {
                return this._ContentsTotalExcessAOP;
            }
            set
            {
                this._ContentsTotalExcessAOP = value;
            }
        }
        public double ContentsTotalTheft
        {
            get
            {
                return this._ContentsTotalTheft;
            }
            set
            {
                this._ContentsTotalTheft = value;
            }
        }

        public int ExperienceCredit
        {
            get
            {
                return this._ExperienceCredit;
            }
            set
            {
                this._ExperienceCredit = value;
            }
        }

        public double BuildingPremiumTotal
        {
            get
            {
                return this._BuildingPremiumTotal;
            }
            set
            {
                this._BuildingPremiumTotal = value;
            }
        }
        public double ContentsPremiumTotal
        {
            get
            {
                return this._ContentsPremiumTotal;
            }
            set
            {
                this._ContentsPremiumTotal = value;
            }
        }

        public int Mode
        {
            get
            {
                return this._mode;
            }
            set
            {
                this._mode = value;
            }
        }

        public int DiscAccess
        {
            get
            {
                return this._DiscAccess;
            }
            set
            {
                this._DiscAccess = value;
            }
        }
        public int DiscShutter
        {
            get
            {
                return this._DiscShutter;
            }
            set
            {
                this._DiscShutter = value;
            }
        }
        public int DiscTheft
        {
            get
            {
                return this._DiscTheft;
            }
            set
            {
                this._DiscTheft = value;
            }
        }
        public int DiscFire
        {
            get
            {
                return this._DiscFire;
            }
            set
            {
                this._DiscFire = value;
            }
        }
        public double DiscUnderwritter
        {
            get
            {
                return this._DiscUnderwritter;
            }
            set
            {
                this._DiscUnderwritter = value;
            }
        }

        public string ConstructionYear
        {
            get
            {
                return this._ConstructionYear;
            }
            set
            {
                this._ConstructionYear = value;
            }
        }

        public string NumOfStories
        {
            get
            {
                return this._NumOfStories;
            }
            set
            {
                this._NumOfStories = value;
            }
        }

        public bool Residence
        {
            get { return this._Residence; }
            set { this._Residence = value; }
        }

        public bool Condo
        {
            get { return this._Condo; }
            set { this._Condo = value; }
        }

        public bool IsLiability
        {
            get { return this._IsLiability; }
            set { this._IsLiability = value; }
        }

        public int GrdPropertiesID
        {
            get { return this._GrdPropertiesID; }
            set {  this._GrdPropertiesID = value; }
        }

        public string BankID
        {
            get { return this._BankID; }
            set { this._BankID = value; }
        }

        public DataTable OPPPropertiesBankCollection
        {
            get
            {
                //if (this._OPPPropertiesBankCollection == null)
                    //this._OPPPropertiesBankCollection = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection);
            }
            set
            {
                this._OPPPropertiesBankCollection = value;
            }
        }

        public DataTable OPPAdditionalCoverageDetailCollection
        {
            get
            {
                //if (this._OPPPropertiesBankCollection == null)
                //this._OPPPropertiesBankCollection = DataTableOPPPropertiesBankTemp();
                return (this._OPPAdditionalCoverageDetailCollection);
            }
            set
            {
                this._OPPAdditionalCoverageDetailCollection = value;
            }
        }

        #endregion

        #region Public Methods

        public void SaveProperties(int UserID, DataTable PropertiesTableTemp, int taskControlID, bool IsOPPQuote)
        {
            //for (int i = 0; i < PropertiesTableTemp.Rows.Count; i++)
            //{
            //    //if (i == 0 && int.Parse(PropertiesTableTemp.Rows[i]["TaskcontrolID"].ToString()) == taskControlID)
            //    if (i == 0)
            //    _PropertiesID_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 1)
            //    _PropertiesID2_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 2)
            //    _PropertiesID3_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 3)
            //    _PropertiesID4_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 4)
            //    _PropertiesID5_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 5)
            //    _PropertiesID6_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 6)
            //    _PropertiesID7_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 7)
            //    _PropertiesID8_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 8)
            //    _PropertiesID9_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());

            //    if (i == 9)
            //    _PropertiesID10_OLD = int.Parse(PropertiesTableTemp.Rows[i]["PropertiesID"].ToString());
            //}


            DBRequest Executor = new DBRequest();
            Executor.Update("DeletePropertiesByTaskControlID", DeletePropertiesByTaskControlIDXml(taskControlID,IsOPPQuote));

            //DataTable dt = PropertiesTableTemp;
            for (int i = 0; i < PropertiesTableTemp.Rows.Count; i++)
            {
                this.TaskControlID = taskControlID;
                this.Families = int.Parse(PropertiesTableTemp.Rows[i]["Families"].ToString());
                this.ConstructionType = int.Parse(PropertiesTableTemp.Rows[i]["ConstructionType"].ToString());
                this.ExperienceCredit = int.Parse(PropertiesTableTemp.Rows[i]["ExperienceCredit"].ToString());
                this.City = PropertiesTableTemp.Rows[i]["City"].ToString();
                this.Building = bool.Parse(PropertiesTableTemp.Rows[i]["Building"].ToString());
                this.Contents = bool.Parse(PropertiesTableTemp.Rows[i]["Contents"].ToString());
                this.Another = bool.Parse(PropertiesTableTemp.Rows[i]["Another"].ToString());
                this.Rented = bool.Parse(PropertiesTableTemp.Rows[i]["Rented"].ToString());
                this.Pool = bool.Parse(PropertiesTableTemp.Rows[i]["Pool"].ToString());
                this.Deductible = int.Parse(PropertiesTableTemp.Rows[i]["Deductible"].ToString());
                this.Primary = bool.Parse(PropertiesTableTemp.Rows[i]["Primary"].ToString());
                this.Secondary = bool.Parse(PropertiesTableTemp.Rows[i]["Secondary"].ToString());
                this.Bank = PropertiesTableTemp.Rows[i]["Bank"].ToString().Trim();
                this.LoanNo = PropertiesTableTemp.Rows[i]["LoanNo"].ToString().Trim();
                this.Address1 = PropertiesTableTemp.Rows[i]["Address1"].ToString().Trim();
                this.Address2 = PropertiesTableTemp.Rows[i]["Address2"].ToString().Trim();
                this.State = PropertiesTableTemp.Rows[i]["State"].ToString().Trim();
                this.ZipCode = PropertiesTableTemp.Rows[i]["ZipCode"].ToString().Trim();
                this.Description = PropertiesTableTemp.Rows[i]["Description"].ToString().Trim();
                this.HomeAssistance = double.Parse(PropertiesTableTemp.Rows[i]["HomeAssistance"].ToString());
                this.BuildingLimitFire = double.Parse(PropertiesTableTemp.Rows[i]["BuildingLimitFire"].ToString());
                this.BuildingLimitExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["BuildingLimitExtCoverage"].ToString());
                this.BuildingLimitVandalism = double.Parse(PropertiesTableTemp.Rows[i]["BuildingLimitVandalism"].ToString());
                this.BuildingLimitEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["BuildingLimitEarthquake"].ToString());
                this.BuildingLimitAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingLimitAOP"].ToString());
                this.BuildingLimitExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingLimitExcessAOP"].ToString());
                this.BuildingLimitTheft = double.Parse(PropertiesTableTemp.Rows[i]["BuildingLimitTheft"].ToString());  
                this.BuildingRateFire = double.Parse(PropertiesTableTemp.Rows[i]["BuildingRateFire"].ToString());  
                this.BuildingFactorFire = double.Parse(PropertiesTableTemp.Rows[i]["BuildingFactorFire"].ToString());
                this.BuildingTotalFire = double.Parse(PropertiesTableTemp.Rows[i]["BuildingTotalFire"].ToString());
                this.BuildingPremiumFire = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumFire"].ToString());
                this.BuildingRateExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["BuildingRateExtCoverage"].ToString());
                this.BuildingFactorExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["BuildingFactorExtCoverage"].ToString());
                this.BuildingTotalExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["BuildingTotalExtCoverage"].ToString());
                this.BuildingPremiumExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumExtCoverage"].ToString());
                this.BuildingRateVandalism = double.Parse(PropertiesTableTemp.Rows[i]["BuildingRateVandalism"].ToString());
                this.BuildingFactorVandalism = double.Parse(PropertiesTableTemp.Rows[i]["BuildingFactorVandalism"].ToString());
                this.BuildingTotalVandalism = double.Parse(PropertiesTableTemp.Rows[i]["BuildingTotalVandalism"].ToString());
                this.BuildingPremiumVandalism = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumVandalism"].ToString());
                this.BuildingRateEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["BuildingRateEarthquake"].ToString());
                this.BuildingFactorEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["BuildingFactorEarthquake"].ToString());
                this.BuildingTotalEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["BuildingTotalEarthquake"].ToString());
                this.BuildingPremiumEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumEarthquake"].ToString());
                this.BuildingRateAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingRateAOP"].ToString());
                this.BuildingFactorAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingFactorAOP"].ToString());
                this.BuildingTotalAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingTotalAOP"].ToString());
                this.BuildingPremiumAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumAOP"].ToString());
                this.BuildingRateExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingRateExcessAOP"].ToString());
                this.BuildingFactorExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingFactorExcessAOP"].ToString());
                this.BuildingTotalExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingTotalExcessAOP"].ToString());
                this.BuildingPremiumExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumExcessAOP"].ToString());
                this.BuildingRateTheft = double.Parse(PropertiesTableTemp.Rows[i]["BuildingRateTheft"].ToString());
                this.BuildingFactorTheft = double.Parse(PropertiesTableTemp.Rows[i]["BuildingFactorTheft"].ToString());
                this.BuildingTotalTheft = double.Parse(PropertiesTableTemp.Rows[i]["BuildingTotalTheft"].ToString());
                this.BuildingPremiumTheft = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumTheft"].ToString());
                this.ContentsLimitFire = double.Parse(PropertiesTableTemp.Rows[i]["ContentsLimitFire"].ToString());
                this.ContentsLimitExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["ContentsLimitExtCoverage"].ToString());
                this.ContentsLimitVandalism = double.Parse(PropertiesTableTemp.Rows[i]["ContentsLimitVandalism"].ToString());
                this.ContentsLimitEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["ContentsLimitEarthquake"].ToString());
                this.ContentsLimitAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsLimitAOP"].ToString());
                this.ContentsLimitExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsLimitExcessAOP"].ToString());
                this.ContentsLimitTheft = double.Parse(PropertiesTableTemp.Rows[i]["ContentsLimitTheft"].ToString());  
                this.ContentsRateFire = double.Parse(PropertiesTableTemp.Rows[i]["ContentsRateFire"].ToString());
                this.ContentsFactorFire = double.Parse(PropertiesTableTemp.Rows[i]["ContentsFactorFire"].ToString());
                this.ContentsTotalFire = double.Parse(PropertiesTableTemp.Rows[i]["ContentsTotalFire"].ToString());
                this.ContentsPremiumFire = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumFire"].ToString());
                this.ContentsRateExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["ContentsRateExtCoverage"].ToString());
                this.ContentsFactorExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["ContentsFactorExtCoverage"].ToString());
                this.ContentsTotalExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["ContentsTotalExtCoverage"].ToString());
                this.ContentsPremiumExtCoverage = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumExtCoverage"].ToString());
                this.ContentsRateVandalism = double.Parse(PropertiesTableTemp.Rows[i]["ContentsRateVandalism"].ToString());
                this.ContentsFactorVandalism = double.Parse(PropertiesTableTemp.Rows[i]["ContentsFactorVandalism"].ToString());
                this.ContentsTotalVandalism = double.Parse(PropertiesTableTemp.Rows[i]["ContentsTotalVandalism"].ToString());
                this.ContentsPremiumVandalism = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumVandalism"].ToString());
                this.ContentsRateEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["ContentsRateEarthquake"].ToString());
                this.ContentsFactorEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["ContentsFactorEarthquake"].ToString());
                this.ContentsTotalEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["ContentsTotalEarthquake"].ToString());
                this.ContentsPremiumEarthquake = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumEarthquake"].ToString());
                this.ContentsRateAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsRateAOP"].ToString());
                this.ContentsFactorAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsFactorAOP"].ToString());
                this.ContentsTotalAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsTotalAOP"].ToString());
                this.ContentsPremiumAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumAOP"].ToString());
                this.ContentsRateExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsRateExcessAOP"].ToString());
                this.ContentsFactorExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsFactorExcessAOP"].ToString());
                this.ContentsTotalExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsTotalExcessAOP"].ToString());
                this.ContentsPremiumExcessAOP = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumExcessAOP"].ToString());
                this.ContentsRateTheft = double.Parse(PropertiesTableTemp.Rows[i]["ContentsRateTheft"].ToString());
                this.ContentsFactorTheft = double.Parse(PropertiesTableTemp.Rows[i]["ContentsFactorTheft"].ToString());
                this.ContentsTotalTheft = double.Parse(PropertiesTableTemp.Rows[i]["ContentsTotalTheft"].ToString());
                this.ContentsPremiumTheft = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumTheft"].ToString());
                this.BuildingPremiumTotal = double.Parse(PropertiesTableTemp.Rows[i]["BuildingPremiumTotal"].ToString());
                this.ContentsPremiumTotal = double.Parse(PropertiesTableTemp.Rows[i]["ContentsPremiumTotal"].ToString());
                this.Charge = double.Parse(PropertiesTableTemp.Rows[i]["Charge"].ToString());
                this.SubTotalPremium = double.Parse(PropertiesTableTemp.Rows[i]["SubTotalPremium"].ToString());
                this.TotalPremium = double.Parse(PropertiesTableTemp.Rows[i]["TotalPremium"].ToString());
                this.DiscAccess = int.Parse(PropertiesTableTemp.Rows[i]["DiscAccess"].ToString());
                this.DiscShutter = int.Parse(PropertiesTableTemp.Rows[i]["DiscShutter"].ToString());
                this.DiscTheft = int.Parse(PropertiesTableTemp.Rows[i]["DiscTheft"].ToString());
                this.DiscFire = int.Parse(PropertiesTableTemp.Rows[i]["DiscFire"].ToString());
                this.DiscUnderwritter = double.Parse(PropertiesTableTemp.Rows[i]["DiscUnderwritter"].ToString());
                this.ConstructionYear = PropertiesTableTemp.Rows[i]["ConstructionYear"].ToString().Trim();
                this.NumOfStories = PropertiesTableTemp.Rows[i]["NumOfStories"].ToString().Trim();
                this.IsLiability = bool.Parse(PropertiesTableTemp.Rows[i]["IsLiability"].ToString());
                this.Residence = (PropertiesTableTemp.Rows[i]["Residence"] != System.DBNull.Value) ? bool.Parse(PropertiesTableTemp.Rows[i]["Residence"].ToString()):false;
                this.Condo = (PropertiesTableTemp.Rows[i]["Condo"] != System.DBNull.Value) ? bool.Parse(PropertiesTableTemp.Rows[i]["Condo"].ToString()) : false;
                
                    
                this.Mode = 1; //Add
                this.SaveProperty(UserID,IsOPPQuote, i+1);
            }

            this.Mode = (int)PropertiesMode.CLEAR;
        }

        public static DataTable GetPropertiesByTaskControlID(int TaskControlID, bool IsOPPQuote)
        {
            DataTable dt = GetPropertiesByTaskControlIDDB(TaskControlID, IsOPPQuote);
            return dt;
        }

        public static DataTable GetPropertiesByPropertiesID(int propertiesID, bool IsOPPQuote)
        {
            DataTable dt = GetPropertiesByPropertiesIDDB(propertiesID, IsOPPQuote);
            return dt;
        }

        #endregion

        #region Private Methods

        private XmlDocument DeletePropertiesByTaskControlIDXml(int taskControlID, bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
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

        public static DataTable GetPropertiesByTaskControlIDDB(int taskControlID, bool IsOPPQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
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

            DataTable dt = exec.GetQuery("GetPropertiesByTaskControlID", xmlDoc);

            return dt;
        }

        private static DataTable GetPropertiesByPropertiesIDDB(int PropertiesID, bool IsOPPQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("PropertiesID",
                SqlDbType.Int, 0, PropertiesID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                  SqlDbType.Bit, 0, IsOPPQuote.ToString(),
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

            DataTable dt = exec.GetQuery("GetPropertiesByPropertiesID", xmlDoc);
            return dt;
        }

        private void SaveProperty(int UserID, bool IsOPPQuote, int index)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this.Mode)
                {
                    case 1:  //ADD
                        if(index == 1)
                            this.PropertiesID = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 2)
                            this.PropertiesID2 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 3)
                            this.PropertiesID3 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 4)
                            this.PropertiesID4 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 5)
                            this.PropertiesID5 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 6)
                            this.PropertiesID6 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 7)
                            this.PropertiesID7 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 8)
                            this.PropertiesID8 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 9)
                            this.PropertiesID9 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        if (index == 10)
                            this.PropertiesID10 = Executor.Insert("AddProperties", this.GetInsertPropertiesXml(IsOPPQuote));

                        //this.History(this._mode, UserID);
                        break;

                    case 3:  //DELETE
                        //Executor.Update("DeleteAutoGuardServicesContract",this.GetDeletePoliciesXml(IsOPPQuote));
                        break;

                    case 4:  //CLEAR						
                        break;

                    default: //UPDATE
                        //this.History(this._mode, UserID);
                        //Executor.Update("UpdateProperties", this.GetUpdatePropertiesXml(IsOPPQuote));
                        break;
                }
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the Property. " + xcp.Message, xcp);
            }
        }

        private XmlDocument GetInsertPropertiesXml(bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[107];

            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, this.TaskControlID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Building", SqlDbType.Bit, 0, this.Building.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Contents", SqlDbType.Bit, 0, this.Contents.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Another", SqlDbType.Bit, 0, this.Another.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Deductible",SqlDbType.Int, 0, this.Deductible.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ConstructionType", SqlDbType.Int, 0, this.ConstructionType.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Address1", SqlDbType.VarChar, 30, this.Address1.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Address2",  SqlDbType.VarChar, 30, this.Address2.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("City", SqlDbType.VarChar, 14, this.City.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("State", SqlDbType.Char, 2, this.State.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ZipCode", SqlDbType.VarChar, 10, this.ZipCode.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Description", SqlDbType.VarChar, 200, this.Description.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Families",  SqlDbType.Int, 0, this.Families.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Primary",  SqlDbType.Bit, 0, this.Primary.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Secondary", SqlDbType.Bit, 0, this.Secondary.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Rented",  SqlDbType.Bit, 0, this.Rented.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Pool",  SqlDbType.Bit, 0, this.Pool.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("HomeAssistance", SqlDbType.Float, 0, this.HomeAssistance.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingLimitFire", SqlDbType.Float, 0, this.BuildingLimitFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingLimitExtCoverage", SqlDbType.Float, 0, this.BuildingLimitExtCoverage.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingLimitVandalism", SqlDbType.Float, 0, this.BuildingLimitVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingLimitEarthquake", SqlDbType.Float, 0, this.BuildingLimitEarthquake.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingLimitAOP", SqlDbType.Float, 0, this.BuildingLimitAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingLimitExcessAOP", SqlDbType.Float, 0, this.BuildingLimitExcessAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingLimitTheft", SqlDbType.Float, 0, this.BuildingLimitTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingRateFire", SqlDbType.Float, 0, this.BuildingRateFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingRateExtCoverage",SqlDbType.Float, 0, this.BuildingRateExtCoverage.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingRateVandalism", SqlDbType.Float, 0, this.BuildingRateVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingRateEarthquake",  SqlDbType.Float, 0, this.BuildingRateEarthquake.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingRateAOP", SqlDbType.Float, 0, this.BuildingRateAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingRateExcessAOP", SqlDbType.Float, 0, this.BuildingRateExcessAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingRateTheft", SqlDbType.Float, 0, this.BuildingRateTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingFactorFire", SqlDbType.Float, 0, this.BuildingFactorFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingFactorExtCoverage", SqlDbType.Float, 0, this.BuildingFactorExtCoverage.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingFactorVandalism", SqlDbType.Float, 0, this.BuildingFactorVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingFactorEarthquake", SqlDbType.Float, 0, this.BuildingFactorEarthquake.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingFactorAOP", SqlDbType.Float, 0, this.BuildingFactorAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingFactorExcessAOP", SqlDbType.Float, 0, this.BuildingFactorExcessAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingFactorTheft", SqlDbType.Float, 0, this.BuildingFactorTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingTotalFire",SqlDbType.Float, 0, this.BuildingTotalFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingTotalExtCoverage",SqlDbType.Float, 0, this.BuildingTotalExtCoverage.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingTotalVandalism",SqlDbType.Float, 0, this.BuildingTotalVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingTotalEarthquake",SqlDbType.Float, 0, this.BuildingTotalEarthquake.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingTotalAOP",SqlDbType.Float, 0, this.BuildingTotalAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingTotalExcessAOP",SqlDbType.Float, 0, this.BuildingTotalExcessAOP.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingTotalTheft",SqlDbType.Float, 0, this.BuildingTotalTheft.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumFire", SqlDbType.Float, 0, this.BuildingPremiumFire.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumExtCoverage", SqlDbType.Float, 0, this.BuildingPremiumExtCoverage.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumVandalism",SqlDbType.Float, 0, this.BuildingPremiumVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumEarthquake", SqlDbType.Float, 0, this.BuildingPremiumEarthquake.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumAOP", SqlDbType.Float, 0, this.BuildingPremiumAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumExcessAOP",SqlDbType.Float, 0, this.BuildingPremiumExcessAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumTheft", SqlDbType.Float, 0, this.BuildingPremiumTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsLimitFire", SqlDbType.Float, 0, this.ContentsLimitFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsLimitExtCoverage", SqlDbType.Float, 0, this.ContentsLimitExtCoverage.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsLimitVandalism", SqlDbType.Float, 0, this.ContentsLimitVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsLimitEarthquake",SqlDbType.Float, 0, this.ContentsLimitEarthquake.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsLimitAOP",SqlDbType.Float, 0, this.ContentsLimitAOP.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsLimitExcessAOP", SqlDbType.Float, 0, this.ContentsLimitExcessAOP.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsLimitTheft", SqlDbType.Float, 0, this.ContentsLimitTheft.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsRateFire",SqlDbType.Float, 0, this.ContentsRateFire.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsRateExtCoverage", SqlDbType.Float, 0, this.ContentsRateExtCoverage.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsRateVandalism", SqlDbType.Float, 0, this.ContentsRateVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsRateEarthquake", SqlDbType.Float, 0, this.ContentsRateEarthquake.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsRateAOP", SqlDbType.Float, 0, this.ContentsRateAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsRateExcessAOP", SqlDbType.Float, 0, this.ContentsRateExcessAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsRateTheft",SqlDbType.Float, 0, this.ContentsRateTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsFactorFire", SqlDbType.Float, 0, this.ContentsFactorFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsFactorExtCoverage", SqlDbType.Float, 0, this.ContentsFactorExtCoverage.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsFactorVandalism", SqlDbType.Float, 0, this.ContentsFactorVandalism.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsFactorEarthquake", SqlDbType.Float, 0, this.ContentsFactorEarthquake.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsFactorAOP",SqlDbType.Float, 0, this.ContentsFactorAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsFactorExcessAOP",SqlDbType.Float, 0, this.ContentsFactorExcessAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsFactorTheft", SqlDbType.Float, 0, this.ContentsFactorTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsTotalFire", SqlDbType.Float, 0, this.ContentsTotalFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsTotalExtCoverage", SqlDbType.Float, 0, this.ContentsTotalExtCoverage.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsTotalVandalism", SqlDbType.Float, 0, this.ContentsTotalVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsTotalEarthquake", SqlDbType.Float, 0, this.ContentsTotalEarthquake.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsTotalAOP",SqlDbType.Float, 0, this.ContentsTotalAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsTotalExcessAOP", SqlDbType.Float, 0, this.ContentsTotalExcessAOP.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsTotalTheft", SqlDbType.Float, 0, this.ContentsTotalTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumFire", SqlDbType.Float, 0, this.ContentsPremiumFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumExtCoverage",SqlDbType.Float, 0, this.ContentsPremiumExtCoverage.ToString(),  ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumVandalism", SqlDbType.Float, 0, this.ContentsPremiumVandalism.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumEarthquake", SqlDbType.Float, 0, this.ContentsPremiumEarthquake.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumAOP", SqlDbType.Float, 0, this.ContentsPremiumAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumExcessAOP", SqlDbType.Float, 0, this.ContentsPremiumExcessAOP.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumTheft",SqlDbType.Float, 0, this.ContentsPremiumTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ExperienceCredit",SqlDbType.Int, 0, this.ExperienceCredit.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("BuildingPremiumTotal", SqlDbType.Float, 0, this.BuildingPremiumTotal.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ContentsPremiumTotal",SqlDbType.Float, 0, this.ContentsPremiumTotal.ToString(),ref cookItems);            
            DbRequestXmlCooker.AttachCookItem("SubTotalPremium", SqlDbType.Float, 0, this.SubTotalPremium.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Charge", SqlDbType.Float, 0, this.Charge.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("TotalPremium", SqlDbType.Float, 0, this.TotalPremium.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Bank", SqlDbType.Char, 3, this.Bank.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("LoanNo", SqlDbType.VarChar, 30, this.LoanNo.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("IsOppQuote", SqlDbType.Bit, 0, IsOppQuote.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiscAccess", SqlDbType.Int, 0, this.DiscAccess.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiscShutter", SqlDbType.Int, 0, this.DiscShutter.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiscTheft", SqlDbType.Int, 0, this.DiscTheft.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiscFire", SqlDbType.Int, 0, this.DiscFire.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("DiscUnderwritter", SqlDbType.Float, 0, this.DiscUnderwritter.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ConstructionYear", SqlDbType.VarChar, 4, this.ConstructionYear.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("NumOfStories", SqlDbType.VarChar, 4, this.NumOfStories.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Residence", SqlDbType.Bit, 0, this.Residence.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Condo", SqlDbType.Bit, 0, this.Condo.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("IsLiability", SqlDbType.Bit, 0, this.IsLiability.ToString(), ref cookItems);

            //DbRequestXmlCooker.AttachCookItem("PropertiesID", SqlDbType.Int, 0, this.GrdPropertiesID.ToString(), ref cookItems);
            //DbRequestXmlCooker.AttachCookItem("BankID", SqlDbType.VarChar, 3, this.BankID.ToString(), ref cookItems);

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

        public void SaveOPPPropertiesBank(int UserID, DataTable oPPPropertiesBankTemp, int taskControlID, int propertyID, bool IsOPPQuote)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            this.OPPPropertiesBankCollection = oPPPropertiesBankTemp;
            if (oPPPropertiesBankTemp.Rows.Count > 0)
            {
                for (int i = 0; i < oPPPropertiesBankTemp.Rows.Count; i++)
                {
                    this.Mode = 1; //Add

                    Executor.BeginTrans();
                    int dependienteID = Executor.Insert("AddOPPPropertiesBank", this.GetInsertOPPPropertiesBankXml(i, propertyID, IsOPPQuote));
                    Executor.CommitTrans();
                }
            }
        }

        public void DeleteOPPPropertiesBankByTaskControlID(int taskControlID, bool IsOPPQuote)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            Executor.BeginTrans();
            Executor.Update("DeleteOPPPropertiesBankByTaskcontrolID", DeleteOPPPropertiesBankXml(taskControlID, IsOPPQuote));
            Executor.CommitTrans();
        }

        private XmlDocument GetInsertOPPPropertiesBankXml(int index, int propertiesID, bool IsOPPQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[6];

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankID",
              SqlDbType.VarChar, 100, OPPPropertiesBankCollection.Rows[index]["BankID"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankDesc",
              SqlDbType.VarChar, 500, OPPPropertiesBankCollection.Rows[index]["BankDesc"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LoanNo",
              SqlDbType.VarChar, 500, OPPPropertiesBankCollection.Rows[index]["LoanNo"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertiesID",
              SqlDbType.Int, 0, propertiesID.ToString(),
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

        private XmlDocument DeleteOPPPropertiesBankXml(int taskControlID, bool IsOPPQuote)
        {

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
                 ref cookItems);

            //DbRequestXmlCooker.AttachCookItem("PropertiesID", SqlDbType.Int, 0, taskControlID.ToString(),
            //    ref cookItems);

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

        public static DataTable GetOPPPropertiesBankByTaskControlID(int TaskControlID, int PropertyID, bool IsOPPQuote)
        {
            DataTable dt = GetOPPPropertiesBankByTaskControlIDDB(TaskControlID, PropertyID, IsOPPQuote);
            return dt;
        }

        private static DataTable GetOPPPropertiesBankByTaskControlIDDB(int taskControlID, int PropertiesID, bool IsOPPQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertiesID", 
                SqlDbType.Int, 0, PropertiesID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
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

            DataTable dt = exec.GetQuery("[GetOPPPropertiesBankByTaskcontrolID]", xmlDoc);

            return dt;
        }

        //ADDITIONAL COVERAGE DETAIL\\
        public void SaveOPPAdditionalCoveragesDetail(int UserID, DataTable oPPAdditionalCoveragesDetailTemp, int taskControlID, bool IsOPPQuote)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            //int actualPropertyID = 0;
            Executor.Update("DeleteOPPAdditionalCoverageDetailByTaskcontrolID", DeleteOPPAdditionalCoverageDetailXml(IsOPPQuote, taskControlID));
            //SI SOLAMENTE BORRO LOS ADDCOV CON EL TASKCONTROLID, Y ANADO LOS DATOS DE DONDE LOS TASKCONTROLID SON IGUAL A LOS QUE SE BORRRARON

            this.OPPAdditionalCoverageDetailCollection = oPPAdditionalCoveragesDetailTemp;
            //if (oPPAdditionalCoveragesDetailTemp.Rows.Count > 0)
            //{
                //for (int j = 0; j < OPPAdditionalCoverageDetailCollection.Rows.Count; j++)
                //{
                //    if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID2_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID2;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID3_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID3;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID4_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID4;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID5_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID5;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID6_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID6;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID7_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID7;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID8_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID8;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID9_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID9;
                //    }

                //    else if (OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"].ToString().Trim() == _PropertiesID10_OLD.ToString().Trim())
                //    {
                //        OPPAdditionalCoverageDetailCollection.Rows[j]["PropertiesID"] = PropertiesID10;
                //    }
                //}

                //OPPAdditionalCoverageDetailCollection.AcceptChanges();

                for (int i = 0; i < OPPAdditionalCoverageDetailCollection.Rows.Count; i++)
                {
                    this.Mode = 1; //Add
                    //aqui, si oPPAdditionalCoveragesDetailTemp.rows[i]["TaskcontrolID"].toString() == taskcontrolID)
                    //if (int.Parse(oPPAdditionalCoveragesDetailTemp.Rows[i]["TaskcontrolID"].ToString()) == taskControlID)
                    //{
                    Executor.BeginTrans();
                    int dependienteID = Executor.Insert("AddOPPAdditionalCoverageDetail", this.GetInsertOPPAdditionalCoverageDetailXml(i, IsOPPQuote, taskControlID));
                    Executor.CommitTrans();
                    //}
                }
            //}
        }
       
        //public void DeleteOPPAdditionalCoverageDetailByTaskControlID(int taskControlID, int propertyID, bool IsOPPQuote)
        //{
        //    Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
        //    Executor.BeginTrans();
        //    Executor.Update("DeleteOPPAdditionalCoverageDetailByTaskcontrolID", DeleteOPPAdditionalCoverageDetailXml(IsOPPQuote));
        //    Executor.CommitTrans();
        //}

        private XmlDocument GetInsertOPPAdditionalCoverageDetailXml(int index, bool IsOPPQuote, int TaskcontrolID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[11];

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, TaskcontrolID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AdditionalCoveragesID",
              SqlDbType.Int, 0, OPPAdditionalCoverageDetailCollection.Rows[index]["AdditionalCoveragesID"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AdditionalCoveragesDesc",
              SqlDbType.VarChar, 500, OPPAdditionalCoverageDetailCollection.Rows[index]["AdditionalCoveragesDesc"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Description",
              SqlDbType.VarChar, 500, OPPAdditionalCoverageDetailCollection.Rows[index]["Description"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limits",
              SqlDbType.Float, 1, OPPAdditionalCoverageDetailCollection.Rows[index]["Limits"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Deductible",
              SqlDbType.Float, 1, OPPAdditionalCoverageDetailCollection.Rows[index]["Deductible"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium",
              SqlDbType.Float, 1, OPPAdditionalCoverageDetailCollection.Rows[index]["Premium"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertiesID",
              SqlDbType.Int, 0, OPPAdditionalCoverageDetailCollection.Rows[index]["PropertiesID"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RateValue1",
              SqlDbType.Float, 1, OPPAdditionalCoverageDetailCollection.Rows[index]["RateValue1"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RateValue2",
              SqlDbType.Int, 0, OPPAdditionalCoverageDetailCollection.Rows[index]["RateValue2"].ToString(),
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

        
        private XmlDocument DeleteOPPAdditionalCoverageDetailXml(bool IsOPPQuote, int TaskcontrolID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                 SqlDbType.Int, 0, TaskcontrolID.ToString(),
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

        public static DataTable GetOPPAdditionalCoverageDetail(bool IsOPPQuote, int taskControlID)
        {
            DataTable dt = GetOPPAdditionalCoverageDetailByTaskControlIDDB(IsOPPQuote, taskControlID);
            return dt;
        }

        //public static DataTable GetOPPAdditionalCoverageDetail(bool IsOPPQuote)
        //{
        //    DataTable dt = GetOPPAdditionalCoverageDetailByTaskControlIDDB(IsOPPQuote);
        //    return dt;
        //}

        private static DataTable GetOPPAdditionalCoverageDetailByTaskControlIDDB(bool IsOPPQuote, int taskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                 SqlDbType.Bit, 0, IsOPPQuote.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                 SqlDbType.Int, 0, taskControlID.ToString(),
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

            DataTable dt = exec.GetQuery("GetOPPAdditionalCoverageDetailByTaskcontrolID", xmlDoc);

            return dt;
        }


        #endregion


    }
}
