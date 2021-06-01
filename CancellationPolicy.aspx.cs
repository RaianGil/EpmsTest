using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
//using Epolicy.Reports;


using EPolicy.TaskControl;
using EPolicy;
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using System.Xml;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace EPolicy
{
	/// <summary>
	/// Summary description for CancellationPolicy.
	/// </summary>
    public partial class CancellationPolicy : System.Web.UI.Page
    {
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            //DropDownList
            DataTable dtCancellationMethod = EPolicy.LookupTables.LookupTables.GetTable("CancellationMethod");
            DataTable dtCancellationReason = EPolicy.LookupTables.LookupTables.GetTable("CancellationReason");
            DataTable dtAdjustmentType = LookupTables.LookupTables.GetTable("PaymentAdjustmentType");
            DataTable dtCreditDebit = LookupTables.LookupTables.GetTable("CreditDebit");

            //CancellationMethod
            ddlCancellationMethod.DataSource = dtCancellationMethod;
            ddlCancellationMethod.DataTextField = "CancellationMethodDesc";
            ddlCancellationMethod.DataValueField = "CancellationMethodID";
            ddlCancellationMethod.DataBind();
            ddlCancellationMethod.SelectedIndex = -1;
            ddlCancellationMethod.Items.Insert(0, "");

            ddlCancellationMethod.SelectedIndex = ddlCancellationMethod.Items.IndexOf(ddlCancellationMethod.Items.FindByText("ShortRate"));

            //CancellationReason
            ddlCancellationReason.DataSource = dtCancellationReason;
            ddlCancellationReason.DataTextField = "CancellationReasonDesc";
            ddlCancellationReason.DataValueField = "CancellationReasonID";
            ddlCancellationReason.DataBind();
            ddlCancellationReason.SelectedIndex = -1;
            ddlCancellationReason.Items.Insert(0, "");


            ddlCancellationReason.SelectedIndex = ddlCancellationReason.Items.IndexOf(ddlCancellationReason.Items.FindByText("Customer Request"));
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.litPopUp.Visible = false;

            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (cp == null)
            {
                Response.Redirect("HomePage.aspx?001");
            }
            else
            {
                if (!cp.IsInRole("CANCELLATION POLICY") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    Response.Redirect("HomePage.aspx?001");
                }
            }

            BtnSave = EPolicy.Utilities.ConfirmDialogBoxPopUp(BtnSave, "document.CancPol", "Are you sure you want to save this info?");

            if (!IsPostBack)
            {
                this.SetReferringPage();

                TxtReturnCharge.Attributes.Add("onblur", "addCharges()");
                TxtReturnPremium.Attributes.Add("onblur", "addCharges()");

                if (Session["TaskControl"] != null)
                {
                    if (Session["CancellationEdit"] != null)
                    {
                        int mmode = (int)Session["CancellationEdit"];

                        switch (mmode)
                        {
                            case 1: //Update
                                FillTextControl();
                                EnableControls();
                                break;

                            default:
                                FillTextControl();
                                DisableControls();
                                break;
                        }
                    }
                    else
                    {
                        FillTextControl();
                        DisableControls();
                    }
                }
            }
            else
            {
                if (Session["CancellationEdit"] != null)
                    if ((int)Session["CancellationEdit"] != 1)
                        DisableControls();
            }
        }

        private void EnableControls()
        {
            BtnCalculate.Visible = true;
            btnEdit.Visible = false;
            BtnSave.Visible = false; //true
            //BtnSubmit.Visible = false;
            btnCancel.Visible = true;
            BtnExit.Visible = false;
            btnPrint.Visible = false;
            imgCalendarEff.Visible = true;

            //ddlCancellationMethod.Enabled = true;
            ddlCancellationMethod.Enabled = false;
            ddlCancellationReason.Enabled = true;

            txtCancellationDate.Enabled = true;
            TxtUnearnedPercent.Enabled = true;
            TxtReturnPremium.Enabled = true;
            TxtReturnCharge.Enabled = true;
            TxtTotalReturnPremium.Enabled = false;
            TxtCancellationEntryDate.Enabled = false;

        }

        private void DisableControls()
        {
            TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];

            BtnCalculate.Visible = false;
            btnEdit.Visible = true;
           // BtnSave.Visible = false;
            btnCancel.Visible = false;
            BtnExit.Visible = true;
            btnPrint.Visible = false;
            imgCalendarEff.Visible = false;
            //if (txtCancellationDate.Text.Trim() != "")
            //    btnPrint.Visible = true;
            //else
            //    btnPrint.Visible = false;

            ddlCancellationMethod.Enabled = false;
            ddlCancellationReason.Enabled = false;

            txtCancellationDate.Enabled = false;
            TxtUnearnedPercent.Enabled = false;
            TxtReturnPremium.Enabled = false;
            TxtReturnCharge.Enabled = false;
            TxtTotalReturnPremium.Enabled = false;
            TxtCancellationEntryDate.Enabled = false;
        }

        private void FillTextControl()
        {
            //TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];
            TaskControl.Policy taskControl = GetTaskControl();
            DataTable dt = null;
            DataTable dtPolicyClass = LookupTables.LookupTables.GetTable("PolicyClass");

            //PolicyClass
            ddlPolicyClass.DataSource = dtPolicyClass;
            ddlPolicyClass.DataTextField = "PolicyClassDesc";
            ddlPolicyClass.DataValueField = "PolicyClassID";
            ddlPolicyClass.DataBind();
            ddlPolicyClass.SelectedValue = taskControl.PolicyClassID.ToString();
            ddlPolicyClass.Items.Insert(0, "");

            StatusButtonDisplay();
            dt = GetPolicyCancellationByTaskcontrolID(GetPolicyTaskControlID());
            BtnSaveToPolicyCancelation.Text = "Submit";

			 //verifica si la poliza esta Financiada - PremiumFinance
			if(IsPolicyFinanced())
				lblIsPolicyFinanced.Visible = true;
			else
				lblIsPolicyFinanced.Visible = false;


            if (dt != null && dt.Rows.Count > 0 && taskControl.CancellationMethod == 0)
            {

                ddlCancellationMethod.SelectedIndex = ddlCancellationMethod.Items.IndexOf(ddlCancellationMethod.Items.FindByValue(dt.Rows[0]["CancellationMethod"].ToString()));

                ddlCancellationReason.SelectedIndex = ddlCancellationReason.Items.IndexOf(ddlCancellationReason.Items.FindByValue(dt.Rows[0]["CancellationReason"].ToString()));


                if (dt.Rows[0]["CancellationDate"].ToString().Trim() != "")
                    txtCancellationDate.Text = DateTime.Parse(dt.Rows[0]["CancellationDate"].ToString()).ToString("MM/dd/yyyy");
                else
                    txtCancellationDate.Text = "";

                TxtUnearnedPercent.Text =  dt.Rows[0]["CancellationUnearnedPercent"].ToString();
                TxtReturnPremium.Text =  dt.Rows[0]["ReturnPremium"].ToString();
                TxtReturnCharge.Text =  dt.Rows[0]["ReturnCharge"].ToString();
                //TxtTotalReturnPremium.Text = taskControl.CancellationAmount.ToString("###,###.00");

                if (dt.Rows[0]["CancellationEntryDate"].ToString().Trim() != "")
                    TxtCancellationEntryDate.Text = DateTime.Parse(dt.Rows[0]["CancellationEntryDate"].ToString()).ToString("MM/dd/yyyy");
                else
                    TxtCancellationEntryDate.Text = DateTime.Now.ToShortDateString();

                //txtEffDt.Text = taskControl.EffectiveDate;
                //txtExpDt.Text = taskControl.ExpirationDate;

                double rc=0.00, rp=0.00;

                if(dt.Rows[0]["ReturnCharge"].ToString() != "")
                   rc=double.Parse(dt.Rows[0]["ReturnCharge"].ToString());

                if(dt.Rows[0]["ReturnPremium"].ToString() != "")
                   rp=double.Parse(dt.Rows[0]["ReturnPremium"].ToString());

                double amt = rc + rp;

                TxtTotalReturnPremium.Text = amt.ToString("###,###.00");						

            }
            else if (dt != null && dt.Rows.Count > 0 && taskControl.CancellationMethod != 0)
            {
                ddlCancellationMethod.SelectedIndex = 0;
                if (taskControl.CancellationMethod != 0)
                {
                    for (int i = 0; ddlCancellationMethod.Items.Count - 1 >= i; i++)
                    {
                        if (ddlCancellationMethod.Items[i].Value == taskControl.CancellationMethod.ToString())
                        {
                            ddlCancellationMethod.SelectedIndex = i;
                            i = ddlCancellationMethod.Items.Count - 1;
                        }
                    }
                }

                ddlCancellationReason.SelectedIndex = 0;
                if (taskControl.CancellationReason != 0)
                {
                    for (int i = 0; ddlCancellationReason.Items.Count - 1 >= i; i++)
                    {
                        if (ddlCancellationReason.Items[i].Value == taskControl.CancellationReason.ToString())
                        {
                            ddlCancellationReason.SelectedIndex = i;
                            i = ddlCancellationReason.Items.Count - 1;
                        }
                    }
                }

                // txtCancellationDate.Text = taskControl.CancellationDate;
                txtCancellationDate.Text = DateTime.Parse(taskControl.CancellationDate).ToString("MM/dd/yyyy");
                TxtUnearnedPercent.Text = taskControl.CancellationUnearnedPercent.ToString("###,###.000");
                TxtReturnPremium.Text = taskControl.ReturnPremium.ToString("###,###.00");
                TxtReturnCharge.Text = taskControl.ReturnCharge.ToString("###,###.00");
                //TxtTotalReturnPremium.Text = taskControl.CancellationAmount.ToString("###,###.00");
                TxtCancellationEntryDate.Text =  DateTime.Parse(taskControl.CancellationEntryDate).ToString("MM/dd/yyyy");

                //txtEffDt.Text = taskControl.EffectiveDate;
                //txtExpDt.Text = taskControl.ExpirationDate;

                double amt = taskControl.ReturnCharge + taskControl.ReturnPremium;

                TxtTotalReturnPremium.Text = amt.ToString("###,###.00");

                //			DateTime date   = DateTime.Now;
                //			TxtCancellationEntryDate.Text    = date.ToShortDateString().Trim();
            }
            else 
            {
                TxtCancellationEntryDate.Text = DateTime.Now.ToShortDateString();
            }

        }
		
		 private bool IsPolicyFinanced()
        {
            bool result = false;
             System.Data.DataTable dtFM = (new FinanceMaster()).GetFinanceMasterByTaskcontrolID(GetPolicyTaskControlID());
            if (dtFM.Rows.Count > 0) 
                result = true;
			
            return result;
        }

        private void SetReferringPage()
        {
            if ((Session["FromUI"] != null) && (Session["FromUI"].ToString() != ""))
            {
                this.ReferringPage = Session["FromUI"].ToString();
                //Session.Remove("FromUI");
            }
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


        private void ReturnToReferringPage()
        {
            if (this.ReferringPage != "")
            {
                Response.Redirect(this.ReferringPage);
            }
            Response.Redirect("HomePage.aspx");
        }

        protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Session.Remove("CancellationEdit");
            ReturnToReferringPage();
        }


        protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            int mmode = 1;
            Session.Add("CancellationEdit", mmode);

            BtnApprove.Visible = false;
            //BtnSubmit.Visible = false;
            BtnSaveToPolicyCancelation.Visible = true;

            if(lblStatusText.Text !="New")
            BtnSaveToPolicyCancelation.Text = "Save";

            DefaultValue();
            EnableControls();
        }

        private void DefaultValue()
        {
            
        }

        protected void btnCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
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

            //TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];
            TaskControl.Policy taskControl = GetTaskControl();

            TaskControl.TaskControl tc = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);

            Session["TaskControl"] = tc;
            FillTextControl();

            int mmode = 0;
            Session.Add("CancellationEdit", mmode);
            DisableControls();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (bool.Parse(ConfirmDialogBoxPopUp.Value.Trim()) == true)
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
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

                try
                {
                    CalculatedCancellation(true);

                    FillProperties();
                    TaskControl.Policy taskControl = GetTaskControl();

                    taskControl.SaveCancellation(userID);  //(userID);
                    FillTextControl();
                    DisableControls();

                    int mmode = 0;
                    Session.Add("CancellationEdit", mmode);

                    this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Cancellation information saved successfully.");
                    this.litPopUp.Visible = true;
                }
                catch (Exception exp)
                {
                    this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to save this Cancellation. " + exp.Message);
                    this.litPopUp.Visible = true;
                }
            }
            else
            {
                //				string js="";
                //				js = "<script language=javascript>alert('No Pudo Salvar.');</script>";
                //				Response.Write(js);
            }
        }



        protected void SaveToPolicy()
        {
                Login.Login cp = HttpContext.Current.User as Login.Login;
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

                try
                {

                    //TaskControl.Policy taskControl = GetTaskControl();
                    CalculatedCancellation(true);

                    FillProperties();

                    //EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl-Policy"];
                    EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"]; 
                    //TaskControl.TaskControl taskControl = TaskControl.TaskControl.GetTaskControlByTaskControlID(GetPolicyTaskControlID(), userID);


					string Message = "";
                    //if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                    //{
                        if (!CancelPolicyPPS(ref Message))
                        {
                            throw new Exception(Message);
                            //this.lblRecHeader.Text = Message;
                            //mpeSeleccion.Show();
                            ////this.litPopUp.Visible = true;
                            //return;
                        }
                    //}
					
                    taskControl.SaveCancellation(userID);  //(userID);
                    SubmitRequestEmail("Approved");
                    FillTextControl();
                    DisableControls();

                    int mmode = 0;
                    Session.Add("CancellationEdit", mmode);

                    //this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Cancellation information saved successfully.");
                    //this.litPopUp.Visible = true;
                }
                catch (Exception exp)
                {
                    throw new Exception(exp.Message);
                    //this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to save this Cancellation. " + exp.Message);
                    //this.litPopUp.Visible = true;
                    //this.lblRecHeader.Text = exp.Message;
                    //mpeSeleccion.Show();
                }
        }

       

        public void FillProperties()
        {
            //TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];
            TaskControl.Policy taskControl = GetTaskControl();

            taskControl.CancellationMethod = ddlCancellationMethod.SelectedItem.Value != "" ? int.Parse(ddlCancellationMethod.SelectedItem.Value) : 0;
            taskControl.CancellationReason = ddlCancellationReason.SelectedItem.Value != "" ? int.Parse(ddlCancellationReason.SelectedItem.Value) : 0;
            taskControl.CancellationUnearnedPercent = double.Parse(TxtUnearnedPercent.Text);
            taskControl.CancellationDate = txtCancellationDate.Text;

            if (TxtReturnPremium.Text.Trim() == "")
                taskControl.ReturnPremium = 0.00;
            else
                taskControl.ReturnPremium = double.Parse(TxtReturnPremium.Text);

            if (TxtReturnCharge.Text.Trim() == "")
                taskControl.ReturnCharge = 0.00;
            else
                taskControl.ReturnCharge = double.Parse(TxtReturnCharge.Text);

            double amt = taskControl.ReturnCharge + taskControl.ReturnPremium;
            TxtTotalReturnPremium.Text = amt.ToString("###,###.00");

            //taskControl.CancellationAmount = double.Parse(TxtTotalReturnPremium.Text);
            taskControl.CancellationEntryDate = TxtCancellationEntryDate.Text;

            //if (TxtTotalReturnPremium.Text.Trim() == "")
            //    taskControl.CancellationAmount = 0.00;
            //else
            //    taskControl.CancellationAmount = double.Parse(TxtTotalReturnPremium.Text);

            Session.Add("TaskControl", taskControl);
        }

        protected void BtnCalculate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CalculatedCancellation(false);
        }

        protected void btnPrint_Click(object sender, System.EventArgs e)
        {
           
            try
            {
                TaskControl.Policy taskControl  = GetTaskControl();

                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
                EPolicy.TaskControl.Autos taskControl1 = (EPolicy.TaskControl.Autos)Session["TaskControl"];
                GetCancellationByTaskControlIDForInvoice_VITableAdapters.GetCancellationByTaskControlIDForInvoice_VI_DTTableAdapter ds1 = new GetCancellationByTaskControlIDForInvoice_VITableAdapters.GetCancellationByTaskControlIDForInvoice_VI_DTTableAdapter();
                ReportDataSource rds1 = new ReportDataSource();
                rds1 = new ReportDataSource("DataSet1", (DataTable) ds1.GetData(taskControl.TaskControlID));

                //Nuevo
                string ImgPath = "", AgentDesc = "";
                Uri pathAsUri = null;
                DataTable dt = null, dtAgent = null;

                dt = GetImageLogoByAgentID(taskControl1.Agent.ToString().Trim());
                dtAgent = GetAgentByAgentID(taskControl1.Agent.ToString().Trim());

                if (dtAgent.Rows.Count > 0)
                {
                    AgentDesc = dtAgent.Rows[0]["AgentDesc"].ToString().Trim();
                }

                if (dt.Rows.Count > 0)
                {
                    ImgPath = Server.MapPath("Images2\\AgencyLogos\\" + dt.Rows[0]["ImgPath"].ToString().Trim());
                    pathAsUri = new Uri(ImgPath);
                }
                else if (AgentDesc.ToUpper().Contains("GUARDIAN"))
                {
                    ImgPath = Server.MapPath("Images2\\AgencyLogos\\guardianLogo.png");
                    pathAsUri = new Uri(ImgPath);
                }
                else
                {

                }

                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/EndorInvoice_VI.rdlc");
                viewer1.LocalReport.EnableExternalImages = true;
                viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.SetParameters(parameters);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                string fileName1 = "CancPolicyNo- " + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-Com8";
                string _FileName1 = "CancPolicyNo- " + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-Com8" + ".pdf";

                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                    File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

                byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (FileStream fs1 = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1, FileMode.Create))
                {
                    fs1.Write(bytes1, 0, bytes1.Length);
                    fs1.Close();
                }

                //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);

                //FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + _FileName1 + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + _FileName + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

            }
            catch (Exception ecp)
            {
                throw new Exception(ecp.Message);
            }



            //List<string> mergePaths = new List<string>();

            //if (Session["CancFromAutos"] != null)
            //{
            //    mergePaths = PrintCancellation(mergePaths, "VI/CancellationNotice");
            //    mergePaths = PrintCancellation(mergePaths, "VI/CancellationNoticeCompanyCopy");
            //    mergePaths = PrintCancellation(mergePaths, "VI/CancellationNoticeAgentsCopy");
            //    mergePaths = PrintCancellation(mergePaths, "VI/CancellationNoticePage2");
            //}
            //else
            //{
            //    mergePaths = PrintCancellation(mergePaths, "Cancel");
            //}

            //string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
            ////Generar PDF
            //OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            //string FinalFile = "";
            //FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
        }

        protected void btnPrintCancellationRequest_Click(object sender, System.EventArgs e)
        {
            if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnPrintCancellationCheckRequest")
                CreateCancellationReports("check");
            else
                CreateCancellationReports("request");
             
        }

        private void CreateCancellationReports(string type)
        {
             try
            {
                TaskControl.Policy taskControl = GetTaskControl();

                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
                EPolicy.TaskControl.Autos taskControl1 = (EPolicy.TaskControl.Autos)Session["TaskControl"];

                GetReportCancellationRequestTableAdapters.GetReportCancellationRequestTableAdapter ds1 = new GetReportCancellationRequestTableAdapters.GetReportCancellationRequestTableAdapter();

                ReportDataSource rds1 = new ReportDataSource();
                rds1 = new ReportDataSource("GetReportCancellationRequest", (DataTable)ds1.GetData(GetPolicyTaskControlID()));

                //Nuevo
                string ImgPath = "", AgentDesc = "";
                Uri pathAsUri = null;
                DataTable dt = null, dtAgent = null;


                //ReportParameter[] parameters = new ReportParameter[1];
                //parameters[0] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;

                if (type == "check")
                {
                    viewer1.LocalReport.ReportPath = Server.MapPath("Reports/PolicyCancellationCheckRequest.rdlc");
                    string PFCText = "";
                    if (IsPolicyFinanced())
                    {
                        PFCText = "THIS POLICY IS FINANCED,PLEASE VERIFY IN PPS OR IN PREMIUM FINANCE";
                    }

                    ReportParameter PFC = new ReportParameter("PFC",PFCText);
                    viewer1.LocalReport.SetParameters(PFC);

                }
                else
                {
                    ReportParameter[] parameters = new ReportParameter[4];

                     parameters[0] = new ReportParameter("CancellationReason", ddlCancellationReason.SelectedItem.Value.ToString());
                     parameters[1] = new ReportParameter("CancellationMethod", ddlCancellationMethod.SelectedItem.Value.ToString());
                     parameters[2] = new ReportParameter("CancellationDate", txtCancellationDate.Text.Trim());
                     parameters[3] = new ReportParameter("ReturnPremium", TxtReturnPremium.Text.Trim().Replace("$", "").Replace(",", ""));

                    viewer1.LocalReport.ReportPath = Server.MapPath("Reports/CancellationRequest.rdlc");
                    viewer1.LocalReport.SetParameters(parameters);
                }

                viewer1.LocalReport.EnableExternalImages = true;
                viewer1.LocalReport.DataSources.Add(rds1);
                //viewer1.LocalReport.SetParameters(parameters);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;

                string fileName1="";
                string _FileName1 = "";

                if (type == "check")
                {
                     fileName1 = "CancPolicyNo- " + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-CheckRequest";
                     _FileName1 = "CancPolicyNo- " + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-CheckRequest" + ".pdf";
                }
                else
                {
                     fileName1 = "CancPolicyNo- " + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-Request";
                     _FileName1 = "CancPolicyNo- " + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.TaskControlID.ToString().Trim() + "-Request" + ".pdf";
                }

                

                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                    File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

                byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (FileStream fs1 = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1, FileMode.Create))
                {
                    fs1.Write(bytes1, 0, bytes1.Length);
                    fs1.Close();
                }
                TextBox1.Text = _FileName1;
                
                if (type != "email")
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + _FileName1 + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                
            }
            catch (Exception ecp)
            {
                LogError(ecp.Message);
                throw new Exception(ecp.Message);
            }     
        }
       

        private static DataTable GetImageLogoByAgentID(string AgentID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("AgentID",
                SqlDbType.VarChar, 10, AgentID.ToString(),
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetAgencyLogoByAgentID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

        private static DataTable GetAgentByAgentID(string AgentID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("AgentID",
                SqlDbType.VarChar, 10, AgentID.ToString(),
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
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetAgentByAgentID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }

        protected void BtnExit_Click(object sender, System.EventArgs e)
        {
            Session.Remove("FromUI");
            Session.Remove("CancellationEdit");
            Session.Remove("CancFromAuto");

            if (Session["CancFromAutoExit"] != null)
            {
                TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];
                TaskControl.QuoteAuto taskControl2 = (TaskControl.QuoteAuto)TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, 1);
                Session["TaskControl"] = taskControl2;
            }
			
			 EPolicy.TaskControl.Autos taskControl3 = (EPolicy.TaskControl.Autos)Session["TaskControl-Policy"];

            Session.Remove("TaskControl");
            Session.Remove("TaskControl-Policy");
            Session.Add("TaskControl", taskControl3);
			
            ReturnToReferringPage();
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
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

            //TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];
            TaskControl.Policy taskControl = GetTaskControl();


            if (Session["CancFromAutoExit"] == null)
            {
                TaskControl.TaskControl tc = TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                Session["TaskControl"] = tc;
            }

            FillTextControl();

            int mmode = 0;
            Session.Add("CancellationEdit", mmode);
            DisableControls();
        }

        protected void BtnSaveToPolicyCancelation_Click(object sender, System.EventArgs e)
        {
            TaskControl.Policy taskControl = GetTaskControl();
            if (bool.Parse(ConfirmDialogBoxPopUp.Value.Trim()) == true)
            {
                ArrayList errorMessages = new ArrayList();
                Login.Login cp = HttpContext.Current.User as Login.Login;
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

                try
                {
                    if (((Button)sender).ID.ToString() != "btnSubmitAlert")
                    {
                        if (lblStatusText.Text == "New")
                        {
                            string DivAlert;
                            DivAlert = @"<div align=""center"">";
                            DivAlert += "<br>";
                            DivAlert += @"<h3><b>To Submit the Policy Cancellation, you must attach the following documents to the client.</b></h3>";
                            DivAlert += "<br>";
                            DivAlert += @"<p><b>.	Cancelled Registration </b></p>";
                            DivAlert += "<br>";
                            DivAlert += @"<h4><b>In the event that the Cancelled Registration cannot be presented, the following can be used.</b></h4>";
                            DivAlert += "<br>";
                            DivAlert += @"<p><b>.	New Vehicle Registration  </b></p>";
                            DivAlert += "<br>";
                            DivAlert += @"<p><b>.	Proof of insurance </b></p>";
                            DivAlert += "<br>";
                            DivAlert += @"<p><b>.	Shipping Documents </b></p>";
                            DivAlert += "<br>";
                            DivAlert += "<div>";
                            phAlert.Controls.Add(new Literal() { Text = DivAlert });
                            mpeAlert.Show();

                            if (VerifyCencellationDocuments())
                                btnSubmitAlert.Enabled = true;
                            else
                                btnSubmitAlert.Enabled = false;
                        }
                        else {
                            SaveCancellation();
                        }
                    }
                    else
                    {
                        SaveCancellation();
                    }

                    //this.lblRecHeader.Text = "Cancellation information saved successfully.";
                    //mpeSeleccion.Show();
                    
                }
                catch (Exception exp)
                {
                    this.lblRecHeader.Text = "Unable to save this Cancellation. " + exp.Message;
                    mpeSeleccion.Show();
                    //this.litPopUp.Visible = true;
                }
            }
            else
            {
                //				string js="";
                //				js = "<script language=javascript>alert('No Pudo Salvar.');</script>";
                //				Response.Write(js);
            }
        }

        protected void SaveCancellation()
        {
            //if (!VerifyCencellationDocuments())
            //{
            //    lblRecHeader.Text = "No documents where found";
            //    mpeSeleccion.Show();
            
            //}
            CalculatedCancellation(true);

                     //string Message = "";
                    //if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                    //{
                    //    if (!CancelPolicyPPS(ref Message))
                    //    {
                    //        this.lblRecHeader.Text = Message;
                    //        mpeSeleccion.Show();
                    //        //this.litPopUp.Visible = true;
                    //        return;
                    //    }
                    //}

                    //FillProperties();
                    TaskControl.Policy taskControl = GetTaskControl();

                    //taskControl.SaveCancellation(userID);  //(userID);
                    AddPolicyCancellation(GetPolicyTaskControlID());

                    if (lblStatusText.Text.Trim() == "New")
                      SubmitRequestEmail("");

                    FillTextControl();
                    DisableControls();

                    int mmode = 0;
                    Session.Add("CancellationEdit", mmode);      
        }

        protected bool VerifyCencellationDocuments()
        {
             //TaskControl.Policy taskControl = GetTaskControl();

             Login.Login cp = HttpContext.Current.User as Login.Login;
             Customer.Customer customer = (Customer.Customer)Session["Customer"];
             EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
             customer = taskControl.Customer;
            bool flag=false;

            DataTable DtCert = EPolicy.Customer.Customer.GetDocumentsByCustomerNo(taskControl.CustomerNo, GetPolicyTaskControlID(), taskControl.isQuote ? 0 : taskControl.TCIDQuotes);

            if (DtCert.Rows.Count > 0)
            {
                for (int i = 0; i < DtCert.Rows.Count; i++)
                {
                    if (DtCert.Rows[i]["TaskcontrolTypeID"].ToString() == "39")
                        flag = true;
                }
               
            }
            return flag;

       
        }

        protected void btnSubmitAlert_Click(object sender, System.EventArgs e)
        {

            
        }

        protected void BtnApprove_Click(object sender, System.EventArgs e)
        {
            mpeStatus.Show();
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            TaskControl.Policy taskControl = GetTaskControl();

            DataTable dt = GetPolicyCancellationByTaskcontrolID(GetPolicyTaskControlID());

            string Messege="";
            //Status can be: Submitted,Approved,Rejected
            try
            {
                if (dt.Rows.Count > 0)
                {

                    if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "BtnSubmit")
                    {
                        UpdatePolicyCancellationStatusByTaskcontrolID(GetPolicyTaskControlID(), "Submitted", true);

                        Messege = "The cancellation has been submited for approval...";
                    }

                    if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnApprove2")
                    {
                        SaveToPolicy();
                        UpdatePolicyCancellationStatusByTaskcontrolID(GetPolicyTaskControlID(), "Approved", true);
                        Messege = "The cancellation has been Approved...";
                    }

                    if (((System.Web.UI.WebControls.Button)sender).ID.ToString() == "btnRejected")
                    {
                        UpdatePolicyCancellationStatusByTaskcontrolID(GetPolicyTaskControlID(), "Rejected", true);
                        Messege = "The cancellation has been Rejected...";
                        SubmitRequestEmail("Rejected");
                    }

                    //StatusButtonDisplay();
                    FillTextControl();

                    lblRecHeader.Text = Messege;
                    mpeSeleccion.Show();
                }
                else 
                {
                    lblRecHeader.Text = "The cancelation information has not been saved.";
                    mpeSeleccion.Show();
                }
        }
            catch (Exception ex)
            {
                lblRecHeader.Text = ex.Message;
                mpeSeleccion.Show();
            }
        }

       
        protected void btnEdit_Click(object sender, System.EventArgs e)
        {
            int mmode = 1;
            Session.Add("CancellationEdit", mmode);

            BtnApprove.Visible = false;
            //BtnSubmit.Visible = false;
            BtnSaveToPolicyCancelation.Visible = true;

            if (lblStatusText.Text != "New")
                BtnSaveToPolicyCancelation.Text = "Save";
           

            DefaultValue();
            EnableControls();
        }

        private void StatusButtonDisplay()
        {

            TaskControl.Policy taskControl = GetTaskControl();
            Login.Login cp = HttpContext.Current.User as Login.Login;
            DataTable dt = null;

            dt = GetPolicyCancellationByTaskcontrolID(GetPolicyTaskControlID());


            BtnSaveToPolicyCancelation.Visible = false;
            if ( dt != null && dt.Rows.Count > 0)
            {

                if(taskControl.CancellationMethod !=0)
                {

                    btnPrintCancellationCheckRequest.Visible = true;
                    btnPrintCancellationRequest.Visible = true;
                    
                    BtnSave.Visible = false;
                    BtnApprove.Visible = false;
                    //BtnSubmit.Visible = false;
                    lblStatusText.Text = "Cancelled";
                
                }
                else if(bool.Parse(dt.Rows[0]["Rejected"].ToString()))
                {
                    BtnSave.Visible = false;
                    
                    BtnApprove.Visible = true;

                    if (cp.IsInRole("CANCELLATION APPROVE"))
                        BtnApprove.Enabled = true;
                    else
                        BtnApprove.Enabled = false;

                            //BtnSubmit.Visible = false;
                    lblStatusText.Text = "Rejected";
                }
                else if (bool.Parse(dt.Rows[0]["Approved"].ToString()))
                {
					btnPrintCancellationCheckRequest.Visible = true;
                    BtnSave.Visible = false;
                    BtnApprove.Visible = false;
                    //BtnSubmit.Visible = false;
                    lblStatusText.Text = "Approved";
                    btnPrintCancellationRequest.Visible = true;
                }
                else if (bool.Parse(dt.Rows[0]["Submitted"].ToString()))
                {
                    BtnSave.Visible = false;
                    BtnApprove.Visible = true;
                    if (cp.IsInRole("CANCELLATION APPROVE"))
                        BtnApprove.Enabled = true;
                    else
                        BtnApprove.Enabled = false;
                    //BtnSubmit.Visible = false;
                    lblStatusText.Text = "Submitted";
                    btnPrintCancellationRequest.Visible = true;
                }
                else 
                {
                    BtnSave.Visible = false;
                    BtnApprove.Visible = false;
                   // BtnSaveToPolicyCancelation.Visible = true;
                    lblStatusText.Text = "Pending";
                }
            }
            else{
            BtnSave.Visible=false;
            BtnApprove.Visible=false;
            //BtnSubmit.Visible = true;
            }   
        }

        public void CalculatedCancellation(bool FromSave)
        {
            try
            {
                validateCalculate();

                //TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];
                TaskControl.Policy taskControl = GetTaskControl();

                taskControl.CalculateReturnPremium(DateTime.Parse(txtCancellationDate.Text), int.Parse(ddlCancellationMethod.SelectedItem.Value), int.Parse(ddlCancellationReason.SelectedItem.Value.ToString()));

                Session.Add("TaskControl", taskControl);

                //if (FromSave == true)
                //FillTextControl();
                //TxtCancellationEntryDate.Text = taskControl.CancellationEntryDate.Trim();
                TxtUnearnedPercent.Text = taskControl.CancellationUnearnedPercent.ToString("###,###.000");
                TxtReturnPremium.Text = taskControl.ReturnPremium.ToString("###,###.00");
                TxtReturnCharge.Text = taskControl.ReturnCharge.ToString("###,###.00");
                TxtTotalReturnPremium.Text = taskControl.CancellationAmount.ToString("###,###.00");
            }
            catch (Exception xcp)
            {
                if (FromSave)
                {
                    throw new Exception(xcp.Message);
                }
                else
                {
                    this.litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message);
                    this.litPopUp.Visible = true;

                    //lblRecHeader.Text = xcp.Message;
                    //mpeSeleccion.Show();
                }
            }
        }

        public void validateCalculate()
        {
            //TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];
            TaskControl.Policy taskControl = GetTaskControl();

            string errorMessage = String.Empty;

            if (this.txtCancellationDate.Text == "")
                errorMessage = "Cancellation Date is missing or wrong.";
            else
                if (this.ddlCancellationMethod.SelectedItem.Value == "")
                    errorMessage = "Cancellation Method is missing or wrong.";
                else
                    if (this.ddlCancellationReason.SelectedItem.Value == "")
                        errorMessage = "Cancellation Reason is missing or wrong.";
                    else
                        if (DateTime.Parse(this.txtCancellationDate.Text) < DateTime.Parse(taskControl.EffectiveDate))//> DateTime.Parse(DateTime.Now.ToShortDateString()))
                            errorMessage = "The Cancellation Date date must be equal or later than the Effective Date.";
                        else
                            if (DateTime.Parse(this.txtCancellationDate.Text + " 00:00:00.000") > DateTime.Parse(taskControl.ExpirationDate))
                                errorMessage = "The Cancellation Date date must be equal or earlier than the Expiration Date.";

            //throw the exception.
            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
        }

        protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
        }

        protected void BtnCalculate_Click(object sender, System.EventArgs e)
        {
            CalculatedCancellation(false);
            btnPrintCancellationRequest.Visible = true;
            //CalculatedCancellation(false);
            //EnableControls();
        }
        protected void btnCalendar_ServerClick(object sender, EventArgs e)
        {

        }

       
        private TaskControl.Policy GetTaskControl()
        {
            TaskControl.QuoteAuto taskControl2;
            TaskControl.Policy taskControl;
            TaskControl.RoadAssistance taskControl3;
            EPolicy.TaskControl.Autos taskControlAutos;

            if (Session["CancFromRoadAssistance"] != null)
            {
                taskControl3 = (RoadAssistance)Session["TaskControl"];

                return taskControl3;
            }
            else if (Session["CancFromAutos"] != null)
            {
                //taskControl = (TaskControl.Policy)Session["TaskControl"];
                taskControlAutos = (EPolicy.TaskControl.Autos)Session["TaskControl"];
                taskControlAutos.TotalPremium = TaskControl.Autos.GetNetTotalPremium(taskControlAutos.TaskControlID);
                return taskControlAutos;
            }
            else
            {
                taskControl2 = (TaskControl.QuoteAuto)Session["TaskControl"];
                taskControl = taskControl2.Policy;
                Session["TaskControl"] = taskControl;
                Session.Remove("CancFromAuto");
                return taskControl;
            } 
        }

        #region UPLOAD DOCUMENTS
        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            FillGridDocuments(true);
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
            customer = taskControl.Customer;

            //var uc = (UserControl)Page.LoadControl("~/AddDocuments.ascx");
            //Panel1.Controls.Add(uc);
            //ModalPopupExtender1.Show();
            //return;

            if (customer.CustomerNo == "0")
            {
                //ShowMessageDialog("You must save customer in order to proceed.");
            }
            else
            {
                txtDocumentDesc.Text = "";
                mpeAdjunto.Show();
            }
        }

        protected void btnAdjuntarCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
                customer = taskControl.Customer;

                if (txtDocumentDesc.Text.Trim() == "")
                    throw new Exception("Please Fill the description Field.");

                if (ddlTransaction.Items.Count > 1)
                    if (ddlTransaction.SelectedItem.Text == "")
                        throw new Exception("Please Select a Transaction.");

                if (this.FileUpload1.PostedFile != null)
                {
                    if (FileUpload1.PostedFile.FileName == "")
                    {
                        throw new Exception("Please select a file from the browser.");
                    }
                }
                else
                {
                    throw new Exception("Please select a file from the browser.");
                }

                if (this.FileUpload1.PostedFile.FileName != "")
                {
                    if (this.FileUpload1.PostedFile != null)
                    {
                        string File = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.'));

                        switch (File.ToLower())
                        {
                            case ".pdf":

                                break;

                            case ".jpeg":

                                break;

                            case ".png":

                                break;

                            case ".jpg":

                                break;

                            default:

                                if (this.FileUpload1.PostedFile.FileName.Split(".".ToCharArray())[1].ToString().ToLower() != "pdf")
                                {
                                    throw new Exception("The File Format is not supported.");
                                }
                                break;
                        }

                        if (this.FileUpload1.PostedFile.ContentLength > 4000001)
                        {
                            throw new Exception("The file size must be up to 4MB.");
                        }
                    }
                }

                //SaveDocuments
               // int docid = EPolicy.Customer.Customer.Savedocuments(customer.CustomerNo.ToString(), txtDocumentDesc.Text.Trim(), ddlTransaction.SelectedItem.Value.Trim());
                int docid = EPolicy.Customer.Customer.Savedocuments(customer.CustomerNo.ToString(), txtDocumentDesc.Text.Trim(), GetPolicyTaskControlID().ToString(),"39");//39 is the cancellation taskcontroltypeid
                //Upload Document
                if (FileUpload1.PostedFile.FileName != null)
                {
                    string fileName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.'));


                    switch (fileName.ToLower())
                    {
                        case ".pdf":

                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".pdf";
                            break;

                        case ".jpeg":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".jpeg";
                            break;

                        case ".png":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".png";
                            break;

                        case ".jpg":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".jpg";
                            break;

                        default:
                            break;
                    }

                    FileUpload1.PostedFile.SaveAs(fileName);

                    FillGridDocuments(false);
                    txtDocumentDesc.Text = "";
                    ddlTransaction.SelectedIndex = 1;
                    Session["Transaction"] = null;
                    mpeAdjunto.Show();
                }
            }
            catch (Exception exp)
            {
                ddlTransaction.SelectedIndex = 1;
                mpeSeleccion.Show();
                lblRecHeader.Text = exp.Message;
                return;
            }
        }

        private void FillGridDocuments(bool Refresh)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            TaskControl.Policy taskControl = GetTaskControl();
            customer = taskControl.Customer;

            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            gvAdjuntar.DataSource = null;
            System.Data.DataTable DtCert = null;
            System.Data.DataTable dtTransaction = null;

            if (customer.CustomerNo != "")
            {
                DtCert = EPolicy.Customer.Customer.GetDocumentsByCustomerNo(taskControl.CustomerNo, GetPolicyTaskControlID(), 0);
                dtTransaction = TaskControl.TaskControl.GetTaskControlByCustomerNo(customer.CustomerNo, userID);
            }

            if ((dtTransaction != null && !IsPostBack) || Refresh)
            {
                if (dtTransaction.Rows.Count > 0)
                {
                    for (int i = dtTransaction.Rows.Count - 1; i >= 0; i--)
                    {
                        if (taskControl.TaskControlID != 0)
                            if (dtTransaction.Rows[i]["TaskControlID"].ToString().Trim() != taskControl.TaskControlID.ToString().Trim())
                            {
                                if (dtTransaction.Rows[i]["TaskControlID"].ToString().Trim() != taskControl.TCIDQuotes.ToString().Trim())
                                    dtTransaction.Rows.RemoveAt(i);
                            }
                    }

                    //Transaction
                    ddlTransaction.DataSource = dtTransaction;
                    ddlTransaction.DataTextField = "TaskControlTypeID";
                    ddlTransaction.DataValueField = "TaskControlID";
                    ddlTransaction.DataBind();
                    ddlTransaction.SelectedIndex = -1;
                    ddlTransaction.Items.Insert(0, "");

                    if (ddlTransaction.Items.Count > 1)
                        foreach (ListItem item in ddlTransaction.Items)
                        {
                            if (item.Text != "")
                            {
                                DataRow[] Row = dtTransaction.Select("TaskControlID = '" + item.Value + "'");
                                item.Text = Row[0]["TaskControlTypeDesc"].ToString().Trim().Contains("Home Owners") ? Row[0]["TaskControlTypeDesc"].ToString().Trim().Replace("Home Owners", "Residential Property") + " - " + Row[0]["TaskControlID"].ToString().Trim() : Row[0]["TaskControlTypeDesc"].ToString().Trim() + " - " + Row[0]["TaskControlID"].ToString().Trim();
                            }
                        }
                    ddlTransaction.SelectedIndex = 1;
                }
            }
            else
            { }

            if (DtCert != null)
            {
                if (DtCert.Rows.Count != 0)
                {
                    gvAdjuntar.DataSource = DtCert;
                    gvAdjuntar.DataBind();
                }
                else
                {
                    gvAdjuntar.DataSource = null;
                    gvAdjuntar.DataBind();
                }
            }
            else
            {
                gvAdjuntar.DataSource = null;
                gvAdjuntar.DataBind();
            }
        }

        protected void gvAdjuntar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
            customer = taskControl.Customer;
            int documentID = 0;
            try
            {
                if (e.CommandName.Trim() == "View")
                {
                    int index = Int32.Parse(e.CommandArgument.ToString());
                    GridViewRow row = gvAdjuntar.Rows[index];
                    TableCell cell = row.Cells[1]; //ID is displayed in 2nd column  
                    int i = int.Parse(cell.Text);

                    documentID = i;

                    string fileName = System.Configuration.ConfigurationManager.AppSettings["RootURL"].ToString().Trim();

                    fileName = fileName + "Documents\\";

                    string[] fileNames = System.IO.Directory.GetFiles(fileName, @"*" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "*");

                    fileName = fileNames[0].ToString();

                    fileName = fileName.Substring(fileName.LastIndexOf('.'));

                    string fileType = fileName.Substring(fileName.LastIndexOf('.') + 1).ToUpper();

                    ddlTransaction.SelectedIndex = ddlTransaction.Items.IndexOf(ddlTransaction.Items.FindByValue(row.Cells[4].Text.Trim()));

                    //Session["Transaction"] = ddlTransaction.SelectedIndex;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('Documents/" + documentID.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "" + fileName + "','" + fileType + "','status=yes,menubar,scrollbars=yes,resizable=yes,copyhistory=no,width=1150,height=725');", true);
                }
            }
            catch (Exception exp)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = exp.Message;
                return;
            }

            mpeAdjunto.Show();
        }

        protected void gvAdjuntar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;

            }
            catch (Exception exc)
            {

            }
        }

        protected void gvAdjuntar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
                customer = taskControl.Customer;
                int index = e.RowIndex;
                GridViewRow row = gvAdjuntar.Rows[index];
                TableCell cell = row.Cells[1]; // ID is displayed in 2nd column  
                int i = int.Parse(cell.Text);

                //Se elimna de la tabla
                EPolicy.Customer.Customer.DeleteDocumentsByDocumentsID(i);

                //Se elimina el documento fisicamente
                string fileName = System.Configuration.ConfigurationManager.AppSettings["RootURL"].ToString().Trim();

                fileName = fileName + "Documents\\";

                string[] fileNames = System.IO.Directory.GetFiles(fileName, @"*" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "*");

                fileName = fileNames[0].ToString();

                //fileName = fileName + "Documents\\" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".pdf";

                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Document has been deleted!');", true);

                FillTextControl();
                mpeAdjunto.Show();
            }
            catch (Exception exp)
            {
                //ShowMessageDialog(exp.Message);
            }
        }

        protected void ddlTransaction_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Session["Transaction"] = ddlTransaction.SelectedIndex;
            mpeAdjunto.Show();
        }
        #endregion UPLOAD DOCUMENTS
        protected void ddlCancellationMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TaskControl.Policy taskControl = GetTaskControl();

            //if (taskControl.PolicyClassID == 3 && ddlCancellationMethod.SelectedItem.Text == "ShortRate") //--> AUTO
            //{
            //    EnableControls();
            //    BtnCalculate.Visible = false;
            //}
            //else
            //{
            //    BtnCalculate.Visible = true;
            //}

        }

        protected void ddlCancellationReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CancellationReasonID = 0;

            try
            {
                CancellationReasonID = int.Parse(ddlCancellationReason.SelectedItem.Value.ToString().Trim());

                Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

                DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];


                DbRequestXmlCooker.AttachCookItem("CancellationReasonID", SqlDbType.Int, 0, CancellationReasonID.ToString(), ref cookItems);

                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                DataTable dt = Executor.GetQuery("GetCancellationMethotByCancellationReasonID", xmlDoc);

                ddlCancellationMethod.DataSource = dt;
                ddlCancellationMethod.DataTextField = "CancellationMethodDesc";
                ddlCancellationMethod.DataValueField = "CancellationMethodID";
                ddlCancellationMethod.DataBind();
            }
            catch (Exception ex)
            {

            }

        }

        private void SubmitRequestEmail(string status)
        {
            try 
            {
                TaskControl.Policy taskControl = GetTaskControl();
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                DataTable dt = new DataTable();

                string emailNoreplay = "guardian.insurance@guardianinsurance.com";
                string EmailNoReplayPass = "gicSTTGI1";
                string emailSend = "lsemailservice@gmail.com";
                string msg = "";
                string pdf = "";
                string SubmittedEmail = "";
                string ExportFilesPath = ConfigurationManager.AppSettings["ExportsFilesPathName"].ToString().Trim();
                string DocumentPath = System.Configuration.ConfigurationManager.AppSettings["DocumentsPath"];
                MailMessage SM = new MailMessage();
                string InsuredName = taskControl.Customer.FirstName.ToString() == "" && taskControl.Customer.LastName1.ToString() == "" ? taskControl.Customer.LastName2.ToString() : taskControl.Customer.FirstName.ToString() + " " + taskControl.Customer.LastName1.ToString();

                // remplaza los "enters/newlines" con <br> para que el comentario retenga su fromato en los emails.
                msg = txtStatusComment.Text.Trim();
                int text = msg.IndexOf("\n");
                msg = Regex.Replace(msg, @"\r\n?|\n", "<br>");
                //////////////////////////////////////////////

                if (status.Trim() != "")
                {
                    try
                    {
                        dt = GetPolicyCancellationByTaskcontrolID(GetPolicyTaskControlID());
                        int userId = 0;
                        userId = int.Parse(dt.Rows[0]["SubmittedUserID"].ToString());
                        SubmittedEmail = GetUserEmail(userId);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error trying to find the submited email...");
                    }

                    SM.Subject = "POLICY CANCELLATION REQUEST – " + InsuredName;
                    SM.From = new System.Net.Mail.MailAddress(emailNoreplay);
                    SM.Body = "<p>The Policy Cancellation in ePPS for the subject prospect has been given the following status: " + status.ToUpper() + "<br><br>" + msg + "<br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + taskControl.AgentDesc.ToString().Trim() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                    SM.IsBodyHtml = true;

                    if (HttpContext.Current.Request.Url.ToString().Contains("localhost") || HttpContext.Current.Request.Url.ToString().Contains("epps-test") || HttpContext.Current.Request.Url.ToString().Contains("192.168.21.11:84"))
                    {
                        // ====TEST=====dmoyett@lanzasoftware.com
                        //SM.To.Add("smartinez@guardianinsurance.com");
                        //SM.To.Add("dmoyett@lanzasoftware.com");
                        //SM.To.Add("rcruz@guardianinsurance.com");
                        SM.CC.Add("rcruz@guardianinsurance.com");
                        SM.To.Add(SubmittedEmail);
                        SM.Bcc.Add("dmoyett@lanzasoftware.com");
                        //============
                    }
                    else
                    {
                        //====PROD====
                       // SM.To.Add("rcruz@guardianinsurance.com");
                        //SM.CC.Add("rcruz@guardianinsurance.com");
                        // SM.To.Add("smartinez@guardianinsurance.com");
                        //SM.To.Add("smartinez@guardianinsurance.com");
                        SM.CC.Add("rcruz@guardianinsurance.com");
                        SM.To.Add(SubmittedEmail);
                        //SM.Bcc.Add("dmoyett@lanzasoftware.com");
                        //============
                    }
                }
                else
                {
                    CreateCancellationReports("email");
                    pdf = TextBox1.Text.Trim();
                    //System.Data.DataTable dt = EPolicy.Customer.Customer.GetDocumentsByCustomerNo(taskControl.CustomerNo, GetPolicyTaskControlID(), taskControl.isQuote ? 0 : taskControl.TCIDQuotes);
                    SM.Subject = "POLICY CANCELLATION REQUEST – " + InsuredName;
                    SM.From = new System.Net.Mail.MailAddress(emailNoreplay);
                    SM.Body = "<p>We have submitted a Policy Cancellation in ePPS for the subject prospect and are hereby requesting Guardian’s acceptance in order to issue the Cancellation. Your prompt response is appreciated. <br><br>" + "Thank you, " + "<br>" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim() + "-" + taskControl.AgentDesc.ToString().Trim() + "</p>";//+" QUOTE #: <b>"+taskControl.TaskControlID.ToString()+"<b>"+
                    SM.IsBodyHtml = true;
                    SM.Attachments.Add(new Attachment(ExportFilesPath + pdf));

                    if (HttpContext.Current.Request.Url.ToString().Contains("localhost") || HttpContext.Current.Request.Url.ToString().Contains("epps-test") || HttpContext.Current.Request.Url.ToString().Contains("192.168.21.11:84"))
                    {
                        // ====TEST=====dmoyett@lanzasoftware.com
                        //SM.To.Add("smartinez@guardianinsurance.com");
                        //SM.To.Add("dmoyett@lanzasoftware.com");
                        SM.To.Add("rcruz@guardianinsurance.com");
                        SM.Bcc.Add("dmoyett@lanzasoftware.com");
                        //============
                    }
                    else
                    {
                        //====PROD====
                        SM.To.Add("rcruz@guardianinsurance.com");
                        // SM.To.Add("smartinez@guardianinsurance.com");
                        //SM.To.Add("dmoyett@lanzasoftware.com");
                        SM.Bcc.Add("dmoyett@lanzasoftware.com");
                        //============
                    }
                }
                try
                {
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailNoreplay, EmailNoReplayPass);
                    client.Host = ConfigurationManager.AppSettings["IPMail"].ToString().Trim();
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(SM);
                }
                catch (Exception exc)
                {
                    msg = exc.InnerException.ToString() + " " + exc.Message;
                }

                lblRecHeader.Text = "Cancellation sent for submission confirmation!";
                mpeSeleccion.Show();
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message.ToString();
                mpeSeleccion.Show();
            }
        }


        private List<string> PrintCancellation(List<string> mergePaths, string FullPath)
        {
            try
            {
                TaskControl.Policy taskControl = GetTaskControl();
                ReportViewer viewer = new ReportViewer();
                string[] Paths = FullPath.Split("/".ToCharArray());
                string Name = Paths[Paths.Length - 1];
                viewer.LocalReport.ReportPath = Server.MapPath("Reports/" + FullPath + ".rdlc");
                viewer.LocalReport.DataSources.Clear();
                viewer.ProcessingMode = ProcessingMode.Local;

                GetReportCancellationTableAdapters.GetReportCancellationTableAdapter ta = new GetReportCancellationTableAdapters.GetReportCancellationTableAdapter();

                GetReportCancellation.GetReportCancellationDataTable dt = new GetReportCancellation.GetReportCancellationDataTable();

                ta.Fill(dt, taskControl.TaskControlID);

                Microsoft.Reporting.WebForms.ReportDataSource cancellation =
                new Microsoft.Reporting.WebForms.ReportDataSource("GetReportCancellation_GetReportCancellation", (DataTable)dt);

                viewer.LocalReport.DataSources.Add(cancellation);
                viewer.LocalReport.Refresh();

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType;
                string encoding = string.Empty;
                string extension;
                //string fileName =  "BOPCodingSheet" ;
                string _FileName = ("Cancellation" + Name + taskControl.PolicyType + taskControl.TaskControlID.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".pdf");

                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName))
                    File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (FileStream fs = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);
                return mergePaths;
            }
            catch (Exception ecp)
            {
                throw new Exception(ecp.Message);
            }
        }

        private bool CancelPolicyPPS(ref string Message)
        {
            try
            {
                LogError("1");
                TaskControl.Policy taskControl = GetTaskControl();
                string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

                SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                System.Data.DataTable PPSPolicy = new System.Data.DataTable();
                System.Data.DataTable dt = new DataTable();//GetHomeOwnersToPPSByTaskControlID(TaskControlID);


                LogError("2");
                cmd.CommandText = "sproc_ConsumeXMLePPS-CANCELLATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                 string PolicyID = "";
                if (taskControl.Suffix.Trim() == "00")
                    PolicyID = taskControl.PolicyType + int.Parse(taskControl.PolicyNo).ToString();
                else
                    PolicyID = taskControl.PolicyType + int.Parse(taskControl.PolicyNo).ToString() + "-" + taskControl.Suffix.Trim();

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PolicyID", PolicyID.ToString());//"PAP328266");
                cmd.Parameters.AddWithValue("@CancDate", DateTime.Parse(txtCancellationDate.Text.ToString()).ToString("yyyy-MM-dd") + "T00:00:00");//DateTime.Parse(dt.Rows[0]["EffectiveDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                cmd.Parameters.AddWithValue("@CancAmt", TxtTotalReturnPremium.Text.ToString().Trim());//DateTime.Parse(dt.Rows[0]["ExpirationDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                cmd.Parameters.AddWithValue("@CancMethodID", ddlCancellationMethod.SelectedItem.Value.ToString());
                cmd.Parameters.AddWithValue("@CancReasonID", ddlCancellationReason.SelectedItem.Value.ToString());
                cmd.Parameters.AddWithValue("@ePPSTotPremium", taskControl.TotalPremium.ToString());
				cmd.Parameters.AddWithValue("@Factor", TxtUnearnedPercent.Text.ToString().Trim());

                LogError("3");
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                sqlConnection1.Close();

                LogError("4");
                if (PPSPolicy.Rows.Count > 0)
                {
                     LogError("5 - " + PPSPolicy.Rows[0][0].ToString() + "-" + txtCancellationDate.Text + "-" + TxtTotalReturnPremium.Text + "-" + ddlCancellationMethod.SelectedItem.Value + "-" + ddlCancellationReason.SelectedItem.Value + "-" + taskControl.TotalPremium.ToString());
                    if (PPSPolicy.Rows[0][0].ToString() == "ENDOSO")
                    {
                        Message = "This Policy Has Endorsement Cancellation must be done from PPS.";
                        return false;
                    }
                    else if (PPSPolicy.Rows[0][0].ToString() == "OK")
                        return true;
                    else
                        return false;
                }
                else
                {
                    Message = "Error Cancellation PPS, Please Try Again.";
                    return false;
                }
            }
            catch (Exception exp)
            {
                LogError("6 -" + exp.Message);
                Message = exp.Message.ToString();
                return false;
            }
        }

        private void LogError(string exp)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", exp);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", exp);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", exp);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", exp.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        private void AddPolicyCancellation(int TaskcontrolID)
        {
             EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[25];
                DbRequestXmlCooker.AttachCookItem("TaskcontrolID", SqlDbType.Int, 0, TaskcontrolID.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancellationDate", SqlDbType.DateTime, 0, txtCancellationDate.Text, ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancellationEntryDate", SqlDbType.DateTime, 0, TxtCancellationEntryDate.Text, ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancellationUnearnedPercent", SqlDbType.Float, 0, TxtUnearnedPercent.Text, ref cookItems);  
                DbRequestXmlCooker.AttachCookItem("ReturnCharge", SqlDbType.Float, 0, TxtReturnCharge.Text.Trim(), ref cookItems);     
                DbRequestXmlCooker.AttachCookItem("CancellationMethod", SqlDbType.Int, 0, ddlCancellationMethod.SelectedItem.Value.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancellationReason", SqlDbType.Int, 0, ddlCancellationReason.SelectedItem.Value.ToString(), ref cookItems);

                DbRequestXmlCooker.AttachCookItem("ReturnPremium", SqlDbType.Float, 0, TxtReturnPremium.Text.Trim(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Status", SqlDbType.VarChar, 50, "", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancellationAmount", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("ReturnCommissionAgency", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancLossFund", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancOverHead", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancBankFee", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancProfit", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancConcurso", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancDealerProfit", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancCanReserve", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancWarranty", SqlDbType.Float, 0, "0.00", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancInitialMileage", SqlDbType.Int, 0, "0", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CancMileage", SqlDbType.Int, 0, "0", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Submitted", SqlDbType.Bit, 0, "True", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Approved", SqlDbType.Bit, 0, "False", ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Rejected", SqlDbType.Bit, 0, "False", ref cookItems);

                DbRequestXmlCooker.AttachCookItem("SubmittedUserID", SqlDbType.Int, 0, cp.UserID.ToString(), ref cookItems);

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

                try
                {
                    exec.GetQuery("AddPolicyCancellation", xmlDoc);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to add Yacht Information, please try again.", ex);
                }

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }
        private DataTable GetPolicyCancellationByTaskcontrolID(int TaskcontrolID)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("TaskcontrolID", SqlDbType.Int, 0, TaskcontrolID.ToString(), ref cookItems);
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
                try
                {
                    dt =  exec.GetQuery("GetPolicyCancellationByTaskcontrolID", xmlDoc);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to get Cancellation info, please try again.", ex);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void UpdatePolicyCancellationStatusByTaskcontrolID(int TaskcontrolID,string Status,bool Set) //aqui
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];
                DbRequestXmlCooker.AttachCookItem("TaskcontrolID", SqlDbType.Int, 0, TaskcontrolID.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Status", SqlDbType.VarChar, 25, Status.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("Set", SqlDbType.Bit, 0, Set.ToString(), ref cookItems);

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
                try
                {
                    exec.GetQuery("UpdatePolicyCancellationStatusByTaskcontrolID", xmlDoc);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to update Cancellation status, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
		
		private int GetPolicyTaskControlID()
        {
            TaskControl.QuoteAuto taskControl2;
            TaskControl.Policy taskControl;
            TaskControl.RoadAssistance taskControl3;
            //EPolicy.TaskControl.Autos taskControl4 = (EPolicy.TaskControl.Autos)Session["TaskControl-Policy"]; 
            EPolicy.TaskControl.Autos taskControl4 = (EPolicy.TaskControl.Autos)Session["TaskControl"]; 

            return taskControl4.TaskControlID;
        }

        private string GetUserEmail(int User)
        {
            string Result = "";
            try
            {
                string conString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
//                       String.Format(@"SELECT top 1 AU.* FROM 
//                                        AuthenticatedUser AU
//                                        WHERE AccountDisable = 0 and RTRIM(LTRIM(CONCAT(RTRIM(AU.FirstName),' ',RTRIM(AU.LastName)))) =  RTRIM(LTRIM({0}))"
//                                       , "'" + User.Trim() + "'"), connection))

                     String.Format(@"SELECT top 1 AU.* FROM 
                                        AuthenticatedUser AU
                                        WHERE AccountDisable = 0 and UserID =  {0}"
                                       , User.ToString()), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var tb = new System.Data.DataTable();
                            tb.Load(reader);
                            if (tb != null)
                            {
                                if (tb.Rows.Count > 0)
                                {
                                    Result = tb.Rows[0]["Email"].ToString();
                                }
                                else
                                {
                                    Result = "";
                                }
                            }
                        }
                    }
                }
                return Result;
            }
            catch (Exception)
            {
                return Result;
            }
        }
    }
}
