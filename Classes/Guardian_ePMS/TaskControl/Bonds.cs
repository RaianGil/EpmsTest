using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Quotes;
using EPolicy.Audit;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
    public class Bonds : Policy
    {
        public Bonds(bool isQuote)
        {
            this.isQuote = isQuote;
            this.DepartmentID = 1;     //AutoGap
            this.PolicyClassID = 26;
            this.PolicyType = "BND";
            this.InsuranceCompany = "001";
            this.Agency = "000";
            this.Agent = "000";
            this.Bank = "000";
            this.Dealer = "000";
            this.CompanyDealer = "000";
            this.Status = "Inforce";
            this.TaskStatusID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskStatus", "Open"));
            this.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Bonds").ToString());
            this.TotalPremium = 0.00;
            this.Term = 12;

            if (this.isQuote)
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Bonds Quote"));
            else
            {
                this.DepartmentID = 1;
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Bonds"));
            }
            // Para el History
            this._mode = (int)TaskControlMode.ADD;
        }

        #region Variable

        private int _mode = (int)TaskControlMode.CLEAR;
        private DataTable _BondsCollection = null;
        private DataTable _BondsReqDocCollection = null;
        private DataTable _dtBonds = null;
        private TaskControl _oldPolices = null;

        private int _TypeOfBond = 0;
        private string _BondDescription = "";
        private string _BondRequiredDocuments = "";
        private string _Limits = "";
        private string _Obligee = "";
        private string _CompanyName = "";
        private int _CustomerType = 0;
        private string _AccountNumber = "";
        private string _NombreEstacion = "";
        private string _CantidadPrestadaSolicitante = "";
        private string _RenewalOfBnd = "";
        private string _Signature = "";
        private double _PaymentAmount = 0.0;

        private bool _isQuote = false;
        private int _RequiredDocumentID = 0;

        #endregion

        #region Properties

        public DataTable BondsReqDocCollection
        {
            get
            {
                if (this._BondsReqDocCollection == null)
                    this._BondsReqDocCollection = DataTableReqDocumentsTemp();
                return (this._BondsReqDocCollection);
            }
            set
            {
                this._BondsReqDocCollection = value;
            }
        }

        public bool isQuote
        {
            get
            {
                return this._isQuote;
            }
            set
            {
                this._isQuote = value;
            }
        }

        public int TypeOfBond
        {
            get
            {
                return this._TypeOfBond;
            }
            set
            {
                this._TypeOfBond = value;
            }
        }

        public string BondDescription
        {
            get
            {
                return this._BondDescription;
            }
            set
            {
                this._BondDescription = value;
            }
        }

        public string BondRequiredDocuments
        {
            get
            {
                return this._BondRequiredDocuments;
            }
            set
            {
                this._BondRequiredDocuments = value;
            }
        }

        public string Limits
        {
            get
            {
                return this._Limits;
            }
            set
            {
                this._Limits = value;
            }
        }

        public string Obligee
        {
            get
            {
                return this._Obligee;
            }
            set
            {
                this._Obligee = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                this._CompanyName = value;
            }
        }

        public int CustomerType
        {
            get
            {
                return this._CustomerType;
            }
            set
            {
                this._CustomerType = value;
            }
        }

        public string AccountNumber
        {
            get
            {
                return this._AccountNumber;
            }
            set
            {
                this._AccountNumber = value;
            }
        }

        public string NombreEstacion
        {
            get
            {
                return this._NombreEstacion;
            }
            set
            {
                this._NombreEstacion = value;
            }
        }

        public string CantidadPrestadaSolicitante
        {
            get
            {
                return this._CantidadPrestadaSolicitante;
            }
            set
            {
                this._CantidadPrestadaSolicitante = value;
            }
        }

        public string RenewalOfBnd
        {
            get
            {
                return this._RenewalOfBnd;
            }
            set
            {
                this._RenewalOfBnd = value;
            }
        }

        public string Signature
        {
            get
            {
                return this._Signature;
            }
            set
            {
                this._Signature = value;
            }
        }

        public double PaymentAmount
        {
            get
            {
                return this._PaymentAmount;
            }
            set
            {
                this._PaymentAmount = value;
            }
        }

        #endregion

        #region Public Method

        //public void SaveBonds(int UserID)
        //{
        //    this._mode = (int)this.Mode;        // Se le asigna el mode de taskControl.
        //    this.PolicyMode = (int)this.Mode;   // Se le asigna el mode de taskControl.

        //    this.Validate();

        //    if (this.Agent == "000")
        //        this.Agent = "No Agent";

        //    base.ValidatePolicy();

        //    if (this.Agent == "No Agent")
        //        this.Agent = "000";
        //    // Se utiliza para el History
        //    //if (this._mode ==2)
        //    //if (this._mode == 2)
        //    //   oldPolices = (Policies)Policies.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

        //    //Si el usuario cambio la prima manualmente, no debe calcular la misma.
        //    if (this.TotalPremium == 0)
        //    {
        //        //CalculatePremium();
        //    }

        //    if (this.Customer.CustomerNo.Trim() == "")
        //        this.Customer.Mode = 1;
        //    else
        //        this.Customer.Mode = 2;

        //    this.Customer.IsBusiness = false;
        //    this.Customer.Save(UserID);

        //    this.CustomerNo = this.Customer.CustomerNo;
        //    this.ProspectID = this.Customer.ProspectID;

        //    base.Save();
        //    if (isQuote) // AQUI
        //        base.SaveOPPQuote(UserID);    // Validate and Save Quote in Policy Class
        //    else
        //        base.SavePol(UserID);	// Validate and Save Policy

        //    this.SaveBondsDB(UserID, this.TaskControlID);

        //    //this.SaveAutoGuardianXtra(UserID, this.TaskControlID);  // Salva la info del auto

        //    this._mode = (int)TaskControlMode.UPDATE;
        //    this.Mode = (int)TaskControlMode.CLEAR;
        //    //FillProperties(this);
        //}

        public void SaveBonds(int UserID)
        {
            this._mode = (int)this.Mode;  // Se le asigna el mode de taskControl.
            this.PolicyMode = (int)this.Mode;  // Se le asigna el mode de taskControl.

            if (isQuote)
            {
                this.ValidateQuote();
                if (this.Prospect.ProspectID == 0)
                    this.Prospect.Mode = 1;
                else
                    this.Prospect.Mode = 2;

                this.Prospect.IsBusiness = false;
                this.Prospect.LocationID = this.OriginatedAt;
                this.Prospect.SaveProspect(UserID);

                this.ProspectID = this.Prospect.ProspectID;

                //if (IsEndorsement)
                //{
                if (this.Customer.CustomerNo.Trim() == "")
                    this.Customer.Mode = 1;
                else
                    this.Customer.Mode = 2;

                this.Customer.IsBusiness = false;
                this.Customer.Save(UserID);

                this.CustomerNo = this.Customer.CustomerNo;
                this.ProspectID = this.Customer.ProspectID;
                //}
            }
            else
            {
                this.Validate();
                base.ValidatePolicy();

                //if (this._mode == 2)
                //    oldAutos = (Autos)Autos.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

                if (this.Customer.CustomerNo.Trim() == "")
                    this.Customer.Mode = 1;
                else
                    this.Customer.Mode = 2;

                this.Customer.IsBusiness = false;
                this.Customer.Save(UserID);

                this.CustomerNo = this.Customer.CustomerNo;
                this.ProspectID = this.Customer.ProspectID;
            }

            base.Save(); // AQUI

            if (isQuote) // AQUI
            {
                base.SaveOPPQuote(UserID);    // Validate and Save Quote in Policy Class
                this.SaveBondsQuoteDB(UserID, this.TaskControlID);
            }
            else
            {
                base.SavePol(UserID);	// Validate and Save Policy
                this.SaveBondsDB(UserID, this.TaskControlID);
            }

            this.SaveRequiredDocuments(UserID, this.TaskControlID);

            this._mode = (int)TaskControlMode.UPDATE;
            this.Mode = (int)TaskControlMode.CLEAR;

        }

        public void SaveRequiredDocuments(int UserID, int taskControlID)
        {
            DBRequest Executor = new DBRequest();
            Executor.Update("DeleteBondRequiredDocumentByTaskControlID", DeleteRequiredDocumentsByTaskControlIDXml(taskControlID));

            for (int i = 0; i < BondsReqDocCollection.Rows.Count; i++)
            {
                this.Mode = 1; //Add

                Executor.BeginTrans();
                //this.VehicleID = Executor.Insert("AddVehicleDetail", this.GetInsertVehicleDetailXml(i));
                Executor.Insert("AddBondRequiredDocument", this.GetInsertRequiredDocumentsXml(i));
                Executor.CommitTrans();
            }
        }


        private XmlDocument DeleteRequiredDocumentsByTaskControlIDXml(int taskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
            SqlDbType.Int, 0, taskControlID.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
            SqlDbType.Bit, 0, isQuote.ToString(),
            ref cookItems);

            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            return xmlDoc;
        }

        private XmlDocument GetInsertRequiredDocumentsXml(int index)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[4];


            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);


            DbRequestXmlCooker.AttachCookItem("RequiredDocumentDesc",
            SqlDbType.VarChar, 500, BondsReqDocCollection.Rows[index]["RequiredDocumentDesc"].ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
            SqlDbType.Bit, 0, isQuote.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Checked",
            SqlDbType.Bit, 0, BondsReqDocCollection.Rows[index]["Checked"].ToString(),
            ref cookItems);

            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            return xmlDoc;
        }

        public void ValidateQuote()
        {
            string errorMessage = String.Empty;

            //if (this.EffectiveDate == "")
            //    errorMessage = "Effective Date is missing or wrong.";
            //else
            //    if (this.Term == 0)
            //        errorMessage = "Term is missing or wrong.";
            //    else
            //        if (this.Prospect.FirstName == "") // && this.isBusinessPolicy == false)
            //            errorMessage = "First Name is missing or wrong.";
            //        //else
            //        //    if (this.Prospect.LastName1 == "")
            //        //        errorMessage = "Last Name is missing or wrong.";
            //        else
            //            if (this.OriginatedAt == 0)
            //                errorMessage = "Originated is missing.";
            //            else
            //                if (this.TotalPremium == 0)
            //                    errorMessage = "TotalPremium must be greater than 0.";

            //throw the exception.
            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
        }

        #endregion

        public static Bonds GetBonds(int TaskControlID, bool isQuote)
        {
            Bonds Bnd = null;

            DataTable dt = GetBondsByTaskControlIDDB(TaskControlID, isQuote);

            Bnd = new Bonds(isQuote);

            if (isQuote)
                Bnd = (Bonds)Policy.GetPolicyQuoteByTaskControlID(TaskControlID, Bnd);  //PolicyQuote
            else
                Bnd = (Bonds)Policy.GetPolicyByTaskControlID(TaskControlID, Bnd);  //Policy

            Bnd._dtBonds = dt;

            Bnd = FillProperties(Bnd, TaskControlID, isQuote);

            return Bnd;
        }

        public static DataTable GetBondsByTaskControlIDDB(int TaskControlID, bool isQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsQuote",
                SqlDbType.Bit, 0, isQuote.ToString(),
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

            DataTable dt = exec.GetQuery("GetBondsByTaskControlID", xmlDoc);
            return dt;
        }

        private static Bonds FillProperties(Bonds bnd, int taskControlID, bool isQuote)
        {
            try
            {
                bnd.TaskControlID = int.Parse(bnd._dtBonds.Rows[0]["TaskControlID"].ToString());
                bnd.TypeOfBond = int.Parse(bnd._dtBonds.Rows[0]["TypeOfBondID"].ToString());
                bnd.BondRequiredDocuments = bnd._dtBonds.Rows[0]["RequiredDocuments"].ToString();
                bnd.BondDescription = bnd._dtBonds.Rows[0]["DescriptionOfBond"].ToString();
                bnd.Limits = bnd._dtBonds.Rows[0]["Limits"].ToString();
                bnd.Obligee = bnd._dtBonds.Rows[0]["Obligee"].ToString();
                bnd.CompanyName = bnd._dtBonds.Rows[0]["CompanyName"].ToString();
                bnd.CustomerType = int.Parse(bnd._dtBonds.Rows[0]["CustomerType"].ToString());
                bnd.AccountNumber = bnd._dtBonds.Rows[0]["AccNumber"].ToString();
                bnd.NombreEstacion = bnd._dtBonds.Rows[0]["NombreEstacion"].ToString();
                bnd.CantidadPrestadaSolicitante = bnd._dtBonds.Rows[0]["CantidadPrestadaSolicitante"].ToString();
                bnd.RenewalOfBnd = bnd._dtBonds.Rows[0]["RenewalOfBnd"].ToString();
                bnd.Signature = bnd._dtBonds.Rows[0]["Signature"].ToString();
                if (bnd._dtBonds.Rows[0]["PaymentAmount"].ToString() == "")
                {
                    bnd.PaymentAmount = 0.0;
                }
                else
                {
                    bnd.PaymentAmount = double.Parse(bnd._dtBonds.Rows[0]["PaymentAmount"].ToString());
                }
                bnd._BondsReqDocCollection = GetReqDocumentsByTaskControlID(taskControlID, isQuote);

                return bnd;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not fill properties. ", ex);
            }
        }

        public static DataTable GetReqDocumentsByTaskControlID(int TaskControlID, bool isQuote)
        {
            DataTable dt = GetReqDocumentsByTaskControlIDDB(TaskControlID, isQuote);
            return dt;
        }

        private static DataTable GetReqDocumentsByTaskControlIDDB(int taskControlID, bool isQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("isQuote",
                SqlDbType.Bit, 0, isQuote.ToString(),
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

            DataTable dt = exec.GetQuery("GetReqDocumentsByTaskControlID", xmlDoc);

            return dt;
        }

        public void SaveBondsDB(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


            Executor.BeginTrans();
            Executor.Insert("AddBonds", this.GetInsertBondsXml());

            Executor.CommitTrans();
        }

        private XmlDocument GetInsertBondsXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[13];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TypeOfBondID",
              SqlDbType.Int, 0, TypeOfBond.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DescriptionOfBond",
              SqlDbType.VarChar, 5000, BondDescription.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limits",
             SqlDbType.VarChar, 50, Limits.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Obligee",
             SqlDbType.VarChar, 500, Obligee.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CompanyName",
             SqlDbType.VarChar, 100, CompanyName.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CustomerType",
              SqlDbType.Int, 0, CustomerType.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AccNumber",
              SqlDbType.VarChar, 500, AccountNumber.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NombreEstacion",
              SqlDbType.VarChar, 500, NombreEstacion.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CantidadPrestadaSolicitante",
              SqlDbType.VarChar, 50, CantidadPrestadaSolicitante.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalOfBnd",
              SqlDbType.VarChar, 50, RenewalOfBnd.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Signature",
              SqlDbType.VarChar, 50, Signature.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmount",
             SqlDbType.Float, 50, PaymentAmount.ToString(),
             ref cookItems);

            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            return xmlDoc;
        }

        public void SaveBondsQuoteDB(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


            Executor.BeginTrans();
            Executor.Insert("AddBondsQuote", this.GetInsertBondsQuoteXml());

            Executor.CommitTrans();
        }
        private XmlDocument GetInsertBondsQuoteXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[13];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TypeOfBondID",
              SqlDbType.Int, 0, TypeOfBond.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DescriptionOfBond",
              SqlDbType.VarChar, 5000, BondDescription.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limits",
             SqlDbType.VarChar, 50, Limits.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Obligee",
             SqlDbType.VarChar, 500, Obligee.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CompanyName",
             SqlDbType.VarChar, 100, CompanyName.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CustomerType",
              SqlDbType.Int, 0, CustomerType.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AccNumber",
                SqlDbType.VarChar, 500, AccountNumber.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NombreEstacion",
                SqlDbType.VarChar, 500, NombreEstacion.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("CantidadPrestadaSolicitante",
                SqlDbType.VarChar, 50, CantidadPrestadaSolicitante.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("RenewalOfBnd",
                SqlDbType.VarChar, 50, RenewalOfBnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Signature",
            SqlDbType.VarChar, 50, Signature.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PaymentAmount",
             SqlDbType.Float, 0, PaymentAmount.ToString(),
             ref cookItems);


            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            return xmlDoc;
        }


        private DataTable DataTableReqDocumentsTemp()
        {
            DataTable myDataTable = new DataTable("DataTableVehicleTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "BondRequiredDocumentID";
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementSeed = 1;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.AllowDBNull = false;
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = true;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "TaskControlID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TaskControlID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "RequiredDocumentDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "RequiredDocumentDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Checked";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Checked";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }


        #region Private Methods

        #endregion

    }
}
