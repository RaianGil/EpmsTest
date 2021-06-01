using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Baldrich.DBRequest;
using EPolicy.Quotes;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{

public class QuoteAuto 	: Quote
{ 
	public QuoteAuto(bool isPolicy)
	{
		this.IsPolicy = isPolicy;

		this.PolicyClassID	  = 3;  //Auto Personal
		this.PolicyID         = 0;
		this.InsuranceCompany = "001";
		this.Agency           = "001";
		this.Agent            = "001";
		this.Bank			  = "000";
		this.Dealer			  = "000";
		this.CompanyDealer	  = "000";
		this.Term			  = 12;

		if (this.IsPolicy)   //Policy
		{
			this.Policy.DepartmentID = 1;  //Policy
			this.TaskControlTypeID   = int.Parse(LookupTables.LookupTables.GetID("TaskControlType","Auto Personal Policy"));
            this.Policy.AgentList    = Policy.GetAgentListByPolicyClassID(this.PolicyClassID);
			this.Policy.Suffix       = "00";
			this.Policy.Bank	     = "000";
			this.Policy.CompanyDealer= "000";
			this.TaskStatusID     = 18;  //Open //int.Parse(LookupTables.LookupTables.GetID("TaskStatus","Open"));
		}
		else				// Quote
		{
			this.TaskStatusID     = 13;  //Open //int.Parse(LookupTables.LookupTables.GetID("TaskStatus","Open"));
			this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType","Quote Auto"));
		}
	}

#region Private Attributes
	private System.Collections.ArrayList _AutoCovers;
	private System.Collections.ArrayList _Drivers;
	private int tmpAutoCoverCount=1;
	private int tmpDriverCount=1;
	private int _VehicleCount = 0;
#endregion

#region Public Properties	

	
		public int VehicleCount
		{
			get
			{
				return(_VehicleCount); 
			}
			set 
			{
				_VehicleCount = value;
			}
		}

	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>AutoCovers</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public System.Collections.ArrayList AutoCovers
	{
		get
		{
			if (_AutoCovers == null)
				_AutoCovers = new System.Collections.ArrayList();
			return(_AutoCovers);
		}
		set {
			_AutoCovers = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>Drivers</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public System.Collections.ArrayList Drivers
	{
		get
		{
			if (_Drivers == null)
				_Drivers = new System.Collections.ArrayList();
			return(_Drivers); 
		}
		set {
			_Drivers = value;
		}
	}
#endregion

    
#region Public Methods
	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>AddAutoCover</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// AutoCover Cover
	/// <param name=""></param>
	/// <returns>void</returns>
	/// 
	
	public bool ReadyForCalculation()
	{				
		if(base.EffectiveDate.Trim() != "")
		{	
			if(this.HasCarsAndDrivers(this))
			{
				if(this.AllCoversHaveAnAssignedDriver(this))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}

	public bool HasCarsAndDrivers(QuoteAuto QuoteAuto)
	{
		if(QuoteAuto.Drivers.Count < 1 || 
			QuoteAuto.AutoCovers.Count < 1)
			return false;
			
		return true;
	}

	public bool AllCoversHaveAnAssignedDriver(
		QuoteAuto QuoteAuto)
	{
		foreach (Quotes.AutoCover autoCover in 
			QuoteAuto.AutoCovers)
		{
			if(autoCover.AssignedDrivers.Count < 1)
			{
				return false;
			}
		}

		return true;
	}

	public int GetNewInternalID()
	{
		if(this._AutoCovers.Count == 0)
		{
			return 1;
		}
		else
		{
			int[] currentIDs = new int[this._AutoCovers.Count];
			int i = 0;
		
			foreach(AutoCover cover in this._AutoCovers)
			{
				currentIDs[i] = cover.InternalID;
				i++;
			}

			Array.Sort(currentIDs);
			return (currentIDs[currentIDs.GetUpperBound(0)] + 1);
		}
	}

	public int GetNewDriverInternalID()
	{
		if(this._Drivers.Count == 0)
		{
			return 1;
		}
		else
		{
			int[] currentIDs = new int[this._Drivers.Count];
			int i = 0;
		
			foreach(AutoDriver driver in this._Drivers)
			{
				currentIDs[i] = driver.InternalID;
				i++;
			}

			Array.Sort(currentIDs);
			return (currentIDs[currentIDs.GetUpperBound(0)] + 1);
		}
	}

	public void AddAutoCover(AutoCover Cover, bool Update)
	{
		if (_AutoCovers == null)
			_AutoCovers = new System.Collections.ArrayList();
		
		if(!_AutoCovers.Contains(Cover) && Update)
		{
			_AutoCovers.Add(Cover);
		}
		else if(!_AutoCovers.Contains(Cover))
		{
			Cover.InternalID = this.GetNewInternalID();			

			_AutoCovers.Add(Cover);
		}
	}

	public void AddDriver(AutoDriver Driver)
	{
		if (_Drivers == null)
			_Drivers = new System.Collections.ArrayList();
		if(!_Drivers.Contains(Driver)) 
		{
			Driver.InternalID = this.GetNewDriverInternalID();
			_Drivers.Add(Driver);
		}
	}

	public void OverridePremium(int quoteAutoID, int BCID, int anniversary,string value, decimal TotalCover, decimal newTotPrem)
	{
		DbRequestXmlCookRequestItem[] cookItems = 
			new DbRequestXmlCookRequestItem[4];

		DbRequestXmlCooker.AttachCookItem("QuotesAutoID",
			SqlDbType.Int, 0, quoteAutoID.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("BreakdownCoversTypeID",
			SqlDbType.Int, 0, BCID.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("BreakdownAnniversary",
			SqlDbType.Int, 0, anniversary.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("BreakdownValue",
			SqlDbType.VarChar, 20, value.ToString(),
			ref cookItems);

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
		exec.Update("UpdateOverridePremium",xmlDoc);

		if (anniversary == 1)  //Solamente pasa una sola vez para que no vuelva a salvar lo mismo.
		{
			UpdateAutoPolicyOvrrPremium(quoteAutoID,BCID, TotalCover);
			UpdatePolicyOvrrPremium(TaskControlID,newTotPrem);
		}
	}

	private void UpdateAutoPolicyOvrrPremium(int quoteAutoID, int BCID, decimal TotalCover)
	{
		//Busco el tipo de cubierta para poder actualizar en la tabla de autoCover el campo designado.
		string BCType = LookupTables.LookupTables.GetDescription("BreakdownCoversType",BCID.ToString());
		if (BCType.Trim() != "VehicleRental")
		{
			BCType = BCType.Trim()+"Premium";
		}

		DbRequestXmlCookRequestItem[] cookItems = 
			new DbRequestXmlCookRequestItem[4];

		DbRequestXmlCooker.AttachCookItem("QuotesAutoID",
			SqlDbType.Int, 0, quoteAutoID.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("OvrrPremium",
			SqlDbType.Bit, 0, true.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("BCType",
			SqlDbType.Char, 100, BCType.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("PremiumCover",
			SqlDbType.Money, 0, TotalCover.ToString(),
			ref cookItems);

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

		exec.Update("UpdateAutoPolicyOvrrPremium",xmlDoc);
	}

	private void UpdatePolicyOvrrPremium(int taskControlID, decimal newTotPrem)
	{
		double newprem = Double.Parse(newTotPrem.ToString());

		DbRequestXmlCookRequestItem[] cookItems = 
			new DbRequestXmlCookRequestItem[2];

		DbRequestXmlCooker.AttachCookItem("TaskControlID",
			SqlDbType.Int, 0, taskControlID.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("TotalPremium",
			SqlDbType.Money, 0, newTotPrem.ToString(),
			ref cookItems);

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

		exec.Update("UpdatePolicyOvrrPremium",xmlDoc);
	}

	public void Calculate()
	{
		if(this.ReadyForCalculation())
		{
			decimal ttl = 0.0M, chrg = 0.0M;
			if (_AutoCovers != null)
			{
				for (int i = 0; i < _AutoCovers.Count; i++)
				{
					AutoCover AC = (AutoCover)_AutoCovers[i];

					// Si hubo un overridePremium no recalcula la prima.
					if(!AC.OvrrPremium)
					{
						AC.Term = base.Term;
						AC.EffectiveDate = base.EffectiveDate;
						AC.isSecondVehicle = (/*_AutoCovers.Count*/ i > 0); 
						AC.Breakdown = new System.Collections.ArrayList();
						AC.Calculate(this.IsPolicy);
						ttl += AC.TotalAmount;
						chrg += AC.Charge;
					}
					else
					{
						AC = LoadBreakdownAfterOvrridePremium(AC);

						ttl += AC.TotalAmount;
						chrg += AC.Charge;
					}
				}
			}
			this.TotalPremium = (decimal)ttl;
			this.Charge = (decimal)chrg;
		}
		else
		{
			this.TotalPremium = 0;
			this.Charge = 0;

			foreach(AutoCover autoCover in this.AutoCovers)
			{
				autoCover.TotalAmount = 0;
				autoCover.Charge = 0;
				autoCover.Breakdown = null;
			}
		}
	}

	public AutoCover LoadBreakdownAfterOvrridePremium(AutoCover AC)
	{
        DataTable dt = CoverBreakdown.GetQuotesBreakdownCoverForAutoCover(AC.QuotesAutoId, this.IsPolicy);					
		Enumerators.Premiums enumPrem;
		int enumPremInt = 0;
		int anniversary = 0;
		decimal mvalue;

		for (int a = 0; a <= dt.Rows.Count-1; a++)
		{
			enumPrem = (Enumerators.Premiums)((int) dt.Rows[a]["BreakdownCoversTypeID"]);
			if((int)enumPrem != enumPremInt)
			{
				anniversary = 0;
			}
			else
			{
				anniversary=anniversary+1;
			}

			if((int) dt.Rows[a]["BreakdownCoversTypeID"]==4)
			{
				AC.AddCoverBreakdown(enumPrem,anniversary, dt.Rows[a]["BreakdownValue"].ToString());
			}
			else
			{
				mvalue = decimal.Parse(dt.Rows[a]["BreakdownValue"].ToString());
				AC.AddCoverBreakdown(enumPrem,anniversary, mvalue);
			}

			enumPremInt = (int) enumPrem;
		}

		AutoCoverCalculator ACC = new AutoCoverCalculator();
		ACC.SetIsPolicy(true);

		AC = ACC.SetTotal(AC);	

		this.TotalPremium = AC.TotalAmount;
		this.Charge = AC.Charge;

		return AC;
	}


	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>GetAutoCover</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// AutoCover AC
	/// <param name=""></param>
	/// <returns>AutoCover</returns>

	public AutoCover GetAutoCover(AutoCover AC)
	{
		AutoCover retVal = null;
		int t = _AutoCovers.IndexOf(AC);
		if(t >= 0)
		{
			retVal = (AutoCover)_AutoCovers[t];
		}
		return retVal;
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>GetDriver</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// AutoDriver AD
	/// <param name=""></param>
	/// <returns>AutoDriver</returns>

	public AutoDriver GetDriver(AutoDriver AD)
	{
		int t = _Drivers.IndexOf(AD);
		if(t >= 0){
			return (AutoDriver)_Drivers[t];
		}
		return AD;
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>RemoveAutoCover</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// AutoCover Cover
	/// <param name=""></param>
	/// <returns>void</returns>

	public void RemoveAutoCover(AutoCover Cover)
	{
		if (AutoCovers.Contains(Cover))
			AutoCovers.Remove(Cover);
	}


	public void RemoveDriver(AutoDriver Driver)
	{
		if (Drivers.Contains(Driver))
			Drivers.Remove(Driver);
	}

 
	public void Save(int UserID, AutoCover Cover, AutoDriver Driver, bool SaveAll)
	{	
		if (IsPolicy && !SaveAll)	// For Policy
		{
			base.SavePolicyFromQuote(UserID);
		}
		
		// Calculate Premium before save.
		this.Calculate(); 

		if (!IsPolicy)				//For Quote
		{
			base.Save(UserID);
		}

		// Save Drivers
		if (_Drivers != null)
		{
			if(Driver != null)
			{
				if(this._Drivers.IndexOf(Driver) != -1)
				{
					Driver = (AutoDriver)this._Drivers[this._Drivers.IndexOf(Driver)];
					Driver.QuoteID = this.QuoteId;

					if(this.IsPolicy)					
					{
						Driver.SetIsPolicy(true);
					}
					
					Driver._TCIDForAutoDriver = this.TaskControlID;  //Se usa para el history de AutoDriver.
					Driver.Save(UserID, false, 0);
					
					//Victor
					if(!this.IsPolicy)
					{
						if (this._Drivers.Count == 1)
						{
							this.Prospect.ProspectID = Driver.ProspectID;
							this.ProspectID		     = Driver.ProspectID;
						}
					}

					if(this.IsPolicy)					
					{
						//Actualiza el campo de modify en la tabla de Policy cuando ya haya un TaskControlID.
						if(this.Policy.TaskControlID != 0)
						{
							this.Policy.UpdatePolicyModifyField();
						}
					}
				}
			}
			else if(SaveAll == true)
			{
				for (int i = 0; i < _Drivers.Count; i++)
				{
					AutoDriver AD = (AutoDriver)_Drivers[i];
					AD.QuoteID = this.QuoteId;
					if(this.IsPolicy)					
					{
						AD.SetIsPolicy(true);
					}

					AD._TCIDForAutoDriver = this.TaskControlID;  //Se usa para el history de AutoDriver.
					AD.Save(UserID, false, 0);

					//Victor
					if(!this.IsPolicy)
					{
						if (i == 0) //Primer driver (Main).
						{
							this.Prospect.ProspectID = AD.ProspectID;
							this.ProspectID			 = AD.ProspectID;
						}
					}

					if(this.IsPolicy)					
					{
						//Actualiza el campo de modify en la tabla de Policy cuando ya haya un TaskControlID.
						if(this.Policy.TaskControlID != 0)
						{
							this.Policy.UpdatePolicyModifyField();
						}
					}
				}
			}
		}

		// Save Auto Covers
		if (_AutoCovers != null)
		{
			if(Cover != null)
			{
				if(this._AutoCovers.IndexOf(Cover) != -1)
				{
					Cover = (AutoCover)this._AutoCovers[this._AutoCovers.IndexOf(Cover)];
					Cover.QuotesId = this.QuoteId;
					if(this.IsPolicy)					
					{
						Cover.SetIsPolicy(true);
					}

                    if (Cover.QuotesAutoId == 0)
                        Cover.Mode = 1;

					Cover._TCIDForAutoCover = this.TaskControlID;  //Se usa para el history de AutoCover.
					Cover.Save(UserID);

					if(this.IsPolicy)
					{
						this.Policy.AutoType = Cover.AutoPolicyType;
						//this.Policy.IsMaster = Quotes.AutoCover.IsMasterCover(Cover.PolicySubClassId);

						//Actualiza el campo de modify en la tabla de Policy cuando ya haya un TaskControlID.
						if(this.Policy.TaskControlID != 0)
						{
							this.Policy.UpdatePolicyModifyField();
						}
					}
				}
			}
			else if(SaveAll == true)
			{
				for (int i = 0; i < _AutoCovers.Count; i++)
				{
					AutoCover AC = (AutoCover)_AutoCovers[i];
					AC.QuotesId = this.QuoteId;
					if(this.IsPolicy)					
					{
						AC.SetIsPolicy(true);
					}
					AC._ModeForHistory = this.Mode;
					AC._TCIDForAutoCover = this.TaskControlID;  //Se usa para el history de AutoCover.
					
					AC.Save(UserID);

					if(this.IsPolicy)
					{
						this.Policy.AutoType = AC.AutoPolicyType;
						//this.Policy.IsMaster = Quotes.AutoCover.IsMasterCover(AC.PolicySubClassId);
						
						//Actualiza el campo de modify en la tabla de Policy cuando ya haya un TaskControlID.
						if(this.Policy.TaskControlID != 0)
						{
							this.Policy.UpdatePolicyModifyField();
						}
					}
				}
			}

			if(this.IsPolicy)
			{
				this.Policy = Policy.GetPrintPolicyReport(this.Policy);
			}
		}

		this.Calculate(); //2004-09-07

		if (IsPolicy && SaveAll)
		{
			base.Save(UserID);

			//Update quoteID in Drivers, Vehichle and Cover Breakdown.
			UpdateTableRelationToQuoteAuto();
			this.QuoteId = this.TaskControlID;  
		}
		
		if (!IsPolicy)
		{
			//Victor
			//base.Save(UserID);
		}
	}

	public void SaveAutoCover(int UserID, AutoCover Cover, AutoDriver Driver, bool SaveAll)
	{
		// Save Auto Covers
		if (_AutoCovers != null)
		{
			if(Cover != null)
			{
				if(this._AutoCovers.IndexOf(Cover) != -1)
				{
					Cover = (AutoCover)this._AutoCovers[this._AutoCovers.IndexOf(Cover)];
					Cover.QuotesId = this.QuoteId;
					if(this.IsPolicy)					
					{
						Cover.SetIsPolicy(true);
					}

					Cover._TCIDForAutoCover = this.TaskControlID;  //Se usa para el history de AutoCover.
					Cover.Save(UserID);

					if(this.IsPolicy)
					{
						this.Policy.AutoType = Cover.AutoPolicyType;
						//this.Policy.IsMaster = Quotes.AutoCover.IsMasterCover(Cover.PolicySubClassId);
					}
				}
			}
		}
	}

	public override void Save(int UserID)
	{
		this.Save(UserID, null, null, true);
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>GetQuoteAuto</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// int id
	/// <param name=""></param>
	/// <returns>QuoteAuto</returns>
	public static QuoteAuto GetQuoteAuto(int TaskControlID, int UserID, bool isPolicy)
	{
		QuoteAuto QA = new QuoteAuto(isPolicy);
		QA.IsPolicy = isPolicy;

		QA.Load(TaskControlID, UserID);
		
		if(QA.IsPolicy)
		{
			QA.Policy.InsuranceCompany = QA.InsuranceCompany;
			QA.Policy.PolicyClassID	   = QA.PolicyClassID;
			
			Quotes.AutoCover AC = null;
			AC = (AutoCover) QA.AutoCovers[0];
			QA.Policy.AutoType = AC.AutoPolicyType;

			QA.Policy = Policy.GetPolicyByTaskControlID(TaskControlID,QA.Policy);   // Get Policy

			QA.Policy.PolicyCicleEnd = 1;
			QA.QuoteId		 = QA.Policy.TaskControlID;
			QA.Customer		 = QA.Policy.Customer;
			QA.TaskControlID	 = QA.Policy.TaskControlID;
			QA.Term			 = QA.Policy.Term;
			QA.EffectiveDate   = QA.Policy.EffectiveDate;
			QA.ExpirationDate  = QA.Policy.ExpirationDate;
			//this.PolicyClassID   = this.Policy.PolicyClassID;
			
			string charge				= QA.Policy.Charge.ToString();
			string totalPremium			= QA.Policy.TotalPremium.ToString();

			QA.Charge			= decimal.Parse(charge);
			QA.TotalPremium	= decimal.Parse(totalPremium);
		}
		return QA;
	}

	private void UpdateTableRelationToQuoteAuto()
	{
		DbRequestXmlCookRequestItem[] cookItems = 
			new DbRequestXmlCookRequestItem[2];

		DbRequestXmlCooker.AttachCookItem("QuotesID",
			SqlDbType.Int, 0, this.QuoteId.ToString(),
			ref cookItems);

		DbRequestXmlCooker.AttachCookItem("TaskControlID",
			SqlDbType.Int, 0, this.TaskControlID.ToString(),
			ref cookItems);

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

		exec.Update("UpdateTableRelationToQuoteAuto",xmlDoc);
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>Load</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// int TaskControlID
	/// <param name=""></param>
	/// <returns>QuoteAuto</returns>
	private void Load(int TaskControlID, int UserID)
	{
		// Load Quote
		base.LoadFromTask(TaskControlID, UserID);

		//Load Policy to fill QuoteField
		if(this.IsPolicy)
		{
			this.Policy.PolicyCicleEnd = 1;
			this.QuoteId		 = this.TaskControlID;
			this.Customer		 = this.Policy.Customer;
			this.TaskControlID	 = this.Policy.TaskControlID;
			this.Term			 = this.Policy.Term;
			this.EffectiveDate   = this.Policy.EffectiveDate;
			this.ExpirationDate  = this.Policy.ExpirationDate;
			//this.PolicyClassID   = this.Policy.PolicyClassID;
			
			string charge				= this.Policy.Charge.ToString();
			string totalPremium			= this.Policy.TotalPremium.ToString();

			this.Charge			= decimal.Parse(charge);
			this.TotalPremium	= decimal.Parse(totalPremium);
		}
		///////////////////////////////////////////////////////////

		// Load Associated Drivers
		_Drivers = AutoDriver.LoadDriversForQuote(this.QuoteId,this.IsPolicy);
		tmpDriverCount = _Drivers.Count + 1;
		// Load Auto Covers
		_AutoCovers = AutoCover.LoadAutoCoverForQuote(this.QuoteId, _Drivers, this.IsPolicy);
		tmpAutoCoverCount = _AutoCovers.Count + 1;
		this.VehicleCount = AutoCovers.Count;
	}
#endregion
}
// END CLASS DEFINITION AutoQuote

}