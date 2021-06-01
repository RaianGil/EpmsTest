using System;
using System.Xml;
using System.Data;
using Baldrich.DBRequest;
using EPolicy.Audit;
using System.Data.SqlClient;
using System.Collections;
using EPolicy.XmlCooker;

namespace EPolicy.LookupTables
{
	/// <summary>
	/// Summary description for IncentiveSupplier.
	/// </summary>
	public class IncentiveSupplier
	{
		public IncentiveSupplier()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables
    
		//private IncentiveSupplier  oldIncentiveSupplier = null;
		private DataTable _navigationViewModelTable;
		private int     _actionMode = (int)Mode.UPDATE;
		private  int    _incentiveSupplierID = 0 ;
		private  int    _policyClassID = 0 ;
		private  string _supplierID = String.Empty;
		private  string _policyType = String.Empty;
		private  string _insuranceCompanyID = String.Empty;
		private  string _bankID = String.Empty;
		private  string _companyDealerID = String.Empty;
		private  string _incentiveRate = String.Empty ;
		private  string _effectiveDate = String.Empty;
		private  string _incentiveEntryDate = DateTime.Now.ToShortDateString();
		private  int    _supplierLevel = 0;
		private  bool   _rateAmt = true;
		private  float  _incentiveAmount = 0;

		#endregion

		#region Properties

		public DataTable NavigationViewModelTable
		{
			get
			{
				return this._navigationViewModelTable;
			}
			set
			{
				this._navigationViewModelTable = value;
			}
		}
		
		public int ActionMode
		{
			get
			{
				return this._actionMode;
			}
			set
			{
				this._actionMode = value;
			}
		}

		public int IncentiveSupplierID
		{
			get
			{
				return this._incentiveSupplierID;
			}
			set
			{
				this._incentiveSupplierID = value;
			}
		}


		public int PolicyClassID
		{
			get
			{
				return this._policyClassID;
			}
			set
			{
				this._policyClassID = value;
			}
		}


		public string SupplierID
		{
			get
			{
				return this._supplierID;
			}
			set
			{
				this._supplierID = value;
			}
		}
		
		public string PolicyType
		{
			get
			{
				return this._policyType;
			}
			set
			{
				this._policyType = value;
			}
		}
        
		public string InsuranceCompanyID
		{
			get
			{
				return this._insuranceCompanyID;
			}
			set
			{
				this._insuranceCompanyID = value;
			}
		}
        
		public string BankID
		{
			get
			{
				return this._bankID;
			}
			set
			{
				this._bankID = value;
			}
		}
        
		public string CompanyDealerID
		{
			get
			{
				return this._companyDealerID;
			}
			set
			{
				this._companyDealerID = value;
			}
		}

		public string IncentiveRate
		{
			get
			{
				return this._incentiveRate;
			}
			set
			{
				this._incentiveRate = value;
			}
		}


		public string EffectiveDate
		{
			get
			{
				return this._effectiveDate;
			}
			set
			{
				this._effectiveDate = value;
			}
		}

		public string IncentiveEntryDate
		{
			get
			{
				return this._incentiveEntryDate;
			}
			set
			{
				this._incentiveEntryDate = value;
			}
		}

		public int SupplierLevel
		{
			get
			{
				return this._supplierLevel;
			}
			set
			{
				this._supplierLevel = value;
			}
		}

		public bool RateAmt
		{
			get
			{
				return this._rateAmt;
			}
			set
			{
				this._rateAmt = value;
			}
		}

		public float IncentiveAmount
		{
			get
			{
				return this._incentiveAmount;
			}
			set
			{
				this._incentiveAmount = value;
			}
		}

		

		#endregion

		#region Public Methods
		      
