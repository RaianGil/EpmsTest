using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace UtilityComponents
{
	/// <summary>
	/// Summary description for DefaultEnterControl.
	/// </summary>
	
	[DefaultProperty("Text"),
	ToolboxData("<{0}:DefaultEnterControl runat=server></{0}:DefaultEnterControl>")]
	public class DefaultEnterControl : System.Web.UI.Control
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
					script += @"<script type=""text/javascript"" event=""onkeyup"" for=""document"">"+
						@"if(window.event.keyCode == 13)"+
						@"{"+
							@"document.getElementById("""+ 
							this.BoundControlID+
							@""").click();"+
						@"}"+
						@"</script>";
					this.Page.RegisterStartupScript("DefaultEnterControl",script);
				}
			}
		}
	}
}
