using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using System.Security;
using System.Security.Principal;
using EPolicy.LookupTables;
using System.Web.UI.WebControls;
using EPolicy.XmlCooker;

namespace EPolicy.Login
{

	public class Login:IPrincipal
	{
		public Login()
		{
			
		}

		//public Login(IIdentity identity, string[] roles)
        public Login(IIdentity identity, string userID)
		{
			_identity = identity;
			//_roles = new string[roles.Length];
            
            this.UserID = int.Parse(userID);
            GetRolesByUserID();
            string[] roles = GetRole().Split('|');
            _roles = new string[roles.Length];
			
            roles.CopyTo(_roles,0);
			Array.Sort(_roles);
		}

		#region Variable

		private string _UserName  = "";
		private bool   _IsAuthenticated = false;
		private DataTable _DtUser;
		private IIdentity _identity;
		private string[] _roles;
		private string[] _TempRoles;
		private string _getRoles="";
		private int _UserID = 0;
		private bool _AccountDisable = false;
		private bool _ChangePassword = false;
        private bool _Dias30 = false;
        private DateTime _Dias30Date = DateTime.Now;
		private string _Password="";
		private string _OldPassword="";
		private string _FirstName="";
		private string _LastName="";
		private int    _Location = 0;
		private string _Comments="";
		private string _Time1="";
		private string _Time2="";
		private bool   _AllDay = false;
		private bool   _Lunes = false;
		private bool   _Martes = false;
		private bool   _Miercoles = false;
		private bool   _Jueves = false;
		private bool   _Viernes = false;
		private bool   _Sabado = false;
		private bool   _Domingo = false;
		private string _DealerID = "000";
        private string _Agent = "000";
        private string _AgentVI = "000";
        private bool _FilterAgent = false;
        private bool _FilterDealer = false;
		private string _ConfirmPassword="";
		private DateTime _EntryDate;
		private int _Mode = (int) LoginMode.CLEAR;
		private bool _pass= false;
		private DataTable _DtGroup;
		private DataTable _DtPermission;
		private DataTable _DtTempGroup;

	    #endregion

		#region Public Enumeration

		public enum LoginMode{ADD = 1, UPDATE = 2, DELETE = 3, CLEAR = 4};

		#endregion

		#region Properties

