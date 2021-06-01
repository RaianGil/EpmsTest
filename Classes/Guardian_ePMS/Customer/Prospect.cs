using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.LookupTables;
using EPolicy.Audit;
using System.Collections;
using EPolicy.XmlCooker;
using EPolicy.Customer;

namespace EPolicy.Customer
{
	/// <summary>
	/// Summary description for Prospect.
	/// </summary>
    /// 
    //Editado por Pedro 3/24/2014 a las 11:32 AM 
	public class Prospect
	{
		public Prospect()
		{
		}
        
		#region Variables
		
		private DataTable _CustomersMatchingPhoneNumbers;
		private DataTable _NavigationProspectTable;
		private int _Mode = (int) ProspectMode.UPDATE;
		private int _prospectID = 0;
		private string _customerNumber = "";
		private bool _isBusiness = false;
		private int _locationID = 0;
		private int _referredID = 0;
		private string _referredDesc = "";
		private DateTime _entryDate = DateTime.Now;
		private DateTime _convertToClient = DateTime.Now;
		private string _firstName = "";
		private string _lastName1 = "";
		private string _lastName2 = "";
		private int _householdIncomeID = 0;
		private string _homePhone = "";
		private string _workPhone = "";
		private string _cellular = "";
		private string _email = "";
		private string _businessName = "";
		private bool _modify = false;
		private bool _IsDriver = false;
		private string _Warning	= "";

		#endregion

		#region Properties

		

		public DataTable CustomersMatchingPhoneNumbers
		{
			get
			{
				return this._CustomersMatchingPhoneNumbers;
			}
			set
			{
				this._CustomersMatchingPhoneNumbers = value;
			}
		}
		
		public DataTable NavigationProspectTable
		{
			get
			{
				return this._NavigationProspectTable;
			}
			set
			{
				this._NavigationProspectTable = value;
			}
		}

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

		public int ProspectID
		{
			get
			{
				return this._prospectID;
			}
			set
			{
				this._prospectID = value;
				this.CustomerNumber = this.GetCustomerNumber();
			}
		}

		public string CustomerNumber
		{
			get
			{
				return this._customerNumber;
			}
			set
			{
				this._customerNumber = value.Trim();
			}
		}

		public bool IsBusiness
		{
			get
			{
				return this._isBusiness;
			}
			set
			{
				this._isBusiness = value;
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

		public int ReferredID
		{
			get
			{
				return this._referredID;
			}
			set
			{
				this._referredID = value;
			}
		}

		public string ReferredDesc
		{
			get
			{
				return this._referredDesc;
			}
			set
			{
				this._referredDesc = value.ToUpper().Trim();
			}
		}

		public DateTime EntryDate
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

		public DateTime ConvertToClient
		{
			get
			{
				return this._convertToClient;
			}
			set
			{
				this._convertToClient = value;
			}
		}

		public string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				this._firstName = value.ToUpper().Trim();
			}
		}

		public string LastName1
		{
			get
			{
				return this._lastName1;
			}
			set
			{
				this._lastName1 = value.ToUpper().Trim();
			}
		}

		public string LastName2
		{
			get
			{
				return this._lastName2;
			}
			set
			{
				this._lastName2 = value.ToUpper().Trim();
			}
		}

		public int HouseholdIncomeID
		{
			get
			{
				return this._householdIncomeID;
			}
			set
			{
				this._householdIncomeID = value;
			}
		}

		public string HomePhone
		{
			get
			{
				return this._homePhone;
			}
			set
			{
				this._homePhone = value.Trim();
			}
		}
		
		public string WorkPhone
		{
			get
			{
				return this._workPhone;
			}
			set
			{
				this._workPhone = value.Trim();
			}
		}

		public string Cellular
		{
			get
			{
				return this._cellular;
			}
			set
			{
				this._cellular = value.Trim();
			}
		}

		public string Email
		{
			get
			{
				return this._email;
			}
			set
			{
				this._email = value.ToUpper().Trim();
			}
		}

		public string BusinessName
		{
			get
			{
				return this._businessName;
			}
			set
			{
				this._businessName= value.ToUpper().Trim();
			}
		}

		public bool Modify
		{
			get
			{
				return this._modify;
			}
			set
			{
				this._modify = value;
			}
		}

		public bool IsDriver
		{
			get
			{
				return this._IsDriver;
			}
			set
			{
				this._IsDriver = value;
			}
		}

		public string Warning
		{
			get
			{
				return this._Warning;
			}
			set
			{
				this._Warning = value;
			}
		}
		#endregion

		#region Public Enumeration

