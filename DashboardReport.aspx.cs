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
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.XmlCooker;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace EPolicy
{
    public partial class DashboardReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string dateFrom = (String)Session["dateFrom"];
                string dateTo = (String)Session["dateTo"];
                string CompanyName = (String)Session["CompanyName"];
                string ReportSet = (String)Session["ReportSet"];
                string posType = "EOI"; //Si la sesion de posType no trae, buscará EOI por Default

                try
                {
                    if (Session["PosType"] != "")
                        posType = (String)Session["PosType"];
                }
                catch
                {

                }

                string BegPeriod = "0";
                string EndPeriod = "0";

                if (CompanyName == "%1%")
                {
                    BegPeriod = "1";
                    EndPeriod = "5";
                }

                if (CompanyName == "%6%")
                {
                    BegPeriod = "6";
                    EndPeriod = "10";
                }

                if (CompanyName == "%11%")
                {
                    BegPeriod = "11";
                    EndPeriod = "15";
                }

                if (CompanyName == "%16%")
                {
                    BegPeriod = "16";
                    EndPeriod = "20";
                }

                if (CompanyName == "%21%")
                {
                    BegPeriod = "21";
                    EndPeriod = "25";
                }

                if (CompanyName == "%26%")
                {
                    BegPeriod = "26";
                    EndPeriod = "31";
                }

                ReportViewer1.LocalReport.ReportPath = ("Reports/ReportSROSales.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                switch (ReportSet)
                {
                    case "AllSales":
                        SROSalesTableAdapters.GetDashboardSROVentasReportsTableAdapter TaNormal =
                        new SROSalesTableAdapters.GetDashboardSROVentasReportsTableAdapter();

                        //SROSales.GetDashboardSROVentasReportsDataTable NormalDt =
                        //new SROSales.GetDashboardSROVentasReportsDataTable();

                        //TaNormal.Fill(NormalDt, dateFrom, dateTo, CompanyName);

                        Microsoft.Reporting.WebForms.ReportDataSource NormalDataSource =
                        new Microsoft.Reporting.WebForms.ReportDataSource("SROSales", (DataTable)TaNormal.GetData(dateFrom,dateTo,CompanyName));

                        ReportViewer1.LocalReport.DataSources.Add(NormalDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;

                    case "WeekSales":
                        SROSalesTableAdapters.GetDashboardSROVentasReportsByWeekDateTableAdapter TaDias =
                        new SROSalesTableAdapters.GetDashboardSROVentasReportsByWeekDateTableAdapter();

                        SROSales.GetDashboardSROVentasReportsByWeekDateDataTable DiasDt =
                        new SROSales.GetDashboardSROVentasReportsByWeekDateDataTable();

                        TaDias.Fill(DiasDt, dateFrom, dateTo, CompanyName);

                        Microsoft.Reporting.WebForms.ReportDataSource DiasDataSource =
                        new Microsoft.Reporting.WebForms.ReportDataSource("SROSales", (DataTable)DiasDt);

                        ReportViewer1.LocalReport.DataSources.Add(DiasDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;

                    case "PeriodSales":
                        SROSalesTableAdapters.GetDashboardSROVentasReportsByMonthDateTableAdapter TaPeriod =
                        new SROSalesTableAdapters.GetDashboardSROVentasReportsByMonthDateTableAdapter();

                        SROSales.GetDashboardSROVentasReportsByMonthDateDataTable PeriodDt =
                        new SROSales.GetDashboardSROVentasReportsByMonthDateDataTable();

                        TaPeriod.Fill(PeriodDt, dateFrom, dateTo, BegPeriod, EndPeriod);

                        Microsoft.Reporting.WebForms.ReportDataSource PeriodDataSource =
                        new Microsoft.Reporting.WebForms.ReportDataSource("SROSales", (DataTable)PeriodDt);

                        ReportViewer1.LocalReport.DataSources.Add(PeriodDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;

                    case "BankSales":
                        SROSalesTableAdapters.GetDashboardSROVentasReportsByBankTableAdapter TaBank =
                        new SROSalesTableAdapters.GetDashboardSROVentasReportsByBankTableAdapter();

                        SROSales.GetDashboardSROVentasReportsByBankDataTable BankDt =
                        new SROSales.GetDashboardSROVentasReportsByBankDataTable();

                        TaBank.Fill(BankDt, dateFrom, dateTo, CompanyName);

                        Microsoft.Reporting.WebForms.ReportDataSource BankDataSource =
                        new Microsoft.Reporting.WebForms.ReportDataSource("SROSales", (DataTable)BankDt);

                        ReportViewer1.LocalReport.DataSources.Add(BankDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;

                    case "PolicySales":
                        SROSalesTableAdapters.GetDashboardSROVentasReportsByPosTypeTableAdapter TaSold =
                        new SROSalesTableAdapters.GetDashboardSROVentasReportsByPosTypeTableAdapter();

                        SROSales.GetDashboardSROVentasReportsByPosTypeDataTable SoldDt =
                        new SROSales.GetDashboardSROVentasReportsByPosTypeDataTable();

                        TaSold.Fill(SoldDt, dateFrom, dateTo, CompanyName);

                        Microsoft.Reporting.WebForms.ReportDataSource SoldDataSource =
                        new Microsoft.Reporting.WebForms.ReportDataSource("SROSales", (DataTable)SoldDt);

                        ReportViewer1.LocalReport.DataSources.Add(SoldDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;

                    case "RegionSales":
                        SROSalesTableAdapters.GetDashboardSROVentasReportsByRegionAndPosTypeTableAdapter TaRegion =
                        new SROSalesTableAdapters.GetDashboardSROVentasReportsByRegionAndPosTypeTableAdapter();

                        SROSales.GetDashboardSROVentasReportsByRegionAndPosTypeDataTable RegionDt =
                        new SROSales.GetDashboardSROVentasReportsByRegionAndPosTypeDataTable();

                        TaRegion.Fill(RegionDt, dateFrom, dateTo, CompanyName, posType);

                        Microsoft.Reporting.WebForms.ReportDataSource RegionDataSource =
                        new Microsoft.Reporting.WebForms.ReportDataSource("SROSales", (DataTable)RegionDt);

                        ReportViewer1.LocalReport.DataSources.Add(RegionDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;

                    default:
                        SROSalesTableAdapters.GetDashboardSROVentasReportsTableAdapter ta =
                        new SROSalesTableAdapters.GetDashboardSROVentasReportsTableAdapter();

                        SROSales.GetDashboardSROVentasReportsDataTable dt =
                        new SROSales.GetDashboardSROVentasReportsDataTable();

                        ta.Fill(dt, dateFrom, dateTo, CompanyName);

                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource =
                        new Microsoft.Reporting.WebForms.ReportDataSource("SROSales", (DataTable)dt);

                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;
                        break;
                }
            }
        }
    }
}