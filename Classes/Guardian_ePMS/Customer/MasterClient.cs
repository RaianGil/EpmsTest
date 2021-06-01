using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.LookupTables;
using EPolicy.XmlCooker;
using EPolicy.Audit;



namespace EPolicy.Customer
{
	/// <summary>
	/// Summary description for MasterClient.
	/// </summary>
	public class MasterCustomer
	{
		public MasterCustomer()
		{
	
		}

		#region Variables

		private DataTable _dtMasterClient;
		private DataTable _NavegationMasterClientTable;
		private int _Mode = (int) MasterCustomerMode.CLEAR;
		private int    _MasterCustomerID = 0;
		private string _Firstna	= "";
		private string _Lastna1	= "";
		private string _Lastna2	= "";
		private string _MasterCustomerDesc = "";
		private bool   _Modify			 = true;

		#endregion

		#region Properties

		public int Mode
		{
			get
			{
				return this._Mode;
			}
			set
			{
				this._Mode = value;
			}
		}

		public DataTable NavegationMasterClientTable
		{
			get
			{
				return this._NavegationMasterClientTable;
			}
			set
			{
				this._NavegationMasterClientTable = value;
			}
		}

		public int MasterCustomerID
		{
			get
			{
				return this._MasterCustomerID;
			}
			set 
			{
				this._MasterCustomerID = value;
			}
		}

		public string FirstName    //Used for Customer and ContactPerson.
		{
			get
			{
				return this._Firstna;
			}
			set 
			{
				this._Firstna = value;
			}
		}

		public string LastName1    //Used for Customer and ContactPerson.
		{
			get
			{
				return this._Lastna1;
			}
			set 
			{
				this._Lastna1 = value;
			}
		}

		public string LastName2
		{
			get
			{
				return this._Lastna2;
			}
			set 
			{
				this._Lastna2 = value;
			}
		}

		public string MasterCustomerDesc
		{
			get
			{
				return this._MasterCustomerDesc;
			}
			set
			{
				this._MasterCustomerDesc = value;
			}
		}

		public bool Modify
		{
			get
			{
				return this._Modify;
			}
			set
			{
				this._Modify = value;
			}
		}


		#endregion

		#region Public Enumeration

		public enum MasterCustomerMode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Public Methods

		public void Save(int UserID)
		{
			this.Validate();
       
			 SaveMasterCustomer(UserID);      //Save MasterCustomer

			this.Mode = (int)MasterCustomerMode.CLEAR;
		}

		public static DataTable GetMasterCustomerByCriteria(string MasterCustomerID,string FirstName,string LastName1,string LastName2)
		{
			DataTable dt = GetMasterCustomerByCriteriaDB(MasterCustomerID,FirstName,LastName1,LastName2);

			return dt;
		}

		public static MasterCustomer GetMasterCustomer(int MasterCustomerID)
		{
			MasterCustomer masterCustomer = new MasterCustomer();
				
			masterCustomer._dtMasterClient = 
				GetMasterCustomerByMasterCustomerIDDB(MasterCustomerID);
			
			
			masterCustomer = FillMasterCustomerProperties(masterCustomer);
			return masterCustomer;
		}

		public static DataTable GetCustomerByMasterCustomerID(int MasterCustomerID)
		{
			DataTable dt = GetCustomerByMasterCustomerIDDB(MasterCustomerID);

			return dt;
		}

		#endregion

		#region Private Methods

		private static DataTable GetMasterCustomerByCriteriaDB(string MasterCustomerID,string FirstName,string LastName1,string LastName2)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[4];

