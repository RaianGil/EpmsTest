using System;
using System.Xml;
using System.Data;
using Baldrich.DBRequest;
using System.Data.SqlClient;
using EPolicy.XmlCooker;
using EPolicy.Audit;



namespace EPolicy.LookupTables
{
	/// <summary>
	/// Summary description for CompanyDealer.
	/// </summary>
	public class CompanyDealer
	{
		public CompanyDealer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Enumerations

        public enum Mode { ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4, UPDATEPROFITPREMIER = 5, ADDPROFITPREMIER = 6, UPDATECONCURSO = 7, ADDCONCURSO = 8, UPDATEPROFITDEALER = 9, ADDPROFITDEALER = 10}

		#endregion

		#region Variables
    
		//private CompanyDealer  oldCompanyDealer = null;
		private DataTable _navigationViewModelTable;
		private int _actionMode = (int)Mode.UPDATE;
		private  string _companyDealerID = String.Empty;
		private  string _companyDealerDesc = String.Empty;
		private  string _dea_unico = String.Empty;
		private  string _entry_dt = DateTime.Now.ToShortDateString();
		private  string _dea_coresu = String.Empty;
		private  string _dea_namer = String.Empty;
		private  bool _bal = false;
		private  bool _eas = false ;
		private  bool _triangle = false ;
		private  string _mgrp_auto = String.Empty;
		private string _mgrp_mbi = String.Empty;
		private string _dea_add1 = String.Empty;
		private string _dea_add2 = String.Empty;
		private string _dea_city = String.Empty;
		private string _dea_state = String.Empty;
		private string _dea_zip = String.Empty;
		private bool _Rfloor = false;
        private string _VSCClientID = String.Empty;
        private string _MasterPolicyID = String.Empty;
        private string _Profit = "225.00";
        private string _Concurso = "100.00";
        private string _BankFee = "125.00";
        private string _ProfitDealer = "900.00";
        private bool _HideProfitDealer = false;
        private bool _LimitProfitDealer = false;

        private double _EfProfitPremier = 0.00;
        private string _EfProfitPremierDate = string.Empty;
        private double _EfConcurso = 0.00;
        private string _EfConcursoDate = string.Empty;
        private double _EfProfitDealer = 0.00;
        private string _EfProfitDealerDate = string.Empty;

        private double _OldEfProfitPremier = 0.00;
        private string _OldEfProfitPremierDate = string.Empty;
        private double _OldEfConcurso = 0.00;
        private string _OldEfConcursoDate = string.Empty;
        private double _OldEfProfitDealer = 0.00;
        private string _OldEfProfitDealerDate = string.Empty;

		#endregion

		#region Properties

		public DataTable NavigationViewModelTable
		{
			get
			{
				return this._navigationViewModelTable;
			}
			set
			{
				this._navigationViewModelTable = value;
			}
		}
		
		public int ActionMode
		{
			get
			{
				return this._actionMode;
			}
			set
			{
				this._actionMode = value;
			}
		}

		public string CompanyDealerID
		{
			get
			{
				return this._companyDealerID;
			}
			set
			{
				this._companyDealerID = value;
			}
		}


		public string CompanyDealerDesc
		{
			get
			{
				return this._companyDealerDesc;
			}
			set
			{
				this._companyDealerDesc = value;
			}
		}


		public string dea_unico
		{
			get
			{
				return this._dea_unico;
			}
			set
			{
				this._dea_unico = value;
			}
		}
		public string entry_dt
		{
			get
			{
				return this._entry_dt;
			}
			set
			{
				this._entry_dt = value;
			}
		}

		
		public string dea_coresu
		{
			get
			{
				return this._dea_coresu;
			}
			set
			{
				this._dea_coresu = value;
			}
		}


		public string dea_namer
		{
			get
			{
				return this._dea_namer;
			}
			set
			{
				this._dea_namer = value;
			}
		}

        
		public bool bal
		{
			get
			{
				return this._bal;
			}
			set
			{
				this._bal = value;
			}
		}
        
		public bool eas
		{
			get
			{
				return this._eas;
			}
			set
			{
				this._eas = value;
			}
		}
        
		public bool triangle
		{
			get
			{
				return this._triangle;
			}
			set
			{
				this._triangle = value;
			}
		}

