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
	/// Summary description for CommissionAgent.
	/// </summary>
	public class CommissionAgent
	{
		public CommissionAgent()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

		public enum Mode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Variables
    
		//private CommissionAgent  oldCommissionAgent = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  int _commissionAgentID = 0 ;
		private  int _policyClassID = 0 ;
		private  string _agentID = String.Empty;
		private  string _policyType = String.Empty;
		private  string _insuranceCompanyID = String.Empty;
		private  string _bankID = String.Empty;
		private  string _companyDealerID = String.Empty;
		private  string _commissionRate = String.Empty ;
		private  string _effectiveDate = String.Empty;
		private  string _commissionEntryDate = DateTime.Now.ToShortDateString();
		private  int _agentLevel = 0;
		private  bool _rateAmt = true;
		private  decimal _commissionAmount = 0.00m;

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

		public int CommissionAgentID
		{
			get
			{
				return this._commissionAgentID;
			}
			set
			{
				this._commissionAgentID = value;
			}
		}


		public int PolicyClassID
		{
			get
			{
				return this._policyClassID;
			}
			set
			{
				this._policyClassID = value;
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
		
		public string PolicyType
		{
			get
			{
				return this._policyType;
			}
			set
			{
				this._policyType = value;
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
        
		public string BankID
		{
			get
			{
				return this._bankID;
			}
			set
			{
				this._bankID = value;
			}
		}
        
		public string CompanyDealerID
		{
			get
			{
				return this._companyDealerID;
			}
			set
			{
				this._companyDealerID = value;
			}
		}

		public string CommissionRate
		{
			get
			{
				return this._commissionRate;
			}
			set
			{
				this._commissionRate = value;
			}
		}


		public string EffectiveDate
		{
			get
			{
				return this._effectiveDate;
			}
			set
			{
				this._effectiveDate = value;
			}
		}

		public string CommissionEntryDate
		{
			get
			{
				return this._commissionEntryDate;
			}
			set
			{
				this._commissionEntryDate = value;
			}
		}

		public int AgentLevel
		{
			get
			{
				return this._agentLevel;
			}
			set
			{
				this._agentLevel = value;
			}
		}

		public bool RateAmt
		{
			get
			{
				return this._rateAmt;
			}
			set
			{
				this._rateAmt = value;
			}
		}

		public decimal CommissionAmount
		{
			get
			{
				return this._commissionAmount;
			}
			set
			{
				this._commissionAmount = value;
			}
		}

		

		#endregion

		#region Public Methods
		      
		private string GetNextCommissionAgentID()
		{
			DataTable dt = LookupTables.GetTable("CommissionAgent");
			DataRow[] dr = dt.Select("","CommissionAgentID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["CommissionAgentID"] = dr[rec].ItemArray[0].ToString();
				myRow["PolicyClassID"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["CommissionAgentID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}
		#endregion    

		#region Public Functions

		public void Delete(int CommissionAgentID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteCommissionAgentID", 
					this.GetDeleteCommissionAgentXml(CommissionAgentID));
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
						this.CommissionAgentID = int.Parse(GetNextCommissionAgentID());
						executor.Insert("AddCommissionAgent",this.GetAddCommissionAgentXml());
						this.History(this._actionMode,UserID);
//						     this.AuditInsert(UserID);
//						      this.CommitAudit();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
//						this.AuditUpdate(UserID);
						executor.Update("UpdateCommissionAgent",this.GetUpdateCommissionAgentXml());
//							this.CommitAudit();//Commit audit;
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

		public CommissionAgent GetCommissionAgentByCommissionAgentID(int CommissionAgentID)
		{
			try
			{
				DataTable dtCommissionAgent = new DataTable();
				CommissionAgent commissionAgent = new CommissionAgent();
				this.CommissionAgentID = CommissionAgentID;
		


				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtCommissionAgent = executor.GetQuery("GetCommissionAgentByCommissionAgentID", 
					this.GetCommissionAgentByCommissionAgentIDXml());

				this.PolicyClassID = 
					int.Parse(dtCommissionAgent.Rows[0]["PolicyClassID"].ToString().Trim());

				this.AgentID = 
					dtCommissionAgent.Rows[0]["AgentID"].ToString().Trim();

				this.PolicyType = 
					dtCommissionAgent.Rows[0]["PolicyType"].ToString().Trim();

				this.InsuranceCompanyID = 
					dtCommissionAgent.Rows[0]["InsuranceCompanyID"].ToString().Trim();

				this.BankID = 
					dtCommissionAgent.Rows[0]["BankID"].ToString().Trim();

				this.CompanyDealerID = 
					dtCommissionAgent.Rows[0]["CompanyDealerID"].ToString().Trim();

				this.CommissionRate = 
					dtCommissionAgent.Rows[0]["CommissionRate"].ToString().Trim();

				this.EffectiveDate = 
					dtCommissionAgent.Rows[0]["EffectiveDate"]!= System.DBNull.Value?((DateTime) dtCommissionAgent.Rows[0]["EffectiveDate"]).ToShortDateString():"";
					//dtCommissionAgent.Rows[0]["EffectiveDate"].ToString().Trim();

				this.CommissionEntryDate = 
					dtCommissionAgent.Rows[0]["CommissionEntryDate"]!= System.DBNull.Value?((DateTime) dtCommissionAgent.Rows[0]["CommissionEntryDate"]).ToShortDateString():"";
				
				this.AgentLevel = 
					int.Parse(dtCommissionAgent.Rows[0]["AgentLevel"].ToString().Trim());
				
				this.RateAmt =
					bool.Parse(dtCommissionAgent.Rows[0]["RateAmt"].ToString());

				this.CommissionAmount = 
					decimal.Parse(dtCommissionAgent.Rows[0]["CommissionAmount"].ToString().Trim());


            return this;

			}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Commission.", ex);
			}
		}


		public static DataTable GetCommissionAgentByAgentID(string AgentID)
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				
				DataTable dt = executor.GetQuery("GetCommissionAgentByAgentID",
					GetCommissionAgentByAgentIDXml(AgentID));
						
				return dt;

			}

			catch(Exception ex)
		
			{
				throw new Exception(
					"Could not retrieve information for this Commission.", ex);
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
						throw new Exception("The Commission Agent make you are attempting to " +
							"relate to this Commission Agent does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Commission Agent.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Commission Agent.", Ex);
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
						throw new Exception("The Commission Agent you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Commission Agent before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Commission Agent.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Commission Agent.", Ex);
			}
		}
  
		private XmlDocument GetDeleteCommissionAgentXml(int CommissionAgentID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CommissionAgentID",
				SqlDbType.Int, 0, this.CommissionAgentID.ToString(),
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
        
		private XmlDocument GetAddCommissionAgentXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[12];

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.Char, 3, this.AgentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.VarChar, 20, this.PolicyType.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.VarChar, 20, this.InsuranceCompanyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("BankID",
				SqlDbType.VarChar, 14, this.BankID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.VarChar, 2, this.CompanyDealerID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CommissionRate",
				SqlDbType.VarChar, 10, this.CommissionRate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.VarChar, 12, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionEntryDate",
				SqlDbType.VarChar, 4, this.CommissionEntryDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("AgentLevel",
				SqlDbType.Int, 0, this.AgentLevel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("RateAmt",
				SqlDbType.Bit, 0, this.RateAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAmount",
				SqlDbType.Decimal, 0, this.CommissionAmount.ToString(),
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

		private XmlDocument GetUpdateCommissionAgentXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[13];
			
			DbRequestXmlCooker.AttachCookItem("CommissionAgentID",
				SqlDbType.Int, 0, this.CommissionAgentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyClassID",
				SqlDbType.Int, 0, this.PolicyClassID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.Char, 3, this.AgentID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("PolicyType",
				SqlDbType.VarChar, 20, this.PolicyType.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID",
				SqlDbType.VarChar, 20, this.InsuranceCompanyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("BankID",
				SqlDbType.VarChar, 14, this.BankID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.VarChar, 2, this.CompanyDealerID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CommissionRate",
				SqlDbType.VarChar, 10, this.CommissionRate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.VarChar, 12, this.EffectiveDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionEntryDate",
				SqlDbType.VarChar, 4, this.CommissionEntryDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("AgentLevel",
				SqlDbType.Int, 0, this.AgentLevel.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("RateAmt",
				SqlDbType.Bit, 0, this.RateAmt.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CommissionAmount",
				SqlDbType.Decimal, 0, this.CommissionAmount.ToString(),
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

		private XmlDocument GetCommissionAgentByCommissionAgentIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CommissionAgentID",
				SqlDbType.Int, 0, this.CommissionAgentID.ToString(),
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

		private static XmlDocument GetCommissionAgentByAgentIDXml(string AgentID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("AgentID",
				SqlDbType.Char, 0, AgentID.ToString(),
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


			if(this.PolicyClassID == 0)
			{
				errorMessage += "The Line of Business cannot be empty.  ";
				found = true;
			}

            if (this.EffectiveDate.Trim() == "")
            {
                errorMessage += "The effective Date cannot be empty.  ";
                found = true;
            }

			if(this.AgentLevel <= 0)
			{
				errorMessage += "The Agent level has greater than  one (1).";
				found = true;
			}


			if (found == true)
			{
				throw new Exception(errorMessage);
			}
	
		}

        public void DeleteCommissionAgentByCommissionAgentID(int CommissionAgentID)
        {
            try
            {
                Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
                executor.Delete("DeleteCommissionAgentByCommissionAgentID",
                    this.DeleteCommissionAgentByCommissionAgentIDXml(CommissionAgentID));
            }
            catch (Exception ex)
            {
                this.HandleDeleteError(ex);
            }
        }

        private XmlDocument DeleteCommissionAgentByCommissionAgentIDXml(int CommissionAgentID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("CommissionAgentID",
                SqlDbType.Int, 0, CommissionAgentID.ToString(),
                ref cookItems);

            try
            {
                return DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

        }


        #region History

        private void History(int mode, int userID)
		{
			Audit.History history = new Audit.History();
			
			if(_actionMode == 2)
			{
				EPolicy.LookupTables.CommissionAgent oldCommissionAgent = new EPolicy.LookupTables.CommissionAgent();
				oldCommissionAgent = oldCommissionAgent.GetCommissionAgentByCommissionAgentID(this.CommissionAgentID);//userID);
				
				history.BuildNotesForHistory("PolicyClassID",oldCommissionAgent.PolicyClassID.ToString(),this.PolicyClassID.ToString(),mode);
				history.BuildNotesForHistory("AgentID",oldCommissionAgent.AgentID,this.AgentID,mode);
				history.BuildNotesForHistory("PolicyType",oldCommissionAgent.PolicyType,this.PolicyType,mode);
				history.BuildNotesForHistory("InsuranceCompanyID",oldCommissionAgent.InsuranceCompanyID,this.InsuranceCompanyID,mode);
				history.BuildNotesForHistory("BankID",oldCommissionAgent.BankID,this.BankID,mode);
				history.BuildNotesForHistory("CompanyDealerID",oldCommissionAgent.CompanyDealerID,this.CompanyDealerID,mode);
				history.BuildNotesForHistory("CommissionRate",oldCommissionAgent.CommissionRate,this.CommissionRate,mode);
				history.BuildNotesForHistory("EffectiveDate",oldCommissionAgent.EffectiveDate,this.EffectiveDate,mode);
				history.BuildNotesForHistory("CommissionEntryDate",oldCommissionAgent.CommissionEntryDate,this.CommissionEntryDate,mode);
				history.BuildNotesForHistory("AgentLevel",oldCommissionAgent.AgentLevel.ToString(),this.AgentLevel.ToString(),mode);

				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("CommissionAgentID.","",this.CommissionAgentID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.CommissionAgentID.ToString();
			history.Subject = "COMMISSION AGENT";			
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
#endregion

	}


