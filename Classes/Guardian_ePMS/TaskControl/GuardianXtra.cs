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
    public class GuardianXtra:Policy
    {
        public GuardianXtra()
        {
            this.DepartmentID = 1;     //AutoGap
            this.PolicyClassID = 23;
            this.PolicyType = "XPA";
            this.InsuranceCompany = "001";
            this.Agency = "002";
            this.Agent = "000";
            this.Bank = "000";
            this.Dealer = "000";
            this.CompanyDealer = "000";
            this.Status = "Inforce";
            this.TaskStatusID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskStatus", "Open"));
            this.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "GuardianXtra").ToString());
            this.TotalPremium = 0.00;
            this.Term = 12;
            // Para el History
            this._mode = (int)TaskControlMode.ADD;
        }

        #region Variable
        private int _VehicleMakeID = 0;
        private int _VehicleModelID = 0;
        private int _VehicleYearID = 0;
        private string _VIN = "";
        private string _Plate = "";
        private int _NewUse = 0;
        private int _GuardianXtraID = 0;

        private int _mode = (int)TaskControlMode.CLEAR;

        private DataTable _GuardianXtraCollection = null;

        private DataTable _dtGuardianXtra = null;
        private TaskControl _oldPolices = null;

        /// Variables for Auto
        private string _XtraVIN = "";
        private int _XtraVehicleMakeID = 0;
        private int _XtraVehicleModelID = 0;
        private int _XtraVehicleYearID = 0;
        private string _XtraPlate = "";
        private string _XtraVehicleMake = "";
        private string _XtraVehicleModel = "";
        private string _XtraVehicleYear = "";
        private double _XtraPremium = 0;
        private string _XtraHasCoverageExplain = "";
        private bool _XtraHasCoverage = false;
        private bool _XtraDefferedPayment = false;
        private bool _XtraIsFourPayment = false;
        private bool _XtraIsSixPayment = false;
        private bool _XtraIsCommercialAuto = false;
        private bool _XtraIsCreditPayment = false;
        private bool _XtraIsDebitPayment = false;
        private bool _XtraIsPersonalAuto = false;
        private bool _XtraIsCashPayment = false;

        #endregion

        #region Properties

        public TaskControl oldPolices
        {
            get { return this._oldPolices; }
            set { this._oldPolices = value; }
        }

        public DataTable dtGuardianXtra
        {
            get { return this._dtGuardianXtra; }
            set { this._dtGuardianXtra = value; }
        }
        public int GuardianXtraID
        {
            get { return this._GuardianXtraID; }
            set { this._GuardianXtraID = value; }
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

        public int NewUsed
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

        /// XtraAuto

        public string XtraVIN
        {
            get
            {
                return this._XtraVIN;
            }
            set
            {
                this._XtraVIN = value;
            }
        }

        public int XtraVehicleMakeID
        {
            get
            {
                return this._XtraVehicleMakeID;
            }
            set
            {
                this._XtraVehicleMakeID = value;
            }
        }

        public int XtraVehicleModelID
        {
            get
            {
                return this._XtraVehicleModelID;
            }
            set
            {
                this._XtraVehicleModelID = value;
            }
        }

        public int XtraVehicleYearID
        {
            get
            {
                return this._XtraVehicleYearID;
            }
            set
            {
                this._XtraVehicleYearID = value;
            }
        }

        public string XtraPlate
        {
            get
            {
                return this._XtraPlate;
            }
            set
            {
                this._XtraPlate = value;
            }
        }

        public string XtraVehicleMake
        {
            get
            {
                return this._XtraVehicleMake;
            }
            set
            {
                this._XtraVehicleMake = value;
            }
        }

        public string XtraVehicleModel
        {
            get
            {
                return this._XtraVehicleModel;
            }
            set
            {
                this._XtraVehicleModel = value;
            }
        }

        public string XtraVehicleYear
        {
            get
            {
                return this._XtraVehicleYear;
            }
            set
            {
                this._XtraVehicleYear = value;
            }
        }

        public double XtraPremium
        {
            get
            {
                return this._XtraPremium;
            }
            set
            {
                this._XtraPremium = value;
            }
        }

        public string XtraHasCoverageExplain
        {
            get
            {
                return this._XtraHasCoverageExplain;
            }
            set
            {
                this._XtraHasCoverageExplain = value;
            }
        }
        public bool XtraHasCoverage
        {
            get
            {
                return this._XtraHasCoverage;
            }
            set
            {
                this._XtraHasCoverage = value;
            }
        }
        public bool XtraDefferedPayment
        {
            get
            {
                return this._XtraDefferedPayment;
            }
            set
            {
                this._XtraDefferedPayment = value;
            }
        }
        public bool XtraIsFourPayment
        {
            get
            {
                return this._XtraIsFourPayment;
            }
            set
            {
                this._XtraIsFourPayment = value;
            }
        }
        public bool XtraIsSixPayment
        {
            get
            {
                return this._XtraIsSixPayment;
            }
            set
            {
                this._XtraIsSixPayment = value;
            }
        }

        public bool XtraIsCommercialAuto
        {
            get
            {
                return this._XtraIsCommercialAuto;
            }
            set
            {
                this._XtraIsCommercialAuto = value;
            }
        }

        public bool XtraIsCreditPayment
        {
            get
            {
                return this._XtraIsCreditPayment;
            }
            set
            {
                this._XtraIsCreditPayment = value;
            }
        }

        public bool XtraIsDebitPayment
        {
            get
            {
                return this._XtraIsDebitPayment;
            }
            set
            {
                this._XtraIsDebitPayment = value;
            }
        }

        public bool XtraIsCashPayment
        {
            get
            {
                return this._XtraIsCashPayment;
            }
            set
            {
                this._XtraIsCashPayment = value;
            }
        }

        public bool XtraIsPersonalAuto
        {
            get
            {
                return this._XtraIsPersonalAuto;
            }
            set
            {
                this._XtraIsPersonalAuto = value;
            }
        }

        #endregion

        #region Public Method

        public void SaveGuardianX(int UserID)
        {
            this.SaveGuardianXtra(UserID);
        }

        public void SaveGuardianXtra(int UserID)
        {
            this._mode = (int)this.Mode;        // Se le asigna el mode de taskControl.
            this.PolicyMode = (int)this.Mode;   // Se le asigna el mode de taskControl.

            this.Validate();

            if (this.Agent == "000")
                this.Agent = "No Agent";
            
            base.ValidatePolicy();

            if (this.Agent == "No Agent")
                this.Agent = "000";
            // Se utiliza para el History
            //if (this._mode ==2)
            //if (this._mode == 2)
            //   oldPolices = (Policies)Policies.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

            //Si el usuario cambio la prima manualmente, no debe calcular la misma.
            if (this.TotalPremium == 0)
            {
                //CalculatePremium();
            }

            if (this.Customer.CustomerNo.Trim() == "")
                this.Customer.Mode = 1;
            else
                this.Customer.Mode = 2;

            this.Customer.IsBusiness = false;
            this.Customer.Save(UserID);

            this.CustomerNo = this.Customer.CustomerNo;
            this.ProspectID = this.Customer.ProspectID;

            base.Save();
            base.SavePol(UserID);	// Validate and Save Policy

            //SaveGuardianXtraPolicies(UserID);  // Save Road Policies

            this.SaveAutoGuardianXtra(UserID, this.TaskControlID);  // Salva la info del auto

            this._mode = (int)TaskControlMode.UPDATE;
            this.Mode = (int)TaskControlMode.CLEAR;
            //FillProperties(this);
        }

        public static GuardianXtra GetGuardianXtra(int TaskControlID)
        {
            GuardianXtra GuardianXtra = null;

            DataTable dt = GetGuardianXtraByTaskControlID(TaskControlID);

            GuardianXtra = new GuardianXtra();
            GuardianXtra = (GuardianXtra)Policy.GetPolicyByTaskControlID(TaskControlID, GuardianXtra);

            GuardianXtra = FillProperties(GuardianXtra);

            return GuardianXtra;
        }

        private static GuardianXtra FillProperties(GuardianXtra guardianXtra)
        {

            guardianXtra.GuardianXtraCollection = GuardianXtra.GetAutoGuardianXtraByTaskControlID(guardianXtra.TaskControlID);
            return guardianXtra;
        }

        public static DataTable GetGuardianXtraByTaskControlID(int TaskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
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

            DataTable dt = exec.GetQuery("GetGuardianXtraIDByTaskControlID", xmlDoc);
            return dt;
        }

        private void SaveGuardianXtraPolicies(int UserID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this.Mode)
                {
                    case 1:  //ADD
                        Executor.Insert("AddRoadAssistance", this.GetInsertPoliciesXml());
                        //this.History(this._mode, UserID);
                        break;

                    case 3:  //DELETE
                        //Executor.Update("DeleteAutoGuardServicesContract",this.GetDeletePoliciesXml());
                        break;

                    case 4:  //CLEAR						
                        break;

                    default: //UPDATE
                        //  this.History(this._mode, UserID);
                        Executor.Update("AddRoadAssistance", this.GetInsertPoliciesXml());
                        break;
                }
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to save the Policy. " + xcp.Message, xcp);
            }
        }

        private XmlDocument GetInsertPoliciesXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
              new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
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

        public void SaveAutoGuardianXtra(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


            Executor.BeginTrans();
            Executor.Insert("AddAutoGuardianXtra", this.GetInsertAutoGuardianXtraXml());

            Executor.CommitTrans();
        }

        #endregion

        #region GuardianXtra Autos

        private XmlDocument GuardianXtraTaskControlXml(int taskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
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

        private XmlDocument GetInsertAutoGuardianXtraXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[20];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VIN",
              SqlDbType.Char, 17, XtraVIN.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
              SqlDbType.Int, 0, XtraVehicleMakeID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleModelID",
             SqlDbType.Int, 0, XtraVehicleModelID.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleYearID",
              SqlDbType.Int, 0, XtraVehicleYearID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Plate",
             SqlDbType.VarChar, 7, XtraPlate.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleMake",
             SqlDbType.VarChar, 50, XtraVehicleMake.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleModel",
             SqlDbType.VarChar, 50, XtraVehicleModel.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleYear",
             SqlDbType.Int, 0, XtraVehicleYear.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium",
             SqlDbType.Float, 0, XtraPremium.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HasCoverageExplain",
             SqlDbType.VarChar, 100, XtraHasCoverageExplain.ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("HasCoverage",
           SqlDbType.Bit, 0, XtraHasCoverage.ToString(),
           ref cookItems);

            DbRequestXmlCooker.AttachCookItem("DefferedPayment",
           SqlDbType.Bit, 0, XtraDefferedPayment.ToString(),
           ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsFourPayment",
           SqlDbType.Bit, 0, XtraIsFourPayment.ToString(),
           ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsSixPayment",
           SqlDbType.Bit, 0, XtraIsSixPayment.ToString(),
           ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsCommercialAuto",
            SqlDbType.Bit, 0, XtraIsCommercialAuto.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsPersonalAuto",
            SqlDbType.Bit, 0, XtraIsPersonalAuto.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsCreditPayment",
            SqlDbType.Bit, 0, XtraIsCreditPayment.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsDebitPayment",
            SqlDbType.Bit, 0, XtraIsDebitPayment.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsCashPayment",
            SqlDbType.Bit, 0, XtraIsCashPayment.ToString(),
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

        public static DataTable GetAutoGuardianXtraByTaskControlID(int TaskControlID)
        {
            DataTable dt = GetAutoGuardianXtraByTaskControlIDDB(TaskControlID);
            return dt;
        }

        private static DataTable GetAutoGuardianXtraByTaskControlIDDB(int taskControlID)
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

            DataTable dt = exec.GetQuery("GetAutoGuardianXtraByTaskControlID", xmlDoc);

            return dt;
        }

        public DataTable GuardianXtraCollection
        {
            get
            {
                if (this._GuardianXtraCollection == null)
                    this._GuardianXtraCollection = DataTableRoadAssistTemp();
                return (this._GuardianXtraCollection);
            }
            set
            {
                this._GuardianXtraCollection = value;
            }
        }

        private DataTable DataTableRoadAssistTemp()
        {
            DataTable myDataTable = new DataTable("DataTableRoadAssistTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "AsistenciaCarreteraAutoID";
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
            myDataColumn.ColumnName = "VIN";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VIN";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleMakeID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleMakeID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleModelID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleModelID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "VehicleYearID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleYearID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "NewUse";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NewUse";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Plate";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Plate";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleMake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleMake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleModel";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleModel";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "VehicleYear";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "VehicleYear";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NewUseDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NewUseDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;

        }

        // HISTORY 
        private void History(int mode, int userID)
        {
            Audit.History history = new Audit.History();
            if (this._mode == 2)
                oldPolices = (TaskControl)TaskControl.GetTaskControlByTaskControlID(this.TaskControlID, userID);
            //              oldPolices = (Policies)Policies.GetTaskControlByTaskControlID(this.TaskControlID, userID);

            if (_mode == 2)
            {
                // Campos de TaskControl
                history.BuildNotesForHistory("TaskControlTypeID",
                    LookupTables.LookupTables.GetDescription("TaskControlType", oldPolices.TaskControlTypeID.ToString()),
                    LookupTables.LookupTables.GetDescription("TaskControlType", this.TaskControlTypeID.ToString()),
                    mode);
                history.BuildNotesForHistory("TaskStatusID",
                    LookupTables.LookupTables.GetDescription("TaskStatus", oldPolices.TaskStatusID.ToString()),
                    LookupTables.LookupTables.GetDescription("TaskStatus", this.TaskStatusID.ToString()),
                    mode);
                history.BuildNotesForHistory("ProspectID", oldPolices.ProspectID.ToString(), this.ProspectID.ToString(), mode);
                history.BuildNotesForHistory("CustomerNo", oldPolices.CustomerNo, this.CustomerNo, mode);
                history.BuildNotesForHistory("PolicyID", oldPolices.PolicyID.ToString(), this.PolicyID.ToString(), mode);
                history.BuildNotesForHistory("PolicyClassID",
                    LookupTables.LookupTables.GetDescription("PolicyClass", oldPolices.PolicyClassID.ToString()),
                    LookupTables.LookupTables.GetDescription("PolicyClass", this.PolicyClassID.ToString()),
                    mode);
                history.BuildNotesForHistory("Agency", oldPolices.Agent, this.Agent, mode);
                history.BuildNotesForHistory("Agent", oldPolices.Agent, this.Agent, mode);
                history.BuildNotesForHistory("SupplierID", oldPolices.SupplierID, this.SupplierID, mode);
                history.BuildNotesForHistory("Bank",
                    LookupTables.LookupTables.GetDescription("Bank", oldPolices.Bank.ToString()),
                    LookupTables.LookupTables.GetDescription("Bank", this.Bank.ToString()),
                    mode);
                history.BuildNotesForHistory("InsuranceCompany",
                    LookupTables.LookupTables.GetDescription("InsuranceCompany", oldPolices.InsuranceCompany.ToString()),
                    LookupTables.LookupTables.GetDescription("InsuranceCompany", this.InsuranceCompany.ToString()),
                    mode);
                history.BuildNotesForHistory("Dealer", oldPolices.Dealer, this.Dealer, mode);
                history.BuildNotesForHistory("CompanyDealer",
                    LookupTables.LookupTables.GetDescription("CompanyDealer", oldPolices.CompanyDealer.ToString()),
                    LookupTables.LookupTables.GetDescription("CompanyDealer", this.CompanyDealer.ToString()),
                    mode);
                history.BuildNotesForHistory("CloseDate", oldPolices.CloseDate, this.CloseDate, mode);
                history.BuildNotesForHistory("EnteredBy", oldPolices.EnteredBy, this.EnteredBy, mode);

                history.BuildNotesForHistory("FirstName", oldPolices.Customer.FirstName, this.Customer.FirstName, mode);
                history.BuildNotesForHistory("LastName1", oldPolices.Customer.LastName1, this.Customer.LastName1, mode);
                history.BuildNotesForHistory("LastName2", oldPolices.Customer.LastName2, this.Customer.LastName2, mode);

                history.Actions = "EDIT";
            }
            else  //ADD & DELETE
            {
                history.BuildNotesForHistory("TaskControlID.", "", this.TaskControlID.ToString(), mode);
                history.Actions = "ADD";
            }

            history.KeyID = this.TaskControlID.ToString();
            history.Subject = "POLICIES";
            history.UsersID = userID;
            history.GetSaveHistory();
        }

        #endregion
    }
}