		private string GetNextIncentiveSupplierID()
		{
			DataTable dt = LookupTables.GetTable("IncentiveSupplier");
			DataRow[] dr = dt.Select("","IncentiveSupplierID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["IncentiveSupplierID"] = dr[rec].ItemArray[0].ToString();
				myRow["PolicyClassID"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["IncentiveSupplierID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}
		#endregion    

		#region Public Functions

		public void Delete(int IncentiveSupplierID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteIncentiveSupplierID", 
					this.GetDeleteIncentiveSupplierXml(IncentiveSupplierID));
			}
			catch(Exception ex)
			{
				this.HandleDeleteError(ex);
			}
		}

		public void Save(int UserID)
		{
			this.Validate();
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				executor.BeginTrans();
				
				switch (this.ActionMode)
				{
					case 1: //ADD
						//this.IncentiveSupplierID = int.Parse(GetNextIncentiveSupplierID());
						this.IncentiveSupplierID = executor.Insert("AddIncentiveSupplier", this.GetAddIncentiveSupplierXml());
						this.History(this._actionMode,UserID);
//						this.AuditInsert(UserID);
//						this.CommitAudit();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
//						this.AuditUpdate(UserID);
						executor.Update("UpdateIncentiveSupplier",this.GetUpdateIncentiveSupplierXml());
//						this.CommitAudit();//Commit audit;
						break;
				}
				executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleSaveError(xcp);
			}
		}

		public IncentiveSupplier GetIncentiveSupplierByIncentiveSupplierID(int IncentiveSupplierID)
		{
			try
			{
				DataTable dtIncentiveSupplier = new DataTable();
				IncentiveSupplier incentiveSupplier = new  IncentiveSupplier();
				this.IncentiveSupplierID = IncentiveSupplierID;
		


				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtIncentiveSupplier = executor.GetQuery("GetIncentiveSupplierByIncentiveSupplierID", 
					this.GetIncentiveSupplierByIncentiveSupplierIDXml());

				this.PolicyClassID = 
					int.Parse(dtIncentiveSupplier.Rows[0]["PolicyClassID"].ToString().Trim());

				this.SupplierID = 
					dtIncentiveSupplier.Rows[0]["SupplierID"].ToString().Trim();

				this.PolicyType = 
					dtIncentiveSupplier.Rows[0]["PolicyType"].ToString().Trim();

				this.InsuranceCompanyID = 
					dtIncentiveSupplier.Rows[0]["InsuranceCompanyID"].ToString().Trim();

				this.BankID = 
					dtIncentiveSupplier.Rows[0]["BankID"].ToString().Trim();

				this.CompanyDealerID = 
					dtIncentiveSupplier.Rows[0]["CompanyDealerID"].ToString().Trim();

				this.IncentiveRate = 
					dtIncentiveSupplier.Rows[0]["IncentiveRate"].ToString().Trim();

				this.EffectiveDate = 
					dtIncentiveSupplier.Rows[0]["EffectiveDate"]!= System.DBNull.Value?((DateTime) dtIncentiveSupplier.Rows[0]["EffectiveDate"]).ToShortDateString():"";
				
				this.IncentiveEntryDate = 
					dtIncentiveSupplier.Rows[0]["IncentiveEntryDate"]!= System.DBNull.Value?((DateTime) dtIncentiveSupplier.Rows[0]["IncentiveEntryDate"]).ToShortDateString():"";
				
				this.SupplierLevel = 
					int.Parse(dtIncentiveSupplier.Rows[0]["SupplierLevel"].ToString().Trim());
				
				this.RateAmt =
					bool.Parse(dtIncentiveSupplier.Rows[0]["RateAmt"].ToString());

				this.IncentiveAmount = 
					float.Parse(dtIncentiveSupplier.Rows[0]["IncentiveAmount"].ToString().Trim());


				return this;

			}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Incentive.", ex);
			}
		}


		public static DataTable GetIncentiveSupplierBySupplierID(string SupplierID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				
				DataTable dt = executor.GetQuery("GetIncentiveSupplierBySupplierID",
					GetIncentiveSupplierBySupplierIDXml(SupplierID));
						
				return dt;

			}

			catch(Exception ex)
		
			{
				throw new Exception(
					"Could not retrieve information for this Incentive.", ex);
			}

		}

		#endregion

		#region Private Functions

