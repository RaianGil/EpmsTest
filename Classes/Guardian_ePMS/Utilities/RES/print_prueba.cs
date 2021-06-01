using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.RES
{
    class print_prueba
    {
        private List<string> ImprimirQuote(List<string> mergePaths, string reportName)
        {
            try
            {
                EPolicy.TaskControl.Environmental taskControl = (EPolicy.TaskControl.Environmental)Session["TaskControl"];

                GetReportEnvironmentalTSQuoteTableAdapters.GetReportEnvironmentalTSQuoteTableAdapter ds = new GetReportEnvironmentalTSQuoteTableAdapters.GetReportEnvironmentalTSQuoteTableAdapter();
                ReportDataSource rds = new ReportDataSource("GetReportEnvironmentalTSQuote", (DataTable)ds.GetData(taskControl.TaskControlID));

                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.DataSources.Clear();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("Reports/EnvironmentalTS/" + reportName + ".rdlc");
                viewer.LocalReport.DataSources.Add(rds);

                if (reportName == "TSQuote1" || reportName == "TSQuote1-1")
                {
                    GetReportEnvironmentalTankDetailQuoteTableAdapters.GetReportEnvironmentalTankDetailQuoteTableAdapter ds4 = new GetReportEnvironmentalTankDetailQuoteTableAdapters.GetReportEnvironmentalTankDetailQuoteTableAdapter();
                    ReportDataSource rds4 = new ReportDataSource("GetReportEnvironmentalTankDetailQuote", (DataTable)ds4.GetData(taskControl.TaskControlID));
                    viewer.LocalReport.DataSources.Add(rds4);
                }

                string value = "";

                if (taskControl.Suffix != "00")
                {
                    value = "REN";
                }
                else
                {
                    value = "NEW";
                }

                //if (chkNonBind.Checked)
                //{
                //    value = "BIND";
                //}

                //if (chkNonBind.Checked)
                //{
                //    value = "BIND";
                //}

                if (reportName == "TSQuote2-1" || reportName == "TSQuote2-2")
                {
                    GetReportCoverageListTableAdapters.GetReportTSCoverageListTableAdapter ds2 =
                    new GetReportCoverageListTableAdapters.GetReportTSCoverageListTableAdapter();

                    ReportDataSource rds2 = new ReportDataSource("GetReportCoverageList", (DataTable)ds2.GetData(taskControl.TaskControlID, taskControl.IsOPPQuote));
                    var result1 = "◦  Completed and signed Chubb TS Application Form, including completed Storage Tank(s) Inventory by             Location.\n" +
                        "◦  Operating Permit issued by the Environmental Quality Board (Permiso de Operación - Junta de Calidad          Ambiental) - Applies only for locations with Underground Storage Tanks (UST).\n" +
                        "◦  The most recent four (4) months of groundwater or soil vapor monitoring results data or statistical                   inventory analysis and/or recent tightness test results for all tanks and pipelines, which must be                        favorable - Applies only for Underground Storage Tanks (UST).\n";

                    if (txtAdditionalSubjectivities.Text.Trim() != "")
                        result1 += "◦  " + txtAdditionalSubjectivities.Text.Trim().Replace("\n", "\n◦  ");
                    var result = result1.Split(new string[] { "\n" }, StringSplitOptions.None);
                    ReportParameter p61 = new ReportParameter("Subjectivity", result);
                    ReportParameter p71 = new ReportParameter("Sufijo", value);

                    viewer.LocalReport.SetParameters(new ReportParameter[] { p61, p71 });
                    viewer.LocalReport.DataSources.Add(rds2);

                    if (reportName == "TSQuote2-2")
                    {
                        GetEnvironmentalSignByNameTableAdapters.GetEnvironmentalSignByNameTableAdapter ds3 =
                        new GetEnvironmentalSignByNameTableAdapters.GetEnvironmentalSignByNameTableAdapter();

                        ReportDataSource rds3 = new ReportDataSource("GetEnvironmentalSignByName", (DataTable)ds3.GetData(taskControl.Sign.Trim()));

                        viewer.LocalReport.DataSources.Add(rds3);
                    }
                }
                else
                {

                    string retroParam = " ";
                    string retroParam1 = " ";
                    string retroParam2 = " ";
                    string endosos = " ";
                    string otherCountry = txtOtherCountry.Text;

                    if (chkInception.Checked)
                    {
                        retroParam = "Inception";
                    }
                    if (chkTBA.Checked)
                    {
                        retroParam = "TBA";
                    }
                    if (checkBoxAsPerScheduleBelow.Checked)
                    {
                        retroParam = "APSB";
                    }
                    if (chkTBAEffDt.Checked)
                    {
                        retroParam1 = "TBA";
                    }
                    if (checkBoxAsPerScheduleBelow2.Checked)
                    {
                        retroParam2 = "APSB";
                    }

                    string paramtria = "0";

                    if (chkTRIA.Checked)
                    {
                        double param1 = 0;

                        if (taskControl.OverwriteTotalPremium > 0)
                        {
                            param1 = Math.Round(float.Parse(txtTriaPerc.Text) * double.Parse(taskControl.OverwriteTotalPremium.ToString().Trim()));
                        }
                        else
                        {
                            param1 = Math.Round(float.Parse(txtTriaPerc.Text) * double.Parse(taskControl.TotalPremium.ToString().Trim()));
                        }

                        paramtria = param1.ToString().Trim();
                    }

                    ReportParameter p1 = new ReportParameter("rpp_EffectiveDate", txtComission.Text);
                    ReportParameter p2 = new ReportParameter("Endosos", endosos);
                    ReportParameter p3 = new ReportParameter("retroDate", retroParam);
                    ReportParameter p4 = new ReportParameter("effDate", retroParam1);
                    ReportParameter p5 = new ReportParameter("otherCountry", otherCountry);
                    ReportParameter p6 = new ReportParameter("Subjectivity", txtAdditionalSubjectivities.Text.Trim());
                    ReportParameter p7 = new ReportParameter("Sufijo", value);
                    ReportParameter p8 = new ReportParameter("PolicyNo", TxtPolicyNo.Text.Trim());
                    ReportParameter p9 = new ReportParameter("tria", paramtria);
                    ReportParameter p10 = new ReportParameter("term", TxtTerm.Text);
                    ReportParameter p11 = new ReportParameter("sign", ddlSign.SelectedItem.Text);
                    ReportParameter p12 = new ReportParameter("deductible", retroParam2);

                    viewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12 });
                }

                viewer.LocalReport.Refresh();

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType;
                string encoding = string.Empty;
                string extension;
                string fileName = "Environmental" + reportName + taskControl.TaskControlID.ToString().Trim() + "-1";
                string _FileName = "Environmental" + reportName + taskControl.TaskControlID.ToString().Trim() + "-1.pdf";

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
                throw new Exception("Could not print preview " + reportName + "." + ecp.Message);
            }
        }
    }
}
