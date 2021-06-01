using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Baldrich.DBRequest;
using EPolicy.Customer;
using EPolicy.Audit;

namespace EPolicy.Quotes
{    
/// <summary>
/// Copyright 2003 Baldrich &amp; Associates Inc.<br/>
/// <fileName>AutoDriver.cs</fileName><br/>
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
	
public class AutoDriver : Prospect
{ 
	public AutoDriver()
	{ 
		base.IsDriver = true;
	}
 
#region Private Attributes

	public int _TCIDForAutoDriver;
	public int _ModeForHistory;
	private AutoDriver _oldAutoDriver = null;
	//private System.Collections.ArrayList _Drivers2;
	private System.Collections.ArrayList _oldAutoDriver2;

	private int _DriverID;
	private string _BirthDate;
	private int _Gender;
	private int _MaritalStatus;
	private string _License;
	private string _SocialSecurity;
	private int _OccupationId;
	private int _QuoteID;
	private string _EmpAddr1;
	private string _EmpAddr2;
	private string _EmpCity;
	private string _EmpState;
	private string _EmpZipCode;
	private string _GoodStudent;
	private string _Training;
	private int _Mode;
	private int _InternalID;
	private bool _IsPolicy = false;
#endregion
	
#region Public Properties

	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>DriverID</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int DriverID
	{
		get
		{
			return(_DriverID);
		}
		set {
			_DriverID = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>BirthDate</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string BirthDate
	{
		get
		{
			return(_BirthDate);
		}
		set {
			_BirthDate = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>Gender</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int Gender
	{
		get
		{
			return(_Gender);
		}
		set {
			_Gender = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>MaritalStatus</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int MaritalStatus
	{
		get
		{
			return(_MaritalStatus);
		}
		set {
			_MaritalStatus = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>License</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string License
	{
		get
		{
			return(_License);
		}
		set {
			_License = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>SocialSecurity</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string SocialSecurity
	{
		get
		{
			return(_SocialSecurity);
		}
		set {
			_SocialSecurity = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>OccupationId</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int OccupationId
	{
		get
		{
			return(_OccupationId);
		}
		set {
			_OccupationId = value;
		}
	}
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>Quotes</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int QuoteID
	{
		get
		{
			return(_QuoteID);
		}
		set 
		{
			_QuoteID = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>EmpAddr1</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string EmpAddr1
	{
		get
		{
			return(_EmpAddr1);
		}
		set 
		{
			_EmpAddr1 = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>EmpAddr2</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string EmpAddr2
	{
		get
		{
			return(_EmpAddr2);
		}
		set 
		{
			_EmpAddr2 = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>EmpCity</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string EmpCity
	{
		get
		{
			return(_EmpCity);
		}
		set 
		{
			_EmpCity = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>EmpState</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string EmpState
	{
		get
		{
			return(_EmpState);
		}
		set 
		{
			_EmpState = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>EmpZipCode</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string EmpZipCode
	{
		get
		{
			return(_EmpZipCode);
		}
		set 
		{
			_EmpZipCode = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>GoodStudent</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string GoodStudent
	{
		get
		{
			return(_GoodStudent);
		}
		set 
		{
			_GoodStudent = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>Training</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public string Training
	{
		get
		{
			return(_Training);
		}
		set 
		{
			_Training = value;
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
	public new int Mode
	{
		get
		{
			return(this._Mode);
		}
		set 
		{
			this._Mode = value;
		}
	}

	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>InternalID</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int InternalID
	{
		get
		{
			return(this._InternalID);
		}
		set 
		{
			this._InternalID = value;
		}
	}
#endregion

#region Public Methods
	/// <summary>
	///		// Test for SSN
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
		// Verify if DriverID, ProspectID or SocialSecurity
		// are not '0' or null and if both are equal.  
		// if no DriverID, SocialSecurity or ProspectID 
		// use InternalID
		try
		{
			if (!Obj.GetType().Equals(this.GetType()))
				return false;
			AutoDriver ad = (AutoDriver)Obj;
			if (((this._DriverID != 0 && ad._DriverID != 0) &&
				this._DriverID == ad._DriverID) ||
				((this.ProspectID != 0 && ad.ProspectID != 0) &&
				this.ProspectID == ad.ProspectID) 
				
				//Social 'Security' is external, mutable, therefore should
				//not be part of this test.  Discovered when attempting
				//to remove existing drivers whose SSN had been
				//edited.
				//Rafael Pérez
				//2004-02-10

				/*|| (((this._SocialSecurity != 
				null && this._SocialSecurity != "") && 
				(ad._SocialSecurity != null && ad._SocialSecurity != "")) &&
				this._SocialSecurity == ad._SocialSecurity)*/)
			{
				return true;
			}
			else if ((/*this._DriverID == 0 && this.ProspectID == 0 
					   * RPR 2004-03-17 &&  */
					/*(this._SocialSecurity == null || 
					 * this._SocialSecurity == "")) && */
					//Rafael Pérez
					//2004-02-10
				     this._InternalID == ad._InternalID))
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
		return false;
	}

	/// <summary>
	///		// To implement Equals must implement GetHashCode also.

	///	</summary>
	/// <remarks>
	/// <functionName>GetHashCode</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>       
	/// <returns>int</returns>

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>Save</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>void</returns>

	public void Save(int UserID, bool IsAlreadyProspect, int ProspectID)
	{
		base.EntryDate = DateTime.Now;
		
		if (this.Mode ==2)// Se utiliza para el History
		{
			//_Drivers2 = AutoDriver.LoadDriversForQuote(this.QuoteID,this._IsPolicy);
			_oldAutoDriver2 = AutoDriver.LoadDriversForQuote(this.QuoteID, this._IsPolicy);

			for (int i = 0; i < _oldAutoDriver2.Count; i++)
			{
				_oldAutoDriver = (AutoDriver) _oldAutoDriver2[i];
				if(_oldAutoDriver.DriverID == this.DriverID)
				{
					i=_oldAutoDriver2.Count;
				}
			}			
		}
	
		if(!IsAlreadyProspect)
		{
			try
			{
				base.SaveProspect(UserID);
			}
			catch (Exception exp)
			{	//No creaba el prospecto y pusimos este catch para poder saber el error.
				throw new Exception(exp.Message.Trim());
			}
		}
		else
		{
			this.fillProspectInfo(base.GetProspect(ProspectID));
		}
		//:~

		if (Mode == (int)Enumerators.Modes.Insert)
		{
			this.Insert(UserID);
		}
		else if(Mode == (int)Enumerators.Modes.Update)
		{
			this.Update(UserID);
			this.History(this.Mode,UserID);
		}
		else if(Mode == (int)Enumerators.Modes.Delete)
			this.Delete(UserID);
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>LoadDriversForQuote</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// int QuoteAutoID
	/// <param name=""></param>
	/// <returns>ArrayList</returns>

	public static ArrayList LoadDriversForQuote(int QuotesID, bool ispolicy)
	{
		DataTable dt = GetQuotesDriversForQuote(QuotesID, ispolicy);
	
		ArrayList AL = new ArrayList(dt.Rows.Count);
		for(int i = 0; i < dt.Rows.Count; i++)
		{
			AutoDriver AD = new AutoDriver();
			AD.FillProperties(dt.Rows[i]);
			
			AD.InternalID = 1 + i; // Set Internal ID
			AL.Add(AD);
		}
		return AL;
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>GetAutoDriver</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// int DriverID
	/// <param name=""></param>
	/// <returns>AutoDriver</returns>

	public static AutoDriver GetAutoDriver(int DriverID)
	{
		DataTable DT = GetQuotesDrivers(DriverID,false);
		if (DT.Rows.Count > 0)
		{
			AutoDriver AD = new AutoDriver();
			AD.FillProperties(DT.Rows[0]);
			return AD;
		}
		else 
			return null;
	}
	
	public static AutoDriver GetAutoDriverForPolicy(int DriverID)
	{
		DataTable DT = GetQuotesDrivers(DriverID,true);
		if (DT.Rows.Count > 0)
		{
			AutoDriver AD = new AutoDriver();
			AD.FillProperties(DT.Rows[0]);
			return AD;
		}
		else 
			return null;
	}
	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>GetQuotesDriversByCriteria</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// AutoDriver AD
	/// <param name=""></param>
	/// <returns>AutoDriver</returns>

	public static DataTable GetQuotesDriversByCriteria(AutoDriver AD)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		XmlDocument xmlDoc = new XmlDocument();

		sb.Append("<parameters>");
		sb.Append("<parameter>");
		sb.Append("<name>License</name>");
		sb.Append("<type>char</type>");
		sb.Append("<length>10</length>");
		sb.Append("<value>" + AD.License + "</value>");
		sb.Append("</parameter>");
		
		sb.Append("<parameter>");
		sb.Append("<name>SocialSecurity</name>");
		sb.Append("<type>char</type>");
		sb.Append("<length>11</length>");
		sb.Append("<value>" + AD.SocialSecurity + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>FirstName</name>");
		sb.Append("<type>char</type>");
		sb.Append("<length>15</length>");
		sb.Append("<value>" + AD.FirstName + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>LastName1</name>");
		sb.Append("<type>char</type>");
		sb.Append("<length>15</length>");
		sb.Append("<value>" + AD.LastName1 + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>LastName2</name>");
		sb.Append("<type>char</type>");
		sb.Append("<length>15</length>");
		sb.Append("<value>" + AD.LastName2 + "</value>");
		sb.Append("</parameter>");

//		sb.Append("<parameter>");
//		sb.Append("<name>HomePhone</name>");
//		sb.Append("<type>char</type>");
//		sb.Append("<length>14</length>");
//		sb.Append("<value>" + AD.HomePhone + "</value>");
//		sb.Append("</parameter>");
//
//		sb.Append("<parameter>");
//		sb.Append("<name>WorkPhone</name>");
//		sb.Append("<type>char</type>");
//		sb.Append("<length>14</length>");
//		sb.Append("<value>" + AD.WorkPhone + "</value>");
//		sb.Append("</parameter>");
//
//		sb.Append("<parameter>");
//		sb.Append("<name>Cellular</name>");
//		sb.Append("<type>char</type>");
//		sb.Append("<length>14</length>");
//		sb.Append("<value>" + AD.Cellular + "</value>");
//		sb.Append("</parameter>");

		sb.Append("</parameters>");
		xmlDoc.InnerXml = sb.ToString();
		sb = null;

		Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

		DataTable DT = exec.GetQuery("GetQuotesDriversByCriteria",xmlDoc);
		
		return DT;
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
	/// <functionName>GetAutoDriverXml</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>XmlDocument</returns>

	private XmlDocument GetAutoDriverXml()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		XmlDocument xmlDoc = new XmlDocument();
			
		sb.Append("<parameters>");

//		sb.Append("<parameter>");
//		sb.Append("<name>QuotesDriversID</name>");
//		sb.Append("<type>int</type>");
//		sb.Append("<value>" + this.DriverID + "</value>");
//		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.QuoteID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>ProspectID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + base.ProspectID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>BirthDate</name>");
		sb.Append("<type>DateTime</type>");
		sb.Append("<value>" + this.BirthDate + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>GenderID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.Gender + "</value>");
		sb.Append("</parameter>");
            
		sb.Append("<parameter>");
		sb.Append("<name>MaritalStatusID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.MaritalStatus + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>License</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>10</Length>");
		sb.Append("<value>" + this.License + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>SocialSecurity</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>11</Length>");
		sb.Append("<value>" + this.SocialSecurity + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>OccupationID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.OccupationId + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpAddr1</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>30</Length>");
		sb.Append("<value>" + this.EmpAddr1 + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpAddr2</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>30</Length>");
		sb.Append("<value>" + this.EmpAddr2 + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpCity</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>14</Length>");
		sb.Append("<value>" + this.EmpCity + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpState</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>2</Length>");
		sb.Append("<value>" + this.EmpState + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpZipCode</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>10</Length>");
		sb.Append("<value>" + this.EmpZipCode + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>GoodStudent</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>1</Length>");
		sb.Append("<value>" + this.GoodStudent + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>Training</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>1</Length>");
		sb.Append("<value>" + this.Training + "</value>");
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
	/// <functionName>GetAutoDriverUpdateXml</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>XmlDocument</returns>

	private XmlDocument GetAutoDriverUpdateXml()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		XmlDocument xmlDoc = new XmlDocument();
			
		sb.Append("<parameters>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesDriversID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.DriverID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.QuoteID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>ProspectID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + base.ProspectID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>BirthDate</name>");
		sb.Append("<type>DateTime</type>");
		sb.Append("<value>" + this.BirthDate + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>GenderID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.Gender + "</value>");
		sb.Append("</parameter>");
            
		sb.Append("<parameter>");
		sb.Append("<name>MaritalStatusID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.MaritalStatus + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>License</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>10</Length>");
		sb.Append("<value>" + this.License + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>SocialSecurity</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>11</Length>");
		sb.Append("<value>" + this.SocialSecurity + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>OccupationID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.OccupationId + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpAddr1</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>30</Length>");
		sb.Append("<value>" + this.EmpAddr1 + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpAddr2</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>30</Length>");
		sb.Append("<value>" + this.EmpAddr2 + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpCity</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>14</Length>");
		sb.Append("<value>" + this.EmpCity + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpState</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>2</Length>");
		sb.Append("<value>" + this.EmpState + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>EmpZipCode</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>10</Length>");
		sb.Append("<value>" + this.EmpZipCode + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>GoodStudent</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>1</Length>");
		sb.Append("<value>" + this.GoodStudent + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>Training</name>");
		sb.Append("<type>char</type>");
		sb.Append("<Length>1</Length>");
		sb.Append("<value>" + this.Training + "</value>");
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
	/// <functionName>GetAutoDriverDeleteXml</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>XmlDocument</returns>

	private XmlDocument GetAutoDriverDeleteXml()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		XmlDocument xmlDoc = new XmlDocument();
			
		sb.Append("<parameters>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesDriversID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.DriverID + "</value>");
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
	/// <functionName>HandleException</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// Exception Ex
	/// <param name=""></param>
	/// <returns>void</returns>

	private void HandleException(Exception Ex)
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
						"saving the Driver.", Ex);
			}
			default:
				throw new Exception("An error ocurred while saving " + 
					" the Driver.", Ex);
		}           
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

	protected void Insert(int UserID)
	{
		Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
		try
		{
			executor.BeginTrans();

			if (this._IsPolicy)
			{
				this._DriverID = executor.Insert("AddAutoDrivers", this.GetAutoDriverXml());
			}
			else
			{
				this._DriverID = executor.Insert("AddQuotesDrivers", this.GetAutoDriverXml());
			}

			executor.CommitTrans();
			//this.AuditInsert(UserID);
			//this.CommitAudit();			
			this.Mode = (int)Enumerators.Modes.Update;
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
	/// <functionName>Update</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>void</returns>

	private void Update(int UserID)
	{
		Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
		try
		{
			//this.AuditUpdate(UserID);
			executor.BeginTrans();

			if (this._IsPolicy)
			{
				executor.Update("UpdateAutoDrivers", this.GetAutoDriverUpdateXml());
			}
			else
			{
				executor.Update("UpdateQuotesDrivers", this.GetAutoDriverUpdateXml());
			}

			executor.CommitTrans();
			//this.CommitAudit();
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
	/// <functionName>Delete</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name=""></param>
	/// <returns>void</returns>

	private void Delete(int UserID)
	{
		Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
		try
		{
			executor.BeginTrans();

			if (this._IsPolicy)
			{
				executor.Update("DeleteAutoDrivers", this.GetAutoDriverDeleteXml());
			}
			else
			{
				executor.Update("DeleteQuotesDrivers", this.GetAutoDriverDeleteXml());
			}

			executor.CommitTrans();
			//this.AuditDelete(UserID);
			//this.CommitAudit();
		}
		catch (Exception xcp)
		{
			executor.RollBackTrans();
			this.HandleException(xcp);
		}
	}


	private void FillProperties(DataRow DR)
	{
		// PROSPECT INFORMATION
		this.ProspectID = (int)DR["ProspectID"];

		this.fillProspectInfo(DR);

		// DRIVER INFORMATION
		this.DriverID = (int)DR["QuotesDriversID"];
		this.QuoteID = (int)DR["QuotesID"];
		this.BirthDate = (DR["BirthDate"]!= System.DBNull.Value)? ((DateTime)DR["BirthDate"]).ToString("d"):"";
		this.Gender = (int)DR["GenderID"];
		this.MaritalStatus = (int)DR["MaritalStatusID"];
		this.License = DR["License"].ToString();
		this.SocialSecurity = DR["SocialSecurity"].ToString();
		this.OccupationId = (int)DR["OccupationID"];
		this.EmpAddr1 = DR["EmpAddr1"].ToString();
		this.EmpAddr2 = DR["EmpAddr2"].ToString();
		this.EmpCity = DR["EmpCity"].ToString();
		this.EmpState = DR["EmpState"].ToString();
		this.EmpZipCode = DR["EmpZipCode"].ToString();
		this.GoodStudent = DR["GoodStudent"].ToString();
		this.Training = DR["Training"].ToString();
	}

	private static DataTable GetQuotesDriversForQuote(int QuotesID, bool ispolicy)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		XmlDocument xmlDoc = new XmlDocument();

		sb.Append("<parameters>");
		sb.Append("<parameter>");
		sb.Append("<name>QuotesID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + QuotesID + "</value>");
		sb.Append("</parameter>");
		sb.Append("</parameters>");
		xmlDoc.InnerXml = sb.ToString();
		sb = null;

		Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

		DataTable dt;
		if (ispolicy)
		{
			dt = exec.GetQuery("GetAutoDriversForQuote",xmlDoc);
		}
		else
		{
			dt = exec.GetQuery("GetQuotesDriversForQuote",xmlDoc);
		}
		return dt;
	}

	private static DataTable GetQuotesDrivers(int DriversID, bool ispolicy)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();

		XmlDocument xmlDoc = new XmlDocument();

		sb.Append("<parameters>");
		sb.Append("<parameter>");
		sb.Append("<name>QuotesDriversID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + DriversID + "</value>");
		sb.Append("</parameter>");
		sb.Append("</parameters>");
		xmlDoc.InnerXml = sb.ToString();
		sb = null;

		Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

		DataTable dt;
		if(ispolicy)
		{
			dt = exec.GetQuery("GetAutosDrivers",xmlDoc);
		}
		else
		{
			dt = exec.GetQuery("GetQuotesDrivers",xmlDoc);
		}
		return dt;
	}

	private void fillProspectInfo(Prospect Prospect)
	{
		this.IsBusiness = Prospect.IsBusiness;
		this.LocationID = Prospect.LocationID;
		this.ReferredID = Prospect.ReferredID;
		this.ReferredDesc = Prospect.ReferredDesc;
		this.EntryDate = Prospect.EntryDate;
		this.ConvertToClient = Prospect.ConvertToClient;
		this.FirstName = Prospect.FirstName;
		this.LastName1 = Prospect.LastName1;
		this.LastName2 = Prospect.LastName2;
		this.HouseholdIncomeID = Prospect.HouseholdIncomeID;
		this.HomePhone = Prospect.HomePhone;
		this.WorkPhone = Prospect.WorkPhone;
		this.Cellular = Prospect.Cellular;
		this.BusinessName = Prospect.BusinessName;
		this.Modify = Prospect.Modify;
	}

	private void fillProspectInfo(DataRow DR)
	{
		this.IsBusiness = bool.Parse(DR["IsBusiness"].ToString().Trim());
		this.LocationID = int.Parse(DR["LocationID"].ToString().Trim());
			
		this.ReferredID = (DR["ReferredID"] != System.DBNull.Value)?
			int.Parse(DR["ReferredID"].ToString().Trim()):
			0;
			
		this.ReferredDesc = DR["ReferredDesc"].ToString().Trim();
		
		this.EntryDate = (DR["EntryDate"] != System.DBNull.Value)?
			(DateTime) DR["EntryDate"]:
			DateTime.Parse("1/1/1900");
		
		this.ConvertToClient = (DR["ConvertToClient"] != System.DBNull.Value)?
			(DateTime) DR["ConvertToClient"]:
			DateTime.Parse("1/1/1900");

		this.FirstName = DR["FirstName"].ToString().Trim();
		this.LastName1 = DR["LastName1"].ToString().Trim();
		this.LastName2 = DR["LastName2"].ToString().Trim();

		this.HouseholdIncomeID = (DR["HouseHoldIncomeID"] != 
			System.DBNull.Value)
			? int.Parse(DR["HouseHoldIncomeID"].ToString().Trim()) : 0;

		this.HomePhone = DR["HomePhone"].ToString().Trim();
		this.WorkPhone = DR["WorkPhone"].ToString().Trim();
		this.Cellular = DR["Cellular"].ToString().Trim();
		this.Email = DR["Email"].ToString().Trim();
		this.BusinessName = DR["BusinessName"].ToString().Trim();
		this.Modify = bool.Parse(DR["Modify"].ToString().Trim());
	}

	#region History

	private void History(int mode, int userID)
	{
		Audit.History history = new Audit.History();
		
		if(_oldAutoDriver != null)
		{
			if(mode == 2 )
			{			
				history.BuildNotesForHistory("FirstName",_oldAutoDriver.FirstName.ToString(),this.FirstName.ToString(),mode);
				history.BuildNotesForHistory("LastName1",_oldAutoDriver.LastName1.ToString(),this.LastName1.ToString(),mode);
				history.BuildNotesForHistory("LastName2",_oldAutoDriver.LastName2.ToString(),this.LastName2.ToString(),mode);
				history.BuildNotesForHistory("BirthDate",_oldAutoDriver.BirthDate.ToString(),this.BirthDate.ToString(),mode);
				history.BuildNotesForHistory("Gender",
					LookupTables.LookupTables.GetDescription("Gender",_oldAutoDriver.Gender.ToString()),
					LookupTables.LookupTables.GetDescription("Gender",this.Gender.ToString()),
					mode);
				history.BuildNotesForHistory("MaritalStatus",
					LookupTables.LookupTables.GetDescription("MaritalStatus",_oldAutoDriver.MaritalStatus.ToString()),
					LookupTables.LookupTables.GetDescription("MaritalStatus",this.MaritalStatus.ToString()),
					mode);
				history.BuildNotesForHistory("License",_oldAutoDriver.License.ToString(),this.License.ToString(),mode);
				history.BuildNotesForHistory("SocialSecurity",_oldAutoDriver.SocialSecurity.ToString(),this.SocialSecurity.ToString(),mode);
			    			
				history.Actions = "EDIT";
			}
	
			if (this._IsPolicy)
				history.KeyID = this._QuoteID.ToString();
			else
				history.KeyID = this._TCIDForAutoDriver.ToString();

			if (this._IsPolicy)
				history.Subject = "AUTOPERSONALPOLICY";			
			else
				history.Subject = "QUOTE";			

			history.UsersID =  userID;
			history.GetSaveHistory();
		}
	}

	#endregion

		
#endregion

}
// END CLASS DEFINITION AutoDriver

}