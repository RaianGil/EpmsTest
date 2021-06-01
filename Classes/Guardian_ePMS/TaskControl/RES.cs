using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Quotes;
using EPolicy.Audit;
using EPolicy.XmlCooker;
using EPolicy.TaskControl;
using UtilityComponents.RES;

namespace EPolicy.TaskControl
{
    public class RES : Policy
    {
        public RES(bool isQuote)
        {
            this.isQuote = isQuote;
            this.DepartmentID = 1;     //AutoGap
            this.PolicyClassID = 29;
            this.PolicyType = "RES";
            this.InsuranceCompany = "001";
            this.Agency = "000";
            this.Agent  = "000";
            this.Bank   = "000";
            this.Dealer = "000";
            this.CompanyDealer = "000";
            this.Status = "Inforce";
            this.TaskStatusID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskStatus", "Open"));
            this.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "RES").ToString());
            this.TotalPremium = 0.00;
            this.Term = 12;
            this.RESLiability = 0;
            //
            this.InsuredPremises = "";
            this.InsuredName = "";
            this.PartOccupied = "";
            this.Owner = 0;
            this.GeneralLesee = 0;
            this.Tenant = 0;
            this.Other = 0;
            this.Individual = 0;
            this.Partnership = 0;
            this.Corporation = 0;
            this.JoinVenture = 0;
            this.OtherTI = 0;
            this.PDLimit = 0;
            this.BILimit = 0;
            this.MedicalPayment = "";
            this.FireDamage = "";
            //
            this.TypePolicy = 0;


