using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.Customer;
using EPolicy.LookupTables;
using EPolicy.Quotes;
using EPolicy.Audit;
using EPolicy.XmlCooker;


using System.Web;
using System.Web.SessionState;
using System.Web.UI;





namespace EPolicy.TaskControl
{
    public class RoadAssistance:Policy
    {
        public RoadAssistance()
        {
            this.DepartmentID = 1;     //AutoGap
            this.PolicyClassID = 21;
            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp.UserID).ToString()).Contains("THOMAS"))
            {
                this.PolicyType = "GVI";

            }
            else {
                this.PolicyType = "GPR"; 
            }
            
            this.InsuranceCompany = "001";
            this.Agency = "000";
            this.Agent = "000";
            this.Bank = "000";
            this.Dealer = "000";
            this.CompanyDealer = "000";
            this.Status = "Inforce";
            this.TaskStatusID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskStatus", "Open"));
            this.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "GuardianRoadAssit").ToString());
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
        private int _RoadAssistID = 0;

        private int _mode = (int)TaskControlMode.CLEAR;

        private DataTable _RoadAssistCollection = null;

        private DataTable _dtRoadAssistance = null;
        private TaskControl _oldPolices = null;

        private bool _IsCreditPayment = false;
        private bool _IsDebitPayment = false;
        private bool _IsCashPayment = false;

        #endregion

        #region Properties

        public TaskControl oldPolices
        {
            get { return this._oldPolices; }
            set { this._oldPolices = value; }
        }

        public DataTable dtRoadAssistance
        {
            get { return this._dtRoadAssistance; }
            set { this._dtRoadAssistance = value; }
        }
        public int RoadAssistID
        {
            get { return this._RoadAssistID; }
            set { this._RoadAssistID = value; }
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


        public bool IsCreditPayment
        {
            get
            {
                return this._IsCreditPayment;
            }
            set
            {
                this._IsCreditPayment = value;
            }
        }

        public bool IsDebitPayment
        {
            get
            {
                return this._IsDebitPayment;
            }
            set
            {
                this._IsDebitPayment = value;
            }
        }

        public bool IsCashPayment
        {
            get
            {
                return this._IsCashPayment;
            }
            set
            {
                this._IsCashPayment = value;
            }
        }

        #endregion

        #region Public Method

        public void SaveRoadAssistance(int UserID)
        {
            this.SaveRoadAssist(UserID);
        }

        public  void SaveRoadAssist(int UserID)
        {
            this._mode = (int)this.Mode;  // Se le asigna el mode de taskControl.
            this.PolicyMode = (int)this.Mode;  // Se le asigna el mode de taskControl.

           this.Validate();
           base.ValidatePolicy();

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

            SaveRoadAssistancePolicies(UserID);  // Save Road Policies

            this.SaveAutoRoadAssist(UserID, this.TaskControlID);

            this._mode = (int)TaskControlMode.UPDATE;
            this.Mode = (int)TaskControlMode.CLEAR;
            //FillProperties(this);
        }

        public static RoadAssistance GetRoadAssistance(int TaskControlID)
        {
            RoadAssistance roadAssistance = null;

            DataTable dt = GetRoadAssistanceByTaskControlID(TaskControlID);

            roadAssistance = new RoadAssistance();
            roadAssistance = (RoadAssistance)Policy.GetPolicyByTaskControlID(TaskControlID, roadAssistance);

            roadAssistance = FillProperties(roadAssistance);

            return roadAssistance;
        }

        private static RoadAssistance FillProperties(RoadAssistance roadAssistance)
        {

            roadAssistance.RoadAssistCollection = RoadAssistance.GetAutoRoadAssistByTaskControlID(roadAssistance.TaskControlID);
            return roadAssistance;
        }

        public static DataTable GetRoadAssistanceByTaskControlID(int TaskControlID)
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

            DataTable dt = exec.GetQuery("GetRoadAssistanceIDByTaskControlID", xmlDoc);
            return dt;
        }
       
        private void SaveRoadAssistancePolicies(int UserID)
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
        public void SaveAutoRoadAssist(int UserID, int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            Executor.Delete("DeleteAutoRoadAssistByTaskControlID", RoadAssistanceTaskControlXml(taskControlID));

            for (int i = 0; i < RoadAssistCollection.Rows.Count; i++)
            {
                this.Mode = 1; //Add

                Executor.BeginTrans();
                int dependienteID = Executor.Insert("AddAutoRoadAssist", this.GetInsertAutoRoadAssistXml(i));
                Executor.CommitTrans();
            }
        }

     
        #endregion

        #region RoadAssist Autos

        private XmlDocument RoadAssistanceTaskControlXml(int taskControlID)
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

        private XmlDocument GetInsertAutoRoadAssistXml(int index)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[15];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
              SqlDbType.Int, 0, this.TaskControlID.ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VIN",
              SqlDbType.VarChar, 100, RoadAssistCollection.Rows[index]["VIN"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleMakeID",
              SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["VehicleMakeID"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleModelID",
             SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["VehicleModelID"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleYearID",
              SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["VehicleYearID"].ToString(),
              ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NewUse",
             SqlDbType.Int, 0, RoadAssistCollection.Rows[index]["NewUse"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Plate",
             SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["Plate"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("NewUseDesc",
             SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["NewUseDesc"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleMake",
             SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["VehicleMake"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleModel",
             SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["VehicleModel"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("VehicleYear",
             SqlDbType.VarChar, 50, RoadAssistCollection.Rows[index]["VehicleYear"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium",
             SqlDbType.Float, 0, RoadAssistCollection.Rows[index]["Premium"].ToString(),
             ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsCreditPayment",
             SqlDbType.Bit, 0, IsCreditPayment.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsDebitPayment",
             SqlDbType.Bit, 0, IsDebitPayment.ToString(),
           ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsCashPayment",
             SqlDbType.Bit, 0, IsCashPayment.ToString(),

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

        public static DataTable GetAutoRoadAssistByTaskControlID(int TaskControlID)
        {
            DataTable dt = GetAutoRoadAssistByTaskControlIDDB(TaskControlID);
            return dt;
        }

        private static DataTable GetAutoRoadAssistByTaskControlIDDB(int taskControlID)
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

            DataTable dt = exec.GetQuery("GetAutoRoadAssistByTaskControlID", xmlDoc);

            return dt;
        }

        public DataTable RoadAssistCollection
        {
            get
            {
                if (this._RoadAssistCollection == null)
                    this._RoadAssistCollection = DataTableRoadAssistTemp();
                return (this._RoadAssistCollection);
            }
            set
            {
                this._RoadAssistCollection = value;
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

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsDebitPayment";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsDebitPayment";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsCreditPayment";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsCreditPayment";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsCashPayment";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsCashPayment";
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
