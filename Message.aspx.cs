using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["MsgHeader"] != null && Session["MsgDetail"] != null)
                {
                    lblHeader.Text = Session["MsgHeader"].ToString();
                    lblMessage.Text = Session["MsgDetail"].ToString();
                    lblMessage2.Text = Session["MsgDetail2"].ToString();
                    Session.Remove("MsgHeader");
                    Session.Remove("MsgDetail");
                    Session.Remove("MsgDetail2");
                }
                else
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
                    Response.Redirect("Default.aspx?001");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Session["FromUI"] != null)
        {
            string fromui = (string)Session["FromUI"];
            Session.Remove("FromUI");
            Response.Redirect(fromui, true);
        }
        else
        {
            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
            Response.Cookies.Add(authCookies);
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx?001");
        }
    }
}