using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using EPolicy.Audit;
using EPolicy.LookupTables;

namespace EPolicy.Quotes
{
	//RPR 2004-02-11
	public struct MakeYearModel
	{
		public string Make, Year, Model;
		
		public MakeYearModel(string Make, string Year, string Model)
		{
			this.Make = Make;
			this.Year = Year;
			this.Model = Model;
		}

		public string this [int index]
		{
			get
			{
				switch(index)
				{
					case 0:
						return this.Make;
					case 1:
						return this.Year;
					case 2:
						return this.Model;
					default:
						return string.Empty;
				}
			}
			set
			{
				switch(index)
				{
					case 0:
						this.Make = value;
						break;
					case 1:
						this.Year = value;
						break;
					case 2:
						this.Model = value;
						break;
					default:
						break;
				}
			}
		}
	}

	public class AutoCover 
	{ 
		#region Private Attributes
		public int _TCIDForAutoCover;
		public int _ModeForHistory;
		private AutoCover _oldAutoCover = null;
		private System.Collections.ArrayList _oldAutoCover2;
		private int _QuotesAutoId;
		private int _QuotesId;
		private int _PolicySubClassId;
		private int _HomeCity;
		private int _WorkCity;
		private int _VehicleAge;
		private int _VehicleMake;
		private int _VehicleModel;
		private int _VehicleYear;
		private string _VIN;
		private string _Plate;
		private int _NewUse;
		private string _VehicleClass;
		private int _Territory;
		private int _AlarmType;
		private decimal _Cost;
		private string _PurchaseDate;
		private decimal _ActualValue;
		private decimal _Depreciation1stYear;
		private decimal _DepreciationAllYear;
		private int _ComprehensiveDeductible;
		private int _CollisionDeductible;
		private decimal _DiscountCompColl;
		private int _BodilyInjuryLimit;
		private int _PropertyDamageLimit;
		private int _CombinedSingleLimit;
		private decimal _DiscountBIPD;
		private int _MedicalLimit;
		private decimal _AssistancePremium;
		private decimal _TowingPremium;
		private decimal _VehicleRental;
		private int _LeaseLoanGapId;
		private int _SeatBelt;
		private int _PersonalAccidentRider;
		private decimal _Factor;
		private System.Collections.ArrayList _Breakdown;
		private System.Collections.ArrayList _AssignedDrivers;
		private System.Collections.ArrayList _Drivers2;
		private decimal _TotalAmount;
		private decimal _Charge;
		private int _Term;
		private int _Mode;
		private bool _isSecondVehicle;
		private string _EffectiveDate = (DateTime.Now).ToString("d");
		private int _InternalID;
		private bool _OvrrPremium = false;
        private string _License = "";
        private string _LicenseExpDate = "";
        private bool _IsLeasing = false;

        private double _MiscDiscount=0;
        private double _EmployeeDiscount = 0;
        private double _ExperienceDiscount = 0;
        private double _TotDiscount = 0;

        private int _AssistanceID = 0;
        private int _VehicleRentalID = 0;
        private int _AccidentalDeathID = 0;
        private decimal _AccidentalDeathPremium = 0;
        private int _AccidentalDeathPerson = 0;
        private int _EquipmentSoundID = 0;
        private decimal _EquipmentSoundPremium = 0;
        private int _EquipmentAudioID = 0;
        private decimal _EquipmentAudioPremium = 0;
        private bool _EquipmentTapes = false;
        private decimal _EquipmentTapesPremium = 0;
        private bool _SpecialEquipmentColl = false;
        private decimal _SpecialEquipmentCollPremium = 0;
        private bool _SpecialEquipmentComp = false;
        private decimal _SpecialEquipmentCompPremium = 0;
        private int _UninsuredSingleID = 0;
        private decimal _UninsuredSinglePremium = 0;
        private int _UninsuredSplitID = 0;
        private decimal _UninsuredSplitPremium = 0;
        private bool _IsAssistanceEmp = false;
        private int _CustomizeEquipLimit = 0;
        private bool _LoJack = false;
        private string _LojackExpDate = "";
        private string _LoJackCertificate = "";
        private decimal _OriginalMedicalPremium = 0;
        private decimal _OriginalTowingPremium = 0;
        private decimal _OriginalVehicleRental = 0;
        private int _TowingID = 0;

		//use for Policy only.
		private bool _IsPolicy = false;
		private string _Bank = "000";
		private string _CompanyDealer = "000";
		private string _AutoPolicyType = "";
		#endregion

        #region Public Properties
		
