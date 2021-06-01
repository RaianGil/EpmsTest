using System;
using System.Xml;
using System.Data;
using Baldrich.DBRequest;
using EPolicy.LookupTables;
using EPolicy.Audit;
using System.Data.SqlClient;
using System.Collections;
using EPolicy.XmlCooker;


namespace EPolicy.LookupTables
{
	/// <summary>
	/// Summary description for Agency.
	/// </summary>
	public class Agency
	{
		public Agency()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

        #region Variables
    
		//private Agency  oldAgency = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  string _agencyID = String.Empty;
		private  string _agencyDesc = String.Empty;
		private  string _agy_addr1 = String.Empty;
		private  string _agy_addr2 = String.Empty;
		private  string _agy_city = String.Empty;
		private  string _agy_st = String.Empty;
		private  string _agy_zip = String.Empty ;
		private  string _agy_phone = String.Empty ;
		private  string _agy_actdt = DateTime.Now.ToShortDateString();

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


		public string AgencyDesc
		{
			get
			{
				return this._agencyDesc;
			}
			set
			{
				this._agencyDesc = value;
			}
		}

		public string agy_addr1
		{
			get
			{
				return this._agy_addr1;
			}
			set
			{
				this._agy_addr1 = value;
			}
		}
        
		public string agy_addr2
		{
			get
			{
				return this._agy_addr2;
			}
			set
			{
				this._agy_addr2 = value;
			}
		}
        
		public string agy_city
		{
			get
			{
				return this._agy_city;
			}
			set
			{
				this._agy_city = value;
			}
		}
        
		public string agy_st
		{
			get
			{
				return this._agy_st;
			}
			set
			{
				this._agy_st = value;
			}
		}

		public string agy_zip
		{
			get
			{
				return this._agy_zip;
			}
			set
			{
				this._agy_zip = value;
			}
		}


		public string agy_phone
		{
			get
			{
				return this._agy_phone;
			}
			set
			{
				this._agy_phone = value;
			}
		}

		public string agy_actdt
		{
			get
			{
				return this._agy_actdt;
			}
			set
			{
				this._agy_actdt = value;
			}
		}
		#endregion

		#region Public Methods
		      
