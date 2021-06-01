using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Baldrich.DBRequest;

namespace EPolicy.Quotes
{    
	
public class CoverBreakdown 
{ 
#region Private Attributes
	private int _AutoCoverID;
	private System.Collections.SortedList _Breakdown;
	private int _Type;

	//use for Policy only.
	private bool _IsPolicy = false;
#endregion

#region Public Properties
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
	/// <propertyName>Breakdown</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public System.Collections.SortedList Breakdown
	{
		get
		{
			if (_Breakdown == null)
				_Breakdown = new SortedList();
			return(_Breakdown);
		}
		set {
			_Breakdown = value;
		}
	}
     
	/// <summary>
	///        
	/// </summary>  
	/// <remarks>
	/// <propertyName>Type</propertyName><br/>
	/// <author>Benigno Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>
	public int Type
	{
		get
		{
			return(_Type);
		}
		set {
			_Type = value;
		}
	}
           
#endregion

#region Public Methods
	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>AddCoverBreakdown</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// string value
	/// <param name=""></param>
	/// <returns>void</returns>

	public void AddCoverBreakdown(int BDAnniversary, object BDValue)
	{
		if (this.Breakdown[BDAnniversary] == null)
			this.Breakdown[BDAnniversary] = BDValue;
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

	public void Save(int UserID)
	{
		this.Insert();
	}

	/// <summary>
	///		
	///	</summary>
	/// <remarks>
	/// <functionName>LoadCoverBreakdownForQuote</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// 
	/// <param name="">int AutoCoverID</param>
	/// <returns>void</returns>

	public static ArrayList LoadCoverBreakdownForAutoCover(int AutoCoverID,  bool ispolicy)
	{
		///TODO: Fix this method to load all
		DataTable dt = GetQuotesBreakdownCoverForAutoCover(AutoCoverID,ispolicy);
		ArrayList AL = new ArrayList(dt.Rows.Count);
		for(int i = 0; i < dt.Rows.Count; i++)
		{
			CoverBreakdown CB = new CoverBreakdown();
			CB._AutoCoverID = (int)dt.Rows[i]["QuotesAutoID"];
			CB._Breakdown = new SortedList(); 
			CB._Type = (int)dt.Rows[i]["BreakdownCoversTypeID"];
			AL.Add(CB);
		}
		return AL;
	}
	/// <summary>
	///		// Tests for either VehicleVIN or QuotesAutoID (if it's not '0')
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
			// check if types are equal
			return (this._Type == ((CoverBreakdown)Obj)._Type);
		}
		catch
		{
			return false;
		}
	}

	/// <summary>
	///		// To implement Equals must implement this one also.

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
	/// <functionName>GetCoverBreakdownXml</functionName><br/>
	/// <author>B. Nieves</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// </remarks>          
	/// int index
	/// <param name=""></param>
	/// <returns>XmlDocument</returns>

	private XmlDocument GetCoverBreakdownXml(int index)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		XmlDocument xmlDoc = new XmlDocument();
			
		sb.Append("<parameters>");

		sb.Append("<parameter>");
		sb.Append("<name>QuotesAutoID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.AutoCoverID + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>BreakdownCoversTypeID</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + this.Type + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>BreakdownAnniversary</name>");
		sb.Append("<type>int</type>");
		sb.Append("<value>" + (index + 1) + "</value>");
		sb.Append("</parameter>");

		sb.Append("<parameter>");
		sb.Append("<name>BreakdownValue</name>");
		sb.Append("<type>varchar</type>");
		sb.Append("<Length>20</Length>");
		sb.Append("<value>" + this.Breakdown[index] + "</value>");
		sb.Append("</parameter>");

		sb.Append("</parameters>");

		xmlDoc.InnerXml = sb.ToString();

		sb = null;

		return xmlDoc;
	}
    
	private void HandleException(Exception Ex)
	{
		switch(Ex.GetBaseException().GetType().FullName)
		{
			case "System.Data.SqlClient.SqlException":
				SqlException sqlException = (SqlException)Ex.GetBaseException();
			switch(sqlException.Number)
			{
				case 547:
					throw new Exception("The Cover Breakdown you are trying to " +
						"relate to this search fields does not exist.", Ex);
				default:
					throw new Exception("An database server error ocurred while " +
						"saving the Cover Breakdown.", Ex);
			}
			default:
				throw new Exception("An error ocurred while saving " + 
					" the Cover Breakdown.", Ex);
		}
	}
    	
	private void Insert()
	{
		Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
		try
		{
			for ( int i=0; i < this.Breakdown.Count; i++)
			{
				executor.BeginTrans();
				if (this._IsPolicy)
				{
					executor.Insert("AddAutoBreakdownCovers", this.GetCoverBreakdownXml(i));
				}
				else
				{
					executor.Insert("AddQuotesBreakdownCovers", this.GetCoverBreakdownXml(i));
				}

				executor.CommitTrans();

			}
		}
		catch (Exception xcp)
		{
			executor.RollBackTrans();
			this.HandleException(xcp);
		}
	}

	public static DataTable GetQuotesBreakdownCoverForAutoCover(int AutoCoverID, bool ispolicy)
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
		
		DataTable dt;
		if (ispolicy)
		{
			dt = exec.GetQuery("GetAutoBreakdownCoversByQuotesAutoID",xmlDoc);
		}
		else
		{
			dt = exec.GetQuery("GetQuotesBreakdownCoverByQuotesAutoID",xmlDoc);
		}

		return dt;
	}

#endregion

}
// END CLASS DEFINITION CoverBreakdown
}