		public string mgrp_auto
		{
			get
			{
				return this._mgrp_auto;
			}
			set
			{
				this._mgrp_auto = value;
			}
		}


		public string mgrp_mbi
		{
			get
			{
				return this._mgrp_mbi;
			}
			set
			{
				this._mgrp_mbi = value;
			}
		}

		public string dea_add1
		{
			get
			{
				return this._dea_add1;
			}
			set
			{
				this._dea_add1 = value;
			}
		}

		public string dea_add2
		{
			get
			{
				return this._dea_add2;
			}
			set
			{
				this._dea_add2 = value;
			}
		}



		public string dea_city
		{
			get
			{
				return this._dea_city;
			}
			set
			{
				this._dea_city = value;
			}
		}



		public string dea_state
		{
			get
			{
				return this._dea_state;
			}
			set
			{
				this._dea_state = value;
			}
		}

		public string dea_zip
		{
			get
			{
				return this._dea_zip;
			}
			set
			{
				this._dea_zip = value;
			}
		}

		public bool Rfloor
		{
			get
			{
				return this._Rfloor;
			}
			set
			{
				this._Rfloor = value;
			}
		}

        public string VSCClientID
        {
            get
            {
                return this._VSCClientID;
            }
            set
            {
                this._VSCClientID = value;
            }
        }

        public string MasterPolicyID
        {
            get
            {
                return this._MasterPolicyID;
            }
            set
            {
                this._MasterPolicyID = value;
            }
        }

        public string Profit
          {
            get
            {
                return this._Profit;
            }
            set
            {
                this._Profit = value;
            }
        }

        public string ProfitDealer
        {
            get
            {
                return this._ProfitDealer;
            }
            set
            {
                this._ProfitDealer = value;
            }
        }

        public string Concurso
        {
            get
            {
                return this._Concurso;
            }
            set
            {
                this._Concurso = value;
            }
        }

        public string BankFee
        {
            get
            {
                return this._BankFee;
            }
            set
            {
                this._BankFee = value;
            }
        }

        public string EfProfitPremierDate
        {
            get
            {
                return this._EfProfitPremierDate;
            }
            set
            {
                this._EfProfitPremierDate = value;
            }
        }

        public double EfProfitPremier
        {
            get
            {
                return this._EfProfitPremier;
            }
            set
            {
                this._EfProfitPremier = value;
            }
        }

        public string EfConcursoDate
        {
            get
            {
                return this._EfConcursoDate;
            }
            set
            {
                this._EfConcursoDate = value;
            }
        }

        public double EfConcurso
        {
            get
            {
                return this._EfConcurso;
            }
            set
            {
                this._EfConcurso = value;
            }
        }

        public string EfProfitDealerDate
        {
            get
            {
                return this._EfProfitDealerDate;
            }
            set
            {
                this._EfProfitDealerDate = value;
            }
        }

        public double EfProfitDealer
        {
            get
            {
                return this._EfProfitDealer;
            }
            set
            {
                this._EfProfitDealer = value;
            }
        }

        public string OldEfProfitPremierDate
        {
            get
            {
                return this._OldEfProfitPremierDate;
            }
            set
            {
                this._OldEfProfitPremierDate = value;
            }
        }

        public double OldEfProfitPremier
        {
            get
            {
                return this._OldEfProfitPremier;
            }
            set
            {
                this._OldEfProfitPremier = value;
            }
        }

        public string OldEfConcursoDate
        {
            get
            {
                return this._OldEfConcursoDate;
            }
            set
            {
                this._OldEfConcursoDate = value;
            }
        }

        public double OldEfConcurso
        {
            get
            {
                return this._OldEfConcurso;
            }
            set
            {
                this._OldEfConcurso = value;
            }
        }

        public string OldEfProfitDealerDate
        {
            get
            {
                return this._OldEfProfitDealerDate;
            }
            set
            {
                this._OldEfProfitDealerDate = value;
            }
        }

        public double OldEfProfitDealer
        {
            get
            {
                return this._OldEfProfitDealer;
            }
            set
            {
                this._OldEfProfitDealer = value;
            }
        }





