using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EPolicy.XmlCooker;
using Baldrich.DBRequest;
using System.Xml;
using System.Configuration;
using System.IO;
using WinSCP;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.AccessControl;

namespace EPolicy
{
    public partial class RoadAssistAA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Call Dynamic Payment Services Transactions
            DataTable dt = GetRoadAssistFile();

            //Create Text File        
            string Filename = CreateTextFile(dt);

            UpdateTramsFl(dt);

            //Connect and Upload File to SFTP
             UploadSFTP(@"F:\inetpub\wwwroot\EpmsTest\RoadAssistFile\", Filename);

            //Move File Uploded
            string sourcePath = @"F:\inetpub\wwwroot\EpmsTest\RoadAssistFile\" + Filename; // @"C:\inetpub\wwwroot\Guardian_ePMS\RoadAssistFile\" + Filename;
            string destFile = @"F:\inetpub\wwwroot\EpmsTest\RoadAssistFile\Processed\" + Filename; //@"C:\inetpub\wwwroot\Guardian_ePMS\RoadAssistFile\Processed\" + Filename;

            if (File.Exists(sourcePath))
                File.Copy(sourcePath, destFile, true);

            if (File.Exists(sourcePath))
                File.Delete(sourcePath);

            //Send Email
            SendEmailToGuardian(Filename);
        }