        public int QuotesAutoId
		{
			get
			{
				return(_QuotesAutoId);
			}
			set 
			{
				_QuotesAutoId = value;
			}
		}
     
		
		public int QuotesId
		{
			get
			{
				return(_QuotesId);
			}
			set 
			{
				_QuotesId = value;
			}
		}
     
		
		public int PolicySubClassId
		{
			get
			{
				return(_PolicySubClassId);
			}
			set 
			{
				_PolicySubClassId = value;
			}
		}
     
		
		public int HomeCity
		{
			get
			{
				return(_HomeCity);
			}
			set 
			{
				_HomeCity = value;
			}
		}
     
		
		public int WorkCity
		{
			get
			{
				return(_WorkCity);
			}
			set 
			{
				_WorkCity = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>VehicleAge</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int VehicleAge
		{
			get
			{
				return(_VehicleAge);
			}
			set 
			{
				_VehicleAge = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>VehicleMake</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int VehicleMake
		{
			get
			{
				return(_VehicleMake);
			}
			set 
			{
				_VehicleMake = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>VehicleModel</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int VehicleModel
		{
			get
			{
				return(_VehicleModel);
			}
			set 
			{
				_VehicleModel = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>VehicleYear</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int VehicleYear
		{
			get
			{
				return(_VehicleYear);
			}
			set 
			{
				_VehicleYear = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>VIN</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public string VIN
		{
			get
			{
				if(this._VIN == null)
					this._VIN = string.Empty;
				return(_VIN.Trim());
			}
			set 
			{
				_VIN = value.Trim();
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Plate</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public string Plate
		{
			get
			{
				if(this._Plate == null)
					this._Plate = string.Empty;
				return(_Plate.Trim());
			}
			set 
			{
				_Plate = value.Trim();
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>NewUse</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int NewUse
		{
			get
			{
				return(_NewUse);
			}
			set 
			{
				_NewUse = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>VehicleClass</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public string VehicleClass
		{
			get
			{
				if(this._VehicleClass == null)
					this._VehicleClass = string.Empty;
				return(_VehicleClass.Trim());
			}
			set 
			{
				_VehicleClass = value.Trim();
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Territory</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int Territory
		{
			get
			{
				return(_Territory);
			}
			set 
			{
				_Territory = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>AlarmType</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int AlarmType
		{
			get
			{
				return(_AlarmType);
			}
			set 
			{
				_AlarmType = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Cost</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal Cost
		{
			get
			{
				return(_Cost);
			}
			set 
			{
				_Cost = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>PurchaseDate</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public string PurchaseDate
		{
			get
			{
				if(this._PurchaseDate == null)
					this._PurchaseDate = string.Empty;
				return(_PurchaseDate.Trim());
			}
			set 
			{
				_PurchaseDate = value.Trim();
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>ActualValue</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal ActualValue
		{
			get
			{
				return(_ActualValue);
			}
			set 
			{
				_ActualValue = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Depreciation1stYear</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal Depreciation1stYear
		{
			get
			{
				return(_Depreciation1stYear);
			}
			set 
			{
				_Depreciation1stYear = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>DepreciationAllYear</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal DepreciationAllYear
		{
			get
			{
				return(_DepreciationAllYear);
			}
			set 
			{
				_DepreciationAllYear = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>ComprehensiveDeductible</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int ComprehensiveDeductible
		{
			get
			{
				return(_ComprehensiveDeductible);
			}
			set 
			{
				_ComprehensiveDeductible = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>CollisionDeductible</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int CollisionDeductible
		{
			get
			{
				return(_CollisionDeductible);
			}
			set 
			{
				_CollisionDeductible = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>DiscountCompColl</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal DiscountCompColl
		{
			get
			{
				return(_DiscountCompColl);
			}
			set 
			{
				_DiscountCompColl = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>BodyInjuryLimit</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int BodilyInjuryLimit
		{
			get
			{
				return(_BodilyInjuryLimit);
			}
			set 
			{
				_BodilyInjuryLimit = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>PropertyDamageLimit</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int PropertyDamageLimit
		{
			get
			{
				return(_PropertyDamageLimit);
			}
			set 
			{
				_PropertyDamageLimit = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>CombinedSingleLimit</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int CombinedSingleLimit
		{
			get
			{
				return(_CombinedSingleLimit);
			}
			set 
			{
				_CombinedSingleLimit = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>DiscountBIPD</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal DiscountBIPD
		{
			get
			{
				return(_DiscountBIPD);
			}
			set 
			{
				_DiscountBIPD = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>MedicalLimit</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int MedicalLimit
		{
			get
			{
				return(_MedicalLimit);
			}
			set 
			{
				_MedicalLimit = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>AssistancePremium</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal AssistancePremium
		{
			get
			{
				return(_AssistancePremium);
			}
			set 
			{
				_AssistancePremium = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>TowingPremium</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal TowingPremium
		{
			get
			{
				return(_TowingPremium);
			}
			set 
			{
				_TowingPremium = value;
			}
		}
     
		public decimal VehicleRental
		{
			get
			{
				return(_VehicleRental);
			}
			set 
			{
				_VehicleRental = value;
			}
		}
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>LeaseLoanGapId</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int LeaseLoanGapId
		{
			get
			{
				return(_LeaseLoanGapId);
			}
			set 
			{
				_LeaseLoanGapId = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>SeatBelt</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int SeatBelt
		{
			get
			{
				return(_SeatBelt);
			}
			set 
			{
				_SeatBelt = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>PersonalAccidentRider</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int PersonalAccidentRider
		{
			get
			{
				return(_PersonalAccidentRider);
			}
			set 
			{
				_PersonalAccidentRider = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Factor</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal Factor
		{
			get
			{
				return(_Factor);
			}
			set 
			{
				_Factor = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Breakdown</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public System.Collections.ArrayList Breakdown
		{
			get
			{
				if (_Breakdown == null)
					_Breakdown = new System.Collections.ArrayList();
				return(_Breakdown);
			}
			set 
			{
				_Breakdown = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>AssignedDrivers</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public System.Collections.ArrayList AssignedDrivers
		{
			get
			{
				if (_AssignedDrivers == null)
					_AssignedDrivers = new System.Collections.ArrayList();
				return(_AssignedDrivers);
			}
			set 
			{
				_AssignedDrivers = value;
			}
		}
     
		public decimal TotalAmount
		{
			get
			{
				return(_TotalAmount);
			}
			set 
			{
				_TotalAmount = value;
			}
		}
     
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Charge</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public decimal Charge
		{
			get
			{
				return(_Charge);
			}
			set 
			{
				_Charge = value;
			}
		}
 
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Term</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int Term
		{
			get
			{
				return(_Term);
			}
			set 
			{
				_Term = value;
			}
		}

		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>Mode</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int Mode
		{
			get
			{
				return(_Mode);
			}
			set 
			{
				_Mode = value;
			}
		}
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>EffectiveDate</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public string EffectiveDate
		{
			get
			{
				if(this._EffectiveDate == null)
					this._EffectiveDate = string.Empty;
				return(_EffectiveDate.Trim());
			}
			set 
			{
				_EffectiveDate = value.Trim();
			}
		}
   
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>isSecondVehicle</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public bool isSecondVehicle
		{
			get
			{
				return(_isSecondVehicle);
			}
			set 
			{
				_isSecondVehicle = value;
			}
		}
   
		/// <summary>
		///        
		/// </summary>  
		/// <remarks>
		/// <propertyName>InternalID</propertyName><br/>
		/// <author>Benigno Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>
		public int InternalID
		{
			get
			{
				return(_InternalID);
			}
			set 
			{
				_InternalID = value;
			}
		}
   
		public string Bank
		{
			get
			{
				return(_Bank);
			}
			set 
			{
				_Bank = value;
			}
		}

		public string CompanyDealer
		{
			get
			{
				return(_CompanyDealer);
			}
			set 
			{
				_CompanyDealer = value;
			}
		}

		public string AutoPolicyType
		{
			get
			{
				return(_AutoPolicyType);
			}
			set 
			{
				_AutoPolicyType = value;
			}
		}

		public bool OvrrPremium
		{
			get
			{
				return(_OvrrPremium); 
			}
			set 
			{
				_OvrrPremium = value;
			}
		}

        public string License
        {
            get
            {
                return (_License);
            }
            set
            {
                _License = value;
            }
        }

        public string LicenseExpDate
        {
            get
            {
                return (_LicenseExpDate);
            }
            set
            {
                _LicenseExpDate = value;
            }
        }

        public bool IsLeasing
        {
            get
            {
                return (_IsLeasing);
            }
            set
            {
                _IsLeasing = value;
            }
        }

        public double MiscDiscount
        {
            get
            {
                return (_MiscDiscount);
            }
            set
            {
                _MiscDiscount = value;
            }
        }
        public double EmployeeDiscount
        {
            get
            {
                return (_EmployeeDiscount);
            }
            set
            {
                _EmployeeDiscount = value;
            }
        }
        public double ExperienceDiscount
        {
            get
            {
                return (_ExperienceDiscount);
            }
            set
            {
                _ExperienceDiscount = value;
            }
        }
        public double TotDiscount
        {
            get
            {
                return (_TotDiscount);
            }
            set
            {
                _TotDiscount = value;
            }
        }

        public int AssistanceID
        {
            get
            {
                return (_AssistanceID);
            }
            set
            {
                _AssistanceID = value;
            }
        }
        public int VehicleRentalID
        {
            get
            {
                return (_VehicleRentalID);
            }
            set
            {
                _VehicleRentalID = value;
            }
        }
        public int AccidentalDeathID
        {
            get
            {
                return (_AccidentalDeathID);
            }
            set
            {
                _AccidentalDeathID = value;
            }
        }
        public decimal AccidentalDeathPremium
        {
            get
            {
                return (_AccidentalDeathPremium);
            }
            set
            {
                _AccidentalDeathPremium = value;
            }
        }
        public int AccidentalDeathPerson
        {
            get
            {
                return (_AccidentalDeathPerson);
            }
            set
            {
                _AccidentalDeathPerson = value;
            }
        }
        public int EquipmentSoundID
        {
            get
            {
                return (_EquipmentSoundID);
            }
            set
            {
                _EquipmentSoundID = value;
            }
        }
        public decimal EquipmentSoundPremium
        {
            get
            {
                return (_EquipmentSoundPremium);
            }
            set
            {
                _EquipmentSoundPremium = value;
            }
        }
        public int EquipmentAudioID
        {
            get
            {
                return (_EquipmentAudioID);
            }
            set
            {
                _EquipmentAudioID = value;
            }
        }
        public decimal EquipmentAudioPremium
        {
            get
            {
                return (_EquipmentAudioPremium);
            }
            set
            {
                _EquipmentAudioPremium = value;
            }
        }
        public bool EquipmentTapes
        {
            get
            {
                return (_EquipmentTapes);
            }
            set
            {
                _EquipmentTapes = value;
            }
        }
        public decimal EquipmentTapesPremium
        {
            get
            {
                return (_EquipmentTapesPremium);
            }
            set
            {
                _EquipmentTapesPremium = value;
            }
        }
        public bool SpecialEquipmentColl
        {
            get
            {
                return (_SpecialEquipmentColl);
            }
            set
            {
                _SpecialEquipmentColl = value;
            }
        }
        public decimal SpecialEquipmentCollPremium
        {
            get
            {
                return (_SpecialEquipmentCollPremium);
            }
            set
            {
                _SpecialEquipmentCollPremium = value;
            }
        }
        public bool SpecialEquipmentComp
        {
            get
            {
                return (_SpecialEquipmentComp);
            }
            set
            {
                _SpecialEquipmentComp = value;
            }
        }
        public decimal SpecialEquipmentCompPremium
        {
            get
            {
                return (_SpecialEquipmentCompPremium);
            }
            set
            {
                _SpecialEquipmentCompPremium = value;
            }
        }
        public int UninsuredSingleID
        {
            get
            {
                return (_UninsuredSingleID);
            }
            set
            {
                _UninsuredSingleID = value;
            }
        }
        public decimal UninsuredSinglePremium
        {
            get
            {
                return (_UninsuredSinglePremium);
            }
            set
            {
                _UninsuredSinglePremium = value;
            }
        }
        public int UninsuredSplitID
        {
            get
            {
                return (_UninsuredSplitID);
            }
            set
            {
                _UninsuredSplitID = value;
            }
        }
        public decimal UninsuredSplitPremium
        {
            get
            {
                return (_UninsuredSplitPremium);
            }
            set
            {
                _UninsuredSplitPremium = value;
            }
        }
        public bool IsAssistanceEmp
        {
            get
            {
                return (_IsAssistanceEmp);
            }
            set
            {
                _IsAssistanceEmp = value;
            }
        }
        public int CustomizeEquipLimit
        {
            get
            {
                return (_CustomizeEquipLimit);
            }
            set
            {
                _CustomizeEquipLimit = value;
            }
        }
        public bool LoJack
        {
            get
            {
                return (_LoJack);
            }
            set
            {
                _LoJack = value;
            }
        }
        public string LojackExpDate
        {
            get
            {
                return (_LojackExpDate);
            }
            set
            {
                _LojackExpDate = value;
            }
        }
        public string LoJackCertificate
        {
            get
            {
                return (_LoJackCertificate);
            }
            set
            {
                _LoJackCertificate = value;
            }
        }
        public decimal OriginalMedicalPremium
        {
            get
            {
                return (_OriginalMedicalPremium);
            }
            set
            {
                _OriginalMedicalPremium = value;
            }
        }
        public decimal OriginalTowingPremium
        {
            get
            {
                return (_OriginalTowingPremium);
            }
            set
            {
                _OriginalTowingPremium = value;
            }
        }
        public decimal OriginalVehicleRental
        {
            get
            {
                return (_OriginalVehicleRental);
            }
            set
            {
                _OriginalVehicleRental = value;
            }
        }
        public int TowingID
        {
            get
            {
                return (_TowingID);
            }
            set
            {
                _TowingID = value;
            }
        }
#endregion

        #region Public Methods

		//:RPR 2004-05-25
		public static bool IsCSLonly(int ClassID)
		{
			DbRequestXmlCookRequestItem[] items = 
			{
				new DbRequestXmlCookRequestItem("PolicySubClassID", 
				SqlDbType.Int, 0, ClassID.ToString())};
			Baldrich.DBRequest.DBRequest executer = new Baldrich.DBRequest.DBRequest();
			DataTable result;

			try
			{
				result = executer.GetQuery("IsCSLonly",
					DbRequestXmlCooker.Cook(items));
				if(result.Rows.Count > 0)
				{
					return bool.Parse(result.Rows[0]["CSLonly"].ToString());
				}
				else
				{
					throw new Exception(
						"An error ocurred while retrieving CSLonly " + 
						"property for class id '" + 
						ClassID.ToString() + "'.");
				}
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve CSLonly property for " +
					"the requested ClassID.", ex);
			}
		}

		public static bool IsMasterCover(int ClassID)
		{
			DbRequestXmlCookRequestItem[] items = 
			{
					new DbRequestXmlCookRequestItem("PolicySubClassID", 
				SqlDbType.Int, 0, ClassID.ToString())};
			Baldrich.DBRequest.DBRequest executer = new Baldrich.DBRequest.DBRequest();
			DataTable result;

			try
			{
				result = executer.GetQuery("IsMasterCover",
					DbRequestXmlCooker.Cook(items));
				if(result.Rows.Count > 0)
				{
					return bool.Parse(result.Rows[0]["IsMaster"].ToString());
				}
				else
				{
					throw new Exception(
						"An error ocurred while retrieving IsMaster " + 
						"property for class id '" + 
						ClassID.ToString() + "'.");
				}
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve a IsMaster property for " +
					"the requested ClassID.", ex);
			}
		}

		public static bool IsSpecialCover(int ClassID)
		{
			DbRequestXmlCookRequestItem[] items = 
			{
					new DbRequestXmlCookRequestItem("PolicySubClassID", 
				SqlDbType.Int, 0, ClassID.ToString())};
			Baldrich.DBRequest.DBRequest executer = new Baldrich.DBRequest.DBRequest();
			DataTable result;

			try
			{
				result = executer.GetQuery("IsSpecialCover",
					DbRequestXmlCooker.Cook(items));
				if(result.Rows.Count > 0)
				{
					return bool.Parse(result.Rows[0]["IsSpecial"].ToString());
				}
				else
				{
					throw new Exception(
						"An error ocurred while retrieving IsSpecial " + 
						"property for class id '" + 
						ClassID.ToString() + "'.");
				}
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve a IsSpecial property for " +
					"the requested ClassID.", ex);
			}
		}


		public static int GetAutoPolicySubClassBaseClassID(int ClassID)
		{
			DbRequestXmlCookRequestItem[] items = 
			{
					new DbRequestXmlCookRequestItem("PolicySubClassID", 
				SqlDbType.Int, 0, ClassID.ToString())};
			Baldrich.DBRequest.DBRequest executer = new Baldrich.DBRequest.DBRequest();
			DataTable result;

			try
			{
				result = executer.GetQuery("GetAutoPolicySubClassBaseClassID",
					DbRequestXmlCooker.Cook(items));
				if(result.Rows.Count > 0)
				{
					return int.Parse(
						result.Rows[0]["AutoPolicyBaseSubClassID"].ToString());
				}
				else
				{
					throw new Exception(
						"An error ocurred while retrieving " + 
						"base class id for class id '" + 
						ClassID.ToString() + "'.");
				}
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve a BaseClassID for " +
					"the requested ClassID.", ex);
			}
		}
	
		public static MakeYearModel GetMakeYearModel(int VehicleMakeID, int VehicleModelID,int VehicleYearID)
		{
			MakeYearModel mym = new MakeYearModel(string.Empty,
				string.Empty, string.Empty);

			string[,] mymMetaData = 
				new string[3,3]
			{
					{"MakeID", VehicleMakeID.ToString(), 
				 "GetMakeDescriptionFromMakeID"},
			{
					"YearID", VehicleYearID.ToString(),
				"GetYearFromYearID"},
			{
					"ModelID", VehicleModelID.ToString(), 
				"GetModelDescriptionFromModelID"}};

			for(int i = 0; i <= mymMetaData.GetUpperBound(0) ||
				i <= 2 /*mym indexer upper bound*/; i++)
			{
				if(mymMetaData[i,1] != "0")
				{
					try
					{
						mym[i] = 
							GetMakeYearModelDescription(mymMetaData[i,0], 
							mymMetaData[i,1], mymMetaData[i,2]);
					}
					catch(Exception ex)
					{
						throw new Exception("Could not retrieve data from '" +
							mymMetaData[i,2] + "'.", ex);
					}
				}
			}		
			return mym;
		}

		
		public void AddCoverBreakdown(Enumerators.Premiums Type, int BDAnniversary, object BDValue)
		{
			CoverBreakdown Srch = new CoverBreakdown();
			Srch.Type = (int)Type;

			if(_Breakdown == null)
				_Breakdown = new ArrayList();
		
			if(_Breakdown.Contains(Srch))
			{
				CoverBreakdown CB = (CoverBreakdown)_Breakdown[_Breakdown.IndexOf(Srch)];
				CB.AddCoverBreakdown(BDAnniversary, BDValue);
			}
			else
			{
				CoverBreakdown CB = new CoverBreakdown();
				CB.Type = (int)Type;
				CB.AddCoverBreakdown(BDAnniversary, BDValue);
				_Breakdown.Add(CB);
			}
		}
	
		
		public void Calculate(bool ispolicy)
		{
			this._IsPolicy = ispolicy;

			AutoCoverCalculator ACC = new AutoCoverCalculator();
			ACC.SetIsPolicy(ispolicy);

			ACC.Calculate(this);	
		}

		public decimal CollisionPremium()
		{
			return GetBreakdownTotal(Enumerators.Premiums.Collision);
		}
		
		public decimal ComprehensivePremium()
		{
			return GetBreakdownTotal(Enumerators.Premiums.Comprehensive);
		}

        public decimal BodilyInjuryPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.BodilyInjury);
        }

        public decimal PropertyDamagePremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.PropertyDamage);
        }
		
		public decimal CombinedSinglePremium()
		{
			return GetBreakdownTotal(Enumerators.Premiums.CombinedSingle);
		}

        public decimal LeaseLoanGapPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.LeaseLoanGap);
        }

        public decimal MedicalPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.Medical);
        }

        public decimal PersonalAccidentRiderPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.PersonalAccidentRider);
        }

        public decimal SeatBeltPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.SeatBelt);
        }
        
        public decimal AccidentalDeathBDPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.AccidentalDeath);
        }
        public decimal UninsuredSingleBDPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.UninsuredSingle);
        }
        public decimal UninsuredSplitBDPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.UninsuredSplit);
        }
        public decimal EquipmentAudioBDPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.EquipmentAudio);
        }
        public decimal EquipmentTapesBDPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.EquipmentTapes);
        }
        public decimal CustomizeEquipmentCollBDPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.CustomizeEquipmentColl);
        }
        public decimal CustomizeEquipmentCompBDPremium()
        {
            return GetBreakdownTotal(Enumerators.Premiums.CustomizeEquipmentComp);
        }

		public override bool Equals(object Obj)
		{
			try
			{
				if (!Obj.GetType().Equals(this.GetType()))
					return false;
				AutoCover ad = (AutoCover)Obj;
				// if Mode == "Delete" don't compare anything else
				// verify if QuotesAutoID are equal, if both are not '0'
				// finally check VIN
				// if no VIN or QuotesAutoID use InternalID
			
				if (this.Mode != (int)Enumerators.Modes.Delete)    
				{
					if ((this._QuotesAutoId != 0 && ad._QuotesAutoId != 0 &&
						this._QuotesAutoId == ad._QuotesAutoId) 
					
						//VIN # is external, mutable, therefore should
						//not be part of this test.  Discovered when attempting
						//to save the edition of vehicles whose VIN had been
						//edited (a new vehicle was spawned).
						//Rafael Pérez
						//2004-02-11
						/*|| 
						(((this._VIN != null && this._VIN != "") 
						&& (ad.VIN != null && ad.VIN != "")) &&
						this._VIN == ad.VIN)*/) 
						return true;
					else if ((this._QuotesAutoId == 0 
						/*&&
						(this._VIN == null || this._VIN == ""))*/ &&
						this._InternalID == ad._InternalID))
						return true;
				}
			}
			catch //???
			{
				return false; //???
			}
			return false;
		}

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>GetAssignedDriver</functionName><br/>
		/// <author>B. Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// string VIN
		/// <param name=""></param>
		/// <returns>AutoCover</returns>

		public AssignedDriver GetAssignedDriver(AssignedDriver AD)
		{
			int t = _AssignedDrivers.IndexOf(AD);
			if(t >= 0)
			{
				return (AssignedDriver)_AssignedDrivers[t];
			}
			return AD;
		}
        
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public void RemoveAssignedDriver(AssignedDriver Driver)
		{
			if (AssignedDrivers.Contains(Driver))
				AssignedDrivers.Remove(Driver);
		}
        
		public void Save(int UserID)
		{
			if (this._Mode ==2)
			{
				_Drivers2 = AutoDriver.LoadDriversForQuote(this.QuotesId,this._IsPolicy);
				_oldAutoCover2 = AutoCover.LoadAutoCoverForQuote(this.QuotesId, _Drivers2, this._IsPolicy);

				for (int i = 0; i < _oldAutoCover2.Count; i++)
				{
					_oldAutoCover = (AutoCover) _oldAutoCover2[i];
					if(_oldAutoCover.QuotesAutoId == this.QuotesAutoId)
					{
						i=_oldAutoCover2.Count;
					}
				}			
			}

			// Save, Update or Delete Auto Cover
			if (this._Mode == (int)Enumerators.Modes.Insert)
			{
				this.Insert(UserID);
			}
			else if(this._Mode == (int)Enumerators.Modes.Delete)
			{
				this.Delete(UserID);
			}
			else if(this._Mode == (int)Enumerators.Modes.Update)
			{
				this.History(this.Mode,UserID);
				this.Update(UserID);
			}
		
			if (this._Mode != (int)Enumerators.Modes.Delete)
			{
				// delete CoverBreakdown && Assigned Drivers
				this.DeleteQuotesAutoCoversAndDrivers();
			
				// save Cover Breakdown
				if (_Breakdown != null)
				{
					for(int i = 0; i < _Breakdown.Count; i++)
					{
						CoverBreakdown CB = (CoverBreakdown)_Breakdown[i];
						CB.AutoCoverID = this.QuotesAutoId;
						if(_IsPolicy)
						{
							CB.SetIsPolicy(true);
						}

						CB.Save(UserID);
					}
				}

				// save Assigned Drivers
				if (_AssignedDrivers != null)
				{
					for(int i = 0; i < _AssignedDrivers.Count; i++)
					{
						AssignedDriver AD = (AssignedDriver)_AssignedDrivers[i];
						AD.AutoCoverID = this.QuotesAutoId;

						if(_IsPolicy)
						{
							AD.SetIsPolicy(true);
						}

						AD.Save(UserID);
					}
				}
			}

			//Victor
			if(_IsPolicy)			
				this.AutoPolicyType = this.GetAutoPolicyType(this.PolicySubClassId);
		}

		public static ArrayList LoadAutoCoverForQuote(int QuotesID, ArrayList Drivers, bool ispolicy)
		{
			DataTable dt = GetQuotesAutoForQuote(QuotesID, ispolicy);
            ArrayList AL = new ArrayList(dt.Rows.Count);
            //REMOVER
            if (dt.Rows.Count <= 0)
            {
               AL = new ArrayList(dt.Rows.Count + 1); 
            }
            //ACABA REMOVER
           
			for(int i = 0; i < dt.Rows.Count; i++)
			{
				AutoCover AC = new AutoCover();
				AC._IsPolicy = ispolicy;
				AC.FillProperties(dt.Rows[i]);
				// Load Assigned Drivers
				AC._AssignedDrivers = AssignedDriver.LoadDriversForAutoCover(AC.QuotesAutoId, Drivers, ispolicy);
			
				// Load Cover Breakdown
                //AC._Breakdown = CoverBreakdown.LoadCoverBreakdownForAutoCover(AC.QuotesAutoId, ispolicy);
			
				// Set Internal ID
				AC.InternalID = 1 + i;
				AL.Add(AC);
			}
			return AL;
		}  // end LoadAutoCoverForQuote()

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>GetQuotesAutoByCriteria</functionName><br/>
		/// <author>B. Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// AutoCover AC
		/// <param name=""></param>
		/// <returns>DataTable</returns>

		public static DataTable GetQuotesAutoByCriteria(AutoCover AC)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>VIN</name>");
			sb.Append("<type>char</type>");
			sb.Append("<length>18</length>");
			sb.Append("<value>" + AC.VIN + "</value>");
			sb.Append("</parameter>");

			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable DT = exec.GetQuery("GetQuotesAutoByCriteria",xmlDoc);
		
			return DT;
		} // end GetQuotesAutoByCriteria()

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>GetQuotesAuto</functionName><br/>
		/// <author>B. Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// string VIN
		/// <param name=""></param>
		/// <returns>AutoCover</returns>

		public static AutoCover GetQuotesAuto(string VIN)
		{
			AutoCover AC = new AutoCover();
			AC.VIN = VIN;
			DataTable DT = GetQuotesAutoByCriteria(AC);
			if (DT.Rows.Count != 0)
			{
				AC.FillProperties(DT.Rows[0]);
			}
			return AC;
		}

		public void SetIsPolicy(bool ispolicy)
		{
			this._IsPolicy = ispolicy;
		}
		#endregion

        #region Private Methods

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>GetAutoCoverXml</functionName><br/>
		/// <author>B. Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// 
		/// <param name=""></param>
		/// <returns>XmlDocument</returns>

		private static string GetMakeYearModelDescription(string ParameterName,
			string ID, string SpName)
		{
			Baldrich.DBRequest.DBRequest dbRequest = new Baldrich.DBRequest.DBRequest();
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			try
			{
				DbRequestXmlCooker.AttachCookItem(ParameterName,
					SqlDbType.Int, 0, ID, ref cookItems);
				return dbRequest.GetQuery(SpName,
					DbRequestXmlCooker.Cook(cookItems)).Rows[0][0].ToString();
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve make, year, " + 
					"or model description.", ex);
			}
		}

		private XmlDocument GetAutoCoverXml()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();
			
			sb.Append("<parameters>");

			//		sb.Append("<parameter>");
			//		sb.Append("<name>QuotesAutoID</name>");
			//		sb.Append("<type>int</type>");
			//		sb.Append("<value>" + this._QuotesAutoId + "</value>");
			//		sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>QuotesID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._QuotesId + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PolicySubClassId</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._PolicySubClassId + "</value>");
			sb.Append("</parameter>");
		
			sb.Append("<parameter>");
			sb.Append("<name>HomeCity</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._HomeCity + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>WorkCity</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._WorkCity + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleAge</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleAge + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleMakeID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleMake + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleModelID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleModel + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleYearID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleYear + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VIN</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>18</Length>");
			sb.Append("<value>" + this._VIN + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Plate</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>8</Length>");
			sb.Append("<value>" + this._Plate + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>NewUse</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._NewUse + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleClassID</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>2</Length>");
			sb.Append("<value>" + this._VehicleClass + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>TerritoryID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._Territory + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>AlarmTypeID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._AlarmType + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Cost</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._Cost + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PurchaseDate</name>");
			sb.Append("<type>DateTime</type>");
			sb.Append("<value>" + this._PurchaseDate + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>ActualValue</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._ActualValue + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Depreciation1stYear</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._Depreciation1stYear + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>DepreciationAllYear</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._DepreciationAllYear + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>ComprehensiveDeductibleID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._ComprehensiveDeductible + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>ComprehensivePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.ComprehensivePremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CollisionDeductibleID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._CollisionDeductible + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CollisionPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.CollisionPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>DiscountCompColl</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._DiscountCompColl + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>BodilyInjuryLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._BodilyInjuryLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>BodilyInjuryPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.BodilyInjuryPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PropertyDamageLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._PropertyDamageLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PropertyDamagePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.PropertyDamagePremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CombinedSingleLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._CombinedSingleLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CombinedSinglePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.CombinedSinglePremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>DiscountBIPD</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._DiscountBIPD + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>MedicalLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._MedicalLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>MedicalPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.MedicalPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>AssistancePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._AssistancePremium + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>TowingPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._TowingPremium + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleRental</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._VehicleRental + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>LeaseLoanGapID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._LeaseLoanGapId + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>LeaseLoanGapPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.LeaseLoanGapPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>SeatBelt</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._SeatBelt + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>SeatBeltPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.SeatBeltPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PersonalAccidentRider</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._PersonalAccidentRider + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PersonalAccidentRiderPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.PersonalAccidentRiderPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Factor</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._Factor + "</value>");
			sb.Append("</parameter>");

            //if(this._IsPolicy)
            //{
				sb.Append("<parameter>");
				sb.Append("<name>Bank</name>");
				sb.Append("<type>char</type>");
				sb.Append("<Length>3</Length>");
				sb.Append("<value>" + this._Bank + "</value>");
				sb.Append("</parameter>");

				sb.Append("<parameter>");
				sb.Append("<name>CompanyDealer</name>");
				sb.Append("<type>char</type>");
				sb.Append("<Length>3</Length>");
				sb.Append("<value>" + this._CompanyDealer + "</value>");
				sb.Append("</parameter>");
            //}

                sb.Append("<parameter>");
                sb.Append("<name>LicenseNumber</name>");
                sb.Append("<type>varchar</type>");
                sb.Append("<Length>10</Length>");
                sb.Append("<value>" + this._License + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LicenseExpDate</name>");
                sb.Append("<type>Datetime</type>");
                sb.Append("<value>" + this._LicenseExpDate + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>IsLeasing</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._IsLeasing + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>ExperienceDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._ExperienceDiscount + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EmployeeDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._EmployeeDiscount + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>MiscDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._MiscDiscount + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>TotDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._TotDiscount + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>AssistanceID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._AssistanceID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>VehicleRentalID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._VehicleRentalID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>AccidentalDeathID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._AccidentalDeathID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>AccidentalDeathPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._AccidentalDeathPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>AccidentalDeathPerson</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._AccidentalDeathPerson + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentSoundID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._EquipmentSoundID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentSoundPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this.EquipmentSoundPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentAudioID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._EquipmentAudioID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentAudioPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._EquipmentAudioPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentTapes</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._EquipmentTapes + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentTapesPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._EquipmentTapesPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentColl</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._SpecialEquipmentColl + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentCollPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._SpecialEquipmentCollPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentComp</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._SpecialEquipmentComp + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentCompPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._SpecialEquipmentCompPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSingleID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._UninsuredSingleID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSinglePremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._UninsuredSinglePremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSplitID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._UninsuredSplitID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSplitPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._UninsuredSplitPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>IsAssistanceEmp</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._IsAssistanceEmp + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>CustomizeEquipLimit</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._CustomizeEquipLimit + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LoJack</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._LoJack + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LojackExpDate</name>");
                sb.Append("<type>varchar</type>");
                sb.Append("<Length>20</Length>");
                sb.Append("<value>" + this._LojackExpDate + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LoJackCertificate</name>");
                sb.Append("<type>varchar</type>");
                sb.Append("<Length>50</Length>");
                sb.Append("<value>" + this._LoJackCertificate + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>OriginalMedicalPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._OriginalMedicalPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>OriginalTowingPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._OriginalTowingPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>OriginalVehicleRental</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._OriginalVehicleRental + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>TowingID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._TowingID + "</value>");
                sb.Append("</parameter>");

			sb.Append("</parameters>");

			xmlDoc.InnerXml = sb.ToString();

			sb = null;

			return xmlDoc;
		}
		
		private XmlDocument GetAutoCoverUpdateXml()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();
			
			sb.Append("<parameters>");

			sb.Append("<parameter>");
			sb.Append("<name>QuotesAutoID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._QuotesAutoId + "</value>");
			sb.Append("</parameter>");

			//		sb.Append("<parameter>");
			//		sb.Append("<name>QuotesID</name>");
			//		sb.Append("<type>int</type>");
			//		sb.Append("<value>" + this._QuotesId + "</value>");
			//		sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PolicySubClassId</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._PolicySubClassId + "</value>");
			sb.Append("</parameter>");
		
			sb.Append("<parameter>");
			sb.Append("<name>HomeCity</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._HomeCity + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>WorkCity</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._WorkCity + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleAge</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleAge + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleMakeID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleMake + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleModelID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleModel + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleYearID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._VehicleYear + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VIN</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>18</Length>");
			sb.Append("<value>" + this._VIN + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Plate</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>8</Length>");
			sb.Append("<value>" + this._Plate + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>NewUse</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._NewUse + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleClassID</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>2</Length>");
			sb.Append("<value>" + this._VehicleClass + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>TerritoryID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._Territory + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>AlarmTypeID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._AlarmType + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Cost</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._Cost + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PurchaseDate</name>");
			sb.Append("<type>DateTime</type>");
			sb.Append("<value>" + this._PurchaseDate + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>ActualValue</name>");
			sb.Append("<type>Money</type>");
			sb.Append("<value>" + this._ActualValue + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Depreciation1stYear</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._Depreciation1stYear + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>DepreciationAllYear</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._DepreciationAllYear + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>ComprehensiveDeductibleID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._ComprehensiveDeductible + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>ComprehensivePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.ComprehensivePremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CollisionDeductibleID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._CollisionDeductible + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CollisionPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.CollisionPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>DiscountCompColl</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._DiscountCompColl + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>BodilyInjuryLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._BodilyInjuryLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>BodilyInjuryPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.BodilyInjuryPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PropertyDamageLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._PropertyDamageLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PropertyDamagePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.PropertyDamagePremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CombinedSingleLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._CombinedSingleLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>CombinedSinglePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.CombinedSinglePremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>DiscountBIPD</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._DiscountBIPD + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>MedicalLimitID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._MedicalLimit + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>MedicalPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.MedicalPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>AssistancePremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._AssistancePremium + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>TowingPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._TowingPremium + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>VehicleRental</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this._VehicleRental + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>LeaseLoanGapID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._LeaseLoanGapId + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>LeaseLoanGapPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.LeaseLoanGapPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>SeatBelt</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._SeatBelt + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>SeatBeltPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.SeatBeltPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PersonalAccidentRider</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._PersonalAccidentRider + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>PersonalAccidentRiderPremium</name>");
			sb.Append("<type>money</type>");
			sb.Append("<value>" + this.PersonalAccidentRiderPremium() + "</value>");
			sb.Append("</parameter>");

			sb.Append("<parameter>");
			sb.Append("<name>Factor</name>");
			sb.Append("<type>float</type>");
			sb.Append("<value>" + this._Factor + "</value>");
			sb.Append("</parameter>");

            //if(this._IsPolicy)
            //{
				sb.Append("<parameter>");
				sb.Append("<name>Bank</name>");
				sb.Append("<type>char</type>");
				sb.Append("<Length>3</Length>");
				sb.Append("<value>" + this._Bank + "</value>");
				sb.Append("</parameter>");

				sb.Append("<parameter>");
				sb.Append("<name>CompanyDealer</name>");
				sb.Append("<type>char</type>");
				sb.Append("<Length>3</Length>");
				sb.Append("<value>" + this._CompanyDealer + "</value>");
				sb.Append("</parameter>");
            //}

                sb.Append("<parameter>");
                sb.Append("<name>LicenseNumber</name>");
                sb.Append("<type>varchar</type>");
                sb.Append("<Length>10</Length>");
                sb.Append("<value>" + this._License + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LicenseExpDate</name>");
                sb.Append("<type>Datetime</type>");
                sb.Append("<value>" + this._LicenseExpDate + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>IsLeasing</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._IsLeasing + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>ExperienceDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._ExperienceDiscount + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EmployeeDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._EmployeeDiscount + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>MiscDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._MiscDiscount + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>TotDiscount</name>");
                sb.Append("<type>Float</type>");
                sb.Append("<value>" + this._TotDiscount + "</value>");
                sb.Append("</parameter>");
            
                sb.Append("<parameter>");
                sb.Append("<name>AssistanceID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._AssistanceID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>VehicleRentalID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._VehicleRentalID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>AccidentalDeathID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._AccidentalDeathID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>AccidentalDeathPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._AccidentalDeathPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>AccidentalDeathPerson</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._AccidentalDeathPerson + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentSoundID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._EquipmentSoundID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentSoundPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this.EquipmentSoundPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentAudioID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._EquipmentAudioID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentAudioPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._EquipmentAudioPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentTapes</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._EquipmentTapes + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>EquipmentTapesPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._EquipmentTapesPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentColl</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._SpecialEquipmentColl + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentCollPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._SpecialEquipmentCollPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentComp</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._SpecialEquipmentComp + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>SpecialEquipmentCompPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._SpecialEquipmentCompPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSingleID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._UninsuredSingleID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSinglePremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._UninsuredSinglePremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSplitID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._UninsuredSplitID + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>UninsuredSplitPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._UninsuredSplitPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>IsAssistanceEmp</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._IsAssistanceEmp + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>CustomizeEquipLimit</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._CustomizeEquipLimit + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LoJack</name>");
                sb.Append("<type>bit</type>");
                sb.Append("<value>" + this._LoJack + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LojackExpDate</name>");
                sb.Append("<type>varchar</type>");
                sb.Append("<Length>20</Length>");
                sb.Append("<value>" + this._LojackExpDate + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>LoJackCertificate</name>");
                sb.Append("<type>varchar</type>");
                sb.Append("<Length>50</Length>");
                sb.Append("<value>" + this._LoJackCertificate + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>OriginalMedicalPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._OriginalMedicalPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>OriginalTowingPremium</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._OriginalTowingPremium + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>OriginalVehicleRental</name>");
                sb.Append("<type>money</type>");
                sb.Append("<value>" + this._OriginalVehicleRental + "</value>");
                sb.Append("</parameter>");

                sb.Append("<parameter>");
                sb.Append("<name>TowingID</name>");
                sb.Append("<type>int</type>");
                sb.Append("<value>" + this._TowingID + "</value>");
                sb.Append("</parameter>");

			sb.Append("</parameters>");

			xmlDoc.InnerXml = sb.ToString();

			sb = null;

			return xmlDoc;
		}

		private XmlDocument GetAutoCoverDeleteXml()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();
			
			sb.Append("<parameters>");

			sb.Append("<parameter>");
			sb.Append("<name>QuotesAutoID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._QuotesAutoId + "</value>");
			sb.Append("</parameter>");

			sb.Append("</parameters>");

			xmlDoc.InnerXml = sb.ToString();

			sb = null;

			return xmlDoc;
		}

		
		private void Insert(int UserID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				executor.BeginTrans();
				if (this._IsPolicy)
				{
					this.QuotesAutoId = executor.Insert("AddAutoPolicy", this.GetAutoCoverXml());
				}
				else
				{
					this.QuotesAutoId = executor.Insert("AddQuotesAuto", this.GetAutoCoverXml());
				}

				executor.CommitTrans();
				//this.AuditInsert(UserID);
				//this.CommitAudit();
			
				//RPR 2004-03-17
				this.Mode = (int)Enumerators.Modes.Update;
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleException(xcp);
			}
		}

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>HandleException</functionName><br/>
		/// <author>B. Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// Exception Ex
		/// <param name=""></param>
		/// <returns>void</returns>

		public void HandleException(Exception Ex)
		{
			switch(Ex.GetBaseException().GetType().FullName)
			{
				case "System.Data.SqlClient.SqlException":
					SqlException sqlException = (SqlException)Ex.GetBaseException();
				switch(sqlException.Number)
				{
					case 547:
						throw new Exception("The Auto Cover you are trying to " +
							"relate to this search fields does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"adding the Auto Cover.", Ex);
				}
				default:
					throw new Exception("An error ocurred while adding " + 
						" the Auto Cover.", Ex);
			}
		}

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>Update</functionName><br/>
		/// <author>B. Nieves</author><br/>	
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// 
		/// <param name=""></param>
		/// <returns>void</returns>

		private void Update(int UserID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				//this.AuditUpdate(UserID);
				executor.BeginTrans();

				if (this._IsPolicy)
				{
					executor.Update("UpdateAutoPolicy", this.GetAutoCoverUpdateXml());
				}
				else
				{
					executor.Update("UpdateQuotesAuto", this.GetAutoCoverUpdateXml());
				}

				executor.CommitTrans();
				//this.CommitAudit();
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleException(xcp);
			}
		}

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>Delete</functionName><br/>
		/// <author>B. Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// 
		/// <param name=""></param>
		/// <returns>void</returns>

		private void Delete(int UserID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				executor.BeginTrans();

				if (this._IsPolicy)
				{
					executor.Delete("DeleteAutoPolicy", this.GetAutoCoverDeleteXml());
				}
				else
				{
					executor.Delete("DeleteQuotesAuto", this.GetAutoCoverDeleteXml());
				}

				executor.CommitTrans();
				//this.AuditDelete(UserID);
				//this.CommitAudit();
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleException(xcp);
			}
		}

		/// <summary>
		///		
		///	</summary>
		/// <remarks>
		/// <functionName>VerifyPrincipal</functionName><br/>
		/// <author>B. Nieves</author><br/>
		/// <modifiedBy date=""></modifiedBy><br/>
		/// </remarks>          
		/// 
		/// <param name=""></param>
		/// <returns>void</returns>

		private void VerifyPrincipal()
		{
			///TODO: DO THIS METHOD: AutoCover.VerifyPrincipal()
		
		}

		private void FillProperties(DataRow DR)
		{
			// COVER INFORMATION
			this._QuotesAutoId = (int)DR["QuotesAutoID"];
			this._QuotesId = (int)DR["QuotesID"];
			this._PolicySubClassId = (int)DR["PolicySubClassID"];
			this._HomeCity = (int)DR["HomeCity"];
			this._WorkCity = (int)DR["WorkCity"];
			this._VehicleAge = (int)DR["VehicleAge"];
			this._VehicleMake = (int)DR["VehicleMakeID"];
			this._VehicleModel = (int)DR["VehicleModelID"];
			this._VehicleYear = (int)DR["VehicleYearID"];
			this._VIN = DR["VIN"].ToString();
			this._Plate = DR["Plate"].ToString();
			this._NewUse = (int)DR["NewUse"];
			this._VehicleClass = DR["VehicleClassID"].ToString();
			this._Territory = (int)DR["TerritoryID"];
			this._AlarmType = (int)DR["AlarmTypeID"];
			this._Cost = decimal.Parse(DR["Cost"].ToString());
			this._PurchaseDate = (DR["PurchaseDate"]!= System.DBNull.Value)? ((DateTime)DR["PurchaseDate"]).ToString("d"):"";
			this._ActualValue = decimal.Parse(DR["ActualValue"].ToString());
			this._Depreciation1stYear = decimal.Parse(DR["Depreciation1stYear"].ToString());
			this._DepreciationAllYear = decimal.Parse(DR["DepreciationAllYear"].ToString());
			this._ComprehensiveDeductible = (int)DR["ComprehensiveDeductibleID"];
			this._CollisionDeductible = (int)DR["CollisionDeductibleID"];
			this._DiscountCompColl = decimal.Parse(DR["DiscountCompColl"].ToString());
			this._BodilyInjuryLimit= (int)DR["BodilyInjuryLimitID"];
			this._PropertyDamageLimit = (int)DR["PropertyDamageLimitID"];
			this._CombinedSingleLimit = (int)DR["CombinedSingleLimitID"];
			this._DiscountBIPD = decimal.Parse(DR["DiscountBIPD"].ToString());
			this._MedicalLimit = (int)DR["MedicalLimitID"];
			this._AssistancePremium = decimal.Parse(DR["AssistancePremium"].ToString());
			this._TowingPremium = decimal.Parse(DR["TowingPremium"].ToString());
			this._VehicleRental = decimal.Parse(DR["VehicleRental"].ToString());
			this._LeaseLoanGapId = (int)DR["LeaseLoanGapID"];
			this._SeatBelt = (int)DR["SeatBelt"];
			this._PersonalAccidentRider = (int)DR["PersonalAccidentRider"];
			this._Factor = decimal.Parse(DR["Factor"].ToString());
            this._Bank = DR["Bank"].ToString();
            this._CompanyDealer = DR["CompanyDealer"].ToString();
            this.OvrrPremium = (DR["OvrrPremium"] != System.DBNull.Value) ? ((bool)DR["OvrrPremium"]) : false;
            this._License = DR["LicenseNumber"].ToString();
            this._LicenseExpDate = (DR["LicenseExpDate"] != System.DBNull.Value) ? ((DateTime)DR["LicenseExpDate"]).ToString("d") : "";
            this._IsLeasing = (DR["IsLeasing"] != System.DBNull.Value) ? ((bool)DR["IsLeasing"]) : false;
            this._ExperienceDiscount = double.Parse(DR["ExperienceDiscount"].ToString());
            this._EmployeeDiscount = double.Parse(DR["EmployeeDiscount"].ToString());
            this._MiscDiscount = double.Parse(DR["MiscDiscount"].ToString());
            this._TotDiscount = double.Parse(DR["TotDiscount"].ToString());
            this._AssistanceID =(int)DR["AssistanceID"];
            this._VehicleRentalID = (int)DR["VehicleRentalID"];
            this._AccidentalDeathID = (int)DR["AccidentalDeathID"];
            this._AccidentalDeathPremium =decimal.Parse(DR["AccidentalDeathPremium"].ToString());
            this._AccidentalDeathPerson = (int)DR["AccidentalDeathPerson"];
            this._EquipmentSoundID = (int)DR["EquipmentSoundID"];
            this._EquipmentSoundPremium =decimal.Parse(DR["EquipmentSoundPremium"].ToString());
            this._EquipmentAudioID = (int)DR["EquipmentAudioID"];
            this._EquipmentAudioPremium =decimal.Parse(DR["EquipmentAudioPremium"].ToString());
            this._EquipmentTapes = (DR["EquipmentTapes"] != System.DBNull.Value) ? ((bool)DR["EquipmentTapes"]) : false;
            this._EquipmentTapesPremium = decimal.Parse(DR["EquipmentTapesPremium"].ToString());
            this._SpecialEquipmentColl = (DR["SpecialEquipmentColl"] != System.DBNull.Value) ? ((bool)DR["SpecialEquipmentColl"]) : false;
            this._SpecialEquipmentCollPremium =decimal.Parse(DR["SpecialEquipmentCollPremium"].ToString());
            this._SpecialEquipmentComp = (DR["SpecialEquipmentComp"] != System.DBNull.Value) ? ((bool)DR["SpecialEquipmentComp"]) : false;
            this._SpecialEquipmentCompPremium =decimal.Parse(DR["SpecialEquipmentCompPremium"].ToString());
            this._UninsuredSingleID = (int)DR["UninsuredSingleID"];
            this._UninsuredSinglePremium = decimal.Parse(DR["UninsuredSinglePremium"].ToString());
            this._UninsuredSplitID = (int)DR["UninsuredSplitID"];
            this._UninsuredSplitPremium = decimal.Parse(DR["UninsuredSplitPremium"].ToString());
            this._IsAssistanceEmp = (DR["IsAssistanceEmp"] != System.DBNull.Value) ? ((bool)DR["IsAssistanceEmp"]) : false;
            this._CustomizeEquipLimit = (int)DR["CustomizeEquipLimit"];
            this._LoJack = (DR["LoJack"] != System.DBNull.Value) ? ((bool)DR["LoJack"]) : false;
            this._LojackExpDate = DR["LojackExpDate"].ToString();
            this._LoJackCertificate = DR["LoJackCertificate"].ToString();
            this._OriginalMedicalPremium = decimal.Parse(DR["OriginalMedicalPremium"].ToString());
            this._OriginalTowingPremium = decimal.Parse(DR["OriginalTowingPremium"].ToString());
            this._OriginalVehicleRental = decimal.Parse(DR["OriginalVehicleRental"].ToString());
            this._TowingID = (int)DR["TowingID"];
			if (this._IsPolicy)
			{
				this._Bank			= DR["Bank"].ToString();
				this._CompanyDealer = DR["CompanyDealer"].ToString();
				this.AutoPolicyType = GetAutoPolicyType(this.PolicySubClassId);
			}			
		}

		private string GetAutoPolicyType(int policySubClass)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>PolicySubClassID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + policySubClass + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt;
	
			dt = exec.GetQuery("GetAutoPolicyType",xmlDoc);

			if (dt.Rows.Count !=0)
			{
				return dt.Rows[0]["AutoPolicyType"].ToString().Trim();
			}
			else
			{
				return "";
			}			
		}

		private static DataTable GetQuotesAutoForQuote(int QuotesID, bool ispolicy)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>QuotesID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + QuotesID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt;
			if (ispolicy)
			{
				dt = exec.GetQuery("GetAutoPolicyForQuote",xmlDoc);
			}
			else
			{
				dt = exec.GetQuery("GetQuotesAutoForQuote",xmlDoc);
			}
			return dt;
		}

		private void DeleteQuotesAutoCoversAndDrivers()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>QuotesAutoID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this.QuotesAutoId + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			if(_IsPolicy)
			{
				exec.Delete("DeleteAutoAutoCoversAndDrivers",xmlDoc);
			}
			else
			{
				exec.Delete("DeleteQuotesAutoCoversAndDrivers",xmlDoc);
			}
		}

		private decimal GetBreakdownTotal(Enumerators.Premiums Type)
		{
			CoverBreakdown Srch = new CoverBreakdown();
			decimal SubTotal = 0;
			Srch.Type = (int)Type;
			if (Breakdown.Contains(Srch))
			{
				CoverBreakdown CB = (CoverBreakdown)Breakdown[Breakdown.IndexOf(Srch)];
				for (int i = 0; i < CB.Breakdown.Count; i++)
				{
					object obj = CB.Breakdown.GetByIndex(i);
					if (obj is decimal)
					{
						//SubTotal += (decimal)obj;
						SubTotal += System.Math.Round((decimal)obj,0);
					}
					else
					{
						try
						{
							//SubTotal += decimal.Parse(obj.ToString());
							SubTotal += System.Math.Round(decimal.Parse(obj.ToString()),0);
						}
						catch
						{
							
						}
					}
				}
			}
			return SubTotal;
		}

		#region History

		private void History(int mode, int userID)
		{ 
			Audit.History history = new Audit.History();
			
			if(mode == 2)
			{				
				history.BuildNotesForHistory("ActualValue",_oldAutoCover.ActualValue.ToString("###,###"),this.ActualValue.ToString("###,###"),mode);
				history.BuildNotesForHistory("HomeCity",_oldAutoCover.HomeCity.ToString(),this.HomeCity.ToString(),mode);
				history.BuildNotesForHistory("WorkCity",_oldAutoCover.WorkCity.ToString(),this.WorkCity.ToString(),mode);
				history.BuildNotesForHistory("VehicleAge",_oldAutoCover.VehicleAge.ToString(),this.VehicleAge.ToString(),mode);
				history.BuildNotesForHistory("VehicleMake",
					LookupTables.LookupTables.GetDescription("VehicleMake",_oldAutoCover.VehicleMake.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleMake",this.VehicleMake.ToString()),
					mode);
				history.BuildNotesForHistory("VehicleModel",
					LookupTables.LookupTables.GetDescription("VehicleModel",_oldAutoCover.VehicleModel.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleModel",this.VehicleModel.ToString()),
					mode);
				history.BuildNotesForHistory("VehicleYear",_oldAutoCover.VehicleYear.ToString(),this.VehicleYear.ToString(),mode);
				history.BuildNotesForHistory("VIN",_oldAutoCover.VIN.ToString(),this.VIN.ToString(),mode);
				history.BuildNotesForHistory("Plate",_oldAutoCover.Plate.ToString(),this.Plate.ToString(),mode);
				history.BuildNotesForHistory("NewUse",_oldAutoCover.NewUse.ToString(),this.NewUse.ToString(),mode);
				history.BuildNotesForHistory("VehicleUse",_oldAutoCover.VehicleClass.ToString(),this.VehicleClass.ToString(),mode);
				history.BuildNotesForHistory("Territory",
					LookupTables.LookupTables.GetDescription("Territory",_oldAutoCover.Territory.ToString()),
					LookupTables.LookupTables.GetDescription("Territory",this.Territory.ToString()),
					mode);
				history.BuildNotesForHistory("AlarmType",
					LookupTables.LookupTables.GetDescription("AlarmType",_oldAutoCover.AlarmType.ToString()),
					LookupTables.LookupTables.GetDescription("AlarmType",this.AlarmType.ToString()),
					mode);
				history.BuildNotesForHistory("Cost",_oldAutoCover.Cost.ToString("###,###"),this.Cost.ToString("###,###"),mode);
				history.BuildNotesForHistory("PurchaseDate",_oldAutoCover.PurchaseDate.ToString(),this.PurchaseDate.ToString(),mode);
				history.BuildNotesForHistory("Depreciation1stYear",_oldAutoCover.Depreciation1stYear.ToString(),this.Depreciation1stYear.ToString(),mode);
				history.BuildNotesForHistory("DepreciationAllYear",_oldAutoCover.DepreciationAllYear.ToString(),this.DepreciationAllYear.ToString(),mode);
				history.BuildNotesForHistory("ComprehensiveDeductible",
					LookupTables.LookupTables.GetDescription("ComprehensiveDeductible",_oldAutoCover.ComprehensiveDeductible.ToString()),
					LookupTables.LookupTables.GetDescription("ComprehensiveDeductible",this.ComprehensiveDeductible.ToString()),
					mode);
				history.BuildNotesForHistory("CollisionDeductible",
					LookupTables.LookupTables.GetDescription("CollisionDeductible",_oldAutoCover.CollisionDeductible.ToString()),
					LookupTables.LookupTables.GetDescription("CollisionDeductible",this.CollisionDeductible.ToString()),
					mode);
				history.BuildNotesForHistory("DiscountCompColl",_oldAutoCover.DiscountCompColl.ToString(),this.DiscountCompColl.ToString(),mode);
				history.BuildNotesForHistory("BodilyInjuryLimit",
					LookupTables.LookupTables.GetDescription("BodilyInjuryLimit",_oldAutoCover.BodilyInjuryLimit.ToString()),
					LookupTables.LookupTables.GetDescription("BodilyInjuryLimit",this.BodilyInjuryLimit.ToString()),
					mode);
				history.BuildNotesForHistory("PropertyDamageLimit",
					LookupTables.LookupTables.GetDescription("PropertyDamageLimit",_oldAutoCover.PropertyDamageLimit.ToString()),
					LookupTables.LookupTables.GetDescription("PropertyDamageLimit",this.PropertyDamageLimit.ToString()),
					mode);
				history.BuildNotesForHistory("CombinedSingleLimit",
					LookupTables.LookupTables.GetDescription("CombinedSingleLimit",_oldAutoCover.CombinedSingleLimit.ToString()),
					LookupTables.LookupTables.GetDescription("CombinedSingleLimit",this.CombinedSingleLimit.ToString()),
					mode);
				history.BuildNotesForHistory("DiscountBIPD",_oldAutoCover.DiscountBIPD.ToString(),this.DiscountBIPD.ToString(),mode);
				history.BuildNotesForHistory("MedicalLimit",
					LookupTables.LookupTables.GetDescription("MedicalLimit",_oldAutoCover.MedicalLimit.ToString()),
					LookupTables.LookupTables.GetDescription("MedicalLimit",this.MedicalLimit.ToString()),
					mode);
                history.BuildNotesForHistory("AssistancePremium", _oldAutoCover.AssistancePremium.ToString("###,###"), this.AssistancePremium.ToString("###,###"), mode);
				history.BuildNotesForHistory("TowingPremium",_oldAutoCover.TowingPremium.ToString(),this.TowingPremium.ToString(),mode);
				history.BuildNotesForHistory("VehicleRental",_oldAutoCover.VehicleRental.ToString("###,###"),this.VehicleRental.ToString("###,###"),mode);
				history.BuildNotesForHistory("LeaseLoanGapId",
					LookupTables.LookupTables.GetDescription("LeaseLoanGap",_oldAutoCover.LeaseLoanGapId.ToString()),
					LookupTables.LookupTables.GetDescription("LeaseLoanGap",this.LeaseLoanGapId.ToString()),
					mode);
				history.BuildNotesForHistory("PersonalAccidentRider",
					LookupTables.LookupTables.GetDescription("PersonalAccidentRider",_oldAutoCover.PersonalAccidentRider.ToString()),
					LookupTables.LookupTables.GetDescription("PersonalAccidentRider",this.PersonalAccidentRider.ToString()),
					mode);
                history.BuildNotesForHistory("OvrrPremium", _oldAutoCover.OvrrPremium.ToString(), this.OvrrPremium.ToString(), mode);

                history.BuildNotesForHistory("LicenseNumber", _oldAutoCover.License.ToString(), this.License.ToString(), mode);
                history.BuildNotesForHistory("LicenseExpDate", _oldAutoCover.LicenseExpDate.ToString(), this.LicenseExpDate.ToString(), mode);
                history.BuildNotesForHistory("IsLeasing", _oldAutoCover.IsLeasing.ToString(), this.IsLeasing.ToString(), mode);

                history.BuildNotesForHistory("Road Assist",
                    LookupTables.LookupTables.GetDescription("RoadAssist", _oldAutoCover.AssistanceID.ToString()),
                    LookupTables.LookupTables.GetDescription("RoadAssist", this.AssistanceID.ToString()),
                    mode);

                history.BuildNotesForHistory("Transportation Expenses",
                   LookupTables.LookupTables.GetDescription("VehicleRental", _oldAutoCover.VehicleRentalID.ToString()),
                   LookupTables.LookupTables.GetDescription("VehicleRental", this.VehicleRentalID.ToString()),
                   mode);

                history.BuildNotesForHistory("Accidental Death",
                 LookupTables.LookupTables.GetDescription("AccidentalDeath", _oldAutoCover.AccidentalDeathID.ToString()),
                 LookupTables.LookupTables.GetDescription("AccidentalDeath", this.AccidentalDeathID.ToString()),
                 mode);

                history.BuildNotesForHistory("Accidental Death Premium", _oldAutoCover.AccidentalDeathPremium.ToString("###,###"), this.AccidentalDeathPremium.ToString("###,###"), mode);
                history.BuildNotesForHistory("Accidental Death Person", _oldAutoCover.AccidentalDeathPerson.ToString(), this.AccidentalDeathPerson.ToString(), mode);

                history.BuildNotesForHistory("Equipment Sound",
                LookupTables.LookupTables.GetDescription("EquitmentSonido", _oldAutoCover.EquipmentSoundID.ToString()),
                LookupTables.LookupTables.GetDescription("EquitmentSonido", this.EquipmentSoundID.ToString()),
                mode);

                history.BuildNotesForHistory("Equipment Sound Premium", _oldAutoCover.EquipmentSoundPremium.ToString("###,###"), this.EquipmentSoundPremium.ToString("###,###"), mode);

                history.BuildNotesForHistory("Equipment Audio",
                LookupTables.LookupTables.GetDescription("EquitmentAudio", _oldAutoCover.EquipmentAudioID.ToString()),
                LookupTables.LookupTables.GetDescription("EquitmentAudio", this.EquipmentAudioID.ToString()),
                mode);

                history.BuildNotesForHistory("Equipment Audio Premium", _oldAutoCover.EquipmentAudioPremium.ToString("###,###"), this.EquipmentAudioPremium.ToString("###,###"), mode);
                history.BuildNotesForHistory("Equipment Tapes", _oldAutoCover.EquipmentTapes.ToString(), this.EquipmentTapes.ToString(), mode);
                history.BuildNotesForHistory("Equipment Tapes Premium", _oldAutoCover.EquipmentTapesPremium.ToString("###,###"), this.EquipmentTapesPremium.ToString("###,###"), mode);
                history.BuildNotesForHistory("Special Equipment Coll", _oldAutoCover.SpecialEquipmentColl.ToString(), this.SpecialEquipmentColl.ToString(), mode);
                history.BuildNotesForHistory("Special Equipment Coll Premium", _oldAutoCover.SpecialEquipmentCollPremium.ToString("###,###"), this.SpecialEquipmentCollPremium.ToString("###,###"), mode);
                history.BuildNotesForHistory("Special Equipment Comp", _oldAutoCover.SpecialEquipmentComp.ToString(), this.SpecialEquipmentComp.ToString(), mode);
                history.BuildNotesForHistory("Special Equipment Comp Premium", _oldAutoCover.SpecialEquipmentCompPremium.ToString("###,###"), this.SpecialEquipmentCompPremium.ToString("###,###"), mode);
                
                history.BuildNotesForHistory("Uninsured Single",
               LookupTables.LookupTables.GetDescription("UninsuredSingle", _oldAutoCover.UninsuredSingleID.ToString()),
               LookupTables.LookupTables.GetDescription("UninsuredSingle", this.UninsuredSingleID.ToString()),
               mode);

                history.BuildNotesForHistory("Uninsured Single Premium", _oldAutoCover.UninsuredSinglePremium.ToString("###,###"), this.UninsuredSinglePremium.ToString("###,###"), mode);
                
                history.BuildNotesForHistory("Uninsured Split",
               LookupTables.LookupTables.GetDescription("UninsuredSplit", _oldAutoCover.UninsuredSplitID.ToString()),
               LookupTables.LookupTables.GetDescription("UninsuredSplit", this.UninsuredSplitID.ToString()),
               mode);

                history.BuildNotesForHistory("Uninsured Spit Premium", _oldAutoCover.UninsuredSplitPremium.ToString("###,###"), this.UninsuredSplitPremium.ToString("###,###"), mode);
                history.BuildNotesForHistory("Customize Equip Limit", _oldAutoCover.CustomizeEquipLimit.ToString(), this.CustomizeEquipLimit.ToString(), mode);

                history.BuildNotesForHistory("Lo Jack", _oldAutoCover.LoJack.ToString(), this.LoJack.ToString(), mode);
                history.BuildNotesForHistory("Lo Jack Exp Date", _oldAutoCover.LojackExpDate.ToString(), this.LojackExpDate.ToString(), mode);
                history.BuildNotesForHistory("Lo Jack Certificate", _oldAutoCover.LoJackCertificate.ToString(), this.LoJackCertificate.ToString(), mode);           

                // Campos de Policy
				if (this._IsPolicy)
				{
					history.BuildNotesForHistory("Bank",
						LookupTables.LookupTables.GetDescription("Bank",_oldAutoCover.Bank.ToString()),
						LookupTables.LookupTables.GetDescription("Bank",this.Bank.ToString()),
						mode);
					history.BuildNotesForHistory("CompanyDealer",
						LookupTables.LookupTables.GetDescription("CompanyDealer",_oldAutoCover.CompanyDealer.ToString()),
						LookupTables.LookupTables.GetDescription("CompanyDealer",this.CompanyDealer.ToString()),
						mode);
				}// Terminan Campos Policy
							
				history.Actions = "EDIT";
			}

			if (this._IsPolicy)
				history.KeyID = this._QuotesId.ToString();
			else
				history.KeyID = this._TCIDForAutoCover.ToString();

			if (this._IsPolicy)
				history.Subject = "AUTOPERSONALPOLICY";			
			else
				history.Subject = "QUOTE";			

			history.UsersID =  userID;
			history.GetSaveHistory();
		}

		#endregion

#endregion

	}
	// END CLASS DEFINITION AutoCover

}