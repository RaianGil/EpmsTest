using System;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;
using EPolicy.LookupTables;
using Baldrich.DBRequest;
using EPolicy.Customer;
using EPolicy.Audit;
using EPolicy.XmlCooker;


namespace EPolicy.TaskControl
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class TaskControl
	{
		public TaskControl()
		{
	
		}

		#region Variable

		private DataTable _dtTaskControl ;
		private DataTable _NavegationTaskControlTable;
		private int _Mode = (int) TaskControlMode.CLEAR;
		private Customer.Prospect _Prospect = new Customer.Prospect();
		private Customer.Customer _Customer = new Customer.Customer();
		private int      _TaskControlID     = 0;
		private int      _TaskControlTypeID = 0;
		private int      _TaskStatusID      = 0;
		private int      _ProspectID		= 0;
		private string   _CustomerNo        = "";
		private int	     _PolicyID          = 0;
		private int      _PolicyClassID     = 0;
		private string   _InsuranceCompany = "";
		private string   _Bank             = "";
		private string   _Agency           = "000";
		private string   _Agent            = "";
		private string   _SupplierID       = "";
		private string   _Dealer           = "";
		private string   _CompanyDealer    = "";
		private string   _CloseDate        = "";
		private string	 _EnteredBy		   = "";
		private DateTime _EntryDate        = DateTime.Now;
		private int      _Aging            = 0;
		private LookupTables.CompanyDealer _CompanyDealerObject;
        private string _AgentCode = "";
        private string _AgentDesc = "";


		#endregion

		#region Properties

		public LookupTables.CompanyDealer CompanyDealerObject
		{
			get
			{
				return this._CompanyDealerObject;
			}
			set
			{
				this._CompanyDealerObject = value;
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

		public DataTable NavegationTaskControlTable
		{
			get
			{
				return this._NavegationTaskControlTable;
			}
			set
			{
				this._NavegationTaskControlTable = value;
			}
		}

		public Customer.Prospect Prospect
		{
			get
			{
				return this._Prospect;
			}
			set
			{
				this._Prospect = value;
			}
		}

		public Customer.Customer Customer
		{
			get
			{
				return this._Customer;
			}
			set
			{
				this._Customer = value;
			}
		}

		public int TaskControlID
		{
			get
			{
				return this._TaskControlID;
			}
			set
			{
				this._TaskControlID = value;
			}
		}

		public int TaskControlTypeID
		{
			get
			{
				return this._TaskControlTypeID;
			}
			set
			{
				this._TaskControlTypeID = value;
			}
		}

		public int TaskStatusID
		{
			get
			{
				return this._TaskStatusID;
			}
			set
			{
				this._TaskStatusID = value;
			}
		}

		public int ProspectID
		{
			get
			{
				return this._ProspectID;
			}
			set
			{
				this._ProspectID = value;

				if (this._ProspectID != 0)
				{
					Customer.Prospect prospect = new Customer.Prospect();
					this.Prospect = prospect.GetProspect(this._ProspectID);
				}
			}
		}
		
		public string CustomerNo
		{
			get
			{
				return this._CustomerNo;
			}
			set
			{
				this._CustomerNo = value;
			}
		}

		public int PolicyID
		{
			get
			{
				return this._PolicyID;
			}
			set
			{
				this._PolicyID = value;
			}
		}

		public int PolicyClassID
		{
			get
			{
				return this._PolicyClassID;
			}
			set
			{
				this._PolicyClassID = value;
			}
		}

		public string InsuranceCompany
		{
			get
			{
				return this._InsuranceCompany;
			}
			set
			{
				this._InsuranceCompany = value;
			}
		}

		public string Bank
		{
			get
			{
				return this._Bank;
			}
			set
			{
				this._Bank = value;
			}
		}

		public string Agency
		{
			get
			{
				return this._Agency;
			}
			set
			{
				this._Agency = value;
			}
		}

		public string Agent
		{
			get
			{
				return this._Agent;
			}
			set
			{
				this._Agent = value;
			}
		}

		public string SupplierID
		{
			get
			{
				return this._SupplierID;
			}
			set
			{
				this._SupplierID = value;
			}
		}

		public string Dealer
		{
			get
			{
				return this._Dealer;
			}
			set
			{
				this._Dealer = value;
			}
		}

		public string CompanyDealer
		{
			get
			{
				return this._CompanyDealer;
			}
			set
			{
				this._CompanyDealer = value;
			}
		}

		public DateTime EntryDate
		{
			get
			{
				return this._EntryDate;
			}
			set
			{
				this._EntryDate = value;
			}
		}

		public string CloseDate
		{
			get
			{
				return this._CloseDate;
			}
			set
			{
				this._CloseDate = value;
			}
		}

		public string EnteredBy
		{
			get
			{
				return this._EnteredBy;
			}
			set
			{
				this._EnteredBy = value;
			}
		}

		public int Aging
		{
			get
			{
				return this._Aging;
			}
			set 
			{
				this._Aging = value;
			}
		}

        public string AgentCode
        {
            get
            {
                return this._AgentCode;
            }
            set
            {
                this._AgentCode = value;
            }
        }

        public string AgentDesc
        {
            get
            {
                return this._AgentDesc;
            }
            set
            {
                this._AgentDesc = value;
            }
        }
	
		#endregion

		#region Public Enumeration

		public enum TaskControlMode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Public Methods

		public virtual void Save() //Temporary overload.
		{
			this.SaveTaskControl();
		}

		public virtual void Save(int UserID)
		{
			this.SaveTaskControl(UserID);
		}

		public static DataTable GetTaskControlByCriteria(string taskControlTypeID, string taskStatusID, string agentID, string bankID, string BegDate, string EndDate, string DateType, int UserID)
		{
			DataTable dt = GetTaskControlByCriteriaDB(taskControlTypeID,taskStatusID, agentID, bankID, BegDate, EndDate, DateType, UserID);
		          
			return dt;
		}

		public static DataTable GetTaskControlByTaskControlIDInTable(string TaskControlID, int UserID)
		{
			DataTable dt= GetTaskControlByTaskControlIDInTableDB(TaskControlID, UserID);

			return dt;
		}

		public static TaskControl GetTaskControlByTaskControlID(int TaskControlID,
			int UserID)
		{
			TaskControl taskControl = null;
			 
			DataTable dt= GetTaskControlByTaskControlIDDB(TaskControlID);

			if (dt.Rows[0]["CustomerNo"].ToString() != "" && dt.Rows[0]["ProspectID"] == System.DBNull.Value)
			{
				EPolicy.Customer.Customer cust = EPolicy.Customer.Customer.GetCustomer(dt.Rows[0]["CustomerNo"].ToString(),UserID);
				if (cust != null)
				{
					if (cust.ProspectID != 0 )
					{
						UpdateTaskControlProspectID(cust.ProspectID, TaskControlID);
					}
				}
			}

			if (dt.Rows.Count > 0)
			{
				int TaskControlTypeID = int.Parse(dt.Rows[0]["TaskControlTypeID"].ToString());
				string taskControlDesc = LookupTables.LookupTables.GetDescription("TaskControlType",dt.Rows[0]["TaskControlTypeID"].ToString().Trim());
				taskControlDesc = taskControlDesc.ToString().Replace(" ","").Trim();

				Type t;
				switch (TaskControlTypeID)
				{
					case 4:				// Quote Auto
						t = Type.GetType("EPolicy.TaskControl." + taskControlDesc,true);
						taskControl = (TaskControl) t.InvokeMember("Get"+taskControlDesc,
							BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
							null,null,new object[3] {TaskControlID,UserID,false});	
						break;

					case 10:			// Auto Personal Policy
						taskControlDesc = "QuoteAuto";
						t = Type.GetType("EPolicy.TaskControl." + taskControlDesc,true);
						taskControl = (TaskControl) t.InvokeMember("Get"+taskControlDesc,
							BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
							null,null,new object[3] {TaskControlID,UserID,true});	
						break;

                    case 14:			// PersonalPackage
                        taskControlDesc = "PersonalPackage";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] {TaskControlID, false });
                        break;

                    case 15:			//PersonalPackageQuote
                        taskControlDesc = "PersonalPackage";                                           
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] {TaskControlID, true });
                        break;

                    case 18: // Use for QCertified
                        taskControlDesc = "VehicleServiceContractQuote";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[1] {TaskControlID});
                        break;

                    case 20:			// DwellingQuote
                        taskControlDesc = "Dwelling";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] { TaskControlID, true });
                        break;

                    case 21:			// Dwelling
                        taskControlDesc = "Dwelling";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] { TaskControlID, false });
                        break;

                    case 22:			// ProfessionalLiabilityQuote
                        taskControlDesc = "ProfessionalLiability";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] { TaskControlID, true });
                        break;

                    case 23:			// ProfessionalLiability
                        taskControlDesc = "ProfessionalLiability";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] { TaskControlID, false });
                        break;

                    case 25:			// AUTOS VI
                        taskControlDesc = "Autos";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] { TaskControlID, true });
                        break;

                    case 26:			// AUTOS VI
                        taskControlDesc = "Autos";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                            null, null, new object[2] { TaskControlID, false });
                        break;



                    case 29:			// GuardianRoadAssist
                        taskControlDesc = "RoadAssistance";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[1] { TaskControlID });
                        break;

                    case 30:			// Double Interest Policy
                        taskControlDesc = "DoubleInterest";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID,true });
                        break;

                    case 31:			// Double Interest Policy
                        taskControlDesc = "DoubleInterest";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, false });
                        break;

                    case 32:            // Home Owners Quote
                        taskControlDesc = "HomeOwners";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get"+ taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, true });
                        break;

                    case 33:            // Home Owners Policy
                        taskControlDesc = "HomeOwners";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get"+ taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, false });
                        break;

                    case 34:                   // Bonds
                        taskControlDesc = "Bonds";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, false });
                        break;

                    case 35:                   // Bonds Quote
                        taskControlDesc = "Bonds";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, true });
                        break;

                    case 37:                   // Yacht Policy
                        taskControlDesc = "Yacht";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, false });
                        break;

                    case 38:                   // Yacht Quote
                        taskControlDesc = "Yacht";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, true });
                        break;

                    case 40:                   // Auto High Limit Quote
                        taskControlDesc = "AutoHighLimit";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, true });
                        break;

                    case 41:                   // Auto High Limit Policy
                        taskControlDesc = "AutoHighLimit";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, true });
                        break;

                    case 42:                   // Bonds
                        taskControlDesc = "RES";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, false });
                        break;

                    case 43:                   // Bonds Quote
                        taskControlDesc = "RES";
                        t = Type.GetType("EPolicy.TaskControl." + taskControlDesc, true);
                        taskControl = (TaskControl)t.InvokeMember("Get" + taskControlDesc,
                            BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
                           null, null, new object[2] { TaskControlID, true });
                        break;


                    default:		   //Other Types
						t = Type.GetType("EPolicy.TaskControl." + taskControlDesc,true);
						taskControl = (TaskControl) t.InvokeMember("Get"+taskControlDesc,
							BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static,
							null,null,new object[1] {TaskControlID});
						break;
				}	
			}		

			taskControl._dtTaskControl = dt;
			taskControl = FillProperties(taskControl, UserID);

			return taskControl;
		}
		
		public static DataTable GetTaskControlByProspectID(int ProspectID)
		{
			DataTable dt= GetTaskControlByProspectIDDB(ProspectID);

			return dt;
		}

        public static DataTable GetTaskControlByCustomerNo(string CustomerNo, int UserID)
		{
			DataTable dt= GetTaskControlByCustomerNoDB(CustomerNo, UserID);

			return dt;
		}

		public static int GetStatusByTaskControlID(int taskControlID)
		{

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",

				SqlDbType.Int, 0, taskControlID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetStatusByTaskControlID", xmlDoc);
				int status = 0;
				if (dt.Rows.Count != 0)
				{
					status = (int) dt.Rows[0]["TaskStatusID"];
				}
				return status;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}
		}

		public static DataTable GetPoliciesFromHdpolicyByCustomerNo(string CustomerNo)
		{
			DataTable dt= GetPoliciesFromHdpolicyByCustomerNoDB(CustomerNo);

			return dt;
		}

		public static DataTable GetPoliciesFromHdpolicyByHdpolicyID(int hdpolicyID)
		{
			DataTable dt= GetPoliciesFromHdpolicyByHdpolicyIDDB(hdpolicyID);

			return dt;
		}

		public virtual void Validate()
		{
			string errorMessage = String.Empty;

			if (this.TaskControlTypeID == 0)
				errorMessage = "Task Control Type is missing or wrong.";
			else
//				if (this.ProspectID == 0)
//				errorMessage = "ProspectID is missing or wrong.";

			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}
		}

		public static DataTable GetCompanyDealerByCompanyDealerID(string BankID, string DealerID)
		{
			DataTable dtDealer = GetFdealerByBankIDAndDealerIDDB(BankID,DealerID);
			DataTable dt;

			if (dtDealer.Rows.Count == 0)
			{
				//throw the exception.
				throw new Exception("The Comapny Dealer Code was not found in our database according to Bank Code and Dealer Code. Please try again.");
			}
			else
			{
				dt = GetCompanyDealerByCompanyDealerIDDB(dtDealer.Rows[0]["CompanyDealerID"].ToString());
				if (dt.Rows.Count == 0)
				{
					//throw the exception.
					throw new Exception("The Comapny Dealer Code was not found in our database according to Bank Code and Dealer Code. Please try again.");
				}
			}
			return dt;
		}

		public void SaveStatusInTaskControl()
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TaskStatusID",
				SqlDbType.Int, 0, this.TaskStatusID.ToString(),
				ref cookItems);

			try
			{
				Executor.BeginTrans();
				Executor.Update("SaveStatusInTaskControl",DbRequestXmlCooker.Cook(cookItems));
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to change the Declined status. "+xcp.Message,xcp);
			}
		}

        public static DataTable GetImageLogoByAgentID(string AgentID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("AgentID",
                SqlDbType.VarChar, 10, AgentID.ToString(),
                ref cookItems);


            DBRequest exec = new DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetAgencyLogoByAgentID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

        public static DataTable GetAgentByAgentID(string AgentID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("AgentID",
                SqlDbType.VarChar, 10, AgentID.ToString(),
                ref cookItems);


            DBRequest exec = new DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetAgentByAgentID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

		#endregion

		#region Private Methods

		private static bool UpdateTaskControlProspectID(int prospectID, int taskControlID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, prospectID.ToString(),
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
				exec.Update("UpdateTaskControlProspectID", xmlDoc);
				return true;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not save the prospectid in task control.", ex);
			}			
		}

		private static DataTable GetCustomerNumberByProspectID(int prospectID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, prospectID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetCustomerNumberByProspectID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
		}

		private static DataTable GetFdealerByBankIDAndDealerIDDB(string BankID, string FdealerID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("BankID",
				SqlDbType.Char, 3, BankID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("FdealerID",
				SqlDbType.Char, 3, FdealerID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetFdealerByBankIDAndDealerID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
		}

		private static DataTable GetCompanyDealerByCompanyDealerIDDB(string CompanyDealerID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.Char, 3, CompanyDealerID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetCompanyDealerByCompanyDealerID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}

		}

		private static DataTable GetTaskControlByTaskControlIDInTableDB(string taskControlID,int UserID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.VarChar, 10, taskControlID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("UserID",
				SqlDbType.Int, 0, UserID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetTaskControlByTaskControlInTableID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}		
	
		}


		public static DataTable GetTaskControlByTaskControlIDDB(int taskControlID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskControlID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetTaskControlByTaskControlID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}		

		}

		public static DataTable GetTaskControlByProspectIDDB(int prospectID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, prospectID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetTaskControlByProspectID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}
		}

		public static DataTable GetTaskControlByCustomerNoDB(string CustomerNo, int UserID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("CustomerNo",
				SqlDbType.Char, 10, CustomerNo,
				ref cookItems);
            DbRequestXmlCooker.AttachCookItem("UserID",
                SqlDbType.Int, 0, UserID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetTaskControlByCustomerNo", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve customer by criteria.", ex);
			}
		}
		
			public static DataTable GetPoliciesFromHdpolicyByCustomerNoDB(string CustomerNo)
			{
				DbRequestXmlCookRequestItem[] cookItems = 
					new DbRequestXmlCookRequestItem[1];

				DbRequestXmlCooker.AttachCookItem("CustomerNo",
					SqlDbType.Char, 10, CustomerNo,
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
				DataTable dt = null;
				try
				{
					dt = exec.GetQuery("GetPoliciesFromHdpolicyByCustomerNo", xmlDoc);
					return dt;
				}
				catch(Exception ex)
				{
					throw new Exception("Could not retrieve customer by criteria.", ex);
				}
			}


		public static DataTable GetPoliciesFromHdpolicyByHdpolicyIDDB(int hdpolicyID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

            
			DbRequestXmlCooker.AttachCookItem("HdpolicyID",
				SqlDbType.Int, 0, hdpolicyID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetPoliciesFromHdpolicyByHdpolicyID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve customer by criteria.", ex);
			}
		}

		public static DataTable GetPoliciesFromHdpolicyByPolicyNoDB(string PolClassID, string poltype, string polino, string certno)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[4];

			DbRequestXmlCooker.AttachCookItem("PolClass",
				SqlDbType.Char, 1, PolClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Poltype",
				SqlDbType.Char, 3, poltype.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Polino",
				SqlDbType.Char, 11, polino.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Certno",
				SqlDbType.Char, 10, certno.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetPoliciesFromHdpolicyByPolicyNo", xmlDoc);

				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve policy (in Hdpolicy DataBases).", ex);
			}
		}

		public static DataTable GetPoliciesFromHdpolicyByPolicyNoDB(string PolClassID, string poltype, string polino, string certno, string sufijo)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[5];

			DbRequestXmlCooker.AttachCookItem("PolClass",
				SqlDbType.Char, 1, PolClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Poltype",
				SqlDbType.Char, 3, poltype.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Polino",
				SqlDbType.Char, 11, polino.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Certno",
				SqlDbType.Char, 10, certno.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Sufijo",
				SqlDbType.Char, 2, sufijo.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetPoliciesFromHdpolicyByPolicyNo2", xmlDoc);

				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve policy (in Hdpolicy DataBases).", ex);
			}
		}

		public static DataTable GetPoliciesFromHdpolicyByLoanNoDB(string PolClassID, string loanNo)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("PolClass",
				SqlDbType.Char, 1, PolClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LoanNO",
				SqlDbType.Char, 15, loanNo,
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetPoliciesFromHdpolicyByLoanNo", xmlDoc);

				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve policy (in Hdpolicy DB) by criteria.", ex);
			}
		}

		private static DataTable  GetTaskControlByCriteriaDB(string taskControlTypeID, string taskStatusID, string agentID, string bankID,  string BegDate, string EndDate, string DateType, int UserID)

		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[8];

			DbRequestXmlCooker.AttachCookItem("TaskControlTypeID",
				SqlDbType.VarChar, 10, taskControlTypeID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("TaskStatusID",
				SqlDbType.VarChar, 10, taskStatusID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Agent",
				SqlDbType.VarChar, 3, agentID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Bank",
				SqlDbType.VarChar, 3, bankID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("BegDate",
				SqlDbType.VarChar, 10, BegDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EndDate",
				SqlDbType.VarChar, 10, EndDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("DateType",
				SqlDbType.VarChar, 1, DateType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("UserID",
				SqlDbType.Int, 0, UserID.ToString(),
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
			DataTable dt = null;
			try
			{
				dt = exec.GetQuery("GetTaskControlByCriteria", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
		}

		private void SaveTaskControl() //TEMP!!!
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (this.Mode)
				{
					case 1:  //ADD
						this.TaskControlID = 
							Executor.Insert(
							"AddTaskControl",
							this.GetInsertTaskControlXml());
						break;

					case 3:  //DELETE
						Executor.Update("DeleteTaskControl",
							this.GetDeleteTaskControlXml());
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						Executor.Update("UpdateTaskControl",
							this.GetUpdateTaskControlXml());
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception(
				"Error while trying to save this Task Control. " + 
					xcp.Message,xcp);
			}
		}

		private void SaveTaskControl(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (this.Mode)
				{
					case 1:  //ADD
						this.TaskControlID = 
							Executor.Insert("AddTaskControl",this.GetInsertTaskControlXml());
//						this.AuditInsert(UserID);
//						this.CommitAudit();
						this.Mode = 2;
						break;

					case 3:  //DELETE
						Executor.Update("DeleteTaskControl",this.GetDeleteTaskControlXml());
//						this.AuditDelete(UserID);
//						this.CommitAudit();
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
//						this.AuditUpdate(UserID);
						Executor.Update("UpdateTaskControl",this.GetUpdateTaskControlXml());
//						this.CommitAudit();
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save this Task Control. "+xcp.Message,xcp);
			}
		}

		private XmlDocument GetDeleteTaskControlXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("policyID",
				SqlDbType.Int, 0, PolicyID.ToString(),
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

		private XmlDocument GetInsertTaskControlXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[18];

			DbRequestXmlCooker.AttachCookItem("TaskControlTypeID",
				SqlDbType.Int, 0, this.TaskControlTypeID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("TaskStatusID",
				SqlDbType.Int, 0, this.TaskStatusID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, this.ProspectID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CustomerNo",
				SqlDbType.Char, 10, this.CustomerNo.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("PolicyID",
				SqlDbType.Int, 0, this.PolicyID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Agency",
				SqlDbType.Char, 3, this.Agency.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Agent",
				SqlDbType.Char, 3, this.Agent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 3, this.SupplierID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Bank",
				SqlDbType.Char, 3, this.Bank.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompany",
				SqlDbType.Char, 3, this.InsuranceCompany.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Dealer",
				SqlDbType.Char, 3, this.Dealer.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CompanyDealer",
				SqlDbType.Char, 3, this.CompanyDealer.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EntryDate",
				SqlDbType.DateTime, 0, this.EntryDate.ToString(),
				ref cookItems);
			
			if(this.CloseDate =="")
			{
				DbRequestXmlCooker.AttachCookItem("CloseDate",
					SqlDbType.DateTime, 0, this.CloseDate.ToString(),
					ref cookItems);
			}
			else
			{
				DateTime date = DateTime.Parse(this.CloseDate+" 12:01:00 AM");

				DbRequestXmlCooker.AttachCookItem("CloseDate",
					SqlDbType.DateTime, 0, date.ToString(),
					ref cookItems);
			}

			DbRequestXmlCooker.AttachCookItem("EnteredBy",
				SqlDbType.Char, 30, this.EnteredBy.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AgentCode",
                SqlDbType.VarChar, 50, this.AgentCode.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AgentDesc",
                SqlDbType.VarChar, 50, this.AgentDesc.ToString(),
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

		private XmlDocument GetUpdateTaskControlXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[19];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("TaskControlTypeID",
				SqlDbType.Int, 0, this.TaskControlTypeID.ToString(),
				ref cookItems);
		
			DbRequestXmlCooker.AttachCookItem("TaskStatusID",
				SqlDbType.Int, 0, this.TaskStatusID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("ProspectID",
				SqlDbType.Int, 0, this.ProspectID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CustomerNo",
				SqlDbType.Char,10, this.CustomerNo.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("PolicyID",
				SqlDbType.Int, 0, this.PolicyID.ToString(),
				ref cookItems);
						
			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Agency",
				SqlDbType.Char, 3, this.Agency.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Agent",
				SqlDbType.Char, 3, this.Agent.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SupplierID",
				SqlDbType.Char, 3, this.SupplierID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Bank",
				SqlDbType.Char, 3, this.Bank.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompany",
				SqlDbType.Char, 3, this.InsuranceCompany.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Dealer",
				SqlDbType.Char, 3, this.Dealer.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CompanyDealer",
				SqlDbType.Char, 3, this.CompanyDealer.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EntryDate",
				SqlDbType.DateTime, 0, this.EntryDate.ToString(),
				ref cookItems);
			
			if(this.CloseDate =="")
			{
				DbRequestXmlCooker.AttachCookItem("CloseDate",
					SqlDbType.DateTime, 0, this.CloseDate.ToString(),
					ref cookItems);
			}
			else
			{
				DateTime date = DateTime.Parse(this.CloseDate+" 12:01:00 AM");

				DbRequestXmlCooker.AttachCookItem("CloseDate",
					SqlDbType.DateTime, 0, date.ToString(),
					ref cookItems);
			}

			DbRequestXmlCooker.AttachCookItem("EnteredBy",
				SqlDbType.Char, 30, this.EnteredBy.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AgentCode",
                SqlDbType.VarChar, 50, this.AgentCode.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AgentDesc",
                SqlDbType.VarChar, 50, this.AgentDesc.ToString(),
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

        public static DataTable GetPolicyInfoByPolicyNo(string PolicyNo)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("PolicyNo",
                SqlDbType.Char, 11, PolicyNo,
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetPolicyInfo", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve customer by criteria.", ex);
            }
        }



		private static TaskControl FillProperties(TaskControl taskControl, int UserID)
		{
			taskControl.TaskControlID       = (taskControl._dtTaskControl.Rows[0]["TaskControlID"]!=System.DBNull.Value) ? (int) taskControl._dtTaskControl.Rows[0]["TaskControlID"]:0;
			taskControl.TaskControlTypeID   = (taskControl._dtTaskControl.Rows[0]["TaskControlTypeID"]!=System.DBNull.Value) ? (int) taskControl._dtTaskControl.Rows[0]["TaskControlTypeID"]:0;
			taskControl.TaskStatusID        = (taskControl._dtTaskControl.Rows[0]["TaskStatusID"]!=System.DBNull.Value) ? (int) taskControl._dtTaskControl.Rows[0]["TaskStatusID"]:0;
			taskControl.ProspectID			= (taskControl._dtTaskControl.Rows[0]["ProspectID"]!=System.DBNull.Value) ? (int) taskControl._dtTaskControl.Rows[0]["ProspectID"]:0;
			taskControl.CustomerNo			= taskControl._dtTaskControl.Rows[0]["CustomerNo"].ToString().Trim();
			taskControl.PolicyID			= (taskControl._dtTaskControl.Rows[0]["PolicyID"]!=System.DBNull.Value) ? (int) taskControl._dtTaskControl.Rows[0]["PolicyID"]:0;
			taskControl.PolicyClassID       = (taskControl._dtTaskControl.Rows[0]["PolicyClassID"]!=System.DBNull.Value) ? (int) taskControl._dtTaskControl.Rows[0]["PolicyClassID"]:0;
			taskControl.Bank				= taskControl._dtTaskControl.Rows[0]["Bank"].ToString();
			taskControl.InsuranceCompany	= taskControl._dtTaskControl.Rows[0]["InsuranceCompany"].ToString();
			taskControl.Agency				= taskControl._dtTaskControl.Rows[0]["Agency"].ToString();
			taskControl.Agent				= taskControl._dtTaskControl.Rows[0]["Agent"].ToString();
			taskControl.SupplierID			= taskControl._dtTaskControl.Rows[0]["SupplierID"].ToString();
			taskControl.Dealer				= taskControl._dtTaskControl.Rows[0]["Dealer"].ToString();
			taskControl.CompanyDealer		= taskControl._dtTaskControl.Rows[0]["CompanyDealer"].ToString();
			taskControl.EntryDate			= ((DateTime) taskControl._dtTaskControl.Rows[0]["EntryDate"]);
			taskControl.CloseDate			= (taskControl._dtTaskControl.Rows[0]["CloseDate"]!= System.DBNull.Value)? ((DateTime) taskControl._dtTaskControl.Rows[0]["CloseDate"]).ToShortDateString():"";
			taskControl.Aging				= (int) taskControl._dtTaskControl.Rows[0]["Aging"];
			taskControl.EnteredBy			= taskControl._dtTaskControl.Rows[0]["EnteredBy"].ToString();
            taskControl.AgentCode = taskControl._dtTaskControl.Rows[0]["AgentCode"].ToString();
            taskControl.AgentDesc = taskControl._dtTaskControl.Rows[0]["AgentDesc"].ToString();
			

//			if (taskControl.CompanyDealer != "" && taskControl.PolicyClassID == 10)
//			{
//				LookupTables.CompanyDealer companyDealer = new LookupTables.CompanyDealer();
//				taskControl.CompanyDealerObject = companyDealer.GetCompanyDealer(taskControl.CompanyDealer);
//			}

			// Verifica si hay customerNo.
			if (taskControl.CustomerNo == "")
			{
				if (taskControl.ProspectID != 0)
				{
					DataTable dt = GetCustomerNumberByProspectID(taskControl.ProspectID);
			
					if (dt.Rows.Count !=0 )
					{
						taskControl.CustomerNo = dt.Rows[0]["CustomerNo"].ToString();
					}
				}
			}

			if(taskControl.CustomerNo!="")
			{
				taskControl.Customer = EPolicy.Customer.Customer.GetCustomer(
					taskControl.CustomerNo, UserID);
			}
			else
			{
				if(taskControl.ProspectID!=0)
				{
					Customer.Prospect prospect = new Customer.Prospect();
					taskControl.Prospect = prospect.GetProspect(taskControl.ProspectID);
				}
			}
			
			return taskControl;
		}

//		#region Auditing
//
//		private void InitializeAuditItem()
//		{
//			if(this._auditItem == null)
//			{
//				this._auditItem = new AuditItem();
//			}
//		}
//
//		private void AuditInsert(int UserID)
//		{
//			AuditItem auditItem = new AuditItem();
//			Action action = new Action();
//			
//			action.ActionTypeID = 1; //INSERT
//			
//			action.AddKeyToKeyChain("TaskControlID",
//				this.TaskControlID.ToString());
//			
//			this.AddMutations(ref action, this, true);
//			
//			auditItem.UserID = UserID;
//
//			auditItem.NoteText = "TaskControl.TaskControl.Save() (INSERT)";
//			auditItem.AddAction(action);
//			this._auditItem = auditItem;
//		}
//
//		private void AuditUpdate(int UserID)
//		{
//			AuditItem auditItem = new AuditItem();
//			Action action = new Action();
//			TaskControl currentTaskControl = 
//				TaskControl.GetTaskControlByTaskControlID(
//				this.TaskControlID, UserID);
//			
//			action.ActionTypeID = 2; //UPDATE
//
//			action.AddKeyToKeyChain("TaskControlID",
//				this.TaskControlID.ToString());
//            
//			this.AddMutations(ref action, 
//				currentTaskControl, false);
//			auditItem.UserID = UserID;
//			auditItem.NoteText = "TaskControl.TaskControl.Save() (UPDATE)";			
//			auditItem.AddAction(action);
//			
//			this._auditItem = auditItem;
//		}
//
//		private void AuditDelete(int UserID)
//		{
//			this.InitializeAuditItem();
//
//			Action action = new Action();
//			
//			action.ActionTypeID = 3; //DELETE
//			
//			this.AddKeychains(ref action);
//
//			this.AddMutations(ref action, this, false);
//			
//			this._auditItem.AddAction(action);
//		}
//
//		private void AddKeychains(ref Action action)
//		{
//			action.AddKeyToKeyChain("TaskControlID", this.TaskControlID.ToString());
//		}
//
//		private void AddMutations(ref Action Action, 
//			TaskControl CurrentTaskControl, bool IsInsert)
//		{
//            Action.AddMutation("Agency", CurrentTaskControl.Agency.Trim(), 
//				this.Agency.Trim(), "TaskControl", IsInsert, 
//				(this.Agency.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("Agent", CurrentTaskControl.Agent.Trim(), 
//				this.Agent.Trim(), "TaskControl", IsInsert, 
//				(this.Agent.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("Bank", CurrentTaskControl.Bank.Trim(), 
//				this.Bank.Trim(), "TaskControl", IsInsert, 
//				(this.Bank.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("CloseDate", 
//				this.SafeConvertToDateTime(CurrentTaskControl.CloseDate.Trim()), 
//				this.SafeConvertToDateTime(this.CloseDate.Trim()), "TaskControl",
//				IsInsert, (this.CloseDate.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("CustomerNo", CurrentTaskControl.CustomerNo.Trim(), 
//				this.CustomerNo.Trim(), "TaskControl", IsInsert, 
//				(this.CustomerNo.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("Dealer", CurrentTaskControl.Dealer.Trim(), 
//				this.Dealer.Trim(), "TaskControl", IsInsert, 
//				(this.Dealer.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("InsuranceCompany", CurrentTaskControl.InsuranceCompany.Trim(), 
//				this.InsuranceCompany.Trim(), "TaskControl", IsInsert, 
//				(this.InsuranceCompany.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("CompanyDealer",CurrentTaskControl.CompanyDealer.Trim(), 
//				this.CompanyDealer.Trim(), "TaskControl", IsInsert, 
//				(this.CompanyDealer.Trim() == string.Empty) ? true : false);
//			Action.AddMutation("PolicyClassID", CurrentTaskControl.PolicyClassID, 
//				this.PolicyClassID, "TaskControl", IsInsert, 
//				(this.PolicyClassID == 0) ? true : false);
////			Action.AddMutation("PolicyID", CurrentTaskControl.PolicyID, 
////				this.PolicyID, "TaskControl", IsInsert, 
////				(this.PolicyID == 0) ? true : false);
//			Action.AddMutation("ProspectID", CurrentTaskControl.ProspectID, 
//				this.ProspectID, "TaskControl", IsInsert, 
//				(this.ProspectID == 0) ? true : false);
//			Action.AddMutation("TaskControlID", CurrentTaskControl.TaskControlID, 
//				this.TaskControlID, "TaskControl", IsInsert, 
//				(this.TaskControlID == 0) ? true : false);
//			Action.AddMutation("TaskControlTypeID", CurrentTaskControl.TaskControlTypeID, 
//				this.TaskControlTypeID, "TaskControl", IsInsert, 
//				(this.TaskControlTypeID == 0) ? true : false);
//			Action.AddMutation("TaskStatusID", CurrentTaskControl.TaskStatusID, 
//				this.TaskStatusID, "TaskControl", IsInsert, 
//				(this.TaskStatusID == 0) ? true : false);
//		}
//
//		private void CommitAudit()
//		{
//			this._auditItem.DateTimeStamp = DateTime.Now;
//			this._auditItem.Save();
//		}

//		private object SafeConvertToDateTime(string StringObject)
//		{
//			if(StringObject != string.Empty)
//			{
//				try	{return DateTime.Parse(StringObject);}
//				catch{/*Write to error logging sub-system.*/}
//			}
//			return StringObject;
//		}
//
//		#endregion

		#endregion
	}
}