			DbRequestXmlCooker.AttachCookItem("MasterCustomerID",
				SqlDbType.VarChar, 15, MasterCustomerID,
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.VarChar, 15, FirstName.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.VarChar, 15, LastName1.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.VarChar, 15, LastName2.Trim(),
				ref cookItems);
			
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			
			try
			{
				DataTable dt = exec.GetQuery("GetMasterCustomerByCriteria", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
		}

		private static DataTable GetMasterCustomerByMasterCustomerIDDB(int MasterCustomerID)

		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("MasterCustomerID",
				SqlDbType.Int, 0, MasterCustomerID.ToString(),
				ref cookItems);

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;
		
			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			
			try
			{
				DataTable dt = exec.GetQuery("GetMasterCustomerByMasterCustomerID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
		
		
		}


		private static DataTable GetCustomerByMasterCustomerIDDB(int MasterCustomerID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("MasterCustomerID",
				SqlDbType.Char, 10, MasterCustomerID.ToString(),
				ref cookItems);

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;
			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			
			try
			{
				DataTable dt = exec.GetQuery("GetCustomerByMasterCustomerID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
			
		}

		private void Validate()
		{
			string errorMessage = String.Empty;
			bool found = false;


			if (this.FirstName == "" &&  found == false)
			{
				errorMessage = "FirstName is missing or wrong.";
				found = true;
			}

			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}	
		}


		private void SaveMasterCustomer(int UserID)
		{
			this._Modify = true;

			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();

				switch (this.Mode)
				{
					case 1:  //ADD
						this.MasterCustomerID = 
							Executor.Insert("AddMasterCustomer",this.GetInsertMasterCustomerXml());
						this.History(this.Mode,UserID);
//						this.AuditInsert(UserID);
//						this.CommitAudit();
						break;

					case 3:  //DELETE
						Executor.Update("DeleteMasterCustomer",this.GetDeleteMasterCustomerXml());
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						this.History(this.Mode,UserID);
//						this.AuditUpdate(UserID);
						Executor.Update("UpdateMasterCustomer",this.GetUpdateMasterCustomerXml());
//						this.CommitAudit();//Commit audit;
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the Customer. "+xcp.Message,xcp);
			}
		}

		private XmlDocument GetDeleteMasterCustomerXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("MasterCustomerID",
				SqlDbType.Int, 0, MasterCustomerID.ToString(),
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

		private XmlDocument GetInsertMasterCustomerXml()
		{
			string masterDesc = this.FirstName.Trim()+' '+this.LastName1.Trim()+' '+this.LastName2.Trim();

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[5];

			DbRequestXmlCooker.AttachCookItem("MasterCustomerDesc",
				SqlDbType.Char, 45, masterDesc.Trim(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.Char, 15, FirstName.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.Char, 15, LastName1.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.Char, 15, LastName2.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Modify",
				SqlDbType.Bit, 0, _Modify.ToString(),
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

		private XmlDocument GetUpdateMasterCustomerXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			DbRequestXmlCooker.AttachCookItem("MasterCustomerID",
				SqlDbType.Int, 0, MasterCustomerID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("MasterCustomerDesc",
				SqlDbType.Char, 45, MasterCustomerDesc.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.Char, 15, FirstName.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.Char, 15, LastName1.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.Char, 15, LastName2.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Modify",
				SqlDbType.Bit, 0, _Modify.ToString(),
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

		#endregion

		#region FillProperties
		private static MasterCustomer FillMasterCustomerProperties(MasterCustomer masterCustomer)
		{
			masterCustomer.MasterCustomerID	= (int)  masterCustomer._dtMasterClient.Rows[0]["MasterCustomerID"];
			masterCustomer.MasterCustomerDesc = masterCustomer._dtMasterClient.Rows[0]["MasterCustomerDesc"].ToString();
			masterCustomer.FirstName		= masterCustomer._dtMasterClient.Rows[0]["FirstName"].ToString().Trim();
			masterCustomer.LastName1		= masterCustomer._dtMasterClient.Rows[0]["LastName1"].ToString().Trim();
			masterCustomer.LastName2		= masterCustomer._dtMasterClient.Rows[0]["LastName2"].ToString().Trim();
			
			return masterCustomer;
		}
		#endregion

		#region History

		private void History(int mode, int userID)
		{
			Audit.History history = new Audit.History();

			EPolicy.Customer.MasterCustomer oldMasterCustomer = new EPolicy.Customer.MasterCustomer();

//			oldMasterCustomer = oldMasterCustomer.g(this.MasterPolicyID);//userID);
			oldMasterCustomer = MasterCustomer.GetMasterCustomer(this.MasterCustomerID);
			
			if(mode == 2)
			{
				history.BuildNotesForHistory("FirstName",oldMasterCustomer.FirstName.ToString(),this.FirstName.ToString(),mode);
				history.BuildNotesForHistory("LastName1",oldMasterCustomer.LastName1.ToString(),this.LastName1.ToString(),mode);
				history.BuildNotesForHistory("LastName2",oldMasterCustomer.LastName2,this.LastName2,mode);
				
				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("MasterCustomerID.","",this.MasterCustomerID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.MasterCustomerID.ToString();
			history.Subject = "MASTER CUSTOMER";			
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

	}
}
