using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using EPolicy.LookupTables;
using EPolicy.XmlCooker;
using System.Xml;

public partial class CertificateFileUpload : System.Web.UI.Page
{

    #region Protected Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        this.litPopUp.Visible = false;
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

        Control Banner = new Control();
        Banner = LoadControl(@"TopBanner.ascx");
        this.phTopBanner.Controls.Add(Banner);

        //Setup Left-side Banner			
        //Control LeftMenu = new Control();
        //LeftMenu = LoadControl(@"LeftMenu.ascx");
        //phTopBanner1.Controls.Add(LeftMenu);

        int userID = 0;

        if (cp == null)
        {
            Response.Redirect("Default.aspx?001");
        }
        else
        {
            if (!cp.IsInRole("SRO DASHBOARD ADMIN") && !cp.IsInRole("SRO DASHBOARD UPLOADER") && !cp.IsInRole("ADMINISTRATOR"))
            {
                Response.Redirect("Default.aspx?001");
            }
        }

        if (cp != null)
        {
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
        }

        if (!IsPostBack)
        {
            DataTable dtMonth = EPolicy.LookupTables.LookupTables.GetTable("DashboardMonthsForClosing");
            DataTable dtYear = EPolicy.LookupTables.LookupTables.GetTable("DashboardYearsForClosing");

            //Month
            ddlMonth.DataSource = dtMonth;
            ddlMonth.DataTextField = "MonthName";
            ddlMonth.DataValueField = "MonthNo";
            ddlMonth.DataBind();
            ddlMonth.SelectedIndex = -1;
            ddlMonth.Items.Insert(0, "");

            //Year
            ddlYear.DataSource = dtYear;
            ddlYear.DataTextField = "YEAR";
            ddlYear.DataValueField = "YEAR";
            ddlYear.DataBind();
            ddlYear.SelectedIndex = -1;
            ddlYear.Items.Insert(0, "");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

        try
        {
            if (cp.IsInRole("SRO DASHBOARD UPLOADER") || cp.IsInRole("SRO DASHBOARD ADMIN"))
            {

                if (FileUpload1.PostedFile.FileName == "")
                {
                    throw new Exception("Please select a file with browse button.");
                }

                if (Path.GetFileName(FileUpload1.PostedFile.FileName) != "")  //Si no existe file no hace ninguna funcion.
                {
                    if (FileUpload1.PostedFile != null)
                    {
                        string newnm = Path.GetFileName(FileUpload1.PostedFile.FileName);

                        //FileUpload1.PostedFile.SaveAs(@"C:\\Inetpub\\wwwroot\\Guardian_ePMS\\ExportFiles\\" + newnm);
                        FileUpload1.PostedFile.SaveAs(@"F:\\\\inetpub\\\\wwwroot\\\\Epms\\\\ExportFiles\\\\" + newnm);

                        //ReviewFile(newnm);
                        ProcessFile(newnm);
                    }
                    string attachedFile = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    this.litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString("File loaded and processed.");
                    this.litPopUp.Visible = true;
                }
            }
            else
            {
                throw new Exception("You do not have permission to upload files.");
            }
        }
        catch (Exception exp)
        {
            this.litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString("" + exp.Message);
            this.litPopUp.Visible = true;
            return;
        }
    }

    protected void btnIsClosed_Click(object sender, EventArgs e)
    {
        this.litPopUp.Visible = false;
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

        bool 閉じて = false;

        if (btnIsClosed.Text.StartsWith("Open"))
            閉じて = false;
        else
            閉じて = true;

        if (!cp.IsInRole("SRO DASHBOARD ADMIN"))
        {
            //throw new Exception("You do not have permission to Open nor Close existing data.");
            this.litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString("You do not have permission to Open nor Close existing data.");
            this.litPopUp.Visible = true;

            Literal Lit = new Literal();
            
            Lit.ID = "LitPopUp";
            Lit.Text = "You do not have permission to Open nor Close existing data.";
            
            UpdatePanel1.ContentTemplateContainer.Controls.Add(Lit);
            Lit.Visible = true;
            return;
        }

        UpdateDashboardClosedFiles(int.Parse(ddlMonth.SelectedItem.Value), int.Parse(ddlYear.SelectedItem.Value), 閉じて);

        ddlMonth_SelectedIndexChanged(sender, e);
    }

