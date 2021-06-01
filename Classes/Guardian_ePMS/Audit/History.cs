using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.XmlCooker;

namespace EPolicy.Audit
{
	/// <summary>
	/// Summary description for History.
	/// </summary>
	public class History
	{
		public History()
		{
	 
		}

		#region Variables
		
		private int      _HistoryID  = 0;
		private string   _KeyID      = String.Empty;
		private string   _Subject    = String.Empty;
		private string   _Actions    = String.Empty;
		private DateTime _EntryDate  = DateTime.Now;
		private string   _Users      = String.Empty;
		private int      _UsersID    = 0;
		private string   _Notes      = String.Empty;
		private bool     _NFirst     = false;
		private string   _OldNotes   = String.Empty;
		private string   _NewNotes   = String.Empty;

		#endregion

		#region Public Enumeration

		public enum HistoryActions{ADD = 1, UPDATE = 2, DELETE = 3};
		
		#endregion

	    #region Properties

		public int HistoryID
		{
			get
			{
				return this._HistoryID;
			}
			set
			{
				this._HistoryID = value;
			}
		}

		public string KeyID
		{
			get
			{
				return this._KeyID;
			}
			set
			{
				this._KeyID = value;
			}
		}

		public string Subject
		{
			get
			{
				return this._Subject;
			}
			set
			{
				this._Subject = value;
			}
		}

		public string Actions
		{
			get
			{
				return this._Actions;
			}
			set
			{
				this._Actions = value;
			}
		}

		public DateTime EntryDate
		{
			get
			{
				return this._EntryDate;
			}
			set
			{
				this._EntryDate = value;
			}
		}

		public string Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users = value;
			}
		}
	
		public int UsersID
		{
			get
			{
				return this._UsersID;
			}
			set
			{
				this._UsersID = value;
			}
		}

		public string Notes
		{
			get
			{
				return this._Notes;
			}
			set
			{
				this._Notes = value;
			}
		}

		#endregion

		#region Public Methods

		public void GetSaveHistory()
		{			
			if (this.Actions == "EDIT")
			{
				this.Notes = _OldNotes+"\r\n";
				this.Notes = this.Notes + _NewNotes;
			}

			GetUserByUserID();

			DBRequest Executor = new DBRequest();
			try
			{
				if(this.Notes.Trim() !="")
					this.HistoryID = Executor.Insert("AddHistory",this.GetInsertHistoryXml());
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the history transaction. " + 
					xcp.Message,xcp);
			}
		}

        public void GetNotes()
        {
            if (this.Actions == "EDIT")
            {
                this.Notes = _OldNotes + "\r\n";
                this.Notes = this.Notes + _NewNotes;
            }
        }

		public void BuildNotesForHistory(string Field, string OldFieldValue, string NewFieldValue, int mode)
		{
            if (mode == 1 || mode == 6 || mode == 8 || mode == 10)  //ADD
			{
                if (mode == 6 || mode == 8 || mode == 10) //ADD 
                {
                    Notes = Notes + " " + "[ADD]".ToString() + "\r\n";
                    Notes = Notes + " " + Field.ToString() + "-" + NewFieldValue.ToString() + "\r\n";
                }
                else
                {
                    Notes = "[ADD]".ToString();
                }
			}
			else
			{
                if (mode == 2 || mode == 5 || mode == 7 || mode == 9)  //EDIT
				{
					if(OldFieldValue.Trim().ToUpper() != NewFieldValue.Trim().ToUpper())
					{
						if(_NFirst == false)
						{
							_OldNotes = "[Old Value]"+"\r\n";
							_NewNotes = "[New Value]"+"\r\n";

							_OldNotes = _OldNotes +"  "+ Field.ToString()+"-"+ OldFieldValue.ToString()+"\r\n";
							_NewNotes =	_NewNotes +"  "+ Field.ToString()+"-"+ NewFieldValue.ToString()+"\r\n";

							_NFirst = true;	
						}
						else
						{						
							_OldNotes = _OldNotes +"  "+  Field.ToString()+"-"+ OldFieldValue.ToString()+"\r\n";
							_NewNotes =	_NewNotes +"  "+  Field.ToString()+"-"+ NewFieldValue.ToString()+"\r\n";
						}		
					}
								
				}
				if(mode == 3) //DELETE
				{
					Notes = "[DELETE]".ToString()+"\r\n";
					Notes = Field.ToString()+"-"+ OldFieldValue.ToString();
				}
				
			}
		}

		public static DataTable GetHistoryByKeyIDAndSubject(string keyID, string subject)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("KeyID",
				SqlDbType.VarChar, 25, keyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Subject",
				SqlDbType.VarChar, 25, subject.ToString(),
				ref cookItems);

			DBRequest exec = new DBRequest();
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
				dt = exec.GetQuery("GetHistoryByKeyIDAndSubject", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve history by key and subject.", ex);
			}			
		}

		#endregion

		#region Private Methods


		private XmlDocument GetInsertHistoryXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			DbRequestXmlCooker.AttachCookItem("KeyID",
				SqlDbType.VarChar, 25, this.KeyID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Subject",
				SqlDbType.VarChar, 25, this.Subject.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Actions",
				SqlDbType.VarChar, 10, this.Actions.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("EntryDate",
				SqlDbType.DateTime, 0, this.EntryDate.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Users",
				SqlDbType.VarChar, 30, this.Users.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Notes",
				SqlDbType.VarChar, 1000, this.Notes.ToString(),
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

		private void GetUserByUserID()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this.UsersID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			DBRequest exec = new DBRequest();

			DataTable dt = exec.GetQuery("GetAuthenticatedUserByUserID",xmlDoc);
			
			if(dt != null)
			{
				if(dt.Rows.Count !=0)
				{
					this.Users = dt.Rows[0]["FirstName"].ToString().Trim()+" "+
						dt.Rows[0]["LastName"].ToString().Trim();
				}
			}
		}
		#endregion
	}
}
