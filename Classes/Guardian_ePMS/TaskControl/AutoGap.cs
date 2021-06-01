using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.TaskControl;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Quotes;
using EPolicy.Audit;
using EPolicy.XmlCooker;


namespace EPolicy.TaskControl
{
	/// <summary>
	/// Summary description for AutoGap.
	/// </summary>
	public class AutoGap:Policy
	{
		public AutoGap()
		{
			this.DepartmentID     = 1;     //AutoGap
			this.PolicyClassID	  = 1;
			this.PolicyType       = "GAP";
			this.InsuranceCompany = "004";
			this.Agency           = "000";
			this.Agent            = "001";
			this.Bank             = "000";
			this.Dealer			  = "000";
			this.CompanyDealer	  = "000";
			this.Status           = "Inforce";
			this.TaskStatusID     = int.Parse(LookupTables.LookupTables.GetID("TaskStatus","Open"));
			this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType","AutoGap"));
			this.TotalPremium = 385.00;
			// Para el History
			this._mode =(int) TaskControlMode.ADD;
		}

		#region Variable
		private AutoGap  oldAutoGap = null;
		private DataTable _dtAGapPolicy ;
		private string _Vin			    = "";
		private int    _VehicleMakeID   = 0;
		private int    _VehicleModelID	= 0;
		private int    _VehicleYearID	= 0;
		private int _Milleage			= 0;
		private double _NewCost			= 0.00;
		private double _FinanceAmt		= 0.00;
		private double _Balance  		= 0.00;
		private bool _CashCheck		    = false;
		private bool _Finance			= false;
		private bool _PaymentSchedule   = false;
		private bool _Leasing			= false;
		private bool _Void				= false;
		//private AuditItem _auditItem;
		// Para el History
		private int _mode = (int) TaskControlMode.CLEAR;

		#endregion

		#region Properties

