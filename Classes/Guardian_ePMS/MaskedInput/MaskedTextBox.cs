using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;

namespace MaskedInput 
{
	/// <summary>
	/// Copyright 2002 Softek PR.<br/>
	/// <fileName>MaskedTextBox.cs</fileName><br/>
	/// <classDescription>
	/// This class declares a textbox that uses a mask
	/// to limit the input it will receive.
	/// </classDescription><br/>
	/// <author>Javier J. Vega Caro</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// <version>1.0</version><br/>
	/// <copyrightInfo>
	/// The code included in this source file may not be used or modified 
	/// in any way without the written consent of Softek PR.
	/// </copyrightInfo><br/>
	/// </summary>
	/// <remarks>The mask itself has the following format:<br/> 
	/// X is used for letters <br/>
	/// 9 is used for numbers <br/>
	/// D is used for numbers and the / character (used for dates)
	/// Any symbols will be interpreted as is.<br/>
	/// For example the mask to filter for a social security number is:<br/>
	/// <code>999-99-9999</code><br/>
	/// Remember to include a MaskedTextHeader control ( included in this library)
	/// in the form or the mask.js for it to work.
	/// </remarks>
	[DefaultProperty("Mask"),
	ToolboxData("<{0}:MaskedTextBox runat=server></{0}:MaskedTextBox>")]
	public class MaskedTextBox : TextBox 
	{
		/// <summary>
		/// the string containing the mask
		/// </summary>
		private string _mask = "";
		/// <summary>
		/// A boolean value that will tell the system if the text to be
		/// entered is a date value
		/// </summary>
		private bool _isDate = false;
		private bool _isZipCode = false;
		private bool _isCurrency = false;
		//private System.Windows.Forms.ErrorProvider errorProvider1;

		/// <summary>
		/// The string that will control what format the textbox should
		/// have.<br/> Use X for letters,<br/> 9 for numbers,<br/> everything else
		/// will be interpreted as is.
		/// </summary>
		[Bindable(true),
		Category("MaskedInput"),
		DefaultValue("")]
		public string Mask
		{
			set
			{
				if(_isDate)
				{
					_mask = "DD/DD/9999";
				}
				else
				{
					_mask = value;
				}
			}
			get
			{
				return _mask;
			}
		}
		/// <summary>
		/// A boolean value that will be used to set a specific mask
		/// and regexmask to values that will permit valid dates
		/// </summary>
		[Bindable(true),
		Category("MaskedInput"),
		DefaultValue("False")]
		public bool IsDate
		{
			set
			{
				_isDate = value;
				_isZipCode = false;
				_isCurrency = false;
				if(_isDate)
				{
					_mask = "DD/DD/9999";
				}
			}
			get
			{
				return _isDate;
			}
		}
		[Bindable(true),
		Category("MaskedInput"),
		DefaultValue("False")]
		public bool IsCurrency
		{
			set
			{
				_isCurrency = value;
				_isDate = false;
				_isZipCode = false;
				if(_isCurrency)
				{
					_mask = "CCCCCCCCCC"; // Added Currency Support up to 9999999999 or 9999999.99
				}
			}
			get
			{
				return _isCurrency;
			}
		}
		[Bindable(true),
		Category("MaskedInput"),
		DefaultValue("False")]
		public bool IsZipCode
		{
			set
			{
				_isZipCode = value;
				_isDate = false;
				_isCurrency = false;
				if(_isZipCode)
				{
					_mask = "99999Z9999";
				}
			}
			get
			{
				return _isZipCode;
			}
		}
		/// <summary>
		/// gets a regular expression that is equal to the whole
		/// mask string.
		/// </summary>
		[Bindable(false),
		Category("MaskedInput")]
		public string RegexMask
		{
			get
			{
				return this.ChangeMaskToRegex(this._mask);
			}
		}

