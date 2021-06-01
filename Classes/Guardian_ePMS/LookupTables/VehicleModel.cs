using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using System.Data.SqlClient;
using EPolicy.XmlCooker;
using EPolicy.Audit;



namespace EPolicy.LookupTables
{
	/// <summary>
	/// Summary description for ViewModel.
	/// </summary>
	public class VehicleModel
	{
		public VehicleModel()
		{
			//This is the constructor.
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables

		//private VehicleModel  oldVehicleModel = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private int _vehicleModelID = 0;
		private int _vehicleMakeID = 0;
		private string _vehicleModelDescription = String.Empty;
		
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

		public int VehicleModelID
		{
			get
			{
				return this._vehicleModelID;
			}
			set
			{
				this._vehicleModelID = value;
			}
		}

		public int VehicleMakeID
		{
			get
			{
				return this._vehicleMakeID;
			}
			set
			{
				this._vehicleMakeID = value;
			}
		}

		public string VehicleModelDescription
		{
			get
			{
				return this._vehicleModelDescription;
			}
			set
			{
				this._vehicleModelDescription = value;
			}
		}

		#endregion
	
		#region Public Functions

		public void Delete(int VehicleModelID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteVehicleModel", 
					this.GetDeleteVehicleModelXml(VehicleModelID));
			}
			catch(Exception ex)
			{
                this.HandleDeleteError(ex);
			}
		}

		public void Save(int UserID)
		{
			int vehicleModelID = 0;
			this.Validate();
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();

			try
			{
				executor.BeginTrans();
				
				switch (this.ActionMode)
				{
					case 1: //ADD
						vehicleModelID = executor.Insert("AddVehicleModel", this.GetInsertVehicleModelXml());
						this.VehicleModelID = vehicleModelID;
						this.History(this._actionMode,UserID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
						executor.Update("UpdateVehicleModel",this.GetUpdateVehicleModelXml());
						vehicleModelID = this.VehicleModelID;
						break;
				}
				executor.CommitTrans();
				this.VehicleModelID = vehicleModelID;
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleSaveError(xcp);
			}
		}

        public void SaveVSC(int UserID)
        {
            int vehicleModelID = 0;
            this.Validate();
            Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();

            try
            {
                executor.BeginTrans();

                switch (this.ActionMode)
                {
                    case 1: //ADD
                        vehicleModelID = executor.Insert("AddVSCVehicleModel", this.GetInsertVehicleModelXml());
                        this.VehicleModelID = vehicleModelID;
                        this.History(this._actionMode, UserID);
                        break;
                    case 3: //DELETE
                        break;
                    case 4: //CLEAR
                        break;
                    default: //UPDATE
                        this.HistoryVSC(this._actionMode, UserID);
                        executor.Update("UpdateVSCVehicleModel", this.GetUpdateVehicleModelXml());
                        vehicleModelID = this.VehicleModelID;
                        break;
                }
                executor.CommitTrans();
                this.VehicleModelID = vehicleModelID;
            }
            catch (Exception xcp)
            {
                executor.RollBackTrans();
                this.HandleSaveError(xcp);
            }
        }

		public VehicleModel GetVehicleModel(int VehicleModelID)
		{
			try
			{
				DataTable dtVehicleModel = new DataTable();
				VehicleModel vehicleModel = new VehicleModel();
				this.VehicleModelID = VehicleModelID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtVehicleModel = executor.GetQuery("GetVehicleModelByVehicleModelId", 
					this.GetVehicleModelByVehicleModelIdXML());

				this.VehicleMakeID = 
					int.Parse(dtVehicleModel.Rows[0]["VehicleMakeID"].ToString().Trim());

				this.VehicleModelDescription = 
					dtVehicleModel.Rows[0]["VehicleModelDesc"].ToString().Trim();

				this.VehicleModelID = 
					int.Parse(dtVehicleModel.Rows[0]["VehicleModelID"].ToString().Trim());
			
				return this;
			
			}

			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this vehicle model.", ex);
			}
		}

