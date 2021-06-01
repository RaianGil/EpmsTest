using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.Xml;
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using EPolicy.LookupTables;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;
using System.Text;


using iTextSharp.text;
//using iTextSharp.xmp;

namespace EPolicy
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public static string DashboardSet = "";
        public static string posType = "";

        #region Table Selection
        private DataTable DtChartCompanySells1 = null;
        private DataTable DtChartCompanySells2 = null;
        private DataTable DtChartCompanySells3 = null;
        private DataTable DtChartCompanySells4 = null;
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                if (cp == null)
                {
                    Response.Redirect("Default.aspx?001");
                }
                else
                {
                    if (!cp.IsInRole("DASHBOARD") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        Response.Redirect("Default.aspx?001");
                    }
                }


                Control Banner = new Control();
                Banner = LoadControl(@"TopBanner.ascx");
                this.PlaceHolder1.Controls.Add(Banner);

                if (!IsPostBack)
                {
                    //DataTable dtGroupCompany = login.GetGroupCompanyByUserID(login.UserID);

                    txtBegDate.Text = DateTime.Now.Month.ToString().Trim() + "/01/" + DateTime.Now.Year.ToString().Trim();
                    txtEndDate.Text = DateTime.Now.ToShortDateString();

                    //Company
                    //ddlCompany.DataSource = dtGroupCompany;
                    //ddlCompany.DataTextField = "CompanyDesc";
                    //ddlCompany.DataValueField = "CompanyID";
                    //ddlCompany.DataBind();
                    //ddlCompany.SelectedIndex = -1;
                    //ddlCompany.Items.Insert(0, "");

                    ddlCompany.SelectedIndex = 0;
                    //if (login.CompanyID != 0)
                    //{
                    //    for (int i = 0; ddlCompany.Items.Count - 1 >= i; i++)
                    //    {
                    //        if (ddlCompany.Items[i].Value == login.CompanyID.ToString())
                    //        {
                    //            ddlCompany.SelectedIndex = i;
                    //            i = ddlCompany.Items.Count - 1;
                    //        }
                    //    }
                    //}

                    txtBegDate.Text = "04/01/2016";
                    txtEndDate.Text = "04/30/2016";

                    SetAllSalesDashboard();
                    BtnReset();
                    BtnTotalSales.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
                    BtnTotalSales.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        public void ExceptionHandler(Exception ex)
        {
            //Get a StackTrace object for the exception
            StackTrace st = new StackTrace(ex, true);

            //Get the first stack frame
            StackFrame frame = st.GetFrame(0);

            //Get the file name
            string fileName = frame.GetFileName();

            //Get the method name
            string methodName = frame.GetMethod().Name;

            //Get the line number from the stack frame
            int line = frame.GetFileLineNumber();

            //Get the column number
            int col = frame.GetFileColumnNumber();

            this.litPopUp.Text = EPolicy.Utilities.MakeLiteralPopUpString("File loaded and processed.");
            this.litPopUp.Visible = true;
        }

        #region Methods
        private void SetGuardianDashboard(string PosType)
        {
            //Set Dashboard to set after choosing a graphic
            DashboardSet = "GuardianSales";
            posType = "";

            //GetDataforChart
            DtChartCompanySells1 = GetDashboardGuardian(txtBegDate.Text.Trim(), txtEndDate.Text.Trim(), PosType);
            //DtChartCompanySells3 = GvPoliciesSold(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            //DtChartCompanySells4 = GvPremiumSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());

            HideControls();

            if (DtChartCompanySells1.Rows.Count > 0)
            {
                SellGuardianDistributionYearChart(PosType);
                //SellGuardianChart();
                //SellPercent();

                FillGuardianSales(PosType);

                ChartCompanySells.Visible = true;
                ddlPosType.Visible = true;
                //ChartPercentage.Visible = true;

                //Div2.Visible = true;
            }
        }
        private void SetAllSalesDashboard()
        {
            //Set Dashboard to set after choosing a graphic
            DashboardSet = "AllSales";
            posType = "";

            //GetDataforChart
            DtChartCompanySells1 = GetDashboardSROVentas(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            DtChartCompanySells3 = GvPoliciesSold(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            DtChartCompanySells4 = GvPremiumSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());

            HideControls();

            if (DtChartCompanySells1.Rows.Count > 0)
            {
                SellChart();
                SellPercent();

                ChartCompanySells.Visible = true;
                ChartPercentage.Visible = true;

                Div2.Visible = true;
            }

            if (DtChartCompanySells3.Rows.Count > 0)
            {
                SellSalesCountChart();
                FillGvSolds();

                Panel3.Visible = true;
                gvSolds.Visible = true;
                ChartSales.Visible = true;

                Div3.Visible = true;
            }

            if (DtChartCompanySells4.Rows.Count > 0)
            {
                SellPremiumSalesChart();
                FillGvPremiumSales();

                Panel4.Visible = true;
                gvPremiums.Visible = true;
                ChartPremium.Visible = true;

                Div4.Visible = true;
            }
        }
        private void SetPosTypeDashboard(string PosType)
        {
            //Set Dashboard to set after choosing a graphic
            DashboardSet = PosType;
            posType = PosType;

            //GetDataforChart
            DtChartCompanySells1 = GvSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim(), PosType);
            //DtChartCompanySells2 = GvPoliciesSold(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            //DtChartCompanySells3 = GvPremiumSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            FillColReportViewer();

            HideControls();

            if (DtChartCompanySells1.Rows.Count > 0)
            {
                SellPosTypeChart(PosType);
                FillGvSales(PosType);

                if (PosType != "Banco") //No buscaré Top Ten para bancos
                {
                    FillTopTen(PosType);

                    //if (PosType == "Colecturia" || PosType == "EOI")
                    imgDashboardMap.Visible = true;
                }

                Panel1.Visible = true;
                ChartCompanySells.Visible = true;
            }

            //if (DtChartCompanySells2.Rows.Count > 0)
            //{
            //    SellSalesCountChart();
            //    FillGvSolds();

            //    Panel2.Visible = true;
            //    gvSolds.Visible = true;
            //    ChartPercentage.Visible = true;
            //}

            //if (DtChartCompanySells3.Rows.Count > 0)
            //{
            //    SellPremiumSalesChart();
            //    FillGvPremiumSales();

            //    Panel3.Visible = true;
            //    gvPremiums.Visible = true;
            //    ChartSales.Visible = true;
            //}
        }
        private void SetWeekDashboard()
        {
            //Set Dashboard to set after choosing a graphic
            DashboardSet = "TimedSales";
            posType = "";

            //GetDataforChart
            DtChartCompanySells1 = GvMonthDateSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            DtChartCompanySells2 = GvWeekNameSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            //DtChartCompanySells3 = GvPremiumSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            FillColReportViewer();

            HideControls();

            if (DtChartCompanySells1.Rows.Count > 0)
            {
                SellWeekDayChart();
                FillGvWeekDate();

                Panel1.Visible = true;
                ChartCompanySells.Visible = true;
                gvSales.Visible = true;
            }

            if (DtChartCompanySells2.Rows.Count > 0)
            {
                SellWeekNameChart();
                FillGvWeekName();

                Panel2.Visible = true;
                Div2.Visible = true;
                ChartPercentage.Visible = true;
            }
        }
        private void SetDistribitionDashboard()
        {
            //Set Dashboard to set after choosing a graphic
            DashboardSet = "DistributionSales";
            posType = "";

            //GetDataforChart
            DtChartCompanySells1 = GvPosTypeDistributions2(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            //DtChartCompanySells2 = GvWeekNameSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            //DtChartCompanySells3 = GvPremiumSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());
            FillColReportViewer();

            HideControls();

            if (DtChartCompanySells1.Rows.Count > 0)
            {
                SellDistributionChart();
                FillGvPosTypeDistribution();

                Panel1.Visible = true;
                ChartCompanySells.Visible = true;
                gvSales.Visible = true;
            }

            //if (DtChartCompanySells2.Rows.Count > 0)
            //{
            //    SellWeekNameChart();
                //FillGvWeekName();

                //Panel2.Visible = true;
                //gvSolds.Visible = true;
            //    ChartPercentage.Visible = true;
            //}
        }

        private void HideControls()
        {
            ReportViewer1.Visible = false;
            ddlPosType.Visible = false;
            gvSales.Visible = false;
            gvSolds.Visible = false;
            gvPremiums.Visible = false;
            gvIndividualSales.Visible = false;
            gvTopTen.Visible = false;
            gvGuardianSales.Visible = false;
            LblTopX.Visible = false;
            TxtTopX.Visible = false;
            BtnTopX.Visible = false;
            BtnExport.Visible = false;
            BtnExportTop.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            ChartCompanySells.Visible = false;
            ChartPercentage.Visible = false;
            ChartSales.Visible = false;
            ChartPremium.Visible = false;

            imgDashboardMap.Visible = false;

            Div2.Visible = false;
            Div3.Visible = false;
            Div4.Visible = false;
        }
        #endregion

        #region Charts' Fill Methods
        private void SellGuardianChart()
        {
            // Populate series data
            double[] yValues = new double[DtChartCompanySells1.Rows.Count];
            string[] xValues = new string[DtChartCompanySells1.Rows.Count];

            //ChartCompanySells.Legends.Add(new Legend("Leyenda"));
            LegendItem newLegendItem = new LegendItem();

            for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
            {
                ChartCompanySells.Series.Add(new Series());

                yValues[i] = double.Parse(DtChartCompanySells1.Rows[i]["PrimaCurrentYear"].ToString());
                xValues[i] = DtChartCompanySells1.Rows[i]["Month"].ToString();

                xValues[i] += "\n" + double.Parse(yValues[i].ToString()).ToString("$###,###");

                ChartCompanySells.Series[i].Points.AddXY(xValues[i], yValues[i]);
                ChartCompanySells.Series[i].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column", true);
                ChartCompanySells.Series[i].LabelFormat = "${0,000.00}";
                ChartCompanySells.Series[i].IsValueShownAsLabel = true;
                ChartCompanySells.Series[i].ToolTip = DtChartCompanySells1.Columns[i].ToString();
                ChartCompanySells.Series[i].Name = DtChartCompanySells1.Rows[i]["Month"].ToString();
                ChartCompanySells.Series[i].PostBackValue = DtChartCompanySells1.Rows[i]["Month"].ToString();
            }

            //ChartCompanySells.Series["Series1"].Points.DataBindXY(xValues, yValues);

            // Set chart type
            //string chartTypeName = "Column";
            //ChartCompanySells.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), chartTypeName, true);

            ChartCompanySells.Titles[0].Text = "Premium Written";
            ChartCompanySells.Titles[0].PostBackValue = "Title"; //SHOW ALL

            // Set X axis margin for the area chart
            ChartCompanySells.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
            ChartCompanySells.ChartAreas[0].AxisX.LabelStyle.Enabled = false; // <- Elimina los label de donde naturalmente irian los nombre de las compañias

            // Show as 2D or 3D
            ChartCompanySells.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            ChartCompanySells.ChartAreas[0].AxisX.Interval = 1;
            ChartCompanySells.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartCompanySells.ChartAreas[0].AxisY.LabelStyle.Format = "${0,000.00}";

            ChartCompanySells.ResetAutoValues();

            this.ChartCompanySells.Legends[0].Enabled = true;
            this.ChartCompanySells.Series["Series1"].Enabled = false;
        }                    //Primera Gráfica

        private void SellChart()
        {
            // Populate series data
            double[] yValues = new double[DtChartCompanySells1.Rows.Count];
            string[] xValues = new string[DtChartCompanySells1.Rows.Count];

            //ChartCompanySells.Legends.Add(new Legend("Leyenda"));
            LegendItem newLegendItem = new LegendItem();

            for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
            {
                ChartCompanySells.Series.Add(new Series());

                yValues[i] = double.Parse(DtChartCompanySells1.Rows[i]["Total"].ToString());
                xValues[i] = (DtChartCompanySells1.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE", "").Replace(
                    "ASSURANCE", ""));

                ChartCompanySells.Series[i].Points.AddXY(xValues[i], yValues[i]);
                ChartCompanySells.Series[i].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Column", true);
                ChartCompanySells.Series[i].IsValueShownAsLabel = true;
                ChartCompanySells.Series[i].LabelFormat = "{0,000}";
                ChartCompanySells.Series[i].ToolTip = (DtChartCompanySells1.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(
                    "ASSURANCE", ""));
                ChartCompanySells.Series[i].Name = (DtChartCompanySells1.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(
                    "ASSURANCE", ""));
                ChartCompanySells.Series[i].PostBackValue = (DtChartCompanySells1.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(
                    "ASSURANCE", ""));
            }

            //ChartCompanySells.Series["Series1"].Points.DataBindXY(xValues, yValues);

            // Set chart type
            //string chartTypeName = "Column";
            //ChartCompanySells.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), chartTypeName, true);

            ChartCompanySells.Titles[0].Text = "Policy Count";
            ChartCompanySells.Titles[0].PostBackValue = "Title"; //SHOW ALL

            // Set X axis margin for the area chart
            ChartCompanySells.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
            ChartCompanySells.ChartAreas[0].AxisX.LabelStyle.Enabled = false; // <- Elimina los label de donde naturalmente irian los nombre de las compañias
            ChartCompanySells.ChartAreas[0].AxisY.LabelStyle.Format = "{0,000}";

            // Show as 2D or 3D
            ChartCompanySells.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            ChartCompanySells.ChartAreas[0].AxisX.Interval = 1;
            ChartCompanySells.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            ChartCompanySells.ResetAutoValues();

            this.ChartCompanySells.Legends[0].Enabled = true;
            //this.ChartCompanySells.Series["Series2"].Enabled = false;

            for (int w = 0; w <= this.ChartCompanySells.Series.Count; w++)
            {
                if (w == this.ChartCompanySells.Series.Count)
                    this.ChartCompanySells.Series[w - 1].Enabled = false;
            }
        }                            //Primera Gráfica

        private void SellPercent()
        {
            try
            {
                string[] xValues;
                double[] yValues;

                if (DtChartCompanySells1.Rows.Count > 0)
                {
                    yValues = new double[DtChartCompanySells1.Rows.Count];
                    xValues = new string[DtChartCompanySells1.Rows.Count];

                    for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
                    {
                        yValues[i] = double.Parse(DtChartCompanySells1.Rows[i]["Total"].ToString());
                        xValues[i] = (DtChartCompanySells1.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE", "").Replace(
                    "ASSURANCE", ""));

                        xValues[i] += "\n" + int.Parse(yValues[i].ToString()).ToString("###,###");
                    }

                    ChartPercentage.Series[0].Points.DataBindXY(xValues, yValues);
                    ChartPercentage.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                    ChartPercentage.Legends[0].Enabled = true;

                    ChartPercentage.Series[0].SetCustomProperty("PieLabelStyle", "Enabled");

                    ChartPercentage.Series[0]["PieLabelStyle"] = "Outside";
                    ChartPercentage.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartPercentage.ChartAreas[0].Area3DStyle.Inclination = 10;

                    ChartPercentage.Titles[0].Text = "Market Share Percentage";
                    ChartPercentage.Titles[0].PostBackValue = "Title";

                    this.ChartPercentage.Legends[0].Enabled = false;

                    for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
                    {
                        ChartPercentage.Series[0].Points[i].PostBackValue = (DtChartCompanySells1.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE", "").Replace(
                    "ASSURANCE", ""));
                    }

                    foreach (DataPoint p in ChartPercentage.Series["Series1"].Points)
                    {
                        p.Label = "#PERCENT\n#VALX";
                    }
                }
                else
                {
                    ChartPercentage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }                          //Segunda Gráfica

        private void SellPosTypeChart(string PosType)
        {
            try
            {
                string[] xValues;
                double[] yValues;

                if (DtChartCompanySells1.Rows.Count > 0)
                {
                    yValues = new double[DtChartCompanySells1.Rows.Count];
                    xValues = new string[DtChartCompanySells1.Rows.Count];

                    for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
                    {
                        yValues[i] = double.Parse(DtChartCompanySells1.Rows[i]["Total"].ToString());

                        if (PosType != "Banco")
                            xValues[i] = (DtChartCompanySells1.Rows[i]["CityAreaDesc"].ToString().Replace("CUQUES", "CULEBRA & VIEQUES"));
                        else
                            xValues[i] = (DtChartCompanySells1.Rows[i]["NOMBRE_EA"].ToString().Replace("COOPERATIVO", "COOP")
                                .Replace(" DE PUERTO RICO", "").Replace(" BANK - BBVA", "").Replace("BANCO POPULAR", "POPULAR"));

                        xValues[i] += "\n" + int.Parse(yValues[i].ToString()).ToString("###,###");
                    }

                    ChartCompanySells.Series[0].Points.DataBindXY(xValues, yValues);
                    ChartCompanySells.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                    ChartCompanySells.Legends[0].Enabled = true;

                    //ChartCompanySells.Series[0].SetCustomProperty("PieLabelStyle", "Outside");

                    ChartCompanySells.Series[0]["PieLabelStyle"] = "Outside";
                    ChartCompanySells.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartCompanySells.ChartAreas[0].Area3DStyle.Inclination = 10;

                    ChartCompanySells.Titles[0].Text = "No. Policies Sold By " + (PosType == "Banco" ? "Bank" : PosType == "Colecturia" ? "Revenue Collection" :
                        PosType == "Cooperativa" ? "Coops" : "EOI");
                    ChartCompanySells.Titles[0].PostBackValue = "Title";

                    this.ChartCompanySells.Legends[0].Enabled = false;

                    for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
                    {
                        if (PosType != "Banco")
                            ChartCompanySells.Series[0].Points[i].PostBackValue = DtChartCompanySells1.Rows[i]["CityAreaDesc"].ToString().Trim();
                        else
                            ChartCompanySells.Series[0].Points[i].PostBackValue = DtChartCompanySells1.Rows[i]["NOMBRE_EA"].ToString().Trim();
                    }

                    foreach (DataPoint p in ChartCompanySells.Series["Series1"].Points)
                    {
                        p.Label = "#PERCENT\n#VALX";
                    }
                }
                else
                {
                    ChartCompanySells.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       //Primera Gráfica
        private void SellSalesCountChart()
        {
            try
            {
                string[] xValues;
                double[] yValues;

                if (DtChartCompanySells3.Rows.Count > 0)
                {
                    yValues = new double[DtChartCompanySells3.Rows.Count];
                    xValues = new string[DtChartCompanySells3.Rows.Count];

                    for (int i = 0; i < DtChartCompanySells3.Rows.Count; i++)
                    {
                        yValues[i] = double.Parse(DtChartCompanySells3.Rows[i]["Total"].ToString());
                        xValues[i] = (DtChartCompanySells3.Rows[i]["TIPO_POS"].ToString());

                        xValues[i] += "\n" + int.Parse(yValues[i].ToString()).ToString("###,###");
                    }

                    ChartSales.Series[0].Points.DataBindXY(xValues, yValues);
                    ChartSales.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                    ChartSales.Legends[0].Enabled = true;

                    //ChartSales.Series[0].SetCustomProperty("PieLabelStyle", "Outside");

                    ChartSales.Series[0]["PieLabelStyle"] = "Outside";
                    ChartSales.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartSales.ChartAreas[0].Area3DStyle.Inclination = 10;

                    ChartSales.Titles[0].Text = "Policy Sold By Point of Sales";
                    ChartSales.Titles[0].PostBackValue = "Title";

                    this.ChartSales.Legends[0].Enabled = false;

                    for (int i = 0; i < DtChartCompanySells3.Rows.Count; i++)
                    {
                        ChartSales.Series[0].Points[i].PostBackValue = (DtChartCompanySells3.Rows[i]["TIPO_POS"].ToString());
                    }

                    foreach (DataPoint p in ChartSales.Series["Series1"].Points)
                    {
                        p.Label = "#PERCENT\n#VALX";
                    }
                }
                else
                {
                    ChartSales.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }                  //Tercera Gráfica
        private void SellPremiumSalesChart()
        {
            try
            {
                string[] xValues;
                double[] yValues;

                if (DtChartCompanySells4.Rows.Count > 0)
                {
                    yValues = new double[DtChartCompanySells4.Rows.Count];
                    xValues = new string[DtChartCompanySells4.Rows.Count];

                    for (int i = 0; i < DtChartCompanySells4.Rows.Count; i++)
                    {
                        yValues[i] = double.Parse(DtChartCompanySells4.Rows[i]["Total"].ToString());
                        xValues[i] = (DtChartCompanySells4.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE", "").Replace(
                    "ASSURANCE", ""));

                        xValues[i] += "\n" + double.Parse(yValues[i].ToString()).ToString("$###,###");
                    }

                    ChartPremium.Series[0].Points.DataBindXY(xValues, yValues);
                    ChartPremium.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                    ChartPremium.Legends[0].Enabled = true;

                    //ChartPremium.Series[0].SetCustomProperty("PieLabelStyle", "Outside");

                    ChartPremium.Series[0]["PieLabelStyle"] = "Outside";
                    ChartPremium.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartPremium.ChartAreas[0].Area3DStyle.Inclination = 10;

                    ChartPremium.Titles[0].Text = "Premiums Written By Co.";
                    ChartPremium.Titles[0].PostBackValue = "Title";

                    this.ChartPremium.Legends[0].Enabled = false;

                    for (int i = 0; i < DtChartCompanySells4.Rows.Count; i++)
                    {
                        ChartPremium.Series[0].Points[i].PostBackValue = (DtChartCompanySells4.Rows[i]["MSRO_NAME"].ToString().Replace("INSURANCE", "").Replace("COMPANY", "").Replace("COOPERATIVA DE", "").Replace(
                    "ASSURANCE", ""));
                    }

                    foreach (DataPoint p in ChartPremium.Series["Series1"].Points)
                    {
                        p.Label = "#PERCENT\n#VALX";
                    }
                }
                else
                {
                    ChartPremium.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }                //Cuarta  Gráfica

        private void SellWeekDayChart()
        {
            try
            {
                string[] xValues;
                double[] yValues;

                if (DtChartCompanySells1.Rows.Count > 0)
                {
                    yValues = new double[DtChartCompanySells1.Rows.Count];
                    xValues = new string[DtChartCompanySells1.Rows.Count];

                    for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
                    {
                        yValues[i] = double.Parse(DtChartCompanySells1.Rows[i]["Total"].ToString());
                        xValues[i] = DtChartCompanySells1.Rows[i]["DAYS"].ToString();

                        xValues[i] += "\n" + int.Parse(yValues[i].ToString()).ToString("###,###");
                    }

                    ChartCompanySells.Series[0].Points.DataBindXY(xValues, yValues);
                    ChartCompanySells.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                    ChartCompanySells.Legends[0].Enabled = true;

                    //ChartCompanySells.Series[0].SetCustomProperty("PieLabelStyle", "Outside");

                    ChartCompanySells.Series[0]["PieLabelStyle"] = "Outside";
                    ChartCompanySells.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartCompanySells.ChartAreas[0].Area3DStyle.Inclination = 10;

                    ChartCompanySells.Titles[0].Text = "No. Policies Sold By Month Date";
                    ChartCompanySells.Titles[0].PostBackValue = "Title";

                    for (int i = 0; i < DtChartCompanySells1.Rows.Count; i++)
                    {
                        ChartCompanySells.Series[0].Points[i].PostBackValue = DtChartCompanySells1.Rows[i]["DAYS"].ToString();
                    }

                    foreach (DataPoint p in ChartCompanySells.Series["Series1"].Points)
                    {
                        p.Label = "#PERCENT\n#VALX";
                    }
                }
                else
                {
                    ChartCompanySells.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }                     //Primera Gráfica
        private void SellWeekNameChart()
        {
            ///Utiliza la segunda tabla del Dashboard para mostrar estos resultados
            try
            {
                string[] xValues;
                double[] yValues;

                if (DtChartCompanySells2.Rows.Count > 0)
                {
                    yValues = new double[DtChartCompanySells2.Rows.Count];
                    xValues = new string[DtChartCompanySells2.Rows.Count];

                    for (int i = 0; i < DtChartCompanySells2.Rows.Count; i++)
                    {
                        yValues[i] = double.Parse(DtChartCompanySells2.Rows[i]["Total"].ToString());
                        xValues[i] = DtChartCompanySells2.Rows[i]["WEEKNAME"].ToString();

                        xValues[i] += "\n" + int.Parse(yValues[i].ToString()).ToString("###,###");
                    }

                    ChartPercentage.Series[0].Points.DataBindXY(xValues, yValues);
                    ChartPercentage.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
                    ChartPercentage.Legends[0].Enabled = true;

                    //ChartPercentage.Series[0].SetCustomProperty("PieLabelStyle", "Outside");

                    ChartPercentage.Series[0]["PieLabelStyle"] = "Outside";
                    ChartPercentage.ChartAreas[0].Area3DStyle.Enable3D = true;
                    ChartPercentage.ChartAreas[0].Area3DStyle.Inclination = 10;

                    ChartPercentage.Titles[0].Text = "No. Policies Sold By Weekdate";
                    ChartPercentage.Titles[0].PostBackValue = "Title";

                    for (int i = 0; i < DtChartCompanySells2.Rows.Count; i++)
                    {
                        ChartPercentage.Series[0].Points[i].PostBackValue = DtChartCompanySells2.Rows[i]["WEEKNAME"].ToString();
                    }

                    foreach (DataPoint p in ChartPercentage.Series["Series1"].Points)
                    {
                        p.Label = "#PERCENT\n#VALX";
                    }
                }
                else
                {
                    ChartPercentage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }                    //Segunda Gráfica

        private void SellDistributionChart()
        {
            // Populate series data
            //double[] yValues = new double[DtChartCompanySells1.Rows.Count];
            //string[] xValues = new string[DtChartCompanySells1.Rows.Count];

            //ChartCompanySells.Legends.Add(new Legend("Leyenda"));
            LegendItem newLegendItem = new LegendItem();

            for (int i = 1; i < DtChartCompanySells1.Columns.Count - 1; i++)
            {
                Series series = new Series();
                foreach (DataRow dr in DtChartCompanySells1.Rows)
                {


                    double y = (double)dr[i]; 
                    //decimal y = (decimal)dr[i]; 
                    //((int)dr[i] / (int)dr[12]) * 100;
                    series.Points.AddXY(dr["TIPO_POS"].ToString(), y);

                    //series.Label = "#PERCENT{P0}";
                  
                    
                    //int test = (int)dr[i];
                    //series.Points.AddXY(dr["COMPANY"].ToString(), test);
                }
                ChartCompanySells.Series.Add(series);
                //ChartCompanySells.Series[i].Label = DtChartCompanySells1.Columns[i].ToString();
                ChartCompanySells.Series[i].ToolTip = DtChartCompanySells1.Columns[i].ToString();
                ChartCompanySells.Series[i].Name = DtChartCompanySells1.Columns[i].ToString();
                //ChartCompanySells.Series[i].PostBackValue = DtChartCompanySells1.Rows[i]["TIPO_POS"].ToString();
                //ChartCompanySells.Series[i].AxisLabel = "#PERCENT";
            }

            ChartCompanySells.Titles[0].Text = "Sales";
            ChartCompanySells.Titles[0].PostBackValue = "Title"; //SHOW ALL

            // Set X axis margin for the area chart
            ChartCompanySells.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
            ChartCompanySells.ChartAreas[0].AxisX.LabelStyle.Enabled = true; // <- Elimina los label de donde naturalmente irian los nombre de las compañias

            // Set Y axis margin for the area chart
            //ChartCompanySells.ChartAreas[0].AxisY.CustomLabels

            // Show as 2D or 3D
            ChartCompanySells.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            ChartCompanySells.ChartAreas[0].AxisX.Interval = 1;
            ChartCompanySells.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartCompanySells.ChartAreas[0].AxisY.LabelStyle.Format = "{0.00} %";

            ChartCompanySells.ResetAutoValues();

            this.ChartCompanySells.Legends[0].Enabled = true;
            this.ChartCompanySells.Series["Series1"].Enabled = false;
        }                //Primera Gráfica

        private void SellGuardianDistributionYearChart(string PosType)
        {
            // Populate series data
            //double[] yValues = new double[DtChartCompanySells1.Rows.Count];
            //string[] xValues = new string[DtChartCompanySells1.Rows.Count];

            //ChartCompanySells.Legends.Add(new Legend("Leyenda"));
            LegendItem newLegendItem = new LegendItem();

            for (int i = 1; i < DtChartCompanySells1.Columns.Count; i++)
            {
                Series series = new Series();
                foreach (DataRow dr in DtChartCompanySells1.Rows)
                {
                    double y = (double)dr[i];
                    //decimal y = (decimal)dr[i]; 
                    //((int)dr[i] / (int)dr[12]) * 100;
                    series.Points.AddXY(dr["Month"].ToString(), y);

                    //series.Label = "#PERCENT{P0}";

                    //int test = (int)dr[i];
                    //series.Points.AddXY(dr["COMPANY"].ToString(), test);
                }
                ChartCompanySells.Series.Add(series);
                //ChartCompanySells.Series[i].Label = DtChartCompanySells1.Columns[i].ToString();
                ChartCompanySells.Series[i].ToolTip = DtChartCompanySells1.Columns[i].ToString();
                ChartCompanySells.Series[i].Name = DtChartCompanySells1.Columns[i].ToString();
                //ChartCompanySells.Series[i].PostBackValue = DtChartCompanySells1.Rows[i]["TIPO_POS"].ToString();
                //ChartCompanySells.Series[i].AxisLabel = "#PERCENT";
            }

            if (PosType != "General")
            {
                ChartCompanySells.Titles[0].Text = "Premium Written By " + PosType;
            }

            else
            {
                ChartCompanySells.Titles[0].Text = "Premium Written";
                ddlPosType.SelectedIndex = 0;
            }

            ChartCompanySells.Titles[0].PostBackValue = "Title"; //SHOW ALL

            // Set X axis margin for the area chart
            ChartCompanySells.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
            ChartCompanySells.ChartAreas[0].AxisX.LabelStyle.Enabled = true; // <- Elimina los label de donde naturalmente irian los nombre de las compañias

            // Set Y axis margin for the area chart
            //ChartCompanySells.ChartAreas[0].AxisY.CustomLabels

            // Show as 2D or 3D
            ChartCompanySells.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            ChartCompanySells.ChartAreas[0].AxisX.Interval = 1;
            ChartCompanySells.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartCompanySells.ChartAreas[0].AxisY.LabelStyle.Format = "${0,000.00}";

            ChartCompanySells.ResetAutoValues();

            this.ChartCompanySells.Legends[0].Enabled = true;
            this.ChartCompanySells.Series["Series1"].Enabled = false;
        }    //Primera Gráfica
        #endregion

        #region DataTables' Procedures
        private DataTable GetDashboardSROVentas(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("CompanyID", SqlDbType.Int, 0, CompanyID.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardSROVentas", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not gather sell information, please try again." + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GetDashboardGuardian(string BegDate, string EndDate, string PosType)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                //DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("CompanyID", SqlDbType.Int, 0, CompanyID.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("PosType", SqlDbType.VarChar, 15, PosType, ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardGuardianInformation", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not gather Guardian information, please try again." + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvSales(string BegDate, string EndDate, string PosType)
        {
            //DataTable Data = null;
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    //if (PosType != "EOI")
                        dt = exec.GetQuery("GetDashboardSalesByPosType", xmlDoc);
                    //else
                        //dt = exec.GetQuery("GetDashboardByEOIStations", xmlDoc);

                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Bank Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return Data;
        }
        private DataTable GvTopTen(string BegDate, string EndDate, string PosType)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[4];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("PosType", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("TopX", SqlDbType.VarChar, 10, TxtTopX.Text.Trim(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardTopTenSalesByPosType", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Bank Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvGuardianSales(string PosType)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                //DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("PosType", SqlDbType.VarChar, 15, PosType, ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardGuardianInformation", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Bank Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvPoliciesSold(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardSalesCount", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Premium Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvPremiumSales(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardPremiums", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Premium Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvMonthDateSales(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardDaySales", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Day Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvWeekNameSales(string BegDate, string EndDate)
        {
             try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardWeekNameSales", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display WeekName Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvPosTypeDistributions(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardDistributionsByPosType", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Distribution Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable GvPosTypeDistributions2(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDashboardDistributionsByPosType2", xmlDoc);
                    //dt = exec.GetQuery("GetPivotDistributionSalesCount", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Distribution Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable _TempCol(string BegDate, string EndDate, string PosType)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    switch (PosType)
                    {
                        case "Banco":
                            dt = exec.GetQuery("GetPivotByBank", xmlDoc);
                            return dt;

                        case "Colecturia":
                            dt = exec.GetQuery("GetPivotByColecturia", xmlDoc);
                            return dt;

                        case "Cooperativa":
                            dt = exec.GetQuery("GetPivot", xmlDoc);
                            return dt;

                        case "EOI":
                            dt = exec.GetQuery("GetPivot", xmlDoc);
                            return dt;

                        default:
                            dt = null;
                            return dt;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not retrieve pivot table, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable _TempIndividualSales(string BegDate, string EndDate, string PosType)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetPivotIndividualSalesByPosType", xmlDoc);
                    return dt;                   
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not retrieve pivot table, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable _TempCount(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetPivotSalesCount", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Sales Count, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable _TempPremium(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetPivotPremiumSales", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Premium Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable _TempMonth(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetPivotDaySales", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Day Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private DataTable _TempWeekDays(string BegDate, string EndDate)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                //DbRequestXmlCooker.AttachCookItem("TipoPos", SqlDbType.VarChar, 15, PosType.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetPivotDateNameSales", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not display Datename Sales, please try again.", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DataTables' Click Methods
        protected void ChartCompanySells_Click(object sender, ImageMapEventArgs e)
        {
            string value = e.PostBackValue.ToString();

            ///Reemplaza para buscar por MSRO_NAME
            if (value == "SEGUROS MULTIPLES (A157)")
                value = "%MULTIPLE%";

            else if (value == " SEGUROS MULTIPLES (A157)")
                value = "%MULTIPLE%";

            else if (value == "SEGUROS TRIPLE S INC (A165)")
                value = "%TRIPLE%";

            ///Reemplaza para buscar por CityArea
            else if (value == "CULEBRA & VIEQUES")
                value = "%CUQUES%";

            else if (value == "Other")
                value = "%NA%";

            else if (value == "#N/A")
                value = "%Other%";

            ///Reemplaza para buscar por Bancos
            else if (value == "BANCO POPULAR DE PUERTO RICO")
                value = "%POPULAR%";

            else if (value == "BANCO COOPERATIVO")
                value = "%COOP%";

            ///Busca todo
            else if (value == "Title")
                value = "*"; //Se buscará toda la info available

            else
            {
                value = value.Split(" ".ToCharArray())[0];
                value = "%" + value + "%";
            }

            string ReportSet = "";

            if (value == "%Sunday%" || value == "%Monday%" || value == "%Tuesday%" || value == "%Wednesday%" || value == "%Thursday%" || value == "%Friday%" || value == "%Saturday%")
                ReportSet = "WeekSales";

            else if (value == "%1%" || value == "%6%" || value == "%11%" || value == "%16%" || value == "%21%" || value == "%26%")
                ReportSet = "PeriodSales";

            else if (value == "%EOI%" || value == "%Cooperativa%" || value == "%Colecturia%" || value == "%Banco%")
                ReportSet = "PolicySales";

            else if (value == "%NORTE%" || value == "%SUR%" || value == "%ESTE%" || value == "%OESTE%" || value == "%CENTRO%" || value == "%METRO%" || value == "%ARECIBO%" || value == "%BAYAMON%" || value == "CAGUAS" || value == "%CAROLINA%" || value == "%CUQUES%" || value == "%MAYAGUEZ%" || value == "%PONCE%" || value == "%SAN%" || value == "%NA%" || value == "%Other%")
                ReportSet = "RegionSales";            

            else if (value == "%POPULAR%" || value == "%FIRST%" || value == "%ORIENTAL%" || value == "%SANTANDER%" || value == "%SCOTIABANK%" || value == "%COOP%")
                ReportSet = "BankSales";

            else
                ReportSet = "AllSales";

            Session.Add("dateFrom", txtBegDate.Text);
            Session.Add("dateTo", txtEndDate.Text);
            Session.Add("CompanyName", value);
            Session.Add("ReportSet", ReportSet);
            Session.Add("PosType", posType);    //Este posType es solo para diferenciar al escoger una gráfica que busca por región.

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('DashboardReport.aspx', 'Reports', 'addressbar=no,status=1,menubar=0,scrollbar=0,resizable=0,copyhistory=0,width=1024,height=768');", true);

            switch (DashboardSet)
            {
                case "AllSales":
                    SetAllSalesDashboard();
                    break;

                case "GuardianSales":
                    SetGuardianDashboard("General");
                    break;

                case "Banco":
                    SetPosTypeDashboard("Banco");
                    break;

                case "Colecturia":
                    SetPosTypeDashboard("Colecturia");
                    break;

                case "Cooperativa":
                    SetPosTypeDashboard("Cooperativa");
                    break;

                case "EOI":
                    SetPosTypeDashboard("EOI");
                    break;

                case "TimedSales":
                    SetWeekDashboard();
                    break;

                case "DistributionSales":
                    SetDistribitionDashboard();
                    break;

                default:
                    SetAllSalesDashboard();
                    break;
            }
        }
        #endregion

        #region DropDownLists' Methods
        protected void ddlPosType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PosType = ddlPosType.SelectedItem.Text.Trim();

            if (PosType == "Tipo Pos")
                PosType = "General";

            SetGuardianDashboard(PosType);
        }
        #endregion

        #region GridViews' Fill Methods
        private void FillGuardianSales(string PosType)
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = GvGuardianSales(PosType);

                if (dt.Rows.Count > 0)
                {
                    gvGuardianSales.DataSource = dt;
                    gvGuardianSales.DataBind();
                    ReportViewer1.Visible = false;
                    Panel1.Visible = true;
                    gvGuardianSales.Visible = true;
                }
                else
                {
                    gvGuardianSales.DataSource = null;
                    gvGuardianSales.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing Guardian Yearly Sales.");
                this.litPopUp.Visible = true;
            }
        }
        private void FillGvSales(string PosType)
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = _TempCol(txtBegDate.Text.Trim(), txtEndDate.Text.Trim(), PosType);     //Trae el pivote temporera de SQL
                //DataTable dt2 = FillColDt(txtBegDate.Text.Trim(), txtEndDate.Text.Trim(), "Banco");   //Trae la suma total de polizas vendidas por region
                DataTable Col = new DataTable();        //Tabla creada para calcular el porcentaje por region
                decimal AvgPercent = 0;

                if (dt.Rows.Count > 0)
                {
                    int allZerosColumn = 0;

                    for (int y = 1; y <= dt.Columns.Count - 1; y++)
                    {
                        if (dt.Rows[dt.Rows.Count - 1][y].ToString() == "0")
                        {
                            allZerosColumn = y;
                            dt.Columns.RemoveAt(allZerosColumn);
                            dt.AcceptChanges();
                            y--;
                        }
                    }

                    if (gvSales.Columns.Count > 0)
                        gvSales.Columns.Clear();

                    foreach (DataColumn DC in dt.Columns)
                    {
                        BoundField Field = new BoundField();
                        Field.DataField = DC.ColumnName;
                        if (DC.ColumnName.ToString().Trim() != "CUQUES")
                            Field.HeaderText = DC.ColumnName.Replace("COOPERATIVO", "COOP").
                                Replace(" DE PUERTO RICO", "").Replace(" BANK - BBVA", "").
                                Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");
                        else
                            Field.HeaderText = "CULEBRA & VIEQUES";

                        gvSales.Columns.Add(Field);
                    }

                    BoundField TotalField = new BoundField();
                    TotalField.DataField = "Total";
                    TotalField.HeaderText = "Total";
                    TotalField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    //gvSales.HeaderStyle.Font.Size.Unit = 9;
                    gvSales.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    
                    for (int i = 1; i < gvSales.Columns.Count; i++)
                    {
                        gvSales.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    gvSales.Columns.Add(TotalField);

                    Col.Clear();

                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Col.Columns.Add(dt.Columns[x].ColumnName.Trim(), typeof(string));
                    }

                    Col.Columns.Add("Total", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++) //  foreach (DataRow row in dt.Rows)
                    {
                        DataRow ColRow = Col.NewRow();

                        for (int w = 1; w < dt.Columns.Count; w++)
                        {
                            ColRow["Company"] = dt.Rows[i]["Company"].ToString().Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");

                            ColRow[dt.Columns[w].ColumnName] = (Math.Round(decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100, 2)).ToString() + "%";
                            AvgPercent += decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100; //!= 0)
                        }

                        ColRow["Total"] = (Math.Round(AvgPercent / (dt.Columns.Count - 1), 2)).ToString() + "%";

                        Col.Rows.Add(ColRow);

                        AvgPercent = 0; //Resets the value in order to be used again by the next row in the for instance.
                    }

                    //if (PosType == "EOI")
                    //    FillGvIndividualSales("EOI2");
                    //else
                        FillGvIndividualSales(PosType);

                    gvSales.DataSource = Col;

                    if (PosType != "Banco")
                    {
                        gvSales.Width = Unit.Pixel(1200);
                    }
                    else
                    {
                        gvSales.Width = Unit.Pixel(800);
                    }
                    
                    gvSales.DataBind();
                    ReportViewer1.Visible = false;
                    gvSales.Visible = true;
                }
                else
                {
                    gvSales.DataSource = null;
                    gvSales.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing PosType Sales.");
                this.litPopUp.Visible = true;
            }
        }
        private void FillTopTen(string PosType)
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = GvTopTen(txtBegDate.Text.Trim(), txtEndDate.Text.Trim(), PosType);     //Trae el top ten de SQL
                
                if (dt.Rows.Count > 0)
                {
                    gvTopTen.DataSource = dt;
                    gvTopTen.DataBind();
                    ReportViewer1.Visible = false;
                    gvTopTen.Visible = true;
                    LblTopX.Visible = true;
                    TxtTopX.Visible = true;
                    BtnTopX.Visible = true;
                    BtnExportTop.Visible = true;
                }
                else
                {
                    gvTopTen.DataSource = null;
                    gvTopTen.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing TopTen Sales.");
                this.litPopUp.Visible = true;
            }
        }
        private void FillGvSolds()
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = _TempCount(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae el pivote temporera de SQL
                //DataTable dt2 = FillColDt(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae la suma total de polizas vendidas por region
                DataTable Col = new DataTable();        //Tabla creada para calcular el porcentaje por region
                decimal AvgPercent = 0;
                decimal TotalPremium = 0;

                if (dt.Rows.Count > 0)
                {
                    int allZerosColumn = 0;


                    for (int y = 1; y <= dt.Columns.Count - 1; y++)
                    {
                        if (dt.Rows[dt.Rows.Count - 1][y].ToString() == "0")
                        {
                            allZerosColumn = y;
                            dt.Columns.RemoveAt(allZerosColumn);
                            dt.AcceptChanges();
                            y--;
                        }
                    }

                    if (gvSolds.Columns.Count > 0)
                        gvSolds.Columns.Clear();

                    foreach (DataColumn DC in dt.Columns)
                    {
                        BoundField Field = new BoundField();
                        Field.DataField = DC.ColumnName;
                        Field.HeaderText = DC.ColumnName;

                        gvSolds.Columns.Add(Field);
                    }

                    BoundField TotalField = new BoundField();
                    TotalField.DataField = "Total";
                    TotalField.HeaderText = "Total";
                    TotalField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    //gvSales.HeaderStyle.Font.Size.Unit = 9;
                    gvSolds.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                    for (int i = 1; i < gvSolds.Columns.Count; i++)
                    {
                        gvSolds.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    gvSolds.Columns.Add(TotalField);

                    Col.Clear();

                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Col.Columns.Add(dt.Columns[x].ColumnName.Trim(), typeof(string));
                    }

                    Col.Columns.Add("Total", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++) //  foreach (DataRow row in dt.Rows)
                    {
                        DataRow ColRow = Col.NewRow();

                        for (int w = 1; w < dt.Columns.Count; w++)
                        {
                            ColRow["Company"] = dt.Rows[i]["Company"].ToString().Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");

                            ColRow[dt.Columns[w].ColumnName] = (Math.Round(decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100, 2)).ToString() + "%";
                            //AvgPercent += decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100; //!= 0)
                            AvgPercent += decimal.Parse(dt.Rows[i][w].ToString());
                            TotalPremium += decimal.Parse(dt.Rows[dt.Rows.Count - 1][w].ToString());
                        }

                        //ColRow["Total"] = (Math.Round(AvgPercent / (dt.Columns.Count - 1), 2)).ToString() + "%";
                        ColRow["Total"] = (Math.Round(AvgPercent / TotalPremium * 100, 2)).ToString() + "%";
                        Col.Rows.Add(ColRow);

                        ///Resets the value in order to be used again by the next row in the for instance.
                        AvgPercent = 0;
                        TotalPremium = 0;
                    }

                    gvSolds.DataSource = Col;
                    gvSolds.Width = Unit.Pixel(800);
                    gvSolds.DataBind();
                    ReportViewer1.Visible = false;
                }
                else
                {
                    gvSolds.DataSource = null;
                    gvSolds.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing Premium Sales.");
                this.litPopUp.Visible = true;
            }
        }
        private void FillGvPremiumSales()
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = _TempPremium(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae el pivote temporera de SQL
                //DataTable dt2 = FillColDt(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae la suma total de polizas vendidas por region
                DataTable Col = new DataTable();        //Tabla creada para calcular el porcentaje por region
                decimal AvgPercent = 0;
                decimal TotalPremium = 0;

                if (dt.Rows.Count > 0)
                {
                    int allZerosColumn = 0;


                    for (int y = 1; y <= dt.Columns.Count - 1; y++)
                    {
                        if (dt.Rows[dt.Rows.Count - 1][y].ToString() == "0")
                        {
                            allZerosColumn = y;
                            dt.Columns.RemoveAt(allZerosColumn);
                            dt.AcceptChanges();
                            y--;
                        }
                    }

                    if (gvPremiums.Columns.Count > 0)
                        gvPremiums.Columns.Clear();

                    foreach (DataColumn DC in dt.Columns)
                    {
                        BoundField Field = new BoundField();
                        Field.DataField = DC.ColumnName;
                        Field.HeaderText = DC.ColumnName;

                        gvPremiums.Columns.Add(Field);
                    }

                    BoundField TotalField = new BoundField();
                    TotalField.DataField = "Total";
                    TotalField.HeaderText = "Total";
                    TotalField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    //gvSales.HeaderStyle.Font.Size.Unit = 9;
                    gvPremiums.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    gvPremiums.Columns[0].HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                    for (int i = 1; i < gvPremiums.Columns.Count; i++)
                    {
                        gvPremiums.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    gvPremiums.Columns.Add(TotalField);

                    Col.Clear();

                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Col.Columns.Add(dt.Columns[x].ColumnName.Trim(), typeof(string));
                    }

                    Col.Columns.Add("Total", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++) //  foreach (DataRow row in dt.Rows)
                    {
                        DataRow ColRow = Col.NewRow();

                        for (int w = 1; w < dt.Columns.Count; w++)
                        {
                            ColRow["Company"] = dt.Rows[i]["Company"].ToString().Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");

                            ColRow[dt.Columns[w].ColumnName] = (Math.Round(decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100, 2)).ToString() + "%";
                            //AvgPercent += decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100; //!= 0)
                            AvgPercent += decimal.Parse(dt.Rows[i][w].ToString());
                            TotalPremium += decimal.Parse(dt.Rows[dt.Rows.Count - 1][w].ToString());
                        }

                        //ColRow["Total"] = (Math.Round(AvgPercent / (dt.Columns.Count - 1), 2)).ToString() + "%";
                        ColRow["Total"] = (Math.Round(AvgPercent / TotalPremium * 100, 2)).ToString() + "%";
                        Col.Rows.Add(ColRow);

                        ///Resets the value in order to be used again by the next row in the for instance.
                        AvgPercent = 0;
                        TotalPremium = 0;
                    }

                    gvPremiums.DataSource = Col;
                    gvPremiums.Width = Unit.Pixel(800);
                    gvPremiums.DataBind();
                    ReportViewer1.Visible = false;
                }
                else
                {
                    gvPremiums.DataSource = null;
                    gvPremiums.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing Premium Sales.");
                this.litPopUp.Visible = true;
            }
        }

        private void FillGvIndividualSales(string PosType)
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = _TempIndividualSales(txtBegDate.Text.Trim(), txtEndDate.Text.Trim(), PosType);     //Trae el pivote temporera de SQL
                //DataTable dt2 = FillColDt(txtBegDate.Text.Trim(), txtEndDate.Text.Trim(), "Banco");   //Traía la suma total de polizas vendidas por region
                DataTable Col = new DataTable();        //Tabla creada para calcular el porcentaje por region
                decimal AvgPercent = 0;

                if (dt.Rows.Count > 0)
                {
                    int allZerosColumn = 0;


                    for (int y = 1; y <= dt.Columns.Count - 1; y++)
                    {
                        if (dt.Rows[dt.Rows.Count - 1][y].ToString() == "0")
                        {
                            allZerosColumn = y;
                            dt.Columns.RemoveAt(allZerosColumn);
                            dt.AcceptChanges();
                            y--;
                        }
                    }

                    if (gvIndividualSales.Columns.Count > 0)
                        gvIndividualSales.Columns.Clear();

                    foreach (DataColumn DC in dt.Columns)
                    {
                        BoundField Field = new BoundField();
                        Field.DataField = DC.ColumnName;
                        if (DC.ColumnName.ToString().Trim() != "CUQUES")
                            Field.HeaderText = DC.ColumnName.Replace("COOPERATIVO", "COOP").
                                Replace(" DE PUERTO RICO", "").Replace(" BANK - BBVA", "").
                                Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");
                        else
                            Field.HeaderText = "CULEBRA & VIEQUES";

                        gvIndividualSales.Columns.Add(Field);
                    }

                    BoundField TotalField = new BoundField();
                    TotalField.DataField = "Total";
                    TotalField.HeaderText = "Total";
                    TotalField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    //gvSales.HeaderStyle.Font.Size.Unit = 9;
                    gvIndividualSales.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                    for (int i = 1; i < gvIndividualSales.Columns.Count; i++)
                    {
                        gvIndividualSales.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    gvIndividualSales.Columns.Add(TotalField);

                    Col.Clear();

                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Col.Columns.Add(dt.Columns[x].ColumnName.Trim(), typeof(string));
                    }

                    Col.Columns.Add("Total", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++) //  foreach (DataRow row in dt.Rows)
                    {
                        DataRow ColRow = Col.NewRow();

                        for (int w = 1; w < dt.Columns.Count; w++)
                        {
                            ColRow["CENTRO"] = dt.Rows[i]["CENTRO"].ToString();

                            ColRow[dt.Columns[w].ColumnName] = (Math.Round(decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100, 2)).ToString() + "%";
                            AvgPercent += decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100; //!= 0)
                        }

                        ColRow["Total"] = (Math.Round(AvgPercent / (dt.Columns.Count - 1), 2)).ToString() + "%";

                        Col.Rows.Add(ColRow);

                        AvgPercent = 0; //Resets the value in order to be used again by the next row in the for instance.
                    }

                    gvIndividualSales.DataSource = Col;
                    gvIndividualSales.DataBind();
                    ReportViewer1.Visible = false;
                    gvIndividualSales.Visible = true;
                    BtnExport.Visible = true;
                }
                else
                {
                    gvIndividualSales.DataSource = null;
                    gvIndividualSales.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing PosType Sales.");
                this.litPopUp.Visible = true;
            }
        }

        private void FillGvWeekDate()
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = _TempMonth(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae el pivote temporera de SQL
                //DataTable dt2 = FillColDt(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae la suma total de polizas vendidas por region
                DataTable Col = new DataTable();        //Tabla creada para calcular el porcentaje por region
                decimal AvgPercent = 0;

                if (dt.Rows.Count > 0)
                {
                    int allZerosColumn = 0;


                    for (int y = 1; y <= dt.Columns.Count - 1; y++)
                    {
                        if (dt.Rows[dt.Rows.Count - 1][y].ToString() == "0")
                        {
                            allZerosColumn = y;
                            dt.Columns.RemoveAt(allZerosColumn);
                            dt.AcceptChanges();
                            y--;
                        }
                    }

                    if (gvSales.Columns.Count > 0)
                        gvSales.Columns.Clear();

                    foreach (DataColumn DC in dt.Columns)
                    {
                        BoundField Field = new BoundField();
                        Field.DataField = DC.ColumnName;
                        Field.HeaderText = DC.ColumnName;

                        gvSales.Columns.Add(Field);
                    }

                    BoundField TotalField = new BoundField();
                    TotalField.DataField = "Total";
                    TotalField.HeaderText = "Total";
                    TotalField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    //gvSales.HeaderStyle.Font.Size.Unit = 9;
                    gvSales.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                    for (int i = 1; i < gvSales.Columns.Count; i++)
                    {
                        gvSales.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    gvSales.Columns.Add(TotalField);

                    Col.Clear();

                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Col.Columns.Add(dt.Columns[x].ColumnName.Trim(), typeof(string));
                    }

                    Col.Columns.Add("Total", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++) //  foreach (DataRow row in dt.Rows)
                    {
                        DataRow ColRow = Col.NewRow();

                        for (int w = 1; w < dt.Columns.Count; w++)
                        {
                            ColRow["Company"] = dt.Rows[i]["Company"].ToString().Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");

                            ColRow[dt.Columns[w].ColumnName] = (Math.Round(decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100, 2)).ToString() + "%";
                            AvgPercent += decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100; //!= 0)
                        }

                        ColRow["Total"] = (Math.Round(AvgPercent / (dt.Columns.Count - 1), 2)).ToString() + "%";

                        Col.Rows.Add(ColRow);

                        AvgPercent = 0; //Resets the value in order to be used again by the next row in the for instance.
                    }

                    gvSales.DataSource = Col;
                    gvSales.Width = Unit.Pixel(800);
                    gvSales.DataBind();
                    ReportViewer1.Visible = false;
                }
                else
                {
                    gvSales.DataSource = null;
                    gvSales.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing Premium Sales.");
                this.litPopUp.Visible = true;
            }
        }
        private void FillGvWeekName()
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = _TempWeekDays(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae el pivote temporera de SQL
                //DataTable dt2 = FillColDt(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae la suma total de polizas vendidas por region
                DataTable Col = new DataTable();        //Tabla creada para calcular el porcentaje por region
                decimal AvgPercent = 0;

                if (dt.Rows.Count > 0)
                {
                    int allZerosColumn = 0;


                    for (int y = 1; y <= dt.Columns.Count - 1; y++)
                    {
                        if (dt.Rows[dt.Rows.Count - 1][y].ToString() == "0")
                        {
                            allZerosColumn = y;
                            dt.Columns.RemoveAt(allZerosColumn);
                            dt.AcceptChanges();
                            y--;
                        }
                    }

                    if (gv4.Columns.Count > 0)
                        gv4.Columns.Clear();

                    foreach (DataColumn DC in dt.Columns)
                    {
                        BoundField Field = new BoundField();
                        Field.DataField = DC.ColumnName;
                        Field.HeaderText = DC.ColumnName;

                        gv4.Columns.Add(Field);
                    }

                    BoundField TotalField = new BoundField();
                    TotalField.DataField = "Total";
                    TotalField.HeaderText = "Total";
                    gv4.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                    for (int i = 1; i < gv4.Columns.Count; i++)
                    {
                        gv4.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    gv4.Columns.Add(TotalField);

                    Col.Clear();

                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Col.Columns.Add(dt.Columns[x].ColumnName.Trim(), typeof(string));
                    }

                    Col.Columns.Add("Total", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++) //  foreach (DataRow row in dt.Rows)
                    {
                        DataRow ColRow = Col.NewRow();

                        for (int w = 1; w < dt.Columns.Count; w++)
                        {
                            ColRow["Company"] = dt.Rows[i]["Company"].ToString().Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");

                            ColRow[dt.Columns[w].ColumnName] = (Math.Round(decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100, 2)).ToString() + "%";
                            AvgPercent += decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100; //!= 0)
                        }

                        ColRow["Total"] = (Math.Round(AvgPercent / (dt.Columns.Count - 1), 2)).ToString() + "%";

                        Col.Rows.Add(ColRow);

                        AvgPercent = 0; //Resets the value in order to be used again by the next row in the for instance.
                    }

                    gv4.DataSource = Col;
                    gv4.Width = Unit.Pixel(900);
                    gv4.DataBind();
                    ReportViewer1.Visible = false;
                }
                else
                {
                    gv4.DataSource = null;
                    gv4.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing Premium Sales.");
                this.litPopUp.Visible = true;
            }
        }

        private void FillGvPosTypeDistribution()
        {
            try
            {
                this.litPopUp.Visible = false;

                DataTable dt = _TempCount(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae el pivote temporera de SQL
                //DataTable dt2 = FillColDt(txtBegDate.Text.Trim(), txtEndDate.Text.Trim());   //Trae la suma total de polizas vendidas por region
                DataTable Col = new DataTable();        //Tabla creada para calcular el porcentaje por region
                decimal AvgPercent = 0;

                if (dt.Rows.Count > 0)
                {
                    int allZerosColumn = 0;


                    for (int y = 1; y <= dt.Columns.Count - 1; y++)
                    {
                        if (dt.Rows[dt.Rows.Count - 1][y].ToString() == "0")
                        {
                            allZerosColumn = y;
                            dt.Columns.RemoveAt(allZerosColumn);
                            dt.AcceptChanges();
                            y--;
                        }
                    }

                    if (gvSales.Columns.Count > 0)
                        gvSales.Columns.Clear();

                    foreach (DataColumn DC in dt.Columns)
                    {
                        BoundField Field = new BoundField();
                        Field.DataField = DC.ColumnName;
                        Field.HeaderText = DC.ColumnName;

                        gvSales.Columns.Add(Field);
                    }

                    BoundField TotalField = new BoundField();
                    TotalField.DataField = "Total";
                    TotalField.HeaderText = "Total";
                    gvSales.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

                    for (int i = 1; i < gvSales.Columns.Count; i++)
                    {
                        gvSales.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    }

                    gvSales.Columns.Add(TotalField);

                    Col.Clear();

                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Col.Columns.Add(dt.Columns[x].ColumnName.Trim(), typeof(string));
                    }

                    Col.Columns.Add("Total", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++) //  foreach (DataRow row in dt.Rows)
                    {
                        DataRow ColRow = Col.NewRow();

                        for (int w = 1; w < dt.Columns.Count; w++)
                        {
                            ColRow["Company"] = dt.Rows[i]["Company"].ToString().Replace(" INSURANCE COMPANY", "").Replace("COOPERATIVA DE ", "").Replace(" ASSURANCE COMPANY", "").Replace(" INSURANCE", "");

                            ColRow[dt.Columns[w].ColumnName] = (Math.Round(decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100, 2)).ToString() + "%";
                            AvgPercent += decimal.Parse(dt.Rows[i][dt.Columns[w].ColumnName].ToString()) / (decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) != 0 ? decimal.Parse(dt.Rows[dt.Rows.Count - 1][dt.Columns[w].ColumnName].ToString()) : 1) * 100; //!= 0)
                        }

                        ColRow["Total"] = (Math.Round(AvgPercent / (dt.Columns.Count - 1), 2)).ToString() + "%";

                        Col.Rows.Add(ColRow);

                        AvgPercent = 0; //Resets the value in order to be used again by the next row in the for instance.
                    }

                    gvSales.DataSource = Col;                    
                    gvSales.DataBind();
                    ReportViewer1.Visible = false;
                }
                else
                {
                    gvSales.DataSource = null;
                    gvSales.DataBind();
                }
            }
            catch (Exception Exc)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Encountered an error processing Distribution Sales.");
                this.litPopUp.Visible = true;
            }
        }
        #endregion

        #region ReportViewers' Fill Methods
        private void FillColReportViewer()
        {
            //    ReportViewer1.LocalReport.ReportPath = ("Reports/AAA.rdlc");
            //    ReportViewer1.LocalReport.DataSources.Clear();
            //    ReportViewer1.ProcessingMode = ProcessingMode.Local;

            //    SROSalesReportTableAdapters.GetPivotTableAdapter ta =
            //    new SROSalesReportTableAdapters.GetPivotTableAdapter();

            //    SROSalesReport.GetPivotDataTable reportDt =
            //    new SROSalesReport.GetPivotDataTable();

            //    ta.Fill(reportDt, "Colecturia", txtBegDate.Text, txtEndDate.Text);

            //    Microsoft.Reporting.WebForms.ReportDataSource rptDataSource =
            //    new Microsoft.Reporting.WebForms.ReportDataSource("SROSalesReport", (DataTable)reportDt);

            //    ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
            //    ReportViewer1.LocalReport.Refresh();
            //    ReportViewer1.Visible = true;
        }
        #endregion

        #region Buttons
        protected void BtnGuardian_Click(object sender, EventArgs e)
        {
            SetGuardianDashboard("General");
            BtnReset();
            BtnGuardian.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnGuardian.ForeColor = Color.White;
        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Search.aspx");
        }
        protected void BtnBank_Click(object sender, EventArgs e)
        {
            SetPosTypeDashboard("Banco");
            BtnReset();
            BtnBank.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnBank.ForeColor = Color.White;
        }
        protected void BtnDistributions_Click(object sender, EventArgs e)
        {
            SetDistribitionDashboard();
            BtnReset();
            BtnDistribution.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnDistribution.ForeColor = Color.White;
        }
        protected void BtnCol_Click(object sender, EventArgs e)
        {
            SetPosTypeDashboard("Colecturia");
            BtnReset();
            BtnCol.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnCol.ForeColor = Color.White;
        }
        protected void BtnCoop_Click(object sender, EventArgs e)
        {
            SetPosTypeDashboard("Cooperativa");
            BtnReset();
            BtnCoop.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnCoop.ForeColor = Color.White;
        }
        protected void BtnEoi_Click(object sender, EventArgs e)
        {
            SetPosTypeDashboard("EOI");
            BtnReset();
            BtnEoi.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnEoi.ForeColor = Color.White;
        }
        protected void BtnWeek_Click(object sender, EventArgs e)
        {
            SetWeekDashboard();
            BtnReset();
            BtnWeek.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnWeek.ForeColor = Color.White;
        }
        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            SetAllSalesDashboard();
        }
        protected void BtnTotalSales_Click(object sender, EventArgs e)
        {
            SetAllSalesDashboard();
            BtnReset();
            BtnTotalSales.BackColor = System.Drawing.ColorTranslator.FromHtml("#036893");
            BtnTotalSales.ForeColor = Color.White;
        }
        protected void BtnTopX_Click(object sender, EventArgs e)
        {
            SetPosTypeDashboard(posType);
            //FillTopTen(posType);
        }
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            //Response.Clear();  
            //Response.Buffer = true;  
            //Response.ClearContent();  
            //Response.ClearHeaders();  
            //Response.Charset = "";  
            //string FileName ="SRO_Sales"+ DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
            //    DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".xls";  
            //StringWriter strwritter = new StringWriter();  
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);        
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //Response.Buffer = true;

            ////Response.ContentType ="application/vnd.ms-excel";    
            ////Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";    
            
            ////Response.AddHeader("Content-Disposition","attachment;filename=" + FileName);  
            //Response.AddHeader("Content-disposition", "attachment; filename=" + FileName);
            //Response.ContentType = "application/octet-stream";    
            //gvIndividualSales.GridLines = GridLines.Both;
            //gvIndividualSales.HeaderStyle.Font.Bold = true;
            //gvIndividualSales.RenderControl(htmltextwrtter);  
            //Response.Write(strwritter.ToString());  
            //Response.End(); 

            string FileName = "SRO_Sales" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".csv";  

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "application/text";
            StringBuilder sBuilder = new System.Text.StringBuilder();
            for (int index = 0; index < gvIndividualSales.Columns.Count; index++)
            {
                sBuilder.Append(gvIndividualSales.Columns[index].HeaderText + ',');
            }
            sBuilder.Append("\r\n");
            for (int i = 0; i < gvIndividualSales.Rows.Count; i++)
            {
                for (int k = 0; k < gvIndividualSales.HeaderRow.Cells.Count; k++)
                {
                    sBuilder.Append(gvIndividualSales.Rows[i].Cells[k].Text.Replace(",", "") + ",");
                }
                sBuilder.Append("\r\n");
            }
            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();


        }
        protected void BtnExportTop_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //Response.Buffer = true;
            //string FileName = "SRO_Sales_TopSellers" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
            //    DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".xls";
            //StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ////Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-disposition", "attachment; filename=" + FileName);
            //Response.ContentType = "application/octet-stream"; 
            //gvTopTen.GridLines = GridLines.Both;
            //gvTopTen.HeaderStyle.Font.Bold = true;
            //gvTopTen.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End(); 

            //if (gvIndividualSales.Rows.Count > 0)
            //{
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=" + "Prueba.xlsx");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            //    StringWriter sw = new StringWriter();
            //    gvIndividualSales.HeaderRow.Style.Add("background-color", "#fff");
            //    gvIndividualSales.HeaderRow.Style.Add("color", "#000");
            //    gvIndividualSales.HeaderRow.Style.Add("font-weight", "bold");

            //    for (int i = 0; i < gvIndividualSales.Rows.Count; i++)
            //    {
            //        GridViewRow grow = gvIndividualSales.Rows[i];
            //        grow.BackColor = System.Drawing.Color.White;
            //        grow.Attributes.Add("class", "textmode");
            //    }

            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {
            //        gvIndividualSales.RenderControl(hw);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }

            //}

            string FileName = "SRO_Sales_TopSellers" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".csv";

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "application/text";
            StringBuilder sBuilder = new System.Text.StringBuilder();
            for (int index = 0; index < gvTopTen.Columns.Count; index++)
            {
                sBuilder.Append(gvTopTen.Columns[index].HeaderText + ',');
            }
            sBuilder.Append("\r\n");
            for (int i = 0; i < gvTopTen.Rows.Count; i++)
            {
                for (int k = 0; k < gvTopTen.HeaderRow.Cells.Count; k++)
                {
                    sBuilder.Append(gvTopTen.Rows[i].Cells[k].Text.Replace(",", "") + ",");
                }
                sBuilder.Append("\r\n");
            }
            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();
        }

        private void BtnReset()
        {
            ///Guardian: #ffac37
            ///Selected: #036893
            
            BtnBank.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnBank.ForeColor = Color.Black;
            BtnCol.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnCol.ForeColor = Color.Black;
            BtnCoop.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnCoop.ForeColor = Color.Black;
            BtnDistribution.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnDistribution.ForeColor = Color.Black;
            BtnEoi.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnEoi.ForeColor = Color.Black;
            BtnGuardian.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnGuardian.ForeColor = Color.Black;
            BtnTotalSales.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnTotalSales.ForeColor = Color.Black;
            BtnWeek.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffac37");
            BtnWeek.ForeColor = Color.Black;
        }
        #endregion

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //SetAllSalesDashboard();
        }
    }
}
