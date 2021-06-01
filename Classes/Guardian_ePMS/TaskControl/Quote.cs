using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using EPolicy.Customer;
using Baldrich.DBRequest;
using EPolicy.Quotes;
using EPolicy.XmlCooker;
using EPolicy.Audit;

namespace EPolicy.TaskControl
{    

	public class Quote 	: TaskControl
	{ 

#region Private Attributes
		private QuoteAuto _oldQuote = null;
		private int _QuoteId = 0;
		private int _Term = 0;
		private string _EffectiveDate = string.Empty;
		private string _ExpirationDate = string.Empty;
		private decimal _Charge = 0;
		private decimal _TotalPremium = 0;
		private string _ConvertToPolicyDate = string.Empty;
		private bool _IsPolicy = false;
		private Policy _Policy = new Policy();        
        private string _MasterPolicyID = "";
        private bool _IsMaster = false;
        private string _PolicyType = "";
        private string _FileNumber = "";
		private int _modeQuoteForHistory = 1;
#endregion

#region	Public Properties

		public bool IsPolicy
		{
			get
			{
				return(_IsPolicy);
			}
			set 
			{
				_IsPolicy = value;
			}
		}

		public Policy Policy
		{
			get
			{
				return(_Policy);
			}
			set 
			{
				_Policy = value;
			}
		}

		public int QuoteId
		{
			get
			{
				return(_QuoteId);
			}
			set 
			{
				_QuoteId = value;
			}
		}
     
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
     
		public string EffectiveDate
		{
			get
			{
				return(_EffectiveDate);
			}
			set 
			{
				_EffectiveDate = value;
			}
		}
  
		public string ExpirationDate
		{
			get
			{
				return(_ExpirationDate);
			}
			set 
			{
				_ExpirationDate = value;
			}
		}
     
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
     
		public decimal TotalPremium
		{
			get
			{
				return(_TotalPremium);
			}
			set 
			{
				_TotalPremium = value;
			}
		}
     
		public string ConvertToPolicyDate
		{
			get
			{
				if(this._ConvertToPolicyDate == null)
				{
					this._ConvertToPolicyDate = string.Empty;
				}
				return(this._ConvertToPolicyDate);
			}
			set 
			{
				_ConvertToPolicyDate = value;
			}
		}

        public string MasterPolicyID
        {
            get
            {
                return (_MasterPolicyID);
            }
            set
            {
                _MasterPolicyID = value;
            }
        }

        public bool IsMaster
        {
            get
            {
                return (_IsMaster);
            }
            set
            {
                _IsMaster = value;
            }
        }

        public string PolicyType
        {
            get
            {
                return (_PolicyType);
            }
            set
            {
                _PolicyType = value;
            }
        }

        public string FileNumber
        {
            get
            {
                return (_FileNumber);
            }
            set
            {
                _FileNumber = value;
            }
        }
#endregion

#region Public Methods

		
		public void ConvertToPolicy()
		{
			///TODO: DO THIS METHOD: Quote.ConvertToPolicy()
			;
		}

		
		public virtual void SavePolicyFromQuote(int UserID)
		{
		    if(IsPolicy)		// Save Policy
			{
				if( this._QuoteId == 0)
				{
					this._QuoteId = this.Policy.GetPolicyIDTemp();
				}			
			}
		}

