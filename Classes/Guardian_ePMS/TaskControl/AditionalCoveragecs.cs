using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
    public class AditionalCoveragecs
    {
        public AditionalCoveragecs()
        {

        }

        #region Variables

        private DataTable _dtAditionalCoveragecs;
        private PolicyDetailcs oldAditionalCoveragecs = null;
        private DataTable _AditionalCoveragecsTableTemp = null;
        private int _AditionalCoveragePropertiesID = 0;
        private int _AditionalCoverageID = 0;
        private int _TaskControlID = 0;
        private int _PropertiesID = 0;
        private string _AditionalCoverageDesc = "";
        private string _Limits = "";
        private string _Deductible = "";
        private double _Premium = 0.00;
        private double _Neto = 0.00;
        private string _Description = "";
        private int _Mode = (int)AditionalCoveragecsMode.CLEAR;

        #endregion

        #region Public Enumeration

        public enum AditionalCoveragecsMode { ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4 };

        #endregion

        #region Properties

        public DataTable AditionalCoverageTableTemp
        {
            get
            {
                return (this._AditionalCoveragecsTableTemp);
            }
            set
            {
                this._AditionalCoveragecsTableTemp = value;
            }
        }

        public int Mode
        {
            get
            {
                return this._Mode;
            }
            set
            {
                this._Mode = value;
            }
        }

        public int AditionalCoveragePropertiesID
        {
            get
            {
                return this._AditionalCoveragePropertiesID;
            }
            set
            {
                this._AditionalCoveragePropertiesID = value;
            }
        } 
        public int AditionalCoverageID
        {
            get
            {
                return this._AditionalCoverageID;
            }
            set
            {
                this._AditionalCoverageID = value;
            }
        }

        public int TaskControlID
        {
            get
            {
                return this._TaskControlID;
            }
            set
            {
                this._TaskControlID = value;
            }
        }

        public int PropertiesID
        {
            get
            {
                return this._PropertiesID;
            }
            set
            {
                this._PropertiesID = value;
            }
        }

        private string AditionalCoverageDesc
        {
            get
            {
                return this._AditionalCoverageDesc;
            }
            set
            {
                this._AditionalCoverageDesc = value;
            }
        }

        private string Limits
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

        private string Deductible
        {
            get
            {
                return this._Deductible;
            }
            set
            {
                this._Deductible = value;
            }
        }

        private double Premium
        {
            get
            {
                return this._Premium;
            }
            set
            {
                this._Premium = value;
            }
        }

        private double Neto
        {
            get
            {
                return this._Neto;
            }
            set
            {
                this._Neto = value;
            }
        }

        private string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        #endregion

        #region Public Methods

        public static DataTable GetAditionalCoverageTableTempLoaded(int propertiesID, bool IsOppQuote)
        {
            DataTable dt = GetAditionalCoverageByPropertiesIDDB(propertiesID, IsOppQuote);
            return dt;
        }

        public void SaveAditionalCoverage(int UserID, int propertiesID, int taskControlID, bool IsOppQuote)
        {
            DataTable dt = this.AditionalCoverageTableTemp;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.PropertiesID = propertiesID;
                this.TaskControlID = taskControlID;
                this.AditionalCoverageID = (int)this.AditionalCoverageTableTemp.Rows[i]["AdditionalCoveragesID"];
                this.AditionalCoverageDesc = this.AditionalCoverageTableTemp.Rows[i]["AdditionalCoveragesDesc"].ToString();
                this.Limits = this.AditionalCoverageTableTemp.Rows[i]["Limits"].ToString();
                this.Deductible = this.AditionalCoverageTableTemp.Rows[i]["Deductible"].ToString();
                this.Premium = double.Parse(this.AditionalCoverageTableTemp.Rows[i]["Premium"].ToString());
                this.Neto = double.Parse(this.AditionalCoverageTableTemp.Rows[i]["Neto"].ToString());
                this.Description = this.AditionalCoverageTableTemp.Rows[i]["Description"].ToString();

                this.Mode = 1; //Add
                this.Save(UserID, IsOppQuote);
            }
        }

        public void DeleteAditionalCoveragePropertiesByTaskControlID(int taskControlID, bool IsOppQuote)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            Executor.BeginTrans();
            Executor.Delete("DeleteAditionalCoveragePropertiesByTaskControlID", DeleteAditionalCoverageByTaskControlIDXml(taskControlID, IsOppQuote));
            Executor.CommitTrans();
        }

        #endregion

        #region Private Methods
        private void Save(int UserID, bool IsOppQuote)
        {
            //if (this.Mode ==2) //Para el History
            //	oldF1500Detailcs = (F1500Detailcs) GetF1500DetailByF1500DetailID(this.F1500DetailID);

            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this.Mode)
                {
                    case 1:  //ADD
                        this.AditionalCoverageID = Executor.Insert("AddAditionalCoverageProperties", this.GetInsertAditionalCoverageXml(IsOppQuote));
                        break;
                }
                Executor.CommitTrans();

                if (this.Mode == 1)  //ADD
                {
                    this.History(this.Mode, UserID);
                }
            }

            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error please try again. " + xcp.Message, xcp);
            }

            this.Mode = 4;
        }

        private XmlDocument DeleteAditionalCoverageByTaskControlIDXml(int taskControlID, bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOppQuote.ToString(),
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

        private static DataTable GetAditionalCoverageByPropertiesIDDB(int propertiesID, bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("PropertiesID",
                SqlDbType.Int, 0, propertiesID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOppQuote.ToString(),
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
                dt = exec.GetQuery("GetAditionalCoveragePropertiesByPropertiesID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not found the record in the Databases.", ex);
            }
        }

        private XmlDocument GetInsertAditionalCoverageXml(bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[10];

            DbRequestXmlCooker.AttachCookItem("AditionalCoverageID",
                SqlDbType.Int, 0, this.AditionalCoverageID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertiesID",
                SqlDbType.Int, 0, this.PropertiesID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AditionalCoverageDesc",
                SqlDbType.VarChar, 100, this.AditionalCoverageDesc.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Limits",
                SqlDbType.VarChar, 20, this.Limits.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Deductible",
                SqlDbType.VarChar, 20, this.Deductible.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Premium",
                SqlDbType.Float, 0, this.Premium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Description",
                SqlDbType.VarChar, 6000, this.Description.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOppQuote.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto",
                SqlDbType.Float, 0, this.Neto.ToString(),
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

        //New Stored Procedure para tomar las opciones para el DDL para el Agente 3/17/2014 Pedro
        private static DataTable GetAdditionalCoveragesByAgent()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[0];

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
                dt = exec.GetQuery("GetAdditionalCoveragesByAgent", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not found the record in the Databases.", ex);
            }
            }

         private void History(int mode, int userID)
        {
            //Audit.History history = new Audit.History();

            //if (mode == 2)
            //{
            //    history.BuildNotesForHistory("PolicyDetailsID", oldPolicyDetailcs.PolicyDetailsID.ToString(), this.PolicyDetailsID.ToString(), mode);
            //    history.BuildNotesForHistory("TaskControlID", oldPolicyDetailcs.TaskControlID.ToString(), this.TaskControlID.ToString(), mode);
            //    history.BuildNotesForHistory("Year", oldPolicyDetailcs.YearID.ToString(), this.YearID.ToString(), mode);
            //    history.BuildNotesForHistory("CoversID", oldPolicyDetailcs.CoversID.ToString(), this.CoversID.ToString(), mode);
            //    history.BuildNotesForHistory("LimitID", oldPolicyDetailcs.LimitsID.ToString(), this.LimitsID.ToString(), mode);
            //    history.BuildNotesForHistory("Premium", oldPolicyDetailcs.Premium.ToString(), this.Premium.ToString(), mode);

            //    history.Actions = "MODIFICAR";
            //}
            //else  //ADD & DELETE
            //{
            //    history.BuildNotesForHistory("TASKCONTROLID", "", this.TaskControlID.ToString(), mode);
            //    history.Actions = "AÑADIR";
            //}

            //history.KeyID = this.TaskControlID.ToString();
            //history.Subject = "POLICIES";
            //history.UsersID = userID;
            //history.GetSaveHistory();
        }

        #endregion
    }
}