		public enum ProspectMode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};
		public enum MutationType{PROSPECT = 1};

		#endregion

		#region Public Methods

		public void SaveProspect(int UserID, bool Validate)
		{
			if(Validate)
			{
				this.SaveProspect(UserID);
			}
			else
			{
				this.PersistProspectToDatabase(UserID);
			}
		}

		public void SaveProspect(int UserID)
		{
			this.Validate();
            this.PersistProspectToDatabase(UserID);			
		}

		private void PersistProspectToDatabase(int UserID)
		{
			this.Modify = true;
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();

			try
			{
                executor.BeginTrans();
				switch (this.Mode)
				{
					case 1: //ADD
						this.ProspectID = executor.Insert("AddProspect", 
							this.GetInsertProspectXml());
						//this.AuditInsert(UserID);
						//this.CommitAudit();
						this.History(this.Mode,UserID,(int)MutationType.PROSPECT);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this.Mode,UserID,(int)MutationType.PROSPECT);
						//this.AuditUpdate(UserID); //Prepare audit object.
						executor.Update("UpdateProspect",
							this.GetUpdateProspectXml());
						//this.CommitAudit(); //Commit audit;
						break;
				}
                executor.CommitTrans();
				this.Mode = (int) ProspectMode.CLEAR;
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				throw new Exception("Error while trying to save or modify prospect information. " + 
					xcp.Message, xcp);
			}
		}

		public Customer GetPopulatedCustomerFromProspect()
		{
			Customer customer = new Customer();
					
			//Common fields
			customer.FirstName = this.FirstName;
			customer.LastName1 = this.LastName1;
			customer.JobPhone = this.WorkPhone;
			customer.Email = this.Email;
			customer.LocationID = this.LocationID;
			
			if(this.IsBusiness)
			{
				customer.EmplName = this.BusinessName;
				customer.IsBusiness = true;				
			}
			else
			{
				customer.LastName2 = this.LastName2;
				customer.HomePhone = this.HomePhone;
				customer.Cellular = this.Cellular;
				customer.HouseHoldIncome = this.HouseholdIncomeID;
				customer.IsBusiness = false;
			}
			return customer;
		}

        // Editar Pedro 3/24/2014 POR 
		public DataTable GetProspectByCriteria(string ProspectNo,
			string FirstName, string LastName1,string LastName2,
			string Email, string Phone, bool IsBusiness, 
			bool All, /*string EmployerSocialSecurity,*/ 
			string BusinessName, int userID)
		{
			DataTable dt = GetProspectByCriteriaDB(ProspectNo, FirstName,
				LastName1, LastName2, Email, Phone,	IsBusiness, All, BusinessName, userID);
			return dt;
		}

		public DataTable GetClientsWithMatchingPhoneNumbers()
		{
			DataTable dt = this.GetClientsWithMatchingPhoneNumbersDB();
			return dt;
		}

		public Prospect GetProspect(int ProspectID)
		{
            DataTable dtProspect = new DataTable();
			Prospect prospect = new Prospect();
			this.ProspectID = ProspectID;

			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
			dtProspect = executor.GetQuery("GetProspectByProspectId", 
				this.GetProspectByProspectIdXML());

			this.IsBusiness = bool.Parse(dtProspect.Rows[0]["IsBusiness"].ToString().Trim());
			this.LocationID = int.Parse(dtProspect.Rows[0]["LocationID"].ToString().Trim());
			
			this.ReferredID = (dtProspect.Rows[0]["ReferredID"] != System.DBNull.Value)?
				int.Parse(dtProspect.Rows[0]["ReferredID"].ToString().Trim()):
				0;
			
			this.ReferredDesc = dtProspect.Rows[0]["ReferredDesc"].ToString().Trim();
			this.EntryDate = DateTime.Parse(dtProspect.Rows[0]["EntryDate"].ToString().Trim());
			
			this.ConvertToClient = (dtProspect.Rows[0]["ConvertToClient"] != 
				System.DBNull.Value)?
				(DateTime) dtProspect.Rows[0]["ConvertToClient"]:
				DateTime.Parse("1/1/1900");

			this.FirstName = dtProspect.Rows[0]["FirstName"].ToString().Trim();
			this.LastName1 = dtProspect.Rows[0]["LastName1"].ToString().Trim();
			this.LastName2 = dtProspect.Rows[0]["LastName2"].ToString().Trim();

			this.HouseholdIncomeID = (dtProspect.Rows[0]["HouseHoldIncomeID"] != 
				System.DBNull.Value)
				? int.Parse(dtProspect.Rows[0]["HouseHoldIncomeID"].ToString().Trim()) : 0;

			this.HomePhone = dtProspect.Rows[0]["HomePhone"].ToString().Trim();
			this.WorkPhone = dtProspect.Rows[0]["WorkPhone"].ToString().Trim();
			this.Cellular = dtProspect.Rows[0]["Cellular"].ToString().Trim();
			this.Email = dtProspect.Rows[0]["Email"].ToString().Trim();
			this.BusinessName = dtProspect.Rows[0]["BusinessName"].ToString().Trim();
			this.Modify = bool.Parse(dtProspect.Rows[0]["Modify"].ToString().Trim());

			return this;
		}

		#endregion

		#region Private Methods

		private string GetCustomerNumber()
		{
			DataTable dtResult = new DataTable();
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
			dtResult = executor.GetQuery("GetCustomerNumberByProspectID", 
				this.GetCustomerNumberFromProspectIdXml());
			if(dtResult.Rows.Count > 0 &&
				dtResult.Rows[0]["CustomerNo"].ToString().Trim() != "")
			{
				return dtResult.Rows[0]["CustomerNo"].ToString().Trim();
			}
			else
			{
				return "None";
			}
		}
		
		private XmlDocument GetCustomerNumberFromProspectIdXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, this.ProspectID.ToString(),
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

		private XmlDocument GetInsertProspectXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[16];

			DbRequestXmlCooker.AttachCookItem("IsBusiness",
				SqlDbType.Bit, 0, this.IsBusiness.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("LocationID",
				SqlDbType.Int, 0, this.LocationID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("ReferredID",
				SqlDbType.Int, 0, this.ReferredID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("ReferredDesc",
				SqlDbType.Char, 20, this.ReferredDesc.Trim(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("EntryDate",
				SqlDbType.DateTime, 8, this.EntryDate.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("ConvertToClient",
				SqlDbType.DateTime, 8, this.ConvertToClient.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.Char, 75, this.FirstName,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.Char, 15, this.LastName1,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.VarChar, 100, this.LastName2,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("HouseholdIncomeID",
				SqlDbType.Int, 0, this.HouseholdIncomeID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("HomePhone",
				SqlDbType.Char, 14, this.HomePhone,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("WorkPhone",
				SqlDbType.Char, 14, this.WorkPhone,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("Cellular",
				SqlDbType.Char, 14, this.Cellular,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("Email",
				SqlDbType.Char, 50, this.Email,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("BusinessName",
				SqlDbType.Char, 20, this.BusinessName,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("Modify",
				SqlDbType.Bit, 0, this.Modify.ToString(),
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
		private XmlDocument GetUpdateProspectXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[16];

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, this.ProspectID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("IsBusiness",
				SqlDbType.Bit, 0, this.IsBusiness.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("LocationID",
				SqlDbType.Int, 0, this.LocationID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("ReferredID",
				SqlDbType.Int, 0, this.ReferredID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("ReferredDesc",
				SqlDbType.Char, 20, this.ReferredDesc.Trim(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("ConvertToClient",
				SqlDbType.DateTime, 8, this.ConvertToClient.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.Char, 75, this.FirstName,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.Char, 15, this.LastName1,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.VarChar, 100, this.LastName2,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("HouseholdIncomeID",
				SqlDbType.Int, 0, this.HouseholdIncomeID.ToString(),
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("HomePhone",
				SqlDbType.Char, 14, this.HomePhone,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("WorkPhone",
				SqlDbType.Char, 14, this.WorkPhone,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("Cellular",
				SqlDbType.Char, 14, this.Cellular,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("Email",
				SqlDbType.Char, 50, this.Email,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("BusinessName",
				SqlDbType.Char, 20, this.BusinessName,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("Modify",
				SqlDbType.Bit, 0, this.Modify.ToString(),
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

		private XmlDocument GetProspectByProspectIdXML()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, this.ProspectID.ToString(),
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

		private DataTable GetClientsWithMatchingPhoneNumbersDB()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[4];


			DbRequestXmlCooker.AttachCookItem("WorkPhone",
				SqlDbType.Char, 14, this.WorkPhone,
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("HomePhone",
				SqlDbType.Char, 14, this.HomePhone,
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Cellular",
				SqlDbType.Char, 14, this.Cellular,
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("IsBusiness",
				SqlDbType.Bit, 0, this.IsBusiness.ToString(),
				ref cookItems);
			

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			DataTable dt = exec.GetQuery("GetCustomerByPhoneNumbers",DbRequestXmlCooker.Cook(cookItems));
			
			
			
			try
			{
				return dt;
//				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

		}
		
        // EDITADO POR 3/24/2014 11:30 AM
		private DataTable GetProspectByCriteriaDB(string ProspectId, string FirstName,
			string LastName1, string LastName2, string Email, string Phone,
			bool IsBusiness, bool All, string BusinessName, int userID)
		{
          

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[10];

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.VarChar, 10, ProspectId,
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("FirstName",
				SqlDbType.Char, 15, FirstName,
				ref cookItems);
            
			DbRequestXmlCooker.AttachCookItem("LastName1",
				SqlDbType.Char, 15, LastName1,
				ref cookItems);
			DbRequestXmlCooker.AttachCookItem("LastName2",
				SqlDbType.VarChar, 100, LastName2,
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Email",
				SqlDbType.Char, 50, Email,
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Phone",
				SqlDbType.Char, 14, Phone,
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("IsBusiness",
				SqlDbType.Bit, 0, IsBusiness.ToString(),
				ref cookItems);
            

			DbRequestXmlCooker.AttachCookItem("All",
				SqlDbType.Bit, 0, All.ToString(),
				ref cookItems);	

			DbRequestXmlCooker.AttachCookItem("BusinessName",
				SqlDbType.Char, 20, BusinessName,
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UserID",
                SqlDbType.Int, 0, userID.ToString(),
                ref cookItems);
           

           

            
			//
			XmlDocument xmlDoc;

			try
			{
               
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			try
			{
				DataTable dt = exec.GetQuery("GetProspectByCriteria", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
                throw new Exception("Could not retrieve prospect by criteria.", ex);
			}
		}

		
		private bool isEmail(string inputEmail)
		{
			//inputEmail  = NulltoString(inputEmail);
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			System.Text.RegularExpressions.Regex re = 
				new System.Text.RegularExpressions.Regex (strRegex);
			if (re.IsMatch(inputEmail.Trim()))
				return (true);
			else
				return (false);
		}
			

		private void Validate()
		{
			string errorMessage = String.Empty;
			this.Warning = String.Empty;

			bool found = false;

            //if(this.FirstName == "")
            //{
            //    errorMessage += "First name is missing or wrong.  ";
            //    found = true;
            //}

			if(this.LocationID == 0)
			{
				errorMessage += "Originated At is required.  ";
				found = true;
			}
			
            //if(this.HomePhone == "" && this.WorkPhone == "" && this.Cellular == "")
            //{
            //    if(this.IsBusiness)
            //    {
            //        errorMessage += "Phone number is required.  ";
            //    }
            //    else
            //    {
            //        errorMessage += "Home Phone, Work Phone or Cellular Phone is required.  ";
            //    }
            //    found = true;
            //}

            //if(this.HomePhone == string.Empty )//&& this.HomePhone.Length != 14)
            //{
            //    errorMessage += "Please verify homephone.  "; //The correct format is: " +
            //        //"(xxx) xxx-xxxx.  ";
            //    found = true;
            //}

            //if(this.WorkPhone == string.Empty) //&& this.WorkPhone.Length != 14)
            //{
            //    errorMessage += "Please verify phone number. "; // The correct format is: " +
            //        //"(xxx) xxx-xxxx.  ";
            //    found = true;
            //}

            //if(this.Cellular == string.Empty) // && this.Cellular.Length != 14)
            //{
            //    errorMessage += "Please verify cellular phone. "; // The correct format is: " +
            //        //"(xxx) xxx-xxxx.  ";
            //    found = true;
            //}

			//Check email address validity
            //if(this.Email.Trim() != "" && !this.isEmail(this.Email))
            //{
            //    errorMessage += "Email address is invalid.  ";
            //    found = true;
            //}

			//Verify by the phones.
			if (found == false && !this.IsDriver)
			{
				for(int x=1;x<=2;x++)
				{
					string phone="";
						
					switch (x)
					{
						case 1:
							if(this.IsBusiness)
							{
								if(this.WorkPhone != "")
								{
									phone = this.WorkPhone;
								}
								else
								{
									phone ="";
								}
							}
							else
							{
								if(this.HomePhone != "")
								{
									phone = this.HomePhone;
								}
								else
								{
									phone ="";
								}
							}
							break;

						case 2:
							if(this.Cellular != "")
							{
								phone = this.Cellular;
							}
							else
							{
								phone ="";
							}
							break;
					}
					
					if (phone != "")
					{
						DataTable dt = VerifyByPhone(phone);
						if (dt.Rows.Count != 0)
						{
							for(int i=0;i<=dt.Rows.Count-1;i++)
							{
								if(this.Mode == 1)
								{
									this.Warning = 
										"This phone number "+ phone.Trim()+
										" at least exist in the following prospect:\r\n"+
										"Prospect ID  :"+dt.Rows[i]["ProspectID"].ToString().Trim()+
										"\r\n"+"Prospect Name:"+
										dt.Rows[i]["FirstName"].ToString().Trim()+
										" "+dt.Rows[i]["Lastname1"].ToString().Trim();
									//found = true;
							
									i = dt.Rows.Count -1;
								}
								else if (this.ProspectID != 
									(int) dt.Rows[i]["ProspectID"] && this.Mode == 2)
								{
									this.Warning = 
										"This phone number "+ phone.Trim()+
										" at least exist in the following prospect:\r\n"+
										"Prospect ID  :"+dt.Rows[i]["ProspectID"].ToString().Trim()+
										"\r\n"+"Prospect Name:"+
										dt.Rows[i]["FirstName"].ToString().Trim()+" "+
										dt.Rows[i]["Lastname1"].ToString().Trim();
									//found = true;
								}
							}
						}
					}
				}
			}

			if (found == true)
			{
				throw new Exception(errorMessage);
			}
		}

		private DataTable VerifyByPhone(string phone)
		{
			DataTable dtProsByPhone;

			dtProsByPhone = GetProspectByPhoneDB(phone);
			return dtProsByPhone;
		}

		private DataTable GetProspectByPhoneDB(string phone)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];
			
			DbRequestXmlCooker.AttachCookItem("Phone",
				SqlDbType.Char, 14, phone,
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("IsBusiness",
				SqlDbType.Bit, 0, this.IsBusiness.ToString(),
				ref cookItems);

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			DataTable dt = exec.GetQuery("GetProspectVerifyByPhone",DbRequestXmlCooker.Cook(cookItems));

			try
			{
				return dt;
				//return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

		}

		private void History(int mode, int userID, int MutationTypeID)
		{
			Audit.History history = new Audit.History();
			
			if(mode == 2)
			{
				//Prospect oldProspect = null;
				EPolicy.Customer.Prospect oldProspect = new EPolicy.Customer.Prospect();

				oldProspect = oldProspect.GetProspect(this.ProspectID);//userID);
			    
				if(this.IsBusiness) //Business Prospect
				{
					history.BuildNotesForHistory("BusinessName",oldProspect.BusinessName,this.BusinessName,mode);
				}
				else 
				{
					history.BuildNotesForHistory("LastName2",oldProspect.LastName2,this.LastName2,mode);
					history.BuildNotesForHistory("HouseHoldIncome",
						LookupTables.LookupTables.GetDescription("HouseHoldIncome",oldProspect.HouseholdIncomeID.ToString()),
						LookupTables.LookupTables.GetDescription("HouseHoldIncome",this.HouseholdIncomeID.ToString()),
						mode);
					history.BuildNotesForHistory("HomePhone",oldProspect.HomePhone,this.HomePhone,mode);
					history.BuildNotesForHistory("Cellular",oldProspect.Cellular,this.Cellular,mode);
												
				}
				history.BuildNotesForHistory("IsBusiness",oldProspect.IsBusiness.ToString(),this.IsBusiness.ToString(),mode);
				history.BuildNotesForHistory("LocationID",
					LookupTables.LookupTables.GetDescription("Location",oldProspect.LocationID.ToString()),
					LookupTables.LookupTables.GetDescription("Location",this.LocationID.ToString()),
					mode);
				history.BuildNotesForHistory("ReferredID",
					LookupTables.LookupTables.GetDescription("ReferredBy",oldProspect.ReferredID.ToString()),
					LookupTables.LookupTables.GetDescription("ReferredBy",this.ReferredID.ToString()),
					mode);
				history.BuildNotesForHistory("ReferredDesc",oldProspect.ReferredDesc,this.ReferredDesc,mode);
				history.BuildNotesForHistory("FirstName",oldProspect.FirstName,this.FirstName,mode);
				history.BuildNotesForHistory("LastName1",oldProspect.LastName1,this.LastName1,mode);
				history.BuildNotesForHistory("WorkPhone",oldProspect.WorkPhone,this.WorkPhone,mode);
				history.BuildNotesForHistory("Email",oldProspect.Email,this.Email,mode);
				history.BuildNotesForHistory("Modify",oldProspect.Modify.ToString(),this.Modify.ToString(),mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("Prospect ID.","",this.ProspectID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.ProspectID.ToString();
			history.Subject = "PROSPECT";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}

		#endregion
	}
}