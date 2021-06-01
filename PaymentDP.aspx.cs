using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using EPolicy.XmlCooker;
using Baldrich.DBRequest;
using System.Xml;
using System.Configuration;
using System.Data;
using System.Configuration;
using System.Web.Security;
using EPolicy.TaskControl;
using Baldrich.DBRequest;
using System.Net.Mail;
using WebMail = System.Web.Mail;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.IO;

using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using System.Xml;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPolicy;
using EPolicy.Customer;
using EPolicy.TaskControl;
using Baldrich.DBRequest;
using OPPReport;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using EPolicy.Quotes;
using EPolicy.XmlCooker;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.Services;
using System.Configuration;
using System.Xml.Schema;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Net.Mime;
namespace EPolicy
{
    public partial class PaymentDP : System.Web.UI.Page
    {
        private int TaskControlID = 0;
        private int PaymentID = 0;
        private string Email = "";

        private double Payment1 = 0.0;
        private double Payment2 = 0.0;
        private double Payment3 = 0.0;
        private double Payment4 = 0.0;
        private double Payment5 = 0.0;
        private double Payment6 = 0.0;
        private string Fecha1 ="";
        private string Fecha2 = "";
        private string Fecha3 = "";
        private string Fecha4 = "";
        private string Fecha5 = "";
        private string Fecha6 = "";
        private string CustomerNumber = "";
        private string ResultMessage = "";
        private string ResultMessagePV = "";
        private string RequestInfo = "";
        private string RequestResponse = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = cp.UserID;

