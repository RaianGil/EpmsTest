using Baldrich.DBRequest;
using EPolicy.TaskControl;
using EPolicy.XmlCooker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class PremiumFinance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.litPopUp.Visible = false;


        Control Banner = new Control();
        Banner = LoadControl(@"TopBannerNew.ascx");
        this.phTopBanner.Controls.Add(Banner);

        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        if (cp == null)
        {
            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
            Response.Cookies.Add(authCookies);
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx?001");
        }

        if (!IsPostBack)
        {
            this.SetReferringPage();
        }


        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        string taskControlID = Request.QueryString["taskControlID"];

        if (taskControlID != null)
        {
            LblControlNo.Text = taskControlID;


            DataTable dt = GetInfoPremiumFinanceByTaskControlID(taskControlID);
            DataTable dt2 = GetPremiumFinanceByTaskControlID(taskControlID);

            FillTextControl(dt);

            if (dt2.Rows.Count < 1)
            {
                gvPremium.Visible = false;
                gvPremium.DataSource = null;
                gvPremium.DataBind();
            }
            else
            {
                FillGrid(dt2);
            }
        }
        else
        {

        }

    }

    private void SetReferringPage()
    {
        if ((Session["FromUI"] != null) && (Session["FromUI"].ToString() != ""))
        {
            this.ReferringPage = Session["FromUI"].ToString();
            //Session.Remove("FromUI");
        }
    }

    protected void BtnExit_Click(object sender, EventArgs e)
    {
        if (LblControlNo.Text.Replace(" No.:","") == "")
            Response.Redirect("MainMenu.aspx", false);
        else
            ReturnToReferringPage();
    }

    private void ReturnToReferringPage()
    {
        if (this.ReferringPage != "")
        {
            Response.Redirect(this.ReferringPage);
        }
        Response.Redirect("HomePage.aspx");
    }

    private void FillGrid(DataTable dt)
    {

            gvPremium.Visible = true;
            gvPremium.DataSource = dt;
            gvPremium.DataBind();

            gvPremium.UseAccessibleHeader = true;
            gvPremium.HeaderRow.TableSection = TableRowSection.TableHeader;
        

    }


    private string ReferringPage
    {
        get
        {
            return ((ViewState["referringPage"] != null) ?
                ViewState["referringPage"].ToString() : "");
        }
        set
        {
            ViewState["referringPage"] = value;
        }
    }

    private void FillTextControl(DataTable dt)
    {
        txtPolicyNo.Text = dt.Rows[0]["PolicyNo"].ToString();
        string effDate = dt.Rows[0]["EffectiveDate"].ToString();
        string[] date1 = effDate.Split(new char[0]);
        txtEffectiveDate.Text = date1[0];
        string expDate = dt.Rows[0]["ExpirationDate"].ToString();
        string[] date2 = expDate.Split(new char[0]);
        txtExpirationDate.Text = date2[0];
        txtName.Text = dt.Rows[0]["Name"].ToString();

        DataTable dtPrem = GetPremiumFinanceByTaskControlID(LblControlNo.Text);
        if (!IsPostBack)
        {
            if (dtPrem != null)
            {
                if (dtPrem.Rows.Count > 0)
                {
                    ddlTerms.SelectedIndex = ddlTerms.Items.IndexOf(ddlTerms.Items.FindByText(dtPrem.Rows[0]["Term"].ToString()));
                }
            }
        }

        if (txtTotalPremium.Text == "" && dtPrem.Rows.Count == 0)
        {
            txtTotalPremium.Text = dt.Rows[0]["TotalPremium"].ToString();

            string downPayment = calculateDownPayment(txtTotalPremium.Text).ToString();
            txtDownPayment.Text = String.Format("{0:c2}", double.Parse(downPayment), System.Globalization.NumberStyles.Currency);
            string unpaidBalance = calculateUnpaidBalance(downPayment).ToString();
            txtUnpaidBalance.Text = unpaidBalance;
            txtUnpaid.Text = txtUnpaidBalance.Text;
            string charge = calculateChargeAmount(unpaidBalance).ToString();
            txtCharge.Text = String.Format("{0:c2}", double.Parse(charge), System.Globalization.NumberStyles.Currency);
            string amount = calculateTotalPayment(double.Parse(charge)).ToString();
            txtAmount.Text = String.Format("{0:c2}", double.Parse(amount), System.Globalization.NumberStyles.Currency);

            txtTotalPremium.Text = String.Format("{0:c2}", double.Parse(dt.Rows[0]["TotalPremium"].ToString()), System.Globalization.NumberStyles.Currency);
            txtUnpaidBalance.Text = String.Format("{0:c2}", double.Parse(unpaidBalance), System.Globalization.NumberStyles.Currency);
            txtUnpaid.Text = txtUnpaidBalance.Text;
        }
        else if(!IsPostBack)
        {
            txtTotalPremium.Text = dt.Rows[0]["TotalPremium"].ToString();

            string downPayment = dtPrem.Rows[0]["DownPayment"].ToString();//calculateDownPayment(txtTotalPremium.Text).ToString();
            txtDownPayment.Text = String.Format("{0:c2}", double.Parse(downPayment), System.Globalization.NumberStyles.Currency);
            string unpaidBalance = dtPrem.Rows[0]["UnpaidBalance"].ToString();//calculateUnpaidBalance(downPayment).ToString();
            txtUnpaidBalance.Text = unpaidBalance;
            txtUnpaid.Text = txtUnpaidBalance.Text;
            string charge = dtPrem.Rows[0]["Charge"].ToString();//calculateChargeAmount(unpaidBalance).ToString();
            txtCharge.Text = String.Format("{0:c2}", double.Parse(charge), System.Globalization.NumberStyles.Currency);
            string amount = dtPrem.Rows[0]["TotalPayment"].ToString();//calculateTotalPayment(double.Parse(charge)).ToString();
            txtAmount.Text = String.Format("{0:c2}", double.Parse(amount), System.Globalization.NumberStyles.Currency);

            txtTotalPremium.Text = String.Format("{0:c2}", double.Parse(dt.Rows[0]["TotalPremium"].ToString()), System.Globalization.NumberStyles.Currency);
            txtUnpaidBalance.Text = String.Format("{0:c2}", double.Parse(unpaidBalance), System.Globalization.NumberStyles.Currency);
            txtUnpaid.Text = txtUnpaidBalance.Text;
        }
        
    }

    private double calculateDownPayment(string totalPremium)
    {
        double totalPrem = double.Parse(totalPremium);
        double downPayment = Math.Round(totalPrem, 2) * 35 / 100;
        return downPayment;
    }

    private double calculateUnpaidBalance(string downPayment)
    {

        double downPaym = double.Parse(downPayment);
        double unpaidBalance = double.Parse(txtTotalPremium.Text.Replace("$", "")) - Math.Round(downPaym, 2);
        return unpaidBalance;
    }

    private double calculateChargeAmount(string unpaidBalance)
    {
        double unpaidAmount = double.Parse(unpaidBalance);
        double paymentAmount = ((Math.Round(unpaidAmount, 2) * (15.0/100.0)) / 12.0)* int.Parse(ddlTerms.SelectedItem.Value) + 25.0;
        return paymentAmount;
    }

    private double calculateTotalPayment(double financeCharge)
    {
        double unpaidAmount = double.Parse(txtUnpaidBalance.Text);
        double totalPayment = Math.Round(unpaidAmount, 2) + Math.Round(financeCharge, 2); 
        return totalPayment; 
    }

    private double calculateMonthlyPayments(double totalPayments)
    {
       
        double monthlyPayment = Math.Round(totalPayments, 2) / int.Parse(ddlTerms.SelectedItem.Value);
       return monthlyPayment;
    }

    protected void txtTotalPremium_TextChanged(object sender, EventArgs e)
    {
        txtDownPayment.Text = calculateDownPayment(txtTotalPremium.Text.Replace("$", "")).ToString();
        txtUnpaidBalance.Text = calculateUnpaidBalance(txtDownPayment.Text).ToString();

        
        txtDownPayment.Text = String.Format("{0:c2}", double.Parse(txtDownPayment.Text), System.Globalization.NumberStyles.Currency);

        txtUnpaidBalance.Text = String.Format("{0:c2}", double.Parse(txtUnpaidBalance.Text), System.Globalization.NumberStyles.Currency);



    }

    protected void gvPremium_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }


    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (LblControlNo.Text.Replace(" No.:", "") == "")
        {
            DataTable dt = new DataTable();
            string downPayment = txtDownPayment.Text.ToString() == "" ? calculateDownPayment(txtTotalPremium.Text.Replace("$", "")).ToString() : txtDownPayment.Text.ToString().Replace("$", "");
            txtDownPayment.Text = String.Format("{0:c2}", double.Parse(downPayment), System.Globalization.NumberStyles.Currency);
            string unpaidBalance = calculateUnpaidBalance(downPayment).ToString();
            txtUnpaidBalance.Text = unpaidBalance;
            txtUnpaid.Text = txtUnpaidBalance.Text;
            string charge = calculateChargeAmount(unpaidBalance).ToString();
            txtCharge.Text = String.Format("{0:c2}", double.Parse(charge), System.Globalization.NumberStyles.Currency);
            string amount = calculateTotalPayment(double.Parse(charge)).ToString();
            txtAmount.Text = String.Format("{0:c2}", double.Parse(amount), System.Globalization.NumberStyles.Currency);


            txtUnpaidBalance.Text = String.Format("{0:c2}", double.Parse(unpaidBalance), System.Globalization.NumberStyles.Currency);
            txtUnpaid.Text = txtUnpaidBalance.Text;

            double totalPay = double.Parse(txtAmount.Text.Replace("$", "")) / int.Parse(ddlTerms.SelectedItem.Value);
            double input = Math.Round(totalPay, 2);
            string total = String.Format("{0:c2}", totalPay, System.Globalization.NumberStyles.Currency);
            //DateTime date = DateTime.Parse(txtEffectiveDate.Text);
            double added = double.Parse(txtAmount.Text.Replace("$", "")) - (input * int.Parse(ddlTerms.SelectedItem.Value));
            double sum;

            dt.Columns.Add("ID");
            dt.Columns.Add("PaymentDate");
            dt.Columns.Add("TaskControlID");
            dt.Columns.Add("PaymentAmount");
            dt.Columns.Add("Rate");
            dt.Columns.Add("Payment");
            dt.Columns.Add("Term");
            dt.Columns.Add("DownPayment");
            dt.Columns.Add("UnpaidBalance");
            dt.Columns.Add("Charge");
            dt.Columns.Add("TotalPayment");
            int RowNumber = 0;
            for (int x = 1; x <= int.Parse(ddlTerms.SelectedItem.Value); x++)
            {
                DataRow dr = dt.NewRow();

                if (x > 1)
                {
                    //date = date.AddMonths(1);
                    //dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);

                    dr["ID"] = RowNumber.ToString();
                    dr["PaymentDate"] = "";
                    dr["TaskControlID"] = "";
                    dr["PaymentAmount"] = total;
                    dr["Rate"] = "";
                    dr["Payment"] = "";
                    dr["Term"] = "";
                    dr["DownPayment"] = downPayment;
                    dr["UnpaidBalance"] = unpaidBalance;
                    dr["Charge"] = charge;
                    dr["TotalPayment"] = amount;

                    #region OLD WAY
                    //dt.Rows[RowNumber]["ID"] = RowNumber.ToString();
                    //dt.Rows[RowNumber]["PaymentDate"] = "";
                    //dt.Rows[RowNumber]["TaskControlID"] = "";
                    //dt.Rows[RowNumber]["PaymentAmount"] = amount;
                    //dt.Rows[RowNumber]["Rate"] = "";
                    //dt.Rows[RowNumber]["Payment"] = "";
                    //dt.Rows[RowNumber]["Term"] = "";
                    //dt.Rows[RowNumber]["DownPayment"] = downPayment;
                    //dt.Rows[RowNumber]["UnpaidBalance"] = unpaidBalance;
                    //dt.Rows[RowNumber]["Charge"] = charge;
                    //dt.Rows[RowNumber]["TotalPayment"] = total;
                    #endregion
                }
                else
                {
                    //date = date.AddMonths(1);
                    sum = added + Math.Round(totalPay, 2);
                    string totalAdded = String.Format("{0:c2}", sum, System.Globalization.NumberStyles.Currency);
                    //dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);
                    dr["ID"] = RowNumber.ToString();
                    dr["PaymentDate"] = "";
                    dr["TaskControlID"] = "";
                    dr["PaymentAmount"] = total;
                    dr["Rate"] = "";
                    dr["Payment"] = "";
                    dr["Term"] = "";
                    dr["DownPayment"] = downPayment;
                    dr["UnpaidBalance"] = unpaidBalance;
                    dr["Charge"] = charge;
                    dr["TotalPayment"] = amount;
                }

                dt.Rows.Add(dr);

                RowNumber++;
            }

            dt.AcceptChanges();

            FillGrid(dt);
        }
        else
        {
            DataTable dt = GetPremiumFinanceByTaskControlID(LblControlNo.Text);
            string downPayment = txtDownPayment.Text.ToString() == "" ? calculateDownPayment(txtTotalPremium.Text.Replace("$", "")).ToString() : txtDownPayment.Text.ToString().Replace("$", "");
            txtDownPayment.Text = String.Format("{0:c2}", double.Parse(downPayment), System.Globalization.NumberStyles.Currency);
            string unpaidBalance = calculateUnpaidBalance(downPayment).ToString();
            txtUnpaidBalance.Text = unpaidBalance;
            txtUnpaid.Text = txtUnpaidBalance.Text;
            string charge = calculateChargeAmount(unpaidBalance).ToString();
            txtCharge.Text = String.Format("{0:c2}", double.Parse(charge), System.Globalization.NumberStyles.Currency);
            string amount = calculateTotalPayment(double.Parse(charge)).ToString();
            txtAmount.Text = String.Format("{0:c2}", double.Parse(amount), System.Globalization.NumberStyles.Currency);


            txtUnpaidBalance.Text = String.Format("{0:c2}", double.Parse(unpaidBalance), System.Globalization.NumberStyles.Currency);
            txtUnpaid.Text = txtUnpaidBalance.Text;

            double totalPay = double.Parse(txtAmount.Text.Replace("$", "")) / int.Parse(ddlTerms.SelectedItem.Value);
            double input = Math.Round(totalPay, 2);
            string total = String.Format("{0:c2}", totalPay, System.Globalization.NumberStyles.Currency);
            DateTime date = DateTime.Parse(txtEffectiveDate.Text);
            double added = double.Parse(txtAmount.Text.Replace("$", "")) - (input * int.Parse(ddlTerms.SelectedItem.Value));
            double sum;

            if (dt.Rows.Count > 0)
            {
                DeletePremiumFinanceByTaskControlID(LblControlNo.Text);

                for (int x = 1; x <= int.Parse(ddlTerms.SelectedItem.Value); x++)
                {
                    if (x > 1)
                    {
                        date = date.AddMonths(1);
                        dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);
                    }
                    else
                    {
                        date = date.AddMonths(1);

                        sum = added + Math.Round(totalPay, 2);


                        string totalAdded = String.Format("{0:c2}", sum, System.Globalization.NumberStyles.Currency);
                        dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);
                    }


                }
            }
            else
            {

                for (int x = 1; x <= int.Parse(ddlTerms.SelectedItem.Value); x++)
                {


                    if (x > 1)
                    {
                        date = date.AddMonths(1);
                        dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);
                    }
                    else
                    {
                        date = date.AddMonths(1);
                        sum = added + Math.Round(totalPay, 2);
                        string totalAdded = String.Format("{0:c2}", sum, System.Globalization.NumberStyles.Currency);
                        dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);
                    }
                }
            }

            FillGrid(dt);
        }
    }

    public DataTable addPremiumFinance(string taskControlID, DateTime paymentDate, string paymentAmount, string DownPayment, string UnpaidBalance, string Charge, string TotalPayment)
    {
        DataTable dt = null;
   
        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[8];
        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControlID.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PaymentDate", SqlDbType.DateTime, 10, paymentDate.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PaymentAmount", SqlDbType.VarChar, 10, paymentAmount.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("Term", SqlDbType.VarChar, 10, ddlTerms.SelectedItem.Value.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DownPayment", SqlDbType.VarChar, 10, DownPayment.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("UnpaidBalance", SqlDbType.VarChar, 10, UnpaidBalance.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("Charge", SqlDbType.VarChar, 10, Charge.ToString().Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("TotalPayment", SqlDbType.VarChar, 10, TotalPayment.ToString().Trim(), ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        XmlDocument xmlDoc;

        xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        dt = null;
        dt = exec.GetQuery("AddPremiumFinance", xmlDoc);

        return dt;
    }

    private DataTable GetInfoPremiumFinanceByTaskControlID(string taskControlID)
    {

        DataTable dt = null;
        try
        {

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControlID, ref cookItems);
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            dt = exec.GetQuery("GetInfoPremiumFinanceByTaskControlID", xmlDoc);

        }
        catch (Exception ep)
        {
        }
        return dt;
    }
    public static void DeletePremiumFinanceByTaskControlID(string taskControlID)
    {
        DBRequest Executor = new DBRequest();

        try
        {
            Executor.BeginTrans();
            Executor.Update("DeletePremiumFinanceByTaskControlID", DeletePremiumFinanceByTaskControlIDXml(taskControlID));
            Executor.CommitTrans();
        }
        catch (Exception xcp)
        {
            Executor.RollBackTrans();
            throw new Exception("Error. Please try again. " + xcp.Message, xcp);
        }
    }

    private static XmlDocument DeletePremiumFinanceByTaskControlIDXml(string taskControlID)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("TaskControlID",
            SqlDbType.Int, 0, taskControlID.ToString(),
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

        return xmlDoc;
    }


    private DataTable GetPremiumFinanceByTaskControlID(string taskControlID)
    {

        DataTable dt = null;
        try
        {

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControlID, ref cookItems);
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            dt = exec.GetQuery("GetPremiumFinanceByTaskControlID", xmlDoc);

        }
        catch (Exception ep)
        {
        }
        return dt;
    }

    protected void txtDownPayment_TextChanged(object sender, EventArgs e)
    {
        string downPayment = calculateDownPayment(txtTotalPremium.Text.Replace("$", "")).ToString();

        if (txtDownPayment.Text.ToString() != "" && downPayment != "")
        {
            if (double.Parse(txtDownPayment.Text.ToString().Replace("$", "")) < double.Parse(downPayment))
            {
                txtDownPayment.Text = String.Format("{0:c2}", double.Parse(downPayment), System.Globalization.NumberStyles.Currency);
            }
            else
            {
                txtUnpaidBalance.Text = calculateUnpaidBalance(txtDownPayment.Text.ToString().Replace("$","")).ToString();
                txtUnpaidBalance.Text = String.Format("{0:c2}", double.Parse(txtUnpaidBalance.Text), System.Globalization.NumberStyles.Currency);
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (LblControlNo.Text.Replace(" No.:", "") == "")
        {
            txtDownPayment.Text = "";
            btnCalculate_Click(String.Empty, EventArgs.Empty);
            DataTable dt = new DataTable();
            FillGrid(dt);
        }
        else
        {
            DataTable dt = GetPremiumFinanceByTaskControlID(LblControlNo.Text);
            string downPayment = calculateDownPayment(txtTotalPremium.Text.Replace("$", "")).ToString();
            txtDownPayment.Text = String.Format("{0:c2}", double.Parse(downPayment), System.Globalization.NumberStyles.Currency);
            string unpaidBalance = calculateUnpaidBalance(downPayment).ToString();
            txtUnpaidBalance.Text = unpaidBalance;
            txtUnpaid.Text = txtUnpaidBalance.Text;
            string charge = calculateChargeAmount(unpaidBalance).ToString();
            txtCharge.Text = String.Format("{0:c2}", double.Parse(charge), System.Globalization.NumberStyles.Currency);
            string amount = calculateTotalPayment(double.Parse(charge)).ToString();
            txtAmount.Text = String.Format("{0:c2}", double.Parse(amount), System.Globalization.NumberStyles.Currency);

            txtUnpaidBalance.Text = String.Format("{0:c2}", double.Parse(unpaidBalance), System.Globalization.NumberStyles.Currency);
            txtUnpaid.Text = txtUnpaidBalance.Text;

            double totalPay = double.Parse(txtAmount.Text.Replace("$", "")) / int.Parse(ddlTerms.SelectedItem.Value);
            double input = Math.Round(totalPay, 2);
            string total = String.Format("{0:c2}", totalPay, System.Globalization.NumberStyles.Currency);
            DateTime date = DateTime.Parse(txtEffectiveDate.Text);
            double added = double.Parse(txtAmount.Text.Replace("$", "")) - (input * int.Parse(ddlTerms.SelectedItem.Value));
            double sum;

            if (dt.Rows.Count > 0)
            {
                DeletePremiumFinanceByTaskControlID(LblControlNo.Text);

                for (int x = 1; x <= int.Parse(ddlTerms.SelectedItem.Value); x++)
                {
                    if (x > 1)
                    {
                        date = date.AddMonths(1);
                        dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);
                    }
                    else
                    {
                        date = date.AddMonths(1);

                        sum = added + Math.Round(totalPay, 2);


                        string totalAdded = String.Format("{0:c2}", sum, System.Globalization.NumberStyles.Currency);
                        dt = addPremiumFinance(LblControlNo.Text, date, total, downPayment, unpaidBalance, charge, amount);
                    }
                }
            }
            else
            {
            }

            FillGrid(dt);
        }
    }
}