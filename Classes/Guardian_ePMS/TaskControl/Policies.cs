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
	public class Policies:Policy
	{
		public Policies()
		{
			this.AgentList		  = Policy.GetAgentListByPolicyClassID(0);
			//this.SupplierList	  = Policy.GetSupplierListByPolicyClassID(0);
			this.DepartmentID     = 0;    
			this.PolicyClassID	  = 0;
			this.PolicyType       = "";
			this.InsuranceCompany = "";
			this.Agency           = "";
			this.Agent            = "";
			this.SupplierID		  = "";
			this.Bank             = "000";
			this.Dealer			  = "000";
			this.CompanyDealer	  = "000";
			this.Status           = "Inforce";
			this.TaskStatusID     = int.Parse(LookupTables.LookupTables.GetID("TaskStatus","Open"));
			this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType","Policies"));
			// Para el History
			this._mode =(int) TaskControlMode.ADD;
		}

		#region Variable

		private Policies  oldPolices = null;
		//private Policy _Policy = new Policy();
		private DataTable _dtPolices ;
		private PolicyDetailcs _PolicyDetailcs= null;
		private int _PoliciesID			= 0;
		private int _Milleages			= 0;
		private double _Cost			= 0.00;
		private double _FinanceAmt   	= 0.00;
		private string _VehicleClass	= "";
		private int _VehicleMakeID      = 0;
		private int _VehicleModelID     = 0;
		private int _VehicleYearID		= 0;
		private string _VIN				= "";
		private string _Plate           = "";
		private string _PurchaseDate    = "";
		private int _NewUse				= 0;
		private string _Comments		= "";
		private double _Balance			= 0.00;
		private bool _PrestamoArrenda   = false;
		private string _InsuranceCompanyPrimaria = "000";
		private string _NumeroPolizaPrimaria = "";
		private int _CoveragePlan		= 0;
		private int _Miles				= 0;
		private bool _Diesel				= false;
		private bool _WD4				= false;
		private bool _Turbo				= false;
		private bool _CommercialUse		= false;
		private string _VehicleCode		= "";
        private string _EffDateCompany = "";
        private string _Class = "";
        private double _LossFund = 0.00;
        private double _OverHead = 0.00;
        private double _BankFee = 0.00;
        private double _Profit = 0.00;
        private double _Concurso = 0.00;
        private double _DealerCost = 0.00;
        private double _DealerProfit = 0.00;
        private double _CanReserve = 0.00;
        private double _DealerNet = 0.00;
        private int _VehicleServiceContractQuoteID = 0;
        private int _CompulsoryInsuranceQuoteID = 0;

        private int _EtchRateID = 0;
        private int _UltProtRateID = 0;

		private int _mode				= (int) TaskControlMode.CLEAR;

        private DataTable _RoadAssistCollection = null;
        private string _LicExpDate = "";

	
		#endregion

		#region Properties

		public PolicyDetailcs PolicyDetailcs
		{
			get
			{
				if(this._PolicyDetailcs == null)
					this._PolicyDetailcs = new PolicyDetailcs();
				return (this._PolicyDetailcs);
			}
			set
			{
				this._PolicyDetailcs = value;
			}
		}

		public int PoliciesID
		{
			get
			{
				return this._PoliciesID;
			}
			set 
			{
				this._PoliciesID = value;
			}
		}

		public int Milleages
		{
			get
			{
				return this._Milleages;
			}
			set 
			{
				this._Milleages = value;
			}
		}

		public double Cost
		{
			get
			{
				return this._Cost;
			}
			set 
			{
				this._Cost = value;
			}
		}
		
		public double FinanceAmt
		{
			get
			{
				return this._FinanceAmt;
			}
			set 
			{
				this._FinanceAmt = value;
			}
		}

		public string VehicleClass
		{
			get
			{
				return this._VehicleClass;
			}
			set 
			{
				this._VehicleClass = value;
			}
		}

		public int VehicleMakeID
		{
			get
			{
				return this._VehicleMakeID;
			}
			set 
			{
				this._VehicleMakeID = value;
			}
		}

		public int VehicleModelID
		{
			get
			{
				return this._VehicleModelID;
			}
			set 
			{
				this._VehicleModelID = value;
			}
		}

		public int VehicleYearID
		{
			get
			{
				return this._VehicleYearID;
			}
			set 
			{
				this._VehicleYearID = value;
			}
		}

		public string VIN
		{
			get
			{
				return this._VIN;
			}
			set 
			{
				this._VIN = value;
			}
		}

		public string Plate
		{
			get
			{
				return this._Plate;
			}
			set 
			{
				this._Plate = value;
			}
		}

		public string PurchaseDate
		{
			get
			{
				return this._PurchaseDate;
			}
			set 
			{
				this._PurchaseDate = value;
			}
		}

		public int NewUse
		{
			get
			{
				return this._NewUse;
			}
			set 
			{
				this._NewUse = value;
			}
		}

		public string Comments
		{
			get
			{
				return this._Comments;
			}
			set 
			{
				this._Comments = value;
			}
		}

		public double Balance
		{
			get
			{
				return this._Balance;
			}
			set 
			{
				this._Balance = value;
			}
		}

		public bool PrestamoArrenda   
		{
			get
			{
				return this._PrestamoArrenda;
			}
			set 
			{
				this._PrestamoArrenda = value;
			}
		}

		public string InsuranceCompanyPrimaria 
		{
			get
			{
				return this._InsuranceCompanyPrimaria;
			}
			set 
			{
				this._InsuranceCompanyPrimaria = value;
			}
		}

		public string NumeroPolizaPrimaria 
		{
			get
			{
				return this._NumeroPolizaPrimaria;
			}
			set 
			{
				this._NumeroPolizaPrimaria = value;
			}
		}

		public int CoveragePlan		
		{
			get
			{
				return this._CoveragePlan;
			}
			set 
			{
				this._CoveragePlan = value;
			}
		}

		public int Miles		
		{
			get
			{
				return this._Miles;
			}
			set 
			{
				this._Miles = value;
			}
		}

		public bool Diesel		
		{
			get
			{
				return this._Diesel;
			}
			set 
			{
				this._Diesel = value;
			}
		}

		public bool WD4		
		{
			get
			{
				return this._WD4;
			}
			set 
			{
				this._WD4 = value;
			}
		}

		public bool Turbo		
		{
			get
			{
				return this._Turbo;
			}
			set 
			{
				this._Turbo = value;
			}
		}

		public bool CommercialUse		
		{
			get
			{
				return this._CommercialUse;
			}
			set 
			{
				this._CommercialUse = value;
			}
		}

		public string VehicleCode
		{
			get
			{
				return this._VehicleCode;
			}
			set 
			{
				this._VehicleCode = value;
			}
		}

        public string EffDateCompany
		{
			get
			{
                return this._EffDateCompany;
			}
			set 
			{
                this._EffDateCompany = value;
			}
		}

        public string Class
        {
            get
            {
                return this._Class;
            }
            set
            {
                this._Class = value;
            }
        }

        public double LossFund
        {
            get
            {
                return this._LossFund;
            }
            set
            {
                this._LossFund = value;
            }
        }

        public double OverHead
        {
            get
            {
                return this._OverHead;
            }
            set
            {
                this._OverHead = value;
            }
        }

        public double BankFee
        {
            get
            {
                return this._BankFee;
            }
            set
            {
                this._BankFee = value;
            }
        }

        public double Profit
        {
            get
            {
                return this._Profit;
            }
            set
            {
                this._Profit = value;
            }
        }

        public double Concurso
        {
            get
            {
                return this._Concurso;
            }
            set
            {
                this._Concurso = value;
            }
        }

        public double DealerCost
        {
            get
            {
                return this._DealerCost;
            }
            set
            {
                this._DealerCost = value;
            }
        }

        public double DealerProfit
        {
            get
            {
                return this._DealerProfit;
            }
            set
            {
                this._DealerProfit = value;
            }
        }

        public double CanReserve
        {
            get
            {
                return this._CanReserve;
            }
            set
            {
                this._CanReserve = value;
            }
        }

        public double DealerNet
        {
            get
            {
                return this._DealerNet;
            }
            set
            {
                this._DealerNet = value;
            }
        }

        public int VehicleServiceContractQuoteID
        {
            get
            {
                return this._VehicleServiceContractQuoteID;
            }
            set
            {
                this._VehicleServiceContractQuoteID = value;
            }
        }

        public int CompulsoryInsuranceQuoteID
        {
            get
            {
                return this._CompulsoryInsuranceQuoteID;
            }
            set
            {
                this._CompulsoryInsuranceQuoteID = value;
            }
        }

        public int EtchRateID
        {
            get
            {
                return this._EtchRateID;
            }
            set
            {
                this._EtchRateID = value;
            }
        }

        public int UltProtRateID
        {
            get
            {
                return this._UltProtRateID;
            }
            set
            {
                this._UltProtRateID = value;
            }
        }

        public string LicExpDate
        {
            get
            {
                return this._LicExpDate;
            }
            set
            {
                this._LicExpDate = value;
            }
        }
        
		#endregion

		#region Public Methods

		// Añadi
		public void SavePolicies(int UserID)
		{
			this.SavePol(UserID);
		}
		//

		public override void SavePol(int UserID)
		{
			this._mode		= (int) this.Mode;  // Se le asigna el mode de taskControl.
			this.PolicyMode = (int) this.Mode;  // Se le asigna el mode de taskControl.

			this.Validate();
			base.ValidatePolicy();
			

			// Se utiliza para el History
			//if (this._mode ==2)
			if (this._mode ==2)
				oldPolices = (Policies) Policies.GetTaskControlByTaskControlID(this.TaskControlID,UserID);
			
			//Si el usuario cambio la prima manualmente, no debe calcular la misma.
			if (this.TotalPremium == 0)
			{
				//CalculatePremium();
			}

			if (this.Customer.CustomerNo.Trim() == "")
				this.Customer.Mode = 1;
			else
				this.Customer.Mode = 2;

			this.Customer.IsBusiness = false;
			this.Customer.Save(UserID);

			this.CustomerNo = this.Customer.CustomerNo;
			this.ProspectID = this.Customer.ProspectID;

			base.Save();
			base.SavePol(UserID);	// Validate and Save Policy
            
			SavePoliciesPolicies(UserID);  // Save Policies
			this.PolicyDetailcs.SavePolicyDetail(UserID,this.TaskControlID);

            this.SaveAutoRoadAssist(UserID, this.TaskControlID);

			this._mode = (int) TaskControlMode.UPDATE;
			this.Mode = (int) TaskControlMode.CLEAR;
			//FillProperties(this);
		}

		public override void Validate()
		{
			string errorMessage = String.Empty;

            if (this.PolicyClassID != 9 && this.PolicyClassID != 1 && this.PolicyClassID != 13 && this.PolicyClassID != 15 && this.PolicyClassID != 16 && this.PolicyClassID != 17 && this.PolicyClassID != 19)//No aplica para Auto Gap,Etch y para VSC y OSO, QCertified, Ult. Protector, AssistenciaCarr.
			{
				if (this.PolicyNo == "")
					errorMessage = "Policy No. is missing or wrong.";
			}
            else
                if (this.PolicyClassID == 9 || this.PolicyClassID == 1 || this.PolicyClassID == 13 || this.PolicyClassID == 16 || this.PolicyClassID == 17 || this.PolicyClassID == 19)//Aplica para Auto Gap, Etch y para VSC, QCertified, UltProt., AssistenciaCarr.
                {
                    if (this.Term.ToString().Trim() == "0" || this.Term.ToString().Trim() == "")
                        errorMessage = "Term is missing or wrong.";
                    else
                        if (this.PolicyNo == "" && this.AutoAssignPolicy == false)
                            errorMessage = "Policy No./Contract No. is missing or wrong.";
                        else
                            if (this.Mode == 1 && this.PolicyClassID != 19) // ASISTENCIA CARR.
                            {
                                string result = CheckVINByPolicyClass(this.VIN, this.PolicyClassID);
                                if (result.Trim() != "")
                                    errorMessage = result;
                            }
                }

            if (errorMessage.Trim() == "")
            {
                if (this.EffectiveDate == "")
                    errorMessage = "Effective Date is missing or wrong.";
                else
                    if (this.PolicyClassID == 0)
                        errorMessage = "Line of Business is missing or wrong.";
                    else
                        if (this.Customer.FirstName == "")
                            errorMessage = "First Name is missing or wrong.";
                        else
                            if (this.Customer.LastName1 == "")
                                errorMessage = "Last Name is missing or wrong.";
                            else
                                if (this.OriginatedAt == 0)
                                    errorMessage = "Originated is missing.";
                                else
                                    if (this.TotalPremium == 0)
                                        errorMessage = "TotalPremium must be greater than 0.";
                                    else
                                        if (this.PolicyClassID == 1 || this.PolicyClassID == 14 || this.PolicyClassID == 16) //VSC, DriverPlus, QCertified
                                        {
                                            if (this.Customer.City.Trim() == "")
                                                errorMessage = "The City is missing or wrong.";
                                            else
                                                if (this.Customer.Address1.Trim() == "")
                                                    errorMessage = "The Address 1 is missing or wrong.";
                                                else
                                                    if (this.VehicleMakeID == 0)
                                                        errorMessage = "Vehicle Make is missing or wrong.";
                                                    else
                                                        if (this.VehicleModelID == 0)
                                                            errorMessage = "Vehicle Model is missing or wrong.";
                                                        else
                                                            if (this.VehicleYearID == 0)
                                                                errorMessage = "Vehicle Year is missing or wrong.";
                                                            else
                                                                if (this.NewUse == 0)
                                                                    errorMessage = "New / Used is missing or wrong.";
                                                                else
                                                                    if (this.VIN.Trim() == "")
                                                                        errorMessage = "The VIN is missing or wrong.";
                                                                    else
                                                                        if (this.VIN.Length < 17)
                                                                            errorMessage = "The VIN must be 17 digits.";
                                                                        else
                                                                            if (this.AutoAssignPolicy == false) 
                                                                                if (this.PolicyClassID == 1) // VSC
                                                                                {
                                                                                    if (this.InsuranceCompany == "002" && this.PolicyNo.Length < 7)//Debe ser 7 o mas.
                                                                                        errorMessage = "Contract No. must be equal or greater than 7 digit.  Format (PR#####)";
                                                                                    else if (this.InsuranceCompany == "003" && this.PolicyNo.Length < 8)
                                                                                        errorMessage = "Contract No. must be equal or greater than 8 digit.  Format (PR######)";

                                                                                    if (this.Mode == 1)
                                                                                    {
                                                                                        if (CheckPolicyNo(this.PolicyType, this.PolicyNo, this.Certificate, this.Suffix))
                                                                                            errorMessage = "This contract number is already exist in our database.";
                                                                                    }
                                                                                }
                                                                                else if (this.PolicyClassID == 16) // QCERT
                                                                                {
                                                                                    if (this.InsuranceCompany == "002" && this.PolicyNo.Length < 7)//Debe ser 7 o mas.
                                                                                        errorMessage = "Contract No. must be equal or greater than 7 digit.";
                                                                                    else if (this.InsuranceCompany == "003" && this.PolicyNo.Length < 8)
                                                                                        errorMessage = "Contract No. must be equal or greater than 8 digit.";
                                                                                }
                                                                            else
                                                                                 if (this.Suffix == "00" && this.PolicyClassID != 14)
                                                                                 {
                                                                                                if (this.Mode == 1 && CheckContactNoByDealer(this.InsuranceCompany, this.CompanyDealer, this.PolicyNo))
                                                                                                {
                                                                                                    errorMessage = "This contract number not belong to this dealer.";
                                                                                                }
                                                                                                else
                                                                                                    if (this.Mode == 1)
                                                                                                    {
                                                                                                        if (this.PolicyClassID == 13) //Etch
                                                                                                        {
                                                                                                            if (CheckPolicyNoEtch(this.PolicyType, this.PolicyNo, this.Certificate, this.Suffix, this.Term.ToString(), this.InsuranceCompany.ToString()))
                                                                                                                errorMessage = "This contract number is already exist in our database.";
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (CheckPolicyNo(this.PolicyType, this.PolicyNo, this.Certificate, this.Suffix))
                                                                                                                errorMessage = "This contract number is already exist in our database.";
                                                                                                        }
                                                                                                    }
                                                                                            }
                                                                                   }
                                                                            if (this.PolicyClassID == 14)
                                                                            {
                                                                                //if (this.PolicyNo.Trim().Substring(0, 2) != "CT")
                                                                                //    this.PolicyNo = "CT" + this.PolicyNo.Trim();

                                                                                if (this.Mode == 1)
                                                                                {
                                                                                    if (CheckPolicyNo(this.PolicyType, this.PolicyNo, this.Certificate, this.Suffix))
                                                                                        errorMessage = "This contract number is already exist in our database.";
                                                                                }

                                                                            }
            }
        

            if (errorMessage.Trim() == "")
            {
                if (this.PolicyClassID == 9) //Auto Gap
                {
                    if (this.VehicleMakeID == 0)
                        errorMessage = "Vehicle Make is missing or wrong.";
                    else
                        if (this.VehicleModelID == 0)
                            errorMessage = "Vehicle Model is missing or wrong.";
                        else
                            if (this.VehicleYearID == 0)
                                errorMessage = "Vehicle Year is missing or wrong.";
                            else
                                if (this.NewUse == 0)
                                    errorMessage = "New / Used is missing or wrong.";
                                else
                                    if (this.Cost == 0.00)
                                        errorMessage = "Cost is missing or wrong.";
                                    else
                                        if (this.FinanceAmt == 0.00)
                                            errorMessage = "Finance Amount is missing or wrong.";
                                        else
                                            if (this.VIN.Trim() == "")
                                                errorMessage = "The VIN is missing or wrong.";
                                            else
                                                if (this.VIN.Length < 17)
                                                    errorMessage = "The VIN must be 17 digits.";
               }
            }

            if (errorMessage.Trim() == "")
            {
                if (this.PolicyClassID == 13 || this.PolicyClassID == 17) //Etch, Ult.Prot
                {
                    if (this.Customer.City.Trim() == "")
                        errorMessage = "The City is missing or wrong.";
                    else
                        if (this.Customer.Address1.Trim() == "")
                            errorMessage = "The Address 1 is missing or wrong.";
                        else
                            if (this.VehicleMakeID == 0)
                                errorMessage = "Vehicle Make is missing or wrong.";
                            else
                                if (this.VehicleModelID == 0 && this.PolicyClassID == 13)
                                    errorMessage = "Vehicle Model is missing or wrong.";
                                else
                                    if (this.VehicleYearID == 0)
                                        errorMessage = "Vehicle Year is missing or wrong.";
                                    else
                                        if (this.VIN.Trim() == "" && this.PolicyClassID == 13)
                                            errorMessage = "The VIN is missing or wrong.";
                                        else
                                            if (this.VIN.Length < 17 && this.PolicyClassID == 13)
                                                errorMessage = "The VIN must be 17 digits.";
                                            else
                                                if (this.Mode == 1)
                                                {
                                                    if (this.PolicyClassID == 13 || this.PolicyClassID == 17) //Etch, UltProt
                                                    {
                                                        if (CheckPolicyNoEtch(this.PolicyType, this.PolicyNo, this.Certificate, this.Suffix, this.Term.ToString(), this.InsuranceCompany.ToString()))
                                                            errorMessage = "This contract number is already exist in our database.";
                                                    }
                                                    else
                                                    {
                                                        if (CheckPolicyNo(this.PolicyType, this.PolicyNo, this.Certificate, this.Suffix))
                                                            errorMessage = "This contract number is already exist in our database.";
                                                    }
                                                }
                                                else if (this.Mode == 2)
                                                {
                                                    if (this.PolicyClassID == 13) //Etch
                                                    {
                                                        if (CheckPolicyNoEtchEdit(this.PolicyType, this.PolicyNo, this.Certificate, this.Suffix, this.Term.ToString(), this.InsuranceCompany.ToString(), this.TaskControlID))
                                                            errorMessage = "This conract number and terms already exist to this Insurance Company.";
                                                    }
                                                }
                }
                if (this.PolicyClassID == 15) // OSO
                {
                    if (this.Customer.City.Trim() == "")
                        errorMessage = "The City is missing or wrong.";
                    else
                        if (this.Customer.Address1.Trim() == "")
                            errorMessage = "The Address 1 is missing or wrong.";
                        else
                            if (this.VehicleMakeID == 0)
                                errorMessage = "Vehicle Make is missing or wrong.";
                            else
                                if (this.VehicleModelID == 0)
                                    errorMessage = "Vehicle Model is missing or wrong.";
                                else
                                    if (this.VehicleYearID == 0)
                                        errorMessage = "Vehicle Year is missing or wrong.";
                                    else
                                        if (this.NewUse == 0)
                                            errorMessage = "New/Used is missing or wrong.";
                                        else
                                            if (this.VIN.Trim() == "")
                                                errorMessage = "The VIN is missing or wrong.";
                                            else
                                                if (this.VIN.Length < 17)
                                                    errorMessage = "The VIN must be 17 digits.";
                                                else
                                                    if (this.Customer.JobPhone.Trim() == "" && this.Customer.Cellular.Trim() == "" && this.Customer.HomePhone.Trim() == "")
                                                        errorMessage = "Enter at least one phone number.";

                                                    if (this.Mode == 1 && this.Suffix == "00")
                                                    {
                                                        string result = CheckVINByPolicyClass(this.VIN, this.PolicyClassID);
                                                        if (result.Trim() != "")
                                                            errorMessage = result;
                                                    }
                }
            }
			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}
		}

        public string CheckVINByPolicyClass(string VIN, int PolicyClassID)
        {
            DataTable dt = Policy.GetVINByPolicyClass(VIN, PolicyClassID);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                if (this.PolicyClassID == 17) //ULT.PROTECTOR
                {
                    return "";
                }
                else
                {
                    if (dt.Rows[0]["Certificate"].ToString().Trim() != "")
                        return "This VIN number is already exist in our database. Policy Number - " + dt.Rows[0]["PolicyNo"].ToString().Trim() + "  Certificate - " + dt.Rows[0]["Certificate"].ToString().Trim();
                    else
                        return "This VIN number is already exist in our database. Policy Number - " + dt.Rows[0]["PolicyNo"].ToString().Trim();
                }
            }           
        }

        public bool CheckPolicyNo(string policyType,string policyNo, string certificate, string sufijo)
        {
            DataTable dt = Policy.GetPolicyByPolicyNo(policyType, policyNo, certificate, sufijo);

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckPolicyNoEtch(string policyType, string policyNo, string certificate, string sufijo, string Term, string InsuranceCompany)
        {
            DataTable dt = Policy.GetEtchValidateCounter(policyNo, Term, InsuranceCompany, sufijo);

            if (dt.Rows[0]["Exist"].ToString() == "True")
            {
                return true;
            }
            else
            {
                return false;
            }              
        }

        public bool CheckPolicyNoEtchEdit(string policyType, string policyNo, string certificate, string sufijo, string Term, string InsuranceCompany, int TControlID)
        {
            DataTable dt = Policy.GetEtchValidateCounterEdit(policyNo, Term, InsuranceCompany, TControlID, sufijo);

            if (dt.Rows[0]["Exist"].ToString() == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckContactNoByDealer(string insuranceCompany, string companyDealer, string policyNo)
        {
            if (this.AutoAssignPolicy == false)
            {
                DataTable dt = Policy.GetPolicyByPolicyNoAndCompanyDealer(insuranceCompany, companyDealer, policyNo.Substring(2, policyNo.Length-2));

                if (dt.Rows.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }        

		public static Policies GetPolicies(int TaskControlID)
		{
			Policies policies = null;

			DataTable dt = GetPoliciesByTaskControlID(TaskControlID);
            
			policies = new Policies();
			policies = (Policies) Policy.GetPolicyByTaskControlID(TaskControlID,policies);  //Policy
			policies._dtPolices = dt;

			policies = FillProperties(policies);

			return policies;
		}

		public static void DeletePoliciesByTaskControlID(int taskControlID)
		{
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

			try
			{
				Executor.BeginTrans();
				Executor.Update("DeletePolicies",DeletePoliciesByTaskControlIDXml(taskControlID));
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error, Please try again. "+xcp.Message,xcp);
			}
		}

        public static DataTable GetEtchRateByetchRateID(int EtchRateID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("EtchRateID",
            SqlDbType.Int, 0, EtchRateID.ToString(),
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetEtchRateByetchRateID", xmlDoc);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        public static DataTable GetUltProtRateByUltProtRateID(int UltProtRateID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("UltProtRateID",
            SqlDbType.Int, 0, UltProtRateID.ToString(),
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetUltProtRateByUltProtRateID", xmlDoc);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        public static DataTable GetCompulsoryInsuranceRate(int UseType, int IsNew)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("UseType",
            SqlDbType.Int, 0, UseType.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsNew",
            SqlDbType.Int, 0, IsNew.ToString(),
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetCompulsoryInsuranceRate", xmlDoc);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        public static DataTable GetVSCSunGuardInterfase(int policyClassID, string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, policyClassID.ToString(),
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetVSCSunGuardInterface", xmlDoc);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        public static DataTable GetVSCProductionHeader(int policyClassID, string BegDate, string EndDate, bool IsNewPolicies)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, policyClassID.ToString(),
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
            DataTable dt = null;
            try
            {
                if (IsNewPolicies)
                {
                    dt = exec.GetQuery("GetVSCProductionPoliciesHeader", xmlDoc);
                }
                else
                {
                    dt = exec.GetQuery("GetVSCProductionCancHeader", xmlDoc);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }			
        }

        public static DataTable GetVSCProductionDetail(int policyClassID, string BegDate, string EndDate, bool IsNewPolicies)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, policyClassID.ToString(),
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
            DataTable dt = null;
            try
            {
                if (IsNewPolicies)
                {
                    dt = exec.GetQuery("GetVSCProductionPoliciesDetail", xmlDoc);
                }
                else
                {
                    dt = exec.GetQuery("GetVSCProductionCancDetail", xmlDoc);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        public static DataTable GetVSCProductionUpdate(int policyClassID, string BegDate, string EndDate, bool IsNewPolicies)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, policyClassID.ToString(),
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetVSCProductionPoliciesUpdate", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }
		#endregion

		#region Private Methods

		public static DataTable GetCoversByPolicyClassID(int policyClassID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, policyClassID.ToString(),
				ref cookItems);

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
	
			DataTable dt = exec.GetQuery("GetCoversByPolicyClassID",xmlDoc);
			
			return dt;	
		}

		public static DataTable GetLimitByCoversID(int coversID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CoversID",
				SqlDbType.Int, 0, coversID.ToString(),
				ref cookItems);

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
	
			DataTable dt = exec.GetQuery("GetLimitByCoversID",xmlDoc);
			
			return dt;	
		}

		public void CalculatePremium()
		{
//			Quotes.MBIQuote MbiQuotes = new Quotes.MBIQuote();
//			MbiQuotes.Calculate(this.PolicyType.Trim(),this.Model.Trim(),this.Make.Trim(),this.Term);
//
//			if(this.InsuranceCompany == "097")
//			{
//				this.TotalPremium = 175.00;
//			}
//			else
//			{
//				this.TotalPremium = MbiQuotes.premium;
//			}
//
//			this.VehicleClass = MbiQuotes.classification;
//			this.Mileage      = MbiQuotes.plan;		
		}


		private static DataTable GetPoliciesByTaskControlID(int TaskControlID)
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
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			
			DataTable dt = exec.GetQuery("GetPoliciesByTaskControlID",xmlDoc);
			return dt;
		}

		private void SavePoliciesPolicies(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (this.Mode)
				{
					case 1:  //ADD
						this.PoliciesID = Executor.Insert("AddPolicies",this.GetInsertPoliciesXml());
						this.History(this._mode,UserID);
						break;

					case 3:  //DELETE
						//Executor.Update("DeleteAutoGuardServicesContract",this.GetDeletePoliciesXml());
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						this.History(this._mode,UserID);
						Executor.Update("UpdatePolicies",this.GetUpdatePoliciesXml());
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Policy. "+xcp.Message,xcp);
			}
		}

		private static XmlDocument DeletePoliciesByTaskControlIDXml(int taskControlID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskControlID.ToString(),
				ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private XmlDocument GetUpdatePoliciesXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[37];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Milleages",
				SqlDbType.Int, 0, this.Milleages.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Cost",
				SqlDbType.Float, 0, this.Cost.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FinanceAmt",
				SqlDbType.Float, 0, this.FinanceAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Balance",
				SqlDbType.Float, 0, this.Balance.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleClass",
				SqlDbType.VarChar, 20, this.VehicleClass.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
				SqlDbType.Int, 0, this.VehicleMakeID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleModelID",
				SqlDbType.Int, 0, this.VehicleModelID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleYearID",
				SqlDbType.Int, 0, this.VehicleYearID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VIN",
				SqlDbType.Char, 17, this.VIN.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Plate",
				SqlDbType.VarChar, 7, this.Plate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PurchaseDate",
				SqlDbType.DateTime, 0, this.PurchaseDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("NewUse",
				SqlDbType.Int, 0, this.NewUse.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Comments",
				SqlDbType.VarChar, 500, this.Comments.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("PrestamoArrenda",
				SqlDbType.Bit, 0, this.PrestamoArrenda.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyPrimaria",
				SqlDbType.Char, 3, this.InsuranceCompanyPrimaria.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("NumeroPolizaPrimaria",
				SqlDbType.Char, 11, this.NumeroPolizaPrimaria.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CoveragePlan",
				SqlDbType.Int, 0, this.CoveragePlan.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Miles",
				SqlDbType.Int, 0, this.Miles.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Diesel",
				SqlDbType.Bit, 0, this.Diesel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("WD4",
				SqlDbType.Bit, 0, this.WD4.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Turbo",
				SqlDbType.Bit, 0, this.Turbo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommercialUse",
				SqlDbType.Bit, 0, this.CommercialUse.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleCode",
				SqlDbType.VarChar, 20, this.VehicleCode.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDateCompany",
                SqlDbType.DateTime, 0, this.EffDateCompany.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LossFund",
                SqlDbType.Float, 0, this.LossFund.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OverHead",
                SqlDbType.Float, 0, this.OverHead.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("BankFee",
                SqlDbType.Float, 0, this.BankFee.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Profit",
                SqlDbType.Float, 0, this.Profit.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("Concurso",
                SqlDbType.Float, 0, this.Concurso.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DealerCost",
                SqlDbType.Float, 0, this.DealerCost.ToString(),
                ref cookItems);
   
             DbRequestXmlCooker.AttachCookItem("DealerProfit",
                SqlDbType.Float, 0, this.DealerProfit.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("CanReserve",
                SqlDbType.Float, 0, this.CanReserve.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("DealerNet",
                SqlDbType.Float, 0, this.DealerNet.ToString(),
                ref cookItems);
            
             DbRequestXmlCooker.AttachCookItem("VehicleServiceContractQuoteID",
                SqlDbType.Int, 0, this.VehicleServiceContractQuoteID.ToString(),
                ref cookItems);

             DbRequestXmlCooker.AttachCookItem("EtchRateID",
                 SqlDbType.Int, 0, this.EtchRateID.ToString(),
                 ref cookItems);

             DbRequestXmlCooker.AttachCookItem("UltProtRateID",
             SqlDbType.Int, 0, this.UltProtRateID.ToString(),
             ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}


		private XmlDocument GetInsertPoliciesXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[37];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Milleages",
				SqlDbType.Int, 0, this.Milleages.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Cost",
				SqlDbType.Float, 0, this.Cost.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FinanceAmt",
				SqlDbType.Float, 0, this.FinanceAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Balance",
				SqlDbType.Float, 0, this.Balance.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleClass",
				SqlDbType.VarChar, 20, this.VehicleClass.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
				SqlDbType.Int, 0, this.VehicleMakeID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleModelID",
				SqlDbType.Int, 0, this.VehicleModelID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleYearID",
				SqlDbType.Int, 0, this.VehicleYearID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VIN",
				SqlDbType.Char, 17, this.VIN.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Plate",
				SqlDbType.VarChar, 7, this.Plate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PurchaseDate",
				SqlDbType.DateTime, 0, this.PurchaseDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("NewUse",
				SqlDbType.Int, 0, this.NewUse.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Comments",
				SqlDbType.VarChar, 500, this.Comments.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PrestamoArrenda",
				SqlDbType.Bit, 0, this.PrestamoArrenda.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyPrimaria",
				SqlDbType.Char, 3, this.InsuranceCompanyPrimaria.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("NumeroPolizaPrimaria",
				SqlDbType.Char, 11, this.NumeroPolizaPrimaria.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CoveragePlan",
				SqlDbType.Int, 0, this.CoveragePlan.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Miles",
				SqlDbType.Int, 0, this.Miles.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Diesel",
				SqlDbType.Bit, 0, this.Diesel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("WD4",
				SqlDbType.Bit, 0, this.WD4.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Turbo",
				SqlDbType.Bit, 0, this.Turbo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommercialUse",
				SqlDbType.Bit, 0, this.CommercialUse.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleCode",
				SqlDbType.VarChar, 20, this.VehicleCode.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffDateCompany",
               SqlDbType.DateTime, 0, this.EffDateCompany.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LossFund",
                SqlDbType.Float, 0, this.LossFund.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OverHead",
                SqlDbType.Float, 0, this.OverHead.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankFee",
               SqlDbType.Float, 0, this.BankFee.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Profit",
               SqlDbType.Float, 0, this.Profit.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Concurso",
               SqlDbType.Float, 0, this.Concurso.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerCost",
               SqlDbType.Float, 0, this.DealerCost.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerProfit",
               SqlDbType.Float, 0, this.DealerProfit.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CanReserve",
               SqlDbType.Float, 0, this.CanReserve.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerNet",
               SqlDbType.Float, 0, this.DealerNet.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleServiceContractQuoteID",
               SqlDbType.Int, 0, this.VehicleServiceContractQuoteID.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EtchRateID",
                SqlDbType.Int, 0, this.EtchRateID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UltProtRateID",
                SqlDbType.Int, 0, this.UltProtRateID.ToString(),
                ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private static Policies FillProperties(Policies policies)
		{
			policies.PoliciesID		  = (int) policies._dtPolices.Rows[0]["PoliciesID"];
			policies.Milleages		  = (int) policies._dtPolices.Rows[0]["Milleages"];
			policies.Cost		      = (double) policies._dtPolices.Rows[0]["Cost"];
			policies.FinanceAmt		  = (double) policies._dtPolices.Rows[0]["FinanceAmt"];
			policies.Balance		  = (double) policies._dtPolices.Rows[0]["Balance"];
			policies.VehicleMakeID	  = (int) policies._dtPolices.Rows[0]["VehicleMakeID"];
			policies.VehicleModelID	  = (int) policies._dtPolices.Rows[0]["VehicleModelID"];
			policies.VehicleYearID	  = (int) policies._dtPolices.Rows[0]["VehicleYearID"];
			policies.VIN		      = policies._dtPolices.Rows[0]["VIN"].ToString();
			policies.VehicleClass     = policies._dtPolices.Rows[0]["VehicleClass"].ToString();
			policies.Plate			  = policies._dtPolices.Rows[0]["Plate"].ToString();
            policies.PurchaseDate	  = (policies._dtPolices.Rows[0]["PurchaseDate"]!= System.DBNull.Value)?((DateTime) policies._dtPolices.Rows[0]["PurchaseDate"]).ToShortDateString():"";
			policies.NewUse           = (int) policies._dtPolices.Rows[0]["NewUse"];
			policies.Comments		  = policies._dtPolices.Rows[0]["Comments"].ToString();
			policies.PrestamoArrenda  = (bool) policies._dtPolices.Rows[0]["PrestamoArrenda"];
			policies.InsuranceCompanyPrimaria = policies._dtPolices.Rows[0]["InsuranceCompanyPrimaria"].ToString();
			policies.NumeroPolizaPrimaria = policies._dtPolices.Rows[0]["NumeroPolizaPrimaria"].ToString();
			policies.CoveragePlan	  = (int) policies._dtPolices.Rows[0]["CoveragePlan"];
			policies.Miles			  = (int) policies._dtPolices.Rows[0]["Miles"];
			policies.Diesel			  = (bool) policies._dtPolices.Rows[0]["Diesel"];
			policies.WD4			  = (bool) policies._dtPolices.Rows[0]["WD4"];
			policies.Turbo			  = (bool) policies._dtPolices.Rows[0]["Turbo"];
			policies.CommercialUse	  = (bool) policies._dtPolices.Rows[0]["CommercialUse"];
			policies.VehicleCode	  = policies._dtPolices.Rows[0]["VehicleCode"].ToString();        
            policies.EffDateCompany = (policies._dtPolices.Rows[0]["EffDateCompany"] != System.DBNull.Value) ? ((DateTime)policies._dtPolices.Rows[0]["EffDateCompany"]).ToShortDateString() : "";
            policies.LossFund = (policies._dtPolices.Rows[0]["LossFund"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["LossFund"] : 0.00;
            policies.OverHead = (policies._dtPolices.Rows[0]["OverHead"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["OverHead"] : 0.00;
            policies.BankFee = (policies._dtPolices.Rows[0]["BankFee"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["BankFee"] : 0.00;
            policies.Profit = (policies._dtPolices.Rows[0]["Profit"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["Profit"] : 0.00;
            policies.Concurso = (policies._dtPolices.Rows[0]["Concurso"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["Concurso"] : 0.00;
            policies.DealerCost = (policies._dtPolices.Rows[0]["DealerCost"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["DealerCost"] : 0.00;
            policies.DealerProfit = (policies._dtPolices.Rows[0]["DealerProfit"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["DealerProfit"] : 0.00;
            policies.CanReserve = (policies._dtPolices.Rows[0]["CanReserve"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["CanReserve"] : 0.00;
            policies.DealerNet = (policies._dtPolices.Rows[0]["DealerNet"] != System.DBNull.Value) ? (double)policies._dtPolices.Rows[0]["DealerNet"] : 0.00;
            policies.VehicleServiceContractQuoteID = (int)policies._dtPolices.Rows[0]["VehicleServiceContractQuoteID"];
            policies.EtchRateID = (int)policies._dtPolices.Rows[0]["EtchRateID"];
            policies.CompulsoryInsuranceQuoteID = (int)policies._dtPolices.Rows[0]["CompulsoryInsuranceQuoteID"];
            policies.UltProtRateID = (int)policies._dtPolices.Rows[0]["UltProtRateID"];

            policies.RoadAssistCollection = Policies.GetAutoRoadAssistByTaskControlID(policies.TaskControlID);

			return policies;
		}

		#endregion

		#region History

		private void History(int mode, int userID)
		{ 
			Audit.History history = new Audit.History();
			
			if(_mode == 2)
			{				
				// Campos de TaskControl
				history.BuildNotesForHistory("TaskControlTypeID",
					LookupTables.LookupTables.GetDescription("TaskControlType",oldPolices.TaskControlTypeID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskControlType",this.TaskControlTypeID.ToString()),
					mode);
				history.BuildNotesForHistory("TaskStatusID",
					LookupTables.LookupTables.GetDescription("TaskStatus",oldPolices.TaskStatusID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskStatus",this.TaskStatusID.ToString()),
					mode);	
				history.BuildNotesForHistory("ProspectID",oldPolices.ProspectID.ToString(),this.ProspectID.ToString(),mode);							
				history.BuildNotesForHistory("CustomerNo",oldPolices.CustomerNo,this.CustomerNo,mode);
				history.BuildNotesForHistory("PolicyID",oldPolices.PolicyID.ToString(),this.PolicyID.ToString(),mode);							
				history.BuildNotesForHistory("PolicyClassID",
					LookupTables.LookupTables.GetDescription("PolicyClass",oldPolices.PolicyClassID.ToString()),
					LookupTables.LookupTables.GetDescription("PolicyClass",this.PolicyClassID.ToString()),
					mode);	
				history.BuildNotesForHistory("Agency",oldPolices.Agent,this.Agent,mode);
				history.BuildNotesForHistory("Agent",oldPolices.Agent,this.Agent,mode);
				history.BuildNotesForHistory("SupplierID",oldPolices.SupplierID,this.SupplierID,mode);
				history.BuildNotesForHistory("Bank",
					LookupTables.LookupTables.GetDescription("Bank",oldPolices.Bank.ToString()),
					LookupTables.LookupTables.GetDescription("Bank",this.Bank.ToString()),
					mode);	
				history.BuildNotesForHistory("InsuranceCompany",
					LookupTables.LookupTables.GetDescription("InsuranceCompany",oldPolices.InsuranceCompany.ToString()),
					LookupTables.LookupTables.GetDescription("InsuranceCompany",this.InsuranceCompany.ToString()),
					mode);
				history.BuildNotesForHistory("Dealer",oldPolices.Dealer,this.Dealer,mode);
				history.BuildNotesForHistory("CompanyDealer",
					LookupTables.LookupTables.GetDescription("CompanyDealer",oldPolices.CompanyDealer.ToString()),
					LookupTables.LookupTables.GetDescription("CompanyDealer",this.CompanyDealer.ToString()),
					mode);	
				history.BuildNotesForHistory("CloseDate",oldPolices.CloseDate,this.CloseDate,mode);
				history.BuildNotesForHistory("EnteredBy",oldPolices.EnteredBy,this.EnteredBy,mode);
				// Terminan Campos TaskControl
				
				// Campos de Policies
				history.BuildNotesForHistory("Cost",oldPolices.Cost.ToString(),this.Cost.ToString(),mode);
				history.BuildNotesForHistory("Finance Amt",oldPolices.FinanceAmt.ToString(),this.FinanceAmt.ToString(),mode);
				history.BuildNotesForHistory("Milleages",oldPolices.Milleages.ToString(),this.Milleages.ToString(),mode);
				history.BuildNotesForHistory("VehicleClass",oldPolices.VehicleClass,this.VehicleClass,mode);
				history.BuildNotesForHistory("_Vehicle Make",
					LookupTables.LookupTables.GetDescription("VehicleMake",oldPolices.VehicleMakeID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleMake",this.VehicleMakeID.ToString()),
					mode);	
				history.BuildNotesForHistory("Vehicle Model",
					LookupTables.LookupTables.GetDescription("VehicleModel",oldPolices.VehicleModelID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleModel",this.VehicleModelID.ToString()),
					mode);
				history.BuildNotesForHistory("Vehicle Year",
					LookupTables.LookupTables.GetDescription("VehicleYear",oldPolices.VehicleYearID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleYear",this.VehicleYearID.ToString()),
					mode);	
				history.BuildNotesForHistory("VIN",oldPolices.VIN.ToString(),this.VIN.ToString(),mode);
				history.BuildNotesForHistory("Plate",oldPolices.Plate.ToString(),this.Plate.ToString(),mode);
				history.BuildNotesForHistory("Purchaser Date",oldPolices.PurchaseDate.ToString(),this.PurchaseDate.ToString(),mode);
				history.BuildNotesForHistory("NewUse",
					LookupTables.LookupTables.GetDescription("NewUse",oldPolices.NewUse.ToString()),
					LookupTables.LookupTables.GetDescription("NewUse",this.NewUse.ToString()),
					mode);	
				history.BuildNotesForHistory("Comments",oldPolices.Comments,this.Comments,mode);
				history.BuildNotesForHistory("Contract Type",oldPolices.PrestamoArrenda.ToString(),this.PrestamoArrenda.ToString(),mode);
				history.BuildNotesForHistory("Primary Insurance Company",oldPolices.InsuranceCompanyPrimaria,this.InsuranceCompanyPrimaria,mode);
				history.BuildNotesForHistory("Policy No Primary",oldPolices.NumeroPolizaPrimaria,this.NumeroPolizaPrimaria,mode);
                history.BuildNotesForHistory("EffDateCompany", oldPolices.EffDateCompany.ToString(), this.EffDateCompany.ToString(), mode);
				// Terminan Campos Policies

				// Campos de Policy
				history.BuildNotesForHistory("DepartmentID",
					LookupTables.LookupTables.GetDescription("Department",oldPolices.DepartmentID.ToString()),
					LookupTables.LookupTables.GetDescription("Department",this.DepartmentID.ToString()),
					mode);
				history.BuildNotesForHistory("PolicyType",oldPolices.PolicyType,this.PolicyType,mode);
				history.BuildNotesForHistory("PolicyNo",oldPolices.PolicyNo,this.PolicyNo,mode);
				history.BuildNotesForHistory("Certificate",oldPolices.Certificate,this.Certificate,mode);
				history.BuildNotesForHistory("Suffix",oldPolices.Suffix,this.Suffix,mode);
				history.BuildNotesForHistory("LoanNo",oldPolices.LoanNo.ToString(),this.LoanNo.ToString(),mode);
				history.BuildNotesForHistory("Term",oldPolices.Term.ToString(),this.Term.ToString(),mode);
				history.BuildNotesForHistory("EffectiveDate",oldPolices.EffectiveDate.ToString(),this.EffectiveDate.ToString(),mode);
				history.BuildNotesForHistory("ExpirationDate",oldPolices.ExpirationDate.ToString(),this.ExpirationDate.ToString(),mode);
				history.BuildNotesForHistory("Charge",oldPolices.Charge.ToString(),this.Charge.ToString(),mode);
				history.BuildNotesForHistory("TotalPremium",oldPolices.TotalPremium.ToString(),this.TotalPremium.ToString(),mode);
				history.BuildNotesForHistory("Status",oldPolices.Status.ToString(),this.Status.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgency",oldPolices.CommissionAgency.ToString(),this.CommissionAgency.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgencyPercent",oldPolices.CommissionAgencyPercent.ToString(),this.CommissionAgencyPercent.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgent",oldPolices.CommissionAgent.ToString(),this.CommissionAgent.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgentPercent",oldPolices.CommissionAgentPercent.ToString(),this.CommissionAgentPercent.ToString(),mode);
				history.BuildNotesForHistory("CommissionDate",oldPolices.CommissionDate.ToString(),this.CommissionDate.ToString(),mode);
				history.BuildNotesForHistory("CancellationDate",oldPolices.CancellationDate.ToString(),this.CancellationDate.ToString(),mode);
				history.BuildNotesForHistory("CancellationEntryDate",oldPolices.CancellationEntryDate.ToString(),this.CancellationEntryDate.ToString(),mode);
				history.BuildNotesForHistory("CancellationUnearnedPercent",oldPolices.CancellationUnearnedPercent.ToString(),this.CancellationUnearnedPercent.ToString(),mode);
				history.BuildNotesForHistory("ReturnPremium",oldPolices.ReturnPremium.ToString(),this.ReturnPremium.ToString(),mode);
				history.BuildNotesForHistory("ReturnCharge",oldPolices.ReturnCharge.ToString(),this.ReturnCharge.ToString(),mode);
				history.BuildNotesForHistory("CancellationAmount",oldPolices.CancellationAmount.ToString(),this.CancellationAmount.ToString(),mode);
				history.BuildNotesForHistory("CancellationMethod",oldPolices.CancellationMethod.ToString(),this.CancellationMethod.ToString(),mode);
				history.BuildNotesForHistory("CancellationReason",oldPolices.CancellationReason.ToString(),this.CancellationReason.ToString(),mode);
				history.BuildNotesForHistory("ReturnCommissionAgency",oldPolices.ReturnCommissionAgency.ToString(),this.ReturnCommissionAgency.ToString(),mode);
				history.BuildNotesForHistory("ReturnCommissionAgent",oldPolices.ReturnCommissionAgent.ToString(),this.ReturnCommissionAgent.ToString(),mode);
				history.BuildNotesForHistory("PaidAmount",oldPolices.PaidAmount.ToString(),this.PaidAmount.ToString(),mode);
				history.BuildNotesForHistory("PaidDate",oldPolices.PaidDate.ToString(),this.PaidDate.ToString(),mode);
				history.BuildNotesForHistory("AutoAssignPolicy",oldPolices.AutoAssignPolicy.ToString(),this.AutoAssignPolicy.ToString(),mode);
				history.BuildNotesForHistory("InvoiceNumber",oldPolices.InvoiceNumber.ToString(),this.InvoiceNumber.ToString(),mode);
				history.BuildNotesForHistory("FileNumber",oldPolices.FileNumber.ToString(),this.FileNumber.ToString(),mode);
				history.BuildNotesForHistory("IsLeasing",oldPolices.IsLeasing.ToString(),this.IsLeasing.ToString(),mode);
				history.BuildNotesForHistory("PaymentType",oldPolices.PaymentType.ToString(),this.PaymentType.ToString(),mode);
				history.BuildNotesForHistory("IsMaster",oldPolices.IsMaster.ToString(),this.IsMaster.ToString(),mode);
				history.BuildNotesForHistory("TCIDApplicationAuto",oldPolices.TCIDApplicationAuto.ToString(),this.TCIDApplicationAuto.ToString(),mode);
				history.BuildNotesForHistory("TCIDQuote",oldPolices.TCIDQuotes.ToString(),this.TCIDQuotes.ToString(),mode);
				history.BuildNotesForHistory("PrintPolicy",oldPolices.PrintPolicy.ToString(),this.PrintPolicy.ToString(),mode);
				history.BuildNotesForHistory("MasterPolicyID",
					LookupTables.LookupTables.GetDescription("MasterPolicy",oldPolices.MasterPolicyID.ToString()),
					LookupTables.LookupTables.GetDescription("MasterPolicy",this.MasterPolicyID.ToString()),
					mode);
				history.BuildNotesForHistory("Prem_Mes",oldPolices.Prem_Mes.ToString(),this.Prem_Mes.ToString(),mode);
				history.BuildNotesForHistory("PMT1",oldPolices.PMT1.ToString(),this.PMT1.ToString(),mode);
				history.BuildNotesForHistory("PrintDate",oldPolices.PrintDate.ToString(),this.PrintDate.ToString(),mode);
				history.BuildNotesForHistory("OriginatedAt",oldPolices.OriginatedAt.ToString(),this.OriginatedAt.ToString(),mode);
				// Terminan Campos Policy

                history.BuildNotesForHistory("FirstName", oldPolices.Customer.FirstName, this.Customer.FirstName, mode);
                history.BuildNotesForHistory("LastName1", oldPolices.Customer.LastName1, this.Customer.LastName1, mode);
                history.BuildNotesForHistory("LastName2", oldPolices.Customer.LastName2, this.Customer.LastName2, mode);


				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("TaskControlID.","",this.TaskControlID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.TaskControlID.ToString();
			history.Subject = "POLICIES";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}
		
		
		private object SafeConvertToDateTime(string StringObject)
		{
			if(StringObject != string.Empty)
			{
				try	{return DateTime.Parse(StringObject);}
				catch{/*Write to error logging sub-system.*/}
			}
			return StringObject;
		}


		#endregion

        #region RoadAssist Autos

        public void SaveAutoRoadAssist(int UserID, int taskControlID)
            {
                Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
                Executor.Update("DeleteAutoRoadAssistByTaskControlID", DeleteAutoRoadAssistByTaskControlIDXml(taskControlID));

                for (int i = 0; i < RoadAssistCollection.Rows.Count; i++)
                {
                    this.Mode = 1; //Add

                    Executor.BeginTrans();
                    int dependienteID = Executor.Insert("AddAutoRoadAssist", this.GetInsertAutoRoadAssistXml(i));
                    Executor.CommitTrans();
                }

                //this.Mode = (int)DwellingPropertiesMode.CLEAR;
            }

        private XmlDocument DeleteAutoRoadAssistByTaskControlIDXml(int taskControlID)
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                    SqlDbType.Int, 0, taskControlID.ToString(),
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

        private XmlDocument GetInsertAutoRoadAssistXml(int index)
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[12];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                  SqlDbType.Int, 0, this.TaskControlID.ToString(),
                  ref cookItems);

                DbRequestXmlCooker.AttachCookItem("VIN",
                  SqlDbType.VarChar, 100, RoadAssistCollection.Rows[index]["VIN"].ToString(),
                  ref cookItems);

                DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
                  SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["VehicleMakeID"].ToString(),
                  ref cookItems);

                DbRequestXmlCooker.AttachCookItem("VehicleModelID",
                 SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["VehicleModelID"].ToString(),
                 ref cookItems);

                DbRequestXmlCooker.AttachCookItem("VehicleYearID",
                  SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["VehicleYearID"].ToString(),
                  ref cookItems);

                DbRequestXmlCooker.AttachCookItem("NewUse",
                 SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["NewUse"].ToString(),
                 ref cookItems);

                DbRequestXmlCooker.AttachCookItem("Plate",
                 SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["Plate"].ToString(),
                 ref cookItems);

                DbRequestXmlCooker.AttachCookItem("NewUseDesc",
                 SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["NewUseDesc"].ToString(),
                 ref cookItems);

                DbRequestXmlCooker.AttachCookItem("VehicleMake",
                 SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["VehicleMake"].ToString(),
                 ref cookItems);

                DbRequestXmlCooker.AttachCookItem("VehicleModel",
                 SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["VehicleModel"].ToString(),
                 ref cookItems);

                DbRequestXmlCooker.AttachCookItem("VehicleYear",
                 SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["VehicleYear"].ToString(),
                 ref cookItems);

                DbRequestXmlCooker.AttachCookItem("Premium",
                 SqlDbType.Float, 0, RoadAssistCollection.Rows[index]["Premium"].ToString(),
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

        public static DataTable GetAutoRoadAssistByTaskControlID(int TaskControlID)
            {
                DataTable dt = GetAutoRoadAssistByTaskControlIDDB(TaskControlID);
                return dt;
            }

        private static DataTable GetAutoRoadAssistByTaskControlIDDB(int taskControlID)
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

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

                DataTable dt = exec.GetQuery("GetAutoRoadAssistByTaskControlID", xmlDoc);

                return dt;
            }

        public DataTable RoadAssistCollection
        {
            get
            {
                if (this._RoadAssistCollection == null)
                    this._RoadAssistCollection = DataTableRoadAssistTemp();
                return (this._RoadAssistCollection);
            }
            set
            {
                this._RoadAssistCollection = value;
            }
        }

        private DataTable DataTableRoadAssistTemp()
        {
            DataTable myDataTable = new DataTable("DataTableRoadAssistTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "AsistenciaCarreteraAutoID";
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
            myDataColumn.ColumnName = "VIN";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VIN";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleMakeID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleMakeID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleModelID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleModelID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleYearID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleYearID";
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
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Plate";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Plate";
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
            myDataColumn.ColumnName = "VehicleYear";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleYear";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NewUseDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NewUseDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium";
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
