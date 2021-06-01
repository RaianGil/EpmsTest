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


using EPolicy;
using EPolicy.TaskControl;
using EPolicy2.Reports;
//using Topaz;

namespace EPolicy
{
	/// <summary>
	/// Summary description for PrintPolicy.
	/// </summary>
    public partial class PrintPolicy : System.Web.UI.Page
    {
        private Control LeftMenu;

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            Control Banner = new Control();
            Banner = LoadControl(@"TopBanner.ascx");
            //((Baldrich.BaldrichWeb.Components.TopBanner)Banner).SelectedOption = (int)Baldrich.HeadBanner.MenuOptions.Home;
            this.Placeholder1.Controls.Add(Banner);

            //Setup Left-side Banner

            LeftMenu = new Control();
            LeftMenu = LoadControl(@"LeftMenu.ascx");
            //((Baldrich.BaldrichWeb.Components.MenuEventControl)LeftMenu).Height = "534px";
            phTopBanner1.Controls.Add(LeftMenu);
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
            if (!IsPostBack)
            {
                //this.SetReferringPage();
                FillTextControl();
                EPolicy.TaskControl.Policy taskControl;
                if (Session["TaskControlPolicy"] != null)
                {
                    taskControl = (EPolicy.TaskControl.Policy)Session["TaskControlPolicy"];
                }
                else
                {
                    taskControl = (EPolicy.TaskControl.Policy)Session["TaskControl"];
                }
                
                if (taskControl.PolicyClassID == 1) // VSC
                {
                    ChkPrintJustOne.Visible = false;
                    hplAgreements.Visible = true;
                }


                //if (Request.QueryString["SigData"] != null)
                //{
                //    //private Topaz.SigPlusNET sp;
                //    Topaz.SigPlusNET sp = new Topaz.SigPlusNET();
                    
                //    sp.AutoKeyStart();
                //    sp.SetImageXSize(200); //Witdh
                //    sp.SetImageYSize(50); //Height
                //    sp.SetSigCompressionMode(1);
                //    sp.SetDisplayPenWidth(5);
                //    sp.SetJustifyMode(5);
                //    sp.SetImageFileFormat(0);
                //    sp.AutoKeyFinish();
                //    sp.SetEncryptionMode(2);
                //    sp.SetSigString(Request.QueryString["SigData"].ToString());

                //    System.Drawing.Image im = sp.GetSigImage();

                //    Login.Login cp = HttpContext.Current.User as Login.Login;
                //    string filename = "C:\\Inetpub\\wwwroot\\Optima\\Sign\\" + cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim().Replace(" ", "") + ".bmp";
                //    im.Save(filename);

                    
                //}
            }
        }

        private void FillTextControl()
        {
            //if (Request.QueryString["taskControlID"] != null)
            //{
            //    Login.Login cp = HttpContext.Current.User as Login.Login;
            //    int userID = 0;
            //    userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            //    taskControl = (TaskControl.Policy) TaskControl.TaskControl.GetTaskControlByTaskControlID(int.Parse(Request.QueryString["taskControlID"].Trim()), userID);
            //}

            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            EPolicy.TaskControl.Policy taskControl;

            if (Session["TaskControlPolicy"] != null)
            {
                taskControl = (EPolicy.TaskControl.Policy)Session["TaskControlPolicy"];
                EPolicy.TaskControl.QuoteAuto taskControl2 = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                taskControl = taskControl2.Policy;
                taskControl = (EPolicy.TaskControl.Policy)taskControl.GetPrintPolicyReport((Policy)taskControl, "Interno");
            }
            else
            {
                taskControl = (EPolicy.TaskControl.Policy)Session["TaskControl"];
                taskControl = (EPolicy.TaskControl.Policy)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                taskControl = (EPolicy.TaskControl.Policy)taskControl.GetPrintPolicyReport((Policy)taskControl, "Interno");
            }

            //EPolicy.TaskControl.Policies taskControl = (EPolicy.TaskControl.Policies) Session["TaskControl"];
            //taskControl = (EPolicy.TaskControl.Policies)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
            //taskControl = (EPolicy.TaskControl.Policies)taskControl.GetPrintPolicyReport((Policy)taskControl, "Interno");

            if (taskControl.PrintPolicyList != null)
            {
                ChkPolicyList.DataSource = taskControl.PrintPolicyList;
                ChkPolicyList.DataTextField = "ReportDescription";
                ChkPolicyList.DataValueField = "ReportFileName";
                ChkPolicyList.DataBind();

                for (int i = 0; i <= ChkPolicyList.Items.Count - 1; i++)
                {
                    ChkPolicyList.Items[i].Selected = true;
                }
            }

            if (taskControl.PrintCopyList != null)
            {
                ChkCopyList.DataSource = taskControl.PrintCopyList;
                ChkCopyList.DataTextField = "ReportCopyName";
                ChkCopyList.DataValueField = "ReportCopyFooter";
                ChkCopyList.DataBind();

                for (int i = 0; i <= ChkCopyList.Items.Count - 1; i++)
                {
                    ChkCopyList.Items[i].Selected = true;
                }
            }

            if (taskControl.PrintJustOnceList != null)
            {
                ChkPrintJustOne.DataSource = taskControl.PrintJustOnceList;
                ChkPrintJustOne.DataTextField = "ReportDescription";
                ChkPrintJustOne.DataValueField = "ReportFileName";
                ChkPrintJustOne.DataBind();

                for (int i = 0; i <= ChkPrintJustOne.Items.Count - 1; i++)
                {
                    ChkPrintJustOne.Items[i].Selected = true;
                }
            }
        }

