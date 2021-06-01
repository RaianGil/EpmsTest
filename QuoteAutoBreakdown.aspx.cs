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
using EPolicy.TaskControl;
using EPolicy.Quotes;
using System.Web.Security;

namespace EPolicy
{
	/// <summary>
	/// Summary description for QuoteAutoBreakdown.
	/// </summary>
	public partial class QuoteAutoBreakdown : System.Web.UI.Page
	{
		
		// local variables
		private int InternalAutoID
		{
			get
			{
				try
				{
					if (Session["InternalAutoID"] != null)
						return int.Parse(Session["InternalAutoID"].ToString());
					else 
						return 0;
				}
				catch {return 0;}
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.litPopUp.Visible = false;

			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
				Response.Redirect("Default.aspx?001");
			}
			else
			{
                if (!cp.IsInRole("AUTO PERSONAL BREAKDOWN") && !cp.IsInRole("ADMINISTRATOR"))
				{
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
					Response.Redirect("Default.aspx?001");
				}
			}

			this.BtnPrint.Attributes.Add("onclick","PrintPage();");

			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
			
			if(!IsPostBack)
			{
				this.SetReferringPage();

				this.btnDrivers.Visible = false;
			
				if(InternalAutoID > 0) // Comes from QuoteAutoVehicle
				{
					SetMainDataGrid();
					DisableControls();
				}
				else
				{
					Response.Redirect("QuoteAutoVehicles.aspx");
				}
			}
		}
		
		private void SetMainDataGrid()
		{
			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto) Session["TaskControl"];
			// Get ID of Auto 
			AutoCover AC = new AutoCover();

            for (int i = 0; i < QA.AutoCovers.Count; i++)
            {
                AC = (AutoCover)QA.AutoCovers[i];
                if (AC.InternalID == InternalAutoID)
                {
                    i = QA.AutoCovers.Count;
                }
            }

			//AC.InternalID = InternalAutoID;
			//AC = QA.GetAutoCover(AC);
			// Show DataGrid
			SetDataGrid(AC.Breakdown);
			FillDataGrid(AC.Breakdown, AC.QuotesAutoId);
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			//  Benny:	this.InitializeDriverObjectSessionVariable();

