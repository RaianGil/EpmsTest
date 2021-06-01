namespace EPolicy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;
    using EPolicy.XmlCooker;
    using System.Xml;

    public partial class AddDocuments : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            if (customer != null)
            {
                FillGridDocuments();
            }
            //mpeAdjunto.Show();
        }

        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];

            if (customer.CustomerNo == "0")
            {
                //lblRecHeader.Text = "You must save customer in order to proceed.";
                //mpeSeleccion.Show();
            }
            else
            {
                txtDocumentDesc2.Text = "";
                //mpeAdjunto.Show();
            }
        }

        protected void btnAdjuntarCargar2_Click(object sender, EventArgs e)
        {
            try
            {

                Customer.Customer customer = (Customer.Customer)Session["Customer"];

                if (txtDocumentDesc2.Text.Trim() == "")
                    throw new Exception("Please Fill the description Field.");

                if (this.FileUpload2.PostedFile != null)
                {
                    if (FileUpload2.PostedFile.FileName == "")
                    {
                        throw new Exception("Please select a file from the browser.");
                    }
                }
                else
                {
                    throw new Exception("Please select a file from the browser.");
                }

                if (this.FileUpload2.PostedFile.FileName != "")
                {
                    if (this.FileUpload2.PostedFile != null)
                    {
                        string File = FileUpload2.PostedFile.FileName.Substring(FileUpload2.PostedFile.FileName.LastIndexOf('.'));

                        switch (File)
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

                                if (this.FileUpload2.PostedFile.FileName.Split(".".ToCharArray())[1].ToString().ToLower() != "pdf")
                                {
                                    throw new Exception("The File Format is not supported.");
                                }
                                break;
                        }

                        if (this.FileUpload2.PostedFile.ContentLength > 4000001)
                        {
                            throw new Exception("The file size must be up to 4MB.");
                        }
                    }
                }

                //SaveDocuments
                int docid = Savedocuments();

                //Upload Document
                if (FileUpload2.PostedFile.FileName != null)
                {
                    string fileName = FileUpload2.PostedFile.FileName.Substring(FileUpload2.PostedFile.FileName.LastIndexOf('.'));


                    switch (fileName)
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

                    FileUpload2.PostedFile.SaveAs(fileName);

                    FillGridDocuments();
                    txtDocumentDesc2.Text = "";
                    Session["AddDocumentOpen"] = true;
                    //mpeAdjunto.Show();
                }
            }
            catch (Exception exp)
            {
                //lblRecHeader.Text = exp.Message;
                //mpeSeleccion.Show();
                Session["AddDocumentMessage"] = exp;
            }
        }

        private void FillGridDocuments()
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];

            gvAdjuntar2.DataSource = null;
            DataTable DtCert = null;
            DataTable dtOldClaimID = null;

            //if (claim.Cover == "REOPEN" || claim.Cover == "REOPEN2")
            //{
            //    //dtOldClaimID = GetOldClaimIDForReOpenClaims(claim.ClaimNumber);
            //    //DtCert = GetDocumentsByCustomerNo(claim.ClaimID, int.Parse(dtOldClaimID.Rows[0]["ClaimID"].ToString()));
            //}
            //else
            DtCert = GetDocumentsByCustomerNo(customer.CustomerNo, 0);

            //DtCert = GetDocumentsByClaimID(claim.ClaimID);

            if (DtCert != null)
            {
                if (DtCert.Rows.Count != 0)
                {
                    gvAdjuntar2.DataSource = DtCert;
                    gvAdjuntar2.DataBind();
                }
                else
                {
                    gvAdjuntar2.DataSource = null;
                    gvAdjuntar2.DataBind();
                }
            }
            else
            {
                gvAdjuntar2.DataSource = null;
                gvAdjuntar2.DataBind();
            }
        }
        protected void gvAdjuntar2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            int documentID = 0;
            try
            {
                if (e.CommandName.Trim() == "View")
                {
                    int index = Int32.Parse(e.CommandArgument.ToString());
                    GridViewRow row = gvAdjuntar2.Rows[index];
                    TableCell cell = row.Cells[1]; //ID is displayed in 2nd column  
                    int i = int.Parse(cell.Text);

                    documentID = i;

                    string fileName = System.Configuration.ConfigurationManager.AppSettings["RootURL"].ToString().Trim();

                    fileName = fileName + "Documents\\";

                    string[] fileNames = System.IO.Directory.GetFiles(fileName, @"*" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "*");

                    fileName = fileNames[0].ToString();

                    fileName = fileName.Substring(fileName.LastIndexOf('.'));

                    string fileType = fileName.Substring(fileName.LastIndexOf('.') + 1).ToUpper();

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('Documents/" + documentID.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "" + fileName + "','" + fileType + "','status=yes,menubar,scrollbars=yes,resizable=yes,copyhistory=no,width=1150,height=725');", true);
                }
            }
            catch (Exception exp)
            {
                //mpeSeleccion.Show();
                //lblRecHeader.Text = exp.Message;
                return;
            }

            //mpeAdjunto.Show();
        }

        protected void gvAdjuntar2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;

            }
            catch (Exception exc)
            {

            }
        }

        protected void gvAdjuntar2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                int index = e.RowIndex;
                GridViewRow row = gvAdjuntar2.Rows[index];
                TableCell cell = row.Cells[1]; // ID is displayed in 2nd column  
                int i = int.Parse(cell.Text);

                //Se elimna de la tabla
                DeleteDocumentsByDocumentsID(i);

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
                //mpeAdjunto.Show();
            }
            catch (Exception exp)
            {
                //lblRecHeader.Text = exp.Message;
                //mpeSeleccion.Show();
            }
        }

        private int Savedocuments()
        {
            try
            {
                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
                int id = 0;
                try
                {
                    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                    DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, customer.CustomerNo.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("Description", SqlDbType.VarChar, 200, txtDocumentDesc2.Text.Trim(), ref cookItems);
                    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                    Executor.BeginTrans();
                    id = Executor.Insert("AddDocuments", xmlDoc);
                    Executor.CommitTrans();
                }
                catch (Exception xcp)
                {
                    Executor.RollBackTrans();
                    throw new Exception("Error Could not Save the Document, Try again.");
                }

                txtDocumentDesc2.Text = "";

                return id;
            }
            catch (Exception exp)
            {
                //mpeSeleccion.Show();
                //lblRecHeader.Text = exp.Message;
                return 0;
            }
        }

        private DataTable DeleteDocumentsByDocumentsID(int DocID)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("DocumentsID", SqlDbType.Int, 0, DocID.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("DeleteDocumentsByDocumentsID", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not delete the information, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable GetDocumentsByCustomerNo(string CustomerNo, int oldClaimID)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, CustomerNo.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("OldClaimID", SqlDbType.Int, 0, oldClaimID.ToString(), ref cookItems);

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
                    dt = exec.GetQuery("GetDocumentsByCustomerNo", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}