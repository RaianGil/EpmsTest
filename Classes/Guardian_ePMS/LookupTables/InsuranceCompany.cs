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
	/// Summary description for InsuranceCompany.
	/// </summary>
	public class InsuranceCompany
	{
		public InsuranceCompany()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables

		//private InsuranceCompany  oldInsuranceCompany = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  string _insuranceCompanyID = String.Empty;
		private  string _insuranceCompanyDesc = String.Empty;
		private  string _ins_names = String.Empty;
		private  string _ins_addr1 = String.Empty;
		private  string _ins_addr2 = String.Empty;
		private  string _ins_city = String.Empty;
		private  string _ins_st = String.Empty;
		private  string _ins_zip = String.Empty ;
		private  string _ins_phone = String.Empty ;
		private  string _ins_actdt = DateTime.Now.ToShortDateString();
		private  bool _ins_print = false ;
		private int _ins_seq = 0 ;
		private bool _ins_canpr = false;
		private bool _ins_labpr = false;
		private int _apuntador = 0 ;
		private int _quote = 0;
		private int _apun_trams = 0;

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


		public string InsuranceCompanyDesc
		{
			get
			{
				return this._insuranceCompanyDesc;
			}
			set
			{
				this._insuranceCompanyDesc = value;
			}
		}


		public string ins_names
		{
			get
			{
				return this._ins_names;
			}
			set
			{
				this._ins_names = value;
			}
		}
		public string ins_addr1
		{
			get
			{
				return this._ins_addr1;
			}
			set
			{
				this._ins_addr1 = value;
			}
		}
        
		public string ins_addr2
		{
			get
			{
				return this._ins_addr2;
			}
			set
			{
				this._ins_addr2 = value;
			}
		}
        
		public string ins_city
		{
			get
			{
				return this._ins_city;
			}
			set
			{
				this._ins_city = value;
			}
		}
        
		public string ins_st
		{
			get
			{
				return this._ins_st;
			}
			set
			{
				this._ins_st = value;
			}
		}

		public string ins_zip
		{
			get
			{
				return this._ins_zip;
			}
			set
			{
				this._ins_zip = value;
			}
		}


		public string ins_phone
		{
			get
			{
				return this._ins_phone;
			}
			set
			{
				this._ins_phone = value;
			}
		}

		public string ins_actdt
		{
			get
			{
				return this._ins_actdt;
			}
			set
			{
				this._ins_actdt = value;
			}
		}

		public bool ins_print
		{
			get
			{
				return this._ins_print;
			}
			set
			{
				this._ins_print = value;
			}
		}

		public int ins_seq
		{
			get
			{
				return this._ins_seq;
			}
			set
			{
				this._ins_seq = value;
			}
		}

		public bool ins_canpr
		{
			get
			{
				return this._ins_canpr;
			}
			set
			{
				this._ins_canpr = value;
			}
		}

		public bool ins_labpr
		{
			get
			{
				return this._ins_labpr;
			}
			set
			{
				this._ins_labpr = value;
			}
		}

		public int apuntador
		{
			get
			{
				return this._apuntador;
			}
			set
			{
				this._apuntador = value;
			}
		}

		public int quote
		{
			get
			{
				return this._quote;
			}
			set
			{
				this._quote = value;
			}
		}

		public int apun_trams
		{
			get
			{
				return this._apun_trams;
			}
			set
			{
				this._apun_trams = value;
			}
		}

		#endregion

		#region Public Methods
		      
		private string GetNextInsuranceCompanyID()
		{
			DataTable dt = LookupTables.GetTable("InsuranceCompany");
			DataRow[] dr = dt.Select("","InsuranceCompanyID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["InsuranceCompanyID"] = dr[rec].ItemArray[0].ToString();
				myRow["InsuranceCompanyDesc"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["InsuranceCompanyID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}
		#endregion    

		#region Public Functions

		public void Delete(string InsuranceCompanyID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteInsuranceCompanyID", 
					this.GetDeleteInsuranceCompanyXml(InsuranceCompanyID));
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
						this.InsuranceCompanyID = GetNextInsuranceCompanyID();
						executor.Update("AddInsuranceCompany",this.GetInsertInsuranceCompanyXml());
						this.History(this._actionMode,UserID);
//						      this.AuditInsert(UserID);
//						      this.CommitAudit();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
//						this.AuditUpdate(UserID);
						executor.Update("UpdateInsuranceCompany",this.GetUpdateInsuranceCompanyXml());
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

		public InsuranceCompany GetInsuranceCompany(string InsuranceCompanyID)
		{
			try
			{
				DataTable dtInsuranceCompany = new DataTable();
				InsuranceCompany insuranceCompany = new InsuranceCompany();
				this.InsuranceCompanyID = InsuranceCompanyID;
		


				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtInsuranceCompany = executor.GetQuery("GetinsuranceCompanyByInsuranceCompanyID", 
					this.GetInsuranceCompanyByInsuranceCompanyIDXml());

				this.InsuranceCompanyDesc = 
					dtInsuranceCompany.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();

				this.ins_names = 
					dtInsuranceCompany.Rows[0]["ins_names"].ToString().Trim();

				this.InsuranceCompanyID = 
					dtInsuranceCompany.Rows[0]["InsuranceCompanyID"].ToString().Trim();

				this.ins_addr1 = 
					dtInsuranceCompany.Rows[0]["ins_addr1"].ToString().Trim();

				this.ins_addr2 = 
					dtInsuranceCompany.Rows[0]["ins_addr2"].ToString().Trim();

				this.ins_city = 
					dtInsuranceCompany.Rows[0]["ins_city"].ToString().Trim();

				this.ins_st = 
					dtInsuranceCompany.Rows[0]["ins_st"].ToString().Trim();

				this.ins_zip = 
					dtInsuranceCompany.Rows[0]["ins_zip"].ToString().Trim();

				this.ins_phone = 
					dtInsuranceCompany.Rows[0]["ins_phone"].ToString().Trim();

				this.ins_actdt = 
					dtInsuranceCompany.Rows[0]["ins_actdt"]!= System.DBNull.Value?((DateTime) dtInsuranceCompany.Rows[0]["ins_actdt"]).ToShortDateString():"";

				this.ins_print = 
					bool.Parse(dtInsuranceCompany.Rows[0]["ins_print"].ToString().Trim());

				this.ins_seq = 
					int.Parse(dtInsuranceCompany.Rows[0]["ins_seq"].ToString().Trim());

				this.ins_canpr = 
					bool.Parse(dtInsuranceCompany.Rows[0]["ins_canpr"].ToString().Trim());

				this.ins_labpr =
					bool.Parse(dtInsuranceCompany.Rows[0]["ins_labpr"].ToString().Trim());

				this.apuntador = 
					int.Parse(dtInsuranceCompany.Rows[0]["apuntador"].ToString().Trim());

				this.quote = 
					int.Parse(dtInsuranceCompany.Rows[0]["quote"].ToString().Trim());

				this.apun_trams = 
					int.Parse(dtInsuranceCompany.Rows[0]["apun_trams"].ToString().Trim());

		 return this;

				}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Insurance Comapny.", ex);
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
						throw new Exception("The Insurance Company make you are attempting to " +
							"relate to this Insurance Company does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Insurance Company.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Insurance Company.", Ex);
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
						throw new Exception("The Insurance Company you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Insurance Company before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Insurance Company.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Isurance Company.", Ex);
			}
		}
  
		private XmlDocument GetDeleteInsuranceCompanyXml(string InsuranceCompanyID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.VarChar, 3, InsuranceCompanyID.ToString(),
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
        
		private XmlDocument GetInsertInsuranceCompanyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[17];
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.VarChar, 3, this.InsuranceCompanyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyDesc",
				SqlDbType.VarChar, 30, this.InsuranceCompanyDesc.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ins_names",
				SqlDbType.VarChar, 10, this.ins_names.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ins_addr1",
				SqlDbType.VarChar, 20, this.ins_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ins_addr2",
				SqlDbType.VarChar, 20, this.ins_addr2.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_city",
				SqlDbType.VarChar, 14, this.ins_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_st",
				SqlDbType.VarChar, 2, this.ins_st.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_zip",
				SqlDbType.VarChar, 10, this.ins_zip.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_phone",
				SqlDbType.VarChar, 12, this.ins_phone.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_actdt",
				SqlDbType.VarChar, 4, this.ins_actdt.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_print",
				SqlDbType.Bit, 0, this.ins_print.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_seq",
				SqlDbType.Int, 0, this.ins_seq.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_canpr",
				SqlDbType.Bit, 0, this.ins_canpr.ToString(),
				ref cookItems);

			
			DbRequestXmlCooker.AttachCookItem("ins_labpr",
				SqlDbType.Bit, 0, this.ins_labpr.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("apuntador",
				SqlDbType.Int, 0, this.apuntador.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("quote",
				SqlDbType.Int, 0, this.quote.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("apun_trams",
				SqlDbType.Int, 0, this.apun_trams.ToString(),
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

		private XmlDocument GetUpdateInsuranceCompanyXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[17];
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.VarChar, 3, this.InsuranceCompanyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyDesc",
				SqlDbType.VarChar, 30, this.InsuranceCompanyDesc.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ins_names",
				SqlDbType.VarChar, 10, this.ins_names.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ins_addr1",
				SqlDbType.VarChar, 20, this.ins_addr1.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ins_addr2",
				SqlDbType.VarChar, 20, this.ins_addr2.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_city",
				SqlDbType.VarChar, 14, this.ins_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_st",
				SqlDbType.VarChar, 2, this.ins_st.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_zip",
				SqlDbType.VarChar, 10, this.ins_zip.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_phone",
				SqlDbType.VarChar, 12, this.ins_phone.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_actdt",
				SqlDbType.VarChar, 4, this.ins_actdt.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_print",
				SqlDbType.Bit, 0, this.ins_print.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_seq",
				SqlDbType.Int, 0, this.ins_seq.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ins_canpr",
				SqlDbType.Bit, 0, this.ins_canpr.ToString(),
				ref cookItems);

			
			DbRequestXmlCooker.AttachCookItem("ins_labpr",
				SqlDbType.Bit, 0, this.ins_labpr.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("apuntador",
				SqlDbType.Int, 0, this.apuntador.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("quote",
				SqlDbType.Int, 0, this.quote.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("apun_trams",
				SqlDbType.Int, 0, this.apun_trams.ToString(),
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

		private XmlDocument GetInsuranceCompanyByInsuranceCompanyIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.Int, 0, this.InsuranceCompanyID.ToString(),
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

			if(this.InsuranceCompanyDesc == "")
			{
				errorMessage += "The Insurance Company cannot be empty.  ";
				found = true;
			}

			DataTable dt =  LookupTables.GetTable("InsuranceCompany");

			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["InsuranceCompanyDesc"].ToString().Trim() == this.InsuranceCompanyDesc.Trim())
					{
						errorMessage = "The InsuranceCompany Description is Already Exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["InsuranceCompanyDesc"].ToString().Trim() == this.InsuranceCompanyDesc.Trim() && dt.Rows[i]["InsuranceCompanyID"].ToString().Trim() != this.InsuranceCompanyID.ToString().Trim())
					{
						errorMessage = "The InsuranceCompany Description is Already Exist.";
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
				EPolicy.LookupTables.InsuranceCompany oldInsuranceCompany = new EPolicy.LookupTables.InsuranceCompany();
				oldInsuranceCompany = oldInsuranceCompany.GetInsuranceCompany(this.InsuranceCompanyID);//userID);

				history.BuildNotesForHistory("InsuranceCompanyDesc",oldInsuranceCompany.InsuranceCompanyDesc.ToString(),this.InsuranceCompanyDesc.ToString(),mode);
				history.BuildNotesForHistory("ins_names",oldInsuranceCompany.ins_names,this.ins_names,mode);
				history.BuildNotesForHistory("ins_addr1",oldInsuranceCompany.ins_addr1,this.ins_addr1,mode);
				history.BuildNotesForHistory("ins_addr2",oldInsuranceCompany.ins_addr2,this.ins_addr2,mode);
				history.BuildNotesForHistory("ins_city",oldInsuranceCompany.ins_city,this.ins_city,mode);
				history.BuildNotesForHistory("ins_st",oldInsuranceCompany.ins_st.ToString(),this.ins_st.ToString(),mode);
				history.BuildNotesForHistory("ins_zip",oldInsuranceCompany.ins_zip.ToString(),this.ins_zip.ToString(),mode);
				history.BuildNotesForHistory("ins_phone",oldInsuranceCompany.ins_phone.ToString(),this.ins_phone.ToString(),mode);
				history.BuildNotesForHistory("ins_actdt",oldInsuranceCompany.ins_actdt.ToString(),this.ins_actdt.ToString(),mode);
				history.BuildNotesForHistory("ins_print",oldInsuranceCompany.ins_print.ToString(),this.ins_print.ToString(),mode);
				history.BuildNotesForHistory("ins_seq",oldInsuranceCompany.ins_seq.ToString(),this.ins_seq.ToString(),mode);
				history.BuildNotesForHistory("ins_canpr",oldInsuranceCompany.ins_canpr.ToString(),this.ins_canpr.ToString(),mode);
				history.BuildNotesForHistory("ins_labpr",oldInsuranceCompany.ins_labpr.ToString(),this.ins_labpr.ToString(),mode);
				history.BuildNotesForHistory("apuntador",oldInsuranceCompany.apuntador.ToString(),this.apuntador.ToString(),mode);
				history.BuildNotesForHistory("quote",oldInsuranceCompany.quote.ToString(),this.quote.ToString(),mode);
				history.BuildNotesForHistory("apun_trams",oldInsuranceCompany.apun_trams.ToString(),this.apun_trams.ToString(),mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("InsuranceCompanyID.","",this.InsuranceCompanyID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.InsuranceCompanyID.ToString();
			history.Subject = "INSURANCE COMPANY";			
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