			Control Banner = new Control();
			Banner = LoadControl(@"TopBannerNew.ascx");
			this.Placeholder1.Controls.Add(Banner);

			
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgBreakdown.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBreakdown_ItemCommand);

		}
		#endregion

		private void FillDataGrid(ArrayList AL, int quoteAutoID)
		{
			decimal[] subttl = new decimal[10]; //TODO: Change this to Dynamic!!!
			if (AL != null && AL.Count > 0)
			{
				DataTable DT = getDisplayDataTable(AL.Count);
				DataRow row;
				
				// Display ISOCode
				CoverBreakdown srch = new CoverBreakdown();
				srch.Type = (int)Enumerators.Premiums.IsoCode;
				int ii = AL.IndexOf(srch);

				if (ii >= 0)
				{
					CoverBreakdown ISOC = (CoverBreakdown)AL[ii];
					row = DT.NewRow();
					
					row["QuoteAutoID"] = quoteAutoID.ToString();
					row["BCID"]        = srch.Type.ToString();

					row["Premium"] = Enumerators.DecodePremiums(srch.Type);
					for(int k = 0; k < ISOC.Breakdown.Count; k++)
					{
						string txtISO = ISOC.Breakdown[k].ToString();
						if(txtISO != null && txtISO!= "")
						{
							row["Period" + k] = txtISO;
						}
					}
					row["Total"] = "N/A";
					DT.Rows.Add(row);
				}

				// Display Depreciation

				srch = new CoverBreakdown();
				srch.Type = (int)Enumerators.Premiums.Depreciation;
				int icb = AL.IndexOf(srch);
				if (icb >= 0)
				{
					CoverBreakdown Depr = (CoverBreakdown)AL[icb];
					row = DT.NewRow();

					row["QuoteAutoID"] = quoteAutoID.ToString();
					row["BCID"]        = srch.Type.ToString();

					row["Premium"] = "Actual Value"; //Enumerators.DecodePremiums(srch.Type);
					for(int k = 0; k < Depr.Breakdown.Count; k++)
					{
						string txtDepr = Depr.Breakdown[k].ToString();
						if(txtDepr != null && txtDepr!= "")
						{
							row["Period" + k] = 
								String.Format("{0:c}", 
								Math.Round(Decimal.Parse(txtDepr), 0));
						}
					}
					row["Total"] = "N/A";
					DT.Rows.Add(row);
				}
				
				// Add all values in one shot
				for (int i = 0; i < AL.Count; i++)
				{					
					CoverBreakdown CB = (CoverBreakdown)AL[i];
					if (CB.Type != (int)Enumerators.Premiums.IsoCode &&
						CB.Type != (int)Enumerators.Premiums.Periods &&
						CB.Type != (int)Enumerators.Premiums.Depreciation &&
						CB.Type != (int)Enumerators.Premiums.AnnualPremium &&
						CB.Type != (int)Enumerators.Premiums.ActualValue
						)
					{
						row = DT.NewRow();

						row["QuoteAutoID"] = quoteAutoID.ToString();
						row["BCID"]        = CB.Type.ToString();

						row["Premium"] = Enumerators.DecodePremiums(CB.Type);
						decimal ttl = 0.0m;
						
						for(int j = 0; j < CB.Breakdown.Count; j++)
						{
							//string str = CB.Breakdown[j].ToString();
							
							//RPR 2004-04-05
							decimal str = 0.0m;

							if(CB.Type == (int)Enumerators.Premiums.LeaseLoanGap)
							{
                                str = Math.Round(decimal.Parse(CB.Breakdown[j].ToString()), 0);
							}
							else
							{
								str = Math.Round(decimal.Parse(CB.Breakdown[j].ToString()), 0);
							}

							if(true) //str != null && str!= "")
							{
								row["Period" + j] = String.Format("{0:c}",
									str);
								ttl += str;  //TODO: fix this;
								subttl[j] += str;
							}
						}
						row["Total"] = String.Format("{0:c}", 
							Math.Round(ttl, 0));
						
						for(int k = 0; k < row.ItemArray.Length; k++)
						{
							if(row[k].ToString() == string.Empty)
								row[k] = String.Format("{0:c}", 0);
						}

						DT.Rows.Add(row);
					}
				}
				
				// Add Totals for each Period
				decimal GrandTotal = 0;
				row = DT.NewRow();

				row["QuoteAutoID"] = quoteAutoID.ToString();
				row["BCID"]        = "0";

				row["Premium"] = "Total";
				for(int m = 0; m < subttl.Length; m++)
				{
					decimal dbl = subttl[m];
					if(dbl > 0)
					{
						row["Period" + m] = String.Format("{0:c}", 
							Math.Round(dbl, 0));
						GrandTotal += dbl;
					}
				}
				row["Total"] = 
					String.Format("{0:c}", Math.Round(GrandTotal, 0));
				DT.Rows.Add(row);

				dgBreakdown.DataSource = DT;
				dgBreakdown.DataBind();
			}
		}
		private DataTable getDisplayDataTable(int cnt)
		{
			DataSet ds = new DataSet("DSBreakdown");
			DataTable dt = ds.Tables.Add("Breakdown");
			// 0    Premium
			// 1..  Periods
			// x    Total
			dt.Columns.Add("QuoteAutoID", typeof(string));
			dt.Columns.Add("BCID", typeof(string));
			dt.Columns.Add("Premium", typeof(string));
			for (int i=0; i<=cnt; i++)
			{
				dt.Columns.Add("Period" + i, typeof(string));
			}
			dt.Columns.Add("Total", typeof(string));
			return dt;
		}
		
		private void SetDataGrid(ArrayList AL)
		{
			if(AL.Count > 0) 
			{
				CoverBreakdown CB = new CoverBreakdown();
				CB.Type = (int)Enumerators.Premiums.Periods;
				CB = (CoverBreakdown)AL[AL.IndexOf(CB)];
				SortedList SL = CB.Breakdown;
				// Set DataGrid properties
				dgBreakdown.AutoGenerateColumns = false;

				//			// Get a DataSet object filled with data
				//			DataSet ds = DB.GetDataSet();

				// Create ID column & add to DataGrid
				BoundColumn col = new BoundColumn();
				col.HeaderText="Concept";
				col.DataField="Premium";
				col.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
				dgBreakdown.Columns.Add(col);

				for (int i = 0; i < SL.Count; i++)
				{
					// Create Name column & add to DataGrid
					col = new BoundColumn();
					col.HeaderText="Period " + SL[i].ToString(); 
					col.DataField="Period" + i;
					col.ItemStyle.HorizontalAlign =	System.Web.UI.WebControls.HorizontalAlign.Right;
					dgBreakdown.Columns.Add(col);
				}

				// Create Address column & add to DataGrid
				col = new BoundColumn();
				col.HeaderText="Total"; 
				col.DataField="Total";
				col.ItemStyle.HorizontalAlign =	System.Web.UI.WebControls.HorizontalAlign.Right;
				dgBreakdown.Columns.Add(col); 
				
				this.dgBreakdown.AlternatingItemStyle.BackColor = System.Drawing.Color.FromArgb(254, 251, 246);
				this.dgBreakdown.GridLines = System.Web.UI.WebControls.GridLines.Both;
				this.dgBreakdown.BorderColor = System.Drawing.Color.FromArgb(214, 227, 234);				
			}
		}

		private string ReferringPage
		{
			get
			{
				return ((ViewState["referringPage"] != null)?
					ViewState["referringPage"].ToString():"");
			}
			set
			{
				ViewState["referringPage"] = value;
			}
		}

		private void SetReferringPage()
		{
			if((Session["FromPage"] != null) && (Session["FromPage"].ToString() != ""))
			{
				this.ReferringPage = Session["FromPage"].ToString();
			}
		}

		private void ReturnToReferringPage()
		{
			if(this.ReferringPage != "")
			{
				Session.Remove("FromPage");
				Response.Redirect(this.ReferringPage);
			}
			Response.Redirect("HomePage.aspx");
		}

		private void Imagebutton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string js = "<script language=javascript> window.print(); </script>";
			Response.Write(js);
		}

		private void DisableControls()
		{
			LblPeriod1.Visible = false;
			LblPeriod2.Visible = false;
			LblPeriod3.Visible = false;
			LblPeriod4.Visible = false;
			LblPeriod5.Visible = false;
			LblPeriod6.Visible = false;
			LblPeriod7.Visible = false;
			LblCover.Visible   = false;

			TxtPeriod1.Visible = false;
			TxtPeriod2.Visible = false;
			TxtPeriod3.Visible = false;
			TxtPeriod4.Visible = false;
			TxtPeriod5.Visible = false;
			TxtPeriod6.Visible = false;
			TxtPeriod7.Visible = false;

			TxtPeriod1.Text = "";
			TxtPeriod2.Text = "";
			TxtPeriod3.Text = "";
			TxtPeriod4.Text = "";
			TxtPeriod5.Text = "";
			TxtPeriod6.Text = "";
			TxtPeriod7.Text = "";

			TxtPeriod2.Visible = false;
			TxtPeriod3.Visible = false;
			TxtPeriod4.Visible = false;
			TxtPeriod5.Visible = false;
			TxtPeriod6.Visible = false;
			TxtPeriod7.Visible = false;

			this.BtnUpdate.Visible = false;
		}

		private void EnableControls()
		{
			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

			AutoCover AC = new AutoCover();
			AC.InternalID = InternalAutoID;
			AC = QA.GetAutoCover(AC);
			ArrayList AL = AC.Breakdown;

			CoverBreakdown CB = new CoverBreakdown();
			CB.Type = (int)Enumerators.Premiums.Periods;
			CB = (CoverBreakdown)AL[AL.IndexOf(CB)];
			SortedList SL = CB.Breakdown;

			for (int i = 1; i <= SL.Count; i++)
			{				
				string txt = "TxtPeriod"+i.ToString();
				TextBoxEnabled(txt);
			}

			this.BtnUpdate.Visible = true;
			LblCover.Visible       = true;
		}

		private void TextBoxEnabled(string txt)
		{
			switch(txt)
			{
				case "TxtPeriod1":
					TxtPeriod1.Visible = true;
					LblPeriod1.Visible = true;
					break;
				case "TxtPeriod2":
					TxtPeriod2.Visible = true;
					LblPeriod2.Visible = true;
					break;
				case "TxtPeriod3":
					TxtPeriod3.Visible = true;
					LblPeriod3.Visible = true;
					break;
				case "TxtPeriod4":
					TxtPeriod4.Visible = true;
					LblPeriod4.Visible = true;
					break;
				case "TxtPeriod5":
					TxtPeriod5.Visible = true;
					LblPeriod5.Visible = true;
					break;
				case "TxtPeriod6":
					TxtPeriod6.Visible = true;
					LblPeriod6.Visible = true;
					break;
				case "TxtPeriod7":
					TxtPeriod7.Visible = true;
					LblPeriod7.Visible = true;
					break;
				default:
					break;
			}
		}

		private string TextBoxValue(string txt)
		{
			switch(txt)
			{
				case "1":
					return TxtPeriod1.Text.Trim();			
				case "2":
					return TxtPeriod2.Text.Trim();
				case "3":
					return TxtPeriod3.Text.Trim();
				case "4":
					return TxtPeriod4.Text.Trim();
				case "5":
					return TxtPeriod5.Text.Trim();
				case "6":
					return TxtPeriod6.Text.Trim();
				case "7":
					return TxtPeriod7.Text.Trim();
				default:
					return "";
			}
		}

		private void dgBreakdown_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemType.ToString() != "Pager") // Select
			{							
				if (int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.IsoCode &&
					int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.Periods &&
					int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.Depreciation &&
					int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.AnnualPremium &&
					int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.ActualValue &&
					int.Parse(e.Item.Cells[1].Text)  != 0)
				{
					LblCover.Text = "Cover: "+LookupTables.LookupTables.GetDescription("BreakdownCoversType", e.Item.Cells[1].Text.Trim());
					
					EnableControls();

					this.InpQuotesAutoID.Value = e.Item.Cells[0].Text.Trim();
					this.InpBCID.Value		   = e.Item.Cells[1].Text.Trim();
				}
				else
				{
					DisableControls();
				}
				SetMainDataGrid();
			}		
		}

		private void BtnUpdate_Click(object sender, System.EventArgs e)
		{
			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

			AutoCover AC = new AutoCover();
			AC.InternalID = InternalAutoID;
			AC = QA.GetAutoCover(AC);
			ArrayList AL = AC.Breakdown;

			CoverBreakdown CB = new CoverBreakdown();
			CB.Type = (int)Enumerators.Premiums.Periods;
			CB = (CoverBreakdown)AL[AL.IndexOf(CB)];
			SortedList SL = CB.Breakdown;

			AutoCover AC2 = null;
			AC2 = AC;
			
			try
			{
				this.ValidateOvvrPremium(SL.Count);

				decimal coverTotal= this.SetTotalByCover(SL.Count);
				decimal dif = coverTotal - AC2.ComprehensivePremium();
				decimal newPrem = QA.TotalPremium + dif;

				for (int i = 1; i <= SL.Count; i++)
				{				
					//Actualiza cada Aniversario por cubierta.
					QA.OverridePremium(int.Parse(this.InpQuotesAutoID.Value),int.Parse(this.InpBCID.Value),i,this.TextBoxValue(i.ToString().Trim()),coverTotal,newPrem);			
				}

				AC2.OvrrPremium = true;
			
				// Reload Breakdowns.
				AC2.Breakdown = null;
				AC2 = QA.LoadBreakdownAfterOvrridePremium(AC2);
			
				QA.RemoveAutoCover(AC2);
				QA.AddAutoCover(AC2, true);			

				Session["TaskControl"] = QA;

				DisableControls();
				SetMainDataGrid();

				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Override Premium saved successfully.");
				this.litPopUp.Visible = true;
			}
			catch(Exception xcp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString(xcp.Message.Trim());
				this.litPopUp.Visible = true;
			}
		}

		public decimal SetTotalByCover(int period)
		{
			decimal totamt = 0;
			for (int i = 1; i <= period; i++)
			{				
				//Totaliza la cantidad por cubierta.
				totamt += decimal.Parse(this.TextBoxValue(i.ToString().Trim()));			
			}			
			return totamt;
		}

		public void ValidateOvvrPremium(int index)
		{
			string textValue = "";

			for (int i = 1; i <= index; i++)
			{	
				textValue = this.TextBoxValue(i.ToString().Trim());

				for(int a = 0; a <= textValue.Length-1; a++)
				{
					if(!Char.IsNumber(textValue,a))
					{
						if(textValue.Substring(a,1) != ".")
						{
							throw new Exception("Error. Please verify the value in the Period "+i.ToString().Trim());
						}
					}
				}

				if(textValue.Trim()=="")
				{
					switch(i)
					{
						case 1:
							TxtPeriod1.Text = "0";	
							break;
						case 2:
							TxtPeriod2.Text = "0";	
							break;
						case 3:
							TxtPeriod3.Text = "0";	
							break;
						case 4:
							TxtPeriod4.Text = "0";
							break;
						case 5:
							TxtPeriod5.Text = "0";	
							break;
						case 6:
							TxtPeriod6.Text = "0";
							break;
						case 7:
							TxtPeriod7.Text = "0";	
							break;
					}
				}
			}
		}

		protected void btnDrivers_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("QuoteAutoDrivers.aspx");
		}

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Report");
			ReturnToReferringPage();
		}

		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
		
		}
        protected void dgBreakdown_ItemCommand1(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Pager") // Select
            {
                if (int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.IsoCode &&
                    int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.Periods &&
                    int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.Depreciation &&
                    int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.AnnualPremium &&
                    int.Parse(e.Item.Cells[1].Text) != (int)Enumerators.Premiums.ActualValue &&
                    int.Parse(e.Item.Cells[1].Text) != 0)
                {
                    LblCover.Text = "Cover: " + LookupTables.LookupTables.GetDescription("BreakdownCoversType", e.Item.Cells[1].Text.Trim());

                    EnableControls();

                    this.InpQuotesAutoID.Value = e.Item.Cells[0].Text.Trim();
                    this.InpBCID.Value = e.Item.Cells[1].Text.Trim();
                }
                else
                {
                    DisableControls();
                }
                SetMainDataGrid();
            }	
        }
}
}
