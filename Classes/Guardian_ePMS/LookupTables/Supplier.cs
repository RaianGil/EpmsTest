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
	/// Summary description for Supplier.
	/// </summary>
	public class Supplier
	{
		public Supplier()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables
    
		//private Supplier  oldSupplier = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  string _supplierID = String.Empty;
		private  string _supplierDesc = String.Empty;
		private  string _supplier_addr1 = String.Empty;
		private  string _supplier_addr2 = String.Empty;
		private  string _supplier_city = String.Empty;
		private  string _supplier_st = String.Empty;
		private  string _supplier_zip = String.Empty ;
		private  string _supplier_phone = String.Empty ;
		private  string _supplier_actdt = DateTime.Now.ToShortDateString();

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


		public string SupplierDesc
		{
			get
			{
				return this._supplierDesc;
			}
			set
			{
				this._supplierDesc = value;
			}
		}

		public string Supplier_addr1
		{
			get
			{
				return this._supplier_addr1;
			}
			set
			{
				this._supplier_addr1 = value;
			}
		}
        
		public string Supplier_addr2
		{
			get
			{
				return this._supplier_addr2;
			}
			set
			{
				this._supplier_addr2 = value;
			}
		}
        
		public string Supplier_city
		{
			get
			{
				return this._supplier_city;
			}
			set
			{
				this._supplier_city = value;
			}
		}
        
		public string Supplier_st
		{
			get
			{
				return this._supplier_st;
			}
			set
			{
				this._supplier_st = value;
			}
		}

		public string Supplier_zip
		{
			get
			{
				return this._supplier_zip;
			}
			set
			{
				this._supplier_zip = value;
			}
		}


		public string Supplier_phone
		{
			get
			{
				return this._supplier_phone;
			}
			set
			{
				this._supplier_phone = value;
			}
		}

		public string Supplier_actdt
		{
			get
			{
				return this._supplier_actdt;
			}
			set
			{
				this._supplier_actdt = value;
			}
		}
		#endregion

		#region Public Methods

        public void SaveSupplier(string MasterSupplierID, string supplierID)
        {
            SaveSupplierinDB(MasterSupplierID, supplierID);
        }

        public static DataTable GetGroupSupplierBySupplierID(string MasterSupplierID)
        {
            DataTable dt = GetGroupSupplierBySupplierIDDB(MasterSupplierID);

            return dt;
        }

        public void DeleteGroupSupplierByGroupSupplierID(int GroupSupplierID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();

                Executor.Delete("DeleteGroupSupplier", this.DeleteGroupSupplierByGroupSupplierIDXml(GroupSupplierID));
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to delete this dealer for this User. " + xcp.Message, xcp);
            }

        }

		private string GetNextSupplierID()
		{
			DataTable dt = LookupTables.GetTable("Supplier");
			DataRow[] dr = dt.Select("","SupplierID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["SupplierID"] = dr[rec].ItemArray[0].ToString();
				myRow["SupplierDesc"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["SupplierID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}
		#endregion    

		#region Public Functions

        private static DataTable GetGroupSupplierBySupplierIDDB(string MasterSupplierID)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            XmlDocument xmlDoc = new XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>MasterSupplierID</name>");
            sb.Append("<type>char</type>");
            sb.Append("<lenght>3</lenght>");
            sb.Append("<value>" + MasterSupplierID + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetGroupSupplierBySupplierID", xmlDoc);
            return dt;
        }

        private void SaveSupplierinDB(string MasterSupplierID, string supplierID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("MasterSupplierID",
                SqlDbType.Char, 3, MasterSupplierID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("SupplierID",
                SqlDbType.Char, 3, supplierID.ToString(),
                ref cookItems);

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            int a = exec.Insert("AddGroupSupplier", xmlDoc);
        }

        private XmlDocument DeleteGroupSupplierByGroupSupplierIDXml(int GroupSupplierID)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            XmlDocument xmlDoc = new XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>GroupSupplierID</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + GroupSupplierID + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            return xmlDoc;
        }

		public void Delete(string SupplierID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteSupplierID", 
					this.GetDeleteSupplierXml(SupplierID));
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
						this.SupplierID = GetNextSupplierID();
						executor.Update("AddSupplier",this.GetInsertSupplierXml());
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
						executor.Update("UpdateSupplier",this.GetUpdateSupplierXml());
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

		public Supplier GetSupplier(string SupplierID)
		{
			try
			{
				DataTable dtSupplier = new DataTable();
				Supplier supplier = new Supplier();
				this.SupplierID = SupplierID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtSupplier = executor.GetQuery("GetSupplierBySupplierID", 
					this.GetSupplierBySupplierIDXml());

				this.SupplierDesc = 
					dtSupplier.Rows[0]["SupplierDesc"].ToString().Trim();

				this.Supplier_addr1 = 
					dtSupplier.Rows[0]["Supplier_addr1"].ToString().Trim();

				this.SupplierID = 
					dtSupplier.Rows[0]["SupplierID"].ToString().Trim();

				this.Supplier_addr2 = 
					dtSupplier.Rows[0]["Supplier_addr2"].ToString().Trim();

				this.Supplier_city = 
					dtSupplier.Rows[0]["Supplier_city"].ToString().Trim();

				this.Supplier_st = 
					dtSupplier.Rows[0]["Supplier_st"].ToString().Trim();

				this.Supplier_zip = 
					dtSupplier.Rows[0]["Supplier_zip"].ToString().Trim();

				this.Supplier_phone = 
					dtSupplier.Rows[0]["Supplier_phone"].ToString().Trim();

				this.Supplier_actdt = 
					dtSupplier.Rows[0]["Supplier_actdt"]!= System.DBNull.Value?((DateTime) dtSupplier.Rows[0]["Supplier_actdt"]).ToShortDateString():"";
				
				return this;
			}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Supplier.", ex);
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
						throw new Exception("The Supplier make you are attempting to " +
							"relate to this Supplier does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Supplier.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Supplier.", Ex);
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
						throw new Exception("The Supplier you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Supplier before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Supplier.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Supplier.", Ex);
			}
		}
  
		private XmlDocument GetDeleteSupplierXml(string SupplierID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.VarChar, 3, this.SupplierID.ToString(),
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
        
		private XmlDocument GetInsertSupplierXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];

			
			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.VarChar, 3, this.SupplierID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierDesc",
				SqlDbType.VarChar, 30, this.SupplierDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Supplier_addr1",
				SqlDbType.VarChar, 20, this.Supplier_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_addr2",
				SqlDbType.VarChar, 20, this.Supplier_addr2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_city",
				SqlDbType.VarChar, 14, this.Supplier_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Supplier_st",
				SqlDbType.VarChar, 2, this.Supplier_st.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Supplier_zip",
				SqlDbType.VarChar, 10, this.Supplier_zip.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_phone",
				SqlDbType.VarChar, 14, this.Supplier_phone.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_actdt",
				SqlDbType.VarChar, 4, this.Supplier_actdt.ToString(),
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

		private XmlDocument GetUpdateSupplierXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[9];
			
			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 3, this.SupplierID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierDesc",
				SqlDbType.Char, 30, this.SupplierDesc.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_addr1",
				SqlDbType.Char, 20, this.Supplier_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_addr2",
				SqlDbType.VarChar, 20, this.Supplier_addr2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_city",
				SqlDbType.VarChar, 14, this.Supplier_city.ToString(),
				ref cookItems);			

			DbRequestXmlCooker.AttachCookItem("Supplier_st",
				SqlDbType.VarChar, 2, this.Supplier_st.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Supplier_zip",
				SqlDbType.VarChar, 10, this.Supplier_zip.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Supplier_phone",
				SqlDbType.VarChar, 14, this.Supplier_phone.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Supplier_actdt",
				SqlDbType.VarChar, 4, this.Supplier_actdt.ToString(),
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

		private XmlDocument GetSupplierBySupplierIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 3, this.SupplierID.ToString(),
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

			if(this.SupplierDesc == "")
			{
				errorMessage += "The Supplier cannot be empty.  ";
				found = true;
			}

			DataTable dt =  LookupTables.GetTable("Supplier");
			
			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["SupplierDesc"].ToString().Trim().ToUpper() == this.SupplierDesc.Trim().ToUpper())
					{
						errorMessage = "This Supplier Description is Already exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["SupplierDesc"].ToString().Trim() == this.SupplierDesc.Trim() && dt.Rows[i]["SupplierID"].ToString().Trim() != this.SupplierID.ToString().Trim())
					{
						errorMessage = "The Supplier Description is Already exist.";
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
				EPolicy.LookupTables.Supplier oldSupplier = new EPolicy.LookupTables.Supplier();
				oldSupplier = oldSupplier.GetSupplier(this.SupplierID);//userID);

				history.BuildNotesForHistory("SupplierDesc",oldSupplier.SupplierDesc.ToString(),this.SupplierDesc.ToString(),mode);
				history.BuildNotesForHistory("Supplier_addr1",oldSupplier.Supplier_addr1,this.Supplier_addr1,mode);
				history.BuildNotesForHistory("Supplier_addr2",oldSupplier.Supplier_addr2,this.Supplier_addr2,mode);
				history.BuildNotesForHistory("Supplier_city",oldSupplier.Supplier_city,this.Supplier_city,mode);
				history.BuildNotesForHistory("Supplier_st",oldSupplier.Supplier_st,this.Supplier_st,mode);
				history.BuildNotesForHistory("Supplier_zip",oldSupplier.Supplier_zip,this.Supplier_zip,mode);
				history.BuildNotesForHistory("Supplier_phone",oldSupplier.Supplier_phone,this.Supplier_phone,mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("SupplierID.","",this.SupplierID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.SupplierID.ToString();
			history.Subject = "SUPPLIER";			
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
