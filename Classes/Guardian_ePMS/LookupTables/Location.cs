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
	/// Summary description for Location.
	/// </summary>
	public class Location
	{
		public Location()
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
		private  int _locationID = 0;
		private  string _locationDesc = String.Empty;
		private  int _customerCounterID = 0;
		
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

		public int LocationID
		{
			get
			{
				return this._locationID;
			}
			set
			{
				this._locationID = value;
			}
		}


		public string LocationDesc
		{
			get
			{
				return this._locationDesc;
			}
			set
			{
				this._locationDesc = value;
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
		
		#endregion
	
		#region Public Methods
		      
			private int GetNextLocationID()
					{
						DataTable dt = LookupTables.GetTable("Location");
						DataRow[] dr = dt.Select("","LocationID");

						DataTable dt2 = dt.Clone();

						for (int rec = 0; rec<=dr.Length-1; rec++)
						{
							DataRow myRow = dt2.NewRow();
							myRow["LocationID"] = dr[rec].ItemArray[0].ToString();
							myRow["LocationDesc"] = dr[rec].ItemArray[1].ToString();

							dt2.Rows.Add(myRow);
							dt2.AcceptChanges();
						}

						int ID = 0;

						ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["LocationID"].ToString())+1;
						
						return (int.Parse(ID.ToString())); 
					}
		#endregion    

		#region Public Functions

		public void Delete(int LocationID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteLocationID", 
					this.GetDeleteLocationXml(LocationID));
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
						this.LocationID = GetNextLocationID();
						executor.Insert("AddLocation",this.GetAddLocationXml());
						this.History(this._actionMode,UserID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
						executor.Update("UpdateLocation",this.GetUpdateLocationXml());
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

		public Location GetLocation(int LocationID)
		{
			try
			{
				DataTable dtLocation = new DataTable();
				Location location = new Location();
				this.LocationID = LocationID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtLocation = executor.GetQuery("GetlocationByLocationID", 
					this.GetLocationByLocationIDXml());

				this.LocationDesc = 
					dtLocation.Rows[0]["LocationDesc"].ToString().Trim();

				this.CustomerCounterID = 
					int.Parse(dtLocation.Rows[0]["CustomerCounterID"].ToString().Trim());
			
		   return this;

			}

				catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Location.", ex);
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
						throw new Exception("The Location make you are attempting to " +
							"relate to this Location does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Location.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Location.", Ex);
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
						throw new Exception("The Location you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Location before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Location.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Location.", Ex);
			}
		}
  
		private XmlDocument GetDeleteLocationXml(int LocationID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];


			DbRequestXmlCooker.AttachCookItem("LocationID",
				SqlDbType.Int, 0, LocationID.ToString(),
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
        
		private XmlDocument GetAddLocationXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[3];

			DbRequestXmlCooker.AttachCookItem("LocationID",
				SqlDbType.Int, 0, this.LocationID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LocationDesc",
				SqlDbType.VarChar, 50, this.LocationDesc.ToString(),
				ref cookItems);

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

		private XmlDocument GetUpdateLocationXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[3];

			DbRequestXmlCooker.AttachCookItem("LocationID",
				SqlDbType.Int, 0, this.LocationID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LocationDesc",
				SqlDbType.VarChar, 50, this.LocationDesc.ToString(),
				ref cookItems);

			
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

		private XmlDocument GetLocationByLocationIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("LocationID",
				SqlDbType.Int, 0, this.LocationID.ToString(),
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

			if(this.LocationDesc == "")
			{
				errorMessage += "The Location Description cannot be empty.  ";
				found = true;
			}

			DataTable dt =  LookupTables.GetTable("Location");

			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["LocationDesc"].ToString().Trim() == this.LocationDesc.Trim())
					{
						errorMessage = "The Location Description is Already Exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["LocationDesc"].ToString().Trim() == this.LocationDesc.Trim() && dt.Rows[i]["LocationID"].ToString().Trim() != this.LocationID.ToString().Trim())
					{
						errorMessage = "The Location Description is Already Exist.";
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
				EPolicy.LookupTables.Location oldLocation = new EPolicy.LookupTables.Location();
				oldLocation = oldLocation.GetLocation(this.LocationID);//userID);

				history.BuildNotesForHistory("LocationDesc",oldLocation.LocationDesc.ToString(),this.LocationDesc.ToString(),mode);
				history.BuildNotesForHistory("CustomerCounterID",
					LookupTables.GetDescription("CustomerCounter",oldLocation.CustomerCounterID.ToString()),
					LookupTables.GetDescription("CustomerCounter",this.CustomerCounterID.ToString()),
					mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("LocationID.","",this.LocationID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.LocationID.ToString();
			history.Subject = "LOCATION";			
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
