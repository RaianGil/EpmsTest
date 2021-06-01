using System;
using System.Data;
using Baldrich.DBRequest;
using System.Xml;
using EPolicy.XmlCooker;

namespace EPolicy.LookupTables
{
 	public class LookupTables
	{
		
		#region Public operators
		
		public LookupTables()
		{
		}

		public static DataTable GetTableTaskStatusByTaskType(int TaskType)
		{		
			DataTable dt = GetTableTaskStatusByTaskTypeDB(TaskType);
			return dt;
		}

		public static DataTable GetTable(string tableName)
		{		
			DataTable dt = GetTableByTableName(tableName);
			return dt;
		}

        public static DataTable GetUsersByLetters(string Letter)
        {
            DataTable dt = GetUsersByLetterSP(Letter);
            return dt;
        }

		public static string GetID(string tableName, string description)
		{
			DataTable dt = GetTableByTableName(tableName);
			
			string IDCode ="";

			for(int i=0;i<= dt.Rows.Count-1; i++)
			{
				if (description.ToUpper().Trim() == 
					dt.Rows[i][tableName+"Desc"].ToString().ToUpper().Trim())
				{
					IDCode = dt.Rows[i][tableName+"ID"].ToString().Trim();
				}
			}
			return IDCode;
		}

        public static string GetIDToVehicleRentalOPP(string tableName, string description)
        {
            DataTable dt = GetTableByTableName(tableName);

            string IDCode = "";

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (description.ToUpper().Trim() ==
                    dt.Rows[i]["VehicleRentalDesc"].ToString().ToUpper().Trim())
                {
                    IDCode = dt.Rows[i]["VehicleRentalID"].ToString().Trim();
                }
            }
            return IDCode;
        }

		public static string GetDescription(string tableName, string ID)
		{
			DataTable dt = GetTableByTableName(tableName);
			
			string description ="";

			for(int i=0;i<= dt.Rows.Count-1; i++)
			{
				if (ID.ToUpper().Trim() == 
					dt.Rows[i][tableName+"ID"].ToString().ToUpper().Trim())
				{
					description = dt.Rows[i][tableName+"Desc"].ToString().Trim();
				}
			}
			return description;
		}

