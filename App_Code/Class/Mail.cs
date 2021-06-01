using System;
using WebMail = System.Web.Mail;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using System.Net;
using System.Web;
using System.Web.UI;

namespace EPolicy
{
	/// <summary>
	/// Summary description for Mail.
	/// </summary>
	public class Mail
	{
		public Mail()
		{

		}

		//private string _PDFPathName = ConfigurationSettings.AppSettings["PDFPathName"];
		private string _MailFrom    = "";
		private string _MailTo      = "";
		private string[] _MailToCollection;
		private string _MailSubject = "";
		private string _MailBody    = "";
		private ListBox _MailAttach  = null;

		#region Properties

		public string MailFrom
		{
			get
			{
				return (string) _MailFrom;
			}
			set 
			{
				_MailFrom = value;
			}
		}

		public string MailTo
		{
			get
			{
				return (string) _MailTo;
			}
			set 
			{
				_MailTo = value;
			}
		}

		public string[] MailToCollection
		{
			get
			{
				return  _MailToCollection;
			}
			set 
			{
				_MailToCollection = value;
			}
		}


		public string MailSubject
		{
			get
			{
				return (string) _MailSubject;
			}
			set 
			{
				_MailSubject = value;
			}
		}

		public ListBox MailAttach
		{
			get
			{
				return _MailAttach;
			}
			set 
			{
				_MailAttach = value;
			}
		}

		public string MailBody
		{
			get
			{
				return (string) _MailBody;
			}
			set 
			{
				_MailBody = value;
			}
		}
		#endregion

		private void SendMailExample()
		{
			//Set Mailer = Server.CreateObject("SMTPsvg.Mailer")

			//Mailer.RemoteHost  = "mail.phcpr.com"
			//
			//Mailer.FromName    = "Webserver Form"
			//Mailer.FromAddress = "webserver@phcpr.com"
			//Mailer.AddRecipient "Victor Lanza", "victor@phcpr.com"
			//Mailer.Subject     = "Contact Us Page has been submitted"
			//
			//strMsgHeader = "Form Information Follows: " & vbCrLf
			//for i = 1 to Request.Form.Count
			//  strMsgInfo = strMsgInfo & Request.Form.Key(i) & " - " &  Request.Form.Item(i) & vbCrLf
			//next
			//strMsgFooter = vbCrLf & "End of form information"
			//Mailer.BodyText = strMsgHeader & strMsgInfo & strMsgFooter
			//
			//if not Mailer.SendMail then
			//  Response.Write " Mailing Failed... Error is: <br>"
			//  Response.Write Mailer.Response
			//else
			//  Response.redirect "/thankyou.htm"
			//end if
		}
	

		public string SendMail()
		{
			string msg = "";
			WebMail.MailMessage mailMessage = new WebMail.MailMessage();	

			mailMessage.From	   = MailFrom ;
			mailMessage.To         = MailTo;  
			mailMessage.Subject    = MailSubject;
			mailMessage.Body	   = MailBody; 
			mailMessage.BodyFormat = WebMail.MailFormat.Text;     

			WebMail.MailAttachment attachment = null; 
			// check to see if there are any attachments or not
			if(MailAttach.Items.Count > 0) 
			{					
				foreach(ListItem l in MailAttach.Items)
				{
					// Attaches a new attachment contained in the List
					Page page = new Page();
					//attachment = new WebMail.MailAttachment(@"\attach\"+l.Text);
					attachment = new WebMail.MailAttachment(l.Text);
					mailMessage.Attachments.Add(attachment);						
					//													
					//					//Console.Write("\nPlease enter the URL to post data to : ");
					//					string uriString = "\\victorlanza"; //Console.ReadLine();
					//					WebClient myWebClient = new WebClient();
					//					string fileName = l.Text.Trim();  //Console.ReadLine();
					//					//Console.WriteLine("Uploading {0} to {1} ...",fileName,uriString);                  
					//					// Upload the file to the URL using the HTTP 1.0 POST.
					//					byte[] responseArray = myWebClient.UploadFile(uriString,"POST",fileName);
					//					// Decode and display the response.
					//					//Console.WriteLine("\nResponse Received.The contents of the file uploaded are: \n{0}",Encoding.ASCII.GetString(responseArray));
				}
			}


			try
			{
				//string IPMail = "mail.colonialpr.com";  //ConfigurationSettings.AppSettings["IPMail"];
				string IPMail = ConfigurationSettings.AppSettings["IPMail"];
				WebMail.SmtpMail.SmtpServer = IPMail; //"Baexch";  "12.174.230.85";
				WebMail.SmtpMail.Send(mailMessage);		
	
				msg = "Message has been sent successfuly.";
			}
			catch(Exception exc)
			{
				msg = exc.InnerException.ToString()+" "+ exc.Message;
			}

			return msg;
		}
	}
}