        public VehicleModel GetVSCVehicleModel(int VehicleModelID)
        {
            try
            {
                DataTable dtVehicleModel = new DataTable();
                VehicleModel vehicleModel = new VehicleModel();
                this.VehicleModelID = VehicleModelID;

                Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();

                dtVehicleModel = executor.GetQuery("GetVSCVehicleModelByVehicleModelId",
                    this.GetVehicleModelByVehicleModelIdXML());

                this.VehicleMakeID =
                    int.Parse(dtVehicleModel.Rows[0]["VehicleMakeID"].ToString().Trim());

                this.VehicleModelDescription =
                    dtVehicleModel.Rows[0]["VehicleModelDesc"].ToString().Trim();

                this.VehicleModelID =
                    int.Parse(dtVehicleModel.Rows[0]["VehicleModelID"].ToString().Trim());

                return this;

            }

            catch (Exception ex)
            {
                throw new Exception(
                    "Could not retrieve information for this vehicle model.", ex);
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
						throw new Exception("The vehicle make you are attempting to " +
							"relate to this vehicle model does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the vehicle model.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the vehicle model.", Ex);
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
						throw new Exception("The vehicle model you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this vehicle model before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the vehicle model.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the vehicle model.", Ex);
			}
		}

		private XmlDocument GetDeleteVehicleModelXml(int VehicleModelID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("VehicleModelID",
				SqlDbType.Int, 0, VehicleModelID.ToString(),
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
        
		private XmlDocument GetInsertVehicleModelXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			
			DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
				SqlDbType.Int, 0, this.VehicleMakeID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleModelDesc",
				SqlDbType.VarChar, 50, this.VehicleModelDescription.ToString(),
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

		private XmlDocument GetUpdateVehicleModelXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[3];


			DbRequestXmlCooker.AttachCookItem("VehicleModelID",
				SqlDbType.Int, 0, this.VehicleModelID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
				SqlDbType.Int, 0, this.VehicleMakeID.ToString(),
				ref cookItems);


			DbRequestXmlCooker.AttachCookItem("VehicleModelDesc",
				SqlDbType.VarChar, 50, this.VehicleModelDescription.ToString(),
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

		private XmlDocument GetVehicleModelByVehicleModelIdXML()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("VehicleModelID",
				SqlDbType.Int, 0, this.VehicleModelID.ToString(),
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

			if(this.VehicleMakeID == 0)
			{
				errorMessage += "Vehicle make must be a number greater than zero (0).  ";
				found = true;
			}

			if(this.VehicleModelDescription == String.Empty)
			{
				errorMessage += "Vehicle model description cannot be empty.  ";
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
				EPolicy.LookupTables.VehicleModel oldVehicleModel = new EPolicy.LookupTables.VehicleModel();

			    oldVehicleModel = oldVehicleModel.GetVehicleModel(this.VehicleModelID);//userID);

				history.BuildNotesForHistory("VehicleMakeID",
					LookupTables.GetDescription("VehicleMake",oldVehicleModel.VehicleMakeID.ToString()),
					LookupTables.GetDescription("VehicleMake",this.VehicleMakeID.ToString()),
					mode);
				history.BuildNotesForHistory("VehicleModelDescription",oldVehicleModel.VehicleModelDescription.ToString(),this.VehicleModelDescription.ToString(),mode);
				

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("VehicleModelID.","",this.VehicleModelID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.VehicleModelID.ToString();
			history.Subject = "VEHICLE MODEL";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}

        private void HistoryVSC(int mode, int userID)
        {
            Audit.History history = new Audit.History();

            if (_actionMode == 2)
            {
                EPolicy.LookupTables.VehicleModel oldVehicleModel = new EPolicy.LookupTables.VehicleModel();

                oldVehicleModel = oldVehicleModel.GetVSCVehicleModel(this.VehicleModelID);//userID);

                history.BuildNotesForHistory("VehicleMakeID",
                    LookupTables.GetDescriptionToVSCVehicleMake("VSCVehicleMake", oldVehicleModel.VehicleMakeID.ToString()),
                    LookupTables.GetDescriptionToVSCVehicleMake("VSCVehicleMake", this.VehicleMakeID.ToString()),
                    mode);
                history.BuildNotesForHistory("VehicleModelDescription", oldVehicleModel.VehicleModelDescription.ToString(), this.VehicleModelDescription.ToString(), mode);


                history.Actions = "EDIT";
            }
            else  //ADD & DELETE
            {
                history.BuildNotesForHistory("VehicleModelID.", "", this.VehicleModelID.ToString(), mode);
                history.Actions = "ADD";
            }

            history.KeyID = this.VehicleModelID.ToString();
            history.Subject = "VEHICLE MODEL";
            history.UsersID = userID;
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
