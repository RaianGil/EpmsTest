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
	/// Summary description for MasterPolicy.
	/// </summary>
	public class MasterPolicy
	{
		public MasterPolicy()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables
    
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  string _masterPolicyID = String.Empty;
		private  string _masterPolicyDesc = String.Empty;
		private  int _pol_seq = 0;
		private  string _pol_number = String.Empty;
		private  bool _pol_cert = false;
		private  string _pol_type = String.Empty;
		private  string _pol_agency = String.Empty;
		private  string _pol_agent = String.Empty ;
		private  string _pol_insco = String.Empty ;
		private  string _pol_acct = DateTime.Now.ToShortDateString();
		private  string _pol_bank = String.Empty;
		private  int _pol_end = 0;

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

		public string MasterPolicyID
		{
			get
			{
				return this._masterPolicyID;
			}
			set
			{
				this._masterPolicyID = value;
			}
		}


		public string MasterPolicyDesc
		{
			get
			{
				return this._masterPolicyDesc;
			}
			set
			{
				this._masterPolicyDesc = value;
			}
		}


		public int pol_seq
		{
			get
			{
				return this._pol_seq;
			}
			set
			{
				this._pol_seq = value;
			}
		}
		public string pol_number
		{
			get
			{
				return this._pol_number;
			}
			set
			{
				this._pol_number = value;
			}
		}
        
		public bool pol_cert
		{
			get
			{
				return this._pol_cert;
			}
			set
			{
				this._pol_cert = value;
			}
		}
        
		public string pol_type
		{
			get
			{
				return this._pol_type;
			}
			set
			{
				this._pol_type = value;
			}
		}
        
		public string pol_agency
		{
			get
			{
				return this._pol_agency;
			}
			set
			{
				this._pol_agency = value;
			}
		}

		public string pol_agent
		{
			get
			{
				return this._pol_agent;
			}
			set
			{
				this._pol_agent = value;
			}
		}


		public string pol_insco
		{
			get
			{
				return this._pol_insco;
			}
			set
			{
				this._pol_insco = value;
			}
		}

		public string pol_acct
		{
			get
			{
				return this._pol_acct;
			}
			set
			{
				this._pol_acct = value;
			}
		}

		public string pol_bank
		{
			get
			{
				return this._pol_bank;
			}
			set
			{
				this._pol_bank = value;
			}
		}

		public int pol_end
		{
			get
			{
				return this._pol_end;
			}
			set
			{
				this._pol_end = value;
			}
		}
		#endregion

		#region Public Methods
		      
		private string GetNextMasterPolicyID()
		{
			DataTable dt = LookupTables.GetTable("MasterPolicy");
			DataRow[] dr = dt.Select("","MasterPolicyID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["MasterPolicyID"] = dr[rec].ItemArray[0].ToString();
				myRow["MasterPolicyDesc"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["MasterPolicyID"].ToString())+1;
						
			return (ID.ToString().PadLeft(4,'0')); 
		}
		#endregion    

		#region Public Functions

		public void Delete(string MasterPolicyID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteMasterPolicyID", 
					this.GetDeleteMasterPolicyXml(MasterPolicyID));
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
						this.MasterPolicyID = GetNextMasterPolicyID();
						executor.Update("AddMasterPolicy",this.GetInsertMasterPolicyXml());
						this.History(this._actionMode,UserID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
						executor.Update("UpdateMasterPolicy",this.GetUpdateMasterPolicyXml());
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

		public MasterPolicy GetMasterPolicy(string MasterPolicyID)
		{
			try
			{
				DataTable dtMasterPolicy = new DataTable();
				MasterPolicy masterPolicy = new MasterPolicy();
				this.MasterPolicyID = MasterPolicyID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtMasterPolicy = executor.GetQuery("GetmasterPolicyByMasterPolicyID", 
					this.GetMasterPolicyByMasterPolicyIDXml());

				this.MasterPolicyDesc = 
					dtMasterPolicy.Rows[0]["MasterPolicyDesc"].ToString().Trim();

//				this.MasterPolicyID = 
//					dtMasterPolicy.Rows[0]["MasterPolicyID"].ToString().Trim();

				this.pol_seq = 
					int.Parse(dtMasterPolicy.Rows[0]["pol_seq"].ToString().Trim());

				this.pol_number = 
				    dtMasterPolicy.Rows[0]["pol_number"].ToString().Trim();

				this.pol_cert = 
					bool.Parse(dtMasterPolicy.Rows[0]["pol_cert"].ToString().Trim());

				this.pol_type = 
					dtMasterPolicy.Rows[0]["pol_type"].ToString().Trim();

				this.pol_agency = 
					dtMasterPolicy.Rows[0]["pol_agency"].ToString().Trim();

				this.pol_agent = 
					dtMasterPolicy.Rows[0]["pol_agent"].ToString().Trim();

				this.pol_insco = 
					dtMasterPolicy.Rows[0]["pol_insco"].ToString().Trim();

				this.pol_acct = 
					dtMasterPolicy.Rows[0]["pol_acct"].ToString().Trim();

				this.pol_bank = 
					dtMasterPolicy.Rows[0]["pol_bank"].ToString().Trim();

				this.pol_end = 
					int.Parse(dtMasterPolicy.Rows[0]["pol_end"].ToString().Trim());

             return this;

			}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Master Policy.", ex);
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
						throw new Exception("The MasterPolicy make you are attempting to " +
							"relate to this Master Policy does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the MasterPolicy.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Master Policy.", Ex);
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
						throw new Exception("The Master Policy you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Master Policy before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Master Policy.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Master Policy.", Ex);
			}
		}
  
		private XmlDocument GetDeleteMasterPolicyXml(string MasterPolicyID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
				SqlDbType.VarChar, 4, MasterPolicyID.ToString(),
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
        
	private XmlDocument GetInsertMasterPolicyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[12];

			DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
				SqlDbType.VarChar, 4, this.MasterPolicyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("MasterPolicyDesc",
				SqlDbType.VarChar, 30, this.MasterPolicyDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_seq",
				SqlDbType.Int, 0, this.pol_seq.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_number",
				SqlDbType.VarChar, 11, this.pol_number.ToString(),
				ref cookItems);


			DbRequestXmlCooker.AttachCookItem("pol_cert",
				SqlDbType.Bit, 0, this.pol_cert.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_type",
				SqlDbType.VarChar, 3, this.pol_type.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_agency",
				SqlDbType.VarChar, 3, this.pol_agency.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_agent",
				SqlDbType.VarChar, 3, this.pol_agent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_insco",
				SqlDbType.VarChar, 2, this.pol_insco.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_acct",
				SqlDbType.VarChar, 10, this.pol_acct.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_bank",
				SqlDbType.Int, 0, this.pol_bank.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_end",
				SqlDbType.Int, 0, this.pol_end.ToString(),
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

		private XmlDocument GetUpdateMasterPolicyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[12];

			DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
				SqlDbType.VarChar, 4, this.MasterPolicyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("MasterPolicyDesc",
				SqlDbType.VarChar, 30, this.MasterPolicyDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_seq",
				SqlDbType.Int, 0, this.pol_seq.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_number",
				SqlDbType.VarChar, 11, this.pol_number.ToString(),
				ref cookItems);


			DbRequestXmlCooker.AttachCookItem("pol_cert",
				SqlDbType.Bit, 0, this.pol_cert.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_type",
				SqlDbType.VarChar, 3, this.pol_type.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_agency",
				SqlDbType.VarChar, 3, this.pol_agency.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_agent",
				SqlDbType.VarChar, 3, this.pol_agent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_insco",
				SqlDbType.VarChar, 2, this.pol_insco.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_acct",
				SqlDbType.VarChar, 10, this.pol_acct.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("pol_bank",
				SqlDbType.Int, 0, this.pol_bank.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("pol_end",
				SqlDbType.Int, 0, this.pol_end.ToString(),
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

		private XmlDocument GetMasterPolicyByMasterPolicyIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];


			DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
				SqlDbType.Int, 0, this.MasterPolicyID.ToString(),
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

			if(this.MasterPolicyDesc == "")
			{
				errorMessage += "The Master Policy cannot be empty.  ";
				found = true;
			}

			DataTable dt =  LookupTables.GetTable("MasterPolicy");
			
			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["MasterPolicyDesc"].ToString().Trim() == this.MasterPolicyDesc.Trim())
					{
						errorMessage = "The MasterPolicy Description is Already Exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["MasterPolicyDesc"].ToString().Trim() == this.MasterPolicyDesc.Trim() && dt.Rows[i]["MasterPolicyID"].ToString().Trim() != this.MasterPolicyID.ToString().Trim())
					{
						errorMessage = "The MasterPolicy Description is Already Exist.";
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

			EPolicy.LookupTables.MasterPolicy oldMasterPolicy = new EPolicy.LookupTables.MasterPolicy();

			oldMasterPolicy = oldMasterPolicy.GetMasterPolicy(this.MasterPolicyID);//userID);
			
			if(_actionMode == 2)
			{
				history.BuildNotesForHistory("MasterPolicyDesc",oldMasterPolicy.MasterPolicyDesc.ToString(),this.MasterPolicyDesc.ToString(),mode);
				history.BuildNotesForHistory("pol_seq",oldMasterPolicy.pol_seq.ToString(),this.pol_seq.ToString(),mode);
				history.BuildNotesForHistory("pol_number",oldMasterPolicy.pol_number,this.pol_number,mode);
				history.BuildNotesForHistory("pol_cert",oldMasterPolicy.pol_cert.ToString(),this.pol_cert.ToString(),mode);
				history.BuildNotesForHistory("pol_type",oldMasterPolicy.pol_type,this.pol_type,mode);
				history.BuildNotesForHistory("pol_agency",oldMasterPolicy.pol_agency.ToString(),this.pol_agency.ToString(),mode);
				history.BuildNotesForHistory("pol_agent",oldMasterPolicy.pol_agent.ToString(),this.pol_agent.ToString(),mode);
				history.BuildNotesForHistory("pol_insco",oldMasterPolicy.pol_insco.ToString(),this.pol_insco.ToString(),mode);
				history.BuildNotesForHistory("pol_acct",oldMasterPolicy.pol_acct.ToString(),this.pol_acct.ToString(),mode);
				history.BuildNotesForHistory("pol_bank",oldMasterPolicy.pol_bank.ToString(),this.pol_bank.ToString(),mode);
				history.BuildNotesForHistory("pol_end",oldMasterPolicy.pol_end.ToString(),this.pol_end.ToString(),mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("MasterPolicyID.","",this.MasterPolicyID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.MasterPolicyID.ToString();
			history.Subject = "MASTER POLICY";			
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
