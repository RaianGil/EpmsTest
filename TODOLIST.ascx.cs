namespace EPolicy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;
    using EPolicy.TaskControl;
    using Baldrich.DBRequest;
    using EPolicy.XmlCooker;
    using System.Xml;

    public partial class TODOLIST : System.Web.UI.UserControl
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            string myScript = "";

            if (cp.UserID == 1)//cp.IsInRole("ADMINISTRATOR")
            {

                myScript = @"<script>         function pageLoad() {
                    $(document).ready(function () {

                        var cookieHeight = 'divHeight';
                        var cookieWidth = 'divWidth';

                        //On document ready, if we find height from our cookie, 
                        //we set the div to this height.
                        var height = Cookies.get(cookieHeight);
                        var width = Cookies.get(cookieWidth);
                        if (height != null && width != null) {
                            $('#note').css('height', height + 'px');
                            $('#note').css('width', width + 'px');
                        }

                        $(""#note"").resizable({
                            maxHeight: 1000,
                            minHeight: 300,
                            minWidth: 500,
                            handles: 'ne, se, sw, nw',
                            stop: function (e, ui) {
                                Cookies.set(""divHeight"", ui.size.height);
                                Cookies.set(""divWidth"", ui.size.width);
                            }
                        });

                        $(""#note"").draggable({ containment: ""window"", cancel: ""#border"" });

                        var $window = $(window),
                        $body = $('body'),
                        $note = $(""#note"");

                        $note.fadeIn();

                        $('.close').on('click', function () {
                            $note.fadeOut(""slow"");
                            $window.off('scroll');
                        });
                    });
                } </script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript, false);
                FillGridHO();
            }
            else if (cp.IsInRole("HOME OWNERS APPROVE"))
            {
                myScript = @"<script>         function pageLoad() {
                    $(document).ready(function () {

                        var cookieHeight = 'divHeight';
                        var cookieWidth = 'divWidth';

                        //On document ready, if we find height from our cookie, 
                        //we set the div to this height.
                        var height = Cookies.get(cookieHeight);
                        var width = Cookies.get(cookieWidth);
                        if (height != null && width != null) {
                            $('#note').css('height', height + 'px');
                            $('#note').css('width', width + 'px');
                        }

                        $(""#note"").resizable({
                            maxHeight: 1000,
                            minHeight: 300,
                            minWidth: 500,
                            handles: 'ne, se, sw, nw',
                            stop: function (e, ui) {
                                Cookies.set(""divHeight"", ui.size.height);
                                Cookies.set(""divWidth"", ui.size.width);
                            }
                        });

                        $(""#note"").draggable({ containment: ""window"", cancel: ""#border"" });

                        var $window = $(window),
                        $body = $('body'),
                        $note = $(""#note"");

                        $note.fadeIn();

                        $('.close').on('click', function () {
                            $note.fadeOut(""slow"");
                            $window.off('scroll');
                        });
                    });
                } </script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript, false);
                FillGridHO();
            }
            else if (cp.IsInRole("HOME OWNERS REQUEST"))
            {
                myScript = @"<script>         function pageLoad() {
                    $(document).ready(function () {

                        var cookieHeight = 'divHeight';
                        var cookieWidth = 'divWidth';

                        //On document ready, if we find height from our cookie, 
                        //we set the div to this height.
                        var height = Cookies.get(cookieHeight);
                        var width = Cookies.get(cookieWidth);
                        if (height != null && width != null) {
                            $('#note').css('height', height + 'px');
                            $('#note').css('width', width + 'px');
                        }

                        $(""#note"").resizable({
                            maxHeight: 1000,
                            minHeight: 300,
                            minWidth: 500,
                            handles: 'ne, se, sw, nw',
                            stop: function (e, ui) {
                                Cookies.set(""divHeight"", ui.size.height);
                                Cookies.set(""divWidth"", ui.size.width);
                            }
                        });

                        $(""#note"").draggable({ containment: ""window"", cancel: ""#border"" });

                        var $window = $(window),
                        $body = $('body'),
                        $note = $(""#note"");

                        $note.fadeIn();

                        $('.close').on('click', function () {
                            $note.fadeOut(""slow"");
                            $window.off('scroll');
                        });
                    });
                } </script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript, false);
                FillGridHO();
            }
        }

        private void FillGrid()
        {
            try
            {

                GridQuotes.DataSource = null;
                DataTable dt = new DataTable();

                dt.Columns.Add("TaskControlID");
                dt.Rows.Add("1234");
                dt.Rows.Add("1235");
                dt.Rows.Add("1236");
                dt.Rows.Add("1237");
                dt.Rows.Add("1238");

                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        GridQuotes.DataSource = dt;
                        GridQuotes.DataBind();
                    }
                    else
                    {
                        GridQuotes.DataSource = null;
                        GridQuotes.DataBind();
                    }
                }
                else
                {
                    GridQuotes.DataSource = null;
                    GridQuotes.DataBind();
                }
            }
            catch (Exception exp)
            {
            }
        }

        private void FillGridHO()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                GridQuotes.DataSource = null;
                DataTable dt = null;
                string Enteredby = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();

                if (cp.IsInRole("HOME OWNERS REQUEST"))
                    dt = GetTodoListHO(" where HOQ.Approved = 0 AND HOQ.Submitted = 1 AND HOQ.Rejected <> 1 AND TC.EnteredBy = '" + Enteredby + "'");
                else
                    dt = GetTodoListHO(" where HOQ.Approved = 0 AND HOQ.Submitted = 1 AND HOQ.Rejected <> 1");

                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        GridQuotes.DataSource = dt;
                        GridQuotes.DataBind();
                        if (cp.IsInRole("HOME OWNERS REQUEST"))
                        {
                            GridQuotes.Columns[4].Visible = false;
                            GridQuotes.Columns[5].Visible = false;
                        }
                    }
                    else
                    {
                        GridQuotes.DataSource = null;
                        GridQuotes.DataBind();
                    }
                }
                else
                {
                    GridQuotes.DataSource = null;
                    GridQuotes.DataBind();
                }
            }
            catch (Exception exp)
            {
            }
        }

        protected void GridQuotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int index = 0;
                GridViewRow row = null;
                switch (e.CommandName)
                {
                    case "Select": //Edit
                        index = Int32.Parse(e.CommandArgument.ToString());
                        row = GridQuotes.Rows[index];
                        DataTable DtTaskControl = GetTodoListHO(" where HOQ.Approved = 0 AND HOQ.Submitted = 1 AND HOQ.Rejected <> 1 AND TC.TaskControlID =" + row.Cells[1].Text.Trim());
                        Session.Add("DtTaskControl", DtTaskControl);
                        //Si tiene un solo record se va a dirigir a la pantalla correspondiente.
                        if (DtTaskControl.Rows.Count == 1)
                            GoToSpecificWebPage();
                        break;
                    case "Approved":
                        index = Int32.Parse(e.CommandArgument.ToString());
                        row = GridQuotes.Rows[index];
                        UpdateHomeOwnersStatus(row.Cells[1].Text.Trim(), false, false);
                        EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(row.Cells[1].Text.Trim(), cp.Identity.Name.Split("|".ToCharArray())[0].ToString() + "- APPROVED " + "QUOTE#: " + row.Cells[1].Text.Trim());
                        FillGridHO();
                        break;
                    case "Rejected":
                        index = Int32.Parse(e.CommandArgument.ToString());
                        row = GridQuotes.Rows[index];
                        UpdateHomeOwnersStatus(row.Cells[1].Text.Trim(), true, false);
                        EPolicy.Customer.Customer.AddCustomerCommentsByTaskControlID(row.Cells[1].Text.Trim(), cp.Identity.Name.Split("|".ToCharArray())[0].ToString() + "- DECLINED " + "QUOTE#: " + row.Cells[1].Text.Trim());
                        FillGridHO();
                        break;
                    case "Delete":

                        break;
                    default: //Page
                        break;
                }

            }
            catch (Exception exp)
            {
            }
        }

        protected void GridQuotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //CalculatePremium();
        }

        protected void GridQuotes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[2].Visible = false; //created date 

            }
            catch (Exception exc)
            {

            }
        }

        private void GoToSpecificWebPage()
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

            DataTable dtSpec = (DataTable)Session["DtTaskControl"];

            int i = int.Parse(dtSpec.Rows[0]["TaskControlID"].ToString());
            TaskControl.TaskControl taskControl = TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);

            Session["Prospect"] = taskControl.Prospect;

            if (Session["DtTaskControl"] != null)
            {

                DataTable dtFilter = (DataTable)Session["DtTaskControl"];

                DataTable dt = dtFilter.Clone();

                dt.Columns.Remove("OriginatedAt");

                DataRow[] dr =
                    dtFilter.Select("TaskControlTypeID = " + taskControl.TaskControlTypeID, "TaskControlID");

                for (int rec = 0; rec <= dr.Length - 1; rec++)
                {
                    DataRow myRow = dt.NewRow();
                    myRow["TaskControlID"] = (int)dr[rec].ItemArray[0];
                    myRow["TaskControlTypeID"] = (int)dr[rec].ItemArray[3];

                    dt.Rows.Add(myRow);
                    dt.AcceptChanges();
                }

                taskControl.NavegationTaskControlTable = dt;

                string ToPage;

                if (Session["ToPage"] == null)
                {
                    if (taskControl.TaskControlTypeID == 4)
                    {
                        ToPage = "ExpressAutoQuote.aspx";
                    }
                    else
                    {
                        if (taskControl.TaskControlTypeID == 15) //PersonalPackageQuote
                        {
                            ToPage = "PersonalPackage.aspx";
                        }
                        else
                        {
                            if (taskControl.TaskControlTypeID == 18) // Use for QCertified
                            {
                                ToPage = "VehicleServiceContractQuote.aspx";
                            }
                            else
                            {
                                ToPage = taskControl.GetType().Name.Trim() + ".aspx";
                            }
                        }
                    }
                }
                else
                {
                    ToPage = Session["ToPage"].ToString();
                }

                if (Session["TaskControl"] == null)
                    Session.Add("TaskControl", taskControl);
                else
                    Session["TaskControl"] = taskControl;

                Session.Remove("DtTaskControl");



                Response.Redirect(ToPage + "?" + taskControl.TaskControlID);
            }
        }

        private DataTable GetTodoListHO(string WhereClause) 
        {
            DataTable dt = new DataTable();

            DBRequest Executor = new DBRequest();

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("WhereClause",
                SqlDbType.VarChar, 8000, WhereClause.ToString(),
                ref cookItems);

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                dt = Executor.GetQuery("GetTodoListHO", xmlDoc);
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        private DataTable UpdateHomeOwnersStatus(string TaskControlID, bool Rejected, bool Revert)
        {
            DataTable dt = new DataTable();

            DBRequest Executor = new DBRequest();

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Rejected",
                SqlDbType.Bit, 0, Rejected.ToString(),
                ref cookItems);
            
            DbRequestXmlCooker.AttachCookItem("Revert",
                SqlDbType.Bit, 0, Revert.ToString(),
                ref cookItems);

            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                dt = Executor.GetQuery("UpdateHomeOwnersStatus", xmlDoc);
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }
    }
}