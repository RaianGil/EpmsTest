using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using System.Data.SqlClient;
using EPolicy.XmlCooker;


namespace EPolicy.LookupTables
{
	/// <summary>
	/// Summary description for LookupTableMetadata.
	/// </summary>
	public class LookupTableMetadata
	{
		public LookupTableMetadata()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables

		private int _actionMode = (int)Mode.UPDATE;
		private int _lookupTableID = 0;
		private string _tableName = String.Empty;
		private bool _isValuePair = false;
		private string _idFieldName = String.Empty;
		private string _descriptionFieldName = String.Empty;
		private string _defaultSearchFieldA = String.Empty;
		private string _defaultSearchFieldB = String.Empty;
		private string _maintenancePagePath = String.Empty;
		
		#endregion

		#region Properties

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

		public int LookupTableID
		{
			get
			{
				return this._lookupTableID;
			}
			set
			{
				this._lookupTableID = value;
			}
		}

		public string TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		public bool IsValuePair
		{
			get
			{
				return this._isValuePair;
			}
			set
			{
				this._isValuePair = value;
			}
		}

		public string IdFieldName
		{
			get
			{
				return this._idFieldName;
			}
			set
			{
				this._idFieldName = value;
			}
		}

		public string DescriptionFieldName
		{
			get
			{
				return this._descriptionFieldName;
			}
			set
			{
				this._descriptionFieldName = value;
			}
		}

		

		public string DefaultSearchFieldA
		{
			get
			{
				return this._defaultSearchFieldA;
			}
			set
			{
				this._defaultSearchFieldA = value;
			}
		}

		public string DefaultSearchFieldB
		{
			get
			{
				return this._defaultSearchFieldB;
			}
			set
			{
				this._defaultSearchFieldB = value;
			}
		}		

		public string MaintenancePagePath
		{
			get
			{
				return this._maintenancePagePath;
			}
			set
			{
				this._maintenancePagePath = value;
			}
		}

		#endregion

		
		#region Public Functions