    protected void BtnExit_Click(object sender, EventArgs e)
    {
        RemoveSessionLookUp();
        Session.Clear();
        Response.Redirect("SearchPolicies.aspx");
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.litPopUp.Visible = false;

        if (ddlMonth.SelectedItem.Text != "" && ddlYear.SelectedItem.Text != "")
        {
            btnIsClosed.Visible = true;

            DataTable dt = GetIfClosed(int.Parse(ddlMonth.SelectedItem.Value), int.Parse(ddlYear.SelectedItem.Value));

            if (dt.Rows.Count > 0)
            {
                bool IsClosed = (bool)dt.Rows[0]["IsClosed"];

                if (IsClosed)
                    btnIsClosed.Text = "Open " + ddlMonth.SelectedItem.Text.Trim() + " " + ddlYear.SelectedItem.Text.Trim();
                else
                    btnIsClosed.Text = "Close " + ddlMonth.SelectedItem.Text.Trim() + " " + ddlYear.SelectedItem.Text.Trim();
            }
            else
            {
                btnIsClosed.Visible = false;
                //throw new Exception("The specific period chosen does not exists in the system.");
                this.litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString("The specific period chosen does not exists in the system.");
                this.litPopUp.Visible = true;

                return;
            }
        }
        else
        {
            btnIsClosed.Visible = false;
        }
    }

    #endregion

    #region Private Methods

    private void ProcessFile(string newnm)
    {
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

        //StreamReader sr = File.OpenText(@"C:\\Inetpub\\wwwroot\\Guardian_ePMS\\ExportFiles\\" + newnm);
        StreamReader sr = File.OpenText(@"F:\\\\inetpub\\\\wwwroot\\\\Epms\\\\ExportFiles\\\\" + newnm);
        string line = "";

        long stream = sr.BaseStream.Length;
        ///Esta linea lee la primera linea, usualmente los headers. Está aquí para que lea los headers y no se guarden. 元先生
        line = sr.ReadLine();
        for (int i = 1; i <= 100000; i++)
        {
            line = sr.ReadLine();
            line = line.Replace('"', ' ');
            //line = line.Replace(" ", "");

            string[] line2 = line.Split(',');
            
            ///This conditional is only used to Verify if Data already exists in the Database
            ///If exists, it'll be deleted if user meets required permissions
            ///If not, data wont be deleted
            ///IF no data exists beforehand, user will freely add it
            if (i == 1)
            {

                DataTable dt = GetCertificateInfoGuardianDashboardXml(line2);

                if (dt.Rows.Count > 0 && !cp.IsInRole("SRO DASHBOARD ADMIN"))
                {
                    throw new Exception("You do not have permission to rewrite existing data.");
                }


                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

                try
                {
                    exec.BeginTrans();

                    exec.Delete("DeleteCertificateInfoGuardianDashboard", this.DeleteCertificateInfoGuardianDashboardXml(line2));

                    exec.CommitTrans();
                }
                catch (Exception xcp)
                {
                    exec.RollBackTrans();
                    throw new Exception("Transaction could not be saved in database." + xcp.Message);
                }
            }        

             AddCertificate(line2);


            if (sr.EndOfStream)
            {
                i = 100000;
                //break;
            }
        }
        sr.Close();
    }

    private void AddCertificate(string[] line2)
    {
        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

        try
        {
            exec.BeginTrans();

            exec.Insert("AddCertificateInfoGuardianDashboard", this.GetAddCertificateInfoGuardianDashboardXml(line2));

            exec.CommitTrans();
        }
        catch (Exception xcp)
        {
            exec.RollBackTrans();
            throw new Exception("Transaction could not be saved in database." + xcp.Message);
        }
    }

    private XmlDocument GetAddCertificateInfoGuardianDashboardXml(string[] line2)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[14];

