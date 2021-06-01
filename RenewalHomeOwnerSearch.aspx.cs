using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPolicy;
using EPolicy.Customer;
using EPolicy.TaskControl;
using Baldrich.DBRequest;
using OPPReport;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using EPolicy.Quotes;
using EPolicy.XmlCooker;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Xml.Schema;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using EPolicy.TaskControl;


public partial class RenewalHomeOwnerSearch : System.Web.UI.Page
{
    private string Querystring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Control Banner = new Control();
        Banner = LoadControl(@"TopBannerNew.ascx");
        this.phTopBanner.Controls.Add(Banner);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckHomeOwnersExistPPS() == true && GetPPSCancelledPolicy() == true)
            {
                lblRecHeader.Text = "This policy is cancelled. Please try again.";
                mpeSeleccion.Show();
            }
            else if (CheckHomeOwnersExistPPS() == true && GetPPSHasEndorsementHO() == true)
            {
                if (GetLastPolicyFromPPS())
                {
                    throw new Exception("This policy has more than 45 days. Please make a new policy.");
                }
                else
                {
                    string destinationURL = "";
                    destinationURL = "HomeOwners.aspx" + "?PolicyIDPPS=";
                    if (txtPolicySuffix.Text.Trim() == "" || txtPolicySuffix.Text.Trim() == "00")
                    {
                        Querystring = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + "00";
                    }
                    else
                    {
                        Querystring = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + txtPolicySuffix.Text.Trim();
                    }
                    Response.Redirect(destinationURL + Querystring, false);
                }
            }
            else if (CheckHomeOwnersExistePPS() == true)
            {
                if (GetLastPolicyFromPPS())
                {
                    throw new Exception("This policy has more than 45 days. Please make a new policy.");
                }
                else
                {
                    EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                    int userID = 0;

                    try
                    {
                        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(
                            "Could not parse user id from cp.Identity.Name.", ex);
                    }
                    string destinationURL = "";
                    destinationURL = "HomeOwners.aspx" + "?PolicyIDePPS=";
                    TaskControl taskControl = TaskControl.GetTaskControlByTaskControlID(GetTaskControlByPolicyTypeAndPolicyNoAndSuffix(ddlPolicyType.SelectedItem.Text, txtPolicyNo.Text.Trim(), txtPolicySuffix.Text.Trim()), userID);
                    Customer customer = Customer.GetCustomer(taskControl.CustomerNo, userID);
                    if (Session["Customer"] == null)
                        Session.Add("Customer", customer);
                    else
                        Session["Customer"] = customer;
                    if (txtPolicySuffix.Text.Trim() == "" || txtPolicySuffix.Text.Trim() == "00")
                    {
                        Querystring = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + "00";
                    }
                    else
                    {
                        Querystring = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + txtPolicySuffix.Text.Trim();
                    }
                    Response.Redirect(destinationURL + Querystring, false);
                }
            }
            else if (CheckHomeOwnersExistPPS() == true)
            {
                if (GetLastPolicyFromPPS())
                {
                    throw new Exception("This policy has more than 45 days. Please make a new policy.");
                }
                else
                {
                    string destinationURL = "";
                    destinationURL = "HomeOwners.aspx" + "?PolicyIDPPS=";
                    if (txtPolicySuffix.Text.Trim() == "" || txtPolicySuffix.Text.Trim() == "00")
                    {
                        Querystring = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + "00";
                    }
                    else
                    {
                        Querystring = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + txtPolicySuffix.Text.Trim();
                    }
                    Response.Redirect(destinationURL + Querystring, false);
                }
            }
            else
            {
                lblRecHeader.Text = "Policy not found. Please try again.";
                mpeSeleccion.Show();
            }
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }
    }

    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Search.aspx");
    }

    protected bool CheckHomeOwnersExistPPS()
    {
        try
        {
            SqlConnection cn = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            //SqlConnection cn = new SqlConnection(@"Data Source=192.168.1.22\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            System.Data.DataTable table = new System.Data.DataTable();

            using (var con = cn)
            {
                using (var cmd = new SqlCommand("sproc_ConsumeXMLePPS-HOMEOWNER_Verify", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    if (txtPolicySuffix.Text.Trim() == "" || txtPolicySuffix.Text.Trim() == "00")
                    {
                        cmd.Parameters.AddWithValue("PolicyID", ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("PolicyID", ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + txtPolicySuffix.Text.Trim());
                    }
                    da.Fill(table);
                }
            }

            if (table.Rows.Count > 0)
            {
               return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
            return false;
        }
    }

    protected bool CheckHomeOwnersExistePPS()
    {
        try
        {
            if (GetTaskControlByPolicyTypeAndPolicyNoAndSuffix(ddlPolicyType.SelectedItem.Text, txtPolicyNo.Text.Trim(), txtPolicySuffix.Text.Trim()) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
            return false;
        }
        
    }

    private bool GetPPSHasEndorsementHO()
    {
        string Message = "";
        bool HasEndorment = false;

        try
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            //string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
            //SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            System.Data.DataTable PPSPolicy = new System.Data.DataTable();
            System.Data.DataTable dt = new DataTable();

            cmd.CommandText = "sproc_CheckHasEndorsement";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            cmd.Parameters.Clear();
            string PolicyNo;
            if (txtPolicySuffix.Text.Trim() == "00" || txtPolicySuffix.Text.Trim() == "")
            {
                PolicyNo = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim();
            }
            else
            {
                PolicyNo = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + txtPolicySuffix.Text.Trim();
            }
            cmd.Parameters.AddWithValue("@PolicyID", PolicyNo);

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(PPSPolicy);

            sqlConnection1.Close();

            if (PPSPolicy.Rows.Count > 0)
            {
                //Validar si en PPS hay endoso.
                if (PPSPolicy.Rows[0][0].ToString() == "ENDOSO")
                {
                    Message = "This Policy already has endorsement in PPS any further endorsements must be done from PPS.";
                    HasEndorment = true;
                }
            }
            else
            {
                Message = "Error Checking Endorsment in PPS, Please Try Again.";
                HasEndorment = false;
            }
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }

        return HasEndorment;
    }

    private bool GetPPSCancelledPolicy()
    {
        string Message = "";
        bool HasCancelled = false;

        try
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            //string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
            //SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            System.Data.DataTable PPSPolicy = new System.Data.DataTable();
            System.Data.DataTable dt = new DataTable();

            cmd.CommandText = "sproc_CheckPolicyCancelled";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            cmd.Parameters.Clear();
            string PolicyNo;
            if (txtPolicySuffix.Text.Trim() == "00" || txtPolicySuffix.Text.Trim() == "")
            {
                PolicyNo = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim();
            }
            else
            {
                PolicyNo = ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + txtPolicySuffix.Text.Trim();
            }
            cmd.Parameters.AddWithValue("@PolicyID", PolicyNo);

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(PPSPolicy);

            sqlConnection1.Close();

            if (PPSPolicy.Rows.Count > 0)
            {
                //Validar si en PPS esta cancelada.
                if (PPSPolicy.Rows[0][0].ToString() == "CANCELLED")
                {
                    Message = "This Policy is Cancelled in PPS, Please verify.";
                    HasCancelled = true;
                }
            }
            else
            {
                Message = "Error Checking if the policy is cancelled in PPS, Please Try Again.";
                HasCancelled = false;
            }
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }

        return HasCancelled;
    }

    private int GetTaskControlByPolicyTypeAndPolicyNoAndSuffix(string policyType,string policyNo, string suffix)
    {
        if (suffix == "")
        {
            suffix = "00";
        }
        DataTable dt = new DataTable();

        DBRequest Executor = new DBRequest();

        try
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("PolicyType",
                            SqlDbType.VarChar, 3, policyType.ToString(),
                            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyNo",
                            SqlDbType.VarChar, 11, policyNo.ToString(),
                            ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Sufijo",
                            SqlDbType.VarChar, 2, suffix.ToString(),
                            ref cookItems);


            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
            dt = Executor.GetQuery("GetTaskControlByPolicyNoAndSuffix", xmlDoc);
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0]["TaskControlID"].ToString());
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items.", ex);
        } 
    }

    private bool GetLastPolicyFromPPS()
    {
        bool Result = false;
        EPolicy.TaskControl.TaskControl taskControl = (EPolicy.TaskControl.TaskControl)Session["TaskControl"];
        DataTable dt = new DataTable();
        SqlConnection con = null;
        try
        {
            //string connection = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();//@"Data Source=ASPIREJ;Initial Catalog=ClaimNext;User ID=sa;password=sqlserver;";
            con = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            //con = new SqlConnection(@"Data Source=192.168.1.22\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            //con = new SqlConnection(connection);
            con.Open();
            using (SqlCommand cmd = new SqlCommand("sproc_Consume_ePPS_LastPolicy", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //cmd.Parameters.Add("@PolicyID", SqlDbType.VarChar).Value = txtPolicyNo.Text.ToString().Trim();
                if (txtPolicySuffix.Text.Trim() == "" || txtPolicySuffix.Text.Trim() == "00")
                {
                    cmd.Parameters.AddWithValue("PolicyID", ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-00" );
                }
                else
                {
                    cmd.Parameters.AddWithValue("PolicyID", ddlPolicyType.SelectedItem.Text + txtPolicyNo.Text.Trim() + "-" + txtPolicySuffix.Text.Trim());
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string expdt = dt.Rows[0]["Expire"].ToString();
                    string[] arrayPolicytoRenew;
                    arrayPolicytoRenew = dt.Rows[0]["PolicyID"].ToString().Trim().Split('-');
                    if (arrayPolicytoRenew.Length > 1)
                    {
                        txtPolicyNo.Text = arrayPolicytoRenew[0].ToString().Replace("HOM","").Replace("INC","");
                        txtPolicySuffix.Text = arrayPolicytoRenew[1];
                    }
                    else
                    {
                        txtPolicyNo.Text = arrayPolicytoRenew[0].ToString().Replace("HOM", "").Replace("INC", "");
                    }
                    int totaldays = int.Parse(Math.Round((DateTime.Now - DateTime.Parse(expdt)).TotalDays, 0).ToString());
                    if ((DateTime.Now - DateTime.Parse(expdt)).TotalDays > 45)
                        Result = true;
                }
                con.Close();
            }
        }
        catch (Exception exp)
        {
            con.Close();
        }
        return Result;
    }


}