		public int Mode
		{
			get
			{
				return this._Mode;
			}
			set
			{
				this._Mode = value;
				if(this._Mode == 2)  //Add or Edit.
				{
					this._DtTempGroup = this._DtGroup.Clone();
				}
				else if(this._Mode == 4) //Clear
				{
					this._DtTempGroup = null;
				}
			}
		}

		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this._UserID = value;
			}
		}

		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
			}
		}

		public bool IsAuthenticated
		{
			get
			{
				return this._IsAuthenticated;
			}
			set
			{
				this._IsAuthenticated = value;
			}
		}

		public IIdentity Identity
		{
			get
			{
				return _identity;
			}
		}

		public bool AccountDisable
		{
			get
			{
				return this._AccountDisable;
			}
			set
			{
				this._AccountDisable = value;
			}
		}

		public bool ChangePassword
		{
			get
			{
				return this._ChangePassword;
			}
			set
			{
				this._ChangePassword = value;
			}
		}

        public bool Dias30
        {
            get
            {
                return this._Dias30;
            }
            set
            {
                this._Dias30 = value;
            }
        }

        public DateTime Dias30Date
        {
            get
            {
                return this._Dias30Date;
            }
            set
            {
                this._Dias30Date = value;
            }
        }

		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if(!_pass)
				{
					this.OldPassword = this.Password;
					_pass = true;
				}
				this._Password = value;
			}
		}

		public int LocationID
		{
			get
			{
				return this._Location;
			}
			set
			{
				this._Location = value;
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


		public string Comments
		{
			get
			{
				return this._Comments;
			}
			set
			{
				this._Comments = value;
			}
		}
			
		public string Time1
		{
			get
			{
				return this._Time1;
			}
			set
			{
				this._Time1 = value;
			}
		}

		public string Time2
		{
			get
			{
				return this._Time2;
			}
			set
			{
				this._Time2 = value;
			}
		}

		public bool AllDay
		{
			get
			{
				return this._AllDay;
			}
			set
			{
				this._AllDay = value;
			}
		}

		public bool Lunes
		{
			get
			{
				return this._Lunes;
			}
			set
			{
				this._Lunes = value;
			}
		}

		public bool Martes
		{
			get
			{
				return this._Martes;
			}
			set
			{
				this._Martes = value;
			}
		}

		public bool Miercoles
		{
			get
			{
				return this._Miercoles;
			}
			set
			{
				this._Miercoles = value;
			}
		}

		public bool Jueves
		{
			get
			{
				return this._Jueves;
			}
			set
			{
				this._Jueves = value;
			}
		}

		public bool Viernes
		{
			get
			{
				return this._Viernes;
			}
			set
			{
				this._Viernes = value;
			}
		}

		public bool Sabado
		{
			get
			{
				return this._Sabado;
			}
			set
			{
				this._Sabado = value;
			}
		}

		public bool Domingo
		{
			get
			{
				return this._Domingo;
			}
			set
			{
				this._Domingo = value;
			}
		}

		public string ConfirmPassword
		{
			get
			{
				return this._ConfirmPassword;
			}
			set
			{
				this._ConfirmPassword = value;
			}
		}

		public string OldPassword
		{
			get
			{
				return this._OldPassword;
			}
			set
			{
				this._OldPassword = value;
			}
		}

		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				this._FirstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				this._LastName = value;
			}
		}

		public DataTable GroupTable
		{
			get
			{
				return this._DtGroup;
			}
			set
			{
				this._DtGroup = value;
			}
		}

		public DataTable PermissionTable
		{
			get
			{
				return this._DtPermission;
			}
			set
			{
				this._DtPermission = value;
			}
		}

		public DataTable GroupTempTable
		{
			get
			{
				return this._DtTempGroup;
			}
			set
			{
				this._DtTempGroup = value;
			}
		}

		public string DealerID
		{
			get
			{
				return this._DealerID;
			}
			set
			{
				this._DealerID = value;
			}
		}

        public string Agent
        {
            get
            {
                return this._Agent;
            }
            set
            {
                this._Agent = value;
            }
        }

        public string AgentVI
        {
            get
            {
                return this._AgentVI;
            }
            set
            {
                this._AgentVI = value;
            }
        }

        public bool FilterAgent
        {
            get
            {
                return this._FilterAgent;
            }
            set
            {
                this._FilterAgent = value;
            }
        }
        public bool FilterDealer
        {
            get
            {
                return this._FilterDealer;
            }
            set
            {
                this._FilterDealer = value;
            }
        }

		#endregion

		#region Public methods

		public void Save(bool password)
		{
			this.Validate(password);
			
			SaveLogin();      //Save Login
			if(this.Mode ==1)
			{
				this.SaveAuthenticatedGroupUser(true);
			}

			this._DtGroup = GetAuthenticatedGroupByUserID(this.UserID);

			_pass = false;
			this.Mode = (int) LoginMode.CLEAR;
		}

		public void SaveDealer(int UserID, string CompanyDealerID)
		{
			SaveDealerinDB(UserID,CompanyDealerID);
		}

        public void SaveAgentByUser(int UserID, string AgentID)//bool IsAgentVI)
        {
            SaveAgentinDB(UserID, AgentID);//, IsAgentVI);
        }

		public void DeleteGroupDealerByGroupDealerID(int GroupDealerID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
			
				Executor.Delete("DeleteGroupDealer",this.DeleteGroupDealerByGroupDealerIDXml(GroupDealerID));
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to delete this dealer for this User. "+xcp.Message,xcp);
			}
			
		}

        public void DeleteGroupAgentByGroupAgentID(int GroupAgentID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();

                Executor.Delete("DeleteGroupAgent", this.DeleteGroupAgentByGroupAgentIDXml(GroupAgentID));
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to delete this agent for this User. " + xcp.Message, xcp);
            }

        }

		public void SavePasword(string newPassword, string confirmPassword, string oldPassword, string newPasswordText)
		{
			newPassword = newPassword.Trim();
			confirmPassword = confirmPassword.Trim();
			oldPassword = oldPassword.Trim();
			newPasswordText = newPasswordText.Trim();

			string errorMessage = String.Empty;
			bool found = false;

				   
			if (newPassword == "" &&  found == false)
			{
				errorMessage = "New Password is missing or wrong.";
				found = true;
			}

			if (confirmPassword == "" &&  found == false)
			{
				errorMessage = "Confirm Password is missing or wrong.";
				found = true;
			}

			if (confirmPassword != newPassword &&  found == false)
			{
				errorMessage = "The new password not match with the confirm password, please try again.";
				found = true;
			}
	
			int space = newPasswordText.IndexOf(" ");

			if(space != -1 &&  found == false)
			{
				errorMessage = "Six-character minimum; no spaces, Ten-character maximum.";
				found = true;
			}

			if ((newPasswordText.Length > 10 || newPasswordText.Length < 6) &&  found == false)
			{
				errorMessage = "Six-character minimum; no spaces, Ten-character maximum.";
				found = true;
			}

			string newpass = newPasswordText.Substring(0,1);
			newpass = newpass.PadRight(newPasswordText.Length,(char.Parse(newpass)));

			if (newPasswordText.Trim()== newpass.Trim() &&  found == false)
			{
				errorMessage = "The new password must contain differents characters.";
				found = true;
			}

			//Vefify if the new password is the same that the old password.
			if (newPassword == this._DtUser.Rows[0]["OldPassword"].ToString().Trim() &&  found == false)
			{
				errorMessage = "Cannot repeat any of your previous password.\r\n"+
					"Please type a different password.";
				found = true;
			}

			if(oldPassword == newPassword)
			{
				errorMessage = "Cannot repeat any of your previous password.\r\n"+
					"Please type a different password.";
				found = true;
			}

			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}	

			SaveNewPassword(newPassword, oldPassword);
		}

		public bool GetAuthenticatedUser(string UserName, string Password)
		{
			IsAuthenticated = false;
	
			IsAuthenticated = GetAuthenticatedUserByUserNamePassWord(UserName.Trim(), Password.Trim());

			if (IsAuthenticated)
			{
				GetRolesByUserID();
				AddLoginCount();
			}

			return IsAuthenticated;
		}

        public bool GetAuthenticatedUserAD(string UserName)
        {
            IsAuthenticated = false;

            IsAuthenticated = GetAuthenticatedUserByUserNameAD(UserName.Trim());

            if (IsAuthenticated)
            {
                GetRolesByUserID();
                AddLoginCount();
            }

            return IsAuthenticated;
        }

		public void AddLoginCount()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[3];

			DbRequestXmlCooker.AttachCookItem("LocationID",
				SqlDbType.VarChar, 10, this.LocationID.ToString(),
				ref cookItems);

			string loginName;
			
			if(this.UserName.Trim() != "")
			{
				loginName = this.UserName.Trim();
			}
			else
			{
				loginName = "UNKNOWN";
			}

			DbRequestXmlCooker.AttachCookItem("LoginName",
				SqlDbType.VarChar, 50, this.UserName.Trim(),
				ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UserID",
                SqlDbType.Int, 0, UserID.ToString(),
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

			try
			{
                exec.BeginTrans();
				int ln = exec.Insert("AddLoginCount", xmlDoc);
                exec.CommitTrans();
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve the Login Count.", ex);
			}			
		}

		public void ForAuthorization(string[] roles)
		{
			_roles = new string[roles.Length];
			roles.CopyTo(_roles,0);
			Array.Sort(_roles);
		}

		public string GetRole()
		{
			return _getRoles; 
		}

		public bool IsInRole(string role)
		{
			return Array.BinarySearch(_roles, role) >= 0 ? true:false;
		}

		public static int GetLocationByUserID(int UserID)
		{
			DataTable dt = GetUserByUserIDDB(UserID);

			if (dt.Rows.Count != 0)
			{
				return (int) dt.Rows[0]["LocationID"];
			}
			else
			{
				return 0;
			}
		}

        public static string GetAgentByUserID(int UserID)
        {
            DataTable dt = GetUserByUserIDDB(UserID);

            if (dt.Rows.Count != 0)
            {
                return dt.Rows[0]["Agent"].ToString();
            }
            else
            {
                return "000";
            }
        }

        //public static string GetAgentByUserID_VI(int UserID)
        //{
        //    DataTable dt = GetUserByUserIDDB(UserID);

        //    if (dt.Rows.Count != 0)
        //    {
        //        return dt.Rows[0]["AgentVI"].ToString();
        //    }
        //    else
        //    {
        //        return "000";
        //    }
        //}

		public static string GetDealerByUserID(int UserID)
		{
			DataTable dt = GetUserByUserIDDB(UserID);

			if (dt.Rows.Count != 0)
			{
				return dt.Rows[0]["DealerID"].ToString();
			}
			else
			{
				return "000";
			}
		}

		public static DataTable GetGroupDealerByUserID(int UserID)
		{
			DataTable dt = GetGroupDealerByUserIDDB(UserID);

			return dt;
		}

        public static DataTable GetGroupAgentByUserID(int UserID)
        {
            DataTable dt = GetGroupAgentByUserIDDB(UserID);

            return dt;
        }

        public static DataTable GetGroupAgentVIByUserID(int UserID)
        {
            DataTable dt = GetGroupAgentVIByUserIDDB(UserID);

            return dt;
        }

		public static Login GetUser(int UserID)
		{
			Login login = new Login();
				
			login._DtUser = GetUserByUserIDDB(UserID);
			login._DtGroup = GetAuthenticatedGroupByUserID(UserID);
			
			login = FillLoginProperties(login);
			return login;
		}

		public static DataTable GetPermissionByGroupID(int authenticatedGroupID)
		{
			DataTable dt = GetPermissionByGroupIDDB(authenticatedGroupID);
			
			return dt;
		}

		public void DeleteAuthenticatedGroupUse(int authenticatedGroupUserID)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
			
				Executor.Delete("DeleteAuthenticatedGroupUser",this.GetDeleteAuthenticatedGroupUserXml(authenticatedGroupUserID));
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to delete this member for this User. "+xcp.Message,xcp);
			}
		}

		public void AddAuthenticatedGroupUse(int authenticatedGroupID,string authenticatedGroupDesc)
		{
			//Verificar si existe este grupo para este usuario.
			bool recordExist = false;
			for(int i=0;i<=this._DtGroup.Rows.Count-1;i++)
			{
				if((int) this._DtGroup.Rows[i]["AuthenticatedGroupID"] == authenticatedGroupID)
				{
					recordExist = true;
				}
			}

			if(!recordExist)
			{
				//DtTempGroup
				this._DtTempGroup = this._DtGroup.Clone();

				DataRow dr;
				dr = this._DtTempGroup.NewRow();

				dr["AuthenticatedGroupID"] = authenticatedGroupID;
				dr["UserID"]				= this.UserID;
				dr["AuthenticatedGroupDesc"]  = authenticatedGroupDesc;
						
				this._DtTempGroup.Rows.Add(dr);
				this._DtTempGroup.AcceptChanges();

				SaveAuthenticatedGroupUser(false);

				this._DtTempGroup = null;
			}
			else
			{
				//throw the exception.
				throw new Exception("This item selected is already exist.");
			}
		}

        public static DataTable GetAuthenticatedUserByUserName(string UserName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            XmlDocument xmlDoc = new XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>UserName</name>");
            sb.Append("<type>varchar</type>");
            sb.Append("<Length>50</Length>");
            sb.Append("<value>" + UserName + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetAuthenticatedUserByUserName", xmlDoc);
            return dt;
        }

		#endregion

		#region Private methods

		private void Validate(bool password)
		{
			string errorMessage = String.Empty;
			bool found = false;

			if (this.FirstName == "" &&  found == false)
			{
				errorMessage = "FirstName is missing or wrong.";
				found = true;
			}

			if (this.UserName == "" &&  found == false)
			{
				errorMessage = "UserName is missing or wrong.";
				found = true;
			}

			if(password)
			{
				if (this.Password != this.OldPassword)
				{
					if (this.ConfirmPassword != this.Password)
					{
						errorMessage = "The new password not match with the confirm password, please try again.";
						found = true;
					}
				}
				else
				{
					DataTable dt = GetUserByUserIDDB(this.UserID);
					if (dt.Rows.Count != 0)
					{
						if(this.Password.Trim() == this._DtUser.Rows[0]["OldPassword"].ToString().Trim())
						{
							errorMessage = "Cannot repeat any of your previous password.\r\n"+
								"Please type a different password.";
							found = true;
						}
					}
				}
			}
			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}	
		}

		private void SaveLogin()
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();

				switch (this.Mode)
				{
					case 1:  //ADD
						this.UserID = Executor.Insert("AddAuthenticatedUser",this.GetInsertAuthenticatedUserXml());
						break;

					case 3:  //DELETE
						Executor.Delete("DeleteAuthenticatedUser",this.GetDeleteAuthenticatedUserXml());
						break;

					case 4:  //CLEAR						
						break;

					default: //UPDATE
						Executor.Update("UpdateAuthenticatedUser",this.GetUpdateAuthenticatedUserXml(false));
						break;
				}
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the User. "+xcp.Message,xcp);
			}
		}
		
		private void SaveNewPassword(string newPassword, string oldPassword)
		{
			this.UserID				= (int)  this._DtUser.Rows[0]["UserID"];
			this.AccountDisable		= (bool) this._DtUser.Rows[0]["AccountDisable"];
			this.ChangePassword		= (bool) this._DtUser.Rows[0]["ChangePassword"];
			this.UserName			= this._DtUser.Rows[0]["UserName"].ToString();
			this.Password			= newPassword.Trim();
			this.OldPassword		= oldPassword.Trim();
			this.FirstName			= this._DtUser.Rows[0]["FirstName"].ToString();
			this.LastName			= this._DtUser.Rows[0]["LastName"].ToString();
			this.LocationID         = (int)  this._DtUser.Rows[0]["LocationID"];
			this.Comments			= this._DtUser.Rows[0]["Comments"].ToString();
			this.Time1				= this._DtUser.Rows[0]["Time1"].ToString();
			this.Time2				= this._DtUser.Rows[0]["Time2"].ToString();
			this.AllDay				= (bool) this._DtUser.Rows[0]["AllDay"];
			this.Lunes				= (bool) this._DtUser.Rows[0]["Lunes"];
			this.Martes				= (bool) this._DtUser.Rows[0]["Martes"];
			this.Miercoles			= (bool) this._DtUser.Rows[0]["Miercoles"];
			this.Jueves				= (bool) this._DtUser.Rows[0]["Jueves"];
			this.Viernes			= (bool) this._DtUser.Rows[0]["Viernes"];
			this.Sabado				= (bool) this._DtUser.Rows[0]["Sabado"];
			this.Domingo			= (bool) this._DtUser.Rows[0]["Domingo"];
			this.DealerID			= this._DtUser.Rows[0]["DealerID"].ToString();
            this.Agent              = this._DtUser.Rows[0]["Agent"].ToString();
            this.AgentVI            = this._DtUser.Rows[0]["AgentVI"].ToString();
            this.FilterAgent        = (bool)this._DtUser.Rows[0]["FilterAgent"];
            this.FilterDealer       = (bool)this._DtUser.Rows[0]["FilterDealer"];

			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();

				Executor.Update("UpdateAuthenticatedUser",this.GetUpdateAuthenticatedUserXml(true));

                Executor.CommitTrans(); //Victor

			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the new password. "+xcp.Message,xcp);
			}
		}

		private void SaveAuthenticatedGroupUser(bool newUser)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();

				int identity = Executor.Insert("AddAuthenticatedGroupUser",this.GetInsertAuthenticatedGroupUserXml(newUser));

				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the members to the user. "+xcp.Message,xcp);
			}
		}

		private void SaveDealerinDB(int UserID, string CompanyDealerID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("UserID",
				SqlDbType.Int, 0, UserID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
				SqlDbType.Char, 3, CompanyDealerID.ToString(),
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
	
			int a = exec.Insert("AddGroupDealer",xmlDoc);
		}

        private void SaveAgentinDB(int UserID, string AgentID)//, bool IsAgentVI)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("UserID",
                SqlDbType.Int, 0, UserID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AgentID",
                SqlDbType.Char, 3, AgentID.ToString(),
                ref cookItems);

            //DbRequestXmlCooker.AttachCookItem("IsAgentVI",
            //    SqlDbType.Bit, 0, bool.Parse(IsAgentVI.ToString()).ToString(),
            //    ref cookItems);

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

            int a = exec.Insert("AddGroupAgent", xmlDoc);
        }

		private XmlDocument GetDeleteAuthenticatedGroupUserXml(int authenticatedGroupUserID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedGroupUserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + authenticatedGroupUserID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

		private XmlDocument DeleteGroupDealerByGroupDealerIDXml(int GroupDealerID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>GroupDealerID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + GroupDealerID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

        private XmlDocument DeleteGroupAgentByGroupAgentIDXml(int GroupAgentID)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            XmlDocument xmlDoc = new XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>GroupAgentID</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + GroupAgentID + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            return xmlDoc;
        }

		private XmlDocument GetDeleteAuthenticatedUserXml()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + UserID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

		private XmlDocument GetInsertAuthenticatedUserXml()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AccountDisable</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + AccountDisable + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>ChangePassword</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + ChangePassword + "</value>");
			sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>Dias30</name>");
            sb.Append("<type>bit</type>");
            sb.Append("<value>" + Dias30 + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>Dias30Date</name>");
            sb.Append("<type>DateTime</type>");
            sb.Append("<value>" + Dias30Date + "</value>");
            sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>UserName</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>50</Length>");
			sb.Append("<value>" + UserName + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Password</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>100</Length>");
			sb.Append("<value>" + Password + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>OldPassword</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>100</Length>");
			sb.Append("<value>" + OldPassword + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>FirstName</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>20</Length>");
			sb.Append("<value>" + FirstName + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>LastName</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>20</Length>");
			sb.Append("<value>" + LastName + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>LocationID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + LocationID + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Comments</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>500</Length>");
			sb.Append("<value>" + Comments + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Time1</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>10</Length>");
			sb.Append("<value>" + Time1 + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Time2</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>10</Length>");
			sb.Append("<value>" + Time2 + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>AllDay</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + AllDay + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Lunes</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Lunes + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Martes</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Martes + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Miercoles</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Miercoles + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Jueves</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Jueves + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Viernes</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Viernes + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Sabado</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Sabado + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Domingo</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Domingo + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>DealerID</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>3</Length>");
			sb.Append("<value>" + DealerID + "</value>");
			sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>Agent</name>");
            sb.Append("<type>char</type>");
            sb.Append("<Length>3</Length>");
            sb.Append("<value>" + Agent + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>FilterAgent</name>");
            sb.Append("<type>bit</type>");
            sb.Append("<value>" + FilterAgent  + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>FilterDealer</name>");
            sb.Append("<type>bit</type>");
            sb.Append("<value>" + FilterDealer + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>AgentVI</name>");
            sb.Append("<type>char</type>");
            sb.Append("<Length>3</Length>");
            sb.Append("<value>" + AgentVI + "</value>");
            sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

		private XmlDocument GetUpdateAuthenticatedUserXml(bool FromUser)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + UserID + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>AccountDisable</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + AccountDisable + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>ChangePassword</name>");
			sb.Append("<type>bit</type>");
			if(this.ChangePassword && FromUser==true)
			{
				sb.Append("<value>" + false + "</value>");
				this.ChangePassword = false;
			}
			else
			{
				sb.Append("<value>" + ChangePassword + "</value>");
			}
			sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>Dias30</name>");
            sb.Append("<type>bit</type>");
            sb.Append("<value>" + Dias30 + "</value>");
            sb.Append("</parameter>");

            sb.Append("<parameter>");
            sb.Append("<name>Dias30Date</name>");
            sb.Append("<type>DateTime</type>");
            if (FromUser == true)
            {
                sb.Append("<value>" + DateTime.Now + "</value>");
                this.Dias30Date = DateTime.Now;
            }
            else
            {
                sb.Append("<value>" + Dias30Date + "</value>");
            }
            sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>UserName</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>50</Length>");
			sb.Append("<value>" + UserName + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Password</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>100</Length>");
			sb.Append("<value>" + Password + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>OldPassword</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>100</Length>");
			sb.Append("<value>" + OldPassword + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>FirstName</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>20</Length>");
			sb.Append("<value>" + FirstName + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>LastName</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>20</Length>");
			sb.Append("<value>" + LastName + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>LocationID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + LocationID + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Comments</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>500</Length>");
			sb.Append("<value>" + Comments + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Time1</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>10</Length>");
			sb.Append("<value>" + Time1 + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Time2</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>10</Length>");
			sb.Append("<value>" + Time2 + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>AllDay</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + AllDay + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Lunes</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Lunes + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Martes</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Martes + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Miercoles</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Miercoles + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Jueves</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Jueves + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Viernes</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Viernes + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Sabado</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Sabado + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>Domingo</name>");
			sb.Append("<type>bit</type>");
			sb.Append("<value>" + Domingo + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>DealerID</name>");
			sb.Append("<type>char</type>");
			sb.Append("<Length>3</Length>");
			sb.Append("<value>" + DealerID + "</value>");
			sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>Agent</name>");
            sb.Append("<type>char</type>");
            sb.Append("<Length>3</Length>");
            sb.Append("<value>" + Agent + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>FilterAgent</name>");
            sb.Append("<type>bit</type>");
            sb.Append("<value>" + FilterAgent + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>FilterDealer</name>");
            sb.Append("<type>bit</type>");
            sb.Append("<value>" + FilterDealer + "</value>");
            sb.Append("</parameter>");
            sb.Append("<parameter>");
            sb.Append("<name>AgentVI</name>");
            sb.Append("<type>char</type>");
            sb.Append("<Length>3</Length>");
            sb.Append("<value>" + AgentVI + "</value>");
            sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

		private XmlDocument GetInsertAuthenticatedGroupUserXml(bool newUser)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedGroupID</name>");
			sb.Append("<type>int</type>");
			if (newUser)
			{
				sb.Append("<value>" + 12 + "</value>");
			}
			else
			{
				sb.Append("<value>" + (int) this._DtTempGroup.Rows[0]["AuthenticatedGroupID"] + "</value>");
			}
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this.UserID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

		private void GetRolesByUserID()
		{
			_DtGroup = GetAuthenticatedGroupByUserID();

			if (_DtGroup.Rows.Count!=0)
			{
				for(int i=0;i<=_DtGroup.Rows.Count-1;i++)
				{
					GetAuthenticatePermissionByGroupID((int) _DtGroup.Rows[i]["AuthenticatedGroupID"]);
				}
			}			
		}

		private static DataTable GetPermissionByGroupIDDB(int authenticatedGroupID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedGroupID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + authenticatedGroupID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetAuthenticatedPermissionByAuthenticatedGroupID",xmlDoc);
			return dt;
		}

		private void GetAuthenticatePermissionByGroupID(int authenticatedGroupID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedGroupID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + authenticatedGroupID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			_DtPermission = exec.GetQuery("GetAuthenticatedPermissionByAuthenticatedGroupID",xmlDoc);

			if (_DtPermission.Rows.Count !=0)
			{
				AddRoleInVariable(_DtPermission);
			}
		}

		private void AddRoleInVariable(DataTable dtPermission)
		{
			for(int i=0;i<=dtPermission.Rows.Count-1;i++)
			{
				if(_TempRoles==null)
				{
					if (_getRoles!="")
					{
						_getRoles = _getRoles + "|";
					}

					_getRoles = _getRoles+LookupTables.LookupTables.GetDescription("AuthenticatedPermission",dtPermission.Rows[i]["AuthenticatedPermissionID"].ToString().Trim());							
				
					_TempRoles = _getRoles.Split('|');
					Array.Sort(_TempRoles);
				}
				else
				{
					if (Array.BinarySearch(_TempRoles,LookupTables.LookupTables.GetDescription("AuthenticatedPermission",dtPermission.Rows[i]["AuthenticatedPermissionID"].ToString().Trim())) >= 0)
					{
					}
					else
					{
						if (_getRoles!="")
						{
							_getRoles = _getRoles + "|";
						}

						_getRoles = _getRoles+LookupTables.LookupTables.GetDescription("AuthenticatedPermission",dtPermission.Rows[i]["AuthenticatedPermissionID"].ToString().Trim());							
				
						_TempRoles = _getRoles.Split('|');
						Array.Sort(_TempRoles);
					}
				}
			}
		}

		private static DataTable GetAuthenticatedGroupByUserID(int userID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + userID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetAuthenticatedGroupByUserID",xmlDoc);
			return dt;
		}

		private DataTable GetAuthenticatedGroupByUserID()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + this._UserID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetAuthenticatedGroupByUserID",xmlDoc);
			return dt;
		}

		private static DataTable GetUserByUserIDDB(int UserID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + UserID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetAuthenticatedUserByUserID",xmlDoc);
			return dt;
		}

		private static DataTable GetGroupDealerByUserIDDB(int UserID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + UserID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetGroupDealerByUserID",xmlDoc);
			return dt;
		}

        private static DataTable GetGroupAgentByUserIDDB(int UserID)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            XmlDocument xmlDoc = new XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>UserID</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + UserID + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetGroupAgentByUserID", xmlDoc);
            return dt;
        }

        private static DataTable GetGroupAgentVIByUserIDDB(int UserID)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            XmlDocument xmlDoc = new XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>UserID</name>");
            sb.Append("<type>int</type>");
            sb.Append("<value>" + UserID + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dt = exec.GetQuery("GetGroupAgentVIByUserID", xmlDoc);
            return dt;
        }

		private bool GetAuthenticatedUserByUserNamePassWord(string UserName, string Password)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>UserName</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>50</Length>");
			sb.Append("<value>" + UserName + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>PassWord</name>");
			sb.Append("<type>varchar</type>");
			sb.Append("<Length>50</Length>");
			sb.Append("<value>" + Password + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			_DtUser = exec.GetQuery("GetAuthenticatedUserByUserNamePassWord",xmlDoc);
			
			bool accesso = false;

			if (_DtUser.Rows.Count > 0)
			{
				if (_DtUser.Rows[0]["UserName"].ToString() == UserName && _DtUser.Rows[0]["PassWord"].ToString() == Password)
				{
					if((bool) _DtUser.Rows[0]["AccountDisable"])
					{
						accesso = false;						
						throw new Exception("Account disabled, Please contact your administrator.");
					}
					else
					{
						accesso = true;
					}
				}
				else
				{
					accesso = false;
					throw new Exception("Wrong username or password, Please try again..");
				}
			}
			else
			{
				accesso = false;
				throw new Exception("Wrong username or password, Please try again...");
			}

			if (accesso)
			{
				_UserName = _DtUser.Rows[0]["FirstName"].ToString().Trim()+" "+_DtUser.Rows[0]["LastName"].ToString().Trim();
				_UserID	  = (int) _DtUser.Rows[0]["UserID"];
                _Agent = _DtUser.Rows[0]["Agent"].ToString();
                _AgentVI = _DtUser.Rows[0]["AgentVI"].ToString();
				this.LocationID		= (int)  _DtUser.Rows[0]["LocationID"];
				this.ChangePassword = (bool) _DtUser.Rows[0]["ChangePassword"];
                this.Dias30 = (bool)_DtUser.Rows[0]["Dias30"];
                this.Dias30Date = (DateTime)_DtUser.Rows[0]["Dias30Date"];
				this.Time1 = _DtUser.Rows[0]["Time1"].ToString();
				this.Time2 = _DtUser.Rows[0]["Time2"].ToString();
				this.AllDay = (bool) _DtUser.Rows[0]["AllDay"];
				this.Lunes   = (bool) _DtUser.Rows[0]["Lunes"];
				this.Martes  = (bool) _DtUser.Rows[0]["Martes"];
				this.Miercoles = (bool) _DtUser.Rows[0]["Miercoles"];
				this.Jueves  = (bool) _DtUser.Rows[0]["Jueves"];
				this.Viernes = (bool) _DtUser.Rows[0]["Viernes"];
				this.Sabado  = (bool) _DtUser.Rows[0]["Sabado"];
				this.Domingo = (bool) _DtUser.Rows[0]["Domingo"];
				this.DealerID = _DtUser.Rows[0]["DealerID"].ToString();
                this.Agent = _DtUser.Rows[0]["Agent"].ToString();
                this.AgentVI = _DtUser.Rows[0]["AgentVI"].ToString();
                this.FilterAgent = (bool)_DtUser.Rows[0]["FilterAgent"];
                this.FilterDealer = (bool)_DtUser.Rows[0]["FilterDealer"];
			}
			return accesso;
		}

        private bool GetAuthenticatedUserByUserNameAD(string UserName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            XmlDocument xmlDoc = new XmlDocument();

            sb.Append("<parameters>");
            sb.Append("<parameter>");
            sb.Append("<name>UserName</name>");
            sb.Append("<type>varchar</type>");
            sb.Append("<Length>50</Length>");
            sb.Append("<value>" + UserName + "</value>");
            sb.Append("</parameter>");
            sb.Append("</parameters>");
            xmlDoc.InnerXml = sb.ToString();
            sb = null;

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            _DtUser = exec.GetQuery("GetAuthenticatedUserByUserName", xmlDoc);

            bool accesso = false;

            if (_DtUser.Rows.Count > 0)
            {
                if (_DtUser.Rows[0]["UserName"].ToString().ToUpper() == UserName.ToUpper())
                {
                    if ((bool)_DtUser.Rows[0]["AccountDisable"])
                    {
                        accesso = false;
                        throw new Exception("Account disabled. Please contact your administrator..");
                    }
                    else
                    {
                        accesso = true;
                    }
                }
                else
                {
                    accesso = false;
                    throw new Exception("Wrong username or password, please try again...");
                }
            }
            else
            {
                accesso = false;
                throw new Exception("Wrong username or password, please try again...");
            }

            if (accesso)
            {
                _UserName = _DtUser.Rows[0]["FirstName"].ToString().Trim() + " " + _DtUser.Rows[0]["LastName"].ToString().Trim();
                _UserID = (int)_DtUser.Rows[0]["UserID"];
                this.LocationID = (int)_DtUser.Rows[0]["LocationID"];
                this.ChangePassword = (bool)_DtUser.Rows[0]["ChangePassword"];
                this.Dias30 = (bool)_DtUser.Rows[0]["Dias30"];
                this.Dias30Date = (DateTime)_DtUser.Rows[0]["Dias30Date"];
                this.Time1 = _DtUser.Rows[0]["Time1"].ToString();
                this.Time2 = _DtUser.Rows[0]["Time2"].ToString();
                this.AllDay = (bool)_DtUser.Rows[0]["AllDay"];
                this.Lunes = (bool)_DtUser.Rows[0]["Lunes"];
                this.Martes = (bool)_DtUser.Rows[0]["Martes"];
                this.Miercoles = (bool)_DtUser.Rows[0]["Miercoles"];
                this.Jueves = (bool)_DtUser.Rows[0]["Jueves"];
                this.Viernes = (bool)_DtUser.Rows[0]["Viernes"];
                this.Sabado = (bool)_DtUser.Rows[0]["Sabado"];
                this.Domingo = (bool)_DtUser.Rows[0]["Domingo"];
                this.DealerID = _DtUser.Rows[0]["DealerID"].ToString();
                this.Agent = _DtUser.Rows[0]["Agent"].ToString();
                this.AgentVI = _DtUser.Rows[0]["AgentVI"].ToString();
                this.FilterAgent = (bool)_DtUser.Rows[0]["FilterAgent"];
                this.FilterDealer = (bool)_DtUser.Rows[0]["FilterDealer"];
            }
            return accesso;
        }

		#endregion

		#region FillProperties
		private static Login FillLoginProperties(Login login)
		{
			login.UserID			= (int)  login._DtUser.Rows[0]["UserID"];
			login.AccountDisable	= (bool) login._DtUser.Rows[0]["AccountDisable"];
			login.ChangePassword	= (bool) login._DtUser.Rows[0]["ChangePassword"];
            login.Dias30 = (bool)login._DtUser.Rows[0]["Dias30"];
            login.Dias30Date = (DateTime)login._DtUser.Rows[0]["Dias30Date"];
			login.UserName			= login._DtUser.Rows[0]["UserName"].ToString();
			login.Password			= login._DtUser.Rows[0]["Password"].ToString();
			login.OldPassword		= login._DtUser.Rows[0]["Password"].ToString().Trim();
			login.FirstName			= login._DtUser.Rows[0]["FirstName"].ToString();
			login.LastName			= login._DtUser.Rows[0]["LastName"].ToString();
			login.LocationID	    = (int)  login._DtUser.Rows[0]["LocationID"];
			login.EntryDate  	    = (DateTime) login._DtUser.Rows[0]["EntryDate"];
			login.Comments          = login._DtUser.Rows[0]["Comments"].ToString();
			login.Time1             = login._DtUser.Rows[0]["Time1"].ToString();
			login.Time2             = login._DtUser.Rows[0]["Time2"].ToString();
			login.AllDay            = (bool) login._DtUser.Rows[0]["AllDay"];
			login.Lunes             = (bool) login._DtUser.Rows[0]["Lunes"];
			login.Martes            = (bool) login._DtUser.Rows[0]["Martes"];
			login.Miercoles         = (bool) login._DtUser.Rows[0]["Miercoles"];
			login.Jueves            = (bool) login._DtUser.Rows[0]["Jueves"];
			login.Viernes           = (bool) login._DtUser.Rows[0]["Viernes"];
			login.Sabado            = (bool) login._DtUser.Rows[0]["Sabado"];
			login.Domingo           = (bool) login._DtUser.Rows[0]["Domingo"];
			login.DealerID          = login._DtUser.Rows[0]["DealerID"].ToString();
            login.Agent             = login._DtUser.Rows[0]["Agent"].ToString();
            login.AgentVI           = login._DtUser.Rows[0]["AgentVI"].ToString();
            login.FilterAgent = (bool)login._DtUser.Rows[0]["FilterAgent"];
            login.FilterDealer = (bool)login._DtUser.Rows[0]["FilterDealer"];
			
			return login;
		}
		#endregion

		#region Groups & Permissions
		public void SaveGroupAndPermissions(int authenticatedGroupID,ListBox lbxSelected)
		{
			GroupAndPermissionsVerify(authenticatedGroupID,lbxSelected);
		}

		private void GroupAndPermissionsVerify(int authenticatedGroupID,ListBox lbxSelected)
		{
			DataTable dt = Login.GetPermissionByGroupID(authenticatedGroupID);
	
			for (int i=0;i<=dt.Rows.Count-1;i++)
			{
				bool exist = false;
				int rec = 0;
				for (int a =0;a<=lbxSelected.Items.Count-1;a++)
				{
					rec=a;
					if((int) dt.Rows[i]["AuthenticatedPermissionID"] == int.Parse(lbxSelected.Items[a].Value))
					{
						exist = true;
					}
				}
				if(!exist)
				{
					int permissionID;
					if(lbxSelected.Items.Count ==0)
					{
						permissionID = 0;
					}
					else
					{
						permissionID= int.Parse(lbxSelected.Items[rec].Value);
					}
					SaveGroupAndPermission(authenticatedGroupID,(int) dt.Rows[i]["AuthenticatedGroupPermissionID"],permissionID,"Deleted");
				}
			}

			for (int i=0;i<=lbxSelected.Items.Count-1;i++)
			{
				bool exist = false;
				int rec = 0;
				for (int a =0;a<=dt.Rows.Count-1;a++)
				{
					rec=a;
					if((int) dt.Rows[a]["AuthenticatedPermissionID"] == int.Parse(lbxSelected.Items[i].Value))
					{
						exist = true;
					}
				}
				
				if(!exist)
				{
					int authenticatedGroupPermissionID = 0;				
					SaveGroupAndPermission(authenticatedGroupID,authenticatedGroupPermissionID,int.Parse(lbxSelected.Items[i].Value),"Add");
				}
			}
		}

		private void SaveGroupAndPermission(int authenticatedGroupID,int authenticatedGroupPermissionID, int permissionID,string mode)
		{
			Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
			try
			{
				Executor.BeginTrans();
				switch (mode)
				{
					case "Add":  
						int autGroupPermissionID = Executor.Insert("AddAuthenticatedGroupPermission",this.GetInsertAuthenticatedGroupPermissionXml(authenticatedGroupID,permissionID));
						break;
					case "Deleted":  
						Executor.Delete("DeleteAuthenticatedGroupPermission",this.GetDeleteAuthenticatedGroupPermissionXml(authenticatedGroupPermissionID));
						break;
				}
						
				Executor.CommitTrans();
			}
			catch (Exception xcp)
			{
				Executor.RollBackTrans();
				throw new Exception("Error while trying to save the User. "+xcp.Message,xcp);
			}
		}

		public static DataTable GetPermissions(int authenticatedGroupID)
		{
			DataTable dt = GetPermissionDB(authenticatedGroupID);

			return dt;
		}

		private XmlDocument GetInsertAuthenticatedGroupPermissionXml(int authenticatedGroupID, int permissionID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedGroupID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + authenticatedGroupID + "</value>");
			sb.Append("</parameter>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedPermissionID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + permissionID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

		private XmlDocument GetDeleteAuthenticatedGroupPermissionXml(int authenticatedGroupPermissionID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedGroupPermissionID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + authenticatedGroupPermissionID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			return xmlDoc;
		}

		private static DataTable GetPermissionDB(int authenticatedGroupID)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			XmlDocument xmlDoc = new XmlDocument();

			sb.Append("<parameters>");
			sb.Append("<parameter>");
			sb.Append("<name>AuthenticatedGroupID</name>");
			sb.Append("<type>int</type>");
			sb.Append("<value>" + authenticatedGroupID + "</value>");
			sb.Append("</parameter>");
			sb.Append("</parameters>");
			xmlDoc.InnerXml = sb.ToString();
			sb = null;

			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dt = exec.GetQuery("GetAuthenticatedGroupPermissionByAuthenticatedGroupID",xmlDoc);
			return dt;
		}

		#endregion
	}
}
