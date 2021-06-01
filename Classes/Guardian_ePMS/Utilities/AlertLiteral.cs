using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace UtilityComponents
{
	/// <summary>
	/// Summary description for WebCustomControl1.
	/// </summary>
	

	[DefaultProperty("Text"),
	ToolboxData("<{0}:AlertLiteral runat=server></{0}:AlertLiteral>")]
	public class AlertLiteral : System.Web.UI.WebControls.Literal 
	{
		private string _message = "";
		private bool _isValid = true;
		private bool _useDefault = true;
		private const string defaultRequiredMessage = @"The following fields are required:\r\n";
		private const string defaultRegexMessage = @"The following fields have invalid format:\r\n";
		private const string defaultCompareMessage = @"The following fields have invalid data:\r\n";
		private const string defaultRangeMessage = @"The following fields are out of range:\r\n";
		private const string defaultCustomMessage = @"The following fields are invalid:\r\n";
		private string _requiredMessage = defaultRequiredMessage;
		private string _regexMessage = defaultRegexMessage;
		private string _compareMessage = defaultCompareMessage;
		private string _rangeMessage = defaultRangeMessage;
		private string _customMessage = defaultCustomMessage;
	
		
		[Bindable(true),
		Category("Appearance"),
		DefaultValue("")]
		public string RequiredValidatorMessage
		{
			get
			{
				return this._requiredMessage;
			}
			set
			{
				this._requiredMessage = value;
				if(this._requiredMessage.Trim().Equals(""))
				{
					this._requiredMessage = defaultRequiredMessage;
				}
			}
		}
		public string RegexValidatorMessage
		{
			get
			{
				return this._regexMessage;
			}
			set
			{
				this._regexMessage = value;
				if(this._regexMessage.Trim().Equals(""))
				{
					this._regexMessage = defaultRegexMessage;
				}
			}
		}
		public string RangeValidatorMessage
		{
			get
			{
				return this._rangeMessage;
			}
			set
			{
				this._rangeMessage = value;
				if(this._rangeMessage.Trim().Equals(""))
				{
					this._rangeMessage = defaultRangeMessage;
				}
			}
		}
		public string CompareValidatorMessage
		{
			get
			{
				return this._compareMessage;
			}
			set
			{
				this._compareMessage = value;
				if(this._compareMessage.Trim().Equals(""))
				{
					this._compareMessage = defaultCompareMessage;
				}
			}
		}
		public string CustomValidatorMessage
		{
			get
			{
				return this._customMessage;
			}
			set
			{
				this._customMessage = value;
				if(this._customMessage.Trim().Equals(""))
				{
					this._customMessage = defaultCustomMessage;
				}
			}
		}
		public bool UseDefaultGroupMessages
		{
			get
			{
				return this._useDefault;
			}
			set
			{
				this._useDefault = value;
			}
		}
		public bool CheckValid()
		{
			CreateMessage();
			return _isValid;
		}

		
		private void CreateMessage()
		{
			Page.Validate();
			this._isValid = true;
			StringBuilder sbRequired = new StringBuilder();
			StringBuilder sbCompare = new StringBuilder();
			StringBuilder sbCustom = new StringBuilder();
			StringBuilder sbRange = new StringBuilder();
			StringBuilder sbRegex = new StringBuilder();
			StringBuilder sbMessage = new StringBuilder();
			for(int i=0;i<Page.Validators.Count;i++)
			{
				if(!Page.Validators[i].IsValid)
				{
					switch(Page.Validators[i].GetType().Name)
					{
						case "RequiredFieldValidator":
							if(_useDefault)
							{
								sbRequired.Append("* ");
							}
							sbRequired.Append(((RequiredFieldValidator)Page.Validators[i]).ErrorMessage);
							sbRequired.Append(@"\r\n");

							break;
						case "CompareValidator":
							if(_useDefault)
							{
								sbCompare.Append("* ");
							}
							sbCompare.Append(((CompareValidator)Page.Validators[i]).ErrorMessage);
							sbCompare.Append(@"\r\n");
							break;
						case "CustomValidator":
							if(_useDefault)
							{
								sbCustom.Append("* ");
							}
								sbCustom.Append(((CustomValidator)Page.Validators[i]).ErrorMessage);
								sbCustom.Append(@"\r\n");
							break;
						case "RegularExpressionValidator":
							if(_useDefault)
							{
								sbRegex.Append("* ");
							}
							sbRegex.Append(((RegularExpressionValidator)Page.Validators[i]).ErrorMessage);
							sbRegex.Append(@"\r\n");
							break;
						case "RangeValidator":
							if(_useDefault)
							{
								sbRange.Append("* ");
							}
							sbRange.Append(((RangeValidator)Page.Validators[i]).ErrorMessage);
							sbRange.Append(@"\r\n");
							break;
					}//End switch(Page.Validators[i].GetType().Name)
				}
			}//End for(int i=0;i<Page.Validators.Count;i++)
			if(!sbRequired.ToString().Equals(""))
			{
				this._isValid = false;
				if(_useDefault)
				{
					sbMessage.Append(_requiredMessage);
				}
				sbMessage.Append(sbRequired.ToString());
			}
			if(!sbRegex.ToString().Equals(""))
			{
				this._isValid = false;
				if(_useDefault)
				{
					sbMessage.Append(_regexMessage );
				}
				sbMessage.Append(sbRegex.ToString());
			}
			if(!sbCompare.ToString().Equals(""))
			{
				this._isValid = false;
				if(_useDefault)
				{
					sbMessage.Append(_compareMessage );
				}
				sbMessage.Append(sbCompare.ToString());
			}
			if(!sbRange.ToString().Equals(""))
			{
				this._isValid = false;
				if(_useDefault)
				{
					sbMessage.Append(_rangeMessage );
				}
				sbMessage.Append(sbRange.ToString());
			}
			if(!sbCustom.ToString().Equals(""))
			{
				this._isValid = false;
				if(_useDefault)
				{
					sbMessage.Append(_customMessage );
				}
				sbMessage.Append(sbCustom.ToString());
			}
			if(this._isValid)
			{
				this._message = "";
				this.Visible = false;
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				this._message = sbMessage.ToString();
				this._message = Regex.Replace(_message,"\\r\\n",@"\r\n");
				this._message = Regex.Replace(_message,"\\n\\r",@"\n\r");
				this._message = Regex.Replace(_message,"'",@"\'");
				this._message = Regex.Replace(_message,"\"","\\\"");

				sb.Append("<script language=javascript>alert('");
				sb.Append(_message);
				sb.Append("');</script>");
				this.Text = sb.ToString();
				this.Visible = true;
			}
			
		}
	}
	/// <summary>
	/// Summary description for WebCustomControl1.
	/// </summary>
	[DefaultProperty("Text"),
	ToolboxData("<{0}:FocusManager runat=server></{0}:FocusManager>")]
	public class FocusManager : System.Web.UI.Control
	{
		private string _boundControlID = String.Empty;

		[Bindable(true),
		Category("Appearance"),
		DefaultValue("")]
		public string BoundControlID
		{
			get
			{
				return this._boundControlID;
			}

			set
			{
				this._boundControlID = value;
			}
		}

		/// <summary>
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			string script = String.Empty;
			if (this.BoundControlID != String.Empty)
			{
				if (this.Page.FindControl(this.BoundControlID) != null)
				{
					script += @"<script type=""text/javascript"">"+
						@"document.getElementById("""+ 
						this.BoundControlID+
						@""").focus();</script>";
					this.Page.RegisterStartupScript("FocusManager",script);
				}
			}
		}
	}


}


		

