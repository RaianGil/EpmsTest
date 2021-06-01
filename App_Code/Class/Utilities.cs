using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;

namespace EPolicy
{
	/// <summary>
	/// Summary description for Utilities.
	/// </summary>
	
	public class ExtendedRadioButton : System.Web.UI.WebControls.RadioButton
	{
		public ExtendedRadioButton()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Global Variables

		string _value = string.Empty;
		
		#endregion


		#region Public Properties

		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		#endregion

		#region Public Operators
		
		public override bool Equals(
			Object Obj)
		{
			if (!Obj.GetType().Equals(this.GetType()))
				return false;
			ExtendedRadioButton extendedRadioButton = 
				(ExtendedRadioButton)Obj;
			if(extendedRadioButton.Value == this.Value)
				return true;
			return false;
		}

		#endregion
	}
	
	public class Utilities
	{
		public Utilities()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static string LongDateSpanish(DateTime datetime)
		{
			string Dayweek = DayofWeek(datetime.DayOfWeek.ToString().Trim().ToUpper());
			string Day = datetime.Day.ToString();
			string Month = MonthName(datetime.Month);
			string Year = datetime.Year.ToString();

			return Dayweek+", "+Month+" "+Day+ ", "+Year; 
			//Thursday, December 6, 2007.
		}

		public static string DayofWeek(string dayofweek)
		{
			string days = "";

			switch (dayofweek)
			{
				case "SUNDAY":
					days =  "SUNDAY";
					break;
				case "MONDAY":
					days =  "MONDAY";
					break;
				case "TUESDAY":
					days =  "TUESDAY";
					break;
				case "WEDNESDAY":
					days = "WEDNESDAY";
					break;	
				case "THURSDAY":
					days = "THURSDAY";
					break;	
				case "FRIDAY":
					days = "FRIDAY";
					break;	
				case "SATURDAY":
					days = "SATURDAY";
					break;	
			}
			return days;
		}
			
		public static string MonthName(int month)
		{
			string monthName = "";

			switch (month)
			{
				case 1:
					monthName =  "JANUARY";
					break;
				case 2:
					monthName =  "FEBRUARY";
					break;
				case 3:
					monthName =  "MARCH";
					break;
				case 4:
					monthName = "APRIL";
					break;	
				case 5:
					monthName = "MAY";
					break;	
				case 6:
					monthName = "JUNE";
					break;	
				case 7:
					monthName = "JULY";
					break;	
				case 8:
					monthName = "AUGUST";
					break;	
				case 9:
					monthName = "SEPTEMBER";
					break;
				case 10:
					monthName = "OCTOBER";
					break;
				case 11:
					monthName = "NOVEMBER";
					break;
				case 12:
					monthName = "DECEMBER";
					break;
			}
			return monthName;
		}

		public static string ShowTelephone (string phoneNumber)
		{
			return(FormatPhone (phoneNumber));
		}
		public static DataTable SortDataTable(DataTable input, string sortString)
		{
			DataRow [] myrows = input.Select("",sortString);
			DataTable cloned = input.Clone();

			foreach(DataRow row in myrows)
			{
				cloned.ImportRow(row);
			}
			return(cloned);
		}

		
		public static string MakeLiteralPopUpString(string message)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script language=javascript>alert('");
			message = Regex.Replace(message,"\\r\\n",@"\r\n");
			message = Regex.Replace(message,"\\n\\r",@"\n\r");
			message = Regex.Replace(message,"'",@"\'");
			message = Regex.Replace(message,"\"","\\\"");
			sb.Append(message);
			sb.Append("');</script>");
			return(sb.ToString());
		}

		public static System.Web.UI.WebControls.ImageButton ConfirmDialogBoxPopUp(System.Web.UI.WebControls.ImageButton button, string FormName, string message)
		{
			message = "'"+message+"'";

			button.Page.RegisterClientScriptBlock("doAlert",
				"<script language=javascript>"+"\r\n"+
				"function confirm_save(){"+"\r\n"+
				"if(confirm("+message+")== true)"+"\r\n"+
				FormName+".ConfirmDialogBoxPopUp.value = true;"+"\r\n"+
				"else"+"\r\n"+
				FormName+".ConfirmDialogBoxPopUp.value = false;}"+"\r\n"+	
				"</script>");
		
			button.Attributes.Add("onclick","confirm_save();");
			return(button);
		}

		public static System.Web.UI.WebControls.Button ConfirmDialogBoxPopUp(System.Web.UI.WebControls.Button button, string FormName, string message)
		{
			message = "'"+message+"'";

			button.Page.RegisterClientScriptBlock("doAlert",
				"<script language=javascript>"+"\r\n"+
				"function confirm_save(){"+"\r\n"+
				"if(confirm("+message+")== true)"+"\r\n"+
				FormName+".ConfirmDialogBoxPopUp.value = true;"+"\r\n"+
				"else"+"\r\n"+
				FormName+".ConfirmDialogBoxPopUp.value = false;}"+"\r\n"+	
				"</script>");
		
			button.Attributes.Add("onclick","confirm_save();");
			return(button);
		}