        public static string GetDescriptionToVSCVehicleMake(string tableName, string ID)
        {
            DataTable dt = GetTableByTableName(tableName);

            string description = "";

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (ID.ToUpper().Trim() ==
                    dt.Rows[i]["VehicleMakeID"].ToString().ToUpper().Trim())
                {
                    description = dt.Rows[i]["VehicleMakeDesc"].ToString().Trim();
                }
            }
            return description;
        }

        public static string GetDescriptionToVSCVehicleModel(string tableName, string ID)
        {
            DataTable dt = GetTableByTableName(tableName);

            string description = "";

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (ID.ToUpper().Trim() ==
                    dt.Rows[i]["VehicleModelID"].ToString().ToUpper().Trim())
                {
                    description = dt.Rows[i]["VehicleModelDesc"].ToString().Trim();
                }
            }
            return description;
        }

        public static string GetDescriptionToVehicleUse(string tableName, string ID)
        {
            DataTable dt = GetTableByTableName(tableName);

            string description = "";

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (ID.ToUpper().Trim() ==
                    dt.Rows[i]["VehicleUseID"].ToString().ToUpper().Trim())
                {
                    description = dt.Rows[i]["VehicleUseDesc"].ToString().Trim();
                }
            }
            return description;
        }

        public static string GetDescriptionToVehicleTerritory(string tableName, string ID)
        {
            DataTable dt = GetTableByTableName(tableName);

            string description = "";

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (ID.ToUpper().Trim() ==
                    dt.Rows[i]["VehicleTerritoryID"].ToString().ToUpper().Trim())
                {
                    description = dt.Rows[i]["VehicleTerritoryDesc"].ToString().Trim();
                }
            }
            return description;
        }

        public static string GetFBRegionDescription(string ID)
        {
            DataTable dt = GetTableByTableNameFBRegion(ID);

            string description = "";

            if (dt.Rows.Count != 0)
            {
                description = dt.Rows[0]["FBRegionDesc"].ToString().Trim();
            }
            return description;
        }

		public static DataTable GetLookupTableFromSP(int LookupTableID, string SPname)
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			DataTable dtResult = 
				exec.GetQuery(SPname, 
				GetRetrieveLookupTableFromSPxml(LookupTableID));
			return dtResult;
		}
		
		public static DataTable GetLookupTableFromSP(string LookupTableName, string SPname)
		{   
			return GetLookupTableFromSP(
				GetLookupTableIdFromTableName(LookupTableName), SPname);
		}

        public static DataTable GetVSCLookupTableFromSP(int LookupTableID, string SPname)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            DataTable dtResult =
                exec.GetQuery(SPname,
                GetRetrieveLookupTableFromSPxml(LookupTableID));
            return dtResult;
        }

        public static DataTable GetVSCLookupTableFromSP(string LookupTableName, string SPname)
        {
            return GetLookupTableFromSP(
                GetLookupTableIdFromTableName(LookupTableName), SPname);
        }

		public static int Add(int LookupTableID, string Description)
		{
			Validate(LookupTableID, 0, Description, true);

			return ExecuteAddQuery(GetAddXml(LookupTableID, Description));
		}

		public static int Add(string LookupTableName, string Description)
		{
			return Add(
				GetLookupTableIdFromTableName(LookupTableName.Trim()), 
				Description);
		}

        public static int AddVSC(int LookupTableID, string Description)
        {
            ValidateVSC(LookupTableID, 0, Description, true);

            return ExecuteVSCAddQuery(GetAddXml(LookupTableID, Description));
        }

        public static int AddVSCBankFee(string BankID, string EffectiveDate, double BankFee, int PolicyClassID)
        {

            return ExecuteVSCAddBankFeeQuery(GetAddBankFeeXml(BankID, EffectiveDate, BankFee, PolicyClassID));
   
        }

        public static int AddVSCProfitPremier(string CompanyDealerID, string EffectiveDate, double ProfitPremier, int PolicyClassID)
        {

            return ExecuteVSCAddProfitPremierQuery(GetAddProfitPremierXml(CompanyDealerID, EffectiveDate, ProfitPremier, PolicyClassID));

        }

        public static int AddVSCConcurso(string CompanyDealerID, string EffectiveDate, double Concurso, int PolicyClassID)
        {

            return ExecuteVSCAddConcursoQuery(GetAddConcursoXml(CompanyDealerID, EffectiveDate, Concurso, PolicyClassID));

        }

        public static int AddVSCProfitDealer(string CompanyDealerID, string EffectiveDate, double ProfitDealer, int PolicyClassID)
        {

            return ExecuteVSCAddProfitDealerQuery(GetAddProfitDealerXml(CompanyDealerID, EffectiveDate, ProfitDealer, PolicyClassID));

        }

		public static void Update(
			int LookupTableID, int DescriptionID, string Description)
		{
			Validate(LookupTableID, DescriptionID, Description, false);
			
			ExecuteUpdateQuery(
				GetUpdateLookupTableDescriptionXml(LookupTableID, DescriptionID,
				Description));
		}

		public static void Update(string LookupTableName, int DescriptionID, string Description)
		{
			Update(GetLookupTableIdFromTableName(LookupTableName.Trim()), DescriptionID,
				Description);
		}

        public static void UpdateVSC(
            int LookupTableID, int DescriptionID, string Description)
        {
            ValidateVSC(LookupTableID, DescriptionID, Description, false);

            ExecuteVSCUpdateQuery(
                GetUpdateLookupTableDescriptionXml(LookupTableID, DescriptionID,
                Description));
        }
        public static void UpdateVSCBankFee(
            int VSCBankFeeID, string EffectiveDate, string BankFee)
        {
            ExecuteVSCBankFeeUpdateQuery(
                GetUpdateVSCBankFeeXml(VSCBankFeeID, EffectiveDate,
                BankFee));
        }

        public static void UpdateVSCProfitPremier(
        int VSCProfitPremierID, string EffectiveDate, string ProfitPremier)
        {
            ExecuteVSCProfitPremierUpdateQuery(
                GetUpdateVSCProfitPremierXml(VSCProfitPremierID, EffectiveDate,
                ProfitPremier));
        }



        public static void UpdateVSCConcurso(
        int VSCConcursoID, string EffectiveDate, string Concurso)
        {
            ExecuteVSCConcursoUpdateQuery(
                GetUpdateVSCConcursoXml(VSCConcursoID, EffectiveDate,
                Concurso));
        }

        public static void UpdateVSCProfitDealer(
        int VSCProfitDealerID, string EffectiveDate, string ProfitDealer)
        {
            ExecuteVSCProfitDealerUpdateQuery(
                GetUpdateVSCProfitDealerXml(VSCProfitDealerID, EffectiveDate,
                ProfitDealer));
        }


        public static void DeleteVSC(int LookupTableID, int DescriptionID)
        {
            ExecuteVSCDeleteQuery(GetDeleteLookupTableDescriptionXml(
                LookupTableID, DescriptionID));
        }

        public static void DeleteVSCBankFee(int VSCBankFeeID)
        {
            ExecuteVSCBankFeeDeleteQuery(GetDeleteVSCBankFeeXml(
                VSCBankFeeID));
        }

		public static void Delete(int LookupTableID, int DescriptionID)
		{
			ExecuteDeleteQuery(GetDeleteLookupTableDescriptionXml(
				LookupTableID, DescriptionID));
		}

		public static void Delete(string LookupTableName, int DescriptionID)
		{
			Delete(GetLookupTableIdFromTableName(LookupTableName), DescriptionID);
		}

		public static void Delete(
			string LookupTableName, int DescriptionID, bool LookupTableMetadataDescription)
		{
			if(LookupTableMetadataDescription)
			{
				Delete(GetLookupTableIdFromTableName(LookupTableName), DescriptionID, true);
			}
			else
			{
				Delete(LookupTableName, DescriptionID);
			}
		}
		
		public static void Delete(
			int LookupTableID, int DescriptionID, bool LookupTableMetadataDescription)
		{
			if(LookupTableMetadataDescription)
			{
				ExecuteDeleteQuery(GetDeleteLookupTableDescriptionXml(
					LookupTableID, DescriptionID), true);
			}
			else
			{
				Delete(LookupTableID, DescriptionID);
			}
		}

		public static string GetTableMaintenancePathFromTableID(int TableID)
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			try
			{
				DataTable dtResult = 
					exec.GetQuery("GetLookupTableMaintenancePathFromLookupTableID",
					GetTableMaintenancePathFromTableIdXml(TableID));
				return dtResult.Rows[0]["MaintenancePagePath"].ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("Could not retrieve maintenance path for this id.", ex);
			}
		}

        public static string GetVSCTableMaintenancePathFromTableID(int TableID)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            try
            {
                DataTable dtResult =
                    exec.GetQuery("GetVSCLookupTableMaintenancePathFromLookupTableID",
                    GetTableMaintenancePathFromTableIdXml(TableID));
                return dtResult.Rows[0]["MaintenancePagePath"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve maintenance path for this id.", ex);
            }
        }

		public static DataTable GetValuePairLookupTableNames()
		{
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			
			return exec.GetQuery("GetValuePairLookupTableNames", 
				GetValuePairLookupTableNamesXml());
		}

		public static DataTable GetNonValuePairLookupTableNames()
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			
			return exec.GetQuery("GetNonValuePairLookupTableNames",
				GetNonValuePairLookupTableNamesXml());
		}

        // Anadido por: Yamil De Jesus
        // Date: 5/3/2010
        // VSC Maintenance
        public static DataTable GetVSCNonValuePairLookupTableNames()
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetVSCNonValuePairLookupTableNames",
                GetNonValuePairLookupTableNamesXml());
        }

        public static DataTable GetVSCValuePairLookupTableNames()
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetVSCValuePairLookupTableNames",
                GetNonValuePairLookupTableNamesXml());
        }

        public static DataTable GetVSCAllLookupTableNames()
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetVSCAllLookupTableNames",
                GetNonValuePairLookupTableNamesXml());
        }

		public static DataTable GetLookupTableNameFromTableID(int LookupTableID)
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			
			return exec.GetQuery("GetLookupTableNameFromTableID",
				GetLookupTableNameFromTableIdXml(LookupTableID));
		}

        public static DataTable GetVSCLookupTableNameFromTableID(int LookupTableID)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetVSCLookupTableNameFromTableID",
                GetLookupTableNameFromTableIdXml(LookupTableID));
        }

		public static DataTable GetNonValuePairLookupTableSearchFields(
			int LookupTableID, int Top, bool IsTopNull)
		{
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			return exec.GetQuery("GetNonValuePairLookupTableSearchFields", 
				GetNonValuePairLookupTableSearchFieldsXml(LookupTableID,
				Top, IsTopNull));
		}

        public static DataTable GetVSCNonValuePairLookupTableSearchFields(
            int LookupTableID, int Top, bool IsTopNull)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetVSCNonValuePairLookupTableSearchFields",
                GetNonValuePairLookupTableSearchFieldsXml(LookupTableID,
                Top, IsTopNull));
        }

		public static DataTable GetRecordsForNonValuePairLookupTable(
			int LookupTableID, string SearchFieldA, string SearchCriterionA,
			string SearchFieldB, string SearchCriterionB, bool Metadata)
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			return exec.GetQuery("GetNonValuePairLookupTableRecordByCriteria", 
				GetRecordsForNonValuePairLookupTableXml(LookupTableID,
				SearchFieldA, SearchCriterionA,	SearchFieldB, SearchCriterionB, Metadata));
		}

        public static DataTable GetVSCRecordsForNonValuePairLookupTable(
            int LookupTableID, string SearchFieldA, string SearchCriterionA,
            string SearchFieldB, string SearchCriterionB, bool Metadata)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            return exec.GetQuery("GetVSCNonValuePairLookupTableRecordByCriteria",
                GetRecordsForNonValuePairLookupTableXml(LookupTableID,
                SearchFieldA, SearchCriterionA, SearchFieldB, SearchCriterionB, Metadata));
        }

		public static DataTable GetLookupTableNamesNotInMetadataStore()
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

			return exec.GetQuery("GetLookupTableNamesNotInMetadataStore", 
				GetLookupTableNamesNotInMetadataStoreXml());
		}

		public static int GetLookupTableIdFromTableName(string TableName)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("LookupTableName",
				SqlDbType.VarChar, 50, TableName.Trim(),
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
			DataTable dtResult = null;
			try
			{
				dtResult = exec.GetQuery("GetLookupTableIdFromTableName", xmlDoc);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}


            
			if(dtResult.Rows != null)
			{
				return int.Parse(dtResult.Rows[0]["LookupTableID"].ToString());
			}
			else
			{
				return 0;
			}
		}//End GetLookupTableIdFromTableName

        public static DataTable GetVSCBankFeeList(string BankID, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BankID",
                SqlDbType.VarChar, 50, BankID.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 4, PolicyClassID.ToString(),
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
            DataTable dtResult = null;
            try
            {
                dtResult = exec.GetQuery("GetVSCBankFeeListByBank", xmlDoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve prospect by criteria.", ex);
            }

            return dtResult;

            //if (dtResult.Rows != null)
            //{
            //    return dtResult;
            //}
            //else
            //{
            //    return 0;
            //}
        }

        public static DataTable GetVSCProfitPremier(string CompanyDealerID, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("CompanyDealer",
                SqlDbType.VarChar, 50, CompanyDealerID.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, PolicyClassID.ToString(),
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
            DataTable dtResult = null;
            try
            {
                dtResult = exec.GetQuery("GetVSCProfitPremierListByDealer", xmlDoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve prospect by criteria.", ex);
            }

            return dtResult;

        }

        public static DataTable GetVSCConcurso(string CompanyDealerID, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("CompanyDealer",
                SqlDbType.VarChar, 50, CompanyDealerID.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
                SqlDbType.Int, 0, PolicyClassID.ToString(),
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
            DataTable dtResult = null;
            try
            {
                dtResult = exec.GetQuery("GetVSCConcursoListByDealer", xmlDoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve prospect by criteria.", ex);
            }

            return dtResult;

        }

        public static DataTable GetVSCProfitDealer(string CompanyDealerID, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("CompanyDealer",
                SqlDbType.VarChar, 50, CompanyDealerID.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
                SqlDbType.Int, 0, PolicyClassID.ToString(),
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
            DataTable dtResult = null;
            try
            {
                dtResult = exec.GetQuery("GetVSCProfitDealerListByDealer", xmlDoc);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve prospect by criteria.", ex);
            }

            return dtResult;

        }

		public static DataTable GetLookupTableNames()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[0];

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
			DataTable dtResult = null;
			try
			{
				dtResult = exec.GetQuery("GetLookupTableNames", xmlDoc);
				return dtResult;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}
		}

        public static DataTable GetCiudadByZipCode(string Letter)
         {
             DbRequestXmlCookRequestItem[] cookItems =
                 new DbRequestXmlCookRequestItem[1];

             DbRequestXmlCooker.AttachCookItem("ZipCode",
             SqlDbType.VarChar, 20, Letter.ToString(),
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
                 dt = exec.GetQuery("GetCiudadByZipCode", xmlDoc);
                 return dt;
             }
             catch (Exception ex)
             {
                 throw new Exception("Could not retrieve prospect by criteria.", ex);
             }
         }

         public static DataTable GetZipCodeByDistinctCiudad(string Letter)
         {

             DbRequestXmlCookRequestItem[] cookItems =
                 new DbRequestXmlCookRequestItem[1];

             DbRequestXmlCooker.AttachCookItem("Ciudad",
             SqlDbType.VarChar, 20, Letter.ToString(),
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
                 dt = exec.GetQuery("GetZipCodeByDistinctCiudad", xmlDoc);
                 return dt;
             }
             catch (Exception ex)
             {
                 throw new Exception("Could not retrieve prospect by criteria.", ex);
             }
         }


		

		public static DataTable GetLookupTableColumnsByLookupTableID(int LookupTableID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
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
			DataTable dtResult = null;
			try
			{
				dtResult = exec.GetQuery("GetLookupTableColumnsByLookupTableID", xmlDoc);
				return dtResult;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}

		}

		#endregion

		#region Private operators
        
		private static int ExecuteAddQuery(XmlDocument XmlDoc)
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			return exec.Insert("AddLookupTableDescription", XmlDoc);
		}

        private static int ExecuteVSCAddQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            return exec.Insert("AddVSCLookupTableDescription", XmlDoc);
        }

        private static int ExecuteVSCAddBankFeeQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            return exec.Insert("AddVSCBankFee", XmlDoc);
        }

        private static int ExecuteVSCAddProfitPremierQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            return exec.Insert("AddVSCProfitPremierByDealer", XmlDoc);
        }

        private static int ExecuteVSCAddConcursoQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            return exec.Insert("AddVSCConcursoByDealer", XmlDoc);
        }

        private static int ExecuteVSCAddProfitDealerQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            return exec.Insert("AddVSCProfitDealerByDealer", XmlDoc);
        }

		private static void ExecuteUpdateQuery(XmlDocument XmlDoc)
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			exec.Update("UpdateLookupTableDescription", XmlDoc);
		}

        private static void ExecuteVSCUpdateQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            exec.Update("UpdateVSCLookupTableDescription", XmlDoc);
        }

        private static void ExecuteVSCBankFeeUpdateQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            exec.Update("UpdateVSCBankFee", XmlDoc);
        }

        private static void ExecuteVSCProfitPremierUpdateQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            exec.Update("UpdateVSCProfitPremier", XmlDoc);
        }


        private static void ExecuteVSCConcursoUpdateQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            exec.Update("UpdateVSCConcurso", XmlDoc);
        }

        private static void ExecuteVSCProfitDealerUpdateQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            exec.Update("UpdateVSCProfitDealer", XmlDoc);
        }

		private static void ExecuteDeleteQuery(XmlDocument XmlDoc)
		{
			Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
			exec.Delete("DeleteLookupTableDescription", XmlDoc);
		}

        private static void ExecuteVSCDeleteQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            exec.Delete("DeleteVSCLookupTableDescription", XmlDoc);
        }

        private static void ExecuteVSCBankFeeDeleteQuery(XmlDocument XmlDoc)
        {
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            exec.Delete("DeleteVSCBankFee", XmlDoc);
        }

		private static void ExecuteDeleteQuery(
			XmlDocument XmlDoc, bool LookupTableMetadataDescription)
		{
			if(LookupTableMetadataDescription)
			{
				Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
				exec.Delete("DeleteLookupTableMetadataDescription", XmlDoc);
			}
			else
			{
				ExecuteDeleteQuery(XmlDoc);
			}
		}

        private static void ExecuteVSCDeleteQuery(
            XmlDocument XmlDoc, bool LookupTableMetadataDescription)
        {
            if (LookupTableMetadataDescription)
            {
                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                exec.Delete("DeleteVSCLookupTableMetadataDescription", XmlDoc);
            }
            else
            {
                ExecuteDeleteQuery(XmlDoc);
            }
        }

		private static XmlDocument GetRecordsForNonValuePairLookupTableXml(
			int LookupTableID, string SearchFieldA, string SearchCriterionA,
			string SearchFieldB, string SearchCriterionB, bool Metadata)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[6];

			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SearchFieldA",
				SqlDbType.VarChar, 50, SearchFieldA.Trim(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("SearchCriterionA",
				SqlDbType.VarChar, 50, SearchCriterionA.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SearchFieldB",
				SqlDbType.VarChar, 50, SearchFieldB.Trim(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("SearchCriterionB",
				SqlDbType.VarChar, 50, SearchCriterionB.Trim(),
				ref cookItems);
			

			DbRequestXmlCooker.AttachCookItem("Metadata",
				SqlDbType.Bit, 0, Metadata.ToString(),
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

		private static XmlDocument GetNonValuePairLookupTableSearchFieldsXml(
			int LookupTableID, int Top, bool IsTopNull)
		{

		  DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];
			
			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("Top",
				SqlDbType.Int, 0, IsTopNull ? "NuLL": Top.ToString(),
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

		private static XmlDocument GetLookupTableNameFromTableIdXml(int LookupTableID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			
			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
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

		private static XmlDocument GetLookupTableNamesNotInMetadataStoreXml()
		{

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[0];

			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

		}

		private static XmlDocument GetValuePairLookupTableNamesXml()
		{

			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[0];

			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}

		}

		private static XmlDocument GetNonValuePairLookupTableNamesXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[0];

			try
			{
				return DbRequestXmlCooker.Cook(cookItems);
			}
			catch(Exception ex)
			{
				throw new Exception("Could not cook items.", ex);
			}


		}

		private static XmlDocument GetTableMaintenancePathFromTableIdXml(int LookupTableID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
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

		private static XmlDocument GetDeleteLookupTableDescriptionXml(int LookupTableID,
			int DescriptionID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];
			
			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
				ref cookItems);

			DbRequestXmlCooker.AttachCookItem("DescriptionID",
				SqlDbType.Int, 0, DescriptionID.ToString(),
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

        private static XmlDocument GetDeleteVSCBankFeeXml(int VSCBankFeeID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("VSCBankFeeID",
                SqlDbType.Int, 0, VSCBankFeeID.ToString(),
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

		private static XmlDocument GetUpdateLookupTableDescriptionXml(int LookupTableID,
			int DescriptionID, string Description)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[3];

			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("DescriptionID",
				SqlDbType.Int, 0, DescriptionID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Description",
				SqlDbType.VarChar, 50, Description.ToString(),
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

        private static XmlDocument GetUpdateVSCBankFeeXml(int VSCBankFeeID,
            string EffectiveDate, string BankFee)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("VSCBankFeeID",
                SqlDbType.Int, 0, VSCBankFeeID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankFee",
                SqlDbType.VarChar, 3, BankFee.ToString(),
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

        private static XmlDocument GetUpdateVSCProfitPremierXml(int VSCProfitPremierID,
        string EffectiveDate, string ProfitPremier)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("VSCProfitPremierID",
                SqlDbType.Int, 0, VSCProfitPremierID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ProfitPremier",
                SqlDbType.VarChar, 3, ProfitPremier.ToString(),
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

      

        private static XmlDocument GetUpdateVSCConcursoXml(int VSCConcursoID,
        string EffectiveDate, string Concurso)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("VSCConcursoID",
                SqlDbType.Int, 0, VSCConcursoID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Concurso",
                SqlDbType.VarChar, 3, Concurso.ToString(),
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

        private static XmlDocument GetUpdateVSCProfitDealerXml(int VSCProfitDealerID,
        string EffectiveDate, string ProfitDealer)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[3];

            DbRequestXmlCooker.AttachCookItem("VSCProfitDealerID",
                SqlDbType.Int, 0, VSCProfitDealerID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ProfitDealer",
                SqlDbType.VarChar, 3, ProfitDealer.ToString(),
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

		private static XmlDocument GetRetrieveLookupTableFromSPxml(int LookupTableID)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
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

		private static XmlDocument GetAddXml(int LookupTableID, string Description)
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[2];

			DbRequestXmlCooker.AttachCookItem("LookupTableID",
				SqlDbType.Int, 0, LookupTableID.ToString(),
				ref cookItems);
			
			DbRequestXmlCooker.AttachCookItem("Description",
				SqlDbType.VarChar, 50, Description.Trim(),
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

        private static XmlDocument GetAddBankFeeXml(string BankID, string EffectiveDate, double BankFee, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("BankID",
                SqlDbType.VarChar, 3, BankID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("BankFee",
            SqlDbType.VarChar, 3, BankFee.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, PolicyClassID.ToString(),
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

        private static XmlDocument GetAddProfitPremierXml(string CompanyDealerID, string EffectiveDate, double ProfitPremier, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
                SqlDbType.VarChar, 3, CompanyDealerID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ProfitPremier",
            SqlDbType.VarChar, 3, ProfitPremier.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 4, PolicyClassID.ToString(),
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

        private static XmlDocument GetAddConcursoXml(string CompanyDealerID, string EffectiveDate, double Concurso, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
                SqlDbType.VarChar, 3, CompanyDealerID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Concurso",
            SqlDbType.VarChar, 3, Concurso.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, PolicyClassID.ToString(),
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

        private static XmlDocument GetAddProfitDealerXml(string CompanyDealerID, string EffectiveDate, double ProfitDealer, int PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
                SqlDbType.VarChar, 3, CompanyDealerID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EffectiveDate",
                SqlDbType.VarChar, 50, EffectiveDate.Trim(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ProfitDealer",
            SqlDbType.VarChar, 3, ProfitDealer.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
            SqlDbType.Int, 0, PolicyClassID.ToString(),
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
		
		private static DataTable GetTableByTableName(string TableName)
		{
			
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[0];

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
				dt = exec.GetQuery("Get" + TableName, xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}
		
		}

        private static DataTable GetUsersByLetterSP(string Letter)
        {

            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("Letter",
            SqlDbType.Char, 1, Letter.ToString(),
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
                dt = exec.GetQuery("GetAuthenticatedUserByLetter", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve prospect by criteria.", ex);
            }

        }

        private static DataTable GetTableByTableNameFBRegion(string ID)
        {

            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("FBRegionID",
                SqlDbType.Int, 0, ID.ToString(),
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
                dt = exec.GetQuery("GetFBRegionByFBRegionID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve prospect by criteria.", ex);
            }

        }

		private static DataTable GetTableTaskStatusByTaskTypeDB(int TaskTypeID)
		{
			
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];

			DbRequestXmlCooker.AttachCookItem("TaskTypeID",
				SqlDbType.Int, 0, TaskTypeID.ToString(),
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
				 dt = exec.GetQuery("GetTableTaskStatusByTaskType", xmlDoc);
				return dt;
			}
			catch(Exception ex)
			{
				throw new Exception("Could not retrieve prospect by criteria.", ex);
			}
		
		}

		private static void Validate(
			int LookupTableID, int DescriptionID, string Description, bool Add)
		{
			string errorMessage = String.Empty;
			bool found = false;

			DataTable dt =  
				GetTable(
				GetLookupTableNameFromTableID(LookupTableID).Rows[0]["TableName"].ToString()); 

			if (Add)
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i][1].ToString().Trim().ToUpper() == Description.Trim().ToUpper())
					{
						errorMessage += "This Description is Already exist.";
						found = true;
					}
				}
			}
			else
			{
				for (int i=0; i<=dt.Rows.Count-1; i++)
				{
					if(dt.Rows[i][1].ToString().Trim().ToUpper() == Description.Trim().ToUpper() && dt.Rows[i][0].ToString().Trim() != DescriptionID.ToString().Trim())
					{
						errorMessage += "This Description is Already exist.";
						found = true;
					}
				}
			}

			if (found == true)
			{
				throw new Exception(errorMessage);
			}
		}
        private static void ValidateVSC(
            int LookupTableID, int DescriptionID, string Description, bool Add)
        {
            string errorMessage = String.Empty;
            bool found = false;

            DataTable dt =
                GetTable(
                GetVSCLookupTableNameFromTableID(LookupTableID).Rows[0]["TableName"].ToString());

            if (Add)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i][1].ToString().Trim().ToUpper() == Description.Trim().ToUpper())
                    {
                        errorMessage += "This Description is Already exist.";
                        found = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i][1].ToString().Trim().ToUpper() == Description.Trim().ToUpper() && dt.Rows[i][0].ToString().Trim() != DescriptionID.ToString().Trim())
                    {
                        errorMessage += "This Description is Already exist.";
                        found = true;
                    }
                }
            }

            if (found == true)
            {
                throw new Exception(errorMessage);
            }
        }
		#endregion
	}
}