		private void HandleSaveError(Exception Ex)
		{
			switch(Ex.GetBaseException().GetType().FullName)
			{
				case "System.Data.SqlClient.SqlException":
					SqlException sqlException = (SqlException)Ex.GetBaseException();
				switch(sqlException.Number)
				{
					case 547:
						throw new Exception("The Incentive Supplier make you are attempting to " +
							"relate to this Incentive Supplier does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Incentive Supplier.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Incentive Supplier.", Ex);
			}
		}
		private void HandleDeleteError(Exception Ex)
		{
			switch(Ex.GetBaseException().GetType().FullName)
			{
				case "System.Data.SqlClient.SqlException":
					SqlException sqlException = (SqlException)Ex.GetBaseException();
				switch(sqlException.Number)
				{
					case 547:
						throw new Exception("The Incentive Supplier you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Incentive Supplier before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Incentive Supplier.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Incentive Supplier.", Ex);
			}
		}
  
		private XmlDocument GetDeleteIncentiveSupplierXml(int IncentiveSupplierID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("IncentiveSupplierID",
				SqlDbType.Int, 0, this.IncentiveSupplierID.ToString(),
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
        
		private XmlDocument GetAddIncentiveSupplierXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[12];

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 3, this.SupplierID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.VarChar, 20, this.PolicyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.VarChar, 20, this.InsuranceCompanyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("BankID",
				SqlDbType.VarChar, 14, this.BankID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.VarChar, 2, this.CompanyDealerID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("IncentiveRate",
				SqlDbType.VarChar, 10, this.IncentiveRate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.VarChar, 12, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveEntryDate",
				SqlDbType.VarChar, 4, this.IncentiveEntryDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SupplierLevel",
				SqlDbType.Int, 0, this.SupplierLevel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("RateAmt",
				SqlDbType.Bit, 0, this.RateAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveAmount",
				SqlDbType.Float, 0, this.IncentiveAmount.ToString(),
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

		private XmlDocument GetUpdateIncentiveSupplierXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[13];
			
			DbRequestXmlCooker.AttachCookItem("IncentiveSupplierID",
				SqlDbType.Int, 0, this.IncentiveSupplierID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 3, this.SupplierID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.VarChar, 20, this.PolicyType.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.VarChar, 20, this.InsuranceCompanyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("BankID",
				SqlDbType.VarChar, 14, this.BankID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.VarChar, 2, this.CompanyDealerID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("IncentiveRate",
				SqlDbType.VarChar, 10, this.IncentiveRate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.VarChar, 12, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveEntryDate",
				SqlDbType.VarChar, 4, this.IncentiveEntryDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SupplierLevel",
				SqlDbType.Int, 0, this.SupplierLevel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("RateAmt",
				SqlDbType.Bit, 0, this.RateAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IncentiveAmount",
				SqlDbType.Float, 0, this.IncentiveAmount.ToString(),
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

		private XmlDocument GetIncentiveSupplierByIncentiveSupplierIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("IncentiveSupplierID",
				SqlDbType.Int, 0, this.IncentiveSupplierID.ToString(),
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

		private static XmlDocument GetIncentiveSupplierBySupplierIDXml(string SupplierID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 0, SupplierID.ToString(),
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

		private void Validate()
		{
			string errorMessage = String.Empty;
			bool found = false;


			if(this.PolicyClassID == 0)
			{
				errorMessage += "The Line of Business cannot be empty.  ";
				found = true;
			}


			if(this.SupplierLevel <= 1)
			{
				//errorMessage += "The Supplier level has greater than  one (1).";
				//found = true;
			}


			if (found == true)
			{
				throw new Exception(errorMessage);
			}
	
		}
	
	
		#region History

		private void History(int mode, int userID)
		{
			Audit.History history = new Audit.History();
			
			if(_actionMode == 2)
			{
				EPolicy.LookupTables.IncentiveSupplier oldIncentiveSupplier = new EPolicy.LookupTables.IncentiveSupplier();
				oldIncentiveSupplier = oldIncentiveSupplier.GetIncentiveSupplierByIncentiveSupplierID(this.IncentiveSupplierID);//userID);

				history.BuildNotesForHistory("PolicyClassID",oldIncentiveSupplier.PolicyClassID.ToString(),this.PolicyClassID.ToString(),mode);
				history.BuildNotesForHistory("SupplierID",oldIncentiveSupplier.SupplierID.ToString(),this.SupplierID.ToString(),mode);
				history.BuildNotesForHistory("PolicyType",oldIncentiveSupplier.PolicyType.ToString(),this.PolicyType.ToString(),mode);
				history.BuildNotesForHistory("InsuranceCompanyID",
					LookupTables.GetDescription("InsuranceCompany",oldIncentiveSupplier.InsuranceCompanyID.ToString()),
					LookupTables.GetDescription("InsuranceCompany",this.InsuranceCompanyID.ToString()),
					mode);
				history.BuildNotesForHistory("CompanyDealerID",
					LookupTables.GetDescription("CompanyDealer",oldIncentiveSupplier.CompanyDealerID.ToString()),
					LookupTables.GetDescription("CompanyDealer",this.CompanyDealerID.ToString()),
					mode);
				history.BuildNotesForHistory("IncentiveRate",oldIncentiveSupplier.IncentiveRate,this.IncentiveRate,mode);
				history.BuildNotesForHistory("EffectiveDate",oldIncentiveSupplier.EffectiveDate.ToString(),this.EffectiveDate.ToString(),mode);
				history.BuildNotesForHistory("IncentiveEntryDate",oldIncentiveSupplier.IncentiveEntryDate,this.IncentiveEntryDate,mode);
				history.BuildNotesForHistory("SupplierLevel",oldIncentiveSupplier.SupplierLevel.ToString(),this.SupplierLevel.ToString(),mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("IncentiveSupplierID.","",this.IncentiveSupplierID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.IncentiveSupplierID.ToString();
			history.Subject = "INCENTIVE SUPPLIER";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}

//		private void AddMutations(ref Action Action, 
//			
//			IncentiveSupplier CurrentIncentiveSupplier, bool IsInsert)
//		{
//
//			Action.AddMutation("PolicyClassID", CurrentIncentiveSupplier.PolicyClassID, 
//				this.PolicyClassID, "IncentiveSupplier", IsInsert, 
//				(this.PolicyClassID == 0) ? true : false);
//
//			Action.AddMutation("SupplierID", CurrentIncentiveSupplier.SupplierID, 
//				this.SupplierID, "IncentiveSupplier", IsInsert, 
//				(this.SupplierID == string.Empty) ? true : false);
//
//			Action.AddMutation("PolicyType", CurrentIncentiveSupplier.PolicyType, 
//				this.PolicyType, "IncentiveSupplier", IsInsert, 
//				(this.PolicyType == string.Empty) ? true : false);
//
//			Action.AddMutation("InsuranceCompanyID", CurrentIncentiveSupplier.InsuranceCompanyID, 
//				this.InsuranceCompanyID, "IncentiveSupplier", IsInsert, 
//				(this.InsuranceCompanyID == string.Empty) ? true : false);
//
//			Action.AddMutation("BankID", CurrentIncentiveSupplier.BankID, 
//				this.BankID, "IncentiveSupplier", IsInsert, 
//				(this.BankID == string.Empty) ? true : false);
//
//			Action.AddMutation("CompanyDealerID", CurrentIncentiveSupplier.CompanyDealerID, 
//				this.CompanyDealerID, "IncentiveSupplier", IsInsert, 
//				(this.CompanyDealerID == string.Empty) ? true : false);
//
//			Action.AddMutation("IncentiveRate", CurrentIncentiveSupplier.IncentiveRate, 
//				this.IncentiveRate, "IncentiveSupplier", IsInsert, 
//				(this.IncentiveRate == string.Empty) ? true : false);
//
//			Action.AddMutation("EffectiveDate", CurrentIncentiveSupplier.EffectiveDate, 
//				this.EffectiveDate, "IncentiveSupplier", IsInsert, 
//				(this.EffectiveDate == string.Empty) ? true : false);
//
//			Action.AddMutation("IncentiveEntryDate", CurrentIncentiveSupplier.IncentiveEntryDate, 
//				this.IncentiveEntryDate, "IncentiveSupplier", IsInsert, 
//				(this.IncentiveEntryDate == string.Empty) ? true : false);
//
//			Action.AddMutation("SupplierLevel", CurrentIncentiveSupplier.SupplierLevel, 
//				this.SupplierLevel, "IncentiveSupplier", IsInsert, 
//				(this.SupplierLevel == 0) ? true : false);
//
//		}

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
#endregion
	}