		/// <summary>
		/// Function to add the mask to the textbox
		/// </summary>
		/// <param name="writer">This writes the events in the textbox</param>
		protected override void AddAttributesToRender(HtmlTextWriter writer) 
		{
          
			//if(!_mask.Equals(""))
			//{
			base.AddAttributesToRender(writer);
			writer.AddAttribute("onkeypress", "MaskIt(this,'" + _mask + "',true);");
			//writer.AddAttribute("onkeydown", "MaskIt(this,'" + _mask + "');");
			writer.AddAttribute("onblur", "MaskIt(this,'" + _mask + "',false);");
			//writer.AddAttribute("onpropertychange", "MaskIt(this,'" + _mask + "');");
			//}
		}
		/// <summary>
		/// A function to make sure a mask is entered then it the max length
		/// should be set to the mask length.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(System.EventArgs e)
		{
			if(!_mask.Equals(""))
			{
				this.MaxLength = _mask.Length;
			}
			if(this._isDate)
			{
				this.MaxLength = _mask.Length;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="maskText">The mask format that will need to be translated
		/// into a regex</param>
		/// <returns>A regex which will validate the exact 
		/// mask given by the user</returns>
		private string ChangeMaskToRegex (string maskText)
		{
			StringBuilder sb = new StringBuilder();
			string maskInterpretation = "";
			string tempValue = "";
			int counter = 0;
			int startPosition = 0;
			string DateRegex = @"(((([0]?(1|3|5|7|8){1})|([1]{1}(0|2){1})){1}[/]{1}(([0]?[1-9]{1})|([1-2]{1}\d{1})|([3]{1}[0-1]{1})){1})|((([0]?(4|6|9){1})|([1]{1}[1]{1})){1}[/]{1}(([0]?[1-9]{1})|([1-2]{1}\d{1})|([3]{1}[0]{1})){1})|(([0]?[2]{1}){1}[/]{1}(([0]?[1-9]{1})|([1-2]{1}\d{1})){1}))[/]{1}\d{4}";
			
			if(this._isDate)
			{
				maskInterpretation = DateRegex;
			}
			else if(this._isZipCode)
			{
				maskInterpretation = @"\d{5}(-\d{4})?";
			}
			else if(this._isCurrency)
			{
				maskInterpretation = @"^\d*[.]\d{0,2}$";
			}
			else
			{
				for(int i=0;i < maskText.Length;i++)
				{
					counter = 0;
					startPosition = i;
					switch(maskText.Substring(i,1))
					{
						case "9":
							while(maskText.Substring(i,1).Equals("9"))
							{
								counter++;
								i++;
								if(!(i < maskText.Length)) 
									break;
							}
							sb.Append(@"\d{");
							sb.Append(counter.ToString());
							sb.Append("}");
							break;
						case "A":
							while(maskText.Substring(i,1).Equals("A"))
							{
								counter++;
								i++;
								if(!(i < maskText.Length)) 
									break;
							}
							sb.Append(@"[a-zA-Z\d]{");
							sb.Append(counter.ToString());
							sb.Append("}");
							break;
							/*case "C":
								while(maskText.Substring(i,1).Equals("C"))
								{
									counter++;
									i++;
									if(!(i < maskText.Length)) 
										break;
								}
								sb.Append(@"[.\d]{");
								sb.Append(counter.ToString());
								sb.Append("}");
								break;
								/*case "E":
									while(maskText.Substring(i,1).Equals("E"))
									{
										counter++;
										i++;
										if(!(i < maskText.Length)) 
											break;
									}
									sb.Append(@".{");
									sb.Append(counter.ToString());
									sb.Append("}");
									break;*/
						case "X":
							while(maskText.Substring(i,1).Equals("X"))
							{
								counter++;
								i++;
								if(!(i < maskText.Length)) break;
							}
							sb.Append(@"[a-zA-Z]{");
							sb.Append(counter.ToString());
							sb.Append("}");
							break;
						default:
							tempValue = maskText.Substring(startPosition,1);
							while(tempValue.Equals(maskText.Substring(i,1)))
							{
								counter++;
								i++;
								if(!(i < maskText.Length)) break;
							}
							if(tempValue.Equals(@"\"))
							{
								sb.Append(@"[\\]{");
								sb.Append(counter.ToString());
								sb.Append("}");
							}
							else
							{
								sb.Append("[");
								sb.Append(maskText.Substring(startPosition,1));
								sb.Append("]{");
								sb.Append(counter.ToString());
								sb.Append("}");
							}
							break;
					}
					i = startPosition + counter -1;
					/*if(maskText.Substring(i,1).Equals("9"))
					{
						counter = 0;
						startPosition = i;
						while(maskText.Substring(i,1).Equals("9"))
						{
							counter++;
							i++;
							if(!(i < maskText.Length)) 
								break;
						}
						sb.Append(@"\d{");
						sb.Append(counter.ToString());
						sb.Append("}");
						i = startPosition + counter -1;

					}
					else if (maskText.Substring(i,1).Equals("X"))
					{
						counter = 0;
						startPosition = i;
						while(maskText.Substring(i,1).Equals("X"))
						{
							counter++;
							i++;
							if(!(i < maskText.Length)) break;
						}
						sb.Append(@"[a-zA-Z]{");
						sb.Append(counter.ToString());
						sb.Append("}");
						i = startPosition + counter - 1;
					}
					else
					{
						counter = 0;
						startPosition = i;
						tempValue = maskText.Substring(startPosition,1);
						while(tempValue.Equals(maskText.Substring(i,1)))
						{
							counter++;
							i++;
							if(!(i < maskText.Length)) break;
						}
						if(tempValue.Equals(@"\"))
						{
							sb.Append(@"[\\]{");
							sb.Append(counter.ToString());
							sb.Append("}");
						}
						else
						{
							sb.Append("[");
							sb.Append(maskText.Substring(startPosition,1));
							sb.Append("]{");
							sb.Append(counter.ToString());
							sb.Append("}");
						}
						i = startPosition + counter - 1;
					}*/
				}
				maskInterpretation = sb.ToString();
			}

			return(maskInterpretation);
		}

		private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			MaskedTextBox sd = (MaskedTextBox) sender;
			if (_isCurrency)
			{
				this.MaskDecimal(e);
			}
		}

		private void MaskDecimal(System.Windows.Forms.KeyPressEventArgs e)
		{
			if(Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
			{
				e.Handled = false;
				//errorProvider1.SetError(this, string.Empty);
			}
			else
			{
				e.Handled = true;
				//errorProvider1.SetError(this, GetStr("ONLYDIGITANDDOT"));
			}
		}
	}
	/// <summary>
	/// Copyright 2002 Softek PR.<br/>
	/// <fileName>MaskedTextBox.cs</fileName><br/>
	/// <classDescription>
	/// This class declares a literal that will contain the javascript 
	/// functions needed for the maskedtextbox to work.
	/// </classDescription><br/>
	/// <author>Javier J. Vega Caro</author><br/>
	/// <modifiedBy date=""></modifiedBy><br/>
	/// <version>1.0</version><br/>
	/// <copyrightInfo>
	/// The code included in this source file may not be used or modified 
	/// in any way without the written consent of Softek PR.
	/// </copyrightInfo><br/>
	/// </summary>
	public class MaskedTextHeader: Literal
	{
		/// <summary>
		/// When the control is loaded this function makes sure that the
		/// literal contains the javascript function.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(System.EventArgs e)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("<script language='javascript'>");
			sb.Append("function MaskIt(InputTxt,Format,detect){var invalid = true;");
			sb.Append("var temp = \"\";if(detect){if(InputTxt.value.length>=Format.length)");
			sb.Append("{window.event.keyCode = 0;return;}");
			sb.Append("var keypressed = window.event.keyCode;window.event.keyCode = 0;");
			sb.Append("if (InputTxt.createTextRange){");
			sb.Append("InputTxt.caretPos = document.selection.createRange().duplicate();}");
			sb.Append("if (InputTxt.createTextRange && InputTxt.caretPos){");
			sb.Append("var caretPos = InputTxt.caretPos;caretPos.text =");
			sb.Append("caretPos.text.charAt(caretPos.text.length - 1) == ' ' ?");
			sb.Append("String.fromCharCode(keypressed) + ' ' : ");
			sb.Append("String.fromCharCode(keypressed);}");
			sb.Append("else{InputTxt.value  = String.fromCharCode(keypressed);}}var tr;");
			sb.Append("var p;var dateIncrement = false;var increase = false;var dotted = false;");
			sb.Append("var decrease = false;var actValue;var myString;var inserted = false;");
			sb.Append("var c=0;");  // decimal point counter
			sb.Append("if(detect){if (InputTxt.createTextRange){");
			sb.Append("tr = InputTxt.createTextRange();");
			sb.Append("tr.caretPos = document.selection.createRange().duplicate();");
			sb.Append("tr.caretPos.text = String.fromCharCode(247);");
			sb.Append("tr.caretPos.text = String.fromCharCode(247);");
			sb.Append("actValue=InputTxt.value.replace(String.fromCharCode(247),'');");
			sb.Append("val = InputTxt.value;for(p=0;p<val.length;p++){");
			sb.Append("if(val.charAt(p)!=actValue.charAt(p))break;}}}");
			sb.Append("if (InputTxt.value.replace( /^\\s*/, \"\" ) == \"\"){return;}");
			sb.Append("else{for(i=0,j=0;j<Format.length;i++,j++){");
			sb.Append("if((InputTxt.value.length-1)<i)break;switch(Format.slice(j,j+1))");
			sb.Append("{case \"X\":if ((((InputTxt.value.charCodeAt(i) >= 65) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 90))||");
			sb.Append("((InputTxt.value.charCodeAt(i) >= 97) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 122))||");
			sb.Append("(InputTxt.value.charCodeAt(i)==209)||");
			sb.Append("(InputTxt.value.charCodeAt(i)==241)))");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);}else{j--;}break;case \"9\":");
			sb.Append("if(((InputTxt.value.charCodeAt(i) >= 48) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 57)))");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);}else{j--;}break;case \"A\":");
			sb.Append("if((((InputTxt.value.charCodeAt(i) >= 48) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 57)))||");
			sb.Append("((((InputTxt.value.charCodeAt(i) >= 65) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 90))||");
			sb.Append("((InputTxt.value.charCodeAt(i) >= 97) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 122))||");
			sb.Append("(InputTxt.value.charCodeAt(i)==209)||");
			sb.Append("(InputTxt.value.charCodeAt(i)==241))))");
			//sb.Append("{temp += InputTxt.value.slice(i,i+1);}else{j--;}	break;");
			//sb.Append("case \"E\":temp += InputTxt.value.slice(i,i+1);break;case \"Z\":");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);}else{j--;}	break;case \"Z\":");
			sb.Append("if ((InputTxt.value.slice(i,i+1) == \"-\")&&(detect))");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);inserted = true;}");
			sb.Append("else if((temp.length==j)&&(((InputTxt.value.charCodeAt(i) >= 48)");
			sb.Append("&& (InputTxt.value.charCodeAt(i) <= 57))))");
			sb.Append("{temp += \"-\" + InputTxt.value.slice(i,i+1);inserted = true;}");
			sb.Append("else{j--;}break;case \"C\":if (InputTxt.value.slice(i,i+1) == \".\")");
			sb.Append("{if(!dotted){temp += InputTxt.value.slice(i,i+1);dotted = true;}else{j--;}}");
			sb.Append("else if((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 57))");
			//			sb.Append("{temp += InputTxt.value.slice(i,i+1);}else{j--;}break;");
			// Added Currency Support
			sb.Append("{if(!dotted){temp += InputTxt.value.slice(i,i+1);}else{");
			sb.Append("if (c < 2)");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);}else{j--;}c++;}}else{j--;}break;");
			
			sb.Append("case \"D\":switch(j){case 0:");
			sb.Append("if(((InputTxt.value.charCodeAt(i) >= 50) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 57)))");
			sb.Append("{if(p>=(InputTxt.value.length-1)){dateIncrement = true;}temp = 0;");
			sb.Append("i--;}else if(((InputTxt.value.charCodeAt(i) >= 48) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 49))){");
			sb.Append("if(p>=(InputTxt.value.length-1)){dateIncrement = true;}else");
			sb.Append("{decrease = true;}temp = InputTxt.value.slice(i,i+1);}else");
			sb.Append("{j--;}break;case 1:");
			sb.Append("if(((InputTxt.value.charCodeAt(i) >= 48) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 57)))");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);}");
			sb.Append("else if((InputTxt.value.charCodeAt(i) == 47)&&(temp.length==i))");
			sb.Append("{temp = \"0\" + temp;}else{j--;}");
			sb.Append("if(p<(InputTxt.value.length-1)){if(i==0){dateIncrement = true;");
			sb.Append("i+=2;}}break;case 3:if(((InputTxt.value.charCodeAt(i) >= 52) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 57))){dateIncrement = true;");
			sb.Append("if(p==4){temp += 0;increase = true;i--;}else{");
			sb.Append("temp += InputTxt.value.slice(i,i+1);}}");
			sb.Append("else if(((InputTxt.value.charCodeAt(i) >= 48) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 51)))");
			sb.Append("{dateIncrement = true;temp += InputTxt.value.slice(i,i+1);}");
			sb.Append("else{j--;}break;case 4:");
			sb.Append("if(((InputTxt.value.charCodeAt(i) >= 48) && ");
			sb.Append("(InputTxt.value.charCodeAt(i) <= 57)))");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);}");
			sb.Append("else if((InputTxt.value.charCodeAt(i) == 47)&&(i==temp.length)&&((p==6)||(p==5)||(p==10))){");
			sb.Append("temp = temp.slice(0,i-1) + \"0\" + temp.slice(i-1,i);}");
			sb.Append("else{j--;}if((p<(InputTxt.value.length-1))&&(i==3))");
			sb.Append("{dateIncrement = true;i++;}break;}break;default:");
			sb.Append("if (InputTxt.value.slice(i,i+1) == Format.slice(j,j+1))");
			sb.Append("{temp += InputTxt.value.slice(i,i+1);inserted = true;}");
			sb.Append("else if(temp.length==j){temp += Format.slice(j,j+1);");
			sb.Append("inserted = true;i--;}else{j--;}break;}}}");
			sb.Append("if (temp.length > Format.length)");
			sb.Append("{temp = temp.slice(0,Format.length);}");
			sb.Append("if(temp!=InputTxt.value){InputTxt.value = temp;}");
			sb.Append("if(detect){if(p<(InputTxt.value.length-1))p--;");
			sb.Append("if(dateIncrement){if(p<7){p++;if(inserted)p++;");
			sb.Append("if(increase)p++;if(decrease)p--;");
			sb.Append("if(((p==2)&&(InputTxt.value.slice(2,3)==\"/\"))||((p==5)");
			sb.Append("&&(InputTxt.value.slice(5,6)==\"/\")))p++;}}else");
			sb.Append("{if(inserted)p++;}tr.moveStart('character',parseInt(p));");
			sb.Append("tr.collapse();tr.select();}}");
			sb.Append("</script>");

			this.Text = sb.ToString();
		}

		//		private void MaskIt(InputTxt,Format,detect)
		//		{
		//			var invalid = true;
		//			var temp = "";
		//			if(detect)
		//			{
		//				if(InputTxt.value.length>=Format.length)
		//				{
		//					window.event.keyCode = 0;
		//					return;
		//				}
		//				var keypressed = window.event.keyCode;
		//				window.event.keyCode = 0;
		//				if (InputTxt.createTextRange)
		//				{
		//						InputTxt.caretPos = document.selection.createRange().duplicate();
		//				}
		//				if (InputTxt.createTextRange && InputTxt.caretPos)
		//				{
		//					var caretPos = InputTxt.caretPos;
		//					caretPos.text  = caretPos.text.charAt(caretPos.text.length - 1)  ==  ' ' ? String.fromCharCode(keypressed)  +  ' '  :  String.fromCharCode(keypressed);
		//
		//				}
		//				else
		//				{
		//					InputTxt.value  = String.fromCharCode(keypressed);
		//				}
		//
		//			}
		//			var tr;
		//			var p;
		//			var dateIncrement = false;
		//			var increase = false;
		//			var dotted = false;
		//			var decrease = false;
		//			var actValue;
		//			var myString;
		//			var inserted = false;
		//			var c=0;
		//			if(detect)
		//			{
		//				if (InputTxt.createTextRange)
		//				{
		//					tr = InputTxt.createTextRange();
		//					tr.caretPos = document.selection.createRange().duplicate();
		//					tr.caretPos.text = String.fromCharCode(247);
		//					tr.caretPos.text = String.fromCharCode(247);
		//					actValue=InputTxt.value.replace(String.fromCharCode(247),"");
		//					val = InputTxt.value;
		//					for(p=0; p<val.length; p++)
		//					{
		//						if(val.charAt(p)!=actValue.charAt(p))
		//							break;
		//
		//					}
		//
		//				}
		//
		//			}
		//			if (InputTxt.value.replace( /^\\s*/, "" ) == "")
		//			{
		//				return;
		//
		//			}
		//			else
		//			{
		//				for(i=0,j=0;j<Format.length; i++,j++)
		//				{
		//					if((InputTxt.value.length-1)<i)
		//						break;
		//					switch(Format.slice(j,j+1))
		//					{
		//						case "X":
		//							if ((((InputTxt.value.charCodeAt(i) >= 65) && (InputTxt.value.charCodeAt(i) <= 90))||((InputTxt.value.charCodeAt(i) >= 97) && (InputTxt.value.charCodeAt(i) <= 122))||(InputTxt.value.charCodeAt(i)==209)||(InputTxt.value.charCodeAt(i)==241)))
		//							{
		//								temp += InputTxt.value.slice(i,i+1);
		//
		//							}
		//							else
		//							{
		//								j--;
		//
		//							}
		//							break;
		//						case "9":if(((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 57)))
		//								 {
		//									 temp += InputTxt.value.slice(i,i+1);
		//
		//								 }
		//								 else
		//								 {
		//									 j--;
		//
		//								 }
		//							break;
		//						case "A":if((((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 57)))||((((InputTxt.value.charCodeAt(i) >= 65) && (InputTxt.value.charCodeAt(i) <= 90))||((InputTxt.value.charCodeAt(i) >= 97) && (InputTxt.value.charCodeAt(i) <= 122))||(InputTxt.value.charCodeAt(i)==209)||(InputTxt.value.charCodeAt(i)==241))))
		//								 {
		//									 temp += InputTxt.value.slice(i,i+1);
		//
		//								 }
		//								 else
		//								 {
		//									 j--;
		//
		//								 }
		//							break;
		//						case "Z":if ((InputTxt.value.slice(i,i+1) == "-")&&(detect))
		//								 {
		//									 temp += InputTxt.value.slice(i,i+1);
		//									 inserted = true;
		//
		//								 }
		//								 else if((temp.length==j)&&(((InputTxt.value.charCodeAt(i) >= 48)&& (InputTxt.value.charCodeAt(i) <= 57))))
		//								 {
		//									 temp += "-" + InputTxt.value.slice(i,i+1);
		//									 inserted = true;
		//
		//								 }
		//								 else
		//								 {
		//									 j--;
		//
		//								 }
		//							break;
		//						case "C":
		//								 if (InputTxt.value.slice(i,i+1) == ".")
		//								 {
		//									 if(!dotted)
		//									 {
		//										 temp += InputTxt.value.slice(i,i+1);
		//										 dotted = true;
		//									 }
		//									 else
		//									 {
		//										 j--;
		//
		//									 }
		//
		//								 }
		//								 else if((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 57))
		//								 {
		//									 if(!dotted)
		//										 temp += InputTxt.value.slice(i,i+1);
		//									 else
		//									 {
		//										 if (c < 2)
		//											 temp += InputTxt.value.slice(i,i+1);
		//										 else
		//											 j--;
		//										 }
		//										 c++;
		//									 }
		//								 }
		//								 else
		//								 {
		//									 j--;
		//
		//								 }
		//							break;
		//						case "D":switch(j)
		//								 {
		//									 case 0:if(((InputTxt.value.charCodeAt(i) >= 50) && (InputTxt.value.charCodeAt(i) <= 57)))
		//											{
		//												if(p>=(InputTxt.value.length-1))
		//												{
		//													dateIncrement = true;
		//
		//												}
		//												temp = 0;
		//												i--;
		//
		//											}
		//											else if(((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 49)))
		//											{
		//												if(p>=(InputTxt.value.length-1))
		//												{
		//													dateIncrement = true;
		//
		//												}
		//												else
		//												{
		//													decrease = true;
		//
		//												}
		//												temp = InputTxt.value.slice(i,i+1);
		//
		//											}
		//											else
		//											{
		//												j--;
		//
		//											}
		//										 break;
		//									 case 1:if(((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 57)))
		//											{
		//												temp += InputTxt.value.slice(i,i+1);
		//
		//											}
		//											else if((InputTxt.value.charCodeAt(i) == 47)&&(temp.length==i))
		//											{
		//												temp = "0" + temp;
		//
		//											}
		//											else
		//											{
		//												j--;
		//
		//											}
		//										 if(p<(InputTxt.value.length-1))
		//										 {
		//											 if(i==0)
		//											 {
		//												 dateIncrement = true;
		//												 i+=2;
		//
		//											 }
		//
		//										 }
		//										 break;
		//									 case 3:if(((InputTxt.value.charCodeAt(i) >= 52) && (InputTxt.value.charCodeAt(i) <= 57)))
		//											{
		//												dateIncrement = true;
		//												if(p==4)
		//												{
		//													temp += 0;
		//													increase = true;
		//													i--;
		//
		//												}
		//												else
		//												{
		//													temp += InputTxt.value.slice(i,i+1);
		//
		//												}
		//
		//											}
		//											else if(((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 51)))
		//											{
		//												dateIncrement = true;
		//												temp += InputTxt.value.slice(i,i+1);
		//
		//											}
		//											else
		//											{
		//												j--;
		//
		//											}
		//										 break;
		//									 case 4:if(((InputTxt.value.charCodeAt(i) >= 48) && (InputTxt.value.charCodeAt(i) <= 57)))
		//											{
		//												temp += InputTxt.value.slice(i,i+1);
		//
		//											}
		//											else if((InputTxt.value.charCodeAt(i) == 47)&&(i==temp.length)&&((p==6)||(p==5)||(p==10)))
		//											{
		//												temp = temp.slice(0,i-1) + "0" + temp.slice(i-1,i);
		//
		//											}
		//											else
		//											{
		//												j--;
		//
		//											}
		//										 if((p<(InputTxt.value.length-1))&&(i==3))
		//										 {
		//											 dateIncrement = true;
		//											 i++;
		//
		//										 }
		//										 break;
		//
		//								 }
		//							break;
		//						default:if (InputTxt.value.slice(i,i+1) == Format.slice(j,j+1))
		//								{
		//									temp += InputTxt.value.slice(i,i+1);
		//									inserted = true;
		//
		//								}
		//								else if(temp.length==j)
		//								{
		//									temp += Format.slice(j,j+1);
		//									inserted = true;
		//									i--;
		//
		//								}
		//								else
		//								{
		//									j--;
		//
		//								}
		//							break;
		//
		//					}
		//
		//				}
		//
		//			}
		//			if (temp.length > Format.length)
		//			{
		//				temp = temp.slice(0,Format.length);
		//
		//			}
		//			if(temp!=InputTxt.value)
		//			{
		//				InputTxt.value = temp;
		//
		//			}
		//			if(detect)
		//			{
		//				if(p<(InputTxt.value.length-1))p--;
		//				if(dateIncrement)
		//				{
		//					if(p<7)
		//					{
		//						p++;
		//						if(inserted)p++;
		//						if(increase)p++;
		//						if(decrease)p--;
		//						if(((p==2) && (InputTxt.value.slice(2,3)=="/")) || ((p==5) && (InputTxt.value.slice(5,6) == "/")))
		//							p++;
		//
		//					}
		//				}
		//				else
		//				{
		//					if(inserted)
		//						p++;
		//				}
		//				tr.moveStart("character",parseInt(p));
		//				tr.collapse();
		//				tr.select();
		//			}
		//		}// end maskit()
		//
	}
}



