using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Audit;
using EPolicy.XmlCooker;
using EPolicy.Quotes;
using Baldrich.DBRequest;
using System.Xml;


namespace EPolicy.TaskControl
{
    public class UltimateProtectorQuote : TaskControl
    {
        public UltimateProtectorQuote()
		{
			this.PolicyClassID	  = 17;
			this.PolicyID         = 0;
			this.InsuranceCompany = "000";
			this.Agency           = "001";
			this.Agent            = "001";
			this.Bank			  = "000";
			this.Dealer			  = "000";
			this.CompanyDealer	  = "000";
			this.TaskStatusID     = 33; //Unapplied //int.Parse(LookupTables.LookupTables.GetID("TaskStatus","Open"));
            this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "UltimateProtectorQuote"));
			this._mode =(int) TaskControlMode.ADD;
		}

		#region Variables

        private UltimateProtectorQuote oldUltimateProtectorQuote = null;
        private DataTable _dtUltimateProtectorQuote;
        private int _UltimateProtectorID = 0;
		private string	_EffectiveDate    			 = DateTime.Now.ToShortDateString();
		private int	_CoveragePlanID					 = 0;
		private int	_NewUse							 = 2;
        private int _Milleages                       = 0;
		private int	_Term  							 = 0;
		private int	_Miles    						 = 0;
		private int	_VehicleMakeID					 = 0;
		private int _VehicleModelID					 = 0;
		private int	_VehicleYearID					 = 0;
		private bool  _Turbo						 = false;
		private bool  _WD4							 = false;
		private bool  _Diesel						 = false;
        private string _Code = "";
        private string _Class = "";
        private double _LossFund = 0.00;
        private double _OverHead = 0.00;
        private double _BankFee = 0.00;
        private double _Profit = 0.00;
        private double _Concurso = 0.00;
        private double _DealerCost = 0.00;
        private double _DealerProfit = 0.00;
        private double _TotalPrice = 0.00;
        private double _CanReserve = 0.00;
        private double _DealerNet = 0.00;
        private double _Charge = 0.00;
        private bool _WithCharge = false;
        private int _VSCRateID = 0;
        private bool _PowerTrain = false;
        private bool _CommercialUse = false;
        private bool _HideProfitDealer = false;
		private int _mode = (int) TaskControlMode.CLEAR;

		#endregion

		#region Properties

        public int UltimateProtectorID
		{
			get
			{
                return this._UltimateProtectorID;
			}
			set 
			{
                this._UltimateProtectorID = value;
			}
		}
 
		public string EffectiveDate
		{
			get
			{
				return this._EffectiveDate;
			}
			set 
			{
				this._EffectiveDate = value;
			}
		}

		public int CoveragePlanID
		{
			get
			{
				return this._CoveragePlanID;
			}
			set 
			{
				this._CoveragePlanID = value;
			}
		}
		
		public int NewUse
		{
			get
			{
				return this._NewUse;
			}
			set 
			{
				this._NewUse = value;
			}
		}

        public int Milleages
        {
            get
            {
                return this._Milleages;
            }
            set
            {
                this._Milleages = value;
            }
        }

		public int Term
		{
			get
			{
				return this._Term;
			}
			set 
			{
				this._Term = value;
			}
		}

		public int Miles
		{
			get
			{
				return this._Miles;
			}
			set 
			{
				this._Miles = value;
			}
		}

		public int VehicleMakeID
		{
			get
			{
				return this._VehicleMakeID;
			}
			set 
			{
				this._VehicleMakeID = value;
			}
		}

		public int VehicleModelID
		{
			get
			{
				return this._VehicleModelID;
			}
			set 
			{
				this._VehicleModelID = value;
			}
		}

		public int VehicleYearID
		{
			get
			{
				return this._VehicleYearID;
			}
			set 
			{
				this._VehicleYearID = value;
			}
		}

		public bool Turbo
		{
			get
			{
				return this._Turbo;
			}
			set 
			{
				this._Turbo = value;
			}
		}

		public bool Diesel
		{
			get
			{
				return this._Diesel;
			}
			set 
			{
				this._Diesel = value;
			}
		}

		public bool WD4
		{
			get
			{
				return this._WD4;
			}
			set 
			{
				this._WD4 = value;
			}
		}

        public string Code 
        {
			get
			{
				return this._Code;
			}
			set 
			{
				this._Code = value;
			}
		}

        public string Class
        {
			get
			{
				return this._Class;
			}
			set 
			{
				this._Class = value;
			}
		}

        public double LossFund
        {
			get
			{
				return this._LossFund;
			}
			set 
			{
				this._LossFund = value;
			}
		}

        public double OverHead
        {
			get
			{
				return this._OverHead;
			}
			set 
			{
				this._OverHead = value;
			}
		}

        public double BankFee
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

        public double Profit 
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

        public double Concurso
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

        public double DealerCost
        {
			get
			{
				return this._DealerCost;
			}
			set 
			{
				this._DealerCost = value;
			}
		}

        public double DealerProfit
        {
			get
			{
				return this._DealerProfit;
			}
			set 
			{
				this._DealerProfit = value;
			}
		}

        public double TotalPrice
        {
			get
			{
				return this._TotalPrice;
			}
			set 
			{
				this._TotalPrice = value;
			}
		}

        public double CanReserve
        {
			get
			{
				return this._CanReserve;
			}
			set 
			{
				this._CanReserve = value;
			}
		}

        public double DealerNet
        {
            get
            {
                return this._DealerNet;
            }
            set
            {
                this._DealerNet = value;
            }
        }

        public double Charge
        {
            get
            {
                return this._Charge;
            }
            set
            {
                this._Charge = value;
            }
        }

        public bool WithCharge
        {
            get
            {
                return this._WithCharge;
            }
            set
            {
                this._WithCharge = value;
            }
        }

        public int VSCRateID
        {
            get
            {
                return this._VSCRateID;
            }
            set
            {
                this._VSCRateID = value;
            }
        }

        public bool PowerTrain
        {
            get
            {
                return this._PowerTrain;
            }
            set
            {
                this._PowerTrain = value;
            }
        }

        public bool CommercialUse
        {
            get
            {
                return this._CommercialUse;
            }
            set
            {
                this._CommercialUse = value;
            }
        }
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

		#endregion

		#region Public Methods

		public override void Save(int UserID)
		{
			this._mode = (int) this.Mode;  // Se le asigna el mode de taskControl.

            if (this.Prospect.ProspectID == 0)
                this.Prospect.Mode = 1;
            else
                this.Prospect.Mode = 2;

            this.Prospect.IsBusiness = false;
            this.Prospect.SaveProspect(UserID);

            this.ProspectID = this.Prospect.ProspectID;

			base.Validate();
			this.Validate();
			
			if (this._mode ==2)
                oldUltimateProtectorQuote = (UltimateProtectorQuote)UltimateProtectorQuote.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

			base.Save(UserID);	// Validate and Save TaskControl
            SaveUltimateProtectorQuote(UserID);			    // Save TaskPayment
			
			this._mode = (int) TaskControlMode.UPDATE;
			this.Mode = (int) TaskControlMode.CLEAR;
		}

		public override void Validate()
		{
			string errorMessage = String.Empty;
			bool found = false;

            if (this.Prospect.FirstName.Trim() == "" && found == false)
            {
                errorMessage = "The First Name Date is missing or wrong.";
                found = true;
            }

            if (this.Prospect.HomePhone.Trim() == "" && found == false)
            {
                errorMessage = "The Home Phone Date is missing or wrong.";
                found = true;
            }

			if (this.EffectiveDate.Trim() == "" && found == false)
			{
				errorMessage = "The Effective Date is missing or wrong.";
				found = true;
			}

			if (this.NewUse == 0 && found == false)
			{
				errorMessage = "The New / Used is missing or wrong.";
				found = true;
			}

            //if (this.Term == 0 && found == false)
            //{
            //    errorMessage = "The Term is missing or wrong.";
            //    found = true;
            //}

            //if (this.Miles == 0 && found == false)
            //{
            //    errorMessage = "The Miles is missing or wrong.";
            //    found = true;
            //}

			if (this.VehicleMakeID == 0 && found == false)
			{
                errorMessage = "The Vehicle Make is missing or wrong.";
				found = true;
			}

			if (this.VehicleModelID == 0 && found == false)
			{
                errorMessage = "The Vehicle Model is missing or wrong.";
				found = true;
			}

            if (this.TotalPrice <= 0.00 && found == false)
            {
                errorMessage = "The Total Price must be greater than zero.";
                found = true;
            }
			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}
		}

        public static UltimateProtectorQuote GetUltimateProtectorQuote(int taskControlID)
		{
            UltimateProtectorQuote UltimateProtectorQuote = null;

            DataTable dt = GetUltimateProtectorQuoteByTaskControlID(taskControlID);

            UltimateProtectorQuote = new UltimateProtectorQuote();
            UltimateProtectorQuote._dtUltimateProtectorQuote = dt;

            UltimateProtectorQuote = FillProperties(UltimateProtectorQuote);

            return UltimateProtectorQuote;
		}
		
		#endregion

		#region Private Methods

        private static DataTable GetUltimateProtectorQuoteByTaskControlID(int taskControlID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, taskControlID.ToString(),
				ref cookItems);

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			XmlDocument xmlDoc;
			
			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}
			DataTable dt = null;
			try
			{
                dt = exec.GetQuery("GetUltimateProtectorQuoteByTaskControlID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}			
		}

        private void SaveUltimateProtectorQuote(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (this._mode)
				{
					case 1:  //ADD
                        this.UltimateProtectorID =
                            Executor.Insert("AddUltimateProtectorQuote", this.GetInsertUltimateProtectorQuoteXml());
						this.History(this._mode,UserID);
						break;

					case 3:  //DELETE
                        Executor.Update("DeleteUltimateProtectorQuote", this.GetDeleteUltimateProtectorQuoteXml());
						this.History(this._mode,UserID);
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						this.History(this._mode,UserID);
                        Executor.Update("UpdateUltimateProtectorQuote", this.GetUpdateUltimateProtectorQuoteXml());
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the quote. "+xcp.Message,xcp);
			}
		}

        public void DeleteUltimateProtectorQuote(int UserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
                Executor.Update("DeleteUltimateProtectorQuote", this.GetDeleteUltimateProtectorQuoteXml());
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to delete the Quote. "+xcp.Message,xcp);
			}
		}

        private XmlDocument GetDeleteUltimateProtectorQuoteXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
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

        private XmlDocument GetUpdateUltimateProtectorQuoteXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[32];
			
			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlTypeID",
            SqlDbType.Int, 0, this.TaskControlTypeID.ToString(),
            ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CoveragePlanID",
				SqlDbType.Int, 0, this.CoveragePlanID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("NewUse",
				SqlDbType.Int, 0, this.NewUse.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Milleages",
                    SqlDbType.Int, 0, this.Milleages.ToString(),
                    ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Term",
				SqlDbType.Int, 0, this.Term.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Miles",
				SqlDbType.Int, 0, this.Miles.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
				SqlDbType.Int, 0, this.VehicleMakeID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("VehicleModelID",
				SqlDbType.Int, 0, this.VehicleModelID.ToString(),
				ref cookItems);		
			
			DbRequestXmlCooker.AttachCookItem("VehicleYearID",
				SqlDbType.Int, 0, this.VehicleYearID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Turbo",
				SqlDbType.Bit, 0, this.Turbo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("WD4",
				SqlDbType.Bit, 0, this.WD4.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Diesel",
				SqlDbType.Bit, 0, this.Diesel.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Code",
                SqlDbType.VarChar, 20, this.Code.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Class",
                SqlDbType.VarChar, 20, this.Class.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LossFund",
                SqlDbType.Float, 0, this.LossFund.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OverHead",
                SqlDbType.Float, 0, this.OverHead.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankFee",
                SqlDbType.Float, 0, this.BankFee.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Profit",
                SqlDbType.Float, 0, this.Profit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Concurso",
                SqlDbType.Float, 0, this.Concurso.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerCost",
                SqlDbType.Float, 0, this.DealerCost.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerProfit",
                SqlDbType.Float, 0, this.DealerProfit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPrice",
                SqlDbType.Float, 0, this.TotalPrice.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CanReserve",
                SqlDbType.Float, 0, this.CanReserve.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerNet",
                SqlDbType.Float, 0, this.DealerNet.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VSCRateID",
                SqlDbType.Int, 0, this.VSCRateID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PowerTrain",
                SqlDbType.Bit, 0, this.PowerTrain.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CompanyDealer",
                SqlDbType.Char, 3, this.CompanyDealer.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Charge",
                SqlDbType.Float, 0, this.Charge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WithCharge",
                SqlDbType.Bit, 0, this.WithCharge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommercialUse",
                SqlDbType.Bit, 0, this.CommercialUse.ToString(),
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

        private XmlDocument GetInsertUltimateProtectorQuoteXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[32];
			
			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlTypeID",
            SqlDbType.Int, 0, this.TaskControlTypeID.ToString(),
            ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EffectiveDate",
				SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("CoveragePlanID",
				SqlDbType.Int, 0, this.CoveragePlanID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("NewUse",
				SqlDbType.Int, 0, this.NewUse.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Milleages",
                    SqlDbType.Int, 0, this.Milleages.ToString(),
                    ref cookItems);
						
			DbRequestXmlCooker.AttachCookItem("Term",
				SqlDbType.Int, 0, this.Term.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Miles",
				SqlDbType.Int, 0, this.Miles.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
				SqlDbType.Int, 0, this.VehicleMakeID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("VehicleModelID",
				SqlDbType.Int, 0, this.VehicleModelID.ToString(),
				ref cookItems);		
			
			DbRequestXmlCooker.AttachCookItem("VehicleYearID",
				SqlDbType.Int, 0, this.VehicleYearID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Turbo",
				SqlDbType.Bit, 0, this.Turbo.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("WD4",
				SqlDbType.Bit, 0, this.WD4.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Diesel",
				SqlDbType.Bit, 0, this.Diesel.ToString(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Code",
                 SqlDbType.VarChar, 20, this.Code.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Class",
                SqlDbType.VarChar, 20, this.Class.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LossFund",
                SqlDbType.Float, 0, this.LossFund.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OverHead",
                SqlDbType.Float, 0, this.OverHead.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankFee",
                SqlDbType.Float, 0, this.BankFee.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Profit",
                SqlDbType.Float, 0, this.Profit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Concurso",
                SqlDbType.Float, 0, this.Concurso.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerCost",
                SqlDbType.Float, 0, this.DealerCost.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerProfit",
                SqlDbType.Float, 0, this.DealerProfit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPrice",
                SqlDbType.Float, 0, this.TotalPrice.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CanReserve",
                SqlDbType.Float, 0, this.CanReserve.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DealerNet",
                SqlDbType.Float, 0, this.DealerNet.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VSCRateID",
                 SqlDbType.Int, 0, this.VSCRateID.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PowerTrain",
                SqlDbType.Bit, 0, this.PowerTrain.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CompanyDealer",
                SqlDbType.Char, 3, this.CompanyDealer.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Charge",
                SqlDbType.Float, 0, this.Charge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("WithCharge",
                SqlDbType.Bit, 0, this.WithCharge.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommercialUse",
                SqlDbType.Bit, 0, this.CommercialUse.ToString(),
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

        private static UltimateProtectorQuote FillProperties(UltimateProtectorQuote ultimateProtectorQuote)
		{
			ultimateProtectorQuote.UltimateProtectorID    = (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["ultimateProtectorID"];
			ultimateProtectorQuote.TaskControlID				= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["TaskControlID"];
			ultimateProtectorQuote.EffectiveDate				= ((DateTime) ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["EffectiveDate"]).ToShortDateString();
			ultimateProtectorQuote.CoveragePlanID				= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["CoveragePlanID"];
			ultimateProtectorQuote.NewUse						= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["NewUse"];
            ultimateProtectorQuote.Milleages                   = (int)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Milleages"];
			ultimateProtectorQuote.Term						= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Term"];
			ultimateProtectorQuote.Miles						= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Miles"];
			ultimateProtectorQuote.VehicleMakeID				= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["VehicleMakeID"];
			ultimateProtectorQuote.VehicleModelID				= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["VehicleModelID"];
			ultimateProtectorQuote.VehicleYearID				= (int)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["VehicleYearID"];
			ultimateProtectorQuote.Turbo						= (bool)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Turbo"];
			ultimateProtectorQuote.WD4							= (bool)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["WD4"];
			ultimateProtectorQuote.Diesel						= (bool)  ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Diesel"];
            ultimateProtectorQuote.Code  = (ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Code"] != System.DBNull.Value) ? (ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Code"].ToString()) : "";
            ultimateProtectorQuote.Class = (ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Class"] != System.DBNull.Value) ? (ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Class"].ToString()) : "";          
            ultimateProtectorQuote.LossFund                    = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["LossFund"];
            ultimateProtectorQuote.OverHead = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["OverHead"];
            ultimateProtectorQuote.BankFee = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["BankFee"];
            ultimateProtectorQuote.Profit = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Profit"];
            ultimateProtectorQuote.Concurso = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Concurso"];
            ultimateProtectorQuote.DealerCost = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["DealerCost"];
            ultimateProtectorQuote.DealerProfit = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["DealerProfit"];
            ultimateProtectorQuote.TotalPrice = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["TotalPrice"];
            ultimateProtectorQuote.CanReserve = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["CancReserve"];
            ultimateProtectorQuote.DealerNet = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["DealerNet"];
            ultimateProtectorQuote.VSCRateID = (int)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["VSCRateID"];
            ultimateProtectorQuote.PowerTrain = (bool)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["PowerTrain"];
            ultimateProtectorQuote.Charge = (double)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["Charge"];
            ultimateProtectorQuote.WithCharge = (bool)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["WithCharge"];
            ultimateProtectorQuote.CommercialUse = (bool)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["CommercialUse"];
            //ultimateProtectorQuote.HideProfitDealer = (bool)ultimateProtectorQuote._dtUltimateProtectorQuote.Rows[0]["HideProfitDealer"];

            return ultimateProtectorQuote;
		}

		#region History

		private void History(int mode, int userID)
		{
			Audit.History history = new Audit.History();
			
			if(_mode == 2)
			{		    
				// Campos de TaskControl
				history.BuildNotesForHistory("TaskControlTypeID",
					LookupTables.LookupTables.GetDescription("TaskControlType",oldUltimateProtectorQuote.TaskControlTypeID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskControlType",this.TaskControlTypeID.ToString()),
					mode);
				history.BuildNotesForHistory("TaskStatusID",
                    LookupTables.LookupTables.GetDescription("TaskStatus", oldUltimateProtectorQuote.TaskStatusID.ToString()),
					LookupTables.LookupTables.GetDescription("TaskStatus",this.TaskStatusID.ToString()),
					mode);
                history.BuildNotesForHistory("ProspectID", oldUltimateProtectorQuote.ProspectID.ToString(), this.ProspectID.ToString(), mode);
                history.BuildNotesForHistory("CustomerNo", oldUltimateProtectorQuote.CustomerNo, this.CustomerNo, mode);
                history.BuildNotesForHistory("PolicyID", oldUltimateProtectorQuote.PolicyID.ToString(), this.PolicyID.ToString(), mode);							
				history.BuildNotesForHistory("PolicyClassID",
                    LookupTables.LookupTables.GetDescription("PolicyClass", oldUltimateProtectorQuote.PolicyClassID.ToString()),
					LookupTables.LookupTables.GetDescription("PolicyClass",this.PolicyClassID.ToString()),
					mode);
                history.BuildNotesForHistory("Agency", oldUltimateProtectorQuote.Agent, this.Agent, mode);
                history.BuildNotesForHistory("Agent", oldUltimateProtectorQuote.Agent, this.Agent, mode);
				history.BuildNotesForHistory("Bank",
                    LookupTables.LookupTables.GetDescription("Bank", oldUltimateProtectorQuote.Bank.ToString()),
					LookupTables.LookupTables.GetDescription("Bank",this.Bank.ToString()),
					mode);
                history.BuildNotesForHistory("InsuranceCompany", oldUltimateProtectorQuote.InsuranceCompany, this.InsuranceCompany, mode);
                history.BuildNotesForHistory("Dealer", oldUltimateProtectorQuote.Dealer, this.Dealer, mode);
				history.BuildNotesForHistory("CompanyDealer",
                    LookupTables.LookupTables.GetDescription("CompanyDealer", oldUltimateProtectorQuote.CompanyDealer.ToString()),
					LookupTables.LookupTables.GetDescription("CompanyDealer",this.CompanyDealer.ToString()),
					mode);	
				//history.BuildNotesForHistory("EntryDate",oldPayment.EntryDate.t,this.EntryDate,mode);
                history.BuildNotesForHistory("CloseDate", oldUltimateProtectorQuote.CloseDate, this.CloseDate, mode);
                history.BuildNotesForHistory("EnteredBy", oldUltimateProtectorQuote.EnteredBy, this.EnteredBy, mode);
				// Terminan Campos TaskControl

				history.BuildNotesForHistory("Effective Date",oldUltimateProtectorQuote.EffectiveDate,this.EffectiveDate,mode);				
	
				history.BuildNotesForHistory("CoveragePlan",
					LookupTables.LookupTables.GetDescription("CoveragePlan",oldUltimateProtectorQuote.CoveragePlanID.ToString()),
					LookupTables.LookupTables.GetDescription("CoveragePlan",this.CoveragePlanID.ToString()),
					mode);	
				history.BuildNotesForHistory("NewUse",
					LookupTables.LookupTables.GetDescription("NewUse",oldUltimateProtectorQuote.NewUse.ToString()),
					LookupTables.LookupTables.GetDescription("NewUse",this.NewUse.ToString()),
					mode);
				history.BuildNotesForHistory("VehicleMake",
					LookupTables.LookupTables.GetDescription("VehicleMake",oldUltimateProtectorQuote.VehicleMakeID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleMake",this.VehicleMakeID.ToString()),
					mode);
				history.BuildNotesForHistory("VehicleModel",
					LookupTables.LookupTables.GetDescription("VehicleModel",oldUltimateProtectorQuote.VehicleModelID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleModel",this.VehicleModelID.ToString()),
					mode);
				history.BuildNotesForHistory("VehicleYear",
					LookupTables.LookupTables.GetDescription("VehicleYear",oldUltimateProtectorQuote.VehicleYearID.ToString()),
					LookupTables.LookupTables.GetDescription("VehicleYear",this.VehicleYearID.ToString()),
					mode);
                history.BuildNotesForHistory("Milleages", oldUltimateProtectorQuote.Milleages.ToString(), this.Milleages.ToString(), mode);
				history.BuildNotesForHistory("Turbo",oldUltimateProtectorQuote.Turbo.ToString(),this.Turbo.ToString(),mode);	
				history.BuildNotesForHistory("WD4",oldUltimateProtectorQuote.WD4.ToString(),this.WD4.ToString(),mode);	
				history.BuildNotesForHistory("Diesel",oldUltimateProtectorQuote.Diesel.ToString(),this.Diesel.ToString(),mode);
                history.BuildNotesForHistory("Code", oldUltimateProtectorQuote.Code.ToString(), this.Code.ToString(), mode);
                history.BuildNotesForHistory("Class", oldUltimateProtectorQuote.Class.ToString(), this.Class.ToString(), mode);
                history.BuildNotesForHistory("LossFund", oldUltimateProtectorQuote.LossFund.ToString(), this.LossFund.ToString(), mode);
                history.BuildNotesForHistory("OverHead", oldUltimateProtectorQuote.OverHead.ToString(), this.OverHead.ToString(), mode);
                history.BuildNotesForHistory("BankFee", oldUltimateProtectorQuote.BankFee.ToString(), this.BankFee.ToString(), mode);
                history.BuildNotesForHistory("Profit", oldUltimateProtectorQuote.Profit.ToString(), this.Profit.ToString(), mode);
                history.BuildNotesForHistory("Concurso", oldUltimateProtectorQuote.Concurso.ToString(), this.Concurso.ToString(), mode);
                history.BuildNotesForHistory("DealerCost", oldUltimateProtectorQuote.DealerCost.ToString(), this.DealerCost.ToString(), mode);
                history.BuildNotesForHistory("DealerProfit", oldUltimateProtectorQuote.DealerProfit.ToString(), this.DealerProfit.ToString(), mode);
                history.BuildNotesForHistory("TotalPrice", oldUltimateProtectorQuote.TotalPrice.ToString(), this.TotalPrice.ToString(), mode);
                history.BuildNotesForHistory("CanReserve", oldUltimateProtectorQuote.CanReserve.ToString(), this.CanReserve.ToString(), mode);
                history.BuildNotesForHistory("DealerNet", oldUltimateProtectorQuote.DealerNet.ToString(), this.DealerNet.ToString(), mode);
                history.BuildNotesForHistory("Power Train", oldUltimateProtectorQuote.PowerTrain.ToString(), this.PowerTrain.ToString(), mode);
                history.BuildNotesForHistory("Commercial Use", oldUltimateProtectorQuote.CommercialUse.ToString(), this.CommercialUse.ToString(), mode);
				history.Actions = "EDIT";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("TaskControlID.","",this.TaskControlID.ToString(),mode);
				history.Actions = "ADD";
			}

			history.KeyID = this.TaskControlID.ToString();
			history.Subject = "UltimateProtectorQUOTE";			
			history.UsersID =  userID;
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
