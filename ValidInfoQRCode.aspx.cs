using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EncryptClass;
using System.Data;
using EPolicy.XmlCooker;
using Baldrich.DBRequest;
using System.Xml;

public partial class ValidInfoQRCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Tc"] != null)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage2.Text = "";

                EncryptClass.EncryptClass enc = new EncryptClass.EncryptClass();
                string[] value = new string[2];
                string decrypt = enc.Decrypt(Request.QueryString["Tc"].Trim());
                value = decrypt.Split('&');
                string tcid = value[0];
                string type = value[1];


                DataTable dt = GetValidatInfoForQRCode(int.Parse(tcid), type);

                if (dt.Rows.Count > 0)
                {
                    if (type == "p")
                    {
                        if ((DateTime)dt.Rows[0]["EffectiveDate"] <= DateTime.Now && (DateTime)dt.Rows[0]["ExpirationDate"] >= DateTime.Now)
                        {
                            lblMessage.Text = "The Policy is Active and Valid.";
                        }
                        else
                        {
                            lblMessage.Text = "The Policy is Inactive and Invalid.";
                            lblMessage2.Text = "Please Contact with your Insurance representative.";
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 0) //&& DateTime.Parse(dt.Rows[0]["CloseDate"].ToString()) >= DateTime.Now)
                        {
                            lblMessage.Text = "The Yacht Quote is Valid.";
                        }
                        else
                        {
                            lblMessage.Text = "The Yacht Quote is Invalid.";
                            lblMessage2.Text = "Please Contact with your Insurance representative.";
                        }
                    
                    }
                }
            }
            catch (Exception exc)
            {
                lblMessage.Text = "Error in your Information. Please Try again.";
            }
        }
        else
        {
            lblMessage.Text = "Invalid QR Code Information. Please Try again.";
        }


    }

    private static System.Data.DataTable GetValidatInfoForQRCode(int TaskControlID, string type)
    
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

        DbRequestXmlCooker.AttachCookItem("TaskControlID",
            SqlDbType.Int, 0, TaskControlID.ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("Type",
            SqlDbType.VarChar, 5, type.ToString(),
            ref cookItems);



        DBRequest exec = new DBRequest();
        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items.", ex);
        }
        System.Data.DataTable dt = null;
        try
        {
            dt = exec.GetQuery("GetValidatInfoForQRCode", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve data from database.", ex);
        }

    }
}