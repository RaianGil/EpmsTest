using System;
using System.Data;
using System.Text;
using System.IO;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using System.Reflection;
using EPolicy.TaskControl;
using EPolicy.Audit;
using EPolicy.XmlCooker;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace EPolicy.TaskControl
{
	public class Policy:TaskControl
	{
		public Policy()
		{
			this._mode =(int) TaskControlMode.ADD;
		}
	    
		#region Variable

        //private DeferredPayment _DeferredPayment;
		private DataTable _dtPolicy ;
		private ListBox   _LbxAgentSelected;
		private ListBox   _LbxSupplierSelected;
		private DataTable _AgentSelected;
		private DataTable _SupplierSelected;
		private PaymentPolicy _Payments = new PaymentPolicy();
		private int      _DepartmentID     = 0;
		private string   _PolicyType       = "";
		private string   _PolicyNo         = "";
		private string   _Certificate      = "";
		private string   _Suffix           = "00";
		private string   _LoanNo           = "";
		private int      _Term             = 0;
		private string   _EffectiveDate    = "";
		private string   _ExpirationDate   = "";
		private double   _Charge           = 0.00;
		private double   _TotalPremium     = 0.00;
		private string   _Status           = "Inforce";
		private double   _CommissionAgency	       = 0.00;
		private string   _CommissionAgencyPercent  = "0";
		private double   _CommissionAgent          = 0.00;
		private string   _CommissionAgentPercent   = "";
		private string   _CommissionDate		   = "";
		private double   _ReturnPremium    = 0.00;
		private double   _ReturnCharge     = 0.00;
		private double   _CancellationAmount = 0.00;
		private string   _CancellationDate = "";
		private string   _CancellationEntryDate = "";
		private double   _CancellationUnearnedPercent = 0.00;
		private int      _CancellationMethod = 0;
		private int      _CancellationReason = 0;
		private double   _ReturnCommissionAgency = 0.00;
		private double   _ReturnCommissionAgent  = 0.00;
		private double   _PaidAmount = 0.00;
		private string   _PaidDate	 = "";
		private int      _OriginatedAt = 0;
		private bool	 _AutoAssignPolicy= true;
		private string   _InvoiceNumber   = "";
		private string   _FileNumber      = "";
		private bool	 _IsLeasing       = false;
		private int		 _PaymentType     = 1;  //Normal
		private bool     _IsMaster        = false;
		private int		 _PolicyCounterID = 0;
		private int		 _PolicyCicleEnd  = 0;
		private int      _PolicyIDTemp    = 0;
		private string   _AutoType        = "";
		private int		 _TCIDApplicationAuto = 0;
		private int      _TCIDQuotes      = 0;
		private bool     _PrintPolicy     = false;
		private string	 _MasterPolicyID  = "";
		private double   _Prem_Mes		  = 0.00;
		private decimal  _PMT1			  = 0;
		private string   _PrintDate		  = "";
        private bool     _IsEndorsement = false;
		private int      _Endoso		  = 0;
		private bool     _Modify		  = true;
		private bool	 _Trams_Fl		  = false;
		private string	 _Trams_DT		  = "";
		private bool	 _Ctrams_FL		  = false;
		private string	 _Ctrams_DT		  = "";
		private string   _Ren_Rei		  = "";
		private double   _Rein_Amt		  = 0;
		private DataTable _dtAgentList ;
		private DataTable _dtSupplierList ;
		private DataTable _dtPrintPolicyList ;
		private DataTable _dtPrintCopyList ;
		private DataTable _dtPrintJustOnceList ;
		private DataTable _dtPrintPolicyReady;
		private DataTable _dtPrintCopyReady;
        private bool _IsDeferred = false;
        private bool _IsFamily = false;
        private bool _IsEmployee = false;
        private string _EmployeeName = "";
        private int _FBPositionID = 0;
        private int _FBSubsidiaryID = 0;
        private int _FBBranchesID = 0;
        private bool _Pending = true;
        private double _CancLossFund = 0.00;
        private double _CancOverHead = 0.00;
        private double _CancBankFee = 0.00;
        private double _CancProfit = 0.00;
        private double _CancConcurso = 0.00;
        private double _CancDealerProfit = 0.00;
        private double _CancCanReserve = 0.00;
        private bool _CancWarranty = false;
        private int _CancInitialMileage = 0;
        private int _CancMileage = 0;


		// Variables to save the original value of the cancellation.
		private bool   _OriginalCancellation = false;
		private double _CancellationUnearnedPercentReadySave = 0.00;
		private string _CancellationDateReadySave = "";
		private double _CancellationReturnPremiumReadySave = 0.00;
		private double _CancellationReturnChargeReadySave = 0.00;
		private double _CancellationAmountReadySave = 0.00;	
		private int    _CancellationMethodReadySave = 0;
		private int    _CancellationReasonReadySave = 0;	
		private int _mode = (int) TaskControlMode.CLEAR;

		#endregion

		#region Properties

        //public DeferredPayment DeferredPayment
        //{
        //    get
        //    {
        //        return this._DeferredPayment;
        //    }
        //    set
        //    {
        //        this._DeferredPayment = value;
        //    }
        //}

		public int OriginatedAt
		{
			get
			{
				return this._OriginatedAt;
			}
			set
			{
				this._OriginatedAt = value;
			}
		}

		public string AutoType
		{
			get
			{
				return this._AutoType;
			}
			set
			{
				this._AutoType = value;
			}
		}

		public int PolicyIDTemp
		{
			get
			{
				return this._PolicyIDTemp;
			}
			set
			{
				this._PolicyIDTemp = value;
			}
		}

		public int PolicyCicleEnd
		{
			get
			{
				return this._PolicyCicleEnd;
			}
			set
			{
				this._PolicyCicleEnd = value;
			}
		}

		public int PolicyMode
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

		public ListBox LbxAgentSelected
		{
			get
			{
				return this._LbxAgentSelected;
			}
			set
			{
				this._LbxAgentSelected = value;
			}
		}
		
		public DataTable AgentSelectedTable
		{
			get
			{
				return this._AgentSelected;
			}
			set
			{
				this._AgentSelected = value;
			}
		}

		public ListBox LbxSupplierSelected
		{
			get
			{
				return this._LbxSupplierSelected;
			}
			set
			{
				this._LbxSupplierSelected = value;
			}
		}
		
		public DataTable SupplierSelectedTable
		{
			get
			{
				return this._SupplierSelected;
			}
			set
			{
				this._SupplierSelected = value;
			}
		}

		public PaymentPolicy PaymentsDetail
		{
			get
			{
				return this._Payments;
			}
			set
			{
				this._Payments = value;
			}
		}
		
		public int DepartmentID
		{
			get
			{
				return this._DepartmentID;
			}
			set
			{
				this._DepartmentID = value;
			}
		}

		public string PolicyType
		{
			get
			{
				return this._PolicyType;
			}
			set
			{
				this._PolicyType = value;
			}
		}

		public string PolicyNo
		{
			get
			{
				return this._PolicyNo;
			}
			set
			{
				this._PolicyNo = value;
			}
		}

		public string Certificate
		{
			get
			{
				return this._Certificate;
			}
			set
			{
				this._Certificate = value;
			}
		}

		public string Suffix
		{
			get
			{
				return this._Suffix;
			}
			set
			{
				this._Suffix = value;
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

		public string ExpirationDate
		{
			get
			{
				return this._ExpirationDate;
			}
			set
			{
				this._ExpirationDate = value;
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

		public string Status
		{
			get
			{
				string status = this._Status.Trim();

				switch (status)
				{
					case "Inforce":
					case "Expired":
                        if (this.PolicyClassID == 1) //VSC(nose aplica el charge pq se usa para el deducible.
                        {
                            if ((double)this.PaymentsDetail.TotalPaid >= this.TotalPremium)
                            {
                                status += "/" + "Paid";
                            }
                            else
                            {
                                status += "/" + "Unpaid";
                            }
                        }
                        else
                        {
                            if ((double)this.PaymentsDetail.TotalPaid >= this.TotalPremium + this.Charge)
                            {
                                status += "/" + "Paid";
                            }
                            else
                            {
                                status += "/" + "Unpaid";
                            }
                        }										
						break;
					case "Cancelled":
						status += "/"+ LookupTables.LookupTables.GetDescription("CancellationMethod",this.CancellationMethod.ToString());
						break;
				}
				return status;
			}
			set
			{
				this._Status = value;
			}
		}

		public double CommissionAgency
		{
			get
			{
				return this._CommissionAgency;
			}
			set 
			{
				this._CommissionAgency = value;
			}
		}

		public string CommissionAgentPercent
		{
			get
			{
				return this._CommissionAgentPercent;
			}
			set 
			{
				this._CommissionAgentPercent = value;
			}
		}

		public double CommissionAgent
		{
			get
			{
				return this._CommissionAgent;
			}
			set 
			{
				this._CommissionAgent = value;
			}
		}

		public string CommissionAgencyPercent
		{
			get
			{
				return this._CommissionAgencyPercent;
			}
			set 
			{
				this._CommissionAgencyPercent = value;
			}
		}

		public string CommissionDate
		{
			get
			{
				return this._CommissionDate;
			}
			set 
			{
				this._CommissionDate = value;
			}
		}

		public string PaidDate
		{
			get
			{
				return this._PaidDate;
			}
			set 
			{
				this._PaidDate = value;
			}
		}

		public double ReturnPremium
		{
			get
			{
				return this._ReturnPremium;
			}
			set 
			{
				this._ReturnPremium = value;
			}
		}

		public double ReturnCharge
		{
			get
			{
				return this._ReturnCharge;
			}
			set 
			{
				this._ReturnCharge = value;
			}
		}

		public double CancellationAmount
		{
			get
			{
				return this._CancellationAmount;
			}
			set 
			{
				this._CancellationAmount = value;
			}
		}

		public string CancellationDate
		{
			get
			{
				return this._CancellationDate;
			}
			set
			{
				this._CancellationDate = value;
			}
		}

		public string CancellationEntryDate
		{
			get
			{
				return this._CancellationEntryDate;
			}
			set
			{
				this._CancellationEntryDate = value;
			}
		}

		public double CancellationUnearnedPercent
		{
			get
			{
				return this._CancellationUnearnedPercent;
			}
			set
			{
				this._CancellationUnearnedPercent = value;
			}
		}

		public int CancellationMethod
		{
			get
			{
				return this._CancellationMethod;
			}
			set
			{
				this._CancellationMethod = value;
			}
		}

		public int CancellationReason
		{
			get
			{
				return this._CancellationReason;
			}
			set
			{
				this._CancellationReason = value;
			}
		}

		public bool OriginalCancellation
		{
			get
			{
				return this._OriginalCancellation;
			}
			set
			{
				this._OriginalCancellation = value;
			}
		}

		public double CancellationUnearnedPercentReadySave
		{
			get
			{
				return this._CancellationUnearnedPercentReadySave;
			}
			set
			{
				this._CancellationUnearnedPercentReadySave = value;
			}
		}

		public string CancellationDateReadySave
		{
			get
			{
				return this._CancellationDateReadySave;
			}
			set
			{
				this._CancellationDateReadySave = value;
			}
		}

		public double CancellationReturnPremiumReadySave
		{
			get
			{
				return this._CancellationReturnPremiumReadySave;
			}
			set
			{
				this._CancellationReturnPremiumReadySave = value;
			}
		}

		public double CancellationReturnChargeReadySave
		{
			get
			{
				return this._CancellationReturnChargeReadySave;
			}
			set
			{
				this._CancellationReturnChargeReadySave = value;
			}
		}

		public double CancellationAmountReadySave
		{
			get
			{
				return this._CancellationAmountReadySave;
			}
			set
			{
				this._CancellationAmountReadySave = value;
			}
		}

		public int CancellationMethodReadySave
		{
			get
			{
				return this._CancellationMethodReadySave;
			}
			set
			{
				this._CancellationMethodReadySave = value;
			}
		}

		public int CancellationReasonReadySave
		{
			get
			{
				return this._CancellationReasonReadySave;
			}
			set
			{
				this._CancellationReasonReadySave = value;
			}
		}

		public double ReturnCommissionAgent
		{
			get
			{
				return this._ReturnCommissionAgent;
			}
			set 
			{
				this._ReturnCommissionAgent = value;
			}
		}

		public double ReturnCommissionAgency
		{
			get
			{
				return this._ReturnCommissionAgency;
			}
			set 
			{
				this._ReturnCommissionAgency = value;
			}
		}
		
		public double PaidAmount
		{
			get
			{
				return this._PaidAmount;
			}
			set 
			{
				this._PaidAmount = value;
			}
		}
		public DataTable AgentList
		{
			get
			{
				return this._dtAgentList;
			}
			set
			{
				this._dtAgentList = value;
			}
		}

		public DataTable SupplierList
		{
			get
			{
				return this._dtSupplierList;
			}
			set
			{
				this._dtSupplierList = value;
			}
		}

		public DataTable PrintPolicyList
		{
			get
			{
				return this._dtPrintPolicyList;
			}
			set
			{
				this._dtPrintPolicyList = value;
			}
		}

		public DataTable PrintCopyList
		{
			get
			{
				return this._dtPrintCopyList;
			}
			set
			{
				this._dtPrintCopyList = value;
			}
		}

		public DataTable PrintJustOnceList
		{
			get
			{
				return this._dtPrintJustOnceList;
			}
			set
			{
				this._dtPrintJustOnceList = value;
			}
		}

		public DataTable PrintPolicyReady
		{
			get
			{
				return this._dtPrintPolicyReady;
			}
			set
			{
				this._dtPrintPolicyReady = value;
			}
		}

		public DataTable PrintCopyReady
		{
			get
			{
				return this._dtPrintCopyReady;
			}
			set
			{
				this._dtPrintCopyReady = value;
			}
		}

		public bool	 AutoAssignPolicy
		{
			get
			{
				return this._AutoAssignPolicy;
			}
			set
			{
				this._AutoAssignPolicy = value;
			}
		}

		public string   InvoiceNumber
		{
			get
			{
				return this._InvoiceNumber;
			}
			set
			{
				this._InvoiceNumber = value;
			}
		}

		public string   FileNumber
		{
			get
			{
				return this._FileNumber;
			}
			set
			{
				this._FileNumber = value;
			}
		}

		public bool	 IsLeasing
		{
			get
			{
				return this._IsLeasing;
			}
			set
			{
				this._IsLeasing = value;
			}
		}

		public int PaymentType
		{
			get
			{
				return this._PaymentType;
			}
			set
			{
				this._PaymentType = value;
			}
		}	

		public bool IsMaster
		{
			get
			{
				return this._IsMaster;
			}
			set
			{
				this._IsMaster = value;
			}
		}

		public int TCIDApplicationAuto
		{
			get
			{
				return this._TCIDApplicationAuto;
			}
			set
			{
				this._TCIDApplicationAuto = value;
			}
		}	

		public int TCIDQuotes
		{
			get
			{
				return this._TCIDQuotes;
			}
			set
			{
				this._TCIDQuotes = value;
			}
		}	

		public bool PrintPolicy
		{
			get
			{
				return this._PrintPolicy;
			}
			set
			{
				this._PrintPolicy = value;
			}
		}

		public string MasterPolicyID
		{
			get
			{
				return this._MasterPolicyID;
			}
			set
			{
				this._MasterPolicyID = value;
			}
		}
		public double Prem_Mes
		{
			get
			{
				return this._Prem_Mes;
			}
			set
			{
				this._Prem_Mes = value;
			}
		}

		public decimal PMT1
		{
			get
			{
				return this._PMT1;
			}
			set
			{
				this._PMT1 = value;
			}
		}

		public string PrintDate
		{
			get
			{
				return this._PrintDate;
			}
			set
			{
				this._PrintDate = value;
			}
		}

        public bool IsEndorsement
        {
            get
            {
                return this._IsEndorsement;
            }
            set
            {
                this._IsEndorsement = value;
            }
        }

		public int Endoso
		{
			get
			{
				return this._Endoso;
			}
			set
			{
				this._Endoso = value;
			}
		}

		public bool Trams_FL
		{
			get
			{
				return this._Trams_Fl;
			}
			set
			{
				this._Trams_Fl = value;
			}
		}

		public string Trams_DT
		{
			get
			{
				return this._Trams_DT;
			}
			set
			{
				this._Trams_DT = value;
			}
		}

		public bool Ctrams_FL
		{
			get
			{
				return this._Ctrams_FL;
			}
			set
			{
				this._Ctrams_FL = value;
			}
		}

		public string Ctrams_DT
		{
			get
			{
				return this._Ctrams_DT;
			}
			set
			{
				this._Ctrams_DT = value;
			}
		}

		public string   Ren_Rei		 	
		{
			get
			{
				return this._Ren_Rei;
			}
			set
			{
				this._Ren_Rei = value;
			}
		}

		public double   Rein_Amt	
		{
			get
			{
				return this._Rein_Amt;
			}
			set
			{
				this._Rein_Amt = value;
			}
		}

        public bool IsDeferred
        {
            get
            {
                return this._IsDeferred;
            }
            set
            {
                this._IsDeferred = value;
            }
        }

        public bool IsFamily
        {
            get
            {
                return this._IsFamily;
            }
            set
            {
                this._IsFamily = value;
            }
        }
        public bool IsEmployee
        {
            get
            {
                return this._IsEmployee;
            }
            set
            {
                this._IsEmployee = value;
            }
        }
        public string EmployeeName
        {
            get
            {
                return this._EmployeeName;
            }
            set
            {
                this._EmployeeName = value;
            }
        }
        public int FBPositionID
        {
            get
            {
                return this._FBPositionID;
            }
            set
            {
                this._FBPositionID = value;
            }
        }
        public int FBSubsidiaryID
        {
            get
            {
                return this._FBSubsidiaryID;
            }
            set
            {
                this._FBSubsidiaryID = value;
            }
        }
        public int FBBranchesID
        {
            get
            {
                return this._FBBranchesID;
            }
            set
            {
                this._FBBranchesID = value;
            }
        }
        public bool Pending
        {
            get
            {
                return this._Pending;
            }
            set
            {
                this._Pending = value;
            }
        }

        public double CancLossFund
        {
            get
            {
                return this._CancLossFund;
            }
            set
            {
                this._CancLossFund = value;
            }
        }

        public double CancOverHead
        {
            get
            {
                return this._CancOverHead;
            }
            set
            {
                this._CancOverHead = value;
            }
        }

        public double CancBankFee
        {
            get
            {
                return this._CancBankFee;
            }
            set
            {
                this._CancBankFee = value;
            }
        }

        public double CancProfit
        {
            get
            {
                return this._CancProfit;
            }
            set
            {
                this._CancProfit = value;
            }
        }

        public double CancConcurso
        {
            get
            {
                return this._CancConcurso;
            }
            set
            {
                this._CancConcurso = value;
            }
        }

        public double CancDealerProfit
        {
            get
            {
                return this._CancDealerProfit;
            }
            set
            {
                this._CancDealerProfit = value;
            }
        }

        public double CancCanReserve
        {
            get
            {
                return this._CancCanReserve;
            }
            set
            {
                this._CancCanReserve = value;
            }
        }

        public bool CancWarranty
        {
            get
            {
                return this._CancWarranty;
            }
            set
            {
                this._CancWarranty = value;
            }
        }

        public int CancInitialMileage
        {
            get
            {
                return this._CancInitialMileage;
            }
            set
            {
                this._CancInitialMileage = value;
            }
        }

        public int CancMileage
        {
            get
            {
                return this._CancMileage;
            }
            set
            {
                this._CancMileage = value;
            }
        }
		#endregion

		#region Public Methods

		public void SavePolicyforQuote(int UserID)
		{
			SavePolicy(UserID);
		}

		public virtual void SavePol(int UserID)
		{
            if (PolicyClassID != 25)
            {
                if (PolicyClassID != 3 && PolicyClassID != 21 && PolicyClassID != 23 && PolicyClassID != 26 && PolicyClassID != 27 && PolicyClassID != 29)  // No aplica para Auto Privado
                {
                    if (this.SupplierID.Trim() != "000")
                        SaveSupplier();
                    else
                        DeleteSupplierByTaskControlID();
                }

                if (this.Agent.Trim() != "000")
                    SaveAgent();
                else
                    DeleteAgentByTaskControlID();
            }

            //if (IsDeferred)   //Pago diferido.
            //{
            //    DeferredPayment.Mode = this._mode;
            //    DeferredPayment.SaveDeferredPayment(UserID);
            //}

			SavePolicy(UserID);


			if (this._mode==1)
			{
				this.PaymentsDetail.InsertAutomaticPaymentFromPolicy(this);
			}
		}

        public static void DeletePolicyPPSError(int TaskControlID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            exec.Update("DeletePolicyPPSError", xmlDoc);
		}

		public void UpdateApplicationAutoStatus(int statusID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TCIDApplicationAuto.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("StatusID",
				SqlDbType.Int, 0, statusID.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			exec.Update("UpdateApplicationAutoStatus",xmlDoc);
		}
		
		public void UpdateApplicationAutoPolicyNo(string policyType, string policyNo, string certificate, string suffix)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TCIDApplicationAuto.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, policyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyNo",
				SqlDbType.Char, 11, policyNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Certificate",
				SqlDbType.Char, 10, certificate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Sufijo",
				SqlDbType.Char, 2, suffix.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TCIDPolicy",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			exec.Update("UpdateApplicationAutoPolicyNo",xmlDoc);
		}

		public void UpdateConvertToPolicy()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TCIDQuotes.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ConvertToPolicy",
				SqlDbType.DateTime, 0, DateTime.Now.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			exec.Update("UpdateConvertToPolicy",xmlDoc);
		}

		public void UpdatePolicyModifyField()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Modify",
				SqlDbType.Bit, 0, true.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			exec.Update("UpdatePolicyModifyField",xmlDoc);
		}

		public static DataTable GetPolicies(int policyClass, string policyType,string policyNo, string certificate, string sufijo, string loanNo, string bank, int UserID)
		{
			string VIN=""; // Se hizo un overloading tambien se utiliza en payments.cs
			DataTable dt = GetPolicyByCriteria(policyClass,policyType,policyNo,certificate,sufijo,loanNo,bank,VIN,UserID);

			return dt;
		}

		public static DataTable GetPolicies(int policyClass, string policyType,string policyNo, string certificate, string sufijo, string loanNo, string bank,string VIN, int UserID)
		{
			DataTable dt = GetPolicyByCriteria(policyClass,policyType,policyNo,certificate,sufijo,loanNo,bank,VIN,UserID);

			return dt;
		}

		public static DataTable GetPolicies(string policyType,string policyNo, string certificate, string sufijo)
		{
			DataTable dt = GetPolicyByPolicyNo(policyType,policyNo,certificate,sufijo);

			return dt;
		}

		public int GetPolicyIDTemp()
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

			try
			{
				Executor.BeginTrans();
				this.PolicyIDTemp = Executor.Insert("AddPolicyIdTemp",this.AddPolicyIdTempXml());
                Executor.CommitTrans();
				return this.PolicyIDTemp;            
				
			}
			catch(Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Policy. "+xcp.Message,xcp);
			}
		}

		#region Calculate Return Premium
        public void CalculateReturnPremium(DateTime canDate, int canMethod, int canReason)
        {
            if (canDate < DateTime.Parse(this.EffectiveDate))
                throw new ArgumentException("Cancellation date cannot be neither prior " +
                    "to inception date nor later to expiration date");

            if ((canMethod == 3 || (canDate == DateTime.Parse(this.EffectiveDate))))
            {
                this.CancellationMethod = 3;  //Siempre es FLAT.
                this.ReturnPremium = this.TotalPremium;
                this.ReturnCharge = this.Charge;
                this.CancellationAmount = this.TotalPremium;
                this.CancellationUnearnedPercent = 0.0;
            }
            else
            {
                double mFactor = 0.0;
                double NewProRataTotPrem = 0.0;
                double NewShotRateTotPrem = 0.0;

                TimeSpan tsDAYS1 = DateTime.Parse(this.ExpirationDate) - canDate;
                TimeSpan tsDAYS2 = DateTime.Parse(this.ExpirationDate) - DateTime.Parse(this.EffectiveDate);

                int mDAYS1 = tsDAYS1.Days;
                int mDAYS2 = tsDAYS2.Days;

                mFactor = double.Parse(mDAYS1.ToString()) / double.Parse(mDAYS2.ToString());

                if (canMethod == 1)//Prorata
                {
                    mFactor = Math.Round(mFactor, 3);
                }
                else
                {
                    mFactor = Math.Round(mFactor*.90, 3);
                }

                DataTable dt = GetEndorsementByEndoNum(this.TaskControlID);

                double totprem = 0.0;
                if (dt.Rows.Count > 0)
                    totprem = (double)dt.Rows[dt.Rows.Count - 1]["ActualPremTotal"];
                else
                    totprem = this.TotalPremium;

                NewProRataTotPrem = Math.Round(totprem * mFactor, 0);
                //NewShotRateTotPrem = Math.Round(NewProRataTotPrem * 0.9, 0);
                NewShotRateTotPrem = Math.Round(totprem * mFactor, 0);

                this.ReturnCharge = 0.00;
                this.CancellationUnearnedPercent = mFactor;

                if (canMethod == 1)//Prorata
                {
                    this.ReturnPremium = NewProRataTotPrem;
                    this.CancellationAmount = NewProRataTotPrem;
                    //this.CancellationUnearnedPercent = mFactor;
                }
                else
                {
                    this.ReturnPremium = NewShotRateTotPrem;
                    this.CancellationAmount = NewShotRateTotPrem;
                    //this.CancellationUnearnedPercent = .90;
                }
            }
        }

        private  DataTable GetEndorsementByEndoNum(int TaskControlID)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("OPPTaskControlID",
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

                DataTable dt = exec.GetQuery("GetOPPEndorsementByEndoNum", xmlDoc);
                return dt;
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error, Please try again. " + xcp.Message, xcp);
            }
        }
        public void CalculateCancellationVSC(DateTime canDate, int canMethod, int canReason)
        {
            string cancellationMethod = LookupTables.LookupTables.GetDescription("CancellationMethod", canMethod.ToString());

            if ((cancellationMethod == "Flat"))
            {
                this.CancellationDate = this.EffectiveDate;// canDate.ToShortDateString();
                this.CancellationMethod = 3;				  //Siempre es FLAT.
                this.CancellationReason = 7;                  // Flat Cancel.
                this.CancellationUnearnedPercent = 0.00;
                this.ReturnCharge = 0.00;  //Cancelacion es FLAT.
                this.ReturnPremium = this.TotalPremium;
                this.CancellationAmount = this.TotalPremium;  //Cancelacion es FLAT.

            }
            else
            {
                this.CancellationDate = canDate.ToShortDateString();
                this.CancellationMethod = canMethod;				 
                this.CancellationReason = canReason;    
            }
        }

        public void CalculateCancellationETCH(DateTime canDate, int canMethod, int canReason)
        {
            // SOLO PARA FLAT
            this.CancellationDate = this.EffectiveDate;// canDate.ToShortDateString();
            this.CancellationMethod = 3;				  //Siempre es FLAT.
            this.CancellationReason = 7;                  // Flat Cancel.
            this.CancellationUnearnedPercent = 0.00;
            this.ReturnCharge = 0.00;  //Cancelacion es FLAT.
            this.ReturnPremium = this.TotalPremium;
            this.CancellationAmount = this.TotalPremium;  //Cancelacion es FLAT.
        }

        public void CalculateCancellationOSO(DateTime canDate, int canMethod, int canReason)
        {
            // SOLO PARA FLAT
            this.CancellationDate = this.EffectiveDate;// canDate.ToShortDateString();
            this.CancellationMethod = 3;				  //Siempre es FLAT.
            this.CancellationReason = 7;                  // Flat Cancel.
            this.CancellationUnearnedPercent = 0.00;
            this.ReturnCharge = 0.00;  //Cancelacion es FLAT.
            this.ReturnPremium = this.TotalPremium;
            this.CancellationAmount = this.TotalPremium;  //Cancelacion es FLAT.
        }
        
		public void CalculateCancellationAutoGap(DateTime canDate, int canMethod, int canReason)
		{
            string cancellationMethod = LookupTables.LookupTables.GetDescription("CancellationMethod", canMethod.ToString());

            if ((cancellationMethod == "Flat"))
            {
                this.CancellationDate = this.EffectiveDate;// canDate.ToShortDateString();
                this.CancellationMethod = 3;				  //Siempre es FLAT.
                this.CancellationReason = 7;                  // Flat Cancel.
                this.CancellationUnearnedPercent = 0.00;
                this.ReturnCharge = 0.00;  //Cancelacion es FLAT.
                this.ReturnPremium = this.TotalPremium;
                this.CancellationAmount = this.TotalPremium;  //Cancelacion es FLAT.

            }
            else
            {
                double MFACTOR1 = 0.00;
                double MFACTOR2 = 0.00;
                double mFACTOR_1 = 0.00;
                double MDIFF1 = 0.00;
                double MDIFF2 = 0.00;
                double MPUNTOS = 0.00;

                if (canDate < DateTime.Parse(this.EffectiveDate))
                    throw new ArgumentException("Cancellation date cannot be neither prior " +
                        "to inception date nor later to expiration date");

                int mDAYS1 = canDate.DayOfYear - DateTime.Parse(this.EffectiveDate).DayOfYear;
                if (mDAYS1 == 366)
                    mDAYS1 = 355;

                int mMESES1 = ((canDate.Month - DateTime.Parse(this.EffectiveDate).Month) + (canDate.Year - DateTime.Parse(this.EffectiveDate).Year) * 12);

                if (canDate.Day > DateTime.Parse(this.EffectiveDate).Day)
                    mMESES1 = mMESES1 + 1;

                if (this.Term > 1 && this.Term < 12)
                {
                    MFACTOR1 = GetFactorAutoGap(1, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(12, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term;
                    MDIFF2 = 12 - 1;
                }

                if (this.Term > 12 && this.Term < 18)
                {
                    MFACTOR1 = GetFactorAutoGap(12, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(18, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 12;
                    MDIFF2 = 18 - 12;
                }

                if (this.Term > 18 && this.Term < 24)
                {
                    MFACTOR1 = GetFactorAutoGap(16, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(24, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 18;
                    MDIFF2 = 24 - 18;
                }

                if (this.Term > 24 && this.Term < 30)
                {
                    MFACTOR1 = GetFactorAutoGap(24, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(30, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 24;
                    MDIFF2 = 30 - 24;
                }

                if (this.Term > 30 && this.Term < 36)
                {
                    MFACTOR1 = GetFactorAutoGap(30, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(36, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 30;
                    MDIFF2 = 36 - 30;
                }

                if (this.Term > 36 && this.Term < 48)
                {
                    MFACTOR1 = GetFactorAutoGap(36, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(48, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 36;
                    MDIFF2 = 48 - 36;
                }

                if (this.Term > 48 && this.Term < 60)
                {
                    MFACTOR1 = GetFactorAutoGap(48, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(60, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 48;
                    MDIFF2 = 60 - 48;
                }

                if (this.Term > 60 && this.Term < 72)
                {
                    MFACTOR1 = GetFactorAutoGap(60, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(72, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 60;
                    MDIFF2 = 72 - 60;
                }

                if (this.Term > 72 && this.Term < 84)
                {
                    MFACTOR1 = GetFactorAutoGap(72, mDAYS1, mMESES1, canMethod);
                    MFACTOR2 = GetFactorAutoGap(84, mDAYS1, mMESES1, canMethod);
                    MDIFF1 = this.Term - 72;
                    MDIFF2 = 84 - 72;
                }

                if (MFACTOR1 != 0)
                {
                    // se esta modificando 10.19.2000
                    // para calcular los terminos irregulares

                    //  mdiff1  = difencia de meses por encima del termino inferior
                    //  mpuntos = factores de los terminos del range

                    MPUNTOS = MFACTOR1 - MFACTOR2;

                    double FACTOR = MPUNTOS / 12 * MDIFF1;
                    double MFACTOR = Math.Round(MFACTOR1 - FACTOR, 0);

                    double mPRIMA_GAN = (MFACTOR * .01 * this.TotalPremium);
                    double mREB_SEG = this.TotalPremium - mPRIMA_GAN;

                    this.ReturnCharge = 0.00;
                    this.ReturnPremium = mREB_SEG;
                    this.CancellationAmount = mREB_SEG;
                }
                else
                {
                    if (canMethod == 2) //ShortRate
                    {
                        if (mDAYS1 <= 365)
                        {
                            //MKEY = STR(MDAYS1,3)+STR(MTERMINO,2)
                            mFACTOR_1 = GetFactorFromShortRate(mDAYS1, this.Term);
                        }
                        else
                        {
                            //MKEY = STR(MMESES1,2)+STR(MTERMINO,2)
                            mFACTOR_1 = GetFactorFromShortRateMO(mMESES1, this.Term);
                        }
                    }
                    else //canMethod == 1 Pro Rata
                    {
                        if (mDAYS1 <= 365)
                        {
                            //MKEY = STR(MDAYS1,3)+STR(MTERMINO,2)
                            mFACTOR_1 = GetFactorFromProRate(mDAYS1, this.Term);
                        }
                        else
                        {
                            //MKEY = STR(MMESES1,2)+STR(MTERMINO,2)
                            mFACTOR_1 = GetFactorFromShortRateMO(mMESES1, this.Term);
                        }
                    }
                    double mPRIMA_GAN = (mFACTOR_1 * .01) * this.TotalPremium;
                    double mREB_SEG = this.TotalPremium - mPRIMA_GAN;

                    this.ReturnCharge = 0.00;
                    this.ReturnPremium = mREB_SEG;
                    this.CancellationAmount = mREB_SEG;
                }
            }
		}

		private double GetFactorAutoGap(int term,int MDIAS,int mMESES1,int canMethod)
		{
			if (canMethod == 2) //ShortRate
			{
				if (MDIAS <= 365)
				{
					//MKEY = STR(MDIAS,3)+STR(term,2)
					double mFACTOR_1 = GetFactorFromShortRate(MDIAS,term);
					return mFACTOR_1;
				}
				else
				{
					//MKEY = STR(mMESES1,2)+STR(term,2)
					double mFACTOR_1 = GetFactorFromShortRateMO(mMESES1, this.Term);
					return mFACTOR_1;
				}
			}
			else //canMethod == 1 Pro Rata
			{
				if(MDIAS <= 365)
				{
					//MKEY = STR(MDIAS,3)+STR(term,2)
					double mFACTOR_1 = GetFactorFromProRate(MDIAS,this.Term);
					return mFACTOR_1;
				}
				else
				{
					//MKEY = STR(mMESES1,2)+STR(term,2)
					double mFACTOR_1 = GetFactorFromShortRateMO(mMESES1, this.Term);
					return mFACTOR_1;
				}
			}
		}

		private double GetFactorFromShortRate(int mdias, int term)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("Time_Forc",
				SqlDbType.Int, 0, mdias.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Term_Mons",
				SqlDbType.Int, 0, term.ToString(),
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
	
			DataTable dt = exec.GetQuery("GetFactorFromShortRate",xmlDoc);

			if(dt.Rows.Count > 0)
			{
				return (double) dt.Rows[0]["Factor"];

			}
			else
			{			
				return 0.00;
			}
		}

		private double GetFactorFromProRate(int mdias, int term)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("Time_Forc",
				SqlDbType.Int, 0, mdias.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Term_Mons",
				SqlDbType.Int, 0, term.ToString(),
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
	
			DataTable dt = exec.GetQuery("GetFactorFromProRata",xmlDoc);

			if(dt.Rows.Count > 0)
			{
				int fact =  (int) dt.Rows[0]["Factor"];
				return double.Parse(fact.ToString());
			}
			else
			{			
				return 0.00;
			}
		}

		private double GetFactorFromShortRateMO(int mdias, int term)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("Time_Forc",
				SqlDbType.Int, 0, mdias.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Term_Mons",
				SqlDbType.Int, 0, term.ToString(),
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
	
			DataTable dt = exec.GetQuery("GetFactorFromShortRateMO",xmlDoc);

			if(dt.Rows.Count > 0)
			{
				return (double) dt.Rows[0]["Factor"];

			}
			else
			{			
				return 0.00;
			}
		}

		private void CalculateCancellationEtch(DateTime canDate, int canMethod, int canReason)
		{
			this.CancellationDate	= canDate.ToShortDateString();
			this.CancellationMethod = 3;				  //Siempre es FLAT.
			this.CancellationReason = 7;                  // Flat Cancel.
			this.CancellationUnearnedPercent = 0.00;
			this.ReturnCharge	    = this.Charge;  //Cancelacion es FLAT.
			this.ReturnPremium		= this.TotalPremium;
			this.CancellationAmount = this.Charge+this.TotalPremium;  //Cancelacion es FLAT.
		}
		
		private void CalculateCancellationAutoGuard(DateTime cancellationDate, int cancellationMethodID, int cancellationReasonID)
		{
			DateTime effectiveDate  = DateTime.Parse(this.EffectiveDate);
			DateTime expirationDate = DateTime.Parse(this.ExpirationDate);

			string 	cancellationMethod = LookupTables.LookupTables.GetDescription("CancellationMethod",cancellationMethodID.ToString());
			string 	cancellationReason = LookupTables.LookupTables.GetDescription("CancellationReason",cancellationReasonID.ToString());

			if (cancellationDate < effectiveDate || cancellationDate > expirationDate)
			{
				throw new ArgumentException("Cancellation date cannot be neither prior "+
					"to inception date nor later to expiration date");
			}

			double returnPremium = 0;
			double returnCharge  = 0;
			double factor		 = 0;

			if ((cancellationMethod == "Flat") || (cancellationDate == effectiveDate))
			{
				cancellationMethodID = 3;  //Siempre es FLAT.
				returnPremium = this.TotalPremium;
				returnCharge  = this.Charge;
			}
			else
			{
				int daysTotal = (expirationDate - effectiveDate).Days;
				int days      = (expirationDate - cancellationDate).Days;

				factor = Math.Round((double) days/daysTotal,3);
				factor *= ((cancellationMethod == "ShortRate")?0.90:1);

				returnPremium = (double) Math.Round(this.TotalPremium * factor,1);
				returnCharge  =(double) Math.Round(this.Charge*(returnPremium/this.TotalPremium),1);
			}

			this.CancellationDate	= cancellationDate.ToShortDateString();
			this.CancellationMethod = cancellationMethodID;		
			this.CancellationReason = cancellationReasonID;
			this.CancellationUnearnedPercent =factor;
			this.ReturnCharge	    = Math.Round(returnCharge,0); 
			this.ReturnPremium		= Math.Round(returnPremium,0);
			this.CancellationAmount = Math.Round(returnPremium,0)+Math.Round(returnCharge,0); 
			this.CancellationEntryDate = DateTime.Now.ToShortDateString();

			//			this.CancellationMethodReadySave		  = cancellationMethodID;
			//			this.CancellationReasonReadySave		  = cancellationReasonID;
			//			this.CancellationDateReadySave			  = cancellationDate.ToShortDateString();
			//			this.CancellationUnearnedPercentReadySave = factor;
			//			this.CancellationReturnPremiumReadySave   = returnPremium;
			//			this.CancellationReturnChargeReadySave    = returnCharge;
			//			this.CancellationAmountReadySave          = returnPremium+returnCharge;
		}

		#endregion

		public void SaveCancellation(int UserID)
		{
            //if(this.PolicyClassID != 1) //No aplica para VSC
			    

//			switch(this.PolicyClassID) 
//			{	
//				case 3:				//AutoPersonal
//					break;
//				
//				case 9:				//AutoGap
//					SaveCancellationAutoGap();
//					break;
//
//				case 10:			//Etch
//					SaveCancellationEtch();
//					break;
//			}

            SaveCancellationPolicies(); //Salva los cambios en PolicyCommission.
			SaveCancellationPolicy(UserID);  //Salva los cambios en la tabla de Policy.

            //Insertar transaccion de pago si la poliza tiene pago.
            if ((this.PaidAmount > 0) && !PaymentPolicy.HasCancellationTransaction(this.TaskControlID))
            {
                PaymentPolicy pp = new PaymentPolicy();
                pp.InsertCancellationPayment(this);
                PaymentPolicy.UpdatePolicyPaidAmount(this.TaskControlID, this.PaidAmount + (this.CancellationAmount));
            }
		}

		public void SaveCancellationPolicy(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

			try
			{
				Executor.Update("UpdateCancellation",this.GetUpdateCancellationXml());

				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Cancellation. "+xcp.Message,xcp);
			}		
		}

		public void SaveCancellationPolicies()
		{
			//Calculate the return commission.
			this.ReturnCommissionAgency = this.ReturnPremium * (double.Parse(this.CommissionAgencyPercent)/1000);
			//this.ReturnCommissionAgent  =this.ReturnPremium * MRATEAG;	

			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.Update("UpdatePolicyCommissionForPolicies",this.ApplyReturnCommissionAgent());
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Cancellation. "+xcp.Message,xcp);
			}		
		}

		public void SaveCancellationEtch()
		{
			//Calculate the return commission.
			this.ReturnCommissionAgency = 0.00;
			this.ReturnCommissionAgent  = 0.00;

			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.Update("UpdatePolicyIncentiveForEtch",this.ApplyReturnIncentiveSupplier());
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Cancellation. "+xcp.Message,xcp);
			}		
		}

		public static DataTable GetPoliciesByCustomerNo(string CustomerNo)
		{
			DataTable dt = GetPoliciesByCustomerNoDB(CustomerNo);

			return dt;
		}

		public static Policy GetPolicyByTaskControlID(int TaskControlID, Policy _policy)
		{
			Policy policy = _policy; 

			DataTable dt= GetPolicyByTaskControlIDDB(TaskControlID);

			policy._dtPolicy = dt;
			policy = FillProperties(policy);

			return policy;
		}
		
		public static Policy GetPolicyByTaskControlID(int TaskControlID, Policy pol, bool _audit)
		{	
			DataTable dt= GetPolicyByTaskControlIDDB(TaskControlID);

			pol._dtPolicy = dt;
			pol = FillProperties(pol);

			return pol;
		}

		public virtual void ValidatePolicy()
		{
			string errorMessage = String.Empty;

            if (this.PolicyClassID != 25)
            {
                if (this.PolicyClassID != 1 && this.PolicyClassID != 13 && this.PolicyClassID != 16 && this.PolicyClassID != 17) //No aplica para VSC y Etch, QCertified, UltProt
                {
                    if (this.Agency == "")
                        errorMessage = "Agency missing or wrong.";
                }

                if (this.PolicyClassID != 1 && this.PolicyClassID != 13 && this.PolicyClassID != 14 && this.PolicyClassID != 16 && this.PolicyClassID != 17 && this.PolicyClassID != 21) //No aplica para VSC y Etch, QCertified, UltProt
                {
                    if (this.Agent == "" || this.Agent == "000")
                        errorMessage = "Agent missing or wrong.";
                }

                if (this.InsuranceCompany == "")
                    errorMessage = "InsuranceCompany missing or wrong.";
                //			else
                //				if (this.Customer.CustomerNo == "")
                //				errorMessage = "Customer missing or wrong.";
                else
                    if (this.Term <= 0)
                    errorMessage = "Term must be greater than 0.";

                //if (this.PolicyClassID == 1) //No aplica para VSC
                //{
                //    if (this.SupplierID == "000")
                //        errorMessage = "Supplier missing or wrong.";
                //}

                //throw the exception.
                if (errorMessage != String.Empty)
                {
                    throw new Exception(errorMessage);
                }

                if (this._mode == 1)                // Verifica en ADD.
                {
                    if (this.PolicyClassID != 1 && this.PolicyClassID != 3 && this.PolicyClassID != 13 && this.PolicyClassID != 14 && this.PolicyClassID != 15 && this.PolicyClassID != 16 && this.PolicyClassID != 17 && this.PolicyClassID != 21) //No aplica para VSC y etch, OSO, QCertified, UltProt
                    {
                        //this.VerifyAgencyPercent();				 //Verify Agency And Commissions.
                        //this.VerifyAgentPercentByPolicyClass();  //Verify Agent And Commissions.
                    }
                }

                if (this.SupplierID.Trim() != "000")
                {
                    //Verifica que los niveles de Supplier sean consecutivos.
                    if (this.LbxSupplierSelected != null)
                    {
                        if (this.LbxSupplierSelected.Items.Count != 0)
                        {
                            bool LevelError = false;
                            for (int i = 0; this.LbxSupplierSelected.Items.Count - 1 >= i; i++)
                            {
                                if (int.Parse(this.LbxSupplierSelected.Items[i].Value.Split("|".ToCharArray())[0]) != i + 1 && !LevelError)
                                {
                                    LevelError = true;
                                    int level = i + 1;
                                    errorMessage = "The Supplier level " +
                                        level.ToString().Trim() +
                                        " is required, Please verify...";
                                }
                            }
                        }
                    }
                }

                //Verifica que los niveles de agentes sean consecutivos.
                if (this.Agent.Trim() != "000")
                {
                    if (this.LbxAgentSelected != null)
                    {
                        if (this.LbxAgentSelected.Items.Count != 0)
                        {
                            bool LevelError = false;
                            for (int i = 0; this.LbxAgentSelected.Items.Count - 1 >= i; i++)
                            {
                                if (int.Parse(this.LbxAgentSelected.Items[i].Value.Split("|".ToCharArray())[0]) != i + 1 && !LevelError)
                                {
                                    LevelError = true;
                                    int level = i + 1;
                                    errorMessage = "The Agent level " +
                                        level.ToString().Trim() +
                                        " is required, Please verify...";
                                }
                            }
                        }
                    }
                }

                //throw the exception.
                if (errorMessage != String.Empty)
                {
                    throw new Exception(errorMessage);
                }
            }
            else
            {
                if (Customer.AddressPhysical1 == "")
                {
                    errorMessage = "Physical Address is missing or wrong.";
                }

                if (Customer.CityPhysical == "")
                {
                    errorMessage = "Physical City is missing or wrong.";
                }

                if (Customer.StatePhysical == "")
                {
                    errorMessage = "State is missing or wrong.";
                }

                if (Customer.ZipPhysical == "")
                {
                    errorMessage = "Zipcode is missing or wrong.";
                }
                if (errorMessage != String.Empty)
                {
                    throw new Exception(errorMessage);
                }
            }
		}

		public void AddReportPrintList(string reportDescription, string reportFileName)
		{
			DataRow myRow;			

			//this._dtPrintPolicyReady    = this.PrintPolicyList.Clone();
			myRow = this._dtPrintPolicyReady.NewRow();
	
			myRow["PolicyReportID"]   = "0";
			myRow["ReportFileName"]   = reportFileName;
			myRow["ReportDescription"]= reportDescription;
			myRow["PrintJustOnce"]    = false;

			this._dtPrintPolicyReady.Rows.Add(myRow);
			this._dtPrintPolicyReady.AcceptChanges();
		}

		public void AddReportCopyList(string reportCopyName, string reportCopyFooter)
		{	
			DataRow myRow;

			//this._dtPrintCopyReady    = this.PrintCopyList.Clone();
			myRow = this._dtPrintCopyReady.NewRow();
				
			myRow["PolicyReportID"]   = "0";
			myRow["ReportCopyName"]   = reportCopyName;
			myRow["ReportCopyFooter"] = reportCopyFooter;

			this._dtPrintCopyReady.Rows.Add(myRow);
			this._dtPrintCopyReady.AcceptChanges();
		}


        public static DataTable GetUnderwritterRulesByUnderwritterRulesID(int UnderwritterRulesID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
           new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("UnderwritterRulesID",
                SqlDbType.Int, 0, UnderwritterRulesID.ToString(),
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

            DataTable dt = exec.GetQuery("GetUnderwritterRulesByUnderwritterRulesID", xmlDoc);
            return dt;
        }

		#endregion

		#region Public Agent Methods

		public static DataTable GetAgentListByPolicyClassID(int policyClassID)
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
	
			DataTable dt = exec.GetQuery("GetAgentListByPolicyClassID",xmlDoc);
			
			return dt;	
		}

		public static DataTable GetSupplierListByPolicyClassID(int policyClassID)
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
	
			DataTable dt = exec.GetQuery("GetSupplierListByPolicyClassID",xmlDoc);
			
			return dt;	
		}

		public static DataTable GetAvailableAgent(int taskcontrolID, int policyClassID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskcontrolID.ToString(),
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
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
	
			DataTable dt = exec.GetQuery("GetAvailableAgentByTaskControlID",xmlDoc);
			
			return dt;	
		}
			
		public static DataTable GetAvailableSupplier(int taskcontrolID, int policyClassID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskcontrolID.ToString(),
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
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
	
			DataTable dt = exec.GetQuery("GetAvailableSupplierByTaskControlID",xmlDoc);
			
			return dt;	
		}

		public static DataTable GetSelectedAgent(int taskcontrolID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskcontrolID.ToString(),
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
	
			DataTable dt = exec.GetQuery("GetSelectedAgentByTaskControlID",xmlDoc);
			return dt;	
		}

		public static DataTable GetSelectedSupplier(int taskcontrolID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskcontrolID.ToString(),
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
	
			DataTable dt = exec.GetQuery("GetSelectedSupplierByTaskControlID",xmlDoc);
			return dt;	
		}

		#endregion

		#region Private Agent Methods

		private void SaveAgent()
		{
			//AgentVerify();

            if(this.Agent != "000")
                SaveAgentDB(0, this.Agent, "0", 1, "0", "Add");
		}

		private void SaveSupplier()
		{
			SupplierVerify();
		}

		private void AgentVerify()
		{
			ListBox lbxSelected = this.LbxAgentSelected;
			DataTable dt = Policy.GetSelectedAgent(this.TaskControlID);
			int policyCommissionID;

			for (int i=0;i<=dt.Rows.Count-1;i++)
			{
				bool exist = false;
				policyCommissionID = 0;
				int rec = 0;
				for (int a =0;a<=lbxSelected.Items.Count-1;a++)
				{
					rec=a;                        
					if(dt.Rows[i]["AgentID"].ToString() == lbxSelected.Items[a].Value.Split("|".ToCharArray())[1] &&
						(int) dt.Rows[i]["CommissionLevel"] == int.Parse(lbxSelected.Items[a].Text.Split("|".ToCharArray())[0]))
					{
						exist = true;
					}
				}				
				if(!exist)
				{					
					if(lbxSelected.Items.Count ==0)
					{
						policyCommissionID = 0;
					}
					else
					{
						policyCommissionID= (int) dt.Rows[i]["PolicyCommissionID"];// int.Parse(lbxSelected.Items[rec].Value);
					}
					SaveAgentDB(policyCommissionID,"","",0,"","Deleted");
				}
			}

			for (int i=0;i<=lbxSelected.Items.Count-1;i++)
			{
				bool exist = false;
				policyCommissionID = 0;
				string agentID	= "";
				int agentLevel	= 0;

				if (this._mode == 1)  // ADD
				{
					agentID		= lbxSelected.Items[i].Value.Split("|".ToCharArray())[1];
					agentLevel	= int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]);
				}
				else				 //EDIT
				{
					int rec = 0;
					for (int a =0;a<=dt.Rows.Count-1;a++)
					{
						rec=a;
						if(dt.Rows[a]["AgentID"].ToString() == lbxSelected.Items[i].Value.Split("|".ToCharArray())[1] &&
							(int) dt.Rows[a]["CommissionLevel"] == int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]))					
						{
							exist = true;
						}
						else 
						{
							agentID		= lbxSelected.Items[i].Value.Split("|".ToCharArray())[1];
							agentLevel	= int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]);
						}
					}
				}
				
				if(!exist)
				{						
					string[] agentinf = VerifyAgentPercentByPolicyClass(agentID, agentLevel); //Se verifica el porciento de comisión de acuerdo a la linea de negocio ya que hay diferenres parametros.
					SaveAgentDB(0,agentID,agentinf[0].ToString(),int.Parse(agentinf[1].ToString()),agentinf[2].ToString(),"Add");
				
					if (agentLevel == 1) // Para guaradar la info. del primer agente en la tabla de Policy.
					{
						this.CommissionAgentPercent = agentinf[0].ToString();
					}
				}
			}
		}

		private void SupplierVerify()
		{
			ListBox lbxSelected = this.LbxSupplierSelected;
			DataTable dt = Policy.GetSelectedSupplier(this.TaskControlID);
			int policyIncentiveID;

			for (int i=0;i<=dt.Rows.Count-1;i++)
			{
				bool exist = false;
				policyIncentiveID = 0;
				int rec = 0;
				for (int a =0;a<=lbxSelected.Items.Count-1;a++)
				{
					rec=a;                        
					if(dt.Rows[i]["SupplierID"].ToString() == lbxSelected.Items[a].Value.Split("|".ToCharArray())[1] &&
						(int) dt.Rows[i]["IncentiveLevel"] == int.Parse(lbxSelected.Items[a].Text.Split("|".ToCharArray())[0]))
					{
						exist = true;
					}
				}				
				if(!exist)
				{					
					if(lbxSelected.Items.Count ==0)
					{
						policyIncentiveID = 0;
					}
					else
					{
						policyIncentiveID= (int) dt.Rows[i]["PolicyIncentiveID"];// int.Parse(lbxSelected.Items[rec].Value);
					}
					SaveSupplierDB(policyIncentiveID,"","",0,"","Deleted");
				}
			}

			for (int i=0;i<=lbxSelected.Items.Count-1;i++)
			{
				bool exist = false;
				policyIncentiveID = 0;
				string supplierID	= "";
				int supplierLevel	= 0;

				if (this._mode == 1)  // ADD
				{
					supplierID		= lbxSelected.Items[i].Value.Split("|".ToCharArray())[1];
					supplierLevel	= int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]);
				}
				else				 //EDIT
				{
					if(dt.Rows.Count !=0)
					{
						int rec = 0;
						for (int a =0;a<=dt.Rows.Count-1;a++)
						{
							rec=a;
							if(dt.Rows[a]["SupplierID"].ToString() == lbxSelected.Items[i].Value.Split("|".ToCharArray())[1] &&
								(int) dt.Rows[a]["IncentiveLevel"] == int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]))					
							{
								exist = true;
							}
							else 
							{
								supplierID		= lbxSelected.Items[i].Value.Split("|".ToCharArray())[1];
								supplierLevel	= int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]);
							}
						}
					}
					else
					{
						supplierID		= lbxSelected.Items[i].Value.Split("|".ToCharArray())[1];
						supplierLevel	= int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]);
					}
				}
				
				if(!exist)
				{
                    string[] supplierinf;
                    if (this.PolicyClassID == 1) //VSC - El supplierLevel va hacer siempre uno.
                    {
                       supplierinf = VerifySupplierPercentByPolicyClass(supplierID,1); //Se verifica el porciento de comisión de acuerdo a la linea de negocio ya que hay diferenres parametros.
                    }
                    else
                    {
                       supplierinf = VerifySupplierPercentByPolicyClass(supplierID, supplierLevel); //Se verifica el porciento de comisión de acuerdo a la linea de negocio ya que hay diferenres parametros.
                    }
                    
                    SaveSupplierDB(0,supplierID,supplierinf[0].ToString(),int.Parse(supplierinf[1].ToString()),supplierinf[2].ToString(),"Add");
				
					if (supplierLevel == 1) // Para guaradar la info. del primer agente en la tabla de Policy.
					{
						//this.CommissionAgentPercent = supplierinf[0].ToString();
					}
				}
			}
		}

		private void SaveAgentDB(int policyCommissionID,string AgentID,string CommissionAgentPercent, int CommissionLevel, string CommissionAmount, string mode)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (mode)
				{
					case "Add":  
						int polCommissionID = Executor.Insert("AddPolicyCommission",this.GetInsertPolicyCommissionXml(AgentID,CommissionAgentPercent,CommissionAmount,CommissionLevel));
						break;
					case "Deleted":  
						Executor.Delete("DeletePolicyCommission",this.GetDeletePolicyCommissionXml(policyCommissionID));
						break;
				}
						
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the User. "+xcp.Message,xcp);
			}
		}

		private void SaveSupplierDB(int policyIncentiveID,string SupplierID,string IncentiveSupplierPercent, int IncentiveLevel, string IncentiveAmount, string mode)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (mode)
				{
					case "Add":  
						int polIncentiveID = Executor.Insert("AddPolicyIncentive",this.GetInsertPolicyIncentiveXml(SupplierID,IncentiveSupplierPercent,IncentiveAmount,IncentiveLevel));
						break;
					case "Deleted":  
						Executor.Delete("DeletePolicyIncentive",this.GetDeletePolicyIncentiveXml(policyIncentiveID));
						break;
				}
						
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the policy incentive. "+xcp.Message,xcp);
			}
		}

		private XmlDocument GetInsertPolicyCommissionXml(string AgentID,string CommissionAgentPercent,string CommissionAmount,int CommissionLevel)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAgentID",
				SqlDbType.Char, 3, AgentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAgentPercent",
				SqlDbType.Char, 3, CommissionAgentPercent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionLevel",
				SqlDbType.Int, 0, CommissionLevel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TotalPremium",
				SqlDbType.Float, 8, this.TotalPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAmount",
				SqlDbType.Decimal, 9, CommissionAmount.ToString(),
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

		private XmlDocument GetInsertPolicyIncentiveXml(string SupplierID,string IncentiveSupplierPercent,string IncentiveAmount,int IncentiveLevel)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveSupplierID",
				SqlDbType.Char, 3, SupplierID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveSupplierPercent",
				SqlDbType.Char, 3, IncentiveSupplierPercent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveLevel",
				SqlDbType.Int, 0, IncentiveLevel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TotalPremium",
				SqlDbType.Float, 8, this.TotalPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveAmount",
				SqlDbType.Decimal, 9, IncentiveAmount.ToString(),
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

		private XmlDocument GetDeletePolicyCommissionXml(int policyCommissionID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("PolicyCommissionID",
				SqlDbType.Int, 0, policyCommissionID.ToString(),
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

		private void DeleteSupplierByTaskControlID()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                exec.BeginTrans();
				exec.Delete("DeletePolicyIncentiveByTaskControlID",xmlDoc);
                exec.CommitTrans();
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}			
		}

        private void DeleteAgentByTaskControlID()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                exec.BeginTrans();
                exec.Delete("DeletePolicyCommissionByTaskControlID", xmlDoc);
                exec.CommitTrans();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

		private XmlDocument GetDeletePolicyIncentiveXml(int policyIncentiveID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("PolicyIncentiveID",
				SqlDbType.Int, 0, policyIncentiveID.ToString(),
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

		private string[] VerifyAgentPercentByPolicyClass(string agentID, int agentLevel)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[5];

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.Char, 3, agentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, this.PolicyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.Char, 3, this.InsuranceCompany.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgentLevel",
				SqlDbType.Int, 0, agentLevel.ToString(),
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

			DataTable dt = exec.GetQuery("GetCommissionAgentByCriteria",xmlDoc);

			if (dt.Rows.Count != 0)
			{
				string commissionAgentPercent="";
				string agentLev="";
				string commissionAmount="";

				for (int i=0;dt.Rows.Count >=i;i++)
				{
					if ((DateTime)dt.Rows[i]["EffectiveDate"] <= DateTime.Parse(this.EffectiveDate))
					{
						commissionAgentPercent = dt.Rows[0]["CommissionRate"].ToString();
						agentLev			   = dt.Rows[0]["AgentLevel"].ToString();
						commissionAmount       = dt.Rows[0]["CommissionAmount"].ToString();
						i = dt.Rows.Count;
					}
				}	
				string[] agentcomm = new string[3];
				agentcomm[0] = commissionAgentPercent;
				agentcomm[1] = agentLev;
				agentcomm[2] = commissionAmount;

				return agentcomm;
			}
			else
			{
				throw new Exception("Sorry, Commission not found for this Agent(Agent ID-"+agentID+").");
			}
		}

		private void VerifyAgentPercentByPolicyClass()
		{
			ListBox lbxSelected = this.LbxAgentSelected;
			for (int i=0;i<=lbxSelected.Items.Count-1;i++)
			{
				bool AgentNotExist = true;
				string agentID	= "";
				int agentLevel	= 0;

				agentID		= lbxSelected.Items[i].Value.Split("|".ToCharArray())[1];
				agentLevel	= int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]);
						
				///////////////////////////////////////
				DbRequestXmlCookRequestItem[] cookItems = 
					new DbRequestXmlCookRequestItem[5];

				DbRequestXmlCooker.AttachCookItem("PolicyClassID",
					SqlDbType.Int, 0, this.PolicyClassID.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("AgentID",
					SqlDbType.Char, 3, agentID.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("PolicyType",
					SqlDbType.Char, 3, this.PolicyType.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
					SqlDbType.Char, 3, this.InsuranceCompany.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("AgentLevel",
					SqlDbType.Int, 0, agentLevel.ToString(),
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

				DataTable dt = exec.GetQuery("GetCommissionAgentByCriteria",xmlDoc);

				if (dt.Rows.Count != 0)
				{
					for (int a=0;dt.Rows.Count-1 >=a;a++)
					{
						if ((DateTime)dt.Rows[a]["EffectiveDate"] <= DateTime.Parse(this.EffectiveDate))
						{
							AgentNotExist = false;
						}
					}	
				}
				else
				{
					throw new Exception("Sorry, Commission not found for this Agent(Agent ID-"+agentID+").");
				}

				if (AgentNotExist)
				{
					throw new Exception("Sorry, Commission not found for this Agent(Agent ID-"+agentID+").");
				}
			}
		}

		private string[] VerifySupplierPercentByPolicyClass(string supplierID, int supplierLevel)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[5];

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 3, supplierID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, this.PolicyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.Char, 3, this.InsuranceCompany.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierLevel",
				SqlDbType.Int, 0, supplierLevel.ToString(),
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

			DataTable dt = exec.GetQuery("GetIncentiveSupplierByCriteria",xmlDoc);

			if (dt.Rows.Count != 0)
			{
				string incentiveSupplierPercent="";
				string supplierLev="";
				string incentiveAmount="";

				for (int i=0;dt.Rows.Count >=i;i++)
				{
					if ((DateTime)dt.Rows[i]["EffectiveDate"] <= DateTime.Parse(this.EffectiveDate))
					{
						incentiveSupplierPercent = dt.Rows[0]["IncentiveRate"].ToString();
						supplierLev			     = dt.Rows[0]["SupplierLevel"].ToString();
						incentiveAmount          = dt.Rows[0]["IncentiveAmount"].ToString();
						i = dt.Rows.Count;
					}
				}	
				string[] suppliercomm = new string[3];
				suppliercomm[0] = incentiveSupplierPercent;
				suppliercomm[1] = supplierLev;
				suppliercomm[2] = incentiveAmount;

				return suppliercomm;
			}
			else
			{
				throw new Exception("Sorry, Incentive not found for this Supplier(Supplier ID-"+supplierID+").");
			}
		}

		private void VerifySupplierPercentByPolicyClass()
		{
			ListBox lbxSelected = this.LbxSupplierSelected;
			for (int i=0;i<=lbxSelected.Items.Count-1;i++)
			{
				bool SupplierNotExist = true;
				string supplierID	= "";
				int supplierLevel	= 0;

				supplierID		= lbxSelected.Items[i].Value.Split("|".ToCharArray())[1];
				supplierLevel	= int.Parse(lbxSelected.Items[i].Text.Split("|".ToCharArray())[0]);
						
				///////////////////////////////////////
				DbRequestXmlCookRequestItem[] cookItems = 
					new DbRequestXmlCookRequestItem[5];

				DbRequestXmlCooker.AttachCookItem("PolicyClassID",
					SqlDbType.Int, 0, this.PolicyClassID.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("SupplierID",
					SqlDbType.Char, 3, supplierID.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("PolicyType",
					SqlDbType.Char, 3, this.PolicyType.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
					SqlDbType.Char, 3, this.InsuranceCompany.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("SupplierLevel",
					SqlDbType.Int, 0, supplierLevel.ToString(),
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

				DataTable dt = exec.GetQuery("GetIncentiveSupplierByCriteria",xmlDoc);

				if (dt.Rows.Count != 0)
				{
					for (int a=0;dt.Rows.Count-1 >=a;a++)
					{
						if ((DateTime)dt.Rows[a]["EffectiveDate"] <= DateTime.Parse(this.EffectiveDate))
						{
							SupplierNotExist = false;
						}
					}	
				}
				else
				{
					throw new Exception("Sorry, Incentive not found for this Supplier(Supplier ID-"+supplierID+").");
				}

				if (SupplierNotExist)
				{
					throw new Exception("Sorry, Incentive not found for this Supplier(Supplier ID-"+supplierID+").");
				}
			}
		}

		#endregion

		#region Private Agency Methods

		private void VerifyAgencyPercent()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[4];

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.Char, 3, this.Agency.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, this.PolicyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.Char, 3, this.InsuranceCompany.ToString(),
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

			DataTable dt = exec.GetQuery("GetCommissionAgencyByCriteria",xmlDoc);

			bool AgentNotExist = true;
			if (dt.Rows.Count != 0)
			{
				for (int i=0;dt.Rows.Count-1 >=i;i++)
				{
					if ((DateTime)dt.Rows[i]["EffectiveDate"] <= DateTime.Parse(this.EffectiveDate))
					{
						this.CommissionAgencyPercent = dt.Rows[0]["CommissionRate"].ToString();
						i = dt.Rows.Count;
						AgentNotExist = false;
					}
				}			
			}
			else
			{
				throw new Exception("Sorry, Commission not found for this Agency(Agency ID-"+this.Agency+").\r\n"+
					"Please Verify The Policy Type and the Insurance Company.");
			}

			if (AgentNotExist)
			{
				throw new Exception("Sorry, Commission not found for this Agency(Agency ID-"+this.Agency+").\r\n"+
					"Please Verify The Policy Type and the Insurance Company.");
			}
		}

		#endregion

		#region Private Methods

		private XmlDocument ApplyReturnCommissionAgent()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnComm",
				SqlDbType.Float, 0, this.ReturnPremium.ToString(),
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

		private XmlDocument ApplyReturnIncentiveSupplier()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
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

		public static Policy GetPrintPolicyReport(Policy policy)
		{
			TaskControl tc = (TaskControl) policy;
			try
			{
				DbRequestXmlCookRequestItem[] cookItems = 
					new DbRequestXmlCookRequestItem[4];

				DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
					SqlDbType.Char, 3, policy.InsuranceCompany.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("PolicyClassID",
					SqlDbType.Int, 0, policy.PolicyClassID.ToString(),
					ref cookItems);

				DbRequestXmlCooker.AttachCookItem("AutoPolicyType",
					SqlDbType.Char, 15, policy.AutoType.ToString(),
					ref cookItems);				

				string mastpol = "0";
				if (policy.MasterPolicyID.Trim() != "")
				{
					mastpol = policy.MasterPolicyID.Trim();
				}
			
				DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
					SqlDbType.Char, 4, mastpol.ToString(),
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
				
				DataTable dtPrintPolicy = exec.GetQuery("GetPolicyReportHeader",xmlDoc);
	
			
				if (dtPrintPolicy.Rows.Count > 0)
				{
					cookItems = new DbRequestXmlCookRequestItem[1];

					DbRequestXmlCooker.AttachCookItem("PolicyReportID",
						SqlDbType.Int, 0, dtPrintPolicy.Rows[0]["PolicyReportID"].ToString(),
						ref cookItems);
					exec = new Baldrich.DBRequest.DBRequest();					

					try
					{
						xmlDoc = DbRequestXmlCooker.Cook(cookItems);
					}
					catch(Exception ex)
					{
						throw new Exception("Could not cook items.", ex);
					}
		
					exec = new Baldrich.DBRequest.DBRequest();
					
					policy._dtPrintCopyList     = exec.GetQuery("GetPolicyReportCopy",xmlDoc);

					// Get the copy list for the polices.
					DataTable DtPrintPolicyList  = exec.GetQuery("GetPolicyReport",xmlDoc);

					
					// Get the policies list for the polices.
					DataRow[] drPrintPolicyList   = DtPrintPolicyList.Select("PrintJustOnce = 0");
					policy._dtPrintPolicyList = DtPrintPolicyList.Clone();
					DataRow myRow;
					for (int i = 0; i <= drPrintPolicyList.Length -1; i++)
					{
						myRow = policy._dtPrintPolicyList.NewRow();
						myRow["PolicyReportID"]   = drPrintPolicyList[i].ItemArray[0].ToString();
						myRow["ReportFileName"]   = drPrintPolicyList[i].ItemArray[1].ToString();
						myRow["ReportDescription"]= drPrintPolicyList[i].ItemArray[2].ToString();
						myRow["PrintJustOnce"]    = drPrintPolicyList[i].ItemArray[3].ToString();
						policy._dtPrintPolicyList.Rows.Add(myRow);
						policy._dtPrintPolicyList.AcceptChanges();
					}

					// Get the Document that that print just once.
					DataRow[] drPrintJustOnceList  = DtPrintPolicyList.Select("PrintJustOnce = 1");
					policy._dtPrintJustOnceList    = DtPrintPolicyList.Clone();
					myRow = null;
					for (int i = 0; i <= drPrintJustOnceList.Length -1; i++)
					{
						myRow = policy._dtPrintJustOnceList.NewRow();
						myRow["PolicyReportID"]   = drPrintJustOnceList[i].ItemArray[0].ToString();
						myRow["ReportFileName"]   = drPrintJustOnceList[i].ItemArray[1].ToString();
						myRow["ReportDescription"]= drPrintJustOnceList[i].ItemArray[2].ToString();
						myRow["PrintJustOnce"]    = drPrintJustOnceList[i].ItemArray[3].ToString();
						policy._dtPrintJustOnceList.Rows.Add(myRow);
						policy._dtPrintJustOnceList.AcceptChanges();
					}
				}
			}
			catch (Exception xcp)
			{
				throw new Exception("Could not get the policy report." ,xcp);
			}

			return policy;
		}

        public Policy GetPrintPolicyReport(Policy policy,string Interno)
        {
            TaskControl tc = (TaskControl)policy;
            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[4];

                DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
                    SqlDbType.Char, 3, policy.InsuranceCompany.ToString(),
                    ref cookItems);

                DbRequestXmlCooker.AttachCookItem("PolicyClassID",
                    SqlDbType.Int, 0, policy.PolicyClassID.ToString(),
                    ref cookItems);

                DbRequestXmlCooker.AttachCookItem("AutoPolicyType",
                    SqlDbType.Char, 15, policy.AutoType.ToString(),
                    ref cookItems);

                string mastpol = "0";
                if (policy.MasterPolicyID.Trim() != "")
                {
                    mastpol = policy.MasterPolicyID.Trim();
                }

                DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
                    SqlDbType.Char, 4, mastpol.ToString(),
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

                DataTable dtPrintPolicy = exec.GetQuery("GetPolicyReportHeader", xmlDoc);


                if (dtPrintPolicy.Rows.Count > 0)
                {
                    cookItems = new DbRequestXmlCookRequestItem[1];

                    DbRequestXmlCooker.AttachCookItem("PolicyReportID",
                        SqlDbType.Int, 0, dtPrintPolicy.Rows[0]["PolicyReportID"].ToString(),
                        ref cookItems);
                    exec = new Baldrich.DBRequest.DBRequest();

                    try
                    {
                        xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not cook items.", ex);
                    }

                    exec = new Baldrich.DBRequest.DBRequest();

                    policy._dtPrintCopyList = exec.GetQuery("GetPolicyReportCopy", xmlDoc);

                    // Get the copy list for the polices.
                    DataTable DtPrintPolicyList = exec.GetQuery("GetPolicyReport", xmlDoc);


                    // Get the policies list for the polices.
                    DataRow[] drPrintPolicyList = DtPrintPolicyList.Select("PrintJustOnce = 0");
                    policy._dtPrintPolicyList = DtPrintPolicyList.Clone();
                    DataRow myRow;
                    for (int i = 0; i <= drPrintPolicyList.Length - 1; i++)
                    {
                        myRow = policy._dtPrintPolicyList.NewRow();
                        myRow["PolicyReportID"] = drPrintPolicyList[i].ItemArray[0].ToString();
                        myRow["ReportFileName"] = drPrintPolicyList[i].ItemArray[1].ToString();
                        myRow["ReportDescription"] = drPrintPolicyList[i].ItemArray[2].ToString();
                        myRow["PrintJustOnce"] = drPrintPolicyList[i].ItemArray[3].ToString();
                        policy._dtPrintPolicyList.Rows.Add(myRow);
                        policy._dtPrintPolicyList.AcceptChanges();
                    }

                    // Get the Document that that print just once.
                    DataRow[] drPrintJustOnceList = DtPrintPolicyList.Select("PrintJustOnce = 1");
                    policy._dtPrintJustOnceList = DtPrintPolicyList.Clone();
                    myRow = null;
                    for (int i = 0; i <= drPrintJustOnceList.Length - 1; i++)
                    {
                        myRow = policy._dtPrintJustOnceList.NewRow();
                        myRow["PolicyReportID"] = drPrintJustOnceList[i].ItemArray[0].ToString();
                        myRow["ReportFileName"] = drPrintJustOnceList[i].ItemArray[1].ToString();
                        myRow["ReportDescription"] = drPrintJustOnceList[i].ItemArray[2].ToString();
                        myRow["PrintJustOnce"] = drPrintJustOnceList[i].ItemArray[3].ToString();
                        policy._dtPrintJustOnceList.Rows.Add(myRow);
                        policy._dtPrintJustOnceList.AcceptChanges();
                    }
                }
            }
            catch (Exception xcp)
            {
                throw new Exception("Could not get the policy report.", xcp);
            }

            return policy;
        }

		private static DataTable GetFdealerByBankIDAndDealerIDDB(string BankID, string FdealerID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("BankID",
				SqlDbType.Char, 3, BankID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FdealerID",
				SqlDbType.Char, 3, FdealerID.ToString(),
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
	
			DataTable dt = exec.GetQuery("GetFdealerByBankIDAndDealerID",xmlDoc);
			return dt;		
		}

		private static DataTable GetCompanyDealerByCompanyDealerIDDB(string CompanyDealerID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.Char, 3, CompanyDealerID.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetCompanyDealerByCompanyDealerID",xmlDoc);
			return dt;
		}

		private static DataTable GetPoliciesByCustomerNoDB(string CustomerNo)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CustomerNo",
				SqlDbType.Char, 10, CustomerNo.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetPoliciesByCustomerNo",xmlDoc);
			return dt;
		}

        public static DataTable GetVINByPolicyClass(string vin, int policyClass)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("VIN",
                SqlDbType.VarChar, 17, vin.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClass",
                SqlDbType.Int, 0, policyClass.ToString(),
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

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetVINByPolicyClass", xmlDoc);
            return dt;
        }

		public static DataTable GetEtchValidateCounter(string policyNo, string Term, string InsuranceCompany, string Suffix)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[4];

			DbRequestXmlCooker.AttachCookItem("PolicyNo",
				SqlDbType.VarChar, 11, policyNo.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
                SqlDbType.VarChar, 2, Term.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InsuranceCompany",
                SqlDbType.VarChar, 3, InsuranceCompany.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Suffix",
            SqlDbType.Char, 2, Suffix,
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetEtchValidateCounter", xmlDoc);
			return dt;
		}

        public static DataTable GetEtchValidateCounterEdit(string policyNo, string Term, string InsuranceCompany, int TControlID, string Suffix)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[5];

            DbRequestXmlCooker.AttachCookItem("PolicyNo",
                SqlDbType.VarChar, 11, policyNo.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
                SqlDbType.VarChar, 2, Term.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InsuranceCompany",
                SqlDbType.VarChar, 3, InsuranceCompany.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
            SqlDbType.Int, 0, TControlID.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Suffix",
            SqlDbType.Char, 2, Suffix,
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

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetEtchValidateCounterEdit", xmlDoc);
            return dt;
        }

        public static DataTable GetPolicyByPolicyNo(string policyType, string policyNo, string certificate, string sufijo)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("PolicyType",
                SqlDbType.VarChar, 3, policyType.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo",
                SqlDbType.VarChar, 11, policyNo.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Certificate",
                SqlDbType.VarChar, 10, certificate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Sufijo",
                SqlDbType.VarChar, 2, sufijo.ToString(),
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

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetPolicyByPolicyNo", xmlDoc);
            return dt;
        }

        public static DataTable GetPolicyByPolicyNoAndCompanyDealer(string insuranceCompany, string companyDealer , string policyNo)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("InsuranceCompany",SqlDbType.Char, 3, insuranceCompany.ToString(),ref cookItems);
            DbRequestXmlCooker.AttachCookItem("CompanyDealer",SqlDbType.Char, 3, companyDealer.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PolicyNo", SqlDbType.VarChar, 11, policyNo.ToString(),ref cookItems);
            
            XmlDocument xmlDoc;
            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetVSCPolicyCounterByPolicyNoAndCompanyDealer", xmlDoc);
            return dt;
        }

		private static DataTable  GetPolicyByCriteria(int policyClass, string policyType,string policyNo, string certificate, string sufijo, string loanNo, string bank, string VIN, int UserID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];

			DbRequestXmlCooker.AttachCookItem("PolicyClass",
				SqlDbType.VarChar, 10, policyClass.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.VarChar, 3, policyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyNo",
				SqlDbType.VarChar, 11, policyNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Sufijo",
				SqlDbType.VarChar, 2, sufijo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Certificate",
				SqlDbType.VarChar, 10, certificate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LoanNo",
				SqlDbType.VarChar, 15, loanNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Bank",
				SqlDbType.VarChar, 3, bank.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VIN",
				SqlDbType.Char, 18, VIN.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("UserID",
				SqlDbType.Int, 0, UserID.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetPolicyByCriteria",xmlDoc);
			return dt;
		}

		private static DataTable GetPolicyByTaskControlIDDB(int TaskControlID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, TaskControlID.ToString(),
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

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetPolicyByTaskControlID",xmlDoc);
			return dt;
		}


		public void AssignPolicyNo()
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			DataTable dtPolicyNo;
			int policyCounterID = 0;
			
			switch (this.PolicyClassID)
			{
				case 1:		//Vehicle Services Contract		
                case 16:
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml ());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                { 
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for VSC Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                char pad = '0';
                                if (InsuranceCompany == "002")
                                {
                                    this.PolicyNo = this.PolicyNo.PadLeft(5,pad);
                                }
                                else 
                                {
                                    this.PolicyNo = this.PolicyNo.PadLeft(6,pad);
                                    
                                }

                                if(this.PolicyClassID == 1)
                                    this.PolicyNo = "PR" + this.PolicyNo.Trim();
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
					break;

                case 3:		//AutoPersonalPolicy
                    if (this.Ren_Rei.Trim() == "")//Cuando no es renovacion, siempre es automatico para que asigne num de poliza
                        this.AutoAssignPolicy = true; 

                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        if (this.IsMaster)    //Cuando la poliza es un Master Policy.
                        {
                            dtPolicyNo = Executor.GetQuery("GetPolicyMasterCounter", this.GetPolicyMasterCounterXml());
                            if (dtPolicyNo.Rows.Count != 0)
                            {
                                int PolNo = ((int)dtPolicyNo.Rows[0]["pol_seq"]) + 1;

                                //this.PolicyNo = dtPolicyNo.Rows[0]["Pol_type"].ToString().Trim()+"-"+dtPolicyNo.Rows[0]["Pol_number"].ToString().Trim();
                                this.PolicyNo = dtPolicyNo.Rows[0]["Pol_number"].ToString().Trim();

                                //Si el campo pol_end = 0, significa que no tiene prefijo y va sin guión.
                                if (dtPolicyNo.Rows[0]["pol_end"].ToString().Trim() == "0")
                                    this.Certificate = PolNo.ToString().PadLeft(7,'0');
                                else
                                    this.Certificate = PolNo.ToString().PadLeft(7, '0');                            

                                this._PolicyCounterID = int.Parse(dtPolicyNo.Rows[0]["MasterPolicyID"].ToString());
                                policyCounterID = this._PolicyCounterID;

                                if (this._PolicyCounterID != 0)
                                {								//Actualiza la tabla de numero de polizas (Counters).
                                    Executor.BeginTrans();
                                    Executor.Update("UpdatePolicyMasterByMasterPolicyID", this.UpdateMasterPolicyByMasterPolicyIDXml());
                                    Executor.CommitTrans();
                                }
                                else
                                {
                                    throw new Exception("Error in assign policy number");
                                }
                            }
                            else
                            {
                                throw new Exception("Sorry, Policy counter not found, Please verify.");
                            }
                        }
                        else
                        {
                            dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                            if (dtPolicyNo.Rows.Count != 0)
                            {
                                int PolNo = 0;
                                for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                                {
                                    if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                    {
                                        PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                        this.PolicyNo = PolNo.ToString();
                                        this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                        policyCounterID = this._PolicyCounterID;
                                        i = dtPolicyNo.Rows.Count;
                                    }
                                }

                                if (this._PolicyCounterID != 0)
                                {								//Actualiza la tabla de numero de polizas (Counters).
                                    Executor.BeginTrans();
                                    Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                    Executor.CommitTrans();
                                    //this.PolicyNo = this.Customer.LocationID.ToString().PadLeft(2, '0') + "-29-" + PolNo.ToString();
                                    //this.PolicyNo = "01" + "-29-" + PolNo.ToString();
                                    this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                                }
                                else
                                {
                                    throw new Exception("Error in assign policy number");
                                }
                            }
                            else
                            {
                                throw new Exception("Sorry, Policy counter not found, Please verify.");
                            }
                        }
                    }
                    break;

				case 9:		//Auto Gap
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        if (this.IsMaster)    //Cuando la poliza es un Master Policy.
                        {
                            dtPolicyNo = Executor.GetQuery("GetPolicyMasterCounter", this.GetPolicyMasterCounterXml());
                            if (dtPolicyNo.Rows.Count != 0)
                            {
                                int PolNo = ((int)dtPolicyNo.Rows[0]["pol_seq"]) + 1;

                                this.PolicyNo = dtPolicyNo.Rows[0]["Pol_number"].ToString().Trim();
                                this.Certificate = PolNo.ToString().Trim().PadLeft(7,'0');

                                this._PolicyCounterID = int.Parse(dtPolicyNo.Rows[0]["MasterPolicyID"].ToString());
                                policyCounterID = this._PolicyCounterID;

                                if (this._PolicyCounterID != 0)
                                {								//Actualiza la tabla de numero de polizas (Counters).
                                    Executor.BeginTrans();
                                    Executor.Update("UpdatePolicyMasterByMasterPolicyID", this.UpdateMasterPolicyByMasterPolicyIDXml());
                                    Executor.CommitTrans();
                                }
                                else
                                {
                                    throw new Exception("Error in assign policy number");
                                }
                            }
                            else
                            {
                                throw new Exception("Sorry, Policy counter not found, Please verify.");
                            }
                        }
                        else
                        {
                            dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                            if (dtPolicyNo.Rows.Count != 0)
                            {
                                for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                                {
                                    if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                    {
                                        int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                        this.PolicyNo = PolNo.ToString();
                                        this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                        policyCounterID = this._PolicyCounterID;
                                        i = dtPolicyNo.Rows.Count;
                                    }
                                    else
                                    {
                                        throw new Exception("Must assign a new pool number for Gap Policy.");
                                    }
                                }

                                if (this._PolicyCounterID != 0)
                                {								//Actualiza la tabla de numero de polizas (Counters).
                                    Executor.BeginTrans();
                                    Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                    Executor.CommitTrans();
                                }
                                else
                                {
                                    throw new Exception("Error in assign policy number");
                                }
                            }
                            else
                            {
                                throw new Exception("Sorry, Policy counter not found, Please verify.");
                            }
                        }
                    }
					break;

                case 12:		//OPP
                   if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for OPP Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7,'0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
					break;

                case 18:		//DW
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for Dwelling Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 14: //Driver Plus
                    //if (this.PolicyNo.Trim().Substring(0,2) != "CT")
                    //    this.PolicyNo = "CT" + this.PolicyNo.Trim();
                    break;

                case 15: //OSO

                    //if (this.Ren_Rei.Trim() == "")//Cuando no es renovacion, siempre es automatico para que asigne num de poliza
                        //this.AutoAssignPolicy = true; 

                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for OSO Policy.");
                                }

                                if (this._PolicyCounterID != 0)
                                {								//Actualiza la tabla de numero de polizas (Counters).
                                    Executor.BeginTrans();
                                    Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                    Executor.CommitTrans();

                                    this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                                    this.PolicyNo = this.PolicyNo.Trim();
                                }
                                else
                                {
                                    throw new Exception("Error in assign policy number");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 19:		//ORA
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for Dwelling Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;
                case 20:		//ProfessionalLiability
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for Professional Liability Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 21:		//RoadAssistance

                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for Professional Liability Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 22:		//AUTOS ST. THOMAS
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for PPA Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 23:		//GuardianXtra
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        string PType = this.PolicyType;

                        if (this.PolicyClassID == 23)
                        {
                            this.PolicyType = "XPA";
                        }
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());

                        this.PolicyType = PType;

                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for XPA Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 24:		//DOUBLE INTERST
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {

                        if (this.PolicyClassID == 24) // Yo no quise pero pues...
                        {
                            this.PolicyClassID = 22;
                            this.PolicyType = "PAP";
                        }
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (this.PolicyClassID == 22)
                        {
                            this.PolicyClassID = 24;
                        }
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for PAP Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 25:          //HOME OWNERS
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente[ClaimNext].
                    {

                        //if (this.PolicyClassID == 25)
                        //{
                        //    this.PolicyType = "HO";
                        //}
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for Bond Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {                                                    //Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                case 26:          //BONDS
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {

                        if (this.PolicyClassID == 26)
                        {
                            this.PolicyType = "BND";
                        }
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for Bond Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {                                                    //Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                    case 27:          //YACHT
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {

                        if (this.PolicyClassID == 27)
                        {
                            this.PolicyType = "MAR";
                        }
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for Yacht Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {                                                    //Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim();
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                    case 28:          //AUTO HIGH LIMIT
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for PPA Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {								//Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

                    case 29:          //RES
                    if (this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
                    {

                        if (this.PolicyClassID == 29)
                        {
                            this.PolicyType = "RES";
                        }
                        dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter", this.GetPolicyCounterXml());
                        if (dtPolicyNo.Rows.Count != 0)
                        {
                            for (int i = 0; dtPolicyNo.Rows.Count >= i; i++)
                            {
                                if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int)dtPolicyNo.Rows[i]["PolicyLast"])
                                {
                                    int PolNo = ((int)dtPolicyNo.Rows[i]["PolicyLast"]) + 1;
                                    this.PolicyNo = PolNo.ToString();
                                    this._PolicyCounterID = (int)dtPolicyNo.Rows[i]["PolicyCounterID"];
                                    policyCounterID = this._PolicyCounterID;
                                    i = dtPolicyNo.Rows.Count;
                                }
                                else
                                {
                                    throw new Exception("Must assign a new pool number for RES Policy.");
                                }
                            }

                            if (this._PolicyCounterID != 0)
                            {                                                    //Actualiza la tabla de numero de polizas (Counters).
                                Executor.BeginTrans();
                                Executor.Update("UpdatePolicyCounterByPolicyCounterID", this.UpdatePolicyCounterByPolicyCounterIDXml());
                                Executor.CommitTrans();

                                this.PolicyNo = this.PolicyNo.Trim().PadLeft(7, '0');
                            }
                            else
                            {
                                throw new Exception("Error in assign policy number");
                            }
                        }
                        else
                        {
                            throw new Exception("Sorry, Policy counter not found, Please verify.");
                        }
                    }
                    break;

				default:
					break;
			
			}

			//Asignar número de póliza según el policy class.
//			switch (this.PolicyClassID)
//			{
//				case 1:		//AutoGuardServicesContract
//					dtPolicyNo = Executor.GetQuery("GetCustomerCounterByParameter",this.GetPolicyNoCounterXml());
//					string policyNo=  dtPolicyNo.Rows[0]["SequenceCurrent"].ToString();
//					
//					string poli2 = "";
//					
//					policyNo = policyNo.Trim();
//					int pol = 6 - policyNo.Length;
//					
//					poli2 = poli2.PadLeft(pol,'0');
//					poli2 = this.CompanyDealer.Trim()+poli2+policyNo;
//			
//					this.PolicyNo = poli2;
//					break;
//
//				case 3:		//AutoPersonalPolicy
//					if(this.AutoAssignPolicy) //Número de Poliza asignado automaticamente.
//					{	
//						if (this.IsMaster)    //Cuando la poliza es un Master Policy.
//						{					
//							int policyCounterID = 0;
//							dtPolicyNo = Executor.GetQuery("GetPolicyMasterCounter",this.GetPolicyMasterCounterXml());
//							if (dtPolicyNo.Rows.Count != 0)
//							{
//								int PolNo = ((int) dtPolicyNo.Rows[0]["pol_seq"])+1;
//
//								if (this.MasterPolicyID == "0047") // Si es master de Guaranty no tiene certificado.
//								{
//									this.PolicyNo = PolNo.ToString();
//								}
//								else
//								{
//									//this.PolicyNo = dtPolicyNo.Rows[0]["Pol_type"].ToString().Trim()+"-"+dtPolicyNo.Rows[0]["Pol_number"].ToString().Trim();
//									this.PolicyNo = dtPolicyNo.Rows[0]["Pol_number"].ToString().Trim();
//
//									//Si el campo pol_end = 0, significa que no tiene prefijo y va sin guión.
//									if(dtPolicyNo.Rows[0]["pol_end"].ToString().Trim() == "0")
//										this.Certificate = PolNo.ToString().Trim();
//									else
//										this.Certificate = dtPolicyNo.Rows[0]["pol_end"].ToString().Trim()+"-"+PolNo.ToString().Trim();
//									//this.Certificate = PolNo.ToString();
//								}
//
//								this._PolicyCounterID = int.Parse(dtPolicyNo.Rows[0]["MasterPolicyID"].ToString());
//								policyCounterID = this._PolicyCounterID;
//								
//
//								if(this._PolicyCounterID != 0)
//								{								//Actualiza la tabla de numero de polizas (Counters).
//									Executor.BeginTrans();
//									Executor.Update("UpdatePolicyMasterByMasterPolicyID",this.UpdateMasterPolicyByMasterPolicyIDXml());
//									Executor.CommitTrans();
//								}
//								else
//								{
//									throw new Exception("Error in assign policy number");
//								}
//							}
//							else
//							{
//								throw new Exception("Sorry, Policy counter not found, Please verify.");
//							}
//						}
//						else	
//						{
//							int policyCounterID = 0;
//							dtPolicyNo = Executor.GetQuery("GetPolicyCounterByParameter",this.GetPolicyCounterXml());
//							if (dtPolicyNo.Rows.Count != 0)
//							{
//								for (int i=0;dtPolicyNo.Rows.Count >=i;i++)
//								{
//									if ((int)dtPolicyNo.Rows[i]["PolicyEnd"] > (int) dtPolicyNo.Rows[i]["PolicyLast"])
//									{
//										int PolNo = ((int) dtPolicyNo.Rows[i]["PolicyLast"])+1;
//										this.PolicyNo = PolNo.ToString();
//										this._PolicyCounterID = (int) dtPolicyNo.Rows[i]["PolicyCounterID"];
//										policyCounterID = this._PolicyCounterID;
//										i = dtPolicyNo.Rows.Count;
//									}
//								}	
//
//								if(this._PolicyCounterID != 0)
//								{								//Actualiza la tabla de numero de polizas (Counters).
//									Executor.BeginTrans();
//									Executor.Update("UpdatePolicyCounterByPolicyCounterID",this.UpdatePolicyCounterByPolicyCounterIDXml());
//									Executor.CommitTrans();
//								}
//								else
//								{
//									throw new Exception("Error in assign policy number");
//								}
//							}
//							else
//							{
//								throw new Exception("Sorry, Policy counter not found, Please verify.");
//							}
//						}
//					}
//					break;
//
//				default:
//					break;
//			}
		}

		public void SaveOnlyPolicy(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				Executor.Update("UpdatePolicy",this.GetUpdatePolicyXml());
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Only Policy. "+xcp.Message,xcp);
			}
		}

		private void SavePolicy(int UserID)
		{
            if (this.PolicyClassID == 1)  // Para VSC para que pueda sincronizar Imaging Data File.
            {
                this._Modify = true;
            }
            else
            {
                this._Modify = false;
            }

			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				if (this._mode == 1)  //Asignar Número de póliza según el tipo de póliza.
				{
					AssignPolicyNo();
				}

				Executor.BeginTrans();
				switch (this._mode)
				{
					case 1:  //ADD
						this.PolicyID = Executor.Insert("AddPolicy",this.GetInsertPolicyXml());
						break;

					case 3:  //DELETE
						Executor.Update("DeletePolicy",this.GetDeletePolicyXml());
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						Executor.Update("UpdatePolicy",this.GetUpdatePolicyXml());
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Policy. "+xcp.Message,xcp);
			}

			//Refresh and load the policy to print.
           GetPrintPolicyReport(this,"Interno");
		}

 		private XmlDocument GetPolicyMasterCounterXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
				SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
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

		private XmlDocument GetPolicyCounterXml()
		{


			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[5];

			DbRequestXmlCooker.AttachCookItem("DepartmentID",
				SqlDbType.Int, 0, this.DepartmentID.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, this.PolicyType.Trim().ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.Char, 3, this.InsuranceCompany.Trim().ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
                SqlDbType.Char, 3, "000",
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

		private XmlDocument GetPolicyNoCounterXml()
		{
			string customerCounterID = LookupTables.LookupTables.GetID("CustomerCounter",this.GetType().Name);

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CustomerCounterID",
				SqlDbType.Int, 0, int.Parse(customerCounterID.Trim()).ToString(),
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

		private XmlDocument AddPolicyIdTempXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[0];

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

		private XmlDocument UpdateMasterPolicyByMasterPolicyIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("PolicyCounterID",
				SqlDbType.Char, 4, this._PolicyCounterID.ToString(),
				ref cookItems);

            //if (this.MasterPolicyID == "0047") // Si es master de Guaranty no tiene certificado.
            //{
            //    DbRequestXmlCooker.AttachCookItem("PolicyNo",
            //        SqlDbType.Int, 0, this.PolicyNo.ToString(),
            //        ref cookItems);
            //}
            //else
            //{
				int mpos = this.Certificate.IndexOf("-",0);
				if(mpos != -1)
				{
					string mfnd = this.Certificate.Substring(mpos,1);

					if(mfnd == "-")
					{
						int mfound = this.Certificate.IndexOf("-",0);
						string mcert = this.Certificate.Substring(mfound+1);

						DbRequestXmlCooker.AttachCookItem("PolicyNo",
							SqlDbType.Int, 0, mcert.ToString(),
							ref cookItems);
					}
				}
				else //ara los certificados que no tengan prefijos.
				{
					DbRequestXmlCooker.AttachCookItem("PolicyNo",
						SqlDbType.Int, 0, this.Certificate.ToString(),
						ref cookItems);
				}
			//}

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

		private XmlDocument UpdatePolicyCounterByPolicyCounterIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("PolicyCounterID",
				SqlDbType.Int, 0, this._PolicyCounterID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyNo",
				SqlDbType.Int, 0, this.PolicyNo.ToString(),
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

		private XmlDocument GetDeletePolicyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("policyID",
				SqlDbType.Int, 0, PolicyID.ToString(),
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

		private XmlDocument GetInsertPolicyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[66];

			DbRequestXmlCooker.AttachCookItem("policyID",
				SqlDbType.Int, 0, this.PolicyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("taskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("DepartmentID",
				SqlDbType.Int, 0, this.DepartmentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, this.PolicyType.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("PolicyNo",
				SqlDbType.Char, 11, this.PolicyNo.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("Certificate",
				SqlDbType.Char, 10, this.Certificate.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("Sufijo",
				SqlDbType.Char, 2, this.Suffix.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("LoanNo",
				SqlDbType.Char, 15, this.LoanNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Term",
				SqlDbType.Int, 0, this.Term.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 8, this.EffectiveDate.ToString(),
				ref cookItems);

			if (this.ExpirationDate !="")
			{
				DateTime date2;
				date2 = DateTime.Parse(this.ExpirationDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("ExpirationDate",
					SqlDbType.DateTime, 0, date2.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("ExpirationDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	

			DbRequestXmlCooker.AttachCookItem("Charge",
				SqlDbType.Float, 0, this.Charge.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("TotalPremium",
				SqlDbType.Float, 0, this.TotalPremium.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("Status",
				SqlDbType.VarChar, 50,this.Status.Split("/".ToCharArray())[0].ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAgency",
				SqlDbType.Float, 0, this.CommissionAgency.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAgencyPercent",
				SqlDbType.Char, 3, this.CommissionAgencyPercent.ToString(),
				ref cookItems);

			if (this.CancellationDate !="")
			{
				DateTime date2;
				date2 = DateTime.Parse(this.CancellationDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("CancellationDate",
					SqlDbType.DateTime, 0, date2.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("CancellationDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	

			if (this.CancellationEntryDate !="")
			{
				DateTime date2;
				date2 = DateTime.Parse(this.CancellationEntryDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
					SqlDbType.DateTime, 0, date2.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	
		
			DbRequestXmlCooker.AttachCookItem("CancellationUnearnedPercent",
				SqlDbType.Float, 0, this.CancellationUnearnedPercent.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("ReturnPremium",
				SqlDbType.Float, 0, this.ReturnPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnCharge",
				SqlDbType.Float, 0, this.ReturnCharge.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationAmount",
				SqlDbType.Float, 0, this.CancellationAmount.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationMethod",
				SqlDbType.Int, 0, this.CancellationMethod.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationReason",
				SqlDbType.Int, 0, this.CancellationReason.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnCommissionAgency",
				SqlDbType.Float, 0, this.ReturnCommissionAgency.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaidAmount",
				SqlDbType.Float, 0, this.PaidAmount.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AutoAssignPolicy",
				SqlDbType.Bit, 0, this.AutoAssignPolicy.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InvoiceNumber",
				SqlDbType.Char, 7, this.InvoiceNumber.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FileNumber",
				SqlDbType.Char, 8, this.FileNumber.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IsLeasing",
				SqlDbType.Bit, 0, this.IsLeasing.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaymentType",
				SqlDbType.Int, 0, this.PaymentType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IsMaster",
				SqlDbType.Bit, 0, this.IsMaster.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TCIDApplicationAuto",
				SqlDbType.Int, 0, this.TCIDApplicationAuto.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("TCIDQuotes",
				SqlDbType.Int, 0, this.TCIDQuotes.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PrintPolicy",
				SqlDbType.Bit, 0, this.PrintPolicy.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
				SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Prem_Mes",
				SqlDbType.Float, 0, this.Prem_Mes.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PMT1",
				SqlDbType.Float, 0, this.PMT1.ToString(),
				ref cookItems);

			if (this.PrintDate !="")
			{
				DateTime date3;
				date3 = DateTime.Parse(this.PrintDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("PrintDate",
					SqlDbType.DateTime, 0, date3.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("PrintDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	

			DbRequestXmlCooker.AttachCookItem("OriginatedAt",
				SqlDbType.Int, 0, this.OriginatedAt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Endoso",
				SqlDbType.Int, 0, this.Endoso.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Modify",
				SqlDbType.Bit, 0, this._Modify.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Trams_FL",
				SqlDbType.Bit, 0, this.Trams_FL.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Trams_DT",
				SqlDbType.DateTime, 0, this.Trams_DT.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Ctrams_FL",
				SqlDbType.Bit, 0, this.Ctrams_FL.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Ctrams_DT",
				SqlDbType.DateTime, 0, this.Ctrams_DT.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Ren_Rei",
				SqlDbType.Char, 2, this.Ren_Rei.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Rein_Amt",
				SqlDbType.Float, 0, this.Rein_Amt.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsDeferred",
                SqlDbType.Bit, 0, this.IsDeferred.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsFamily",
                SqlDbType.Bit, 0, this.IsFamily.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsEmployee",
                SqlDbType.Bit, 0, this.IsEmployee.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EmployeeName",
                SqlDbType.VarChar, 50, this.EmployeeName.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FBPositionID",
                SqlDbType.Int, 0, this.FBPositionID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FBSubsidiaryID",
                SqlDbType.Int, 0, this.FBSubsidiaryID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FBBranchesID",
                SqlDbType.Int, 0, this.FBBranchesID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Pending",
                SqlDbType.Bit, 0, this.Pending.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancLossFund",
                SqlDbType.Float, 0, this.CancLossFund.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancOverHead",
                SqlDbType.Float, 0, this.CancOverHead.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancBankFee",
               SqlDbType.Float, 0, this.CancBankFee.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancProfit",
               SqlDbType.Float, 0, this.CancProfit.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancConcurso",
               SqlDbType.Float, 0, this.CancConcurso.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancDealerProfit",
               SqlDbType.Float, 0, this.CancDealerProfit.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancCanReserve",
               SqlDbType.Float, 0, this.CancCanReserve.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancWarranty",
               SqlDbType.Bit, 0, this.CancWarranty.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancInitialMileage",
               SqlDbType.Int, 0, this.CancInitialMileage.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancMileage",
               SqlDbType.Int, 0, this.CancMileage.ToString(),
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
       
		private XmlDocument GetUpdatePolicyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[66];

			DbRequestXmlCooker.AttachCookItem("policyID",
				SqlDbType.Int, 0, this.PolicyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("taskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("DepartmentID",
				SqlDbType.Int, 0, this.DepartmentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, this.PolicyType.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("PolicyNo",
				SqlDbType.Char, 11, this.PolicyNo.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("Certificate",
				SqlDbType.Char, 10, this.Certificate.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("Sufijo",
				SqlDbType.Char, 2, this.Suffix.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("LoanNo",
				SqlDbType.Char, 15, this.LoanNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Term",
				SqlDbType.Int, 0, this.Term.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 8, this.EffectiveDate.ToString(),
				ref cookItems);

			if (this.ExpirationDate !="")
			{
				DateTime date2;
				date2 = DateTime.Parse(this.ExpirationDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("ExpirationDate",
					SqlDbType.DateTime, 0, date2.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("ExpirationDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	
	
			DbRequestXmlCooker.AttachCookItem("Charge",
				SqlDbType.Float, 0, this.Charge.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("TotalPremium",
				SqlDbType.Float, 0, this.TotalPremium.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("Status",
				SqlDbType.VarChar, 50, this.Status.Split("/".ToCharArray())[0].ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAgency",
				SqlDbType.Float, 0, this.CommissionAgency.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAgencyPercent",
				SqlDbType.Char, 3, this.CommissionAgencyPercent.ToString(),
				ref cookItems);

			if (this.CancellationDate !="")
			{
				DateTime date2;
				date2 = DateTime.Parse(this.CancellationDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("CancellationDate",
					SqlDbType.DateTime, 0, date2.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("CancellationDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	
	
			if (this.CancellationEntryDate !="")
			{
				DateTime date2;
				date2 = DateTime.Parse(this.CancellationEntryDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
					SqlDbType.DateTime, 0, date2.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	
			
			DbRequestXmlCooker.AttachCookItem("CancellationUnearnedPercent",
				SqlDbType.Float, 0, this.CancellationUnearnedPercent.ToString(),
				ref cookItems);
	
			DbRequestXmlCooker.AttachCookItem("ReturnPremium",
				SqlDbType.Float, 0, this.ReturnPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnCharge",
				SqlDbType.Float, 0, this.ReturnCharge.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationAmount",
				SqlDbType.Float, 0, this.CancellationAmount.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationMethod",
				SqlDbType.Int, 0, this.CancellationMethod.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationReason",
				SqlDbType.Int, 0, this.CancellationReason.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnCommissionAgency",
				SqlDbType.Float, 0, this.ReturnCommissionAgency.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaidAmount",
				SqlDbType.Float, 0, this.PaidAmount.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AutoAssignPolicy",
				SqlDbType.Bit, 0, this.AutoAssignPolicy.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InvoiceNumber",
				SqlDbType.Char, 7, this.InvoiceNumber.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FileNumber",
				SqlDbType.Char, 8, this.FileNumber.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IsLeasing",
				SqlDbType.Bit, 0, this.IsLeasing.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaymentType",
				SqlDbType.Int, 0, this.PaymentType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IsMaster",
				SqlDbType.Bit, 0, this.IsMaster.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TCIDApplicationAuto",
				SqlDbType.Int, 0, this.TCIDApplicationAuto.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("TCIDQuotes",
				SqlDbType.Int, 0, this.TCIDQuotes.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PrintPolicy",
				SqlDbType.Bit, 0, this.PrintPolicy.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
				SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Prem_Mes",
				SqlDbType.Float, 0, this.Prem_Mes.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PMT1",
				SqlDbType.Float, 0, this.PMT1.ToString(),
				ref cookItems);

			if (this.PrintDate !="")
			{
				DateTime date3;
				date3 = DateTime.Parse(this.PrintDate+" 12:01:00 AM");
				DbRequestXmlCooker.AttachCookItem("PrintDate",
					SqlDbType.DateTime, 0, date3.ToString(),
					ref cookItems);
			}
			else
			{
				DbRequestXmlCooker.AttachCookItem("PrintDate",
					SqlDbType.DateTime, 0, "",
					ref cookItems);
			}	

			DbRequestXmlCooker.AttachCookItem("OriginatedAt",
				SqlDbType.Int, 0, this.OriginatedAt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Endoso",
				SqlDbType.Int, 0, this.Endoso.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Modify",
				SqlDbType.Bit, 0, this._Modify.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Trams_FL",
				SqlDbType.Bit, 0, this.Trams_FL.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Trams_DT",
				SqlDbType.DateTime, 0, this.Trams_DT.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Ctrams_FL",
				SqlDbType.Bit, 0, this.Ctrams_FL.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Ctrams_DT",
				SqlDbType.DateTime, 0, this.Ctrams_DT.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Ren_Rei",
				SqlDbType.Char, 2, this.Ren_Rei.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Rein_Amt",
				SqlDbType.Float, 0, this.Rein_Amt.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsDeferred",
                SqlDbType.Bit, 0, this.IsDeferred.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsFamily",
                SqlDbType.Bit, 0, this.IsFamily.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsEmployee",
                SqlDbType.Bit, 0, this.IsEmployee.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EmployeeName",
                SqlDbType.VarChar, 50, this.EmployeeName.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FBPositionID",
                SqlDbType.Int, 0, this.FBPositionID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FBSubsidiaryID",
                SqlDbType.Int, 0, this.FBSubsidiaryID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FBBranchesID",
                SqlDbType.Int, 0, this.FBBranchesID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Pending",
                SqlDbType.Bit, 0, this.Pending.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancLossFund",
                SqlDbType.Float, 0, this.CancLossFund.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancOverHead",
                SqlDbType.Float, 0, this.CancOverHead.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancBankFee",
               SqlDbType.Float, 0, this.CancBankFee.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancProfit",
               SqlDbType.Float, 0, this.CancProfit.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancConcurso",
               SqlDbType.Float, 0, this.CancConcurso.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancDealerProfit",
               SqlDbType.Float, 0, this.CancDealerProfit.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancCanReserve",
               SqlDbType.Float, 0, this.CancCanReserve.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancWarranty",
               SqlDbType.Bit, 0, this.CancWarranty.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancInitialMileage",
               SqlDbType.Int, 0, this.CancInitialMileage.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancMileage",
               SqlDbType.Int, 0, this.CancMileage.ToString(),
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

		private XmlDocument GetUpdateCancellationXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[21];

			//Change the Status to cancelled;
			this._Status = LookupTables.LookupTables.GetDescription("PolicyStatus","3");
 		
			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("Status",
				SqlDbType.VarChar, 50, this._Status.Split("|".ToCharArray())[0].ToString(),
				ref cookItems);

			DateTime date2;
			date2 = DateTime.Parse(this.CancellationDate+" 12:01:00 AM");
			DbRequestXmlCooker.AttachCookItem("CancellationDate",
				SqlDbType.DateTime, 0, date2.ToString(),
				ref cookItems);

			DateTime date3;
			date3 = DateTime.Parse(this.CancellationEntryDate+" 12:01:00 AM");
			DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
				SqlDbType.DateTime, 0, date3.ToString(),
				ref cookItems);

            this.CancellationEntryDate = date3.ToShortDateString();

			DbRequestXmlCooker.AttachCookItem("CancellationUnearnedPercent",
				SqlDbType.Float, 0, this.CancellationUnearnedPercent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnPremium",
				SqlDbType.Float, 0, this.ReturnPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnCharge",
				SqlDbType.Float, 0, this.ReturnCharge.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationAmount",
				SqlDbType.Float, 0, this.CancellationAmount.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationMethod",
				SqlDbType.Int, 0, this.CancellationMethod.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CancellationReason",
				SqlDbType.Int, 0, this.CancellationReason.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReturnCommissionAgency",
				SqlDbType.Float, 0, this.ReturnCommissionAgency.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancLossFund",
                SqlDbType.Float, 0, this.CancLossFund.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancOverHead",
                SqlDbType.Float, 0, this.CancOverHead.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancBankFee",
                SqlDbType.Float, 0, this.CancBankFee.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancProfit",
                SqlDbType.Float, 0, this.CancProfit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancConcurso",
                SqlDbType.Float, 0, this.CancConcurso.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancDealerProfit",
                SqlDbType.Float, 0, this.CancDealerProfit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancCanReserve",
                SqlDbType.Float, 0, this.CancCanReserve.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancWarranty",
                SqlDbType.Bit, 0, this.CancWarranty.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancInitialMileage",
                SqlDbType.Int, 0, this.CancInitialMileage.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancMileage",
                SqlDbType.Int, 0, this.CancMileage.ToString(),
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

		private static Policy FillProperties(Policy policy)
		{
			policy.DepartmentID   = (int) policy._dtPolicy.Rows[0]["DepartmentID"];
			policy.TaskControlID  = (int) policy._dtPolicy.Rows[0]["TaskControlID"];
			policy.PolicyID       = (int) policy._dtPolicy.Rows[0]["PolicyID"];
			policy.PolicyNo       = policy._dtPolicy.Rows[0]["policyNo"].ToString();
			policy.PolicyType     = policy._dtPolicy.Rows[0]["PolicyType"].ToString();
			policy.Certificate    = policy._dtPolicy.Rows[0]["Certificate"].ToString();
			policy.Suffix         = policy._dtPolicy.Rows[0]["Sufijo"].ToString();
			policy.Term           = (int) policy._dtPolicy.Rows[0]["Term"];
			policy.EffectiveDate  = (policy._dtPolicy.Rows[0]["EffectiveDate"]!= System.DBNull.Value)? ((DateTime) policy._dtPolicy.Rows[0]["EffectiveDate"]).ToShortDateString():"";
			policy.ExpirationDate = (policy._dtPolicy.Rows[0]["ExpirationDate"]!= System.DBNull.Value)? ((DateTime) policy._dtPolicy.Rows[0]["ExpirationDate"]).ToShortDateString():"";
			policy.LoanNo         = policy._dtPolicy.Rows[0]["LoanNo"].ToString();
			policy.TotalPremium   = (double) policy._dtPolicy.Rows[0]["TotalPremium"];
			policy.Charge		  =  (policy._dtPolicy.Rows[0]["Charge"]!= System.DBNull.Value)?(double) policy._dtPolicy.Rows[0]["Charge"]:0.00;
			policy.Status		  = policy._dtPolicy.Rows[0]["Status"].ToString();
			policy.CommissionAgency				= (policy._dtPolicy.Rows[0]["CommissionAgency"] != System.DBNull.Value)? (double) policy._dtPolicy.Rows[0]["CommissionAgency"]:0.00;
			policy.CommissionAgencyPercent		= policy._dtPolicy.Rows[0]["CommissionAgencyPercent"].ToString();
			policy.CommissionAgent				= (policy._dtPolicy.Rows[0]["CommissionAgent"] != System.DBNull.Value)? (double) policy._dtPolicy.Rows[0]["CommissionAgent"]:0.00;
			policy.CommissionAgentPercent		= policy._dtPolicy.Rows[0]["CommissionAgentPercent"].ToString();
			policy.CommissionDate				= (policy._dtPolicy.Rows[0]["CommissionDate"]!= System.DBNull.Value)? ((DateTime) policy._dtPolicy.Rows[0]["CommissionDate"]).ToShortDateString():"";
			policy.CancellationDate				= (policy._dtPolicy.Rows[0]["CancellationDate"]!= System.DBNull.Value)? ((DateTime) policy._dtPolicy.Rows[0]["CancellationDate"]).ToShortDateString():"";
			policy.CancellationEntryDate		= (policy._dtPolicy.Rows[0]["CancellationEntryDate"]!= System.DBNull.Value)? ((DateTime) policy._dtPolicy.Rows[0]["CancellationEntryDate"]).ToShortDateString():"";
			policy.CancellationUnearnedPercent  = (policy._dtPolicy.Rows[0]["CancellationUnearnedPercent"]!= System.DBNull.Value)? ((double) policy._dtPolicy.Rows[0]["CancellationUnearnedPercent"]):0;
			policy.ReturnPremium				= (policy._dtPolicy.Rows[0]["ReturnPremium"]!= System.DBNull.Value)? ((double) policy._dtPolicy.Rows[0]["ReturnPremium"]):0;
			policy.ReturnCharge					= (policy._dtPolicy.Rows[0]["ReturnCharge"]!= System.DBNull.Value)? ((double) policy._dtPolicy.Rows[0]["ReturnCharge"]):0;
			policy.CancellationAmount			= (policy._dtPolicy.Rows[0]["CancellationAmount"]!= System.DBNull.Value)? ((double) policy._dtPolicy.Rows[0]["CancellationAmount"]):0;
			policy.CancellationMethod			= (policy._dtPolicy.Rows[0]["CancellationMethod"]!= System.DBNull.Value)?(int) policy._dtPolicy.Rows[0]["CancellationMethod"]:0;
			policy.CancellationReason			= (policy._dtPolicy.Rows[0]["CancellationReason"]!= System.DBNull.Value)?(int) policy._dtPolicy.Rows[0]["CancellationReason"]:0;
			policy.ReturnCommissionAgency		= (double) policy._dtPolicy.Rows[0]["ReturnCommissionAgency"];
			policy.PaymentsDetail				= PaymentPolicy.GetPaymentsByTaskControlID(policy);
			policy.PaidAmount					= (policy._dtPolicy.Rows[0]["PaidAmount"]!= System.DBNull.Value)? ((double) policy._dtPolicy.Rows[0]["PaidAmount"]):0;
			policy.PaidDate						= (policy._dtPolicy.Rows[0]["PaidDate"]!= System.DBNull.Value)? ((DateTime) policy._dtPolicy.Rows[0]["PaidDate"]).ToShortDateString():"";	
			policy.AutoAssignPolicy			    = (policy._dtPolicy.Rows[0]["AutoAssignPolicy"]!= System.DBNull.Value)?((bool) policy._dtPolicy.Rows[0]["AutoAssignPolicy"]):false;
			policy.InvoiceNumber			    = (policy._dtPolicy.Rows[0]["InvoiceNumber"]!= System.DBNull.Value)? (policy._dtPolicy.Rows[0]["InvoiceNumber"].ToString()):"";
			policy.FileNumber				    = (policy._dtPolicy.Rows[0]["FileNumber"]!= System.DBNull.Value)? (policy._dtPolicy.Rows[0]["FileNumber"].ToString()):"";
			policy.IsLeasing  					= (policy._dtPolicy.Rows[0]["IsLeasing"]!= System.DBNull.Value)? ((bool) policy._dtPolicy.Rows[0]["IsLeasing"]):false;
			policy.PaymentType					= (policy._dtPolicy.Rows[0]["PaymentType"]!= System.DBNull.Value)? ((int) policy._dtPolicy.Rows[0]["PaymentType"]):0;
			policy.TCIDApplicationAuto			= (policy._dtPolicy.Rows[0]["TCIDApplicationAuto"]!= System.DBNull.Value)? (int) policy._dtPolicy.Rows[0]["TCIDApplicationAuto"]:0;
			policy.TCIDQuotes					= (policy._dtPolicy.Rows[0]["TCIDQuotes"]!= System.DBNull.Value)? (int) policy._dtPolicy.Rows[0]["TCIDQuotes"]:0;
			policy.PrintPolicy					= (policy._dtPolicy.Rows[0]["PrintPolicy"]!= System.DBNull.Value)? (bool) policy._dtPolicy.Rows[0]["PrintPolicy"]:false;
			policy.MasterPolicyID	 			= (policy._dtPolicy.Rows[0]["MasterPolicyID"]!= System.DBNull.Value)? policy._dtPolicy.Rows[0]["MasterPolicyID"].ToString():"";
			policy.Prem_Mes						= (policy._dtPolicy.Rows[0]["Prem_Mes"]!= System.DBNull.Value)? (double) policy._dtPolicy.Rows[0]["Prem_Mes"]:0.00;
			policy.PMT1							= (policy._dtPolicy.Rows[0]["PMT1"]!= System.DBNull.Value)? decimal.Parse(((double) policy._dtPolicy.Rows[0]["PMT1"]).ToString()):0;
			policy.PrintDate					= (policy._dtPolicy.Rows[0]["PrintDate"]!= System.DBNull.Value)? ((DateTime) policy._dtPolicy.Rows[0]["PrintDate"]).ToShortDateString():"";
			policy.OriginatedAt					= (policy._dtPolicy.Rows[0]["OriginatedAt"]!= System.DBNull.Value)?(int) policy._dtPolicy.Rows[0]["OriginatedAt"]:0;
			policy.Endoso						= (int) policy._dtPolicy.Rows[0]["Endoso"];
			policy.Ren_Rei						= policy._dtPolicy.Rows[0]["Ren_Rei"].ToString();
			policy.Rein_Amt						= (policy._dtPolicy.Rows[0]["Rein_Amt"]!= System.DBNull.Value)? (double) policy._dtPolicy.Rows[0]["Rein_Amt"]:0.00;
            policy.IsMaster                     = (policy._dtPolicy.Rows[0]["IsMaster"] != System.DBNull.Value) ? (bool)policy._dtPolicy.Rows[0]["IsMaster"] : false;
            policy.IsDeferred                   = (policy._dtPolicy.Rows[0]["IsDeferred"] != System.DBNull.Value) ? (bool)policy._dtPolicy.Rows[0]["IsDeferred"] : false;
            policy.Trams_FL = (policy._dtPolicy.Rows[0]["Trams_fl"] != System.DBNull.Value) ? (bool)policy._dtPolicy.Rows[0]["Trams_fl"] : false;
            policy.Trams_DT = (policy._dtPolicy.Rows[0]["Trams_DT"] != System.DBNull.Value) ? ((DateTime)policy._dtPolicy.Rows[0]["Trams_DT"]).ToString() : "";
            policy.IsFamily = (policy._dtPolicy.Rows[0]["IsFamily"] != System.DBNull.Value) ? (bool)policy._dtPolicy.Rows[0]["IsFamily"] : false;
            policy.IsEmployee = (policy._dtPolicy.Rows[0]["IsEmployee"] != System.DBNull.Value) ? (bool)policy._dtPolicy.Rows[0]["IsEmployee"] : false;
            policy.EmployeeName = policy._dtPolicy.Rows[0]["EmployeeName"].ToString();
            policy.FBPositionID = (policy._dtPolicy.Rows[0]["FBPositionID"] != System.DBNull.Value) ? (int)policy._dtPolicy.Rows[0]["FBPositionID"] : 0;
            policy.FBSubsidiaryID = (policy._dtPolicy.Rows[0]["FBSubsidiaryID"] != System.DBNull.Value) ? (int)policy._dtPolicy.Rows[0]["FBSubsidiaryID"] : 0;
            policy.FBBranchesID = (policy._dtPolicy.Rows[0]["FBBranchesID"] != System.DBNull.Value) ? (int)policy._dtPolicy.Rows[0]["FBBranchesID"] : 0;
            policy.Pending = (policy._dtPolicy.Rows[0]["Pending"] != System.DBNull.Value) ? (bool)policy._dtPolicy.Rows[0]["Pending"] : false;
            policy.CancLossFund = (policy._dtPolicy.Rows[0]["CancLossFund"] != System.DBNull.Value) ? ((double)policy._dtPolicy.Rows[0]["CancLossFund"]) : 0;
            policy.CancOverHead = (policy._dtPolicy.Rows[0]["CancOverHead"] != System.DBNull.Value) ? ((double)policy._dtPolicy.Rows[0]["CancOverHead"]) : 0;
            policy.CancBankFee = (policy._dtPolicy.Rows[0]["CancBankFee"] != System.DBNull.Value) ? ((double)policy._dtPolicy.Rows[0]["CancBankFee"]) : 0;
            policy.CancProfit = (policy._dtPolicy.Rows[0]["CancProfit"] != System.DBNull.Value) ? ((double)policy._dtPolicy.Rows[0]["CancProfit"]) : 0;
            policy.CancConcurso = (policy._dtPolicy.Rows[0]["CancConcurso"] != System.DBNull.Value) ? ((double)policy._dtPolicy.Rows[0]["CancConcurso"]) : 0;
            policy.CancDealerProfit = (policy._dtPolicy.Rows[0]["CancDealerProfit"] != System.DBNull.Value) ? ((double)policy._dtPolicy.Rows[0]["CancDealerProfit"]) : 0;
            policy.CancCanReserve = (policy._dtPolicy.Rows[0]["CancCanReserve"] != System.DBNull.Value) ? ((double)policy._dtPolicy.Rows[0]["CancCanReserve"]) : 0;

            policy.CancWarranty = (policy._dtPolicy.Rows[0]["CancWarranty"] != System.DBNull.Value) ? ((bool)policy._dtPolicy.Rows[0]["CancWarranty"]) : false;
            policy.CancInitialMileage = (policy._dtPolicy.Rows[0]["CancInitialMileage"] != System.DBNull.Value) ? ((int)policy._dtPolicy.Rows[0]["CancInitialMileage"]) : 0;
            policy.CancMileage = (policy._dtPolicy.Rows[0]["CancMileage"] != System.DBNull.Value) ? ((int)policy._dtPolicy.Rows[0]["CancMileage"]) : 0;

			//policy = Policy.GetPrintPolicyReport(policy);

//			policy.CancellationMethodReadySave			= policy.CancellationMethod;
//			policy.CancellationReasonReadySave			= policy.CancellationReason;
//			policy.CancellationDateReadySave		    = policy.CancellationDate;
//			policy.CancellationUnearnedPercentReadySave = policy.CancellationUnearnedPercent;
//			policy.CancellationReturnPremiumReadySave   = policy.ReturnPremium;
//			policy.CancellationReturnChargeReadySave    = policy.ReturnCharge;
//			policy.CancellationAmountReadySave          = policy.CancellationAmount;
			return policy;
		}
		#endregion

		#region Auditing

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

        #region Use Only For OPPQuote

        public static Policy GetPolicyQuoteByTaskControlID(int TaskControlID, Policy _policy)
        {
            Policy policy = _policy;

            DataTable dt = GetPolicyQuoteByTaskControlIDDB(TaskControlID);

            policy._dtPolicy = dt;

            policy = FillProperties(policy);
       

            return policy;
        }

        private static DataTable GetPolicyQuoteByTaskControlIDDB(int TaskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
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

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetPolicyQuoteByTaskControlID", xmlDoc);
            return dt;
        }
        
        public void SaveOPPQuote(int UserID)
        {
            this._Modify = false;

            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this._mode)
                {
                    case 1:  //ADD
                        this.PolicyID = Executor.Insert("AddPolicyQuote", this.GetInsertPolicyQuoteXml(UserID));
                        break;

                    case 4:  //CLEAR						
                        break;

                    default: //UPDATE
                        Executor.Update("UpdatePolicyQuote", this.GetUpdatePolicyQuoteXml(UserID));
                        break;
                }
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the Quote. " + xcp.Message, xcp);
            }
        }

        private XmlDocument GetInsertPolicyQuoteXml(int UserID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[49];

            DbRequestXmlCooker.AttachCookItem("policyID",
                SqlDbType.Int, 0, this.PolicyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("taskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DepartmentID",
                SqlDbType.Int, 0, this.DepartmentID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyType",
                SqlDbType.Char, 3, this.PolicyType.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo",
                SqlDbType.Char, 11, this.PolicyNo.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Certificate",
                SqlDbType.Char, 10, this.Certificate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Sufijo",
                SqlDbType.Char, 2, this.Suffix.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LoanNo",
                SqlDbType.Char, 15, this.LoanNo.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
                SqlDbType.Int, 0, this.Term.ToString(),
                ref cookItems);

          
            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.DateTime, 8, this.EffectiveDate.ToString(),
                ref cookItems);

            //CAMBIO PEDRO 5/6/2014 2:27 PM POR POR POR
            DbRequestXmlCooker.AttachCookItem("UserID", 
            SqlDbType.Int, 0, UserID.ToString(), 
            ref cookItems); 

            if (this.ExpirationDate != "")
            {
                DateTime date2;
                date2 = DateTime.Parse(this.ExpirationDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("ExpirationDate",
                    SqlDbType.DateTime, 0, date2.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("ExpirationDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            DbRequestXmlCooker.AttachCookItem("Charge",
                SqlDbType.Float, 0, this.Charge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
                SqlDbType.Float, 0, this.TotalPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Status",
                SqlDbType.VarChar, 50, this.Status.Split("/".ToCharArray())[0].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommissionAgency",
                SqlDbType.Float, 0, this.CommissionAgency.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommissionAgencyPercent",
                SqlDbType.Char, 3, this.CommissionAgencyPercent.ToString(),
                ref cookItems);

            if (this.CancellationDate != "")
            {
                DateTime date2;
                date2 = DateTime.Parse(this.CancellationDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("CancellationDate",
                    SqlDbType.DateTime, 0, date2.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("CancellationDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            if (this.CancellationEntryDate != "")
            {
                DateTime date2;
                date2 = DateTime.Parse(this.CancellationEntryDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
                    SqlDbType.DateTime, 0, date2.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            DbRequestXmlCooker.AttachCookItem("CancellationUnearnedPercent",
                SqlDbType.Float, 0, this.CancellationUnearnedPercent.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ReturnPremium",
                SqlDbType.Float, 0, this.ReturnPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ReturnCharge",
                SqlDbType.Float, 0, this.ReturnCharge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancellationAmount",
                SqlDbType.Float, 0, this.CancellationAmount.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancellationMethod",
                SqlDbType.Int, 0, this.CancellationMethod.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancellationReason",
                SqlDbType.Int, 0, this.CancellationReason.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ReturnCommissionAgency",
                SqlDbType.Float, 0, this.ReturnCommissionAgency.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaidAmount",
                SqlDbType.Float, 0, this.PaidAmount.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoAssignPolicy",
                SqlDbType.Bit, 0, this.AutoAssignPolicy.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InvoiceNumber",
                SqlDbType.Char, 7, this.InvoiceNumber.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FileNumber",
                SqlDbType.Char, 8, this.FileNumber.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsLeasing",
                SqlDbType.Bit, 0, this.IsLeasing.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentType",
                SqlDbType.Int, 0, this.PaymentType.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsMaster",
                SqlDbType.Bit, 0, this.IsMaster.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TCIDApplicationAuto",
                SqlDbType.Int, 0, this.TCIDApplicationAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TCIDQuotes",
                SqlDbType.Int, 0, this.TCIDQuotes.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PrintPolicy",
                SqlDbType.Bit, 0, this.PrintPolicy.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
                SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Prem_Mes",
                SqlDbType.Float, 0, this.Prem_Mes.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PMT1",
                SqlDbType.Float, 0, this.PMT1.ToString(),
                ref cookItems);

            if (this.PrintDate != "")
            {
                DateTime date3;
                date3 = DateTime.Parse(this.PrintDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("PrintDate",
                    SqlDbType.DateTime, 0, date3.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("PrintDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            DbRequestXmlCooker.AttachCookItem("OriginatedAt",
                SqlDbType.Int, 0, this.OriginatedAt.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Endoso",
                SqlDbType.Int, 0, this.Endoso.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Modify",
                SqlDbType.Bit, 0, this._Modify.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Trams_FL",
                SqlDbType.Bit, 0, this.Trams_FL.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Trams_DT",
                SqlDbType.DateTime, 0, this.Trams_DT.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Ctrams_FL",
                SqlDbType.Bit, 0, this.Ctrams_FL.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Ctrams_DT",
                SqlDbType.DateTime, 0, this.Ctrams_DT.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Ren_Rei",
                SqlDbType.Char, 2, this.Ren_Rei.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rein_Amt",
                SqlDbType.Float, 0, this.Rein_Amt.ToString(),
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

        private XmlDocument GetUpdatePolicyQuoteXml(int UserID)
        {

         


            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[49];


            //Cambio POR 
            DbRequestXmlCooker.AttachCookItem("UserID", 
                SqlDbType.Int, 0, UserID.ToString(),
                ref cookItems); 

            DbRequestXmlCooker.AttachCookItem("policyID",
                SqlDbType.Int, 0, this.PolicyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("taskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DepartmentID",
                SqlDbType.Int, 0, this.DepartmentID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyType",
                SqlDbType.Char, 3, this.PolicyType.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo",
                SqlDbType.Char, 11, this.PolicyNo.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Certificate",
                SqlDbType.Char, 10, this.Certificate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Sufijo",
                SqlDbType.Char, 2, this.Suffix.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LoanNo",
                SqlDbType.Char, 15, this.LoanNo.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
                SqlDbType.Int, 0, this.Term.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.DateTime, 8, this.EffectiveDate.ToString(),
                ref cookItems);

            if (this.ExpirationDate != "")
            {
                DateTime date2;
                date2 = DateTime.Parse(this.ExpirationDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("ExpirationDate",
                    SqlDbType.DateTime, 0, date2.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("ExpirationDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            DbRequestXmlCooker.AttachCookItem("Charge",
                SqlDbType.Float, 0, this.Charge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
                SqlDbType.Float, 0, this.TotalPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Status",
                SqlDbType.VarChar, 50, this.Status.Split("/".ToCharArray())[0].ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommissionAgency",
                SqlDbType.Float, 0, this.CommissionAgency.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommissionAgencyPercent",
                SqlDbType.Char, 3, this.CommissionAgencyPercent.ToString(),
                ref cookItems);

            if (this.CancellationDate != "")
            {
                DateTime date2;
                date2 = DateTime.Parse(this.CancellationDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("CancellationDate",
                    SqlDbType.DateTime, 0, date2.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("CancellationDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            if (this.CancellationEntryDate != "")
            {
                DateTime date2;
                date2 = DateTime.Parse(this.CancellationEntryDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
                    SqlDbType.DateTime, 0, date2.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("CancellationEntryDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            DbRequestXmlCooker.AttachCookItem("CancellationUnearnedPercent",
                SqlDbType.Float, 0, this.CancellationUnearnedPercent.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ReturnPremium",
                SqlDbType.Float, 0, this.ReturnPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ReturnCharge",
                SqlDbType.Float, 0, this.ReturnCharge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancellationAmount",
                SqlDbType.Float, 0, this.CancellationAmount.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancellationMethod",
                SqlDbType.Int, 0, this.CancellationMethod.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CancellationReason",
                SqlDbType.Int, 0, this.CancellationReason.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ReturnCommissionAgency",
                SqlDbType.Float, 0, this.ReturnCommissionAgency.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaidAmount",
                SqlDbType.Float, 0, this.PaidAmount.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoAssignPolicy",
                SqlDbType.Bit, 0, this.AutoAssignPolicy.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InvoiceNumber",
                SqlDbType.Char, 7, this.InvoiceNumber.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FileNumber",
                SqlDbType.Char, 8, this.FileNumber.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsLeasing",
                SqlDbType.Bit, 0, this.IsLeasing.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentType",
                SqlDbType.Int, 0, this.PaymentType.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsMaster",
                SqlDbType.Bit, 0, this.IsMaster.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TCIDApplicationAuto",
                SqlDbType.Int, 0, this.TCIDApplicationAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TCIDQuotes",
                SqlDbType.Int, 0, this.TCIDQuotes.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PrintPolicy",
                SqlDbType.Bit, 0, this.PrintPolicy.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
                SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Prem_Mes",
                SqlDbType.Float, 0, this.Prem_Mes.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PMT1",
                SqlDbType.Float, 0, this.PMT1.ToString(),
                ref cookItems);

            if (this.PrintDate != "")
            {
                DateTime date3;
                date3 = DateTime.Parse(this.PrintDate + " 12:01:00 AM");
                DbRequestXmlCooker.AttachCookItem("PrintDate",
                    SqlDbType.DateTime, 0, date3.ToString(),
                    ref cookItems);
            }
            else
            {
                DbRequestXmlCooker.AttachCookItem("PrintDate",
                    SqlDbType.DateTime, 0, "",
                    ref cookItems);
            }

            DbRequestXmlCooker.AttachCookItem("OriginatedAt",
                SqlDbType.Int, 0, this.OriginatedAt.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Endoso",
                SqlDbType.Int, 0, this.Endoso.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Modify",
                SqlDbType.Bit, 0, this._Modify.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Trams_FL",
                SqlDbType.Bit, 0, this.Trams_FL.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Trams_DT",
                SqlDbType.DateTime, 0, this.Trams_DT.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Ctrams_FL",
                SqlDbType.Bit, 0, this.Ctrams_FL.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Ctrams_DT",
                SqlDbType.DateTime, 0, this.Ctrams_DT.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Ren_Rei",
                SqlDbType.Char, 2, this.Ren_Rei.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rein_Amt",
                SqlDbType.Float, 0, this.Rein_Amt.ToString(),
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
        #endregion

        #region Public PPS Methods

        public bool GetPPSHasEndorsement (out string Message)
        {
           
            Message = "";
            bool HasEndorment = false;

            try
            {
                string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                System.Data.DataTable PPSPolicy = new System.Data.DataTable();
                System.Data.DataTable dt = new DataTable();

                cmd.CommandText = "sproc_CheckHasEndorsement";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                cmd.Parameters.Clear();
                var PolicyNo = this.PolicyNo;
                if (this.PolicyNo[0].ToString() == "0")
                {
                    //Removes Policy Sufix to identify the policy as it is in PPS (Without 0000000 format)
                    PolicyNo = this.PolicyNo.Contains("-") ? int.Parse(this.PolicyNo.Substring(0, this.PolicyNo.IndexOf("-")).Replace("-", "")).ToString() : int.Parse(this.PolicyNo).ToString();
                }
                cmd.Parameters.AddWithValue("@PolicyID", this.PolicyType + PolicyNo);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                sqlConnection1.Close();

                if (PPSPolicy.Rows.Count > 0)
                {
                    if (PPSPolicy.Rows[0][0].ToString() == "ENDOSO")
                    {
                        Message = "This Policy already has endorsement in PPS any further endorsements must be done from PPS.";
                        HasEndorment = true;
                    }
                }
                else
                {
                    Message = "Error Checking Endorsment in PPS, Please Try Again.";
                    HasEndorment = true;
                }

                if (HasEndorment == false && DateTime.Parse(this.ExpirationDate) < DateTime.Now)
                {
                    Message = "Error: This policy is already expired.";
                    HasEndorment = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return HasEndorment;
        }
        #endregion Public PPS Methos
    }
}