        private void UpdateTramsFl(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i > dt.Rows.Count; i++)
                {
                    //Call SP UpdatePolicyRoadAssistTrams
                    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                    DbRequestXmlCooker.AttachCookItem("TaskControlID ", SqlDbType.VarChar, 20, dt.Rows[i]["TaskControlID"].ToString().Trim(), ref cookItems);
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

                    exec.BeginTrans();
                    exec.Update("UpdatePolicyRoadAssistTrams", xmlDoc);
                    exec.CommitTrans();
                }
            }
        }

        private DataTable GetRoadAssistFile2()
        {
            string ToDate = DateTime.Now.ToShortDateString();
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
            DbRequestXmlCooker.AttachCookItem("FromDate ", SqlDbType.VarChar, 20, ToDate.Trim(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ToDate ", SqlDbType.VarChar, 20, ToDate.Trim(), ref cookItems);
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
            DataTable dt = exec.GetQuery("GetRoadAssistFileForSFTP", xmlDoc);

            return dt;
        }

        private DataTable GetRoadAssistFile()
        {
            string ToDate = DateTime.Now.ToShortDateString();

            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("FromDate",
                SqlDbType.VarChar, 20, "",
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ToDate",
                SqlDbType.VarChar, 20, ToDate.ToString(),
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
                dt = exec.GetQuery("GetRoadAssistFileForSFTP", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not execute.", ex);
            }
        }

        private string CreateTextFile(DataTable dt)
        {
            string file = "";
            if (dt.Rows.Count > 0)
            {
                StreamWriter sr;
                string rowString = "";
                double TotalAmt = 0.00;
                int countRec = 0;
                string cuenta = "";
                string programa = "";
                string varYear = DateTime.Now.Year.ToString();
                string varMonth = DateTime.Now.Month.ToString();
                string varDay = DateTime.Now.Day.ToString();
                string varHour = DateTime.Now.Hour.ToString();
                string varMin = DateTime.Now.Minute.ToString();
                string varSec = DateTime.Now.Second.ToString();
                file = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');// +varHour.PadLeft(2, '0') + varMin.PadLeft(2, '0') + varSec.PadLeft(2, '0');
                file = file.Trim();
                file = file.PadRight(8, ' ');

                string path = @"F:\inetpub\wwwroot\EpmsTest\RoadAssistFile\"; // @"C:\inetpub\wwwroot\Guardian_ePMS\RoadAssistFile\"; //@"F:\inetpub\wwwroot\ePMS\RoadAssistFile\";
                file = "PR_GUI_GUIPR_" + cuenta + "" + programa + "_" + file.Trim() + ".txt";

                string FileName = path + file.Trim();
                sr = File.CreateText(FileName);

                //Header
                //rowString = "1" + varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0') + "0136".PadRight(20, ' ') + "".PadRight(51, ' ');
                //sr.WriteLine(rowString);

                //Transactions
                rowString = "";
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {

                    string ClaveAfiliado = dt.Rows[i]["ClaveAfiliado"].ToString().Replace(",", " ");
                    string Poliza = dt.Rows[i]["Poliza"].ToString().Replace(",", " ");
                    string Cedula = dt.Rows[i]["Cedula"].ToString().Replace(",", " ");
                    string Nombre = dt.Rows[i]["Nombre"].ToString().Replace(",", " ");
                    string Appaterno = dt.Rows[i]["Appaterno"].ToString().Replace(",", " ");
                    string Apmaterno = dt.Rows[i]["Apmaterno"].ToString().Replace(",", " ");
                    string Telefono = dt.Rows[i]["Telefono"].ToString().Replace(",", " ").Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    string Telefono2 = dt.Rows[i]["Telefono2"].ToString().Replace(",", " ").Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");

                    string FechaInicioVigencia = dt.Rows[i]["FechaInicioVigencia"].ToString().Replace(",", " ");
                    string varfecvigYear = DateTime.Parse(FechaInicioVigencia).Year.ToString();
                    string varfecvigMonth = DateTime.Parse(FechaInicioVigencia).Month.ToString().PadLeft(2, '0');
                    string varfecvigDay = DateTime.Parse(FechaInicioVigencia).Day.ToString().PadLeft(2, '0');
                    FechaInicioVigencia = varfecvigYear + varfecvigMonth + varfecvigDay;

                    string FechaFinVigencia = dt.Rows[i]["FechaFinVigencia"].ToString().Replace(",", " ");
                    string varfecFinvigYear = DateTime.Parse(FechaFinVigencia).Year.ToString();
                    string varfecFinvigMonth = DateTime.Parse(FechaFinVigencia).Month.ToString().PadLeft(2, '0');
                    string varfecFinvigDay = DateTime.Parse(FechaFinVigencia).Day.ToString().PadLeft(2, '0');
                    FechaFinVigencia = varfecFinvigYear + varfecFinvigMonth + varfecFinvigDay;

                    string Dirreccion1 = dt.Rows[i]["Dirreccion1"].ToString().Replace(",", " ");
                    string Dirreccion2 = dt.Rows[i]["Dirreccion2"].ToString().Replace(",", " ");
                    string Ciudad = dt.Rows[i]["Ciudad"].ToString().Replace(",", " ");
                    string Estado = dt.Rows[i]["Estado"].ToString().Replace(",", " ");
                    string ZipCode = dt.Rows[i]["ZipCode"].ToString().Replace(",", " ");
                    string AllAddrs = Dirreccion1.Trim() + Dirreccion2.Trim() + Ciudad.Trim() + Estado.Trim() + ZipCode.Trim();
                    if (AllAddrs.Length > 60)
                        AllAddrs = AllAddrs.Substring(0, 60);

                    string Marca = dt.Rows[i]["Marca"].ToString().Replace(",", " ");
                    string Modelo = dt.Rows[i]["Modelo"].ToString().Replace(",", " ");
                    string Year = dt.Rows[i]["Year"].ToString().Replace(",", " ");
                    string VIN = dt.Rows[i]["VIN"].ToString().Replace(",", " ");
                    string Plate = dt.Rows[i]["Plate"].ToString().Replace(",", " ");
                    string Color = dt.Rows[i]["Color"].ToString().Replace(",", " ");
                    string Cuenta = dt.Rows[i]["Cuenta"].ToString().Replace(",", " ");
                    string Programa = dt.Rows[i]["Programa"].ToString().Replace(",", " ");

           rowString = ClaveAfiliado.Trim() + "|" + Poliza.PadRight(10, ' ') + "|" + Cedula.PadRight(0, ' ') + "|" + Nombre.Trim() + "|" + Appaterno.Trim() + "|" +
                        Apmaterno.Trim() + "|" + Telefono.Trim() + "|" + Telefono2.Trim() + "|" + FechaInicioVigencia + "|" + FechaFinVigencia + "|" +
                        AllAddrs.Trim() + "|" + Marca.Trim() + "|" + Modelo.Trim() + "|" + Year.PadRight(4, ' ') + "|" + VIN.Trim() + "|" +
                        Plate.Trim() + "|" + Color.Trim() + "|" + Cuenta.Trim() + "|" + Programa.Trim();

                     countRec++;
                    sr.WriteLine(rowString);
                }

                sr.Close();
            }
            return file;
        }

        private static void UploadSFTP(string path, string filename)
        {
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = "70.35.142.116",
                    PortNumber = 60322,
                    UserName = "ginsurance",
                    Password = "Ginsur4nc3^&",
                    SshHostKeyFingerprint = "ssh-ed25519 256 f8:4c:6f:7b:66:99:4c:95:c4:f1:bb:a4:06:84:df:ac",
                    //SshHostKeyFingerprint = "ssh-rsa 2048 xx:xx:xx:xx:xx:xx:xx:xx:..."
                    //GiveUpSecurityAndAcceptAnySshHostKey = true
                };

                using (Session session = new Session())
                {
                    // Connect
                    //sessionOptions.SshHostKeyFingerprint = session.ScanFingerprint(sessionOptions);
                    session.ExecutablePath = @"F:\inetpub\wwwroot\EpmsTest\RoadAssistFile\WinSCP.exe"; // @"C:\inetpub\wwwroot\ePMS\RoadAssistFile\winscp.exe";
                    sessionOptions.WebdavSecure = false;
                    //session.DisableVersionCheck = true;
                    session.Open(sessionOptions);

                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;
                    transferOptions.FilePermissions = null; // This is default
                    transferOptions.PreserveTimestamp = false;

                    TransferOperationResult transferResult;
                    // transferResult = session.PutFiles(@"" + path + "20171122142025.txt", @"//liason2.avon.com/Home/loeric/Dynamics_Payments/20171122142025.txt", false, transferOptions);
                    //transferResult = session.PutFiles(@"" + path + filename, @"//Home/loeric/Dynamics_Payments/" + filename, false, transferOptions);
					transferResult = session.PutFiles(@"" + path + filename, @"//GUI/GUPRR/" + filename, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();
                    session.Close();
                    //// Print results
                    //foreach (TransferEventArgs transfer in transferResult.Transfers)
                    //{
                    //    Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                    //}
                }

                //return 0;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error: {0}", e);
                //return 1;
            }
        }

        public string SendEmailToGuardian(string Filename)
        {
            string msg = "";
            MailMessage mailMessage = new MailMessage();
            string emailNoreplay = "policyconfirmation@midoceanpr.com"; //"lsemailservice@gmail.com"; //"policyconfirmation@midoceanpr.com";//"lsemailservice@gmail.com";
            string EmailNoReplayPass = "Conf1rm@tion";

            string html = @"<html><body>" +
            "<div align=" + "center" + ">" +
            "<br /> <br />" +

            "<p style=" + "font-family:Helvetica;font-size:30px;color:Black;" + ">Transaction File Processed</p>" +
            "<br />" +
            "<p style=" + "font-family:Helvetica;font-size:20px;color:Black;" + ">File Attached: " + Filename + "</p>" +

             "<br />" +
                //"<p style=" + "font-family:Helvetica;font-size:24px;color:Red;" + ">THIS IS A CONFIDENTIAL FILE</p>" +
            "<p style=" + "font-family:Helvetica;font-size:24px;color:Red;" + ">THIS IS ONLY A TEST</p>" +
              "<br /><br />" +
            "<p style=" + "font-family:Helvetica;font-size:14px;color:Gray;" + ">FROM GUARDIAN INSURANCE AUTOMATIC FILE TRANSFER SERVICES</p>" +

            //"<br />" +
            "<img src=" + "cid:GreyLine" + " width=" + "350x" + " height=" + "4px" + ">" +
            "<br />" +
             "<img src=" + "cid:companyLogo" + " width=" + "235" + " height=" + "104" + ">" +
            "<br /><br />" +
            "</div>" +
            "</body></html>";

            //string emailNoreplay = "policyconfirmation@midoceanpr.com"; // ConfigurationManager.AppSettings["EmailNoReplay"].ToString().Trim(); // "mail.puertoricohosting.com";

            //mailMessage.To.Add("smartinez@guardianinsurance.com");
            //mailMessage.To.Add("rcruz@guardianinsurance.com");
            //mailMessage.To.Add("vlanza@lanzasoftware.com");
           // mailMessage.To.Add("ydejesus@lanzasoftware.com");
            mailMessage.To.Add("arosado@lanzasoftware.com");


            mailMessage.From = new System.Net.Mail.MailAddress(emailNoreplay);
            mailMessage.Subject = "Guardian Road Assist Transactions File Processed";

            string destFile = @"F:\inetpub\wwwroot\EpmsTest\RoadAssistFile\Processed\" + Filename; //@"C:\inetpub\wwwroot\Guardian_ePMS\RoadAssistFile\Processed\" + Filename;
            mailMessage.Attachments.Add(new Attachment(destFile));
            mailMessage.Body = "\r\n";
            mailMessage.Body += html;

            AlternateView av = AlternateView.CreateAlternateViewFromString(mailMessage.Body, new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Html));
            av.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
            LinkedResource logo = new LinkedResource("F:\\inetpub\\wwwroot\\EpmsTest\\Images2\\guardianLogo.png", MediaTypeNames.Image.Jpeg);  // new LinkedResource("C:\\inetpub\\wwwroot\\Guardian_ePMS\\Images2\\guardianLogo.png", MediaTypeNames.Image.Jpeg);
            logo.ContentId = "companyLogo";
            av.LinkedResources.Add(logo);
            //mailMessage.AlternateViews.Add(av);     

            LinkedResource logo3 = new LinkedResource("F:\\inetpub\\wwwroot\\EpmsTest\\Images2\\GreyLine.png", MediaTypeNames.Image.Jpeg); //new LinkedResource("C:\\inetpub\\wwwroot\\Guardian_ePMS\\Images2\\GreyLine.png", MediaTypeNames.Image.Jpeg);
            logo3.ContentId = "GreyLine";
            av.LinkedResources.Add(logo3);
            mailMessage.AlternateViews.Add(av);

            try
            {
                string emailNoreplayPass = emailNoreplay; //ConfigurationManager.AppSettings["EmailNoReplayPass"].ToString().Trim(); // "mail.puertoricohosting.com";

                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(emailNoreplay, EmailNoReplayPass); // new System.Net.NetworkCredential(emailNoreplay, emailNoreplayPass);
                client.Host = ConfigurationManager.AppSettings["IPMail"].ToString().Trim();    //client.Host = "smtp.gmail.com";
                client.Port = 587; // 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(mailMessage);
                msg = "0001";
            }
            catch (Exception exc)
            {
                msg = exc.InnerException.ToString() + " " + exc.Message;
            }

            return msg;
        }
    }
}