        private void History(EPolicy.TaskControl.Policy taskControl, int userID)
        {
            Audit.History history = new Audit.History();

            history.Actions = "PRINT";
            history.KeyID = taskControl.TaskControlID.ToString();
            history.Subject = "AUTOPERSONALPOLICY";
            history.BuildNotesForHistory("TaskControlID.", "", taskControl.TaskControlID.ToString(), 5);  //5 = mode Print
            history.UsersID = userID;
            history.GetSaveHistory();
        }

        private void ReturnToReferringPage()
        {
            string wp = Session["FromUI"].ToString();
            Session.Remove("FromUI");
            Response.Redirect(wp);
        }

        protected void PrintPolicies_Click(object sender, System.EventArgs e)
        {

            //TaskControl.Policy taskControl = (TaskControl.Policy) Session["TaskControlPolicy"];

            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            EPolicy.TaskControl.Policy taskControl;

            if (Session["TaskControlPolicy"] != null)
            {
                taskControl = (EPolicy.TaskControl.Policy)Session["TaskControlPolicy"];
                EPolicy.TaskControl.QuoteAuto taskControl2 = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                taskControl = taskControl2.Policy;
                taskControl = (EPolicy.TaskControl.Policy)taskControl.GetPrintPolicyReport((Policy)taskControl, "Interno");
            }
            else
            {
                taskControl = (EPolicy.TaskControl.Policy)Session["TaskControl"];
                taskControl = (EPolicy.TaskControl.Policy)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                taskControl = (EPolicy.TaskControl.Policy)taskControl.GetPrintPolicyReport((Policy)taskControl, "Interno");
            }
            //TaskControl.Policies taskControl = (TaskControl.Policies)Session["TaskControl"];
            //taskControl = (TaskControl.Policies)TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
            //taskControl = (TaskControl.Policies)taskControl.GetPrintPolicyReport((Policy)taskControl, "Interno");

            taskControl.PrintPolicyReady = null;
            taskControl.PrintJustOnceList = null;
            taskControl.PrintCopyReady = null;

            taskControl.PrintPolicyReady = taskControl.PrintPolicyList.Clone();
            taskControl.PrintCopyReady = taskControl.PrintCopyList.Clone();

            for (int i = 0; i <= this.ChkPolicyList.Items.Count - 1; i++)
            {
                if (this.ChkPolicyList.Items[i].Selected)
                    taskControl.AddReportPrintList(this.ChkPolicyList.Items[i].Text.ToString(), this.ChkPolicyList.Items[i].Value.ToString());
            }

            for (int i = 0; i <= this.ChkPrintJustOne.Items.Count - 1; i++)
            {
                if (this.ChkPrintJustOne.Items[i].Selected)
                    taskControl.AddReportPrintList(this.ChkPrintJustOne.Items[i].Text.ToString(), this.ChkPrintJustOne.Items[i].Value.ToString());
            }

            for (int i = 0; i <= this.ChkCopyList.Items.Count - 1; i++)
            {
                if (this.ChkCopyList.Items[i].Selected)
                    taskControl.AddReportCopyList(this.ChkCopyList.Items[i].Text.ToString(), this.ChkCopyList.Items[i].Value.ToString());
            }

            try
            {
                if (taskControl.PrintPolicyReady == null)
                    throw new Exception("This policy don't have any document for print.");
                else
                    if (taskControl.PrintPolicyReady.Rows.Count == 0)
                        throw new Exception("This policy don't have any document for print.");
                    else
                        if (taskControl.PrintCopyReady == null)
                            throw new Exception("This policy don't have any document for print.");
                        else
                            if (taskControl.PrintCopyReady.Rows.Count == 0)
                                throw new Exception("This policy don't have any document for print.");

                EPolicy2.Reports.PrintPolicy printPolicy = new EPolicy2.Reports.PrintPolicy();
                //Baldrich.TaskControl.PrintPolicies printPolicy = new Baldrich.TaskControl.PrintPolicies();

                //ActiveReport3 rpt;
                //if (taskControl.PolicyClassID == 3) //Auto Personal
                //{
                //    rpt = printPolicy.SetPrintPolicy(taskControl, (TaskControl.QuoteAuto)Session["TaskControl"], userID);
                //}
                //else
                //{
                //    rpt = printPolicy.SetPrintPolicy(taskControl, userID);
                //}

                //this.History(taskControl, userID);

                //Session.Add("Report", rpt);
                //Session.Add("FromPage", "PrintPolicy.aspx");
                //Response.Redirect("ActiveXViewer.aspx");
            }
            catch (Exception xcp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message);
                this.litPopUp.Visible = true;
            }
        }

        protected void BtnBack_Click(object sender, System.EventArgs e)
        {
            Session.Remove("Report");
            ReturnToReferringPage();
        }

        protected void btnSign_Click(object sender, EventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            string filename = cp.Identity.Name.Split("|".ToCharArray())[0].ToString().Trim().Replace(" ","")+".bmp";
            string js = "<script language=javascript> javascript:popwindow=window.open('CustomerSign.htm?Sign=" + filename +
                    "','popwindow','toolbar=no,modalpopup=yes,location=no,directories=no,status=no,menubar,scrollbars=no,resizable=no,copyhistory=no,width=350,height=300,left=435, top=200');popwindow.focus(); </script>";
            Response.Write(js);
        }
        protected void btnPaseDoc_Click(object sender, EventArgs e)
        {
            //SigPlusNET s = new SigPlusNET();
            ////s.SetSigCompressionMode = 1;
            //s.SetSigString(this.hiddenSigString.Value.Trim());
            //s.SetImageFileFormat(4);
            //bool result = s.WriteImageFile("c:\\victor.jpg");

            //bool a = result;
        }
    }
}