		public string Vin
		{
			get
			{
				return this._Vin;
			}
			set 
			{
				this._Vin = value;
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

		public int Milleage
		{
			get
			{
				return this._Milleage;
			}
			set 
			{
				this._Milleage = value;
			}
		}

		public double NewCost
		{
			get
			{
				return this._NewCost;
			}
			set 
			{
				this._NewCost = value;
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

		public bool CashCheck
		{
			get
			{
				return this._CashCheck;
			}
			set 
			{
				this._CashCheck = value;
			}
		}

		public bool Finance
		{
			get
			{
				return this._Finance;
			}
			set 
			{
				this._Finance = value;
			}
		}

		public bool PaymentSchedule
		{
			get
			{
				return this._PaymentSchedule;
			}
			set 
			{
				this._PaymentSchedule = value;
			}
		}

		public bool Leasing
		{
			get
			{
				return this._Leasing;
			}
			set 
			{
				this._Leasing = value;
			}
		}

		public bool Void
		{
			get
			{
				return this._Void;
			}
			set 
			{
				this._Void = value;
			}
		}

		#endregion

		public override void Save(int UserID)
		{
		}

		#region Public Methods


		public void SaveAutoGap(int UserID)
		{
			this.SavePol(UserID);
		}

		public override void SavePol(int UserID)
		{
			this._mode		= (int) this.Mode;  // Se le asigna el mode de taskControl.
			this.PolicyMode = (int) this.Mode;  // Se le asigna el mode de taskControl.

			base.ValidatePolicy();
			this.Validate();

			// Se utiliza para el History
			if (this._mode ==2)
				oldAutoGap = (AutoGap) AutoGap.GetTaskControlByTaskControlID(this.TaskControlID,UserID);
			
			//Si el usuario cambio la prima manualmente, no debe calcular la misma.
			if (this.TotalPremium == 0)
			{
				CalculatePremium();
			}

			this.Customer.Save(UserID);
			base.Save();
			base.SavePol(UserID);	// Validate and Save Policy
            
			SaveAutoGapPolicy(UserID);  // Save AutoGapPolicy

			this._mode = (int) TaskControlMode.UPDATE;
			this.Mode = (int) TaskControlMode.CLEAR;
			//FillProperties(this);
		}

		public override void Validate()
		{
			string errorMessage = String.Empty;

			if (this.Vin == "")
				errorMessage = "VIN is missing or wrong.";
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
				if (this.OriginatedAt == 0)
				errorMessage = "Originated is missing.";
				
			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}
		}

		public static AutoGap GetAutoGap(int TaskControlID)
		{
			AutoGap AGapPolicy = null;

			DataTable dt = GetAutoGapByTaskControlID(TaskControlID);

			AGapPolicy = new AutoGap();
			AGapPolicy = (AutoGap) Policy.GetPolicyByTaskControlID(TaskControlID,AGapPolicy);  //Policy
			AGapPolicy._dtAGapPolicy = dt;

			AGapPolicy = FillProperties(AGapPolicy);

			return AGapPolicy;
		}

		#endregion

		#region Private Methods

		public void CalculatePremium()
		{
			this.TotalPremium = 385.00;
		}

		private static DataTable GetAutoGapByTaskControlID(int TaskControlID)
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
			
			DataTable dt = exec.GetQuery("GetAutoGapByTaskControlID",xmlDoc);
			return dt;
		}

		private void SaveAutoGapPolicy(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (this.Mode)
				{
					case 1:  //ADD
						this.PolicyID = Executor.Insert("AddAutoGap",this.GetInsertAutoGapXml());
						this.History(this._mode,UserID);
						break;

					case 3:  //DELETE
						Executor.Update("DeleteAutoGap",this.GetDeleteAutoGapXml());
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						this.History(this._mode,UserID);
						Executor.Update("UpdateAutoGap",this.GetUpdateAutoGapXml());
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


		private XmlDocument GetDeleteAutoGapXml()
		{
			XmlDocument xmlDoc = null;

			return xmlDoc;
		}

		private XmlDocument GetUpdateAutoGapXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[13];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VIN",
				SqlDbType.Char, 17, this.Vin.ToString(),
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

			DbRequestXmlCooker.AttachCookItem("Milleage",
				SqlDbType.Int, 0, this.Milleage.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("NewCost",
				SqlDbType.Float, 0, this.NewCost.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FinanceAmt",
				SqlDbType.Float, 0, this.FinanceAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CashCheck",
				SqlDbType.Bit, 0, this.CashCheck.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Finance",
				SqlDbType.Bit, 0, this.Finance.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaymentSchedule",
				SqlDbType.Bit, 0, this.PaymentSchedule.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Leasing",
				SqlDbType.Bit, 0, this.Leasing.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Void",
				SqlDbType.Bit, 0, this.Void.ToString(),
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


		private XmlDocument GetInsertAutoGapXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[13];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VIN",
				SqlDbType.Char, 17, this.Vin.ToString(),
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

			DbRequestXmlCooker.AttachCookItem("Milleage",
				SqlDbType.Int, 0, this.Milleage.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("NewCost",
				SqlDbType.Float, 0, this.NewCost.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FinanceAmt",
				SqlDbType.Float, 0, this.FinanceAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CashCheck",
				SqlDbType.Bit, 0, this.CashCheck.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Finance",
				SqlDbType.Bit, 0, this.Finance.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PaymentSchedule",
				SqlDbType.Bit, 0, this.PaymentSchedule.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Leasing",
				SqlDbType.Bit, 0, this.Leasing.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Void",
				SqlDbType.Bit, 0, this.Void.ToString(),
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

		private static AutoGap FillProperties(AutoGap policy)
		{
			policy.Vin				  = policy._dtAGapPolicy.Rows[0]["VIN"].ToString();
			policy.VehicleMakeID	  = (int) policy._dtAGapPolicy.Rows[0]["VehicleMakeID"];
			policy.VehicleModelID	  = (int) policy._dtAGapPolicy.Rows[0]["VehicleModelID"];
			policy.VehicleYearID	  = (int) policy._dtAGapPolicy.Rows[0]["VehicleYearID"];
			policy.Milleage			  = (int) policy._dtAGapPolicy.Rows[0]["Milleage"];
			policy.NewCost			  = (double) policy._dtAGapPolicy.Rows[0]["NewCost"];
			policy.FinanceAmt		  = (double) policy._dtAGapPolicy.Rows[0]["FinanceAmt"];
			policy.CashCheck          = (bool) policy._dtAGapPolicy.Rows[0]["CashCheck"];
			policy.Finance            = (bool) policy._dtAGapPolicy.Rows[0]["Finance"];
			policy.PaymentSchedule    = (bool) policy._dtAGapPolicy.Rows[0]["PaymentSchedule"];
			policy.Leasing            = (bool) policy._dtAGapPolicy.Rows[0]["Leasing"];
			policy.Void               = (bool) policy._dtAGapPolicy.Rows[0]["Void"];

			return policy;
		}

		#endregion

		#region History

		private void History(int mode, int userID)
		{ 
			Audit.History history = new Audit.History();
			
			if(_mode == 2)
			{				
				// Campos de TaskControl
				history.BuildNotesForHistory("VIN",oldAutoGap.Vin.ToString(),this.Vin,mode);
				history.BuildNotesForHistory("VehicleMake",
					LookupTables.LookupTables.GetDescription("VehicleMake",oldAutoGap.VehicleMakeID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleMake",this.VehicleMakeID.ToString()),
					mode);	
				history.BuildNotesForHistory("VehicleModel",
					LookupTables.LookupTables.GetDescription("VehicleModel",oldAutoGap.VehicleModelID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleModel",this.VehicleModelID.ToString()),
					mode);
				history.BuildNotesForHistory("VehicleYear",
					LookupTables.LookupTables.GetDescription("VehicleYear",oldAutoGap.VehicleYearID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleYear",this.VehicleYearID.ToString()),
					mode);	
				history.BuildNotesForHistory("Milleage",oldAutoGap.Milleage.ToString(),this.Milleage.ToString(),mode);
				history.BuildNotesForHistory("NewCost",oldAutoGap.NewCost.ToString(),this.NewCost.ToString(),mode);
				history.BuildNotesForHistory("FinanceAmt",oldAutoGap.FinanceAmt.ToString(),this.FinanceAmt.ToString(),mode);
				history.BuildNotesForHistory("CashCheck",oldAutoGap.CashCheck.ToString(),this.CashCheck.ToString(),mode);
				history.BuildNotesForHistory("Finance",oldAutoGap.Finance.ToString(),this.Finance.ToString(),mode);
				history.BuildNotesForHistory("PaymentSchedule",oldAutoGap.PaymentSchedule.ToString(),this.PaymentSchedule.ToString(),mode);
				history.BuildNotesForHistory("Leasing",oldAutoGap.Leasing.ToString(),this.Leasing.ToString(),mode);
				history.BuildNotesForHistory("Void",oldAutoGap.Void.ToString(),this.Void.ToString(),mode);

				// Terminan Campos AutoGap

				// Campos de Policy
				history.BuildNotesForHistory("DepartmentID",
					LookupTables.LookupTables.GetDescription("Department",oldAutoGap.DepartmentID.ToString()),
					LookupTables.LookupTables.GetDescription("Department",this.DepartmentID.ToString()),
					mode);
				history.BuildNotesForHistory("PolicyType",oldAutoGap.PolicyType,this.PolicyType,mode);
				history.BuildNotesForHistory("PolicyNo",oldAutoGap.PolicyNo,this.PolicyNo,mode);
				history.BuildNotesForHistory("Certificate",oldAutoGap.Certificate,this.Certificate,mode);
				history.BuildNotesForHistory("Suffix",oldAutoGap.Suffix,this.Suffix,mode);
				history.BuildNotesForHistory("LoanNo",oldAutoGap.LoanNo.ToString(),this.LoanNo.ToString(),mode);
				history.BuildNotesForHistory("Term",oldAutoGap.Term.ToString(),this.Term.ToString(),mode);
				history.BuildNotesForHistory("EffectiveDate",oldAutoGap.EffectiveDate.ToString(),this.EffectiveDate.ToString(),mode);
				history.BuildNotesForHistory("ExpirationDate",oldAutoGap.ExpirationDate.ToString(),this.ExpirationDate.ToString(),mode);
				history.BuildNotesForHistory("Charge",oldAutoGap.Charge.ToString(),this.Charge.ToString(),mode);
				history.BuildNotesForHistory("TotalPremium",oldAutoGap.TotalPremium.ToString(),this.TotalPremium.ToString(),mode);
				history.BuildNotesForHistory("Status",oldAutoGap.Status.ToString(),this.Status.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgency",oldAutoGap.CommissionAgency.ToString(),this.CommissionAgency.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgencyPercent",oldAutoGap.CommissionAgencyPercent.ToString(),this.CommissionAgencyPercent.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgent",oldAutoGap.CommissionAgent.ToString(),this.CommissionAgent.ToString(),mode);
				history.BuildNotesForHistory("CommissionAgentPercent",oldAutoGap.CommissionAgentPercent.ToString(),this.CommissionAgentPercent.ToString(),mode);
				history.BuildNotesForHistory("CommissionDate",oldAutoGap.CommissionDate.ToString(),this.CommissionDate.ToString(),mode);
				history.BuildNotesForHistory("CancellationDate",oldAutoGap.CancellationDate.ToString(),this.CancellationDate.ToString(),mode);
				history.BuildNotesForHistory("CancellationEntryDate",oldAutoGap.CancellationEntryDate.ToString(),this.CancellationEntryDate.ToString(),mode);
				history.BuildNotesForHistory("CancellationUnearnedPercent",oldAutoGap.CancellationUnearnedPercent.ToString(),this.CancellationUnearnedPercent.ToString(),mode);
				history.BuildNotesForHistory("ReturnPremium",oldAutoGap.ReturnPremium.ToString(),this.ReturnPremium.ToString(),mode);
				history.BuildNotesForHistory("ReturnCharge",oldAutoGap.ReturnCharge.ToString(),this.ReturnCharge.ToString(),mode);
				history.BuildNotesForHistory("CancellationAmount",oldAutoGap.CancellationAmount.ToString(),this.CancellationAmount.ToString(),mode);
				history.BuildNotesForHistory("CancellationMethod",oldAutoGap.CancellationMethod.ToString(),this.CancellationMethod.ToString(),mode);
				history.BuildNotesForHistory("CancellationReason",oldAutoGap.CancellationReason.ToString(),this.CancellationReason.ToString(),mode);
				history.BuildNotesForHistory("ReturnCommissionAgency",oldAutoGap.ReturnCommissionAgency.ToString(),this.ReturnCommissionAgency.ToString(),mode);
				history.BuildNotesForHistory("ReturnCommissionAgent",oldAutoGap.ReturnCommissionAgent.ToString(),this.ReturnCommissionAgent.ToString(),mode);
				history.BuildNotesForHistory("PaidAmount",oldAutoGap.PaidAmount.ToString(),this.PaidAmount.ToString(),mode);
				history.BuildNotesForHistory("PaidDate",oldAutoGap.PaidDate.ToString(),this.PaidDate.ToString(),mode);
				history.BuildNotesForHistory("AutoAssignPolicy",oldAutoGap.AutoAssignPolicy.ToString(),this.AutoAssignPolicy.ToString(),mode);
				history.BuildNotesForHistory("InvoiceNumber",oldAutoGap.InvoiceNumber.ToString(),this.InvoiceNumber.ToString(),mode);
				history.BuildNotesForHistory("FileNumber",oldAutoGap.FileNumber.ToString(),this.FileNumber.ToString(),mode);
				history.BuildNotesForHistory("IsLeasing",oldAutoGap.IsLeasing.ToString(),this.IsLeasing.ToString(),mode);
				history.BuildNotesForHistory("PaymentType",oldAutoGap.PaymentType.ToString(),this.PaymentType.ToString(),mode);
				history.BuildNotesForHistory("IsMaster",oldAutoGap.IsMaster.ToString(),this.IsMaster.ToString(),mode);
				history.BuildNotesForHistory("TCIDApplicationAuto",oldAutoGap.TCIDApplicationAuto.ToString(),this.TCIDApplicationAuto.ToString(),mode);
				history.BuildNotesForHistory("TCIDQuote",oldAutoGap.TCIDQuotes.ToString(),this.TCIDQuotes.ToString(),mode);
				history.BuildNotesForHistory("PrintPolicy",oldAutoGap.PrintPolicy.ToString(),this.PrintPolicy.ToString(),mode);
				history.BuildNotesForHistory("MasterPolicyID",
					LookupTables.LookupTables.GetDescription("MasterPolicy",oldAutoGap.MasterPolicyID.ToString()),
					LookupTables.LookupTables.GetDescription("MasterPolicy",this.MasterPolicyID.ToString()),
					mode);
				history.BuildNotesForHistory("Prem_Mes",oldAutoGap.Prem_Mes.ToString(),this.Prem_Mes.ToString(),mode);
				history.BuildNotesForHistory("PMT1",oldAutoGap.PMT1.ToString(),this.PMT1.ToString(),mode);
				history.BuildNotesForHistory("PrintDate",oldAutoGap.PrintDate.ToString(),this.PrintDate.ToString(),mode);
				history.BuildNotesForHistory("OriginatedAt",oldAutoGap.OriginatedAt.ToString(),this.OriginatedAt.ToString(),mode);
				// Terminan Campos Policy

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("TaskControlID.","",this.TaskControlID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.TaskControlID.ToString();
			history.Subject = "AUTOGAP";			
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
	}
}