		public override void Save(int UserID)  // Antes estaba Virtual
		{
			///TODO: Use UserID			
			///
			// Se utiliza para el History
			if (this.Mode ==2)
				_oldQuote = (QuoteAuto) QuoteAuto.GetTaskControlByTaskControlID(this.TaskControlID,UserID);


			if(IsPolicy)   // Save Policy
			{
				//this.InitializeAuditItem();
				try
				{
					this.Policy.PolicyMode = (int) this.Mode;

//					if(this.Policy.PolicyMode == 1) //Add
//					{
//						this.History(this.Mode,UserID);
//					}

                 
					this.FillPropertiesPolicy();				
					this.Policy.ValidatePolicy();
					base.Save(UserID);
				
					this.Policy.TaskControlID = this.TaskControlID;

					this.Policy.SavePol(UserID);

					if(this.Policy.PolicyMode == 1) //Add
					{
						this.History(this.Policy.PolicyMode,UserID);
					}

					if(this.Policy.PolicyMode == 2) //Edit
					{
						this.History(this.Mode,UserID);
					}

					
					if (this.Policy.TCIDQuotes != 0)   //Actualiza el status en Quotes que ya el quotes fue convertido a poliza.
					{
						this.Policy.UpdateConvertToPolicy();
					}
				
					//Se cambia el status del application Auto de DataEntry(4) a Print(5).
					if (TaskControl.GetStatusByTaskControlID(this.Policy.TCIDApplicationAuto) == 4)
					{						
						this.Policy.UpdateApplicationAutoStatus(5);
					}

					//Se Actualiza el numero de poliza en application Auto.
					this.Policy.UpdateApplicationAutoPolicyNo(this.Policy.PolicyType.Trim(),this.Policy.PolicyNo.Trim(),this.Policy.Certificate.Trim(),this.Policy.Suffix.Trim());
				}
				catch (Exception exp)
				{
					throw new Exception(exp.Message);
				}
			}
			else		   // Save Quote
			{
				_modeQuoteForHistory = this.Mode;
				
				base.Save(UserID);

				if(this._QuoteId == 0)
				{
					Insert(UserID);
					this.History(_modeQuoteForHistory,UserID);
					//this.AuditInsert(UserID);
					//this.CommitAudit();
				}
				else
				{
					this.History(this.Mode,UserID);
					//this.AuditUpdate(UserID);
					Update(UserID);
					//this.CommitAudit();
				}
			}
		}

		private void FillPropertiesPolicy()
		{
			this.Policy.Customer		= this.Customer;
			this.Policy.TaskControlID	= this.TaskControlID;
			this.Policy.Term			= this.Term;
			this.Policy.EffectiveDate   = this.EffectiveDate;
			this.Policy.ExpirationDate  = this.ExpirationDate;
			this.Policy.PolicyClassID   = this.PolicyClassID;

            //this.Policy.MasterPolicyID = this.MasterPolicyID;
            //this.Policy.IsMaster = this.IsMaster;
            //this.Policy.PolicyType = this.PolicyType;
            //this.Policy.FileNumber = this.FileNumber;

			string charge				= this.Charge.ToString();
			string totalPremium			= this.TotalPremium.ToString();

			this.Policy.Charge			= double.Parse(charge);
			this.Policy.TotalPremium	= double.Parse(totalPremium);
		}

		public void Update(int UserID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				executor.BeginTrans();
				executor.Update("UpdateQuotes", this.GetQuoteUpdateXml());
				executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleException(xcp);
			}
		}

		protected void LoadFromTask(int TaskControlID, int UserID)
		{
			DataTable dtTask = TaskControl.GetTaskControlByTaskControlIDDB(TaskControlID);
			if (dtTask.Rows.Count > 0)
			{
				FillTaskInfo(dtTask, UserID);
			}

			if(!this.IsPolicy)
			{
				DataTable dtQuote = this.GetDataTableFromTask(TaskControlID);
				if (dtQuote.Rows.Count > 0)
				{
					this.FillProperties(dtQuote);
				}
			}
		}

#endregion

#region Private Methods