		public void Delete(int LookupTableID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteLookupTableMetadata", 
					this.GetDeleteLookupTableMetadataXml(LookupTableID));
			}
			catch(Exception ex)
			{
				this.HandleDeleteError(ex);
			}
		}

		public void Save()
		{
			int lookupTableID = 0;
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();

			try
			{
				executor.BeginTrans();
				
				switch (this.ActionMode)
				{
					case 1: //ADD
						this.Validate(this.ActionMode);
						lookupTableID = executor.Insert("AddLookupTableMetadata", 
							this.GetAddLookupTableMetadataXml());
						this.LookupTableID = lookupTableID;
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.Validate(this.ActionMode);
						executor.Update("UpdateLookupTableMetadata",
							this.GetUpdateLookupTableMetadataXml());
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

		public static int AddTableNameToMetadataStore(string TableName)
		{
            Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			return executor.Insert("AddTableNameToMetadataStore", 
				GetAddTableNameToMetadataStoreXml(TableName));
		}

		public void Get(int LookupTableID)
		{
			try
			{
				DataTable dtLookupTableMetadata = new DataTable();

				LookupTableMetadata lookupTableMetadata = new LookupTableMetadata();
				this.LookupTableID = LookupTableID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtLookupTableMetadata = executor.GetQuery(
					"GetLookupTableMetadataByLookupTableID", 
					this.GetLookupTableMetadataByLookupTableIDXml());

                this.TableName = 
					dtLookupTableMetadata.Rows[0]["TableName"].ToString().Trim();

				this.IsValuePair = 
					bool.Parse(dtLookupTableMetadata.Rows[0]["IsValuePair"].ToString().Trim());

				this.IdFieldName =
					dtLookupTableMetadata.Rows[0]["IdFieldName"].ToString().Trim();

				this.DescriptionFieldName =
					dtLookupTableMetadata.Rows[0]["DescriptionFieldName"].ToString().Trim();

				this.DefaultSearchFieldA =
					dtLookupTableMetadata.Rows[0]["DefaultSearchFieldA"].ToString().Trim();
				
				this.DefaultSearchFieldB =
					dtLookupTableMetadata.Rows[0]["DefaultSearchFieldB"].ToString().Trim();

				this.MaintenancePagePath = 
					dtLookupTableMetadata.Rows[0]["MaintenancePagePath"].ToString().Trim();
			}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this lookup table.", ex);
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
						throw new Exception("The lookup table you are trying to " +
							"relate to this search fields does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the lookup table metadata.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the lookup table metadata.", Ex);
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
						throw new Exception("The lookup table metadata record you are " + 
							"attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this lookup table metadata record before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the lookup table metadata record.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the lookup table metadata record.", Ex);
			}
		}

		private XmlDocument GetDeleteLookupTableMetadataXml(int LookupTableID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
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
        
		private XmlDocument GetAddLookupTableMetadataXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			
			DbRequestXmlCooker.AttachCookItem("IsValuePair",
				SqlDbType.Bit, 0, this.IsValuePair.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IdFieldName",
				SqlDbType.VarChar, 50, this.IdFieldName.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("DescriptionFieldName",
				SqlDbType.VarChar, 50, this.DescriptionFieldName.ToString(),
				ref cookItems);
            
			DbRequestXmlCooker.AttachCookItem("DefaultSearchFieldA",
				SqlDbType.VarChar, 50, this.DefaultSearchFieldA.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("DefaultSearchFieldB",
				SqlDbType.VarChar, 50, this.DefaultSearchFieldB.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("MaintenancePagePath",
				SqlDbType.VarChar, 50, this.MaintenancePagePath.ToString(),
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

		private static XmlDocument GetAddTableNameToMetadataStoreXml(string TableName)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TableName",
				SqlDbType.VarChar, 50, TableName.ToString(),
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


		private XmlDocument GetUpdateLookupTableMetadataXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[7];
			
			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, this.LookupTableID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IsValuePair",
				SqlDbType.Bit, 0, this.IsValuePair.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("IdFieldName",
				SqlDbType.VarChar, 50, this.IdFieldName.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("DescriptionFieldName",
				SqlDbType.VarChar, 50, this.DescriptionFieldName.ToString(),
				ref cookItems);
            
			DbRequestXmlCooker.AttachCookItem("DefaultSearchFieldA",
				SqlDbType.VarChar, 50, this.DefaultSearchFieldA.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("DefaultSearchFieldB",
				SqlDbType.VarChar, 50, this.DefaultSearchFieldB.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("MaintenancePagePath",
				SqlDbType.VarChar, 50, this.MaintenancePagePath.ToString(),
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

		private XmlDocument GetLookupTableMetadataByLookupTableIDXml()
		{
				DbRequestXmlCookRequestItem[] cookItems = 
			 new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, this.LookupTableID.ToString(),
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

		private void Validate(int ActionMode)
		{
			string errorMessage = String.Empty;
			bool found = false;

			if(ActionMode == (int)Mode.UPDATE)
			{
				if(this.LookupTableID == 0)
				{
					errorMessage += "The lookup table ID must be greater than zero (0).  ";
					found = true;
				}
			}

			if(this.IdFieldName == String.Empty && this.IsValuePair == true)
			{
				errorMessage += "An ID field name must be provided.  ";
				found = true;
			}

			if(this.DescriptionFieldName == String.Empty && this.IsValuePair == true)
			{
				errorMessage += "A description field name must be provided.  ";
				found = true;
			}

			if(this.DefaultSearchFieldA == String.Empty && this.IsValuePair == false)
			{
				errorMessage += "A default search field A must be provided.  ";
				found = true;
			}

			if(this.DefaultSearchFieldB == String.Empty && this.IsValuePair == false)
			{
				errorMessage += "A default search field B must be provided.  ";
				found = true;
			}

			if (found == true)
			{
				throw new Exception(errorMessage);
			}
		}

		#endregion
	}
}