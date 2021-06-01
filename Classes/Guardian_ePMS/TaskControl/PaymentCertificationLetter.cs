using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Audit;
using System.Collections;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
	/// <summary>
	/// Summary description for PaymentCertificationLetter.
	/// </summary>
	public class PaymentCertificationLetter:TaskControl
	{
		public PaymentCertificationLetter()
		{
			this.CompanyDealer     = "000";
			this.EntryDate         = DateTime.Now;
			this.TaskStatusID      = 22;
			this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType","Payment Certification Letter"));
			this._mode =(int) TaskControlMode.ADD;
            
		}

    	#region Variable

		private PaymentCertificationLetter  oldPaymentCertificationLetter = null;
		private DataTable _dtPaymentCertificationLetter ;
		private int     _PaymentCertificationLetterID = 0;
		private string  _PolicyType  = string.Empty;
		private string  _PolicyNo    = string.Empty;
		private string  _Certificate = string.Empty;
		private string  _Suffix      = string.Empty;
		private string  _FirstName   = string.Empty;
		private string  _LastName1   = string.Empty;
		private string  _LastName2	 = string.Empty;
		private decimal _TotalPremium = 0;
		private bool    _CheckNo = false;
		private bool    _Licence = false;
		private bool    _Requisition = false;
		private bool    _ReceiptCompanyDealer = false;
		private string  _Comments = string.Empty;
		private string  _EffectiveDate    = "";
		private string  _ExpirationDate   = "";
		private int _mode = (int) TaskControlMode.CLEAR;

	#endregion

		#region Properties

		public int PaymentCertificationLetterID
		{
			get
			{
				return this._PaymentCertificationLetterID;
			}
			set 
			{
				this._PaymentCertificationLetterID = value;
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

		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set 
			{
				this._FirstName = value;
			}
		}

		public string LastName1
		{
			get
			{
				return this._LastName1;
			}
			set 
			{
				this._LastName1 = value;
			}
		}

		public string LastName2
		{
			get
			{
				return this._LastName2;
			}
			set 
			{
				this._LastName2 = value;
			}
		}

		public decimal TotalPremium
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

		public bool CheckNo
		{
			get
			{
				return this._CheckNo;
			}
			set 
			{
				this._CheckNo = value;
			}
		}

		public bool Licence
		{
			get
			{
				return this._Licence;
			}
			set 
			{
				this._Licence = value;
			}
		}

		public bool Requisition
		{
			get
			{
				return this._Requisition;
			}
			set 
			{
				this._Requisition = value;
			}
		}

		public bool ReceiptCompanyDealer
		{
			get
			{
				return this._ReceiptCompanyDealer;
			}
			set 
			{
				this._ReceiptCompanyDealer = value;
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
		#endregion

		# region Public Methods

		public override void Save(int UserID)
		{
			this._mode = (int) this.Mode;						// Se le asigna el mode de taskControl.

			base.Validate();
			this.Validate();

			if (this._mode ==2)
				oldPaymentCertificationLetter = (PaymentCertificationLetter) PaymentCertificationLetter.GetTaskControlByTaskControlID(this.TaskControlID,UserID);

			base.Save(UserID);			    // Validate and Save TaskControl
            
			SavePaymentCertificationLetter(UserID);			    // Save TaskApplicationAuto

			this._mode = (int) TaskControlMode.UPDATE;
			this.Mode = (int) TaskControlMode.CLEAR;
		}

		public void SavePaymentCertificationLetter(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (this._mode)
				{
					case 1:  //ADD
						this.PaymentCertificationLetterID = Executor.Insert("AddPaymentcertificationLetter",this.GetInsertPaymentCertificationLetterXml());
						this.History(this._mode,UserID);
						break;

					case 3:  //DELETE
						Executor.Update("DeleteTaskPaymentCertificationLetter",this.GetDeleteTaskPaymentCertificationLetterXml());
						this.History(this._mode,UserID);
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						this.History(this._mode,UserID);
						Executor.Update("UpdatePaymentCertificationLetter",this.GetUpdatePaymentCertificationLetterXml());
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Payment Certification Letter. "+xcp.Message,xcp);
			}
		}

		public static PaymentCertificationLetter GetPaymentCertificationLetter(int taskControlID)
		{
			PaymentCertificationLetter paymentCertificationLetter = null;

			DataTable dt = GetPaymentCertificationLetterByTaskControlID(taskControlID);

			paymentCertificationLetter = new  PaymentCertificationLetter();
			paymentCertificationLetter._dtPaymentCertificationLetter = dt;

			paymentCertificationLetter = FillProperties(paymentCertificationLetter);

			return paymentCertificationLetter;
		}

		public static PaymentCertificationLetter GetPaymentCertificationLetter(string policyType, string policyNo, string certificate, string suffix)
		{
			PaymentCertificationLetter paymentCertificationLetter = null;

			DataTable dt = GetPaymentCertificationLetterByCriteria(policyType, policyNo, certificate, suffix);

			if(dt.Rows.Count != 0)
			{
				TaskControl TC =  TaskControl.GetTaskControlByTaskControlID((int) dt.Rows[0]["TaskControlID"],0);
				paymentCertificationLetter = (PaymentCertificationLetter) TC;
				//paymentCertificationLetter =PaymentCertificationLetter.GetPaymentCertificationLetter((int) dt.Rows[0]["TaskControlID"]);
			}

			return paymentCertificationLetter;
		}

		public void DeleteTaskPaymentCertificationLetter(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				Executor.Update("DeleteTaskPaymentCertificationLetter",this.GetDeleteTaskPaymentCertificationLetterXml());
				//				this.AuditDelete(UserID);
				//				this.CommitAudit();
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Payment. "+xcp.Message,xcp);
			}
		}

		#endregion

		# region Private Methods

		public override void Validate()
		{
			string errorMessage = String.Empty;

			if(_PolicyType == "")
				errorMessage = "The Policy Type is missing, Please verify...";
			else
				if(_PolicyNo == "")
				errorMessage = "The Policy No. is missing, Please verify...";
			else
				if(_Suffix == "")
				errorMessage = "The Suffix is missing, Please verify...";
			else
				if(Bank == "")
				errorMessage = "The bank is missing, Please verify...";
			else
				if(_EffectiveDate == "")
				errorMessage = "The Effective Date or ExpirationDate is missing, Please verify...";
			else
				if(_ExpirationDate == "")
				errorMessage = "The Expiration Date or ExpirationDate is missing, Please verify...";
			else
				if(_TotalPremium == 0)
				errorMessage = "The Total Premium is missing, Please verify...";
			else
				if(CustomerNo == "")
				errorMessage = "The customer number is missing, Please verify...";
			else
				if(_FirstName == "")
				errorMessage = "The customer name is missing, Please verify...";
			else
				if(_LastName1 == "")
				errorMessage = "The customer lastname is missing, Please verify...";
			else
				if(_CheckNo == false && _Licence == false && _Requisition == false && _ReceiptCompanyDealer == false)
				errorMessage = "The Documents is not match, Please verify...";
			else
				if(_Licence == false)
				errorMessage = "The Licence is missing , Please verify...";
			else
				if(_CheckNo != true && _Licence != false && _Requisition != true && _ReceiptCompanyDealer != true)
				errorMessage = "The Documents is not match, Please verify...";
			else
				if(_CheckNo != false && _Licence != false && _Requisition != false && _ReceiptCompanyDealer != true)
				errorMessage = "The Documents is not match, Please verify...";
			else
				if(_CheckNo != false && _Licence != false && _Requisition != true && _ReceiptCompanyDealer != false)
				errorMessage = "The Documents is not match, Please verify...";
			else
				if(_CheckNo != true && _Licence != false && _Requisition != true && _ReceiptCompanyDealer != false)
				errorMessage = "The Documents is not match, Please verify...";
			else
				if(_CheckNo != true && _Licence != false && _Requisition != false && _ReceiptCompanyDealer != true)
				errorMessage = "The Documents is not match, Please verify...";
			
			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}
		}

		private static DataTable GetPaymentCertificationLetterByTaskControlID(int taskControlID)
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
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetPaymentCertificationLetterByTaskControlID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve task by criteria.", ex);
			}
			
		}

		private static DataTable GetPaymentCertificationLetterByCriteria(string policyType, string policyNo, string certificate, string suffix)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[4];

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.Char, 3, policyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyNo",
				SqlDbType.Char, 11, policyNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Certificate",
				SqlDbType.Char, 10, certificate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Suffix",
				SqlDbType.Char, 2, suffix.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetPaymentCertificationLetterByCriteria", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve task by criteria.", ex);
			}
			
		}

		private XmlDocument GetDeleteTaskPaymentCertificationLetterXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
		}	


		private XmlDocument GetUpdatePaymentCertificationLetterXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[17];

			DbRequestXmlCooker.AttachCookItem("PaymentCertificationLetterID",
				SqlDbType.Int, 0, this.PaymentCertificationLetterID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
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

			DbRequestXmlCooker.AttachCookItem("Suffix",
				SqlDbType.Char, 2, this.Suffix.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.Char, 15, this.FirstName.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.Char, 15, this.LastName1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.Char, 15, this.LastName2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TotalPremium",
				SqlDbType.Money, 0, this.TotalPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CheckNo",
				SqlDbType.Bit, 0, this.CheckNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Licence",
				SqlDbType.Bit, 0, this.Licence.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Requisition",
				SqlDbType.Bit, 0, this.Requisition.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReceiptCompanyDealer",
				SqlDbType.Bit, 0, this.ReceiptCompanyDealer.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Comments",
				SqlDbType.VarChar, 1000, this.Comments.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 8, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ExpirationDate",
				SqlDbType.DateTime, 8, this.ExpirationDate.ToString(),
				ref cookItems);
			
			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
		}

		private XmlDocument GetInsertPaymentCertificationLetterXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[16];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
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

			DbRequestXmlCooker.AttachCookItem("Suffix",
				SqlDbType.Char, 2, this.Suffix.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.Char, 15, this.FirstName.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.Char, 15, this.LastName1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.Char, 15, this.LastName2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TotalPremium",
				SqlDbType.Money, 0, this.TotalPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CheckNo",
				SqlDbType.Bit, 0, this.CheckNo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Licence",
				SqlDbType.Bit, 0, this.Licence.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Requisition",
				SqlDbType.Bit, 0, this.Requisition.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ReceiptCompanyDealer",
				SqlDbType.Bit, 0, this.ReceiptCompanyDealer.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Comments",
				SqlDbType.VarChar, 1000, this.Comments.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 8, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ExpirationDate",
				SqlDbType.DateTime, 8, this.ExpirationDate.ToString(),
				ref cookItems);
			
			
			
			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			
		}

		
		private static PaymentCertificationLetter FillProperties(PaymentCertificationLetter paymentCertificationLetter)
		{
			paymentCertificationLetter.PaymentCertificationLetterID = (int) paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["PaymentCertificationLetterID"];
			paymentCertificationLetter.PolicyType                   = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["PolicyType"].ToString();
			paymentCertificationLetter.PolicyNo			            = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["PolicyNo"].ToString();
			paymentCertificationLetter.Certificate                  = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["Certificate"].ToString();
			paymentCertificationLetter.Suffix                       = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["Suffix"].ToString();
			paymentCertificationLetter.FirstName                    = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["FirstName"].ToString();
			paymentCertificationLetter.LastName1                    = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["LastName1"].ToString();
			paymentCertificationLetter.LastName2                    = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["LastName2"].ToString();
			paymentCertificationLetter.TotalPremium                 = (decimal)paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["TotalPremium"];
			paymentCertificationLetter.CheckNo					    = (paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["CheckNo"]!=System.DBNull.Value) ? (bool) paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["CheckNo"]: false;
			paymentCertificationLetter.Licence					    = (paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["Licence"]!=System.DBNull.Value) ? (bool) paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["Licence"]: false;
			paymentCertificationLetter.Requisition				    = (paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["Requisition"]!=System.DBNull.Value) ? (bool) paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["Requisition"]: false;
			paymentCertificationLetter.ReceiptCompanyDealer   	    = (paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["ReceiptCompanyDealer"]!=System.DBNull.Value) ? (bool) paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["ReceiptCompanyDealer"]: false;
			paymentCertificationLetter.Comments                     = paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["Comments"].ToString();

			paymentCertificationLetter.EffectiveDate  = (paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["EffectiveDate"]!= System.DBNull.Value)? ((DateTime) paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["EffectiveDate"]).ToShortDateString():"";
			paymentCertificationLetter.ExpirationDate = (paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["ExpirationDate"]!= System.DBNull.Value)? ((DateTime) paymentCertificationLetter._dtPaymentCertificationLetter.Rows[0]["ExpirationDate"]).ToShortDateString():"";


			return paymentCertificationLetter;
		}

		#region History
		
		private void History(int mode, int userID)
		{
			Audit.History history = new Audit.History();
			
			if(_mode == 2)
			{
				//Payment oldPaymentCertificationLetter = null;
			    
				// Campos de TaskControl
				history.BuildNotesForHistory("TaskControlTypeID",
					LookupTables.LookupTables.GetDescription("TaskControlType",oldPaymentCertificationLetter.TaskControlTypeID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskControlType",this.TaskControlTypeID.ToString()),
					mode);
				history.BuildNotesForHistory("TaskStatusID",
					LookupTables.LookupTables.GetDescription("TaskStatus",oldPaymentCertificationLetter.TaskStatusID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskStatus",this.TaskStatusID.ToString()),
					mode);	
				history.BuildNotesForHistory("ProspectID",oldPaymentCertificationLetter.ProspectID.ToString(),this.ProspectID.ToString(),mode);							
				history.BuildNotesForHistory("CustomerNo",oldPaymentCertificationLetter.CustomerNo,this.CustomerNo,mode);
				history.BuildNotesForHistory("PolicyID",oldPaymentCertificationLetter.PolicyID.ToString(),this.PolicyID.ToString(),mode);							
				history.BuildNotesForHistory("PolicyClassID",
					LookupTables.LookupTables.GetDescription("PolicyClass",oldPaymentCertificationLetter.PolicyClassID.ToString()),
					LookupTables.LookupTables.GetDescription("PolicyClass",this.PolicyClassID.ToString()),
					mode);	
				history.BuildNotesForHistory("Agency",oldPaymentCertificationLetter.Agent,this.Agent,mode);
				history.BuildNotesForHistory("Agent",oldPaymentCertificationLetter.Agent,this.Agent,mode);
				history.BuildNotesForHistory("Bank",
					LookupTables.LookupTables.GetDescription("Bank",oldPaymentCertificationLetter.Bank.ToString()),
					LookupTables.LookupTables.GetDescription("Bank",this.Bank.ToString()),
					mode);	
				history.BuildNotesForHistory("InsuranceCompany",oldPaymentCertificationLetter.InsuranceCompany,this.InsuranceCompany,mode);
				history.BuildNotesForHistory("Dealer",oldPaymentCertificationLetter.Dealer,this.Dealer,mode);
				history.BuildNotesForHistory("CompanyDealer",
					LookupTables.LookupTables.GetDescription("CompanyDealer",oldPaymentCertificationLetter.CompanyDealer.ToString()),
					LookupTables.LookupTables.GetDescription("CompanyDealer",this.CompanyDealer.ToString()),
					mode);	
				//history.BuildNotesForHistory("EntryDate",oldPaymentCertificationLetter.EntryDate.t,this.EntryDate,mode);
				history.BuildNotesForHistory("CloseDate",oldPaymentCertificationLetter.CloseDate,this.CloseDate,mode);
				history.BuildNotesForHistory("EnteredBy",oldPaymentCertificationLetter.EnteredBy,this.EnteredBy,mode);
				// Terminan Campos TaskControl

				history.BuildNotesForHistory("PolicyNo",oldPaymentCertificationLetter.PolicyNo,this.PolicyNo,mode);
				history.BuildNotesForHistory("PolicyType",oldPaymentCertificationLetter.PolicyType,this.PolicyType,mode);
				history.BuildNotesForHistory("Certificate",oldPaymentCertificationLetter.Certificate,this.Certificate,mode);
				history.BuildNotesForHistory("Sufijo",oldPaymentCertificationLetter.Suffix,this.Suffix,mode);
				history.BuildNotesForHistory("TotalPremium",oldPaymentCertificationLetter.TotalPremium.ToString("###,###.00"),this.TotalPremium.ToString("###,###.00"),mode);
				history.BuildNotesForHistory("FirstName",oldPaymentCertificationLetter.FirstName.ToString(),this.FirstName.ToString(),mode);
				history.BuildNotesForHistory("LastName1",oldPaymentCertificationLetter.LastName1.ToString(),this.LastName1.ToString(),mode);
				history.BuildNotesForHistory("LastName2",oldPaymentCertificationLetter.LastName2.ToString(),this.LastName2.ToString(),mode);
				history.BuildNotesForHistory("CheckNo",oldPaymentCertificationLetter.CheckNo.ToString(),this.CheckNo.ToString(),mode);
				history.BuildNotesForHistory("Licence",oldPaymentCertificationLetter.Licence.ToString(),this.Licence.ToString(),mode);
				history.BuildNotesForHistory("Requisition",oldPaymentCertificationLetter.Requisition.ToString(),this.Requisition.ToString(),mode);
				history.BuildNotesForHistory("ReceiptCompanyDealer",oldPaymentCertificationLetter.ReceiptCompanyDealer.ToString(),this.ReceiptCompanyDealer.ToString(),mode);
				history.BuildNotesForHistory("Comments",oldPaymentCertificationLetter.Comments.ToString(),this.Comments.ToString(),mode);
				history.BuildNotesForHistory("EffectiveDate",oldPaymentCertificationLetter.EffectiveDate.ToString(),this.EffectiveDate.ToString(),mode);
				history.BuildNotesForHistory("ExpirationDate",oldPaymentCertificationLetter.ExpirationDate.ToString(),this.ExpirationDate.ToString(),mode);
				

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("TaskControlID.","",this.TaskControlID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.TaskControlID.ToString();
			history.Subject = "PAYMENT CERTIFICATION LETTER";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}


		#endregion

		#endregion


	}
}
