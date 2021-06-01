using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.XmlCooker;

namespace EPolicy.TaskControl
{
	/// <summary>
	/// Summary description for PolicyDetail.
	/// </summary>
	public class PolicyDetailcs
	{
		public PolicyDetailcs()
		{
	
		}

		#region Variables

		private DataTable _dtPolicyDetail ;
		private PolicyDetailcs  oldPolicyDetailcs	= null;
		private DataTable _PolicyDetailTableTemp = null;
		private int       _PolicyDetailsID			= 0;
		private int       _TaskControlID			= 0;
		private int       _YearID					= 0;
		private int       _CoversID					= 0;
		private int	      _LimitsID					= 0;
		private double    _Premium					= 0.00;
		private int       _Mode					    = (int)PolicyDetailcsMode.CLEAR;

		#endregion

		#region Public Enumeration

		public enum PolicyDetailcsMode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Properties

		public DataTable PolicyDetailTableTemp
		{
			get
			{
				if(this._PolicyDetailTableTemp == null)
					this._PolicyDetailTableTemp = DataTablePolicyDetailTemp();
				return (this._PolicyDetailTableTemp);
			}
			set
			{
				this._PolicyDetailTableTemp = value;
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

		public int PolicyDetailsID
		{
			get
			{
				return this._PolicyDetailsID;
			}
			set
			{
				this._PolicyDetailsID = value;
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

		private int YearID		
		{
			get
			{
				return this._YearID;
			}
			set
			{
				this._YearID = value;
			}
		}

		private int CoversID		
		{
			get
			{
				return this._CoversID;
			}
			set
			{
				this._CoversID = value;
			}
		}

		private int LimitsID		
		{
			get
			{
				return this._LimitsID;
			}
			set
			{
				this._LimitsID = value;
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

		#endregion

		#region Public Methods

		public static PolicyDetailcs GetPolicyDetailByPolicyDetailID(int policyDetailID)
		{
			PolicyDetailcs policyDetailcs = null;
			 
			DataTable dt= GetPolicyDetailByPolicyDetailIDDB(policyDetailID);
			policyDetailcs = new PolicyDetailcs();
	
			policyDetailcs._dtPolicyDetail = dt;
			policyDetailcs = FillProperties(policyDetailcs);

			return policyDetailcs;
		}

		public static DataTable GetPolicyDetailTableTempLoaded(int taskControlID)
		{
			DataTable dt= GetPolicyDetailByTaskControlIDDB(taskControlID);
			return dt;
		}

		public static DataTable GetF1500DetailByF1500ID(int taskControlID)
		{
			DataTable dt= GetPolicyDetailByTaskControlIDDB(taskControlID);
			return dt;
		}

		public void SavePolicyDetail(int UserID, int taskControlID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            Executor.BeginTrans();
			Executor.Delete("DeletePolicyDetailByTaskControlID",DeletePolicyDetailByTaskControlIDXml(taskControlID));
            Executor.CommitTrans();

			DataTable dt = this.PolicyDetailTableTemp;
			for (int i = 0 ; i < dt.Rows.Count; i++)
			{
				this.TaskControlID	=  taskControlID;
				this.PolicyDetailsID =  (int) this.PolicyDetailTableTemp.Rows[i]["PolicyDetailsID"];
				this.YearID	        =  (int) this.PolicyDetailTableTemp.Rows[i]["YearID"];
				this.CoversID       =  (int) this.PolicyDetailTableTemp.Rows[i]["CoversID"];
				this.LimitsID		=  (int) this.PolicyDetailTableTemp.Rows[i]["LimitsID"];
				this.Premium		=  (double) this.PolicyDetailTableTemp.Rows[i]["Premium"];

				this.Mode = 1; //Add
				this.Save(UserID);
			}
		}

		public static void DeletePolicyDetailByPolicyDetailID(int policyDetailID)
		{
			//Hay que eliminar todas las transacciones relacionadas.
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			Executor.Update("DeletePolicyDetail",DeletePolicyDetailByPolicyDetailIDXml(policyDetailID));
		}

		#endregion

		#region Private Methods
		private void Save(int UserID)
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
						this.PolicyDetailsID = Executor.Insert("AddPolicyDetail",this.GetInsertPolicyDetailXml());
						break;
				}
				Executor.CommitTrans();
				
				if (this.Mode==1)  //ADD
				{
					this.History(this.Mode,UserID);
				}
			}

			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error please try again. "+xcp.Message,xcp);
			}

			this.Mode = 4;
		}

		private XmlDocument DeletePolicyDetailByTaskControlIDXml(int taskControlID)
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
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private static XmlDocument DeletePolicyDetailByPolicyDetailIDXml(int policyDetailID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("PolicyDetailID",
				SqlDbType.Int, 0, policyDetailID.ToString(),
				ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private static DataTable GetPolicyDetailByPolicyDetailIDDB(int policyDetailID)			                     
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("PolicyDetailID",
				SqlDbType.Int, 0, policyDetailID.ToString(),
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
				dt = exec.GetQuery("GetPolicyDetailByPolicyDetailID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Can not found the record in the Databases.", ex);
			}			
		}

		private static DataTable GetPolicyDetailByTaskControlIDDB(int taskcontrolID)
		{                        
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskcontrolID",
				SqlDbType.Int, 0, taskcontrolID.ToString(),
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
				dt = exec.GetQuery("GetTaskcontrolIDDetailByTaskcontrolID", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Can not found the record in the Databases.", ex);
			}			
		}

		private XmlDocument GetInsertPolicyDetailXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[5];

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("YearID",
				SqlDbType.Int, 0, this.YearID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CoversID",
				SqlDbType.Int, 0, this.CoversID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LimitsID",
				SqlDbType.Int, 0, this.LimitsID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Premium",
				SqlDbType.Float, 0, this.Premium.ToString(),
				ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private XmlDocument GetUpdatePolicyDetailXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			DbRequestXmlCooker.AttachCookItem("PolicyDetailsID",
				SqlDbType.Int, 0, this.PolicyDetailsID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("TaskControlID",
				SqlDbType.Int, 0, this.TaskControlID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("YearID",
				SqlDbType.Int, 0, this.YearID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CoversID",
				SqlDbType.Int, 0, this.CoversID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("LimitsID",
				SqlDbType.Int, 0, this.LimitsID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Premium",
				SqlDbType.Float, 0, this.Premium.ToString(),
				ref cookItems);

			XmlDocument xmlDoc;

			try
			{
				xmlDoc = DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

			return xmlDoc;
		}

		private void History(int mode, int userID)
		{
			Audit.History history = new Audit.History();
			
			if(mode == 2)
			{				
				history.BuildNotesForHistory("PolicyDetailsID",oldPolicyDetailcs.PolicyDetailsID.ToString(),this.PolicyDetailsID.ToString(),mode);
				history.BuildNotesForHistory("TaskControlID",oldPolicyDetailcs.TaskControlID.ToString(),this.TaskControlID.ToString(),mode);
				history.BuildNotesForHistory("Year",oldPolicyDetailcs.YearID.ToString(),this.YearID.ToString(),mode);
				history.BuildNotesForHistory("CoversID",oldPolicyDetailcs.CoversID.ToString(),this.CoversID.ToString(),mode);
				history.BuildNotesForHistory("LimitID",oldPolicyDetailcs.LimitsID.ToString(),this.LimitsID.ToString(),mode);
				history.BuildNotesForHistory("Premium",oldPolicyDetailcs.Premium.ToString(),this.Premium.ToString(),mode);

				history.Actions = "MODIFICAR";
			}
			else  //ADD & DELETE
			{
				history.BuildNotesForHistory("TASKCONTROLID","",this.TaskControlID.ToString(),mode);
				history.Actions = "AÑADIR";
			}

			history.KeyID = this.TaskControlID.ToString();
			history.Subject = "POLICIES";			
			history.UsersID =  userID;
			history.GetSaveHistory();
		}

		private static PolicyDetailcs FillProperties(PolicyDetailcs policyDetailcs)
		{
			policyDetailcs.PolicyDetailsID			= (policyDetailcs._dtPolicyDetail.Rows[0]["PolicyDetailsID"]!=System.DBNull.Value) ? (int) policyDetailcs._dtPolicyDetail.Rows[0]["PolicyDetailsID"]:0;
			policyDetailcs.TaskControlID		    = (policyDetailcs._dtPolicyDetail.Rows[0]["TaskControlID"]!=System.DBNull.Value) ? (int)policyDetailcs._dtPolicyDetail.Rows[0]["TaskControlID"]:0;
			policyDetailcs.YearID					= (policyDetailcs._dtPolicyDetail.Rows[0]["YearID"]!=System.DBNull.Value) ? (int)policyDetailcs._dtPolicyDetail.Rows[0]["Year"]:0;
			policyDetailcs.CoversID				    = (policyDetailcs._dtPolicyDetail.Rows[0]["CoversID"]!=System.DBNull.Value) ? (int)policyDetailcs._dtPolicyDetail.Rows[0]["CoversID"]:0;
			policyDetailcs.LimitsID		            = (policyDetailcs._dtPolicyDetail.Rows[0]["LimitsID"]!=System.DBNull.Value) ? (int)policyDetailcs._dtPolicyDetail.Rows[0]["LimitID"]:0;
			policyDetailcs.Premium                  = (policyDetailcs._dtPolicyDetail.Rows[0]["Premium"]!=System.DBNull.Value) ? (double)policyDetailcs._dtPolicyDetail.Rows[0]["Premium"]:0.00;

			return policyDetailcs;
		}

		#endregion

		#region DataTable F1500DetailTemp

		private DataTable DataTablePolicyDetailTemp()
		{
			DataTable myDataTable = new DataTable("PolicyDetailTemp");
			DataColumn myDataColumn;
 
			myDataColumn = new DataColumn();
			myDataColumn.DataType = System.Type.GetType("System.Int32");
			myDataColumn.ColumnName = "PolicyDetailsID";
			myDataColumn.AutoIncrement = true;
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
			myDataColumn.DataType = System.Type.GetType("System.Int32");
			myDataColumn.ColumnName = "YearID";
			myDataColumn.AutoIncrement = false;
			myDataColumn.Caption = "YearID";
			myDataColumn.ReadOnly = false;
			myDataColumn.Unique = false;
			myDataTable.Columns.Add(myDataColumn);

			myDataColumn = new DataColumn();
			myDataColumn.DataType = System.Type.GetType("System.String");
			myDataColumn.ColumnName = "YearDesc";
			myDataColumn.AutoIncrement = false;
			myDataColumn.Caption = "YearDesc";
			myDataColumn.ReadOnly = false;
			myDataColumn.Unique = false;
			myDataTable.Columns.Add(myDataColumn);

			myDataColumn = new DataColumn();
			myDataColumn.DataType = System.Type.GetType("System.Int32");
			myDataColumn.ColumnName = "CoversID";
			myDataColumn.AutoIncrement = false;
			myDataColumn.Caption = "CoversID";
			myDataColumn.ReadOnly = false;
			myDataColumn.Unique = false;
			myDataTable.Columns.Add(myDataColumn);

			myDataColumn = new DataColumn();
			myDataColumn.DataType = System.Type.GetType("System.String");
			myDataColumn.ColumnName = "CoversDesc";
			myDataColumn.AutoIncrement = false;
			myDataColumn.Caption = "CoversDesc";
			myDataColumn.ReadOnly = false;
			myDataColumn.Unique = false;
			myDataTable.Columns.Add(myDataColumn);

			myDataColumn = new DataColumn();
			myDataColumn.DataType = System.Type.GetType("System.Int32");
			myDataColumn.ColumnName = "LimitsID";
			myDataColumn.AutoIncrement = false;
			myDataColumn.Caption = "LimitsID";
			myDataColumn.ReadOnly = false;
			myDataColumn.Unique = false;
			myDataTable.Columns.Add(myDataColumn);

			myDataColumn = new DataColumn();
			myDataColumn.DataType = System.Type.GetType("System.String");
			myDataColumn.ColumnName = "LimitsDesc";
			myDataColumn.AutoIncrement = false;
			myDataColumn.Caption = "LimitsDesc";
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

		#endregion
	}
}
