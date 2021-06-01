using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Audit;
using EPolicy.XmlCooker;
using EPolicy.Quotes;

namespace EPolicy.TaskControl
{
    public class CompulsoryInsuranceQuote : TaskControl
    {
        public CompulsoryInsuranceQuote()
		{
			this.PolicyClassID	  = 15;
			this.PolicyID         = 0;
			this.InsuranceCompany = "000";
			this.Agency           = "001";
			this.Agent            = "001";
			this.Bank			  = "000";
			this.Dealer			  = "000";
			this.CompanyDealer	  = "000";
			this.TaskStatusID     = 31; //Unapplied //int.Parse(LookupTables.LookupTables.GetID("TaskStatus","Open"));
			this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType","CompulsoryInsuranceQuote"));
			this._mode =(int) TaskControlMode.ADD;
		}

        #region Variables

        private CompulsoryInsuranceQuote oldCompulsoryInsuranceQuote = null;
        private DataTable _dtCompulsoryInsuranceQuote;
        private int _CompulsoryInsuranceID = 0;
        private string _EffectiveDate = DateTime.Now.ToShortDateString();
        private string _ExpirationDate = "";
        private string _RegisterDate = DateTime.Now.ToShortDateString();
        private int _NewUse = 2;
        private int _VehicleMakeID = 0;
        private int _VehicleModelID = 0;
        private int _VehicleYearID = 0;
        private int _Term = 0;
        private string _Code = "";
        private string _Class = "";
        private string _Plate = "";
        private string _VIN = "";
        private double _TotalPrice = 0.00;
        private bool _CommercialUse = false;
        private bool _isLeasing = false;
        private string _LicExpDate = "";

  
        private int _mode = (int)TaskControlMode.CLEAR;

        #endregion

        #region Properties

