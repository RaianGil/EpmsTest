using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace OPTIMAIns
{
    public class CreatePDFBatch
    {
        public CreatePDFBatch()
        {

        }

        public string AppendTextToPDF(string[] contractNum, string address1, string address2, string address3, string ReportPath, string ProviderName)
        {
            try
            {
                int lastDigit = 0;
                string fileMask = "*.pdf";
                string contractPath = System.Configuration.ConfigurationSettings.AppSettings["contractPath"];
                string ProcessedPath = System.Configuration.ConfigurationSettings.AppSettings["ProcessedPath"];
                List<string> mergePaths = new List<string>();
                //seach in a path for *.pdf

                fileMask = string.IsNullOrEmpty(fileMask) ? "*.*" : fileMask;
                string[] files;// = Directory.GetFiles(contractPath, fileMask);

                //Add the report path to the list to be merged in the same batch
                mergePaths.Add(ReportPath);

                foreach (string contract in contractNum)
                {
                    fileMask = contract + "*.pdf";
                    files = Directory.GetFiles(contractPath, fileMask);

                    string NewFilePath = Path.Combine(contractPath, contract + "_new.pdf");

                    foreach (string file in files)
                    {
                        string output = new string(file.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
                        if (contract == output)
                        {
                            // open the reader
                            PdfReader reader = new PdfReader(file);
                            FileStream fs = new FileStream(NewFilePath, FileMode.Append, FileAccess.Write);

                            PdfStamper stamper = null;
                            stamper = new PdfStamper(reader, fs);
                            //using (PdfStamper stamper = new PdfStamper(reader, fs))
                            //{
                            PdfContentByte cb = stamper.GetUnderContent(1);

                            //Add Barcode
                            if (contract.Length > 0)
                            {
                                Barcode128 barcode = new Barcode128();
                                barcode.CodeType = Barcode.CODE128;
                                barcode.ChecksumText = true;
                                barcode.GenerateChecksum = true;
                                barcode.StartStopText = true;
                                barcode.Code = contract;

                                iTextSharp.text.Image barImg = barcode.CreateImageWithBarcode(cb, null, null);
                                barImg.SetAbsolutePosition(450, 800);
                                cb.AddImage(barImg);
                            }

                            //Set the font to the text
                            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, false), 13f);

                            //Split the contractNum# to add in the contractNum boxes
                            var NPI = System.Configuration.ConfigurationSettings.AppSettings["NPI"].Select(digit => digit.ToString());
                            // var intList = NPI.Select(digit => digit.ToString());
                            string[] digitarray = NPI.ToArray();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[0].ToString(), 83, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[1].ToString(), 106, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[2].ToString(), 129, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[3].ToString(), 152, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[4].ToString(), 174, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[5].ToString(), 196, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[6].ToString(), 218, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[7].ToString(), 241, 574, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, digitarray[8].ToString(), 263, 574, 0);
                            cb.EndText();

                            bool res = int.TryParse(digitarray[9].ToString(), out lastDigit);

                            if (res == true)
                            {
                                cb.BeginText();
                                cb.ShowTextAligned(1, digitarray[9].ToString(), 286, 574, 0);
                                cb.EndText();
                            }

                            //Add the Billing Address
                            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, false), 8f);
                            cb.BeginText();
                            cb.ShowTextAligned(1, address1, 380, 582, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, address2, 380, 575, 0);
                            cb.EndText();

                            cb.BeginText();
                            cb.ShowTextAligned(1, address3, 380, 567, 0);
                            cb.EndText();

                            stamper.FormFlattening = true;
                            stamper.Writer.CloseStream = false;

                            //Close the stamper and the reader
                            stamper.Close();
                            reader.Close();
                            fs.Close();

                            //}// stamper
                            //Add the file path to be merge
                            mergePaths.Add(NewFilePath);
                            break;
                        }
                    }//for each file

                }// for each contract
                // Call the merge function               
                string mergePath = MergePDFFiles(mergePaths, ProcessedPath, ProviderName);
                // result = true;
                return mergePath;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error", ex.Message.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public string MergePDFFiles(List<string> mergePaths, string ProcessedPath, string CustomerName)
        {
            Document document = new Document(PageSize.LETTER);
            string onlyFileName = CustomerName + "_" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + "_" +
                DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + +DateTime.Now.Second + ".pdf";

            string destinationBatchfile = Path.Combine(ProcessedPath, onlyFileName);
            bool result = false;
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationBatchfile, FileMode.Create));
                document.Open();

                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int n = 0;
                int rotation = 0;

                foreach (string filename in mergePaths)
                {
                    PdfReader reader = new PdfReader(filename);

                    n = reader.NumberOfPages;
                    int i = 0;

                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(1));
                        document.NewPage();
                        //Insert to Destination on the first page
                        if (i == 1)
                        {
                            Chunk fileRef = new Chunk(" ");
                            fileRef.SetLocalDestination(filename);
                            document.Add(fileRef);
                        }

                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                       
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                    //document.SetPageSize(PageSize.LETTER);
                }
                //close the batch file
                document.Close();

                foreach (var path in mergePaths)
                { File.Delete(path); }
                // result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return onlyFileName;
        }
    }
}