        private XmlDocument GetQuoteXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[13];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
                SqlDbType.Int, 0, this.PolicyClassID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
                SqlDbType.Int, 0, this.Term.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpirationDate",
                SqlDbType.DateTime, 0, this.ExpirationDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Charge",
                SqlDbType.Money, 0, this.Charge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPremium",
                SqlDbType.Money, 0, this.TotalPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ConvertToPolicy",
                SqlDbType.DateTime, 0, this.ConvertToPolicyDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyID",
                SqlDbType.Int, 0, (this.PolicyID == 0 ? this.PolicyID.ToString() : ""),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
                SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsMaster",
                SqlDbType.Bit, 0, this.IsMaster.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyType",
                SqlDbType.Char, 3, this.PolicyType.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FileNumber",
                SqlDbType.Char, 8, this.FileNumber.ToString(),
                ref cookItems);
            try
            {
                return DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

		private XmlDocument GetQuoteUpdateXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[14];
			
			DbRequestXmlCooker.AttachCookItem("QuotesID",
				SqlDbType.Int, 0, this.QuoteId.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Term",
				SqlDbType.Int, 0, this.Term.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ExpirationDate",
				SqlDbType.DateTime, 0, this.ExpirationDate.ToString(),
				ref cookItems);
            
			DbRequestXmlCooker.AttachCookItem("Charge",
				SqlDbType.Money, 0, this.Charge.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TotalPremium",
				SqlDbType.Money, 0, this.TotalPremium.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ConvertToPolicy",
				SqlDbType.DateTime, 0, this.ConvertToPolicyDate.ToString(),
				ref cookItems);
		
			DbRequestXmlCooker.AttachCookItem("PolicyID",
				SqlDbType.Int, 0, (this.PolicyID==0 ? this.PolicyID.ToString() : "" ),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
                  SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
                  ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsMaster",
                SqlDbType.Bit, 0, this.IsMaster.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyType",
                SqlDbType.Char, 3, this.PolicyType.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FileNumber",
                SqlDbType.Char, 8, this.FileNumber.ToString(),
                ref cookItems);
			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}


			//		sb.Append("<parameter>");
			//		sb.Append("<name>PolicyID</name>");
			//		sb.Append("<type>int</type>");
			//		sb.Append("<value>" + 
			//			(this.PolicyID==0 ? this.PolicyID.ToString() : "" ) + 
			//			"</value>");
			//		sb.Append("</parameter>");
			//
			//		sb.Append("</parameters>");
			//
			//		xmlDoc.InnerXml = sb.ToString();
			//
			//		sb = null;
			//
			//		return xmlDoc;
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

		private void HandleException(Exception Ex)
		{
			switch(Ex.GetBaseException().GetType().FullName)
			{
				case "System.Data.SqlClient.SqlException":
					SqlException sqlException = (SqlException)Ex.GetBaseException();
				switch(sqlException.Number)
				{
					case 547:
						throw new Exception("The Quote you are trying to " +
							"relate to this search fields does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Quote.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Quote.", Ex);
			}            
		}

		

		private void Insert(int UserID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				executor.BeginTrans();
				_QuoteId = executor.Insert("AddQuotes", this.GetQuoteXml());
				executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleException(xcp);
			}
		}

		
		private DataTable GetDataTableFromTask(int TaskControlID)
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
			DataTable dtResult = null;
			try
			{
				dtResult = exec.GetQuery("GetQuotesFromTaskControlID", xmlDoc);
				return dtResult;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
		}

		private void FillProperties(DataTable dtResult)
		{
			this._QuoteId = (int)dtResult.Rows[0]["QuotesId"];
			//		this._TaskControlID = (int)dtResult.Rows[0]["TaskControlID"];
			//		this._PolicyClassID = (int)dtResult.Rows[0]["PolicyClassID"];
			this._Term = (int)dtResult.Rows[0]["Term"];
			//RPR 2004-03-10
			try
			{
				this._EffectiveDate = 
					((DateTime)dtResult.Rows[0]["EffectiveDate"]).ToShortDateString();
			}
			catch
			{
				this.EffectiveDate = "";
			}
			//:~

			// (dt.Rows[0]["CloseDate"]!= System.DBNull.Value)? ((DateTime) dt.Rows[0]["CloseDate"]).ToShortDateString():"";
		
			try
			{
				this._ExpirationDate = 
					((DateTime)dtResult.Rows[0]["ExpirationDate"]).ToShortDateString();
			}
			catch
			{
				this._ExpirationDate = "";
			}

			// (dt.Rows[0]["CloseDate"]!= System.DBNull.Value)? ((DateTime) dt.Rows[0]["CloseDate"]).ToShortDateString():"";
			this._Charge = decimal.Parse(dtResult.Rows[0]["Charge"].ToString());
			this._TotalPremium = decimal.Parse(dtResult.Rows[0]["TotalPremium"].ToString());
			this._ConvertToPolicyDate = (dtResult.Rows[0]["ConvertToPolicy"]!= System.DBNull.Value)? ((DateTime) dtResult.Rows[0]["ConvertToPolicy"]).ToShortDateString():"";
			//		this._PolicyID = (int)dtResult.Rows[0]["PolicyID"];

            this._MasterPolicyID = dtResult.Rows[0]["MasterPolicyID"].ToString();
            this._IsMaster = (dtResult.Rows[0]["IsMaster"] != System.DBNull.Value) ? (bool) dtResult.Rows[0]["IsMaster"] : false;
            this._PolicyType = dtResult.Rows[0]["PolicyType"].ToString();
            this._FileNumber = dtResult.Rows[0]["FileNumber"].ToString();
		}

		private void FillTaskInfo(DataTable dt, int UserID)
		{
			TaskControlID       = (int) dt.Rows[0]["TaskControlID"];
			TaskControlTypeID   = (int) dt.Rows[0]["TaskControlTypeID"];
			TaskStatusID        = (int) dt.Rows[0]["TaskStatusID"];
			
			//Victor
			if(dt.Rows[0]["ProspectID"] != System.DBNull.Value)
				ProspectID	= (int) dt.Rows[0]["ProspectID"];
		
			CustomerNo			= dt.Rows[0]["CustomerNo"].ToString().Trim();
			PolicyID			= (int) dt.Rows[0]["PolicyID"];
			PolicyClassID       = (int) dt.Rows[0]["PolicyClassID"];
			Bank				= dt.Rows[0]["Bank"].ToString();
			InsuranceCompany	= dt.Rows[0]["InsuranceCompany"].ToString();
			Agency				= dt.Rows[0]["Agency"].ToString();
			Agent				= dt.Rows[0]["Agent"].ToString();
			Dealer				= dt.Rows[0]["Dealer"].ToString();
			CompanyDealer		= dt.Rows[0]["CompanyDealer"].ToString();
			EntryDate			= ((DateTime) dt.Rows[0]["EntryDate"]);
			CloseDate			= (dt.Rows[0]["CloseDate"]!= System.DBNull.Value)? ((DateTime) dt.Rows[0]["CloseDate"]).ToShortDateString():"";
			
			System.TimeSpan ts	= DateTime.Today.Subtract(EntryDate); //(SELECT DATEDIFF(day, T.EntryDate ,GETDATE())) as Aging
			Aging				= ts.Days;

			if(CustomerNo!="")
			{
				Customer = EPolicy.Customer.Customer.GetCustomer(CustomerNo, UserID);
			}
			else
			{
				if(ProspectID != 0)
				{
					Prospect prospect = (new Prospect()).GetProspect(ProspectID);
				}
			}
		}


		#region History

		private void History(int mode, int userID)
		{ 
			Audit.History history = new Audit.History();
			
			if(mode == 2)
			{				
				// Campos de TaskControl
				history.BuildNotesForHistory("TaskControlTypeID",
					LookupTables.LookupTables.GetDescription("TaskControlType",_oldQuote.TaskControlTypeID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskControlType",this.TaskControlTypeID.ToString()),
					mode);
				history.BuildNotesForHistory("TaskStatusID",
					LookupTables.LookupTables.GetDescription("TaskStatus",_oldQuote.TaskStatusID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskStatus",this.TaskStatusID.ToString()),
					mode);	
				history.BuildNotesForHistory("ProspectID",_oldQuote.ProspectID.ToString(),this.ProspectID.ToString(),mode);							
				history.BuildNotesForHistory("CustomerNo",_oldQuote.CustomerNo,this.CustomerNo,mode);
				history.BuildNotesForHistory("PolicyID",_oldQuote.PolicyID.ToString(),this.PolicyID.ToString(),mode);							
				history.BuildNotesForHistory("PolicyClassID",
					LookupTables.LookupTables.GetDescription("PolicyClass",_oldQuote.PolicyClassID.ToString()),
					LookupTables.LookupTables.GetDescription("PolicyClass",this.PolicyClassID.ToString()),
					mode);	
				history.BuildNotesForHistory("Agency",_oldQuote.Agent,this.Agent,mode);
				history.BuildNotesForHistory("Agent",_oldQuote.Agent,this.Agent,mode);
				history.BuildNotesForHistory("Bank",
					LookupTables.LookupTables.GetDescription("Bank",_oldQuote.Bank.ToString()),
					LookupTables.LookupTables.GetDescription("Bank",this.Bank.ToString()),
					mode);	
				history.BuildNotesForHistory("InsuranceCompany",
					LookupTables.LookupTables.GetDescription("InsuranceCompany",_oldQuote.InsuranceCompany.ToString()),
					LookupTables.LookupTables.GetDescription("InsuranceCompany",this.InsuranceCompany.ToString()),
					mode);
				history.BuildNotesForHistory("Dealer",_oldQuote.Dealer,this.Dealer,mode);
				history.BuildNotesForHistory("CompanyDealer",
					LookupTables.LookupTables.GetDescription("CompanyDealer",_oldQuote.CompanyDealer.ToString()),
					LookupTables.LookupTables.GetDescription("CompanyDealer",this.CompanyDealer.ToString()),
					mode);	
				history.BuildNotesForHistory("CloseDate",_oldQuote.CloseDate,this.CloseDate,mode);
				history.BuildNotesForHistory("EnteredBy",_oldQuote.EnteredBy,this.EnteredBy,mode);
				// Terminan Campos TaskControl
				
				// Campos de Policy
				if (IsPolicy)
				{
					history.BuildNotesForHistory("DepartmentID",
						LookupTables.LookupTables.GetDescription("Department",_oldQuote.Policy.DepartmentID.ToString()),
						LookupTables.LookupTables.GetDescription("Department",this.Policy.DepartmentID.ToString()),
						mode);
					history.BuildNotesForHistory("PolicyType",_oldQuote.Policy.PolicyType,this.Policy.PolicyType,mode);
					history.BuildNotesForHistory("PolicyNo",_oldQuote.Policy.PolicyNo,this.Policy.PolicyNo,mode);
					history.BuildNotesForHistory("Certificate",_oldQuote.Policy.Certificate,this.Policy.Certificate,mode);
					history.BuildNotesForHistory("Suffix",_oldQuote.Policy.Suffix,this.Policy.Suffix,mode);
					history.BuildNotesForHistory("LoanNo",_oldQuote.Policy.LoanNo.ToString(),this.Policy.LoanNo.ToString(),mode);
					history.BuildNotesForHistory("Term",_oldQuote.Policy.Term.ToString(),this.Policy.Term.ToString(),mode);
					history.BuildNotesForHistory("EffectiveDate",_oldQuote.Policy.EffectiveDate.ToString(),this.Policy.EffectiveDate.ToString(),mode);
					history.BuildNotesForHistory("ExpirationDate",_oldQuote.Policy.ExpirationDate.ToString(),this.Policy.ExpirationDate.ToString(),mode);
					history.BuildNotesForHistory("Charge",_oldQuote.Policy.Charge.ToString(),this.Policy.Charge.ToString(),mode);
					history.BuildNotesForHistory("TotalPremium",_oldQuote.Policy.TotalPremium.ToString(),this.Policy.TotalPremium.ToString(),mode);
					history.BuildNotesForHistory("Status",_oldQuote.Policy.Status.ToString(),this.Policy.Status.ToString(),mode);
					history.BuildNotesForHistory("CommissionAgency",_oldQuote.Policy.CommissionAgency.ToString(),this.Policy.CommissionAgency.ToString(),mode);
					history.BuildNotesForHistory("CommissionAgencyPercent",_oldQuote.Policy.CommissionAgencyPercent.ToString(),this.Policy.CommissionAgencyPercent.ToString(),mode);
					history.BuildNotesForHistory("CommissionAgent",_oldQuote.Policy.CommissionAgent.ToString(),this.Policy.CommissionAgent.ToString(),mode);
					history.BuildNotesForHistory("CommissionAgentPercent",_oldQuote.Policy.CommissionAgentPercent.ToString(),this.Policy.CommissionAgentPercent.ToString(),mode);
					history.BuildNotesForHistory("CommissionDate",_oldQuote.Policy.CommissionDate.ToString(),this.Policy.CommissionDate.ToString(),mode);
					history.BuildNotesForHistory("CancellationDate",_oldQuote.Policy.CancellationDate.ToString(),this.Policy.CancellationDate.ToString(),mode);
					history.BuildNotesForHistory("CancellationEntryDate",_oldQuote.Policy.CancellationEntryDate.ToString(),this.Policy.CancellationEntryDate.ToString(),mode);
					history.BuildNotesForHistory("CancellationUnearnedPercent",_oldQuote.Policy.CancellationUnearnedPercent.ToString(),this.Policy.CancellationUnearnedPercent.ToString(),mode);
					history.BuildNotesForHistory("ReturnPremium",_oldQuote.Policy.ReturnPremium.ToString(),this.Policy.ReturnPremium.ToString(),mode);
					history.BuildNotesForHistory("ReturnCharge",_oldQuote.Policy.ReturnCharge.ToString(),this.Policy.ReturnCharge.ToString(),mode);
					history.BuildNotesForHistory("CancellationAmount",_oldQuote.Policy.CancellationAmount.ToString(),this.Policy.CancellationAmount.ToString(),mode);
					history.BuildNotesForHistory("CancellationMethod",_oldQuote.Policy.CancellationMethod.ToString(),this.Policy.CancellationMethod.ToString(),mode);
					history.BuildNotesForHistory("CancellationReason",_oldQuote.Policy.CancellationReason.ToString(),this.Policy.CancellationReason.ToString(),mode);
					history.BuildNotesForHistory("ReturnCommissionAgency",_oldQuote.Policy.ReturnCommissionAgency.ToString(),this.Policy.ReturnCommissionAgency.ToString(),mode);
					history.BuildNotesForHistory("ReturnCommissionAgent",_oldQuote.Policy.ReturnCommissionAgent.ToString(),this.Policy.ReturnCommissionAgent.ToString(),mode);
					history.BuildNotesForHistory("PaidAmount",_oldQuote.Policy.PaidAmount.ToString(),this.Policy.PaidAmount.ToString(),mode);
					history.BuildNotesForHistory("PaidDate",_oldQuote.Policy.PaidDate.ToString(),this.Policy.PaidDate.ToString(),mode);
					history.BuildNotesForHistory("AutoAssignPolicy",_oldQuote.Policy.AutoAssignPolicy.ToString(),this.Policy.AutoAssignPolicy.ToString(),mode);
					history.BuildNotesForHistory("InvoiceNumber",_oldQuote.Policy.InvoiceNumber.ToString(),this.Policy.InvoiceNumber.ToString(),mode);
					history.BuildNotesForHistory("FileNumber",_oldQuote.Policy.FileNumber.ToString(),this.Policy.FileNumber.ToString(),mode);
					history.BuildNotesForHistory("IsLeasing",_oldQuote.Policy.IsLeasing.ToString(),this.Policy.IsLeasing.ToString(),mode);
					history.BuildNotesForHistory("PaymentType",_oldQuote.Policy.PaymentType.ToString(),this.Policy.PaymentType.ToString(),mode);
					history.BuildNotesForHistory("IsMaster",_oldQuote.Policy.IsMaster.ToString(),this.Policy.IsMaster.ToString(),mode);
					history.BuildNotesForHistory("TCIDApplicationAuto",_oldQuote.Policy.TCIDApplicationAuto.ToString(),this.Policy.TCIDApplicationAuto.ToString(),mode);
					history.BuildNotesForHistory("TCIDQuote",_oldQuote.Policy.TCIDQuotes.ToString(),this.Policy.TCIDQuotes.ToString(),mode);
					history.BuildNotesForHistory("PrintPolicy",_oldQuote.Policy.PrintPolicy.ToString(),this.Policy.PrintPolicy.ToString(),mode);
					history.BuildNotesForHistory("MasterPolicyID",
						LookupTables.LookupTables.GetDescription("MasterPolicy",_oldQuote.Policy.MasterPolicyID.ToString()),
						LookupTables.LookupTables.GetDescription("MasterPolicy",this.Policy.MasterPolicyID.ToString()),
						mode);
					history.BuildNotesForHistory("Prem_Mes",_oldQuote.Policy.Prem_Mes.ToString(),this.Policy.Prem_Mes.ToString(),mode);
					history.BuildNotesForHistory("PMT1",_oldQuote.Policy.PMT1.ToString(),this.Policy.PMT1.ToString(),mode);
					history.BuildNotesForHistory("PrintDate",_oldQuote.Policy.PrintDate.ToString(),this.Policy.PrintDate.ToString(),mode);
					history.BuildNotesForHistory("OriginatedAt",_oldQuote.Policy.OriginatedAt.ToString(),this.Policy.OriginatedAt.ToString(),mode);
				}// Terminan Campos Policy
				else //For Quote
				{
					history.BuildNotesForHistory("Term",_oldQuote.Term.ToString(),this.Term.ToString(),mode);
					history.BuildNotesForHistory("EffectiveDate",_oldQuote.EffectiveDate.ToString(),this.EffectiveDate.ToString(),mode);
					history.BuildNotesForHistory("ExpirationDate",_oldQuote.ExpirationDate.ToString(),this.ExpirationDate.ToString(),mode);
					if(_oldQuote.TotalPremium.ToString("###,###.00").Trim() != ".00")
					{
						history.BuildNotesForHistory("Charge",_oldQuote.Charge.ToString("###,###.00"),this.Charge.ToString("###,###.00"),mode);
						history.BuildNotesForHistory("TotalPremium",_oldQuote.TotalPremium.ToString("###,###.00"),this.TotalPremium.ToString("###,###.00"),mode);
					}
				}
				

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("TaskControlID.","",this.TaskControlID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.TaskControlID.ToString();

			if (IsPolicy)
				history.Subject = "AUTOPERSONALPOLICY";			
			else
				history.Subject = "QUOTE";			
		
			history.UsersID =  userID;
			history.GetSaveHistory();
		}

		#endregion
	}
#endregion

}
// END CLASS DEFINITION Quote