        public int CompulsoryInsuranceID
        {
            get
            {
                return this._CompulsoryInsuranceID;
            }
            set
            {
                this._CompulsoryInsuranceID = value;
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

        public string ExpirationDate
        {
            get
            {
                return this._ExpirationDate;
            }
            set
            {
                this._ExpirationDate = value;
            }
        }

        public string LicExpDate
        {
            get
            {
                return this._LicExpDate;
            }
            set
            {
                this._LicExpDate = value;
            }
        }

        public string RegisterDate
        {
            get
            {
                return this._RegisterDate;
            }
            set
            {
                this._RegisterDate = value;
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

        public string Plate
        {
            get
            {
                return this._Plate;
            }
            set
            {
                this._Plate = value;
            }
        }

        public string VIN
        {
            get
            {
                return this._VIN;
            }
            set
            {
                this._VIN = value;
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
        public bool isLeasing
        {
            get
            {
                return this._isLeasing;
            }
            set
            {
                this._isLeasing = value;
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
        #endregion

        #region Public Methods

        public override void Save(int UserID)
        {
            this._mode = (int)this.Mode;  // Se le asigna el mode de taskControl.

            if (this.Customer.ProspectID == 0)
                this.Customer.Mode = 1;
            else
                this.Customer.Mode = 2;

            this.Customer.IsBusiness = false;
            this.Customer.Save(UserID);

            this.ProspectID = this.Customer.ProspectID;

            base.Validate();
            this.Validate();

            if (this._mode == 2)
                oldCompulsoryInsuranceQuote = (CompulsoryInsuranceQuote)CompulsoryInsuranceQuote.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

            base.Save(UserID);	// Validate and Save TaskControl
            SaveCompulsoryInsuranceQuote(UserID);			    // Save TaskPayment

            this._mode = (int)TaskControlMode.UPDATE;
            this.Mode = (int)TaskControlMode.CLEAR;
        }

        public override void Validate()
        {
            string errorMessage = String.Empty;
            bool found = false;

            if (this.Customer.FirstName.Trim() == "" && found == false)
            {
                errorMessage = "The First Name is missing or wrong.";
                found = true;
            }

            //if (this.Customer.HomePhone.Trim() == "" && found == false)
            //{
            //    errorMessage = "The Home Phone is missing or wrong.";
            //    found = true;
            //}

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

            //if (this.VehicleMakeID == 0 && found == false)
            //{
            //    errorMessage = "The Vehicle Make is missing or wrong.";
            //    found = true;
            //}

            //if (this.VehicleModelID == 0 && found == false)
            //{
            //    errorMessage = "The Vehicle Model is missing or wrong.";
            //    found = true;
            //}

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

        public static CompulsoryInsuranceQuote GetCompulsoryInsuranceQuote(int taskControlID)
        {
            CompulsoryInsuranceQuote compulsoryInsuranceQuote = null;

            DataTable dt = GetCompulsoryInsuranceQuoteByTaskControlID(taskControlID);

            compulsoryInsuranceQuote = new CompulsoryInsuranceQuote();
            compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote = dt;

            compulsoryInsuranceQuote = FillProperties(compulsoryInsuranceQuote);

            return compulsoryInsuranceQuote;
        }

        #endregion

        #region Private Methods

        private static DataTable GetCompulsoryInsuranceQuoteByTaskControlID(int taskControlID)
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
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
            DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetCompulsoryInsuranceQuoteByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve prospect by criteria.", ex);
            }
        }

        

        private void SaveCompulsoryInsuranceQuote(int UserID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this._mode)
                {
                    case 1:  //ADD
                        this.CompulsoryInsuranceID =
                            Executor.Insert("AddCompulsoryInsuranceQuote", this.GetInsertCompulsoryInsuranceQuoteXml());
                        this.History(this._mode, UserID);
                        break;

                    case 3:  //DELETE
                        Executor.Update("DeleteCompulsoryInsuranceQuote", this.GetDeleteCompulsoryInsuranceQuoteXml());
                        this.History(this._mode, UserID);
                        break;

                    case 4:  //CLEAR						
                        break;

                    default: //UPDATE
                        this.History(this._mode, UserID);
                        Executor.Update("UpdateCompulsoryInsuranceQuote", this.GetUpdateCompulsoryInsuranceQuoteXml());
                        break;
                }
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the quote. " + xcp.Message, xcp);
            }
        }

        public void DeleteCompulsoryInsuranceQuote(int UserID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                Executor.Update("DeleteCompulsoryInsuranceQuote", this.GetDeleteCompulsoryInsuranceQuoteXml());
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to delete the Quote. " + xcp.Message, xcp);
            }
        }

        private XmlDocument GetDeleteCompulsoryInsuranceQuoteXml()
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
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }

        private XmlDocument GetUpdateCompulsoryInsuranceQuoteXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[18];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpirationDate",
            SqlDbType.DateTime, 0, this.ExpirationDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RegisterDate",
            SqlDbType.DateTime, 0, this.RegisterDate.ToString(),
            ref cookItems);

             DbRequestXmlCooker.AttachCookItem("NewUse",
                SqlDbType.Int, 0, this.NewUse.ToString(),
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

            DbRequestXmlCooker.AttachCookItem("Code",
                SqlDbType.VarChar, 20, this.Code.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Class",
                SqlDbType.VarChar, 20, this.Class.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Plate",
                SqlDbType.VarChar, 10, this.Plate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VIN",
                SqlDbType.VarChar, 20, this.VIN.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Agent",
                SqlDbType.Char, 3, this.Agent.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPrice",
                SqlDbType.Float, 0, this.TotalPrice.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommercialUse",
                SqlDbType.Bit, 0, this.CommercialUse.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
            SqlDbType.Int, 0, this.Term.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isLeasing",
                SqlDbType.Bit, 0, this.isLeasing.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LicExpDate",
            SqlDbType.DateTime, 0, this.LicExpDate.ToString(),
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

        private XmlDocument GetInsertCompulsoryInsuranceQuoteXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[18];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.DateTime, 0, this.EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ExpirationDate",
            SqlDbType.DateTime, 0, this.ExpirationDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RegisterDate",
            SqlDbType.DateTime, 0, this.RegisterDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NewUse",
                SqlDbType.Int, 0, this.NewUse.ToString(),
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

            DbRequestXmlCooker.AttachCookItem("Code",
                 SqlDbType.VarChar, 20, this.Code.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Class",
                SqlDbType.VarChar, 20, this.Class.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Plate",
                SqlDbType.VarChar, 10, this.Plate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VIN",
                SqlDbType.VarChar, 20, this.VIN.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Agent",
                SqlDbType.Char, 3, this.Agent.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TotalPrice",
                SqlDbType.Float, 0, this.TotalPrice.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CommercialUse",
                SqlDbType.Bit, 0, this.CommercialUse.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Term",
            SqlDbType.Int, 0, this.Term.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LicExpDate",
                SqlDbType.DateTime, 0, this.LicExpDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isLeasing",
                SqlDbType.Bit, 0, this.isLeasing.ToString(),
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

        private static CompulsoryInsuranceQuote FillProperties(CompulsoryInsuranceQuote compulsoryInsuranceQuote)
        {
            compulsoryInsuranceQuote.CompulsoryInsuranceID = (int)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["CompulsoryInsuranceID"];
            compulsoryInsuranceQuote.TaskControlID = (int)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["TaskControlID"];
            compulsoryInsuranceQuote.EffectiveDate = ((DateTime)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["EffectiveDate"]).ToShortDateString();
            compulsoryInsuranceQuote.ExpirationDate = ((DateTime)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["ExpirationDate"]).ToShortDateString();
            compulsoryInsuranceQuote.RegisterDate = ((DateTime)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["RegisterDate"]).ToShortDateString();
            compulsoryInsuranceQuote.NewUse = (int)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["NewUse"];
            compulsoryInsuranceQuote.VehicleMakeID = (int)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["VehicleMakeID"];
            compulsoryInsuranceQuote.VehicleModelID = (int)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["VehicleModelID"];
            compulsoryInsuranceQuote.VehicleYearID = (int)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["VehicleYearID"];
            compulsoryInsuranceQuote.Code = (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["Code"] != System.DBNull.Value) ? (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["Code"].ToString()) : "";
            compulsoryInsuranceQuote.Class = (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["Class"] != System.DBNull.Value) ? (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["Class"].ToString()) : "";
            compulsoryInsuranceQuote.Plate = (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["Plate"] != System.DBNull.Value) ? (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["Plate"].ToString()) : "";
            compulsoryInsuranceQuote.VIN = (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["VIN"] != System.DBNull.Value) ? (compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["VIN"].ToString()) : "";
            compulsoryInsuranceQuote.TotalPrice = (double)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["TotalPrice"];
            compulsoryInsuranceQuote.CommercialUse = (bool)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["CommercialUse"];
            compulsoryInsuranceQuote.Term = (int)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["Term"];
            compulsoryInsuranceQuote.LicExpDate = ((DateTime)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["LicExpDate"]).ToShortDateString();
            compulsoryInsuranceQuote.isLeasing = (bool)compulsoryInsuranceQuote._dtCompulsoryInsuranceQuote.Rows[0]["isLeasing"];



            return compulsoryInsuranceQuote;
        }

        #region History

        private void History(int mode, int userID)
        {
            Audit.History history = new Audit.History();

            if (_mode == 2)
            {
                // Campos de TaskControl
                history.BuildNotesForHistory("TaskControlTypeID",
                    LookupTables.LookupTables.GetDescription("TaskControlType", oldCompulsoryInsuranceQuote.TaskControlTypeID.ToString()),
                    LookupTables.LookupTables.GetDescription("TaskControlType", this.TaskControlTypeID.ToString()),
                    mode);
                history.BuildNotesForHistory("TaskStatusID",
                    LookupTables.LookupTables.GetDescription("TaskStatus", oldCompulsoryInsuranceQuote.TaskStatusID.ToString()),
                    LookupTables.LookupTables.GetDescription("TaskStatus", this.TaskStatusID.ToString()),
                    mode);
                history.BuildNotesForHistory("ProspectID", oldCompulsoryInsuranceQuote.ProspectID.ToString(), this.ProspectID.ToString(), mode);
                history.BuildNotesForHistory("CustomerNo", oldCompulsoryInsuranceQuote.CustomerNo, this.CustomerNo, mode);
                history.BuildNotesForHistory("PolicyID", oldCompulsoryInsuranceQuote.PolicyID.ToString(), this.PolicyID.ToString(), mode);
                history.BuildNotesForHistory("PolicyClassID",
                    LookupTables.LookupTables.GetDescription("PolicyClass", oldCompulsoryInsuranceQuote.PolicyClassID.ToString()),
                    LookupTables.LookupTables.GetDescription("PolicyClass", this.PolicyClassID.ToString()),
                    mode);
                history.BuildNotesForHistory("Agency", oldCompulsoryInsuranceQuote.Agent, this.Agent, mode);
                history.BuildNotesForHistory("Agent", oldCompulsoryInsuranceQuote.Agent, this.Agent, mode);
                history.BuildNotesForHistory("Bank",
                    LookupTables.LookupTables.GetDescription("Bank", oldCompulsoryInsuranceQuote.Bank.ToString()),
                    LookupTables.LookupTables.GetDescription("Bank", this.Bank.ToString()),
                    mode);
                history.BuildNotesForHistory("InsuranceCompany", oldCompulsoryInsuranceQuote.InsuranceCompany, this.InsuranceCompany, mode);
                history.BuildNotesForHistory("Dealer", oldCompulsoryInsuranceQuote.Dealer, this.Dealer, mode);
                history.BuildNotesForHistory("CompanyDealer",
                    LookupTables.LookupTables.GetDescription("CompanyDealer", oldCompulsoryInsuranceQuote.CompanyDealer.ToString()),
                    LookupTables.LookupTables.GetDescription("CompanyDealer", this.CompanyDealer.ToString()),
                    mode);
                //history.BuildNotesForHistory("EntryDate",oldPayment.EntryDate.t,this.EntryDate,mode);
                history.BuildNotesForHistory("CloseDate", oldCompulsoryInsuranceQuote.CloseDate, this.CloseDate, mode);
                history.BuildNotesForHistory("EnteredBy", oldCompulsoryInsuranceQuote.EnteredBy, this.EnteredBy, mode);
                // Terminan Campos TaskControl

                history.BuildNotesForHistory("Effective Date", oldCompulsoryInsuranceQuote.EffectiveDate, this.EffectiveDate, mode);
                history.BuildNotesForHistory("Expiration Date", oldCompulsoryInsuranceQuote.ExpirationDate, this.ExpirationDate, mode);
                history.BuildNotesForHistory("Register Date", oldCompulsoryInsuranceQuote.RegisterDate, this.RegisterDate, mode);


                history.BuildNotesForHistory("NewUse",
                    LookupTables.LookupTables.GetDescription("NewUse", oldCompulsoryInsuranceQuote.NewUse.ToString()),
                    LookupTables.LookupTables.GetDescription("NewUse", this.NewUse.ToString()),
                    mode);
                history.BuildNotesForHistory("VehicleMake",
                    LookupTables.LookupTables.GetDescription("VehicleMake", oldCompulsoryInsuranceQuote.VehicleMakeID.ToString()),
                    LookupTables.LookupTables.GetDescription("VehicleMake", this.VehicleMakeID.ToString()),
                    mode);
                history.BuildNotesForHistory("VehicleModel",
                    LookupTables.LookupTables.GetDescription("VehicleModel", oldCompulsoryInsuranceQuote.VehicleModelID.ToString()),
                    LookupTables.LookupTables.GetDescription("VehicleModel", this.VehicleModelID.ToString()),
                    mode);
                history.BuildNotesForHistory("VehicleYear",
                    LookupTables.LookupTables.GetDescription("VehicleYear", oldCompulsoryInsuranceQuote.VehicleYearID.ToString()),
                    LookupTables.LookupTables.GetDescription("VehicleYear", this.VehicleYearID.ToString()),
                    mode);
                history.BuildNotesForHistory("Code", oldCompulsoryInsuranceQuote.Code.ToString(), this.Code.ToString(), mode);
                history.BuildNotesForHistory("Class", oldCompulsoryInsuranceQuote.Class.ToString(), this.Class.ToString(), mode);
                history.BuildNotesForHistory("Plate", oldCompulsoryInsuranceQuote.Plate.ToString(), this.Plate.ToString(), mode);
                history.BuildNotesForHistory("VIN", oldCompulsoryInsuranceQuote.VIN.ToString(), this.VIN.ToString(), mode);
                history.BuildNotesForHistory("TotalPrice", oldCompulsoryInsuranceQuote.TotalPrice.ToString(), this.TotalPrice.ToString(), mode);
                history.BuildNotesForHistory("Commercial Use", oldCompulsoryInsuranceQuote.CommercialUse.ToString(), this.CommercialUse.ToString(), mode);
                history.Actions = "EDIT";
            }
            else  //ADD & DELETE
            {
                history.BuildNotesForHistory("TaskControlID.", "", this.TaskControlID.ToString(), mode);
                history.Actions = "ADD";
            }

            history.KeyID = this.TaskControlID.ToString();
            history.Subject = "COMPULSORYINSURANCEQUOTE";
            history.UsersID = userID;
            history.GetSaveHistory();
        }


        private object SafeConvertToDateTime(string StringObject)
        {
            if (StringObject != string.Empty)
            {
                try { return DateTime.Parse(StringObject); }
                catch {/*Write to error logging sub-system.*/}
            }
            return StringObject;
        }

        #endregion

        #endregion
    }



    
}