		private string GetNextAgencyID()
		{
			DataTable dt = LookupTables.GetTable("Agency");
			DataRow[] dr = dt.Select("","AgencyID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["AgencyID"] = dr[rec].ItemArray[0].ToString();
				myRow["AgencyDesc"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["AgencyID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}
		#endregion

		#region Public Functions

		public void Delete(string AgencyID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteAgencyID", 
					this.GetDeleteAgencyXml(AgencyID));
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
						
						this.AgencyID = GetNextAgencyID();
						executor.Update("AddAgency", this.GetInsertAgencyXml());
						this.History(this._actionMode,UserID);
//							this.AuditInsert(UserID);
//						this.CommitAudit();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
//						this.AuditUpdate(UserID);
						executor.Update("UpdateAgency",
							this.GetUpdateAgencyXml());
//							this.CommitAudit();//Commit audit;
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

		public Agency GetAgency(string AgencyID)
		{
			try
			{
				DataTable dtAgency = new DataTable();
				Agency agency = new Agency();
				this.AgencyID = AgencyID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtAgency = executor.GetQuery("GetagencyByAgencyID", 
					this.GetAgencyByAgencyIDXml());

				this.AgencyDesc = 
					dtAgency.Rows[0]["AgencyDesc"].ToString().Trim();

				this.agy_addr1 = 
					dtAgency.Rows[0]["agy_addr1"].ToString().Trim();

				this.AgencyID = 
					dtAgency.Rows[0]["AgencyID"].ToString().Trim();

				this.agy_addr2 = 
					dtAgency.Rows[0]["agy_addr2"].ToString().Trim();

				this.agy_city = 
					dtAgency.Rows[0]["agy_city"].ToString().Trim();

				this.agy_st = 
					dtAgency.Rows[0]["agy_st"].ToString().Trim();

				this.agy_zip = 
					dtAgency.Rows[0]["agy_zip"].ToString().Trim();

				this.agy_phone = 
					dtAgency.Rows[0]["agy_phone"].ToString().Trim();

				this.agy_actdt = 
					dtAgency.Rows[0]["agy_actdt"]!= System.DBNull.Value?((DateTime) dtAgency.Rows[0]["agy_actdt"]).ToShortDateString():"";
		
				return this;

			}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Agency.", ex);
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
						throw new Exception("The Agency make you are attempting to " +
							"relate to this Agency does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Agency.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Agency.", Ex);
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
						throw new Exception("The Agency you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Agency before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Agency.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Agency.", Ex);
			}
		}
  
		private XmlDocument GetDeleteAgencyXml(string AgencyID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.VarChar, 3, this.AgencyID.ToString(),
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
        
		private XmlDocument GetInsertAgencyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];

			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.VarChar, 3, this.AgencyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgencyDesc",
				SqlDbType.VarChar, 30,this.AgencyDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agy_addr1",
				SqlDbType.VarChar, 20,this.agy_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agy_addr2",
				SqlDbType.VarChar, 20,this.agy_addr2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agy_city",
				SqlDbType.VarChar, 14,this.agy_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agy_st",
				SqlDbType.VarChar, 2,this.agy_st.ToString(),
				ref cookItems);			
			
			DbRequestXmlCooker.AttachCookItem("agy_zip",
				SqlDbType.VarChar, 10,this.agy_zip.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agy_phone",
				SqlDbType.VarChar, 14,this.agy_phone.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agy_actdt",
				SqlDbType.VarChar, 4,this.agy_actdt.ToString(),
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

		private XmlDocument GetUpdateAgencyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];

			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.VarChar, 3, this.AgencyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgencyDesc",
				SqlDbType.VarChar, 30,this.AgencyDesc.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agy_addr1",
				SqlDbType.VarChar, 20,this.agy_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agy_addr2",
				SqlDbType.VarChar, 20,this.agy_addr2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agy_city",
				SqlDbType.VarChar, 14,this.agy_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agy_st",
				SqlDbType.VarChar, 2,this.agy_st.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agy_zip",
				SqlDbType.VarChar, 10,this.agy_zip.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agy_phone",
				SqlDbType.VarChar, 14,this.agy_phone.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agy_actdt",
				SqlDbType.VarChar, 4,this.agy_actdt.ToString(),
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

		private XmlDocument GetAgencyByAgencyIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("AgencyID",
				SqlDbType.Int, 0, this.AgencyID.ToString(),
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

			if(this.AgencyDesc == "")
			{
				errorMessage += "The Agency cannot be empty.  ";
				found = true;
			}

			DataTable dt =  LookupTables.GetTable("Agency");
			
			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["AgencyDesc"].ToString().Trim().ToUpper() == this.AgencyDesc.Trim().ToUpper())
					{
						errorMessage = "This Agency Description is Already exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["AgencyDesc"].ToString().Trim() == this.AgencyDesc.Trim() && dt.Rows[i]["AgencyID"].ToString().Trim() != this.AgencyID.ToString().Trim())
					{
						errorMessage = "The Agency Description is Already exist.";
						found = true;
					}
				}
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
				EPolicy.LookupTables.Agency oldAgency = new EPolicy.LookupTables.Agency();
				oldAgency = oldAgency.GetAgency(this.AgencyID);//userID);

				history.BuildNotesForHistory("AgencyDesc",oldAgency.AgencyDesc,this.AgencyDesc,mode);
				history.BuildNotesForHistory("agy_addr1",oldAgency.agy_addr1,this.agy_addr1,mode);
				history.BuildNotesForHistory("agy_addr2",oldAgency.agy_addr2,this.agy_addr2,mode);
				history.BuildNotesForHistory("agy_city",oldAgency.agy_city,this.agy_city,mode);
				history.BuildNotesForHistory("agy_st",oldAgency.agy_st,this.agy_st,mode);
				history.BuildNotesForHistory("agy_zip",oldAgency.agy_zip,this.agy_zip,mode);
				history.BuildNotesForHistory("agy_phone",oldAgency.agy_phone,this.agy_phone,mode);
				

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("AgencyID.","",this.AgencyID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.AgencyID.ToString();
			history.Subject = "AGENCY";			
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