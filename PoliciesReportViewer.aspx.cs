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
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using System.IO;
using System.Net;
using System.Text;
using EPolicy;
using EPolicy.LookupTables;
using EPolicy.TaskControl;
using EPolicy.Login;
using EPolicy2.Reports;
using Microsoft.Reporting.WebForms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Globalization;

public partial class PoliciesReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtAgent = LookupTables.GetTable("AgentList");

        bool IsEffectiveDate = bool.Parse(Session["IsEffectiveDate"].ToString());
        string BegDate = Session["BegDate"].ToString();
        string EndDate = Session["EndDate"].ToString();
        string Agent = Session["Agent"].ToString();
        string PolicyType = Session["PolicyType"].ToString();
        string rdlcDoc = Session["rdlcDoc"].ToString();
        string ReportType = Session["ReportType"].ToString();
        string CompanyPolicy = Session["CompanyPolicy"].ToString();
        string TaskControlTypeID = Session["TaskControlTypeID"].ToString();

        if (!IsPostBack)
        {
            Session.Add("AutoPostBack", (bool)true);
            ReportPrint(IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, rdlcDoc, ReportType, CompanyPolicy, TaskControlTypeID);
        }
        
    }


    private void ReportPrint(bool IsEffectiveDate, string BegDate, string EndDate, string Agent, string PolicyType, string rdlcDoc, string ReportType, string CompanyPolicy, string TaskControlTypeID)
    {
        try
        {
            EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
            ReportViewer viewer = new ReportViewer();

            //EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            if (CompanyPolicy == "Auto VI")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatement.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportCurrentStatement.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWritten.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportPremiumWritten.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewals.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportRenewals.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssued.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportQuotesPoliciesIssued.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued

                    return;
                }
            }
            if (CompanyPolicy == "Double Interest")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatement.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportCurrentStatement.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWritten.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportPremiumWritten.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewals.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportRenewals.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssued.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportQuotesPoliciesIssued.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued

                    #region Renewal ID Cards
                    if (rdlcDoc == "IDCardsDI.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/VI/ReportQuotesPoliciesIssued.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Renewal ID Cards

                    return;
                }
            }
            else if (CompanyPolicy == "GuardianXtra")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatementXtra.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/GuardianXtra/ReportCurrentStatementXtra.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter
                            ta = new GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter();

                        GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable
                            dt = new GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWrittenXtra.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/GuardianXtra/ReportPremiumWrittenXtra.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter
                            ta = new GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter();

                        GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable
                            dt = new GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewalsXtra.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/GuardianXtra/ReportRenewalsXtra.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter
                            ta = new GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter();

                        GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable
                            dt = new GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssuedXtra.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/GuardianXtra/ReportQuotesPoliciesIssuedXtra.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter
                            ta = new GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter();

                        GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable
                            dt = new GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued

                    #region Guardian Payment
                    if (rdlcDoc == "ReportPaidTransactionXtra.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/GuardianXtra/ReportPaidTransactionXtra.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter
                            ta = new GetReportAutoPolicyInfoTableAdapters.GetReportAutoPolicyInfoTableAdapter();

                        GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable
                            dt = new GetReportAutoPolicyInfo.GetReportAutoPolicyInfoDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Guardian Payment

                    return;
                }
            }
            else if (CompanyPolicy == "Home Owner")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatement.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/HomeOwners/ReportCurrentStatement.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter
                                            ta = new GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter();

                        GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable
                            dt = new GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_HO", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWritten.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/HomeOwners/ReportPremiumWritten.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter
                                            ta = new GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter();

                        GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable
                            dt = new GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_HO", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewals.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/HomeOwners/ReportRenewals.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter
                                            ta = new GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter();

                        GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable
                            dt = new GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_HO", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssued.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/HomeOwners/ReportQuotesPoliciesIssued.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter
                                            ta = new GetReportAutoPolicyInfo_HOTableAdapters.GetReportAutoPolicyInfo_HOTableAdapter();

                        GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable
                            dt = new GetReportAutoPolicyInfo_HO.GetReportAutoPolicyInfo_HODataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_HO", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued

                    return;
                }
            }

            else if (CompanyPolicy == "Yacht")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatementYacht.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/Yacht/ReportCurrentStatementYacht.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter
                            ta = new GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter();

                        GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable
                            dt = new GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_Yacht", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWrittenYacht.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/Yacht/ReportPremiumWrittenYacht.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter
                            ta = new GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter();

                        GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable
                            dt = new GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_Yacht", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Yacht Policies with Pending Fields
                    if (rdlcDoc == "ReportYachtPoliciesPendingFields.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/Yacht/ReportYachtPoliciesPendingFields.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportYachtPoliciesPendingFieldsTableAdapters.GetReportYachtPoliciesPendingFieldsTableAdapter
                            ta = new GetReportYachtPoliciesPendingFieldsTableAdapters.GetReportYachtPoliciesPendingFieldsTableAdapter();

                        GetReportYachtPoliciesPendingFields.GetReportYachtPoliciesPendingFieldsDataTable
                            dt = new GetReportYachtPoliciesPendingFields.GetReportYachtPoliciesPendingFieldsDataTable();

                        ta.Fill(dt, BegDate, EndDate, Agent);

                        DataTable dt2 = LookupTables.GetTable("AgencyYacht");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportYachtPoliciesPendingFields", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssuedYacht.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/Yacht/ReportQuotesPoliciesIssuedYacht.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter
                            ta = new GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter();

                        GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable
                            dt = new GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_Yacht", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportPremiumWrittenYacht.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/Yacht/ReportPremiumWrittenYacht.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter
                            ta = new GetReportAutoPolicyInfo_YachtTableAdapters.GetReportAutoPolicyInfo_YachtTableAdapter();

                        GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable
                            dt = new GetReportAutoPolicyInfo_Yacht.GetReportAutoPolicyInfo_YachtDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_Yacht", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Yacht Policies with Pending Fields

                    return;
                }
            }

            else if (CompanyPolicy == "Auto Personal")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatementAutoPR.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoPersonales/ReportCurrentStatementAutoPR.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter
                            ta = new GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter();
                        

                        

                        GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable
                            dt = new GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);


                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoPR", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWrittenAutoPR.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoPersonales/ReportPremiumWrittenAutoPR.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter
                            ta = new GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter();

                        GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable
                            dt = new GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoPR", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewalsAutoPR.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoPersonales/ReportRenewalsAutoPR.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter
                                            ta = new GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter();

                        GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable
                            dt = new GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoPR", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssuedAutoPR.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoPersonales/ReportQuotesPoliciesIssuedAutoPR.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter
                            ta = new GetReportAutoPolicyInfo_AutoPRTableAdapters.GetReportAutoPolicyInfo_AutoPRTableAdapter();

                        GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable
                            dt = new GetReportAutoPolicyInfo_AutoPR.GetReportAutoPolicyInfo_AutoPRDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoPR", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Quotes vs Policies Issued
                    return;
                }
            }

            if (CompanyPolicy == "Road Assist")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatementRoadAssist.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/RoadAssist/ReportCurrentStatementRoadAssist.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter
                                            ta = new GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter();

                        GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable
                            dt = new GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_RoadAssist", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWrittenRoadAssist.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/RoadAssist/ReportPremiumWrittenRoadAssist.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter
                                            ta = new GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter();

                        GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable
                            dt = new GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_RoadAssist", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewalsRoadAssist.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/RoadAssist/ReportRenewalsRoadAssist.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter
                                            ta = new GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter();

                        GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable
                            dt = new GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_RoadAssist", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssuedRoadAssist.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/RoadAssist/ReportQuotesPoliciesIssuedRoadAssist.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter
                                            ta = new GetReportAutoPolicyInfo_RoadAssistTableAdapters.GetReportAutoPolicyInfo_RoadAssistTableAdapter();

                        GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable
                            dt = new GetReportAutoPolicyInfo_RoadAssist.GetReportAutoPolicyInfo_RoadAssistDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_RoadAssist", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued

                    return;
                }
            }
            if (CompanyPolicy == "Auto High Limit")
            {
                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatement.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoHighLimit/ReportCurrentStatement.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter
                                            ta = new GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter();

                        GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable
                            dt = new GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoHighLimit", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;


                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWritten.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoHighLimit/ReportPremiumWritten.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter
                                            ta = new GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter();

                        GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable
                            dt = new GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoHighLimit", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewals.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoHighLimit/ReportRenewals.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter
                                            ta = new GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter();

                        GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable
                            dt = new GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoHighLimit", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssued.rdlc")
                    {
                        ReportViewer1.LocalReport.ReportPath = ("Reports/AutoHighLimit/ReportQuotesPoliciesIssued.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter
                                            ta = new GetReportAutoPolicyInfo_AutoHighLimitTableAdapters.GetReportAutoPolicyInfo_AutoHighLimitTableAdapter();

                        GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable
                            dt = new GetReportAutoPolicyInfo_AutoHighLimit.GetReportAutoPolicyInfo_AutoHighLimitDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, TaskControlTypeID);

                        DataTable dt2 = LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_AutoHighLimit", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer1.LocalReport.SetParameters(param);
                        ReportViewer1.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer1.LocalReport.Refresh();
                        ReportViewer1.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued

                    return;
                }
            }

        }
        catch (Exception exp)
        {
            return;
        }
    }
}