        DbRequestXmlCooker.AttachCookItem("EARN_PAYDATE",
            SqlDbType.VarChar, 10, line2[0],
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("WEEKDAY",
            SqlDbType.VarChar, 15, line2[1].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("YEAR",
            SqlDbType.VarChar, 4, line2[2].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("MONTH_YEAR",
        SqlDbType.VarChar, 7, line2[3].ToString(),
        ref cookItems);

        DbRequestXmlCooker.AttachCookItem("WEEK",
            SqlDbType.VarChar, 7, line2[4].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("MONTH_DATE",
            SqlDbType.VarChar, 2, line2[5].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("TIPO_VEHICULO",
            SqlDbType.VarChar, 20, line2[6].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("TIPO_POS",
            SqlDbType.VarChar, 15, line2[7].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("EARN_SELLERCODE",
            SqlDbType.VarChar, 25, line2[8].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("NOMBRE_EA",
            SqlDbType.VarChar, 100, line2[9].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("MSRO_NAME",
            SqlDbType.VarChar, 150, line2[10].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("COUNT",
            SqlDbType.VarChar, 0, line2[11].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("TOTAL_PRIMA",
            SqlDbType.VarChar, 50, line2[12].ToString(),
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("ASEGURADORA_CODE",
            SqlDbType.VarChar, 25, line2[13].ToString(),
            ref cookItems);

        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items. " + ex.Message, ex);
        }

        return xmlDoc;
    }

    private DataTable GetCertificateInfoGuardianDashboardXml(string[] line2)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("MONTH_YEAR",
        SqlDbType.VarChar, 10, line2[0].ToString(),
        ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items. " + ex.Message, ex);
        }
        DataTable dt = null;
        try
        {
            dt = exec.GetQuery("GetCertificateInfoGuardianDashboard", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not verify if Data already exists, please try again." + ex.Message, ex);
        }
    }

    private XmlDocument DeleteCertificateInfoGuardianDashboardXml(string[] line2)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("MONTH_YEAR",
        SqlDbType.VarChar, 10, line2[0].ToString(),
        ref cookItems);

        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items. " + ex.Message, ex);
        }

        return xmlDoc;
    }

    private DataTable GetIfClosed(int Month, int Year)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

        DbRequestXmlCooker.AttachCookItem("MONTH",
        SqlDbType.Int, 0, Month.ToString(),
        ref cookItems);

        DbRequestXmlCooker.AttachCookItem("YEAR",
        SqlDbType.Int, 0, Year.ToString(),
        ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items. " + ex.Message, ex);
        }
        DataTable dt = null;
        try
        {
            dt = exec.GetQuery("GetDashboardIsClosed", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not verify if Files already exists, please try again." + ex.Message, ex);
        }
    }

    private void UpdateDashboardClosedFiles(int Month, int Year, bool 閉じて)
    {
        DbRequestXmlCookRequestItem[] cookItems =
           new DbRequestXmlCookRequestItem[3];

        DbRequestXmlCooker.AttachCookItem("MONTH",
        SqlDbType.Int, 0, Month.ToString(),
        ref cookItems);

        DbRequestXmlCooker.AttachCookItem("YEAR",
        SqlDbType.Int, 0, Year.ToString(),
        ref cookItems);

        DbRequestXmlCooker.AttachCookItem("Close",
        SqlDbType.Bit, 0, 閉じて.ToString(),
        ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items. " + ex.Message, ex);
        }
        DataTable dt = null;
        try
        {
            dt = exec.GetQuery("UpdateDashboardClosedFiles", xmlDoc);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not update SRO Files, please try again." + ex.Message, ex);
        }
    }

    private bool IsNOTExist(string id)
    {
        DbRequestXmlCookRequestItem[] cookItems =
        new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("CertInfoAceID",
        SqlDbType.Int, 0, id.ToString(),
        ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items.", ex);
        }

        DataTable dt = null;

        dt = exec.GetQuery("GetCertificateInfoAceByCertInfoAceID", xmlDoc);

        if (dt.Rows.Count > 0)
            return false;
        else
            return true;
    }

    private void ReviewFile(string newnm)
    {
        string FileName = @"C:\Upload\fileTemp.txt";
        //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\OP" + file.Trim() + seq.ToString() + ".txt";
        StreamWriter sw;
        sw = File.CreateText(FileName);

        StreamReader sr = File.OpenText(@"C:\Upload\" + newnm);
        string line = "";

        for (int i = 0; i < 100000; i++)
        {
            line = sr.ReadLine();
            line = line.Replace('"', ' ');
            line = line.Replace(" ", "");

            sw.WriteLine(line);

            if (sr.EndOfStream)
                i = 100000;
        }
        sw.Close();
        sr.Close();

        File.Copy(@"C:\Upload\fileTemp.txt", @"c:\Upload\" + newnm, true);
    }

    private void RemoveSessionLookUp()
    {
        Session.Remove("LookUpTables");
    }

    #endregion
}
