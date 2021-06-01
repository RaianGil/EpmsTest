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
    public class PersonalPackage : Policy
    {
        public PersonalPackage(bool isOPPQuote)
        {
            this.IsOPPQuote = isOPPQuote;
            this.AgentList = Policy.GetAgentListByPolicyClassID(12);
            this.DepartmentID = 0;
            this.PolicyClassID = 12;
            this.PolicyType = "MPP";
            this.InsuranceCompany = "001";
            this.Agency = "001";
            this.Agent = "000";
            this.SupplierID = "000";
            this.Bank = "000";
            this.Dealer = "000";
            this.CompanyDealer = "000";
            this.Status = "Inforce";
            this.Term = 12;
            this.EffectiveDate = DateTime.Now.ToShortDateString();
            this.ExpirationDate = DateTime.Now.AddMonths(12).ToShortDateString();
            this.TaskStatusID = int.Parse(LookupTables.LookupTables.GetID("TaskStatus", "Open"));


            if (this.IsOPPQuote)
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Personal Package Quote"));
            else
            {
                this.DepartmentID = 1;
                this.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Personal Package"));
            }

            // Para el History
            this._mode = (int)TaskControlMode.ADD;
        }

        #region Variable

        private PersonalPackage oldPersonalPackagee = null;
        private Properties _Properties = null;
        private DataTable _PropertiesCollection = null;
        private DataTable _PropertiesCollectionWithoutLiability = null;
        private DataTable _OPPPropertiesBankCollection1;
        private DataTable _OPPPropertiesBankCollection2;
        private DataTable _OPPPropertiesBankCollection3;
        private DataTable _OPPPropertiesBankCollection4;
        private DataTable _OPPPropertiesBankCollection5;
        private DataTable _OPPPropertiesBankCollection6;
        private DataTable _OPPPropertiesBankCollection7;
        private DataTable _OPPPropertiesBankCollection8;
        private DataTable _OPPPropertiesBankCollection9;
        private DataTable _OPPPropertiesBankCollection10;
        private DataTable _AdditionalCoveragesPropertiesCollection0 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection1 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection2 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection3 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection4 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection5 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection6 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection7 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection8 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection9 = null;
        private DataTable _AdditionalCoveragesPropertiesCollection10 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection1_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection2_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection3_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection4_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection5_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection6_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection7_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection8_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection9_14 = null;

        private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_1 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_2 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_3 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_4 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_5 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_6 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_7 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_8 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_9 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_10 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_11 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_12 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_13 = null;
        //private DataTable _OPPAdditionalCoveragesDetailPropertiesCollection10_14 = null;

        private PersonalLiability _PersonalLiability = null;
        private DataTable _PersonalLiabilityCollection = null;
        private DataTable _AdditionalCoveragesLiabilityCollection = null;
        private DataTable _dtOptimaPersonalPackage;
        private Umbrella _Umbrella = null;
        private QuoteAuto _QuoteAuto = null;
        private int _OptimaPersonalPackageID = 0;
        private double _PropertiesPremium = 0.00;
        private double _LiabilityPremium = 0.00;
        private double _AutoPremium = 0.00;
        private double _UmbrellaPremium = 0.00;
        private bool _IsOPPQuote = false;
        private int _TaskControlIDQuoteAuto = 0;
        private string _PropEnd = "";
        private string _LiabEnd = "";
        private string _AutoEnd = "";
        private string _UmbEnd = "";
        private string _AdditionalName = "";
        private DataTable _EndorsementCollection = null;
        private int _mode = (int)TaskControlMode.CLEAR;
        private string _EndoDateTemp = "";

        private TaskControl oldPolices = null;

        private bool _ReminderIsCero = true;
        #endregion

        #region Properties

        public bool IsOPPQuote
        {
            get
            {
                return (this._IsOPPQuote);
            }
            set
            {
                this._IsOPPQuote = value;
            }
        }

        public DataTable EndorsementCollection
        {
            get
            {
                //if (this._EndorsementCollection == null)
                this._EndorsementCollection = GetEndorsementCollection();
                return (this._EndorsementCollection);
            }
            set
            {
                this._EndorsementCollection = value;
            }
        }

        public DataTable PropertiesCollection
        {
            get
            {
                if (this._PropertiesCollection == null)
                    this._PropertiesCollection = DataTablePropertiesTemp();
                return (this._PropertiesCollection);
            }
            set
            {
                this._PropertiesCollection = value;
            }
        }

        public DataTable PropertiesCollectionsWithoutLiability
        {
            get
            {
                if (this._PropertiesCollectionWithoutLiability == null)
                    this._PropertiesCollectionWithoutLiability = DataTablePropertiesWithoutLiabilityTemp();
                return (this._PropertiesCollectionWithoutLiability);
            }
            set
            {
                this._PropertiesCollectionWithoutLiability = value;
            }
        }

        public DataTable PersonalLiabilityCollection
        {
            get
            {
                if (this._PersonalLiabilityCollection == null)
                    this._PersonalLiabilityCollection = DataTablePersonalLiabilityTemp();
                return (this._PersonalLiabilityCollection);
            }
            set
            {
                this._PersonalLiabilityCollection = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection0
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection0 == null)
                    this._AdditionalCoveragesPropertiesCollection0 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection0);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection0 = value;
            }
        }


        public DataTable AdditionalCoveragesPropertiesCollection1
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection1 == null)
                    this._AdditionalCoveragesPropertiesCollection1 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection1);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection1 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection2
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection2 == null)
                    this._AdditionalCoveragesPropertiesCollection2 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection2);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection2 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection3
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection3 == null)
                    this._AdditionalCoveragesPropertiesCollection3 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection3);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection3 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection4
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection4 == null)
                    this._AdditionalCoveragesPropertiesCollection4 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection4);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection4 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection5
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection5 == null)
                    this._AdditionalCoveragesPropertiesCollection5 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection5);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection5 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection6
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection6 == null)
                    this._AdditionalCoveragesPropertiesCollection6 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection6);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection6 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection7
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection7 == null)
                    this._AdditionalCoveragesPropertiesCollection7 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection7);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection7 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection8
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection8 == null)
                    this._AdditionalCoveragesPropertiesCollection8 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection8);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection8 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection9
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection9 == null)
                    this._AdditionalCoveragesPropertiesCollection9 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection9);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection9 = value;
            }
        }

        public DataTable AdditionalCoveragesPropertiesCollection10
        {
            get
            {
                if (this._AdditionalCoveragesPropertiesCollection10 == null)
                    this._AdditionalCoveragesPropertiesCollection10 = DataTableAdditionalCoveragePropertiesTemp();
                return (this._AdditionalCoveragesPropertiesCollection10);
            }
            set
            {
                this._AdditionalCoveragesPropertiesCollection10 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection1_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection1_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection1_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection1_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection1_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection2_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection2_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection2_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection2_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection2_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection3_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection3_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection3_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection3_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection3_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection4_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection4_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection4_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection4_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection4_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection5_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection5_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection5_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection5_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection5_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection6_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection6_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection6_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection6_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection6_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection7_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection7_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection7_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection7_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection7_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection8_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection8_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection8_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection8_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection8_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection9_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection9_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection9_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection9_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection9_1 = value;
            }
        }

        public DataTable OPPAdditionalCoveragesDetailPropertiesCollection10_1
        {
            get
            {
                if (this._OPPAdditionalCoveragesDetailPropertiesCollection10_1 == null)
                    this._OPPAdditionalCoveragesDetailPropertiesCollection10_1 = DataTableOPPAdditionalCoverageDetailTemp();
                return (this._OPPAdditionalCoveragesDetailPropertiesCollection10_1);
            }
            set
            {
                this._OPPAdditionalCoveragesDetailPropertiesCollection10_1 = value;
            }
        }

        public Properties Properties
        {
            get
            {
                if (this._Properties == null)
                    this._Properties = new Properties();
                return (this._Properties);
            }
            set
            {
                this._Properties = value;
            }
        }

        public PersonalLiability PersonalLiability
        {
            get
            {
                if (this._PersonalLiability == null)
                    this._PersonalLiability = new PersonalLiability();
                return (this._PersonalLiability);
            }
            set
            {
                this._PersonalLiability = value;
            }
        }

        public DataTable AdditionalCoveragesLiabilityCollection
        {
            get
            {
                if (this._AdditionalCoveragesLiabilityCollection == null)
                    this._AdditionalCoveragesLiabilityCollection = DataTableAdditionalCoverageLiabilityTemp();
                return (this._AdditionalCoveragesLiabilityCollection);
            }
            set
            {
                this._AdditionalCoveragesLiabilityCollection = value;
            }
        }

        public Umbrella Umbrella
        {
            get
            {
                if (this._Umbrella == null)
                    this._Umbrella = new Umbrella();
                return (this._Umbrella);
            }
            set
            {
                this._Umbrella = value;
            }
        }

        public QuoteAuto QuoteAuto
        {
            get
            {
                if (this._QuoteAuto == null)
                    this._QuoteAuto = new QuoteAuto(false);
                return (this._QuoteAuto);
            }
            set
            {
                this._QuoteAuto = value;
            }
        }

        public int OptimaPersonalPackageID
        {
            get
            {
                return this._OptimaPersonalPackageID;
            }
            set
            {
                this._OptimaPersonalPackageID = value;
            }
        }

        public double PropertiesPremium
        {
            get
            {
                double totprem = 0.00;
                if (this._PropertiesCollection != null)
                {
                    for (int i = 0; this._PropertiesCollection.Rows.Count - 1 >= i; i++)
                    {
                        totprem = totprem + double.Parse(this._PropertiesCollection.Rows[i]["TotalPremium"].ToString());
                    }
                }

                this._PropertiesPremium = totprem;
                return this._PropertiesPremium;
            }
            set
            {
                this._PropertiesPremium = value;
            }
        }

        public double LiabilityPremium
        {
            get
            {
                double totprem = 0.00;
                if (this._PersonalLiabilityCollection != null)
                {
                    for (int i = 0; this._PersonalLiabilityCollection.Rows.Count - 1 >= i; i++)
                    {
                        totprem = totprem + double.Parse(this._PersonalLiabilityCollection.Rows[i]["TotalPremium"].ToString());
                    }
                }
                this._LiabilityPremium = totprem;
                return this._LiabilityPremium;
            }
            set
            {
                this._LiabilityPremium = value;
            }
        }

        public double AutoPremium
        {
            get
            {
                double totprem = 0.00;
                if (this._QuoteAuto != null)
                {
                    totprem = double.Parse(this._QuoteAuto.TotalPremium.ToString());
                }
                this._AutoPremium = totprem;
                return this._AutoPremium;
            }
            set
            {
                this._AutoPremium = value;
            }
        }

        public double UmbrellaPremium
        {
            get
            {
                double totprem = 0.00;
                if (this._Umbrella != null)
                {
                    totprem = this._Umbrella.TotalUmbrellaPremium;
                }
                this._UmbrellaPremium = totprem;
                return this._UmbrellaPremium;
            }
            set
            {
                this._UmbrellaPremium = value;
            }
        }

        public int TaskControlIDQuoteAuto
        {
            get
            {
                return this._TaskControlIDQuoteAuto;
            }
            set
            {
                this._TaskControlIDQuoteAuto = value;
            }
        }

        public string PropEnd
        {
            get
            {
                return this._PropEnd;
            }
            set
            {
                this._PropEnd = value;
            }
        }
        public string LiabEnd
        {
            get
            {
                return this._LiabEnd;
            }
            set
            {
                this._LiabEnd = value;
            }
        }
        public string AutoEnd
        {
            get
            {
                return this._AutoEnd;
            }
            set
            {
                this._AutoEnd = value;
            }
        }
        public string UmbEnd
        {
            get
            {
                return this._UmbEnd;
            }
            set
            {
                this._UmbEnd = value;
            }
        }

        public string AdditionalName
        {
            get
            {
                return this._AdditionalName;
            }
            set
            {
                this._AdditionalName = value;
            }
        }

        public string EndoDateTemp
        {
            get
            {
                return this._EndoDateTemp;
            }
            set
            {
                this._EndoDateTemp = value;
            }
        }

        public DataTable OPPPropertiesBankCollection1
        {
            get
            {
                if (this._OPPPropertiesBankCollection1 == null)
                    this._OPPPropertiesBankCollection1 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection1);
            }
            set
            {
                this._OPPPropertiesBankCollection1 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection2
        {
            get
            {
                if (this._OPPPropertiesBankCollection2 == null)
                    this._OPPPropertiesBankCollection2 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection2);
            }
            set
            {
                this._OPPPropertiesBankCollection2 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection3
        {
            get
            {
                if (this._OPPPropertiesBankCollection3 == null)
                    this._OPPPropertiesBankCollection3 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection3);
            }
            set
            {
                this._OPPPropertiesBankCollection3 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection4
        {
            get
            {
                if (this._OPPPropertiesBankCollection4 == null)
                    this._OPPPropertiesBankCollection4 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection4);
            }
            set
            {
                this._OPPPropertiesBankCollection4 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection5
        {
            get
            {
                if (this._OPPPropertiesBankCollection5 == null)
                    this._OPPPropertiesBankCollection5 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection5);
            }
            set
            {
                this._OPPPropertiesBankCollection5 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection6
        {
            get
            {
                if (this._OPPPropertiesBankCollection6 == null)
                    this._OPPPropertiesBankCollection6 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection6);
            }
            set
            {
                this._OPPPropertiesBankCollection6 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection7
        {
            get
            {
                if (this._OPPPropertiesBankCollection7 == null)
                    this._OPPPropertiesBankCollection7 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection7);
            }
            set
            {
                this._OPPPropertiesBankCollection7 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection8
        {
            get
            {
                if (this._OPPPropertiesBankCollection8 == null)
                    this._OPPPropertiesBankCollection8 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection8);
            }
            set
            {
                this._OPPPropertiesBankCollection8 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection9
        {
            get
            {
                if (this._OPPPropertiesBankCollection9 == null)
                    this._OPPPropertiesBankCollection9 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection9);
            }
            set
            {
                this._OPPPropertiesBankCollection9 = value;
            }
        }

        public DataTable OPPPropertiesBankCollection10
        {
            get
            {
                if (this._OPPPropertiesBankCollection10 == null)
                    this._OPPPropertiesBankCollection10 = DataTableOPPPropertiesBankTemp();
                return (this._OPPPropertiesBankCollection10);
            }
            set
            {
                this._OPPPropertiesBankCollection10 = value;
            }
        }

        public bool ReminderIsCero
        {
            get { return _ReminderIsCero; }
            set { _ReminderIsCero = value; }
        }

        #endregion

        #region Public Methods

        public void SaveOptimaPersonalPackage(int UserID)
        {
            this.SavePol(UserID);
        }

        //public Properties GetProperties(int propertiesID)
        //{
        //    this.Properties = Properties.GetProperties(propertiesID,this.IsOPPQuote);
        //    return this.Properties;
        //}

        public void SavePropAddCov()
        {

        }


        // HISTORY IRIA AQUI?
        public override void SavePol(int UserID)
        {
            this._mode = (int)this.Mode;  // Se le asigna el mode de taskControl.
            this.PolicyMode = (int)this.Mode;  // Se le asigna el mode de taskControl.

            if (IsOPPQuote)
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

                if (IsEndorsement)
                {
                    if (this.Customer.CustomerNo.Trim() == "")
                        this.Customer.Mode = 1;
                    else
                        this.Customer.Mode = 2;

                    this.Customer.IsBusiness = false;
                    this.Customer.Save(UserID);

                    this.CustomerNo = this.Customer.CustomerNo;
                    this.ProspectID = this.Customer.ProspectID;
                }
            }
            else
            {
                this.Validate();
                base.ValidatePolicy();

                if (this._mode == 2)
                    oldPersonalPackagee = (PersonalPackage)PersonalPackage.GetTaskControlByTaskControlID(this.TaskControlID, UserID);

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

            if (IsOPPQuote) // AQUI
                base.SaveOPPQuote(UserID);    // Validate and Save Quote in Policy Class
            else
                base.SavePol(UserID);	// Validate and Save Policy

            SaveOptimaPersonalPackageDB(UserID);  // Save OptimaPersonalPackage

            //Salva Properties
            this.Properties.SaveProperties(UserID, this.PropertiesCollection, this.TaskControlID, this.IsOPPQuote); //Save Properties

            //Save BanksGRD
            this.Properties.DeleteOPPPropertiesBankByTaskControlID(this.TaskControlID, IsOPPQuote); // Solo para Policy no en Quote
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection1, this.TaskControlID, this.Properties.PropertiesID, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection2, this.TaskControlID, this.Properties.PropertiesID2, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection3, this.TaskControlID, this.Properties.PropertiesID3, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection4, this.TaskControlID, this.Properties.PropertiesID4, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection5, this.TaskControlID, this.Properties.PropertiesID5, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection6, this.TaskControlID, this.Properties.PropertiesID6, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection7, this.TaskControlID, this.Properties.PropertiesID7, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection8, this.TaskControlID, this.Properties.PropertiesID8, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection9, this.TaskControlID, this.Properties.PropertiesID9, IsOPPQuote);
            this.Properties.SaveOPPPropertiesBank(UserID, this.OPPPropertiesBankCollection10, this.TaskControlID, this.Properties.PropertiesID10, IsOPPQuote);


            //Antes de salvar la Propiedades elimina las cubiertas adiconales para anadirlas con los nuevos cambios.
            this.Properties.AditionalCoveragecs.DeleteAditionalCoveragePropertiesByTaskControlID(this.TaskControlID, this.IsOPPQuote);

            //Salva Additional Coverages for Properties y cambia propertieID de oppaddcovdetail
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection1;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID, this.TaskControlID, IsOPPQuote);


            //2
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection2;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID2;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID2, this.TaskControlID, IsOPPQuote);

            //3
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection3;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID3;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID3, this.TaskControlID, IsOPPQuote);


            //4
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection4;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID4;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID4, this.TaskControlID, IsOPPQuote);

            //5
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection5;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID5;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID5, this.TaskControlID, IsOPPQuote);

            //6
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection6;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID6;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID6, this.TaskControlID, IsOPPQuote);

            //7
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection7;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID7;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID7, this.TaskControlID, IsOPPQuote);


            //8
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection8;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID8;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID8, this.TaskControlID, IsOPPQuote);


            //9
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection9;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID9;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID9, this.TaskControlID, IsOPPQuote);


            //10
            this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp = this.AdditionalCoveragesPropertiesCollection10;
            if (this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows.Count > 0)
            {
                for (int i = 0; i < this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows.Count; i++)
                {
                    if (this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"].ToString().Trim() == this.Properties.AditionalCoveragecs.AditionalCoverageTableTemp.Rows[0]["PropertiesID"].ToString())
                    {
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.Rows[i]["PropertiesID"] = this.Properties.PropertiesID10;
                        this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.AcceptChanges();
                    }
                }
            }
            this.Properties.AditionalCoveragecs.SaveAditionalCoverage(UserID, this.Properties.PropertiesID10, this.TaskControlID, IsOPPQuote);

            //Salva AddCovDet
            this.Properties.SaveOPPAdditionalCoveragesDetail(UserID, this.OPPAdditionalCoveragesDetailPropertiesCollection1_1, this.TaskControlID, this.IsOPPQuote);

            //Elimina PremiumNet para anadirlas con los nuevos cambios
            DeleteOPPPremiumNet(this.TaskControlID);
            //Save Properties Premium Net
            //if (!IsOPPQuote)
            //{
            this.PropertiesCollection = Properties.GetPropertiesByTaskControlID(this.TaskControlID, IsOPPQuote);

            if (this.PropertiesCollection.Rows.Count >= 1)
            {
                this.AdditionalCoveragesPropertiesCollection1 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[0]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 2)
            {
                this.AdditionalCoveragesPropertiesCollection2 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[1]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 3)
            {
                this.AdditionalCoveragesPropertiesCollection3 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[2]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 4)
            {
                this.AdditionalCoveragesPropertiesCollection4 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[3]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 5)
            {
                this.AdditionalCoveragesPropertiesCollection5 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[4]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 6)
            {
                this.AdditionalCoveragesPropertiesCollection6 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[5]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 7)
            {
                this.AdditionalCoveragesPropertiesCollection7 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[6]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 8)
            {
                this.AdditionalCoveragesPropertiesCollection8 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[7]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 9)
            {
                this.AdditionalCoveragesPropertiesCollection9 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[8]["PropertiesID"], IsOPPQuote);
            }

            if (this.PropertiesCollection.Rows.Count >= 10)
            {
                this.AdditionalCoveragesPropertiesCollection10 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)this.PropertiesCollection.Rows[9]["PropertiesID"], IsOPPQuote);
            }
            //}

            SetPropertiesPremiumNet();

            //Actualiza el PropertyID en Liability
            SetPropertyIDInLiability();

            //Salva PersonalLiability
            this.PersonalLiability.SavePersonalLiability(UserID, this.PersonalLiabilityCollection, this.TaskControlID, this.IsOPPQuote); //Save Properties

            //Antes de salvar el PersonalLiability elimina las cubiertas adiconales para anadirlas con los nuevos cambios.
            this.PersonalLiability.AdditionalCoverageLiability.DeleteAdditionalCoverageLiabilityByTaskControlID(this.TaskControlID, this.IsOPPQuote);

            //Salva Additional Coverages for PersonalLiability
            this.PersonalLiability.AdditionalCoverageLiability.AdditionalCoverageLiabilityTableTemp = this.AdditionalCoveragesLiabilityCollection;
            this.PersonalLiability.AdditionalCoverageLiability.SaveAdditionalCoverageLiability(UserID, this.PersonalLiability.PersonalLiabilityID, this.TaskControlID, IsOPPQuote);

            //Save PersonalLiability Premium Net
            //if (!IsOPPQuote)
            //{
            this.PersonalLiabilityCollection = PersonalLiability.GetPersonalLiabilityByTaskControlID(this.TaskControlID, IsOPPQuote);
            if (this.PersonalLiabilityCollection.Rows.Count >= 1)
            {
                this.AdditionalCoveragesLiabilityCollection = AdditionalCoverageLiability.GetAdditionalCoverageLiabilityTableTempLoaded((int)this.PersonalLiabilityCollection.Rows[0]["PersonalLiabilityID"], IsOPPQuote);
            }
            //}

            SetPersonalLiabilityPremiumNet();

            //Salva Umbrella Policy
            if (this.Umbrella.TaskControlID == 0)
            {
                if (this.Umbrella.EffDt1.Trim() == "")

                    if (this.QuoteAuto.AutoCovers.Count > 0)
                    {

                        this.Umbrella.EffDt1 = this.EffectiveDate;

                        this.Umbrella.ExpDt1 = this.ExpirationDate;

                        //this.Umbrella.LiabilityLimit1 = "";

                        this.Umbrella.PolicyNo1 = this.PolicyType.Trim() + ' ' + this.PolicyNo.Trim() + ' ' + Suffix.Trim();

                    }

                if (this.Umbrella.EffDt2.Trim() == "")

                    if (this.QuoteAuto.AutoCovers.Count > 0)
                    {

                        this.Umbrella.EffDt2 = this.EffectiveDate;

                        this.Umbrella.ExpDt2 = this.ExpirationDate;

                        //this.Umbrella.LiabilityLimit2 = "";

                        this.Umbrella.PolicyNo2 = this.PolicyType.Trim() + ' ' + this.PolicyNo.Trim() + ' ' + Suffix.Trim();

                    }
                this.Umbrella.TaskControlID = this.TaskControlID;
                this.Umbrella.SaveUmbrella(UserID, this.IsOPPQuote);
            }
            else
            {
                if (this.Umbrella.TaskControlID != 0) //Solamente guarda cuando existe el umbrella.
                {
                    //Solamente para que elimine cuando se modifique y no cuando haya un endoso
                    if (this.Umbrella.TaskControlID == TaskControlID)
                        Umbrella.DeleteUmbrellaByTaskControlID(TaskControlID, this.IsOPPQuote);

                    this.Umbrella.SaveUmbrella(UserID, this.IsOPPQuote);
                }
            }

            //Salva Auto Policy
            QuoteAuto.Save();

            this._mode = (int)TaskControlMode.UPDATE;
            this.Mode = (int)TaskControlMode.CLEAR;
        }

        private void SetPropertyIDInLiability()
        {
            if (PersonalLiabilityCollection.Rows.Count > 0)
            {
                PersonalLiabilityCollection.Rows[0]["Property1ID"] = this.Properties.PropertiesID;
                PersonalLiabilityCollection.Rows[0]["Property2ID"] = this.Properties.PropertiesID2;
                PersonalLiabilityCollection.Rows[0]["Property3ID"] = this.Properties.PropertiesID3;
                PersonalLiabilityCollection.Rows[0]["Property4ID"] = this.Properties.PropertiesID4;
                PersonalLiabilityCollection.Rows[0]["Property5ID"] = this.Properties.PropertiesID5;
                PersonalLiabilityCollection.Rows[0]["Property6ID"] = this.Properties.PropertiesID6;
                PersonalLiabilityCollection.Rows[0]["Property7ID"] = this.Properties.PropertiesID7;
                PersonalLiabilityCollection.Rows[0]["Property8ID"] = this.Properties.PropertiesID8;
                PersonalLiabilityCollection.Rows[0]["Property9ID"] = this.Properties.PropertiesID9;
                PersonalLiabilityCollection.Rows[0]["Property10ID"] = this.Properties.PropertiesID10;
                PersonalLiabilityCollection.AcceptChanges();
            }
        }
        // EDITADO 9/30/2014 a las 2:06 PM POR
        private void SetPersonalLiabilityPremiumNet()
        {
            if (this.PersonalLiabilityCollection.Rows.Count != 0)
            {
                string Neto0 = "0.00";
                //string Neto7 = "0.00";
                //string Neto8 = "0.00";
                //string Neto9 = "0.00";
                //string Neto10 = "0.00";
                //string Neto11 = "0.00";
                //string Neto12 = "0.00";
                string Neto13 = "0.00";
                string Neto14 = "0.00";
                double sumaPrem = 0.00;
                double tempTot = 0.00;

                double totprem = double.Parse(this.PersonalLiabilityCollection.Rows[0]["TotalPremium"].ToString());
                double subtotprem = double.Parse(this.PersonalLiabilityCollection.Rows[0]["SubTotalPremium"].ToString());
                //double percent = totprem / double.Parse(this.PersonalLiabilityCollection.Rows[0]["SubTotalPremium"].ToString());
                double percent = double.Parse(this.PersonalLiabilityCollection.Rows[0]["ExperienceCredit"].ToString()) / 100;
                // ADD el Discount de Underwritter 9/30/2014 
                double percentUnderWritter = double.Parse(this.PersonalLiabilityCollection.Rows[0]["DiscUnderWritter"].ToString()) / 100;

                // total Percent 
                double totalPercent = percent + percentUnderWritter;
                totalPercent = Math.Round(totalPercent, 2);
                totalPercent = Math.Round(totalPercent * 100, 2);
                totalPercent = Math.Round(100 - totalPercent, 0) / 100; 

                /*
                percent = Math.Round(percent, 2);
                percent = Math.Round(percent * 100, 2);
                percent = Math.Round(100 - percent, 0) / 100;

                percentUnderWritter = Math.Round(percentUnderWritter, 2);
                percentUnderWritter = Math.Round(percentUnderWritter * 100, 2);
                percentUnderWritter = Math.Round(100 - percentUnderWritter, 0) / 100; 
                */

                //percent = Math.Round(percent, 4);
                //OJO AQUI
                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium1"].ToString()) * totalPercent , 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto1 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium2"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto2 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium3"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto3 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium4"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto4 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium5"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto5 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium6"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto6 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium7"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto7 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium8"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto8 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium9"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto9 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(this.PersonalLiabilityCollection.Rows[0]["Premium10"].ToString()) * totalPercent, 0);
                sumaPrem = sumaPrem + tempTot;
                string Neto10 = tempTot.ToString("###,###.00");

                tempTot = 0.00;
                //tempTot = Math.Round(double.Parse(TxtPremium5.Text) * percent, 0);
                string Neto11 = sumaPrem.ToString("###,###.00");  //tempTot.ToString("###,###.00");

                tempTot = 0.00;
                tempTot = Math.Round(double.Parse(SetAdditionalCoverageTotalPremium(this.AdditionalCoveragesLiabilityCollection).ToString("###,###,###.00")) * totalPercent, 0);
                string Neto12 = tempTot.ToString("###,###.00");

                if (double.Parse(this.PersonalLiabilityCollection.Rows[0]["TotalPremium"].ToString()) != double.Parse(Neto11) + double.Parse(Neto12))
                {
                    double sumaSubTot = double.Parse(Neto11) + double.Parse(Neto12);
                    double sumaTot = double.Parse(this.PersonalLiabilityCollection.Rows[0]["TotalPremium"].ToString());
                    double dif = sumaTot - sumaSubTot;

                    if (double.Parse(this.PersonalLiabilityCollection.Rows[0]["TotalPremium"].ToString()) > double.Parse(Neto11) + double.Parse(Neto12))
                    {
                        sumaSubTot = double.Parse(Neto1) + Math.Abs(dif);
                           // OJO: Lo comentaste
                         Neto1 = sumaSubTot.ToString("###,###.00");

                        sumaSubTot = double.Parse(Neto11) + Math.Abs(dif);
                        //Neto5 = sumaSubTot.ToString("###,###.00");
                        Neto11 = sumaSubTot.ToString("###,###.00");
                    }
                    else
                    {
                        sumaSubTot = double.Parse(Neto1) - Math.Abs(dif);
                        Neto1 = sumaSubTot.ToString("###,###.00");

                        sumaSubTot = double.Parse(Neto11) - Math.Abs(dif);
                        //Neto5 = sumaSubTot.ToString("###,###.00");
                        Neto11 = sumaSubTot.ToString("###,###.00");
                    }
                }

                SaveOPPNetPremium(this.TaskControlID, int.Parse(this.PersonalLiabilityCollection.Rows[0]["PersonalLiabilityID"].ToString()), "L", Neto0, Neto1, Neto2, Neto3,
                Neto4, Neto5, Neto6, Neto7, Neto8, Neto9, Neto10, Neto11, Neto12, Neto13, Neto14);
            }


        }

        private void SetPropertiesPremiumNet()
        {
            for (int i = 0; this.PropertiesCollection.Rows.Count - 1 >= i; i++)
            {
                if (double.Parse(this.PropertiesCollection.Rows[i]["TotalPremium"].ToString()) > 0)
                {
                    string Neto0 = "0";
                    string Neto1 = "0";
                    string Neto2 = "0";
                    string Neto3 = "0";
                    string Neto4 = "0";
                    string Neto5 = "0";
                    string Neto6 = "0";
                    string Neto7 = "0";
                    string Neto8 = "0";
                    string Neto9 = "0";
                    string Neto10 = "0";
                    string Neto11 = "0";
                    string Neto12 = "0";
                    string Neto13 = "0";
                    string Neto14 = "0";

                    double sumaPrem = 0.00;
                    double tempTot = 0.00;
                    double tempTot2 = 0.00;
                    double totprem = double.Parse(this.PropertiesCollection.Rows[i]["TotalPremium"].ToString());
                    double subtotprem = double.Parse(this.PropertiesCollection.Rows[i]["SubTotalPremium"].ToString());
                    double BuildingPremiumTotal = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumTotal"].ToString()));
                    double ContentsPremiumTotal = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumTotal"].ToString()));
                    //double percent = totprem / double.Parse(this.PropertiesCollection.Rows[i]["SubTotalPremium"].ToString());

                    double totDiscOnlyAdCov = 0.0;
                    double totDisc = 0.0;
                    double percent = 0.0;
                    double percent2 = 0.0;

                    double discExperienceCredit = double.Parse(this.PropertiesCollection.Rows[i]["ExperienceCredit"].ToString()) / 100;
                    double discAccesspercent = double.Parse(this.PropertiesCollection.Rows[i]["DiscAccess"].ToString()) / 100;
                    double discShutterpercent = double.Parse(this.PropertiesCollection.Rows[i]["DiscShutter"].ToString()) / 100;
                    double discTheftpercent = double.Parse(this.PropertiesCollection.Rows[i]["DiscTheft"].ToString()) / 100;
                    double discFirepercent = double.Parse(this.PropertiesCollection.Rows[i]["DiscFire"].ToString()) / 100;
                    double discUnderwritterpercent = double.Parse(this.PropertiesCollection.Rows[i]["DiscUnderwritter"].ToString()) / 100;

                    totDisc = discExperienceCredit + discUnderwritterpercent;
                    totDiscOnlyAdCov = discExperienceCredit + discAccesspercent + discShutterpercent + discTheftpercent + discFirepercent + discUnderwritterpercent;

                    percent = Math.Round(totDiscOnlyAdCov, 2);
                    percent = Math.Round(percent * 100, 2);
                    percent = Math.Round(100 - percent, 0) / 100;

                    percent2 = Math.Round(totDisc, 2);
                    percent2 = Math.Round(percent2 * 100, 2);
                    percent2 = Math.Round(100 - percent2, 0) / 100;

                    if (double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumFire"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumFire"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto1 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumFire"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto1 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumExtCoverage"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumExtCoverage"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto2 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumExtCoverage"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto2 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumVandalism"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumVandalism"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto3 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumVandalism"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto3 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumEarthquake"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumEarthquake"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto4 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumEarthquake"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto4 = tempTot.ToString("###,###.00");
                    }

                    tempTot = 0.00;
                    tempTot = double.Parse(this.PropertiesCollection.Rows[i]["HomeAssistance"].ToString());
                    sumaPrem = sumaPrem + tempTot;
                    Neto0 = tempTot.ToString("###,###.00");

                    tempTot = 0.00;
                    //tempTot = Math.Round(double.Parse(TxtBuildingTotalPremium.Text) * percent, 0);
                    Neto5 = sumaPrem.ToString("###,###.00");  // tempTot.ToString("###,###.00");

                    sumaPrem = 0.00;

                    if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumFire"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumFire"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto6 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumFire"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto6 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumExtCoverage"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumExtCoverage"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto7 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumExtCoverage"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto7 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumVandalism"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumVandalism"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto8 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumVandalism"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto8 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumEarthquake"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumEarthquake"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto9 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumEarthquake"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto9 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumAOP"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumAOP"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto10 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumAOP"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto10 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumExcessAOP"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumExcessAOP"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto11 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumExcessAOP"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto11 = tempTot.ToString("###,###.00");
                    }

                    if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumTheft"].ToString()) > 4)
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumTheft"].ToString()) * percent, 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto12 = tempTot.ToString("###,###.00");
                    }
                    else
                    {
                        tempTot = 0.00;
                        tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumTheft"].ToString()), 0);
                        sumaPrem = sumaPrem + tempTot;
                        Neto12 = tempTot.ToString("###,###.00");
                    }

                    tempTot = 0.00;
                    tempTot = Math.Round(double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumTotal"].ToString()) * percent, 0);
                    Neto13 = sumaPrem.ToString("###,###.00");  //tempTot.ToString("###,###.00");

                    tempTot = 0.00;
                    tempTot2 = 0.00;
                    Neto14 = "";
                    if (i == 0)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection1).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }

                    if (i == 1)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection2).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }

                    if (i == 2)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection3).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }

                    if (i == 3)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection4).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }
                    if (i == 4)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection5).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }
                    if (i == 5)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection6).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }
                    if (i == 6)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection7).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }
                    if (i == 7)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection8).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }
                    if (i == 8)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection9).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }
                    if (i == 9)
                    {
                        tempTot = 0.00;
                        tempTot = double.Parse(GetAdditionalCoverageNeto(this.AdditionalCoveragesPropertiesCollection10).ToString("###,###,###.00"));
                        Neto14 = tempTot.ToString("###,###.00");
                    }

                    if (totprem != double.Parse(Neto5) + double.Parse(Neto13) + double.Parse(Neto14))
                    {
                        double sumaSubTot = double.Parse(Neto5) + double.Parse(Neto13) + double.Parse(Neto14);
                        double sumaTot = totprem;
                        double dif = sumaTot - sumaSubTot;

                        if (totprem > double.Parse(Neto5) + double.Parse(Neto13) + double.Parse(Neto14))
                        {
                            if (double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumTotal"].ToString()) > 0)
                            {
                                sumaSubTot = double.Parse(Neto4) + Math.Abs(dif);
                                Neto4 = sumaSubTot.ToString("###,###.00");

                                sumaSubTot = double.Parse(Neto5) + Math.Abs(dif);
                                Neto5 = sumaSubTot.ToString("###,###.00");

                                if (double.Parse(Neto4) < 0)
                                {
                                    Neto1 = (double.Parse(Neto1) - Math.Abs(double.Parse(Neto4))).ToString("###,###.00");
                                    Neto4 = "0.00";
                                }
                            }
                            else
                            {
                                if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumTotal"].ToString()) > 0)
                                {
                                    sumaSubTot = double.Parse(Neto12) + Math.Abs(dif);
                                    Neto12 = sumaSubTot.ToString("###,###.00");

                                    sumaSubTot = double.Parse(Neto13) + Math.Abs(dif);
                                    Neto13 = sumaSubTot.ToString("###,###.00");

                                    if (double.Parse(Neto12) < 0)
                                    {
                                        Neto7 = (double.Parse(Neto7) - Math.Abs(double.Parse(Neto12))).ToString("###,###.00");
                                        Neto12 = "0.00";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (double.Parse(this.PropertiesCollection.Rows[i]["BuildingPremiumTotal"].ToString()) > 0)
                            {
                                //Nos aseguramos que no sea 0 la cubierta
                                if (double.Parse(Neto4) - Math.Abs(dif) > 0)
                                {
                                    sumaSubTot = double.Parse(Neto4) - Math.Abs(dif);
                                    Neto4 = sumaSubTot.ToString("###,###.00");
                                }
                                else
                                {
                                    if (double.Parse(Neto3) - Math.Abs(dif) > 0)
                                    {
                                        sumaSubTot = double.Parse(Neto3) - Math.Abs(dif);
                                        Neto3 = sumaSubTot.ToString("###,###.00");
                                    }
                                    else
                                    {
                                        if (double.Parse(Neto12) - Math.Abs(dif) > 0)
                                        {
                                            sumaSubTot = double.Parse(Neto12) - Math.Abs(dif);
                                            Neto12 = sumaSubTot.ToString("###,###.00");
                                        }
                                        else
                                        {
                                            if (double.Parse(Neto1) - Math.Abs(dif) > 0)
                                            {
                                                sumaSubTot = double.Parse(Neto1) - Math.Abs(dif);
                                                Neto1 = sumaSubTot.ToString("###,###.00");
                                            }
                                        }
                                    }
                                }

                                sumaSubTot = double.Parse(Neto5) - Math.Abs(dif);
                                Neto5 = sumaSubTot.ToString("###,###.00");
                            }
                            else
                            {
                                if (double.Parse(this.PropertiesCollection.Rows[i]["ContentsPremiumTotal"].ToString()) > 0)
                                {
                                    if (double.Parse(Neto12) - Math.Abs(dif) > 0)
                                    {
                                        sumaSubTot = double.Parse(Neto12) - Math.Abs(dif);
                                        Neto12 = sumaSubTot.ToString("###,###.00");
                                    }
                                    else
                                    {
                                        if (double.Parse(Neto11) - Math.Abs(dif) > 0)
                                        {
                                            sumaSubTot = double.Parse(Neto11) - Math.Abs(dif);
                                            Neto11 = sumaSubTot.ToString("###,###.00");
                                        }
                                        else
                                        {
                                            if (double.Parse(Neto10) - Math.Abs(dif) > 0)
                                            {
                                                sumaSubTot = double.Parse(Neto10) - Math.Abs(dif);
                                                Neto10 = sumaSubTot.ToString("###,###.00");
                                            }
                                            else
                                            {
                                                if (double.Parse(Neto8) - Math.Abs(dif) > 0)
                                                {
                                                    sumaSubTot = double.Parse(Neto8) - Math.Abs(dif);
                                                    Neto8 = sumaSubTot.ToString("###,###.00");
                                                }
                                                else
                                                {
                                                    if (double.Parse(Neto9) - Math.Abs(dif) > 0)
                                                    {
                                                        sumaSubTot = double.Parse(Neto9) - Math.Abs(dif);
                                                        Neto9 = sumaSubTot.ToString("###,###.00");
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    sumaSubTot = double.Parse(Neto13) - Math.Abs(dif);
                                    Neto13 = sumaSubTot.ToString("###,###.00");
                                }
                            }
                        }
                    }

                    SaveOPPNetPremium(this.TaskControlID, int.Parse(this.PropertiesCollection.Rows[i]["PropertiesID"].ToString().Trim()), "P", Neto0, Neto1, Neto2, Neto3,
                    Neto4, Neto5, Neto6, Neto7, Neto8, Neto9, Neto10, Neto11, Neto12, Neto13, Neto14);

                    //string[] arr = SetNegativeAmount(Neto0, Neto1, Neto2, Neto3,
                    //Neto4, Neto5, Neto6, Neto7, Neto8, Neto9, Neto10,
                    //Neto11, Neto12, Neto13, Neto14, this.PropertiesCollection.Rows[i]["TotalPremium"].ToString(),
                    //this.PropertiesCollection.Rows[i]["ContentsPremiumTotal"].ToString(),
                    //this.PropertiesCollection.Rows[i]["BuildingPremiumTotal"].ToString(),
                    //this.PropertiesCollection.Rows[i]["SubTotalPremium"].ToString(), tempTot2);

                    //Neto1 = arr[1];
                    //Neto2 = arr[2];
                    //Neto3 = arr[3];
                    //Neto4 = arr[4];
                    //Neto5 = arr[5];
                    //Neto6 = arr[6];
                    //Neto7 = arr[7];
                    //Neto8 = arr[8];
                    //Neto9 = arr[9];
                    //Neto10 = arr[10];
                    //Neto11 = arr[11];
                    //Neto12 = arr[12];
                    //Neto13 = arr[13];
                    //Neto14 = arr[14];

                }
            }

        }

        private string[] SetNegativeAmount(string Neto0, string Neto1, string Neto2, string Neto3,
                   string Neto4, string Neto5, string Neto6, string Neto7, string Neto8, string Neto9, string Neto10,
                   string Neto11, string Neto12, string Neto13, string Neto14, string totalPremium, string contentsPremiumTotal,
                   string buildingPremiumTotal, string subTotalPremium, double DiscountAmt)
        {
            string[] arr = new string[15];
            int mcount = 0;
            double sum = 0.0;

            arr[0] = Neto0;
            arr[1] = Neto1;
            arr[2] = Neto2;
            arr[3] = Neto3;
            arr[4] = Neto4;
            arr[5] = Neto5;
            arr[6] = Neto6;
            arr[7] = Neto7;
            arr[8] = Neto8;
            arr[9] = Neto9;
            arr[10] = Neto10;
            arr[11] = Neto11;
            arr[12] = Neto12;
            arr[13] = Neto13;
            arr[14] = Neto14;

            if (double.Parse(Neto1) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto1);
            }

            if (double.Parse(Neto2) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto2);
            }

            if (double.Parse(Neto3) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto3);
            }

            if (double.Parse(Neto4) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto4);
            }

            if (double.Parse(Neto6) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto6);
            }

            if (double.Parse(Neto7) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto7);
            }

            if (double.Parse(Neto8) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto8);
            }

            if (double.Parse(Neto9) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto9);
            }

            if (double.Parse(Neto10) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto10);
            }

            if (double.Parse(Neto11) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto11);
            }

            if (double.Parse(Neto12) > 4)
            {
                mcount = mcount + 1;
                sum = sum + double.Parse(Neto12);
            }

            double totprem = double.Parse(totalPremium);
            double subtotprem = double.Parse(subTotalPremium);
            double percent = Math.Abs(DiscountAmt) / sum; // ((double.Parse(contentsPremiumTotal) + double.Parse(buildingPremiumTotal)));
            percent = Math.Round(percent, 2);
            percent = Math.Round(percent * 100, 2);
            percent = Math.Round(100 - percent, 0) / 100;

            if (double.Parse(Neto1) > 4)
                arr[1] = Math.Round(double.Parse(Neto1) * percent, 0).ToString();

            if (double.Parse(Neto2) > 4)
                arr[2] = Math.Round(double.Parse(Neto2) * percent, 0).ToString();

            if (double.Parse(Neto3) > 4)
                arr[3] = Math.Round(double.Parse(Neto3) * percent, 0).ToString();

            if (double.Parse(Neto4) > 4)
                arr[4] = Math.Round(double.Parse(Neto4) * percent, 0).ToString();

            if (double.Parse(Neto6) > 4)
                arr[6] = Math.Round(double.Parse(Neto6) * percent, 0).ToString();

            if (double.Parse(Neto7) > 4)
                arr[7] = Math.Round(double.Parse(Neto7) * percent, 0).ToString();

            if (double.Parse(Neto8) > 4)
                arr[8] = Math.Round(double.Parse(Neto8) * percent, 0).ToString();

            if (double.Parse(Neto9) > 4)
                arr[9] = Math.Round(double.Parse(Neto9) * percent, 0).ToString();

            if (double.Parse(Neto10) > 4)
                arr[10] = Math.Round(double.Parse(Neto10) * percent, 0).ToString();

            if (double.Parse(Neto11) > 4)
                arr[11] = Math.Round(double.Parse(Neto11) * percent, 0).ToString();

            if (double.Parse(Neto12) > 4)
                arr[12] = Math.Round(double.Parse(Neto12) * percent, 0).ToString();


            //Anadir el discount en las cubiertas adicionales.

            //arr[14] = "0.00";
            arr[5] = (double.Parse(arr[0]) + double.Parse(arr[1]) + double.Parse(arr[2]) + double.Parse(arr[3]) + double.Parse(arr[4])).ToString();
            arr[13] = (double.Parse(arr[6]) + double.Parse(arr[7]) + double.Parse(arr[8]) + double.Parse(arr[9]) +
                       double.Parse(arr[10]) + double.Parse(arr[11]) + double.Parse(arr[12])).ToString();

            if (double.Parse(totalPremium) != double.Parse(arr[5]) + double.Parse(arr[13]) + double.Parse(arr[14]))
            {
                double sumaSubTot = double.Parse(arr[5]) + double.Parse(arr[13]) + double.Parse(arr[14]);
                double sumaTot = double.Parse(totalPremium);
                double dif = sumaTot - sumaSubTot;

                if (double.Parse(totalPremium) > double.Parse(arr[5]) + double.Parse(arr[13]) + double.Parse(arr[14]))
                {
                    if (double.Parse(buildingPremiumTotal) > 0)
                    {
                        sumaSubTot = double.Parse(arr[4]) + Math.Abs(dif);
                        arr[4] = sumaSubTot.ToString("###,###.00");

                        sumaSubTot = double.Parse(arr[5]) + Math.Abs(dif);
                        arr[5] = sumaSubTot.ToString("###,###.00");

                        if (double.Parse(arr[4]) < 0)
                        {
                            arr[1] = (double.Parse(arr[1]) - Math.Abs(double.Parse(arr[4]))).ToString("###,###.00");
                            arr[4] = "0.00";
                        }
                    }
                    else
                    {
                        if (double.Parse(contentsPremiumTotal) > 0)
                        {
                            sumaSubTot = double.Parse(arr[12]) + Math.Abs(dif);
                            arr[12] = sumaSubTot.ToString("###,###.00");

                            sumaSubTot = double.Parse(arr[13]) + Math.Abs(dif);
                            arr[13] = sumaSubTot.ToString("###,###.00");

                            if (double.Parse(arr[12]) < 0)
                            {
                                arr[7] = (double.Parse(arr[7]) - Math.Abs(double.Parse(arr[12]))).ToString("###,###.00");
                                arr[12] = "0.00";
                            }
                        }
                    }
                }
                else
                {
                    if (double.Parse(buildingPremiumTotal) > 0)
                    {
                        //Nos aseguramos que no sea 0 la cubierta
                        if (double.Parse(arr[4]) - Math.Abs(dif) > 0)
                        {
                            sumaSubTot = double.Parse(arr[4]) - Math.Abs(dif);
                            arr[4] = sumaSubTot.ToString("###,###.00");
                        }
                        else
                        {
                            if (double.Parse(arr[3]) - Math.Abs(dif) > 0)
                            {
                                sumaSubTot = double.Parse(arr[3]) - Math.Abs(dif);
                                arr[3] = sumaSubTot.ToString("###,###.00");
                            }
                            else
                            {
                                if (double.Parse(arr[2]) - Math.Abs(dif) > 0)
                                {
                                    sumaSubTot = double.Parse(arr[2]) - Math.Abs(dif);
                                    arr[2] = sumaSubTot.ToString("###,###.00");
                                }
                                else
                                {
                                    if (double.Parse(arr[1]) - Math.Abs(dif) > 0)
                                    {
                                        sumaSubTot = double.Parse(arr[1]) - Math.Abs(dif);
                                        arr[1] = sumaSubTot.ToString("###,###.00");
                                    }
                                }
                            }
                        }

                        sumaSubTot = double.Parse(arr[5]) - Math.Abs(dif);
                        arr[5] = sumaSubTot.ToString("###,###.00");
                    }
                    else
                    {
                        if (double.Parse(contentsPremiumTotal) > 0)
                        {
                            if (double.Parse(arr[12]) - Math.Abs(dif) > 0)
                            {
                                sumaSubTot = double.Parse(arr[12]) - Math.Abs(dif);
                                arr[12] = sumaSubTot.ToString("###,###.00");
                            }
                            else
                            {
                                if (double.Parse(arr[11]) - Math.Abs(dif) > 0)
                                {
                                    sumaSubTot = double.Parse(arr[11]) - Math.Abs(dif);
                                    arr[11] = sumaSubTot.ToString("###,###.00");
                                }
                                else
                                {
                                    if (double.Parse(arr[10]) - Math.Abs(dif) > 0)
                                    {
                                        sumaSubTot = double.Parse(arr[10]) - Math.Abs(dif);
                                        arr[10] = sumaSubTot.ToString("###,###.00");
                                    }
                                    else
                                    {
                                        if (double.Parse(arr[8]) - Math.Abs(dif) > 0)
                                        {
                                            sumaSubTot = double.Parse(arr[8]) - Math.Abs(dif);
                                            arr[8] = sumaSubTot.ToString("###,###.00");
                                        }
                                        else
                                        {
                                            if (double.Parse(arr[9]) - Math.Abs(dif) > 0)
                                            {
                                                sumaSubTot = double.Parse(arr[9]) - Math.Abs(dif);
                                                arr[9] = sumaSubTot.ToString("###,###.00");
                                            }
                                        }
                                    }
                                }
                            }

                            sumaSubTot = double.Parse(arr[13]) - Math.Abs(dif);
                            arr[13] = sumaSubTot.ToString("###,###.00");

                            //if (double.Parse(arr[12]) < 0)
                            //{
                            //    arr[7] = (double.Parse(arr[7]) - Math.Abs(double.Parse(arr[12]))).ToString("###,###.00");
                            //    arr[12] = "0.00";
                            //}
                        }
                    }
                }
            }

            return arr;
        }

        private string[] SetNegativeAmountOld(string Neto0, string Neto1, string Neto2, string Neto3,
                    string Neto4, string Neto5, string Neto6, string Neto7, string Neto8, string Neto9, string Neto10,
                    string Neto11, string Neto12, string Neto13, string Neto14, string totalPremium, string contentsPremiumTotal,
                    string buildingPremiumTotal)
        {
            //Calculo una media para porciento por cubierta para descontarse
            bool Rebate = false;
            double sum = 0.0;
            double per = 0.0;
            double amt = 0.0;
            string[] arr = new string[15];
            arr[0] = Neto0;
            arr[1] = Neto1;
            arr[2] = Neto2;
            arr[3] = Neto3;
            arr[4] = Neto4;
            arr[5] = Neto5;
            arr[6] = Neto6;
            arr[7] = Neto7;
            arr[8] = Neto8;
            arr[9] = Neto9;
            arr[10] = Neto10;
            arr[11] = Neto11;
            arr[12] = Neto12;
            arr[13] = Neto13;
            arr[14] = Neto14;

            if (double.Parse(Neto14) < 0.0)
                sum = double.Parse(Neto14);
            else
                if (double.Parse(Neto12) < 0.0)
                    sum = double.Parse(Neto12);
                else
                    if (double.Parse(Neto4) < 0.0)
                        sum = double.Parse(Neto4);

            //Aplicar % de descuento
            if (double.Parse(Neto5) > 0.0)
            {
                if (double.Parse(Neto1) > 0.0)
                    if (Math.Round(double.Parse(Neto1) / Math.Abs(sum), 2) < .95)
                        arr[1] = (Math.Round((Math.Round(double.Parse(Neto1) / Math.Abs(sum), 2) * double.Parse(Neto1)), 0)).ToString();
                    else
                        if (double.Parse(Neto1) > Math.Abs(sum))
                        {
                            arr[1] = (double.Parse(Neto1) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto2) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto2) / Math.Abs(sum), 2) < .95)
                        arr[2] = (Math.Round((Math.Round(double.Parse(Neto2) / Math.Abs(sum), 2) * double.Parse(Neto2)), 0)).ToString();
                    else
                        if (double.Parse(Neto2) > Math.Abs(sum))
                        {
                            arr[2] = (double.Parse(Neto2) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto3) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto3) / Math.Abs(sum), 2) < .95)
                        arr[3] = (Math.Round((Math.Round(double.Parse(Neto3) / Math.Abs(sum), 2) * double.Parse(Neto3)), 0)).ToString();
                    else
                        if (double.Parse(Neto3) > Math.Abs(sum))
                        {
                            arr[3] = (double.Parse(Neto3) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto4) < 0.0)
                {
                    arr[4] = "0.00";
                }
                else
                {
                    if (double.Parse(Neto4) > 0.0 && Rebate == false)
                        if (Math.Round(double.Parse(Neto4) / Math.Abs(sum), 2) < .95)
                            arr[4] = (Math.Round((Math.Round(double.Parse(Neto4) / Math.Abs(sum), 2) * double.Parse(Neto4)), 0)).ToString();
                        else
                            if (double.Parse(Neto4) > Math.Abs(sum))
                            {
                                arr[4] = (double.Parse(Neto4) - Math.Abs(sum)).ToString();
                                Rebate = true;
                            }
                }
            }

            if (double.Parse(Neto13) > 0.0)
            {
                if (double.Parse(Neto6) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto6) / Math.Abs(sum), 2) < .95)
                        arr[6] = (Math.Round((Math.Round(double.Parse(Neto6) / Math.Abs(sum), 2) * double.Parse(Neto6)), 0)).ToString();
                    else
                        if (double.Parse(Neto6) > Math.Abs(sum))
                        {
                            arr[6] = (double.Parse(Neto6) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto7) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto7) / Math.Abs(sum), 2) < .95)
                        arr[7] = (Math.Round((Math.Round(double.Parse(Neto7) / Math.Abs(sum), 2) * double.Parse(Neto7)), 0)).ToString();
                    else
                        if (double.Parse(Neto7) > Math.Abs(sum))
                        {
                            arr[7] = (double.Parse(Neto7) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto8) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto8) / Math.Abs(sum), 2) < .95)
                        arr[8] = (Math.Round((Math.Round(double.Parse(Neto8) / Math.Abs(sum), 2) * double.Parse(Neto8)), 0)).ToString();
                    else
                        if (double.Parse(Neto8) > Math.Abs(sum))
                        {
                            arr[8] = (double.Parse(Neto8) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto9) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto9) / Math.Abs(sum), 2) < .95)
                        arr[9] = (Math.Round((Math.Round(double.Parse(Neto9) / Math.Abs(sum), 2) * double.Parse(Neto9)), 0)).ToString();
                    else
                        if (double.Parse(Neto9) > Math.Abs(sum))
                        {
                            arr[9] = (double.Parse(Neto9) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto10) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto10) / Math.Abs(sum), 2) < .95)
                        arr[10] = (Math.Round((Math.Round(double.Parse(Neto10) / Math.Abs(sum), 2) * double.Parse(Neto10)), 0)).ToString();
                    else
                        if (double.Parse(Neto10) > Math.Abs(sum))
                        {
                            arr[10] = (double.Parse(Neto10) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto11) > 0.0 && Rebate == false)
                    if (Math.Round(double.Parse(Neto11) / Math.Abs(sum), 2) < .95)
                        arr[11] = (Math.Round((Math.Round(double.Parse(Neto11) / Math.Abs(sum), 2) * double.Parse(Neto11)), 0)).ToString();
                    else
                        if (double.Parse(Neto11) > Math.Abs(sum))
                        {
                            arr[11] = (double.Parse(Neto11) - Math.Abs(sum)).ToString();
                            Rebate = true;
                        }

                if (double.Parse(Neto12) < 0.0)
                {
                    arr[12] = "0.00";
                }
                else
                {
                    if (double.Parse(Neto12) > 0.0 && Rebate == false)
                        if (Math.Round(double.Parse(Neto12) / Math.Abs(sum), 2) < .95)
                            arr[12] = (Math.Round((Math.Round(double.Parse(Neto12) / Math.Abs(sum), 2) * double.Parse(Neto12)), 0)).ToString();
                        else
                            if (double.Parse(Neto12) > Math.Abs(sum))
                            {
                                arr[12] = (double.Parse(Neto12) - Math.Abs(sum)).ToString();
                                Rebate = true;
                            }
                }
            }

            arr[14] = "0.00";
            arr[5] = (double.Parse(arr[0]) + double.Parse(arr[1]) + double.Parse(arr[2]) + double.Parse(arr[3]) + double.Parse(arr[4])).ToString();
            arr[13] = (double.Parse(arr[6]) + double.Parse(arr[7]) + double.Parse(arr[8]) + double.Parse(arr[9]) +
                       double.Parse(arr[10]) + double.Parse(arr[11]) + double.Parse(arr[12])).ToString();

            if (double.Parse(totalPremium) != double.Parse(arr[5]) + double.Parse(arr[13]) + double.Parse(arr[14]))
            {
                double sumaSubTot = double.Parse(arr[5]) + double.Parse(arr[13]);
                double sumaTot = double.Parse(totalPremium);
                double dif = sumaTot - sumaSubTot;

                if (double.Parse(totalPremium) > double.Parse(arr[5]) + double.Parse(arr[13]))
                {
                    if (double.Parse(contentsPremiumTotal) > 0)
                    {
                        sumaSubTot = double.Parse(arr[12]) + Math.Abs(dif);
                        arr[12] = sumaSubTot.ToString("###,###.00");

                        sumaSubTot = double.Parse(arr[13]) + Math.Abs(dif);
                        arr[13] = sumaSubTot.ToString("###,###.00");

                        if (double.Parse(arr[12]) < 0)
                        {
                            arr[7] = (double.Parse(arr[7]) - Math.Abs(double.Parse(arr[12]))).ToString("###,###.00");
                            arr[12] = "0.00";
                        }
                    }
                    else
                    {
                        if (double.Parse(buildingPremiumTotal) > 0)
                        {
                            sumaSubTot = double.Parse(arr[4]) + Math.Abs(dif);
                            arr[4] = sumaSubTot.ToString("###,###.00");

                            sumaSubTot = double.Parse(arr[5]) + Math.Abs(dif);
                            arr[5] = sumaSubTot.ToString("###,###.00");

                            if (double.Parse(arr[4]) < 0)
                            {
                                arr[1] = (double.Parse(arr[7]) - Math.Abs(double.Parse(arr[4]))).ToString("###,###.00");
                                arr[4] = "0.00";
                            }
                        }
                    }
                }
                else
                {
                    if (double.Parse(contentsPremiumTotal) > 0)
                    {
                        sumaSubTot = double.Parse(arr[12]) - Math.Abs(dif);
                        arr[12] = sumaSubTot.ToString("###,###.00");

                        sumaSubTot = double.Parse(arr[13]) - Math.Abs(dif);
                        arr[13] = sumaSubTot.ToString("###,###.00");

                        if (double.Parse(arr[12]) < 0)
                        {
                            arr[7] = (double.Parse(arr[7]) - Math.Abs(double.Parse(arr[12]))).ToString("###,###.00");
                            arr[12] = "0.00";
                        }
                    }
                    else
                    {
                        if (double.Parse(buildingPremiumTotal) > 0)
                        {
                            sumaSubTot = double.Parse(arr[4]) - Math.Abs(dif);
                            arr[4] = sumaSubTot.ToString("###,###.00");

                            sumaSubTot = double.Parse(arr[5]) - Math.Abs(dif);
                            arr[5] = sumaSubTot.ToString("###,###.00");

                            if (double.Parse(arr[4]) < 0)
                            {
                                arr[1] = (double.Parse(arr[7]) - Math.Abs(double.Parse(arr[4]))).ToString("###,###.00");
                                arr[4] = "0.00";
                            }
                        }
                    }
                }
            }

            return arr;
        }

        private double GetAdditionalCoverageNeto(DataTable dt)
        {
            double neto = 0.00;
            for (int a = 0; dt.Rows.Count - 1 >= a; a++)
            {
                neto = neto + double.Parse(dt.Rows[a]["Neto"].ToString());
            }
            return neto;
        }

        private double SetAdditionalCoverageTotalPremium(DataTable dt)
        {
            double prem = 0.00;
            for (int a = 0; dt.Rows.Count - 1 >= a; a++)
            {
                prem = prem + double.Parse(dt.Rows[a]["Premium"].ToString());
            }
            return prem;
        }

        private double SetAdditionalCoveragePropertiesTotalPremium(DataTable dt)
        {
            double prem = 0.00;
            for (int a = 0; dt.Rows.Count - 1 >= a; a++)
            {
                if (double.Parse(dt.Rows[a]["Premium"].ToString()) > 0.0)
                    prem = prem + double.Parse(dt.Rows[a]["Premium"].ToString());
            }
            return prem;
        }

        private double GetAdditionalCoverageTotalPremiumDiscount(DataTable dt)
        {
            double prem = 0.00;
            for (int a = 0; dt.Rows.Count - 1 >= a; a++)
            {
                if (double.Parse(dt.Rows[a]["Premium"].ToString()) < 0.0)
                    prem = prem + Math.Abs(double.Parse(dt.Rows[a]["Premium"].ToString()));
            }
            return prem;
        }

        private void SaveOPPNetPremium(int taskControlID, int ID, string Type, string Neto0, string Neto1, string Neto2, string Neto3,
            string Neto4, string Neto5, string Neto6, string Neto7, string Neto8, string Neto9, string Neto10, string Neto11,
            string Neto12, string Neto13, string Neto14)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                Executor.BeginTrans();
                int oppNeto = Executor.Insert("UpdateOPPNetPremium", OPPNetPremiumByIDXml(taskControlID, ID, Type, Neto0, Neto1, Neto2, Neto3,
                Neto4, Neto5, Neto6, Neto7, Neto8, Neto9, Neto10, Neto11, Neto12, Neto13, Neto14));
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error, Please try again. " + xcp.Message, xcp);
            }
        }

        public void DeleteOPPPremiumNet(int taskControlID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                Executor.Update("DeleteOPPPremiumNet", this.GetDeleteOPPPremiumNetXml(taskControlID));
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error while trying to delete OPPPremiumNet. " + xcp.Message, xcp);
            }
        }

        private XmlDocument GetDeleteOPPPremiumNetXml(int taskControlID)
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

        public void ValidateQuote()
        {
            string errorMessage = String.Empty;

            if (this.EffectiveDate == "")
                errorMessage = "Effective Date is missing or wrong.";
            else
                if (this.Term == 0)
                    errorMessage = "Term is missing or wrong.";
                else
                    if (this.Prospect.FirstName == "")
                        errorMessage = "First Name is missing or wrong.";
                    else
                        if (this.Prospect.LastName1 == "")
                            errorMessage = "Last Name is missing or wrong.";
                        else
                            if (this.OriginatedAt == 0)
                                errorMessage = "Originated is missing.";
                            else
                                if (this.TotalPremium == 0)
                                    errorMessage = "TotalPremium must be greater than 0.";

            //throw the exception.
            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
        }

        public override void Validate()
        {
            string errorMessage = String.Empty;

            if (this.EffectiveDate == "")
                errorMessage = "Effective Date is missing or wrong.";
            else
                if (this.PolicyClassID == 0)
                    errorMessage = "Line of Business is missing or wrong.";
                else
                    if (this.Customer.FirstName == "")
                        errorMessage = "First Name is missing or wrong.";
                    else
                        if (this.Customer.LastName1 == "")
                            errorMessage = "Last Name is missing or wrong.";
                        else
                            if (this.OriginatedAt == 0)
                                errorMessage = "Originated is missing.";
                            else
                                if (this.TotalPremium == 0)
                                    errorMessage = "TotalPremium must be greater than 0.";


            //throw the exception.
            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }
        }

        public static PersonalPackage GetPersonalPackage(int TaskControlID, bool IsOppQuote)
        {
            PersonalPackage optimaPersonalPackage = null;

            DataTable dt = GetOptimaPersonalPackageByTaskControlID(TaskControlID, IsOppQuote);

            optimaPersonalPackage = new PersonalPackage(IsOppQuote);

            if (IsOppQuote)
                optimaPersonalPackage = (PersonalPackage)Policy.GetPolicyQuoteByTaskControlID(TaskControlID, optimaPersonalPackage);  //PolicyQuote
            else
                optimaPersonalPackage = (PersonalPackage)Policy.GetPolicyByTaskControlID(TaskControlID, optimaPersonalPackage);  //Policy

            optimaPersonalPackage._dtOptimaPersonalPackage = dt;

            optimaPersonalPackage = FillProperties(optimaPersonalPackage, TaskControlID, IsOppQuote);

            return optimaPersonalPackage;
        }

        public static void DeleteOptimaPersonalPackageByTaskControlID(int taskControlID, bool IsOPPQuote)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                Executor.BeginTrans();
                Executor.Update("DeleteOptimaPersonalPackage", DeleteOptimaPersonalPackageByTaskControlIDXml(taskControlID, IsOPPQuote));
                Executor.CommitTrans();
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error, Please try again. " + xcp.Message, xcp);
            }
        }

        public DataTable GetEndorsementCollection()
        {
            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("OPPTaskControlID",
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

                DataTable dt = exec.GetQuery("GetOPPEndorsementByOPPTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error, Please try again. " + xcp.Message, xcp);
            }
        }

        public static DataTable GetEndorsementByEndoNum(int TaskControlID)
        {
            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("OPPTaskControlID",
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

                DataTable dt = exec.GetQuery("GetOPPEndorsementByEndoNum", xmlDoc);
                return dt;
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error, Please try again. " + xcp.Message, xcp);
            }
        }

        public static DataTable GetOPPByTaskControlID(int taskControlID)
        {
            DBRequest Executor = new DBRequest();

            try
            {

                DataTable dt = GetOPPByTaskControlIDDB(taskControlID);
                return dt;
            }
            catch (Exception xcp)
            {
                Executor.RollBackTrans();
                throw new Exception("Error, Please try again. " + xcp.Message, xcp);
            }
        }

        #endregion

        #region Private Methods

        public void CalculatePremium()
        {
            //			Quotes.MBIQuote MbiQuotes = new Quotes.MBIQuote();
            //			MbiQuotes.Calculate(this.PolicyType.Trim(),this.Model.Trim(),this.Make.Trim(),this.Term);
            //
            //			if(this.InsuranceCompany == "097")
            //			{
            //				this.TotalPremium = 175.00;
            //			}
            //			else
            //			{
            //				this.TotalPremium = MbiQuotes.premium;
            //			}
            //
            //			this.VehicleClass = MbiQuotes.classification;
            //			this.Mileage      = MbiQuotes.plan;		
        }

        private static DataTable GetOPPByTaskControlIDDB(int TaskControlID)
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

            DataTable dt = exec.GetQuery("GetOPPByTaskControlID", xmlDoc);
            return dt;
        }

        private static DataTable GetOptimaPersonalPackageByTaskControlID(int TaskControlID, bool IsOppQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
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

            DataTable dt = exec.GetQuery("GetOptimaPersonalPackageByTaskControlID", xmlDoc);
            return dt;
        }

        private void SaveOptimaPersonalPackageDB(int UserID)
        {
            Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            try
            {
                Executor.BeginTrans();
                switch (this.Mode)
                {
                    case 1:  //ADD
                        this.OptimaPersonalPackageID = Executor.Insert("AddOptimaPersonalPackage", this.GetInsertOptimaPersonalPackageXml());
                        this.History(this._mode, UserID);
                        break;

                    case 3:  //DELETE
                        //Executor.Update("DeleteAutoGuardServicesContract",this.GetDeletePoliciesXml());
                        break;

                    case 4:  //CLEAR						
                        break;

                    default: //UPDATE
                        this.History(this._mode, UserID);
                        Executor.Update("UpdateOptimaPersonalPackage", this.GetUpdateOptimaPersonalPackageXml());
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

        private static XmlDocument OPPNetPremiumByIDXml(int taskControlID, int ID, string Type, string Neto0, string Neto1, string Neto2, string Neto3,
            string Neto4, string Neto5, string Neto6, string Neto7, string Neto8, string Neto9, string Neto10, string Neto11,
            string Neto12, string Neto13, string Neto14)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[18];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("ID",
                SqlDbType.Int, 0, ID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Type",
                SqlDbType.Char, 1, Type.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto0",
            SqlDbType.Float, 1, Neto0.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto1",
                SqlDbType.Float, 1, Neto1.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto2",
                SqlDbType.Float, 1, Neto2.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto3",
                SqlDbType.Float, 1, Neto3.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto4",
                SqlDbType.Float, 1, Neto4.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto5",
                SqlDbType.Float, 1, Neto5.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto6",
                SqlDbType.Float, 1, Neto6.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto7",
                SqlDbType.Float, 1, Neto7.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto8",
                SqlDbType.Float, 1, Neto8.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto9",
                SqlDbType.Float, 1, Neto9.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto10",
                SqlDbType.Float, 1, Neto10.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto11",
                SqlDbType.Float, 1, Neto11.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto12",
                SqlDbType.Float, 1, Neto12.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto13",
                SqlDbType.Float, 1, Neto13.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("Neto14",
                SqlDbType.Float, 1, Neto14.ToString(),
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

        private static XmlDocument DeleteOptimaPersonalPackageByTaskControlIDXml(int taskControlID, bool IsOPPQuote)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, IsOPPQuote.ToString(),
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

        private XmlDocument GetUpdateOptimaPersonalPackageXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[13];

            DbRequestXmlCooker.AttachCookItem("OptimaPersonalPackageID",
                SqlDbType.Int, 0, this.OptimaPersonalPackageID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, this.TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertiesPremium",
                 SqlDbType.Float, 0, this.PropertiesPremium.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityPremium",
                SqlDbType.Float, 0, this.LiabilityPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoPremium",
                SqlDbType.Float, 0, this.AutoPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UmbrellaPremium",
                SqlDbType.Float, 0, this.UmbrellaPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, this.IsOPPQuote.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlIDQuoteAuto",
                SqlDbType.Int, 0, this.TaskControlIDQuoteAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropEnd",
                SqlDbType.VarChar, 300, this.PropEnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabEnd",
                SqlDbType.VarChar, 300, this.LiabEnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoEnd",
                SqlDbType.VarChar, 300, this.AutoEnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UmbEnd",
                SqlDbType.VarChar, 300, this.UmbEnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AdditionalName",
                SqlDbType.VarChar, 100, this.AdditionalName.ToString(),
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


        private XmlDocument GetInsertOptimaPersonalPackageXml()
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[12];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
               SqlDbType.Int, 0, this.TaskControlID.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropertiesPremium",
                 SqlDbType.Float, 0, this.PropertiesPremium.ToString(),
                 ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabilityPremium",
                SqlDbType.Float, 0, this.LiabilityPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoPremium",
                SqlDbType.Float, 0, this.AutoPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UmbrellaPremium",
                SqlDbType.Float, 0, this.UmbrellaPremium.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("IsOppQuote",
                SqlDbType.Bit, 0, this.IsOPPQuote.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("TaskControlIDQuoteAuto",
                SqlDbType.Int, 0, this.TaskControlIDQuoteAuto.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PropEnd",
               SqlDbType.VarChar, 300, this.PropEnd.ToString(),
               ref cookItems);

            DbRequestXmlCooker.AttachCookItem("LiabEnd",
                SqlDbType.VarChar, 300, this.LiabEnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AutoEnd",
                SqlDbType.VarChar, 300, this.AutoEnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("UmbEnd",
                SqlDbType.VarChar, 300, this.UmbEnd.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("AdditionalName",
                SqlDbType.VarChar, 100, this.AdditionalName.ToString(),
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

        private static PersonalPackage FillProperties(PersonalPackage optimaPersonalPackage, int taskControlID, bool IsOppQuote)
        {
            optimaPersonalPackage.OptimaPersonalPackageID = (int)optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["OptimaPersonalPackageID"];
            optimaPersonalPackage.PropertiesPremium = (double)optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["PropertiesPremium"];
            optimaPersonalPackage.LiabilityPremium = (double)optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["LiabilityPremium"];
            optimaPersonalPackage.AutoPremium = (double)optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["AutoPremium"];
            optimaPersonalPackage.UmbrellaPremium = (double)optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["UmbrellaPremium"];
            optimaPersonalPackage.TaskControlIDQuoteAuto = (int)optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["TaskControlIDQuoteAuto"];
            optimaPersonalPackage.PropEnd = optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["PropEnd"].ToString().Trim();
            optimaPersonalPackage.LiabEnd = optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["LiabEnd"].ToString().Trim();
            optimaPersonalPackage.AutoEnd = optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["AutoEnd"].ToString().Trim();
            optimaPersonalPackage.UmbEnd = optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["UmbEnd"].ToString().Trim();
            optimaPersonalPackage.AdditionalName = optimaPersonalPackage._dtOptimaPersonalPackage.Rows[0]["AdditionalName"].ToString().Trim();

            optimaPersonalPackage.PropertiesCollection = Properties.GetPropertiesByTaskControlID(taskControlID, IsOppQuote);

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 1)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection1 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[0]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection1 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[0]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 2)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection2 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[1]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection2 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[1]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 3)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection3 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[2]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection3 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[2]["PropertiesID"], IsOppQuote);

            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 4)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection4 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[3]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection4 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[3]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 5)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection5 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[4]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection5 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[4]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 6)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection6 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[5]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection6 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[5]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 7)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection7 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[6]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection7 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[6]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 8)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection8 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[7]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection8 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[7]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 9)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection9 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[8]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection9 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[8]["PropertiesID"], IsOppQuote);
            }

            if (optimaPersonalPackage.PropertiesCollection.Rows.Count >= 10)
            {
                optimaPersonalPackage.AdditionalCoveragesPropertiesCollection10 = AditionalCoveragecs.GetAditionalCoverageTableTempLoaded((int)optimaPersonalPackage.PropertiesCollection.Rows[9]["PropertiesID"], IsOppQuote);
                optimaPersonalPackage.OPPPropertiesBankCollection10 = Properties.GetOPPPropertiesBankByTaskControlID(optimaPersonalPackage.TaskControlID, (int)optimaPersonalPackage.PropertiesCollection.Rows[9]["PropertiesID"], IsOppQuote);
            }

            optimaPersonalPackage.PersonalLiabilityCollection = PersonalLiability.GetPersonalLiabilityByTaskControlID(taskControlID, IsOppQuote);
            if (optimaPersonalPackage.PersonalLiabilityCollection.Rows.Count >= 1)
            {
                optimaPersonalPackage.AdditionalCoveragesLiabilityCollection = AdditionalCoverageLiability.GetAdditionalCoverageLiabilityTableTempLoaded((int)optimaPersonalPackage.PersonalLiabilityCollection.Rows[0]["PersonalLiabilityID"], IsOppQuote);
            }

            optimaPersonalPackage.Umbrella = Umbrella.GetUmbrellaByTaskControlID(taskControlID, IsOppQuote);

            optimaPersonalPackage.OPPAdditionalCoveragesDetailPropertiesCollection1_1 = Properties.GetOPPAdditionalCoverageDetail(IsOppQuote, taskControlID);

            optimaPersonalPackage.QuoteAuto = EPolicy.TaskControl.QuoteAuto.GetQuoteAuto(optimaPersonalPackage.TaskControlIDQuoteAuto, 1, false);
            optimaPersonalPackage.QuoteAuto.Calculate();

            return optimaPersonalPackage;
        }

        private DataTable DataTableAdditionalCoveragePropertiesTemp()
        {
            DataTable myDataTable = new DataTable("AditionalCoverageTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "AdditionalCoveragesPropertiesID";
            myDataColumn.AutoIncrement = true;
            myDataColumn.Caption = "AdditionalCoveragesPropertiesID";
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = true;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "AdditionalCoveragesID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
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
            myDataColumn.ColumnName = "PropertiesID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PropertiesID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "AdditionalCoveragesDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "AditionalCoverageDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Limits";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Limits";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Deductible";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Deductible";
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
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Description";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);


            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Neto";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Neto";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }
        // TODO Add la parte que falta para que lea los additional coverages 
        private DataTable DataTablePropertiesWithoutLiabilityTemp()
        {
            DataTable myDataTable = new DataTable("DataTablePropertiesTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PropertiesID";
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementSeed = 1;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.AllowDBNull = false;
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = true;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ConstructionType";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ConstructionType";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ExperienceCredit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ExperienceCredit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Bank";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Bank";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "City";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "City";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Building";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Building";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Contents";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Contents";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Another";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Another";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Pool";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Pool";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "LoanNo";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "LoanNo";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Deductible";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Deductible";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Primary";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Primary";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Secondary";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Secondary";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Address1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Address1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Address2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Address2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "State";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "State";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ZipCode";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ZipCode";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Description";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "HomeAssistance";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "HomeAssistance";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumTotal";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumTotal";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumTotal";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumTotal";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Charge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Charge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "SubTotalPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SubTotalPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "TotalPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TotalPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscAccess";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscAccess";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscShutter";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscShutter";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ConstructionYear";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ConstructionYear";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NumOfStories";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NumOfStories";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Residence";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Condo";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Condo";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscUnderwritter";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscUnderwritter";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsLiability";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsLiability";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            //DAVID

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Int32");
            //myDataColumn.ColumnName = "PropertiesID";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "PropertyID";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.String");
            //myDataColumn.ColumnName = "BankID";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "BankID";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.String");
            //myDataColumn.ColumnName = "BankDesc";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "BankDesc";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }

        private DataTable DataTablePropertiesTemp()
        {
            DataTable myDataTable = new DataTable("DataTablePropertiesTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PropertiesID";
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementSeed = 1;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.AllowDBNull = false;
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = true;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ConstructionType";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ConstructionType";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ExperienceCredit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ExperienceCredit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Bank";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Bank";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "City";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "City";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Building";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Building";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Contents";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Contents";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Another";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Another";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Pool";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Pool";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "LoanNo";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "LoanNo";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Deductible";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Deductible";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Primary";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Primary";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Secondary";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Secondary";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Address1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Address1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Address2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Address2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "State";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "State";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ZipCode";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ZipCode";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Description";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "HomeAssistance";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "HomeAssistance";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingLimitTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingLimitTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingRateTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingRateTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingFactorTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingFactorTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingTotalTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingTotalTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsLimitTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsLimitTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumExtCoverage";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumExtCoverage";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumVandalism";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumVandalism";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumEarthquake";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumEarthquake";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumExcessAOP";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumExcessAOP";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsRateTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsRateTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsFactorTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsFactorTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsTotalTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsTotalTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BuildingPremiumTotal";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BuildingPremiumTotal";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ContentsPremiumTotal";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ContentsPremiumTotal";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Charge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Charge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "SubTotalPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SubTotalPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "TotalPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TotalPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscAccess";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscAccess";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscShutter";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscShutter";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscTheft";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscTheft";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DiscFire";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscFire";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "ConstructionYear";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ConstructionYear";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "NumOfStories";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "NumOfStories";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Residence";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Condo";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Condo";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscUnderwritter";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscUnderwritter";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "IsLiability";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IsLiability";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            //DAVID

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Int32");
            //myDataColumn.ColumnName = "PropertiesID";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "PropertyID";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.String");
            //myDataColumn.ColumnName = "BankID";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "BankID";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.String");
            //myDataColumn.ColumnName = "BankDesc";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "BankDesc";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }

        private DataTable DataTableOPPPropertiesBankTemp()
        {
            DataTable myDataTable = new DataTable("DataTableOPPPropertiesBankTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "OPPPropertiesBankID";
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
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PropertiesID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PropertiesID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "BankID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BankID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "BankDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BankDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "LoanNo";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "LoanNo";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            //Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["OPPPropertiesBankID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }

        private DataTable DataTableOPPAdditionalCoverageDetailTemp()
        {
            DataTable myDataTable = new DataTable("DataTableOPPAdditionalCoverageDetailTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "OPPAdditionalCoverageDetailID";
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
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PropertiesID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PropertiesID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "AdditionalCoveragesID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "AdditionalCoveragesID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "AdditionalCoveragesDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "AdditionalCoveragesDesc";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Description";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Limits";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Limits";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "RateValue1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "RateValue1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "RateValue2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "RateValue2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Deductible";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Deductible";
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

            //Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["OPPAdditionalCoverageDetailID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;
        }

        private DataTable DataTablePersonalLiabilityTemp()
        {
            DataTable myDataTable = new DataTable("DataTablePropertiesTemp");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiabilityID";
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementSeed = 1;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.AllowDBNull = false;
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = true;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Residence10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Residence10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property1ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property1ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property2ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property2ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property3ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property3ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property4ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property4ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property5ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property5ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property6ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property6ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property7ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property7ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property8ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property8ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property9ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property9ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Property10ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Property10ID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "SwimmingPool10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SwimmingPool10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Families10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Families10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "MedicalPayment10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPayment10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);


            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "PersonalLiability10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiability10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "Rented10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Rented10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium4";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium4";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium5";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium5";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium6";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium6";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium7";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium7";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium8";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium8";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium9";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium9";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicRate10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicRate10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "IncreaseLimit10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "IncreaseLimit10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "BasicPremium10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "BasicPremium10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "MedicalPrem10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "MedicalPrem10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscountFactor10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscountFactor10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Premium10";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Premium10";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "ExperienceCredit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "ExperienceCredit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "TotalLiabilityPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TotalLiabilityPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "Charge";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Charge";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "SubTotalPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "SubTotalPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "TotalPremium";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "TotalPremium";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "Limit";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Limit";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);


            // ADDED BY POR 5/22/2014 11:12 AM 
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Double");
            myDataColumn.ColumnName = "DiscUnderwritter";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "DiscUnderwritter";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);
            // END 



            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Double");
            //myDataColumn.ColumnName = "TotalPremium";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "TotalPremium";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Double");
            //myDataColumn.ColumnName = "TotalPremium";
            //myDataColumn.AutoIncrement = false;
            //myDataColumn.Caption = "TotalPremium";
            //myDataColumn.ReadOnly = false;
            //myDataColumn.Unique = false;
            //myDataTable.Columns.Add(myDataColumn);


            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = myDataTable.Columns["ID"];
            myDataTable.PrimaryKey = PrimaryKeyColumns;

            return myDataTable;

        }

        private DataTable DataTableAdditionalCoverageLiabilityTemp()
        {
            DataTable myDataTable = new DataTable("AditionalCoverageLiability");
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "AdditionalCoverage2ID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.ReadOnly = true;
            myDataColumn.Unique = false;
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
            myDataColumn.ColumnName = "PersonalLiabilityID";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "PersonalLiabilityID";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "AdditionalCoverageLiabilityDesc";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "AdditionalCoverageLiabilityDesc";
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
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "AddInfo1";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "AddInfo1";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "AddInfo2";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "AddInfo2";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "AddInfo3";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "AddInfo3";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Description";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "Description";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Boolean");
            myDataColumn.ColumnName = "OtherStructure";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "OtherStructure";
            myDataColumn.ReadOnly = false;
            myDataColumn.Unique = false;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "StudentsQty";
            myDataColumn.AutoIncrement = false;
            myDataColumn.Caption = "StudentsQty";
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


                // LO NUEVO PARA LAS OPP 9/15/2014
                EPolicy.TaskControl.PersonalPackage oldOppTaskControl = (EPolicy.TaskControl.PersonalPackage)TaskControl.GetTaskControlByTaskControlID(oldPolices.TaskControlID, userID);
                history.BuildNotesForHistory("OldTotalPremium", oldOppTaskControl.TotalPremium.ToString(), this.TotalPremium.ToString(), mode);
                history.BuildNotesForHistory("OldTotalProperties", oldOppTaskControl.PropertiesPremium.ToString(), this.PropertiesPremium.ToString(), mode);
                history.BuildNotesForHistory("OldTotalPersonalLiability", oldOppTaskControl.LiabilityPremium.ToString(), this.LiabilityPremium.ToString(), mode);
                history.BuildNotesForHistory("OldTotalUmbrella", oldOppTaskControl.UmbrellaPremium.ToString(), this.UmbrellaPremium.ToString(), mode);
                try{history.BuildNotesForHistory("OldAdditionalConverages1", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection1_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection1_1.ToString(), mode);}
                catch (Exception ex) { }
                try{history.BuildNotesForHistory("OldAdditionalCoverages2", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection2_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection2_1.ToString(), mode);}
                catch (Exception ex) { }
                try{history.BuildNotesForHistory("OldAdditionalCoverages3", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection3_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection3_1.ToString(), mode);}
                catch (Exception ex) { }
                try{history.BuildNotesForHistory("OldAdditionalCoverages4", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection4_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection4_1.ToString(), mode);}
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldAdditionalCoverages5", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection5_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection5_1.ToString(), mode);}
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldAdditionalCoverages6", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection6_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection6_1.ToString(), mode);}
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldAdditionalCoverages7", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection7_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection7_1.ToString(), mode);}
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldAdditionalCoverages8", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection8_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection8_1.ToString(), mode);}
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldAdditionalCoverages9", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection9_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection9_1.ToString(), mode);}
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldAdditionalCoverages10", oldOppTaskControl.OPPAdditionalCoveragesDetailPropertiesCollection10_1.ToString(), this.OPPAdditionalCoveragesDetailPropertiesCollection10_1.ToString(), mode); }
                catch (Exception ex) { }
                try{history.BuildNotesForHistory("OldPropertiesContent", oldOppTaskControl.Properties.ContentsPremiumTotal.ToString(), this.Properties.ContentsPremiumTotal.ToString(), mode);}
                catch(Exception ex) { }
                try { history.BuildNotesForHistory("OldPropertiesBuilding", oldOppTaskControl.Properties.BuildingPremiumTotal.ToString(), this.Properties.BuildingPremiumTotal.ToString(), mode); }
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldPropertiesTotal", oldOppTaskControl.PropertiesCollection.Rows.Count.ToString(), this.PropertiesCollection.Rows.Count.ToString(), mode); }
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldAdditionalCoverageLiability", oldOppTaskControl.AdditionalCoveragesLiabilityCollection.ToString(), this.AdditionalCoveragesLiabilityCollection.ToString(), mode); }
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldPropertiesBuilding", oldOppTaskControl.Properties.BuildingPremiumTotal.ToString(), this.Properties.BuildingPremiumTotal.ToString(), mode); }
                catch (Exception ex) { }
                try { history.BuildNotesForHistory("OldPropertiesTotal", oldOppTaskControl.PropertiesCollection.Rows.Count.ToString(), this.PropertiesCollection.Rows.Count.ToString(), mode); }
                catch (Exception ex) { }


                history.BuildNotesForHistory("OldAgency", oldOppTaskControl.Agency.ToString(), this.Agency.ToString(), mode);
                history.BuildNotesForHistory("OldAgent", oldOppTaskControl.Agent.ToString(), this.Agent.ToString(), mode);
                history.BuildNotesForHistory("OLDAgentDesc", oldOppTaskControl.AgentDesc.ToString(), this.AgentDesc.ToString(), mode);
                history.BuildNotesForHistory("OldCharge", oldOppTaskControl.Charge.ToString(), this.Charge.ToString(), mode);
                
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
