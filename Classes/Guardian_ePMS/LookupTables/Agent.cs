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
	/// Summary description for Agent.
	/// </summary>
	public class Agent
	{
		public Agent()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables
    
		//private Agent  oldAgent = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  string _agentID = String.Empty;
		private  string _agentDesc = String.Empty;
		private  string _agt_addr1 = String.Empty;
		private  string _agt_addr2 = String.Empty;
		private  string _agt_city = String.Empty;
		private  string _agt_st = String.Empty;
		private  string _agt_zip = String.Empty ;
		private  string _agt_phone = String.Empty ;
		private  string _agt_actdt = DateTime.Now.ToShortDateString();
        private string  _CarsID = String.Empty;
        private string _AgentType = String.Empty;

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

		public string AgentID
		{
			get
			{
				return this._agentID;
			}
			set
			{
				this._agentID = value;
			}
		}


		public string AgentDesc
		{
			get
			{
				return this._agentDesc;
			}
			set
			{
				this._agentDesc = value;
			}
		}

		public string agt_addr1
		{
			get
			{
				return this._agt_addr1;
			}
			set
			{
				this._agt_addr1 = value;
			}
		}
        
		public string agt_addr2
		{
			get
			{
				return this._agt_addr2;
			}
			set
			{
				this._agt_addr2 = value;
			}
		}
        
		public string agt_city
		{
			get
			{
				return this._agt_city;
			}
			set
			{
				this._agt_city = value;
			}
		}
        
		public string agt_st
		{
			get
			{
				return this._agt_st;
			}
			set
			{
				this._agt_st = value;
			}
		}

		public string agt_zip
		{
			get
			{
				return this._agt_zip;
			}
			set
			{
				this._agt_zip = value;
			}
		}


		public string agt_phone
		{
			get
			{
				return this._agt_phone;
			}
			set
			{
				this._agt_phone = value;
			}
		}

		public string agt_actdt
		{
			get
			{
				return this._agt_actdt;
			}
			set
			{
				this._agt_actdt = value;
			}
		}

        public string CarsID
        {
            get
            {
                return this._CarsID;
            }
            set
            {
                this._CarsID = value;
            }
        }

        public string AgentType
        {
            get
            {
                return this._AgentType;
            }
            set
            {
                this._AgentType = value;
            }
        }
		#endregion

		#region Public Methods
		      
		private string GetNextAgentID()
		{
            string AgentType = "";
            if (this.AgentType.ToString() == "")
            {
                AgentType = "Agent";
            }
            else if (this.AgentType.ToString() == "")
            {
                AgentType = "AgentVI";
            }
            else
            {
                AgentType = "AgentList";
            }

            DataTable dt = LookupTables.GetTable(AgentType);
			DataRow[] dr = dt.Select("","AgentID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["AgentID"] = dr[rec].ItemArray[0].ToString();
				myRow["AgentDesc"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["AgentID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}
		#endregion    
		
		#region Public Functions

		public void Delete(string AgentID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteAgentID", 
					this.GetDeleteAgentXml(AgentID));
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
						this.AgentID = GetNextAgentID();
						executor.Update("AddAgent", this.GetInsertAgentXml());
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
						executor.Update("UpdateAgent",this.GetUpdateAgentXml());
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

        //public void SaveVI(int UserID)
        //{
        //    this.ValidateVI();
        //    Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
        //    try
        //    {
        //        executor.BeginTrans();

        //        switch (this.ActionMode)
        //        {
        //            case 1: //ADD
        //                this.AgentID = GetNextAgentID();
        //                executor.Update("AddAgentVI", this.GetInsertAgentXml());
        //                this.HistoryVI(this._actionMode, UserID);
        //                //						this.AuditInsert(UserID);
        //                //						this.CommitAudit();
        //                break;
        //            case 3: //DELETE
        //                break;
        //            case 4: //CLEAR
        //                break;
        //            default: //UPDATE
        //                this.HistoryVI(this._actionMode, UserID);
        //                //						this.AuditUpdate(UserID);
        //                executor.Update("UpdateAgentVI", this.GetUpdateAgentXml());
        //                //						this.CommitAudit();//Commit audit;
        //                break;
        //        }
        //        executor.CommitTrans();
        //    }
        //    catch (Exception xcp)
        //    {
        //        executor.RollBackTrans();
        //        this.HandleSaveError(xcp);
        //    }
        //}

		public Agent GetAgent(string AgentID)
		{
			try
			{
				DataTable dtAgent = new DataTable();
				Agent agent = new Agent();
				this.AgentID = AgentID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtAgent = executor.GetQuery("GetagentByAgentID", 
					this.GetAgentByAgentIDXml());

				this.AgentDesc = 
					dtAgent.Rows[0]["AgentDesc"].ToString().Trim();

				this.agt_addr1 = 
					dtAgent.Rows[0]["agt_addr1"].ToString().Trim();

				this.AgentID = 
					dtAgent.Rows[0]["AgentID"].ToString().Trim();

				this.agt_addr2 = 
					dtAgent.Rows[0]["agt_addr2"].ToString().Trim();

				this.agt_city = 
					dtAgent.Rows[0]["agt_city"].ToString().Trim();

				this.agt_st = 
					dtAgent.Rows[0]["agt_st"].ToString().Trim();

				this.agt_zip = 
					dtAgent.Rows[0]["agt_zip"].ToString().Trim();

				this.agt_phone = 
					dtAgent.Rows[0]["agt_phone"].ToString().Trim();

				this.agt_actdt = 
					dtAgent.Rows[0]["agt_actdt"]!= System.DBNull.Value?((DateTime) dtAgent.Rows[0]["agt_actdt"]).ToShortDateString():"";

                this.CarsID =
                    dtAgent.Rows[0]["CarsID"].ToString().Trim();

                this.AgentType =
                    dtAgent.Rows[0]["AgentType"].ToString().Trim();

				return this;
			}
			catch(Exception ex) 
			{
				throw new Exception("Could not retrieve information for this Agent.", ex);
			}
		}

        //public Agent GetAgentVI(string AgentID)
        //{
        //    try
        //    {
        //        DataTable dtAgentVI = new DataTable();
        //        Agent agentVI = new Agent();
        //        this.AgentID = AgentID;

        //        Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();

        //        dtAgentVI = executor.GetQuery("GetAgentVIByAgentID",
        //            this.GetAgentByAgentIDXml());

        //        this.AgentDesc =
        //            dtAgentVI.Rows[0]["AgentDesc"].ToString().Trim();

        //        this.agt_addr1 =
        //            dtAgentVI.Rows[0]["agt_addr1"].ToString().Trim();

        //        this.AgentID =
        //            dtAgentVI.Rows[0]["AgentID"].ToString().Trim();

        //        this.agt_addr2 =
        //            dtAgentVI.Rows[0]["agt_addr2"].ToString().Trim();

        //        this.agt_city =
        //            dtAgentVI.Rows[0]["agt_city"].ToString().Trim();

        //        this.agt_st =
        //            dtAgentVI.Rows[0]["agt_st"].ToString().Trim();

        //        this.agt_zip =
        //            dtAgentVI.Rows[0]["agt_zip"].ToString().Trim();

        //        this.agt_phone =
        //            dtAgentVI.Rows[0]["agt_phone"].ToString().Trim();

        //        this.agt_actdt =
        //            dtAgentVI.Rows[0]["agt_actdt"] != System.DBNull.Value ? ((DateTime)dtAgentVI.Rows[0]["agt_actdt"]).ToShortDateString() : "";

        //        this.CarsID =
        //            dtAgentVI.Rows[0]["CarsID"].ToString().Trim();

        //        return this;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Could not retrieve information for this Agent.", ex);
        //    }
        //}

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
						throw new Exception("The Agent make you are attempting to " +
							"relate to this Agent does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Agent.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Agent.", Ex);
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
						throw new Exception("The Agent you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Agent before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Agent.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Agent.", Ex);
			}
		}
  
		private XmlDocument GetDeleteAgentXml(string AgentID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.VarChar, 3, this.AgentID.ToString(),
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
        
		private XmlDocument GetInsertAgentXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[11];

			
			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.VarChar, 3, this.AgentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgentDesc",
				SqlDbType.Char, 50, this.AgentDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agt_addr1",
				SqlDbType.VarChar, 20, this.agt_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agt_addr2",
				SqlDbType.VarChar, 20, this.agt_addr2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agt_city",
				SqlDbType.VarChar, 50, this.agt_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agt_st",
				SqlDbType.VarChar, 2, this.agt_st.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agt_zip",
				SqlDbType.VarChar, 10, this.agt_zip.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agt_phone",
				SqlDbType.VarChar, 14, this.agt_phone.ToString(),
				ref cookItems);

			
			DbRequestXmlCooker.AttachCookItem("agt_actdt",
				SqlDbType.VarChar, 4, this.agt_actdt.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CarsID",
            SqlDbType.VarChar, 10, this.CarsID.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AgentType",
            SqlDbType.Int, 0, this.AgentType.ToString(),
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

		private XmlDocument GetUpdateAgentXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[10];
			
			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.Char, 3, this.AgentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgentDesc",
				SqlDbType.Char, 50, this.AgentDesc.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agt_addr1",
				SqlDbType.Char, 20, this.agt_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agt_addr2",
				SqlDbType.VarChar, 20, this.agt_addr2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("agt_city",
				SqlDbType.VarChar, 50, this.agt_city.ToString(),
				ref cookItems);			

			DbRequestXmlCooker.AttachCookItem("agt_st",
				SqlDbType.VarChar, 2, this.agt_st.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agt_zip",
				SqlDbType.VarChar, 10, this.agt_zip.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("agt_phone",
				SqlDbType.VarChar, 14, this.agt_phone.ToString(),
				ref cookItems);

			
			DbRequestXmlCooker.AttachCookItem("agt_actdt",
				SqlDbType.VarChar, 4, this.agt_actdt.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CarsID",
           SqlDbType.VarChar, 10, this.CarsID.ToString(),
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

		private XmlDocument GetAgentByAgentIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.Char, 3, this.AgentID.ToString(),
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

			if(this.AgentDesc == "")
			{
				errorMessage += "The Agent cannot be empty.  ";
				found = true;
			}

			DataTable dt =  LookupTables.GetTable("Agent");
			
			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["AgentDesc"].ToString().Trim().ToUpper() == this.AgentDesc.Trim().ToUpper())
					{
						errorMessage = "This Agent Description is Already exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["AgentDesc"].ToString().Trim() == this.AgentDesc.Trim() && dt.Rows[i]["AgentID"].ToString().Trim() != this.AgentID.ToString().Trim())
					{
						errorMessage = "The Agent Description is Already exist.";
						found = true;
					}
				}
			}

			if (found == true)
			{
				throw new Exception(errorMessage);
			}
		}

        private void ValidateVI()
        {
            string errorMessage = String.Empty;
            bool found = false;

            if (this.AgentDesc == "")
            {
                errorMessage += "The Agent cannot be empty.  ";
                found = true;
            }

            DataTable dt = LookupTables.GetTable("AgentVI");

            if (this.ActionMode == 1)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["AgentDesc"].ToString().Trim().ToUpper() == this.AgentDesc.Trim().ToUpper())
                    {
                        errorMessage = "This Agent Description is Already exist.";
                        found = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["AgentDesc"].ToString().Trim() == this.AgentDesc.Trim() && dt.Rows[i]["AgentID"].ToString().Trim() != this.AgentID.ToString().Trim())
                    {
                        errorMessage = "The Agent Description is Already exist.";
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
				EPolicy.LookupTables.Agent oldAgent = new EPolicy.LookupTables.Agent();
				oldAgent = oldAgent.GetAgent(this.AgentID);//userID);

				history.BuildNotesForHistory("AgentDesc",oldAgent.AgentDesc,this.AgentDesc,mode);
				history.BuildNotesForHistory("agt_addr1",oldAgent.agt_addr1,this.agt_addr1,mode);
				history.BuildNotesForHistory("agt_addr2",oldAgent.agt_addr2,this.agt_addr2,mode);
				history.BuildNotesForHistory("agt_city",oldAgent.agt_city,this.agt_city,mode);
				history.BuildNotesForHistory("agt_st",oldAgent.agt_st,this.agt_st,mode);
				history.BuildNotesForHistory("agt_zip",oldAgent.agt_zip,this._agt_zip,mode);
				history.BuildNotesForHistory("agt_phone",oldAgent.agt_phone,this.agt_phone,mode);
				

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("AgentID.","",this.AgentID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.AgentID.ToString();
			history.Subject = "AGENT";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}

        //private void HistoryVI(int mode, int userID)
        //{
        //    Audit.History historyVI = new Audit.History();

        //    if (_actionMode == 2)
        //    {
        //        EPolicy.LookupTables.Agent oldAgent = new EPolicy.LookupTables.Agent();
        //        oldAgent = oldAgent.GetAgentVI(this.AgentID);//userID);

        //        historyVI.BuildNotesForHistory("AgentDesc", oldAgent.AgentDesc, this.AgentDesc, mode);
        //        historyVI.BuildNotesForHistory("agt_addr1", oldAgent.agt_addr1, this.agt_addr1, mode);
        //        historyVI.BuildNotesForHistory("agt_addr2", oldAgent.agt_addr2, this.agt_addr2, mode);
        //        historyVI.BuildNotesForHistory("agt_city", oldAgent.agt_city, this.agt_city, mode);
        //        historyVI.BuildNotesForHistory("agt_st", oldAgent.agt_st, this.agt_st, mode);
        //        historyVI.BuildNotesForHistory("agt_zip", oldAgent.agt_zip, this._agt_zip, mode);
        //        historyVI.BuildNotesForHistory("agt_phone", oldAgent.agt_phone, this.agt_phone, mode);


        //        historyVI.Actions = "EDIT";
        //    }
        //    else  //ADD & DELETE
        //    {
        //        historyVI.BuildNotesForHistory("AgentID.", "", this.AgentID.ToString(), mode);
        //        historyVI.Actions = "ADD";
        //    }

        //    historyVI.KeyID = this.AgentID.ToString();
        //    historyVI.Subject = "AGENT";
        //    historyVI.UsersID = userID;
        //    historyVI.GetSaveHistory();
        //}

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