        // Anadido para propositos de esconder el profitdealer dependiendo el dealer escogido. Ej. Lozada (Yamil De Jesus)
        public bool HideProfitDealer
        {
            get
            {
                return this._HideProfitDealer;
            }
            set
            {
                this._HideProfitDealer = value;
            }
        }

        public bool LimitProfitDealer
        {
            get
            {
                return this._LimitProfitDealer;
            }
            set
            {
                this._LimitProfitDealer = value;
            }
        }
		#endregion

		#region Public Methods
		      
		private string GetNextCompanyDealerID()
		{
			DataTable dt = LookupTables.GetTable("CompanyDealer");
			DataRow[] dr = dt.Select("","CompanyDealerID");

			DataTable dt2 = dt.Clone();

			for (int rec = 0; rec<=dr.Length-1; rec++)
			{
				DataRow myRow = dt2.NewRow();
				myRow["CompanyDealerID"] = dr[rec].ItemArray[0].ToString();
				myRow["CompanyDealerDesc"] = dr[rec].ItemArray[1].ToString();

				dt2.Rows.Add(myRow);
				dt2.AcceptChanges();
			}

			int ID = 0;

			ID = int.Parse(dt2.Rows[dt2.Rows.Count-1]["CompanyDealerID"].ToString())+1;
						
			return (ID.ToString().PadLeft(3,'0')); 
		}

		#endregion    

		#region Public Functions
        public static DataTable GetSupplierIDByCompanyDealerID(string companyDealerID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
                SqlDbType.Int, 0, companyDealerID.ToString(),
                ref cookItems);

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

            DataTable dt = exec.GetQuery("GetSupplierIDByCompanyDealerID", xmlDoc);

            return dt;	
        }

		public void Delete(string CompanyDealerID)
		{
			try
			{
				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
				executor.Delete("DeleteCompanyDealerID", 
					this.GetDeleteCompanyDealerXml(CompanyDealerID));
			}
			catch(Exception ex)
			{
				this.HandleDeleteError(ex);
			}
		}

