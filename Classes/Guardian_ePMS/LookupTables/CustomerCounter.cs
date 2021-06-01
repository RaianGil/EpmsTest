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
	/// Summary description for CustomerCounter.
	/// </summary>
	public class CustomerCounter
	{
		public CustomerCounter()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables
    
		//private CustomerCounter  oldCustomerCounter = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  int _customerCounterID = 0;
		private  string _customerCounterDesc = String.Empty;
		private  int _sequenceStart = 0;
		private  int _sequeneEnd = 0;
		private  int _sequenceCurrent = 0;
		private  int _sequenceWarning = 0;
		private  string _sequencePrefix = String.Empty;
		private  string _sequenceSeparator = String.Empty ;
		private  string _entryDate = DateTime.Now.ToShortDateString();

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

		public int CustomerCounterID
		{
			get
			{
				return this._customerCounterID;
			}
			set
			{
				this._customerCounterID = value;
			}
		}


		public string CustomerCounterDesc
		{
			get
			{
				return this._customerCounterDesc;
			}
			set
			{
				this._customerCounterDesc = value;
			}
		}


		public int SequenceStart
		{
			get
			{
				return this._sequenceStart;
			}
			set
			{
				this._sequenceStart = value;
			}
		}
		public int SequenceEnd
		{
			get
			{
				return this._sequeneEnd;
			}
			set
			{
				this._sequeneEnd = value;
			}
		}
        
		public int SequenceCurrent
		{
			get
			{
				return this._sequenceCurrent;
			}
			set
			{
				this._sequenceCurrent = value;
			}
		}
        
		public int SequenceWarning
		{
			get
			{
				return this._sequenceWarning;
			}
			set
			{
				this._sequenceWarning = value;
			}
		}
        
		public string SequencePrefix
		{
			get
			{
				return this._sequencePrefix;
			}
			set
			{
				this._sequencePrefix = value;
			}
		}

		public string SequenceSeparator
		{
			get
			{
				return this._sequenceSeparator;
			}
			set
			{
				this._sequenceSeparator = value;
			}
		}


		public string EntryDate
		{
			get
			{
				return this._entryDate;
			}
			set
			{
				this._entryDate = value;
			}
		}
		
		#endregion

		#region Public Methods
		      
		private int GetNextCustomerCounterID()
		{
			DataTable dt = LookupTables.GetTable("CustomerCounter");
			DataRow[] dr = dt.Select("","CustomerCounterID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["CustomerCounterID"] = dr[rec].ItemArray[0].ToString();
				myRow["CustomerCounterDesc"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["CustomerCounterID"].ToString())+1;
						
			return (int.Parse(ID.ToString().PadLeft(4,'0'))); 
		}
		#endregion    

		#region Public Functions

		public void Delete(int CustomerCounterID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteCustomerCounterID", 
					this.GetDeleteCustomerCounterXml(CustomerCounterID));
			}
			catch(Exception ex)
			{
				this.HandleDeleteError(ex);
			}
		}

		public void Save(int UserID)
		{
			//int CustomerCounterID = 0;
			this.Validate();
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				executor.BeginTrans();
				
				switch (this.ActionMode)
				{
					case 1: //ADD
						this.CustomerCounterID = GetNextCustomerCounterID();
						executor.Insert("AddCustomerCounter",this.GetAddCustomerCounterXml());
						this.History(this._actionMode,UserID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
//						this.AuditUpdate(UserID);
						executor.Update("UpdateCustomerCounterTable",this.GetUpdateCustomerCounterTableXml());
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

		public CustomerCounter GetCustomerCounterByCustomerCounterID(int CustomerCounterID)
		{
			try
			{
				DataTable dtCustomerCounter = new DataTable();
				CustomerCounter customerCounter = new CustomerCounter();
				this.CustomerCounterID = CustomerCounterID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtCustomerCounter = executor.GetQuery("GetcustomerCounterByCustomerCounterID", 
					this.GetCustomerCounterByCustomerCounterIDXml());

				this.CustomerCounterDesc = 
					dtCustomerCounter.Rows[0]["CustomerCounterDesc"].ToString().Trim();

				this.SequenceStart = 
					int.Parse(dtCustomerCounter.Rows[0]["SequenceStart"].ToString().Trim());

				this.SequenceEnd = 
					int.Parse(dtCustomerCounter.Rows[0]["SequenceEnd"].ToString().Trim());

				this.CustomerCounterID = 
					int.Parse(dtCustomerCounter.Rows[0]["CustomerCounterID"].ToString().Trim());

				this.SequenceCurrent = 
					int.Parse(dtCustomerCounter.Rows[0]["SequenceCurrent"].ToString().Trim());

				this.SequenceWarning = 
					int.Parse(dtCustomerCounter.Rows[0]["SequenceWarning"].ToString().Trim());

				this.SequencePrefix = 
					dtCustomerCounter.Rows[0]["SequencePrefix"].ToString().Trim();

				this.SequenceSeparator = 
					dtCustomerCounter.Rows[0]["SequenceSeparator"].ToString().Trim();

				this.EntryDate = 
					dtCustomerCounter.Rows[0]["EntryDate"]!= System.DBNull.Value?((DateTime) dtCustomerCounter.Rows[0]["EntryDate"]).ToShortDateString():"";

				return this;
				
			}



			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Customer Counter.", ex);
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
						throw new Exception("The Customer Counter make you are attempting to " +
							"relate to this Customer Counter does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the CustomerCounter.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Customer Counter.", Ex);
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
						throw new Exception("The Customer Counter you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Customer Counter before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Customer Counter.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Customer Counter.", Ex);
			}
		}
  
		private XmlDocument GetDeleteCustomerCounterXml(int CustomerCounterID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CustomerCounterID",
				SqlDbType.Int, 0, CustomerCounterID.ToString(),
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


        private XmlDocument GetAddCustomerCounterXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];

			DbRequestXmlCooker.AttachCookItem("CustomerCounterID",
				SqlDbType.Int, 0, this.CustomerCounterID.ToString(),
				ref cookItems);

			
			DbRequestXmlCooker.AttachCookItem("CustomerCounterDesc",
				SqlDbType.VarChar, 50, this.CustomerCounterDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SequenceStart",
				SqlDbType.Int, 0, this.SequenceStart.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SequenceEnd",
				SqlDbType.Int, 0, this.SequenceEnd.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SequenceCurrent",
				SqlDbType.Int, 0, this.SequenceCurrent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SequenceWarning",
				SqlDbType.Int, 0, this.SequenceWarning.ToString(),
				ref cookItems);
		
			DbRequestXmlCooker.AttachCookItem("SequencePrefix",
				SqlDbType.VarChar, 50, this.SequencePrefix.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SequenceSeparator",
				SqlDbType.VarChar, 50, this.SequenceSeparator.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EntryDate",
				SqlDbType.VarChar, 4, this.EntryDate.ToString(),
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

		private XmlDocument GetUpdateCustomerCounterTableXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];

			DbRequestXmlCooker.AttachCookItem("CustomerCounterID",
				SqlDbType.Int, 0, this.CustomerCounterID.ToString(),
				ref cookItems);

			
			DbRequestXmlCooker.AttachCookItem("CustomerCounterDesc",
				SqlDbType.VarChar, 50, this.CustomerCounterDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SequenceStart",
				SqlDbType.Int, 0, this.SequenceStart.ToString(),
				ref cookItems);

			
			DbRequestXmlCooker.AttachCookItem("SequenceEnd",
				SqlDbType.Int, 0, this.SequenceEnd.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SequenceCurrent",
				SqlDbType.Int, 0, this.SequenceCurrent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SequenceWarning",
				SqlDbType.Int, 0, this.SequenceWarning.ToString(),
				ref cookItems);
		
			DbRequestXmlCooker.AttachCookItem("SequencePrefix",
				SqlDbType.VarChar, 50, this.SequencePrefix.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SequenceSeparator",
				SqlDbType.VarChar, 50, this.SequenceSeparator.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EntryDate",
				SqlDbType.VarChar, 4, this.EntryDate.ToString(),
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

		private XmlDocument GetCustomerCounterByCustomerCounterIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CustomerCounterID",
				SqlDbType.Int, 0, this.CustomerCounterID.ToString(),
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

			if(this.CustomerCounterDesc == "")
			{
				errorMessage += "The Customer Counter cannot be empty.  ";
				found = true;
			}


			DataTable dt =  LookupTables.GetTable("CustomerCounter");
			
			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["CustomerCounterDesc"].ToString().Trim().ToUpper() == this.CustomerCounterDesc.Trim().ToUpper())
					{
						errorMessage = "This CustomerCounter Description is Already exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["CustomerCounterDesc"].ToString().Trim() == this.CustomerCounterDesc.Trim() && dt.Rows[i]["CustomerCounterID"].ToString().Trim() != this.CustomerCounterID.ToString().Trim())
					{
						errorMessage = "The CustomerCounter Description is Already exist.";
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

			EPolicy.LookupTables.CustomerCounter oldCustomerCounter = new EPolicy.LookupTables.CustomerCounter();

			oldCustomerCounter = oldCustomerCounter.GetCustomerCounterByCustomerCounterID(this.CustomerCounterID);//userID);
			
			if(_actionMode == 2)
			{
				history.BuildNotesForHistory("CustomerCounterDesc",oldCustomerCounter.CustomerCounterDesc.ToString(),this.CustomerCounterDesc.ToString(),mode);
				history.BuildNotesForHistory("SequenceStart",oldCustomerCounter.SequenceStart.ToString(),this.SequenceStart.ToString(),mode);
				history.BuildNotesForHistory("SequenceEnd",oldCustomerCounter.SequenceEnd.ToString(),this.SequenceEnd.ToString(),mode);
				history.BuildNotesForHistory("SequenceCurrent",oldCustomerCounter.SequenceCurrent.ToString(),this.SequenceCurrent.ToString(),mode);
				history.BuildNotesForHistory("SequenceWarning",oldCustomerCounter.SequenceWarning.ToString(),this.SequenceWarning.ToString(),mode);
				history.BuildNotesForHistory("SequencePrefix",oldCustomerCounter.SequencePrefix,this.SequencePrefix,mode);
				history.BuildNotesForHistory("SequenceSeparator",oldCustomerCounter.SequenceSeparator.ToString(),this.SequenceSeparator.ToString(),mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("CustomerCounterID.","",this.CustomerCounterID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.CustomerCounterID.ToString();
			history.Subject = "CUSTOMER COUNTER";			
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

