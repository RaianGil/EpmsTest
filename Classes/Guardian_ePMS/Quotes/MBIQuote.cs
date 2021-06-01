using System;
using System.Data;
using System.Xml;
using Baldrich.DBRequest;
using EPolicy.LookupTables;
using EPolicy;

namespace EPolicy.Quotes
{
	public class MBIQuote
	{
		#region Private Variable Declarations

		private string    _carMake = "";
		private string    _carModel = "";
		private int       _term = 0;
		private int       _plan = 0;
		private string    _classification = "";
		private double    _premium = 0;
		private string    _policyType = "";
		private Baldrich.DBRequest.DBRequest _DbRequest = new Baldrich.DBRequest.DBRequest();

		#endregion

		#region Property Declarations
	
		public string carMake
		{
			get
			{
				return(_carMake);
			}
			set
			{
				_carMake = value.ToUpper().Trim();
			}
		}

		public string carModel
		{
			get
			{
				return(_carModel);
			}
			set
			{
				_carModel = value.ToUpper().Trim();
			}
		}

		public int term
		{
			get
			{
				return(_term);
			}
			set
			{
				_term = value;
			}
		}

		public int plan
		{
			get
			{
				return(_plan);
			}
			set
			{
				_plan = value;
			}
		}

		public string classification
		{
			get
			{
				return(_classification);
			}
		}

		public double premium
		{
			get
			{
				return(_premium);
			}
		}

		public string policyType
		{
			get
			{
				return(_policyType);
			}
			set
			{
				_policyType = value.ToUpper();
			}
		}

		//			public DataTable vehicleList
		//			{
		//				get
		//				{
		//					return(GetMBIVehicles());
		//				}
		//			}


		#endregion

		public MBIQuote()
		{

		}
	
		#region Public Methods

		public void Calculate(string policyType, string carModel, string carMake, int term)
		{
			_policyType = policyType;
			_carModel   = carModel;
			_carMake    = carMake;
			_term       = term;

			string tablePremium = "";
			string tablePlan = "";
			DataTable myResults = new DataTable();

			if(this.term==60)
			{
				if(_policyType=="AG")
				{
					tablePremium = "PRIMAG_1";
					tablePlan    = "PLANAG_1";
				}
				else
				{
					tablePremium = "PRIMA_1";
					tablePlan    = "PLAN_1";
				}
			}
			else if(this.term==72)
			{
				if(_policyType=="AG")
				{
					tablePremium = "PRIMAG_2";
					tablePlan    = "PLANAG_2";
				}
				else
				{
					tablePremium = "PRIMA_2";
					tablePlan    = "PLAN_2";
				}
			}

			string data = "";
			myResults = GetMBIDetails();

			if(_policyType=="AG" || _policyType=="MBI")
			{
				data = myResults.Rows[0][tablePremium].ToString();
			}
			if(data!="")
			{
				this._premium = double.Parse(data);
				this._classification = myResults.Rows[0]["CLASS"].ToString();
				string plan = myResults.Rows[0][tablePlan].ToString();
				this._plan = int.Parse(plan);
			}
		}

		#endregion
        
		#region Private Methods

		private DataTable GetMBIDetails()

		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>MAK_NAME</name>");
			sb.Append("<type>nvarchar</type>");
			sb.Append("<value>" + _carMake + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>MOD_NAME</name>");
			sb.Append("<type>nvarchar</type>");
			sb.Append("<value>" + _carModel + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;
			return _DbRequest.GetQuery ("GetMBIDetails", xmlDoc);
		}

		//		private DataTable GetMBIVehicles()
		//
		//		{
		//			System.Text.StringBuilder sb = new System.Text.StringBuilder();
		//
		//			XmlDocument xmlDoc = new XmlDocument();
		//
		//			sb.Append("<parameters>");
		//			sb.Append("</parameters>");
		//			xmlDoc.InnerXml = sb.ToString();
		//			sb = null;
		//			return _objExecuter.GetQuery ("GetMBIVehicles", xmlDoc);
		//		}
		#endregion
	}
}
