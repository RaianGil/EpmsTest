using System;
using System.Xml;
using System.Data.SqlClient;
using System.Text;

namespace EPolicy.XmlCooker
{
	/// <summary>
	/// Summary description for DbRequestXmlCooker.
	/// </summary>
	
	public struct DbRequestXmlCookRequestItem
	{
		public string ParameterName;
		public System.Data.SqlDbType ParameterDataType;
		public int Length;
		public string ParameterData;

		public DbRequestXmlCookRequestItem(string ParameterName,
			System.Data.SqlDbType ParameterDataType, 
			int Length, string ParameterData)
		{
			this.ParameterName = ParameterName;
			this.ParameterDataType = ParameterDataType;
			this.Length = Length;
			this.ParameterData = ParameterData;
		}
	}
    
	public class DbRequestXmlCooker
	{
		
		public DbRequestXmlCooker()
		{
		}

		#region Public Operators

		public static XmlDocument Cook(DbRequestXmlCookRequestItem[] RequestItems)
		{
			StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();
			
			sb.Append("<parameters>");

			if(RequestItems != null && RequestItems.Length > 0)
			{
				for(int i = 0; i <= RequestItems.GetUpperBound(0); i++)
				{
                    AttachXmlParameter(RequestItems[i], ref sb);
				}
			}

			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			return xmlDoc;
		}

		public static XmlDocument Cook()
		{
			return Cook(null);
		}

		public static void AttachCookItem(string ParameterName, 
			System.Data.SqlDbType ParameterDataType,
			int Length, string ParameterData, 
			ref DbRequestXmlCookRequestItem[] CookItems)
		{
			int firstNullIndex = GetFirstNullCookItemIndex(CookItems);

            CookItems[firstNullIndex] = 
				new DbRequestXmlCookRequestItem(ParameterName, ParameterDataType,
				Length, ParameterData);
		}

		#endregion


		#region Private Operators

		private static int GetFirstNullCookItemIndex(
			DbRequestXmlCookRequestItem[] CookItems)
		{
			for(int i = 0; i <= CookItems.GetUpperBound(0); i++)
			{
				if(CookItems[i].ParameterName == null)
				{
					return i;
				}
			}
			return -1;
		}

		private static void AttachXmlParameter(
			DbRequestXmlCookRequestItem RequestItem, ref StringBuilder Sb)
		{
            Sb.Append("<parameter>");
			
			AttachXmlParameterItem("name", RequestItem.ParameterName, ref Sb);
			AttachXmlParameterItem(
				"type", RequestItem.ParameterDataType.ToString().ToLower(),
				ref Sb);

			if(RequestItem.Length != 0)
			{
				AttachXmlParameterItem("Length", RequestItem.Length.ToString(), ref Sb);
			}
			
			AttachXmlParameterItem("value", RequestItem.ParameterData, ref Sb);            
			
            Sb.Append("</parameter>");
		}

		private static void AttachXmlParameterItem(string ItemName, string ItemData, 
			ref StringBuilder Sb)
		{
			Sb.Append("<" + ItemName + ">");
			Sb.Append(ReplaceDisallowedXMLChars(ItemData));
			Sb.Append("</" + ItemName + ">");	
		}

		private static string ReplaceDisallowedXMLChars(string String)
		{
            StringBuilder sb = new StringBuilder(String);
			
			sb.Replace("&", "&amp;");	//&
			sb.Replace("<", "&lt;");	//<
			sb.Replace(">", "&gt;");	//>
			sb.Replace("'", "&apos;");	//'
			sb.Replace("\"", "&quot;"); //"

			return sb.ToString();
		}

		#endregion
	}
}