		public void Save(int UserID)
		{
			this.Validate();
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				executor.BeginTrans();
				
				switch (this.ActionMode)
				{
					case 1: //ADD
                        //this.CompanyDealerID = GetNextCompanyDealerID();
						executor.Update("AddCompanyDealer",this.GetInsertCompanyDealerXml());
						this.History(this._actionMode,UserID);
//						this.AuditInsert(UserID);
//						this.CommitAudit();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
                    case 5: // UPDATE PROFIT PREMIER
                        this.HistoryProfitPremier(this._actionMode, UserID);
                        break;
                    case 6: // ADD PROFIT PREMIER
                        this.HistoryProfitPremier(this._actionMode, UserID);
                        break;
                    case 7: // UPDATE CONCURSO
                        this.HistoryConcurso(this._actionMode, UserID);
                        break;
                    case 8: // ADD CONCURSO
                        this.HistoryConcurso(this._actionMode, UserID);
                        break;
                    case 9: // UPDATE PROFIT DEALER
                        this.HistoryProfitDealer(this._actionMode, UserID);
                        break;
                    case 10: // ADD PROFIT DEALER
                        this.HistoryProfitDealer(this._actionMode, UserID);
                        break;
					default: //UPDATE
						this.History(this._actionMode,UserID);
//						this.AuditUpdate(UserID);
						executor.Update("UpdateCompanyDealer",this.GetUpdateCompanyDealerXml());
//						this.CommitAudit();//Commit audit;
						break;
				}
				executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				executor.RollBackTrans();
				this.HandleSaveError(xcp);
			}
		}

		public CompanyDealer GetCompanyDealer(string CompanyDealerID)
		{
			try
			{
				DataTable dtCompanyDealer = new DataTable();
				CompanyDealer companyDealer = new CompanyDealer();
				this.CompanyDealerID = CompanyDealerID;		

				Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
				dtCompanyDealer = executor.GetQuery("GetCompanyDealerByCompanyDealerID", 
					this.GetCompanyDealerByCompanyDealerIDXml());

				this.CompanyDealerDesc = 
					dtCompanyDealer.Rows[0]["CompanyDealerDesc"].ToString().Trim();

				this.dea_unico = 
					dtCompanyDealer.Rows[0]["dea_unico"].ToString().Trim();

				this.entry_dt = 
					dtCompanyDealer.Rows[0]["entry_dt"]!= System.DBNull.Value?((DateTime) dtCompanyDealer.Rows[0]["entry_dt"]).ToShortDateString():"";

				this.CompanyDealerID = 
					dtCompanyDealer.Rows[0]["CompanyDealerID"].ToString().Trim();

				this.dea_coresu = 
					dtCompanyDealer.Rows[0]["dea_coresu"].ToString().Trim();

				this.dea_namer = 
					dtCompanyDealer.Rows[0]["dea_namer"].ToString().Trim();

				this.bal = 
					bool.Parse(dtCompanyDealer.Rows[0]["bal"].ToString().Trim());

				this.eas = 
					bool.Parse(dtCompanyDealer.Rows[0]["eas"].ToString().Trim());

				this.triangle = 
					bool.Parse(dtCompanyDealer.Rows[0]["triangle"].ToString().Trim());

				this.mgrp_auto = 
					dtCompanyDealer.Rows[0]["mgrp_auto"].ToString().Trim();

				this.mgrp_mbi = 
					dtCompanyDealer.Rows[0]["mgrp_mbi"].ToString().Trim();

				this.dea_add1 = 
					dtCompanyDealer.Rows[0]["dea_add1"].ToString().Trim();

				this.dea_add2 = 
					dtCompanyDealer.Rows[0]["dea_add2"].ToString().Trim();

				this.dea_city = 
					dtCompanyDealer.Rows[0]["dea_city"].ToString().Trim();

				this.dea_state = 
					dtCompanyDealer.Rows[0]["dea_state"].ToString().Trim();

				this.dea_zip = 
					dtCompanyDealer.Rows[0]["dea_zip"].ToString().Trim();

				this.Rfloor = 
					bool.Parse(dtCompanyDealer.Rows[0]["Rfloor"].ToString().Trim());

                this.VSCClientID =
                     dtCompanyDealer.Rows[0]["VSCClientID"].ToString().Trim();

                this.MasterPolicyID =
                     dtCompanyDealer.Rows[0]["MasterPolicyID"].ToString().Trim();

                this.Profit =
                     dtCompanyDealer.Rows[0]["Profit"].ToString().Trim();

                this.Concurso =
                     dtCompanyDealer.Rows[0]["Concurso"].ToString().Trim();

                this.BankFee =
                     dtCompanyDealer.Rows[0]["BankFee"].ToString().Trim();

                this.ProfitDealer =
                     dtCompanyDealer.Rows[0]["ProfitDealer"].ToString().Trim();

                this.HideProfitDealer =
                    bool.Parse(dtCompanyDealer.Rows[0]["HideProfitDealer"].ToString().Trim());

                this.LimitProfitDealer =
                    bool.Parse(dtCompanyDealer.Rows[0]["LimitProfitDealer"].ToString().Trim());
   			return this;

		}
			catch(Exception ex) 
			{
				throw new Exception(
					"Could not retrieve information for this Company Dealer.", ex);
			}
		}

		#endregion

     	#region Private Functions

		private void HandleSaveError(Exception Ex)
		{
			switch(Ex.GetBaseException().GetType().FullName)
			{
				case "System.Data.SqlClient.SqlException":
					SqlException sqlException = (SqlException)Ex.GetBaseException();
				switch(sqlException.Number)
				{
					case 547:
						throw new Exception("The Company Dealer make you are attempting to " +
							"relate to this Company Dealer does not exist.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"saving the Company Dealer.", Ex);
				}
				default:
					throw new Exception("An error ocurred while saving " + 
						" the Company Dealer.", Ex);
			}            
		}

		private void HandleDeleteError(Exception Ex)
		{
			switch(Ex.GetBaseException().GetType().FullName)
			{
				case "System.Data.SqlClient.SqlException":
					SqlException sqlException = (SqlException)Ex.GetBaseException();
				switch(sqlException.Number)
				{
					case 547:
						throw new Exception("The Company Dealer you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this Company Dealer before attempting to delete it.", Ex);
					default:
						throw new Exception("An database server error ocurred while " +
							"deleting the Company Dealer.", Ex);
				}
				default:
					throw new Exception("An error ocurred while deleting " + 
						" the Company Dealer.", Ex);
			}
		}
  
		private XmlDocument GetDeleteCompanyDealerXml(string CompanyDealerID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			
			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.VarChar, 3, CompanyDealerID.ToString(),
				ref cookItems);
		
			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
	     }
        
		private XmlDocument GetInsertCompanyDealerXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[23];

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.VarChar, 3, this.CompanyDealerID.ToString(),
				ref cookItems);
		
			DbRequestXmlCooker.AttachCookItem("CompanyDealerDesc",
				SqlDbType.VarChar, 30, this.CompanyDealerDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_unico",
				SqlDbType.VarChar, 3, this.dea_unico.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("entry_dt",
				SqlDbType.VarChar, 4, this.entry_dt.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_coresu",
				SqlDbType.VarChar, 3, this.dea_coresu.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("dea_namer",
				SqlDbType.VarChar, 30, this.dea_namer.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("bal",
				SqlDbType.Bit, 0, this.bal.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("eas",
				SqlDbType.Bit, 0, this.eas.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("triangle",
				SqlDbType.Bit, 0, this.triangle.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("mgrp_auto",
				SqlDbType.VarChar, 2, this.mgrp_auto.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("mgrp_mbi",
				SqlDbType.VarChar, 2, this.mgrp_mbi.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_add1",
				SqlDbType.VarChar, 20, this.dea_add1.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_add2",
				SqlDbType.VarChar, 20, this.dea_add2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("dea_city",
				SqlDbType.VarChar, 14, this.dea_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_state",
				SqlDbType.VarChar, 2, this.dea_state.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("dea_zip",
				SqlDbType.VarChar, 10, this.dea_zip.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Rfloor",
				SqlDbType.Bit, 0, this.Rfloor.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VSCClientID",
                SqlDbType.VarChar, 50, this.VSCClientID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
                SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Profit",
                SqlDbType.Float, 4, this.Profit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Concurso",
                SqlDbType.Float, 4, this.Concurso.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankFee",
                SqlDbType.Char, 3, this.BankFee.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ProfitDealer",
                SqlDbType.Float, 4, this.ProfitDealer.ToString(),
                ref cookItems);

            try
            {
                return DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
		}

		private XmlDocument GetUpdateCompanyDealerXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[23];

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.VarChar, 3, this.CompanyDealerID.ToString(),
				ref cookItems);
		
			DbRequestXmlCooker.AttachCookItem("CompanyDealerDesc",
				SqlDbType.VarChar, 30, this.CompanyDealerDesc.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_unico",
				SqlDbType.VarChar, 3, this.dea_unico.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("entry_dt",
				SqlDbType.DateTime, 0, this.entry_dt.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_coresu",
				SqlDbType.VarChar, 3, this.dea_coresu.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("dea_namer",
				SqlDbType.VarChar, 30, this.dea_namer.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("bal",
				SqlDbType.Bit, 0, this.bal.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("eas",
				SqlDbType.Bit, 0, this.eas.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("triangle",
				SqlDbType.Bit, 0, this.triangle.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("mgrp_auto",
				SqlDbType.VarChar, 2, this.mgrp_auto.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("mgrp_mbi",
				SqlDbType.VarChar, 2, this.mgrp_mbi.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_add1",
				SqlDbType.VarChar, 20, this.dea_add1.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_add2",
				SqlDbType.VarChar, 20, this.dea_add2.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("dea_city",
				SqlDbType.VarChar, 14, this.dea_city.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("dea_state",
				SqlDbType.VarChar, 2, this.dea_state.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("dea_zip",
				SqlDbType.VarChar, 10, this.dea_zip.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Rfloor",
				SqlDbType.Bit, 0, this.Rfloor.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VSCClientID",
             SqlDbType.VarChar, 50, this.VSCClientID.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MasterPolicyID",
                SqlDbType.Char, 4, this.MasterPolicyID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Profit",
                SqlDbType.Float, 4, this.Profit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Concurso",
                SqlDbType.Float, 4, this.Concurso.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankFee",
                SqlDbType.Float, 3, this.BankFee.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ProfitDealer",
                SqlDbType.Float, 4, this.ProfitDealer.ToString(),
                ref cookItems);

            try
            {
                return DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
		}

		private XmlDocument GetCompanyDealerByCompanyDealerIDXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.Int, 0, this.CompanyDealerID.ToString(),
				ref cookItems);

			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			
		}

		private void Validate()
		{
			string errorMessage = String.Empty;
			bool found = false;

			if(this.CompanyDealerDesc == "")
			{
				errorMessage += "The Company Dealer cannot be empty.  ";
				found = true;
			}

			
			DataTable dt =  LookupTables.GetTable("CompanyDealer");
			
			if (this.ActionMode ==1)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["CompanyDealerDesc"].ToString().Trim() == this.CompanyDealerDesc.Trim())
					{
						errorMessage = "The CompanyDealer Description is Already exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i]["CompanyDealerDesc"].ToString().Trim() == this.CompanyDealerDesc.Trim() && dt.Rows[i]["CompanyDealerID"].ToString().Trim() != this.CompanyDealerID.ToString().Trim())
					{
						//errorMessage = "The CompanyDealer Description is Already Exist.";
						//found = true;
					}
				}
			}

			if (found == true)
			{
				throw new Exception(errorMessage);
			}
		}

		#region History

		private void History(int mode, int userID)
		{
			Audit.History history = new Audit.History();
			
			if(_actionMode == 2)
			{
				EPolicy.LookupTables.CompanyDealer oldCompanyDealer = new EPolicy.LookupTables.CompanyDealer();
				oldCompanyDealer = oldCompanyDealer.GetCompanyDealer(this.CompanyDealerID);//userID);

				history.BuildNotesForHistory("CompanyDealerDesc",oldCompanyDealer.CompanyDealerDesc.ToString(),this.CompanyDealerDesc.ToString(),mode);
				history.BuildNotesForHistory("dea_unico",oldCompanyDealer.dea_unico,this.dea_unico,mode);
				history.BuildNotesForHistory("entry_dt",oldCompanyDealer.entry_dt,this.entry_dt,mode);
				history.BuildNotesForHistory("dea_coresu",oldCompanyDealer.dea_coresu,this.dea_coresu,mode);
				history.BuildNotesForHistory("dea_namer",oldCompanyDealer.dea_namer,this.dea_namer,mode);
				history.BuildNotesForHistory("bal",oldCompanyDealer.bal.ToString(),this.bal.ToString(),mode);
				history.BuildNotesForHistory("eas",oldCompanyDealer.eas.ToString(),this.eas.ToString(),mode);
				history.BuildNotesForHistory("triangle",oldCompanyDealer.triangle.ToString(),this.triangle.ToString(),mode);
				history.BuildNotesForHistory("mgrp_auto",oldCompanyDealer.mgrp_auto,this.mgrp_auto,mode);
				history.BuildNotesForHistory("mgrp_mbi",oldCompanyDealer.mgrp_mbi,this.mgrp_mbi,mode);
				history.BuildNotesForHistory("dea_add1",oldCompanyDealer.dea_add1,this.dea_add1,mode);
				history.BuildNotesForHistory("dea_add2",oldCompanyDealer.dea_add2,this.dea_add2,mode);
				history.BuildNotesForHistory("dea_city",oldCompanyDealer.dea_city,this.dea_city,mode);
				history.BuildNotesForHistory("dea_state",oldCompanyDealer.dea_state,this.dea_state,mode);
				history.BuildNotesForHistory("dea_zip",oldCompanyDealer.dea_zip,this.dea_zip,mode);
				history.BuildNotesForHistory("Rfloor",oldCompanyDealer.Rfloor.ToString(),this.Rfloor.ToString(),mode);
                history.BuildNotesForHistory("VSCClientID", oldCompanyDealer.VSCClientID, this.VSCClientID, mode);
                history.BuildNotesForHistory("MasterPolicyID", oldCompanyDealer.MasterPolicyID, this.MasterPolicyID, mode);
                history.BuildNotesForHistory("Profit", oldCompanyDealer.Profit, this.Profit, mode);
                history.BuildNotesForHistory("Concurso", oldCompanyDealer.Concurso, this.Concurso, mode);
                history.BuildNotesForHistory("BankFee", oldCompanyDealer.BankFee, this.BankFee, mode);
                history.BuildNotesForHistory("ProfitDealer", oldCompanyDealer.ProfitDealer, this.ProfitDealer, mode);
				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("CompanyDealerID.","",this.CompanyDealerID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.CompanyDealerID.ToString();
			history.Subject = "COMPANY DEALER";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}

        private void HistoryProfitPremier(int mode, int userID)
        {
            Audit.History history = new Audit.History();

            if (_actionMode == 5)
            {
                EPolicy.LookupTables.CompanyDealer oldcompanyDealer = new EPolicy.LookupTables.CompanyDealer();
                oldcompanyDealer = oldcompanyDealer.GetCompanyDealer(this.CompanyDealerID);//userID);

                history.BuildNotesForHistory("EffectiveDate", this.OldEfProfitPremierDate.ToString(), this.EfProfitPremierDate.ToString(), mode);
                history.BuildNotesForHistory("ProfitPremier", this.OldEfProfitPremier.ToString(), this.EfProfitPremier.ToString(), mode);

                history.Actions = "EDIT";
            }
            else  //ADD & DELETE
            {
                history.BuildNotesForHistory("EffectiveDate", "", this.EfProfitPremierDate.ToString(), mode);
                history.BuildNotesForHistory("ProfitPremier", "", this.EfProfitPremier.ToString(), mode);
                history.Actions = "ADD";

            }

            history.KeyID = this.CompanyDealerID.ToString();
            history.Subject = "COMPANY DEALER";
            history.UsersID = userID;
            history.GetSaveHistory();
        }

        private void HistoryConcurso(int mode, int userID)
        {
            Audit.History history = new Audit.History();

            if (_actionMode == 7)
            {
                EPolicy.LookupTables.CompanyDealer oldcompanyDealer = new EPolicy.LookupTables.CompanyDealer();
                oldcompanyDealer = oldcompanyDealer.GetCompanyDealer(this.CompanyDealerID);//userID);

                history.BuildNotesForHistory("EffectiveDate", this.OldEfConcursoDate.ToString(), this.EfConcursoDate.ToString(), mode);
                history.BuildNotesForHistory("Concurso", this.OldEfConcurso.ToString(), this.EfConcurso.ToString(), mode);

                history.Actions = "EDIT";
            }
            else  //ADD & DELETE
            {
                history.BuildNotesForHistory("EffectiveDate", "", this.EfConcursoDate.ToString(), mode);
                history.BuildNotesForHistory("Concurso", "", this.EfConcurso.ToString(), mode);
                history.Actions = "ADD";

            }

            history.KeyID = this.CompanyDealerID.ToString();
            history.Subject = "COMPANY DEALER";
            history.UsersID = userID;
            history.GetSaveHistory();
        }

        private void HistoryProfitDealer(int mode, int userID)
        {
            Audit.History history = new Audit.History();

            if (_actionMode == 9)
            {
                EPolicy.LookupTables.CompanyDealer oldcompanyDealer = new EPolicy.LookupTables.CompanyDealer();
                oldcompanyDealer = oldcompanyDealer.GetCompanyDealer(this.CompanyDealerID);//userID);

                history.BuildNotesForHistory("EffectiveDate", this.OldEfProfitDealerDate.ToString(), this.EfProfitDealerDate.ToString(), mode);
                history.BuildNotesForHistory("ProfitDealer", this.OldEfProfitDealer.ToString(), this.EfProfitDealer.ToString(), mode);

                history.Actions = "EDIT";
            }
            else  //ADD & DELETE
            {
                history.BuildNotesForHistory("EffectiveDate", "", this.EfProfitDealerDate.ToString(), mode);
                history.BuildNotesForHistory("ProfitDealer", "", this.EfProfitDealer.ToString(), mode);
                history.Actions = "ADD";

            }

            history.KeyID = this.CompanyDealerID.ToString();
            history.Subject = "COMPANY DEALER";
            history.UsersID = userID;
            history.GetSaveHistory();
        }

		private object SafeConvertToDateTime(string StringObject)
		{
			if(StringObject != string.Empty)
			{
				try	{return DateTime.Parse(StringObject);}
				catch{/*Write to error logging sub-system.*/}
			}
			return StringObject;
		}

#endregion




		#endregion



	}
}