            if (this.isQuote)
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "RES Quote"));
            else
            {
                this.DepartmentID = 1;
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "RES"));
            }
            // Para el History
            this._mode = (int)TaskControlMode.ADD;
        }

        #region Variable

        private int _mode = (int)TaskControlMode.CLEAR;
        private DataTable _dtRES = null;
        private TaskControl _oldPolices = null;

        private string _InsuredPremises = "";
        //General Coverages
        private int _Owner = 0;
        private int _GeneralLesee = 0;
        private int _Tenant = 0;
        private int _Other = 0;
        //Type of Insured
        private int _Individual = 0;
        private int _Partnership = 0;
        private int _Corporation = 0;
        private int _JoinVenture = 0;
        private int _OtherTI = 0;
        private string _PartOccupied = "";
        private string _InsuredName = "";
        private int _BILimit = 0;
        private int _PDLimit = 0;
        private string _MedicalPayment = "";
        private string _FireDamage = "";
        private int _TypePolicy = 0;
        private bool _isQuote = false;
        private int _RequiredDocumentID = 0;
        private int _RESLiability = 0;
        private int _TypeIndex = 0;
        private int _BILimitIndex = 0;

        #endregion

        #region Properties


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

        public string InsuredPremises
        {
            get
            {
                return this._InsuredPremises;
            }
            set
            {
                this._InsuredPremises = value;
            }
        }
        //General Coverages
        public int Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                this._Owner = value;
            }
        }

        public int GeneralLesee
        {
            get
            {
                return this._GeneralLesee;
            }
            set
            {
                this._GeneralLesee = value;
            }
        }

        public int Tenant
        {
            get
            {
                return this._Tenant;
            }
            set
            {
                this._Tenant = value;
            }
        }
        public int Other
        {
            get
            {
                return this._Other;
            }
            set
            {
                this._Other = value;
            }
        }
        //Type of Insured
        public int Individual
        {
            get
            {
                return this._Individual;
            }
            set
            {
                this._Individual = value;
            }
        }
        public int Partnership
        {
            get
            {
                return this._Partnership;
            }
            set
            {
                this._Partnership = value;
            }
        }
        public int Corporation
        {
            get
            {
                return this._Corporation;
            }
            set
            {
                this._Corporation = value;
            }
        }
        public int JoinVenture
        {
            get
            {
                return this._JoinVenture;
            }
            set
            {
                this._JoinVenture = value;
            }
        }
        public int OtherTI
        {
            get
            {
                return this._OtherTI;
            }
            set
            {
                this._OtherTI = value;
            }
        }
        public string PartOccupied
        {
            get
            {
                return this._PartOccupied;
            }
            set
            {
                this._PartOccupied = value;
            }
        }
        public string InsuredName
        {
            get
            {
                return this._InsuredName;
            }
            set
            {
                this._InsuredName = value;
            }
        }

        public int BILimit
        {
            get
            {
                return this._BILimit;
            }
            set
            {
                this._BILimit = value;
            }
        }

        public int PDLimit
        {
            get
            {
                return this._PDLimit;
            }
            set
            {
                this._PDLimit = value;
            }
        }

        public string MedicalPayment
        {
            get
            {
                return this._MedicalPayment;
            }
            set
            {
                this._MedicalPayment = value;
            }
        }

        public string FireDamage
        {
            get
            {
                return this._FireDamage;
            }
            set
            {
                this._FireDamage = value;
            }
        }

        public int TypePolicy
        {
            get
            {
                return this._TypePolicy;
            }
            set
            {
                this._TypePolicy = value;
            }
        }

        public int RESLiability
        {
            get
            {
                return this._RESLiability;
            }
            set
            {
                this._RESLiability = value;
            }
        }

        #endregion

        #region Public Method

        public void SaveRES(int UserID)
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
                this.SaveRESQuoteDB(UserID, this.TaskControlID);
            }
            else
            {
                base.SavePol(UserID);	// Validate and Save Policy
                this.SaveRESDB(UserID, this.TaskControlID);
            }

            //this.SaveRequiredDocuments(UserID, this.TaskControlID);

            this._mode = (int)TaskControlMode.UPDATE;
            this.Mode = (int)TaskControlMode.CLEAR;

        }

 
        public void ValidateQuote()
        {
            string errorMessage = String.Empty;

            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
        }

        #endregion

        public static RES GetRES(int TaskControlID, bool isQuote)
        {
            RES RES = null;

            DataTable dt = GetRESByTaskControlIDDB(TaskControlID, isQuote);

            RES = new RES(isQuote);

            if (isQuote)
                RES = (RES)Policy.GetPolicyQuoteByTaskControlID(TaskControlID, RES);  //PolicyQuote
            else
                RES = (RES)Policy.GetPolicyByTaskControlID(TaskControlID, RES);  //Policy

            RES._dtRES = dt;

            RES = FillProperties(RES, TaskControlID, isQuote);

            return RES;
        }

        public static DataTable GetRESByTaskControlIDDB(int TaskControlID, bool isQuote)
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

            DataTable dt = exec.GetQuery("GetRESsByTaskControlID", xmlDoc);
            return dt;
        }

        private static RES FillProperties(RES RESTable, int taskControlID, bool isQuote)
        {
            try
            {
                RESTable.TaskControlID = int.Parse(RESTable._dtRES.Rows[0]["TaskControlID"].ToString());
                RESTable.InsuredPremises = RESTable._dtRES.Rows[0]["InsuredPremises"].ToString();
                RESTable.PartOccupied    = RESTable._dtRES.Rows[0]["PartOccupied"].ToString();
                RESTable.InsuredName = RESTable._dtRES.Rows[0]["InsuredName"].ToString();
                RESTable.BILimit    = int.Parse(RESTable._dtRES.Rows[0]["BILimit"].ToString());
                RESTable.PDLimit    = int.Parse(RESTable._dtRES.Rows[0]["PDLimit"].ToString());
                RESTable.TypePolicy = int.Parse(RESTable._dtRES.Rows[0]["TypePolicy"].ToString());
                //General Coverages
                RESTable.Owner        = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["Owner"].ToString());
                RESTable.GeneralLesee = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["GeneralLesee"].ToString());
                RESTable.Tenant       = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["Tenant"].ToString());
                RESTable.Other        = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["Other"].ToString());
                //Type of Insured
                RESTable.Individual = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["Individual"].ToString());
                RESTable.Partnership = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["Partnership"].ToString());
                RESTable.Corporation = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["Corporation"].ToString());
                RESTable.JoinVenture = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["JoinVenture"].ToString());
                RESTable.OtherTI = convert_bool_int.intReturn(RESTable._dtRES.Rows[0]["OtherTI"].ToString());
                //Valores Fijos
                RESTable.MedicalPayment = RESTable._dtRES.Rows[0]["MedicalPayment"].ToString();
                RESTable.FireDamage     = RESTable._dtRES.Rows[0]["FireDamage"].ToString();

                return RESTable;
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

        public void SaveRESDB(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


            Executor.BeginTrans();
            Executor.Insert("AddRES", this.GetInsertRESXml());

            Executor.CommitTrans();
        }

        private XmlDocument GetInsertRESXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[18];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InsuredPremises",
              SqlDbType.VarChar, 100, InsuredPremises.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Owner",
              SqlDbType.Int, 0, Owner.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("GeneralLesee",
             SqlDbType.Int, 0, GeneralLesee.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Tenant",
             SqlDbType.Int, 0, Tenant.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Other",
             SqlDbType.Int, 0, Other.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Individual",
             SqlDbType.Int, 0, Individual.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Partnership",
             SqlDbType.Int, 0, Partnership.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Corporation",
             SqlDbType.Int, 0, Corporation.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("JoinVenture",
             SqlDbType.Int, 0, JoinVenture.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherTI",
             SqlDbType.Int, 0, OtherTI.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PartOccupied",
              SqlDbType.VarChar, 50, PartOccupied.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InsuredName",
              SqlDbType.VarChar, 100, InsuredName.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BILimit",
              SqlDbType.Int, 0, BILimit.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PDLimit",
              SqlDbType.Int, 0, PDLimit.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment",
              SqlDbType.VarChar, 20, MedicalPayment.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FireDamage",
              SqlDbType.VarChar, 20, FireDamage.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TypePolicy",
                SqlDbType.Int, 0, TypePolicy.ToString(),
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

        public void SaveRESQuoteDB(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


            Executor.BeginTrans();
            Executor.Insert("AddRESQuote", this.GetInsertRESQuoteXml());

            Executor.CommitTrans();
        }
        private XmlDocument GetInsertRESQuoteXml()
        {
            //DbRequestXmlCookRequestItem = Cantidad de campos
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[18];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InsuredPremises",
              SqlDbType.VarChar, 100, InsuredPremises.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("InsuredName",
             SqlDbType.VarChar, 100, InsuredName.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Owner",
              SqlDbType.Int, 0, Owner.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("GeneralLesee",
             SqlDbType.Int, 0, GeneralLesee.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Tenant",
             SqlDbType.Int, 0, Tenant.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Other",
             SqlDbType.Int, 0, Other.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Individual",
             SqlDbType.Int, 0, Individual.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Partnership",
             SqlDbType.Int, 0, Partnership.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Corporation",
             SqlDbType.Int, 0, Corporation.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("JoinVenture",
             SqlDbType.Int, 0, JoinVenture.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("OtherTI",
             SqlDbType.Int, 0, OtherTI.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PartOccupied",
              SqlDbType.VarChar, 50, PartOccupied.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BILimit",
                SqlDbType.Int, 0, BILimit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PDLimit",
                SqlDbType.Int, 0, PDLimit.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("MedicalPayment",
                SqlDbType.VarChar, 20, MedicalPayment.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("FireDamage",
                SqlDbType.VarChar, 20, FireDamage.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TypePolicy",
                SqlDbType.Int, 0, TypePolicy.ToString(),
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

        #region Private Methods

        #endregion

    }
}