		public static string FormatPhone (string phoneNumber)
		{
			string temp = phoneNumber;
			if(temp.Length<=2)
			{
				return temp;
			}
			if(!Regex.IsMatch(phoneNumber,@"[(]\d{3}[)]\s\d{3}[-]\d{4}"))
			{
				StringBuilder sb = new StringBuilder();
				temp = CleanPhone(phoneNumber.Trim());
				if(temp.Length>=10)
				{
					sb.Append("(");
					sb.Append(temp.Substring(0,3));
					sb.Append(") ");
					sb.Append(temp.Substring(3,3));
					sb.Append("-");
					sb.Append(temp.Substring(6));
				}
				else
				{
					sb.Append(temp.Substring(0,3));
					sb.Append("-");
					sb.Append(temp.Substring(3));
				}
				temp = sb.ToString();
			}
			return(temp);
		}		

		public static string FormatSSN (string socialSecurityNumber)
		{
			string temp = socialSecurityNumber;
			if(temp.Length<=0)
			{
				return temp;
			}
			if(!Regex.IsMatch(socialSecurityNumber,@"\d{3}[-]\d{2}[-]\d{4}"))
			{
				StringBuilder sb = new StringBuilder();
				temp = CleanSSN(socialSecurityNumber.Trim());
				sb.Append(temp.Substring(0,3));
				sb.Append("-");
				sb.Append(temp.Substring(3,2));
				sb.Append("-");
				sb.Append(temp.Substring(5));
				temp = sb.ToString();
			}
			return(temp);
		}

		public static string FormatEmployerSSN (string socialSecurityNumber)
		{
			string temp = socialSecurityNumber;
			if(temp.Length<=0)
			{
				return temp;
			}
			if(!Regex.IsMatch(socialSecurityNumber,@"\d{3}[-]{1}\d{7}"))
			{
				StringBuilder sb = new StringBuilder();
				temp = CleanSSN(socialSecurityNumber.Trim());
				sb.Append(temp.Substring(0,3));
				sb.Append("-");
				sb.Append(temp.Substring(3));
				temp = sb.ToString();
			}
			return(temp);
		}

		public static string FormatZipCode (string zipCode)
		{
			string temp = zipCode;
			string [] tempArray = null;
			if(temp.Length<=0)
			{
				return temp;
			}
			if(!Regex.IsMatch(zipCode,@"\d{5}[-]\d{4}"))
			{

				StringBuilder sb = new StringBuilder();
				temp = Regex.Replace(zipCode.Trim(),"-","");
				tempArray = Regex.Split(temp,@"[,]{1}\s{1}");
				for(int i=0;i<tempArray.Length;i++)
				{
					sb.Append(tempArray[i].Substring(0,5));
					if(tempArray[i].Length>5)
					{
						sb.Append("-");
						sb.Append(tempArray[i].Substring(5));
					}
					sb.Append(", ");
				}
				temp = sb.ToString();
				temp = temp.Substring(0,(temp.Length-2));
			}
			return(temp);
		}
		public static string CleanSocialSecurity(string socialSecurityNumber)
		{
			return(CleanSSN (socialSecurityNumber));
		}
		public static string CleanZipCode(string zipCode)
		{
			return(Regex.Replace(zipCode,@"[-]|\s",""));
		}
		public static string CleanSSN(string formSocialSecurity)
		{
			return(Regex.Replace(formSocialSecurity,@"[-]|\s",""));
		}
		public static string CleanTelephone(string phoneNumber)
		{
			return(CleanPhone (phoneNumber));
		}
		public static string CleanPhone(string formTelephone)
		{
			return(Regex.Replace(formTelephone,@"([()-])|\s",""));
		}

		/// <summary>
		/// Check if onject in question is of the type to be converted
		/// </summary>
		/// <param name="objectToTest">The object to be tested</param>
		/// <param name="nameOfType">The type to be converted</param>
		/// <returns>If casting is valid, returns the same object.  
		/// If the casting is invalid, returns null.</returns>
		public static object CheckObjectType(object objectToTest, string nameOfType)
		{
			if(objectToTest != null)
			{
				if(objectToTest.GetType().ToString() == nameOfType)
				{
					return(objectToTest);
				}
			}
			return(null);
		}
	}

	/// <summary>
	/// Structure to persist the information of 
	/// the address fields in memory.
	/// </summary>
	public struct AddressFields
	{
		public int entityID;
		//Home Address
		public string txtHomeUrb;
		public string txtAddress1;
		public string txtAddress2;
		public string txtCity;
		public string txtState;
		public string txtZipCode;
		//Work Address
		public string txtWorkUrb;
		public string txtBusinessAddr1;
		public string txtBusinessAddr2;
		public string txtBusinessCity;
		public string txtBusinessState;
		public string txtBusinessZipCode;
		//Home Physical Address
		public string txtAddress1Phys;
		public string txtAddress2Phys;
		public string txtCityPhys;
		public string txtStatePhys;
		public string txtZipCodePhys;
		//Work Physical Address
		public string txtBusinessAddrPhys1;
		public string txtBusinessAddrPhys2;
		public string txtBusinessPhysCity;
		public string txtBusinessPhysState;
		public string txtBusinessPhysZipCode;
	}
	public struct ReportFields
	{
		public string Report;
		public string CustType;
		public string LineOfBusiness;
		public string DateFrom;
		public string DateTo;
		public string HouseHoldIncome;
	}

}
