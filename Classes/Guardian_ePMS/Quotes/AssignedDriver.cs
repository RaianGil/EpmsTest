using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Baldrich.DBRequest;

namespace EPolicy.Quotes
{    
/// <summary>
/// Copyright 2003 Baldrich &amp; Associates Inc.<br/>
/// <fileName>AssignedDriver.cs</fileName><br/>
/// <classDescription>
/// </classDescription><br/>
/// <author>B. Nieves</author><br/>
/// <modifiedBy date=""></modifiedBy><br/>
/// <version>1.0</version><br/>
/// <copyrightInfo>
/// The code included in this source file may not be used or modified in any way
/// without the written consent of Baldrich &amp; Associates Inc.
/// </copyrightInfo><br/>
/// </summary>
/// <remarks>
/// </remarks>
	
public class AssignedDriver 
{ 
#region Private Attributes
	private int _AssignedDriverID;
	private AutoDriver _AutoDriver;
	private int _AutoCoverID;
	private bool _PrincipalOperator;
	private bool _OnlyOperator;
	private int _Mode;

	//use for Policy only.
	private bool _IsPolicy = false;
#endregion

#region Public Properties
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>AssignedDriverID</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int AssignedDriverID
	{
		get
		{
			return(_AssignedDriverID);
		}
		set {
			_AssignedDriverID = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>AutoDriver</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public AutoDriver AutoDriver
	{
		get
		{
			return(_AutoDriver);
		}
		set {
			_AutoDriver = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>AutoCoverID</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int AutoCoverID
	{
		get
		{
			return(_AutoCoverID);
		}
		set {
			_AutoCoverID = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>PrincipalOperator</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public bool PrincipalOperator
	{
		get
		{
			return(_PrincipalOperator);
		}
		set {
			_PrincipalOperator = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>OnlyOperator</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public bool OnlyOperator
	{
		get
		{
			return(_OnlyOperator);
		}
		set {
			_OnlyOperator = value;
		}
	}
           
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>Mode</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int Mode
	{
		get
		{
			return(_Mode);
		}
		set 
		{
			_Mode = value;
		}
	}
           
#endregion

#region Public Methods
	/// <summary>
	///		// Test for DriverID
	///	</summary>
	/// <remarks>
	/// <functionName>Equals</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// object Obj
	/// <param name=""></param>
	/// <returns>bool</returns>

	public override bool Equals(object Obj)
	{
		try
		{
			if (!Obj.GetType().Equals(this.GetType()))
				return false;
			
			AssignedDriver AD = (AssignedDriver) Obj;
			AutoDriver Driver = AD.AutoDriver;

			if (((this.AutoCoverID != 0 && AD.AutoCoverID != 0) &&
				 this.AutoCoverID != AD.AutoCoverID) ||
				this.AutoDriver.Equals(Driver))
			return true;
		}
		catch
		{
			return false;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public void UpdateAssignedDriverByAssignedDriver(int assignedDriverID)
	{
		Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
		try
		{
			executor.BeginTrans();
			executor.Update("UpdateAutoAssignDrivers", this.UpdateAutoAssignedDriversXml(assignedDriverID));
			executor.CommitTrans();
		}
		catch (Exception xcp)
		{
			executor.RollBackTrans();
			this.HandleException(xcp);
		}
	}

	public void Save(int UserID)
	{
		if (this.Mode != (int)Enumerators.Modes.Delete)
			Insert();
	}

	
	public bool TestForOnlyOperator(int Years)
	{
		int Age =0;
		try
		{
			DateTime AdBd = DateTime.Parse(_AutoDriver.BirthDate);
			Age = DateTime.Now.Year - AdBd.Year;
			DateTime birthDayThisYear = new DateTime(DateTime.Today.Year, AdBd.Month, AdBd.Day);
			if(birthDayThisYear > DateTime.Today) //you've not yet celebrated your birthday this year
			{
				Age -= 1;
			}
		} 
		catch
		{
			return false;
		}
		if (_AutoDriver.Gender == (int)Enumerators.Gender.Female /*&&
			Commented by RPR on 2004-05-24 due to new business rule confirmed by PDP.
			_AutoDriver.MaritalStatus == (int)Enumerators.MaritalStatus.Single */&&
            (Age >= 30 && Age <=49))
			return true;
		else
			return false;
	}


	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>LoadDriversForAutoCover</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name="">int AutoCoverID</param>
	/// <returns>void</returns>

	public static ArrayList LoadDriversForAutoCover(int AutoCoverID, ArrayList Drivers, bool ispolicy)
	{
		DataTable dt = GetQuotesAssignDriversForAutoCover(AutoCoverID, ispolicy);
		ArrayList AL = new ArrayList(dt.Rows.Count);
		for(int i = 0; i < dt.Rows.Count; i++)
		{
			AssignedDriver AD = new AssignedDriver();
			AD.FillProperties(dt.Rows[i], Drivers);
			AL.Add(AD);
		}
		return AL;
	}

	public void SetIsPolicy(bool ispolicy)
	{
		this._IsPolicy = ispolicy;
	}

#endregion

#region Private Methods
	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>GetAssignedDriverXml</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>XmlDocument</returns>

	private XmlDocument UpdateAutoAssignedDriversXml(int QuotesAssignDriversID)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		XmlDocument xmlDoc = new XmlDocument();
			
		sb.Append("<parameters>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesAssignDriversID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + QuotesAssignDriversID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>OnlyOperator</name>");
		sb.Append("<type>bit</type>");
		sb.Append("<Length>1</Length>");
		sb.Append("<value>" + this.OnlyOperator + "</value>");
		sb.Append("</parameter>");

		sb.Append("</parameters>");

		xmlDoc.InnerXml = sb.ToString();

		sb = null;

		return xmlDoc;
	}

	private XmlDocument GetAssignedDriverXml()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		XmlDocument xmlDoc = new XmlDocument();
			
		sb.Append("<parameters>");

//		sb.Append("<parameter>");
//		sb.Append("<name>QuotesAssignDriversID</name>");
//		sb.Append("<type>int</type>");
//		sb.Append("<value>" + this.AutoDriver.DriverID + "</value>");
//		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesDriversID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.AutoDriver.DriverID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesAutoID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.AutoCoverID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>IsPrincipalOperator</name>");
		sb.Append("<type>bit</type>");
		sb.Append("<value>" + this.PrincipalOperator + "</value>");
		sb.Append("</parameter>");
            
		sb.Append("<parameter>");
		sb.Append("<name>OnlyOperator</name>");
		sb.Append("<type>bit</type>");
		sb.Append("<Length>1</Length>");
		sb.Append("<value>" + this.OnlyOperator + "</value>");
		sb.Append("</parameter>");

		sb.Append("</parameters>");

		xmlDoc.InnerXml = sb.ToString();

		sb = null;

		return xmlDoc;
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>Insert</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>void</returns>

	private void Insert()
	{
		Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
		try
		{
			executor.BeginTrans();

			if (this._IsPolicy)
			{
				this.AutoCoverID = executor.Insert("AddAutoAssignDrivers", this.GetAssignedDriverXml());
			}
			else
			{
				this.AutoCoverID = executor.Insert("AddQuotesAssignDrivers", this.GetAssignedDriverXml());
			}

			executor.CommitTrans();
		}
		catch (Exception xcp)
		{
			executor.RollBackTrans();
			this.HandleException(xcp);
		}
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>HandleException</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// Exception Ex
	/// <param name=""></param>
	/// <returns>void</returns>

	public void HandleException(Exception Ex)
	{
		switch(Ex.GetBaseException().GetType().FullName)
		{
			case "System.Data.SqlClient.SqlException":
				SqlException sqlException = (SqlException)Ex.GetBaseException();
			switch(sqlException.Number)
			{
				case 547:
					throw new Exception("The Driver you are trying to " +
						"relate to this search fields does not exist.", Ex);
				default:
					throw new Exception("An database server error ocurred while " +
						"adding the Driver.", Ex);
			}
			default:
				throw new Exception("An error ocurred while adding " + 
					" the Driver.", Ex);
		}
	}

	private static DataTable GetQuotesAssignDriversForAutoCover(int AutoCoverID, bool ispolicy)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		XmlDocument xmlDoc = new XmlDocument();

		sb.Append("<parameters>");
		sb.Append("<parameter>");
		sb.Append("<name>QuotesAutoID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + AutoCoverID + "</value>");
		sb.Append("</parameter>");
		sb.Append("</parameters>");
		xmlDoc.InnerXml = sb.ToString();
		sb = null;

		Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

		DataTable dt ;
		if(ispolicy)
		{
			dt = exec.GetQuery("GetAutoAssignDriversByQuotesAutoID",xmlDoc);
		}
		else
		{
			dt = exec.GetQuery("GetQuotesAssignDriversByQuotesAutoID",xmlDoc);
		}

		return dt;
	}

	private void FillProperties(DataRow DR, ArrayList Drivers)
	{
		this._AssignedDriverID = (int)DR["QuotesAssignDriversID"];
		this._AutoCoverID = (int)DR["QuotesAutoID"];
		this._PrincipalOperator = (bool)DR["IsPrincipalOperator"];
		this._OnlyOperator = (bool)DR["OnlyOperator"];

		// DRIVER INFORMATION
	//	this._AutoDriver = AutoDriver.GetAutoDriver((int)DR["QuotesDriversID"]);
		EPolicy.Quotes.AutoDriver S = new EPolicy.Quotes.AutoDriver();
		S.DriverID = (int)DR["QuotesDriversID"];
		int t = Drivers.IndexOf(S);
		if(t >= 0)
		{
			this._AutoDriver = (AutoDriver)Drivers[t];
		}
	}


#endregion

}
// END CLASS DEFINITION AssignedDriver
}