using System;
using System.Xml;
using System.Data;
using Baldrich.DBRequest;
using System.Data.SqlClient;
using EPolicy.XmlCooker;
using EPolicy.Audit;

namespace EPolicy.LookupTables
{
	/// <summary>
	/// Summary description for CommissionAgency.
	/// </summary>
	public class CommissionAgency
	{
		public CommissionAgency()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables
    
		//private CommissionAgency  oldCommissionAgency = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  int _commissionAgencyID = 0 ;
		private  int _policyClassID = 0 ;
		private  string _agencyID = String.Empty;
		private  string _policyType = String.Empty;
		private  string _insuranceCompanyID = String.Empty;
		private  string _bankID = String.Empty;
		private  string _companyDealerID = String.Empty;
		private  string _commissionRate = String.Empty ;
		private  string _effectiveDate = String.Empty;
		private  string _commissionEntryDate = DateTime.Now.ToShortDateString();

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

		public int CommissionAgencyID
		{
			get
			{
				return this._commissionAgencyID;
			}
			set
			{
				this._commissionAgencyID = value;
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


		public string AgencyID
		{
			get
			{
				return this._agencyID;
			}
			set
			{
				this._agencyID = value;
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

		public string CommissionRate
		{
			get
			{
				return this._commissionRate;
			}
			set
			{
				this._commissionRate = value;
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

		public string CommissionEntryDate
		{
			get
			{
				return this._commissionEntryDate;
			}
			set
			{
				this._commissionEntryDate = value;
			}
		}

		

		#endregion

		#region Public Methods
		      
		private string GetNextCommissionAgencyID()
		{
			DataTable dt = LookupTables.GetTable("CommissionAgency");
			DataRow[] dr = dt.Select("","CommissionAgencyID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["CommissionAgencyID"] = dr[rec].ItemArray[0].ToString();
				myRow["PolicyClassID"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["CommissionAgencyID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}
		#endregion    

		#region Public Functions

		public void Delete(int CommissionAgencyID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteCommissionAgencyID", 
					this.GetDeleteCommissionAgencyXml(CommissionAgencyID));
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
						//this.CommissionAgencyID = int.Parse(GetNextCommissionAgencyID());
						this.CommissionAgencyID = executor.Insert("AddCommissionAgency",this.GetAddCommissionAgencyXml());
						this.History(this._actionMode,UserID);
//						     this.AuditInsert(UserID);
//						      this.CommitAudit();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
//						this.AuditUpdate(UserID);
						executor.Update("UpdateCommissionAgency",this.GetUpdateCommissionAgencyXml());
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

		public CommissionAgency GetCommissionAgencyByCommissionAgencyID(int CommissionAgencyID)
		{
			try
			{
				DataTable dtCommissionAgency = new DataTable();
				CommissionAgency commissionAgency = new CommissionAgency();
				this.CommissionAgencyID = CommissionAgencyID;

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtCommissionAgency = executor.GetQuery("GetCommissionAgencyByCommissionAgencyID", 
					this.GetCommissionAgencyByCommissionAgencyIDXml());

				this.PolicyClassID = 
					int.Parse(dtCommissionAgency.Rows[0]["PolicyClassID"].ToString().Trim());

				this.AgencyID = 
					dtCommissionAgency.Rows[0]["AgencyID"].ToString().Trim();

				this.PolicyType = 
					dtCommissionAgency.Rows[0]["PolicyType"].ToString().Trim();

				this.InsuranceCompanyID = 
					dtCommissionAgency.Rows[0]["InsuranceCompanyID"].ToString().Trim();

				this.BankID = 
					dtCommissionAgency.Rows[0]["BankID"].ToString().Trim();

				this.CompanyDealerID = 
					dtCommissionAgency.Rows[0]["CompanyDealerID"].ToString().Trim();

				this.CommissionRate = 
					dtCommissionAgency.Rows[0]["CommissionRate"].ToString().Trim();

				this.EffectiveDate = 
					dtCommissionAgency.Rows[0]["EffectiveDate"]!= System.DBNull.Value?((DateTime) dtCommissionAgency.Rows[0]["EffectiveDate"]).ToShortDateString():"";
				//dtCommissionAgency.Rows[0]["EffectiveDate"].ToString().Trim();

				this.CommissionEntryDate = 
					dtCommissionAgency.Rows[0]["CommissionEntryDate"]!= System.DBNull.Value?((DateTime) dtCommissionAgency.Rows[0]["CommissionEntryDate"]).ToShortDateString():"";
				//dtCommissionAgency.Rows[0]["CommissionEntryDate"].ToString().Trim();

            return this;

			}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Commission.", ex);
			}
		}


		public static DataTable GetCommissionAgencyByAgencyID(string AgencyID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				
				DataTable dt = executor.GetQuery("GetCommissionAgencyByAgencyID",
					GetCommissionAgencyByAgencyIDXml(AgencyID));
						
				return dt;

			}

			catch(Exception ex)
		
			{

				throw new Exception(
					"Could not retrieve information for this Commission.", ex);
				
			
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
						throw new Exception("The Commission Agency make you are attempting to " +
							"relate to this Commission Agency does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Commission Agency.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Commission Agency.", Ex);
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
						throw new Exception("The Commission Agency you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Commission Agency before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Commission Agency.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Commission Agency.", Ex);
			}
		}
  
		private XmlDocument GetDeleteCommissionAgencyXml(int CommissionAgencyID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CommissionAgencyID",
				SqlDbType.Int, 0, this.CommissionAgencyID.ToString(),
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
        
		private XmlDocument GetAddCommissionAgencyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.Char, 3, this.AgencyID.ToString(),
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

			DbRequestXmlCooker.AttachCookItem("CommissionRate",
				SqlDbType.VarChar, 10, this.CommissionRate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.VarChar, 12, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionEntryDate",
				SqlDbType.VarChar, 4, this.CommissionEntryDate.ToString(),
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

		private XmlDocument GetUpdateCommissionAgencyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[10];

			DbRequestXmlCooker.AttachCookItem("CommissionAgencyID",
				SqlDbType.Int, 0, this.CommissionAgencyID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.Char, 3, this.AgencyID.ToString(),
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

			DbRequestXmlCooker.AttachCookItem("CommissionRate",
				SqlDbType.VarChar, 10, this.CommissionRate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionEntryDate",
				SqlDbType.DateTime, 0, this.CommissionEntryDate.ToString(),
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

		private XmlDocument GetCommissionAgencyByCommissionAgencyIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CommissionAgencyID",
				SqlDbType.Int, 0, this.CommissionAgencyID.ToString(),
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

		private static XmlDocument GetCommissionAgencyByAgencyIDXml(string AgencyID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.Char, 3, AgencyID.ToString(),
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

            if (this.EffectiveDate.Trim() == "")
            {
                errorMessage += "The effective Date cannot be empty.  ";
                found = true;
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
				EPolicy.LookupTables.CommissionAgency oldCommissionAgency = new EPolicy.LookupTables.CommissionAgency();
				oldCommissionAgency = oldCommissionAgency.GetCommissionAgencyByCommissionAgencyID(this.CommissionAgencyID);//userID);
				
				history.BuildNotesForHistory("PolicyClassID",oldCommissionAgency.PolicyClassID.ToString(),this.PolicyClassID.ToString(),mode);
				history.BuildNotesForHistory("AgencyID",oldCommissionAgency.AgencyID,this.AgencyID,mode);
				history.BuildNotesForHistory("PolicyType",oldCommissionAgency.PolicyType,this.PolicyType,mode);
				history.BuildNotesForHistory("InsuranceCompanyID",oldCommissionAgency.InsuranceCompanyID,this.InsuranceCompanyID,mode);
				history.BuildNotesForHistory("BankID",oldCommissionAgency.BankID,this.BankID,mode);
				history.BuildNotesForHistory("CompanyDealerID",oldCommissionAgency.CompanyDealerID,this.CompanyDealerID,mode);
				history.BuildNotesForHistory("CommissionRate",oldCommissionAgency.CommissionRate,this.CommissionRate,mode);
				history.BuildNotesForHistory("EffectiveDate",oldCommissionAgency.EffectiveDate,this.EffectiveDate,mode);
				history.BuildNotesForHistory("CommissionEntryDate",oldCommissionAgency.CommissionEntryDate,this.CommissionEntryDate,mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("CommissionAgencyID.","",this.CommissionAgencyID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.CommissionAgencyID.ToString();
			history.Subject = "COMMISSIONAGENCY";			
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

		#endregion
	}

}