            if (cp == null)
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                if (!cp.IsInRole("PAYMENT") && !cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("GUARDIAN XTRA") && !cp.IsInRole("GUARIDANROADASSISTANCE"))
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
                    Response.Redirect("Default.aspx?001");
                }
            }

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            string b_id = Request.QueryString["id"];

            if (b_id != null)
            {
                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();

                TaskControlID = int.Parse(encrypt.Decrypt(b_id));  
                SetPaymentAmount();
            }
            else
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
                Response.Redirect("Default.aspx?001");
            }

            if (!IsPostBack)
            {                              
                SetPaymentFields();

                //CreditoYear
                int year = DateTime.Today.Year;
                for (int i = 0; i < 11; i++)
                {
                    ddlYear.Items.Add(year.ToString());
                    year++;
                }
                ddlYear.SelectedIndex = -1;
                ddlYear.Items.Insert(0, "");

                //Mes
                ddlMes.Items.Add("Enero");
                ddlMes.Items.Add("Febrero");
                ddlMes.Items.Add("Marzo");
                ddlMes.Items.Add("Abril");
                ddlMes.Items.Add("Mayo");
                ddlMes.Items.Add("Junio");
                ddlMes.Items.Add("Julio");
                ddlMes.Items.Add("Agosto");
                ddlMes.Items.Add("Septiembre");
                ddlMes.Items.Add("Octubre");
                ddlMes.Items.Add("Noviembre");
                ddlMes.Items.Add("Diciembre");
                ddlMes.SelectedIndex = -1;
                ddlMes.Items.Insert(0, "");

                //RoutingNumber
                DataTable dtRoutingNumber = GetdtRoutingNumber();
                ddlRoutingNumber.DataSource = dtRoutingNumber;
                ddlRoutingNumber.DataTextField = "RoutingNumberDesc";
                ddlRoutingNumber.DataValueField = "RoutingNumber";
                ddlRoutingNumber.DataBind();
                ddlRoutingNumber.SelectedIndex = -1;
                ddlRoutingNumber.Items.Insert(0, "");
            }
        }

        private void SetPaymentAmount()
        {
            EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
            CustomerNumber = taskControl.Customer.CustomerNo.Trim();


            if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsDebitPayment"] && !IsPostBack)
            {
                ddlMetodoPago.Items.RemoveAt(3);
                ddlMetodoPago.Items.RemoveAt(3);
                ////ddlMetodoPago.SelectedIndex = ddlMetodoPago.Items.IndexOf(ddlMetodoPago.Items.FindByValue("1"));
            }

            if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsCreditPayment"] && !IsPostBack)
            {
                ddlMetodoPago.Items.RemoveAt(1);
                ddlMetodoPago.Items.RemoveAt(1);
                ////ddlMetodoPago.SelectedIndex = ddlMetodoPago.Items.IndexOf(ddlMetodoPago.Items.FindByValue("3"));
            }

            if (taskControl.PolicyClassID == 23) //Guardian Xtra
            {
                if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsFourPayment"])
                {
                    Fecha1 = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();
                    Fecha2 = DateTime.Parse(Fecha1).AddMonths(1).ToShortDateString();
                    Fecha3 = DateTime.Parse(Fecha1).AddMonths(2).ToShortDateString();
                  

                    if ((double) taskControl.GuardianXtraCollection.Rows[0]["Premium"] == 89)
                    {
                        ddlPaymentAmount.Text = "22.25";
                        Payment1 = 25.25;
                        Payment2 = 25.25;
                        Payment3 = 25.25;
                    }

                    if ((double)taskControl.GuardianXtraCollection.Rows[0]["Premium"] == 95)
                    {
                        ddlPaymentAmount.Text = "23.75";
                        Payment1 = 26.75;
                        Payment2 = 26.75;
                        Payment3 = 26.75;
                    }

                    if ((double)taskControl.GuardianXtraCollection.Rows[0]["Premium"] == 100)
                    {
                        ddlPaymentAmount.Text = "25.00";
                        Payment1 = 28.00;
                        Payment2 = 28.00;
                        Payment3 = 28.00;
                    }
                }
                else
                    if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsSixPayment"])
                    {
                        Fecha1 = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();
                        Fecha2 = DateTime.Parse(Fecha1).AddMonths(1).ToShortDateString();
                        Fecha3 = DateTime.Parse(Fecha1).AddMonths(2).ToShortDateString();
                        Fecha4 = DateTime.Parse(Fecha1).AddMonths(3).ToShortDateString();
                        Fecha5 = DateTime.Parse(Fecha1).AddMonths(4).ToShortDateString();
                        Fecha6 = DateTime.Parse(Fecha1).AddMonths(5).ToShortDateString();

                        if ((double)taskControl.GuardianXtraCollection.Rows[0]["Premium"] == 89)
                        {
                            ddlPaymentAmount.Text = "14.85";
                            Payment1 = 18.83;
                            Payment2 = 18.83;
                            Payment3 = 18.83;
                            Payment4 = 18.83;
                            Payment5 = 18.83;
                            Payment6 = 18.83;
                        }

                        if ((double)taskControl.GuardianXtraCollection.Rows[0]["Premium"] == 95)
                        {
                            ddlPaymentAmount.Text = "15.85";
                            Payment1 = 19.83;
                            Payment2 = 19.83;
                            Payment3 = 19.83;
                            Payment4 = 19.83;
                            Payment5 = 19.83;
                            Payment6 = 19.83;
                        }

                        if ((double)taskControl.GuardianXtraCollection.Rows[0]["Premium"] == 100)
                        {
                            ddlPaymentAmount.Text = "16.70";
                            Payment1 = 20.66;
                            Payment2 = 20.66;
                            Payment3 = 20.66;
                            Payment4 = 20.66;
                            Payment5 = 20.66;
                            Payment6 = 20.66;
                        }
                    }
                    else
                    {
                        ddlPaymentAmount.Text = ((double)taskControl.GuardianXtraCollection.Rows[0]["Premium"]).ToString("###,###.00");
                    }
            }
        }
    
        private DataTable GetdtRoutingNumber()
        {
           
            DataTable dt = null;

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[0];
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
            dt = exec.GetQuery("GetBankRoutingNumber", xmlDoc);
          

            return dt;
        }     

        protected DataTable GetSearchResult(string BusinessID)
        {
            //EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
            //WsStopNView ws = new WsStopNView();
            DataTable dt = null;

            //dt = ws.GetSearchResultByBusinessID(BusinessID);

            return dt;
        }

        protected void ddlMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMetodoPago_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddlMetodoPago.BorderColor = System.Drawing.Color.Transparent;
            ddlRoutingNumber.BorderColor = System.Drawing.Color.Transparent;
            txtAccountNumber.BorderColor = System.Drawing.Color.Transparent;
            ddlYear.BorderColor = System.Drawing.Color.Transparent;
            ddlMes.BorderColor = System.Drawing.Color.Transparent;
            txtSecurityCode.BorderColor = System.Drawing.Color.Transparent;
            txtAccountNombre.BorderColor = System.Drawing.Color.Transparent;
            ddlPaymentAmount.BorderColor = System.Drawing.Color.Transparent;

            lblerror.Text = "";
            lblerror.Visible = false;

            txtAccountNumber.Text = "";
            if (ddlMetodoPago.SelectedItem.Value.Trim() == "1" || ddlMetodoPago.SelectedItem.Value.Trim() == "2")
            {
                txtAccountNumber.MaxLength = 20;
                divAccBank.Style.Add("height", "110");
                divTarjeta.Style.Add("height", "0");
            }
            else
            {
                txtAccountNumber.MaxLength = 16;
                divAccBank.Style.Add("height", "0");
                divTarjeta.Style.Add("height", "100");
            }

            SetPaymentFields();
        }

        private void SetPaymentFields()
        {
            if (ddlMetodoPago.SelectedItem.Value.Trim() == "1" || ddlMetodoPago.SelectedItem.Value.Trim() == "2") //Checking & Saving
            {
                txtAccountNombre.Visible = true;
                ddlRoutingNumber.Visible = true;
                txtAccountNumber.Visible = true;
                txtSecurityCode.Visible = false;
                cvvimg.Visible = false;
                ddlMes.Visible = false;
                ddlYear.Visible = false;
                ddlPaymentAmount.Visible = true;

                lblAccName.Visible = true;
                lblRoutingNo.Visible = true;
                lblAccNo.Visible = true;
                lblSecCode.Visible = false;
                lblAccNo.Text = "Número de la Cuenta";
                lblPaymentAmount.Visible = true;
                lblMesExp.Visible = false;
                lblAnoExp.Visible = false;

                txtAccountNumber.MaxLength = 20;
                divAccBank.Style.Add("height", "110");
                divTarjeta.Style.Add("height", "0");
            }
            else // visa / mastercard
            {
                ddlRoutingNumber.Visible = false;
                txtSecurityCode.Visible = true;
                cvvimg.Visible = true;
                ddlMes.Visible = true;
                ddlYear.Visible = true;

                txtAccountNombre.Visible = true;
                ddlRoutingNumber.Visible = false;
                txtAccountNumber.Visible = true;
                txtSecurityCode.Visible = true;
                cvvimg.Visible = true;
                ddlMes.Visible = true;
                ddlYear.Visible = true;
                ddlPaymentAmount.Visible = true;

                lblAccName.Visible = true;
                lblSecCode.Visible = true;
                lblAccNo.Visible = true;
                lblRoutingNo.Visible = false;
                lblAccNo.Text = "Número de la Tarjeta";
                lblPaymentAmount.Visible = true;
                lblMesExp.Visible = true;
                lblAnoExp.Visible = true;

                txtAccountNumber.MaxLength = 16;
                divAccBank.Style.Add("height", "0");
                divTarjeta.Style.Add("height", "100");
            }

            if (ddlMetodoPago.SelectedItem.Value.Trim() == "") //"" 
            {
                txtAccountNombre.Visible = false;
                ddlRoutingNumber.Visible = false;
                txtAccountNumber.Visible = false;
                txtSecurityCode.Visible = false;
                cvvimg.Visible = false;
                ddlMes.Visible = false;
                ddlYear.Visible = false;
                ddlPaymentAmount.Visible = false;

                lblAccName.Visible = false;
                lblRoutingNo.Visible = false;
                lblSecCode.Visible = false;
                lblAccNo.Visible = false;
                lblPaymentAmount.Visible = false;
                lblMesExp.Visible = false;
                lblAnoExp.Visible = false;

                txtAccountNumber.MaxLength = 16;
                divAccBank.Style.Add("height", "0");
                divTarjeta.Style.Add("height", "0");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {


                if (!ValidFields())
                {
                    if (!chkTermino.Checked)
                    {
                        throw new Exception("Favor acceptar los terminos y servicios para completar la transacción.");
                    }
                    else
                    {
                        if (ProcesarPago())
                        {
                            Session.Add("MsgHeader", "Su pago fue procesado exitosamente");
                            Session.Add("MsgDetail", "Su número de referencia es " + ResultMessage.Trim());

                            Session.Add("MsgDetail2", ResultMessagePV.Trim());

                            //SendMail();
                        }
                        else
                        {
                            Session.Add("MsgHeader", "Su pago no pudo ser procesado");
                            Session.Add("MsgDetail", "Error: " + ResultMessage.Trim());
                            Session.Add("MsgDetail2", ResultMessagePV.Trim());
                        }

                        Response.Redirect("Message.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                
            }
            catch (Exception exp)
            {
                LogError(exp);
                lblRecHeader.Text = exp.Message;//" " + exp ;
                mpeSeleccion.Show();
            }
        }

        protected static void SendMail() 
        {
            MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
        }

        private bool ValidFields()
        {
            lblerror.Visible = false;
            lblerror.Text = "";

            ddlMetodoPago.BorderColor = System.Drawing.Color.Transparent;
            ddlRoutingNumber.BorderColor = System.Drawing.Color.Transparent;
            txtAccountNumber.BorderColor = System.Drawing.Color.Transparent;
            ddlYear.BorderColor = System.Drawing.Color.Transparent;
            ddlMes.BorderColor = System.Drawing.Color.Transparent;
            txtSecurityCode.BorderColor = System.Drawing.Color.Transparent;
            txtAccountNombre.BorderColor = System.Drawing.Color.Transparent;
            ddlPaymentAmount.BorderColor = System.Drawing.Color.Transparent;
            //txtErrorMessage.Text = "";
            ArrayList errorMessages = new ArrayList();
            bool IsError = false;

            try
            {

                if (ddlMetodoPago.SelectedItem.Value.Trim() == "")
                {
                    errorMessages.Add("Debes de escoger un Método de Pago." + "\r\n");
                    IsError = true;
                    ddlMetodoPago.BorderColor = System.Drawing.Color.Red;
                }

                if (!IsError)
                {
                    if (txtAccountNombre.Text.Trim() == "")
                    {
                        errorMessages.Add("Debes de entrar el nombre de la cuenta." + "\r\n");
                        IsError = true;
                        txtAccountNombre.BorderColor = System.Drawing.Color.Red;
                    }

                    if (txtAccountNumber.Text.Trim() == "")
                    {
                        errorMessages.Add("Debes de entrar el número de cuenta." + "\r\n");
                        IsError = true;
                        txtAccountNumber.BorderColor = System.Drawing.Color.Red;
                    }

                    if (ddlPaymentAmount.Text.Trim() == "")
                    {
                        errorMessages.Add("Debes de escoger la cantidad a pagar." + "\r\n");
                        IsError = true;
                        ddlPaymentAmount.BorderColor = System.Drawing.Color.Red;
                    }

                    if (ddlMetodoPago.SelectedItem.Value.Trim() == "1" || ddlMetodoPago.SelectedItem.Value.Trim() == "2") //Checking & Saving
                    {
                        if (ddlRoutingNumber.SelectedItem.Text.Trim() == "")
                        {
                            errorMessages.Add("Debes de escoger la ruta y transito de la cuenta." + "\r\n");
                            IsError = true;
                            ddlRoutingNumber.BorderColor = System.Drawing.Color.Red;
                        }

                        //if (txtAccountNumber.Text.Trim().Length != 9)
                        //{
                        //    errorMessages.Add("Error en el número de cuenta de banco, deben ser 9 digitos." + "\r\n");
                        //}
                    }

                    if (int.Parse(ddlMetodoPago.SelectedItem.Value.Trim()) > 2) //TarjetaCredito
                    {
                        if (ddlYear.SelectedItem.Text.Trim() == "")
                        {
                            errorMessages.Add("Debes de seleccionar el año de expiración de la tarjeta de crédito." + "\r\n");
                            IsError = true;
                            ddlYear.BorderColor = System.Drawing.Color.Red;
                        }
                        if (ddlMes.SelectedItem.Value.Trim() == "")
                        {
                            errorMessages.Add("Debes de seleccionar el mes de expiración de la tarjeta de crédito." + "\r\n");
                            IsError = true;
                            ddlMes.BorderColor = System.Drawing.Color.Red;
                        }
                        if (txtSecurityCode.Text.Trim() == "" || txtSecurityCode.Text.Trim().Length < 3)
                        {
                            errorMessages.Add("Debes de entrar el número de seguridad." + "\r\n");
                            IsError = true;
                            txtSecurityCode.BorderColor = System.Drawing.Color.Red;
                        }
                    }

                    if (!IsError)
                    {
                        if (int.Parse(ddlMetodoPago.SelectedItem.Value.Trim()) > 2) //TarjetaCredito
                        {
                            if (ddlYear.SelectedItem.Text.Trim() == "")
                            {
                                errorMessages.Add("Debes de seleccionar el año de expiración de la tarjeta de crédito." + "\r\n");
                                IsError = true;
                                ddlYear.BorderColor = System.Drawing.Color.Red;
                            }
                            if (ddlMes.SelectedItem.Text.Trim() == "")
                            {
                                errorMessages.Add("Debes de seleccionar el mes de expiración de la tarjeta de crédito." + "\r\n");
                                IsError = true;
                                ddlMes.BorderColor = System.Drawing.Color.Red;
                            }

                            if (ddlMes.SelectedItem.Value.Trim() != "" && ddlYear.SelectedItem.Value.Trim() != "")
                            {
                                string mes = GetMonth();

                                if (int.Parse(mes) < DateTime.Now.Month &&
                                    int.Parse(ddlYear.SelectedItem.Text.Trim()) == DateTime.Now.Year)
                                {
                                    errorMessages.Add("Error, Tarjeta expirada, Favor de verificar el mes y año de la tarjeta de crédito." + "\r\n");
                                    IsError = true;
                                    ddlYear.BorderColor = System.Drawing.Color.Red;
                                    ddlMes.BorderColor = System.Drawing.Color.Red;
                                }
                            }

                            if (ddlMetodoPago.SelectedItem.Value.Trim() == "3") //Visa
                            {
                                if (txtAccountNumber.Text.Trim().Length != 16)
                                {
                                    errorMessages.Add("Error en el número de la tarjeta de crédito, deben ser 16 digitos." + "\r\n");
                                    IsError = true;
                                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
                                }

                                if (txtAccountNumber.Text.Trim().Substring(0, 1) != "4")
                                {
                                    errorMessages.Add("Error, este número de tarjeta de crédito no pertenece a " +
                                            ddlMetodoPago.SelectedItem.Text.Trim() + ".\r\n");
                                    IsError = true;
                                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
                                }
                            }

                            if (ddlMetodoPago.SelectedItem.Value.Trim() == "4") //MasterCard
                            {
                                if (txtAccountNumber.Text.Trim().Length != 16)
                                {
                                    errorMessages.Add("Error en el número de la tarjeta de crédito, deben ser 16 digitos." + "\r\n");
                                    IsError = true;
                                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
                                }

                                if (txtAccountNumber.Text.Trim().Substring(0, 1) != "5")
                                {
                                    errorMessages.Add("Error, este número de tarjeta de crédito no pertenece a " +
                                       ddlMetodoPago.SelectedItem.Text.Trim() + ".\r\n");
                                    IsError = true;
                                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
                                }
                            }

                            if (ddlMetodoPago.SelectedItem.Value.Trim() == "5") //Amex
                            {
                                if (txtAccountNumber.Text.Trim().Length != 15)
                                {
                                    errorMessages.Add("Error en el número de la tarjeta de crédito, deben ser 15 digitos." + "\r\n");
                                    IsError = true;
                                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
                                }

                                if (txtAccountNumber.Text.Trim().Substring(0, 1) != "3")
                                {
                                    errorMessages.Add("Error, este número de tarjeta de crédito no pertenece a " +
                                        ddlMetodoPago.SelectedItem.Text.Trim() + ".\r\n");
                                    IsError = true;
                                    txtAccountNumber.BorderColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }
                }

                if (errorMessages.Count > 0)
                {
                    string popUpString = "";
                    int maxMess = 1;

                    foreach (string message in errorMessages)
                    {
                        popUpString += " " + maxMess.ToString().Trim() + ". " + message + "";
                        maxMess++;
                    }

                    int a = maxMess * 30;

                    if (a < 61)
                        a = 65;

                    Unit u = new Unit(a.ToString() + "px");

                    lblerror.Height = u;
                    lblerror.Visible = true;
                    lblerror.Text = popUpString.ToString();
                    // ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "ResetScrollPosition();", true);
                }

                return IsError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetMonth()
        {
            string mes = "";
            switch (ddlMes.SelectedItem.Text.Trim())
            {

                case "Enero":
                    mes = "01";
                    break;
                case "Febrero":
                    mes = "02";
                    break;
                case "Marzo":
                    mes = "03";
                    break;
                case "Abril":
                    mes = "04";
                    break;
                case "Mayo":
                    mes = "05";
                    break;
                case "Junio":
                    mes = "06";
                    break;
                case "Julio":
                    mes = "07";
                    break;
                case "Agosto":
                    mes = "08";
                    break;
                case "Septiembre":
                    mes = "09";
                    break;
                case "Octubre":
                    mes = "10";
                    break;
                case "Noviembre":
                    mes = "11";
                    break;
                case "Diciembre":
                    mes = "12";
                    break;
            }

            return mes;
        }

        private bool ProcesarPago()
        {
            EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
            string PaymentMethod = "";
            string CustomerName = "";
            string PaymentType = "";
            if (Session["IsDebitPayment"].ToString() == "1")
            {
                PaymentMethod = "transacción de débito ACH a su cuenta de cheques/ahorros";//"ACH debit transaction to your checking/savings account";
                PaymentType = "cuenta";//"account";

            }
            else
            {
                PaymentMethod = "transacción de débito de tarjeta de crédito";//"credit card debit transaction";
                PaymentType = "tarjeta de crédito";//"credit card";
            }

            if(taskControl.Customer.FirstName.ToString() != "")
            {
                CustomerName = taskControl.Customer.FirstName.ToString() + " " + taskControl.Customer.LastName1.ToString() + " " + taskControl.Customer.LastName2.ToString();
            }
            else if(taskControl.Customer.Initial.ToString() != "")
            {
                CustomerName = taskControl.Customer.FirstName.ToString()+ " " + taskControl.Customer.Initial.ToString() + " " +taskControl.Customer.LastName1.ToString() + " " + taskControl.Customer.LastName2.ToString();
            }
            
            

            bool Result = false;

            //Payment Transaction
            string transactionNumber = GetTransactionNumber(); //Debe ser un consecutivo y Unico
            string Reqdata = "";
            //string xmlResult = PaymentTransaction("LS91A2QS-7522-4DWD-4219-A515469D569P", Reqdata, 1, transactionNumber);
            //AddPayment
            //string[] splitData = xmlResult.Split('|');
            //txtAccountNombre.Text = xmlResult.ToString();
            //AddPayment(transactionNumber, splitData[1].Trim(), splitData[0].Trim());

            //string[] splitData;

            //Create Customer and InstallPayment
             //xmlResult = "";

            string xmlResult = "Success|" + transactionNumber;
            string[] splitData = xmlResult.Split('|');

            if (1 == 1)
            {
                if (xmlResult.Contains("Success"))
                {
                    //AddPayment

                    //txtAccountNombre.Text = xmlResult.ToString();
                    AddPayment(transactionNumber, splitData[1].Trim(), splitData[0].Trim(), xmlResult.ToString(), RequestInfo, RequestResponse);
                    RequestInfo = "";
                    RequestResponse = "";
                }
                else
                {
                    //txtAccountNombre.Text = xmlResult.ToString();
                    if (splitData[1] != null)
                    {
                        AddPayment(transactionNumber, "", "", splitData[0].Trim() + " - " + splitData[1].Trim(), RequestInfo, RequestResponse);
                        RequestInfo = "";
                        RequestResponse = "";
                    }
                    else
                    {
                        AddPayment(transactionNumber, "", "", xmlResult.ToString(), RequestInfo, RequestResponse);
                        RequestInfo = "";
                        RequestResponse = "";
                    }
                    ResultMessagePV = "Could not set installment payments, Please verify in Dynamic Payment Console";
                }
            }
            else
            {
                xmlResult = PaymentVault("LS91A2QS-7522-4DWD-4219-A515469D569P", Reqdata, 1, transactionNumber);
                splitData = xmlResult.Split('|');

                if (xmlResult.Contains("Success"))
                {
                    //AddPayment

                    txtAccountNombre.Text = xmlResult.ToString();
                    AddPayment(transactionNumber, splitData[1].Trim(), splitData[0].Trim(), xmlResult.ToString(), RequestInfo, RequestResponse);
                    RequestInfo = "";
                    RequestResponse = "";
                }
                else
                {
                    //txtAccountNombre.Text = xmlResult.ToString();
                    if (splitData != null)
                    {
                        if (splitData.Count() > 1)
                        {
                            if (splitData[1] != null)
                            {
                                AddPayment(transactionNumber, "", "", splitData[0].Trim() + " - " + splitData[1].Trim(), RequestInfo, RequestResponse);
                                RequestInfo = "";
                                RequestResponse = "";
                            }
                            else
                            {
                                AddPayment(transactionNumber, "", "", xmlResult.ToString(), RequestInfo, RequestResponse);
                                RequestInfo = "";
                                RequestResponse = "";
                            }
                        }
                    }
                    else
                    {
                        AddPayment(transactionNumber, "", "", xmlResult.ToString(), RequestInfo, RequestResponse);
                        RequestInfo = "";
                        RequestResponse = "";
                    }
                    ResultMessagePV = "Could not set installment payments, Please verify in Dynamic Payment Console";
                }
            }
           


            if (xmlResult.Contains("Success"))
            {
                ResultMessage = splitData[1].Trim();
                Result = true;

                string AccNumber = txtAccountNumber.Text.Trim().Substring(txtAccountNumber.Text.Trim().Length - 4);

                if (taskControl.Customer.Email.ToString() != "")
                {
                    string EntryDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month).ToString() + " " + (DateTime.Today.Day.ToString().Length == 1 ? "0" + DateTime.Today.Day.ToString() : DateTime.Today.Day.ToString()) + ", " + DateTime.Today.Year;
                    string DebitDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.AddDays(1).Month).ToString() + " " + (DateTime.Today.AddDays(1).Day.ToString().Length == 1 ? "0" + DateTime.Today.AddDays(1).Day.ToString() : DateTime.Today.AddDays(1).Day.ToString()) + ", " + DateTime.Today.AddDays(1).Year;

                    PrintAfterPay();
                    SendEmail(CustomerName, taskControl.Customer.Email.ToString(), PaymentMethod, PaymentType, ddlPaymentAmount.Text.ToString(), AccNumber, EntryDate, taskControl.PolicyType + taskControl.PolicyNo, DebitDate);
                    txtPDFFile.Text = "";
                }
            }
            else
            {
                if (splitData.Count() > 1)
                {
                    ResultMessage = splitData[0].Trim() + " - " + splitData[1].Trim();
                }
                else
                {
                    ResultMessage = "Failed transaction";
                }
                Result = false;
            }

            return Result;
        }

        private void PrintAfterPay()
        {
            try
            {

                    EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                    List<string> mergePaths = new List<string>();
                    string ProcessesPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

                    mergePaths.Add(PrintPreview("SolicitudGuardianXtra.rdlc"));
                    if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsSixPayment"])//taskControl.XtraIsSixPayment)
                    {
                        mergePaths.Add(PrintPreview("Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc"));
                        mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas6Plazos.rdlc"));
                    }

                    if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsFourPayment"])//taskControl.XtraIsFourPayment)
                    {
                        mergePaths.Add(PrintPreview("SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc"));
                        mergePaths.Add(PrintPreview("PlandePagoDiferidodePrimas4Plazos.rdlc"));
                    }

                    if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsCreditPayment"])//taskControl.XtraIsCreditPayment)
                    {
                        mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc"));
                    }

                    if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsDebitPayment"])//taskControl.XtraIsDebitPayment)
                    {
                        mergePaths.Add(PrintPreview("MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc"));
                    }

                    mergePaths.Add(PrintPreview("HojadeDeclaraciones_XTRA.rdlc"));
                    mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina1.rdlc"));
                    mergePaths.Add(PrintPreview("PolizaAutoPersonalPR3Pagina2.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina2.rdlc"));
                    mergePaths.Add(PrintPreview("ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubierta_PRPagina3.rdlc"));

                    OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                    string finalFile = "";
                    finalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessesPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);
                    txtPDFFile.Text = finalFile;
                    taskControl.PrintPolicy = true;

            }
            catch (Exception exc)
            {
                lblRecHeader.Text = exc.Message.ToString() + " - " + exc.ToString();
                mpeSeleccion.Show();
            }
        }

        private string PrintPreview(string rdlcDoc)
        {
            try
            {
                EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                ReportViewer viewer = new ReportViewer();
                string ProcessPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

                viewer.LocalReport.DataSources.Clear();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("Reports/GuardianXtra/" + rdlcDoc);
                Microsoft.Reporting.WebForms.ReportDataSource rds = null;

                if (rdlcDoc == "SolicitudGuardianXtra.rdlc")
                {

                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                    ReportParameter[] parameters = new ReportParameter[26];

                    parameters[0] = new ReportParameter("Prefix", taskControl.PolicyType.ToString().Trim());
                    parameters[1] = new ReportParameter("Term", taskControl.Term.ToString().Trim());
                    parameters[2] = new ReportParameter("PolicyNo", taskControl.PolicyNo.ToString().Trim());
                    parameters[3] = new ReportParameter("EffDate", taskControl.EffectiveDate.ToString().Trim());
                    parameters[4] = new ReportParameter("ExpDate", taskControl.ExpirationDate.ToString().Trim());
                    parameters[5] = new ReportParameter("VehicleMake", taskControl.XtraVehicleMake.ToString().Trim());
                    parameters[6] = new ReportParameter("VehicleModel", taskControl.XtraVehicleModel.ToString().Trim());
                    parameters[7] = new ReportParameter("VehicleYear", taskControl.XtraVehicleYear.ToString().Trim());
                    parameters[8] = new ReportParameter("VehicleVIN", taskControl.XtraVIN.ToString().Trim());
                    parameters[9] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());
                    parameters[10] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    parameters[11] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.ToString().Trim());
                    parameters[12] = new ReportParameter("CustomerInitial", taskControl.Customer.Initial.ToString().Trim());
                    parameters[13] = new ReportParameter("CustomerLastName1", taskControl.Customer.LastName1.ToString().Trim());
                    parameters[14] = new ReportParameter("CustomerLastName2", taskControl.Customer.LastName2.ToString().Trim());
                    parameters[15] = new ReportParameter("CustomerAddrs1", taskControl.Customer.Address1.ToString().Trim());
                    parameters[16] = new ReportParameter("CustomerAddrs2", taskControl.Customer.Address2.ToString().Trim());
                    parameters[17] = new ReportParameter("CustomerCity", taskControl.Customer.City.ToString().Trim());
                    parameters[18] = new ReportParameter("CustomerState", taskControl.Customer.State.ToString().Trim());
                    parameters[19] = new ReportParameter("CustomerZip", taskControl.Customer.ZipCode.ToString().Trim());
                    parameters[20] = new ReportParameter("Agency", taskControl.Agency.ToString().Trim());
                    parameters[21] = new ReportParameter("Agent", taskControl.AgentDesc.ToString().Trim());
                    parameters[22] = new ReportParameter("AgentNo", taskControl.AgentCode.ToString().Trim());
                    parameters[23] = new ReportParameter("Premium", taskControl.TotalPremium.ToString().Trim());
                    parameters[24] = new ReportParameter("Deducible", taskControl.XtraPremium.ToString().Trim());
                    parameters[25] = new ReportParameter("PhysicalAddrs", taskControl.Customer.AddressPhysical1.ToString().Trim() + ", " + taskControl.Customer.AddressPhysical2.ToString().Trim() + " " + taskControl.Customer.CityPhysical.ToString().Trim() + ", " + taskControl.Customer.StatePhysical.ToString().Trim() + " " + taskControl.Customer.ZipPhysical.ToString().Trim());

                    // viewer.LocalReport.ReportPath = Server.MapPath("Reports/GuardianXtra/SolicitudGuardianXtra.rdlc");
                    viewer.LocalReport.SetParameters(parameters);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();

                }

                if (rdlcDoc == "Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc")
                {

                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                    ReportParameter[] param = new ReportParameter[3];

                    param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                    param[2] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());


                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();

                }

                if (rdlcDoc == "PlandePagoDiferidodePrimas6Plazos.rdlc")
                {


                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                    string Fecha = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();

                    ReportParameter[] param = new ReportParameter[8];

                    param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                    param[2] = new ReportParameter("Sufix", taskControl.Suffix.Trim());
                    param[3] = new ReportParameter("Date1", Fecha);
                    param[4] = new ReportParameter("Date2", DateTime.Parse(Fecha).AddMonths(1).ToShortDateString());
                    param[5] = new ReportParameter("Date3", DateTime.Parse(Fecha).AddMonths(2).ToShortDateString());
                    param[6] = new ReportParameter("Date4", DateTime.Parse(Fecha).AddMonths(3).ToShortDateString());
                    param[7] = new ReportParameter("Date5", DateTime.Parse(Fecha).AddMonths(4).ToShortDateString());

                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();

                }

                if (rdlcDoc == "SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc")
                {


                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


                    ReportParameter[] param = new ReportParameter[3];

                    param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                    param[2] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());


                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();

                }

                if (rdlcDoc == "PlandePagoDiferidodePrimas4Plazos.rdlc")
                {


                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                    string Fecha = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();

                    ReportParameter[] param = new ReportParameter[6];

                    param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                    param[2] = new ReportParameter("Sufix", taskControl.Suffix.Trim());
                    param[3] = new ReportParameter("Date1", Fecha);
                    param[4] = new ReportParameter("Date2", DateTime.Parse(Fecha).AddMonths(1).ToShortDateString());
                    param[5] = new ReportParameter("Date3", DateTime.Parse(Fecha).AddMonths(2).ToShortDateString());

                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();

                }

                if (rdlcDoc == "HojadeDeclaraciones_XTRA.rdlc")
                {

                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                    ReportParameter[] parameters = new ReportParameter[25];

                    parameters[0] = new ReportParameter("Prefix", taskControl.PolicyType.ToString().Trim());
                    parameters[1] = new ReportParameter("Term", taskControl.Term.ToString().Trim());
                    parameters[2] = new ReportParameter("PolicyNo", taskControl.PolicyNo.ToString().Trim());
                    parameters[3] = new ReportParameter("EffDate", taskControl.EffectiveDate.ToString().Trim());
                    parameters[4] = new ReportParameter("ExpDate", taskControl.ExpirationDate.ToString().Trim());
                    parameters[5] = new ReportParameter("VehicleMake", taskControl.XtraVehicleMake.ToString().Trim());
                    parameters[6] = new ReportParameter("VehicleModel", taskControl.XtraVehicleModel.ToString().Trim());
                    parameters[7] = new ReportParameter("VehicleYear", taskControl.XtraVehicleYear.ToString().Trim());
                    parameters[8] = new ReportParameter("VehicleVIN", taskControl.XtraVIN.ToString().Trim());
                    parameters[9] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());
                    parameters[10] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    parameters[11] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.ToString().Trim());
                    parameters[12] = new ReportParameter("CustomerInitial", taskControl.Customer.Initial.ToString().Trim());
                    parameters[13] = new ReportParameter("CustomerLastName1", taskControl.Customer.LastName1.ToString().Trim());
                    parameters[14] = new ReportParameter("CustomerLastName2", taskControl.Customer.LastName2.ToString().Trim());
                    parameters[15] = new ReportParameter("CustomerAddrs1", taskControl.Customer.Address1.ToString().Trim());
                    parameters[16] = new ReportParameter("CustomerAddrs2", taskControl.Customer.Address2.ToString().Trim());
                    parameters[17] = new ReportParameter("CustomerCity", taskControl.Customer.City.ToString().Trim());
                    parameters[18] = new ReportParameter("CustomerState", taskControl.Customer.State.ToString().Trim());
                    parameters[19] = new ReportParameter("CustomerZip", taskControl.Customer.ZipCode.ToString().Trim());
                    parameters[20] = new ReportParameter("Agency", taskControl.Agency.ToString().Trim());
                    parameters[21] = new ReportParameter("Agent", taskControl.AgentDesc.ToString().Trim());
                    parameters[22] = new ReportParameter("AgentNo", taskControl.AgentCode.ToString().Trim());
                    parameters[23] = new ReportParameter("Premium", taskControl.TotalPremium.ToString().Trim());
                    parameters[24] = new ReportParameter("Deducible", taskControl.XtraPremium.ToString().Trim());

                    viewer.LocalReport.SetParameters(parameters);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                if (rdlcDoc == "ReportEndoso_Obligatorio_de_Primas_y_Condiciones_de_Cubiert_aPRPagina1.rdlc")
                {

                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

                    ReportParameter[] param = new ReportParameter[1];

                    param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                if (rdlcDoc == "PolizaAutoPersonalPR3Pagina1.rdlc")
                {


                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    ReportParameter[] param = new ReportParameter[1];

                    param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                if (rdlcDoc == "MOI_AUTORIZACION_PARA_PAGO_CON_TARJETA_DE_CREDITO_2.rdlc")
                {

                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


                    ReportParameter[] param = new ReportParameter[3];

                    param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                    param[2] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                if (rdlcDoc == "MOI_AUTORIZACION_PARA_DEBITO_DIRECTO_2.rdlc")
                {

                    GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
                    GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

                    string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


                    ReportParameter[] param = new ReportParameter[3];

                    param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
                    param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
                    param[2] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                if (rdlcDoc == "MidOceanInvoiceES.rdlc")
                {
                    GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter ds = new GetPaymentByTaskControlIDTableAdapters.GetPaymentByTaskControlIDTableAdapter();
                    GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable dt = new GetPaymentByTaskControlID.GetPaymentByTaskControlIDDataTable();
                    ds.Fill(dt, taskControl.TaskControlID);
                    rds = new ReportDataSource("GetPaymentByTaskControlID", (DataTable)dt);

                    //ReportParameter[] param = new ReportParameter[1];

                    //param[0] = new ReportParameter("PolicyNo", taskControl.PolicyType.ToString().Trim() + "-" + taskControl.PolicyNo.ToString().Trim() + "-" + taskControl.Suffix.ToString().Trim());


                    //viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.LocalReport.Refresh();
                }

                // Variables 
                Warning[] warnings;
                string[] streamIds;
                string mimeType;
                string encoding = string.Empty;
                string extension;
                //  string fileName = "C" + taskControl.TaskControlID.ToString() + DateTime.Now.ToString().Trim();
                string _FileName = rdlcDoc.Replace(".rdlc", "") + taskControl.TaskControlID.ToString() + ".pdf";

                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName))
                    File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (FileStream fs = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Dispose();

                }
                return ProcessPath + _FileName;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        private string GetDateInSpanish(string Month)
        {
            switch (Month)
            {
                case "January":
                    return "enero";
                case "February":
                    return "febrero";
                case "March":
                    return "marzo";
                case "April":
                    return "abril";
                case "May":
                    return "mayo";
                case "June":
                    return "junio";
                case "July":
                    return "julio";
                case "August":
                    return "agosto";
                case "September":
                    return "septiembre";
                case "October":
                    return "octubre";
                case "November":
                    return "noviembre";
                case "December":
                    return "diciembre";
                default:
                    throw new Exception("Could not translate month into spanish date.");
            }
        }

        private void DeleteFile(string pathAndFileName)
        {
            if (File.Exists(pathAndFileName))
                File.Delete(pathAndFileName);
        }

        private List<string> WriteRdlcToPDF(ReportViewer viewer, EPolicy.TaskControl.GuardianXtra taskControl, List<string> mergePaths, int index)
        {
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "FileNo-" + index.ToString();
            string _FileName1 = "FileNo-" + index.ToString() + ".pdf";

            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

            byte[] bytes1 = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            using (FileStream fs1 = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1, FileMode.Create))
            {
                fs1.Write(bytes1, 0, bytes1.Length);
            }

            try
            {
                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);
            }
            catch (Exception ecp)
            {
                // ShowMessage(ecp.Message);

            }
            return mergePaths;
        }

        private void SendEmail(string CustomerName, string Email, string PaymentMethod, string PaymentType, string PaymentAmount, string AccNumber, string EntryDate, string PolicyNumber, string DebitDate)
        {
            try
            {
                EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

                var format = "MMMM dd, yyyy";
                var dt = DateTime.ParseExact(EntryDate, format, new CultureInfo("en-US"));
                EntryDate = dt.ToString(format, new CultureInfo("es-ES"));
                dt = DateTime.ParseExact(DebitDate, format, new CultureInfo("en-US"));
                DebitDate = dt.ToString(format, new CultureInfo("es-ES"));

                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
                string ImagePath = Server.MapPath(@"Images2/MidOcean_logo.png");

                LinkedResource logo = new LinkedResource(ImagePath, MediaTypeNames.Image.Jpeg);
                logo.ContentId = Guid.NewGuid().ToString();

                string htmlBody = "<html><body><p>Estimado " + CustomerName + " (" + Email + ")</p><p>Este correo electrónico es para informarle que la Agencia de Seguros MidOcean ha procesado electrónicamente una sola transacción por la cantidad de $" + PaymentAmount + " de la " + PaymentType + " que finaliza con " + AccNumber + " por su autorización en " + EntryDate + ". El número de póliza que la Agencia de Seguros MidOcean ha proporcionado para esta transacción es la siguiente: " + PolicyNumber + ".</p><p>Esta transacción se debitará de su cuenta en " + DebitDate + " y aparecerá en su estado bancario en la sección de transacciones electrónicas. Si esta transacción es un error o es una transacción fraudulenta, comuníquese con la Agencia de Seguros MidOcean al 787-520-6178 si tiene alguna pregunta o inquietud. Gracias por su pago.</p><br><img src=\"cid:" + logo.ContentId + "\"/></body></html>";
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                   (htmlBody, null, MediaTypeNames.Text.Html);
                avHtml.LinkedResources.Add(logo);

                //Email (El email que ve el que recibe)
                string emailNoreplay = "policyconfirmation@midoceanpr.com";//"lsemailservice@gmail.com";
                //Email (That send the message)
                string emailSend = "lsemailservice@gmail.com";
                string msg = "";
                string pdf = txtPDFFile.Text;
                MailMessage SM = new MailMessage();

                SM.Subject = "MidOcean Insurance - Su pago ha sido recibido";
                SM.From = new System.Net.Mail.MailAddress(emailNoreplay);

                SM.AlternateViews.Add(avHtml);
                SM.IsBodyHtml = true;
                SM.Attachments.Add(new Attachment(ProcessedPath + pdf));
                SM.To.Add(Email);

                //SM.Bcc.Add("econcepcion@guardianinsurance.com");
				//SM.Bcc.Add("smartinez@guardianinsurance.com");
				//SM.Bcc.Add("rcruz@guardianinsurance.com");
				//SM.Bcc.Add("susana.martinez11@gmail.com");

                try
                {
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailNoreplay, "Conf1rm@tion");// new NetworkCredential(emailNoreplay, "L@nzaSoft1$");
                    client.Host = ConfigurationManager.AppSettings["IPMail"].ToString().Trim();    //client.Host = "smtp.gmail.com";
                    client.Port = 587; // 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    client.Send(SM);
                    msg = "0001";
                }
                catch (Exception exc)
                {
                    msg = exc.InnerException.ToString() + " " + exc.Message;
                }

            }
            catch (Exception exc)
            {

            }
        }

        private void AddPayment(string transactionNumber, string PaymentConfirmationNumber, string Result, string ConsoleResult, string RequestInfo, string RequestResponse)
        {
            EncryptClass.EncryptClass encryp = new EncryptClass.EncryptClass();

            string VendorID = "0";

            string x = txtAccountNumber.Text.Trim().Substring(txtAccountNumber.Text.Trim().Length - 4);

            string AccNumber = encryp.Encrypt(x);

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[12];
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentMethodID", SqlDbType.Int, 0, ddlMetodoPago.SelectedItem.Value.Trim(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentDate", SqlDbType.DateTime, 0, DateTime.Now.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentAmount", SqlDbType.Money, 0, ddlPaymentAmount.Text.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("AccountNumberLast4", SqlDbType.VarChar, 100, AccNumber, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PaymentConfirmationNumber", SqlDbType.VarChar, 50, PaymentConfirmationNumber, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("TransactionNumberID", SqlDbType.Int, 0, transactionNumber, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("VendedorID", SqlDbType.Int, 0, VendorID, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Result", SqlDbType.VarChar, 50, Result, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ConsoleResult", SqlDbType.VarChar, 5000, ConsoleResult, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("RequestInfo", SqlDbType.VarChar, 8000, RequestInfo, ref cookItems);
            DbRequestXmlCooker.AttachCookItem("RequestResponse", SqlDbType.VarChar, 8000, RequestResponse, ref cookItems);
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
            PaymentID = exec.Insert("AddPayment", xmlDoc);
        }

        private string GetTransactionNumber()
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[0];
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
            int transNo = exec.Insert("AddTransactionNumber", xmlDoc);

            return transNo.ToString();
        }

        private string GetAccountReference()
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[0];
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
            int transNo = exec.Insert("AddAccountReference", xmlDoc);

            return transNo.ToString();
        }


        private string GetRecurringReference()
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[0];
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
            int transNo = exec.Insert("AddRecurringReference", xmlDoc);

            return transNo.ToString();
        }

        public string PaymentTransaction(string Password, string Transaction, int UserID, string transactionNumber)
        {
            //Visa 
            //4485055377466669
            //4556775235363865
            //4716804944927443
            //4556874611555796
            //4556107555806127

            //Master Card
            //5435606987460936
            //5341399955705614
            //5416329763111127
            //5309242750164407
            //5385945943252171

            //AMEX
            //346424054417717
            //344791662301372
            //340889953628836
            //377508409211437
            //345281936379189

            //http://www.getcreditcardnumbers.com/       

            string[] fields = new string[76];
            string[] retVal = new string[5];
            string status = "0";
            //string transactionNumber = ConfigurationManager.AppSettings["Terminal"].Trim().Trim() + GetTransactionNumber(); // "100008"; //Debe ser un consecutivo
            string returnMessage = "";
            string decryptTransValue = "";
            //EncryptClass.EncryptClass encryptClass = new EncryptClass.EncryptClass();
            //decryptTransValue = encryptClass.Encrypt(Transaction);

            //decryptTransValue = encryptClass.Decrypt(Transaction.Trim());
            string[] RequestTransaction = Transaction.Split('|'); // decryptTransValue.Split('|');

            //DataTable dt = GetSearchResult(TaskControlID.ToString());
            if (Session["TaskControl"]!=null)
            {
                EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];

                fields[0] = ddlPaymentAmount.Text.Trim();// "0.01";// txtPhone.Text.Trim(); //RequestTransaction[0]; //"15.00";
                fields[1] = taskControl.Customer.Address1.Trim(); // dt.Rows[0]["BusinessAddress1"].ToString().Trim();
                fields[2] = taskControl.Customer.Address2.Trim(); //dt.Rows[0]["BusinessAddress2"].ToString().Trim();
                fields[3] = taskControl.Customer.City.Trim();  //dt.Rows[0]["BusinessCity"].ToString().Trim();
                fields[4] = taskControl.Customer.ZipCode.Trim();  //dt.Rows[0]["BusinessZipCode"].ToString().Trim();
                fields[8] = txtAccountNombre.Text.Trim();// "VICTOR R. LANZA AMARAL"; //NameOnAccount
                fields[50] = taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim(); //dt.Rows[0]["BusinessName"].ToString().Trim();
                fields[51] = taskControl.Customer.CustomerNo.Trim();  //ddlPaymentAmount.Text.Trim();
                fields[52] = taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim(); //dt.Rows[0]["BusinessContact"].ToString().Trim();
                fields[53] = taskControl.Customer.Email.Trim(); //dt.Rows[0]["BusinessEmail"].ToString().Trim();
                Email = taskControl.Customer.Email.Trim(); //dt.Rows[0]["BusinessEmail"].ToString().Trim();
                fields[54] = taskControl.Customer.Cellular.Trim();  //dt.Rows[0]["BusinessPhone"].ToString().Trim();

                if (ddlMetodoPago.SelectedItem.Value.Trim() == "3" || ddlMetodoPago.SelectedItem.Value.Trim() == "4")
                {
                    fields[5] = "True"; //Tarjeta de Credito
                    fields[9] = GetMonth(); //ddlMes.SelectedItem.Value.Trim();// "02"; //CardExpiresMonth
                    fields[10] = ddlYear.SelectedItem.Value.Trim(); //"2018"; //CardExpiresYear
                    fields[11] = txtSecurityCode.Text.Trim();  // "000"; //CVVNumber
                    fields[7] = txtAccountNumber.Text.Trim();  // "5347650005981017"; //Tarjeta de Credito - AccountNumber

                    //Visa=1, MasterCard=2, American_Express=3
                    switch (ddlMetodoPago.SelectedItem.Value.Trim())
                    {
                        case "3": //Visa
                            fields[6] = "1";
                            break;
                        case "4": //MC
                            fields[6] = "2";
                            break;
 						 case "5": //AMEX
                            fields[6] = "3";
                            break;
                    }
                }
                else
                {
                    if (ddlMetodoPago.SelectedItem.Value.Trim() == "1" || ddlMetodoPago.SelectedItem.Value.Trim() == "2") //Checking or Saving Account
                    {
                        fields[5] = "False"; //No es tarjeta de Credito

                        if (ddlMetodoPago.SelectedItem.Value.Trim() == "1")
                        {
                            fields[12] = "True"; //Checking
                            fields[13] = "False"; //Saving
                        }
                        else
                        {
                            fields[12] = "False"; //Checking
                            fields[13] = "True"; //Saving
                        }
                        fields[14] = ddlRoutingNumber.SelectedItem.Value.Trim(); // "021502011"; //RoutingNumber
                        fields[15] = txtAccountNumber.Text.Trim(); // "041799936"; //AccountNumber - Bank Account
                    }
                }

                if (Password == "LS91A2QS-7522-4DWD-4219-A515469D569P")
                {
                    try
                    {
                        using (TP.TransactionProcessingSoapClient paymentClient = new TP.TransactionProcessingSoapClient())
                        {
                            if (paymentClient.TestConnection())
                            {

                                TP.WSUpdateResult result = paymentClient.TestCredentials(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), ConfigurationManager.AppSettings["locationId"].ToString(), "__WebService");

                                if (result.returnValue == TP.ReturnValue.Success)
                                {
                                    TP.WSTransaction transaction = new TP.WSTransaction();
                                    transaction.EntityId = Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]);
                                    transaction.LocationId = Convert.ToInt32(ConfigurationManager.AppSettings["locationId"]);

                                    transaction.Description = taskControl.PolicyType.Trim() + " " + taskControl.PolicyNo.Trim(); // "Guardian Xtra Def Pay";
                                    transaction.Field1 = TaskControlID.ToString();
                                    transaction.Field2 = fields[50];
                                    transaction.Field3 = fields[51];
                                    transaction.BillingAddress1 = fields[1];
                                    transaction.BillingAddress2 = fields[2];
                                    transaction.BillingCity = fields[3];
                                    transaction.BillingPostalCode = fields[4];
                                    transaction.BillingStateRegion = "";//"PR";
                                    transaction.BillingCountry = "";//"PUERTO RICO";
                                    transaction.TransactionNumber = transactionNumber;
                                    retVal[1] = transaction.TransactionNumber;

                                    if (fields[5] == "True")
                                    {
                                        switch (fields[6])
                                        {
                                            case "1":
                                                transaction.AccountType = TP.WSAccountType.Visa;
                                                break;
                                            case "2":
                                                transaction.AccountType = TP.WSAccountType.MasterCard;
                                                break;
                                            case "3":
                                                transaction.AccountType = TP.WSAccountType.American_Express;
                                                break;
                                        }

                                        transaction.CardExpiresMonth = Convert.ToByte(fields[9]);
                                        transaction.CardExpiresYear = Convert.ToInt16(fields[10]);
                                        transaction.NameOnAccount = fields[8];
                                        transaction.AccountNumber = fields[7];
                                        transaction.CVVNumber = int.Parse(fields[11]);
                                    }

                                    if (fields[12] == "True" || fields[13] == "True")
                                    {
                                        if (fields[12] == "True")
                                            transaction.AccountType = TP.WSAccountType.Checking;
                                        else if (fields[13] == "True")
                                            transaction.AccountType = TP.WSAccountType.Savings;
                                        transaction.NameOnAccount = fields[8];
                                        transaction.RoutingNumber = fields[14];
                                        transaction.AccountNumber = fields[15];
                                    }

                                    transaction.TotalAmount = Convert.ToDecimal(fields[0]); //Convert.ToDecimal(fields[0].Remove(0, 1));

                                    TP.WSResponseMessage response = new TP.WSResponseMessage();
                                    response = paymentClient.AuthorizeTransaction(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], transaction, TP.WSOwnerApplication.Web_Service);

                                    if (response.ResponseCode == TP.AuthResponseCode.Success)
                                    {
                                        //// Enviar Mensaje Procesamiento
                                        //SendProcessedEmail(int.Parse(fields[73]), transactionNumber);

                                        fields[74] = response.ReferenceNumber;
                                        fields[75] = response.ResponseCode.ToString();
                                        retVal[0] = "S";
                                        status = "2";
                                        //UpdateRequestEntry(fields);
                                        //SendDocumentsByEmail(int.Parse(fields[73]));
                                    }
                                    else
                                    {
                                        retVal[0] = "F";
                                        fields[74] = response.ReferenceNumber;
                                        fields[75] = response.ResponseCode.ToString();
                                        status = "4";
                                        //SendTransactionErrorEmail(int.Parse(fields[73]));
                                    }
                                }
                            }
                            else
                            {
                                //retVal[0] = "C";
                            }

                            return fields[75].Trim() + "|" + fields[74].Trim();
                        }
                    }
                    catch (Exception exc)
                    {
                        returnMessage = "Transaction Fail.";
                        if (exc.Message.Trim().Contains("The message was"))
                        {
                            int index = exc.Message.Trim().IndexOf("The message was");
                            returnMessage = exc.Message.Trim().Substring(index + 16);

                            index = returnMessage.Trim().IndexOf(".");
                            returnMessage = returnMessage.Substring(0, index + 1);
                        }

                        return returnMessage;
                    }
                }
                else
                {
                    return "Wrong Password.";
                }
            }
            else
            {
                return "Error Procesando Pago, no pudo encontrar el Busines.";
            }

        }

        public string PaymentVault(string Password, string Transaction, int UserID, string transactionNumber)
        {
            string[] fields = new string[76];
            string[] retVal = new string[5];
            string status = "0";


            //string transactionNumber = ConfigurationManager.AppSettings["Terminal"].Trim().Trim() + GetTransactionNumber(); // "100008"; //Debe ser un consecutivo
            string returnMessage = "";
            string decryptTransValue = "";
            //EncryptClass.EncryptClass encryptClass = new EncryptClass.EncryptClass();
            //decryptTransValue = encryptClass.Encrypt(Transaction);

            //decryptTransValue = encryptClass.Decrypt(Transaction.Trim());
            string[] RequestTransaction = Transaction.Split('|'); // decryptTransValue.Split('|');

            EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];         

            if (Session["TaskControl"] != null)
            {                
                fields[0] = ddlPaymentAmount.Text.Trim(); //"25.00";// "0.01";// txtPhone.Text.Trim(); //RequestTransaction[0]; //"15.00";
                fields[1] = "";//taskControl.Customer.Address1.Trim(); //dt.Rows[0]["BusinessAddress1"].ToString().Trim();
                fields[2] = "";//taskControl.Customer.Address2.Trim(); //dt.Rows[0]["BusinessAddress2"].ToString().Trim();
                fields[3] = "";// taskControl.Customer.City.Trim();  //dt.Rows[0]["BusinessCity"].ToString().Trim();
                fields[4] = "";// taskControl.Customer.ZipCode.Trim();  // dt.Rows[0]["BusinessZipCode"].ToString().Trim();
                fields[8] = txtAccountNombre.Text.Trim();//txtAccountNombre.Text.Trim();// "VICTOR R. LANZA AMARAL"; //NameOnAccount
                fields[50] = taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim(); //dt.Rows[0]["BusinessName"].ToString().Trim();
                fields[51] = taskControl.Customer.CustomerNo.Trim();  //ddlPaymentAmount.Text.Trim();
                fields[52] = taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim(); //dt.Rows[0]["BusinessContact"].ToString().Trim();
                fields[53] = taskControl.Customer.Email.Trim(); //dt.Rows[0]["BusinessEmail"].ToString().Trim();
                fields[54] = taskControl.Customer.Cellular.Trim();  //dt.Rows[0]["BusinessPhone"].ToString().Trim();

               if (ddlMetodoPago.SelectedItem.Value.Trim() == "3" || ddlMetodoPago.SelectedItem.Value.Trim() == "4" || ddlMetodoPago.SelectedItem.Value.Trim() == "5")
                {
                    fields[5] = "True"; //Tarjeta de Credito
                    fields[9] = GetMonth(); //ddlMes.SelectedItem.Value.Trim();// "02"; //CardExpiresMonth
                    fields[10] = ddlYear.SelectedItem.Value.Trim(); //"2018"; //CardExpiresYear
                    fields[11] = txtSecurityCode.Text.Trim();  // "000"; //CVVNumber
                    fields[7] = txtAccountNumber.Text.Trim();  // "5347650005981017"; //Tarjeta de Credito - AccountNumber

                    //Visa=1, MasterCard=2, American_Express=3
                    switch (ddlMetodoPago.SelectedItem.Value.Trim())
                    {
                        case "3": //Visa
                            fields[6] = "1";
                            break;
                        case "4": //MC
                            fields[6] = "2";
                            break;

                        case "5": //Amex
                            fields[6] = "3";
                            break;
                    }
                }
                else
                {
                    if (ddlMetodoPago.SelectedItem.Value.Trim() == "1" || ddlMetodoPago.SelectedItem.Value.Trim() == "2") //Checking or Saving Account
                    {
                        fields[5] = "False"; //No es tarjeta de Credito

                        if (ddlMetodoPago.SelectedItem.Value.Trim() == "1")
                        {
                            fields[12] = "True"; //Checking
                            fields[13] = "False"; //Saving
                        }
                        else
                        {
                            fields[12] = "False"; //Checking
                            fields[13] = "True"; //Saving
                        }
                        fields[14] = ddlRoutingNumber.SelectedItem.Value.Trim(); // "021502011"; //RoutingNumber
                        fields[15] = txtAccountNumber.Text.Trim(); // "041799936"; //AccountNumber - Bank Account
                    }
                }
            }


            if (Password == "LS91A2QS-7522-4DWD-4219-A515469D569P")
            {
                try
                {
                    using (PV.PaymentVaultSoapClient paymentClient = new PV.PaymentVaultSoapClient())
                    {
                        if (paymentClient.TestConnection())
                        {
                            PV.WSUpdateResult result = paymentClient.TestCredentials(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), ConfigurationManager.AppSettings["locationId"].ToString(), "__WebService");

                            if (result.returnValue == PV.ReturnValue.Success)
                            {
                                PV.WSCustomer cust = new PV.WSCustomer();
                                cust.CustomerNumber = taskControl.Customer.CustomerNo.Trim();
                                cust.FirstName = taskControl.Customer.FirstName.Trim(); //fields[52];
                                cust.LastName = taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim();
                                cust.EntityId = Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]);
                                cust.Email = fields[53];
                                cust.Address1 = fields[1];
                                cust.Address2 = fields[2];
                                cust.City = fields[3];
                                cust.StateRegion = "";//"PR";
                                cust.PostalCode = fields[4];
                                cust.DaytimePhone = fields[54];
                                cust.IsCompany = false;
                                cust.Field1 = TaskControlID.ToString(); 

                                PV.WSUpdateResult responseCust = new PV.WSUpdateResult();
                                responseCust = paymentClient.RegisterCustomer(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), cust);

                                //Success or Exist Customer
                                if (responseCust.returnValue == PV.ReturnValue.Success || responseCust.returnValue == PV.ReturnValue.Error_UniqueConstraint)
                                {
                                    PV.WSAccount account = new PV.WSAccount();
                                    account.CustomerNumber = taskControl.Customer.CustomerNo.Trim();
       
                                    //Se busca si ya el cliente tiene esta cuenta registrada para no hacer la transaccion de cuenta nueva
                                    // PV.WSAccount[] responseRegAccount;
                                    //responseRegAccount = paymentClient.GetRegisteredAccounts(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), account.CustomerNumber);
                                    
                                    if (fields[5] == "True")
                                    {
                                        switch (fields[6])
                                        {
                                            case "1":
                                                account.AccountType = PV.WSAccountType.Visa;
                                                break;
                                            case "2":
                                                account.AccountType = PV.WSAccountType.MasterCard;
                                                break;
                                            case "3":
                                                account.AccountType = PV.WSAccountType.American_Express;
                                                break;
                                        }

                                        account.ExpirationMonth = Convert.ToByte(fields[9]);
                                        account.ExpirationYear = Convert.ToByte(fields[10].Substring(fields[10].ToString().Length - 2));
                                        account.NameOnAccount = fields[8];
                                        account.AccountNumber = fields[7];
                                        //account.CVVNumber = int.Parse(fields[11]);
                                    }

                                    if (fields[12] == "True" || fields[13] == "True")
                                    {
                                        if (fields[12] == "True")
                                            account.AccountType = PV.WSAccountType.Checking;
                                        else if (fields[13] == "True")
                                            account.AccountType = PV.WSAccountType.Savings;
                                        account.NameOnAccount = fields[8];
                                        account.RoutingNumber = int.Parse(fields[14]);
                                        account.AccountNumber = fields[15];
                                    }

                                    account.AccountName = fields[52];
                                    account.BillAddress1 = fields[1];
                                    account.BillAddress2 = fields[2];
                                    account.BillCity = fields[3];
                                    account.BillStateRegion = "";//"PR";
                                    account.BillPostalCode = fields[4];
                                    string AccountReference = GetAccountReference();
                                    account.AccountReferenceID = AccountReference;

                                    RequestInfo += "CustomerNo: " + account.CustomerNumber + " | ";
                                    RequestInfo += "NameOnAccount: " + account.NameOnAccount + " | ";
                                    RequestInfo += "AccountType: " + account.AccountType.ToString() + " | ";
                                    RequestInfo += "ExpirationMonth: " + account.ExpirationMonth.ToString() + " | ";
                                    RequestInfo += "ExpirationYear: " + account.ExpirationYear.ToString() + " | ";
                                    RequestInfo += "AccountNumber: " + account.AccountNumber + " | ";
                                    RequestInfo += "RoutingNumber: " + account.RoutingNumber.ToString() + " | ";

                                    RequestInfo += "AccountName: " + account.AccountName + " | ";
                                    RequestInfo += "BillAddress1: " + account.BillAddress1 + " | ";
                                    RequestInfo += "BillAddress2: " + account.BillAddress2 + " | ";
                                    RequestInfo += "BillCity: " + account.BillCity + " | ";
                                    RequestInfo += "BillStateRegion: " + account.BillStateRegion + " | ";
                                    RequestInfo += "BillPostalCode: " + account.BillPostalCode + " | ";
                                    RequestInfo += "AccountReferenceID: " + account.AccountReferenceID + " | ";


                                    PV.WSUpdateResult responseAccount = new PV.WSUpdateResult();
                                    responseAccount = paymentClient.RegisterAccount(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), account);

                                    if (responseAccount.returnValue == PV.ReturnValue.Success || responseAccount.returnValue == PV.ReturnValue.Error_UniqueConstraint)
                                    {
                                        string resultTranPay = "";
                                        if (fields[5] == "True") //Tarjeta de Credito
                                            resultTranPay = SaleFromCardAccount(paymentClient, AccountReference, transactionNumber);
                                        else
                                            resultTranPay = SaleFromBankAccount(paymentClient, AccountReference, transactionNumber);

                                        fields[75] = resultTranPay;
                                    }

                                    //Set Recurring Payment
                                    if ((responseAccount.returnValue == PV.ReturnValue.Success  || responseAccount.returnValue == PV.ReturnValue.Error_UniqueConstraint) &&  Payment1 != 0.0 &
                                        fields[75].Contains("Success"))
                                    {
                                        PV.WSRecurring transaction = new PV.WSRecurring();
                                        transaction.LocationID = Convert.ToInt32(ConfigurationManager.AppSettings["locationId"]);
                                        transaction.CustomerNumber = taskControl.Customer.CustomerNo.Trim();
                                        transaction.AccountReferenceID = AccountReference;
                                        transaction.Description = taskControl.PolicyType.Trim() + " " + taskControl.PolicyNo.Trim(); // "Guardian Xtra Def Pay";
                                        transaction.Amount = Convert.ToDecimal(Payment1.ToString()); //Convert.ToDecimal(fields[0]);  //36.00m;
                                        transaction.InvoiceNumber = transactionNumber;
                                        transaction.Frequency = PV.WSFrequency.Once_a_Month;
                                        transaction.PaymentDay = 1;
                                        transaction.StartDate = DateTime.Parse(Fecha1); // (DateTime.Parse(DateTime.Now.AddMonths(1).Month.ToString() + "/01/" + DateTime.Now.AddMonths(1).Year.ToString()));
                                        transaction.Field1 = TaskControlID.ToString();

                                        transaction.NumPayments = 0;
                                        if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsSixPayment"])
                                        {
                                            transaction.NumPayments = 5;
                                        }

                                        if ((bool)taskControl.GuardianXtraCollection.Rows[0]["IsFourPayment"])
                                        {
                                            transaction.NumPayments = 3;
                                        }

                                        transaction.PaymentsToDate = 0;
                                        transaction.NotificationMethod = PV.WSNotificationMethod.Email;
                                        transaction.NextPaymentDate = DateTime.Parse(Fecha2);
                                        transaction.Enabled = true;
                                        transaction.PaymentOrigin = PV.WSPaymentOrigin.Internet;

                                        string RecurringReference = GetRecurringReference();
                                        transaction.RecurringReferenceID = RecurringReference; //"100032";//PolicyNo";
                                        transaction.Field1 = transactionNumber;
                                        retVal[1] = transactionNumber;

                                        PV.WSUpdateResult response = new PV.WSUpdateResult();
                                        response = paymentClient.SetupRecurringPayment(Convert.ToInt64(ConfigurationManager.AppSettings["storeId"]), ConfigurationManager.AppSettings["storeKey"], Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]), transaction);
                                        
                                        if (response.returnValue == PV.ReturnValue.Success)
                                        {
                                            //fields[74] = response.message;
                                            //fields[75] = response.returnValue.ToString();
                                            //retVal[0] = "S";
                                            //status = "2";                                          
                                        }
                                        else
                                        {
                                            //retVal[0] = "F";
                                            //fields[74] = response.message;
                                            //fields[75] = response.returnValue.ToString();
                                            //status = "4";
                                            //SendTransactionErrorEmail(int.Parse(fields[73]));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //retVal[0] = "C";
                        }

                        return fields[75].Trim();
                    }
                }
                catch (Exception exc)
                {
                    LogError(exc);
                    returnMessage = "Transaction Fail.";
                    if (exc.Message.Trim().Contains("The message was"))
                    {
                        int index = exc.Message.Trim().IndexOf("The message was");
                        returnMessage = exc.Message.Trim().Substring(index + 16);

                        index = returnMessage.Trim().IndexOf(".");
                        returnMessage = returnMessage.Substring(0, index + 1);
                    }

                    return returnMessage;
                }
            }
            else
            {
                return "Wrong Password.";
            }
        }

        private string SaleFromCardAccount(PV.PaymentVaultSoap paymentClient, string AccountReference, string transactionNumber)
        {
            string result = "Fail";
            try
            {              
                if (Session["TaskControl"] != null)
                {
                    EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                    //PV.WSUpdateResult response = new PV.WSUpdateResult();
                    // using (PV.PaymentVaultSoapClient paymentClient2 = new PV.PaymentVaultSoapClient())
                    //{
                    PV.SaleFromCardAccountRequest transaction = new PV.SaleFromCardAccountRequest();
                    PV.SaleFromCardAccountRequestBody body = new PV.SaleFromCardAccountRequestBody();

                    body.storeId = Convert.ToInt32(ConfigurationManager.AppSettings["storeId"]);
                    body.storeKey = ConfigurationManager.AppSettings["storeKey"].ToString();
                    body.entityId = Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]);
                    body.locationId = Convert.ToInt32(ConfigurationManager.AppSettings["locationId"]);
                    body.accountReferenceId = AccountReference;
                    body.paymentOrigin = PV.WSPaymentOrigin.Internet;
                    body.Amount = Convert.ToDecimal(ddlPaymentAmount.Text.Trim());
                    body.terminalNumber = "__WebService";
                    body.TransactionNumber = GetTransactionNumber(); // transactionNumber;
                    body.Description = taskControl.PolicyType.Trim() + " " + taskControl.PolicyNo.Trim(); // "Guardian Xtra Pay";;

                    body.Field1 = "";//taskControl.Customer.Address1.Trim(); // dt.Rows[0]["BusinessAddress1"].ToString().Trim();
                    body.Field2 = "";//taskControl.Customer.Address2.Trim(); //dt.Rows[0]["BusinessAddress2"].ToString().Trim();
                    body.Field3 = "";//taskControl.Customer.City.Trim();

                    if (txtSecurityCode.Text.Trim() == "")
                        body.CCVS = 0;
                    else
                        body.CCVS = int.Parse(txtSecurityCode.Text.Trim());

                    body.ownerApplication = PV.WSOwnerApplication.Web_Service;

                    RequestInfo += "CCVS: " + body.CCVS.ToString() + " | ";
                    RequestInfo += "storeId: " + body.storeId.ToString() + " | ";
                    RequestInfo += "storeKey: " + body.storeKey + " | ";
                    RequestInfo += "entityId: " + body.entityId.ToString() + " | ";
                    RequestInfo += "locationId: " + body.locationId.ToString() + " | ";
                    RequestInfo += "accountReferenceId: " + body.accountReferenceId + " | ";
                    RequestInfo += "paymentOrigin: " + body.paymentOrigin.ToString() + " | ";
                    RequestInfo += "Amount: " + body.Amount.ToString() + " | ";

                    RequestInfo += "terminalNumber: " + body.terminalNumber + " | ";
                    RequestInfo += "TransactionNumber: " + body.TransactionNumber + " | ";
                    RequestInfo += "Description: " + body.Description + " | ";
                    RequestInfo += "ownerApplication: " + body.ownerApplication.ToString() + " | ";

                    transaction.Body = body;
                    PV.SaleFromCardAccountResponse response = new PV.SaleFromCardAccountResponse();
                    response = paymentClient.SaleFromCardAccount(transaction);
                    if (response.Body.SaleFromCardAccountResult.Success == true)
                    {
                        result = response.Body.SaleFromCardAccountResult.ResponseCode + "|" + response.Body.SaleFromCardAccountResult.ReferenceNumber;
                        RequestResponse = result;
                    }
                    else
                    {
                        result = response.Body.SaleFromCardAccountResult.ResponseCode + "|" + response.Body.SaleFromCardAccountResult.ReferenceNumber;
                        RequestResponse = result;
                    }
                }

                return result;
            }
            catch (Exception exc)
            {
                LogError(exc);
                return exc.Message;
            }
        }

        private string SaleFromBankAccount(PV.PaymentVaultSoap paymentClient, string AccountReference, string transactionNumber)
        {
             string result = "Fail";
             try
             {
                 if (Session["TaskControl"] != null)
                 {
                     EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                     //PV.WSUpdateResult response = new PV.WSUpdateResult();
                     // using (PV.PaymentVaultSoapClient paymentClient2 = new PV.PaymentVaultSoapClient())
                     //{
                     PV.SaleFromBankAccountRequest transaction = new PV.SaleFromBankAccountRequest();
                     PV.SaleFromBankAccountRequestBody body = new PV.SaleFromBankAccountRequestBody();

                     body.storeId = Convert.ToInt32(ConfigurationManager.AppSettings["storeId"]);
                     body.storeKey = ConfigurationManager.AppSettings["storeKey"].ToString();
                     body.entityId = Convert.ToInt32(ConfigurationManager.AppSettings["entityId"]);
                     body.locationId = Convert.ToInt32(ConfigurationManager.AppSettings["locationId"]);
                     body.accountReferenceId = AccountReference;
                     body.paymentOrigin = PV.WSPaymentOrigin.Internet;
                     body.notificationMethod = PV.WSNotificationMethod.Email;
                     body.Amount = Convert.ToDecimal(ddlPaymentAmount.Text.Trim());
                     body.terminalNumber = "__WebService";
                     body.TransactionNumber = GetTransactionNumber(); // transactionNumber;
                     body.Description = taskControl.PolicyType.Trim() + " " + taskControl.PolicyNo.Trim(); // "Guardian Xtra Pay";;

                     body.Field1 = "";//taskControl.Customer.Address1.Trim(); // dt.Rows[0]["BusinessAddress1"].ToString().Trim();
                     body.Field2 = "";//taskControl.Customer.Address2.Trim(); //dt.Rows[0]["BusinessAddress2"].ToString().Trim();
                     body.Field3 = "";//taskControl.Customer.City.Trim();                     
                     body.ownerApplication = PV.WSOwnerApplication.Web_Service;
                     transaction.Body = body;
                     PV.SaleFromBankAccountResponse response = new PV.SaleFromBankAccountResponse();
                     response = paymentClient.SaleFromBankAccount(transaction);
                     if (response.Body.SaleFromBankAccountResult.Success == true)
                     {
                         result = response.Body.SaleFromBankAccountResult.ResponseCode + "|" + response.Body.SaleFromBankAccountResult.ReferenceNumber;

                     }
                     else
                     {
                         result = response.Body.SaleFromBankAccountResult.ResponseCode + "|" + response.Body.SaleFromBankAccountResult.ReferenceNumber;
                     }
                 }

                 return result;
             }
             catch (Exception exc)
             {
                 return exc.Message;
             }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["FromUI"] != null)
            {
                string fromui = (string)Session["FromUI"];
                Session.Remove("FromUI");
                Response.Redirect(fromui, true);
            }
            else
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
                Session.Remove("FromUI");
                Response.Redirect("Default.aspx?007");
            }
        }
        private void LogError(Exception exp)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", exp.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", exp.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", exp.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", exp.TargetSite.ToString());
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

        //MailMessage mailWithImg = getMailWithImg();
        //MySMTPClient.Send(mailWithImg); //* Set up your SMTPClient before!

        //private MailMessage getMailWithImg() {
        //    MailMessage mail = new MailMessage();
        //    mail.IsBodyHtml = true;
        //    mail.AlternateViews.Add(getEmbeddedImage("c:/image.png"));
        //    mail.From = new MailAddress("yourAddress@yourDomain");
        //    mail.To.Add("recipient@hisDomain");
        //    mail.Subject = "yourSubject";
        //    return mail;
        //}
        //private AlternateView getEmbeddedImage(String filePath) {
        //    LinkedResource res = new LinkedResource(filePath);
        //    res.ContentId = Guid.NewGuid().ToString();
        //    string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
        //    AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
        //    alternateView.LinkedResources.Add(res);
        //    return alternateView;
        //}
    }
}