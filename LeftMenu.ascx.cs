namespace EPolicy.EPolicyWeb.Components
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using EPolicy;

    /// <summary>
    ///		Summary description for MenuTaskControl.
    /// </summary>
    public partial class MenuTaskControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            if (cp == null)
            {
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                //VerifyAccess(cp);
            }
        }



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }
}
